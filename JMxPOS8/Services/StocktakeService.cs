using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services;

public class StocktakeService
{
    private readonly DatabaseService _db;
    private readonly StockService _stockService;

    public StocktakeService(DatabaseService db, StockService stockService)
    {
        _db = db;
        _stockService = stockService;
    }

    public async Task<List<StocktakeSession>> GetOpenStocktakesAsync()
    {
        var results = new List<StocktakeSession>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT stocktake_id, stocktake_type, is_committed, is_cancelled, date_created,
                   created_staff_name, date_committed, committed_staff_name, comments
            FROM stocktake
            WHERE is_committed = false AND is_cancelled = false
            ORDER BY date_created DESC";
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadSession(reader));
        return results;
    }

    public async Task<List<StocktakeSession>> GetRecentStocktakesAsync(int limit = 50)
    {
        var results = new List<StocktakeSession>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT stocktake_id, stocktake_type, is_committed, is_cancelled, date_created,
                   created_staff_name, date_committed, committed_staff_name, comments
            FROM stocktake
            ORDER BY date_created DESC
            LIMIT @limit";
        AddParam(cmd, "@limit", limit);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadSession(reader));
        return results;
    }

    public async Task<StocktakeSession> CreateStocktakeAsync(string createdStaffName)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO stocktake (stocktake_type, created_staff_name, modified_staff_name)
            VALUES ('FULL', @staffName, @staffName)
            RETURNING stocktake_id, stocktake_type, is_committed, is_cancelled, date_created,
                      created_staff_name, date_committed, committed_staff_name, comments";
        AddParam(cmd, "@staffName", createdStaffName);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        await Task.Run(() => reader.Read());
        return ReadSession(reader);
    }

    public async Task<List<StocktakeItem>> GetStocktakeItemsAsync(int stocktakeId)
    {
        var results = new List<StocktakeItem>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT item_id, stocktake_id, stock_id, barcode, description,
                   qty_on_record, qty_counted, qty_difference
            FROM stocktake_items
            WHERE stocktake_id = @stocktakeId
            ORDER BY description";
        AddParam(cmd, "@stocktakeId", stocktakeId);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            results.Add(new StocktakeItem
            {
                ItemId = reader.GetInt32(0),
                StocktakeId = reader.GetInt32(1),
                StockId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                Barcode = reader.GetString(3),
                Description = reader.GetString(4),
                QtyOnRecord = reader.GetInt32(5),
                QtyCounted = reader.GetInt32(6),
                QtyDifference = reader.GetInt32(7)
            });
        }
        return results;
    }

    public enum ScanResult { Counted, NotFound }

    // Scanning the same item again just adds another unit to the count, matching how a
    // physical stocktake actually happens (walk the shelf, scan every unit you see).
    public async Task<ScanResult> ScanItemAsync(int stocktakeId, string barcode)
    {
        var stock = await _stockService.FindStockByBarcodeAsync(barcode);
        if (stock == null)
            return ScanResult.NotFound;

        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());

        int? existingItemId = null;
        int existingCounted = 0;
        using (var findCmd = conn.CreateCommand())
        {
            findCmd.CommandText = "SELECT item_id, qty_counted FROM stocktake_items WHERE stocktake_id = @stocktakeId AND stock_id = @stockId";
            AddParam(findCmd, "@stocktakeId", stocktakeId);
            AddParam(findCmd, "@stockId", stock.StockId);
            using var reader = await Task.Run(() => findCmd.ExecuteReader());
            if (await Task.Run(() => reader.Read()))
            {
                existingItemId = reader.GetInt32(0);
                existingCounted = reader.GetInt32(1);
            }
        }

        if (existingItemId.HasValue)
        {
            using var updateCmd = conn.CreateCommand();
            updateCmd.CommandText = @"
                UPDATE stocktake_items
                SET qty_counted = @qtyCounted,
                    qty_difference = @qtyCounted - qty_on_record,
                    date_modified = CURRENT_TIMESTAMP
                WHERE item_id = @itemId";
            AddParam(updateCmd, "@qtyCounted", existingCounted + 1);
            AddParam(updateCmd, "@itemId", existingItemId.Value);
            await Task.Run(() => updateCmd.ExecuteNonQuery());
        }
        else
        {
            int qtyOnRecord = (int)Math.Round(stock.QuantityInStock);
            using var insertCmd = conn.CreateCommand();
            insertCmd.CommandText = @"
                INSERT INTO stocktake_items (
                    stocktake_id, stock_id, barcode, description,
                    qty_on_record, qty_counted, qty_difference
                ) VALUES (
                    @stocktakeId, @stockId, @barcode, @description,
                    @qtyOnRecord, 1, 1 - @qtyOnRecord
                )";
            AddParam(insertCmd, "@stocktakeId", stocktakeId);
            AddParam(insertCmd, "@stockId", stock.StockId);
            AddParam(insertCmd, "@barcode", stock.Barcode);
            AddParam(insertCmd, "@description", stock.Description);
            AddParam(insertCmd, "@qtyOnRecord", qtyOnRecord);
            await Task.Run(() => insertCmd.ExecuteNonQuery());
        }

        return ScanResult.Counted;
    }

    // Adjusts stock.quantityinstock to match what was actually counted for every line in
    // this session, then closes it out. Skips lines with no difference - no point writing
    // a no-op stock update for every item that matched.
    public async Task CommitStocktakeAsync(int stocktakeId, string committedStaffName)
    {
        var items = await GetStocktakeItemsAsync(stocktakeId);

        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var transaction = conn.BeginTransaction();

        foreach (var item in items.Where(i => i.QtyDifference != 0))
        {
            using var cmd = conn.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = "UPDATE stock SET quantityinstock = @qty WHERE stock_id = @stockId";
            AddParam(cmd, "@qty", item.QtyCounted);
            AddParam(cmd, "@stockId", item.StockId);
            await Task.Run(() => cmd.ExecuteNonQuery());
        }

        using (var closeCmd = conn.CreateCommand())
        {
            closeCmd.Transaction = transaction;
            closeCmd.CommandText = @"
                UPDATE stocktake
                SET is_committed = true, date_committed = @now, committed_staff_name = @staffName
                WHERE stocktake_id = @stocktakeId";
            AddParam(closeCmd, "@now", DateTime.Now);
            AddParam(closeCmd, "@staffName", committedStaffName);
            AddParam(closeCmd, "@stocktakeId", stocktakeId);
            await Task.Run(() => closeCmd.ExecuteNonQuery());
        }

        transaction.Commit();
    }

    public async Task CancelStocktakeAsync(int stocktakeId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE stocktake SET is_cancelled = true WHERE stocktake_id = @stocktakeId";
        AddParam(cmd, "@stocktakeId", stocktakeId);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    private static StocktakeSession ReadSession(System.Data.IDataReader reader) => new()
    {
        StocktakeId = reader.GetInt32(0),
        StocktakeType = reader.GetString(1),
        IsCommitted = reader.GetBoolean(2),
        IsCancelled = reader.GetBoolean(3),
        DateCreated = reader.GetDateTime(4),
        CreatedStaffName = reader.GetString(5),
        DateCommitted = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
        CommittedStaffName = reader.GetString(7),
        Comments = reader.GetString(8)
    };

    private static void AddParam(System.Data.IDbCommand cmd, string name, object value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        cmd.Parameters.Add(param);
    }
}
