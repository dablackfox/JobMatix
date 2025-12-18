Option Strict Off
Option Explicit On
Imports VB6 = Microsoft.VisualBasic
Imports System.io
Imports System.Collections
Imports System.Data.Sql
'== Imports VB6 = Microsoft.VisualBasic
'== Imports System.Data.sqlclient
Imports System.Data.OleDb

Module modCreatePOSdb

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '== 
    '== grh= 17-Feb-2014==
    '==  JobMatixPOS version ..--
    '==
    '== grh= 25-Mar-2014==
    '==  JobMatixPOS version 1.0.1001.325..--
    '==     Make sure all Table columns are NOT NULL with DEFAULT specified.
    '==     Rev 1001.329:  Add Staff Picture.
    '==
    '== grh= 24-Apr-2014==
    '==  JobMatixPOS-Admin version 1.0.1001.424..--
    '==     Now used only to ADD (CREATE) POS tables into existing database...
    '==
    '== grh= 01-Jun-2014==
    '==  JobMatixPOS-Admin version 1.0.1001.601..--
    '==    Updates-  Stock now has "isServiceType" 
    '==
    '== grh= 11-Jun-2014==
    '==  NOW Integrated into POS3..
    '==    JobMatixPOS3- version 3.0.3012.611..--
    '==      
    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==
    '==  grh JMxPOS31 3.1.3101.1112 -
    '==       >> Drop tables Accounts, Audit, Supp.Returns, TaxCodes, PricingGradeRules.
    '==
    '==  grh JMxPOS31 3.1.3101.1206 -
    '==       >> Drop table PaymentTypes.
    '==
    '==  grh JobMatixPOS 3.1.3103.0216 -  16-Feb-2015
    '==       >> Add Columns to SerialAuditTrail -
    '==              to Track RM-Imported Serial Movements...
    '==
    '==  grh JobMatixPOS 3.1.3103.0301 -  01-Mar-2015
    '==       >> Expand Stock barcode Column to NVARCHAR 40 chars -
    '==       >> Add model_no Column to Stock -
    '==
    '==  grh JobMatixPOS 3.1.3103.0411 -  11-Apr-2015
    '==       >> Add Columns 'subtotal_ex_non_taxable', 'subtotal_ex_taxable' to Invoice table.  -
    '==
    '==  grh JobMatixPOS 3.1.3107.0815 -  15-Aug-2015
    '==       >> Updates for purchaseOrders Table..  -
    '==       >> Add DocArchive Table..  -
    '==
    '==  NEW VERSION-  JMxPOS32
    '==
    '==   POS dll-  v3.2.3201.131..  31Jan2016= ===
    '==      >> ADD Tables for Stocktake..
    '==          Everyone must re-create new DB..--
    '== = = = = = = = = = = = = = = == = = = = = = =
    '==
    '==  NEW VERSION-  JMxPOS330
    '==
    '==     v3.3.3301.525..  25-May-2016= ===
    '==       >> Update Invoice Table Schema-  Invoices to reflect RM transactions (IV,IP, SA) types
    '==              (For Account customers, part payment with sale records a paymentCredit invoice entry)
    '==       Everyone must re-create new DB..--
    '==
    '==
    '==     v3.3.3301.607/608..  07-June-2016= ===
    '==       >> Update Stock Table Schema- (and fix frmImportRM.)
    '==             Column "isNonStockItem" replaces   "isServiceItem", "isLabour".
    '=                  (Imports "static_quantity".. )
    '==       >> Update Payments Schemas- Now is Just one table (payments) as per RM.
    '== = = = =
    '==
    '==     v3.3.3301.622..  22-June-2016= ===
    '==       >> Update Invoice and PAYMENTS Table Schema-  
    '==     BACK to the ORIGINAL schema-  No IP Invoice records, and PaymentDisbursement records Re-stored.
    '==         (For Account customers, part payment with sale records a separate Payment entry)
    '==
    '== = == = =
    '==
    '==     v3.3.3301.1116..  16-Nov-2016= ===
    '==       >> FOR Licence Checking- (cloning POS licence off Jobmatix)..-
    '==            After Adding POS tables, Add "JMPOS33_SECURITYID" row to SysInfo..
    '==            Then Compute POS security ID using its DateCreated, and update the row with the ID.
    '==
    '==       >> 3301.1129. Updates to cashup_sessions table..
    '==
    '==  3301.1211-11Dec2016= 
    '==      >>  ADD CashDrawer and CurrentUserName to INVOICES and Payments= AND CashupSessions=
    '==
    '==  3303.0111-11Jan2017= 
    '==      >>  ADD PaymentRefundDetail Table.  
    '==               (tracks refund fragments actually applied to  debtor invoices-)   
    '==
    '==
    '==     v3.3.3307.0202..  02-Feb-2017= ===
    '==       >> Returns- Add SupplierReturns Table,  
    '==            and accept GoodsShipped info from RAs Application to update Stock and SerialAudit Info...
    '== 
    '==     v3.4.3401.327- Drop Cashout columns from Invoice and payment records..
    '==         Add Payment-columns for Refund as EFTPOS Dr/Cr-
    '==
    '==  NEW POS Build..3403=  For Adding on Layby's functionality...--
    '== 
    '==     v3.4.3403.0429 = 29Apr2017=
    '==      -- modCreatePOS DB..  Add Tables LayBy and LaybyLine
    '==
    '==
    '==     3403.1014/1028- 09/1428-Oct-2017-
    '==      -- POS Emails now to use Server File-System to store Invoice PDF's for Email..
    '==            at \\[server]\users\public\JobMatixPOS-EmailQueue\ 
    '==                    (SystemInfo setting is :  "POS_EMAILQUEUE_SHAREPATH"
    '==               NB: (Table "DocArchive" to be DROPPED..)
    '==      --  XML Descriptor file to go with each PDF for Email sending info. 
    '==      --  POS Create db.. Add Emailing Defaults to initial SystemInfo..
    '==      --   Refunds now the same for Account Custs and non-a/c custs..   
    '==             -- So Account Payments can draw on CreditNotes for payment of invoices..  
    '==             --    and Table "paymentRefundDetails" is dropped. 
    '==        >> Account Payments can now have discount given on invoices..  
    '==            -- PaymentDisbursment now has TranCode columns ("payment" or "discount".
    '==            -- Payment has new column:  discountGivenOnPayment MONEY NOT NULL DEFAULT 0,"
    '==        >> 3403.1115 --  Add more settings to POS Create DB..
    '==        >> 3411.1125 -- Add TABLES for Subscriptions..
    '==
    '==-- (3411.0314 Was released to Precise..)
    '==-- (3411.0314 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - 
    '==
    '==   >> 3411.0417=  17-Apr-2018=
    '==       -- Fix Subscription line.. comma missing..
    '=
    '=====  = = = === 
    '==
    '==    -- 4201.0929/1002.  Started 19-September-2019-
    '==        -- Add column to Subscriptions Table: "OkToEmailInvoices bit default 0"
    '==        --  ALSO  Belatededly..  Update Create DEFS for Supp. codes..
    '==
    '==--       DROP the PKEY as it is not useful, and expand the column.. in tables supplierCode & PO Line..
    '==-          sSupCodeSql = "ALTER TABLE dbo.SupplierCode DROP CONSTRAINT " & sPkeyConstaintName & " ;" & vbCrLf
    '==-          sSupCodeSql &= "ALTER TABLE dbo.SupplierCode ALTER COLUMN supcode nvarchar(40) NOT NULL; " & vbCrLf
    '==           sSupCodeSql &= "ALTER TABLE dbo.PurchaseOrderLine ALTER COLUMN SupplierCode nvarchar(40) NOT NULL; "
    '= = = = = = = = = ==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==
    '==
    '==    THEME is implementation of EXTENDED Refund Details for REFUNDS..
    '==       --  Involves creating a REFUND DETAILS EXTRA Option (radioButton) to be able to refund same types as Payments..
    '==                 ie dropdown including ZipPay, bank Deposit etc..
    '==             NOTE- Sales form will keep Refund Options Frame for continuity of process,
    '==                   allowing Refunds to Cash, CreditNote or EftPos as always..
    '==                BUT an extra Option (OTHER- Choose From List) will allow user to see a DropDown Combo
    '==                      of remaining keys from master List so User can choose ONE only..
    '==   ALSO TWO new columns to be added to Payments Table-  
    '==                      viz- "RefundOtherDetailAmount"(MONEY), and "RefundOtherDetailKey" (VARCHAR).
    '==                         to Cash/CreditNote/EftPosDr/EftPosCr already recorded in Payment record.
    '==
    '==   ALSO HERE we wanted to update the Create Module to add "Tags" column to the customer Table
    '==      BUT it stuff s up the importing Customers from RM, so columns has to be added on the fly at POS Startup.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-4267 -- (Started 28-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 28-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 28-Sep-2020)
    '==
    '==
    '==  POS DB Create-   (1) Add INSERTIONS to new  Stock Table to set up six Labour Hourly Rates..  
    '==                               Barcodes like "01-LAB-HRLY-WKSH-P1" etc etc  
    '==            (2) ALSO add "SERVCE" to Cat1 and Cat2. 
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==

    '--  sql transaction for CREATE db Tables.--

    Private mSqlTran1 As OleDbTransaction
    Private mbIsTransaction As Boolean = False

    Private msLastSqlErrorMessage As String = ""

    Private msCreateLogPath As String  '--passed in with Create POS..-

    '==  private functions.-
    '==  private functions.-

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =
    '-===FF->

    '--  Execute Command -
    '---  (DOES NOT start another transaction..)--

    Private Function mbExecuteCmd(ByRef cnnUserDB As OleDbConnection, _
                                      ByVal sSql As String, _
                                        ByRef lAffected As Integer, _
                                         ByRef sErrorMsg As String, _
                                      Optional ByVal bIsTransaction As Boolean = False, _
                                        Optional ByRef sqlTran1 As OleDbTransaction = Nothing) As Boolean
        Dim cmd1 As New OleDbCommand
        Dim sMsg As String
        Dim lRecordsAff As Integer
        Dim lError As Integer
        Dim sRollback As String = ""

        '== On Error GoTo GetRst_Error
        msLastSqlErrorMessage = ""
        cmd1.Connection = cnnUserDB
        cmd1.CommandText = sSql
        If bIsTransaction And (Not (sqlTran1 Is Nothing)) Then
            cmd1.Transaction = sqlTran1
        End If

        msLastSqlErrorMessage = ""
        '---DEFAULT timeout is 30 secs--
        sErrorMsg = ""
        Try
            lRecordsAff = cmd1.ExecuteNonQuery
            lAffected = lRecordsAff '--return result--
            mbExecuteCmd = True
        Catch ex As Exception
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            lAffected = lError
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "gbExecuteCmd:  Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & vbCrLf & _
                          "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(msCreateLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            mbExecuteCmd = False
        End Try

        '==cnnUserDB.Execute(sSql, lRecordsAff, ADODB.ExecuteOptionEnum.adExecuteNoRecords) '--04July2001=
    End Function  '-gbExecuteCmd-
    '= = = = = = = = = = = = = = = =
    '-== = =
    '-===FF->


    '==  Create Table and Index Functions--
    '---    for Main Create Jobs function..

    '---subroutine create table --
    '---subroutine create table --
    Private Function mbDb_createTable(ByRef cnnSQL As OleDbConnection, _
                                       ByVal sTableName As String, _
                                       ByVal sCreate As String, _
                                       ByVal sCreateLogPath As String, _
                                       ByRef iSqlErrors As Integer) As Boolean
        Dim bOk As Boolean
        Dim L1 As Integer
        Dim sErrorMsg As String

        Call gbLogMsg(sCreateLogPath, "Creating SQL Table:  '" & sTableName & "'..")
        Call gbLogMsg(sCreateLogPath, "SQL is:  " & sCreate)
        '==gAdvise "Creating SQL table '" + sTableName + "'.."
        bOk = mbExecuteCmd(cnnSQL, sCreate, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        If Not bOk Then
            Call gbLogMsg(sCreateLogPath, "  Failed." & vbCrLf)
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg & vbCrLf)
            MsgBox("Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            iSqlErrors = iSqlErrors + 1
            '== gbCreateJobsDB = False
        Else '--ok--  add privileges--
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  created ok..")
        End If '--create ok-
        '==Return
        mbDb_createTable = bOk
    End Function '--create table..-
    '= = = = = = = = = = = = = = = =

    '---subroutine to create INDEX --
    '---subroutine to create INDEX --

    Private Function mbDb_createIndex(ByRef cnnSQL As OleDbConnection, _
                                      ByVal sTableName As String, _
                                       ByVal fx As Integer, _
                                        ByVal sFldList As String, _
                                         ByVal sCreateLogPath As String, _
                                         ByRef iSqlErrors As Integer, _
                                         Optional ByVal bUnique As Boolean = False) As Boolean
        Dim bOk As Boolean
        Dim L1 As Integer
        Dim s1 As String
        Dim sErrorMsg As String
        Dim sSql As String
        Dim sUnique = IIf(bUnique, " UNIQUE ", "")

        s1 = sTableName & "_IDX" & Trim(CStr(fx))
        sSql = " CREATE " & sUnique & "INDEX " & s1 & " ON " & sTableName & " (" & sFldList & ")"
        sSql = sSql & "  WITH FILLFACTOR=80 "
        Call gbLogMsg(sCreateLogPath, " -- Creating SQL Index:  " & vbCrLf & sSql)
        bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        If Not bOk Then
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & " ** ERROR **  CREATE INDEX failed.." & vbCrLf & sErrorMsg)
            MsgBox("Table: " & sTableName & " ** ERROR **  CREATE INDEX failed.." & vbCrLf & sErrorMsg)
            iSqlErrors = iSqlErrors + 1
        Else
            Call gbLogMsg(sCreateLogPath, "  ==  Table: " & sTableName & " -- INDEX: " & s1 & " created ok..")
        End If '--ok-
        '==Return
        mbDb_createIndex = bOk
    End Function '--create index-
    '= = = = = = = = = = = =
    '-===FF->

    '---JobMatixPOS.. create CONSTRAINT --
    '---JobMatixPOS.. create CONSTRAINT --

    '=3101= Private Function mbDb_createConstraint(ByRef cnnSQL As OleDbConnection, _
    '=3101=                                         ByVal sSql As String, _
    '=3101=                                         ByVal sCreateLogPath As String, _
    '=3101=                                            ByRef iSqlErrors As Integer) As Boolean
    '=3101= Dim bOk As Boolean
    '=3101= Dim L1 As Integer
    '=3101= Dim s1 As String
    '=3101= Dim sErrorMsg As String
    '=3101=      Call gbLogMsg(sCreateLogPath, " -- Creating SQL Constraint:  " & vbCrLf & sSql)
    '=3101=     bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
    '=3101=     If Not bOk Then
    '=3101=        Call gbLogMsg(sCreateLogPath, "Constraint: " & vbCrLf & sSql & vbCrLf & _
    '=3101=                         " ** ERROR **  failed.." & vbCrLf & sErrorMsg)
    '=3101=        MsgBox("Constraint Sql: " & sSql & " ** ERROR **  failed.." & vbCrLf & sErrorMsg)
    '=3101=         iSqlErrors = iSqlErrors + 1
    '=3101=     Else
    '=3101=         Call gbLogMsg(sCreateLogPath, "  ==  Constraint: " & vbCrLf & sSql & vbCrLf & "  was created ok..")
    '=3101=     End If '--ok-
    '=3101= '==Return
    '=3101=    mbDb_createConstraint = bOk
    '=3101= End Function '--create index-
    '= = = = = = = = = = = =
    '-===FF->

    '--  Extract DateParts from DateCreated..---
    '--  Extract DateParts from DateCreated..---
    '------ Result Collection will hold FIVE Integers..--
    '------  <<  Year, DayOfYear, Hour, Minute, Second  >> ----

    Public Function gbGetDateCreatedDateParts(ByRef cnnSQL As OleDbConnection, _
                                              ByVal sSystemKey As String, _
                                                ByRef dateCreated As Date, _
                                                 ByRef colDateParts As Collection) As Boolean
        Dim sSql As String
        Dim rs1 As OleDbDataReader '== ADODB.Recordset

        gbGetDateCreatedDateParts = False
        sSql = "SELECT  DateCreated, "
        sSql = sSql & " DATEPART(yyyy, DateCreated) AS Year,  "
        sSql = sSql & " DATEPART(dayofyear, DateCreated) AS DayOfYear,  "
        sSql = sSql & " DATEPART(hour, DateCreated) AS Hour,  "
        sSql = sSql & " DATEPART(minute, DateCreated) AS Minute,  "
        sSql = sSql & " DATEPART(second, DateCreated) AS Second  "
        sSql = sSql & "   FROM SystemInfo WHERE (SystemKey='" & sSystemKey & "');  "

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetReader(cnnSQL, rs1, sSql) Then '= gbGetRst(cnnSQL, rs1, sSql) Then
            MsgBox("Failed to get systemInfo recordset..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--build dictionary of sysinfo items....-
            If Not (rs1 Is Nothing) Then
                colDateParts = New Collection '--  holds system settings..
                '==  If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                If rs1.Read Then  '== (Not rs1.EOF) Then '---
                    '--add all columun items....
                    dateCreated = CDate(rs1.Item("dateCreated")) '--send back full date for testing..-
                    colDateParts.Add("" & rs1.Item("Year"), "Year")
                    colDateParts.Add("" & rs1.Item("DayOfYear"), "DayOfYear")
                    colDateParts.Add("" & rs1.Item("Hour"), "Hour")
                    colDateParts.Add("" & rs1.Item("Minute"), "Minute")
                    colDateParts.Add("" & rs1.Item("Second"), "Second")
                    gbGetDateCreatedDateParts = True
                End If '-eof-
                rs1.Close()
            End If '--rs nothing=-
        End If '--get rs-
        rs1 = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function '--get date..-
    '= = = = = = = = =  = =  =
    '-===FF->

    '-- make POS SecurityId from JMPOS33_SECURITYID DateCreated.-

    Public Function gsMakeSecurityId(ByRef cnnSQL As OleDbConnection, _
                                         ByRef dateCreated As Date) As String
        Dim colDateParts As Collection
        Dim sDay As String
        Dim sYear As String
        Dim lngSeconds As Integer

        gsMakeSecurityId = ""
        If gbGetDateCreatedDateParts(cnnSQL, "JMPOS33_SECURITYID", dateCreated, colDateParts) Then '--got date..
            sYear = Trim(colDateParts.Item("Year"))
            sDay = Trim(colDateParts.Item("DayOfYear"))
            lngSeconds = (Val(colDateParts.Item("Hour")) * 3600)
            lngSeconds = lngSeconds + (Val(colDateParts.Item("Minute")) * 60) + Val(colDateParts.Item("Second"))
            gsMakeSecurityId = sYear & Trim(CStr(Len(sDay))) & sDay & Trim(CStr(lngSeconds))
        End If '--dateparts..-
    End Function '-security Id..-
    '= = = = = = = = = = = = =

    '---COVER for subroutine create table --

    Public Function gbDb_createTable(ByRef cnnSQL As OleDbConnection, _
                                       ByVal sTableName As String, _
                                       ByVal sCreate As String, _
                                         ByVal sCreateLogPath As String, _
                                          ByRef iSqlErrors As Integer) As Boolean

        gbDb_createTable = mbDb_createTable(cnnSQL, sTableName, sCreate, sCreateLogPath, iSqlErrors)
    End Function  '--create-
    '= = = = = = = = = = = = =
    '-===FF->

    '= 3107.815=
    '-- Make Create SQL for Document Archive Table.
    '==
    '==     3403.1009- 09-Oct-2017-
    '==      -- POS Emails now to use Server File-System to store Invoice PDF's for Email..
    '==            at \\[server]\users\public\JobMatixPOS-EmailQueue\ 
    '==                    (SystemInfo setting is :  "POS_EMAILQUEUE_SHAREPATH"
    '==               NB: (Table "DocArchive" to be DROPPED..)

    'Public Function gsMakeDocArchiveScript() As String
    '    Dim sCreateSql As String

    '    sCreateSql = "CREATE TABLE  dbo.DocArchive ( " ' == . 
    '    sCreateSql &= "  doc_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
    '    sCreateSql &= "  doc_revision INT NOT NULL DEFAULT -1,"
    '    sCreateSql &= "  doc_file_format nvarChar(15) NOT NULL DEFAULT '',"  '==  PDF/XPS -
    '    sCreateSql &= "  doc_app_type nvarChar(15) NOT NULL DEFAULT '',"     '==  PO/INVOICE/STATEMENT -
    '    sCreateSql &= "  doc_title nvarChar(100) NOT NULL DEFAULT '',"
    '    sCreateSql &= "  doc_description nvarChar(1000) NOT NULL DEFAULT '',"

    '    sCreateSql &= "  doc_staff_name nvarChar(50) NOT NULL DEFAULT '', "
    '    sCreateSql &= "  doc_supplier_id INT NOT NULL DEFAULT -1, "
    '    sCreateSql &= "  doc_customer_id INT NOT NULL DEFAULT -1, "
    '    sCreateSql &= "  doc_other_id INT NOT NULL DEFAULT -1, "

    '    sCreateSql &= "  doc_target_name nvarChar(250) NOT NULL DEFAULT '',"
    '    sCreateSql &= "  doc_target_email_address nvarChar(250) NOT NULL DEFAULT '',"
    '    sCreateSql &= "  doc_email_text nvarChar(max) NOT NULL DEFAULT '',"
    '    sCreateSql &= "  doc_email_date_sent datetime NULL,"
    '    sCreateSql &= "  doc_email_sent_ok bit NOT NULL DEFAULT 0,"

    '    sCreateSql &= "  doc_file_title nvarChar(200) NOT NULL DEFAULT '', "
    '    sCreateSql &= "  doc_file_content varbinary(max) NULL, "
    '    sCreateSql &= "  doc_date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
    '    sCreateSql &= " ); "  '--40 and done-- 

    '    gsMakeDocArchiveScript = sCreateSql

    'End Function  '-gsMakeDocArchiveScript=
    '= = = = = = = = = = = = =
    '-===FF->

    '=== Create for POS database = = =
    '--  Just CREATE all required POS3 tables--
    '--  updated 12-Nov-2014---

    Public Function gbCreatePOSdbTables(ByRef cnnSQL As OleDbConnection, _
                                       ByVal sSqlDBName As String, _
                                          ByVal sCreateLogPath As String, _
                                            ByRef iSqlErrors As Integer) As Boolean

        '= Dim k, i, j, ans As Integer
        '= Dim tx, isx, fx As Integer
        Dim bOk As Boolean
        Dim fx, L1 As Integer
        '==Dim s1, Msg As String
        Dim s1, sSql, sCreateSql, sTableName As String
         Dim sFldList, sErrorMsg As String
 
        gbCreatePOSdbTables = False
        mbIsTransaction = False
        iSqlErrors = 0
        msCreateLogPath = sCreateLogPath  '-- set up module-level path var..

        Call gbLogMsg(sCreateLogPath, "- Create POS gbCreatePOSdbTables started.. ")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '== sJT2SecurityId = "" '--we'll create our own.
        '-  CREATE DB now done by caller..

        '--Move to new db--
        '=3101= s1 = "USE " & sSqlDBName & vbCrLf
        '=3011= bOk = mbExecuteCmd(cnnSQL, s1, L1, sErrorMsg)
        Try
            cnnSQL.ChangeDatabase(sSqlDBName)
        Catch ex As Exception
            MsgBox("Error in 'gbCreatePOSdbTables'- Failed ChangeDatabase to: " & sSqlDBName & _
                          vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '==cnnSQL.BeginTrans()
        Try
            mSqlTran1 = cnnSQL.BeginTransaction
            mbIsTransaction = True
        Catch ex As Exception
            MsgBox("Error in 'gbCreatePOSdbTables'- Failed sql.BeginTransaction.." & _
                          vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '--CREATE all rquired tables--
        '--CREATE all rquired tables--
        '--CREATE all rquired tables--

        '-- SQL CREATE for table: Account_Numbers
        '-- SQL CREATE code for Table: Account_Numbers
        '=3101.1112= sTableName = "Accounts" '---..
        '=3101.1112= sCreateSql = "CREATE TABLE  dbo.Accounts ( " ' == . 
        '=3101.1112= sCreateSql &= "  AccountName nvarChar(20) PRIMARY KEY CLUSTERED,"   '-1--
        '=3101.1112= sCreateSql &= "  Account_acn nvarChar(31) NOT NULL DEFAULT '',"   '-2--
        '=3101.1112= sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP); "  '--3 and done-- 
        '=3101.1112= bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '=3101.1112= If Not bOk Then
        '=3101.1112=   Exit Function
        '=3101.1112= End If
        '---- end of table: Account_Numbers ----
        '= = = = = = = = = =
        bOk = True
        '-- SQL CREATE for table: Staff
        '-- SQL CREATE code for Table: Staff
        sTableName = "Staff" '---
        sCreateSql = "CREATE TABLE  dbo.Staff ( " ' == . 
        sCreateSql &= "  staff_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  barcode nvarChar(15) NOT NULL UNIQUE NONCLUSTERED,"
        sCreateSql &= "  lastName nvarChar(50) NOT NULL,"
        sCreateSql &= "  firstName nvarChar(50) NOT NULL,"
        sCreateSql &= "  docket_name nvarChar(50) NOT NULL,"
        sCreateSql &= "  position nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  isAdministrator bit NOT NULL DEFAULT 0,"
        sCreateSql &= "  inactive BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  dateOfBirth datetime NOT NULL,"   '-8--
        sCreateSql &= "  address nvarchar(max) NOT NULL DEFAULT '',"
        sCreateSql &= "  suburb nvarChar(40) NOT NULL DEFAULT '',"
        sCreateSql &= "  state nvarChar(30) NOT NULL DEFAULT '',"
        sCreateSql &= "  postcode nvarChar(10) NOT NULL DEFAULT '',"
        sCreateSql &= "  homePhone nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  mobile nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  emailAddress nvarChar(250) NOT NULL DEFAULT '',"
        sCreateSql &= "  status nvarChar(15) NOT NULL DEFAULT '',"
        '--  ==NB: original RM Column Name "external" is illegal in SQL 2005.. (RESERVED)..
        '== sCreateSql &= "  EX_external BIT NOT NULL DEFAULT 0,"   
        '== sCreateSql &= "  document_delivery_type TINYINT NOT NULL DEFAULT 0,"  
        sCreateSql &= "  password nvarChar(80) NOT NULL DEFAULT '',"       '-NEW =--
        sCreateSql &= "  passwordHint nvarChar(250) NOT NULL DEFAULT '',"   '-NEW =--
        sCreateSql &= "  staffPicture image NULL, "                         '-NEW =--
        sCreateSql &= "  date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= "); "  '--and done-- 

        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "lastName"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Staff ----
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: Supplier
        '-- SQL CREATE code for Table: Supplier
        sTableName = "Supplier" '---..
        sCreateSql = "CREATE TABLE  dbo.Supplier ( " ' == . 
        sCreateSql &= "  supplier_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  barcode nvarChar(15) NOT NULL UNIQUE NONCLUSTERED,"
        sCreateSql &= "  supplierName nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  grade nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  inactive BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  contactName nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  contactPosition nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  address nvarchar(max) NOT NULL DEFAULT '',"
        sCreateSql &= "  suburb nvarChar(40) NOT NULL DEFAULT '',"
        sCreateSql &= "  state nvarChar(30) NOT NULL DEFAULT '',"
        sCreateSql &= "  postcode nvarChar(10) NOT NULL DEFAULT '',"
        sCreateSql &= "  country nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  phone nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  fax nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  emailAddress nvarChar(250) NOT NULL DEFAULT '',"
        sCreateSql &= "  webSiteURL nvarChar(250) NOT NULL DEFAULT '',"
        sCreateSql &= "  altContactName nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  altContactPosition nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  altPhone nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  altFax nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  altEmail nvarChar(250) NOT NULL DEFAULT '',"
        sCreateSql &= "  freight_free BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  reject_backorders BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  deliveryDays INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  abn nvarChar(20) NOT NULL DEFAULT '', "
        '== sCreateSql &= "  document_delivery_type TINYINT NOT NULL DEFAULT 0 "
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '',"
        sCreateSql &= "  date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= "  ); "  '--and done-- 

        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "supplierName"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Supplier ----
        '= = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: Customer-
        '-- SQL CREATE code for Table: Customer
        sTableName = "Customer" '---..
        sCreateSql = "CREATE TABLE  dbo.Customer ( " ' == . 
        sCreateSql &= "  customer_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  barcode nvarChar(15) NOT NULL UNIQUE NONCLUSTERED,"
        '==sCreateSql &= "  notes ntext NOT NULL DEFAULT '',"
        '== sCreateSql &= "  status BIT NOT NULL DEFAULT 0,"   
        sCreateSql &= "  companyName nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  lastName nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  firstName nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  position nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  title nvarChar(10) NOT NULL DEFAULT '',"
        sCreateSql &= "  inactive BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  isAccountCust BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  pricingGrade nvarChar(1) NOT NULL DEFAULT '0',"
        '== sCreateSql &= "  customerDiscount  decimal NOT NULL DEFAULT 0,"
        sCreateSql &= "  openedStaff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  openedStaffName nvarChar(50) NOT NULL DEFAULT '',"
        '== sCreateSql &= "  accountMgrStaff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  creditLimit MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  creditDays INT NOT NULL DEFAULT 0,"
        sCreateSql &= "  address nvarchar(max) NOT NULL DEFAULT '',"
        sCreateSql &= "  suburb nvarChar(40) NOT NULL DEFAULT '',"
        sCreateSql &= "  state nvarChar(30) NOT NULL DEFAULT '',"
        sCreateSql &= "  postcode nvarChar(10) NOT NULL DEFAULT '',"
        sCreateSql &= "  country nvarChar(20) NOT NULL DEFAULT 'Australia',"
        sCreateSql &= "  phone nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  fax nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  mobile nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  email nvarChar(250) NOT NULL DEFAULT '',"
        sCreateSql &= "  abn nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  doNotEmailDocuments BIT NOT NULL DEFAULT 0,"

        '==  Target is new Build 4251..
        '=    '==   ALSO HERE we wanted to update the Create Module to add "Tags" column to the customer Table
        '==      BUT it stuffs up the importing Customers from RM, so column has to be added on the fly at POS Startup.
        '= sCreateSql &= " Tags varChar(2000) NOT NULL DEFAULT '',"

        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '',"
        '== sCreateSql &= "  overseas BIT NOT NULL DEFAULT 0,"   
        '--  ==NB: original RM Column Name "external" is illegal in SQL 2005.. (RESERVED)..
        '==sCreateSql &= "  EX_external BIT NOT NULL DEFAULT 0,"   '-35--
        sCreateSql &= "  date_created datetime not NULL DEFAULT CURRENT_TIMESTAMP,"
        '== sCreateSql &= "  is_barcode_printed BIT NOT NULL DEFAULT 0,"   '-37--
        '== sCreateSql &= "  document_delivery_type TINYINT NOT NULL DEFAULT 0,"  
        '== sCreateSql &= "  group_email_exclusion_id INT NOT NULL DEFAULT -1,"   
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        '==sCreateSql &= "  default_delivery_address INT NOT NULL DEFAULT -1); "   
        sCreateSql &= " ); "  '--40 and done-- 

        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "lastname"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Customer ----
        '= = = = = = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- grh-- New Tables--
        '--  StockBrands, Cat1, Cat2 ----
        '--  StockBrands, Cat1, Cat2 ----

        '-- Create Reference Table of BRANDS..--
        '-- Create Reference Table of BRANDS..--
        sTableName = "StockBrands"
        sCreateSql = " CREATE TABLE  dbo.StockBrands   ( "
        sCreateSql &= " Brand_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreateSql &= " BrandName  varchar(50) NOT NULL DEFAULT '', " '---descr --
        sCreateSql &= " date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, "
        sCreateSql &= " date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP); "
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----
        fx = 1 '---index no...
        sFldList = "BrandName"
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '===
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '== '-- Create Reference Table of Cat1/Cat2..--

        '-- Create Reference Table of Cat1..--
        sTableName = "category1"
        sCreateSql = " CREATE TABLE  dbo.category1  ( "
        sCreateSql &= " cat1_key  nvarchar(6) PRIMARY KEY CLUSTERED, "
        sCreateSql &= " description  nvarchar(36)  NOT NULL DEFAULT '', " '---descr --
        sCreateSql &= " date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, "
        sCreateSql &= " date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP); "
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----
        fx = 1 '---index no...
        sFldList = "description"
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '===
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- Create Reference Table of Cat2..--
        sTableName = "category2"
        sCreateSql = " CREATE TABLE  dbo.category2   ( "
        sCreateSql &= " cat2_key  nvarchar(6) PRIMARY KEY CLUSTERED, " '--- descr --
        sCreateSql &= " description  nvarchar(36)  NOT NULL DEFAULT '', " '---descr --
        sCreateSql &= " date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, "
        sCreateSql &= " date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP); "
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----
        fx = 1 '---index no...
        sFldList = "description"
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '===
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")
        '-- END NEW-  StockBrands, Cat1, Cat2 ----
        '-- END NEW-  StockBrands, Cat1, Cat2 ----

        '-- Payment Types reference..--
        '-- Create Reference Table ofPaymentTypes..--
        '=3101.1206= sTableName = "PaymentTypes"
        '=3101.1206= sCreateSql = " CREATE TABLE  dbo.PaymentTypes  ( "
        '=3101.1206= sCreateSql &= " PaymentType_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        '=3101.1206= sCreateSql &= " description  nvarchar(31) NOT NULL DEFAULT '', " '---descr --
        '=3101.1206= sCreateSql &= " isCash BIT NOT NULL DEFAULT 0,"
        '=3101.1206= sCreateSql &= " inactive BIT NOT NULL DEFAULT 0,"
        '=3101.1206= sCreateSql &= " isElectronicTfr BIT NOT NULL DEFAULT 0,"
        '=3101.1206= sCreateSql &= " date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, "
        '=3101.1206= sCreateSql &= " date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP); "
        '=3101.1206= If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '=3101.1206= '--add other indexes----
        '=3101.1206= fx = 1 '---index no...
        '=3101.1206= sFldList = "description"
        '=3101.1206= If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '=3101.1206= '= = =  = = = == = = = =  = == =
        '=3101.1206= If Not bOk Then
        '=3101.1206= Exit Function
        '=3101.1206= End If
        '=3101.1206= Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- pricing Grades--
        '-- Create Reference Table of pricing Grades..--
        '-- All rules refer to a percentage plus or minus RRP (sell_ex) or COST (cost_ex) --
        '--  NB  Cust Grade ZERO means use selling price in stock record...-- 
        '=3101.1112= sTableName = "PricingGradeRules"
        '=3101.1112= sCreateSql = " CREATE TABLE  dbo.PricingGradeRules  ( "
        '=3101.1112= sCreateSql &= " rule_id INT IDENTITY (1,1)  PRIMARY KEY CLUSTERED, "
        '=3101.1112= '-- Grade A, B, C etc..
        '=3101.1112= sCreateSql &= " pricingGrade nvarChar(1) NOT NULL UNIQUE NONCLUSTERED,"
        '=3101.1112= sCreateSql &= " inactive BIT NOT NULL DEFAULT 0,"
        '=3101.1112= '--- "cost+", "sell-" --
        '=3101.1112= sCreateSql &= " Rule_base  nvarchar(15)  NOT NULL DEFAULT '', " '--- "cost+", "sell-" --
        '=3101.1112= sCreateSql &= " percentage decimal(5,2) NOT NULL DEFAULT 0,"
        '=3101.1112= sCreateSql &= " date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, "
        '=3101.1112= sCreateSql &= " date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '=3101.1112= If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '= = =  = = = == = = = =  = = = =
        '=3101.1112= If Not bOk Then
        '=3101.1112= Exit Function
        '=3101.1112= End If

        '- Tax Codes--
        '-- Create Reference Table of TaxCodes..--
        '=3101.1112= sTableName = " TaxCodes"
        '=3101.1112= sCreateSql = " CREATE TABLE dbo.TaxCodes   ( "
        '=3101.1112= sCreateSql &= " code nvarchar(3) PRIMARY KEY CLUSTERED, " '--- code eg GST --
        '=3101.1112= sCreateSql &= " description  nvarchar(36)  NOT NULL DEFAULT '', " '---descr --
        '=3101.1112= sCreateSql &= " percentage decimal(5,2) NOT NULL DEFAULT 0,"
        '=3101.1112= sCreateSql &= " sales_account nvarChar(31) NOT NULL DEFAULT '',"
        '=3101.1112= sCreateSql &= " goods_account nvarChar(31) NOT NULL DEFAULT '',"
        '=3101.1112= sCreateSql &= " date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, "
        '=3101.1112= sCreateSql &= " date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP); "
        '=3101.1112= If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '=3101.1112= '--add other indexes----
        '=3101.1112= fx = 1 '---index no...
        '=3101.1112= sFldList = "description"
        '=3101.1112= If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '=3101.1112= '=== = = = = = = = = = ==
        '=3101.1112= If Not bOk Then
        '=3101.1112=    Exit Function
        '=3101.1112= End If

        '-- Stock  --
        '-- Stock  --
        '-- SQL CREATE code for Table: Stock--
        sTableName = "stock" '---..
        sCreateSql = "CREATE TABLE  dbo.Stock ( " ' == . 
        '== use iNT for _id. -- sCreateSql &= "  stock_id FLOAT PRIMARY KEY CLUSTERED,"   
        sCreateSql &= "  stock_id INT IDENTITY (1,1)  PRIMARY KEY CLUSTERED,"
        '==3103.301=sCreateSql &= "  barcode nvarChar(15) NOT NULL UNIQUE NONCLUSTERED,"
        sCreateSql &= "  barcode nvarChar(40) NOT NULL UNIQUE NONCLUSTERED,"
        sCreateSql &= "  inactive BIT NOT NULL DEFAULT 0,"
        '= sCreateSql &= "  allow_fractions BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  model_no nvarChar(40) NOT NULL DEFAULT '',"
        sCreateSql &= "  description nvarChar(40) NOT NULL,"
        sCreateSql &= "  sales_prompt nvarChar(50) NOT NULL DEFAULT '',"
        '== sCreateSql &= "  isServiceItem BIT NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  isLabour BIT NOT NULL DEFAULT 0,"
        '-- 3301.607- Imports 'Static_qty' ie Service, labour, Telstra pre-paid etc.
        sCreateSql &= "  isNonStockItem BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  track_serial BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  allow_renaming BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  longDescription nvarchar(max) NOT NULL DEFAULT '',"
        sCreateSql &= "  cat1 nvarChar(6) NOT NULL DEFAULT '',"     '== REFERENCES category1(cat1_key),"
        sCreateSql &= "  cat2 nvarChar(6) NOT NULL DEFAULT '',"      '== REFERENCES category2(cat2_key),"
        '== sCreateSql &= "  brand_id INT NOT NULL REFERENCES StockBrands(brand_id),"
        sCreateSql &= "  BrandName  varchar(50) NOT NULL DEFAULT '', " '---descr --
        sCreateSql &= "  goods_taxCode nvarChar(7) NOT NULL DEFAULT '',"
        sCreateSql &= "  costExTax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sales_taxCode nvarChar(7) NOT NULL DEFAULT '',"
        sCreateSql &= "  sellExTax MONEY NOT NULL DEFAULT 0,"
        '-- Stock quantities/levels only relevant if NOT Service item..
        sCreateSql &= "  qtyInStock INT NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  qtyOnLayby INT NOT NULL DEFAULT 0,"
        '= sCreateSql &= "  salesorder_qty INT NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  static_quantity BIT NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  bonus MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  reOrderLevel INT NOT NULL DEFAULT 0,"
        sCreateSql &= "  order_quantity INT NOT NULL DEFAULT 0,"
        sCreateSql &= "  supplier_id INT NOT NULL  REFERENCES Supplier(supplier_id),"
        sCreateSql &= "  freight BIT NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  tare_weight real NOT NULL DEFAULT 0,"
        '== EACH only --sCreateSql &= "  unitof_measure TINYINT NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  weighted BIT NOT NULL DEFAULT 0,"
        '--  ==NB: original RM Column Name "external" is illegal in SQL 2005.. (RESERVED)..
        '== sCreateSql &= "  EX_external BIT NOT NULL DEFAULT 0,"                     
        '== sCreateSql &= "  picture_file_name nvarChar(255) NOT NULL DEFAULT '',"    
        sCreateSql &= "  cost_account nvarChar(50) NOT NULL DEFAULT '', "
        sCreateSql &= "  income_account nvarChar(50) NOT NULL DEFAULT '',"
        '--  NEW fields- grh..--
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '',"
        sCreateSql &= "  productPicture image NULL, "
        sCreateSql &= "  date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= "  ); "  '-- and done-- 

        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "supplier_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 2 '---index no...
        sFldList = "cat1"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Stock ----
        '= = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: SupplierCode
        '-- SQL CREATE code for Table: SupplierCode
        sTableName = "SupplierCode" '---..
        sCreateSql = "CREATE TABLE  dbo.SupplierCode ( " ' == . 
        '= sCreateSql &= "  supcode nvarChar(15) NOT NULL DEFAULT '',"  
        '=4201.1002= sCreateSql &= "  supcode nvarChar(15) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  supcode nvarChar(40) NOT NULL, "
        sCreateSql &= "  supplier_id INT NOT NULL REFERENCES Supplier(supplier_id),"
        sCreateSql &= "  stock_id INT NOT NULL REFERENCES Stock(stock_id),"
        sCreateSql &= "  date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, "
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP); "

        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "supplier_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: SupplierCode ----
        '= = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")


        '-- SQL CREATE for table: Audit
        '-- SQL CREATE code for Table: Audit
        '=3101.1112= sTableName = "Audit" '---..
        '=3101.1112= sCreateSql = "CREATE TABLE  dbo.Audit ( " ' == . 
        '=3101.1112= sCreateSql &= "  audit_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        '=3101.1112= sCreateSql &= "  drawer nvarChar(8) NOT NULL DEFAULT '',"
        '=3101.1112= sCreateSql &= "  source_id INT NOT NULL DEFAULT -1,"
        '=3101.1112= sCreateSql &= "  tran_type nvarChar(15) NOT NULL DEFAULT '',"
        '=3101.1112= sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        '=3101.1112= sCreateSql &= "  movementQty FLOAT NOT NULL DEFAULT 0,"
        '=3101.1112= sCreateSql &= "  stock_value MONEY NOT NULL DEFAULT 0, "
        '=3101.1112= sCreateSql &= "  comments ntext NOT NULL DEFAULT '',"
        '=3101.1112= sCreateSql &= "  audit_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        '=3101.1112= '--  ==NB: original RM Column Name "external" is illegal in SQL 2005.. (RESERVED)..
        '=3101.1112= '== sCreateSql &= "  EX_exported BIT NOT NULL DEFAULT 0 "   
        '=3101.1112= sCreateSql &= " ); "  '= and done-- 
        '- - - - - - - -
        '=3101.1112= If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '=3101.1112= '--add other indexes----  
        '=3101.1112= fx = 1 '---index no...
        '=3101.1112= sFldList = "tran_type"  '--
        '=3101.1112= If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '=3101.1112= '---- end of table: Audit ----
        '= = = = = = = = = = = = = = = = = = = 
        '=3101.1112= If Not bOk Then
        '=3101.1112=   Exit Function
        '=3101.1112= End If

        '-- PURCHASE ORDERS --
        '-- SQL CREATE for table: PurchaseOrders
        '-- SQL CREATE code for Table: PurchaseOrders
        sTableName = "PurchaseOrder" '---..
        sCreateSql = "CREATE TABLE  dbo.PurchaseOrder ( " ' == . 
        sCreateSql &= "  order_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  revision INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  order_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  due_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  supplier_id INT NOT NULL REFERENCES Supplier(supplier_id),"
        sCreateSql &= "  orderNoSuffix nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  delivery_address nvarChar(511) NOT NULL DEFAULT '',"
        '= 3107.815- Have received some items..--
        sCreateSql &= "  isReceiving BIT NOT NULL default 0, "
        sCreateSql &= "  isCompleted BIT NOT NULL default 0, "
        sCreateSql &= "  isClosedForBackorders  BIT NOT NULL default 0, "
        sCreateSql &= "  isCancelled BIT NOT NULL default 0, "
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '',"
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= " );"  '-- and done-- 
        '----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "supplier_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: P. Orders ----
        '= = = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: PurchaseOrdersLine
        '-- SQL CREATE code for Table: PurchaseOrdersLine
        sTableName = "PurchaseOrderLine" '---..
        sCreateSql = "CREATE TABLE  dbo.PurchaseOrderLine ( "
        sCreateSql &= "  line_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  order_id INT NOT NULL REFERENCES PurchaseOrder(Order_id),"
        sCreateSql &= "  supplier_id INT NOT NULL REFERENCES Supplier(supplier_id),"
        sCreateSql &= "  stock_id INT NOT NULL REFERENCES Stock(stock_id),"
        '=4201.1002=  sCreateSql &= "  suppliercode nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  suppliercode nvarChar(40) NOT NULL DEFAULT '',"
        sCreateSql &= "  goods_taxCode nvarChar(7) NOT NULL DEFAULT '',"
        sCreateSql &= "  cost_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  cost_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  quantity int NOT NULL DEFAULT 0,"
        '= 3107.815- Have received some items.. (in RM indicator in "status"column)=--
        sCreateSql &= "  qtyReceived int NOT NULL DEFAULT 0,"
        sCreateSql &= "  status nvarChar(255) NOT NULL DEFAULT '',"
        '== sCreateSql &= "  goods_id INT NOT NULL DEFAULT -1 REFERENCES Goods(goods_id),"   
        '-- NB Can't reference goods before it is created..
        '--   Create this FKEY ref. after Goods is created.
        sCreateSql &= "  goods_id INT NOT NULL DEFAULT -1, "   '-11--REFERENCES Goods(goods_id)--
        sCreateSql &= "  date_updated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= " );"  '-- and done-- 
        '= sCreateSql &= "  unitof_measure TINYINT NOT NULL DEFAULT 0); "  '-- and done-- 
        '----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "supplier_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: OrdersLine ----
        '= = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-  GoodsReceived-
        '-- SQL CREATE for table: GoodsReceived
        sTableName = "GoodsReceived" '---..
        sCreateSql = "CREATE TABLE  dbo.GoodsReceived ( "
        sCreateSql &= "  goods_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  goods_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  supplier_id INT NOT NULL REFERENCES Supplier(supplier_id),"
        sCreateSql &= "  invoice_no nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  invoice_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  orderNoSuffix nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  order_id INT NOT NULL DEFAULT -1, " '= REFERENCES PurchaseOrders(order_id),"
        '--  CAN'T import terminal Id !! - (NOT NULL)
        '= sCreateSql &= "  terminal_id nvarChar(150) NOT NULL DEFAULT '',"  '--  Computer name-
        '= sCreateSql &= "  cashDrawer nvarChar(15) NOT NULL DEFAULT '',"  '--  TILL name A..Z-
        '= sCreateSql &= "  currentWindowsUserName nvarChar(80) NOT NULL DEFAULT '',"  '--  user name-
        '== sCreateSql &= "  exported BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  freight_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  freight_taxCode nvarChar(7) NOT NULL DEFAULT '',"
        sCreateSql &= "  freight_taxPercentage decimal NOT NULL DEFAULT 0,"
        sCreateSql &= "  freight_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  freight_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  discount_nett MONEY NOT NULL DEFAULT 0,"   '--after deducting tax disc.
        sCreateSql &= "  discount_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_expected MONEY NOT NULL DEFAULT 0, "
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '' "
        '== sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= ");"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "supplier_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Goods ----
        '= = = = = = = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: GoodsLine
        '-- SQL CREATE code for Table: GoodsLine-
        sTableName = "GoodsReceivedLine" '---..
        sCreateSql = "CREATE TABLE  dbo.GoodsReceivedLine ( " ' == . 
        sCreateSql &= "  line_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  goods_id INT NOT NULL REFERENCES GoodsReceived(goods_id),"
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        sCreateSql &= "  goods_taxCode nvarChar(7) NOT NULL DEFAULT '',"
        sCreateSql &= "  goods_taxPercentage decimal NOT NULL DEFAULT 0,"
        sCreateSql &= "  cost_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  cost_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  cost_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sell_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  quantity int NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0 "
        '== sCreateSql &= "  unitof_measure TINYINT NOT NULL DEFAULT 0, "  '--and done-- 
        '= sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= ");"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "stock_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: GoodsLine ----
        '= = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- NOW we can create FKEY constraint for OrdersLine to Goods..--
        '-- NOW we can create FKEY constraint for OrdersLine to Goods..--

        '== NO.  won't work when PO exists with no Goods, will be a violation..
        '== NO.  sCreateSql = "ALTER TABLE dbo.PurchaseOrderLine WITH NOCHECK "
        '== NO.  sCreateSql &= " ADD CONSTRAINT FK_P_OrderLine_Goods1 "
        '== NO.  sCreateSql &= " FOREIGN KEY (goods_id) REFERENCES goodsReceived(goods_id); "
        '== NO.  If Not mbDb_createConstraint(cnnSQL, sCreateSql, sCreateLogPath, iSqlErrors) Then
        '== NO.  MsgBox("Failed to add FKEY Constraint on PurchaseOrderLine..")
        '== NO.  End If
        '---- end of ADD FKEY on ordersLine to Goods. ----
        '= = = = = = = = = = = = = = = = = = = = = = = = = = == =


        '-- SQL CREATE for table: SalesOrder (ie QUOTES)-
        '-- SQL CREATE code for Table: SalesOrder
        sTableName = "SalesOrder" '---..
        sCreateSql = "CREATE TABLE  dbo.SalesOrder ( " ' == . 
        sCreateSql &= "  salesorder_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  salesorder_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        '== sCreateSql &= "  expiry_date datetime NOT NULL DEFAULT '25-Dec-2100',"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  customer_id INT NOT NULL REFERENCES Customer(customer_id),"
        '== sCreateSql &= "  transaction nvarChar(2) NOT NULL DEFAULT '',"   
        '--  ==NB: original RM Column Name "transaction" is illegal.. (RESERVED WORD)..
        sCreateSql &= "  transactionType nvarChar(15) NOT NULL DEFAULT '',"
        '== sCreateSql &= "  status nvarChar(15) NOT NULL DEFAULT '',"
        '== sCreateSql &= "  original_id INT NOT NULL DEFAULT -1,"
        '== sCreateSql &= "  custom nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  subtotal_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  discount_nett MONEY NOT NULL DEFAULT 0,"   '--after deducting tax disc.
        sCreateSql &= "  discount_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  rounding MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0,"
        '= sCreateSql &= "  gp MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  deliveryInstructions nvarchar(max) NOT NULL DEFAULT '',"   '-NEW --
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '' "
        '== sCreateSql &= "  exported BIT NOT NULL DEFAULT 0 "  '-- and done-- 
        '== sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= ");"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "transactionType"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: SalesOrder ----
        '= = = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: SalesOrderLine
        '-- SQL CREATE code for Table: SalesOrderLine
        sTableName = "SalesOrderLine" '---..
        sCreateSql = "CREATE TABLE  dbo.SalesOrderLine ( "
        sCreateSql &= "  line_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  salesorder_id INT NOT NULL REFERENCES SalesOrder(salesorder_id),"
        '== sCreateSql &= "  status nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        '-- grh added this description column..---
        sCreateSql &= "  description nvarChar(40) NOT NULL DEFAULT '', "  '--EX table "SalesOrderDesc"-- 
        sCreateSql &= "  cost_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  cost_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sell_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sales_taxCode nvarChar(7) NOT NULL DEFAULT '',"
        sCreateSql &= "  sales_taxPercentage decimal NOT NULL DEFAULT 0,"
        sCreateSql &= "  sell_inc MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  rrp MONEY NOT NULL DEFAULT 0,"  - Is Sell_inc !
        sCreateSql &= "  sellActual_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sellActual_Tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sellActual_inc MONEY NOT NULL DEFAULT 0,"
        '-- NB: Quantity may be LABOUR (eg serviceItem)..
        sCreateSql &= "  quantity decimal(7,4) NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0"
        '== sCreateSql &= "  parentline_id INT NOT NULL DEFAULT -1, " '= REFERENCES SalesOrderLine(line_id),"
        '== sCreateSql &= "  package BIT NOT NULL DEFAULT 0,"   
        '== sCreateSql &= "  PO_orderline_id INT NOT NULL DEFAULT -1, " '= REFERENCES PurchaseOrderLine(line_id),"
        '== sCreateSql &= "  unitof_measure TINYINT NOT NULL DEFAULT 0, "  '-- and done-- 
        sCreateSql &= " );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "stock_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: SalesOrderLine ----
        '= = = = = = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: SerialAudit
        '-- SQL CREATE code for Table: SerialAudit
        sTableName = "SerialAudit" '---..
        sCreateSql = "CREATE TABLE  dbo.SerialAudit ( "
        sCreateSql &= "  serial_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        sCreateSql &= "  SerialNumber nvarChar(40) NOT NULL DEFAULT '',"
        sCreateSql &= "  isInStock BIT NOT NULL DEFAULT 0,"
        '--  Status:  InStock, OnLayby, Returned --
        sCreateSql &= "  status nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  warranty_date datetime NOT NULL, " '= DEFAULT '25-Dec-2100',"
        sCreateSql &= "  date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= " );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "serialnumber"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: SerialAudit ----
        '= = = = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: SerialAuditTrail --
        '-- SQL CREATE code for Table: SerialAuditTrail
        sTableName = "SerialAuditTrail" '---..
        sCreateSql = "CREATE TABLE  dbo.SerialAuditTrail ( "
        sCreateSql &= "  trail_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        sCreateSql &= "  serialAudit_id INT NOT NULL REFERENCES SerialAudit(serial_id),"
        sCreateSql &= "  original_id INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  tran_type nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  type_id INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  type_line_id INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  trail_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  movement INT NOT NULL DEFAULT 0, "       '-- and done-- 
        sCreateSql &= " is_RM_transaction bit NOT NULL DEFAULT 0, "
        sCreateSql &= " RM_tr_detail nvarchar(255) NOT NULL DEFAULT '' "
        sCreateSql &= " );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "type_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: SerialAuditTrail ----
        '= = = = = = = = = = = = = = = = = = = = == 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")



        '-- Returns --
        '-- Returns --
        '==
        '==  v3.3.3307.0202..  02-Feb-2017= ===
        '==     Returns- Add SupplierReturns Table,  
        '==      and accept GoodsShipped info from RAs Application to update Stock and SerialAudit Info...
        '==       AND INSERT Returns table records for the RA(s),..
        '-- SQL CREATE for table: Returns
        '-- SQL CREATE code for Table: Returns
        sTableName = "SupplierReturns" '---..
        sCreateSql = "CREATE TABLE  dbo.SupplierReturns ( "
        sCreateSql &= "  return_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  return_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  staff_name nvarChar(50) NOT NULL DEFAULT '',"
        sCreateSql &= "  supplier_id INT NOT NULL REFERENCES Supplier(supplier_id),"
        '== sCreateSql &= "  ra_no nvarChar(20) NOT NULL DEFAULT '',"  '= SEE SupplierReturnLine-
        '== sCreateSql &= "  exported BIT NOT NULL DEFAULT 0,"
        sCreateSql &= "  freight_tax nvarChar(3) NOT NULL DEFAULT '',"
        sCreateSql &= "  freight_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  freight_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0, "  '-- and done-- 
        sCreateSql &= "  comments ntext NOT NULL DEFAULT '' "
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "supplier_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Returns ----
        '= = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- ReturnsLine -- Can be a packge of multiple RAs..
        '-- SQL CREATE code for Table: ReturnsLine
        sTableName = "SupplierReturnLine" '---..
        sCreateSql = "CREATE TABLE  dbo.SupplierReturnLine ( "
        sCreateSql &= "  line_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  return_id INT NOT NULL REFERENCES SupplierReturns(return_id),"
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        sCreateSql &= "  serialAudit_id int NOT NULL DEFAULT -1,"
        sCreateSql &= "  serialNumber nvarChar(40) NOT NULL DEFAULT '',"
        '-- grh added lots columns..---
        sCreateSql &= "  invoice_no nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  ra_id int NOT NULL DEFAULT -1,"     '=Jobmatix RA-id-
        sCreateSql &= "  supplier_RMA_no nvarChar(60) NOT NULL DEFAULT '', "
        sCreateSql &= "  barcode nvarChar(40) NOT NULL DEFAULT '',  "
        sCreateSql &= "  description nvarChar(40) NOT NULL DEFAULT '', "
        sCreateSql &= "  quantity int NOT NULL DEFAULT 0,"
        sCreateSql &= "  symptoms nvarChar(511) NOT NULL DEFAULT '', "
        sCreateSql &= "  request_notes nvarChar(2040) NOT NULL DEFAULT '', "
        sCreateSql &= "  goods_taxCode nvarChar(3) NOT NULL DEFAULT '',"
        sCreateSql &= "  cost_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  cost_inc MONEY NOT NULL DEFAULT 0 "
        '== sCreateSql &= "  unitof_measure TINYINT NOT NULL DEFAULT 0, "
        sCreateSql &= " );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "stock_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: ReturnsLine ----
        '= = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '---- end of table: ReturnsLine ----
        '---- end of table: ReturnsLine ----

        '-- Invoice Table ---
        '-- SQL CREATE code for Table: Invoice- (Debits)--
        sTableName = "Invoice" '---..
        sCreateSql = "CREATE TABLE  dbo.Invoice ( "
        '= sCreateSql &= "  docket_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  invoice_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  invoice_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  customer_id INT NOT NULL REFERENCES Customer(customer_id),"
        '== sCreateSql &= "  transaction nvarChar(2) NOT NULL DEFAULT '',"   
        '--  ==NB: original RM Column Name "transaction" is illegal.. (RESERVED WORD)..
        '- Sale, Refund ONLY-   NO MORE for accountpayment, CreditNote.. --
        sCreateSql &= "  transactionType nvarChar(15) NOT NULL DEFAULT '',"
        '-- And "isOnAccount" Forces item into Debtors ledger. (Account customers can have off-account cash sales)-
        '--  If ON, the SALE is equiv to RM "IV", and paymenyCredit equiv to RM "IP" 
        sCreateSql &= "  isOnAccount bit NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  custom nvarChar(20) NOT NULL DEFAULT '',"
        sCreateSql &= "  payment_id INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  JobNumber INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  delivered_layby_id INT NOT NULL DEFAULT -1, " '=3403.522=  REFERENCES Laybys-,"
        '== sCreateSql &= "  isReversal bit NOT NULL DEFAULT 0,"
        sCreateSql &= "  original_id INT NOT NULL DEFAULT -1, " '==  REFERENCES Invoices( invoice_id),"
        '== sCreateSql &= "  origin TINYINT NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  drawer nvarChar(1) NOT NULL DEFAULT '',"
        sCreateSql &= "  terminal_id nvarChar(150) NOT NULL,"  '--  Computer name-
        sCreateSql &= "  cashDrawer nvarChar(15) NOT NULL DEFAULT '',"  '--  TILL name A..Z-
        sCreateSql &= "  currentWindowsUserName nvarChar(80) NOT NULL DEFAULT '',"  '--  user name-
        sCreateSql &= "  subtotal_ex_non_taxable MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_ex_taxable MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  discount_nett MONEY NOT NULL DEFAULT 0,"   '--after deducting tax disc.
        sCreateSql &= "  discount_tax MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  cashOut MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  rounding MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  amountDebitedToAccount MONEY NOT NULL DEFAULT 0,"  '--always Full Transaction Amt.
        '== sCreateSql &= "  amountCreditedToAccount MONEY NOT NULL DEFAULT 0,"  '-- eg for "IP" Account Payment..
        '== sCreateSql &= "  gp MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  commission MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  bonus MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  archive BIT NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  receipt_id INT NOT NULL DEFAULT -1, "
        '== EXTRA column grh==
        sCreateSql &= "  deliveryInstructions nvarchar(max) NOT NULL DEFAULT '',"   '-NEW --
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '' "
        '== sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "transactionType"  '--
        If bOk Then Call mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 2 '---index no...
        sFldList = "customer_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Docket ----
        '= = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: InvoiceLines-
        '-- SQL CREATE code for Table: InvoiceLines-
        sTableName = "InvoiceLine" '---..
        sCreateSql = "CREATE TABLE  dbo.InvoiceLine ( "
        sCreateSql &= "  line_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  invoice_id INT NOT NULL REFERENCES Invoice(invoice_id),"
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        '== EXTRA column grh==
        sCreateSql &= "  description nvarChar(40) NOT NULL DEFAULT '',"   '-NEW --
        sCreateSql &= "  serialNumber nvarChar(40) NOT NULL DEFAULT '',"  '--MUST have Qty=1 --
        sCreateSql &= "  serialAudit_id INT NOT NULL DEFAULT -1, " '== REFERENCES SerialAudit(serial_id),"
        sCreateSql &= "  cost_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  cost_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sell_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sales_taxCode nvarChar(7) NOT NULL DEFAULT '',"
        sCreateSql &= "  sales_taxPercentage decimal NOT NULL DEFAULT 0,"
        sCreateSql &= "  sell_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sellActual_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sellActual_Tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sellActual_inc MONEY NOT NULL DEFAULT 0,"
        '--  Quantity may be LABOUR (eg serviceItem)..
        sCreateSql &= "  quantity decimal(7,4) NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0, "
        sCreateSql &= "  gross_profit MONEY NOT NULL DEFAULT 0"
        '= sCreateSql &= "  customer_id INT NOT NULL REFERENCES Customer(customer_id),"
        '== sCreateSql &= "  package_id int NOT NULL DEFAULT -1 REFERENCES Stock(stock_id),"
        '== sCreateSql &= "  gp MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  unitof_measure TINYINT NOT NULL DEFAULT 0, "  '-- and done-- 
        sCreateSql &= " );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "invoice_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 2 '---index no...
        sFldList = "SerialNumber"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 3 '---index no...
        sFldList = "stock_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '== fx = 4 '---index no...
        '== sFldList = "customer_id"  '--
        '== If bOk Then Call mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: invoice=Line ----
        '= = = = = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- Payments --
        '-- Payments --
        '-- SQL CREATE code for Table: Payments--
        '==
        '==     v3.3.3301.608..  08-June-2016= ===
        '==       We NEED overall Payment record to collects details for the same Payment Event..  
        '==       >> Update Payments Schemas- Now is Just (payments and paymentDetails)-
        '==--      >>       NO !! Disbursements replaced by accountPayment Invoice records as per RM "IP" records..
        '==--      >>  NO !! Disbursements re-instated..accountPayment Invoice "IP" records is DROPPED..
        '== Payment Details only holds INCOMING..
        '==  ALL NEW--
        '==     v3.4.3401.327- Drop Cashout column from payment record..
        '==         Add Payment-columns for Refund as EFTPOS Dr/Cr-
        '==       (Change now recorded also in Comments, cashout dropped..)
        '==  
        sTableName = "Payments" '---..
        sCreateSql = "CREATE TABLE  dbo.Payments ( "
        sCreateSql &= "  payment_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  customer_id INT NOT NULL REFERENCES Customer(customer_id),"
        '-- For Account Payments, a Payment can apply to multiple Invoices.
        '--    see Invoice Table (accountPayments) for distribution..
        '--  invoice_id will apply to the only invoice for non-account sales,
        '---   and to the first invoice covered by an Account payment..
        sCreateSql &= "  invoice_id INT NOT NULL DEFAULT -1, " '=3101.2106=  REFERENCES Invoice(invoice_id),"
        '---Sale, Refund, account (Payment)--
        sCreateSql &= "  transactionType nvarChar(15) NOT NULL DEFAULT '',"  '--Sale, Refund, account (Payment)-
        '--3303.0109=  Special account(Payment) From when applying REFUND value to Debtors invoices
        '= SEE refundDetail Table. sCreateSql &= "  isRefundDisbursement bit NOT NULL DEFAULT 0,"
        '= SEE refundDetail Table. sCreateSql &= "  originalRefundinvoice_id INT NOT NULL DEFAULT -1, " 
        '= REFERENCES Invoice(invoice_id),"
        '--
        sCreateSql &= "  payment_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        '-- Reversal only for Debtors Payments.
        sCreateSql &= "  isReversal bit NOT NULL DEFAULT 0,"
        sCreateSql &= "  originalPayment_id INT NOT NULL DEFAULT -1, "
        '--
        sCreateSql &= "  terminal_id nvarChar(150) NOT NULL,"  '--  Computer name-
        sCreateSql &= "  cashDrawer nvarChar(15) NOT NULL DEFAULT '',"  '--  TILL name A..Z-
        sCreateSql &= "  currentWindowsUserName nvarChar(80) NOT NULL DEFAULT '',"  '--  user name-
        '-- == NB:totalAmountReceived=  Cash TENDERED + EFT, cheque etc..=== 
        sCreateSql &= "  totalAmountReceived MONEY NOT NULL DEFAULT 0,"
        '-- == NB:  changeGiven and cashoutGiven are mutually EXCLUSIVE.=== 
        sCreateSql &= "  discountGivenOnPayment MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  changeGiven MONEY NOT NULL DEFAULT 0,"
        '==3401.327= sCreateSql &= "  cashoutGiven MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  nettAmountCredited MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  amountDebitedToAccount MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  refundCashAmount MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  refundAsCreditNoteCredited MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  refundAsEftPosDr MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  refundAsEftPosCr MONEY NOT NULL DEFAULT 0,"

        '==  Target is new Build 4251..
        sCreateSql &= "  RefundOtherDetailAmount MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  RefundOtherDetailKey nvarChar(32) NOT NULL DEFAULT '',"  '--eg zippay, Amex..etc-

        '--  This can come from Refund or Sale with extra payment.
        '--   Mutually exclusive with-CreditNote Amount Debited.
        sCreateSql &= "  creditNotePaymentCredited MONEY NOT NULL DEFAULT 0,"
        '--  This amount was spent in paying for the SALE..-
        sCreateSql &= "  creditNoteAmountDebited MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  refundCreditNoteAmount MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '' "
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "customer_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Payments ----
        '= = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '==
        '==--   
        '==  NOW Have Pay Detail TABLEs (AND disbursements)--
        '-- Payment Details --
        '--  Records only Cash/Cheques/EFTPOS etc actually Paid IN..
        '--  SEE Payment Record for Cashout, cash refunded, credit Noes..
        '==
        '== 3401.327=  NO !!  Cashout IS DROPPED !!
        '==  Detail record is now created for Standard Types
        '==                   (ie Cash (in), cheque, EftPos etc- 
        '==    PLUS:
        '==       cashRefund (like cash), account (debit amt), CreditNoteDr, CreditNoteCr..

        '-- SQL CREATE code for Table: PaymentDetails
        sTableName = "PaymentDetails" '---..
        sCreateSql = "CREATE TABLE  dbo.PaymentDetails ( "
        sCreateSql &= "  detail_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  payment_id INT NOT NULL REFERENCES Payments(payment_id),"
        sCreateSql &= "  payment_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  paymenttype_key nvarchar(15) NOT NULL DEFAULT '', " '== REFERENCES paymenttypes( paymenttype_id),"
        sCreateSql &= "  paymenttype_subKey nvarchar(15) NOT NULL DEFAULT '', " '== Spare- in case.
        sCreateSql &= "  paymenttype_descr nvarChar(63) NOT NULL DEFAULT '',"
        '-- For the CASH payment, "amount" is the NETT cash amount Received...
        sCreateSql &= "  amount MONEY NOT NULL DEFAULT 0,"
        '== sCreateSql &= "  terminal_id nvarChar(50) NOT NULL, "  '--  Computer name-
        sCreateSql &= "  comments nvarChar(250) NOT NULL DEFAULT '' "  '--  Computer name-
        '== sCreateSql &= "  drawer nvarChar(1) NOT NULL DEFAULT '',"
        '== sCreateSql &= "  eft_method TINYINT NOT NULL DEFAULT 0 "  '-- and done-- 
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "payment_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 2 '---index no...
        sFldList = "paymenttype_key"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: PaymentDetailss ----
        '= = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '==
        '==  3303.0111-11Jan2017= 
        '==      >>  ADD PaymentRefundDetail Table.  
        '==               (tracks refund fragments actually applied to  debtor invoices-)   
        '==  3404.1015   --   Refunds now the same for Account Custs and non-a/c custs..   
        '==             -- So Account Payments can draw on CreditNotes for payment of invoices..  
        '==             --    and Table "paymentRefundDetails" is dropped. 
        '==
        'sTableName = "paymentRefundDetails" '---..
        'sCreateSql = "CREATE TABLE  dbo.paymentRefundDetails ( "
        'sCreateSql &= "  refundDetail_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        'sCreateSql &= "  payment_id INT NOT NULL REFERENCES payments(payment_id),"
        'sCreateSql &= "  refundInvoice_id INT NOT NULL REFERENCES Invoice(invoice_id),"  '-REF refund Invoice.
        ''-- Amount credited to this invoice.
        'sCreateSql &= "  amountDisbursed MONEY NOT NULL DEFAULT 0,"
        'sCreateSql &= "  );"  '--end of fld list..-  
        ''-- Now to create ----
        'If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        ''--add other indexes----  
        'fx = 1 '---index no...
        'sFldList = "refundInvoice_id"  '--
        'If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        ''---- end of table: Payments ----
        ''= = = = = = = = = = = = = = = = = =
        'If Not bOk Then
        '    Exit Function
        'End If
        'Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")
        '-- done -

        '-- PaymentDisbursements --
        '-- SQL CREATE code for Table: PaymentDisbursements
        '--  Distribute payment over these invoices..=--
        sTableName = "PaymentDisbursements" '---..
        sCreateSql = "CREATE TABLE  dbo.PaymentDisbursements ( "
        sCreateSql &= "  Disbursements_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  payment_id INT NOT NULL REFERENCES Payments(payment_id),"
        sCreateSql &= "  invoice_id INT NOT NULL REFERENCES Invoice(invoice_id),"
        sCreateSql &= "  tranCode nvarChar(15) NOT NULL DEFAULT 'payment',"  '- ("payment" or "discount").
        '--  Source:  "Refund #x" or "Payment #x"
        sCreateSql &= "  sourceOfFunds nvarChar(50) NOT NULL DEFAULT '',"  '--TFR FROM Refund or actual accountPayment-
        '-- Amount credited to this invoice.
        sCreateSql &= "  amount MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "invoice_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Payments ----
        '= = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")


        '-- Cashup_Sessions ----
        '-- Cashup_Sessions ----
        '-- Cashup_Sessions ----
        '-- SQL CREATE Table: Cashup_Sessions
        sTableName = "Cashup_Sessions" '---..
        sCreateSql = "CREATE TABLE  dbo.Cashup_Sessions ( " ' == . 
        sCreateSql &= "  session_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  staff_name nvarchar(50) NOT NULL DEFAULT '',"   '-3201.131--
        sCreateSql &= "  session_date datetime NOT NULL  DEFAULT CURRENT_TIMESTAMP,"  '--datetime session cashed-up..
        '= sCreateSql &= "  invoice_id INT NOT NULL DEFAULT -1, " '= REFERENCES invoices(invoice_id),"   
        '= sCreateSql &= "  drawer nvarChar(1) NOT NULL DEFAULT '',"   
        sCreateSql &= "  cashDrawer nvarChar(15) NOT NULL DEFAULT '',"  '--  TILL name A..Z-
        sCreateSql &= "  currentWindowsUserName nvarChar(80) NOT NULL DEFAULT '',"  '--  user name-
        sCreateSql &= "  terminal_id nvarChar(150) NOT NULL DEFAULT '',"  '--  Computer name-
        sCreateSql &= "  first_payment_id INT NOT NULL DEFAULT -1, " '= REFERENCES Payments(payment_id),"   
        sCreateSql &= "  last_payment_id INT NOT NULL DEFAULT -1, " '= REFERENCES Payments(payment_id),"   
        sCreateSql &= "  status nvarChar(15) NOT NULL DEFAULT '',"   '-
        sCreateSql &= "  stock_value MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  stock_variance MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '' "
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "status"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Cashup_Sessions ----
        '= = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE table: Cashup_Shortages
        sTableName = "Cashup_Shortages" '---..
        sCreateSql = "CREATE TABLE  dbo.Cashup_Shortages ( " ' == . 
        sCreateSql &= "  shortage_id INT  IDENTITY (1,1) PRIMARY KEY CLUSTERED,"   '-1--
        sCreateSql &= "  session_id INT NOT NULL REFERENCES Cashup_Sessions(session_id),"   '-2--
        sCreateSql &= "  paymenttype_key nvarChar(15) NOT NULL DEFAULT '',"   '-3--
        sCreateSql &= "  paymenttype_descr nvarChar(31) NOT NULL DEFAULT '',"  '-4--
        sCreateSql &= "  amount_reported MONEY NOT NULL DEFAULT 0, "   '-5--
        sCreateSql &= "  amount_counted MONEY NOT NULL DEFAULT 0 "   '-6--
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "session_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: Cashup_Shortages ----
        '= = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- S t o c k T a k e --
        '-- S t o c k T a k e --
        '-- S t o c k T a k e --

        '== 3201.131- 
        '--  Base (header) table Stocktakes-- 
        '--      1 row for every stocktake in progress or committed.
        '--  Detail table StocktakeItems-- 
        '--      1 row for every stock Item being counted in this Stocktake..

        '-- SQL CREATE code for Table: Stocktake-
        sTableName = "Stocktake" '---..
        sCreateSql = "CREATE TABLE  dbo.Stocktake ( " ' == . 
        sCreateSql &= "  stocktake_id  INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  stocktake_type nvarchar(15) NOT NULL DEFAULT '',"   '-'full', 'partial','single--
        '--  Partial s/t..  ONLY ONE cat1 can be selected..
        sCreateSql &= "  cat1 nvarchar(15) NOT NULL DEFAULT '',"        '-if partial-- 
        '--  Partial s/t..  Multiple cat2 (SUB-Category) can be selected..
        '--   Separated by semicolons..
        sCreateSql &= "  cat2List nvarchar(2000) NOT NULL DEFAULT '',"   '-if partial--
        sCreateSql &= "  currentWindowsUserName nvarChar(80) NOT NULL DEFAULT '',"  '--  user name-
        sCreateSql &= "  terminal_id nvarChar(150) NOT NULL DEFAULT '',"  '--  Computer name-
        sCreateSql &= " is_committed bit NOT NULL DEFAULT 0, "
        sCreateSql &= " is_cancelled bit NOT NULL DEFAULT 0, "

        sCreateSql &= "  date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        '== sCreateSql &= "  created_staff_id int NOT NULL REFERENCES staff(staff_id),"
        sCreateSql &= "  created_staff_name nvarchar(50) NOT NULL DEFAULT '',"   '-3201.131--

        '==sCreateSql &= "  quantity int NOT NULL DEFAULT 0,"   '-3--
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        '== sCreateSql &= "  modified_staff_id int NOT NULL REFERENCES staff(staff_id),"
        sCreateSql &= "  modified_staff_name nvarchar(50) NOT NULL DEFAULT '',"   '-3201.131--
        sCreateSql &= "  date_committed datetime NULL, "
        '== sCreateSql &= "  committed_staff_id int NOT NULL REFERENCES staff(staff_id),"
        sCreateSql &= "  committed_staff_name nvarchar(50) NOT NULL DEFAULT '',"   '-3201.131--
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '' "   '-3201.131--
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "cat1"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: StockTake ----
        '= = = = = = = = = = = = = = = = == 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")


        '-- SQL CREATE code for Table: StocktakeItems  --
        sTableName = "StocktakeItems" '---..
        sCreateSql = "CREATE TABLE  dbo.StocktakeItems ( " ' == . 
        sCreateSql &= "  item_id  INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  stocktake_id int NOT NULL REFERENCES Stocktake(stocktake_id),"
        '==sCreateSql &= "  stocktake_date datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"   
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        sCreateSql &= "  barcode nvarChar(40) NOT NULL DEFAULT '',"
        sCreateSql &= "  cat1 nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  cat2 nvarChar(15) NOT NULL DEFAULT '',"
        sCreateSql &= "  description nvarChar(40) NOT NULL,"
        '-- scan sequence-  order in which count was first attempted.
        '== sCreateSql &= "  scan_sequence int NOT NULL DEFAULT -1,"
        sCreateSql &= "  qty_on_record int NOT NULL DEFAULT 0,"
        sCreateSql &= "  qty_counted int NOT NULL DEFAULT 0,"
        sCreateSql &= "  qty_difference int NOT NULL DEFAULT 0,"  '-- counted - on_record.
        sCreateSql &= "  date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP"
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "stock_id"  '--UNIQUE --
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors, True)
        '--add other indexes----  
        fx = 2 '---index no...
        sFldList = "barcode"  '---UNIQUE --
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors, True)
        '---- end of table: StockTakeItems ----
        '= = = = = = = = = = = = = = = = == 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '-- Stocktake done..


        '-- SQL CREATE for table: StockTakeSerials
        '-- SQL CREATE code for Table: StockTakeSerials-

        '-- NOT USED so far..-

        sTableName = "StockTakeSerials" '---..
        sCreateSql = "CREATE TABLE  dbo.StockTakeSerials ( " ' == . 
        sCreateSql &= "  serialNumber nvarChar(40) NOT NULL DEFAULT '',"   '-1--
        '== THIS can't be PRIMARY KEY..  NOT UNIQUE..-
        '====  sCreateSql &= "  stock_id int PRIMARY KEY CLUSTERED "        '--2 and done-- 
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id) "  '--2 and done-- 
        sCreateSql &= ");"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "serialNumber"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '== EXTRA.. grh.-
        fx = 2 '---index no...
        sFldList = "stock_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: StockTakeSerials ----
        '= = = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")

        '= = = = = = = = = = = = = = = = = = = = = =

        '= 3107.815=
        '-- Create the Document Archive Table.

        '== 3403.1009= NO MORE..

        'sTableName = "DocArchive"
        'sCreateSql = gsMakeDocArchiveScript()
        ''-- Now to create ----
        'If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        'If Not bOk Then
        '    Exit Function
        'End If
        'Call gbLogMsg(sCreateLogPath, "- gbCreatePOSdbTables- Table " & sTableName & " was created ok.. ")
        '= = = = = = = = = = = = = = = = = = = = = =



        '== '--create  s y s t e m I n f o  table..---
        '=='--create  s y s t e m I n f o  table..---
        '=='--create  s y s t e m I n f o  table..---
        '==sTableName = "SystemInfo"
        '==sCreateSql = " CREATE TABLE  dbo.SystemInfo   ( "
        '==sCreateSql &= "  SystemKey varchar (48) PRIMARY KEY CLUSTERED,"
        '==sCreateSql &= "  SystemValue varchar (4000) NOT NULL DEFAULT '',"
        '==sCreateSql &= " DateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        '==sCreateSql &= " DateUpdated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ) "
        '=='===== GoSub db_createTable
        '== If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)

        If (iSqlErrors > 0) Or (Not bOk) Then
            '== cnnSQL.RollbackTrans()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call gbLogMsg(sCreateLogPath, "=== db build completed- with ERRORS !!  =====")
            MsgBox("=== db build completed- with ERRORS !!  =====", MsgBoxStyle.Critical)
            Call gbLogMsg(sCreateLogPath, "=== Rollback executed.. =====")
            Exit Function
        End If

        '-- Populate Reference Tables..  --
        '-- Populate Reference Tables..  --
        '-- Populate Reference Tables..  --

        '--  Add some Categories -1..--
        sSql = "INSERT INTO dbo.category1 (cat1_key, description ) VALUES  ('MOTHER', 'Motherboard Stuff' ); "
        sSql &= "INSERT INTO dbo.category1 (cat1_key, description ) VALUES  ('POWER', 'Power Stuff' ); "
        sSql &= "INSERT INTO dbo.category1 (cat1_key, description ) VALUES  ('AUDIO', 'Audio Stuff' ); "
        sSql &= "INSERT INTO dbo.category1 (cat1_key, description ) VALUES  ('CARD', 'Computer card' ); "
        '==   Target-New-Build-4267 -- (Started 28-Sep-2020)
        '-- Add SERVCE-
        sSql &= "INSERT INTO dbo.category1 (cat1_key, description ) VALUES  ('SERVCE', 'Service Item' ); "
        '== END  Target-New-Build-4267 -- (Started 28-Sep-2020)


        bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table Category1 failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table Category1 failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '--  Add some Categories -2..--
        sSql = "INSERT INTO dbo.category2 (cat2_key, description ) VALUES  ('ACCESS', 'Accessory' ); "
        sSql &= "INSERT INTO dbo.category2 (cat2_key, description ) VALUES  ('SK1155', 'Motherboard' ); "
        sSql &= "INSERT INTO dbo.category2 ( cat2_key, description ) VALUES  ('NOTEBK', 'Notebook' ); "
        sSql &= "INSERT INTO dbo.category2 ( cat2_key, description ) VALUES  ('BATTRY', 'Battery' ); "
        sSql &= "INSERT INTO dbo.category2 ( cat2_key, description ) VALUES  ('PSU', 'Power Supply' ); "
        sSql &= "INSERT INTO dbo.category2 ( cat2_key, description ) VALUES  ('SATA', 'SATA PCI Card' ); "
        sSql &= "INSERT INTO dbo.category2 ( cat2_key, description ) VALUES  ('VIDEO', 'Video card' ); "

        '==   Target-New-Build-4267 -- (Started 28-Sep-2020)
        '-- Add SERVCE-
        sSql &= "INSERT INTO dbo.category2 (cat2_key, description ) VALUES  ('SERVCE', 'Service Item' ); "
        '== END  Target-New-Build-4267 -- (Started 28-Sep-2020)


        bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table category2  failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table category2  failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '--  Add some Brands..--
        '--  Add some Brands..--
        sSql = "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Acer' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('AMD' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Asus' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Bliss' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Canon' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Dell' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('dLink' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Hewlett Packard' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('IBM' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Intel' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Laser' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Microsoft' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Netgear' ) "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Panasonic' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Sanyo' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Seagate' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Samsung' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Sony' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Sunix' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Toshiba' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('Western Digital' ); "
        sSql &= "INSERT INTO dbo.StockBrands (BrandName ) VALUES  ('ZZ Unknown Brand' ); "
        bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table Brands failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table Brands failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '--  Add some Payment Types..--
        '=3101.1206= sSql = "INSERT INTO dbo.PaymentTypes (description, isCash, isElectronicTfr ) VALUES  ('Cash',1, 0 ); "
        '=3101.1206= sSql &= "INSERT INTO dbo.PaymentTypes (description, isCash, isElectronicTfr) VALUES  ('Cheque',0, 0 ); "
        '=3101.1206= sSql &= "INSERT INTO dbo.PaymentTypes (description, isCash, isElectronicTfr) VALUES  ('EftPos',0, 1 ); "
        '=3101.1206= sSql &= "INSERT INTO dbo.PaymentTypes (description, isCash, isElectronicTfr) VALUES  ('MasterCard',0, 1 ); "
        '=3101.1206= sSql &= "INSERT INTO dbo.PaymentTypes (description, isCash, isElectronicTfr) VALUES  ('Visa',0, 1 ); "
        '=3101.1206= sSql &= "INSERT INTO dbo.PaymentTypes (description, isCash, isElectronicTfr) VALUES  ('Credit Card',0, 1 ); "
        '=3101.1206= bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        '=3101.1206= If Not bOk Then
        '=3101.1206= iSqlErrors = iSqlErrors + 1
        '=3101.1206= Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table PaymentTypes failed.." & vbCrLf & sErrorMsg)
        '=3101.1206= MsgBox(" ** ERROR **  INSERT into Table PaymentTypes failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        '=3101.1206= Exit Function
        '=3101.1206= End If

        '--  Add some PricingGradeRules..--
        '--  Add some PricingGradeRules..--
        '--  Add some PricingGradeRules..--
        '=3101.1112= sSql = "INSERT INTO dbo.PricingGradeRules ( pricingGrade, rule_base, percentage) VALUES  ('A', 'cost+', 0 ); "
        '=3101.1112= sSql &= "INSERT INTO dbo.PricingGradeRules ( pricingGrade, rule_base, percentage) VALUES  ('B', 'cost+', 10 ); "
        '=3101.1112= sSql &= "INSERT INTO dbo.PricingGradeRules ( pricingGrade, rule_base, percentage) VALUES  ('C', 'cost+', 15 ); "
        '=3101.1112= sSql &= "INSERT INTO dbo.PricingGradeRules ( pricingGrade, rule_base, percentage) VALUES  ('1', 'sell-', 10 ); "
        '=3101.1112= '- default--  "rrp" --
        '=3101.1112= sSql &= "INSERT INTO dbo.PricingGradeRules ( pricingGrade, rule_base, percentage) VALUES  ('0', 'sell-', 0 ); "
        '=3101.1112= bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        '=3101.1112= If Not bOk Then
        '=3101.1112=   iSqlErrors = iSqlErrors + 1
        '=3101.1112=   Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table PricingGradeRules failed.." & vbCrLf & sErrorMsg)
        '=3101.1112=   MsgBox(" ** ERROR **  INSERT into Table PricingGradeRules failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        '=3101.1112= Exit Function
        '=3101.1112= End If


        '--  Add some Tax Codes..--
        '--  Add some Tax Codes..--
        '=3101.1112= sSql = "INSERT INTO dbo.TaxCodes (code, description, percentage ) VALUES  ('GST', 'Goods And Services Tax', 10.0 ); "
        '=3101.1112= sSql &= "INSERT INTO dbo.TaxCodes (code, description, percentage ) VALUES  ('NTX', 'No Tax', 0 ); "
        '=3101.1112= bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        '=3101.1112= If Not bOk Then
        '=3101.1112=   iSqlErrors = iSqlErrors + 1
        '=3101.1112=   Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table TaxCodes failed.." & vbCrLf & sErrorMsg)
        '=3101.1112=   MsgBox(" ** ERROR **  INSERT into Table TaxCodes failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        '=3101.1112=   Exit Function
        '=3101.1112= End If


        '--  Add a Staff..--
        sSql = "INSERT INTO dbo.Staff (barcode, lastName, firstName, docket_name, isAdministrator, dateOfBirth ) " & _
                                "VALUES  ('1', 'Jimbo', 'Jim', 'Jim-boss', 1, '25-Dec-1990'); "
        bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table Staff failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table Staff failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '--  Add a Supplier..--
        sSql = "INSERT INTO dbo.Supplier (barcode, supplierName, address, country, webSiteURL ) " & _
                                "VALUES  ('ms-001', 'Microsoft Corp.', 'Some street', 'USA', 'http://microsoft.com'); "

        '==   Target-New-Build-4267 -- (Started 28-Sep-2020)
        '-- DROP Aztrinity--
        '-- AND FIX Precise Address.
        'sSql &= "INSERT INTO dbo.Supplier (barcode, supplierName, address, country, webSiteURL ) " & _
        '                        "VALUES  ('az-001', 'Aztrinity', 'Some street', 'Australia', 'http://jobmatix.com.au'); "
        '== END  Target-New-Build-4267 -- (Started 28-Sep-2020)


        sSql &= "INSERT INTO dbo.Supplier (barcode, supplierName, address, country, webSiteURL ) " & _
                                "VALUES  ('anyw-001', 'Anyware Computer Accessories', '17 Huntington Place', 'Australia', ''); "
        sSql &= "INSERT INTO dbo.Supplier (barcode, supplierName, address, country, webSiteURL ) " & _
                                "VALUES  ('Precise-001', 'Precise PCs', 'Wollumbin St, Mbah', 'Australia', ''); "
        bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table Supplier failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table Supplier failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '--  Add a Customer..--
        sSql = "INSERT INTO dbo.Customer (barcode, companyName, lastName, firstName, " & _
                              "openedStaff_id, openedStaffName, creditlimit, address ) " & _
                        "VALUES  ('1', 'General Company', 'Customer', 'General',1,'Jim-boss', 0.00, 'Murwilumbah NSW'); "
        sSql &= "INSERT INTO dbo.Customer (barcode, companyName, lastName, firstName, " & _
                               "openedStaff_id, openedStaffName, creditlimit, address ) " & _
                        "VALUES  ('2', 'Precise PCs', 'Customer', 'Precise',1,'Jim-boss', 100.00, 'Murwilumbah NSW'); "
        bOk = mbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg, mbIsTransaction, mSqlTran1)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table Customer failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table Customer failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If


        '== '-- add some system  Parameters..--
        '== sSql = "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('BusinessABN', '" & sBusinessABN & "' ); "
        '== '==sSql = sSql + "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('BusinessUserName', '" + sDBUserName + "' ); "
        '== sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('JT2SecurityId', '" & sJT2SecurityId & "' ); "
        '== sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('LicenceKey', DEFAULT ); "
        '== sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('LastBackupFullPath', DEFAULT ); "
        '= '--  add business details..-
        '== For Each col1 In colBusinessDetails
        '== sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue ) " & " VALUES  ('" & _
        '==                     msFixSqlStr(col1.Item("name")) & "', '" & msFixSqlStr(col1.Item("value")) & "' ); "
        '== Next col1
        '== bOk = gbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg)
        '== If Not bOk Then
        '==   iSqlErrors = iSqlErrors + 1
        '==   Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table SystemInfo failed.." & vbCrLf & sErrorMsg)
        '==   MsgBox(" ** ERROR **  INSERT into Table SystemInfo  failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        '== End If

        '-- C o m m i t ---
        '-- C o m m i t ---
        If iSqlErrors > 0 Then
            '= cnnSQL.RollbackTrans()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call gbLogMsg(sCreateLogPath, "=== POS db UPDATE stopped- with ERRORS !!  =====")
            MsgBox("=== db build stopped- with ERRORS !!  =====" & vbCrLf & "=== Rollback executed.. =====", MsgBoxStyle.Critical)
            Call gbLogMsg(sCreateLogPath, "=== Rollback executed.. =====")
            Exit Function
        Else '--ok--
            Call gbLogMsg(sCreateLogPath, "  ==> Executing Commit..")
            '== cnnSQL.CommitTrans()
            mSqlTran1.Commit()
            Call gbLogMsg(sCreateLogPath, "  ==>POS Update Commit executed..")
        End If

        '-- GRANT ALL can't be in a transaction..--
        '--- do then here.--
        '-- V1.3 = GRANT ALL  NOT needed..--
        '-- V1.3 = GRANT ALL  NOT needed..--

        '=3101.1113== Add some POS systemInfo values..
        '- first check if systemInfo table exists..
        '-- then do updates.
        Dim datatable1 As DataTable
        '== dim col1 as Collection 
        sSql = "SELECT table_name FROM INFORMATION_SCHEMA.TABLES "
        sSql &= "  WHERE (table_type='BASE TABLE')  AND (table_name='SystemInfo');"
        If Not gbGetDataTable(cnnSQL, datatable1, sSql) Then
            MsgBox("modCreatePOS: Error in getting tables schema recordset.. " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        End If
        If Not (datatable1 Is Nothing) AndAlso (datatable1.Rows.Count > 0) Then  '-exists..
            '-- add some POS system  Parameters..--
            sSql = "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue ) " & _
                                "VALUES  ('POS_ACCOUNTTERMS', 'Account terms 30 days unless other prior arrangement.'); "
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('POS_SELL_MARGIN', '25.0' ); "
            '=3403.1115-  Pricing Grades..
            '-POS_CUSTPRICINGGRADE_COSTPLUS_1-
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('POS_CUSTPRICINGGRADE_COSTPLUS_1', '25.0' ); "
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('POS_CUSTPRICINGGRADE_COSTPLUS_2', '15.0' ); "
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('POS_CUSTPRICINGGRADE_COSTPLUS_3', '10.0' ); "
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('POS_CUSTPRICINGGRADE_COSTPLUS_4', '0.0' ); "


            '=3402.1014= Add Emailing defaults..
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('POS_ALLOW_EMAIL_INVOICES', 'Y' ); "
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('POS_ALLOW_EMAIL_STATEMENTS', 'Y' ); "
            '--texts-
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue ) " & _
                     "VALUES  ('POS_emailtextinvoice', " & _
                       "'&&subject" & vbCrLf & "&&greeting" & vbCrLf & _
                                        "Please find attached your invoice as per above." & vbCrLf & _
                                        "Thank You.." & vbCrLf & "&&BusinessName'" & _
                       "); "
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue ) " & _
                     "VALUES  ('POS_emailtextstatement', " & _
                       "'&&subject" & vbCrLf & "&&greeting" & vbCrLf & _
                                        "Please find attached your statement as per above." & vbCrLf & _
                                        "Thank You.." & vbCrLf & "&&BusinessName'" & _
                       "); "
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue ) " & _
                     "VALUES  ('POS_emailtextpo', " & _
                       "'&&subject" & vbCrLf & "&&greeting" & vbCrLf & _
                                        "Please find attached our Purchase Order as per above." & vbCrLf & _
                                        "Thank You.." & vbCrLf & "&&BusinessName'" & _
                       "); "
            '--  Add row for Security ID, so we have DateCreated.
            sSql &= "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('JMPOS33_SECURITYID', '0' ); "
            bOk = gbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg)
            If Not bOk Then
                iSqlErrors = iSqlErrors + 1
                Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table SystemInfo failed.." & vbCrLf & sErrorMsg)
                MsgBox(" ** modCreatePOS ERROR **  INSERT into Table SystemInfo  failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            End If
        End If  '--exists-

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default


        '==   Target-New-Build-4267 -- (Started 28-Sep-2020)
        '==   Target-New-Build-4267 -- (Started 28-Sep-2020)

        '==  POS DB Create-  Add INSERTIONS to new Stock Table to set up six Labour Hourly Rates..  
        '==                               Barcodes like "01-LAB-HRLY-WKSH-P1" etc etc  
        '--  FIRST-  Add Us (this business) as a Supplier (So we can have supplier for JobTracking Labour..)
        Dim sOurShortName, sOurBizName, sOurBizAddress As String
        Dim sysInfoTemp As clsSystemInfo
        Dim intOurSupplier_id As Integer = -1
        Dim sOurSupplierBarcode As String

        sysInfoTemp = New clsSystemInfo(cnnSQL)
        sOurShortName = ""
        sOurBizName = ""
        If sysInfoTemp.contains("BusinessShortName") Then
            sOurShortName = sysInfoTemp.item("BusinessShortName")
            sOurBizName = gsFixSqlStr(sysInfoTemp.item("BusinessName"))
            sOurBizAddress = gsFixSqlStr(sysInfoTemp.item("BusinessAddress1")) & _
                                              vbCrLf & gsFixSqlStr(sysInfoTemp.item("BusinessAddress2"))
        End If
        '-- make barcode for our supplier record.
        sOurSupplierBarcode = "00-" & Left(sOurShortName, 12)
        '--  Add this Biz as a Supplier..--
        sSql = "INSERT INTO dbo.Supplier (barcode, supplierName, address, country, webSiteURL ) " & _
                                "VALUES  ('" & sOurSupplierBarcode & "', " & _
                                                   " '" & sOurBizName & "', '" & sOurBizAddress & "', 'AUS', 'unkown'); "
        bOk = gbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table Supplier failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table Supplier failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            '=Exit Function
        Else
            '-ok-
            '-- get Our SupplierId, and insert SIX Stock. Labour Rates..  All at sell_inc $100.00.. (sell_ex 90.9091.)
            Dim sSqlInsert As String = ""
            sSql = "SELECT supplier_id FROM dbo.Supplier WHERE barcode='" & sOurSupplierBarcode & "';"
            If gbGetSqlScalarIntegerValue(cnnSQL, sSql, L1) Then
                intOurSupplier_id = L1
                '-- INSERT SIX Rows..
                sSqlInsert = "INSERT INTO [dbo].[stock] (barcode, description, isNonStockItem, " & _
                                                  "  Cat1, Cat2, goods_taxCode,sales_taxCode, sellExTax, supplier_id, comments )"
                sSqlInsert &= " VALUES "
                sSqlInsert &= " ('01-LAB-HRLY-WKSH-P1', 'Priority-1 Workshop Labour per Hour', 1, " & _
                                        " 'SERVCE','SERVCE','GST', 'GST', 90.9091, " & CStr(intOurSupplier_id) & ", 'POS Create..'), "
                sSqlInsert &= " ('01-LAB-HRLY-WKSH-P2', 'Priority-2 Workshop Labour per Hour', 1, " & _
                                        " 'SERVCE','SERVCE','GST', 'GST', 90.9091, " & CStr(intOurSupplier_id) & ", 'POS Create..' ), "
                '-3-
                sSqlInsert &= " ('01-LAB-HRLY-WKSH-P3', 'Priority-3 Workshop Labour per Hour', 1, " & _
                                      " 'SERVCE','SERVCE','GST', 'GST', 90.9091, " & CStr(intOurSupplier_id) & ", 'POS Create..' ), "
                '-4-  ON-SITE-
                sSqlInsert &= " ('01-LAB-HRLY-ONST-P1', 'Priority-1 ON-SITE Labour per Hour', 1, " & _
                                       "'SERVCE','SERVCE','GST', 'GST', 90.9091, " & CStr(intOurSupplier_id) & ", 'POS Create..'), "
                '-5-
                sSqlInsert &= " ('01-LAB-HRLY-ONST-P2', 'Priority-2 ON-SITE Labour per Hour', 1, " & _
                                      " 'SERVCE','SERVCE','GST', 'GST', 90.9091, " & CStr(intOurSupplier_id) & ", 'POS Create..'),  "
                '-6-
                sSqlInsert &= " ('01-LAB-HRLY-ONST-P3', 'Priority-3 ON-SITE Labour per Hour', 1, " & _
                                      " 'SERVCE','SERVCE','GST', 'GST', 90.9091, " & CStr(intOurSupplier_id) & ", 'POS Create..');  "
                If Not gbExecuteCmd(cnnSQL, sSqlInsert, L1, sErrorMsg) Then
                    MsgBox(" ** ERROR **  INSERT into Stock Table failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Information)
                Else  '-ok-
                    '-test-
                    '= MsgBox("ok- Inserted " & L1 & " rows for Supplier_id: " & intOurSupplier_id, MsgBoxStyle.Information)

                End If
            End If  '-get-
        End If  '-ok-
        '== END Target-New-Build-4267 -- (Started 28-Sep-2020)
        '== END Target-New-Build-4267 -- (Started 28-Sep-2020)



        '--  Read SystemInfo to get date-created and make Security Id..--
        Dim dateCreated As DateTime

        Dim sJMPOS_SecurityId As String = gsMakeSecurityId(cnnSQL, dateCreated)

        '=3301.1116=  
        '--  update SystemInfo (Row= SecurityId) to set correct value.--

        sSql = "UPDATE SystemInfo SET SystemValue='" & sJMPOS_SecurityId & "' "
        sSql = sSql & " WHERE (SystemKey='JMPOS33_SECURITYID'); "
        bOk = gbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  UPDATE POS Table SystemInfo (SecurityId) failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  UPDATE POS Table SystemInfo (SecurityId) failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        End If
        '-- get ABN-
        Dim SysInfo1 As clsSystemInfo
        SysInfo1 = New clsSystemInfo(cnnSQL)
        Dim sBusinessABN As String = SysInfo1.item("BusinessABN")

        '--  LOG ALL Details-- DateCreated, DB-name, ABN and SecurityId..
        Call gbLogMsg(sCreateLogPath, "=====JobMatix POS DB build completed ok.. DETAILS FOLLOW:  =====" & vbCrLf & _
                     " == Date: " & VB6.Format(dateCreated, "dd-mmm-yyyy, ") & VB6.Format(dateCreated, "hh:mm:ss") & _
                       ",  DBName= " & sSqlDBName & ",  ABN= " & sBusinessABN & ", SecurityId=" & sJMPOS_SecurityId & ".." & _
                              vbCrLf & "= = = = The End = = = = = ")
        gbCreatePOSdbTables = True

        '=3403.1115==
        MsgBox("Important Note: " & vbCrLf & vbCrLf & _
               "  The JobMatix POS DB Table build completed ok..==" & vbCrLf & vbCrLf & _
               "  NB: Some default settings have been installed.." & vbCrLf & _
               "  These should be reviewed asap" & vbCrLf & _
               "      via the Admin Options/Setup screen when JobMatixPOS is restarted.." & vbCrLf & _
               "  (Email SMTP settings must all be completed " & vbCrLf & _
               "      before Invoices etc can be emailed to customers..)", MsgBoxStyle.Information)
        Exit Function

    End Function '--createSql--
    '= = = = = = = = = =
    '-===FF->

    '==3403.429-- 29Apr2017=
    '=- = Create LayBy Tables for POS database = = =

    Public Function gbCreatePOSLaybyTables(ByRef cnnSQL As OleDbConnection, _
                                           ByVal sSqlDBName As String, _
                                           ByVal sCreateLogPath As String, _
                                            ByRef iSqlErrors As Integer) As Boolean

        Dim bOk As Boolean
        Dim fx, L1 As Integer
        '==Dim s1, Msg As String
        Dim s1, sSql, sCreateSql, sTableName As String
        Dim sFldList, sErrorMsg As String

        gbCreatePOSLaybyTables = False
        mbIsTransaction = False
        iSqlErrors = 0
        msCreateLogPath = sCreateLogPath  '-- set up module-level path var..

        Call gbLogMsg(sCreateLogPath, "- Create POS gbCreatePOSLaybyTables started.. ")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '== sJT2SecurityId = "" '--we'll create our own.
        '-  CREATE DB now done by caller..

        '--Move to new db--
        Try
            cnnSQL.ChangeDatabase(sSqlDBName)
        Catch ex As Exception
            MsgBox("Error in 'gbCreatePOSLaybyTables'- Failed ChangeDatabase to: " & sSqlDBName & _
                          vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '==cnnSQL.BeginTrans()
        Try
            mSqlTran1 = cnnSQL.BeginTransaction
            mbIsTransaction = True
        Catch ex As Exception
            MsgBox("Error in 'gbCreatePOSLaybyTables'- Failed sql.BeginTransaction.." & _
                          vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        bOk = True

        '=3403.429=
        '--  LayBy Tables (sort of) mirror the Sale Invoices and Lines..

        '-- Layby Table ---
        '-- SQL CREATE code for Table: Layby---
        sTableName = "Layby" '---..
        sCreateSql = "CREATE TABLE  dbo.Layby ( "
        sCreateSql &= "  Layby_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  Layby_date_started datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  customer_id INT NOT NULL REFERENCES Customer(customer_id),"
        sCreateSql &= "  transactionType nvarChar(15) NOT NULL DEFAULT '',"
        '== sCreateSql &= "  custom nvarChar(20) NOT NULL DEFAULT '',"
        '=sCreateSql &= "  payment_id INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  JobNumber INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  terminal_id nvarChar(150) NOT NULL,"  '--  Computer name-
        sCreateSql &= "  cashDrawer nvarChar(15) NOT NULL DEFAULT '',"  '--  TILL name A..Z-
        sCreateSql &= "  currentWindowsUserName nvarChar(80) NOT NULL DEFAULT '',"  '--  user name-
        sCreateSql &= "  subtotal_ex_non_taxable MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_ex_taxable MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  subtotal_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  discount_nett MONEY NOT NULL DEFAULT 0,"   '--after deducting tax disc.
        sCreateSql &= "  discount_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  rounding MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0,"
        '== EXTRA column grh==
        sCreateSql &= "  isCancelled bit NOT NULL DEFAULT 0,"
        sCreateSql &= "  date_cancelled datetime  NULL,"
        sCreateSql &= "  cancelled_staff_id INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  isDelivered bit NOT NULL DEFAULT 0,"   '= completed and invoiced.-
        sCreateSql &= "  Layby_date_delivered datetime  NULL,"
        sCreateSql &= "  Layby_delivered_invoice_id INT NOT NULL DEFAULT -1,"
        sCreateSql &= "  deliveryInstructions nvarchar(max) NOT NULL DEFAULT '',"   '-NEW --
        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '' "
        sCreateSql &= "  );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "customer_id"  '--
        If bOk Then Call mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table:layby ----
        '= = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSLaybyTables- Table " & sTableName & " was created ok.. ")

        '-- SQL CREATE for table: InvoiceLines-
        '-- SQL CREATE code for Table: InvoiceLines-
        sTableName = "LaybyLine" '---..
        sCreateSql = "CREATE TABLE  dbo.LaybyLine ( "
        sCreateSql &= "  line_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  Layby_id INT NOT NULL REFERENCES Layby(Layby_id),"
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        '== EXTRA column grh==
        sCreateSql &= "  description nvarChar(40) NOT NULL DEFAULT '',"   '-NEW --
        sCreateSql &= "  serialNumber nvarChar(40) NOT NULL DEFAULT '',"  '--MUST have Qty=1 --
        sCreateSql &= "  serialAudit_id INT NOT NULL DEFAULT -1, " '== REFERENCES SerialAudit(serial_id),"
        sCreateSql &= "  cost_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  cost_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sell_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sales_taxCode nvarChar(7) NOT NULL DEFAULT '',"
        sCreateSql &= "  sales_taxPercentage decimal NOT NULL DEFAULT 0,"
        sCreateSql &= "  sell_inc MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sellActual_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sellActual_Tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  sellActual_inc MONEY NOT NULL DEFAULT 0,"
        '--  Quantity may be LABOUR (eg serviceItem)..
        sCreateSql &= "  quantity decimal(7,4) NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_ex MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_tax MONEY NOT NULL DEFAULT 0,"
        sCreateSql &= "  total_inc MONEY NOT NULL DEFAULT 0, "
        sCreateSql &= "  gross_profit MONEY NOT NULL DEFAULT 0"
        sCreateSql &= " );"  '--end of fld list..-  
        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "layby_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 2 '---index no...
        sFldList = "SerialNumber"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 3 '---index no...
        sFldList = "stock_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '== fx = 4 '---index no...
        '== sFldList = "customer_id"  '--
        '== If bOk Then Call mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table: invoice=Line ----
        '= = = = = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSLaybyTables- Table " & sTableName & " was created ok.. ")

        '-- C o m m i t ---
        '-- C o m m i t ---
        If iSqlErrors > 0 Then
            '= cnnSQL.RollbackTrans()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call gbLogMsg(sCreateLogPath, "=== POS db UPDATE stopped- with ERRORS !!  =====")
            MsgBox("=== db Create layby tables stopped- with ERRORS !!  =====" & vbCrLf & "=== Rollback executed.. =====", MsgBoxStyle.Critical)
            Call gbLogMsg(sCreateLogPath, "=== Rollback executed.. =====")
            Exit Function
        Else '--ok--
            Call gbLogMsg(sCreateLogPath, "  ==> Executing Commit..")
            '== cnnSQL.CommitTrans()
            mSqlTran1.Commit()
            Call gbLogMsg(sCreateLogPath, "  ==>POS Layby defs create Update Commit executed..")
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        gbCreatePOSLaybyTables = True

    End Function  '-gbCreatePOSLaybyTables-
    '= = = = = = = = = = = = = = = == = = =
    '-===FF->

    '==        >> 3411.1125 -- Add TABLES for Subscriptions..
    '==        >> 3411.1125 -- Add TABLES for Subscriptions..
    '==        >> 3411.1125 -- Add TABLES for Subscriptions..

    Public Function gbCreatePOSSubscriptionTables(ByRef cnnSQL As OleDbConnection, _
                                                  ByVal sSqlDBName As String, _
                                                  ByVal sCreateLogPath As String, _
                                                   ByRef iSqlErrors As Integer) As Boolean

        Dim bOk As Boolean
        Dim fx, L1 As Integer
        '==Dim s1, Msg As String
        Dim s1, sSql, sCreateSql, sTableName As String
        Dim sFldList, sErrorMsg As String

        gbCreatePOSSubscriptionTables = False

        mbIsTransaction = False
        iSqlErrors = 0
        msCreateLogPath = sCreateLogPath  '-- set up module-level path var..

        Call gbLogMsg(sCreateLogPath, "- Create POS gbCreatePOSSubscriptionTables started.. ")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '== sJT2SecurityId = "" '--we'll create our own.
        '-  CREATE DB now done by caller..

        '--Move to new db--
        Try
            cnnSQL.ChangeDatabase(sSqlDBName)
        Catch ex As Exception
            MsgBox("Error in 'gbCreatePOSSubscriptionTables'- Failed ChangeDatabase to: " & sSqlDBName & _
                          vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '==cnnSQL.BeginTrans()
        Try
            mSqlTran1 = cnnSQL.BeginTransaction
            mbIsTransaction = True
        Catch ex As Exception
            MsgBox("Error in 'gbCreatePOSSubscriptionTables'- Failed sql.BeginTransaction.." & _
                          vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        bOk = True

        '=3403.429=
        '--  LayBy Tables (sort of) mirror the Sale Invoices and Lines..

        '-- Subscription Table ---
        '-- This is the TEMPLATE to produce the recurring invoice 
        '--    for the particlar product list for the particular recurring period.--
        '-The Child table "SubscriptionLine" details the list of products for this Sub.-

        '-- SQL CREATE code for Table: Subscription---
        sTableName = "Subscription" '---..
        sCreateSql = "CREATE TABLE  dbo.Subscription ( "
        sCreateSql &= "  Subscription_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  customer_id INT NOT NULL REFERENCES Customer(customer_id),"
        sCreateSql &= "  staff_id INT NOT NULL REFERENCES Staff(staff_id),"
        sCreateSql &= "  isActivated bit NOT NULL DEFAULT 0,"
        '- Billing Active from Start date when activation flag set....
        sCreateSql &= "  start_date datetime NOT NULL,"     '- first billing dy.. Billing Active when ACTIVATED is set..
        '=3411.0224- Restore termination date.
        sCreateSql &= "  termination_date datetime NULL,"  '-optional sunset date.
        '-- Period of rental/subs cyle.  1M=monthly;  3M=Quarterly etc..
        sCreateSql &= "  billingPeriod nvarChar(15) NOT NULL DEFAULT '1M Monthly',"  '--  peridi/cycle-

        sCreateSql &= "  terminal_id nvarChar(150) NOT NULL,"  '--  Computer name-
        '= sCreateSql &= "  currentWindowsUserName nvarChar(80) NOT NULL DEFAULT '',"  '--  user name-

        '==  -- 4201.0929.  Started 19-September-2019-
        '==     -- Add column to Subscriptions Table: "OkToEmailInvoices bit default 0"
        sCreateSql &= "  OkToEmailInvoices bit NOT NULL DEFAULT 0,"

        sCreateSql &= "  isCancelled bit NOT NULL DEFAULT 0,"
        sCreateSql &= "  date_cancelled datetime  NULL,"
        sCreateSql &= "  cancelled_staff_id INT NOT NULL DEFAULT -1,"

        sCreateSql &= "  date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreateSql &= "  date_updated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"

        sCreateSql &= "  comments nvarchar(max) NOT NULL DEFAULT '' "
        sCreateSql &= "  );"  '--end of fld list..-  

        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "customer_id"  '--
        If bOk Then Call mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        '---- end of table:layby ----
        '= = = = = = = = = = = = = = = = = = = = =
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSSubscriptionTables- Table " & sTableName & " was created ok.. ")

        '-- Table: SubscriptionLine---
        '-The Child table "SubscriptionLine" details the list of products for this Sub.-
        '==
        '==   >> 3411.0417=  17-Apr-2018=
        '==       -- Fix Subscription line.. comma missing..
        '==
        sTableName = "SubscriptionLine" '---..
        sCreateSql = "CREATE TABLE  dbo.SubscriptionLine ( "
        sCreateSql &= "  line_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  Subscription_id INT  NOT NULL REFERENCES Subscription(Subscription_id) ,"
        sCreateSql &= "  stock_id int NOT NULL REFERENCES Stock(stock_id),"
        sCreateSql &= "  stock_barcode nvarChar(40) NOT NULL  DEFAULT '',"
        sCreateSql &= "  stock_description nvarChar(40) NOT NULL DEFAULT '',"   '-NEW --
        '=3411-0224= User can change line price on Sub Line..
        sCreateSql &= "  sellActual_inc MONEY NOT NULL DEFAULT 0, "
        sCreateSql &= "  quantity decimal(7,4) NOT NULL DEFAULT 0 "
        sCreateSql &= "  );"  '--end of fld list..-  

        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "Subscription_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 2 '---index no...
        sFldList = "stock_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
         '= = = = = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSSubscriptionTables- Table " & sTableName & " was created ok.. ")

        '-- Table: SubscriptionInvoice---
        '-The table "SubscriptionInvoice" records invoices produced for this Sub.-

        sTableName = "SubscriptionInvoice" '---..
        sCreateSql = "CREATE TABLE  dbo.SubscriptionInvoice ( "
        sCreateSql &= "  subs_invoice_line_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        sCreateSql &= "  Subscription_id INT  NOT NULL REFERENCES Subscription(Subscription_id) ,"
        sCreateSql &= "  invoice_id INT NOT NULL REFERENCES Invoice(invoice_id),"
        '-- Period covered by invoice..
        sCreateSql &= "  invoice_period_start_date datetime NOT NULL, "
        '-- END OF Period covered by invoice..
        sCreateSql &= "  invoice_period_end_date datetime NOT NULL, "

        sCreateSql &= "  email_sent_ok bit NOT NULL DEFAULT 0 "
        sCreateSql &= "  );"  '--end of fld list..-  

        '-- Now to create ----
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreateSql, sCreateLogPath, iSqlErrors)
        '--add other indexes----  
        fx = 1 '---index no...
        sFldList = "Subscription_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
        fx = 2 '---index no...
        sFldList = "invoice_id"  '--
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, fx, sFldList, sCreateLogPath, iSqlErrors)
         '= = = = = = = = = = = = = = = = = = 
        If Not bOk Then
            Exit Function
        End If
        Call gbLogMsg(sCreateLogPath, "- gbCreatePOSSubscriptionTables- Table " & sTableName & " was created ok.. ")

        '-- C o m m i t ---
        '-- C o m m i t ---
        If iSqlErrors > 0 Then
            '= cnnSQL.RollbackTrans()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call gbLogMsg(sCreateLogPath, "=== POS db UPDATE stopped- with ERRORS !!  =====")
            MsgBox("=== db Create Subscription tables stopped- with ERRORS !!  =====" & vbCrLf & _
                                              "=== Rollback executed.. =====", MsgBoxStyle.Critical)
            Call gbLogMsg(sCreateLogPath, "=== Rollback executed.. =====")
            Exit Function
        Else '--ok--
            Call gbLogMsg(sCreateLogPath, "  ==> Executing Commit..")
            '== cnnSQL.CommitTrans()
            mSqlTran1.Commit()
            Call gbLogMsg(sCreateLogPath, "  ==>POS Layby defs create Update Commit executed..")
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        gbCreatePOSSubscriptionTables = True

    End Function '-gbCreatePOSSubscriptionTables
    '= = = = = = = = = = = = = = = == = = = = = =

End Module  '--modCreate-

'== the end ===