// LegacyDataImport
//
// Imports a legacy JobMatix MSSQL database (the combined "JobTracking" schema used by
// JMxPOS620.Net / JMxJT620.NET) into the new split-database PostgreSQL schema
// (jobmatix_pos / jobmatix_jobs). Built to validate the PostgreSQL-side equivalent of
// JobMatix's old built-in MSSQL backup/restore feature, using a real restored store
// backup as the test case.
//
// Usage: dotnet run [-- <mssql-conn-string> <pg-pos-conn-string> <pg-jobs-conn-string>]
// All three connection strings default to this session's local dev containers if not
// supplied. To point at a different store's restored backup in future, either pass
// connection strings as args or set env vars: LEGACY_MSSQL_CONNSTR, PG_POS_CONNSTR,
// PG_JOBS_CONNSTR.

using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;

string mssqlConn = args.Length > 0 ? args[0]
    : Environment.GetEnvironmentVariable("LEGACY_MSSQL_CONNSTR")
    ?? "Server=localhost,14330;Database=JobTracking;User Id=sa;Password=TempRestore2026!Pw;TrustServerCertificate=True;";

string pgPosConn = args.Length > 1 ? args[1]
    : Environment.GetEnvironmentVariable("PG_POS_CONNSTR")
    ?? "Host=localhost;Port=5433;Database=jobmatix_pos;Username=jobmatix_user;Password=JobMatix2026!Dev";

string pgJobsConn = args.Length > 2 ? args[2]
    : Environment.GetEnvironmentVariable("PG_JOBS_CONNSTR")
    ?? "Host=localhost;Port=5433;Database=jobmatix_jobs;Username=jobmatix_user;Password=JobMatix2026!Dev";

using var src = new SqlConnection(mssqlConn);
await src.OpenAsync();
using var pgPos = new NpgsqlConnection(pgPosConn);
await pgPos.OpenAsync();
using var pgJobs = new NpgsqlConnection(pgJobsConn);
await pgJobs.OpenAsync();

var results = new List<(string SourceTable, string TargetTable, long SourceRows, long Imported, long Skipped, string Note)>();

Console.WriteLine("=== JobMatix Legacy Data Import ===\n");

// ---------------- jobs-domain lookup tables (no deps) ----------------
await Run("JobBrands", "brands", pgJobs,
    "SELECT Brand_Id, BrandName, BrandCreated FROM JobBrands",
    new[] { "brand_id", "branddescr", "datecreated" });

await Run("JobTaskTypes", "tasktypes", pgJobs,
    "SELECT TaskType_Id, TaskTypeDescription, TaskTypeCreated FROM JobTaskTypes",
    new[] { "tasktype_id", "taskdescription", "datecreated" });

await Run("Symptoms", "symptoms", pgJobs,
    "SELECT Symptom_Id, SymptomDescr, DateCreated FROM Symptoms",
    new[] { "symptom_id", "symptomdescr", "datecreated" });

await Run("GoodsTypes", "goodstypes", pgJobs,
    "SELECT GoodsType_Id, GoodsTypeDescription, GoodsTypeCreated FROM GoodsTypes",
    new[] { "goodstype_id", "goodstypedescription", "goodstypecreated" });

await Run("ModelCheckList", "model_checklist", pgJobs,
    "SELECT CheckList_Id, CheckListDescription, CheckListDateCreated FROM ModelCheckList",
    new[] { "checklist_id", "checklist_description", "date_created" });

await Run("ServiceModelChecklists", "servicemodelchecklists", pgJobs,
    "SELECT ModelCheckList_Id, '' AS modelname, ModelCheckListTaskDescription, 0 AS itemorder, ModelCheckListDateCreated, ModelCheckList_RMStockId FROM ServiceModelChecklists",
    new[] { "modelchecklist_id", "modelname", "checklistitem", "itemorder", "datecreated", "rm_stock_id" });

// ---------------- jobs table (root of jobs domain) ----------------
await Run("Jobs", "jobs", pgJobs,
    @"SELECT Job_Id, CustomerBarcode, RMCustomer_Id, CustomerCompany, CustomerName, CustomerPhone, CustomerMobile,
             Priority, NominatedTech, JobStatus, GoodsInCare, GoodsOther, GoodsBrand, GoodsModel, GoodsExtras,
             MultiAccounts, Username, UserPassword, DataBackupReqd, DataDiskReqd, ProblemShort, ProblemLong,
             ProblemSymptoms, JobReturned, SystemUnderWarranty, DateCreated, DatePromised, RcvdStaffName,
             RcvdRMStaff_Id, Diagnosis, ServiceNotes, SessionTimes, TotalServiceTime, DateCompleted, TechStaffName,
             TechRMStaff_Id, Notifications, DateDelivered, DeliveredStaffName, DeliveredRMStaff_Id, DateUpdated
      FROM Jobs",
    new[] { "job_id", "customerbarcode", "rmcustomer_id", "customercompany", "customername", "customerphone",
            "customermobile", "priority", "nominatedtech", "jobstatus", "goodsincare", "goodsother", "goodsbrand",
            "goodsmodel", "goodsextras", "multiaccounts", "username", "userpassword", "databackupreqd",
            "datadiskreqd", "problemshort", "problemlong", "problemsymptoms", "jobreturned", "systemunderwarranty",
            "datecreated", "datepromised", "rcvdstaffname", "rcvdrmstaff_id", "diagnosis", "servicenotes",
            "sessiontimes", "totalservicetime", "datecompleted", "techstaffname", "techrmstaff_id", "notifications",
            "datedelivered", "deliveredstaffname", "deliveredrmstaff_id", "dateupdated" });

// ---------------- jobs-domain children (depend on Jobs) ----------------
await Run("JobTasks", "tasks", pgJobs,
    "SELECT Task_Id, TaskJob_Id, Description, DateCreated, TaskType_Id, PerformedByRMStaff_id, PerformedByStaffName FROM JobTasks",
    new[] { "task_id", "job_id", "taskdescr", "datecreated", "task_type_id", "performed_by_staff_id", "performed_by_staff_name" });

await Run("JobParts", "parts", pgJobs,
    @"SELECT Part_Id, PartJob_Id, RMDescription, CAST(1 AS DECIMAL(18,4)) AS quantity, RMCost, RMSell, DateCreated,
             RMstock_Id, RMLongDescription, RMCat1, RMCat2, RMCat3, IsWarrantyPart, WarrantyPartNo,
             ServicedByRMStaff_id, ServicedByStaffName, PartSerialNumber, RMBarcode
      FROM JobParts",
    new[] { "part_id", "job_id", "partdescr", "quantity", "costprice", "sellprice", "datecreated",
            "stock_id", "long_description", "cat1", "cat2", "cat3", "is_warranty_part", "warranty_part_no",
            "serviced_by_staff_id", "serviced_by_staff_name", "serial_number", "partcode" });

await Run("JobsCheckLists", "jobchecklists", pgJobs,
    @"SELECT JobCheckList_Id, JobCheckList_JobId, JobCheckListDescription, CAST(1 AS BIT) AS iscompleted, JobCheckListDateUpdated,
             JobCheckListComments, JobCheckList_StaffId, JobCheckListStaffName, JobCheckListDateUpdated
      FROM JobsCheckLists",
    new[] { "jobchecklist_id", "job_id", "checklistitem", "iscompleted", "datecreated",
            "comments", "staff_id", "staff_name", "date_updated" });

await Run("JobServiceCheckLists", "job_service_checklists", pgJobs,
    @"SELECT JobCheckList_Id, JobCheckList_JobId, JobCheckList_RMStockId, JobCheckListSequence,
             JobCheckListTaskDescription, JobCheckListStatus, JobCheckListComments,
             JobCheckList_StaffId, JobCheckListStaffName, JobCheckListDateUpdated
      FROM JobServiceCheckLists",
    new[] { "jobchecklist_id", "job_id", "rm_stock_id", "sequence", "task_description", "status", "comments",
            "staff_id", "staff_name", "date_updated" });

await Run("JobOtherDetails", "jobother", pgJobs,
    @"SELECT JobOther_Id, JobOther_JobId, JobOtherType, JobOtherTextData1, JobOtherDateCreated,
             JobOtherStaffName, JobOtherBarcode, JobOtherIntegerData1, JobOtherIntegerData2, JobOtherTextData2
      FROM JobOtherDetails",
    new[] { "jobother_id", "job_id", "fieldname", "fieldvalue", "datecreated",
            "staff_name", "barcode", "integer_data1", "integer_data2", "text_data2" });

await Run("Job_Attachments", "documents", pgJobs,
    @"SELECT doc_id, doc_job_id, doc_file_title, doc_file_comments, doc_file_content, doc_file_format,
             doc_file_size, doc_date_inserted, doc_party_info, doc_staff_id, doc_staff_name, doc_file_is_image
      FROM Job_Attachments",
    new[] { "doc_id", "job_id", "doc_filename", "doc_description", "doc_data", "doc_type",
            "doc_size", "date_created", "party_info", "staff_id", "staff_name", "is_image" });

await Run("RAItems", "returnauthorizations", pgJobs,
    @"SELECT RA_Id, NULLIF(RA_JobId, -1) AS job_id, RA_CustomerBarcode, RA_CustomerName, RM_Supplier,
             RA_SupplierRMA_No, RA_DateCreated, RA_Status, RM_ItemDescription, RA_Symptoms,
             RA_ReturnResultComment, RA_DateCreated, RA_DateGoodsReceivedBack,
             CASE WHEN RA_RecordLocked='Y' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END, RA_SerialNumber, RA_Origin,
             NULLIF(RA_RMCustomer_Id,-1), RA_CustomerCompany, RA_CustomerPhone, RA_CustomerMobile,
             NULLIF(RM_StockId,-1), RM_ItemSupplierCode, RM_ItemBarcode, RM_ItemCat1, RM_ItemCat2, RM_ItemCat3,
             RM_Item_Sell_ex, NULLIF(RM_GoodsId,-1), RM_InvoiceNo, RM_InvoiceDate, RM_GoodsDate, RM_OrderNo,
             NULLIF(RM_OrderId,-1), NULLIF(RM_SupplierId,-1), RM_SupplierBarcode, RM_Supplier_main_phone,
             RM_Supplier_main_fax, RM_Supplier_main_email, RM_Supplier_AddressInfo, RA_DateRMA_Requested,
             RA_DateRMA_Response, RA_RMA_Granted, RA_SupplierRMA_No, RA_CourierBarcode, RA_DateGoodsSentBack,
             RA_ReturnResult, NULLIF(RM_StaffIdCreated,-1), RM_StaffNameCreated, NULLIF(RM_StaffIdUpdated,-1),
             RM_StaffNameUpdated, RA_DateUpdated, RA_RMA_RequestNotes, NULLIF(RM_SerialAudit_id,-1)
      FROM RAItems",
    new[] { "ra_id", "job_id", "customerbarcode", "customername", "suppliername", "ranumber", "radate",
            "rastatus", "itemdescription", "problemdescription", "resolution", "datecreated", "datecompleted",
            "record_locked", "serial_number", "origin", "rm_customer_id", "customer_company", "customer_phone",
            "customer_mobile", "rm_stock_id", "item_supplier_code", "item_barcode", "item_cat1", "item_cat2",
            "item_cat3", "item_sell_ex", "goods_id", "invoice_no", "invoice_date", "goods_date", "order_no",
            "order_id", "supplier_id", "supplier_barcode", "supplier_phone", "supplier_fax", "supplier_email",
            "supplier_address", "date_rma_requested", "date_rma_response", "rma_granted", "supplier_rma_no",
            "courier_barcode", "date_goods_sent_back", "return_result", "staff_id_created", "staff_name_created",
            "staff_id_updated", "staff_name_updated", "date_updated", "rma_request_notes", "serial_audit_id" });

await Run("RA_Attachments", "ra_attachments", pgJobs,
    @"SELECT doc_id, doc_ra_id, doc_party_info, doc_staff_id, doc_staff_name, doc_file_format, doc_file_title,
             doc_file_is_image, doc_file_size, doc_file_content, doc_file_comments, doc_date_inserted
      FROM RA_Attachments",
    new[] { "doc_id", "doc_ra_id", "doc_party_info", "doc_staff_id", "doc_staff_name", "doc_file_format",
            "doc_file_title", "doc_file_is_image", "doc_file_size", "doc_file_content", "doc_file_comments",
            "date_created" });

await Run("QuoteJobParts", "quote_job_parts", pgJobs,
    @"SELECT QuotePart_Id, NULLIF(QuotePart_JobId,-1), NULLIF(QuotePart_OrderId,-1), QuotePartBarcode,
             QuotePartDescription, QuotePartCat1, QuotePartCat2, QuotePart_OrderQty, NULLIF(QuotePart_StockId,-1),
             QuotePart_Sell_inc, QuotePartDateCreated
      FROM QuoteJobParts",
    new[] { "quotepart_id", "quotepart_jobid", "quotepart_orderid", "quotepart_barcode", "quotepart_description",
            "quotepart_cat1", "quotepart_cat2", "quotepart_orderqty", "quotepart_stockid", "quotepart_sell_inc",
            "date_created" });

// ================= POS-domain =================

await Run("Staff", "staff", pgPos,
    @"SELECT staff_id, barcode, lastName, firstName, docket_name, position, isAdministrator, inactive, dateOfBirth,
             address, suburb, state, postcode, homePhone, mobile, emailAddress, status, password, passwordHint,
             staffPicture, date_created, date_modified
      FROM Staff",
    new[] { "staff_id", "barcode", "lastname", "firstname", "docket_name", "position", "isadministrator", "inactive",
            "dateofbirth", "address", "suburb", "state", "postcode", "homephone", "mobile", "emailaddress", "status",
            "password", "passwordhint", "staffpicture", "date_created", "date_modified" });

await Run("Supplier", "supplier", pgPos,
    @"SELECT supplier_id, barcode, supplierName, grade, inactive, contactName, contactPosition, address, suburb,
             state, postcode, country, phone, '' AS homephone, fax, '' AS mobile, emailAddress, webSiteURL, abn,
             '' AS taxcode, comments, date_created, date_modified,
             altContactName, altContactPosition, altPhone, altFax, altEmail, freight_free, reject_backorders, deliveryDays
      FROM Supplier",
    new[] { "supplier_id", "barcode", "suppliername", "grade", "inactive", "contactname", "contactposition",
            "address", "suburb", "state", "postcode", "country", "businessphone", "homephone", "fax", "mobile",
            "emailaddress", "website", "abn", "taxcode", "notes", "date_created", "date_modified",
            "alt_contact_name", "alt_contact_position", "alt_phone", "alt_fax", "alt_email", "freight_free",
            "reject_backorders", "delivery_days" });

await Run("Customer", "customer", pgPos,
    @"SELECT customer_id, barcode,
             (CASE WHEN companyName <> '' THEN companyName ELSE (firstName + ' ' + lastName) END) AS customername,
             companyName, pricingGrade, inactive, (firstName + ' ' + lastName) AS contactname, position AS contactposition,
             address, suburb, state, postcode, country, phone, fax, mobile, email, abn, isAccountCust, CAST(0 AS DECIMAL(19,4)) AS accountbalance,
             creditLimit, comments, date_created, date_modified,
             firstName, lastName, title, openedStaff_id, openedStaffName, creditDays, doNotEmailDocuments, Tags
      FROM Customer",
    new[] { "customer_id", "barcode", "customername", "companyname", "grade", "inactive", "contactname",
            "contactposition", "address", "suburb", "state", "postcode", "country", "businessphone", "fax",
            "mobile", "emailaddress", "abn", "isaccount", "accountbalance", "creditlimit", "notes",
            "date_created", "date_modified",
            "firstname", "lastname", "title", "opened_staff_id", "opened_staff_name", "credit_days",
            "do_not_email_documents", "tags" });

await Run("Stock", "stock", pgPos,
    @"SELECT stock_id, supplier_id, barcode, description, cat1, barcode AS stockcode, '' AS suppliercode, inactive,
             qtyInStock, reOrderLevel, CAST(0 AS DECIMAL(18,4)) AS maxstocklevel, order_quantity, costExTax, sellExTax, sales_taxCode,
             CAST(0 AS DECIMAL(9,4)) AS taxrate, '' AS unit_of_measure, comments, productPicture, date_created, date_modified, track_serial,
             cat2, model_no, sales_prompt, isNonStockItem, allow_renaming, longDescription, BrandName, goods_taxCode,
             cost_account, income_account, freight
      FROM Stock",
    new[] { "stock_id", "supplier_id", "barcode", "description", "category", "stockcode", "suppliercode",
            "inactive", "quantityinstock", "minstocklevel", "maxstocklevel", "reorderquantity", "costprice",
            "sellprice", "taxcode", "taxrate", "unit_of_measure", "notes", "stockimage", "date_created",
            "date_modified", "requiresserial",
            "cat2", "model_no", "sales_prompt", "is_non_stock_item", "allow_renaming", "long_description",
            "brand_name", "goods_tax_code", "cost_account", "income_account", "freight" });

await Run("StockBrands", "stock_brands", pgPos,
    "SELECT Brand_Id, BrandName, date_created, date_modified FROM StockBrands",
    new[] { "brand_id", "brand_name", "date_created", "date_modified" });

await Run("category1", "category1", pgPos,
    "SELECT cat1_key, description, date_created, date_modified FROM category1",
    new[] { "cat1_key", "description", "date_created", "date_modified" });

await Run("category2", "category2", pgPos,
    "SELECT cat2_key, description, date_created, date_modified FROM category2",
    new[] { "cat2_key", "description", "date_created", "date_modified" });

await Run("SupplierCode", "supplier_code", pgPos,
    "SELECT supcode, supplier_id, stock_id, date_created, date_modified FROM SupplierCode",
    new[] { "supcode", "supplier_id", "stock_id", "date_created", "date_modified" });

await Run("Invoice", "invoice", pgPos,
    @"SELECT invoice_id, staff_id, customer_id, transactionType, CAST(invoice_id AS VARCHAR(20)), invoice_date,
             CAST(NULL AS DATETIME) AS duedate, 'Completed' AS status, (subtotal_ex_taxable + subtotal_ex_non_taxable) AS subtotal,
             subtotal_tax AS taxamount, total_inc AS totalamount, CAST(0 AS DECIMAL(19,4)) AS amountpaid, total_inc AS amountdue,
             '' AS paymentmethod, '' AS paymentreference, (comments + ' ' + deliveryInstructions), invoice_date,
             invoice_date, total_inc, total_ex, total_tax,
             isOnAccount, NULLIF(payment_id,-1), NULLIF(JobNumber,-1), NULLIF(delivered_layby_id,-1),
             NULLIF(original_id,-1), terminal_id, cashDrawer, currentWindowsUserName, discount_nett, discount_tax,
             rounding, deliveryInstructions
      FROM Invoice",
    new[] { "invoice_id", "staff_id", "customer_id", "transactiontype", "invoicenumber", "invoicedate", "duedate",
            "status", "subtotal", "taxamount", "totalamount", "amountpaid", "amountdue", "paymentmethod",
            "paymentreference", "notes", "date_created", "date_modified", "total_inc", "total_ex", "total_tax",
            "is_on_account", "payment_id", "job_number", "delivered_layby_id", "original_id", "terminal_id",
            "cash_drawer", "current_windows_username", "discount_nett", "discount_tax", "rounding",
            "delivery_instructions" });

await Run("InvoiceLine", "invoice_lines", pgPos,
    @"SELECT line_id, invoice_id, stock_id, ROW_NUMBER() OVER (PARTITION BY invoice_id ORDER BY line_id), description,
             quantity, sellActual_ex, (sell_ex - sellActual_ex), sales_taxCode, sales_taxPercentage, sellActual_Tax,
             total_ex, '' AS notes,
             serialNumber, NULLIF(serialAudit_id,-1), cost_ex, cost_inc, sell_ex, sell_inc, gross_profit
      FROM InvoiceLine",
    new[] { "line_id", "invoice_id", "stock_id", "linenumber", "description", "quantity", "unitprice", "discount",
            "taxcode", "taxrate", "taxamount", "linetotal", "notes",
            "serial_number", "serial_audit_id", "cost_ex", "cost_inc", "sell_ex", "sell_inc", "gross_profit" });

await Run("Payments", "payments", pgPos,
    @"SELECT payment_id, staff_id, customer_id, NULLIF(invoice_id,-1), transactionType, payment_date, '' AS paymentmethod,
             '' AS paymentreference, totalAmountReceived, comments, payment_date,
             isReversal, NULLIF(originalPayment_id,-1), terminal_id, cashDrawer, currentWindowsUserName,
             discountGivenOnPayment, changeGiven, nettAmountCredited, amountDebitedToAccount, refundCashAmount,
             refundAsCreditNoteCredited, refundAsEftPosDr, refundAsEftPosCr, creditNotePaymentCredited,
             creditNoteAmountDebited, RefundOtherDetailAmount, RefundOtherDetailKey
      FROM Payments",
    new[] { "payment_id", "staff_id", "customer_id", "invoice_id", "transactiontype", "paymentdate", "paymentmethod",
            "paymentreference", "amount", "notes", "date_created",
            "is_reversal", "original_payment_id", "terminal_id", "cash_drawer", "current_windows_username",
            "discount_given_on_payment", "change_given", "nett_amount_credited", "amount_debited_to_account",
            "refund_cash_amount", "refund_as_credit_note_credited", "refund_as_eftpos_dr", "refund_as_eftpos_cr",
            "credit_note_payment_credited", "credit_note_amount_debited", "refund_other_detail_amount",
            "refund_other_detail_key" });

await Run("PaymentDetails", "payment_details", pgPos,
    @"SELECT detail_id, payment_id, payment_date, paymenttype_key, paymenttype_subKey, paymenttype_descr, amount, comments
      FROM PaymentDetails",
    new[] { "detail_id", "payment_id", "payment_date", "paymenttype_key", "paymenttype_subkey", "paymenttype_descr",
            "amount", "comments" });

await Run("PaymentDisbursements", "payment_disbursements", pgPos,
    @"SELECT Disbursements_id, payment_id, NULLIF(invoice_id,-1), tranCode, sourceOfFunds, amount FROM PaymentDisbursements",
    new[] { "disbursements_id", "payment_id", "invoice_id", "tran_code", "source_of_funds", "amount" });

await Run("PurchaseOrder", "purchase_order", pgPos,
    @"SELECT order_id, revision, order_date, due_date, staff_id, supplier_id, orderNoSuffix, delivery_address,
             isReceiving, isCompleted, isClosedForBackorders, isCancelled, comments, date_modified
      FROM PurchaseOrder",
    new[] { "order_id", "revision", "order_date", "due_date", "staff_id", "supplier_id", "order_no_suffix",
            "delivery_address", "is_receiving", "is_completed", "is_closed_for_backorders", "is_cancelled",
            "comments", "date_modified" });

await Run("GoodsReceived", "goods_received", pgPos,
    @"SELECT goods_id, goods_date, staff_id, supplier_id, invoice_no, invoice_date, orderNoSuffix, NULLIF(order_id,-1),
             subtotal_ex, subtotal_tax, subtotal_inc, freight_ex, freight_taxCode, freight_taxPercentage, freight_tax,
             freight_inc, discount_nett, discount_tax, total_ex, total_tax, total_inc, total_expected, comments
      FROM GoodsReceived",
    new[] { "goods_id", "goods_date", "staff_id", "supplier_id", "invoice_no", "invoice_date", "order_no_suffix",
            "order_id", "subtotal_ex", "subtotal_tax", "subtotal_inc", "freight_ex", "freight_tax_code",
            "freight_tax_percentage", "freight_tax", "freight_inc", "discount_nett", "discount_tax", "total_ex",
            "total_tax", "total_inc", "total_expected", "comments" });

await Run("PurchaseOrderLine", "purchase_order_line", pgPos,
    @"SELECT line_id, order_id, supplier_id, stock_id, suppliercode, goods_taxCode, cost_ex, cost_inc, quantity,
             qtyReceived, status, NULLIF(goods_id,-1), date_updated
      FROM PurchaseOrderLine",
    new[] { "line_id", "order_id", "supplier_id", "stock_id", "supplier_code", "goods_tax_code", "cost_ex",
            "cost_inc", "quantity", "qty_received", "status", "goods_id", "date_updated" });

await Run("GoodsReceivedLine", "goods_received_line", pgPos,
    @"SELECT line_id, goods_id, stock_id, goods_taxCode, goods_taxPercentage, cost_ex, cost_tax, cost_inc, sell_ex,
             quantity, total_ex, total_tax, total_inc
      FROM GoodsReceivedLine",
    new[] { "line_id", "goods_id", "stock_id", "goods_tax_code", "goods_tax_percentage", "cost_ex", "cost_tax",
            "cost_inc", "sell_ex", "quantity", "total_ex", "total_tax", "total_inc" });

await Run("SalesOrder", "sales_order", pgPos,
    @"SELECT salesorder_id, salesorder_date, staff_id, customer_id, transactionType, subtotal_tax, subtotal_inc,
             discount_nett, discount_tax, rounding, total_ex, total_tax, total_inc, deliveryInstructions, comments
      FROM SalesOrder",
    new[] { "salesorder_id", "salesorder_date", "staff_id", "customer_id", "transaction_type", "subtotal_tax",
            "subtotal_inc", "discount_nett", "discount_tax", "rounding", "total_ex", "total_tax", "total_inc",
            "delivery_instructions", "comments" });

await Run("SalesOrderLine", "sales_order_line", pgPos,
    @"SELECT line_id, salesorder_id, stock_id, description, cost_ex, cost_inc, sell_ex, sales_taxCode,
             sales_taxPercentage, sell_inc, sellActual_ex, sellActual_Tax, sellActual_inc, quantity, total_ex,
             total_tax, total_inc
      FROM SalesOrderLine",
    new[] { "line_id", "salesorder_id", "stock_id", "description", "cost_ex", "cost_inc", "sell_ex",
            "sales_tax_code", "sales_tax_percentage", "sell_inc", "sell_actual_ex", "sell_actual_tax",
            "sell_actual_inc", "quantity", "total_ex", "total_tax", "total_inc" });

await Run("Cashup_Sessions", "cashup_sessions", pgPos,
    @"SELECT session_id, staff_id, staff_name, session_date, cashDrawer, currentWindowsUserName, terminal_id,
             first_payment_id, last_payment_id, status, stock_value, stock_variance, comments
      FROM Cashup_Sessions",
    new[] { "session_id", "staff_id", "staff_name", "session_date", "cash_drawer", "current_windows_username",
            "terminal_id", "first_payment_id", "last_payment_id", "status", "stock_value", "stock_variance",
            "comments" });

await Run("Cashup_Shortages", "cashup_shortages", pgPos,
    @"SELECT shortage_id, session_id, paymenttype_key, paymenttype_descr, amount_reported, amount_counted
      FROM Cashup_Shortages",
    new[] { "shortage_id", "session_id", "paymenttype_key", "paymenttype_descr", "amount_reported", "amount_counted" });

await Run("Layby", "layby", pgPos,
    @"SELECT Layby_id, Layby_date_started, staff_id, customer_id, transactionType, JobNumber, terminal_id,
             cashDrawer, currentWindowsUserName, subtotal_ex_non_taxable, subtotal_ex_taxable, subtotal_tax,
             subtotal_inc, discount_nett, discount_tax, rounding, total_ex, total_tax, total_inc, isCancelled,
             date_cancelled, cancelled_staff_id, isDelivered, Layby_date_delivered,
             NULLIF(Layby_delivered_invoice_id,-1), deliveryInstructions, comments
      FROM Layby",
    new[] { "layby_id", "layby_date_started", "staff_id", "customer_id", "transaction_type", "job_number",
            "terminal_id", "cash_drawer", "current_windows_username", "subtotal_ex_non_taxable",
            "subtotal_ex_taxable", "subtotal_tax", "subtotal_inc", "discount_nett", "discount_tax", "rounding",
            "total_ex", "total_tax", "total_inc", "is_cancelled", "date_cancelled", "cancelled_staff_id",
            "is_delivered", "layby_date_delivered", "layby_delivered_invoice_id", "delivery_instructions",
            "comments" });

await Run("LaybyLine", "layby_line", pgPos,
    @"SELECT line_id, Layby_id, stock_id, description, serialNumber, NULLIF(serialAudit_id,-1), cost_ex, cost_inc,
             sell_ex, sales_taxCode, sales_taxPercentage, sell_inc, sellActual_ex, sellActual_Tax, sellActual_inc,
             quantity, total_ex, total_tax, total_inc, gross_profit
      FROM LaybyLine",
    new[] { "line_id", "layby_id", "stock_id", "description", "serial_number", "serial_audit_id", "cost_ex",
            "cost_inc", "sell_ex", "sales_tax_code", "sales_tax_percentage", "sell_inc", "sell_actual_ex",
            "sell_actual_tax", "sell_actual_inc", "quantity", "total_ex", "total_tax", "total_inc", "gross_profit" });

await Run("Subscription", "subscription", pgPos,
    @"SELECT Subscription_id, customer_id, staff_id, isActivated, start_date, termination_date, billingPeriod,
             terminal_id, isCancelled, date_cancelled, cancelled_staff_id, date_created, date_updated, comments,
             OkToEmailInvoices
      FROM Subscription",
    new[] { "subscription_id", "customer_id", "staff_id", "is_activated", "start_date", "termination_date",
            "billing_period", "terminal_id", "is_cancelled", "date_cancelled", "cancelled_staff_id", "date_created",
            "date_updated", "comments", "ok_to_email_invoices" });

await Run("SubscriptionLine", "subscription_line", pgPos,
    @"SELECT line_id, Subscription_id, stock_id, stock_barcode, stock_description, sellActual_inc, quantity FROM SubscriptionLine",
    new[] { "line_id", "subscription_id", "stock_id", "stock_barcode", "stock_description", "sell_actual_inc", "quantity" });

await Run("SubscriptionInvoice", "subscription_invoice", pgPos,
    @"SELECT subs_invoice_line_id, Subscription_id, NULLIF(invoice_id,-1), invoice_period_start_date,
             invoice_period_end_date, email_sent_ok
      FROM SubscriptionInvoice",
    new[] { "subs_invoice_line_id", "subscription_id", "invoice_id", "invoice_period_start_date",
            "invoice_period_end_date", "email_sent_ok" });

await Run("Stocktake", "stocktake", pgPos,
    @"SELECT stocktake_id, stocktake_type, cat1, cat2List, currentWindowsUserName, terminal_id, is_committed,
             is_cancelled, date_created, created_staff_name, date_modified, modified_staff_name, date_committed,
             committed_staff_name, comments
      FROM Stocktake",
    new[] { "stocktake_id", "stocktake_type", "cat1", "cat2_list", "current_windows_username", "terminal_id",
            "is_committed", "is_cancelled", "date_created", "created_staff_name", "date_modified",
            "modified_staff_name", "date_committed", "committed_staff_name", "comments" });

await Run("StocktakeItems", "stocktake_items", pgPos,
    @"SELECT item_id, stocktake_id, stock_id, barcode, cat1, cat2, description, qty_on_record, qty_counted,
             qty_difference, date_modified
      FROM StocktakeItems",
    new[] { "item_id", "stocktake_id", "stock_id", "barcode", "cat1", "cat2", "description", "qty_on_record",
            "qty_counted", "qty_difference", "date_modified" });

await Run("StockTakeSerials", "stocktake_serials", pgPos,
    "SELECT serialNumber, stock_id FROM StockTakeSerials",
    new[] { "serial_number", "stock_id" });

await Run("SupplierReturns", "supplier_returns", pgPos,
    @"SELECT return_id, return_date, staff_id, staff_name, supplier_id, freight_tax, freight_ex, freight_inc,
             total_ex, total_inc, comments
      FROM SupplierReturns",
    new[] { "return_id", "return_date", "staff_id", "staff_name", "supplier_id", "freight_tax", "freight_ex",
            "freight_inc", "total_ex", "total_inc", "comments" });

await Run("SupplierReturnLine", "supplier_return_line", pgPos,
    @"SELECT line_id, return_id, stock_id, NULLIF(serialAudit_id,-1), serialNumber, invoice_no, NULLIF(ra_id,-1),
             supplier_RMA_no, barcode, description, quantity, symptoms, request_notes, goods_taxCode, cost_ex, cost_inc
      FROM SupplierReturnLine",
    new[] { "line_id", "return_id", "stock_id", "serial_audit_id", "serial_number", "invoice_no", "ra_id",
            "supplier_rma_no", "barcode", "description", "quantity", "symptoms", "request_notes", "goods_tax_code",
            "cost_ex", "cost_inc" });

await Run("SerialAudit", "serial_audit", pgPos,
    @"SELECT serial_id, stock_id, SerialNumber, isInStock, status, warranty_date, date_created, date_modified
      FROM SerialAudit",
    new[] { "serial_id", "stock_id", "serial_number", "is_in_stock", "status", "warranty_date", "date_created",
            "date_modified" });

await Run("SerialAuditTrail", "serial_audit_trail", pgPos,
    @"SELECT trail_id, stock_id, serialAudit_id, original_id, tran_type, type_id, type_line_id, trail_date, movement,
             is_RM_transaction, RM_tr_detail
      FROM SerialAuditTrail",
    new[] { "trail_id", "stock_id", "serial_audit_id", "original_id", "tran_type", "type_id", "type_line_id",
            "trail_date", "movement", "is_rm_transaction", "rm_tr_detail" });

// ================= Summary =================
Console.WriteLine("\n=== Import Summary ===");
Console.WriteLine($"{"Source",-24}{"Target",-24}{"SrcRows",10}{"Imported",10}{"Skipped",9}  Note");
foreach (var r in results)
    Console.WriteLine($"{r.SourceTable,-24}{r.TargetTable,-24}{r.SourceRows,10}{r.Imported,10}{r.Skipped,9}  {r.Note}");

long totalSrc = results.Sum(r => r.SourceRows);
long totalImp = results.Sum(r => r.Imported);
long totalSkip = results.Sum(r => r.Skipped);
Console.WriteLine($"\nTOTAL: {totalSrc} source rows, {totalImp} imported, {totalSkip} skipped across {results.Count} tables.");

// ================= Helper: run one table migration =================
async Task Run(string sourceTable, string targetTable, NpgsqlConnection pg, string sql, string[] targetCols)
{
    Console.WriteLine($"-> {sourceTable} => {targetTable}");
    using var cmd = new SqlCommand(sql, src);
    using var reader = await cmd.ExecuteReaderAsync();

    var rows = new List<object?[]>();
    while (await reader.ReadAsync())
    {
        var row = new object?[targetCols.Length];
        for (int i = 0; i < targetCols.Length; i++)
        {
            var v = reader.GetValue(i);
            row[i] = v is DBNull ? null : v;
        }
        rows.Add(row);
    }
    reader.Close();

    long imported = 0, skipped = 0, alreadyPresent = 0;
    string note = "";
    string colList = string.Join(", ", targetCols);

    try
    {
        // Text-format COPY: Postgres parses each field with the target column's normal input
        // function (same as a plain INSERT would), so int-into-numeric, 1/0-into-boolean etc.
        // all just work - unlike binary-format COPY, which requires an exact wire-format type
        // match per column and breaks on these legacy-vs-new-schema type differences.
        using (var writer = pg.BeginTextImport($"COPY {targetTable} ({colList}) FROM STDIN (FORMAT text)"))
        {
            foreach (var row in rows)
            {
                var fields = new string[row.Length];
                for (int i = 0; i < row.Length; i++)
                    fields[i] = FormatCopyText(row[i]);
                writer.Write(string.Join("\t", fields));
                writer.Write('\n');
            }
        }
        imported = rows.Count;
    }
    catch (PostgresException ex) when (ex.SqlState == "23503" || ex.SqlState == "23505")
    {
        // FK violation or PK conflict on bulk path -> fall back to per-row insert, skip+log offending rows
        note = ex.SqlState == "23503" ? "FK-violation fallback" : "PK-conflict fallback";
        string paramList = string.Join(", ", targetCols.Select((_, i) => "$" + (i + 1)));
        foreach (var row in rows)
        {
            await using var insertCmd = new NpgsqlCommand(
                $"INSERT INTO {targetTable} ({colList}) VALUES ({paramList}) ON CONFLICT DO NOTHING", pg);
            for (int i = 0; i < row.Length; i++)
                insertCmd.Parameters.AddWithValue((object?)row[i] ?? DBNull.Value);
            try
            {
                int affected = await insertCmd.ExecuteNonQueryAsync();
                if (affected > 0) imported++; else alreadyPresent++;
            }
            catch (PostgresException)
            {
                skipped++;
            }
        }
        if (alreadyPresent > 0) note += $" ({alreadyPresent} already present from a prior run)";
    }

    // Reset the sequence (if any) for tables with a SERIAL primary key so future app-generated
    // inserts don't collide with the imported historical IDs.
    try
    {
        await using var seqCmd = new NpgsqlCommand(
            $@"SELECT setval(pg_get_serial_sequence('{targetTable}', '{targetCols[0]}'),
                              GREATEST(COALESCE((SELECT MAX({targetCols[0]}) FROM {targetTable}), 1), 1))
               WHERE pg_get_serial_sequence('{targetTable}', '{targetCols[0]}') IS NOT NULL", pg);
        await seqCmd.ExecuteNonQueryAsync();
    }
    catch { /* not every target's first column is a serial PK (e.g. composite/no-PK tables) - fine to ignore */ }

    Console.WriteLine($"   {sourceTable}: {rows.Count} source rows -> {imported} imported, {skipped} skipped {note}");
    results.Add((sourceTable, targetTable, rows.Count, imported, skipped, note));
}

static string FormatCopyText(object? value)
{
    if (value is null) return "\\N";
    switch (value)
    {
        case bool b: return b ? "t" : "f";
        case DateTime dt: return dt.ToString("yyyy-MM-dd HH:mm:ss.ffffff", System.Globalization.CultureInfo.InvariantCulture);
        case decimal dec: return dec.ToString(System.Globalization.CultureInfo.InvariantCulture);
        case float f: return f.ToString(System.Globalization.CultureInfo.InvariantCulture);
        case double d: return d.ToString(System.Globalization.CultureInfo.InvariantCulture);
        // Binary blob content (photos, staff pictures, attachments) is intentionally NOT migrated:
        // bytea's own "\x" hex-format collides with COPY TEXT's generic backslash-escape parsing,
        // corrupting the byte stream. All other columns for these rows still migrate normally -
        // only the raw file bytes are dropped. See LegacyDataImport/README.md.
        case byte[]: return "\\N";
        case int or long or short: return value.ToString()!;
        default:
            // string (and anything else): escape backslash, tab, newline, carriage return per COPY TEXT format
            string s = value.ToString() ?? "";
            var sb = new System.Text.StringBuilder(s.Length);
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\\': sb.Append("\\\\"); break;
                    case '\t': sb.Append("\\t"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\r': sb.Append("\\r"); break;
                    default: sb.Append(c); break;
                }
            }
            return sb.ToString();
    }
}
