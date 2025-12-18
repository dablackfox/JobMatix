Option Strict Off
Option Explicit On
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports VB = Microsoft.VisualBasic

Public Module modCreateJobs
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
    '= = = = = = = = = = =

    '==== SQL - UPDATE JOBS database = = =
    '--  Added 07-July-2009---
    '---   for v1.3 with wider Goods columns..--
    '-----  and NO taskType_id column in JobTasks..--

    '--  15-Oct-2009=  V2-- Createjobs.. Add colBusinessDetails for systemInfo.-
    '--  26-Oct-2009=  V2-- Createjobs.. Add NEW Tables. for Quotes..
    '-----------                           --    (ExtrasChecklist (REF), QuoteJobParts,QuoteJobExtras)..-
    '------------      V2--  Version2-UpgradeFunction for V1.3  DB's.--
    '------------              -- Add NEW Tables. for Quotes..(as above..)..
    '------------              -- DROP COLUMN TaskType_Id from Table 'JobTasks'  (FIRST DROP FKEY Constraint.)..
    '----08-Dec-2009==  V2--  Version2-UpgradeFunction for V1.3  DB's.--
    '------------   !!!! - DO NOT DROP COLUMN TaskType_Id from Table 'JobTasks'  ( DROP FKEY Constraint ONLY !!!! .)..
    '--
    '----18-Jan-2010==  V2--  ADD Column PartSerialNumber to Mirror v1.4 UpgradeFunction for V1.3  DB's.--
    '---------        !! - NB- v1.4/V2 Upgrade should actually be done by running v1.4 on Precise DB..
    '--                    NB- All other customers should have DB CREATED in modern form by V2..--

    '==== SQL - UPDATE JOBS database = = =
    '==== SQL - UPDATE JOBS database = = =
    '----31-Jan-2010==  V2.1--  UpgradeFunction for (V1.4) V2.0 DB's.--
    '-- ----- RA Items.. MUST DROP FKEY CONSTRAINT on Column 'RA_JobId',
    '----  since NOT all RA's have a Job No.==
    '----28-Feb-2010==  V2.1-- EXTRA UpgradeFunction for (V1.4) V2.0 DB's.--
    '-----         Add RequestNotes text column to RAitems table.-
    '----29-Mar-2010==  V2.1-- Cleanup CREATE new DB,  --
    '---     and populate all Reference tables..--
    '----04-May-2010==  V2.1.2422 -- Windows Authentication. --
    '---     NO MORE adding user Login when DB Created..--
    '----26-May-2010==  V2.1.2434 -- CreateDB- SecurityId now created HERE-
    '----         by RE-Reading ABN DateCreated.. --

    '== V2.2.2900==
    '----28-Apr-2011==  V2.2.2900 -- Add tables for Job Service Checklists..
    '----         Also table for Future extra Job Info.. --

    '== V3.0.3013==
    '----19-Nov-2011== REv-3013 -- PREP for VB.NET upgrade...
    '--            Remove all GOSUB calls.. ( For main CreateJobsDb:  make subs into functions..)
    '==
    '== '=3073.21= 21Feb2013==
    '==  Drop Brands 'Barf' and 'Precise PCs' ..--
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==	
    '== '= v3.1.3101.909= 09Sep2014==
    '==    RESTORE moved into this module...--
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== 
    '==  grh. JobMatix 3.1.3101 ---  28-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider is needed for Jet OleDb driver).
    '==
    '==   AND  3101= gbUpgradeJobsDB is GONE -  =
    '== 
    '==  grh. JobMatix 3.1.3103.225 ---  25-Feb-2015 ===
    '==   >> Fixed Error in RESTORE..  Was wrongly reading DataReader for master Path 
    '==               and not catching the error.
    '==
    '==  grh. JobMatix 3.1.3107.706 ---  06-Jul-2015 ===
    '==       >> Expand cols (ProblemLong, ServiceNotes, SessionTimes, Notifications)- to 4000.
    '== 
    '==  grh. JobMatix 3.1.3107.803-  03-Aug-2015 ===
    '==   >>  Now Using .Net 4.5.2
    '==
    '== '==  NEW VERSION-
    '==  grh. JobMatix 3.2.3203.105-  05-Jan-2016 ===
    '==   >>  Now Back to .Net 3.5
    '==     >> Make Create SQL for Jobs Attachments Tables.
    '==             (JobAttachments and RA_Attachments)
    '==     >> Add column "SystemUnderWarranty" to Jobs Table.
    '==
    '==  -- 3327.0121- 21-Jan-2017-
    '==         >>-- TABLE RAItems-  Expand column "RA_Symptoms" from 240 to 511 chars-..--
    '==
    '== 3357.0205= 05Feb2017=
    '==  sSql &= "ALTER TABLE dbo.RAItems ALTER COLUMN RM_ItemBarcode VARCHAR (40) NOT NULL; " & vbCrLf
    '-=  sSql &= "ALTER TABLE dbo.RAItems ADD RM_SerialAudit_id int NOT NULL DEFAULT -1; " & vbCrLf
    '= 
    '==  3401.0415 Backup Function updated and moved to here in "modCreateJobs" module. . -
    '==       - Recognition of Thin Clients..
    '==
    '==
    '==   v3.4.3403.0909 -- 09Sep2017= - 
    '==      -- Backup- DELETE source BAK after successful copy (if OK by user). 
    '==
    '==   >> 3411.0217=  17-Feb-2018= 
    '==          -- 3411.0217-Create new DB (Setup)  TypeOfBusiness item added to ColBusinessDetails.
    '==                 Make it optional to set up the Goods/Tasks etc as per computer Shop.... 
    '==   >>  3431.0515-
    '==         -- Increase ServiceNotes width to MAX..
    '== 
    '==
    '==    3501.0713 13-July-2018= (Updates from 3431.0712- )
    '==       -- 3501.0714  Separate out backup/Restore into separate Module.
    '==
    ''= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 


    '==  private functions.-
    '==  private functions.-

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =
    '-===FF->

    '--  get recordset as collection for SELECT..--

    Private Function mbGetRecordCollection(ByRef cnnSQL As OleDbConnection, _
                                            ByVal sSql As String, _
                                             ByRef colResult As Collection) As Boolean
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim sName As String
        Dim col1 As Collection
        Dim colRow As Collection
        '== Dim fld1 As ADODB.Field

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mbGetRecordCollection = False
        If Not gbGetDataTable(cnnSQL, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset.." & vbCrLf & _
             "Error text: " & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If Not (rs1 Is Nothing) Then
                colResult = New Collection
                If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    '= rs1.MoveFirst()
                    For Each dataRow1 As DataRow In rs1.Rows
                        colRow = New Collection
                        For Each column1 As DataColumn In rs1.Columns '== fld1 In rs1.Fields
                            col1 = New Collection
                            sName = column1.ColumnName
                            col1.Add(sName, "name")
                            col1.Add(dataRow1.Item(sName), "value")
                            colRow.Add(col1, LCase(sName))
                        Next column1 '= fld1 '=col1
                        colResult.Add(colRow)
                    Next dataRow1
                    '== While Not rs1.EOF
                    '==   rs1.MoveNext()
                    '== End While '--eof.-
                    mbGetRecordCollection = True '--got something..-
                End If '--EMPTY. bof-
                '== rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSElect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Execute SQL Command..--
    '-- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Private Function mbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction, _
                                      ByRef sLastSqlErrorMessage As String) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbExecuteSql = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            intAffected = sqlCmd1.ExecuteNonQuery()
            mbExecuteSql = True   '--ok--
            '== MsgBox("Sql exec ok. " & intAffected & " records affected..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbExecuteSql: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            sLastSqlErrorMessage = sErrorMsg
            '== MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '==  Create Table and Index Functions--
    '---subroutine create table --
    '---subroutine create table --
    Private Function mbDb_createTable(ByRef cnnSQL As OleDbConnection, _
                                      ByVal sTableName As String, _
                                       ByVal sCreateSql As String, _
                                        ByVal bIsTransaction As Boolean, _
                                         ByRef sqlTran1 As OleDbTransaction, _
                                           ByVal sCreateLogPath As String, _
                                            ByRef intSqlErrors As Integer) As Boolean
        '== Dim bOk As Boolean
        '= Dim L1 As Integer
        Dim sErrorMsg As String
        Dim sRollback As String = ""
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer

        mbDb_createTable = False
        Call gbLogMsg(sCreateLogPath, "Creating SQL Table:  '" & sTableName & "'.." & vbCrLf & _
                                                                         "SQL is:  " & sCreateSql)
        Try
            sqlCmd1 = New OleDbCommand(sCreateSql, cnnSQL)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            intAffected = sqlCmd1.ExecuteNonQuery()
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  created ok..")
            mbDb_createTable = True
        Catch ex As Exception
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sErrorMsg = "ERROR in CreateTable: " & ex.Message & vbCrLf & "==" & vbCrLf & _
                                 "Sql was: " & vbCrLf & sCreateSql & vbCrLf & vbCrLf & sRollback & _
                                  "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg & vbCrLf)
            MsgBox("Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            intSqlErrors += 1
        End Try
        '=3101= bOk = gbExecuteCmd(cnnSQL, sCreateSql, L1, sErrorMsg)
        '=3101= If Not bOk Then
        '=3101= '== gbCreateJobsDB = False
        '=3101= Else '--ok--  add privileges--
        '=3101= End If '--create ok-
        '=3101= '==Return
        '= mbDb_createTable = bOk
    End Function '--create table..-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '---subroutine to create INDEX --
    '---subroutine to create INDEX --

    Private Function mbDb_createIndex(ByRef cnnSQL As OleDbConnection, _
                                       ByVal sTableName As String, _
                                        ByVal bIsTransaction As Boolean, _
                                         ByRef sqlTran1 As OleDbTransaction, _
                                          ByVal fx As Integer, _
                                          ByVal sFldList As String, _
                                           ByVal sCreateLogPath As String, _
                                                ByRef intSqlErrors As Integer) As Boolean
        '== Dim bOk As Boolean
        '= Dim L1 As Integer
        Dim s1 As String
        Dim sSql, sErrorMsg As String
        Dim sRollback As String = ""
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer

        mbDb_createIndex = False
        s1 = sTableName & "_IDX" & Trim(CStr(fx))
        sSql = " CREATE INDEX " & s1 & " ON " & sTableName & " (" & sFldList & ")"
        sSql = sSql & "  WITH FILLFACTOR=80 "
        Call gbLogMsg(sCreateLogPath, " -- Creating SQL Index:  " & vbCrLf & sSql)

        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSQL)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            intAffected = sqlCmd1.ExecuteNonQuery()
            Call gbLogMsg(sCreateLogPath, "  ==  Table: " & sTableName & " -- INDEX: " & s1 & " created ok..")
            mbDb_createIndex = True
        Catch ex As Exception
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sErrorMsg = "ERROR in CreateTable: " & ex.Message & vbCrLf & "==" & vbCrLf & _
                                  "Sql was: " & vbCrLf & sSql & vbCrLf & vbCrLf & sRollback & _
                                   "--- end of error msg.--" & vbCrLf
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg & vbCrLf)
            MsgBox("Table: " & sTableName & " ** ERROR **  CREATE INDEX failed.." & vbCrLf & sErrorMsg)
            intSqlErrors += 1
        End Try
        '=3101= bOk = gbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg)
        '=3101= If Not bOk Then
        '=3101= Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & " ** ERROR **  CREATE INDEX failed.." & vbCrLf & sErrorMsg)
        '=3101= intSqlErrors += 1
        '=3101= Else
        '=3101= End If '--ok-
        '=3101= '==Return
        '=3101= mbDb_createIndex = bOk
    End Function '--create index-
    '= = = = = = = = = = = =
    '-===FF->

    '-- V22.-  make collection of Tablenames and CreateSql statements..
    '---- CAN BE USED for both DB Upgrade and DB Create..--
    '----  Builds collection and returns no of tables..

    Private Function mlCreateV22Sql(ByRef colCreateAllSql As Collection) As Integer
        Dim sTableName As String
        Dim sCreate As String
        Dim colTable As Collection


        colCreateAllSql = New Collection

        colTable = New Collection
        sTableName = "ServiceModelChecklists"
        colTable.Add(sTableName, "TableName") '--- "ServiceModelChecklists"
        sCreate = " CREATE TABLE  dbo." & sTableName & " ( "
        sCreate = sCreate & " ModelCheckList_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " ModelCheckList_RMStockId int NOT NULL DEFAULT -1, " '--every model relates to a partic. StockItem..--
        sCreate = sCreate & " ModelCheckListTaskDescription  varchar(80) NOT NULL DEFAULT 'N/A', " '---task descr --
        sCreate = sCreate & " ModelCheckListDateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ) "
        colTable.Add(sCreate, "sql")
        colCreateAllSql.Add(colTable)


        '-- Jobs Checklist Instances..--
        '---- A complete instance contains as many lines as in the original Stock Model..-
        '------ A job can contain multiple instances of a particular service item.
        '-------- The checklists will be distinguished by the sequence no.
        sTableName = "JobServiceCheckLists"
        colTable = New Collection
        colTable.Add(sTableName, "TableName") '--"JobServiceCheckLists"
        sCreate = " CREATE TABLE  dbo." & sTableName & " ( "
        sCreate = sCreate & " JobCheckList_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " JobCheckList_JobId    int DEFAULT -1  NOT NULL REFERENCES jobs(Job_Id), "
        sCreate = sCreate & " JobCheckList_RMStockId int NOT NULL DEFAULT -1, " '--every inst. relates to a StockItem..--
        sCreate = sCreate & " JobCheckListSequence   int NOT NULL DEFAULT -1, " '--distinguishes diffeent instances of same stockId..--
        sCreate = sCreate & " JobCheckListTaskDescription  varchar(80) NOT NULL DEFAULT 'N/A', " '---short descr --
        sCreate = sCreate & " JobCheckListStatus varchar(32) NOT NULL DEFAULT '', " '---eg Done/not done. --
        sCreate = sCreate & " JobCheckListComments  varchar(255)  NOT NULL DEFAULT '',  " '--eg reason..-
        sCreate = sCreate & " JobCheckList_StaffId int  NOT NULL DEFAULT -1,  "
        sCreate = sCreate & " JobCheckListStaffName varchar(50)  NOT NULL DEFAULT '',  "
        sCreate = sCreate & " JobCheckListDateUpdated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ) "
        colTable.Add(sCreate, "sql")
        colCreateAllSql.Add(colTable)


        '-- Create General Purpose table for FUTURE extra Jobs info/events..--
        '-- Jobs Supplementary data (various)..--
        '---- Defined by Job Info Type.--
        sTableName = "JobOtherDetails"
        colTable = New Collection
        colTable.Add(sTableName, "TableName") '--"JobOtherDetails"
        sCreate = " CREATE TABLE  dbo." & sTableName & " ( "
        sCreate = sCreate & " JobOther_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " JobOther_JobId int DEFAULT -1  NOT NULL REFERENCES jobs(Job_Id), "
        sCreate = sCreate & " JobOtherType    varchar(16) DEFAULT 'N/A'  NOT NULL, " '--  Type of info..-
        sCreate = sCreate & " JobOtherStaffName varchar(50) DEFAULT 'N/A'  NOT NULL, " '--  Staff involved..-
        sCreate = sCreate & " JobOtherBarcode   varchar(50) DEFAULT ''  NOT NULL, " '--  Misc. barcode if needed..-
        sCreate = sCreate & " JobOtherIntegerData1 int DEFAULT -1  NOT NULL, " '-- integer info if any..-
        sCreate = sCreate & " JobOtherIntegerData2 int DEFAULT -1  NOT NULL, " '-- integer info if any..-
        sCreate = sCreate & " JobOtherTextData1  varchar(4000) DEFAULT '' NOT NULL, " '--  Text info1 if any..-
        sCreate = sCreate & " JobOtherTextData2  varchar(4000) DEFAULT '' NOT NULL, " '--  Text info2 if any..-
        sCreate = sCreate & " JobOtherDateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP  ) "
        colTable.Add(sCreate, "sql")
        colCreateAllSql.Add(colTable)

        mlCreateV22Sql = colCreateAllSql.Count()

    End Function '--V22 sql--
    '= = = = = = = = = = = =
    '-===FF->

    '-- REturn Create RMItems table SQL..--

    '-- Table of RA's --("RMA"- Return Merchandise Authorisation)..--
    '--  Each RA record represents the RMA history for one serialised item.--

    Private Function msCreateRMItemsSql() As String

        Dim sCreate As String

        '== sTableName = "RAItems"
        sCreate = " CREATE TABLE  dbo.RAItems   ( "
        sCreate &= " RA_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate &= " RA_RecordLocked char(1) NOT NULL DEFAULT 'N', "
        sCreate &= " RA_Status varchar (24) NOT NULL DEFAULT '10-Created', "

        '-  SerialNumber is basic starting key..  Lookup [stockedSerials]: Number --
        sCreate &= " RA_SerialNumber varchar(40) NOT NULL DEFAULT '', " '-stockedSerials:Number --DEF was 'n/a'-
        '== 3357.0205= 
        sCreate &= " RM_SerialAudit_id int NOT NULL DEFAULT -1, "
        sCreate &= " RA_Origin varchar (24) NOT NULL DEFAULT 'Counter', " '--Job/Counter/Stock-
        '== NO FKEY Constraint for JobId !!
        '===== 31Jan2010= V2.1 == =sCreate = sCreate + " RA_JobId int DEFAULT -1  NOT NULL REFERENCES jobs(Job_Id), "
        sCreate = sCreate & " RA_JobId int DEFAULT -1  NOT NULL, "

        sCreate = sCreate & " RA_CustomerBarcode varchar (25) NOT NULL DEFAULT '',"
        sCreate = sCreate & " RA_RMCustomer_Id int NOT NULL DEFAULT -1," '---RetailManagerDB customer key --
        sCreate = sCreate & " RA_CustomerCompany varchar (50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & " RA_CustomerName varchar (50) NOT NULL DEFAULT '',"
        sCreate = sCreate & " RA_CustomerPhone varchar (20) NOT NULL DEFAULT '',"
        sCreate = sCreate & " RA_CustomerMobile varchar (20) NOT NULL DEFAULT '',"

        sCreate = sCreate & " RA_DateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP, "
        '--  Stock Item info is from RM "stock" Table..-
        sCreate = sCreate & " RM_StockId int  DEFAULT -1  NOT NULL, " '--RM stockId--
        sCreate = sCreate & " RM_ItemSupplierCode varchar(15) NOT NULL DEFAULT '', " '-Suppl.Prod.code-
        '= 3357.0205= sCreate = sCreate & " RM_ItemBarcode varchar(15) NOT NULL DEFAULT '', " '---stock barcode --
        sCreate = sCreate & " RM_ItemBarcode varchar(40) NOT NULL DEFAULT '', " '---stock barcode --
        '== 3357.0205= 
        '= sSql &= "ALTER TABLE dbo.RAItems ALTER COLUMN RM_ItemBarcode VARCHAR (40) NOT NULL; " & vbCrLf

        sCreate = sCreate & " RM_ItemDescription  varchar(50) NOT NULL DEFAULT 'n/a', " '--- descr --
        sCreate = sCreate & " RM_ItemCat1  varchar(6) NOT NULL DEFAULT 'n/a', " '---stock cat1 --
        sCreate = sCreate & " RM_ItemCat2  varchar(6) NOT NULL DEFAULT 'n/a', " '---stock cat2 --
        sCreate = sCreate & " RM_ItemCat3  varchar(6) NOT NULL DEFAULT 'n/a', " '---stock cat2 --
        sCreate = sCreate & " RM_Item_Sell_ex money DEFAULT 0  NOT NULL, " '--stock Sell price EX GST--
        '--Invoice Info and SupplierId is from RM "Goods" Table--
        sCreate = sCreate & " RM_GoodsId int  DEFAULT -1  NOT NULL, " '--Orig.GOODS rcvd Id--
        sCreate = sCreate & " RM_InvoiceNo  varchar(20) NOT NULL DEFAULT 'n/a', " '- From GOODS record.-
        sCreate = sCreate & " RM_InvoiceDate datetime  NULL,  " '--From GOODS record..-
        sCreate = sCreate & " RM_GoodsDate datetime  NULL,  " '--From GOODS record..-
        sCreate = sCreate & " RM_OrderNo  varchar(20) NOT NULL DEFAULT 'n/a', " '--From GOODS record.-
        sCreate = sCreate & " RM_OrderId int  DEFAULT -1  NOT NULL, " '--Orig.OrderId in RM--
        sCreate = sCreate & " RM_SupplierId int  DEFAULT -1  NOT NULL, " '--Supplier in RM--
        sCreate = sCreate & " RM_SupplierBarcode varchar(15) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RM_Supplier varchar(50) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RM_Supplier_main_phone varchar(20) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RM_Supplier_main_fax varchar(20) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RM_Supplier_main_email varchar(250) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RM_Supplier_AddressInfo varchar(500) NOT NULL DEFAULT '', "

        '==- 3327.0121- 21-Jan-2017----
        '==    TABLE RAItems-  Expand column "RA_Symptoms" from 240 to 511 chars-..--
        sCreate = sCreate & " RA_Symptoms varchar(511) NOT NULL DEFAULT '', " '-Symptoms of dysfunction -
        '-- RA-PROGRESS--
        sCreate = sCreate & " RA_DateRMA_Requested datetime  NULL, "
        sCreate = sCreate & " RA_DateRMA_Response datetime  NULL, "
        sCreate = sCreate & " RA_RMA_RequestNotes varchar(2040) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RA_RMA_Granted char(1) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RA_SupplierRMA_No varchar(48) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RA_CourierBarcode varchar(32) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RA_DateGoodsSentBack datetime  NULL, "
        sCreate = sCreate & " RA_DateGoodsReceivedBack datetime  NULL, "
        sCreate = sCreate & " RA_ReturnResult varchar(15) NOT NULL DEFAULT '', " '--Replaced/Repaired..
        sCreate = sCreate & " RA_ReturnResultComment varchar(64) NOT NULL DEFAULT '', " '--what ?..

        sCreate = sCreate & " RM_StaffIdCreated int  DEFAULT -1  NOT NULL, " '--RM staffId--
        sCreate = sCreate & " RM_StaffNameCreated varchar(50) NOT NULL DEFAULT '', "
        sCreate = sCreate & " RM_StaffIdUpdated int  DEFAULT -1  NOT NULL, " '--RM staffId--
        sCreate = sCreate & " RM_StaffNameUpdated varchar(50) NOT NULL DEFAULT '', "

        sCreate = sCreate & " RA_X1 varchar(32) NOT NULL DEFAULT '', " '--reserved-1--
        sCreate = sCreate & " RA_X2 varchar(32) NOT NULL DEFAULT '', " '--reserved-2--
        sCreate = sCreate & " RA_DateUpdated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP  ) "

        msCreateRMItemsSql = sCreate

    End Function '-RA-items..-
    '= = = = = = = = =  = =  = =
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
        Dim rs1 As DataTable  '= ADODB.Recordset

        gbGetDateCreatedDateParts = False
        sSql = "SELECT  DateCreated, "
        sSql = sSql & " DATEPART(yyyy, DateCreated) AS Year,  "
        sSql = sSql & " DATEPART(dayofyear, DateCreated) AS DayOfYear,  "
        sSql = sSql & " DATEPART(hour, DateCreated) AS Hour,  "
        sSql = sSql & " DATEPART(minute, DateCreated) AS Minute,  "
        sSql = sSql & " DATEPART(second, DateCreated) AS Second  "
        sSql = sSql & "   FROM SystemInfo WHERE (SystemKey='" & sSystemKey & "');  "

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(cnnSQL, rs1, sSql) Then
            MsgBox("Failed to get systemInfo recordset..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--build dictionary of sysinfo items....-
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                colDateParts = New Collection '--  holds system settings..
                '==If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                Dim dataRow1 As DataRow = rs1.Rows(0)   '=first row.-
                '== If (Not rs1.EOF) Then '---
                '--add all columun items....
                dateCreated = CDate(dataRow1.Item("dateCreated")) '--send back full date for testing..-
                colDateParts.Add("" & dataRow1.Item("Year"), "Year")
                colDateParts.Add("" & dataRow1.Item("DayOfYear"), "DayOfYear")
                colDateParts.Add("" & dataRow1.Item("Hour"), "Hour")
                colDateParts.Add("" & dataRow1.Item("Minute"), "Minute")
                colDateParts.Add("" & dataRow1.Item("Second"), "Second")
                gbGetDateCreatedDateParts = True
                '== End If '-eof-
                '= rs1.Close()
            End If '--rs nothing=-
        End If '--get rs-
        rs1 = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function '--get date..-
    '= = = = = = = = =  = =  =

    '-- make SecurityId from ABN DateCreated.-

    Public Function gsMakeSecurityId(ByRef cnnSQL As OleDbConnection, _
                                      ByRef dateCreated As Date) As String
        Dim colDateParts As Collection
        Dim sDay As String
        Dim sYear As String
        Dim lngSeconds As Integer

        gsMakeSecurityId = ""
        If gbGetDateCreatedDateParts(cnnSQL, "BusinessABN", dateCreated, colDateParts) Then '--got date..
            sYear = Trim(colDateParts.Item("Year"))
            sDay = Trim(colDateParts.Item("DayOfYear"))
            lngSeconds = (Val(colDateParts.Item("Hour")) * 3600)
            lngSeconds = lngSeconds + (Val(colDateParts.Item("Minute")) * 60) + Val(colDateParts.Item("Second"))
            gsMakeSecurityId = sYear & Trim(CStr(Len(sDay))) & sDay & Trim(CStr(lngSeconds))
        End If '--dateparts..-
    End Function '-security Id..-
    '= = = = = = = = = = = = =
    '-===FF->

    '= 3203.105=
    '-- Make Create SQL for JobAttachments and RA_Attachments Tables.

    Const K_width_party_info As Integer = 500
    Const K_width_staff_name As Integer = 50
    Const K_width_comments As Integer = 1000

    Public Function gsMakeAttachmentsScript(ByVal strAppType As String) As String
        Dim sCreateSql As String

        gsMakeAttachmentsScript = ""
        If (UCase(strAppType) <> "JOB") And (UCase(strAppType) <> "RA") Then
            Exit Function
        End If
        If (UCase(strAppType) = "JOB") Then
            sCreateSql = "CREATE TABLE  dbo.Job_Attachments ( " ' == . 
        Else
            sCreateSql = "CREATE TABLE  dbo.RA_Attachments ( " ' == . 
        End If
        sCreateSql &= "  doc_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,"
        '== sCreateSql &= "  doc_app_type nvarChar(15) NOT NULL DEFAULT '',"     '==  JOB/RA -
        If (UCase(strAppType) = "JOB") Then
            sCreateSql &= "  doc_job_id INT NOT NULL  REFERENCES jobs(Job_Id), "        '== Job_id  ==
        Else
            sCreateSql &= "  doc_ra_id INT NOT NULL  REFERENCES RAItems(RA_Id), "        '== RA_id ==
        End If
        sCreateSql &= "  doc_party_info nvarChar(" & CStr(K_width_party_info) & ") NOT NULL DEFAULT '',"

        sCreateSql &= "  doc_staff_id INT NOT NULL DEFAULT -1, "
        sCreateSql &= "  doc_staff_name nvarChar(" & CStr(K_width_staff_name) & ") NOT NULL DEFAULT '', "

        sCreateSql &= "  doc_file_format nvarChar(15) NOT NULL DEFAULT '',"  '==  PDF/XPS/PNG etc -
        sCreateSql &= "  doc_file_title nvarChar(200) NOT NULL DEFAULT '', "
        sCreateSql &= "  doc_file_is_image bit NOT NULL DEFAULT 0, "
        sCreateSql &= "  doc_file_size int NOT NULL DEFAULT 0, "
        sCreateSql &= "  doc_file_content varbinary(max) NULL, "
        sCreateSql &= "  doc_file_comments nvarChar(" & CStr(K_width_comments) & ") NOT NULL DEFAULT '',"
        sCreateSql &= "  doc_date_inserted datetime NOT NULL DEFAULT CURRENT_TIMESTAMP "
        sCreateSql &= " ); "  '-- done-- 

        gsMakeAttachmentsScript = sCreateSql
    End Function  '-gsMakeDocArchiveScript=
    '= = = = = = = = = = = = =
    '-===FF->

    '--  upgrade to V22.-- 27-Apr-2011== Rev:2900==
    '----- by adding three Tables..--

    '-- V2.2 -- Service Model Tables..--
    '-- ---  derived from:- Quotation Tables..--
    '=3101 =  GONE  ==

    '=3101 = Public Function gbUpgradeDatabaseV22(ByRef cnnSQL As OleDbConnection, _
    '=3101 =                                        ByRef ColSqlDBInfo As Collection, _
    '=3101 =                                          ByVal bIsSqlAdmin As Boolean, _
    '=3101 =                                            ByVal sCreateLogPath As String, _
    '=3101 =                                              ByRef bRestartNeeded As Boolean) As Boolean '--true if no errors..

    '=3101 =  End Function '-- V22 upgrade..--
    '= = = = = = = = = = = ==
    '-===FF->

    '-- EXTRA..  V2.1 Upgrade--
    '-- EXTRA..  V2.1 Upgrade--

    '-- RA Items.. MUST DROP FKEY CONSTRAINT on Column 'RA_JobId',
    '----  since NOT all RA's have a Job No.==

    '==3101= THIS now OBSOLETE ==
    '==3101= THIS now OBSOLETE ==

    '==3101= Public Function gbUpgradeJobsDBVersion_21(ByRef cnnSQL As OleDbConnection, _
    '==3101=                                            ByRef ColSqlDBInfo As Collection, _
    '==3101=                                             ByVal sSqlDBName As String, _
    '==3101=                                              ByVal sSqlLoginName As String, _
    '==3101=                                                ByVal bV21_UpgradePermitted As Boolean, _
    '==3101=                                                  ByRef bV21_UpgradeNeeded As Boolean, _
    '==3101=                                                   ByRef bV21_UpgradeCompletedOK As Boolean) As Boolean

    '==3101= End Function '--upgrade 2.1 ==
    '= = = = = = = = =  = =  =
    '-===FF->

    '--  V2 Upgrade--
    '--  V2 Upgrade--
    '--create new tables.--
    '---- (ExtrasRefChecklist (REF), QuoteJobParts,QuoteJobExtras)..-
    '-- Input: "bV2UpgradePermitted" is true if upgrade is to be executed now..---
    '--- Output:  "bV2UpgradeNeeded" is returned true if specified DB is at V13 Level..--
    '--- Output:  " bV2UpgradeCompletedOK" is returned true if upgrade was actually executed....--

    '--  OBSOLETE - Won't be called.. -
    '--  OBSOLETE - Won't be called.. -
    '--  OBSOLETE - Won't be called.. -

    '==3101= Public Function gbUpgradeJobsDBVersion2(ByRef cnnSQL As OleDbConnection, _
    '==3101=                                        ByRef ColSqlDBInfo As Collection, _
    '==3101=                                          ByVal sSqlDBName As String, _
    '==3101=                                          ByVal sSqlLoginName As String, _
    '==3101=                                            ByVal bV2UpgradePermitted As Boolean, _
    '==3101=                                             ByRef bV2UpgradeNeeded As Boolean, _
    '==3101=                                               ByRef bV2UpgradeCompletedOK As Boolean) As Boolean

    '==3101=     gbUpgradeJobsDBVersion2 = False
    '==3101=     bV2UpgradeCompletedOK = False
    '==3101=     bV2UpgradeNeeded = False '-- OUTPUT.. assume no update needed..-

    '==3101= '== If bOk Then
    '==3101= '= cnnSQL.CommitTrans()
    '==3101=    bV2UpgradeCompletedOK = True
    '==3101=    Exit Function
    '==3101= End Function '--V2 Upgrade.--
    '= = = = = = = = =  = =  =
    '-===FF->

    '-- Expand Goods columns and DROP taskType_id column in JobTasks..--
    '-- Expand Goods columns and DROP taskType_id column in JobTasks..--

    '=3103= GONE - gbUpgradeJobsDB  =
    '=3103= GONE - gbUpgradeJobsDB  =

    '=3101= Public Function gbUpgradeJobsDB(ByRef cnnSQL As OleDbConnection, _
    '=3101=                                    ByRef ColSqlDBInfo As Collection, _
    '=3101=                                     ByVal sSqlDBName As String, _
    '=3101=                                      ByVal sSqlLoginName As String, _
    '=3101=                                       ByRef bContinue As Boolean) As Boolean

    '= = = = = = = = =  = =  =
    '-===FF->

    '==== SQL - INITIAL Create for JOBS database = = =
    '--  updated 06-July-2009---
    '---   for v1.3 with wider Goods columns..--
    '-----  and NO taskType_id column in JobTasks..--

    '= =Build new sql database= = = ==  =
    '- - - - - - - - -
    '= =Build new sql database= = = ==  sDBUserName=

    Public Function gbCreateJobsDB(ByRef cnnSQL As OleDbConnection, _
                                   ByVal sSqlDBName As String, _
                                     ByVal sBusinessABN As String, _
                                      ByRef colBusinessDetails As Collection, _
                                       ByVal sSqlPrimaryFile As String, _
                                        ByVal sSqlLogFile As String, _
                                         ByVal sCreateLogPath As String, _
                                         ByRef iSqlErrors As Integer, _
                                         ByRef dDateCreated As DateTime, _
                                           ByRef sSecurityId As String) As Boolean

        Dim k, i, j, ans As Integer
        Dim tx, isx, fx As Integer
        Dim bOk As Boolean
        Dim L1 As Integer
        Dim s1, Msg As String
        Dim sCreate, sErrorMsg As String
        '== Dim sKeyField As String
        '== Dim sConnect, sAppPath As String
        Dim sSql, sTableName, sTextdataPath As String
        '== Dim sSection, sErrorMsg As String
        '= Dim sSql As String
        Dim sFldList, sName As String
        '===Dim sDBUserName As String
        Dim sJT2SecurityId As String
        '===Dim vType As Object
        '== Dim lSize As Integer
        '== Dim sLogStuff As String
        Dim col1 As Collection
        Dim dateCreated As Date
        Dim colCreateAllSql As Collection '--V22--
        Dim OleDbTran1 As OleDbTransaction

        gbCreateJobsDB = False
        If Not MsgBox("Database: '" & sSqlDBName & _
                     "' is being (re-) created.." & vbCrLf & _
                      "Do you want to continue ?", _
                            MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Exit Function
        End If
        '=====gbCreateJobsDB = True
        iSqlErrors = 0
        '====sDBUserName = ""  '-- was from Create Form.--
        Call gbLogMsg(sCreateLogPath, "- Create Jobs DB started.. JobMatix Version: " & _
                                      My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & _
                                  " Build " & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & "..")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        sJT2SecurityId = "" '--we'll create our own.
        '--  server connected ok..  check if DB exists--

        '--IF EXISTS--kill database to re-load--
        '--use "sp_databases" to get set of databases from master db--
        '--use "glExecSP" to call the sp and get reslt set of database names--
        '--check result set to see if db exists--
        '== s1 = "USE MASTER " & vbCrLf
        '== bOk = gbExecuteTrans(cnnSQL, s1)
        cnnSQL.ChangeDatabase("master")
        If gbExistsDatabase(cnnSQL, sSqlDBName) Then
            Call gbLogMsg(sCreateLogPath, "Deleting existing SQL database:  '" & sSqlDBName & "'..")
            s1 = "DROP DATABASE " & sSqlDBName & vbCrLf
            bOk = gbExecuteCmd(cnnSQL, s1, L1, sErrorMsg)
            If bOk Then
                Call gbLogMsg(sCreateLogPath, "-- Existing Jobs DB'" & sSqlDBName & "' DROPPED..")
            Else
                Call gbLogMsg(sCreateLogPath, "-- Failed to DROP Jobs DB'" & sSqlDBName & vbCrLf & sErrorMsg)
                MsgBox("-- Failed to DROP Jobs DB'" & sSqlDBName & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
            End If
        End If
        '-- (re) create SQL DB --

        s1 = "CREATE DATABASE " & sSqlDBName & vbCrLf
        '--add file path if specified---
        If Len(sSqlPrimaryFile) > 0 Then
            s1 = s1 & "ON (NAME= " & sSqlDBName & "_dat, FILENAME='" & sSqlPrimaryFile & "')" & vbCrLf
            s1 = s1 & "LOG ON (NAME= " & sSqlDBName & "_log, FILENAME='" & sSqlLogFile & "')" & vbCrLf
        End If
        '-- do create--
        '====gAdvise "Creating SQL database:  '" + sSqlDBName + "'.."
        Call gbLogMsg(sCreateLogPath, "Creating SQL database:  '" & sSqlDBName & "'..")
        bOk = gbExecuteCmd(cnnSQL, s1, L1, sErrorMsg)
        If bOk Then
            Call gbLogMsg(sCreateLogPath, "Created ok SQL database:  '" & sSqlDBName & "'..")
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call gbLogMsg(sCreateLogPath, "Failed to create SQL database !!" & vbCrLf & "SQL is: " & s1 & vbCrLf & sErrorMsg)
            MsgBox("Failed to create SQL database !!" & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            iSqlErrors = iSqlErrors + 1
            gbCreateJobsDB = False
            Exit Function '--kill sql op...---
        End If
        '--DELETE user login if already exists..--

        '----04-May-2010==  V2.1.2422 -- Windows Authentication. --
        '---     NO MORE adding user Login when DB Created..--
        '---     NO MORE adding user Login when DB Created..--

        '--Move to new db--
        s1 = "USE " & sSqlDBName & vbCrLf
        bOk = gbExecuteCmd(cnnSQL, s1, L1, sErrorMsg)
        '== cnnSQL.BeginTrans()
        OleDbTran1 = cnnSQL.BeginTransaction

        '--CREATE all rquired tables--
        sTableName = "Jobs"
        sCreate = " CREATE TABLE  dbo.Jobs   ( "
        sCreate = sCreate & "  Job_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED,"

        sCreate = sCreate & "  CustomerBarcode varchar (25) NOT NULL DEFAULT '',"
        sCreate = sCreate & "  RMCustomer_Id int NOT NULL DEFAULT -1," '---RetailManagerDB customer key --
        sCreate = sCreate & "  CustomerCompany varchar (50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  CustomerName varchar (50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  CustomerPhone varchar (20) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  CustomerMobile varchar (20) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  Priority varchar (1) NOT NULL DEFAULT 'H'," '-H(ome)/B(usiness).-
        sCreate = sCreate & "  NominatedTech varchar(50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  JobStatus varchar (16) NOT NULL  DEFAULT '10-Created',"

        sCreate = sCreate & "  GoodsInCare varchar(250) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  GoodsOther varchar(250) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  GoodsBrand varchar(50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  GoodsModel varchar(50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  GoodsExtras varchar(250) NOT NULL DEFAULT 'N/A'," '--bag, cables etc.

        sCreate = sCreate & "  MultiAccounts varchar(1) DEFAULT 'N' NOT NULL,"
        sCreate = sCreate & "  Username varchar(32)  NOT NULL DEFAULT '',"
        sCreate = sCreate & "  UserPassword varchar(32)  NOT NULL DEFAULT '',"
        sCreate = sCreate & "  DataBackupReqd varchar(1) DEFAULT 'N' NOT NULL,"
        sCreate = sCreate & "  DataDiskReqd varchar(1) DEFAULT 'N' NOT NULL,"
        sCreate = sCreate & "  ProblemShort   varchar(250) NOT NULL DEFAULT ''," '--concat c/box labels..
        sCreate = sCreate & "  ProblemLong   varchar(5000) NOT NULL DEFAULT '',"
        sCreate = sCreate & "  ProblemSymptoms varchar(250) NOT NULL DEFAULT '',"
        sCreate = sCreate & "  JobReturned varchar (1) NOT NULL DEFAULT 'N',"
        sCreate = sCreate & "  SystemUnderWarranty bit NOT NULL DEFAULT 0,"

        sCreate = sCreate & "  DateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreate = sCreate & "  DatePromised datetime NOT NULL DEFAULT '2020/12/25',"
        sCreate = sCreate & "  RcvdStaffName varchar(50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  RcvdRMStaff_Id int NOT NULL DEFAULT -1," '---RetailManagerDB Staff Id key --
        sCreate = sCreate & "  Diagnosis    varchar(550) NOT NULL DEFAULT '',"
        '== 3431.0513= sCreate = sCreate & "  ServiceNotes varchar(4000)  NOT NULL DEFAULT '',"
        sCreate = sCreate & "  ServiceNotes varchar(max)  NOT NULL DEFAULT '',"
        '== NOT YET-  sCreate = sCreate & "  ServiceNotes varchar(MAX)  NOT NULL DEFAULT '',"
        sCreate = sCreate & "  SessionTimes varchar(5000)  NOT NULL DEFAULT '',"
        sCreate = sCreate & "  TotalServiceTime decimal(6,2) NOT NULL DEFAULT 0," '--hours--

        sCreate = sCreate & "  DateCompleted datetime  NULL,"
        sCreate = sCreate & "  TechStaffName varchar(50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  TechRMStaff_Id int NOT NULL DEFAULT -1," '---RetailManagerDB Staff Id key --
        sCreate = sCreate & "  Notifications varchar(5000) NOT NULL DEFAULT '',"
        sCreate = sCreate & "  DateDelivered datetime  NULL,"
        sCreate = sCreate & "  DeliveredStaffName varchar(50) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & "  DeliveredRMStaff_Id int NOT NULL DEFAULT -1" '---RetailManagerDB Staff Id key --
        sCreate = sCreate & ", DateUpdated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP"
        sCreate = sCreate & ")" '--end of list--
        '===== GoSub db_createTable
        bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)

        '--add other indexes----
        fx = 1 '---index no...
        sFldList = " CustomerCompany, JobStatus"
        '===== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)

        fx = 2 '---index no...
        sFldList = " CustomerName, JobStatus"
        '===== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)

        fx = 3 '---index no...
        sFldList = " RcvdStaffName,JobStatus"
        '===== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)

        fx = 4 '---index no...
        sFldList = " JobStatus,CustomerName"
        '===== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)

        fx = 5 '---index no...
        sFldList = " DateCreated ,CustomerName, JobStatus"
        '===== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)

        fx = 6 '---index no...
        sFldList = " Priority, CustomerName, JobStatus"
        '===== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)

        fx = 7 '---index no...
        sFldList = " TechStaffName, CustomerName, JobStatus"
        '===== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If


        '-- Create Reference Table of GOODS TYPES..--
        '-- Create Reference Table of GOODS TYPES..--
        sTableName = "GoodsTypes"
        sCreate = " CREATE TABLE  dbo.GoodsTypes   ( "
        sCreate = sCreate & " GoodsType_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " GoodsTypeDescription  varchar(50) NOT NULL DEFAULT 'N/A', " '---task descr --
        sCreate = sCreate & " GoodsTypeCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '== GoSub db_createTable
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)

        '--add other indexes----
        fx = 1 '---index no...
        sFldList = " GoodsTypeDescription"
        '== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If



        '--Create Reference Table of Service Task Types..--
        '--Create Reference Table of Service Task Types..--
        sTableName = "JobTaskTypes"
        sCreate = " CREATE TABLE  dbo.JobTaskTypes   ( "
        sCreate = sCreate & " TaskType_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " TaskTypeDescription  varchar(50) NOT NULL DEFAULT 'N/A', " '---task descr --
        sCreate = sCreate & " TaskTypeCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '===== GoSub db_createTable
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)

        '--add other indexes----
        fx = 1 '---index no...
        sFldList = " TaskTypeDescription"
        '== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If



        '-- Create Reference Table of BRANDS..--
        '-- Create Reference Table of BRANDS..--
        sTableName = "JobBrands"
        sCreate = " CREATE TABLE  dbo.JobBrands   ( "
        sCreate = sCreate & " Brand_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " BrandName  varchar(50) NOT NULL DEFAULT 'N/A', " '---task descr --
        sCreate = sCreate & " BrandCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '===== GoSub db_createTable
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)

        '--add other indexes----
        fx = 1 '---index no...
        sFldList = "BrandName"
        '== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If



        '--Create Reference table of Symptoms..--
        '-- Create Reference Table of Symptoms..--
        sTableName = "Symptoms"
        sCreate = " CREATE TABLE  dbo.Symptoms   ( "
        sCreate = sCreate & " Symptom_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " SymptomDescr  varchar(50) NOT NULL DEFAULT 'N/A', " '---task descr --
        sCreate = sCreate & " DateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '===== GoSub db_createTable
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)

        '--add other indexes----
        fx = 1 '---index no...
        sFldList = "SymptomDescr"
        '== GoSub db_createIndex
        If bOk Then bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If



        '--Create - dependent table of tasks performed for this job --
        '--Create - dependent table of tasks performed for this job --
        sTableName = "JobTasks"
        sCreate = " CREATE TABLE  dbo.JobTasks   ( "
        sCreate = sCreate & " Task_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " TaskJob_Id int " & "DEFAULT 0  NOT NULL REFERENCES jobs(Job_Id), "
        sCreate = sCreate & " TaskType_Id int NOT NULL DEFAULT -1, " '=== REFERENCES JobTaskTypes(TaskType_Id),==
        sCreate = sCreate & " Description  varchar(50) NOT NULL DEFAULT 'N/A', " '---task descr --
        sCreate = sCreate & " PerformedByRMStaff_id int NOT NULL DEFAULT 0, " '---Staff id --
        sCreate = sCreate & " PerformedByStaffName  varchar(50)NOT NULL DEFAULT 'N/A', " '---Staff name --
        sCreate = sCreate & " DateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '===== GoSub db_createTable
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If


        '--Create - dependent table of job parts --
        '--Create - dependent table of job parts --
        sTableName = "JobParts"
        sCreate = " CREATE TABLE  dbo.JobParts   ( "
        sCreate = sCreate & " Part_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " PartJob_Id int " & "DEFAULT 0  NOT NULL REFERENCES jobs(Job_Id), "
        sCreate = sCreate & " PartSerialNumber varchar(40) NOT NULL DEFAULT ''," '--V2- 18Jan2010..-
        sCreate = sCreate & " RMBarcode varchar(24) NOT NULL, " '---RetailManagerDB Barcode --
        sCreate = sCreate & " RMstock_Id int NOT NULL, " '---RetailManagerDB Stock key --
        sCreate = sCreate & " RMDescription  varchar(50) NOT NULL DEFAULT 'N/A', " '---Stock item descr --
        sCreate = sCreate & " RMLongDescription  varchar(600) NOT NULL DEFAULT 'N/A', " '---Stock long descr --
        sCreate = sCreate & " RMCost  money NOT NULL DEFAULT 0, " '---Stock cost (price) --
        sCreate = sCreate & " RMSell  money NOT NULL DEFAULT 0, " '---Stock Sell (price) --
        sCreate = sCreate & " RMCat1  varchar(16) NOT NULL DEFAULT '', " '---CAT1 Stock cat --
        sCreate = sCreate & " RMCat2  varchar(16) NOT NULL DEFAULT '', " '---CAT2 Stock cat --
        sCreate = sCreate & " RMCat3  varchar(16) NOT NULL DEFAULT '', " '---CAT3 Stock cat --
        sCreate = sCreate & " IsWarrantyPart char(1) NOT NULL DEFAULT 'N',"
        sCreate = sCreate & " WarrantyPartNo varchar(24) NOT NULL DEFAULT 'N/A',"
        sCreate = sCreate & " ServicedByRMStaff_id int NOT NULL DEFAULT 0, " '---Staff id --
        sCreate = sCreate & " ServicedByStaffName  varchar(50)NOT NULL DEFAULT 'N/A', " '---Staff name --
        sCreate = sCreate & " DateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ) "
        '===== GoSub db_createTable
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If


        '-- V2 -- Quotation Tables..--
        '-- This table not created in V2.==
        '-- This table not created in V2.==
        sTableName = "ModelCheckList"
        sCreate = " CREATE TABLE  dbo.ModelCheckList   ( "
        sCreate = sCreate & " CheckList_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " CheckListDescription  varchar(50) NOT NULL DEFAULT 'N/A', " '---task descr --
        sCreate = sCreate & " CheckListDateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '==== NOT NEEDED in V22.. == GoSub db_createTable
        '==== REPLACED BY "ServiceModelChecLists".. ==

        sTableName = "QuoteJobParts"
        sCreate = " CREATE TABLE  dbo.QuoteJobParts   ( "
        sCreate = sCreate & " QuotePart_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " QuotePart_JobId int DEFAULT -1  NOT NULL REFERENCES jobs(Job_Id), "
        sCreate = sCreate & " QuotePart_OrderId int  DEFAULT -1  NOT NULL, " '--quote SalesOrderId--
        sCreate = sCreate & " QuotePartBarcode varchar(15) NOT NULL DEFAULT 'N/A', " '---stock barcode --
        sCreate = sCreate & " QuotePartDescription  varchar(50) NOT NULL DEFAULT 'N/A', " '---task descr --
        sCreate = sCreate & " QuotePartCat1  varchar(6) NOT NULL DEFAULT 'N/A', " '---stock cat1 --
        sCreate = sCreate & " QuotePartCat2  varchar(6) NOT NULL DEFAULT 'N/A', " '---stock cat2 --
        sCreate = sCreate & " QuotePart_OrderQty int  DEFAULT 0  NOT NULL, " '--Orig quote qty this part--
        sCreate = sCreate & " QuotePart_StockId int  DEFAULT -1  NOT NULL, " '--RM stockId--
        sCreate = sCreate & " QuotePart_Sell_inc money DEFAULT 0  NOT NULL, " '--quote Sell price--
        sCreate = sCreate & " QuotePartDateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '===== If bOk Then GoSub db_createTable
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If


        '-- This table not created in V2.==
        '-- This table not created in V2.==
        sTableName = "JobsCheckLists"
        sCreate = " CREATE TABLE  dbo.JobsCheckLists   ( "
        sCreate = sCreate & " JobCheckList_Id int IDENTITY (1,1) PRIMARY KEY CLUSTERED, "
        sCreate = sCreate & " JobCheckList_JobId int DEFAULT -1  NOT NULL REFERENCES jobs(Job_Id), "
        sCreate = sCreate & " JobCheckListDescription  varchar(50) NOT NULL DEFAULT 'N/A', " '---extras descr --
        sCreate = sCreate & " JobCheckListComments  varchar(250)  NOT NULL DEFAULT '',  " '--eg Done/not done..-
        sCreate = sCreate & " JobCheckList_StaffId int  NOT NULL DEFAULT -1,  "
        sCreate = sCreate & " JobCheckListStaffName varchar(50)  NOT NULL DEFAULT '',  "
        sCreate = sCreate & " JobCheckListDateUpdated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP) "
        '==== NOT NEEDED in V22.. == If bOk Then GoSub db_createTable
        '==== REPLACED BY "JobServiceCheckLists"..

        '-- ex V2 -- Quotation Tables..--
        '-- V22 Service Models..--
        '-- V22 Service Models..--
        bOk = True
        L1 = mlCreateV22Sql(colCreateAllSql)
        If (L1 > 0) Then '--create all tables..--
            For Each col1 In colCreateAllSql
                sTableName = col1.Item("TableName")
                sCreate = col1.Item("sql")
                If bOk Then
                    bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)
                End If
            Next col1 '--col1..
        End If '--L1--
        '=== end V22 ========================
        If Not bOk Then
            Exit Function
        End If

        '-- Create RA Table-
        sTableName = "RAItems"
        sCreate = msCreateRMItemsSql() '--get SQL Create string..-
        If bOk Then
            bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)
        End If
        If Not bOk Then
            Exit Function
        End If
        '= = = = = = =  = =

        '= 3203.105=  05Jan2016=
        '-- Create JOB Attachments Table-
        sTableName = "Job_Attachments"
        sCreate = gsMakeAttachmentsScript("JOB")  '--get SQL Create string..-
        If bOk Then
            bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)
        End If
        If Not bOk Then
            Exit Function
        Else
            '-- index-
            fx = 1 '---index no...
            sFldList = "doc_job_id"
            bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)
        End If
        '= = = = = = = = =

        '= 3203.109=  09Jan2016=
        '-- Create RA Attachments Table-
        sTableName = "RA_Attachments"
        sCreate = gsMakeAttachmentsScript("RA")  '--get SQL Create string..-
        If bOk Then
            bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)
        End If
        If Not bOk Then
            Exit Function
        Else
            '-- index-
            fx = 1 '---index no...
            sFldList = "doc_ra_id"
            bOk = mbDb_createIndex(cnnSQL, sTableName, True, OleDbTran1, fx, sFldList, sCreateLogPath, iSqlErrors)
        End If
        '= = = = = = = = =


        '--create systemInfo table..---
        sTableName = "SystemInfo"
        sCreate = " CREATE TABLE  dbo.SystemInfo   ( "
        sCreate = sCreate & "  SystemKey varchar (48) PRIMARY KEY CLUSTERED,"
        sCreate = sCreate & "  SystemValue varchar (4000) NOT NULL DEFAULT '',"
        sCreate = sCreate & " DateCreated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,"
        sCreate = sCreate & " DateUpdated datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ) "
        '===== GoSub db_createTable
        If bOk Then bOk = mbDb_createTable(cnnSQL, sTableName, sCreate, True, OleDbTran1, sCreateLogPath, iSqlErrors)
        If Not bOk Then
            Exit Function
        End If


        If (iSqlErrors > 0) Then
            '== cnnSQL.RollbackTrans()
            '= transaction1.Rollback()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call gbLogMsg(sCreateLogPath, "=== db build completed- with ERRORS !!  =====")
            MsgBox("=== db build completed- with ERRORS !!  =====", MsgBoxStyle.Critical)
            Call gbLogMsg(sCreateLogPath, "=== Rollback executed.. =====")
            Exit Function
        End If

        '-- Populate Reference Tables..  --
        '-- Populate Reference Tables..  --
        '-- Populate Reference Tables..  --

        '-- add some system  Parameters..--
        sSql = "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('RM_JetPath', DEFAULT ); "
        sSql = sSql & "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('RM_JetUsername', DEFAULT ); "
        sSql = sSql & "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('RM_JetPassword', DEFAULT ); "
        sSql = sSql & "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('BusinessABN', '" & sBusinessABN & "' ); "
        '==sSql = sSql + "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('BusinessUserName', '" + sDBUserName + "' ); "
        sSql = sSql & "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('JT2SecurityId', '" & sJT2SecurityId & "' ); "
        sSql = sSql & "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('LicenceKey', DEFAULT ); "
        sSql = sSql & "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue )  VALUES  ('LastBackupFullPath', DEFAULT ); "
        '--  add business details..-
        For Each col1 In colBusinessDetails
            sSql = sSql & "INSERT INTO dbo.SystemInfo (SystemKey, SystemValue ) " & _
                        " VALUES  ('" & msFixSqlStr(col1.Item("name")) & "', '" & msFixSqlStr(col1.Item("value")) & "' ); "
        Next col1
        bOk = mbExecuteSql(cnnSQL, sSql, True, OleDbTran1, sErrorMsg)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table SystemInfo failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table SystemInfo  failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '=3411.0217=
        '-- Don't add computer Reference stuff if not Computer business..
        Dim sBusinessType As String = ""
        Dim bIsComputerShop As Boolean = False
        If colBusinessDetails.Contains("TypeOfBusiness") Then
            col1 = colBusinessDetails.Item("TypeOfBusiness")
            sBusinessType = LCase(col1.Item("value"))
        End If
        If sBusinessType = "computer" Then
            bIsComputerShop = True
        End If
        '--  Add some Goods Types..--
        '--  Add some Goods Types..--
        sSql = "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Generic Goods for Service' ); "
        If bIsComputerShop Then
            sSql &= "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Apple MAC' ); "
            sSql &= "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Desktop' ); "
            sSql &= "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Hard Disk Drive' ); "
            sSql &= "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Laptop' ); "
            sSql &= "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Modem - ADSL' ); "
            sSql &= "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Modem - Dialup' ); "
            sSql &= "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Monitor - LCD' ); "
            sSql &= "INSERT INTO dbo.GoodsTypes (GoodsTypeDescription ) VALUES  ('Tower - Black' ); "
        End If
        bOk = mbExecuteSql(cnnSQL, sSql, True, OleDbTran1, sErrorMsg)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table 'GoodsTypes' failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table 'GoodsTypes'  failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '-- ADD some Symptoms..--
        '-- ADD some Symptoms..--
        sSql = "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Generic- Noisy, Vibrates' ); "
        If bIsComputerShop Then
            sSql &= "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Boot - Nothing' ); "
            sSql &= "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Boot - Looping in Bios' ); "
            sSql &= "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Boot - Slow' ); "
            sSql &= "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Data Recovery to DVD' ); "
            sSql &= "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Hardware Install - HDD' ); "
            sSql &= "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Hardware Install - Upgrade' ); "
            sSql &= "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Network - Dysfunctional' ); "
            sSql &= "INSERT INTO dbo.symptoms (symptomDescr ) VALUES  ('Vibration.' ); "
            sSql &= "INSERT INTO dbo.Symptoms (SymptomDescr ) VALUES  ('Video - Freeziing' ); "
            sSql &= "INSERT INTO dbo.symptoms (symptomDescr ) VALUES  ('Weird noises.' ); "
        End If
        bOk = mbExecuteSql(cnnSQL, sSql, True, OleDbTran1, sErrorMsg)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table 'Symptoms' failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table 'Symptoms'  failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '-- add some Task types..--
        '-- add some Task types..--
        sSql = "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('OS Install' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('OS Repair' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('Hardware Install' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('Software Install' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('CleanUp' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('Security Install' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('Malware Removal' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('Backup Data' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('Recovery Disc(s)' ); "
        sSql = sSql & "INSERT INTO dbo.JobTaskTypes (TaskTypeDescription ) VALUES  ('Insurance Letter' ); "
        bOk = mbExecuteSql(cnnSQL, sSql, True, OleDbTran1, sErrorMsg)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, "** ERROR **  INSERT into Table JobTaskTypes  failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table JobTaskTypes  failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '--  Add some Brands..--
        '--  Add some Brands..--
        sSql = "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Generic Brand' ); "
        If bIsComputerShop Then
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Acer' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Apple' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('AMD' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Asus' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Belkin' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Canon' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Dell' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('dLink' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Hewlett Packard' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('IBM' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Intel' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Laser' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Lenovo' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Microsoft' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Netgear' ) "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Panasonic' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Sanyo' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Samsung' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Seagate' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Sony' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Toshiba' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('Western Digital' ); "
            sSql &= "INSERT INTO dbo.JobBrands (BrandName ) VALUES  ('ZZ Unknown Brand' ); "
        End If '-computer-

        bOk = mbExecuteSql(cnnSQL, sSql, True, OleDbTran1, sErrorMsg)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table JobBrands failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **  INSERT into Table JobBrands failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            Exit Function
        End If

        '-- Add a Model Checklist..-
        sSql = "INSERT INTO dbo.ModelCheckList (CheckListDescription ) VALUES  ('Check Case- Install PSU.' ); "
        sSql = sSql & "INSERT INTO dbo.ModelCheckList (CheckListDescription ) VALUES  ('Install M/Board and Mask.' ); "
        sSql = sSql & "INSERT INTO dbo.ModelCheckList (CheckListDescription ) VALUES  ('Install Video Card.' ); "
        sSql = sSql & "INSERT INTO dbo.ModelCheckList (CheckListDescription ) VALUES  ('Install HDD and Cables' ); "
        '=== Table NOT created in V22.=====
        '==== REPLACED BY "ServiceModelChecLists".. ==
        '====bOk = gbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg)
        '====If Not bOk Then
        '====   iSqlErrors = iSqlErrors + 1
        '====   Call gbLogMsg(sCreateLogPath, " ** ERROR **  INSERT into Table ModelCheckList failed.." + vbCrLf + sErrorMsg)
        '====   MsgBox " ** ERROR **  INSERT into Table ModelCheckList failed.." + vbCrLf + sErrorMsg, vbCritical
        '====End If

        '-- C o m m i t ---
        '-- C o m m i t ---
        If iSqlErrors > 0 Then
            '== cnnSQL.RollbackTrans()
            '== transaction1.Rollback()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Call gbLogMsg(sCreateLogPath, "=== db build stopped- with ERRORS !!  =====")
            MsgBox("=== db build stopped- with ERRORS !!  =====" & vbCrLf & "=== Rollback executed.. =====", MsgBoxStyle.Critical)
            Call gbLogMsg(sCreateLogPath, "=== Rollback was prev. executed.. =====")
            Exit Function
        Else '--ok--
            Call gbLogMsg(sCreateLogPath, "  ==> Executing Commit..")
            '== cnnSQL.CommitTrans()
            '== transaction1.Commit()
            OleDbTran1.Commit()
            Call gbLogMsg(sCreateLogPath, "  ==> Commit executed..")
        End If

        '-- GRANT ALL can't be in a transaction..--
        '--- do then here.--
        '-- V1.3 = GRANT ALL  NOT needed..--
        '-- V1.3 = GRANT ALL  NOT needed..--

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '--  Read SystemInfo to get date-created and make Security Id..--
        sJT2SecurityId = gsMakeSecurityId(cnnSQL, dateCreated)
        '--  update SystemInfo (Row= SecurityId) to set correct value.--
        sSql = "UPDATE SystemInfo SET SystemValue='" & sJT2SecurityId & "' "
        sSql = sSql & " WHERE (SystemKey='JT2SecurityId'); "
        bOk = gbExecuteCmd(cnnSQL, sSql, L1, sErrorMsg)
        If Not bOk Then
            iSqlErrors = iSqlErrors + 1
            Call gbLogMsg(sCreateLogPath, " ** ERROR **  UPDATE Table SystemInfo (SecurityId) failed.." & vbCrLf & sErrorMsg)
            MsgBox(" ** ERROR **   UPDATE Table SystemInfo (SecurityId) failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        End If
        '--  LOG ALL Details-- DateCreated, DB-name, ABN and SecurityId..
        dDateCreated = dateCreated  '=for caller-
        sSecurityId = sJT2SecurityId  '--ditto-
        Call gbLogMsg(sCreateLogPath, "= = =  JobMatix31 MAIN DB build completed ok..  =====" & vbCrLf & _
                                    " == Date: " & VB6.Format(dateCreated, "dd-mmm-yyyy, ") & _
                                    VB6.Format(dateCreated, "hh:mm:ss") & ",  DBName= " & sSqlDBName & _
                                    ",  ABN= " & sBusinessABN & ", SecurityId=" & sJT2SecurityId & ".." & vbCrLf & _
                                            "= = = = = = = = = = = = = ")
        gbCreateJobsDB = True
        Exit Function

    End Function '--createSql--
    '= = = = = = = = = = = = = = = = =


End Module  '-modCreate-
'= = = = = = = = = = = = =