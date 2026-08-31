using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services;

public class JobService
{
    private readonly DatabaseService _db;

    public JobService(DatabaseService db)
    {
        _db = db;
    }

    public async Task<List<JobRecord>> GetOpenJobsAsync(int limit = 200)
    {
        var results = new List<JobRecord>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT job_id, customerbarcode, rmcustomer_id, customername, customerphone, customermobile,
                   priority, nominatedtech, jobstatus, goodsincare, goodsbrand, goodsmodel,
                   databackupreqd, datadiskreqd, problemshort, problemlong, problemsymptoms,
                   systemunderwarranty, datecreated, rcvdstaffname, diagnosis, servicenotes,
                   datecompleted, techstaffname, techrmstaff_id, datedelivered, deliveredstaffname, dateupdated
            FROM jobs
            WHERE jobstatus NOT IN ('70-Delivered', '97-Cancelled')
            ORDER BY job_id DESC
            LIMIT @limit";
        AddParam(cmd, "@limit", limit);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadJob(reader));
        return results;
    }

    public async Task<JobRecord?> GetJobByIdAsync(int jobId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT job_id, customerbarcode, rmcustomer_id, customername, customerphone, customermobile,
                   priority, nominatedtech, jobstatus, goodsincare, goodsbrand, goodsmodel,
                   databackupreqd, datadiskreqd, problemshort, problemlong, problemsymptoms,
                   systemunderwarranty, datecreated, rcvdstaffname, diagnosis, servicenotes,
                   datecompleted, techstaffname, techrmstaff_id, datedelivered, deliveredstaffname, dateupdated
            FROM jobs
            WHERE job_id = @jobId";
        AddParam(cmd, "@jobId", jobId);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        if (!await Task.Run(() => reader.Read()))
            return null;
        return ReadJob(reader);
    }

    public async Task<JobRecord> CreateJobAsync(JobRecord job)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO jobs (
                customerbarcode, rmcustomer_id, customername, customerphone, customermobile,
                priority, nominatedtech, goodsincare, goodsbrand, goodsmodel,
                databackupreqd, datadiskreqd, problemshort, problemlong, problemsymptoms,
                systemunderwarranty, rcvdstaffname
            ) VALUES (
                @customerBarcode, @rmCustomerId, @customerName, @customerPhone, @customerMobile,
                @priority, @nominatedTech, @goodsInCare, @goodsBrand, @goodsModel,
                @dataBackupReqd, @dataDiskReqd, @problemShort, @problemLong, @problemSymptoms,
                @systemUnderWarranty, @rcvdStaffName
            )
            RETURNING job_id, customerbarcode, rmcustomer_id, customername, customerphone, customermobile,
                      priority, nominatedtech, jobstatus, goodsincare, goodsbrand, goodsmodel,
                      databackupreqd, datadiskreqd, problemshort, problemlong, problemsymptoms,
                      systemunderwarranty, datecreated, rcvdstaffname, diagnosis, servicenotes,
                      datecompleted, techstaffname, techrmstaff_id, datedelivered, deliveredstaffname, dateupdated";
        AddParam(cmd, "@customerBarcode", job.CustomerBarcode);
        AddParam(cmd, "@rmCustomerId", job.RmCustomerId);
        AddParam(cmd, "@customerName", job.CustomerName);
        AddParam(cmd, "@customerPhone", job.CustomerPhone);
        AddParam(cmd, "@customerMobile", job.CustomerMobile);
        AddParam(cmd, "@priority", job.Priority);
        AddParam(cmd, "@nominatedTech", job.NominatedTech);
        AddParam(cmd, "@goodsInCare", job.GoodsInCare);
        AddParam(cmd, "@goodsBrand", job.GoodsBrand);
        AddParam(cmd, "@goodsModel", job.GoodsModel);
        AddParam(cmd, "@dataBackupReqd", job.DataBackupReqd ? "Y" : "N");
        AddParam(cmd, "@dataDiskReqd", job.DataDiskReqd ? "Y" : "N");
        AddParam(cmd, "@problemShort", job.ProblemShort);
        AddParam(cmd, "@problemLong", job.ProblemLong);
        AddParam(cmd, "@problemSymptoms", job.ProblemSymptoms);
        AddParam(cmd, "@systemUnderWarranty", job.SystemUnderWarranty);
        AddParam(cmd, "@rcvdStaffName", job.RcvdStaffName);

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        await Task.Run(() => reader.Read());
        return ReadJob(reader);
    }

    // Opening a job for edit flips a stable status to its locked "InProcess" variant, so
    // anyone else looking at the job list sees it's actively being worked on - a real
    // optimistic-locking mechanism carried over from the legacy app (ROADMAP.md Phase 3),
    // not cosmetic. No-op for statuses that have no locked variant (Created/Completed/etc).
    public async Task<string> OpenForEditAsync(int jobId)
    {
        return await TransitionAsync(jobId, new Dictionary<string, string>
        {
            ["20-Suspended"] = "23-InProcessSusp",
            ["30-Started"] = "33-InProcess",
            ["40-QA"] = "43-InProcessQA"
        });
    }

    public async Task<string> CloseEditAsync(int jobId)
    {
        return await TransitionAsync(jobId, new Dictionary<string, string>
        {
            ["23-InProcessSusp"] = "20-Suspended",
            ["33-InProcess"] = "30-Started",
            ["43-InProcessQA"] = "40-QA"
        });
    }

    public async Task StartWorkAsync(int jobId, string techStaffName, int? techStaffId)
    {
        await RequireTransitionAsync(jobId, new[] { "10-Created", "20-Suspended", "23-InProcessSusp" }, "30-Started",
            extraSql: "techstaffname = @techName, techrmstaff_id = @techId",
            extraParams: cmd =>
            {
                AddParam(cmd, "@techName", techStaffName);
                AddParam(cmd, "@techId", techStaffId);
            });
    }

    public async Task SuspendAsync(int jobId)
        => await RequireTransitionAsync(jobId, new[] { "30-Started", "33-InProcess" }, "20-Suspended");

    public async Task SendToQaAsync(int jobId)
        => await RequireTransitionAsync(jobId, new[] { "30-Started", "33-InProcess" }, "40-QA");

    public async Task ReopenFromQaAsync(int jobId)
        => await RequireTransitionAsync(jobId, new[] { "40-QA", "43-InProcessQA" }, "30-Started");

    public async Task CompleteAsync(int jobId, string serviceNotes)
    {
        await RequireTransitionAsync(jobId, new[] { "40-QA", "43-InProcessQA" }, "50-Completed",
            extraSql: "datecompleted = CURRENT_TIMESTAMP, servicenotes = @notes",
            extraParams: cmd => AddParam(cmd, "@notes", serviceNotes));
    }

    public async Task DeliverAsync(int jobId, string deliveredStaffName, int? deliveredStaffId)
    {
        await RequireTransitionAsync(jobId, new[] { "50-Completed" }, "70-Delivered",
            extraSql: "datedelivered = CURRENT_TIMESTAMP, deliveredstaffname = @staffName, deliveredrmstaff_id = @staffId",
            extraParams: cmd =>
            {
                AddParam(cmd, "@staffName", deliveredStaffName);
                AddParam(cmd, "@staffId", deliveredStaffId);
            });
    }

    public async Task CancelAsync(int jobId)
        => await RequireTransitionAsync(jobId,
            new[] { "05-WaitListed", "10-Created", "20-Suspended", "23-InProcessSusp", "30-Started", "33-InProcess", "40-QA", "43-InProcessQA" },
            "97-Cancelled");

    public async Task<List<JobPartLine>> GetJobPartsAsync(int jobId)
    {
        var results = new List<JobPartLine>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT p.part_id, p.job_id, p.stock_id, p.partcode, p.partdescr, p.quantity,
                   p.costprice, p.sellprice, p.is_warranty_part, st.sellprice
            FROM parts p
            LEFT JOIN stock st ON st.stock_id = p.stock_id
            WHERE p.job_id = @jobId
            ORDER BY p.part_id";
        AddParam(cmd, "@jobId", jobId);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            results.Add(new JobPartLine
            {
                PartId = reader.GetInt32(0),
                JobId = reader.GetInt32(1),
                StockId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                PartCode = reader.GetString(3),
                PartDescr = reader.GetString(4),
                Quantity = reader.GetDecimal(5),
                CostPrice = reader.GetDecimal(6),
                SellPrice = reader.GetDecimal(7),
                IsWarrantyPart = reader.GetBoolean(8),
                CurrentSellPrice = reader.IsDBNull(9) ? null : reader.GetDecimal(9)
            });
        }
        return results;
    }

    public enum AddPartResult { Added, NotFound }

    public async Task<AddPartResult> AddPartByBarcodeAsync(int jobId, string barcode, StockService stockService, int? staffId, string staffName)
    {
        var stock = await stockService.FindStockByBarcodeAsync(barcode);
        if (stock == null)
            return AddPartResult.NotFound;

        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO parts (
                job_id, stock_id, partcode, partdescr, quantity, costprice, sellprice,
                serviced_by_staff_id, serviced_by_staff_name
            ) VALUES (
                @jobId, @stockId, @partCode, @partDescr, 1, @costPrice, @sellPrice,
                @staffId, @staffName
            )";
        AddParam(cmd, "@jobId", jobId);
        AddParam(cmd, "@stockId", stock.StockId);
        AddParam(cmd, "@partCode", stock.Barcode);
        AddParam(cmd, "@partDescr", stock.Description);
        AddParam(cmd, "@costPrice", stock.CostPrice);
        AddParam(cmd, "@sellPrice", stock.SellPrice);
        AddParam(cmd, "@staffId", staffId);
        AddParam(cmd, "@staffName", staffName);
        await Task.Run(() => cmd.ExecuteNonQuery());
        return AddPartResult.Added;
    }

    public async Task RemovePartAsync(int partId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM parts WHERE part_id = @partId";
        AddParam(cmd, "@partId", partId);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    private async Task<string> TransitionAsync(int jobId, Dictionary<string, string> statusMap)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());

        string currentStatus;
        using (var fetchCmd = conn.CreateCommand())
        {
            fetchCmd.CommandText = "SELECT jobstatus FROM jobs WHERE job_id = @jobId";
            AddParam(fetchCmd, "@jobId", jobId);
            var result = await Task.Run(() => fetchCmd.ExecuteScalar());
            if (result == null) throw new InvalidOperationException($"Job #{jobId} not found.");
            currentStatus = (string)result;
        }

        if (!statusMap.TryGetValue(currentStatus, out var newStatus))
            return currentStatus;

        using var updateCmd = conn.CreateCommand();
        updateCmd.CommandText = "UPDATE jobs SET jobstatus = @newStatus WHERE job_id = @jobId";
        AddParam(updateCmd, "@newStatus", newStatus);
        AddParam(updateCmd, "@jobId", jobId);
        await Task.Run(() => updateCmd.ExecuteNonQuery());
        return newStatus;
    }

    private async Task RequireTransitionAsync(int jobId, string[] fromStatuses, string toStatus,
        string? extraSql = null, Action<System.Data.IDbCommand>? extraParams = null)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        var paramNames = new List<string>();
        for (int i = 0; i < fromStatuses.Length; i++) paramNames.Add($"@from{i}");
        cmd.CommandText = $@"
            UPDATE jobs
            SET jobstatus = @toStatus{(extraSql != null ? ", " + extraSql : "")}
            WHERE job_id = @jobId AND jobstatus IN ({string.Join(",", paramNames)})";
        AddParam(cmd, "@toStatus", toStatus);
        AddParam(cmd, "@jobId", jobId);
        for (int i = 0; i < fromStatuses.Length; i++) AddParam(cmd, paramNames[i], fromStatuses[i]);
        extraParams?.Invoke(cmd);

        if (await Task.Run(() => cmd.ExecuteNonQuery()) == 0)
            throw new InvalidOperationException($"Job #{jobId} is not in a state that allows this action.");
    }

    private static JobRecord ReadJob(System.Data.IDataReader reader) => new()
    {
        JobId = reader.GetInt32(0),
        CustomerBarcode = reader.GetString(1),
        RmCustomerId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
        CustomerName = reader.GetString(3),
        CustomerPhone = reader.GetString(4),
        CustomerMobile = reader.GetString(5),
        Priority = reader.GetString(6),
        NominatedTech = reader.GetString(7),
        JobStatus = reader.GetString(8),
        GoodsInCare = reader.GetString(9),
        GoodsBrand = reader.GetString(10),
        GoodsModel = reader.GetString(11),
        DataBackupReqd = reader.GetString(12) == "Y",
        DataDiskReqd = reader.GetString(13) == "Y",
        ProblemShort = reader.GetString(14),
        ProblemLong = reader.GetString(15),
        ProblemSymptoms = reader.GetString(16),
        SystemUnderWarranty = reader.GetBoolean(17),
        DateCreated = reader.GetDateTime(18),
        RcvdStaffName = reader.GetString(19),
        Diagnosis = reader.GetString(20),
        ServiceNotes = reader.GetString(21),
        DateCompleted = reader.IsDBNull(22) ? null : reader.GetDateTime(22),
        TechStaffName = reader.GetString(23),
        TechRmStaffId = reader.IsDBNull(24) ? null : reader.GetInt32(24),
        DateDelivered = reader.IsDBNull(25) ? null : reader.GetDateTime(25),
        DeliveredStaffName = reader.GetString(26),
        DateUpdated = reader.GetDateTime(27)
    };

    private static void AddParam(System.Data.IDbCommand cmd, string name, object? value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value ?? DBNull.Value;
        cmd.Parameters.Add(param);
    }
}
