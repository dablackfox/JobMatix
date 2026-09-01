using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JMxPOS8.Services
{
    public class SerialSaleInfo
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public DateTime InvoiceDate { get; set; }
        public string TransactionType { get; set; } = "";
        public string StockDescription { get; set; } = "";
        public string CustomerName { get; set; } = "";
    }

    public class SerialService
    {
        private readonly DatabaseService _db;

        public SerialService(DatabaseService db)
        {
            _db = db;
        }

        // Most recent invoice line (of any transaction type) carrying this serial number, if any.
        // Used both for "is this serial already sold" checks and for warranty/history lookup.
        public async Task<SerialSaleInfo?> FindLatestBySerialAsync(string serialNumber)
        {
            using var conn = _db.GetConnection();
            await Task.Run(() => conn.Open());
            using var cmd = conn.CreateCommand();
            // Historical (migrated) rows populate serial_number, not serialnumber - the new
            // app writes the latter (see SaleService.CommitSaleAsync). Check both so a serial
            // sold before this port existed is still found (ROADMAP.md Phase 6.1).
            cmd.CommandText = @"
                SELECT inv.invoice_id, inv.invoicenumber, inv.invoicedate, inv.transactiontype,
                       st.description,
                       COALESCE(c.companyname, c.customername) as customer_name
                FROM invoice_lines il
                JOIN invoice inv ON inv.invoice_id = il.invoice_id
                JOIN stock st ON st.stock_id = il.stock_id
                LEFT JOIN customer c ON c.customer_id = inv.customer_id
                WHERE il.serialnumber = @serial OR il.serial_number = @serial
                ORDER BY inv.invoicedate DESC, inv.invoice_id DESC
                LIMIT 1";

            var param = cmd.CreateParameter();
            param.ParameterName = "@serial";
            param.Value = serialNumber;
            cmd.Parameters.Add(param);

            using var reader = await Task.Run(() => cmd.ExecuteReader());
            if (await Task.Run(() => reader.Read()))
            {
                return new SerialSaleInfo
                {
                    InvoiceId = Convert.ToInt32(reader[0]),
                    InvoiceNumber = reader[1]?.ToString() ?? "",
                    InvoiceDate = Convert.ToDateTime(reader[2]),
                    TransactionType = reader[3]?.ToString() ?? "",
                    StockDescription = reader[4]?.ToString() ?? "",
                    CustomerName = reader.IsDBNull(5) ? "Walk-in" : reader.GetString(5)
                };
            }

            return null;
        }

        // A serial counts as "already sold" if the most recent transaction referencing it
        // was a Sale (i.e. it hasn't since been returned/refunded).
        public async Task<bool> IsSerialCurrentlySoldAsync(string serialNumber)
        {
            var latest = await FindLatestBySerialAsync(serialNumber);
            return latest != null && string.Equals(latest.TransactionType, "SALE", StringComparison.OrdinalIgnoreCase);
        }

        // Scanning a serial number (instead of the product's own barcode) into the Sale
        // tab's item-search box should still find the right product - direct feedback
        // (2026-09-01): "since we're setting up serial numbers for everything, scanning a
        // serial number into the barcode/product search section should auto select the
        // product and fill in the serial field." Only matches a serial still on hand
        // (is_in_stock = true), same as GetAvailableSerialsAsync - a serial already sold
        // shouldn't silently resolve to "sell this product" again.
        public async Task<int?> FindStockIdBySerialAsync(string serialNumber)
        {
            using var conn = _db.GetConnection();
            await Task.Run(() => conn.Open());
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT stock_id FROM serial_audit WHERE serial_number = @serial AND is_in_stock = true LIMIT 1";

            var param = cmd.CreateParameter();
            param.ParameterName = "@serial";
            param.Value = serialNumber;
            cmd.Parameters.Add(param);

            var result = await Task.Run(() => cmd.ExecuteScalar());
            return result == null || result is DBNull ? null : Convert.ToInt32(result);
        }

        // Serial numbers physically on hand for a given stock item, for the "pick from what's
        // actually in stock" autofill on the Sale tab - mirrors what the old POS's serial
        // picker modal showed, backed by the imported serial_audit table.
        public async Task<List<string>> GetAvailableSerialsAsync(int stockId, string? search, int limit = 20)
        {
            var results = new List<string>();

            using var conn = _db.GetConnection();
            await Task.Run(() => conn.Open());
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT serial_number
                FROM serial_audit
                WHERE stock_id = @stockId
                  AND is_in_stock = true
                  AND (@search = '' OR serial_number ILIKE @searchPattern)
                ORDER BY serial_number
                LIMIT @limit";

            var stockIdParam = cmd.CreateParameter();
            stockIdParam.ParameterName = "@stockId";
            stockIdParam.Value = stockId;
            cmd.Parameters.Add(stockIdParam);

            var searchParam = cmd.CreateParameter();
            searchParam.ParameterName = "@search";
            searchParam.Value = search ?? "";
            cmd.Parameters.Add(searchParam);

            var searchPatternParam = cmd.CreateParameter();
            searchPatternParam.ParameterName = "@searchPattern";
            searchPatternParam.Value = $"%{search}%";
            cmd.Parameters.Add(searchPatternParam);

            var limitParam = cmd.CreateParameter();
            limitParam.ParameterName = "@limit";
            limitParam.Value = limit;
            cmd.Parameters.Add(limitParam);

            using var reader = await Task.Run(() => cmd.ExecuteReader());
            while (await Task.Run(() => reader.Read()))
                results.Add(reader.GetString(0));

            return results;
        }
    }
}
