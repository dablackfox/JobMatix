using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services;

public class GoodsReceivedService
{
    private const decimal GstRate = 0.10m;

    private readonly DatabaseService _db;

    public GoodsReceivedService(DatabaseService db)
    {
        _db = db;
    }

    public async Task<List<GoodsReceivedSummary>> GetRecentAsync(int limit = 50)
    {
        var results = new List<GoodsReceivedSummary>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $@"
            SELECT g.goods_id, g.goods_date, s.suppliername, g.invoice_no, g.total_inc
            FROM goods_received g
            JOIN supplier s ON s.supplier_id = g.supplier_id
            ORDER BY g.goods_date DESC
            LIMIT {limit}";
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            results.Add(new GoodsReceivedSummary
            {
                GoodsId = reader.GetInt32(0),
                GoodsDate = reader.GetDateTime(1),
                SupplierName = reader.GetString(2),
                InvoiceNo = reader.GetString(3),
                TotalInc = reader.GetDecimal(4)
            });
        }
        return results;
    }

    // Writes the header, every line, and applies the stock/cost-price update all at once -
    // goods_received has no draft/committed distinction in the schema (unlike Stocktake), so
    // this is only ever called once the operator has finished entering the whole delivery.
    // Warnings is non-fatal feedback (e.g. a serial count that doesn't match the quantity
    // received, or a serial that's already on hand) - the receipt still goes through, since
    // withholding a whole delivery over a label-entry mismatch would be worse than the gap.
    public async Task<(int GoodsId, List<string> Warnings)> ReceiveGoodsAsync(int supplierId, int staffId, string invoiceNo,
        DateTime invoiceDate, IReadOnlyList<GoodsReceivedLine> lines, string comments)
    {
        var warnings = new List<string>();
        decimal subtotalEx = 0, subtotalTax = 0, subtotalInc = 0;
        foreach (var line in lines)
        {
            subtotalEx += line.LineTotalEx;
        }
        subtotalTax = Math.Round(subtotalEx * GstRate, 2);
        subtotalInc = subtotalEx + subtotalTax;

        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var transaction = conn.BeginTransaction();

        int goodsId;
        using (var cmd = conn.CreateCommand())
        {
            cmd.Transaction = transaction;
            cmd.CommandText = @"
                INSERT INTO goods_received (
                    staff_id, supplier_id, invoice_no, invoice_date,
                    subtotal_ex, subtotal_tax, subtotal_inc,
                    total_ex, total_tax, total_inc, total_expected, comments
                ) VALUES (
                    @staffId, @supplierId, @invoiceNo, @invoiceDate,
                    @subtotalEx, @subtotalTax, @subtotalInc,
                    @subtotalEx, @subtotalTax, @subtotalInc, @subtotalInc, @comments
                )
                RETURNING goods_id";
            AddParam(cmd, "@staffId", staffId);
            AddParam(cmd, "@supplierId", supplierId);
            AddParam(cmd, "@invoiceNo", invoiceNo);
            AddParam(cmd, "@invoiceDate", invoiceDate);
            AddParam(cmd, "@subtotalEx", subtotalEx);
            AddParam(cmd, "@subtotalTax", subtotalTax);
            AddParam(cmd, "@subtotalInc", subtotalInc);
            AddParam(cmd, "@comments", comments);
            goodsId = Convert.ToInt32(await Task.Run(() => cmd.ExecuteScalar()));
        }

        foreach (var line in lines)
        {
            decimal lineTax = Math.Round(line.LineTotalEx * GstRate, 2);
            int lineId;

            using (var lineCmd = conn.CreateCommand())
            {
                lineCmd.Transaction = transaction;
                lineCmd.CommandText = @"
                    INSERT INTO goods_received_line (
                        goods_id, stock_id, goods_tax_code, goods_tax_percentage,
                        cost_ex, cost_tax, cost_inc, sell_ex, quantity,
                        total_ex, total_tax, total_inc
                    ) VALUES (
                        @goodsId, @stockId, 'GST', @gstPercent,
                        @costEx, @costTax, @costInc, 0, @quantity,
                        @totalEx, @totalTax, @totalInc
                    )
                    RETURNING line_id";
                AddParam(lineCmd, "@goodsId", goodsId);
                AddParam(lineCmd, "@stockId", line.StockId);
                AddParam(lineCmd, "@gstPercent", GstRate * 100);
                AddParam(lineCmd, "@costEx", line.CostEx);
                AddParam(lineCmd, "@costTax", Math.Round(line.CostEx * GstRate, 2));
                AddParam(lineCmd, "@costInc", line.CostEx + Math.Round(line.CostEx * GstRate, 2));
                AddParam(lineCmd, "@quantity", (int)Math.Round(line.Quantity));
                AddParam(lineCmd, "@totalEx", line.LineTotalEx);
                AddParam(lineCmd, "@totalTax", lineTax);
                AddParam(lineCmd, "@totalInc", line.LineTotalEx + lineTax);
                lineId = Convert.ToInt32(await Task.Run(() => lineCmd.ExecuteScalar()));
            }

            // Receiving stock both adds to on-hand quantity and refreshes the cost price to
            // what was actually paid this time - the same "latest cost wins" convention the
            // rest of the app already uses for costprice as the *default sell-price basis*.
            // Per-unit COGS reporting for serialized items no longer depends on this field -
            // see the serial_audit.unit_cost writes below (ROADMAP.md Phase 6.1).
            using (var stockCmd = conn.CreateCommand())
            {
                stockCmd.Transaction = transaction;
                stockCmd.CommandText = @"
                    UPDATE stock
                    SET quantityinstock = quantityinstock + @qty,
                        costprice = @costEx
                    WHERE stock_id = @stockId";
                AddParam(stockCmd, "@qty", line.Quantity);
                AddParam(stockCmd, "@costEx", line.CostEx);
                AddParam(stockCmd, "@stockId", line.StockId);
                await Task.Run(() => stockCmd.ExecuteNonQuery());
            }

            if (line.RequiresSerial)
            {
                var serials = line.ParseSerialNumbers();
                if (serials.Count == 0)
                {
                    warnings.Add($"'{line.Description}' requires serials but none were entered - stock and cost were still recorded.");
                }
                else if (serials.Count != (int)Math.Round(line.Quantity))
                {
                    warnings.Add($"'{line.Description}': {serials.Count} serial(s) entered but {line.Quantity:N0} received.");
                }

                foreach (var serial in serials)
                {
                    // A serial still on hand elsewhere (typo, or genuinely re-received) isn't
                    // fatal to the whole delivery - flag it and skip creating a duplicate row.
                    using (var checkCmd = conn.CreateCommand())
                    {
                        checkCmd.Transaction = transaction;
                        checkCmd.CommandText = "SELECT COUNT(*) FROM serial_audit WHERE serial_number = @serial AND is_in_stock = true";
                        AddParam(checkCmd, "@serial", serial);
                        var alreadyInStock = Convert.ToInt32(await Task.Run(() => checkCmd.ExecuteScalar())) > 0;
                        if (alreadyInStock)
                        {
                            warnings.Add($"Serial '{serial}' is already recorded as in stock - skipped.");
                            continue;
                        }
                    }

                    int serialId;
                    using (var serialCmd = conn.CreateCommand())
                    {
                        serialCmd.Transaction = transaction;
                        serialCmd.CommandText = @"
                            INSERT INTO serial_audit (stock_id, serial_number, is_in_stock, status, unit_cost, received_line_id)
                            VALUES (@stockId, @serial, true, 'IN_STOCK', @unitCost, @lineId)
                            RETURNING serial_id";
                        AddParam(serialCmd, "@stockId", line.StockId);
                        AddParam(serialCmd, "@serial", serial);
                        AddParam(serialCmd, "@unitCost", line.CostEx);
                        AddParam(serialCmd, "@lineId", lineId);
                        serialId = Convert.ToInt32(await Task.Run(() => serialCmd.ExecuteScalar()));
                    }

                    using (var trailCmd = conn.CreateCommand())
                    {
                        trailCmd.Transaction = transaction;
                        trailCmd.CommandText = @"
                            INSERT INTO serial_audit_trail (stock_id, serial_audit_id, tran_type, type_id, type_line_id, movement, rm_tr_detail)
                            VALUES (@stockId, @serialId, 'RECEIVED', @goodsId, @lineId, 1, @detail)";
                        AddParam(trailCmd, "@stockId", line.StockId);
                        AddParam(trailCmd, "@serialId", serialId);
                        AddParam(trailCmd, "@goodsId", goodsId);
                        AddParam(trailCmd, "@lineId", lineId);
                        AddParam(trailCmd, "@detail", $"Received via Goods Received #{goodsId}, invoice {invoiceNo}");
                        await Task.Run(() => trailCmd.ExecuteNonQuery());
                    }
                }
            }
        }

        transaction.Commit();
        return (goodsId, warnings);
    }

    private static void AddParam(System.Data.IDbCommand cmd, string name, object value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        cmd.Parameters.Add(param);
    }
}
