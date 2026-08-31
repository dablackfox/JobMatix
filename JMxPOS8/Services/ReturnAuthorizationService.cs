using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services;

public class ReturnAuthorizationService
{
    private readonly DatabaseService _db;

    public ReturnAuthorizationService(DatabaseService db)
    {
        _db = db;
    }

    public async Task<List<ReturnAuthorization>> GetOpenAsync(int limit = 100)
    {
        var results = new List<ReturnAuthorization>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT ra_id, job_id, customerbarcode, customername, supplier_id, suppliername,
                   ranumber, radate, rastatus, origin, itemdescription, rm_stock_id, item_barcode,
                   serial_number, problemdescription, ra_symptoms, rma_request_notes,
                   supplier_rma_no, courier_barcode, return_result, return_result_comment,
                   resolution, staff_name_created, datecreated, date_goods_sent_back,
                   date_goods_received_back, datecompleted
            FROM returnauthorizations
            WHERE rastatus NOT IN ('70-GoodsCompleted', '95-RMA-Refused', '97-RMA-Cancelled')
            ORDER BY ra_id DESC
            LIMIT @limit";
        AddParam(cmd, "@limit", limit);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadRa(reader));
        return results;
    }

    public async Task<ReturnAuthorization> CreateAsync(ReturnAuthorization ra)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO returnauthorizations (
                job_id, customerbarcode, customername, supplier_id, suppliername,
                origin, itemdescription, rm_stock_id, item_barcode, serial_number,
                problemdescription, ra_symptoms, staff_id_created, staff_name_created
            ) VALUES (
                @jobId, @customerBarcode, @customerName, @supplierId, @supplierName,
                @origin, @itemDescription, @rmStockId, @itemBarcode, @serialNumber,
                @problemDescription, @raSymptoms, @staffId, @staffName
            )
            RETURNING ra_id, job_id, customerbarcode, customername, supplier_id, suppliername,
                      ranumber, radate, rastatus, origin, itemdescription, rm_stock_id, item_barcode,
                      serial_number, problemdescription, ra_symptoms, rma_request_notes,
                      supplier_rma_no, courier_barcode, return_result, return_result_comment,
                      resolution, staff_name_created, datecreated, date_goods_sent_back,
                      date_goods_received_back, datecompleted";
        AddParam(cmd, "@jobId", ra.JobId);
        AddParam(cmd, "@customerBarcode", ra.CustomerBarcode);
        AddParam(cmd, "@customerName", ra.CustomerName);
        AddParam(cmd, "@supplierId", ra.SupplierId);
        AddParam(cmd, "@supplierName", ra.SupplierName);
        AddParam(cmd, "@origin", ra.Origin);
        AddParam(cmd, "@itemDescription", ra.ItemDescription);
        AddParam(cmd, "@rmStockId", ra.RmStockId);
        AddParam(cmd, "@itemBarcode", ra.ItemBarcode);
        AddParam(cmd, "@serialNumber", ra.SerialNumber);
        AddParam(cmd, "@problemDescription", ra.ProblemDescription);
        AddParam(cmd, "@raSymptoms", ra.RaSymptoms);
        AddParam(cmd, "@staffId", ra.StaffIdCreated);
        AddParam(cmd, "@staffName", ra.StaffNameCreated);

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        await Task.Run(() => reader.Read());
        return ReadRa(reader);
    }

    public async Task RequestFromSupplierAsync(int raId, string requestNotes)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            UPDATE returnauthorizations
            SET rastatus = '20-RMA-Requested', date_rma_requested = CURRENT_TIMESTAMP,
                rma_request_notes = @notes, date_updated = CURRENT_TIMESTAMP
            WHERE ra_id = @raId AND rastatus = '10-Created'";
        AddParam(cmd, "@notes", requestNotes);
        AddParam(cmd, "@raId", raId);
        if (await Task.Run(() => cmd.ExecuteNonQuery()) == 0)
            throw new InvalidOperationException("RA is not awaiting a supplier request (already progressed, or not found).");
    }

    public async Task GrantRmaAsync(int raId, string supplierRmaNo)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            UPDATE returnauthorizations
            SET rastatus = '30-RMA-Granted', date_rma_response = CURRENT_TIMESTAMP,
                rma_granted = 'Y', supplier_rma_no = @rmaNo, date_updated = CURRENT_TIMESTAMP
            WHERE ra_id = @raId AND rastatus = '20-RMA-Requested'";
        AddParam(cmd, "@rmaNo", supplierRmaNo);
        AddParam(cmd, "@raId", raId);
        if (await Task.Run(() => cmd.ExecuteNonQuery()) == 0)
            throw new InvalidOperationException("RA has not been requested from the supplier yet.");
    }

    // Mirrors the legacy POS_GoodsReturned transaction: the item is leaving the shop for
    // good (going to the supplier), so this permanently decrements stock and, if serialized,
    // marks the serial as returned - it does not go back on the shelf. Only runs the stock
    // side effect when this RA is actually tied to a real stock item (rm_stock_id set) -
    // a Job-origin RA with no stock link is just a status change.
    public async Task SendToSupplierAsync(int raId, string courierBarcode, string staffName)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var transaction = conn.BeginTransaction();

        int? stockId = null;
        string itemBarcode = "", itemDescription = "", serialNumber = "", supplierRmaNo = "";
        int? supplierId = null;
        bool found = false;

        using (var fetchCmd = conn.CreateCommand())
        {
            fetchCmd.Transaction = transaction;
            fetchCmd.CommandText = @"
                SELECT rm_stock_id, item_barcode, itemdescription, serial_number, supplier_rma_no, supplier_id
                FROM returnauthorizations
                WHERE ra_id = @raId AND rastatus = '30-RMA-Granted'
                FOR UPDATE";
            AddParam(fetchCmd, "@raId", raId);
            using var reader = await Task.Run(() => fetchCmd.ExecuteReader());
            found = await Task.Run(() => reader.Read());
            if (found)
            {
                stockId = reader.IsDBNull(0) ? null : reader.GetInt32(0);
                itemBarcode = reader.GetString(1);
                itemDescription = reader.GetString(2);
                serialNumber = reader.GetString(3);
                supplierRmaNo = reader.GetString(4);
                supplierId = reader.IsDBNull(5) ? null : reader.GetInt32(5);
            }
        }
        // Reader must be closed (the using block above) before we can roll back on the
        // same connection - Npgsql doesn't allow starting another operation while a reader
        // from this transaction is still open.
        if (!found)
        {
            transaction.Rollback();
            throw new InvalidOperationException("RA does not have a granted RMA yet.");
        }

        using (var updateCmd = conn.CreateCommand())
        {
            updateCmd.Transaction = transaction;
            updateCmd.CommandText = @"
                UPDATE returnauthorizations
                SET rastatus = '50-GoodsSentToSupplier', date_goods_sent_back = CURRENT_TIMESTAMP,
                    courier_barcode = @courier, date_updated = CURRENT_TIMESTAMP
                WHERE ra_id = @raId";
            AddParam(updateCmd, "@courier", courierBarcode);
            AddParam(updateCmd, "@raId", raId);
            await Task.Run(() => updateCmd.ExecuteNonQuery());
        }

        if (stockId.HasValue)
        {
            int returnId;
            using (var headerCmd = conn.CreateCommand())
            {
                headerCmd.Transaction = transaction;
                headerCmd.CommandText = @"
                    INSERT INTO supplier_returns (staff_name, supplier_id, comments)
                    VALUES (@staffName, @supplierId, @comments)
                    RETURNING return_id";
                AddParam(headerCmd, "@staffName", staffName);
                AddParam(headerCmd, "@supplierId", supplierId);
                AddParam(headerCmd, "@comments", $"RA #{raId} sent to supplier");
                returnId = Convert.ToInt32(await Task.Run(() => headerCmd.ExecuteScalar()));
            }

            int? serialAuditId = null;
            if (!string.IsNullOrWhiteSpace(serialNumber))
            {
                using var findSerialCmd = conn.CreateCommand();
                findSerialCmd.Transaction = transaction;
                findSerialCmd.CommandText = "SELECT serial_id FROM serial_audit WHERE stock_id = @stockId AND serial_number = @serial";
                AddParam(findSerialCmd, "@stockId", stockId.Value);
                AddParam(findSerialCmd, "@serial", serialNumber);
                var result = await Task.Run(() => findSerialCmd.ExecuteScalar());
                serialAuditId = result == null || result is DBNull ? null : Convert.ToInt32(result);
            }

            using (var lineCmd = conn.CreateCommand())
            {
                lineCmd.Transaction = transaction;
                lineCmd.CommandText = @"
                    INSERT INTO supplier_return_line (
                        return_id, stock_id, serial_audit_id, serial_number, ra_id,
                        supplier_rma_no, barcode, description, quantity
                    ) VALUES (
                        @returnId, @stockId, @serialAuditId, @serialNumber, @raId,
                        @supplierRmaNo, @barcode, @description, 1
                    )";
                AddParam(lineCmd, "@returnId", returnId);
                AddParam(lineCmd, "@stockId", stockId.Value);
                AddParam(lineCmd, "@serialAuditId", serialAuditId);
                AddParam(lineCmd, "@serialNumber", serialNumber);
                AddParam(lineCmd, "@raId", raId);
                AddParam(lineCmd, "@supplierRmaNo", supplierRmaNo);
                AddParam(lineCmd, "@barcode", itemBarcode);
                AddParam(lineCmd, "@description", itemDescription);
                await Task.Run(() => lineCmd.ExecuteNonQuery());
            }

            using (var stockCmd = conn.CreateCommand())
            {
                stockCmd.Transaction = transaction;
                stockCmd.CommandText = "UPDATE stock SET quantityinstock = quantityinstock - 1 WHERE stock_id = @stockId";
                AddParam(stockCmd, "@stockId", stockId.Value);
                await Task.Run(() => stockCmd.ExecuteNonQuery());
            }

            if (serialAuditId.HasValue)
            {
                using (var serialCmd = conn.CreateCommand())
                {
                    serialCmd.Transaction = transaction;
                    serialCmd.CommandText = @"
                        UPDATE serial_audit
                        SET status = 'RETURNED', is_in_stock = false, date_modified = CURRENT_TIMESTAMP
                        WHERE serial_id = @serialAuditId";
                    AddParam(serialCmd, "@serialAuditId", serialAuditId.Value);
                    await Task.Run(() => serialCmd.ExecuteNonQuery());
                }

                using (var trailCmd = conn.CreateCommand())
                {
                    trailCmd.Transaction = transaction;
                    trailCmd.CommandText = @"
                        INSERT INTO serial_audit_trail (stock_id, serial_audit_id, tran_type, movement, rm_tr_detail)
                        VALUES (@stockId, @serialAuditId, 'return', -1, @detail)";
                    AddParam(trailCmd, "@stockId", stockId.Value);
                    AddParam(trailCmd, "@serialAuditId", serialAuditId.Value);
                    AddParam(trailCmd, "@detail", $"RA #{raId} returned to supplier");
                    await Task.Run(() => trailCmd.ExecuteNonQuery());
                }
            }
        }

        transaction.Commit();
    }

    public async Task CompleteAsync(int raId, string returnResult, string returnResultComment, bool goodsReceivedBack)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            UPDATE returnauthorizations
            SET rastatus = '70-GoodsCompleted', datecompleted = CURRENT_TIMESTAMP,
                return_result = @result, return_result_comment = @comment,
                date_goods_received_back = CASE WHEN @receivedBack THEN CURRENT_TIMESTAMP ELSE date_goods_received_back END,
                date_updated = CURRENT_TIMESTAMP
            WHERE ra_id = @raId AND rastatus = '50-GoodsSentToSupplier'";
        AddParam(cmd, "@result", returnResult);
        AddParam(cmd, "@comment", returnResultComment);
        AddParam(cmd, "@receivedBack", goodsReceivedBack);
        AddParam(cmd, "@raId", raId);
        if (await Task.Run(() => cmd.ExecuteNonQuery()) == 0)
            throw new InvalidOperationException("RA has not been sent to the supplier yet.");
    }

    public async Task RefuseAsync(int raId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            UPDATE returnauthorizations
            SET rastatus = '95-RMA-Refused', date_updated = CURRENT_TIMESTAMP
            WHERE ra_id = @raId AND rastatus NOT IN ('70-GoodsCompleted', '97-RMA-Cancelled')";
        AddParam(cmd, "@raId", raId);
        if (await Task.Run(() => cmd.ExecuteNonQuery()) == 0)
            throw new InvalidOperationException("RA is already closed out.");
    }

    public async Task CancelAsync(int raId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            UPDATE returnauthorizations
            SET rastatus = '97-RMA-Cancelled', date_updated = CURRENT_TIMESTAMP
            WHERE ra_id = @raId AND rastatus NOT IN ('70-GoodsCompleted', '95-RMA-Refused')";
        AddParam(cmd, "@raId", raId);
        if (await Task.Run(() => cmd.ExecuteNonQuery()) == 0)
            throw new InvalidOperationException("RA is already closed out.");
    }

    private static ReturnAuthorization ReadRa(System.Data.IDataReader reader) => new()
    {
        RaId = reader.GetInt32(0),
        JobId = reader.IsDBNull(1) ? null : reader.GetInt32(1),
        CustomerBarcode = reader.GetString(2),
        CustomerName = reader.GetString(3),
        SupplierId = reader.IsDBNull(4) ? null : reader.GetInt32(4),
        SupplierName = reader.GetString(5),
        RaNumber = reader.GetString(6),
        RaDate = reader.GetDateTime(7),
        RaStatus = reader.GetString(8),
        Origin = reader.GetString(9),
        ItemDescription = reader.GetString(10),
        RmStockId = reader.IsDBNull(11) ? null : reader.GetInt32(11),
        ItemBarcode = reader.GetString(12),
        SerialNumber = reader.GetString(13),
        ProblemDescription = reader.GetString(14),
        RaSymptoms = reader.GetString(15),
        RmaRequestNotes = reader.GetString(16),
        SupplierRmaNo = reader.GetString(17),
        CourierBarcode = reader.GetString(18),
        ReturnResult = reader.GetString(19),
        ReturnResultComment = reader.GetString(20),
        Resolution = reader.GetString(21),
        StaffNameCreated = reader.GetString(22),
        DateCreated = reader.GetDateTime(23),
        DateGoodsSentBack = reader.IsDBNull(24) ? null : reader.GetDateTime(24),
        DateGoodsReceivedBack = reader.IsDBNull(25) ? null : reader.GetDateTime(25),
        DateCompleted = reader.IsDBNull(26) ? null : reader.GetDateTime(26)
    };

    private static void AddParam(System.Data.IDbCommand cmd, string name, object? value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value ?? DBNull.Value;
        cmd.Parameters.Add(param);
    }
}
