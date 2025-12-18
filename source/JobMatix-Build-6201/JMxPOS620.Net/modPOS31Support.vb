Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
'== Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports system.data.sqlclient
Imports system.data.OleDb
'== Imports System.ComponentModel

Module modPOS31Support

    '==  grh. JobMatix 3.1.3101.1110 ---  10-Nov-2014 ===
    '==   >>  CREATED for special POS File subs etc.
    '==
    '==  grh. JobMatix 3.1.3103.0128 ---  28-Jan-2015 ===
    '==   >>  GetInvoices--
    '==   >>  3103.203  FIXED GetInvoices--
    '==
    '==  grh. JobMatix 3.1.3107.0731 ---  31-Jul-2015 ===
    '==   >>  POSSettings file now in CommonApplicationData--
    '==   >>   Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)   
    '==
    '==  grh. JobMatix 3.1.3107.0803 ---  03-Aug-2015 ===
    '==   >>  Get the POSSettings dir from clsWinSpecial--
    '==   >>  807- via FileSubs..
    '==
    '==  grh. JobMatix 3.1.3107.08xx ---  03-Aug-2015 ===
    '==   >>  NO.  Don't Get the POSSettings dir from clsWinSpecial-- (See File Subs)-
    '==
    '==  grh. JobMatix 3.1.3107.0820 ---  20-Aug-2015 ===
    '==   >>  Add Function to save PDF in docs Table..-
    '==
    '==  grh =3301.505= 05May2016=
    '==     >>  Dump DataTable into DataGridView..
    '==          (For printing reports)
    '==
    '== = = = =
    '==
    '==     v3.3.3301.622..  22-June-2016= ===
    '==       >> Update Invoice and PAYMENTS Table Schema-  
    '==     BACK to the ORIGINAL schema-  No IP Invoice records, 
    '==       and PaymentDisbursement records restored to duty..
    '==         (For Account customers, part payment with a sale records a separate Payment entry)
    '==
    '==     Credit-Note Credits/Debits are held and tracked in Payments Table Records..
    '==
    '==
    '==     v3.3.3301.1114..  14-Nov-2016= ===
    '==       >> Add Licence Checking (cloning POS licence off Jobmatix)..-
    '==
    '==     v3.3.3301.1211..  11-Dec-2016= ===
    '==       >> Introducing CashDrawer ID's (as per MYOB RM.).- 
    '==       >> Every W/s running POS must have a current CashDrawer ID. ("A".."Z".).- 
    '==       >> Any given CashDrawer ID can be used by any no. of W/s at a any time..- 
    '==       >> CashDrawerId/Computer (w/s) assignments are kept in SystemInfo Table-     
    '==
    '==     v3.3.3303.0109..  09-Jan-2017= ===
    '==       >> "gbCollectCustomerInvoices" Add functionality to collect Refunds
    '==                   And Disbursed Refund values to Outstanding Invoices...- 
    '==
    '==   3307.0209= 09Feb2017 --
    '==    -- gbGetCreditNoteHistory --
    '==    --  Function to collect Credit Note history..
    '==   3402.328-
    '==       Fixed for  creditNotePaymentCredited, refundAsCreditNoteCredited --
    '==
    '==   3403.430-  30Apr2017=
    '==      >> Add stuff to retrieve Layby's   --
    '==      >> 3403.516 Add function "mbDoesTableExist"--
    '==
    '==     3403.1009- 09-Oct-2017-
    '==      -- POS Emails now to use Server File-System to store Invoice PDF's for Email..
    '==            at \\[server]\users\public\JobMatixPOS-EmailQueue\ 
    '==                    (SystemInfo setting is :  "POS_EMAILQUEUE_SHAREPATH"
    '==               NB: (Table "DocArchive" to be DROPPED..)
    '==      --  XML Descriptor file to go with each PDF for Email sending info. 
    '==
    '==     3403.1014- 07/10/11/14-Oct-2017-
    '==      -- MAJOR SHIFT..
    '==           >> All Customers can have credit Notes (Account and non-a/c custs).
    '==           >>  For Sales to Account Cust-  only onAccount invoices will go to Debtors.
    '==                  So account cust can have normal Cash Sales
    '==           >>  On-Account sales can have partial Debtors payment with it..       
    '==        >>  Refunds now the same for Account Custs and non-a/c custs..   
    '==             -- So Account Payments can draw on CreditNotes for payment of invoices..  
    '==             --    and Table "paymentRefundDetails" is dropped. 
    '==        >>  DROP all references to Refunds and Table "paymentRefundDetails"
    '==
    '==     3403.1030 =
    '==        >> Account Payments can have discount given on invoices..  
    '==              -- Discount is saved as PaymentDisbursement row..(trancode="discount")
    '==     3403.1031 =
    '==        >> Saving PDF.. drop test msgbox....  
    '==     3403.1107 =
    '==        >> gbCollectCustomerInvoices- mark Payment REVERSALs....  
    '==
    '==   >> 3411.0106 -- 06-Jan-2018== .
    '==                -- Add CustomerName to XML... 
    '==
    '==
    '==   >> 3431.0621=  21-June-2018=
    '==    -- Cleanup Email XML Files for reserved chars (eg ampersand,<, > )...
    '==
    '==   >> 3519.0115=  15-Jan-2019-=
    '==    -- Cleanup case for payment types.. (EFTPOS -> EftPos)...
    '==
    '==   Updated.- 3519.0219  Started 18-Feb-2019= 
    '==     -- Fixes to Laybys.. 
    '==         - Fix code to add to layby_id collection (needs cstr() as key.)..
    '==
    '==    Updated- 3519.0227=
    '--      -- Added "gbCollectCustomerInvoicesEx" 
    '==                This a CLONE of function  Collect all (Debtors) Customer Invoices, with assoc. payments..
    '--         THis is for Customer Admin, so we can show ALL INVOICES for Customer/..
    '==  
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==    -- (For Customer Admin)-  "gbCollectCustomerInvoicesEx" Include Refunds in customer Info Tab..
    '==
    '==   Updated.- 3519.0414  Started 14-April-2019= 
    '==    -- (For Selling Out Layby)-  " gbCollectCustomerLaybys" Include Discount in Layby Info...
    '==    
    '==  -- NEW VERSION..
    '==    -- 4201.0531.  Fix modPOS31Support (gbGetCreditNoteHistory)-
    '==                                to initialise "decCreditNoteCreditRemaining" to zero.
    '==   NEW revision-
    '==    -- 4201.0707/0708.  Started 05-July-2019-
    '==       -- Added file->Local Preference for LOCAL Re-ordering of payment Details.
    '==       -- Payments and Sales- NOW Use clsPaymentTypes to get PaymentDetails for Grid. .
    '==      
    '==    -- 4201.0717.  17-July-2019-
    '==       -- Add function  "gIntGetRoundingCents" to do rounding calc.
    '==
    '== NEW revision-
    '==    -- 4201.0727.  Started 25-July-2019-
    '==      -- "gbCollectCustomerInvoices"- 
    '==               MUST Round the Invoice and Tax totals to take 3 decimals down to 2.ie Round to whole cents..
    '==           And SAME for "gbGetCreditNoteHistory"-  Each CreditNote item must be rounded as it is collected..
    '==
    '==
    '== NEW Revision-
    '==   == 4219.1214.  10-Dec-2019-  Started 10-Dec-2019-
    '==     -- PO PDF (email) printing.... 
    '==            In "gbSaveDocumentToEmailQueue", 
    '==                  keep local file copy of PDF for SEVEN days after copying to Email Queue.. 
    '==            ALSO- in "gsGetPDF_file_path" (modPrintSubs), Get AppName from gsGetAppname
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==    SHOW Wait Cursor for gbAddColumnToTable
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==Fixes to Build 4257.0707  
    '==
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==
    '== A.  POS System  30-Jul-2020
    '==
    '==      -- Payments Form needs ReversedInvoices to be filtered out..
    '==           ALSO before committing payment, check that Invoices were not changed in the meantime..
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==    (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)
    '==    (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)
    '==    (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '== Fixes to Build 4267.0929  
    '==
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==
    '==  (a) POS- Reversals to Payments that included Saved Credits Notes-   
    '==        -- Credit note amounts are being treated As credits To Customer instead Of showing As debit (reversed).  
    '==          See "gbGetCreditNoteHistory" CreditNote Report, 
    '==               And CreditNote balance On Sale Screen (from clsDebtors ?) ..
    '==                AND  clsDebtors ("mbLoadCreditNoteBalances")..
    '==          IF Payment record is a REVERSAL, then "creditNotePaymentCredited" and "creditNoteAmountDebited" need reversing.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = = = = == 
    '==
    '==
    '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020 +)
    '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
    '==
    '==  FOR Customer Admin..  Speed up loading Invoices Grid using DGV.AddRange...
    '==   -- modPos32Support-- Use new gbCollectCustomerInvoicesEx2 using Shaping to get Invoices/Disbursements..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '==   ComputePosKey now in clsKeygen42 in DLL JMxKeyGen420_OS
    '==     See class clsJMxPOS31..
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '= Public Const K_ABSOLUTE_MAX_USERS_PERMITTED As Short = 32
    '- settings file path..--

    '= 3107.807= Private Const K_POS_SETTINGS_PATH As String = "localPOSSettings.txt"

    Private mImageUserLogo As Image
    Private msDLLVersion As String = ""

    '= 3107.807=  SEE modFileSupport..     Private clsWinInfo1 As New clsWinSpecial

    Private msCurrentCashDrawer As String = ""  '=3411.0402= 02Apr2018=

    '= = = = = = = = = = = = = = = =
    '-===FF->

    '--CleanUpXMLData-

    Private Function msCleanUpXMLData(ByVal strContent As String) As String
        Dim sResult As String

        sResult = Replace(strContent, "&", "&amp;")
        sResult = Replace(sResult, "<", "&lt;")
        sResult = Replace(sResult, ">", "&gt;")
        sResult = Replace(sResult, "'", "&apos;")
        sResult = Replace(sResult, """", "&quot;")
        msCleanUpXMLData = sResult

        '-TEMP
        '== msCleanUpXMLData = strContent  '--temp- No Change.

    End Function '-msCleanUpXMLData-
    '= = = = = = = = = = = = = = = =

    '-- ROUNDING calculation.. (to 05-cents)

    Public Function gIntGetRoundingCents(ByVal decAmountToRound As Decimal) As Integer
        Dim intCents1, intCentsRounding As Integer

        gIntGetRoundingCents = 0
        intCentsRounding = 0
        intCents1 = (decAmountToRound * 100) Mod 10  '--get original cents.
        Select Case intCents1
            Case 1, 6 : intCentsRounding = -1
            Case 2, 7 : intCentsRounding = -2
            Case 3, 8 : intCentsRounding = 2
            Case 4, 9 : intCentsRounding = 1
        End Select

        gIntGetRoundingCents = intCentsRounding

    End Function '-gIntGetRoundingCents-
    '= = = = =  = = = = = = = = = ==== 
    '-===FF->

    '=3402.516= - Check if TableExists..

    Public Function gbDoesTableExist(ByRef cnnSql As OleDbConnection, _
                                      ByVal sTableName As String) As Boolean
        Dim rdr1 As OleDbDataReader
        '= Dim sTableName As String

        gbDoesTableExist = False
        '--  IF table does not exist !!--
        '-- The following example checks for the existence of a specified table
        '--     by verifying that the table has an object ID. 
        Dim sSql As String = "SELECT * FROM sys.objects " & _
                               "WHERE object_id = OBJECT_ID(N'[dbo].[" & sTableName & "]') AND type in (N'U')"
        If gbGetReader(cnnSql, rdr1, sSql) Then  '--check if row exists..-
            If rdr1.HasRows Then '-table exists..-
                gbDoesTableExist = True
            Else  '--doesn't exist.. must create.
                gbDoesTableExist = False
            End If
            rdr1.Close()
        Else  '-get rdr error
            '--  GET error text !!--
            MsgBox("gbDoesTableExist: Error in reading sys.objects table.." & vbCrLf & _
                                  gsGetLastSqlErrorMessage(), MsgBoxStyle.Critical)
        End If  '--get--

    End Function '-- exists table.-
    '= = = = = = = = = = = == = = =  =
    '-===FF->

    '=3403.522= - Add Column if it doesn't Exist..
    '==  Target is new Build 4251..
    '==    SHOW Wait Cursor for gbAddColumnToTable


    Public Function gbAddColumnToTable(ByRef cnnSql As OleDbConnection, _
                                         ByVal sTableName As String, _
                                         ByVal sColumnName As String, _
                                         ByVal sColumnTypeDef As String, _
                                         ByRef bColumnWasAdded As Boolean) As Boolean
        Dim rdr1 As OleDbDataReader
        Dim sSql, sErrorMsg As String
        Dim intAffected As Integer
        gbAddColumnToTable = False
        bColumnWasAdded = False

        sSql = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS " & vbCrLf &
                            " WHERE (TABLE_NAME = '" & sTableName & "') " & _
                                " AND (COLUMN_NAME = '" & sColumnName & "')" & vbCrLf

        If gbGetReader(cnnSql, rdr1, sSql) Then  '--check if row exists..-
            If rdr1.HasRows Then '-column already exists..-
                gbAddColumnToTable = True  '- is ok-
                rdr1.Close()
                Exit Function
            Else  '--doesn't exist.. must create.
                rdr1.Close()
                '-- add column-
                '- include testing again to save doing Transaction..
                sSql = "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " & vbCrLf &
                                  "WHERE TABLE_NAME = '" & sTableName & "' AND COLUMN_NAME = '" & sColumnName & "')" & vbCrLf &
                                  "BEGIN" & vbCrLf & "ALTER TABLE [dbo]." & sTableName & " ADD " & vbCrLf & _
                                    "[" & sColumnName & "] " & sColumnTypeDef & ";" & vbCrLf & "END"
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                If Not gbExecuteCmd(cnnSql, sSql, intAffected, sErrorMsg) Then
                    Cursor.Current = System.Windows.Forms.Cursors.Default
                    MsgBox("gbAddColumnToTable: Error in Adding column.." & vbCrLf & _
                                          gsGetLastSqlErrorMessage(), MsgBoxStyle.Critical)
                Else '- ok-
                    bColumnWasAdded = True
                    gbAddColumnToTable = True
                End If
            End If '-rdr-
        Else  '-get rdr error
            '--  GET error text !!--
            Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("gbAddColumnToTable: Error in reading INFORMATION_SCHEMA.COLUMNS.." & vbCrLf & _
                                  gsGetLastSqlErrorMessage(), MsgBoxStyle.Critical)
        End If  '--get--
        Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function  '-gbAddColumnToTable-
    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->


    '=3301.1211= 12Dec2016=
    '=3301.1211= 12Dec2016=

    Public Function gbSetCurrentCashDrawer(ByVal strNewCashDrawer As String) As Boolean
        msCurrentCashDrawer = strNewCashDrawer
        gbSetCurrentCashDrawer = True
    End Function '-gbSetCurrentCashDrawer-
    '= = = = = = = = = = = = = = = = == = = =

    Public Function gsGetCurrentCashDrawer() As String

        gsGetCurrentCashDrawer = msCurrentCashDrawer
    End Function '-gbSetCurrentCashDrawer-
    '= = = = = = = = = = = = = = = = == = = =

    ''' <summary>
    ''' Converts the DATA File to array of Bytes
    ''' Thanks to:
    '''     http://www.codeproject.com/Articles/31921/Convert-Image-File-to-Bytes-and-Back  
    ''' </summary>
    ''' <param name="ImageFilePath">The path of the image file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function gabConvertDataFiletoBytes(ByVal DataFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(DataFilePath) = True Then
            Throw New ArgumentNullException("Data File Name Cannot be Null or Empty", "DataFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(DataFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(DataFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '= = = = = = = = = = = =
    '-===FF->
    '=3403.430=
    '--  convert recordset to collection of collections..--
    '-----  Optionally add key to each record..

    Public Function gbMakeRecordCollection(ByRef rs1 As DataTable, _
                                             ByRef colRecordList As Collection, _
                                             Optional ByVal strKeyColumn As String = "") As Boolean
        Dim colRecord As Collection
        Dim col1 As Collection
        '==Dim fldx As ADODB.Field
        Dim lngCount As Integer = 0
        Dim intADOType As Integer
        Dim sName, sKey, sSqlType As String

        gbMakeRecordCollection = False
        If Not (rs1 Is Nothing) Then
            colRecordList = New Collection
            If (rs1.Rows.Count > 0) Then   '=Not (rs1.BOF And rs1.EOF) Then '--not empty..-
                '= rs1.MoveFirst()
                For Each datarow1 As DataRow In rs1.Rows
                    lngCount += 1   '-- count records for default key..-
                    colRecord = New Collection
                    For Each column1 As DataColumn In rs1.Columns '== fldx In rs1.Fields
                        sName = column1.ColumnName
                        '--call global to convert Column-datatype to ADO Type and SqlType..--
                        gbConvertDotNetDataType(column1, intADOType, sSqlType)
                        '-- bypass image cols..-
                        If (UCase(sSqlType) <> "IMAGE") And (LCase(sName) <> "productpicture") Then
                            col1 = New Collection
                            col1.Add(sName, "name")
                            If IsDBNull(datarow1.Item(sName)) Then
                                col1.Add("null", "value")
                            Else  '--ok-
                                Try
                                    col1.Add(Trim(datarow1.Item(sName)), "value")
                                Catch ex As Exception
                                    MsgBox("Failed to Convert datatable data for column: " & sName & _
                                                vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                                    Exit Function
                                End Try
                            End If
                            col1.Add(sSqlType, "sqltype")
                            col1.Add(intADOType, "type")
                            col1.Add(column1.MaxLength, "DefinedSize")
                            colRecord.Add(col1, UCase(sName))
                        End If  '--not picture-
                    Next column1  '=fldx
                    '--add this record-
                    If strKeyColumn = "" Then '--no key needed..-
                        colRecordList.Add(colRecord, CStr(lngCount)) '=DEFAULT 1-based..=s/n NOT unique== , KEY=SerialNo-.-
                    Else '--attach key to record.-
                        sKey = Trim(CStr(datarow1.Item(strKeyColumn))) '-- get key value from indic. column.-
                        colRecordList.Add(colRecord, sKey)
                    End If
                Next datarow1
            End If '--empty.-
            gbMakeRecordCollection = True
        End If '-nothing-

    End Function '--make --
    '= = = = = = = = = = = = =
    '-===FF->

    '=3301.505= (from Reports, with fixes.._-
    '-- Dump DataTable into DataGridView..
    '-- Tfr all columns/rows from Table to Grid..

    Public Function gbDumpTableToGrid(ByRef datatable1 As DataTable, _
                                       ByRef dgv1 As DataGridView) As Boolean
        Dim intCount, ix, rowx As Integer
        Dim datacol1 As DataColumn
        Dim row1 As DataRow
        Dim column1 As DataGridViewColumn
        Dim gridRow1 As DataGridViewRow
        Dim s1, s2 As String

        gbDumpTableToGrid = False
        dgv1.DataSource = Nothing
        dgv1.Rows.Clear()
        dgv1.Columns.Clear()
        '-- FIRST- Build Grid Columns and headers..
        Dim cellSample As New DataGridViewTextBoxCell  '-- to make a text-box type column-

        '-- Build Grid columns.-
        For Each datacol1 In datatable1.Columns
            column1 = New DataGridViewColumn
            column1.CellTemplate = cellSample  '-- makes text-box type column-
            column1.HeaderText = datacol1.ColumnName    '
            column1.Name = datacol1.ColumnName    '
            '== column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '= column1.Width = 50
            dgv1.Columns.Add(column1)
        Next datacol1
        '- add data rows from datatable..
        rowx = 0
        Try
            For Each row1 In datatable1.Rows
                '-- Add a row to the grid for each Payment..
                gridRow1 = New DataGridViewRow  '--prepare datagrid report row..
                dgv1.Rows.Add(gridRow1)
                '-- get cell values into grid..--
                For ix = 0 To datatable1.Columns.Count - 1
                    s1 = CStr(row1.Item(ix))
                    s2 = datatable1.Columns(ix).ColumnName  '-get name=
                    dgv1.Rows(rowx).Cells(ix).Value = CStr(row1.Item(ix))
                Next ix
                rowx += 1
            Next row1
            gbDumpTableToGrid = True
        Catch ex As Exception
            MsgBox("Error loading datagrid.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        '-- done-

    End Function '--dump--
    '= = = = = = = = = =  =
    '-===FF->

    '-- return collection of Payment Types..
    '==
    '== NEW revision-
    '==    -- 4201.0707/0708.  Started 05-July-2019-
    '==       -- Add file->Local Preference for LOCAL Re-ordering of payment Details.
    '==       -- Payments and Sales-  Use clsPaymentTypes to get PaymentDetails for Grid. .

    'Public Function gColPaymentTypes() As Collection
    '    Dim colResult As New Collection
    '    Dim colItem As Collection
    '    Dim ix As Integer

    '    '== payment type array has pairs of (Key, Description)
    '    Dim strPaymentTypes() As String = _
    '               {"Cash", "Cash In", _
    '                 "EftPos_Dr", "EftPos Dr (Chq/Saving)", _
    '                 "EftPos_Cr", "EftPos Cr (Credit)", _
    '                  "Bank-Dep", "Bank Deposit", _
    '                  "Cheque", "Cheque (Customer)", _
    '                    "Amex", "Amex Chg Card", _
    '                    "Diners", "Diners Chg Card", _
    '                     "Other_Chg", "Other Chg Card"}

    '    '==  make array into collection of collections..
    '    ix = 0
    '    While ix <= UBound(strPaymentTypes)
    '        colItem = New Collection
    '        colItem.Add(strPaymentTypes(ix), "key")
    '        colItem.Add(strPaymentTypes(ix + 1), "description")
    '        colResult.Add(colItem)
    '        ix += 2
    '    End While

    '    gColPaymentTypes = colResult
    'End Function '-gColPaymentTypes=
    '= = = = = = = = = = = = = = = =
    '-===FF->


    '-[Create]-Return JobMatix Local data DIR.-

    '= 3107.807=  SEE modFileSupport...

    '= 3107.807= Public Function gsJobMatixLocalDataDir() As String

    '= 3107.807=     gsJobMatixLocalDataDir = ""
    '= 3107.807= '-- get programData dir.
    '= 3107.807= '=3107.803= Dim s1 = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)

    '= 3107.807= Dim s1 As String = clsWinInfo1.AppDataDir
    '= 3107.807=    If (Right(s1, 1) <> "\") Then
    '= 3107.807=         s1 &= "\"
    '= 3107.807=    End If
    '= 3107.807= '- make Jobmatix sub Dir-
    '= 3107.807=     If Not Directory.Exists(s1 & "JobMatix31") Then
    '= 3107.807= '--make it-
    '= 3107.807=         Try
    '= 3107.807=            Directory.CreateDirectory(s1 & "JobMatix31")
    '= 3107.807=        Catch ex As Exception
    '= 3107.807=             MsgBox("Failed to create directory: " & vbCrLf & _
    '= 3107.807=                          s1 & "JobMatix31" & vbCrLf & ex.Message)
    '= 3107.807=            Exit Function
    '= 3107.807=        End Try
    '= 3107.807=    End If  '-exists-

    '= 3107.807=    gsJobMatixLocalDataDir = s1 & "JobMatix31"

    '= 3107.807= End Function '-- gsJobMatixLocalDataDir --
    '= = = = =  = = = = == = = = = = = =


    '-[Create]-Return LocalSettingsPath-

    '= 3107.807= Public Function gsLocalSettingsPath() As String

    '= 3107.807= gsLocalSettingsPath = ""

    '= 3107.807= gsLocalSettingsPath = gsJobMatixLocalDataDir() & "\" & K_POS_SETTINGS_PATH

    '= 3107.807= End Function '-- gsLocalSettingsPath --
    '= = = = =  = = = = == = = = = = 


    '--save/get user logo..
    '--save-
    Public Function gbSaveUserLogo(ByRef imageUserLogo As Image) As Boolean

        mImageUserLogo = imageUserLogo
    End Function '-gbSaveUserLogo-
    '= = = = = = = = = = = = = = = =

    Public Function gbGetUserLogo(ByRef imageUserLogo As Image) As Boolean

        imageUserLogo = mImageUserLogo
        gbGetUserLogo = True
    End Function '-gbSaveUserLogo-
    '= = = = = = = = = = = = = = = =

    '--save/get POS DLL version..=

    Public Function gbSaveDllVersion(ByVal sVersion As String) As Boolean

        msDLLVersion = sVersion
    End Function '- mbSaveDllVersion-
    '= = = = = = = = = = = = == = =

    Public Function gbGetDllVersion(ByRef sVersion As String) As Boolean

        sVersion = msDLLVersion
        gbGetDllVersion = True

    End Function '- mbSaveDllVersion-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '== 3301.1211..  11-Dec-2016= == FOR CashDrawer ID's (as per MYOB RM.).- 
    '--   EX JobMatix33.frm-  
    '- Get current DISTINCT logged-in users...
    '--   User can have multiple sessions..--
    '--  Uses "sp_who":
    '--    in sql server 2000-  defaults to PUBLIC role..
    '--    in Sql Server 2005 needs VIEW ANY DATABASE permission
    '--      and "The public role is granted VIEW ANY DATABASE permission."
    '--        SEE:   http://msdn.microsoft.com/en-us/library/ms175892(v=sql.90).aspx   --

    Public Function gbShowLoggedInUsers(ByRef cnnSql As OleDbConnection, _
                                        ByVal strSqlDbName As String, _
                                        ByRef colWhichUsers As Collection, _
                                          ByRef strUserList As String) As Boolean

        Dim col1 As Collection
        Dim colAllProcesses As Collection
        Dim sLogin, sHost, sItem As String
        Dim sMsg, sDistinctUsers As String

        gbShowLoggedInUsers = False
        sDistinctUsers = ";"
        strUserList = ""
        '== ToolTip1.SetToolTip(labLoggedInUsers, "")
        If Not gbWhoUsing(cnnSql, strSqlDbName, colAllProcesses) Then
            MsgBox("Failed to get user list.." & vbCrLf & _
                    "Sql cmd was 'exec sp_who'..  " & vbCrLf & vbCrLf & _
                    gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
        Else '--ok--
            sMsg = "Current users are: " & vbCrLf & vbCrLf
            colWhichUsers = New Collection
            If (colAllProcesses.Count > 0) Then
                For Each col1 In colAllProcesses
                    sLogin = Trim(col1.Item("LOGINAME"))
                    sHost = Trim(col1.Item("HOSTNAME"))
                    sItem = LCase(sHost & "!" & sLogin)
                    If Not (InStr(sDistinctUsers, sItem & ";") > 0) Then  '--new-
                        sDistinctUsers = sDistinctUsers & LCase(sItem) & ";"
                        sMsg = sMsg & sLogin & " on: " & sHost & ".." & vbCrLf
                        colWhichUsers.Add(col1)
                    End If
                Next col1 '--col1-
                strUserList = sMsg
            Else
                '== labLoggedInUsers.Text = vbCrLf & "No User.."
            End If  '--count.-
            Application.DoEvents()
            gbShowLoggedInUsers = True
        End If  '--who--
    End Function  '--show users.-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '--  Collect all (Debtors) Customer Invoices, with assoc. payments..
    '==  grh =3301.601= 01June2016=
    '==    >>  get payments info from dbo.invoice table..
    '-==           ("accountPayment"transaction type)..

    '==     v3.3.3301.622..  22-June-2016= ===
    '==       >> Update Invoice and PAYMENTS Table Schema-  
    '==     BACK to the ORIGINAL schema-  No IP Invoice records, 
    '==       and PaymentDisbursement records restored to duty..
    '==         (For Account customers, part payment with a sale records a separate Payment entry)
    '==
    '==     v3.3.3303.0109..  09-Jan-2017= ===
    '==       >> See below- "gbCollectCustomerRefunds"
    '==             Adds separate functionality to collect Refunds
    '==                   IGNORE Refunds in this function. (collecr DRs only)..- 
    '==     3403.1107 =
    '==        >> gbCollectCustomerInvoices- mark Payment REVERSALs....  
    '==
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==    (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)


    Public Function gbCollectCustomerInvoices(ByRef cnnSql As OleDbConnection, _
                                              ByVal intCustomer_id As Integer, _
                                               ByVal bOutstandingInvoicesOnly As Boolean, _
                                               ByVal dateCutoff As Date, _
                                                ByRef colInvoices As Collection, _
                                                ByRef decTotalInvoices As Decimal, _
                                                ByRef decTotalOutstanding As Decimal, _
                                                Optional ByVal intClosedDaysToShow As Integer = 0, _
                                                Optional ByVal bIsTransaction As Boolean = False, _
                                                Optional ByRef oledbTransaction1 As OleDbTransaction = Nothing) As Boolean
        Dim s1, sSql, sPayList As String
        Dim int1, intHeight, intInvoice_id As Integer
        '== Dim dtInvoices, dtInvPayments, dtDisbursements As DataTable
        Dim dtInvoices, dtDisbursements As DataTable
        '== Dim colAccountPayments As Collection
        Dim colDisbursements As Collection
        Dim colInvoice1 As Collection
        Dim col1, col2, colPayList As Collection
        '= Dim bIsRefund As Boolean
        Dim decInvoiceTotal, decTotalTax, decPaymentAmount As Decimal
        Dim decPaymentTotal, decAmountOutstanding As Decimal
        Dim dateClosedInvoicesToShow As Date = DateAdd(DateInterval.Day, -intClosedDaysToShow, Today)
        Dim dateInvoice As Date

        gbCollectCustomerInvoices = False
        sSql = "SELECT * FROM dbo.Invoice WHERE (Customer_id=" & CStr(intCustomer_id) & ")"
        '==3303.0110= sSql &= " AND (transactionType IN ('sale', 'refund')) "  '--don't pick up account payments.
        sSql &= " AND (transactionType = 'sale') "  '--don't pick up REFUNDS.
        '=3403.1014= 
        sSql &= " AND (isOnAccount = 1) "  '--don't pick up Cash Sales.

        If IsDate(dateCutoff) Then
            sSql &= " AND (invoice_date <=  '" & Format(dateCutoff, "dd-MMM-yyyy 23:59") & "')"
        End If
        sSql &= " ORDER BY invoice_id;"

        '==  Target build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==  (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)
        Dim bOk As Boolean
        If bIsTransaction Then
            bOk = gbGetDataTableEx(cnnSql, dtInvoices, sSql, oledbTransaction1)
        Else  '-not in tranaction-
            bOk = gbGetDataTable(cnnSql, dtInvoices, sSql)
        End If  '--transaction.
        '== END Target build 4259 -- (Started 17-Jul-2020)

        If Not bOk Then  '=4259=  gbGetDataTable(cnnSql, dtInvoices, sSql) Then
            MsgBox("Error in getting Invoices recordset for a/c debits: " & vbCrLf & _
                                            gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
             Exit Function
        End If '-get-
        colInvoices = New Collection
        decTotalInvoices = 0
        decTotalOutstanding = 0

        If (dtInvoices Is Nothing) OrElse (dtInvoices.Rows.Count <= 0) Then
            '== MsgBox("No invoice found for Customer " & intCustomer_id & "..", MsgBoxStyle.Exclamation)
            '=Me.Close()
            gbCollectCustomerInvoices = True  '- empty is ok-
            Exit Function
        End If

        '--ok have invoices..

        '==  grh =3301.601= 01June2016=
        '==    >>  get payments info from dbo.invoice table..
        '-==           ("accountPayment" transaction type)..

        '==3301.623- Now back to Disbursements..

        '--  get all PaymentDisbursements (JOIN Payments) for Customer..
        '-- get all Disbursement records for the same payment set..
        '==     3403.1030 =
        '==        >> Account Payments can have discount given on invoices..  
        '==              -- Discount is saved as PaymentDisbursement row..(trancode="discount")
        '==     3403.1107 =
        '==        >> gbCollectCustomerInvoices- mark Payment REVERSALs....  

        sSql = "SELECT payments.payment_id,  payments.payment_date,  payments.isReversal, "
        sSql &= "   payments.nettAmountCredited,"
        sSql &= "    PD.invoice_id, PD.amount, "
        sSql &= "    PD.tranCode as disbTranCode, PD.sourceOfFunds "
        sSql &= "  FROM PaymentDisbursements PD  "
        sSql &= "   JOIN dbo.payments on (PD.payment_id=payments.payment_id) "
        '== sSql &= "     JOIN Customer on (payments.customer_id=Customer.customer_id) "
        sSql &= "   WHERE (payments.Customer_id=" & CStr(intCustomer_id) & ")"
        sSql &= " ORDER BY PD.invoice_id;"

        '- get (ALL) dtDisbursements datatable.

        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==  (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)
        '=Dim bOk As Boolean
        If bIsTransaction Then
            bOk = gbGetDataTableEx(cnnSql, dtDisbursements, sSql, oledbTransaction1)
        Else  '-not in tranaction-
            bOk = gbGetDataTable(cnnSql, dtDisbursements, sSql)
        End If  '--transaction.
        '== END Target build 4259 -- (Started 17-Jul-2020)

        If Not bOk Then  '= 4259=  gbGetDataTable(cnnSql, dtDisbursements, sSql) Then
            MsgBox("Error in getting recordset for PaymentDisburse. table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        Else '-ok-
            colDisbursements = New Collection
            If Not (dtDisbursements Is Nothing) AndAlso (dtDisbursements.Rows.Count > 0) Then
                '-- collect amounts for each invoice we found above..
                '-- For each INVOICE we collected, collect all disbursements..
                For Each datarow1 As DataRow In dtInvoices.Rows
                    intInvoice_id = datarow1.Item("invoice_id")
                    colPayList = New Collection
                    For Each dtrowDisb As DataRow In dtDisbursements.Rows
                        col1 = New Collection
                        If dtrowDisb.Item("invoice_id") = intInvoice_id Then '-this payment-
                            col1.Add(dtrowDisb.Item("invoice_id"), "invoice_id")
                            col1.Add(dtrowDisb.Item("payment_id"), "payment_id")
                            col1.Add(dtrowDisb.Item("payment_date"), "date")
                            col1.Add(dtrowDisb.Item("nettAmountCredited"), "paymentTotalCredited")
                            col1.Add(dtrowDisb.Item("amount"), "amount")
                            col1.Add(dtrowDisb.Item("disbTranCode"), "disbTranCode")
                            col1.Add(dtrowDisb.Item("sourceOfFunds"), "sourceOfFunds")
                            col1.Add(dtrowDisb.Item("isReversal"), "isReversal")
                            colPayList.Add(col1)
                        End If
                    Next dtrowDisb
                    colDisbursements.Add(colPayList, CStr(intInvoice_id)) '-collect disbursements this Payment..
                Next datarow1  '--invoices-
            End If '-nothing-
        End If '-get Disbursements-

        '-- Apply disb. amounts to invoices..
        '--   Collect invoices not fully paid..
        '-- Load RESULT collection with all [Outstanding] Invoices for this customer --
        Dim intCount As Integer = 0
        Dim bIsReversal As Boolean
        Dim sDisbTrancode As String
        '==Dim gridRow1 As DataGridViewRow
        For Each datarow1 As DataRow In dtInvoices.Rows
            decPaymentTotal = 0
            intInvoice_id = datarow1.Item("invoice_id")
            '= bIsRefund = (UCase(datarow1.Item("transactiontype")) = "REFUND")
            decInvoiceTotal = CDec(datarow1.Item("total_inc"))

            '= For 4201.0727=  
            '==  MUST Round the Invoice total to take 3 decimals down to 2.
            decInvoiceTotal = Math.Round(decInvoiceTotal, 2, MidpointRounding.AwayFromZero)

            '-Same for Tax..
            decTotalTax = datarow1.Item("total_tax")
            decTotalTax = Math.Round(decTotalTax, 2, MidpointRounding.AwayFromZero)
            '=  END of  For 4201.0727=  

            '=If bIsRefund Then
            '=    decInvoiceTotal = -decInvoiceTotal
            '=End If
            If (colDisbursements.Count > 0) AndAlso colDisbursements.Contains(CStr(intInvoice_id)) Then
                colPayList = colDisbursements.Item(CStr(intInvoice_id))
            Else
                colPayList = New Collection  '--empty- No payments for this invoice.
            End If
            'If (colAccountPayments.Count > 0) AndAlso colAccountPayments.Contains(CStr(intInvoice_id)) Then
            '    colPayList = colAccountPayments.Item(CStr(intInvoice_id))
            'Else
            '    colPayList = New Collection  '--empty- No payments for this invoice.
            'End If
            sPayList = ""
            If (colPayList.Count > 0) Then
                '--LIST and sum payments this invoice.-
                '--  NB: Disbursements from REFUNDS (NOW is DISCOUNTS) will also appear in here..
                For Each col1 In colPayList
                    bIsReversal = CBool(col1.Item("isReversal"))
                    decPaymentAmount = CDec(col1.Item("amount"))
                    sDisbTrancode = col1.Item("disbTranCode")
                    '= decPaymentTotal += IIf(bIsRefund, -decPaymentAmount, decPaymentAmount)
                    decPaymentTotal += IIf(bIsReversal, -decPaymentAmount, decPaymentAmount)
                    If (sPayList <> "") Then sPayList &= vbCrLf
                    s1 = VB.Left(sDisbTrancode, 4)
                    s1 &= IIf(bIsReversal, "(Revrsl): ", ": ")
                    s1 &= RSet(FormatCurrency(decPaymentAmount, 2), 9)
                    sPayList &= Format(col1.Item("date"), "dd-MMM-yy ") & s1
                Next col1 '-payment fraction for this invoice..-
            End If '-count-
            '--compute outstanding, if any.-
            decAmountOutstanding = decInvoiceTotal - decPaymentTotal

            dateInvoice = CDate(datarow1.Item("invoice_date"))
            '--load Collection with [outstanding] or [all] invoices-
            If (decAmountOutstanding <> 0) Or (Not bOutstandingInvoicesOnly) Or _
                 (bOutstandingInvoicesOnly AndAlso (intClosedDaysToShow > 0) AndAlso _
                                (dateInvoice >= dateClosedInvoicesToShow)) Then  '-he must pay. or show all..-
                colInvoice1 = New Collection
                colInvoice1.Add(intInvoice_id, "invoice_id")
                colInvoice1.Add(datarow1.Item("invoice_date"), "invoice_date")
                colInvoice1.Add(datarow1.Item("transactionType"), "transactionType")
                colInvoice1.Add(decInvoiceTotal, "invoiceTotal")
                '= colInvoice1.Add(datarow1.Item("total_tax"), "total_tax")
                '==4201.0727= must be the rounded version..
                colInvoice1.Add(decTotalTax, "total_tax")

                colInvoice1.Add(colPayList, "invoicePayments")
                colInvoice1.Add(sPayList, "invoicePaymentList")
                colInvoice1.Add(decPaymentTotal, "paymentTotalThisInvoice")
                colInvoice1.Add(decAmountOutstanding, "amountOutstanding")
                decTotalInvoices += decInvoiceTotal
                decTotalOutstanding += decAmountOutstanding

                colInvoices.Add(colInvoice1, intInvoice_id)
                intCount += 1  '-count grid rows..-
            End If '-outstanding-
        Next datarow1  '--invoices-
        gbCollectCustomerInvoices = True

    End Function  '- gbCollectCustomerInvoices-
    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '= 3519.0227=

    '-- CLONE of function  Collect all (Debtors) Customer Invoices, with assoc. payments..
    '--  THis is for Customer Admin, so we can show ALL INVOICES for Customer/..
    '--  THis is for Customer Admin, so we can show ALL INVOICES for Customer/..
    '--  THis is for Customer Admin, so we can show ALL INVOICES for Customer/..
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==    --  "gbCollectCustomerInvoicesEx" Include Refunds in customer Info Tab..

    '== 4284.1121--  This is now OBSOLETE..
    '== 4284.1121--  This is now OBSOLETE..
    '== 4284.1121--  This is now OBSOLETE..


    Public Function gbCollectCustomerInvoicesEx_OBSOLETE(ByRef cnnSql As OleDbConnection,
                                           ByVal intCustomer_id As Integer,
                                            ByVal bOutstandingInvoicesOnlyInput As Boolean,
                                            ByVal dateCutoff As Date,
                                             ByRef colInvoices As Collection,
                                             ByRef decTotalInvoices As Decimal,
                                             ByRef decTotalOutstanding As Decimal,
                                             Optional ByVal intClosedDaysToShow As Integer = 0) As Boolean


        Dim s1, sSql, sPayList As String
        Dim int1, intHeight, intInvoice_id As Integer
        '== Dim dtInvoices, dtInvPayments, dtDisbursements As DataTable
        Dim dtInvoices, dtDisbursements As DataTable
        '== Dim colAccountPayments As Collection
        Dim colDisbursements As Collection
        Dim colInvoice1 As Collection
        Dim col1, col2, colPayList As Collection
        '= Dim bIsRefund As Boolean
        Dim decInvoiceTotal, decTotalTax, decPaymentAmount As Decimal
        Dim decPaymentTotal, decAmountOutstanding As Decimal
        Dim dateClosedInvoicesToShow As Date = DateAdd(DateInterval.Day, -intClosedDaysToShow, Today)
        Dim dateInvoice As Date

        '=3519.0227--
        '-- Now want ALL SALES --
        Dim bOutstandingInvoicesOnly As Boolean = False

        gbCollectCustomerInvoicesEx_OBSOLETE = False
        sSql = "SELECT * FROM dbo.Invoice WHERE (Customer_id=" & CStr(intCustomer_id) & ")"
        '==3303.0110= 
        '=3519.0317- NOW include refunds..
        sSql &= " AND (transactionType IN ('sale', 'refund')) "  '--noe want refunds also
        '= sSql &= " AND (transactionType = 'sale') "  '--don't pick up REFUNDS.

        '=3519.0227--
        '-- Now want ALL SALES --
        '==  sSql &= " AND (isOnAccount = 1) "  '--don't pick up Cash Sales.

        If IsDate(dateCutoff) Then
            sSql &= " AND (invoice_date <=  '" & Format(dateCutoff, "dd-MMM-yyyy 23:59") & "')"
        End If
        sSql &= " ORDER BY invoice_id;"
        If Not gbGetDataTable(cnnSql, dtInvoices, sSql) Then
            MsgBox("Error in getting Invoices recordset for a/c debits: " & vbCrLf &
                                            gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '== Me.Close()
            Exit Function
        End If '-get-
        colInvoices = New Collection
        decTotalInvoices = 0
        decTotalOutstanding = 0

        If (dtInvoices Is Nothing) OrElse (dtInvoices.Rows.Count <= 0) Then
            '== MsgBox("No invoice found for Customer " & intCustomer_id & "..", MsgBoxStyle.Exclamation)
            '=Me.Close()
            gbCollectCustomerInvoicesEx_OBSOLETE = True  '- empty is ok-
            Exit Function
        End If

        '--ok have invoices..
        colDisbursements = New Collection

        '==  grh =3301.601= 01June2016=
        '==    >>  get payments info from dbo.invoice table..
        '-==           ("accountPayment" transaction type)..

        '==3301.623- Now back to Disbursements..

        '--  get all PaymentDisbursements (JOIN Payments) for Customer..
        '-- get all Disbursement records for the same payment set..
        '==     3403.1030 =
        '==        >> Account Payments can have discount given on invoices..  
        '==              -- Discount is saved as PaymentDisbursement row..(trancode="discount")
        '==     3403.1107 =
        '==        >> gbCollectCustomerInvoices- mark Payment REVERSALs....  

        sSql = "SELECT payments.payment_id,  payments.payment_date,  payments.isReversal, "
        sSql &= "   payments.nettAmountCredited,"
        sSql &= "    PD.invoice_id, PD.amount, "
        sSql &= "    PD.tranCode as disbTranCode, PD.sourceOfFunds "
        sSql &= "  FROM PaymentDisbursements PD  "
        sSql &= "   JOIN dbo.payments on (PD.payment_id=payments.payment_id) "
        '== sSql &= "     JOIN Customer on (payments.customer_id=Customer.customer_id) "
        sSql &= "   WHERE (payments.Customer_id=" & CStr(intCustomer_id) & ")"
        sSql &= " ORDER BY PD.invoice_id;"

        '- get (ALL) dtDisbursements datatable.
        If Not gbGetDataTable(cnnSql, dtDisbursements, sSql) Then
            MsgBox("Error in getting recordset for PaymentDisburse. table: " & vbCrLf &
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        Else '-ok-
            '= colDisbursements = New Collection
            If Not (dtDisbursements Is Nothing) AndAlso (dtDisbursements.Rows.Count > 0) Then
                '-- collect amounts for each invoice we found above..
                '-- For each INVOICE we collected, collect all disbursements..
                For Each datarow1 As DataRow In dtInvoices.Rows
                    intInvoice_id = datarow1.Item("invoice_id")
                    colPayList = New Collection
                    For Each dtrowDisb As DataRow In dtDisbursements.Rows
                        col1 = New Collection
                        If dtrowDisb.Item("invoice_id") = intInvoice_id Then '-this payment-
                            col1.Add(dtrowDisb.Item("invoice_id"), "invoice_id")
                            col1.Add(dtrowDisb.Item("payment_id"), "payment_id")
                            col1.Add(dtrowDisb.Item("payment_date"), "date")
                            col1.Add(dtrowDisb.Item("nettAmountCredited"), "paymentTotalCredited")
                            col1.Add(dtrowDisb.Item("amount"), "amount")
                            col1.Add(dtrowDisb.Item("disbTranCode"), "disbTranCode")
                            col1.Add(dtrowDisb.Item("sourceOfFunds"), "sourceOfFunds")
                            col1.Add(dtrowDisb.Item("isReversal"), "isReversal")
                            colPayList.Add(col1)
                        End If
                    Next dtrowDisb
                    colDisbursements.Add(colPayList, CStr(intInvoice_id)) '-collect disbursements this Payment..
                Next datarow1  '--invoices-
            End If '-nothing-
        End If '-get Disbursements-

        '-- Apply disb. amounts to invoices..
        '--   Collect invoices (not fully paid.. ??)
        Dim sTransactionType As String
        '-- Load RESULT collection with all [Outstanding] Invoices for this customer --
        Dim intCount As Integer = 0
        Dim bIsReversal, bIsRefund As Boolean
        Dim sDisbTrancode As String
        '==Dim gridRow1 As DataGridViewRow
        For Each datarow1 As DataRow In dtInvoices.Rows
            decPaymentTotal = 0
            intInvoice_id = datarow1.Item("invoice_id")
            '=3519.0317=
            sTransactionType = datarow1.Item("transactiontype")
            bIsRefund = (UCase(datarow1.Item("transactiontype")) = "REFUND")

            decInvoiceTotal = CDec(datarow1.Item("total_inc"))

            '= For 4201.0727=  
            '==  MUST Round the Invoice total to take 3 decimals down to 2.
            decInvoiceTotal = Math.Round(decInvoiceTotal, 2, MidpointRounding.AwayFromZero)

            '-Same for Tax..
            decTotalTax = datarow1.Item("total_tax")
            decTotalTax = Math.Round(decTotalTax, 2, MidpointRounding.AwayFromZero)
            '=  END of  For 4201.0727=  

            '=If bIsRefund Then
            '=    decInvoiceTotal = -decInvoiceTotal
            '=End If
            If (colDisbursements.Count > 0) AndAlso colDisbursements.Contains(CStr(intInvoice_id)) Then
                colPayList = colDisbursements.Item(CStr(intInvoice_id))
            Else
                colPayList = New Collection  '--empty- No payments for this invoice.
            End If
            'If (colAccountPayments.Count > 0) AndAlso colAccountPayments.Contains(CStr(intInvoice_id)) Then
            '    colPayList = colAccountPayments.Item(CStr(intInvoice_id))
            'Else
            '    colPayList = New Collection  '--empty- No payments for this invoice.
            'End If
            sPayList = ""
            If (colPayList.Count > 0) Then
                '--LIST and sum payments this invoice.-
                '--  NB: Disbursements from REFUNDS (NOW is DISCOUNTS) will also appear in here..
                For Each col1 In colPayList
                    bIsReversal = CBool(col1.Item("isReversal"))
                    decPaymentAmount = CDec(col1.Item("amount"))
                    sDisbTrancode = col1.Item("disbTranCode")
                    '= decPaymentTotal += IIf(bIsRefund, -decPaymentAmount, decPaymentAmount)
                    decPaymentTotal += IIf(bIsReversal, -decPaymentAmount, decPaymentAmount)
                    If (sPayList <> "") Then sPayList &= vbCrLf
                    s1 = VB.Left(sDisbTrancode, 4)
                    s1 &= IIf(bIsReversal, "(Revrsl): ", ": ")
                    s1 &= RSet(FormatCurrency(decPaymentAmount, 2), 9)
                    sPayList &= Format(col1.Item("date"), "dd-MMM-yy ") & s1
                Next col1 '-payment fraction for this invoice..-
            End If '-count-
            '--compute outstanding, if any.-
            decAmountOutstanding = decInvoiceTotal - decPaymentTotal

            dateInvoice = CDate(datarow1.Item("invoice_date"))
            '--load Collection with [outstanding] or [all] invoices-
            If (decAmountOutstanding <> 0) Or (Not bOutstandingInvoicesOnly) Or
                 (bOutstandingInvoicesOnly AndAlso (intClosedDaysToShow > 0) AndAlso
                                (dateInvoice >= dateClosedInvoicesToShow)) Then  '-he must pay. or show all..-
                colInvoice1 = New Collection
                colInvoice1.Add(intInvoice_id, "invoice_id")
                '=3519.0227-  add isOnAccount..
                colInvoice1.Add(datarow1.Item("isOnAccount"), "isOnAccount")
                colInvoice1.Add(datarow1.Item("invoice_date"), "invoice_date")
                colInvoice1.Add(datarow1.Item("transactionType"), "transactionType")
                colInvoice1.Add(decInvoiceTotal, "invoiceTotal")
                '= colInvoice1.Add(datarow1.Item("total_tax"), "total_tax")
                '==4201.0727= must be the rounded version..
                colInvoice1.Add(decTotalTax, "total_tax")

                colInvoice1.Add(colPayList, "invoicePayments")
                colInvoice1.Add(sPayList, "invoicePaymentList")
                colInvoice1.Add(decPaymentTotal, "paymentTotalThisInvoice")
                colInvoice1.Add(decAmountOutstanding, "amountOutstanding")
                decTotalInvoices += decInvoiceTotal
                decTotalOutstanding += decAmountOutstanding

                colInvoices.Add(colInvoice1, intInvoice_id)
                intCount += 1  '-count grid rows..-
            End If '-outstanding-
        Next datarow1  '--invoices-
        gbCollectCustomerInvoicesEx_OBSOLETE = True

    End Function '-gbCollectCustomerInvoicesEx
    '= = = = = = = = = = = = = = = = = = = = = = = =  =
    '-===FF->

    '==   Target-New-Build-4284-EXTRA-EXTRA --  (20-Nov-2020)
    '==-- New Version is gbCollectCustomerInvoicesExShaped --
    '--  To use Shape Sql to speed it up..

    Public Function gbCollectCustomerInvoicesEx2(ByRef cnnSql As OleDbConnection,
                                                      ByVal intCustomer_id As Integer,
                                                         ByVal bOutstandingInvoicesOnlyInput As Boolean,
                                                         ByVal dateCutoff As Date,
                                                          ByRef colInvoices As Collection,
                                                           ByRef decTotalInvoices As Decimal,
                                                           ByRef decTotalOutstanding As Decimal,
                                                          Optional ByVal intClosedDaysToShow As Integer = 0) As Boolean


        Dim s1, sSql, sPayList As String
        Dim int1, intHeight, intInvoice_id As Integer
        Dim colInvoice1 As Collection
        Dim col1, col2, colPayList As Collection
        Dim decInvoiceTotal, decTotalTax, decPaymentAmount As Decimal
        Dim decPaymentTotal, decAmountOutstanding As Decimal
        Dim dateClosedInvoicesToShow As Date = DateAdd(DateInterval.Day, -intClosedDaysToShow, Today)
        Dim dateInvoice As Date

        '=3519.0227--
        '-- Now want ALL SALES --
        Dim bOutstandingInvoicesOnly As Boolean = False

        gbCollectCustomerInvoicesEx2 = False

        '== Build-4284-EXTRA-EXTRA
        '--  Use Shape to collect payments for each Invoice..
        Dim sServer, sConnect, sErrors As String
        Dim sShapeSql, sSqlDbName As String
        Dim cnnShape As OleDbConnection
        Dim intRecordsAffected As Integer
        Dim sWhereCondition As String

        sServer = cnnSql.DataSource
        sSqlDbName = cnnSql.Database
        sWhereCondition = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        cnnShape = New OleDbConnection '=  ADODB.Connection
        sConnect = "Provider=MSDataShape; Server=" & sServer & "; Trusted_Connection=true; Integrated Security=SSPI; "
        sConnect = sConnect & "; Data Provider=SQLOLEDB; Data Source=" & sServer & "; "
        '=== sConnect = sConnect + "; Password=" + msSqlPwd + "; User ID=" + msSqlUid
        If gbConnectSql(cnnShape, sConnect) Then
            '-ok..--
        Else
            MsgBox("Login to sql SHAPE dataSource has failed." & vbCrLf, MsgBoxStyle.Exclamation)
            Exit Function
        End If '--connected-
        If Not gbExecuteCmd(cnnShape, "USE " & sSqlDbName & vbCrLf, intRecordsAffected, sErrors) Then
            MsgBox(vbCrLf & "Failed USE for SHAPE connection to DATABASE: '" &
                                            sSqlDbName & "'.." & vbCrLf & sErrors, MsgBoxStyle.Critical)
            Exit Function
        End If '--use-
        '= mCnnShape.CommandTimeout = 10 '-- 10 sec cmd timeout..-
        '= mCnnShape.CursorLocation = ADODB.CursorLocationEnum.adUseClient

        sWhereCondition = "WHERE (invoice.Customer_id=" & CStr(intCustomer_id) & ")"
        sWhereCondition &= " AND (transactionType IN ('sale', 'refund')) "  '--we want refunds also
        If IsDate(dateCutoff) Then
            sWhereCondition &= " AND (invoice_date <=  '" & Format(dateCutoff, "dd-MMM-yyyy 23:59") & "') "
        End If

        '= System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '-- YES Shaping..

        sShapeSql = " SHAPE {"
        sShapeSql &= " SELECT  *,  Invoice.invoice_id AS invoice_id2  "
        sShapeSql &= "  FROM dbo.Invoice "
        sShapeSql &= sWhereCondition
        sShapeSql &= " ORDER BY invoice.invoice_id DESC "
        sShapeSql &= "  } "
        '--  Get child r-sets for Payment Details...
        sShapeSql &= " APPEND ({ SELECT "
        sShapeSql &= "  PD.invoice_id, PD.amount, "
        sShapeSql &= "  PD.tranCode as disbTranCode, PD.sourceOfFunds, "
        '=sShapeSql &= "   payments.invoice_id AS paym_invoice_id, "
        sShapeSql &= "   payments.payment_id, payment_date, payments.transactionType AS payment_tranType, "
        sShapeSql &= "   payments.isReversal, "
        sShapeSql &= "   payments.nettAmountCredited  "
        sShapeSql &= "  FROM dbo.PaymentDisbursements PD "
        sShapeSql &= "        Left outer JOIN [dbo].[Payments]  on (payments.payment_id =PD.payment_id)} "
        sShapeSql &= "   RELATE invoice_id2 to invoice_id)  "
        sShapeSql &= "      AS rsPaymentDisbursements  "
        '--  end of child r-sets for Payment Details...

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '-- start retrieval-
        Dim cmd1 As OleDbCommand
        Dim rdrInvoices, rdrPayDetails As OleDbDataReader
        '= Dim sCustomerBarcode, sCustomerName As String
        Dim sInvoiceId As String
        Dim colShapeInvoice, colShapeInvoices As Collection
        Dim sWaitMsg As String = "Pls Wait. Collecting the Sales Invoices.." & vbCrLf &
                         " This might take a minute-  "
        Dim ix As Integer

        Try
            cmd1 = New OleDbCommand(sShapeSql, cnnShape)
            rdrInvoices = cmd1.ExecuteReader
        Catch ex As Exception
            '=Call mWaitFormOff()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Error getting Invoices/Disbursements recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            Exit Function
        End Try
        '-- check it all-

        colShapeInvoices = New Collection
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '= Collect all invoices..
        Try  '--main try Invoices-
            If rdrInvoices.HasRows Then
                Dim intRowx As Integer = 0
                '--for dropping duplicated due to multiple payments.
                Dim intLastInvoiceId As Integer = -1  '--for dropping duplicated due to multiple payments.

                Do While rdrInvoices.Read
                    '-- get Invoices..
                    ix = rdrInvoices.Item("invoice_id")
                    intLastInvoiceId = ix '- new invoice row..

                    '-- save docket info into a sub-collection..
                    '--     for this docket
                    colShapeInvoice = New Collection
                    sInvoiceId = CStr(ix)  '- for collection key.
                    colShapeInvoice.Add(ix, "Id")

                    '-TESTING-
                    '= MsgBox("Got Invoice # " & sInvoiceId, MsgBoxStyle.Information)
                    colShapeInvoice.Add(rdrInvoices.Item("invoice_id"), "invoice_id")

                    colShapeInvoice.Add(UCase(rdrInvoices.Item("transactionType")), "transactiontype")
                    colShapeInvoice.Add(rdrInvoices.Item("subtotal_inc"), "subtotal_inc")
                    colShapeInvoice.Add(rdrInvoices.Item("subtotal_ex_non_taxable"), "subtotal_ex_non_taxable")
                    colShapeInvoice.Add(rdrInvoices.Item("subtotal_ex_taxable"), "subtotal_ex_taxable")
                    colShapeInvoice.Add(rdrInvoices.Item("subtotal_tax"), "subtotal_tax")

                    colShapeInvoice.Add(rdrInvoices.Item("total_inc"), "total_inc")
                    colShapeInvoice.Add(rdrInvoices.Item("total_tax"), "total_tax")
                    colShapeInvoice.Add(rdrInvoices.Item("invoice_date"), "invoice_date")  '-save Date this invoice.
                    colShapeInvoice.Add(rdrInvoices.Item("cashDrawer"), "cashDrawer")  '-save till_id.
                    '=3301.817= col1.Add(row1.Item("amtCharged"), "amtCharged")  '-save amt Charged this invoice.
                    colShapeInvoice.Add(rdrInvoices.Item("isOnAccount"), "isOnAccount")  '-save whether Charged this invoice.
                    colShapeInvoice.Add(rdrInvoices.Item("discount_nett"), "discount_nett")  '-save discount this invoice.
                    colShapeInvoice.Add(rdrInvoices.Item("discount_tax"), "discount_tax")
                    '=colInvoice.Add(dataRow1.Item("cashout"), "cashout")
                    colShapeInvoice.Add(rdrInvoices.Item("rounding"), "rounding")

                    '-- Capture payment Disbursements as well.
                    colPayList = New Collection
                    If TypeOf rdrInvoices.Item("rsPaymentDisbursements") Is IDataReader Then
                        rdrPayDetails = rdrInvoices.Item("rsPaymentDisbursements")
                        If rdrPayDetails.HasRows Then
                            Dim colDisb As Collection
                            Do While rdrPayDetails.Read
                                colDisb = New Collection
                                colDisb.Add(rdrPayDetails.Item("payment_id"), "payment_id")
                                '-paymentstuff-  payment_date
                                colDisb.Add(rdrPayDetails.Item("payment_date"), "date")
                                colDisb.Add(rdrPayDetails.Item("isReversal"), "isReversal")
                                colDisb.Add(rdrPayDetails.Item("invoice_id"), "invoice_id")
                                colDisb.Add(rdrPayDetails.Item("disbTranCode"), "disbTranCode")
                                colDisb.Add(rdrPayDetails.Item("sourceOfFunds"), "sourceOfFunds")
                                colDisb.Add(rdrPayDetails.Item("amount"), "amount")
                                '=nettAmountCredited- From Payment record.
                                colDisb.Add(rdrPayDetails.Item("nettAmountCredited"), "paymentTotalCredited")
                                colPayList.Add(colDisb)
                            Loop  '-While rdrPayDetails.Read-
                        End If '--has rows..
                        rdrPayDetails.Close()
                    End If '-typeOf-
                    colShapeInvoice.Add(colPayList, "PaymentDisbursements")
                    '=colDisbursements.Add(colPayList, CStr(intInvoice_id)) '-collect disbursements this Payment..

                    colShapeInvoices.Add(colShapeInvoice, sInvoiceId)
                    intRowx += 1
                    '= mFormWait1.labMsg.Text = sWaitMsg & intRowx
                    DoEvents()
                Loop  '-rdrInvoices.Read-
            End If  '-rdrInvoices.HasRows-
            '= Call mWaitFormOff()
        Catch ex As Exception
            '-error-
            rdrInvoices.Close()
            MsgBox("Error Loading Invoices collection....." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End Try  '--main try Invoices-
        rdrInvoices.Close()
        '=System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '= MsgBox("Loaded " & colShapeInvoices.Count & " Invoices.. ", MsgBoxStyle.Information)

        '=ORIGINAL=

        colInvoices = New Collection
        decTotalInvoices = 0
        decTotalOutstanding = 0

        If (colShapeInvoices Is Nothing) OrElse (colShapeInvoices.Count <= 0) Then
            '=    (dtInvoices Is Nothing) OrElse (dtInvoices.Rows.Count <= 0) Then
            '== MsgBox("No invoice found for Customer " & intCustomer_id & "..", MsgBoxStyle.Exclamation)
            '=Me.Close()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            gbCollectCustomerInvoicesEx2 = True  '- empty is ok-
            Exit Function
        End If

        '--ok have invoices..
        '-- Apply disb. amounts to invoices..
        '--   Collect invoices (not fully paid.. ??)
        Dim sTransactionType As String
        '-- Load RESULT collection with all [Outstanding] Invoices for this customer --
        Dim intCount As Integer = 0
        Dim bIsReversal, bIsRefund As Boolean
        Dim sDisbTrancode As String
        '==Dim gridRow1 As DataGridViewRow

        Try  '--select invoices that match requirements..
            For Each colShapeInvoice In colShapeInvoices  '= datarow1 As DataRow In dtInvoices.Rows
                decPaymentTotal = 0
                intInvoice_id = colShapeInvoice.Item("invoice_id")  '= datarow1.Item("invoice_id")
                '=3519.0317=
                sTransactionType = colShapeInvoice.Item("transactiontype")  '= datarow1.Item("transactiontype")
                '= bIsRefund = (UCase(datarow1.Item("transactiontype")) = "REFUND")
                bIsRefund = (UCase(sTransactionType) = "REFUND")

                decInvoiceTotal = colShapeInvoice.Item("total_inc")  '=  CDec(datarow1.Item("total_inc"))
                '= For 4201.0727=  
                '==  MUST Round the Invoice total to take 3 decimals down to 2.
                decInvoiceTotal = Math.Round(decInvoiceTotal, 2, MidpointRounding.AwayFromZero)

                '-Same for Tax..
                decTotalTax = colShapeInvoice.Item("total_tax")  '=  datarow1.Item("total_tax")
                decTotalTax = Math.Round(decTotalTax, 2, MidpointRounding.AwayFromZero)
                '=  END of  For 4201.0727=  

                '=If bIsRefund Then
                '=    decInvoiceTotal = -decInvoiceTotal
                '=End If
                'If (colDisbursements.Count > 0) AndAlso colDisbursements.Contains(CStr(intInvoice_id)) Then
                '    colPayList = colDisbursements.Item(CStr(intInvoice_id))
                'Else
                '    colPayList = New Collection  '--empty- No payments for this invoice.
                'End If
                '="PaymentDisbursements"=
                colPayList = colShapeInvoice.Item("PaymentDisbursements")
                'If (colAccountPayments.Count > 0) AndAlso colAccountPayments.Contains(CStr(intInvoice_id)) Then
                '    colPayList = colAccountPayments.Item(CStr(intInvoice_id))
                'Else
                '    colPayList = New Collection  '--empty- No payments for this invoice.
                'End If
                sPayList = ""
                If (colPayList.Count > 0) Then
                    '--LIST and sum payments this invoice.-
                    '--  NB: Disbursements from REFUNDS (NOW is DISCOUNTS) will also appear in here..
                    For Each col1 In colPayList
                        bIsReversal = CBool(col1.Item("isReversal"))
                        decPaymentAmount = CDec(col1.Item("amount"))
                        sDisbTrancode = col1.Item("disbTranCode")
                        '= decPaymentTotal += IIf(bIsRefund, -decPaymentAmount, decPaymentAmount)
                        decPaymentTotal += IIf(bIsReversal, -decPaymentAmount, decPaymentAmount)
                        If (sPayList <> "") Then sPayList &= vbCrLf
                        s1 = VB.Left(sDisbTrancode, 4)
                        s1 &= IIf(bIsReversal, "(Revrsl): ", ": ")
                        s1 &= RSet(FormatCurrency(decPaymentAmount, 2), 9)
                        sPayList &= Format(col1.Item("date"), "dd-MMM-yy ") & s1
                    Next col1 '-payment fraction for this invoice..-
                End If '-count-
                '--compute outstanding, if any.-
                decAmountOutstanding = decInvoiceTotal - decPaymentTotal

                dateInvoice = CDate(colShapeInvoice.Item("invoice_date"))  '=  CDate(datarow1.Item("invoice_date"))
                '--load Collection with [outstanding] or [all] invoices-
                If (decAmountOutstanding <> 0) Or (Not bOutstandingInvoicesOnly) Or
                 (bOutstandingInvoicesOnly AndAlso (intClosedDaysToShow > 0) AndAlso
                                (dateInvoice >= dateClosedInvoicesToShow)) Then  '-he must pay. or show all..-
                    colInvoice1 = New Collection
                    colInvoice1.Add(intInvoice_id, "invoice_id")
                    '=3519.0227-  add isOnAccount..
                    colInvoice1.Add(colShapeInvoice.Item("isOnAccount"), "isOnAccount")
                    '= colInvoice1.Add(datarow1.Item("invoice_date"), "invoice_date")
                    colInvoice1.Add(colShapeInvoice.Item("invoice_date"), "invoice_date")
                    '=sTransactionType=
                    '= colInvoice1.Add(datarow1.Item("transactionType"), "transactionType")
                    colInvoice1.Add(sTransactionType, "transactionType")
                    colInvoice1.Add(decInvoiceTotal, "invoiceTotal")
                    '= colInvoice1.Add(datarow1.Item("total_tax"), "total_tax")
                    '==4201.0727= must be the rounded version..
                    colInvoice1.Add(decTotalTax, "total_tax")

                    colInvoice1.Add(colPayList, "invoicePayments")
                    colInvoice1.Add(sPayList, "invoicePaymentList")
                    colInvoice1.Add(decPaymentTotal, "paymentTotalThisInvoice")
                    colInvoice1.Add(decAmountOutstanding, "amountOutstanding")
                    decTotalInvoices += decInvoiceTotal
                    decTotalOutstanding += decAmountOutstanding

                    colInvoices.Add(colInvoice1, intInvoice_id)
                    intCount += 1  '-count grid rows..-
                End If '-outstanding-
            Next colShapeInvoice  '= datarow1  '--invoices-
            gbCollectCustomerInvoicesEx2 = True
        Catch ex As Exception
            gbCollectCustomerInvoicesEx2 = False
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Error selecting invoices for result collection..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End Try  '--select invoices that match requirements..
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function '-gbCollectCustomerInvoicesEx2
    '= = = = = = = = = = = = = = = = = = = = ==  = = = = = =
    '-===FF->

    '==
    '==   v3.3.3303.0109..  09-Jan-2017= ===
    '==     "gbCollectCustomerRefunds" Add functionality to collect Refunds
    '==         And un-Disbursed Refund values to Outstanding Invoices...- 
    '==
    '--3303.0109=  Special account(Payment) From when applying REFUND value to Debtors invoices
    '=   isRefundDisbursement bit NOT NULL DEFAULT 0,"
    '=   originalRefundinvoice_id INT NOT NULL DEFAULT -1, " '= REFERENCES Invoice(invoice_id),"

    '-- Get all original refund Invoices for Customer--
    '--  Match with 'isRefundDisbursement' payment records to produce list of Refunds
    '--   with some funds still left to disburse..

    '== NOW GONE ==
    '==        >>  Refunds now the same for Account Custs and non-a/c custs..   
    '==             -- So Account Payments can draw on CreditNotes for payment of invoices..  
    '==             --    and Table "paymentRefundDetails" is dropped. 
    '==        >>  DROP all references to Refunds and Table "paymentRefundDetails"
    '==

    'Public Function gbCollectCustomerRefunds(ByRef cnnSql As OleDbConnection, _
    '                                      ByVal intCustomer_id As Integer, _
    '                                        ByRef colRefunds As Collection, _
    '                                        ByRef decTotalRefunds As Decimal, _
    '                                        ByRef decTotalAvailable As Decimal) As Boolean

    '    Dim s1, sSql, sPayList As String
    '    Dim int1, intHeight, intInvoice_id As Integer
    '    '== Dim dtInvoices, dtInvPayments, dtDisbursements As DataTable
    '    Dim dtRefunds, dtPayments As DataTable
    '    Dim colRefund1 As Collection
    '    Dim col1, col2, colPayList As Collection
    '    Dim decInvoiceTotal, decPaymentAmount As Decimal
    '    Dim decPaymentTotal, decAmountAvailable As Decimal
    '    Dim dateInvoice As Date

    '    gbCollectCustomerRefunds = False

    '    sSql = "SELECT * FROM dbo.Invoice WHERE (Customer_id=" & CStr(intCustomer_id) & ")"
    '    sSql &= " AND (transactionType = 'refund') "
    '    sSql &= " ORDER BY invoice_id;"
    '    If Not gbGetDataTable(cnnSql, dtRefunds, sSql) Then
    '        MsgBox("Error in getting Invoices recordset for a/c debits: " & vbCrLf & _
    '                                        gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End If '-get-
    '    colRefunds = New Collection
    '    decTotalRefunds = 0
    '    decTotalAvailable = 0

    '    If (dtRefunds Is Nothing) OrElse (dtRefunds.Rows.Count <= 0) Then
    '        '== MsgBox("No invoice found for Customer " & intCustomer_id & "..", MsgBoxStyle.Exclamation)
    '        '=Me.Close()
    '        gbCollectCustomerRefunds = True  '- empty is ok-
    '        Exit Function
    '    End If

    '    '--ok have refunds..

    '    '- Collect 'RefundDetail' payment sub records to produce list of Refunds
    '    '--   with some funds still left to disburse..
    '    sSql = "SELECT payments.payment_id,  payment_date, "
    '    sSql &= "  paymentRefundDetails.refundInvoice_id, paymentRefundDetails.amountDisbursed "
    '    sSql &= "  FROM dbo.paymentRefundDetails  "
    '    sSql &= "    JOIN payments ON (payments.payment_id=paymentRefundDetails.payment_id) "
    '    sSql &= "   WHERE (payments.Customer_id=" & CStr(intCustomer_id) & ") "
    '    '= sSql &= "  AND (isRefundDisbursement =1) "
    '    sSql &= " ORDER BY refundInvoice_id;"

    '    '- get payments datatable.
    '    If Not gbGetDataTable(cnnSql, dtPayments, sSql) Then
    '        MsgBox("Error in getting recordset for account paymentRefundDetails table: " & vbCrLf & _
    '                                       gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End If  '-get

    '    '- Filter Refunds to get those with unused funds.
    '    For Each datarow1 As DataRow In dtRefunds.Rows
    '        decPaymentTotal = 0
    '        intInvoice_id = datarow1.Item("invoice_id")
    '        decInvoiceTotal = CDec(datarow1.Item("total_inc"))

    '        '-- look through refund disbursements and compute what's been used.
    '        '--  For this refund.
    '        For Each dtRow1 As DataRow In dtPayments.Rows
    '            If (dtRow1.Item("refundinvoice_id") = intInvoice_id) Then
    '                decPaymentTotal += CDec(dtRow1.Item("amountDisbursed"))
    '            End If '-id-
    '        Next dtRow1
    '        decAmountAvailable = decInvoiceTotal - decPaymentTotal
    '        If (decAmountAvailable > 0) Then
    '            '- have available refund amt left.
    '            '-- Capture this remaining refund amt.-
    '            colRefund1 = New Collection
    '            colRefund1.Add(intInvoice_id, "invoice_id")
    '            colRefund1.Add(datarow1.Item("invoice_date"), "invoice_date")
    '            colRefund1.Add(decInvoiceTotal, "total_inc")
    '            colRefund1.Add(decAmountAvailable, "AmountAvailable")
    '            colRefunds.Add(colRefund1, intInvoice_id)

    '            decTotalRefunds += decInvoiceTotal
    '            decTotalAvailable += decAmountAvailable

    '        End If '-totals-
    '    Next datarow1
    '    gbCollectCustomerRefunds = True  '- empty is ok-

    'End Function  '-gbCollectCustomerRefunds-
    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '==3403.430=  Get Customer Layby's ==
    '-- Returns collection of CURRENT Laybys-
    '--   Each Layby holds a gbMakeRecordCollection collection of stock items..
    '==   get all customers if customer_id NOT supplied.

    Public Function gbCollectCustomerLaybys(ByRef cnnSql As OleDbConnection, _
                                             ByRef colAllCustLaybys As Collection, _
                                              Optional ByVal intInputCustomer_id As Integer = -1, _
                                              Optional ByVal bIncludeDeliveredlaybys As Boolean = False) As Boolean
        Dim s1, sSql, sCustName, sCustBarcode As String
        Dim int1, intId, intCustomer_id, intLayby_id, intStaff_id As Integer
        Dim dateStarted, dateDelivered As Date
        Dim bIsDelivered As Boolean
        Dim dtLaybys, dtLaybyItems As DataTable
        Dim colCustomerIDs, colCust, colCustomerLaybys As Collection
        Dim col1, col2, colLaybyIDs, colLayby, colLaybyItems As Collection
        Dim decLaybyTotal, decLaybyDiscount_nett, decLaybyDiscount_tax As Decimal
        Dim dtRow1 As DataRow

        gbCollectCustomerLaybys = False

        '-- get all layby lines, and then orgainise into collections (Cust/layby/lines).

        sSql = "SELECT *, stock.barcode AS stock_barcode, "
        sSql &= "LBY.customer_id, LBY.isCancelled, LBY.isDelivered, LBY.total_inc AS layby_total_inc, "
        sSql &= "  customer.barcode AS customer_barcode,  "
        sSql &= "  customerName = CASE companyName  "
        sSql &= "     WHEN '' THEN (customer.lastname + ', ' + customer.firstname)"
        sSql &= "     WHEN 'n/a' THEN (customer.lastname + ', ' + customer.firstname)"
        sSql &= "     ELSE companyName "
        sSql &= "   END "
        '= sSql &= "     CUST.companyName, CUST.firstName, CUST.lastName "
        sSql &= " FROM dbo.laybyLine LL "
        sSql &= " JOIN dbo.stock ON (stock.stock_id=LL.stock_id) "
        sSql &= " JOIN dbo.layby LBY ON (LBY.layby_id=LL.layby_id) "
        sSql &= " JOIN dbo.customer ON (customer.customer_id=LBY.customer_id) "
        sSql &= "   WHERE (LBY.isCancelled=0) "
        If (Not bIncludeDeliveredlaybys) Then
            sSql &= "  AND (LBY.isDelivered=0) "
        End If
        If (intInputCustomer_id > 0) Then
            sSql &= " AND (LBY.customer_id=" & CStr(intInputCustomer_id) & ")"
        End If
        sSql &= " ORDER BY customerName, LBY.layby_id;"

        If Not gbGetDataTable(cnnSql, dtLaybyItems, sSql) Then
            MsgBox("Error in getting Layby's recordset- " & vbCrLf & _
                                            gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If '-get-
        colAllCustLaybys = New Collection

        If (dtLaybyItems.Rows.Count > 0) Then  '-have some layby items.
            '- make collection of customers involved..
            colCustomerIDs = New Collection
            For Each dtRow1 In dtLaybyItems.Rows
                intId = dtRow1.Item("customer_id")
                If Not colCustomerIDs.Contains(CStr(intId)) Then
                    colCustomerIDs.Add(dtRow1.Item("customer_id"), CStr(intId))
                End If
            Next dtRow1

            '-- collect stuff for each cust.
            For Each intCustomer_id In colCustomerIDs
                sCustName = ""
                colCust = New Collection
                colLaybyIDs = New Collection
                For Each dtRow1 In dtLaybyItems.Rows
                    If (dtRow1.Item("customer_id") = intCustomer_id) Then
                        '==   Updated.- 3519.0219  Started 18-Feb-2019= 
                        '==   - Fix code to add to layby_id collection (needs cstr() as key.)..
                        intId = dtRow1.Item("layby_id")
                        If Not colLaybyIDs.Contains(CStr(intId)) Then
                            colLaybyIDs.Add(dtRow1.Item("layby_id"), CStr(intId))
                            '- grab cust name once only-
                            If (sCustName = "") Then
                                sCustName = Trim(dtRow1.Item("customerName"))
                                sCustBarcode = Trim(dtRow1.Item("customer_barcode"))
                                'If (sCustName = "") Then '-no company-
                                '    sCustName = dtRow1.Item("firstName") & " " & dtRow1.Item("lastName")
                                'End If
                            End If  '-name=
                        End If
                    End If
                Next dtRow1
                '--collect laybys thiis customer.-
                colCustomerLaybys = New Collection
                For Each intLayby_id In colLaybyIDs
                    colLayby = New Collection
                    colLaybyItems = New Collection
                    intStaff_id = -1
                    '-collect layby contents-
                    For Each dtRow1 In dtLaybyItems.Rows
                        If (dtRow1.Item("layby_id") = intLayby_id) Then
                            If intStaff_id = -1 Then  '--collect layby info-
                                decLaybyTotal = (dtRow1.Item("layby_total_inc"))
                                decLaybyDiscount_nett = (dtRow1.Item("discount_nett"))
                                decLaybyDiscount_tax = (dtRow1.Item("discount_tax"))
                                intStaff_id = dtRow1.Item("staff_id")
                                dateStarted = dtRow1.Item("Layby_date_started")
                                bIsDelivered = False
                                If (dtRow1.Item("isDelivered") <> 0) Then
                                    bIsDelivered = True
                                    dateDelivered = dtRow1.Item("Layby_date_delivered")
                                End If
                            End If
                            col1 = New Collection
                            col1.Add(dtRow1.Item("stock_id"), "stock_id")
                            col1.Add(dtRow1.Item("stock_barcode"), "stock_barcode")
                            col1.Add(dtRow1.Item("serialNumber"), "serialNumber")
                            col1.Add(dtRow1.Item("description"), "description")
                            col1.Add(dtRow1.Item("quantity"), "quantity")
                            '-- collect this layby item.-
                            colLaybyItems.Add(col1)
                        End If '-id-
                        '--collect all itemss-
                    Next dtRow1
                    colLayby.Add(colLaybyItems, "items")
                    colLayby.Add(decLaybyTotal, "total_inc")
                    colLayby.Add(decLaybyDiscount_nett, "discount_nett")
                    colLayby.Add(decLaybyDiscount_tax, "discount_tax")
                    colLayby.Add(dateStarted, "Layby_date_started")
                    colLayby.Add(bIsDelivered, "isDelivered")
                    If bIsDelivered Then
                        colLayby.Add(dateDelivered, "Layby_date_delivered")
                    End If
                    colLayby.Add(intStaff_id, "staff_id")
                    colLayby.Add(intLayby_id, "Layby_id")
                    colCustomerLaybys.Add(colLayby, intLayby_id)
                Next intLayby_id
                colCust.Add(sCustBarcode, "Customer_barcode")
                colCust.Add(sCustName, "Customer_name")
                colCust.Add(intCustomer_id, "Customer_id")
                colCust.Add(colCustomerLaybys, "laybys")
                colAllCustLaybys.Add(colCust, CStr(intCustomer_id))
            Next intCustomer_id

        End If  '--items count-
        gbCollectCustomerLaybys = True

    End Function '-gbCollectCustomerLaybys--
    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Save PDF in docs table-
    '-  3403.1009-  Table is NOW GONE
    '-  3403.1009-  Table is NOW GONE

    'Public Function gbSaveDocumentToDB(ByRef cnnSql As OleDbConnection, _
    '                                   ByVal strFileFullPath As String, _
    '                                   ByVal strFileTitle As String, _
    '                                   ByVal strDocTitle As String, _
    '                                   ByVal strFileFormat As String, _
    '                                   ByVal strAppType As String, _
    '                                   ByVal intCust_id As Integer, _
    '                                   ByVal intSupplier_id As Integer, _
    '                                   ByVal strDocDescription As String, _
    '                                   ByVal strTargetEmailAddress As String, _
    '                                   ByVal strEmailText As String _
    '                                   ) As Boolean

    '    Dim byteFileData As Byte()
    '    Dim sFldList As String = ""
    '    Dim sValueList As String = ""
    '    Dim sUpdate As String = ""
    '    Dim sSqlDataType, sFldData, sSql As String
    '    '== Dim ix, intAffected, intID, intStock_id As Integer
    '    Dim imageParameters1() = New OleDbParameter() {}  '--instantiates zero-length 1-dim array.--
    '    Dim parameter1 As OleDbParameter
    '    Dim cmd1 As OleDbCommand

    '    gbSaveDocumentToDB = False
    '    Try
    '        '--load File bytes..--
    '        byteFileData = gabConvertDataFiletoBytes(strFileFullPath)
    '    Catch ex As Exception
    '        MsgBox("Failed to load Dc File data from File: " & strFileFullPath & vbCrLf & _
    '                           "Error: " & ex.Message)
    '        Exit Function
    '    End Try

    '    '-- INSERT doc row into doc archive Table..
    '    sFldList = "doc_file_title, doc_title, doc_file_format, doc_app_type, doc_description, doc_customer_id,  "
    '    sFldList &= " doc_supplier_id, doc_target_email_address, doc_email_text   "

    '    sValueList = "'" & gsFixSqlStr(strFileTitle) & "', "
    '    sValueList &= "'" & gsFixSqlStr(strDocTitle) & "', "
    '    sValueList &= "'" & gsFixSqlStr(strFileFormat) & "', "
    '    sValueList &= "'" & gsFixSqlStr(strAppType) & "', "
    '    sValueList &= "'" & gsFixSqlStr(strDocDescription) & "', "
    '    sValueList &= CStr(intCust_id) & ", "
    '    sValueList &= CStr(intSupplier_id) & ", "
    '    sValueList &= "'" & gsFixSqlStr(strTargetEmailAddress) & "', "
    '    sValueList &= "'" & gsFixSqlStr(strEmailText) & "' "

    '    '--VARBINARY column- can't use strings.--
    '    '--  make SQL cmd parameter..-
    '    '-- BUILD SQL cmd parameter for image byte[]...
    '    If Not byteFileData Is Nothing Then
    '        If (sFldList <> "") Then
    '            sFldList = sFldList & ", "
    '            sValueList = sValueList & ", "
    '        End If
    '        sFldList = sFldList & "doc_file_content"
    '        sValueList = sValueList & " ? "
    '        parameter1 = New OleDbParameter("@" & "doc_file_content", SqlDbType.VarBinary)
    '        parameter1.Value = byteFileData '= mColRowImages(sFldName)
    '        Dim k As Integer = imageParameters1.Length + 1
    '        ReDim Preserve imageParameters1(k - 1)
    '        imageParameters1(k - 1) = parameter1
    '    End If  '--nothing=                
    '    sSql = "INSERT INTO dbo.DocArchive (" + sFldList + ")  VALUES (" + sValueList + ");"
    '    '== MsgBox("SQL Insert cmd is : " & vbCrLf & sSql, MsgBoxStyle.Information)
    '    Try
    '        cmd1 = New OleDbCommand(sSql, cnnSql)
    '        If (imageParameters1.Length > 0) Then
    '            For ix As Integer = 0 To (imageParameters1.Length - 1)
    '                cmd1.Parameters.Add(imageParameters1(ix))
    '            Next
    '        End If
    '        cmd1.ExecuteNonQuery()
    '    Catch ex As Exception
    '        MsgBox("Sql Error in INSERT document record: " & vbCrLf & "SQL Command was: " & _
    '                      sSql & vbCrLf & ex.Message & vbCrLf & vbCrLf & _
    '                      "Note that barcodes must be UNIQUE..", MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End Try

    '    gbSaveDocumentToDB = True

    'End Function  '-save doc-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-gbSaveDocumentToEmailQueue-
    '-- Build XML descriptor file to go with it..
    '==   >> 3411.0106 -- 06-Jan-2018== .
    '==                -- Add CustomerName to XML... 
    '==
    '==
    '==   >> 3431.0621=  21-June-2018=
    '==    -- Cleanup Email XML Files for reserved chars (eg ampersand,<, > )...
    '==

    '==
    '== NEW Revision-
    '==   == 4219.1214.  10-Dec-2019-  Started 10-Dec-2019-
    '==     -- PO PDF (email) printing.... 
    '==            In "gbSaveDocumentToEmailQueue", 
    '==                  keep local file copy of PDF for SEVEN days after copying to Email Queue.. 
    '==            ALSO- in "gsGetPDF_file_path" (modPrintSubs), Get AppName from gsGetAppname
    '==


    Public Function gbSaveDocumentToEmailQueue(ByRef cnnSql As OleDbConnection, _
                                   ByVal strFileFullPath As String, _
                                   ByVal strFileTitle As String, _
                                   ByVal strFileFormat As String, _
                                   ByVal strAppType As String, _
                                   ByVal intCust_id As Integer, _
                                   ByVal intSupplier_id As Integer, _
                                   ByVal intTransaction_id As Integer, _
                                   ByVal strDocEmailSubject As String, _
                                   ByVal strTargetName As String, _
                                   ByVal strTargetEmailAddress As String, _
                                   ByVal strEmailText As String, _
                                   ByVal strEmailQueueSharePath As String) As Boolean

        Dim sServerShareFullPath As String = Trim(strEmailQueueSharePath)
        Dim sXmlDescriptorFileTitle As String
        Dim sXmlDescriptorFileXml As String = ""

        gbSaveDocumentToEmailQueue = False
        If (strEmailQueueSharePath = "") Then
            MsgBox("Error- No server path provided for email queue..: ", MsgBoxStyle.Exclamation)
            Exit Function
        Else
            '-temp- =3403.1031= GONE=
            '    MsgBox("Testing- the File: " & vbCrLf & strFileFullPath & vbCrLf & _
            '        "Will be stored at: " & vbCrLf & strEmailQueueSharePath, MsgBoxStyle.Information)
        End If

        If VB.Right(sServerShareFullPath, 1) <> "\" Then
            sServerShareFullPath &= "\"
        End If
        sServerShareFullPath &= strFileTitle

        '= sXmlDescriptorFileTitle = strFileTitle & ".xml" '-same name root. +XML.

        '== make xml descriptor File.
        sXmlDescriptorFileXml = "<Pos-doc-descriptor> " & vbCrLf

        sXmlDescriptorFileXml &= "<doc-email-to-name> "
        sXmlDescriptorFileXml &= msCleanUpXMLData(strTargetName)
        sXmlDescriptorFileXml &= "</doc-email-to-name> " & vbCrLf

        sXmlDescriptorFileXml &= "<doc-email-to-address> "
        sXmlDescriptorFileXml &= msCleanUpXMLData(strTargetEmailAddress)
        sXmlDescriptorFileXml &= "</doc-email-to-address> " & vbCrLf

        sXmlDescriptorFileXml &= "<doc-subject> "
        sXmlDescriptorFileXml &= msCleanUpXMLData(strDocEmailSubject)
        sXmlDescriptorFileXml &= "</doc-subject> " & vbCrLf

        sXmlDescriptorFileXml &= "<doc-emailtext> "
        sXmlDescriptorFileXml &= msCleanUpXMLData(strEmailText)
        sXmlDescriptorFileXml &= "</doc-emailtext> " & vbCrLf

        sXmlDescriptorFileXml &= "<doc-file-title> "
        sXmlDescriptorFileXml &= msCleanUpXMLData(strFileTitle)
        sXmlDescriptorFileXml &= "</doc-file-title> " & vbCrLf

        '== FINISH xml descriptor File.
        sXmlDescriptorFileXml &= "</Pos-doc-descriptor> "

        '--make xml file name..
        sXmlDescriptorFileTitle = "Email_" & strFileTitle & ".xml" '-same name root. +XML.
        '-- copy actual PDF doc to server..
        '-- DO NOT overwrite if requested.-
        Dim bOverWrite As Boolean = False
        If My.Computer.FileSystem.FileExists(sServerShareFullPath) Then
            If MsgBox("NB: The PDF file:" & vbCrLf & _
                        "'" & sServerShareFullPath & "'" & vbCrLf & _
                        "Already exists on the Server Email Queue." & vbCrLf & vbCrLf & _
                        "  Do you want to OVERWRITE it with this new one ?", _
                           MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                Exit Function
            End If
            '-ok to overwrite-
            bOverWrite = True
        End If
        '-- copy=
        Try
            File.Copy(strFileFullPath, sServerShareFullPath, bOverWrite)
        Catch ex As Exception
            MsgBox("Failed to copy " & strAppType & " to server share." & vbCrLf & _
                   "File may be a duplicate.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '-ok.. create xml descriptor file on share..
        Dim sXmlPath As String = Path.Combine(Trim(strEmailQueueSharePath), sXmlDescriptorFileTitle)
        '-- WriteAllText overwrites any existing file..-
        Try
            File.WriteAllText(sXmlPath, sXmlDescriptorFileXml)
            gbSaveDocumentToEmailQueue = True
        Catch ex As Exception
            MsgBox("ERROR- Failed to save XML descr. file to server share.", MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        '    Dim intResult = glSaveTextFile(sXmlPath, sXmlDescriptorFileXml)
        '    If (intResult = 0) Then
        '        '-ok-
        '        gbSaveDocumentToEmailQueue = True
        '    Else
        '        MsgBox("ERROR- Failed to save XML descr. file to server share.", MsgBoxStyle.Exclamation)
        '        '- DElete PDF ???  --
        '    End If  '-result-

        '-- NOW delete local source PDF.. strFileFullPath --

        '--  NO NO..
        '==   == 4219.1214.  10-Dec-2019-  Started 10-Dec-2019-
        '==        keep local file copy of PDF for SEVEN days after copying to Email Queue.. 

        If My.Computer.FileSystem.FileExists(strFileFullPath) Then
            'Try
            '    My.Computer.FileSystem.DeleteFile(strFileFullPath, _
            '                                      FileIO.UIOption.OnlyErrorDialogs, _
            '                                      FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
            'Catch ex As Exception
            '    MsgBox("ERROR- Trying to delete old file: " & strFileFullPath & vbCrLf & ex.Message)
            'End Try
        End If  '-exists.-

        '==   == 4219.1214.  10-Dec-2019-  Started 10-Dec-2019-
        '==        keep local file copy of PDF for SEVEN days after copying to Email Queue.. 

        Dim sLocalPath, sFullPath1 As String
        Dim fileSystemInfo1 As FileSystemInfo
        Dim intCount As Integer = 0
        '- find path of local dir.
        Dim intPos As Integer = InStrRev(strFileFullPath, "\")

        If (intPos > 1) Then
            sLocalPath = VB.Left(strFileFullPath, (intPos - 1))
            '-test-
            '= MsgBox("Local path is: [ " & sLocalPath & " ]")
            '-delete all PDF files more than seven days old..
            intCount = 0
            For Each sFoundFileName As String In My.Computer.FileSystem.GetFiles(sLocalPath)
                '=ListBox1.Items.Add(foundFile)
                If VB.Right(LCase(sFoundFileName), 4) = ".pdf" Then
                    sFullPath1 = sFoundFileName  '= sLocalPath & "\" & sFoundFileName
                    fileSystemInfo1 = My.Computer.FileSystem.GetFileInfo(sFullPath1)
                    If (DateDiff(DateInterval.Day, fileSystemInfo1.LastWriteTime, Now) > 7) Then
                        '-test-
                        'MsgBox("We will delete the file: [ " & sFullPath1 & " ]" & vbCrLf & _
                        '          vbCrLf & "Last modified: " & _
                        '                   Format(fileSystemInfo1.LastWriteTime, "dd-MMM-yyyy"), MsgBoxStyle.Information)
                        Try
                            My.Computer.FileSystem.DeleteFile(sFullPath1, _
                                                              FileIO.UIOption.OnlyErrorDialogs, _
                                                              FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
                            intCount += 1
                        Catch ex As Exception
                            MsgBox("ERROR- Trying to delete old file: " & sFullPath1 & vbCrLf & ex.Message)
                        End Try
                    End If  '-datediff-
                End If  '-pdf-
            Next sFoundFileName
            If (intCount > 0) Then
                MsgBox("Info only- we deleted " & intCount & " old PDF files..", MsgBoxStyle.Exclamation)
            End If
        End If  '-intPos-
    End Function  '-gbSaveDocumentToEmailQueue-
    '= = = = = = = = = = = = = = = ==  == = = 
    '-===FF->

    '==
    '==     v3.3.3301.1114..  14-Nov-2016= ===
    '==       >> Add Licence Checking (cloning POS licence off Jobmatix)..-
    '==
    '-- C O M P U T E   L I C E N C E ----
    '-- C O M P U T E   L I C E N C E ----

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==   Target-New-Build-6201 --  (09-June-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '==   ComputePosKey now points to dummy in clsKeygen42 in DLL JMxKeyGen420_OS
    '==   ComputePosKey now in clsKeygen42 in DLL JMxKeyGen420_OS
    '==     See class clsJMxPOS31..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    'Public Function gbComputePosKey(ByVal sBusinessABN As String,
    '                                ByVal sBusinessPostCode As String,
    '                                ByVal sBusinessShortName As String,
    '                                ByVal sSqlDbName As String,
    '                                  ByRef sComputedKeyUnlimited As String,
    '                                    ByRef sComputedKeyLevel2 As String,
    '                                     ByRef sComputedKeyThreeUser As String,
    '                                      ByRef sComputedKeyTwoUser As String,
    '                                         ByRef sComputedKeySingleUser As String
    '                                                                         ) As Boolean

    '    Dim clsKeyGen1 As JMxKeyGen420.clsKeyGen42
    '    Dim bIsPosSys As Boolean = True

    '    gbComputePosKey = False
    '    clsKeyGen1 = New JMxKeyGen420.clsKeyGen42
    '    '-- NOW used class for compute..

    '    If Not clsKeyGen1.ComputePosKey(sBusinessABN, sBusinessPostCode, sBusinessShortName,
    '                                   sSqlDbName, bIsPosSys, sComputedKeyUnlimited,
    '                                  sComputedKeyLevel2, sComputedKeyThreeUser, sComputedKeyTwoUser, sComputedKeySingleUser) Then
    '        MsgBox("Failed to get Licence Keys..", MsgBoxStyle.Exclamation)
    '    Else
    '        '-- ok--
    '        gbComputePosKey = True
    '    End If  '-get keys.

    '== Computes done ==

    '==  ALL THIS key stuff now gone..

    'End Function  '-gbComputePosKey-
    '= = = = = = = = = = = = = === =
    '-===FF->

    '== 3083.310== DateDiff Function to bypass legacy VB6 DateDiff. etc.--

    Public Function gIntDateDiffDays(ByVal date1 As DateTime, _
                                     ByVal date2 As DateTime) As Integer
        Dim timespan1, timespan2 As TimeSpan

        Try
            timespan1 = date1.Subtract(DateTime.MinValue)  '-- interval from beginning of time..
            timespan2 = date2.Subtract(DateTime.MinValue)
            gIntDateDiffDays = timespan2.Days - timespan1.Days

        Catch ex As Exception
            MsgBox("Error in 'gIntDateDiffDays' function.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            gIntDateDiffDays = 0
        End Try

    End Function  '--gIntDateDiffDays-
    '= = = = = = = = = == =
    '-===FF->

    '-- CheckLicenceKey --
    '-- CheckLicenceKey --

    '==   Target-New-Build-6201 --  (15-June-2021)
    '==   Target-New-Build-6201 --  (15-June-2021)

    'Public Function gbCheckLicenceKey(ByVal sTestKey As String,
    '                                   ByVal sComputedKeyUnlimited As String,
    '                                          ByVal sComputedKeyLevel2 As String,
    '                                        ByVal sComputedKeyThreeUser As String,
    '                                           ByVal sComputedKeyTwoUser As String,
    '                                         ByVal sComputedKeySingleUser As String,
    '                                              ByRef bIsLevel2Licence As Boolean,
    '                                             ByRef intMaxUsersLicenced As Integer) As Boolean
    '    Dim intNoUsers As Integer
    '    Dim bLicenceOk As Boolean = False

    '    gbCheckLicenceKey = False
    '    bIsLevel2Licence = False
    '    '== intNoUsers = K_ABSOLUTE_MAX_USERS_PERMITTED
    '    intNoUsers = -1  '== No limit- 

    '    Select Case UCase(sTestKey)
    '        '= Case sComputedPreciseShortKey
    '        '=     bLicenceOk = True
    '        Case sComputedKeyUnlimited
    '            bLicenceOk = True
    '        Case sComputedKeyLevel2
    '            bLicenceOk = True
    '            bIsLevel2Licence = True
    '        Case sComputedKeyThreeUser
    '            bLicenceOk = True
    '            intNoUsers = 3
    '        Case sComputedKeyTwoUser
    '            bLicenceOk = True
    '            intNoUsers = 2
    '        Case sComputedKeySingleUser
    '            bLicenceOk = True
    '            intNoUsers = 1
    '        Case Else
    '            intNoUsers = 0
    '    End Select
    '    gbCheckLicenceKey = bLicenceOk
    '    intMaxUsersLicenced = intNoUsers

    'End Function  '--check key-

    '== END Target-New-Build-6201 --  (15-June-2021)
    '== END Target-New-Build-6201 --  (15-June-2021)

    '= = = = = = = = =  = =
    '= = = = = = = = = == =
    '-===FF->

    '- Get Collection of CurrentCashDrawers-
    '--  Look up SystemInfo Till Assignments for this computer. (msComputerName)
    '-- Sysinfo key is like "CashDrawer_[ Computer ]=X" ( where "computer"=ComputerName)..
    '--         AND Where "X"  can be ["A".."Z"] ie Till assigned to this computer..
    '-- NB.. Multiple computers can be assigned to the same Till at the same time..

    Public Function gbGetCurrentCashDrawers(ByRef sysInfo1 As clsSystemInfo, _
                                            ByRef colCurrentCashDrawers As Collection) As Boolean
        Dim col1 As Collection
        Dim sTillId, sTerminalName As String

        Try
            colCurrentCashDrawers = New Collection
            For Each sKey1 As String In sysInfo1.keys '=  listInfokeys '- colInfokeys
                If (VB.Left(LCase(sKey1), 11) = "cashdrawer_") Then
                    sTerminalName = (VB.Mid(LCase(sKey1), 12)) '== = "_computer")
                    sTillId = UCase(sysInfo1.item(sKey1))  '= UCase(Mid(sKey1, 11, 1))
                    '= sTerminalName = sysInfo1.item(sKey1)
                    col1 = New Collection
                    col1.Add(sTillId, "tillid")
                    col1.Add(sTerminalName, "computer")
                    '= colCurrentCashDrawers.Add(col1, sTillId)
                    colCurrentCashDrawers.Add(col1, sTerminalName)
                End If '-cash drawer-
            Next sKey1
            gbGetCurrentCashDrawers = True
        Catch ex As Exception
            MsgBox("Runtime error in 'gbGetCurrentCashDrawers' " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function '-gbGetCurrentCashDrawers-
    '= = = = = = = = = = = = = = = = ==  = == ==
    '-===FF->

    '==
    '==   v3.3.3301.1211..  11-Dec-2016= ===
    '==        CashDrawer ID's (as per MYOB RM.).- 
    '-- Sysinfo key is like "CashDrawer_[ Computer ]=X" ( where "computer"=ComputerName)..
    '--         AND Where "X"  can be ["A".."Z"] ie Till assigned to this computer..
    '-- NB.. Multiple computers can be assigned to the same Till.

    '-- Assign ThisComputer to a CashDrawer from ["A".."Z"].-
    '-- NB.. Multiple computers can be legally assigned to the same Till.

    Public Function gbAssignCashDrawer(ByRef cnnSql As OleDbConnection, _
                                        ByRef sysInfo1 As clsSystemInfo, _
                                         ByVal strThisComputer As String, _
                                         ByRef strCashDrawerId As String, _
                                         Optional ByVal bChangeRequested As Boolean = False) As Boolean
        Dim colCurrentCashDrawers As Collection
        Dim s1, sMsg, sMsg2 As String
        Dim sCurrentCashDrawer As String = ""
        Dim sTillId, sTerminalName, sKey As String
        Dim sAllTills As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        '= Dim sAllAssigned As String = ""
        '= Dim sTillsAvailable As String = sAllTills
        Dim bTillCancelled As Boolean = False

        gbAssignCashDrawer = False
        sMsg = "Pls note: Each POS W/s must have a current Cash Drawer (Till) Id assigned.." & vbCrLf & _
          "Current Till Assignments are: " & vbCrLf
        Try  '-main-
            While (sCurrentCashDrawer = "") And (Not bTillCancelled)
                '= sTillsAvailable = sAllTills
                sMsg2 = ""
                If gbGetCurrentCashDrawers(sysInfo1, colCurrentCashDrawers) Then
                    For Each col1 As Collection In colCurrentCashDrawers
                        sTillId = UCase(col1.Item("tillid"))
                        '= sAllAssigned &= sTillId    '--accumulated reserved tills.
                        '= sTillsAvailable = Replace(sTillsAvailable, sTillId, "")  '--delete from available.-
                        sTerminalName = col1.Item("computer")
                        sMsg2 &= sTillId & ": " & sTerminalName & ";  "
                        '- check if this is our Till.
                        If LCase(sTerminalName) = LCase(strThisComputer) Then  '-yes-
                            sCurrentCashDrawer = sTillId   '--have current till-
                            Exit For
                        End If
                    Next col1
                Else  '-failed-
                    Exit Function
                End If
                '-- if not found, we have to get one assigned..
                If (sCurrentCashDrawer = "") Or bChangeRequested Then
                    sMsg &= sMsg2 & vbCrLf & "Please enter the ID Letter [A..Z] of your Till: "
                    '== s1 = UCase(InputBox(sMsg2, "", Mid(sTillsAvailable, 1, 1)))
                    s1 = UCase(InputBox(sMsg, "", ""))
                    If ((s1 = "") Or (s1.Length <> 1)) OrElse (InStr(sAllTills, s1) <= 0) Then
                        If MsgBox("Not a valid Till letter.", _
                            MsgBoxStyle.Exclamation + MsgBoxStyle.RetryCancel + _
                                          MsgBoxStyle.DefaultButton1) <> MsgBoxResult.Retry Then
                            bTillCancelled = True
                        End If
                    Else   '= ok  If InStr(sAllTills, s1) > 0 Then  '--valid choice from avail..-
                        '= MsgBox("ok.  you chose to be on Till-" & s1)
                        If MsgBox("ok. you want to be on Till-" & s1 & " now ?", _
                                         MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + _
                                                 MsgBoxStyle.DefaultButton1) <> MsgBoxResult.Yes Then
                            '= Go around= bTillCancelled = True
                        Else '-ok-
                            sCurrentCashDrawer = s1
                            '-- Update sysinfo..
                            sKey = "cashdrawer_" & strThisComputer
                            If Not sysInfo1.UpdateSystemInfo(New Object() {sKey, sCurrentCashDrawer}) Then
                                MsgBox("Failed to update CashDrawer details in systemInfo table..", MsgBoxStyle.Critical)
                            Else
                                strCashDrawerId = sCurrentCashDrawer
                                gbAssignCashDrawer = True
                            End If
                        End If
                    End If  '-s1- input-
                Else '- have till alreday-
                    strCashDrawerId = sCurrentCashDrawer
                    gbAssignCashDrawer = True
                    MsgBox("You are currently assigned to Till-" & sCurrentCashDrawer, MsgBoxStyle.Information)
                End If '--now have current-

            End While '-sCurrentCashDrawer-

        Catch ex As Exception
            MsgBox("Runtime error in 'gbAssignCashDrawer' " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try  '-main-

    End Function  '-gbAssignCashDrawer-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  gbGetCashDrawer-  
    '--  Check/Get a CashDrawer ID for our Workstation..

    Public Function gbGetCashDrawer(ByRef cnnSql As OleDbConnection, _
                                     ByVal strThisComputer As String, _
                                      ByRef strCashDrawerId As String) As Boolean
        Dim sysInfo1 As clsSystemInfo
        Dim colCurrentCashDrawers As Collection
        Dim sCurrentCashDrawer As String = ""

        gbGetCashDrawer = False

        '== TEST=
        Dim strUserList As String
        Dim colWhichUsers As Collection
        'If gbShowLoggedInUsers(cnnSql, strSqlDbName, colWhichUsers, strUserList) Then
        '    MsgBox("Current users are: " & vbCrLf & strUserList, MsgBoxStyle.Information)  '-TEST-
        'End If '-show users.-
        '==-- END TEST =

        Try
            sysInfo1 = New clsSystemInfo(cnnSql)
            If gbGetCurrentCashDrawers(sysInfo1, colCurrentCashDrawers) Then
                For Each col1 As Collection In colCurrentCashDrawers
                    '- check if this is our Till.
                    If LCase(col1.Item("computer")) = LCase(strThisComputer) Then  '-yes-
                        sCurrentCashDrawer = col1.Item("tillid")   '--have current till-
                        Exit For
                    End If
                Next '-col1-
                '-- if no till assigned, then solicit a choice..
                If (sCurrentCashDrawer = "") Then
                    If gbAssignCashDrawer(cnnSql, sysInfo1, strThisComputer, sCurrentCashDrawer) Then
                        strCashDrawerId = sCurrentCashDrawer
                        Call gbSetCurrentCashDrawer(sCurrentCashDrawer)  '-save as global-
                        gbGetCashDrawer = True
                    Else  '-failed-

                    End If '--assign-
                Else '- ok. have till-
                    strCashDrawerId = sCurrentCashDrawer
                    Call gbSetCurrentCashDrawer(sCurrentCashDrawer)  '-save as global-
                    gbGetCashDrawer = True
                End If  '-have till-
            Else  '-failed-
                Exit Function
            End If  '-get-

        Catch ex As Exception
            MsgBox("Runtime error in 'gbGetCashDrawer' " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function  '--gbGetCashDrawer-
    '= = = = = = = = = = = = = = = == = =
    '-===FF->

    '-- Change Cash Drawer..

    Public Function gbChangeCashDrawer(ByRef cnnSql As OleDbConnection, _
                                            ByVal strThisComputer As String, _
                                              ByRef strCashDrawerId As String) As Boolean
        Dim sysInfo1 As clsSystemInfo
        Dim colCurrentCashDrawers As Collection
        Dim sCurrentCashDrawer As String = ""

        gbChangeCashDrawer = False

        Try
            sysInfo1 = New clsSystemInfo(cnnSql)
            If gbGetCurrentCashDrawers(sysInfo1, colCurrentCashDrawers) Then
                If gbAssignCashDrawer(cnnSql, sysInfo1, strThisComputer, sCurrentCashDrawer, True) Then
                    strCashDrawerId = sCurrentCashDrawer
                    Call gbSetCurrentCashDrawer(sCurrentCashDrawer)  '-save as global-
                    gbChangeCashDrawer = True
                Else  '-failed-

                End If '--assign-
            End If  '-get-
        Catch ex As Exception
            MsgBox("Runtime error in 'gbCHANGECashDrawer' " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function  '-change Drawer.-
    '= = = = = = = = = == = == = = =  = 
    '-===FF->

    '=3307.0209=
    '==
    '==    -- 4201.0531.  Fix modPOS31Support (gbGetCreditNoteHistory)..
    '==             to initialise "decCreditNoteCreditRemaining" to zero.
    '==

    '-- gbGetCreditNoteHistory --
    '--  Function to collect Credit Note history..

    '==
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==
    '==  (a) POS- Reversals to Payments that included Saved Credits Notes-   
    '==        -- Credit note amounts are being treated As credits To Customer instead Of showing As debit (reversed).  
    '==          See "gbGetCreditNoteHistory" CreditNote Report, 
    '==               And CreditNote balance On Sale Screen (from clsDebtors ?) ..
    '==          IF Payment record is a REVERSAL, then "creditNotePaymentCredited" and "creditNoteAmountDebited" need reversing.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = = = = == 


    Public Function gbGetCreditNoteHistory(ByRef cnnSql As OleDbConnection, _
                                           ByVal intCustomer_id As Integer, _
                                           ByRef dtCreditNotes As DataTable, _
                                           ByRef decCredits As Decimal, _
                                           ByRef decDebits As Decimal, _
                                           ByRef decCreditNoteCreditRemaining As Decimal) As Boolean
        Dim sSql As String
        Dim decAmount As Decimal
        '==   Target-New-Build-4277 -- (Started 07-October-2020)
        Dim bIsReversal As Boolean
        '== END  Target-New-Build-4277 -- (Started 07-October-2020)


        gbGetCreditNoteHistory = False
        '- get result of creditNotes -DebitNotes.
        '--  This can come from Refund,  or from Sale Transactions with extra payment.
        '--   Mutually exclusive with-CreditNote Amount Debited.
        '= sCreateSql &= "  creditNoteAmountCredited MONEY NOT NULL DEFAULT 0,"
        '--  This amount was spent in paying for the SALE..-
        '= sCreateSql &= "  creditNoteAmountDebited MONEY NOT NULL DEFAULT 0,"
        decCredits = 0
        decDebits = 0

        '=-4201.0531-
        decCreditNoteCreditRemaining = 0  '-4201.0531-

        '-- Get all Payments for customer that have Credit-Note credit or debit..
        sSql = "SELECT payments.customer_id, customer.barcode AS custBarcode, customer.isAccountCust, "
        sSql &= "  customerName = CASE companyName  "
        sSql &= "     WHEN '' THEN (customer.lastname + '.' + customer.firstname)"
        sSql &= "     WHEN 'n/a' THEN (customer.lastname + '.' + customer.firstname)"
        sSql &= "     ELSE companyName "
        sSql &= " END + '.' + customer.barcode, "  '--case- 
        sSql &= "  payments.payment_id, payments.payment_date, invoice_id, transactionType, payments.isReversal, "
        sSql &= "  creditNotePaymentCredited, refundAsCreditNoteCredited, "
        sSql &= " creditNoteAmountDebited, staff.docket_name "
        sSql &= "  FROM Payments  "
        sSql &= "   LEFT JOIN customer ON (customer.customer_id =payments.customer_id) "
        sSql &= "   LEFT JOIN staff on (payments.staff_id=staff.staff_id) "
        sSql &= "   WHERE  (( creditNotePaymentCredited<>0) OR (refundAsCreditNoteCredited<>0) "
        sSql &= "          OR (creditNoteAmountDebited<>0)) "
        '==   sSql &= "    AND ((creditNoteAmountCredited<>0) OR (creditNoteAmountDebited<>0)) "
        If (intCustomer_id > 0) Then
            sSql &= "   AND (payments.Customer_id=" & CStr(intCustomer_id) & ") "
        End If

        sSql &= " ORDER BY customerName, payment_id;"
        If Not gbGetDataTable(cnnSql, dtCreditNotes, sSql) Then
            MsgBox("Error in getting recordset for Payments table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        Else '-ok-
            If (Not (dtCreditNotes Is Nothing)) AndAlso (dtCreditNotes.Rows.Count > 0) Then
                '--have credit note history..
                '= For 4201.0727=  
                '==  MUST Round the CreditNote amounts (dr/cr) to take 3 (if any) decimals down to 2.
                For Each row1 As DataRow In dtCreditNotes.Rows
                    '==   Target-New-Build-4277 -- (Started 07-October-2020)
                    bIsReversal = (row1.Item("isReversal") <> 0)
                    '== END  Target-New-Build-4277 -- (Started 07-October-2020)

                    If (CDec(row1.Item("creditNotePaymentCredited")) <> 0) Then
                        decAmount = CDec(row1.Item("creditNotePaymentCredited"))

                        '==   Target-New-Build-4277 -- (Started 07-October-2020)
                        '== -- decCredits += Math.Round(decAmount, 2, MidpointRounding.AwayFromZero)
                        If bIsReversal Then
                            decCredits -= Math.Round(decAmount, 2, MidpointRounding.AwayFromZero)
                        Else  '-not reversal-
                            decCredits += Math.Round(decAmount, 2, MidpointRounding.AwayFromZero)
                        End If  '-reversal-
                        '== END Target-New-Build-4277 -- (Started 07-October-2020)

                    ElseIf (CDec(row1.Item("refundAsCreditNoteCredited")) <> 0) Then
                        decAmount = CDec(row1.Item("refundAsCreditNoteCredited"))
                        decCredits += Math.Round(decAmount, 2, MidpointRounding.AwayFromZero)
                        '= decCredits += CDec(row1.Item("refundAsCreditNoteCredited"))
                    ElseIf (CDec(row1.Item("creditNoteAmountDebited")) <> 0) Then
                        decAmount = CDec(row1.Item("creditNoteAmountDebited"))

                        '==   Target-New-Build-4277 -- (Started 07-October-2020)
                        '== ---- decDebits += Math.Round(decAmount, 2, MidpointRounding.AwayFromZero)
                        If bIsReversal Then
                            decDebits -= Math.Round(decAmount, 2, MidpointRounding.AwayFromZero)
                        Else '-not-
                            decDebits += Math.Round(decAmount, 2, MidpointRounding.AwayFromZero)
                        End If  '-reversal-
                        '== END  Target-New-Build-4277 -- (Started 07-October-2020)

                    End If
                Next  '--row1-
                decCreditNoteCreditRemaining = decCredits - decDebits
            End If  '--nothing-
            gbGetCreditNoteHistory = True
        End If '= get-

    End Function  '-gbGetCreditNoteHistory--
    '= = = = = = = = = = = = = = = == = = = = = 
    '-===FF->

End Module  '-modPOS31Support-
'= = = =  = = = =  = = == = =

'== end module=
