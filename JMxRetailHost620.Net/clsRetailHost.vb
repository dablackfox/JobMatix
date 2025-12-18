Option Strict Off
Option Explicit On
Imports System.Data
Imports System.Data.OleDb


'== grh 08-June-2021-  
'== This Is theInterface to standardise interface to Retail Host Database..

' Copyright 2021 grhaas@outlook.com

'Licensed under the Apache License, Version 2.0 (the "License");
'you may Not use this file except In compliance With the License.
'You may obtain a copy Of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law Or agreed To In writing, software
'distributed under the License Is distributed On an "AS IS" BASIS,
'WITHOUT WARRANTIES Or CONDITIONS Of ANY KIND, either express Or implied.
'See the License For the specific language governing permissions And
'limitations under the License.

'= = = =  ==  = = = = == = = = = = = = = = = == = = = = = = = = = = = 


'-- 18-Jan-2018= Interface has gone to "modRetailHostIF..
'-- 18-Jan-2018= Interface has gone to "modRetailHostIF..
'-- 18-Jan-2018= Interface has gone to "modRetailHostIF..

'==
'==  NEW DLL- 4219 VERSION
'==    Created- 4219.1122 22-Nov-2019= 
'==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..
'==    THIS class now is PUBLIC..
'==    THIS class now is PUBLIC..
'==    THIS class now is PUBLIC..


''-- Interface to standardise interface to Retail Host Database..-
''---- THIS CLASS (module) for RetailManager ONLY.--

''--  All Host classes will IMPLEMENT this interface..
''--  All Host classes will IMPLEMENT this interface..

'Public Interface _clsRetailHost  '== 3101.1111 Now PUBLIC..--
'    ReadOnly Property connection() As OleDbConnection   '== ADODB.Connection
'    ReadOnly Property colTables() As Collection
'    ReadOnly Property DBname() As String
'    ReadOnly Property IsSqlServer() As Boolean
'    ReadOnly Property CanTrackSerials() As Boolean
'    ReadOnly Property SellPriceIncludesGST() As Boolean
'    ReadOnly Property StockTableColumnNameCat1() As String
'    ReadOnly Property StockTableColumnNameCat2() As String
'    ReadOnly Property CustomerSearchColumns() As Object
'    ReadOnly Property stockSearchColumns() As Object

'    '-- Jet Connect for MYOB Retail manager.-
'    Function connect(ByRef sServer As String, _
'                      ByRef sDSN As String, _
'                       ByRef sUid As String, _
'                        ByRef sPwd As String, _
'                        ByRef bNewPath As Boolean, _
'                        ByRef strSchema As String) As Boolean

'    '-- JobmatixPOS- Save Live Sql Connection Info..-
'    Sub SetConnection(ByVal sServer As String, _
'                         ByRef connection As OleDbConnection, _
'                         ByRef colTables As Collection, _
'                          ByVal DBname As String)

'    Function browseGetPrefColumns(ByVal strTablename As String, _
'                                   ByRef strHostTableName As String, _
'                                    ByRef colPrefs As Collection) As Boolean

'    Function getOriginalColumnName(ByVal strTableName As String, _
'                                         ByVal strGridColName As String) As String

'    Function browseGetSelectedRecord(ByVal strTablename As String, _
'                                      ByRef colSelectedRow As Collection, _
'                                        ByRef colFullRecord As Collection) As Boolean
'    '= New 3311.228=
'    Function staffLookup(ByRef colRecord As Collection) As Boolean
'    Function staffGetStaffRecord(ByVal strBarcode As String, _
'                                  ByVal lngId As Integer, _
'                                    ByRef colStaffInfo As Collection) As Boolean
'    '= New 3311.406=
'    '-- User Can supply Docket name as key..
'    Function staffGetStaffRecordEx(ByVal strBarcode As String, _
'                                    ByVal lngId As Integer, _
'                                    ByVal strDocketName As String, _
'                                     ByRef colStaffInfo As Collection) As Boolean
'    '= New 3311.731=
'    '-- Get full staff list...
'    Function staffGetStaffList(ByRef colRecordList As Collection) As Boolean

'    Function customerLookup(ByRef colRecord As Collection) As Boolean
'    Function customerGetCustomerList(ByRef colRecordList As Collection) As Boolean

'    Function customerGetCustomerRecord(ByVal strBarcode As String, _
'                                        ByVal lngId As Integer, _
'                                          ByRef colRecord As Collection) As Boolean
'    Function customerGetCustomerRecordEx(ByRef colBrowseRow As Collection, _
'                                          ByRef colRecord As Collection) As Boolean
'    Function customerGetSalesHistory(ByVal strBarcode As String, _
'                                       ByVal lngId As Integer, _
'                                         ByRef colRecordList As Collection) As Boolean
'    Function stockLookup(ByVal bServiceChargeRequested As Boolean, _
'                          ByVal sServiceChargeCat1 As String, _
'                            ByVal sServiceChargeCat2 As String, _
'                              ByRef colSelectedRecord As Collection) As Boolean
'    Function stockGetJobPartStockInfo(ByVal strBarcode As String, _
'                                       ByVal lngId As Integer, _
'                                         ByRef colRecord As Collection) As Boolean
'    Function stockGetStockRecord(ByVal strBarcode As String, _
'                                   ByVal lngId As Integer, _
'                                     ByRef colRecord As Collection) As Boolean
'    Function stockGetStockRecordEx(ByRef colBrowseRow As Collection, _
'                                    ByRef colRecord As Collection) As Boolean
'    Function stockGetStockList(ByRef colStockIdList As Collection, _
'                                 ByRef colRecordList As Collection) As Boolean
'    Function stockGetDistinctCategoryList(ByRef colCat1Cat2List As Collection) As Boolean

'    Function invoiceGetInvoiceInfo(ByVal strBarcode As String, _
'                                    ByVal lngGoodsId As Integer, _
'                                     ByRef colInvoiceInfo As Collection) As Boolean
'    Function invoiceGetStockItemInvoices(ByVal strBarcode As String, _
'                                          ByVal lngStockId As Integer, _
'                                           ByRef colRecordList As Collection) As Boolean
'    Function supplierLookup(ByRef colRecord As Collection) As Boolean
'    Function supplierGetSupplierRecord(ByVal strBarcode As String, _
'                                        ByVal lngId As Integer, _
'                                          ByRef colRecord As Collection) As Boolean
'    Function serialGetAllSerials(ByRef sArg As String, _
'                                  ByRef labStatus As Label, _
'                                   ByRef bCancelFlag As Boolean, _
'                                      ByRef colSerialsList As Collection, _
'                                       ByRef strErrorReport As String) As Boolean
'    '--= 3101.1024= Stock_id is optional (Send -1 if not being supplied)..
'    Function serialGetSerialInfo(ByVal intStock_id As Integer, _
'                                   ByVal sSerialNo As String, _
'                                     ByRef colSerialInfo As Collection) As Boolean
'    Function quoteGetAllQuotes(ByRef colQuotesList As Collection, _
'                       Optional ByVal mlCustomerSearchId As Integer = -1) As Boolean

'    Function quoteGetStocklist(ByRef order_id As Integer, _
'                                ByRef colStockList As Collection, _
'                                 ByRef colExpandInstances As Collection) As Boolean
'    Function closeConnection() As Boolean
'End Interface

Public Class clsRetailHost
    Implements _clsRetailHost

    '= = = = == == = == =

    '--  Class to encapsulate interface to Retail Host Database..-
    '---- THIS CLASS for RetailManager ONLY.--

    '--  Other Host classes will IMPLEMENT this interface..
    '--  Other Host classes will IMPLEMENT this interface..
    '--  Other Host classes will IMPLEMENT this interface..


    '-- grh-==06-Feb-2011== Created for Jobmatix for Multiple Retail Host System... ==
    '-- grh-==19-Oct-2011== Jobmatix V 3010... ==
    '-- grh-==25-Oct-2011== Some Fixes for VB.NET.. ==
    '-- grh-==11-Nov-2011== stockGetStocklist.. allow empty input stock list..==
    '----                      Will return FULL stock list..--

    '-- grh-==23-Nov-2011== Upgrade to vb.net...==
    '-- grh-==16-Dec-2011==  stockGetStockRecordEx--

    '-- grh-==22-Dec-2011== makeRecordCollection..  DRFAULT key is 1-based index..

    '-- grh-==27-Feb-2012==3031== Add GET properties "StockTableColumnNameCat1/2"..
    '-- grh-==29-Mar-2012==3031.11== Upgrade msg text for failure to connect to db...

    '== grh-==11-Apr-2012==3041.1== 
    '- --  FIX "customerGetCustomerRecordEx"  to pick up cust. barcode..--
    '==     and Make Barcode the 1st Browser col..
    '==
    '== grh-==01-May-2012==3049.0== 
    '==  Remove popup msgbox msg for Serail-no not found..-

    '== grh= 02Jun2012= 3059.1= 
    '==  CustomerGetCustomerRecord..  Give preference to Customer_id if provided..==
    '==
    '== grh= 14May2013= 3077.514= 
    '==  serialGetAllSerials..  Add references to Status Label and Cancel Flag..==
    '==                         Use static mRst With Events to track progress.. 
    '==
    '== grh= 21Mar2014= 3083.321= 
    '==           >>  Add "date_modified" to Customer collection..
    '==
    '== grh= 18Jun2014= 3083.618= 
    '==           >>  Customer collection..  "Surname" back to 1st column..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '== 
    '==  grh. JobMatix 3.1.3101 ---  14-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb 
    '==           (dropped sqlClient).. (For Jet OleDb driver).
    '==
    '==   >>  3.1.3101.928 - Add Sub SetConnection for JobMatixPOS Live SqlConnection.
    '==
    '==
    '==   grh  3.1.3101.1014 - 
    '==     >> Add "stock_id" input parm to "serialGetSerialInfo".
    '==
    '==   grh  3.1.3101.1025 - 
    '==     >> Add property stockSearchColumns for Browse fulltext search on Stock.
    '==
    '==   grh  3.1.3107.706 - 
    '==     >> Add "barcode" to property customerSearchColumns for Browse fulltext search..
    '==
    '==   grh  3.1.3107.0801 -  01-Aug-2015 
    '==     >> Connect Function. added extra parm "strSchema" to return schema text display to Main..
    '==
    '==   grh  3.1.3107.1007 -  07-Oct-2015 
    '==     >> REMOVE DUPLICATE  mColPrefsSupplier.Add("supplier") (was crashing Browser)-
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW VERSION--
    '==
    '==   grh  3.3.3311.228 -  28-Feb-2016- 
    '==     >> Implement Staff Lookup..
    '==
    '==  3311.302- 02Mar2016-
    '==
    '==    >>  Update clsRetailHost/--JMPOS (mbGetSerialInfo) -
    '==            - to add SALE/Customer Info (if any) to SerialInfo for caller.
    '==  3311.324- 24Mar2016-
    '==          >> Fix SerialInfo SALE/Customer Info to Standard return format..
    '==
    '==  = New 3311.406=
    '==   -- staffGetStaffRecordEx. User Can supply Docket name as key..
    '==
    '==  -- 3311.731- 31July2016-
    '==        >> Updates to Onsite SMS Reminder..  need new method (get Staff List).-
    '==
    '==  NEW DLL- 4219 VERSION
    '==    Created- 4219.1122 22-Nov-2019= 
    '==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..
    '==    THIS class now is PUBLIC..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (16-June-2021)
    '==   Target-New-Build-6201 --  (16-June-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    '--P u b l i c  interface--
    '--
    '--  Note: all results are returned as collections with RetailManager column names..-
    '----- A. Single records are returned as a collection of field collections
    '----                (Each field is a collection "name"/"value"/"type"(ADO type)/"size"(DefinedSize) ---
    '----- B.  Multiple record results are returned as a collection of records of type A. above..

    '= = = = = = = = = = = = = = =

    Private msProvider As String '-- eg: "RetailManager", "JobMatix2"..
    Private mbConnected As Boolean
    Private msAppPath As String

    '-- RetailManager stuff..--
    '-- RetailManager stuff..--
    '-- RetailManager stuff..--

    Private mCnnJet As OleDbConnection   '== ADODB.Connection
    Private mColJetDBInfo As Collection

    Private msJetDbName As String
    Private msJetUid As String
    Private msJetPwd As String

    Private msStockSelectSql As String

    '--  Browser prefs..--

    Private mColPrefsStaff As Collection
    Private mColPrefsCustomer As Collection
    Private mColPrefsStock As Collection
    Private mColPrefsSupplier As Collection
    '= = = = = = = = = = = = = = = = = = = =

    '-- get all serials.-
    '== Private WithEvents mRst As ADODB.Recordset
    '=oleDb= Private WithEvents mRstSerials As ADODB.Recordset
    '=oleDb= Private WithEvents mRstStockedSerials As ADODB.Recordset
    Private mRstSerials As DataTable
    Private mRstStockedSerials As DataTable

    Private mLabStatus As System.Windows.Forms.Label

    Private mlFetchComplete As Integer = -1
    Private msFetchError As String = ""
    Private mlRecCount As Integer = 0
    '= = = = = = = = = = = = = = == = =  = =
    '-===FF->

    '--  expose these for the browser..--
    '--  expose these for the browser..--

    '==oleDb= ReadOnly Property connection() As ADODB.Connection Implements _clsRetailHost.connection
    ReadOnly Property connection() As OleDbConnection Implements _clsRetailHost.connection
        Get
            connection = mCnnJet

        End Get
    End Property '---cnn--
    '= = = = = = = =  == =

    ReadOnly Property colTables() As Collection Implements _clsRetailHost.colTables
        Get

            colTables = mColJetDBInfo
        End Get
    End Property '--tables..-
    '= = = = = = = = = = = = =

    ReadOnly Property DBname() As String Implements _clsRetailHost.DBname
        Get

            DBname = msJetDbName
        End Get
    End Property '--dbname-
    '= = = = = = = = = = = = = =

    ReadOnly Property IsSqlServer() As Boolean Implements _clsRetailHost.IsSqlServer
        Get

            IsSqlServer = False '-- we are Jet..--(R.M.)..
        End Get
    End Property '--issql-
    '= = = = = = = = = = =

    ReadOnly Property CanTrackSerials() As Boolean Implements _clsRetailHost.CanTrackSerials
        Get

            CanTrackSerials = True  '-- we are -(R.M.)..
        End Get
    End Property '--issql-
    '= = = = = = = = = = =
    ReadOnly Property SellPriceIncludesGST() As Boolean Implements _clsRetailHost.SellPriceIncludesGST
        Get

            SellPriceIncludesGST = False '-- we are -(R.M.)..
        End Get
    End Property '--issql-
    '= = = = = = = = = = =

    ReadOnly Property StockTableColumnNameCat1() As String Implements _clsRetailHost.StockTableColumnNameCat1
        Get

            StockTableColumnNameCat1 = "cat1"
        End Get
    End Property '--dbname-
    '= = = = = = = = = = = = = =

    ReadOnly Property StockTableColumnNameCat2() As String Implements _clsRetailHost.StockTableColumnNameCat2
        Get

            StockTableColumnNameCat2 = "cat2"
        End Get
    End Property '--dbname-
    '= = = = = = = = = = = = = =

    '--CustomerSearchColumns--

    ReadOnly Property CustomerSearchColumns() As Object Implements _clsRetailHost.CustomerSearchColumns
        Get
            '-   for MYOB..--
            CustomerSearchColumns = New Object() _
                     {"barcode", "surname", "given_names", "company", "addr1", "addr2", "addr3", "suburb", "email"}
        End Get
    End Property '--dbname-
    '= = = = = = = = = = = = = =

    '-- Stock srch columns.-

    ReadOnly Property stockSearchColumns() As Object Implements _clsRetailHost.stockSearchColumns
        Get
            '-   for MYOB..--
            stockSearchColumns = New Object() _
                     {"barcode", "cat1", "cat2", "description"}
        End Get
    End Property '--dbname-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--initialise--==
    '--initialise--==

    'UPGRADE_NOTE: class_initialize was upgraded to class_initialize_Renamed. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'

    Private Sub Class_Initialize_Renamed()

        mbConnected = False
        msProvider = ""

        msJetDbName = ""
        msJetUid = ""
        msJetPwd = ""

        msAppPath = My.Application.Info.DirectoryPath
        If Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"


        '--  Retail Manager Stock columns---
        '-- FOR Job Parts SPECIAL --
        '--   stockGetJobPartStockInfo --
        msStockSelectSql = " SELECT  CategoryValues.description AS cat3,  cat1, cat2, Barcode, allow_renaming, " & _
                            " stock.description AS OriginalDescription, stock.Stock_id, " & _
                              " stock.longDesc, cost, sell AS OriginalSell , quantity FROM stock " & _
                              " LEFT JOIN (CategorisedStock " & _
                               " LEFT JOIN  CategoryValues  ON ( CategoryValues.catValue_id=    CategorisedStock.catValue_id) " & _
                               "   )     ON ((stock.stock_id=CategorisedStock.stock_id) AND (CategorisedStock.category_level=3)) "
        '==            " WHERE (barcode='" & s1 & "')"


        '--  set up browser prefs..--
        '--  staff--
        mColPrefsStaff = New Collection
        mColPrefsStaff.Add("docket_name")
        mColPrefsStaff.Add("barcode")
        mColPrefsStaff.Add("surname")
        mColPrefsStaff.Add("given_names")
        mColPrefsStaff.Add("staff_id")

        '-- Customer --
        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("surname")
        mColPrefsCustomer.Add("given_names")
        mColPrefsCustomer.Add("date_modified")
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("company")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("customer_id")
        mColPrefsCustomer.Add("account")
        mColPrefsCustomer.Add("addr1")
        mColPrefsCustomer.Add("suburb")
        mColPrefsCustomer.Add("email")

        '--  stock--
        mColPrefsStock = New Collection
        mColPrefsStock.Add("description")
        mColPrefsStock.Add("cat1")
        mColPrefsStock.Add("cat2")
        mColPrefsStock.Add("barcode")
        mColPrefsStock.Add("sell")
        mColPrefsStock.Add("stock_id")
        mColPrefsStock.Add("allow_renaming")

        '--  supplier--
        mColPrefsSupplier = New Collection
        mColPrefsSupplier.Add("supplier")
        mColPrefsSupplier.Add("supplier_id")
        mColPrefsSupplier.Add("barcode")
        '= 3107.1007= mColPrefsSupplier.Add("supplier")
        mColPrefsSupplier.Add("main_addr1")
        mColPrefsSupplier.Add("main_addr2")
        mColPrefsSupplier.Add("main_addr3")
        mColPrefsSupplier.Add("main_suburb")
        mColPrefsSupplier.Add("main_state")
        mColPrefsSupplier.Add("main_postcode")
        mColPrefsSupplier.Add("main_country")
        mColPrefsSupplier.Add("main_phone")


    End Sub  '-init-
    '= = = = = = = = = = = = = = = = = = =  =

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub '--initialise..-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- R e t a i l M a n a g er --
    '-- Connect to Retail Manager..--

    Private Function mbRMConnect(ByRef bNewPath As Boolean, ByRef sConnectLog As String) As Boolean
        '== Dim bNewPath As Boolean

        bNewPath = False
        mbRMConnect = False
        '-- 22Jan2010-- fixing v2.0.2371..--
        '---- Retry in case original DB path invalid.--
        While Not gbLoginJet(mCnnJet, msJetDbName, mColJetDBInfo, msJetUid, msJetPwd, sConnectLog)
            If MsgBox("JobMatix failed to connect to a RetailManager (Jet) Database.. " & vbCrLf & vbCrLf & _
                      "The Path: '" & msJetDbName & "'  may not be valid," & vbCrLf & _
                        "   or you may not have permissions to access this file.." & vbCrLf & vbCrLf & _
                       "Do you want to retry the RM logon with a new path?", _
                        MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then '--retry.. get new path..
                msJetDbName = ""
                bNewPath = True '--case new path succeeds..-
            Else
                Exit Function
            End If ''yes.-
        End While '--logon.-
        '--MsgBox "Connection Control=" + CStr(mCnnJet.Properties("Jet OLEDB: Connection Control"))
        On Error GoTo 0
        mbRMConnect = True
    End Function '--RM-connect..-
    '= = = = = = =
    '-===FF->

    '-- R e t a i l M a n a g er --
    '-- R e t a i l M a n a g er --
    '-- lookup  RM table..--
    '-- lookup  RM table..--
    Private Function mbRMGetRecord(ByRef sSql As String, _
                                    ByRef colFields As Collection) As Boolean
        Dim colFld As Collection '--"name"=, "value"-
        '= Dim fld1 As ADODB.Field
        Dim s1, sName, sSqlType As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim intADOType As Integer

        mbRMGetRecord = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJet, rs1, sSql) Then
            MsgBox("Failed to get recordset for SQL:" & vbCrLf & sSql & vbCrLf, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '--txtMessages.Text = ""
        If Not (rs1 Is Nothing) Then
            If (rs1.Rows.Count > 0) Then  '== Not (rs1.BOF And rs1.EOF) Then '---not empty..-
                '==rs1.MoveFirst()
                '== If (Not rs1.EOF) Then '---And (cx < 100)
                '--return complete row..-
                Dim dataRow1 As DataRow = rs1.Rows(0)  '--first row..-
                colFields = New Collection
                For Each column1 As DataColumn In rs1.Columns '== fld1 In rs1.Fields
                    sName = column1.ColumnName
                    colFld = New Collection
                    colFld.Add(LCase(sName), "name")
                    If Not IsDBNull(dataRow1.Item(sName)) Then
                        colFld.Add(dataRow1.Item(sName), "value")
                    Else '--null-
                        colFld.Add("", "value")
                    End If
                    '--call global to convert Column-datatype to ADO Type and SqlType..--
                    gbConvertDotNetDataType(column1, intADOType, sSqlType)

                    colFld.Add(intADOType, "type")
                    colFld.Add(sSqlType, "sqltype")
                    colFld.Add(column1.MaxLength, "DefinedSize")
                    '--colFld.Add(fld1.DefinedSize, "DefinedSize")
                    colFields.Add(colFld, LCase(sName))
                Next column1 '- fld1
                mbRMGetRecord = True
                '== Else '--not found-
                '== End If '-eof-
            End If '--empty..-
        End If '--rs-
        rs1 = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

    End Function '--RM lookup..--
    '= = = = = = = = = = = = =
    '-===FF->

    '--  convert recordset to collection of collections..--
    '-----  Optionally add key to each record..

    Private Function mbMakeRecordCollection(ByRef rs1 As DataTable, _
                                             ByRef colRecordList As Collection, _
                                             Optional ByVal strKeyColumn As String = "") As Boolean
        Dim colRecord As Collection
        Dim col1 As Collection
        '==Dim fldx As ADODB.Field
        Dim lngCount As Integer = 0
        Dim intADOType As Integer
        Dim sName, sKey, sSqlType As String

        mbMakeRecordCollection = False
        If Not (rs1 Is Nothing) Then
            colRecordList = New Collection
            If (rs1.Rows.Count > 0) Then   '=Not (rs1.BOF And rs1.EOF) Then '--not empty..-
                '= rs1.MoveFirst()
                For Each datarow1 As DataRow In rs1.Rows
                    lngCount += 1   '-- count records for default key..-
                    colRecord = New Collection
                    For Each column1 As DataColumn In rs1.Columns '== fldx In rs1.Fields
                        sName = column1.ColumnName
                        col1 = New Collection
                        col1.Add(sName, "name")
                        If IsDBNull(datarow1.Item(sName)) Then
                            col1.Add("null", "value")
                        Else  '--ok-
                            col1.Add(Trim(datarow1.Item(sName)), "value")
                        End If

                        '--call global to convert Column-datatype to ADO Type and SqlType..--
                        gbConvertDotNetDataType(column1, intADOType, sSqlType)

                        col1.Add(sSqlType, "sqltype")
                        col1.Add(intADOType, "type")
                        col1.Add(column1.MaxLength, "DefinedSize")
                        colRecord.Add(col1, UCase(sName))
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
            mbMakeRecordCollection = True
        End If '-nothing-

    End Function '--make --
    '= = = = = = = = = = = = =
    '-===FF->

    '--  Refresh MYOB R-M Purchases List.--

    Private Function mbRefreshPurchasesList(ByVal lngId As Integer, _
                                             ByRef rs1 As DataTable) As Boolean
        '==Dim lngCount As Integer
        Dim sSql As String
        Dim sWhere1 As String

        mbRefreshPurchasesList = False

        '--  NOTE:  MYOB RM dockets have Customer_Id ONLY. (No Customer barcode.)--
        '--  NOTE:  MYOB RM dockets have Customer_Id ONLY. (No Customer barcode.)--

        If (lngId >= 0) Then '--was supplied..-
            sWhere1 = "(docket.customer_Id= " & CStr(lngId) & ")"
        Else
            MsgBox("No Customer ID supplied for Get Purchases..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        sSql = " SELECT docket.docket_id, docket_date, total_inc, "
        sSql = sSql & "   Cat1, Cat2, Description, barcode,  docket.transaction,  "
        sSql = sSql & "  DocketLine.stock_id, DocketLine.quantity, "
        sSql = sSql & "      sell_inc, docket.customer_Id "
        sSql = sSql & "  FROM (Docket "
        sSql = sSql & "    LEFT JOIN (DocketLine "
        sSql = sSql & "         LEFT JOIN Stock"
        sSql = sSql & "             ON (stock.stock_id=DocketLine.stock_id) )"
        sSql = sSql & "         ON  (DocketLine.docket_id=docket.docket_id) ) "
        sSql = sSql & " WHERE " & sWhere1 & " AND  "
        sSql = sSql & "                        ((Transaction='SA') OR (Transaction='IV')) "
        sSql = sSql & "   ORDER BY docket.docket_id DESC; "

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If gbGetDataTable(mCnnJet, rs1, sSql) Then '--ok-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '== LabPurchases.Caption = "Purchase History (" & lngCount & " Retail Manager Docket Lines)"
            mbRefreshPurchasesList = True
        Else '--failed..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Dockets recordset..", MsgBoxStyle.Exclamation)
        End If

    End Function '--refresh..-
    '= = = = = = = = = = =  =  = =  =

    '--  R e t a i l M a n a g e r ---
    '--  R e t a i l M a n a g e r ---

    '-- Get list of R-M Serial Audit Table..-
    '-- Get list of R-M Serial Audit Table..-
    '--  EX frmFindPart..--

    Private Function mbGetAllSerials(ByRef sArg As String, _
                                      ByRef labStatus As Label, _
                                       ByRef bCancelRequested As Boolean, _
                                        ByRef colSerials As Collection, _
                                         ByRef strErrorReport As String) As Boolean
        Dim ix, k, i, j, lCount, idx As Integer
        Dim lngBlanksCount As Integer
        Dim lngStart, lngNewStart As Integer
        Dim lngNotFound, lngId, intADOType As Integer
        Dim lngError, lngSerialCount As Integer
        Dim lngStockCount, lngStockIdx As Integer
        Dim sSql, sSrchSql As String
        '== Dim sArg As String
        Dim sSerialNo, sStockedSerialNumbers As String
        Dim s1, sKey, sName, sSqlType, sMsg As String
        Dim asColumns As Object
        '==Dim item1 As ListItem
        '== Dim rstSerials As ADODB.Recordset
        '== Dim rstStockedSerials As ADODB.Recordset
        Dim rs1 As DataTable '= ADODB.Recordset
        '= Dim fldx As ADODB.Field
        Dim col1 As Collection
        Dim colGoods As Collection
        Dim colRecord As Collection
        Dim colInvoice As Collection

        mbGetAllSerials = False
        strErrorReport = ""
        On Error GoTo RefreshSerials_Error
        mLabStatus = labStatus    '--make accessible..

        '== sArg = Trim(txtSerialArg.Text)
        sSql = ""
        '--  make srch arg.. sql..-
        asColumns = New Object() {"SerialAudit.number", "Barcode", "Cat1", "stock.description"}
        sSrchSql = gbMakeTextSearchSql(sArg, asColumns)

        labStatus.Text = "Fetching Serial-Audit RecordSet.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '--  Get full list SerialAudit table.. ( Type 'GR' only.)--
        '---- SerialAudit links SerailNo to SerialAuditTrail, which --
        '---    gives Stock_id, Goods_Id and and GoodsLine_Id..--

        sSql = " SELECT  SerialAudit.number AS SerialNo,  '--' AS InStock,"
        '==sSql = sSql + "  StockedSerials.number AS InStock, "
        sSql = sSql & "  cat1, stock.Barcode, stock.description,  "
        sSql = sSql & "   SerialAuditTrail.Type_id AS Goods_Id, "
        sSql = sSql & "  '--' AS InvoiceNo, '--' AS InvoiceDate, '--' AS Supplier, "
        sSql = sSql & "  SerialAuditTrail.stock_id AS stock_id,  "
        '==sSql = sSql + "     SerialAuditTrail.Type_Line_id AS GoodsLine_Id, "
        sSql = sSql & "       stock.Stock_id,  cost "
        sSql = sSql & "  FROM SerialAudit "
        sSql = sSql & "     LEFT JOIN (SerialAuditTrail "
        sSql = sSql & "         LEFT JOIN Stock  "
        sSql = sSql & "          ON (Stock.stock_id=SerialAuditTrail.stock_id) ) "
        sSql = sSql & "     ON (SerialAuditTrail.SerialAudit_Id=SerialAudit.SerialAudit_Id)  "
        sSql = sSql & "       WHERE  ((SerialAuditTrail.Type='GR') OR (SerialAuditTrail.Type='SI')) "
        '=====sSql = sSql + "       WHERE (SerialAudit.number='" + msSerialNo + "') AND  (SerialAuditTrail.Type='GR') "
        '===If sArg <> "" Then
        '===    sSql = sSql + " AND ((SerialAudit.number LIKE '%" & sArg & "%') OR (Barcode LIKE '%" & sArg & "%') OR  " & _
        ''===                            " (Cat1 LIKE '%" & sArg & "%') OR  (stock.description LIKE '%" & sArg & "%') ) "
        '===End If
        If sSrchSql <> "" Then
            sSql = sSql & " AND " & sSrchSql
        End If
        sSql = sSql & " ORDER BY SerialAudit.number "
        '====MsgBox "SQL is: " + vbCrLf + sSql
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mlFetchComplete = -1 '--not ready-
        '--mRst.Properties("Initial Fetch Size") = 0       '--ensure progress event--
        '--mRst.Properties("Background Fetch Size") = 15  '--default--
        mRstSerials = New DataTable  '= ADODB.Recordset
        '= mRstSerials.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        '--lBG = Labwhere.BackColor       '--save--
        '--Labwhere.BackColor = vbYellow  '--progress--
        '--On Error Resume Next
        '== On Error GoTo GetSerials_rset_error
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '== mRstSerials.Open(sSql, mCnnJet, ADODB.CursorTypeEnum.adOpenStatic, _
        '==                     ADODB.LockTypeEnum.adLockReadOnly, ADODB.ExecuteOptionEnum.adAsyncFetch)
        '== '--wait for completion event--
        '== On Error GoTo 0

        '== While (mlFetchComplete < 0) '--Wait for fetchComplete event..--
        '==  System.Windows.Forms.Application.DoEvents()
        '= End While '--fetch--

        If Not gbGetDataTable(mCnnJet, mRstSerials, sSql) Then  '==  (mlFetchComplete <> 0) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            sMsg = "ERROR getting recordset for SerialAudi table.." & vbCrLf & _
                     "Error " & gsGetLastSqlErrorMessage() & vbCrLf & _
                     "SQL was: " & vbCrLf & sSql & vbCrLf & "=== end error msg ===" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sMsg)
            strErrorReport = sMsg
            mRstSerials = Nothing
            '== MsgBox(sMsg, MsgBoxStyle.Exclamation)
            '--Me.Hide
            Exit Function
        Else '--ok--
            '--MsgBox "ok.. got " & rs1.RecordCount & " records.."
            '==DoEvents
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        On Error GoTo RefreshSerials_Error

        lngSerialCount = 0
        lngBlanksCount = 0

        '--  load initial result collection for caller..-
        colSerials = New Collection
        If (mRstSerials Is Nothing) OrElse (mRstSerials.Rows.Count <= 0) Then
            '=(mRstSerials.BOF And mRstSerials.EOF) Then '--empty os ok..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '===MsgBox "No records for SerialAudit recordset.." + vbCrLf + sSql, vbCritical
            mRstSerials = Nothing
            Exit Function
        End If
        labStatus.Text = "Building Initial Serial Collection.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '===  Call mbLoadListView(mRstSerials, ListView1)
        '== mRstSerials.MoveFirst()
        For Each datarow1 As DataRow In mRstSerials.Rows
            colRecord = New Collection
            sSerialNo = datarow1.Item("SerialNo")
            If (sSerialNo <> "") Then '--ignore blank serials.--
                For Each column1 As DataColumn In mRstSerials.Columns  '= fldx In mRstSerials.Fields
                    col1 = New Collection
                    sName = column1.ColumnName
                    col1.Add(sName, "name")
                    col1.Add(Trim(datarow1.Item(sName)), "value")
                    '--call global to convert Column-datatype to ADO Type and SqlType..--
                    gbConvertDotNetDataType(column1, intADOType, sSqlType)
                    col1.Add(intADOType, "type")
                    col1.Add(sSqlType, "sqltype")
                    col1.Add(column1.MaxLength, "DefinedSize")

                    colRecord.Add(col1, UCase(sName))
                Next column1 '=fldx
                '--add this record-
                colSerials.Add(colRecord) '==s/n NOT unique== , sSerialNo   KEY=SerialNo-.-
                lngSerialCount = lngSerialCount + 1
            Else
                lngBlanksCount = lngBlanksCount + 1
            End If '--blank.-
        Next datarow1

        '=== LabStatus.Caption = "Looking up Stocked-Serials.."
        '--  now get list of stockd serials and Upddate ListView with "InStock" status..--
        sSql = " SELECT StockedSerials.number, StockedSerials.SerialAudit_Id AS Audit_id,  "
        sSql = sSql & "   SerialAuditTrail.stock_id AS stock_id  "
        '==sSql = sSql + "        ,cat1, cat2, stock.Barcode, stock.description "
        sSql = sSql & "  FROM StockedSerials  "
        sSql = sSql & "     LEFT JOIN SerialAuditTrail "
        '==sSql = sSql + "         LEFT JOIN Stock  ON (Stock.stock_id=SerialAuditTrail.stock_id) "
        sSql = sSql & "       ON (SerialAuditTrail.SerialAudit_Id=StockedSerials.SerialAudit_Id) "
        sSql = sSql & "    WHERE  ((SerialAuditTrail.Type='GR')  OR (SerialAuditTrail.Type='SI')) "
        sSql = sSql & " ORDER BY StockedSerials.number "
        '====MsgBox "SQL is: " + vbCrLf + sSql
        labStatus.Text = "Fetching Stocked Serials R-Set.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Not gbGetDataTable(mCnnJet, mRstStockedSerials, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get StockedSerials recordset.." & vbCrLf & sSql, MsgBoxStyle.Critical)
            '==mbCancelled = True
            '--Me.Hide
            mRstStockedSerials = Nothing
            '== Exit Function
        Else '--ok--
            '--update listview with stock status..-
            If (Not (mRstStockedSerials Is Nothing)) AndAlso (mRstStockedSerials.Rows.Count > 0) Then '--ok.. not empty--
                '== Not (mRstStockedSerials.BOF And mRstStockedSerials.EOF) Then '--ok.. not empty--
                '=== If (ListView1.ListItems.Count > 0) Then
                If (colSerials.Count() > 0) Then
                    lngNotFound = 0
                    lngNewStart = 1 '--starting index in listview..--
                    '-  NB! ListView.ListItems collection items are numbered from 1..n--
                    '-- For each stocked serial.. find it in the Audit listview..=
                    labStatus.Text = "Building List of Stocked Serials.."
                    System.Windows.Forms.Application.DoEvents()
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    '== mRstStockedSerials.MoveFirst()
                    '== mRstStockedSerials.MoveLast()
                    lngStockCount = mRstStockedSerials.Rows.Count '= mRstStockedSerials.RecordCount
                    '= mRstStockedSerials.MoveFirst()
                    lngStockIdx = 0
                    sStockedSerialNumbers = vbTab
                    '== 3077-  make string list of stocked serials.
                    For Each datarow1 As DataRow In mRstStockedSerials.Rows
                        If Not IsDBNull(datarow1.Item("number")) Then
                            sSerialNo = UCase(Trim(datarow1.Item("number")))
                            If (sSerialNo <> "") Then
                                sStockedSerialNumbers = sStockedSerialNumbers & sSerialNo & vbTab
                            End If '--serial no..-
                        End If '--null..-
                    Next datarow1
                    '== While (Not mRstStockedSerials.EOF)
                    '==   mRstStockedSerials.MoveNext()
                    '== End While '-eof-
                    If gbDebug Then
                        MsgBox("Processed " & mRstStockedSerials.Rows.Count & " stock serials.." & vbCrLf & _
                                                              " And " & lngNotFound & " not found..", MsgBoxStyle.Information)
                    End If
                    '--srch serials collection.  both arg lists are ascending SerialNo...--
                    '==3077== -- Check eack serial against Stocked list..
                    labStatus.Text = "Updating Serials with Stock Status.."
                    System.Windows.Forms.Application.DoEvents()
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    lngStart = lngNewStart
                    For idx = lngStart To colSerials.Count()
                        '==Set item1 = ListView1.ListItems(idx)
                        colRecord = colSerials.Item(idx) '--get next record..
                        col1 = colRecord.Item("SERIALNO") '-- get serialno fld set..-
                        s1 = UCase(col1.Item("value"))
                        '==3077== If (sSerialNo < UCase(col1.Item("value"))) Then '= Trim(UCase(item1.Text)))  '--not yet...--
                        '==3077== lngNewStart = idx '--mark tide, keep going..--
                        lngStockIdx += 1
                        If (lngStockIdx Mod 10 = 0) Then  '--update status every 10..
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                            labStatus.Text = "WAIT.. " & CStr(lngSerialCount) & _
                                  " Serials- Updating InStock: " & lngStockIdx & "/" & lngSerialCount & ".."
                            System.Windows.Forms.Application.DoEvents()
                            '== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                            If bCancelRequested Then
                                '= mRstStockedSerials.Close()
                                mbGetAllSerials = True
                                Exit Function
                            End If
                        End If
                        '==3077== ElseIf (sSerialNo = UCase(col1.Item("value"))) Then  '==Trim(UCase(item1.Text))) '--  found...--
                        If InStr(sStockedSerialNumbers, vbTab & s1 & vbTab) > 0 Then '--found- 
                            '==item1.SubItems(1) = "YES"
                            col1 = colRecord.Item("InStock") '--fld collection for instock..--
                            col1.Remove(("value"))
                            col1.Add("YES", "value") '--re-add "value" item with the new value.-
                            lngNewStart = idx '--resume srch next item from here..-
                            sSerialNo = "" '--found.-
                        End If '-instr-
                        '==3077== '=Exit For
                        '==3077==     ElseIf (sSerialNo > UCase(col1.Item("value"))) Then  '== Trim(UCase(item1.Text)))  '--too far..--
                        '==3077== '-- keep lookiing....--
                        '==3077==     End If
                    Next idx '--next in s-audit list..=
                    If (sSerialNo <> "") Then
                        lngNotFound = lngNotFound + 1
                        lngNewStart = 1 '--start from beginning for next one..-
                    End If
                    '=== ListView1.SelectedItem = ListView1.ListItems(1)
                End If '--count..-
            End If '--empty..-
            '== mbRefreshSerials = True
        End If '--get Stocked ser...-

        labStatus.Text = "Fetching Goods Invoices.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '-- load collection of goods invoices to get Invoice Info..--
        System.Windows.Forms.Application.DoEvents()
        sSql = "SELECT Goods_Id, Invoice_No, Invoice_Date, Supplier.Supplier AS Supplier  "
        sSql = sSql & " FROM Goods LEFT JOIN Supplier ON (Goods.Supplier_Id=Supplier.Supplier_Id)  "
        If Not gbGetDataTable(mCnnJet, rs1, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Goods recordset.." & vbCrLf & sSql, MsgBoxStyle.Critical)
            '=== mbCancelled = True
        Else '--ok--
            colGoods = New Collection
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then  '--ok.. not empty--
                '==-Not (rs1.BOF And rs1.EOF) Then '--ok.. not empty--
                '= rs1.MoveFirst()
                '--build Goods collection.-
                For Each datarow1 As DataRow In rs1.Rows
                    col1 = New Collection
                    lngId = CInt(datarow1.Item("Goods_Id"))
                    s1 = datarow1.Item("Invoice_No")
                    col1.Add(s1, "InvoiceNo")
                    s1 = datarow1.Item("Invoice_Date")
                    col1.Add(s1, "InvoiceDate")
                    s1 = datarow1.Item("Supplier")
                    col1.Add(s1, "Supplier")
                    colGoods.Add(col1, Trim(CStr(lngId)))
                Next datarow1
                '== While Not rs1.EOF
                '==  rs1.MoveNext()
                '== End While
            End If '--empty..-
            '=== If (ListView1.ListItems.Count > 0) Then
            '===  LabStatus.Caption = "Updating Goods Received info..."
            System.Windows.Forms.Application.DoEvents()
            '--  update invoice columns IN SERIALS collection..--
            '==  For idx = 1 To ListView1.ListItems.Count
            For Each colRecord In colSerials
                '==Set item1 = ListView1.ListItems(idx)
                col1 = colRecord.Item("Goods_Id")
                sKey = col1.Item("value") '====  Trim(item1.SubItems(5))  '--goods id..-
                If IsNumeric(sKey) Then
                    If (CInt(Val(sKey)) > 0) Then '--valid goodsid.-
                        On Error Resume Next
                        colInvoice = colGoods.Item(sKey) '--get info this invoice..-
                        lngError = Err().Number
                        On Error GoTo 0 '=====   RefreshSerials_Error
                        If (lngError = 0) Then '--found in goods invoices...-
                            s1 = colInvoice.Item("InvoiceNo")
                            '==  item1.SubItems(6) = s1  '--load listview..-
                            '--  update InvoiceNo column ..
                            col1 = colRecord.Item("InvoiceNo")
                            col1.Remove(("value"))
                            col1.Add(s1, ("value")) '--replace value item.-

                            '--  update InvoiceDate column ..
                            s1 = colInvoice.Item("InvoiceDate")
                            col1 = colRecord.Item("InvoiceDate")
                            col1.Remove(("value"))
                            col1.Add(s1, ("value")) '--replace value item.-
                            '==item1.SubItems(7) = s1

                            s1 = colInvoice.Item("Supplier")
                            '==item1.SubItems(8) = s1
                            '--  update InvoiceDate column ..
                            col1 = colRecord.Item("Supplier")
                            col1.Remove(("value"))
                            col1.Add(s1, ("value")) '--replace value item.-
                        Else '--not found..-
                            '=== item1.SubItems(6) = "NOT FOUND  '--load listview..-"
                        End If '--found..-
                    End If '-- valid..-
                End If '--key numeric..--
            Next colRecord '--colrecord..-
            '==Next idx
            '=== End If  '--count..-
        End If '--get rs1..-
        mbGetAllSerials = True
        '== MsgBox "ok.. found " & lngBlanksCount & " blank serials..", vbInformation  '--TESTING..--
        '==3077== End If '-got serials rs--
        '===  LabStatus.Caption = "Done.."
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
        colGoods = Nothing
        col1 = Nothing
        Exit Function

RefreshSerials_Error:
        k = Err.Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in  'mbRefreshSerials' function.." & vbCrLf & _
                "Error: " & k & ":  " & ErrorToString(k) & vbCrLf & _
                    "Serial lookup is may be incomplete..", MsgBoxStyle.Exclamation)
        rs1 = Nothing
        colGoods = Nothing
        col1 = Nothing
        Exit Function

        '== GetSerials_rset_error:
        '== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '== sMsg = " 'GetSerialsRecordset': Rst Open Error- SQL:" & vbCrLf & sSql & vbCrLf & vbCrLf & _
        '==               "ERROR: " & Str(Err.Number) & "==" & Err.Description & vbCrLf & _
        '==                        "ADO errors: " & gsGetErrors(mCnnJet, Err.Number) & vbCrLf & _
        '=                         "===== end error msg =====" & vbCrLf
        '== strErrorReport = sMsg
        '== If (gsErrorLogPath <> "") Then
        '==    Call gbLogMsg(gsErrorLogPath, sMsg)
        '== End If '--log--
        '= Exit Function

        '= GetStockedSerials_rset_error:

    End Function '--refresh Serials..--
    '= = = = = = = = = = = =
    '-===FF->

    '--GetSerialInfo-
    '---  Lookup/JOIN tables to get info on specific SerialNo..-

    '-- (Input: strSeriallNo;
    '--   Output: Stock_Id, barcode, cat1, cat2, description, cost, sell,
    '--             Goods_Id, Goodsline_id, Qty, Supplier_Id, SupplierCode, InStock (YES/NO) --)

    '--  Lookup SerialNo in SerialAudit table..--
    '---- SerialAudit links SerailNo to SerialAuditTrail, which --
    '---    gives Stock_id, Goods_Id and and GoodsLine_Id..--
    '-- sSql = " SELECT SerialAuditTrail.stock_id AS stock_id, SerialAuditTrail.Type, "
    '-- Sql = sSql + "  SerialAuditTrail.Type_id AS Goods_Id, "
    '-- sSql = sSql + "     SerialAuditTrail.Type_Line_id AS GoodsLine_Id, "
    '-- sSql = sSql + "      cat1, cat2, stock.Barcode, "
    '-- sSql = sSql + "      stock.description, stock.Stock_id,  cost, sell "
    '-- sSql = sSql + "  FROM SerialAudit "
    '-- sSql = sSql + "        LEFT JOIN (SerialAuditTrail "
    '-- sSql = sSql + "            LEFT JOIN Stock "
    '-- sSql = sSql + "             ON (Stock.stock_id=SerialAuditTrail.stock_id) ) "
    '-- sSql = sSql + "        ON (SerialAuditTrail.SerialAudit_Id=SerialAudit.SerialAudit_Id) "
    '-- sSql = sSql + "       WHERE (SerialAudit.number='" + msSerialNo + "') AND  (SerialAuditTrail.Type='GR') "
    '----        If mbRMLookup(sSql, mColItemFields) Then


    '-- FOR RA's: --
    '--Lookup Goodsline/Stock to get Line Qty and Supplier_Id, SupplierCode, info..--
    '-- sSql = " SELECT  GoodsLine.Goods_Id, Goods.supplier_id, supplier.supplier_id, "
    '-- sSql = sSql + "     SupplierCode.supCode AS supplierCode, supplierCode.stock_id, GoodsLine.quantity AS Qty  "
    '-- sSql = sSql + " FROM (GoodsLine "
    '-- sSql = sSql + "  LEFT JOIN (Goods "
    '-- sSql = sSql + "    LEFT JOIN (Supplier "
    '-- sSql = sSql + "      LEFT JOIN SupplierCode "
    '-- sSql = sSql + "      ON (SupplierCode.Supplier_id=Supplier.Supplier_id) ) "
    '-- sSql = sSql + "    ON (Supplier.Supplier_id=Goods.supplier_id)  )"
    '-- sSql = sSql + "  ON  (Goods.Goods_Id=GoodsLine.Goods_Id) )"
    '-- sSql = sSql + "  WHERE (GoodsLine.Goods_Id=" & mlGoodsId & ")  "
    '-- sSql = sSql + "        AND (GoodsLine.Line_Id=" & mlGoodsLineId & ") AND (supplierCode.stock_id=" & mlStockId & ")  "
    '--            If Not mbRMLookup(sSql, colRAFields) Then

    '--  FOR Jobmaint/NewPart, we need In-stock YES/NO column..--
    '--    So now check that this serialNo is actually still in stock..--

    '--  sSql = " SELECT  StockedSerials.stock_id AS stock_id,  "
    '--  sSql = sSql + "   cat1, cat2, stock.Barcode, "
    '--  sSql = sSql + " stock.description, stock.Stock_id, cost, sell, quantity "
    '--  sSql = sSql + "  FROM StockedSerials  "
    '--  sSql = sSql + "  LEFT JOIN  stock ON (StockedSerials.stock_id=stock.stock_id)  "
    '--  sSql = sSql + " WHERE (StockedSerials.number='" + sSerialNo + "')"
    '--  If mbRMLookup(sSql, colFields) Then
    '--                  mbCheckSerialNumber = True
    '--  Else  '--not found.--
    '--      '-- THIS Serial NOT in stock..--
    '--  end if

    '--= 3101.1024= Stock_id is optional (Send -1 if not being supplied)..
    '--  ALSO:  colAllSerialsInfo is now a COLLECTION of reord collections..-
    '==    Since if not Stock_id input, then multiple serials may be discovered..-

    Private Function mbGetSerialInfo(ByVal intInputStock_id As Integer, _
                                       ByVal sSerialNo As String, _
                                         ByRef colAllSerialsInfo As Collection) As Boolean
        Dim sSql As String
        Dim sInStock As String
        Dim datatable1 As DataTable
        Dim col1 As Collection
        Dim colItemFields As Collection
        Dim colSerialInfo, colSale_info As Collection
        Dim lngStockId, lngGoodsId As Integer
        Dim lngGoodsLineId As Integer
        Dim lngSupplierId, intSerialAudit_id, intDocket_id As Integer
        Dim sSupplierCode As String

        mbGetSerialInfo = False
        sSql = " SELECT SerialAudit.SerialAudit_Id, SerialAudit.number AS SerialNo, "
        sSql &= "   SerialAuditTrail.stock_id AS stock_id, SerialAuditTrail.Type, "
        sSql &= "   SerialAuditTrail.Type_id AS Goods_Id, "
        sSql &= "     SerialAuditTrail.Type_Line_id AS GoodsLine_Id, "
        sSql &= "      cat1, cat2, stock.Barcode, "
        sSql &= "      stock.description, stock.Stock_id,  cost, sell "
        sSql &= "  FROM SerialAudit "
        sSql &= "        LEFT JOIN (SerialAuditTrail "
        sSql &= "            LEFT JOIN Stock "
        sSql &= "             ON (Stock.stock_id=SerialAuditTrail.stock_id) ) "
        sSql &= "        ON (SerialAuditTrail.SerialAudit_Id=SerialAudit.SerialAudit_Id) "
        sSql &= "       WHERE (SerialAudit.number='" & sSerialNo & "') AND  (SerialAuditTrail.Type='GR') "
        '=3101.1024=  Filter for stock_id if provided..-
        If (intInputStock_id > 0) Then
            sSql &= " AND (SerialAuditTrail.stock_id= " & CStr(intInputStock_id) & "); "
        End If

        '=3101.1024=  may be multiple records returned.-
        If Not gbGetDataTable(mCnnJet, datatable1, sSql) Then
            MsgBox("Failed to find SerialAudit info for Serial: " & sSerialNo & vbCrLf & _
                                     gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        If Not mbMakeRecordCollection(datatable1, colAllSerialsInfo) Then
            MsgBox("No SerialInfo returned..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '-- get supplier info all selected serials..-
        For Each colSerialInfo In colAllSerialsInfo
            '--  get Qty and supplier info..-
            '-- FOR RA's: --
            intSerialAudit_id = CInt(colSerialInfo.Item("SerialAudit_Id")("value"))  '==3311.302-
            lngGoodsId = CInt(colSerialInfo.Item("Goods_Id")("value"))
            lngGoodsLineId = CInt(colSerialInfo.Item("GoodsLine_Id")("value"))
            lngStockId = CInt(colSerialInfo.Item("Stock_Id")("value"))

            '--Lookup Goodsline/Stock to get Line Qty and Supplier_Id, SupplierCode, info..--
            sSql = " SELECT  GoodsLine.Goods_Id, Goods.supplier_id AS supplier_id, supplier.supplier_id, "
            sSql = sSql & "     GoodsLine.quantity AS Qty  "
            sSql = sSql & " FROM (GoodsLine "
            sSql = sSql & "  LEFT JOIN (Goods "
            sSql = sSql & "    LEFT JOIN Supplier "
            sSql = sSql & "    ON (Supplier.Supplier_id=Goods.supplier_id) )"
            sSql = sSql & "  ON  (Goods.Goods_Id=GoodsLine.Goods_Id) )"
            sSql = sSql & "  WHERE (GoodsLine.Goods_Id=" & lngGoodsId & ")  "
            sSql = sSql & "        AND (GoodsLine.Line_Id=" & lngGoodsLineId & ") " '== AND (supplierCode.stock_id=" & lngStockId & ")  "
            If mbRMGetRecord(sSql, colItemFields) Then
                '--  add Qty, Supplier_id, SupplierCode "columns" (fld collections.) to serial info..-
                colSerialInfo.Add(colItemFields.Item("Qty"))
                colSerialInfo.Add(colItemFields.Item("supplier_id"))
                '===colSerialInfo.Add colItemFields("supplierCode")
                '--  one more query to get SupplierCode..--
                lngSupplierId = CInt(colItemFields.Item("Supplier_Id")("value"))
                sSql = " SELECT SupplierCode.supCode AS supplierCode, supplierCode.stock_id  "
                sSql = sSql & " FROM SupplierCode "
                sSql = sSql & " WHERE (SupplierCode.Supplier_id=" & CStr(lngSupplierId) & ") "
                sSql = sSql & "          AND (supplierCode.stock_id=" & lngStockId & ")  "
                sSupplierCode = "--"
                If mbRMGetRecord(sSql, colItemFields) Then
                    sSupplierCode = colItemFields.Item("supplierCode")("value")
                Else '--no supCode..- make empty col.
                End If '--no supCode..-
                col1 = New Collection
                col1.Add("supplierCode", "name")
                col1.Add(sSupplierCode, "value")
                '== col1.Add(ADODB.DataTypeEnum.adChar, "type")
                col1.Add(ADODB_DataTypeEnum_adChar, "type")
                col1.Add(15, "DefinedSize")
                colSerialInfo.Add(col1, "supplierCode")
                sInStock = "NO"
                '-- Lookup StockedSerials to get InStock status..-
                sSql = " SELECT  StockedSerials.stock_id AS stock_id,  "
                sSql = sSql & "   cat1, cat2, stock.Barcode, "
                sSql = sSql & " stock.description, stock.Stock_id, cost, sell, quantity "
                sSql = sSql & "  FROM StockedSerials  "
                sSql = sSql & "  LEFT JOIN  stock ON (StockedSerials.stock_id=stock.stock_id)  "
                sSql = sSql & " WHERE (StockedSerials.number='" & sSerialNo & "')"
                If mbRMGetRecord(sSql, colItemFields) Then
                    sInStock = "YES"
                Else '--not found.--
                    '-- THIS Serial NOT in stock..--
                End If
                '--  add InStock status. to record..
                col1 = New Collection
                col1.Add("InStock", "name")
                col1.Add(sInStock, "value")
                '== col1.Add(ADODB.DataTypeEnum.adChar, "type")
                col1.Add(ADODB_DataTypeEnum_adChar, "type")
                col1.Add(3, "DefinedSize")
                colSerialInfo.Add(col1, "InStock")
                '==
                '==  3311.302- 02Mar2016-
                '==      Add SALE/Customer Info (if any) to SerialInfo for caller.
                '-- Look for SA/IV Trail records for this Serial-
                colSale_info = New Collection  '--return empty collection if no sale.
                sSql = " SELECT  SerialAuditTrail.stock_id AS stock_id, SerialAuditTrail.Type, "
                sSql &= "   SerialAuditTrail.Type_id, SerialAuditTrail.trail_date, "
                sSql &= "     SerialAuditTrail.Type_Line_id AS DocketLine_Id, "
                sSql &= "     docket.docket_id, docket.customer_id, customer.barcode AS cust_barcode, "
                sSql &= "      customer.surname, customer.given_names, customer.company "
                sSql &= "  FROM SerialAuditTrail "
                sSql &= "   LEFT JOIN (docket "
                sSql &= "       LEFT JOIN Customer "
                sSql &= "       ON (docket.customer_id=customer.customer_id)) "
                sSql &= "   ON (docket.docket_id= SerialAuditTrail.Type_id) "
                sSql &= "       WHERE (SerialAuditTrail.SerialAudit_Id= " & intSerialAudit_id & ") "
                sSql &= "               AND ( (SerialAuditTrail.Type='SA') OR (SerialAuditTrail.Type='IV')) "
                If Not gbGetDataTable(mCnnJet, datatable1, sSql) Then
                    MsgBox("Failed to find SerialAudit Docket info for Serial: " & sSerialNo & vbCrLf & _
                                             gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    '==Exit Function
                Else  '--worked-
                    If (Not (datatable1 Is Nothing)) AndAlso (datatable1.Rows.Count > 0) Then
                        '- have data..  use last row-
                        Dim datarow1 As DataRow = datatable1.Rows(datatable1.Rows.Count - 1)
                        If Not IsDBNull(datarow1.Item("docket_id")) Then
                            '-add sale info to Serial Info-
                            col1 = New Collection
                            col1.Add("RM_sale_invoice_no", "name")
                            col1.Add(datarow1.Item("docket_id"), "value")
                            col1.Add(ADODB_DataTypeEnum_adInteger, "type")
                            col1.Add(4, "DefinedSize")
                            colSale_info.Add(col1, "sale_invoice_no")
                            '-sale date-
                            col1 = New Collection
                            col1.Add("sale_date", "name")
                            col1.Add(datarow1.Item("trail_date"), "value")
                            col1.Add(ADODB_DataTypeEnum_adDate, "type")
                            col1.Add(8, "DefinedSize")
                            colSale_info.Add(col1, "sale_date")
                            '- customer stuff.
                            col1 = New Collection
                            col1.Add("customer_barcode", "name")
                            col1.Add(datarow1.Item("cust_barcode"), "value")
                            col1.Add(ADODB_DataTypeEnum_adVarChar, "type")
                            col1.Add(40, "DefinedSize")
                            colSale_info.Add(col1, "sale_customer_barcode")
                            '-name-
                            col1 = New Collection
                            col1.Add("Customer_name", "name")
                            col1.Add(datarow1.Item("given_names") & " " & datarow1.Item("surname"), "value")
                            col1.Add(ADODB_DataTypeEnum_adVarChar, "type")
                            col1.Add(100, "DefinedSize")
                            colSale_info.Add(col1, "sale_customer_name")
                            col1 = New Collection
                            col1.Add("Customer_company", "name")
                            col1.Add(datarow1.Item("company"), "value")
                            col1.Add(ADODB_DataTypeEnum_adVarChar, "type")
                            col1.Add(100, "DefinedSize")
                            colSale_info.Add(col1, "sale_customer_company")
                            '- done-
                        End If  '-null-
                    End If  '-nothing
                End If
                colSerialInfo.Add(colSale_info, "item_sale_info")
                mbGetSerialInfo = True
            Else
                '--second select failed..-
                MsgBox("Failed to find GoodsLine info for Serial: " & sSerialNo, MsgBoxStyle.Exclamation)
            End If
        Next colSerialInfo

        '=3101= If mbRMGetRecord(sSql, colSerialInfo) Then
        '=3101= Else
        '=3101= '-First SELECT failed..
        '=3101= If gbDebug Then MsgBox("Failed to find SerialAudit info for Serial: " & sSerialNo, MsgBoxStyle.Exclamation)
        '=3101= End If '--lookup serial audit..-
        colItemFields = Nothing
        col1 = Nothing
    End Function '--GetSerialInfo-
    '= = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  Get Goods Invoice..--

    Private Function mbGetGoodsInvoiceInfo(ByVal lngGoodsId As Integer, _
                                            ByRef colInvoiceInfo As Collection) As Boolean
        Dim sSql As String

        mbGetGoodsInvoiceInfo = False
        sSql = " SELECT Goods.goods_id, Invoice_no, invoice_date,goods_date,  "
        sSql = sSql & " order_no, order_id, supplier.supplier_id as supplier_id, "
        sSql = sSql & "supplier.barcode as SupplierBarcode, supplier, "
        sSql = sSql & "main_contact, main_position, main_addr1, main_addr2, main_addr3, "
        sSql = sSql & "main_suburb, main_state, main_postcode, main_country, "
        sSql = sSql & "main_phone, main_fax, main_email  "
        sSql = sSql & "  FROM Goods  "
        sSql = sSql & "      LEFT JOIN Supplier "
        sSql = sSql & "         ON  (supplier.supplier_id= Goods.supplier_id)   "
        sSql = sSql & "  WHERE (Goods.goods_id=" & lngGoodsId & "); "
        If mbRMGetRecord(sSql, colInvoiceInfo) Then
            mbGetGoodsInvoiceInfo = True
        Else
            MsgBox("Can't find Goods Recvd Info..", MsgBoxStyle.Exclamation)
        End If '--lookup goods..-

    End Function '-goodsInvoice..-
    '= = = = = = = = = = = =
    '-===FF->

    '=== MULTI-HOST -- Public Interface ====
    '=== MULTI-HOST -- Public Interface ====

    '- Connect-  Jet RetailManager---
    '- Connect-  Jet RetailManager---

    '--- "sDSN " is Jet DBname or ODBC Data Source Name..--
    '---  "bNewPath"  is returned TRUE if DSN/Path/credentials changed..--

    Public Function connect(ByRef sServer As String, _
                             ByRef sDSN As String, _
                               ByRef sUid As String, _
                               ByRef sPwd As String, _
                                ByRef bNewPath As Boolean, _
                                ByRef strSchema As String) As Boolean _
                              Implements _clsRetailHost.connect
        Dim sSchema As String
        Dim sConnectLog As String
        Dim s1 As String
        connect = False
        msProvider = "retailmanager" '=sProvider '--save..-
        '--  "sServer" not used for MYOB..-
        msJetDbName = sDSN
        msJetUid = sUid
        msJetPwd = sPwd
        If mbRMConnect(bNewPath, sConnectLog) Then
            connect = True
            mbConnected = True
            '==If bNewPath Then  '--return new values
            '--  must return path in case it was blank originally..-
            sDSN = msJetDbName
            sUid = msJetUid
            sPwd = msJetPwd
            '==End If  '--newpath=
            s1 = "JobMatix=V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & _
                     ", Build: " & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & "="
            '--  log schema..--
            sSchema = gsShowJetSchema(msJetDbName, mColJetDBInfo)
            sSchema = sSchema & vbCrLf & "= = = " & s1 & " = = =" & vbCrLf & "===== =====" & vbCrLf
            '=3107.801=
            '== Call glSaveTextFile(msAppPath & "RetailM_Schema.txt", sConnectLog & vbCrLf & sSchema)
            strSchema = sConnectLog & vbCrLf & sSchema  '=3107.801=
        End If '--connect..-
    End Function '--connect--
    '= = = = = = = = = = = =
    '-===FF->

    '- SetConnection-  JobMatix POS and Sql Server---

    '-- NOT used for Retail Maneger..
    '-- NOT used for Retail Maneger..

    Sub SetConnection(ByVal sServer As String, _
                            ByRef connection As OleDbConnection, _
                            ByRef colTables As Collection, _
                             ByVal DBname As String) Implements _clsRetailHost.SetConnection

    End Sub  '-SetConnection-
    '= = = = = = = = = = = =
    '-===FF->

    '---  Browser support ---
    '---  Browser support ---
    '----  specific to Host..--

    '--  RETURN host equivalent table name.--
    '--  RETURN colPrefs (preferred columns) for this Host..--
    '-----  according to Table being browsed..--
    '------ NB: Table names are as per RetailManager..--

    Public Function browseGetPrefColumns(ByVal strTablename As String, _
                                          ByRef strHostTableName As String, _
                                           ByRef colPrefs As Collection) As Boolean _
                                         Implements _clsRetailHost.browseGetPrefColumns

        browseGetPrefColumns = True
        strHostTableName = strTablename '--same Name for RM..--

        Select Case LCase(strTablename)
            Case "staff"
                colPrefs = mColPrefsStaff
            Case "customer"
                colPrefs = mColPrefsCustomer
            Case "stock"
                colPrefs = mColPrefsStock
            Case "supplier"
                colPrefs = mColPrefsSupplier
            Case Else
                colPrefs = Nothing
                browseGetPrefColumns = False
        End Select

    End Function '--pref cols.--
    '= = = = = = = = = = = = == =

    '-- Translate Browser grid column back to original Table col.

    '= Currently (3101.929) NOT USED..
    '= Currently (3101.929) NOT USED..
    '= Currently (3101.929) NOT USED..
    '= Currently (3101.929) NOT USED..

    Function getOriginalColumnName(ByVal strTableName As String, _
                                         ByVal strGridColName As String) As String _
                                          Implements _clsRetailHost.getOriginalColumnName
        '- For Retail Manager..  no translation needed..

        getOriginalColumnName = strGridColName

    End Function  '==getOriginalColumnName-
    '= = = = = = = = =  = = = = = ==  =
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  get full record..--
    '--   browsed row from grid..
    '---  Output:  full record..

    Public Function browseGetSelectedRecord(ByVal strTablename As String, _
                                            ByRef colSelectedRow As Collection, _
                                              ByRef colFullRecord As Collection) As Boolean _
                                          Implements _clsRetailHost.browseGetSelectedRecord
        Dim sSql As String
        Dim sBarcode As String
        Dim lngId As Integer
        Select Case LCase(strTablename)
            Case "staff"
                lngId = CInt(colSelectedRow.Item("staff_id")("value"))
                sSql = "Select * from [staff] WHERE staff_id=" & CStr(lngId) & "; "
            Case "customer"
                sBarcode = colSelectedRow.Item("barcode")("value")
                lngId = CInt(colSelectedRow.Item("customer_id")("value"))
                sSql = "Select * from [customer] WHERE customer_id=" & CStr(lngId) & "; "
            Case "stock"
                lngId = CInt(colSelectedRow.Item("stock_id")("value"))
                sSql = "Select * from [stock] WHERE stock_id=" & CStr(lngId) & "; "

            Case "supplier"
                lngId = CInt(colSelectedRow.Item("supplier_id")("value"))
                sSql = "Select * from [supplier] WHERE supplier_id=" & CStr(lngId) & "; "

            Case Else
                browseGetSelectedRecord = False
                Exit Function
        End Select
        browseGetSelectedRecord = mbRMGetRecord(sSql, colFullRecord)

    End Function '--get full record..-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '=3311= --   Browse/Select STAFF --
    '--   Output:Full RM Staff Record.--
    '---   Returns FALSE if browse cancelled.--

    Public Function staffLookup(ByRef colRecord As Collection) As Boolean _
                              Implements _clsRetailHost.staffLookup
        Dim colSelectedRow As Collection
        Dim colKeys As Collection
        Dim sSql As String
        Dim sBarcode As String
        Dim lngId As Integer
        Dim frmBrowse1 As frmBrowse

        staffLookup = False
        If mbConnected Then
            '--If ShiftDown And CtrlDown And AltDown Then  Txt = "SHIFT+CTRL+ALT+F2."
            frmBrowse1 = New frmBrowse
            frmBrowse1.connection = mCnnJet '--Retail Manager Jet connenction..- 
            frmBrowse1.DBname = msJetDbName
            frmBrowse1.tableName = "staff"
            frmBrowse1.IsSqlServer = False '--bIsSqlServer
            frmBrowse1.colTables = mColJetDBInfo
            '--  pass preferred cols..-
            frmBrowse1.PreferredColumns = mColPrefsStaff  '-- colPrefs

            frmBrowse1.ShowDialog()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow '--get grid row selsected..-

            frmBrowse1.Close()
            System.Windows.Forms.Application.DoEvents()
            '--retrieve selected record and fill in cust details..--
            If colSelectedRow Is Nothing Then
                '====Me.Hide
                Exit Function '====Sub
            Else '--selection made..-
                '-- get barcode..--
                If colSelectedRow.Count() > 0 Then
                    sBarcode = colSelectedRow.Item("barcode")("value")
                    lngId = CInt(colSelectedRow.Item("staff_id")("value"))
                    '==If Not mbLookupCustomerId(lngId, colRecord) Then
                    sSql = "Select * from [staff] WHERE staff_id=" & CStr(lngId) & "; "
                    If Not mbRMGetRecord(sSql, colRecord) Then
                        MsgBox("Failed to retrieve Staff record for Id: '" & lngId & "'..", MsgBoxStyle.Critical)
                    Else '--ok--
                        staffLookup = True
                    End If
                Else
                End If '--got row..--
            End If '--nothing..-
        Else
            MsgBox("No Retail DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected.-
        colSelectedRow = Nothing
        colKeys = Nothing
        frmBrowse1 = Nothing
    End Function  '-staff Lookup..
    '= = = = = = = = = = = = = = = == 
    '-===FF->

    '--  Get Staff record --
    '---  (Input: Barcode;  Output: Full RM staff Record incl "docket_name"..)

    Public Function staffGetStaffRecord(ByVal strBarcode As String, _
                                         ByVal lngId As Integer, _
                                          ByRef colStaffInfo As Collection) As Boolean _
                                        Implements _clsRetailHost.staffGetStaffRecord
        Dim sSql As String
        staffGetStaffRecord = False
        If mbConnected Then
            If (strBarcode <> "") Then '--was supplied..-
                sSql = "Select * from [staff] WHERE barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = "Select * from [staff] WHERE staff_id=" & Str(lngId) & "; "
            Else
                MsgBox("No Staff ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            staffGetStaffRecord = mbRMGetRecord(sSql, colStaffInfo)
        Else
            MsgBox("No Retail provider connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '--get staff..-
    '= = = = = = = = = = = = = =

    '==3311.406=--  Get Staff record Ex.--
    '---  (Input: Barcode;  OR DOCKET_NAME Output: Full RM staff Record incl "docket_name"..)

    Public Function staffGetStaffRecordEx(ByVal strBarcode As String, _
                                           ByVal lngId As Integer, _
                                            ByVal strDocketName As String, _
                                             ByRef colStaffInfo As Collection) As Boolean _
                                                    Implements _clsRetailHost.staffGetStaffRecordEx
        Dim sSql As String
        staffGetStaffRecordEx = False
        If mbConnected Then
            If (strBarcode <> "") Then '--was supplied..-
                sSql = "Select * from [staff] WHERE barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = "Select * from [staff] WHERE staff_id=" & Str(lngId) & "; "
            ElseIf (strDocketName <> "") Then  '--docket-name was supplied..-
                sSql = "Select * from [staff] WHERE docket_name='" & strDocketName & "'; "
            Else
                MsgBox("No Staff ID or Name supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            '-- This gets first record only, if more match..
            '--  Returns False if empty.. (no Match)
            staffGetStaffRecordEx = mbRMGetRecord(sSql, colStaffInfo)
        Else
            MsgBox("No Retail provider connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '--get staff Ex..-
    '= = = = = = = = = = = = = =
    '-===FF->


    '= New 3311.731=
    '-- Get full staff list...(cf Customer List).
    '-- records as per staffGetStaffRecordEx-

    Public Function staffGetStaffList(ByRef colRecordList As Collection) As Boolean _
                                                          Implements _clsRetailHost.staffGetStaffList
        Dim sSql As String
        Dim rs1 As DataTable  '= ADODB.Recordset

        staffGetStaffList = False
        If mbConnected Then
            sSql = " SELECT surname, given_names,  barcode, docket_name, phone, mobile, email "
            sSql = sSql & " FROM [staff] ORDER BY docket_name; "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJet, rs1, sSql) Then
                MsgBox("Failed to get recordset for SQL:" & vbCrLf & sSql & vbCrLf, MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            End If
            '-ok..  make rset into our collection structure.-
            staffGetStaffList = mbMakeRecordCollection(rs1, colRecordList, "barcode")
            '= rs1.Close()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            rs1 = Nothing
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-

    End Function '-staffGetStaffList-
    '= = = = = = = = =  = = = = = = = = =
    '-===FF->

    '--  Customer Queries --
    '--  Customer Queries --

    '--   Browse/Select Customer --
    '--       Output:Full RM Customer Record.--
    '---   Returns FALSE if browse cancelled.--

    Public Function customerLookup(ByRef colRecord As Collection) As Boolean _
                              Implements _clsRetailHost.customerLookup
        '== Dim colPrefs As Collection
        Dim colSelectedRow As Collection
        Dim colKeys As Collection
        Dim sSql As String
        Dim sBarcode As String
        Dim lngId As Integer
        Dim frmBrowse1 As frmBrowse

        customerLookup = False
        If mbConnected Then
            '--If ShiftDown And CtrlDown And AltDown Then  Txt = "SHIFT+CTRL+ALT+F2."
            frmBrowse1 = New frmBrowse
            frmBrowse1.connection = mCnnJet '--Retail Manager Jet connenction..- 
            frmBrowse1.DBname = msJetDbName
            frmBrowse1.tableName = "customer"
            frmBrowse1.IsSqlServer = False '--bIsSqlServer
            frmBrowse1.colTables = mColJetDBInfo
            '--  pass preferred cols..-
            frmBrowse1.PreferredColumns = mColPrefsCustomer '-- colPrefs

            frmBrowse1.ShowDialog()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow '--get grid row selsected..-

            frmBrowse1.Close()
            System.Windows.Forms.Application.DoEvents()
            '--retrieve selected record and fill in cust details..--
            If colSelectedRow Is Nothing Then
                '====Me.Hide
                Exit Function '====Sub
            Else '--selection made..-
                '-- get barcode..--
                If colSelectedRow.Count() > 0 Then
                    sBarcode = colSelectedRow.Item("barcode")("value")
                    lngId = CInt(colSelectedRow.Item("customer_id")("value"))
                    '==If Not mbLookupCustomerId(lngId, colRecord) Then
                    sSql = "Select * from [customer] WHERE customer_id=" & CStr(lngId) & "; "
                    If Not mbRMGetRecord(sSql, colRecord) Then
                        MsgBox("Failed to retrieve customer record for Id: '" & lngId & "'..", MsgBoxStyle.Critical)
                    Else '--ok--
                        customerLookup = True
                    End If
                Else
                End If '--got row..--
            End If '--nothing..-
        Else
            MsgBox("No Retail DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected.-
        colSelectedRow = Nothing
        colKeys = Nothing
        frmBrowse1 = Nothing
    End Function '--lookupCust.-
    '= = = = = = = = = = =  =
    '-===FF->

    '-- Get list of all customers..--

    Public Function customerGetCustomerList(ByRef colRecordList As Collection) As Boolean _
                                      Implements _clsRetailHost.customerGetCustomerList
        Dim sSql As String
        Dim rs1 As DataTable  '= ADODB.Recordset

        customerGetCustomerList = False
        If mbConnected Then
            sSql = " SELECT surname, given_names, company,  Barcode, customer_id, phone, mobile "
            sSql = sSql & " FROM [customer] ORDER BY surname; "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJet, rs1, sSql) Then
                MsgBox("Failed to get recordset for SQL:" & vbCrLf & sSql & vbCrLf, MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            End If
            '-ok..  make rset into our collection structure.-
            customerGetCustomerList = mbMakeRecordCollection(rs1, colRecordList, "barcode")
            '= rs1.Close()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            rs1 = Nothing
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '--cust list..-
    '= = = = = = = = = = =  =
    '-===FF->

    '--  Get Customer Record--
    '---  (Input: Barcode;  Output:Full RM Customer Record..)
    '== 3059.1= Give preference to Customer_id if provided..==

    Public Function customerGetCustomerRecord(ByVal strBarcode As String, _
                                               ByVal lngId As Integer, _
                                                ByRef colRecord As Collection) As Boolean _
                                              Implements _clsRetailHost.customerGetCustomerRecord
        Dim sSql As String

        customerGetCustomerRecord = False
        If mbConnected Then
            If (lngId >= 0) Then  '--was supplied..-
                sSql = "Select * from [customer] WHERE customer_id=" & Str(lngId) & "; "
            ElseIf (strBarcode <> "") Then '--was supplied..-
                sSql = "Select * from [customer] WHERE barcode='" & strBarcode & "'; "
            Else
                MsgBox("No Customer ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            customerGetCustomerRecord = mbRMGetRecord(sSql, colRecord)
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-
    End Function '--get cust...-
    '= = = = = = = = = = = = = =
    '= = = = = = = = = = =  =
    '-===FF->

    '--  Get Customer Record--
    '--  INPUT is Browser Grid row collection..--
    '---   Output:Full RM Customer Record..

    Public Function customerGetCustomerRecordEx(ByRef colBrowseRow As Collection, _
                                                ByRef colRecord As Collection) As Boolean _
                                              Implements _clsRetailHost.customerGetCustomerRecordEx
        Dim sSql As String
        Dim L1, lngId As Integer
        Dim lngError As Integer
        Dim s1, strBarcode As String

        customerGetCustomerRecordEx = False
        lngId = -1
        strBarcode = ""
        If mbConnected Then
            If Not (colBrowseRow Is Nothing) Then
                On Error Resume Next
                L1 = CLng(colBrowseRow.Item("customer_id")("value"))
                lngError = Err.Number
                On Error GoTo 0
                If (lngError <> 0) Then
                    '== MsgBox("No Customer_id was input for customerGetCustomerRecordEx..", MsgBoxStyle.Exclamation)
                    '= Exit Function
                Else  '--found.-
                    lngId = L1
                End If
                '--get barcode..
                On Error Resume Next
                s1 = CStr(colBrowseRow.Item("barcode")("value"))
                lngError = Err.Number
                On Error GoTo 0
                If (lngError <> 0) Then
                Else  '--found.-
                    strBarcode = s1
                End If
            End If  '--nothing
            If (strBarcode <> "") Then '--was supplied..-
                sSql = "Select * from [customer] WHERE barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = "Select * from [customer] WHERE customer_id=" & Str(lngId) & "; "
            Else
                MsgBox("No Customer barcode or ID was supplied for " & vbCrLf & _
                         " RM function customerGetCustomerRecordEx..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            customerGetCustomerRecordEx = mbRMGetRecord(sSql, colRecord)
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '--get cust EX...-
    '= = = = = = = = = = =  =
    '-===FF->

    '-CustomerGetSalesHistory--

    '--  N O T E :  MYOB RM dockets have Customer_Id ONLY. (No Customer barcode.)--
    '--  N O T E :  MYOB RM dockets have Customer_Id ONLY. (No Customer barcode.)--

    '--  BARCODE input parameter is included in case other RetailHosts need String customer key..-

    '--- Input: Customer Id;
    '--    Output: Record Collection of sales-line records..--

    '--  NB: Customer details are looked up separately..--

    Public Function customerGetSalesHistory(ByVal strBarcode As String, _
                                             ByVal lngId As Integer, _
                                              ByRef colRecordList As Collection) As Boolean _
                                            Implements _clsRetailHost.customerGetSalesHistory
        '== Dim sSql As String
        Dim rs1 As DataTable  '=  ADODB.Recordset

        customerGetSalesHistory = False
        If mbConnected Then

            '--  RETAIL MANAGER..-
            '--  RESULTS columns are:
            '-   - docket.docket_id , docket_date, total_inc,
            '--     Cat1, Cat2, Description, barcode,  docket.transaction,
            '--         DocketLine.stock_id, DocketLine.quantity,
            '--              sell_inc, docket.customer_Id

            '--  NOTE:  MYOB RM dockets have Customer_Id ONLY. (No Customer barcode.)--
            If mbRefreshPurchasesList(lngId, rs1) Then
                '- Convert recordset to standard collections..--
                customerGetSalesHistory = mbMakeRecordCollection(rs1, colRecordList)

            End If
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-
    End Function '--lookup staff..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  S t o c k --
    '--  S t o c k --

    '--  StockLookup --         (BROWSE and select part.)-
    '--  StockGetStockRecord --      (Input Barcode, StockId..  return SELECT LIST of columns.)..-

    '--  RetailManager:
    '-  FOR BROWSE..  Returns standard stock record for selected browser row.--

    '--  S t o c k L o o k u p --   (BROWSE and select part.)-
    '--  S t o c k L o o k u p --   (BROWSE and select part.)-
    '--  S t o c k L o o k u p --   (BROWSE and select part.)-
    '--   ALSO: FOR Stock Browse..
    '-- If (msServiceChargeCat1 <> "") Then
    '--    sWhere = " (cat1='" & msServiceChargeCat1 & "') "
    '--       If (msServiceChargeCat2 <> "") Then sWhere = sWhere & " AND (cat2='" & msServiceChargeCat2 & "') "
    '--  BROWSE STOCK --

    Public Function stockLookup(ByVal bServiceChargeRequested As Boolean, _
                                 ByVal sServiceChargeCat1 As String, _
                                  ByVal sServiceChargeCat2 As String, _
                                    ByRef colSelectedRecord As Collection) As Boolean _
                                  Implements _clsRetailHost.stockLookup
        Dim lngId As Integer
        Dim colKeys As Collection
        '==Dim sBarcode As String
        Dim sWhere As String
        Dim colSelectedRow As Collection
        '==Dim colRecord As Collection '--full cust record..-
        Dim sSql As String
        Dim frmBrowse1 As frmBrowse

        stockLookup = False
        If mbConnected Then
            sWhere = ""
            frmBrowse1 = New frmBrowse
            frmBrowse1.connection = mCnnJet '--Retail Manager Jet connection..-
            frmBrowse1.colTables = mColJetDBInfo
            frmBrowse1.DBname = ""
            frmBrowse1.tableName = "stock"
            frmBrowse1.IsSqlServer = False '--bIsSqlServer
            If bServiceChargeRequested Then '--requested.-
                sWhere = " (cat1='" & sServiceChargeCat1 & "') "
                If (sServiceChargeCat2 <> "") Then sWhere = sWhere & " AND (cat2='" & sServiceChargeCat2 & "') "
            Else '-- normal stock part requested...-
                If (sServiceChargeCat1 <> "") Then '--can filter..-
                    sWhere = " (cat1<>'" & sServiceChargeCat1 & "') "
                End If
            End If
            frmBrowse1.WhereCondition = sWhere
            frmBrowse1.PreferredColumns = mColPrefsStock '-- colPrefs

            frmBrowse1.InitialFocusColumn = "description"

            frmBrowse1.ShowDialog()
            '--End If  '--tbrowse-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow '--get grid row selsected..-
            frmBrowse1.Close()
            System.Windows.Forms.Application.DoEvents()
            '--retrieve selected record and fill in cust details..--
            If colSelectedRow Is Nothing Then
                '====Me.Hide
                Exit Function
            Else '--selection made..-
                '-- get barcode..--
                If (colSelectedRow.Count() > 0) Then
                    '--  get standard record.--
                    lngId = CInt(colSelectedRow.Item("stock_id")("value"))
                    sSql = "Select * from [stock] WHERE stock_id=" & CStr(lngId) & "; "
                    If Not mbRMGetRecord(sSql, colSelectedRecord) Then
                        MsgBox("Failed to retrieve stock record for Id: '" & lngId & "'..", MsgBoxStyle.Critical)
                    Else '--ok--
                        stockLookup = True
                    End If
                Else
                    If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                End If '--got row..--
            End If '--nothing..-
            System.Windows.Forms.Application.DoEvents()
            '==Set colPrefs = Nothing
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
        colSelectedRow = Nothing
        colKeys = Nothing
        frmBrowse1 = Nothing

    End Function '--StockLookup--
    '= = = = = = = = = = = = =
    '-===FF->

    '--  stockGetJobPartStockInfo --
    '--  stockGetJobPartStockInfo --

    '----        (Input Barcode, StockId..  return stock SELECT LIST record)..-
    '--           (if Barcode is supplied, use it as key, else use StockId...)
    '-  FOR Jobs NewPart GetRecord..   Selected record must return special columns.--
    '--    NEED special JOINs to get at CAT3..---
    '--       SELECT  CategoryValues.description AS cat3,  cat1, cat2, Barcode, allow_renaming, " & _
    ''--                " stock.description AS OriginalDescription, stock.Stock_id, " & _
    ''--                " stock.longDesc, cost, sell AS OriginalSell , quantity FROM stock " & _
    ''--               " LEFT JOIN (CategorisedStock " & _
    ''--                " LEFT JOIN  CategoryValues  ON ( CategoryValues.catValue_id=    CategorisedStock.catValue_id) " & _
    ''--                "   )   ON ((stock.stock_id=CategorisedStock.stock_id) AND (CategorisedStock.category_level=3)) "

    Public Function stockGetJobPartStockInfo(ByVal strBarcode As String, _
                                              ByVal lngId As Integer, _
                                               ByRef colRecord As Collection) As Boolean _
                                            Implements _clsRetailHost.stockGetJobPartStockInfo
        Dim sSql As String

        stockGetJobPartStockInfo = False
        If mbConnected Then
            If (strBarcode <> "") Then '--was supplied..-
                sSql = msStockSelectSql & " WHERE stock.barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = msStockSelectSql & " WHERE stock.stock_id=" & Str(lngId) & "; "
            Else
                MsgBox("No stock ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            stockGetJobPartStockInfo = mbRMGetRecord(sSql, colRecord)
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-
    End Function '--get stock...-
    '= = = = = = = = = = = = = =

    '--  StockGetStockRecord --
    '--  StockGetStockRecord --
    '--   Get STANDARD full stock record..--

    Public Function stockGetStockRecord(ByVal strBarcode As String, _
                                          ByVal lngId As Integer, _
                                          ByRef colRecord As Collection) As Boolean _
                                             Implements _clsRetailHost.stockGetStockRecord
        Dim sSql As String

        stockGetStockRecord = False
        If mbConnected Then
            If (strBarcode <> "") Then '--was supplied..-
                sSql = "SELECT * FROM stock WHERE stock.barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = "SELECT * FROM stock  WHERE stock.stock_id=" & Str(lngId) & "; "
            Else
                MsgBox("No stock ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            stockGetStockRecord = mbRMGetRecord(sSql, colRecord)
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '--get stock...-
    '= = = = = = = = = = = = = =

    '--  StockGetStockRecordEx --
    '--   Get STANDARD full stock record..--
    '--  INPUT is Browser Grid row collection..--

    Public Function stockGetStockRecordEx(ByRef colBrowseRow As Collection, _
                                               ByRef colRecord As Collection) As Boolean _
                                                  Implements _clsRetailHost.stockGetStockRecordEx
        Dim sSql As String
        Dim lngError As Integer
        Dim lngId As Integer = -1
        Dim strBarcode As String = ""

        stockGetStockRecordEx = False
        If mbConnected Then
            If Not (colBrowseRow Is Nothing) Then
                On Error Resume Next
                lngId = CLng(colBrowseRow("stock_id")("value"))
                lngError = Err.Number
                On Error GoTo 0
                If lngError <> 0 Then
                    MsgBox("No Stock_id was input for stockGetStockRecordEx..", MsgBoxStyle.Exclamation)
                    Exit Function
                End If
            End If  '--nothing-
            If (strBarcode <> "") Then '--was supplied..-
                sSql = "SELECT * FROM stock WHERE stock.barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = "SELECT * FROM stock  WHERE stock.stock_id=" & Str(lngId) & "; "
            Else
                MsgBox("No stock ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            stockGetStockRecordEx = mbRMGetRecord(sSql, colRecord)
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function  '--stockGetStockRecordEx-
    '= = = = = = = = = = = =
    '-===FF->

    '--  Get selected stock records..--
    '--  Input list of stock-id's  for WHERE clause..--
    '--  NOTE: -
    '----  IF no stock list, then RETURNS ALL records..--
    '---      TOO SLOW for getting COMPLETE stock list..--


    Public Function stockGetStockList(ByRef colStockIdList As Collection, _
                                       ByRef colRecordList As Collection) As Boolean _
                                     Implements _clsRetailHost.stockGetStockList
        Dim sSql As String
        Dim sWhere As String
        Dim sStockId As String
        Dim v1 As Object
        Dim rs1 As DataTable  '= ADODB.Recordset

        stockGetStockList = False
        sWhere = ""
        If mbConnected Then
            If Not (colStockIdList Is Nothing) Then
                For Each v1 In colStockIdList
                    sStockId = Trim(CStr(v1))
                    If (InStr(sWhere, " " & sStockId & ")") <= 0) Then '--don't have it.. so add it-
                        If (sWhere <> "") Then sWhere = sWhere & " OR "
                        sWhere = sWhere & " (stock_id= " & sStockId & ")"
                    End If
                Next v1 '--v1-
            End If '--nothing.--
            If (sWhere <> "") Then sWhere = " WHERE " & sWhere
            '== sSql = "SELECT * FROM stock  WHERE " & sWhere & "; "
            sSql = "SELECT * FROM stock  " & sWhere & " ORDER by Cat1,Cat2; "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJet, rs1, sSql) Then
                MsgBox("Failed to get recordset for SQL:" & vbCrLf & sSql & vbCrLf, MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            End If
            '-ok..  make rset into our collection structure.-
            stockGetStockList = mbMakeRecordCollection(rs1, colRecordList, "Stock_id")
            '== rs1.Close()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            rs1 = Nothing
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-
    End Function '--get stock...-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  Get DistinctCategoryList..--
    '----  DISTINCT combinations of Cat1/Cat2..--

    Public Function stockGetDistinctCategoryList(ByRef colRecordList As Collection) As Boolean _
                                     Implements _clsRetailHost.stockGetDistinctCategoryList
        Dim sSql As String
        Dim rs1 As DataTable  '=  As ADODB.Recordset

        stockGetDistinctCategoryList = False
        If mbConnected Then
            '== sSql = "SELECT * FROM stock  WHERE " & sWhere & "; "
            '=  sSql = "SELECT * FROM stock  " & sWhere & " ORDER by Cat1,Cat2; "
            sSql = "SELECT DISTINCT Cat1,Cat2 FROM Stock ORDER BY Cat1,Cat2; "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJet, rs1, sSql) Then
                MsgBox("Failed to get recordset for SQL:" & vbCrLf & sSql & vbCrLf, MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            End If
            '-ok..  make rset into our collection structure.-
            stockGetDistinctCategoryList = mbMakeRecordCollection(rs1, colRecordList)
            '== rs1.Close()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            rs1 = Nothing
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-
    End Function '--get stock...-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  G o o d s  I n v o i c e s --
    '--  G o o d s  I n v o i c e s --
    '---  Get Invoice/Supplier info for given Goods_id.--
    '--    InvoiceGetInvoiceInfo --   (Input strInvoiceNo, lngGoodsId..  return Invoice Info incl Supplier Info.-)
    '--                                (if strInvoiceNo is supplied, use it as key, else use GoodsId...)
    '--  EX RA's-  "mbLookupSupplier" --

    '-- sSql = " SELECT Goods.goods_id, Invoice_no, invoice_date,goods_date,  "
    '--   sSql = sSql + " order_no, order_id, supplier.supplier_id as supplier_id, "
    '--   sSql = sSql + "supplier.barcode as SupplierBarcode, supplier, "
    '--   sSql = sSql + "main_contact, main_position, main_addr1, main_addr2, main_addr3, "
    '--   sSql = sSql + "main_suburb, main_state, main_postcode, main_country, "
    '--   sSql = sSql + "main_phone, main_fax, main_email  "
    '--   sSql = sSql + "  FROM Goods  "
    '--   sSql = sSql + "      LEFT JOIN Supplier "
    '--   sSql = sSql + "         ON  (supplier.supplier_id= Goods.supplier_id)   "
    '--   sSql = sSql + "  WHERE (Goods.goods_id=" & mlGoodsId & "); "
    '--   If mbRMLookup(sSql, colInvoiceFields) Then
    '--      mbLookupSupplier = True
    '--   Else
    '--         MsgBox "Can't find Goods Recvd Info..", vbExclamation
    '--   End If  '--lookup goods..-

    Public Function invoiceGetInvoiceInfo(ByVal strBarcode As String, _
                                           ByVal lngGoodsId As Integer, _
                                            ByRef colInvoiceInfo As Collection) As Boolean _
                                          Implements _clsRetailHost.invoiceGetInvoiceInfo

        invoiceGetInvoiceInfo = False
        If mbConnected Then
            '-- For RM.. Goods_id is primary Key..-
            invoiceGetInvoiceInfo = mbGetGoodsInvoiceInfo(lngGoodsId, colInvoiceInfo)

        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-

    End Function '--InvoiceGetInvoiceInfo--
    '= = = = = = =

    '-- Get all supplier Invoices for for given stock item..)
    '--    InvoiceGetStockItemInvoices  --
    '--
    '--  lookup/Load all Goods Received lines for this stock_id..--
    '--- EX RA's  "mbLoadInvoices"--

    '--  sSql = " SELECT   "
    '--  sSql = sSql + "   Goods.Invoice_No, Goods.Goods_Date,  "
    '--  sSql = sSql + "   Supplier.Supplier, SupplierCode.SupCode, Goods.Supplier_id,  "
    '--  sSql = sSql + "  GoodsLine.goods_id AS Goods_Id, GoodsLine.quantity AS Qty   "
    '--  sSql = sSql + " FROM   "
    '--  sSql = sSql + "  (GoodsLine LEFT JOIN  "
    '--  sSql = sSql + "   (Goods LEFT JOIN  "
    '--  sSql = sSql + "    (Supplier LEFT JOIN "
    '--  sSql = sSql + "       SupplierCode "
    '--  sSql = sSql + "       ON ((SupplierCode.Supplier_id= Supplier.Supplier_id) AND (SupplierCode.stock_id=" & mlStockId & ")) )"
    '--  sSql = sSql + "      ON (Supplier.Supplier_id=Goods.supplier_id)  )"
    '--  sSql = sSql + "    ON (Goods.Goods_id=GoodsLine.goods_id) )  "
    '--  sSql = sSql + " WHERE (GoodsLine.stock_id=" & mlStockId & ") "
    '--  sSql = sSql + "   ORDER BY Goods.Invoice_Date "
    '--    '-- GET recordset of invoices for that stock item..==
    '--    Screen.MousePointer = vbHourglass
    '--    If Not gbGetRst(mCnnJet, rsGoods, sSql) Then
    '--                 MsgBox "Failed to get recordset for SQL:" + vbCrLf + sSql + vbCrLf, vbExclamation
    '--                 Screen.MousePointer = vbDefault
    '--                 Exit Function
    '--    End If

    Public Function invoiceGetStockItemInvoices(ByVal strBarcode As String, _
                                                 ByVal lngStockId As Integer, _
                                                  ByRef colRecordList As Collection) As Boolean _
                                                Implements _clsRetailHost.invoiceGetStockItemInvoices
        Dim sSql As String
        Dim rsGoods As DataTable '= ADODB.Recordset

        invoiceGetStockItemInvoices = False
        If mbConnected Then
            sSql = " SELECT   "
            sSql = sSql & "   Goods.Invoice_No, Goods.Goods_Date,  "
            sSql = sSql & "   Supplier.Supplier, SupplierCode.SupCode, Goods.Supplier_id,  "
            sSql = sSql & "  GoodsLine.goods_id AS Goods_Id, GoodsLine.quantity AS Qty   "
            sSql = sSql & " FROM   "
            sSql = sSql & "  (GoodsLine LEFT JOIN  "
            sSql = sSql & "   (Goods LEFT JOIN  "
            sSql = sSql & "    (Supplier LEFT JOIN "
            sSql = sSql & "       SupplierCode "
            sSql = sSql & "       ON ((SupplierCode.Supplier_id= Supplier.Supplier_id) "
            sSql = sSql & "                          AND (SupplierCode.stock_id=" & lngStockId & ")) )"
            sSql = sSql & "      ON (Supplier.Supplier_id=Goods.supplier_id)  )"
            sSql = sSql & "    ON (Goods.Goods_id=GoodsLine.goods_id) )  "
            sSql = sSql & " WHERE (GoodsLine.stock_id=" & lngStockId & ") "
            sSql = sSql & "   ORDER BY Goods.Invoice_Date "
            '-- GET recordset of invoices for that stock item..==
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJet, rsGoods, sSql) Then
                MsgBox("Failed to get recordset for SQL:" & vbCrLf & sSql & vbCrLf, MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            End If
            '-ok..  make rset into our collection structure.-
            invoiceGetStockItemInvoices = mbMakeRecordCollection(rsGoods, colRecordList)
            '== rsGoods.Close()
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rsGoods = Nothing
    End Function '--invoiceGetStockItemInvoices--
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  SupplierLookup --
    '--  (BROWSE and select supplier.  return FULL RM record..-)-

    Public Function supplierLookup(ByRef colRecord As Collection) As Boolean _
                                     Implements _clsRetailHost.supplierLookup
        Dim colSelectedRow As Collection
        Dim colKeys As Collection
        Dim sSql As String
        Dim sBarcode As String
        Dim lngId As Integer
        Dim frmBrowse1 As frmBrowse

        supplierLookup = False
        If mbConnected Then
            frmBrowse1 = New frmBrowse
            frmBrowse1.connection = mCnnJet '--Retail Manager Jet connenction..-
            frmBrowse1.colTables = mColJetDBInfo
            frmBrowse1.DBname = ""
            frmBrowse1.tableName = "supplier"
            frmBrowse1.IsSqlServer = False '--bIsSqlServer
            '--  pass preferred cols..-
            frmBrowse1.PreferredColumns = mColPrefsSupplier '-- colPrefs
            frmBrowse1.ShowDialog()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow '--get grid row selsected..-
            frmBrowse1.Close()
            System.Windows.Forms.Application.DoEvents()
            '--retrieve selected record and fill in cust details..--
            If colSelectedRow Is Nothing Then
                '====Me.Hide
                Exit Function '====Sub
            Else '--selection made..-
                '-- get barcode..--
                If colSelectedRow.Count() > 0 Then
                    sBarcode = colSelectedRow.Item("barcode")("value")
                    lngId = CInt(colSelectedRow.Item("supplier_id")("value"))
                    sSql = "Select * from [supplier] WHERE supplier_id=" & CStr(lngId) & "; "
                    If Not mbRMGetRecord(sSql, colRecord) Then
                        MsgBox("Failed to retrieve Supplier record for Id: '" & lngId & "'..", MsgBoxStyle.Critical)
                    Else '--ok--
                        supplierLookup = True
                    End If
                Else
                End If '--got row..--
            End If '--nothing..-
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
        colSelectedRow = Nothing
        colKeys = Nothing
        frmBrowse1 = Nothing
    End Function '--supplierLookup--
    '= = = = = = = =  = = = = ==
    '-===FF->

    '--  SupplierGetSupplierRecord --        (Input strBarcode, lngSupplierId..  return FULL RM record..-)
    '--                                (if Barcode is supplied, use it as key, else use SupplierId...)
    '--

    Public Function supplierGetSupplierRecord(ByVal strBarcode As String, _
                                               ByVal lngId As Integer, _
                                                ByRef colRecord As Collection) As Boolean _
                                             Implements _clsRetailHost.supplierGetSupplierRecord
        Dim sSql As String

        supplierGetSupplierRecord = False
        If mbConnected Then
            If (strBarcode <> "") Then '--was supplied..-
                sSql = "SELECT * from [supplier] WHERE barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = "SELECT * from [supplier]  WHERE stock_id=" & Str(lngId) & "; "
            Else
                MsgBox("No stock ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            supplierGetSupplierRecord = mbRMGetRecord(sSql, colRecord)
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '--supplierGet--
    '= = = = = = = = = = = = =
    '-===FF->

    '-- S e r i a l s ---
    '-- S e r i a l s ---

    '-- SerialGetAllSerials --
    '--Results:  SerialNo, InStock, cat1, stock.Barcode, stock.description,       --
    '--              Goods_Id, InvoiceNo, InvoiceDate, Supplier, stock_id,  cost  --

    Public Function serialGetAllSerials(ByRef sArg As String, _
                                        ByRef labStatus As Label, _
                                         ByRef bCancelFlag As Boolean, _
                                           ByRef colSerialsList As Collection, _
                                             ByRef strErrorReport As String) As Boolean _
                                           Implements _clsRetailHost.serialGetAllSerials
        serialGetAllSerials = False
        If mbConnected Then
            serialGetAllSerials = mbGetAllSerials(sArg, labStatus, _
                                     bCancelFlag, colSerialsList, strErrorReport)

        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-

    End Function '-- SerialGetAllSerials --
    '= = = = = = = = =


    '-- SerialGetSerialInfo    --

    '-- (Input: stock_id, strSeriallNo;
    '--   Output: Stock_Id, barcode, cat1, cat2, description, cost, sell,
    '--             Goods_Id, Goodsline_id, Qty, Supplier_Id, SupplierCode, InStock (YES/NO) --)
    '=3101=  also inputs  "intInputStock_id" -

    Public Function serialGetSerialInfo(ByVal intInputStock_id As Integer, _
                                          ByVal sSerialNo As String, _
                                           ByRef colSerialInfo As Collection) As Boolean _
                                            Implements _clsRetailHost.serialGetSerialInfo


        serialGetSerialInfo = False
        If mbConnected Then
            serialGetSerialInfo = mbGetSerialInfo(intInputStock_id, sSerialNo, colSerialInfo)
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-

    End Function '-- SerialGetAllSerials --
    '= = = = = = = = =
    '-===FF->

    '--  Q u o t e s  --
    '--  Q u o t e s  --
    '--  Q u o t e s  --

    '--  QuoteGetAllQuotes   --

    '----  Returns full list of quote records (RM-SalesOrders "QU"--  JOIN customer..--)

    '--   "SELECT SalesOrder_id AS Order_id, SalesOrder_date AS Order_date, " + _
    ''--     " customer.surname, customer.given_names, customer.company, customer.barcode AS CustBarcode,  " + _
    ''--         "SalesOrder.Total_inc,SalesOrder.customer_id as CustId, " + _
    ''--          "customer.phone AS CustPhone, customer.mobile AS CustMobile, SalesOrder.Status AS OrderStatus, " + _
    ''--           " SalesOrder.transaction AS Trans " + _
    ''--    " FROM SalesOrder  LEFT JOIN Customer ON (SalesOrder.customer_id=Customer.Customer_id) " + _
    ''--     " WHERE (SalesOrder.transaction = 'QU') "

    '--  Q u o t e G e t A l l Q u o t e s   --
    '--  Q u o t e G e t A l l Q u o t e s   --

    Public Function quoteGetAllQuotes(ByRef colQuotesList As Collection, _
                               Optional ByVal mlCustomerSearchId As Integer = -1) As Boolean _
                                       Implements _clsRetailHost.quoteGetAllQuotes
        Dim sSql As String
        Dim rstQuote As DataTable  '== ADODB.Recordset

        quoteGetAllQuotes = False
        If mbConnected Then
            '==  quoteGetAllQuotes = mbGetAllQuotes(colQuotesList)
            sSql = "SELECT SalesOrder_id AS Order_id, SalesOrder_date AS Order_date, "
            sSql = sSql & " customer.surname, customer.given_names, customer.company, customer.barcode AS CustBarcode,  "
            sSql = sSql & "SalesOrder.Total_inc,SalesOrder.customer_id as CustId, "
            sSql = sSql & "customer.phone AS CustPhone, customer.mobile AS CustMobile, SalesOrder.Status AS OrderStatus, "
            sSql = sSql & " SalesOrder.transaction AS Trans "
            sSql = sSql & " FROM SalesOrder  LEFT JOIN Customer ON (SalesOrder.customer_id=Customer.Customer_id) "
            sSql = sSql & " WHERE (SalesOrder.transaction = 'QU') "
            If mlCustomerSearchId >= 0 Then
                sSql = sSql & " AND (SalesOrder.customer_id = " & CStr(mlCustomerSearchId) & ") "
            End If
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJet, rstQuote, sSql) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("Failed to get SalesOrder recordset..", MsgBoxStyle.Exclamation)
                rstQuote = Nothing
                Exit Function
                '-Exit Function
            Else '--ok--
                '--make rst into collection..--
                quoteGetAllQuotes = mbMakeRecordCollection(rstQuote, colQuotesList)
            End If '-get.-
            '== rstQuote.Close()
            rstQuote = Nothing
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '-- quoteGetAllQuotes --
    '= = = = = = = = =
    '= = = = = = = = =
    '-===FF->

    '--  QuoteGetQuoteStockList--
    '--  Input Quote (sales-order) id..
    '--  RETURNS Returns full base list of Stock Line Records for Quote..-
    '--   ALSO Returns colExpandInstances as full EXPANDED list of Stock Records for Quote..-
    '--    ie.  for each rSet row, expand Quantity to return multiple 
    '------ collection "rows" as neede..

    Public Function quoteGetStocklist(ByRef order_id As Integer, _
                                       ByRef colStockList As Collection, _
                                         ByRef colExpandInstances As Collection) As Boolean _
                                        Implements _clsRetailHost.quoteGetStocklist
        Dim sSql As String
        Dim rsItems As DataTable  '= ADODB.Recordset
        Dim colRecord As Collection
        Dim col1 As Collection
        '== Dim fldx As ADODB.Field
        Dim intADOType, intQty, qx, sx As Integer
        Dim lngCount, lngExpandedCount As Integer
        Dim sKey, sName, sSqlType As String

        quoteGetStocklist = False
        lngCount = 0
        lngExpandedCount = 0
        If mbConnected Then
            '--retrieve all parts/items for this quote..-
            sSql = "SELECT  stock.cat1,stock.cat2, stock.Description, stock.barcode, SalesOrderLine.quantity AS OrderQty, " & _
                                                                               "SalesOrderLine.sell_inc,SalesOrderLine.stock_id "
            sSql = sSql & "  FROM [SalesOrderLine] LEFT JOIN stock ON (SalesOrderLine.stock_id=stock.stock_id)  "
            sSql = sSql & "WHERE (SalesOrderLine.SalesOrder_id =" & CStr(order_id) & ") "
            sSql = sSql & " ORDER BY stock.Description ASC "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJet, rsItems, sSql) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("Failed to get SalesOrder Items recordset.." & vbCrLf & sSql, MsgBoxStyle.Exclamation)
                '--mbCancelled = True
                '--Me.Hide
                Exit Function
            Else '--ok--
            End If '-got rs--
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If rsItems Is Nothing Then
                MsgBox("SalesOrder Items recordset is empty.." & vbCrLf & sSql, MsgBoxStyle.Exclamation)
                Exit Function
            End If
            '--make rst into collection..--

            '== quoteGetStocklist = mbMakeRecordCollection(rsItems, colStockList)
            '--  HAVE to do it here to expand list based on OrderQty for each stock part..
            colStockList = New Collection
            colExpandInstances = New Collection
            If (Not (rsItems Is Nothing)) AndAlso (rsItems.Rows.Count > 0) Then
                '== Not (rsItems.BOF And rsItems.EOF) Then '--not empty..-
                '== rsItems.MoveFirst()
                For Each datarow1 As DataRow In rsItems.Rows
                    '--  get qty..--
                    '--  create "Qty" "record" instances -
                    intQty = CInt(datarow1.Item("OrderQty"))
                    If (intQty > 0) Then
                        '==For qx = 1 To intQty
                        lngCount += 1   '-- count INSTANCES of stock records for default key..-
                        lngExpandedCount += 1   '-- count INSTANCES of stock records for default key..-
                        colRecord = New Collection
                        For Each column1 As DataColumn In rsItems.Columns    '== fldx In rsItems.Fields
                            col1 = New Collection
                            sName = column1.ColumnName
                            col1.Add(sName, "name")
                            If IsDBNull(datarow1.Item(sName)) Then
                                col1.Add("null", "value")
                            Else  '--ok-
                                col1.Add(Trim(datarow1.Item(sName)), "value")
                            End If
                            '= col1.Add(fldx.Type, "type")
                            '= col1.Add(fldx.DefinedSize, "DefinedSize")

                            '--call global to convert Column-datatype to ADO Type and SqlType..--
                            gbConvertDotNetDataType(column1, intADOType, sSqlType)

                            col1.Add(sSqlType, "sqltype")
                            col1.Add(intADOType, "type")
                            col1.Add(column1.MaxLength, "DefinedSize")
                            colRecord.Add(col1, UCase(sName))
                        Next column1 '== fldx
                        '--add this record instance-
                        colStockList.Add(colRecord, CStr(lngCount)) '=DEFAULT 1-based..=s/n NOT unique== , KEY=SerialNo-.-
                        '--  Add to Expanded collection also..
                        colExpandInstances.Add(colRecord, CStr(lngExpandedCount)) '=DEFAULT 1-based..
                        If (intQty > 1) Then  '--expand 2nd, 3rd etc..
                            For sx = 1 To (intQty - 1)
                                lngExpandedCount += 1
                                colExpandInstances.Add(colRecord, CStr(lngExpandedCount)) '=DEFAULT 1-based..
                            Next sx
                        End If
                        '==Next (qx)
                    End If  '--qty-
                Next datarow1
                '== While Not rsItems.EOF
                '==   rsItems.MoveNext()
                '== End While '--eof.-
                quoteGetStocklist = True
            End If '--empty.-
            '== rsItems.Close()
            rsItems = Nothing
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '-- quoteGetstock --
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- BrowseCreateObject --

    '---- Polymorphism lets us return the parent class type..
    '---   for both RM and RPOS..--

    '== Public Function browseCreateObject(objBrowse1 As clsBrowse22) As Boolean
    '==        Dim browseRM As clsBrowse22
    '==        Dim browseRPOS As clsBrowse22

    '==   browseCreateObject = False
    '==   If mbConnected Then
    '==        '==  (LCase(msProvider) = "retailmanager") Then '--open Jet RM connection..--
    '==            Set browseRM = New clsBrowse22

    '==            browseRM.connection = mCnnJet
    '==            browseRM.DBname = msJetDbName
    '==            browseRM.colTables = mColJetDBInfo
    '==            browseRM.IsSqlServer = False
    '==            Set objBrowse1 = browseRM
    '==            browseCreateObject = True
    '==    Else
    '==      MsgBox "No Retail provider is connected..", vbExclamation
    '==   End If  '--connected..-

    '== End Function  '--BrowseCreateObject--
    '= = = = = = = =  = = = = = == =
    '-===FF->


    '-- Close --


    Public Function closeConnection() As Boolean Implements _clsRetailHost.closeConnection

        If mbConnected Then

            mCnnJet.Close()
            mbConnected = False
        End If

    End Function '--close.--
    '= = = = = = = = = = = =

    '== end class ===
End Class