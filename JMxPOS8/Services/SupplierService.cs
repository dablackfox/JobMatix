using System.Collections.Generic;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services;

public class SupplierService
{
    private readonly DatabaseService _db;

    public SupplierService(DatabaseService db)
    {
        _db = db;
    }

    public async Task<Supplier?> FindSupplierByBarcodeAsync(string barcode)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT supplier_id, barcode, suppliername, contactname, businessphone, emailaddress, inactive
            FROM supplier
            WHERE barcode = @barcode AND supplier_id > 0
            LIMIT 1";
        var param = cmd.CreateParameter();
        param.ParameterName = "@barcode";
        param.Value = barcode;
        cmd.Parameters.Add(param);

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        if (!await Task.Run(() => reader.Read()))
            return null;
        return ReadSupplier(reader);
    }

    public async Task<List<Supplier>> SearchSuppliersAsync(string searchTerm, int limit = 50)
    {
        var results = new List<Supplier>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $@"
            SELECT supplier_id, barcode, suppliername, contactname, businessphone, emailaddress, inactive
            FROM supplier
            WHERE supplier_id > 0 AND inactive = false
              AND (LOWER(suppliername) LIKE LOWER(@search) OR LOWER(barcode) LIKE LOWER(@search))
            ORDER BY suppliername
            LIMIT {limit}";
        var param = cmd.CreateParameter();
        param.ParameterName = "@search";
        param.Value = $"%{searchTerm}%";
        cmd.Parameters.Add(param);

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadSupplier(reader));
        return results;
    }

    private static Supplier ReadSupplier(System.Data.IDataReader reader) => new()
    {
        SupplierId = reader.GetInt32(0),
        Barcode = reader.GetString(1),
        SupplierName = reader.GetString(2),
        ContactName = reader.GetString(3),
        BusinessPhone = reader.GetString(4),
        EmailAddress = reader.GetString(5),
        Inactive = reader.GetBoolean(6)
    };
}
