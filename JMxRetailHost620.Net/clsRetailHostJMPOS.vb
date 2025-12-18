Option Strict Off
Option Explicit On
Imports System.Data
Imports System.Data.OleDb


Public Class clsRetailHostJMPOS
    Implements _clsRetailHost

    '= = = = == == = == =
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


    '==
    '==  NEW DLL- 4219 VERSION
    '==    Created- 4219.1122 22-Nov-2019= 
    '==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..
    '==    THIS class now is PUBLIC..

    '--  Class to encapsulate interface to Retail Host Database..-

    '---- THIS CLASS for JobMatixPOS ONLY.--

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
    '==  grh. JobMatix 3.1.3101 ---  14-Sep-2014 ===
    '==    >> JobMatixPOS version.. (CLONED from RetailManager Class.) 
    '==
    '==   grh  3.1.3101.1025 - 
    '==     >> Add property stockSearchColumns for Browse fulltext search on Stock.
    '==
    '==   grh  3.1.3103.102 - 
    '==     >> quotes retrieval.
    '==
    '==   grh  3.1.3103.204 - 
    '==     >> Tidy up getDatatable error reporting..
    '==
    '==   grh  3.1.3103.423 - 
    '==     >> Fix 'mbGetSerialInfo' to pick up RM imported GR trans...
    '==
    '==   grh  3.1.3107.706 - 
    '==     >> Add "barcode" to property customerSearchColumns for Browse fulltext search..
    '==
    '==   grh  3.1.3107.0801 -  01-Aug-2015 
    '==     >> Connect (dummy) Function. added extra parm "strSchema" to return schema text display to Main..
    '==
    '==   grh  3.1.3107.1015 -  15-Oct-2015 
    '==     >> Staff/Supplier get SQL to translate to RM col.names..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW VERSION--
    '==
    '==   grh  3.3.3311.228 -  28-Feb-2016- 
    '==     >> Implement Staff Lookup..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  3311.304- 04Mar2016-
    '==    >>  Update clsRetailHost/--JMPOS (mbGetSerialInfo) -
    '==            - to add SALE/Customer Info (if any) to SerialInfo for caller.
    '==
    '==  = New 3311.406=
    '==   -- staffGetStaffRecordEx. User Can supply Docket name as key..
    '==
    '==  -- 3311.731- 31July2016-
    '==        >> Updates to Onsite SMS Reminder..  need new method (get Staff List).-
    '==
    '==
    '==  -- 3323.1111- 11-Nov-2016=-
    '==         >>--  Fix to clsRetailHostJMPOS for GetSerials SQL Error..
    '== = = = = 
    '==
    '==  -- 3327.0120- 20-Jan2017=-
    '==         >>--  Fix to "mbGetSerialInfo" to TRIM RM Trail Info while collecting...
    '==
    '=   3357.0205= 05Feb2017=
    '==   should be AS  SerialAudit_Id
    '==     = intSerialAudit_id = CInt(colSerialInfo.Item("Serial_Id")("value"))  '==3311.302-
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
    '--  Note: all results are returned as collections with RetailManager (-like) column names..-
    '----- A. Single records are returned as a collection of field collections
    '----                (Each field is a collection "name"/"value"/"type"(ADO type)/"size"(DefinedSize) ---
    '----- B.  Multiple record results are returned as a collection of records of type A. above..

    '--  BROWSE in place.
    '--  BROWSE in place.  Browse selected table with FlexGrid embedded in client's form.--
    '---     eg clsBrowse22 for Jobs and MYOB Users..-
    '---     eg clsBrowseRPOS for  RetailPointOfSale users..-
    '---        (Polymorphism:  implements clsBrowse22).
    '---   Method:= BrowseCreateObject --  Create browse object for current Retail Host type..
    '---              Creates object from correct class, and returns reference..
    '---              Fills in connection properties....
    '---              returns reference to caller..
    '---              Caller must set remainder of properties. eg FlexGrid ref.
    '---      Browser object will catch Activate, Refresh methods, Find methods etc..
    '= = = = = = = = = = = = = = =

    Private msProvider As String = "" '-- eg: "RetailManager", "JobMatix2"..
    Private mbConnected As Boolean = False
    Private msAppPath As String = ""


    '==  JobMatixPOS stuff-
    Private msServer As String
    Private mCnnSql As OleDbConnection
    Private mColSqlDBInfo As Collection
    Private msSqlDbName As String

    '-- RetailManager stuff..--
    '-- RetailManager stuff..--
    '-- RetailManager stuff..--

    '=JMPOS= Private mCnnJet As OleDbConnection   '== ADODB.Connection
    '=JMPOS= Private mColJetDBInfo As Collection

    '=JMPOS= Private msJetDbName As String
    '=JMPOS= Private msJetUid As String
    '=JMPOS= Private msJetPwd As String

    Private msStockSelectSql As String = ""  '--normal select..
    Private msJobStockSelectSql As String = ""
    Private msCustomerSelectList As String = ""
    Private msStaffSelectSql As String = ""
    Private msSupplierSelectSql As String = ""

    '=3311.228=
    Private msStaffSelectList As String = ""

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

    '=3101.1026=  oleDb..  No Async Sql..
    '== Private mRstSerials As DataTable
    '== Private mRstStockedSerials As DataTable

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
            connection = mCnnSql

        End Get
    End Property '---cnn--
    '= = = = = = = =  == =

    ReadOnly Property colTables() As Collection Implements _clsRetailHost.colTables
        Get

            colTables = mColSqlDBInfo
        End Get
    End Property '--tables..-
    '= = = = = = = = = = = = =

    ReadOnly Property DBname() As String Implements _clsRetailHost.DBname
        Get

            DBname = msSqlDbName
        End Get
    End Property '--dbname-
    '= = = = = = = = = = = = = =

    ReadOnly Property IsSqlServer() As Boolean Implements _clsRetailHost.IsSqlServer
        Get

            IsSqlServer = True  '-- we are Jet..--(R.M.)..
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
            '-   for JMPOS.--
            CustomerSearchColumns = New Object() _
                     {"barcode", "lastname", "firstname", "companyName", "address", "suburb", "email"}
        End Get
    End Property '--dbname-
    '= = = = = = = = = = = = = =

    '--JMPOS  Stock srch columns.-

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

        '=JMPOS= msJetDbName = ""
        '=JMPOS= msJetUid = ""
        '=JMPOS= msJetPwd = ""

        msAppPath = My.Application.Info.DirectoryPath
        If Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"


        '--  Present AS Retail Manager Stock columns---
        '-- FOR NORMAL get stock --
        '--   eg stockGetStockRecord --
        msStockSelectSql = " SELECT  brandName AS cat3,  cat1, cat2, barcode, allow_renaming, " & _
                            " description, stock_id, track_serial, " & _
                            " stock.longDescription AS longdesc, " & _
                            " costExTax AS cost, goods_taxCode AS goods_tax, sellExTax AS sell, sales_taxCode AS sales_tax, " & _
                            " supplier_id, qtyInStock AS quantity " & _
                            " FROM dbo.stock " '=& _
        '== " LEFT JOIN StockBrands AS SB ON (stock.brand_id=SB.brand_id)  "
        '==            " WHERE (barcode='" & s1 & "')"

        '--  Present AS Retail Manager Stock columns---
        '-- FOR Job Parts SPECIAL --
        '--   stockGetJobPartStockInfo --
        msJobStockSelectSql = " SELECT brandName AS cat3,  cat1, cat2, Barcode, allow_renaming, " & _
                            " stock.description AS OriginalDescription, stock.Stock_id, " & _
                            " stock.longDescription AS longdesc, " & _
                            " costExTax AS cost, sellExTax AS OriginalSell, sellExTax AS sell, " & _
                            " qtyInStock AS quantity " & _
                            " FROM dbo.stock "  '= & _
        '== " LEFT JOIN StockBrands AS SB ON (stock.brand_id=SB.brand_id)  "

        '-- 3311- Staff cols..-
        msStaffSelectList = " staff_id, docket_name, barcode, " & _
                               " lastname AS surname, firstname AS given_names, mobile, emailAddress as email "

        '-- Customer cols..-
        msCustomerSelectList = " lastname AS surname, firstname AS given_names, companyName AS company, barcode, "
        msCustomerSelectList &= " customer_id, isAccountCust AS account,  "
        msCustomerSelectList &= " suburb, state, postcode, country, address as addr1, '' AS addr2, '' AS addr3, "
        msCustomerSelectList &= " phone, fax, abn, mobile, email, date_modified "

        '--3107.1015=  Present SUPPLIER AS Retail Manager Stock columns---
        '-- FOR NORMAL get supplier --

        msSupplierSelectSql = "SELECT barcode, inactive, supplierName as supplier, abn, "
        msSupplierSelectSql &= " contactName AS main_contact, contactPosition AS main_position, "
        msSupplierSelectSql &= " address AS main_addr1, '' AS main_addr2, '' AS main_addr3, "
        msSupplierSelectSql &= " suburb AS main_suburb, state AS main_state, postcode AS main_postcode, "
        msSupplierSelectSql &= "  country AS main_country, fax AS main_fax, phone AS main_phone, emailAddress AS main_email, "
        msSupplierSelectSql &= " reject_backorders, freight_free, deliveryDays AS delivery_delay, supplier_id "
        msSupplierSelectSql &= "  FROM dbo.supplier "

        msStaffSelectSql = "SELECT barcode, lastname AS surname, firstname AS given_names, "
        msStaffSelectSql &= " docket_name, dateOfBirth AS dob, "
        msStaffSelectSql &= " homePhone AS phone, mobile, emailAddress as email, staff_id "
        msStaffSelectSql &= " FROM dbo.staff  "

        '--  set up browser prefs..--
        '--  staff--
        mColPrefsStaff = New Collection
        mColPrefsStaff.Add("barcode")
        mColPrefsStaff.Add("lastname AS surname")
        mColPrefsStaff.Add("firstname AS given_names")
        mColPrefsStaff.Add("docket_name")
        mColPrefsStaff.Add("staff_id")

        '-- Customer --
        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("lastname AS surname")
        mColPrefsCustomer.Add("firstname AS given_names")
        mColPrefsCustomer.Add("date_modified")
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("companyName AS company")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("customer_id")
        mColPrefsCustomer.Add("isAccountCust AS account")
        mColPrefsCustomer.Add("address AS addr1")
        mColPrefsCustomer.Add("suburb")
        mColPrefsCustomer.Add("email")

        '--  stock--
        mColPrefsStock = New Collection
        mColPrefsStock.Add("description")
        mColPrefsStock.Add("cat1")
        mColPrefsStock.Add("cat2")
        mColPrefsStock.Add("barcode")
        mColPrefsStock.Add("sellExTax AS sell")
        mColPrefsStock.Add("stock_id")
        mColPrefsStock.Add("allow_renaming")

        '--  supplier--
        mColPrefsSupplier = New Collection
        mColPrefsSupplier.Add("supplierName AS supplier")
        mColPrefsSupplier.Add("supplier_id")
        mColPrefsSupplier.Add("barcode")
        '= mColPrefsSupplier.Add("supplier")
        mColPrefsSupplier.Add("address AS main_addr1")
        '= mColPrefsSupplier.Add("main_addr2")
        '= mColPrefsSupplier.Add("main_addr3")
        mColPrefsSupplier.Add("suburb AS main_suburb")
        mColPrefsSupplier.Add("state AS main_state")
        mColPrefsSupplier.Add("postcode AS main_postcode")
        mColPrefsSupplier.Add("country AS main_country")
        mColPrefsSupplier.Add("phone AS main_phone")
        mColPrefsSupplier.Add("fax AS main_fax")
        mColPrefsSupplier.Add("emailAddress AS main_email")

    End Sub '-Class_Initialize_Renamed-
    '= = = = = = = = = = = = =  = = ==  =

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub '--initialise..-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- R e t a i l M a n a g er --
    '-- Connect to Retail Manager..--

    '-- JMPOS - Connection is done by JobMatix Main..

    '-- R e t a i l JMPOS --
    '-- get  record..--

    Private Function mbJMPOS_GetRecord(ByRef sSql As String, _
                                    ByRef colFields As Collection) As Boolean
        Dim colFld As Collection '--"name"=, "value"-
        '= Dim fld1 As ADODB.Field
        Dim s1, sName, sSqlType As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim intADOType As Integer

        mbJMPOS_GetRecord = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            MsgBox("Failed to get POS recordset. Error was:" & vbCrLf & _
                     vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
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
                        colFld.Add("null", "value")
                    End If
                    '--call global to convert Column-datatype to ADO Type and SqlType..--
                    gbConvertDotNetDataType(column1, intADOType, sSqlType)

                    colFld.Add(intADOType, "type")
                    colFld.Add(sSqlType, "sqltype")
                    colFld.Add(column1.MaxLength, "DefinedSize")
                    '--colFld.Add(fld1.DefinedSize, "DefinedSize")
                    colFields.Add(colFld, LCase(sName))
                Next column1 '- fld1
                mbJMPOS_GetRecord = True
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
            mbMakeRecordCollection = True
        End If '-nothing-

    End Function '--make --
    '= = = = = = = = = = = = =
    '-===FF->

    '--  Refresh JMPOS Purchases List.--

    Private Function mbRefreshPurchasesList(ByVal lngId As Integer, _
                                             ByRef rs1 As DataTable) As Boolean
        '==Dim lngCount As Integer
        Dim sSql As String
        Dim sWhere1 As String

        mbRefreshPurchasesList = False

        '--  NOTE:  JMPOS Invoices have Customer_Id ONLY. (No Customer barcode.)--

        If (lngId >= 0) Then '--was supplied..-
            sWhere1 = "(invoice.customer_Id= " & CStr(lngId) & ")"
        Else
            MsgBox("No Customer ID supplied for Get Purchases..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        sSql = " SELECT invoice.invoice_id, invoice_date, invoiceLine.total_inc, "
        sSql &= "   cat1, cat2, invoiceLine.description, stock.barcode, invoice.transactionType,  "
        sSql &= "  invoiceLine.stock_id, invoiceLine.quantity, "
        sSql &= "     invoiceLine.sell_inc, invoice.customer_Id "
        sSql &= "  FROM invoice "
        sSql &= "    LEFT JOIN invoiceLine ON (invoiceLine.invoice_id=invoice.invoice_id) "
        sSql &= "     LEFT JOIN stock ON (stock.stock_id=invoiceLine.stock_id)"
        sSql &= " WHERE " & sWhere1 & " AND  "
        sSql &= "           ((TransactionType='SALE') OR (TransactionType='refund')) "
        sSql &= "   ORDER BY invoice.invoice_id DESC; "

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If gbGetDataTable(mCnnSql, rs1, sSql) Then '--ok-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '== LabPurchases.Caption = "Purchase History (" & lngCount & " Retail Manager Docket Lines)"
            mbRefreshPurchasesList = True
        Else '--failed..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Invoice recordset.." & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        End If
    End Function '--refresh..-
    '= = = = = = = = = = =  = = =  =

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
        Dim dtSerials As DataTable
        '== Dim rstStockedSerials As ADODB.Recordset
        Dim dt1 As DataTable '= ADODB.Recordset
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
        '= 3323.1111= asColumns = New Object() {"SerialAudit.number", "Barcode", "Cat1", "stock.description"}
        asColumns = New Object() {"SA.serialNumber", "Barcode", "Cat1", "stock.description"}
        sSrchSql = gbMakeTextSearchSql(sArg, asColumns)

        labStatus.Text = "Fetching Serial-Audit RecordSet.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '--  Get full list SerialAudit table.. ( Type 'GR' only.)--
        '---- SerialAudit links SerailNo to SerialAuditTrail, which --
        '---    gives Stock_id, Goods_Id and and GoodsLine_Id..--

        sSql = " SELECT  SA.serialNumber AS serialNo, "
        sSql &= " InStock= CASE SA.isInStock WHEN 0 THEN '--' ELSE 'YES' END,  "
        sSql &= "  cat1, stock.Barcode, stock.description,  "
        sSql &= "   SAT.type_id AS Goods_Id, "
        sSql &= "    '--' AS InvoiceNo, '--' AS InvoiceDate, '--' AS Supplier, "
        sSql &= "    SA.stock_id,  costExTax AS cost "
        sSql &= "  FROM SerialAudit AS SA "
        sSql &= "    LEFT JOIN Stock ON (Stock.stock_id=SA.stock_id) "
        sSql &= "     LEFT JOIN SerialAuditTrail AS SAT "
        sSql &= "                 ON (SAT.SerialAudit_Id=SA.Serial_Id)  "
        sSql &= "   WHERE  ((SAT.tran_type LIKE 'GoodsRec%') OR (SAT.tran_type='SI')  OR (SAT.tran_type='GR')) "

        If (sSrchSql <> "") Then
            sSql = sSql & " AND " & sSrchSql
        End If
        sSql = sSql & " ORDER BY serialNo; "   '= SerialAudit.number -
        '====MsgBox "SQL is: " + vbCrLf + sSql
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        mlFetchComplete = -1 '--not ready-
        '--mRst.Properties("Initial Fetch Size") = 0       '--ensure progress event--
        '--mRst.Properties("Background Fetch Size") = 15  '--default--
        '== mRstSerials = New DataTable  '= ADODB.Recordset
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
        '== End While '--fetch--

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Not gbGetDataTable(mCnnSql, dtSerials, sSql) Then  '==  (mlFetchComplete <> 0) Then
            sMsg = "ERROR (in mbGetAllSerials) getting recordset for SerialAudit table.." & vbCrLf & _
                     "Error " & gsGetLastSqlErrorMessage() & vbCrLf & _
                      "=== end error msg ===" & vbCrLf
            Call gbLogMsg(gsErrorLogPath, sMsg)
            strErrorReport = sMsg
            dtSerials = Nothing
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
        If (dtSerials Is Nothing) OrElse (dtSerials.Rows.Count <= 0) Then
            '=(mRstSerials.BOF And mRstSerials.EOF) Then '--empty os ok..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '===MsgBox "No records for SerialAudit recordset.." + vbCrLf + sSql, vbCritical
            dtSerials = Nothing
            Exit Function
        End If
        labStatus.Text = "Building Initial Serial Collection.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '===  Call mbLoadListView(mRstSerials, ListView1)
        '== mRstSerials.MoveFirst()
        For Each datarow1 As DataRow In dtSerials.Rows
            colRecord = New Collection
            sSerialNo = datarow1.Item("SerialNo")
            If (sSerialNo <> "") Then '--ignore blank serials.--
                For Each column1 As DataColumn In dtSerials.Columns  '= fldx In mRstSerials.Fields
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

        '- NO Stocked Serials Table for JMPOS..==
        '- NO Stocked Serials Table for JMPOS..==

        labStatus.Text = "Fetching Goods Invoices.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '-- load collection of goods invoices to get Invoice Info..--
        System.Windows.Forms.Application.DoEvents()
        sSql = "SELECT goods_id, invoice_no, invoice_date, supplier.supplierName AS supplier  "
        sSql = sSql & " FROM GoodsReceived LEFT JOIN supplier ON (GoodsReceived.supplier_Id=supplier.supplier_Id)  "
        If Not gbGetDataTable(mCnnSql, dt1, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Goods recordset.." & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Critical)
            '=== mbCancelled = True
        Else '--ok--
            colGoods = New Collection
            If (Not (dt1 Is Nothing)) AndAlso (dt1.Rows.Count > 0) Then  '--ok.. not empty--
                '==-Not (rs1.BOF And rs1.EOF) Then '--ok.. not empty--
                '= rs1.MoveFirst()
                '--build Goods collection.-
                For Each datarow1 As DataRow In dt1.Rows
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
                        '==On Error Resume Next
                        '==colInvoice = colGoods.Item(sKey) '--get info this invoice..-
                        '==lngError = Err().Number
                        '== On Error GoTo 0 '=====   RefreshSerials_Error
                        If colGoods.Contains(sKey) Then  '=(lngError = 0) Then '--found in goods invoices...-
                            colInvoice = colGoods.Item(sKey) '--get info this invoice..-
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
        '== rs1 = Nothing
        colGoods = Nothing
        col1 = Nothing
        Exit Function

RefreshSerials_Error:
        k = Err.Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in  'mbRefreshSerials' function.." & vbCrLf & _
                "Error: " & k & ":  " & ErrorToString(k) & vbCrLf & _
                    "Serial lookup is may be incomplete..", MsgBoxStyle.Exclamation)
        '= rs1 = Nothing
        colGoods = Nothing
        col1 = Nothing
        Exit Function

    End Function '--refresh Serials..--
    '= = = = = = = = = = = =
    '-===FF->

    '--GetSerialInfo-
    '---  Lookup/JOIN tables to get info on specific SerialNo..-

    '--Every manufacturer has it's own naming scheme for serial numbers, 
    '--   and they are not guaranteed to be globally unique across manufacturers, 
    '-- but they should be unique per manufacturer as, 
    '--       after all, they want to be able to identify a specific unit. 
    '--  http://serverfault.com/questions/300448/is-a-hard-drives-serial-number-globally-unique

    '-- (Input: strSeriallNo, Stock_id;
    '--   Output: Stock_Id, barcode, cat1, cat2, description, cost, sell,
    '--             Goods_Id, Goodsline_id, Qty, Supplier_Id, SupplierCode, InStock (YES/NO) --)

    '--  Lookup SerialNo in SerialAudit table..--
    '---- SerialAudit links SerailNo to SerialAuditTrail, which --
    '---    gives Stock_id, Goods_Id and and GoodsLine_Id..--

    '-- FOR RA's: --
    '--Lookup Goodsline/Stock to get Line Qty and Supplier_Id, SupplierCode, info..--

    '--  FOR Jobmaint/NewPart, we need In-stock YES/NO column..--
    '--    So now check that this serialNo is actually still in stock..--

    '--= 3101.1024= Stock_id is optional (Send -1 if not being supplied)..
    '--  ALSO:  colAllSerialsInfo is now a COLLECTION of reord collections..-
    '==    Since if not Stock_id input, then multiple serials may be discovered..-

    Private Function mbGetSerialInfo(ByVal intInputStock_id As Integer, _
                                       ByVal sSerialNo As String, _
                                           ByRef colAllSerialsInfo As Collection) As Boolean
        Dim sSql, sName, sValue As String
        Dim sInStock As String = "NO"
        '== Dim sItemBarcode As String
        Dim colSerialInfo, colSale_info As Collection
        Dim col1 As Collection
        Dim colItemFields As Collection
        Dim datatable1 As DataTable
        Dim lngGoodsId As Integer = -1
        Dim lngGoodsLineId As Integer = -1
        Dim lngSupplierId, intSerialAudit_id, intDocket_id As Integer
        Dim sSupplierCode As String

        mbGetSerialInfo = False
        sSql = " SELECT  SA.serial_Id AS SerialAudit_Id, SA.SerialNumber AS SerialNo, SA.Serial_Id, "
        sSql &= "  SA.stock_id AS stock_id, SAT.tran_type, isInStock, "
        sSql &= "  SAT.Type_id AS Goods_Id, "
        sSql &= "  SAT.Type_Line_id AS GoodsLine_Id, "
        sSql &= "    cat1, cat2, stock.Barcode, "
        sSql &= "     stock.description, stock.Stock_id, costExTax AS cost, sellExTax AS sell "
        sSql &= "  FROM SerialAudit AS SA"
        sSql &= "   LEFT JOIN Stock ON (Stock.stock_id=SA.stock_id) "
        sSql &= "    LEFT OUTER JOIN SerialAuditTrail AS SAT "
        '== 3103.423= Can Import RM GR.. stuff.
        '===   sSql &= "     ON ((SAT.SerialAudit_Id=SA.Serial_Id) AND (SAT.tran_type LIKE 'GoodsRec%'))"
        sSql &= "     ON ((SAT.SerialAudit_Id=SA.Serial_Id) AND " & _
                                    "((SAT.tran_type LIKE 'GoodsRec%') OR (SAT.tran_type ='GR')) )"
        '== sSql = sSql & "   WHERE (SA.SerialNumber='" & sSerialNo & "') AND ( SA.stock_id=" & CStr(intInputStock_id) & ");"
        sSql = sSql & "   WHERE (SA.SerialNumber='" & sSerialNo & "') "
        '=3101.1024=  Filter for stock_id if provided..-
        If (intInputStock_id > 0) Then
            sSql &= " AND ( SA.stock_id=" & CStr(intInputStock_id) & "); "
        End If

        '=3101.1024=  may be multiple records returned.-
        If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
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
            '= intSerialAudit_id = CInt(colSerialInfo.Item("Serial_Id")("value"))  '==3311.302-
            '=3357.0205= SerialAudit_Id
            intSerialAudit_id = CInt(colSerialInfo.Item("SerialAudit_Id")("value"))  '==3311.302-
            sInStock = "NO"
            If (colSerialInfo.Item("isInStock")("value")) Then  '--(ie If Is True..  BIT columns come as BOOLEAN in DataTable.-
                sInStock = "YES"
            End If
            '--  get Qty and supplier info..-
            '-- FOR RA's: --
            If IsNumeric(colSerialInfo.Item("Goods_Id")("value")) AndAlso _
                                             (CInt(colSerialInfo.Item("Goods_Id")("value")) > 0) Then
                lngGoodsId = CInt(colSerialInfo.Item("Goods_Id")("value"))
                If IsNumeric(colSerialInfo.Item("GoodsLine_Id")("value")) Then
                    lngGoodsLineId = CInt(colSerialInfo.Item("GoodsLine_Id")("value"))
                End If
            Else  '-no goods info..
                MsgBox("No GoodsReceived info found- " & _
                          "   for Serial: " & sSerialNo & ";  Stock Id: " & intInputStock_id, MsgBoxStyle.Exclamation)
                Exit Function
            End If
            '==    lngStockId = intInputStock_id  '- CInt(colSerialInfo.Item("Stock_Id")("value"))
            '--Lookup Goodsline/Stock to get Line Qty and Supplier_Id, SupplierCode, info..--
            sSql = " SELECT GRL.Goods_Id, GR.supplier_id AS supplier_id, supplier.supplier_id, "
            sSql = sSql & "     GRL.quantity AS Qty  "
            sSql = sSql & " FROM (GoodsReceivedLine AS GRL "
            sSql = sSql & "  LEFT JOIN (GoodsReceived AS GR "
            sSql = sSql & "    LEFT JOIN Supplier "
            sSql = sSql & "    ON (Supplier.Supplier_id=GR.supplier_id) )"
            sSql = sSql & "  ON  (GR.Goods_Id=GRL.Goods_Id) )"
            sSql = sSql & "  WHERE (GRL.Goods_Id=" & lngGoodsId & ")  "
            sSql = sSql & "        AND (GRL.Line_Id=" & lngGoodsLineId & ") " '== AND (supplierCode.stock_id=" & lngStockId & ")  "
            If mbJMPOS_GetRecord(sSql, colItemFields) Then
                '--  add Qty, Supplier_id, SupplierCode "columns" (fld collections.) to serial info..-
                colSerialInfo.Add(colItemFields.Item("Qty"))
                colSerialInfo.Add(colItemFields.Item("supplier_id"))
                '===colSerialInfo.Add colItemFields("supplierCode")
                '--  one more query to get SupplierCode..--
                lngSupplierId = CInt(colItemFields.Item("Supplier_Id")("value"))
                sSql = " SELECT SupplierCode.supCode AS supplierCode, supplierCode.stock_id  "
                sSql = sSql & " FROM SupplierCode "
                sSql = sSql & " WHERE (SupplierCode.Supplier_id=" & CStr(lngSupplierId) & ") "
                sSql = sSql & "          AND (supplierCode.stock_id=" & intInputStock_id & ")  "
                sSupplierCode = "--"
                If mbJMPOS_GetRecord(sSql, colItemFields) Then
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
                '--  add InStock status. to record..
                col1 = New Collection
                col1.Add("InStock", "name")
                col1.Add(sInStock, "value")
                '== col1.Add(ADODB.DataTypeEnum.adChar, "type")
                col1.Add(ADODB_DataTypeEnum_adChar, "type")
                col1.Add(3, "DefinedSize")
                colSerialInfo.Add(col1, "InStock")

                '=3311.304= -- Add SALE info if any--
                '==      Add SALE/Customer Info (if any) to SerialInfo for caller.
                '-- Look for Sale (POS) and SA/IV (RM migrated) Trail records for this Serial-

                colSale_info = New Collection  '--return empty collection if no sale.
                sSql = " SELECT  SAT.stock_id AS stock_id, SAT.tran_type, "
                sSql &= "   SAT.type_id, SAT.trail_date, SAT.is_RM_transaction, "
                sSql &= "     SAT.Type_Line_id AS InvoiceLine_Id, SAT.RM_tr_detail, "
                sSql &= "     invoice.invoice_id, invoice.customer_id, customer.barcode AS cust_barcode, "
                sSql &= "      customer.companyname, customer.lastname, customer.firstname "
                sSql &= "  FROM SerialAuditTrail AS SAT "
                sSql &= "   LEFT OUTER JOIN (invoice "
                sSql &= "       LEFT OUTER JOIN Customer "
                sSql &= "       ON (invoice.customer_id=customer.customer_id)) "
                sSql &= "   ON (invoice.invoice_id= SAT.type_id) "
                sSql &= "    WHERE (SAT.SerialAudit_Id= " & intSerialAudit_id & ") "
                sSql &= "      AND ( (SAT.tran_type='sale') OR (SAT.tran_type='SA') OR (SAT.tran_type='IV')) "
                If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
                    MsgBox("ERROR in find SerialAudit Invoice info for Serial: " & sSerialNo & vbCrLf & _
                                             gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    '==Exit Function
                Else  '--worked-
                    If (Not (datatable1 Is Nothing)) AndAlso (datatable1.Rows.Count > 0) Then
                        '- have data..  use last row-
                        Dim datarow1 As DataRow = datatable1.Rows(datatable1.Rows.Count - 1)
                        If Not IsDBNull(datarow1.Item("type_id")) Then
                            '-add sale info to Serial Info-
                            If (CInt(datarow1.Item("is_RM_transaction")) <> 0) Then  '-RM migrated trail record.
                                '-- Extract Invoice no and customer info from "RM_tr_detail"-
                                '= THIS is how frmImport constructs the Invoice/Cust Detail..-
                                '== sDetail = "RM_trans_type=" & sTrailType & ";  " 
                                '== sDetail &= "RM-_sale_Invoice-No=" & intType_id & "; " & _
                                '== "Customer_barcode=" & rmRow1.Item("barcode") & "; " & _
                                '==   "Customer_company=" & rmRow1.Item("company") & "; " & _
                                '=     "Customer_name=" & rmRow1.Item("customer_name")
                                Dim sRM_fields() As String = Split(datarow1.Item("RM_tr_detail"), ";")
                                Dim intPos As Integer
                                For Each sField As String In sRM_fields
                                    intPos = InStr(sField, "=")
                                    If (intPos > 0) Then
                                        '==3327.0120=  MUST TRIM.  20Jan2017=
                                        sName = Trim(Left(sField, intPos - 1))  '--name-
                                        sValue = Trim(Mid(sField, intPos + 1))
                                        col1 = New Collection
                                        col1.Add(sName, "name")
                                        col1.Add(sValue, "value")
                                        col1.Add(ADODB_DataTypeEnum_adVarChar, "type")
                                        col1.Add(120, "DefinedSize")
                                        colSale_info.Add(col1, sName)
                                    End If  '-intpos-
                                Next sField
                            Else  '- Is a JobMatix POS transaction.
                                col1 = New Collection
                                col1.Add("POS_sale_invoice_no", "name")
                                col1.Add(datarow1.Item("type_id"), "value")
                                col1.Add(ADODB_DataTypeEnum_adInteger, "type")
                                col1.Add(4, "DefinedSize")
                                colSale_info.Add(col1, "sale_invoice_no")

                                '- customer stuff.
                                col1 = New Collection
                                col1.Add("customer_barcode", "name")
                                col1.Add(datarow1.Item("cust_barcode"), "value")
                                col1.Add(ADODB_DataTypeEnum_adVarChar, "type")
                                col1.Add(40, "DefinedSize")
                                colSale_info.Add(col1, "sale_customer_barcode")
                                '-company-
                                col1 = New Collection
                                col1.Add("customer_company", "name")
                                col1.Add(datarow1.Item("companyname"), "value")
                                col1.Add(ADODB_DataTypeEnum_adVarChar, "type")
                                col1.Add(100, "DefinedSize")
                                colSale_info.Add(col1, "sale_customer_company")
                                '-name-
                                col1 = New Collection
                                col1.Add("customer_name", "name")
                                col1.Add(datarow1.Item("firstname") & " " & datarow1.Item("lastname"), "value")
                                col1.Add(ADODB_DataTypeEnum_adVarChar, "type")
                                col1.Add(100, "DefinedSize")
                                colSale_info.Add(col1, "sale_customer_name")
                            End If  '-RM-
                            '-sale date-
                            col1 = New Collection
                            col1.Add("sale_date", "name")
                            col1.Add(datarow1.Item("trail_date"), "value")
                            col1.Add(ADODB_DataTypeEnum_adDate, "type")
                            col1.Add(8, "DefinedSize")
                            colSale_info.Add(col1, "sale_date")
                            '- done-
                        End If  '-null-
                    End If  '-nothing
                End If
                colSerialInfo.Add(colSale_info, "item_sale_info")
                mbGetSerialInfo = True

                mbGetSerialInfo = True
            Else
                '--second select failed..-
                MsgBox("Failed to find GoodsLine info for Serial: " & sSerialNo, MsgBoxStyle.Exclamation)
            End If
        Next colSerialInfo
        '=3101= If mbJMPOS_GetRecord(sSql, colSerialInfo) AndAlso _
        '=3101=                         (Not (colSerialInfo Is Nothing)) AndAlso (colSerialInfo.Count > 0) Then
        '=3101= Else
        '=3101= '-First SELECT failed..
        '=3101= MsgBox("Failed to find SerialAudit info- " & _
        '=3101=                   "   for Serial: " & sSerialNo & ";  Stock Id: " & intInputStock_id, MsgBoxStyle.Exclamation)
        '=3101= End If '--lookup serial audit..-
        colItemFields = Nothing
        col1 = Nothing
    End Function '--GetSerialInfo-
    '= = = = = = = = = = = =
    '-===FF->

    '--  Get Goods Invoice..--
    '-- JMPOS 3.1.3101.1022-
    '--  Return goods info in RetailManager form..

    Private Function mbGetGoodsInvoiceInfo(ByVal lngGoodsId As Integer, _
                                            ByRef colInvoiceInfo As Collection) As Boolean
        Dim sSql As String

        mbGetGoodsInvoiceInfo = False
        sSql = " SELECT GR.goods_id, invoice_no, invoice_date, goods_date,  "
        sSql &= " orderNoSuffix AS order_no, order_id, supplier.supplier_id as supplier_id, "
        sSql &= "supplier.barcode as SupplierBarcode, supplierName AS supplier, "
        sSql &= "contactName AS main_contact, contactPosition AS main_position, "
        sSql &= " address AS main_addr1, '' AS main_addr2, '' AS main_addr3, "
        sSql &= " suburb AS main_suburb, state AS main_state, postcode AS main_postcode, country AS main_country, "
        sSql &= " phone AS main_phone, fax AS main_fax,  emailAddress AS main_email  "
        sSql &= "  FROM GoodsReceived AS GR  "
        sSql &= "      LEFT JOIN Supplier "
        sSql &= "         ON  (supplier.supplier_id= GR.supplier_id)   "
        sSql &= "  WHERE (GR.goods_id=" & lngGoodsId & "); "
        If mbJMPOS_GetRecord(sSql, colInvoiceInfo) Then
            mbGetGoodsInvoiceInfo = True
        Else
            MsgBox("Can't find Goods Recvd Info..", MsgBoxStyle.Exclamation)
        End If '--lookup goods..-

    End Function '-goodsInvoice..-
    '= = = = = = = = = = = =
    '-===FF->

    '=== MULTI-HOST -- Public Interface ====
    '=== MULTI-HOST -- Public Interface ====
    '=== MULTI-HOST -- Public Interface ====

    '- Connect-  Jet RetailManager---
    '- Connect-  NOT USED for JobMatixPOS ----

    Public Function connect(ByRef sServer As String, _
                             ByRef sDSN As String, _
                               ByRef sUid As String, _
                                   ByRef sPwd As String, _
                                  ByRef bNewPath As Boolean, _
                                   ByRef strSchema As String) As Boolean _
                             Implements _clsRetailHost.connect
        '=JMPOS= Dim sSchema As String
        '=JMPOS= Dim sConnectLog As String
        '=JMPOS=  Dim s1 As String
        connect = False
        '=JMPOS=     msProvider = "retailmanager" '=sProvider '--save..-
        '=JMPOS= '--  "sServer" not used for MYOB..-
        '=JMPOS=    msJetDbName = sDSN
        '=JMPOS=     msJetUid = sUid
        '=JMPOS=     msJetPwd = sPwd
        '=JMPOS=     If mbRMConnect(bNewPath, sConnectLog) Then
        '=JMPOS=         connect = True
        '=JMPOS=         mbConnected = True
        '=JMPOS= '==If bNewPath Then  '--return new values
        '=JMPOS= '--  must return path in case it was blank originally..-
        '=JMPOS=         sDSN = msJetDbName
        '=JMPOS=        sUid = msJetUid
        '=JMPOS=        sPwd = msJetPwd
        '=JMPOS= '==End If  '--newpath=
        '=JMPOS=         s1 = "JobMatix=V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & _
        '=JMPOS=                  ", Build: " & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & "="
        '=JMPOS= '--  log schema..--
        '=JMPOS=         sSchema = gsShowJetSchema(msJetDbName, mColJetDBInfo)
        '=JMPOS=         sSchema = sSchema & vbCrLf & "= = = " & s1 & " = = =" & vbCrLf & "===== =====" & vbCrLf
        '=JMPOS=        Call glSaveTextFile(msAppPath & "RetailM_Schema.txt", sConnectLog & vbCrLf & sSchema)
        '=JMPOS=     End If '--connect..-
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

        '-- save connection.
        mCnnSql = connection
        mColSqlDBInfo = colTables
        msSqlDbName = DBname
        msServer = sServer
        msProvider = "JobMatixPOS"

        mbConnected = True

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
    '- For JobMatix POS- Must Translate..

    '= Currently (3101.929) IS NOT USED..
    '= Currently (3101.929) IS NOT USED..
    '= Currently (3101.929) IS NOT USED..
    '= Currently (3101.929) IS NOT USED..

    Function getOriginalColumnName(ByVal strTableName As String, _
                                         ByVal strGridColName As String) As String _
                                          Implements _clsRetailHost.getOriginalColumnName
        Dim sOldColumnName As String = ""

        sOldColumnName = strGridColName   '=Default-
        Select Case LCase(strTableName)
            Case "staff"
                sOldColumnName = strGridColName   '==Temp-
            Case "customer"
                Select Case LCase(strGridColName)
                    Case "surname"
                        sOldColumnName = "lastName"
                    Case "givennames"
                        sOldColumnName = "firstName"
                    Case "company"
                        sOldColumnName = "companyName"
                    Case "account"
                        sOldColumnName = "isAccountCust"
                    Case Else
                        sOldColumnName = strGridColName
                End Select  '-- Customer-
                sOldColumnName = strGridColName   '==Temp-
            Case "stock"
                sOldColumnName = strGridColName   '==Temp-
            Case "supplier"
                sOldColumnName = strGridColName   '==Temp-
            Case Else
                sOldColumnName = strGridColName   '==Temp-
        End Select '--tablename-


        getOriginalColumnName = sOldColumnName

    End Function  '==getOriginalColumnName-
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
                sSql = msStaffSelectSql & " WHERE staff_id=" & CStr(lngId) & "; "
            Case "customer"
                sBarcode = colSelectedRow.Item("barcode")("value")
                lngId = CInt(colSelectedRow.Item("customer_id")("value"))
                sSql = "Select " & msCustomerSelectList & " FROM [customer] WHERE customer_id=" & CStr(lngId) & "; "
            Case "stock"
                lngId = CInt(colSelectedRow.Item("stock_id")("value"))
                sSql = msStockSelectSql & " WHERE stock_id=" & CStr(lngId) & "; "

            Case "supplier"
                lngId = CInt(colSelectedRow.Item("supplier_id")("value"))
                sSql = msSupplierSelectSql & " WHERE supplier_id=" & CStr(lngId) & "; "

            Case Else
                browseGetSelectedRecord = False
                Exit Function
        End Select
        browseGetSelectedRecord = mbJMPOS_GetRecord(sSql, colFullRecord)

    End Function '--get full record..-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '=331.228= Staff Lookup.=
    '--   Browse/Select STAFF --
    '--       Output:Full RM Staff Record.--
    '---   Returns FALSE if browse cancelled.--

    Public Function staffLookup(ByRef colRecord As Collection) As Boolean _
                              Implements _clsRetailHost.staffLookup
        '== Dim colPrefs As Collection
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
            frmBrowse1.connection = mCnnSql '--Retail Manager Jet connenction..- 
            frmBrowse1.DBname = msSqlDbName
            frmBrowse1.tableName = "staff"
            frmBrowse1.IsSqlServer = True   '= False '--bIsSqlServer
            frmBrowse1.colTables = mColSqlDBInfo
            '--  pass preferred cols..-
            frmBrowse1.PreferredColumns = mColPrefsStaff  '-- colPrefs
            frmBrowse1.ShowPreferredColumnsOnly = True

            frmBrowse1.ShowDialog()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow '--get grid row selected..-
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
                    sSql = "SELECT " & msStaffSelectList & " FROM [staff] WHERE staff_id=" & CStr(lngId) & "; "
                    If Not mbJMPOS_GetRecord(sSql, colRecord) Then
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
    End Function '--lookupCust.-
    '= = = = = = = = = = = = = = = =
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
            '=== GetStaffRecord = mbLookupStaff(CStr(varBarcode), colStaffInfo)
            If (strBarcode <> "") Then '--was supplied..-
                sSql = msStaffSelectSql & " WHERE barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = msStaffSelectSql & " WHERE staff_id=" & Str(lngId) & "; "
            Else
                MsgBox("No Staff ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            staffGetStaffRecord = mbJMPOS_GetRecord(sSql, colStaffInfo)
        Else
            MsgBox("No Retail provider connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '--get staff..-
    '= = = = = = = = = = = = = =

    '=3311.406= --  Get Staff record Ex--
    '---  (Input: Barcode;  OR DOCKET_NAME- Output: Full RM staff Record incl "docket_name"..)

    Public Function staffGetStaffRecordEx(ByVal strBarcode As String, _
                                         ByVal lngId As Integer, _
                                         ByVal strDocketName As String, _
                                          ByRef colStaffInfo As Collection) As Boolean _
                                        Implements _clsRetailHost.staffGetStaffRecordEx
        Dim sSql As String
        staffGetStaffRecordEx = False
        If mbConnected Then
            '=== GetStaffRecord = mbLookupStaff(CStr(varBarcode), colStaffInfo)
            If (strBarcode <> "") Then '--was supplied..-
                sSql = msStaffSelectSql & " WHERE barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = msStaffSelectSql & " WHERE staff_id=" & Str(lngId) & "; "
            ElseIf (strDocketName <> "") Then '--was supplied..-
                sSql = msStaffSelectSql & " WHERE docket_name='" & strDocketName & "'; "
            Else
                MsgBox("No Staff ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            staffGetStaffRecordEx = mbJMPOS_GetRecord(sSql, colStaffInfo)
        Else
            MsgBox("No Retail provider connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
    End Function '--get staff..-
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
            sSql = msStaffSelectSql
            sSql = sSql & " ORDER BY docket_name; "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
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
            frmBrowse1.connection = mCnnSql '--Retail Manager Jet connenction..- 
            frmBrowse1.DBname = msSqlDbName
            frmBrowse1.tableName = "customer"
            frmBrowse1.IsSqlServer = True   '= False '--bIsSqlServer
            frmBrowse1.colTables = mColSqlDBInfo
            '--  pass preferred cols..-
            frmBrowse1.PreferredColumns = mColPrefsCustomer '-- colPrefs
            frmBrowse1.ShowPreferredColumnsOnly = True

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
                    sSql = "SELECT " & msCustomerSelectList & " FROM [customer] WHERE customer_id=" & CStr(lngId) & "; "
                    If Not mbJMPOS_GetRecord(sSql, colRecord) Then
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
            sSql = " SELECT " & msCustomerSelectList & " FROM [customer] ORDER BY surname; "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
                MsgBox("Failed to get Customet List recordset.." & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
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
    End Function '--lookup staff..-
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
                sSql = "SELECT " & msCustomerSelectList & " FROM [customer] WHERE customer_id=" & Str(lngId) & "; "
            ElseIf (strBarcode <> "") Then '--was supplied..-
                sSql = "SELECT " & msCustomerSelectList & " FROM [customer] WHERE barcode='" & strBarcode & "'; "
            Else
                MsgBox("No Customer ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            customerGetCustomerRecord = mbJMPOS_GetRecord(sSql, colRecord)
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-
    End Function '--get cust...-
    '= = = = = = = = = = = = = =
    '= = = = = = = = = = =  =
    '-===FF->

    '--  Get Customer Record--
    '--  INPUT is Browser Grid row collection..--
    '---   Output: SELECTED Customer Record columns AS RM columns....

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
                If colBrowseRow.Contains("customer_id") Then
                    L1 = CLng(colBrowseRow.Item("customer_id")("value"))
                    lngId = L1
                Else
                    If colBrowseRow.Contains("barcode") Then
                        strBarcode = CStr(colBrowseRow.Item("barcode")("value"))
                    End If   '-barcode-
                End If  '-id-
            End If  '--nothing
            '--  Use RM-alias SELECT List..
            If (strBarcode <> "") Then '--was supplied..-
                sSql = "SELECT " & msCustomerSelectList & " FROM [customer] WHERE barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = "SELECT " & msCustomerSelectList & " FROM [customer] WHERE customer_id=" & Str(lngId) & "; "
            Else
                MsgBox("No Customer barcode or ID was supplied for " & vbCrLf & _
                         " RM function customerGetCustomerRecordEx..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            customerGetCustomerRecordEx = mbJMPOS_GetRecord(sSql, colRecord)
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
        Dim dt1 As DataTable  '=  ADODB.Recordset

        customerGetSalesHistory = False
        If mbConnected Then

            '--  RETAIL MANAGER..-
            '--  RESULTS columns are:
            '-   - docket.docket_id , docket_date, total_inc,
            '--     Cat1, Cat2, Description, barcode,  docket.transaction,
            '--         DocketLine.stock_id, DocketLine.quantity,
            '--              sell_inc, docket.customer_Id

            '--  NOTE:  MYOB RM dockets have Customer_Id ONLY. (No Customer barcode.)--
            If mbRefreshPurchasesList(lngId, dt1) Then
                '- Convert recordset to standard collections..--
                customerGetSalesHistory = mbMakeRecordCollection(dt1, colRecordList)

            End If
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)

        End If '--connected..-
    End Function '--customerGetSalesHistory..-
    '= = = = = = = = = = = = = = = = =  = = =
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
            frmBrowse1.connection = mCnnSql '--Retail Manager Jet connection..-
            frmBrowse1.colTables = mColSqlDBInfo
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
            frmBrowse1.ShowPreferredColumnsOnly = True
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
                    sSql = msStockSelectSql & " WHERE stock_id=" & CStr(lngId) & "; "
                    If Not mbJMPOS_GetRecord(sSql, colSelectedRecord) Then
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
                sSql = msJobStockSelectSql & " WHERE stock.barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = msJobStockSelectSql & " WHERE stock.stock_id=" & Str(lngId) & "; "
            Else
                MsgBox("No stock ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            stockGetJobPartStockInfo = mbJMPOS_GetRecord(sSql, colRecord)
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
            sSql = msStockSelectSql
            If (strBarcode <> "") Then '--was supplied..-
                sSql &= " WHERE stock.barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql &= " WHERE stock.stock_id=" & Str(lngId) & "; "
            Else
                MsgBox("No stock ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            stockGetStockRecord = mbJMPOS_GetRecord(sSql, colRecord)
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
            sSql = msStockSelectSql  '-- SELECT list ==
            If (strBarcode <> "") Then '--was supplied..-
                sSql &= " WHERE stock.barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql &= " WHERE stock.stock_id=" & Str(lngId) & "; "
            Else
                MsgBox("No stock ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            stockGetStockRecordEx = mbJMPOS_GetRecord(sSql, colRecord)
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
            sSql = msStockSelectSql & sWhere & " ORDER by Cat1,Cat2; "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
                MsgBox("Failed to get Stocklist recordset.." & vbCrLf & _
                               gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
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
            If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
                MsgBox("Failed to get stockCategoryList recordset.." & vbCrLf & _
                             gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
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
        Dim dtGoods As DataTable '= ADODB.Recordset

        invoiceGetStockItemInvoices = False
        If mbConnected Then
            sSql = " SELECT  GR.Invoice_No, GR.Goods_Date,  "
            sSql &= "   Supplier.SupplierName, SupplierCode.SupCode, GR.Supplier_id,  "
            sSql &= "  GRL.goods_id, GRL.quantity AS Qty   "
            sSql &= " FROM (GoodsReceivedLine AS GRL "
            sSql &= "      LEFT JOIN  (GoodsReceived AS GR "
            sSql &= "         LEFT JOIN  (Supplier "
            sSql &= "           LEFT JOIN SupplierCode "
            sSql &= "           ON ((SupplierCode.Supplier_id= Supplier.Supplier_id) "
            sSql &= "                          AND (SupplierCode.stock_id=" & lngStockId & ")) )"
            sSql &= "      ON (Supplier.Supplier_id=GR.supplier_id)  )"
            sSql &= "    ON (GR.Goods_id=GRL.goods_id) )  "
            sSql &= " WHERE (GRL.stock_id=" & lngStockId & ") "
            sSql &= "   ORDER BY GR.Invoice_Date; "
            '-- GET recordset of invoices for that stock item..==
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnSql, dtGoods, sSql) Then
                MsgBox("Failed to get stockItemInvoices recordset.." & vbCrLf & _
                             gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            End If
            '-ok..  make rset into our collection structure.-
            invoiceGetStockItemInvoices = mbMakeRecordCollection(dtGoods, colRecordList)
            '== rsGoods.Close()
        Else
            MsgBox("No DB connected..", MsgBoxStyle.Exclamation)
        End If '--connected..-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '= rsGoods = Nothing
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
            frmBrowse1.connection = mCnnSql '--Retail Manager Jet connenction..-
            frmBrowse1.colTables = mColSqlDBInfo
            frmBrowse1.DBname = ""
            frmBrowse1.tableName = "supplier"
            frmBrowse1.IsSqlServer = False '--bIsSqlServer
            '--  pass preferred cols..-
            frmBrowse1.PreferredColumns = mColPrefsSupplier '-- colPrefs
            frmBrowse1.ShowDialog()
            '-get result=
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
                    sSql = msSupplierSelectSql & " WHERE supplier_id=" & CStr(lngId) & "; "
                    If Not mbJMPOS_GetRecord(sSql, colRecord) Then
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
                sSql = msSupplierSelectSql & " WHERE barcode='" & strBarcode & "'; "
            ElseIf (lngId >= 0) Then  '--was supplied..-
                sSql = msSupplierSelectSql & " WHERE stock_id=" & Str(lngId) & "; "
            Else
                MsgBox("No stock ID supplied for Get Record..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            supplierGetSupplierRecord = mbJMPOS_GetRecord(sSql, colRecord)
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
    '= = = = = = = = = = = = =  = = = = =


    '-- SerialGetSerialInfo    --

    '-- (Input: strSeriallNo;
    '--   Output: Stock_Id, barcode, cat1, cat2, description, cost, sell,
    '--             Goods_Id, Goodsline_id, Qty, Supplier_Id, SupplierCode, InStock (YES/NO) --)
    '== PLUS "intInputStock_id " as extra input..
    '==

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

    '--  QuoteGetAllQuotes  JMPOS 02-Jan-2015 --

    '----  Returns full list of quote records (SalesOrders "quote"--  JOIN customer..--)

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
            sSql &= "    customer.lastName AS surname, customer.firstName As given_names, "
            sSql &= "    customer.companyName AS company, customer.barcode AS CustBarcode,  "
            sSql &= "    SalesOrder.Total_inc, SalesOrder.customer_id as CustId, "
            sSql &= "    customer.phone AS CustPhone, customer.mobile AS CustMobile, 'ok' AS OrderStatus, "
            sSql &= "    SalesOrder.transactionType AS Trans "
            sSql &= " FROM SalesOrder "
            sSql &= " LEFT JOIN Customer ON (SalesOrder.customer_id=Customer.Customer_id) "
            sSql &= " WHERE (SalesOrder.transactionType = 'quote') "
            If mlCustomerSearchId >= 0 Then
                sSql = sSql & " AND (SalesOrder.customer_id = " & CStr(mlCustomerSearchId) & ") "
            End If
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnSql, rstQuote, sSql) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("Failed to get SalesOrder recordset.." & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
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
            sSql = "SELECT  stock.cat1,stock.cat2, stock.Description, stock.barcode,"
            sSql &= " SalesOrderLine.quantity AS OrderQty, SalesOrderLine.sell_inc, SalesOrderLine.stock_id "
            sSql &= "  FROM [SalesOrderLine] "
            sSql &= " LEFT JOIN stock ON (SalesOrderLine.stock_id=stock.stock_id)  "
            sSql &= " WHERE (SalesOrderLine.SalesOrder_id =" & CStr(order_id) & ") "
            sSql &= " ORDER BY stock.Description ASC "
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnSql, rsItems, sSql) Then
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("Failed to get SalesOrder Items recordset.." & vbCrLf & _
                                          gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
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

            '=JMPOS= mCnnJet.Close()
            '=JMPOS= mbConnected = False
        End If

    End Function '--close.--
    '= = = = = = = = = = = =

    '== end class ===
End Class  '-clsRetailHostJMPOS-


'== then end ==