Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports system.data.sqlclient
Imports system.data.OleDb
Imports System.Threading

Public Class ucChildStockAdmin

    '-- JobMatix POS 3.0 --
    '--  Stock and GoodsReceived Functions..--

    '-- NB:  textBox and CheckBox controls
    '--      have the DB Column name set in the "Tag" property..

    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==   grh- JobMatix POS3 3.1.3101.1019-
    '==      >>  Add tabs for showing Serials, GoodsReceived and Sales..
    '==
    '==   grh- JobMatix POS3 3.1.3103.0205-
    '==      >>   Fix Browse Refresh crash (srch should Activate, not refresh..
    '==
    '==   grh- JobMatix POS3 3.1.3103.0217-
    '==      >>   Re-size..  adjust all controls..
    '==
    '==   grh- JobMatix POS3 3.1.3107.0716-
    '==      >> Sell margin is applied to CostExTax..
    '==
    '==  grh. JobMatix 3.1.3107.0805 ---  05-Aug-2015 ===
    '==   >> Now for .Net 4.5.2- 
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '==  '=3301.606= 
    '==     v3.3.3301.606..  06-June-2016= ===
    '==       >>  Drop radio buttons for isService, isLabour etc.
    '==            Add "chkIsNonStockItem"..
    '==
    '==     v3.3.3303.0121..  21-Jan-2017= ===
    '==       >> frmStock- Tidying up..
    '== 
    '==     v3.4.3403.0512 = 12May2017=
    '==      -- Added code to select Label printer (frmGetprinter).
    '==
    '= =     v3.4.3411.0417- 17-April-2018=
    '==             -- catch Enter on srch text-
    '==
    '==       V3.5. 3501.0826- Called From Goods Rcvd.  
    '==         -- Add new Stock (have barcode).
    '==         -- 3501.0913-  Add new Stock (Fix Supplier name.).
    '==  
    '== -- Updated 3501.0922  22-Sept-2018=  
    '==     -- 3501.0922-  ADD AUTO-barcode option for NEW STOCK Item....
    '==           -- New idea.. get list of existing numeric barcodes.
    '==                         --  And pick the lowest missing one..
    '== 
    '== -- Updated 3501.1030  30-Oct-2018=  
    '==     -- RE-arrange Category and Brand, Supplier DropDowns for more clarity...
    '==
    '==   Updated.- 3519.0211 11-Feb-2019= 
    '==     -- Fix Stock Admin Alpha barcode INSERT error.-  
    '--             barcode is alpha, and Must have quotes..
    '==
    '==   Updated.- 3519.0214 12/14-Feb-2019= 
    '==     -- Fixes to for Grid browsing problems..-
    '==         (Force a delay after selection changed before showing details.)..-
    '==     -- Fixes  for making last-added item selectable...-
    '==     -- Fixes to Stock Item Detail panel to make Brand,Supplier AutoSuggestive.. 
    '==     --  Fixes to round off Sell_inc..
    '==     -- MORE Fixes to for Grid browsing problems..-  
    '--           ie. Use "RowEnter" instead of SelectionChanged (as per Customer Admin)
    '==
    '==   Updated.- 3519.0227 27-Feb-2019= 
    '==     -- MORE Fixes to for Grid browsing problems..-
    '==              3519.0227= MUST update WHERE each time so CLEAR works to re-fill Grid.
    '==     --  Fix validations for Stock re-order level..
    '==
    '==   Updated.- 3519.0311  Started 05-March-2019= 
    '==      >> Stock Admin- Add references buttons for Cat1, Cat2, Brands.. 
    '==
    '=== = = = = =  = = = = = = = = = = = = = = = =  = = = = = = = = = = = = = = = = = = = =
    '==
    '== - - - - RELEASED as 3519.0414 --
    '==  
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==
    '==    First New Build- 4201.0416 -
    '==   Updated.- 4201.0416- 
    '==     --for TDI Admin forms in Tabs inside Main Form...
    '==
    '==    New Build- 4201.0416 -  Started 16-April-2019.
    '==    
    '==    -- 4201.0424. TDI Child forms now converted to UserControls....
    '==
    '== NEW revision-
    '==    -- 4201.0727.  Started 25-July-2019-
    '==      -- For the Stock Admin screen, on Serials Tab (grid) 
    '==               add right-click context menu to copy  Serial Number to the clipboard.  
    '==               AND on the Purchases Tab (grid) add right-click context menu to copy  Supplier Invoice Number to the clipboard. 
    '==
    '==
    '== NEW revision to fix previous.  and ad a feature-
    '==
    '==    -- 4201.1013.  13-Oct-2019..
    '==        -- Stock Admin.  On POS Item Sales Tab,  
    '==                      catch DoubleCiick event on invoice line to bring up  (show) the full invoice.
    '--    -- Stock DGV..  Try and catch DownArrow.. In case we are loading an item.
    '==
    '== NEW revision-
    '==
    '==   -- 4201.1027/1028.  27-Oct-2019-  Started 27-Oct-2019-
    '==      -- ucChildStockAdmin.- 
    '==             Add DblClick to Item Purchases Grid to show GoodsReceived for that Item. .
    '==
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1106.  06-Nov-2019-  Started 06-November-2019-
    '==        - 4201.1111-- Showing Stock Info- Clear old stock Picture first (if any)..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '==
    '==  Target is new Build 4234..
    '==  Target is new Build 4234..  05-May-2020==
    '==
    '==   -- GET RID of all DoEvents calls in Stock Admin.. 
    '==          IT is BAD Allows re-entry to unfinished DGV refresh routines  !!!!
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..  Started in here 19-May-2020
    '==
    '==   MAIN THEME Here is implementation of a SupplierCode Field in the StockAdmin details Tab..
    '==       --  Involves also adding code to the New/Update Stock Commit to also add/Update Supplier Code Table if needed.
    '==    ALSO Involves FINALLY revamping frmStock,
    '==                   so as to make into purely a SHELL (container) for the ucChildStockAdmin UserControl.
    '==   ALSO in this release- Load the cat1/Cat2 Combos SORTED in Alpha Order.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '==
    '==  Target is new Build 4253..
    '==  Target is new Build 4253..
    '==
    '==  Target-New-Build-4253..
    '==  Target-New-Build-4253..
    '==
    '==   6.  StockAdmin-  frmStock Host form not closing when new stock item added via call from GoodsReceived..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020) 
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==
    '== --  Purchase Orders- Auto  ordering..  
    '==       (Martin 22/9/2020.)  Qty to order to only be enough to make stock back up to Max level.. ?  
    '==       ie Max (DB order_quantity)..  Same as what MYOB does.
    '==  JUST change the form Label for "order_quantity" to "Max to hold"..
    '==  JUST change the form Label for "order_quantity" to "Max to hold"..
    '==  JUST change the form Label for "order_quantity" to "Max to hold"..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =


    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    '--  data grid column nos.--
    Private Const DGV_SERIALS_NUMBER As Short = 0
    Private Const DGV_SERIALS_STATUS As Short = 1
    Private Const DGV_SERIALS_HISTORY As Short = 2

    '-- goods recvd.-
    Private Const DGV_GOODS_ID As Short = 0
    Private Const DGV_GOODS_DATE As Short = 1
    Private Const DGV_GOODS_SUPPLIER As Short = 2
    Private Const DGV_GOODS_INVOICENO As Short = 3
    Private Const DGV_GOODS_QTY As Short = 4

    '= = = = = = = = = = = = = = = = = = = = = = = = =

    Private mbActivated As Boolean = False

    '--inputs--
    Private msVersionPOS As String = ""

    Private msServer As String
    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '-- DB schema info the Stck Table Columns..-
    Private mDataTableColumns As DataTable
    Private mColColumnDataTypes As Collection  '-- Col. name is key-- data = sqlTYpe-

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection '--
    Private mlJobId As Integer = -1

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1
    '==3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo

    Private msDefaultPrinterName As String = ""

    Private mDecGSTPercentage As Decimal = 0
    Private mDecSellMargin As Decimal = 0

    Private msBusinessName As String = ""
    Private msBusinessShortName As String = ""
    Private msBarcodeFontName As String = "IDAutomationHC39M" '-default-
    Private mIntBarcodeFontSize As Integer = 8  '-default-
    '== Private msColourPrtName As String = ""
    '== Private msReceiptPrtName As String = ""
    '== Private msLabelPrtName As String = ""

    Private mIntForm_top As Integer = -1
    Private mIntForm_left As Integer = -1

    '-- stock list sorting..-
    Private mlSortOrderStock As System.Windows.Forms.SortOrder  '== Integer
    Private mlSortKeyStock As Integer

    Private msRequiredFields As String = ""

    '--  Current Item--
    Private mbIsNewItem As Boolean = False

    Private mbItemIsLoading As Boolean = False
    Private mIntStock_id As Integer = -1
    Private mByteImage1 As Byte()

    Private msModifiedControls As String = ""  '--names of controls modified.-

    '- Stock list now in dataGridView -
    Private mColPrefsStock As Collection
    Private mBrowse1 As clsBrowse3
    Private mLngSelectedRow As Integer = -1

    Private mIntFormDesignWidth As Integer
    Private mIntFormDesignHeight As Integer

    '=3501.0826-  From Goods Rcvd.  Add new Stock (have barcode).
    Private mbAddNewStockOnly As Boolean = False
    Private mbAddNewStockAutoGenBarcode As Boolean = False

    Private msAddNewStockBarcode As String = ""
    Private mIntAddNewStockSupplier_id As Integer = -1
    Private msAddNewStockSupplierName As String = ""

    Private mbIsCancelled As Boolean = False

    Private mColPrefsCategory1 As Collection
    Private mColPrefsCategory2 As Collection
    Private mColPrefsBrands As Collection
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == 


    '==  Target is new Build 4251..  Started in here 19-May-2020
    '==  Target is new Build 4251..  Started in here 19-May-2020
    ''==  Special for SupplierCode Field..
    '--         (Actually lives in SupplierCode Table.)..
    Private mbSupplierCodeModified As Boolean = False

    '= = = = = = =  = = = = = = =  ==  = = = = = = = = = = 

    '--  Popup menu for Right click on Serials Grid...-
    '--  Popup menu for Right click on Serials Grid...-
    Private mContextMenuSerialInfo As ContextMenu

    Private WithEvents mnuCopyItemSerialNo As New MenuItem("Copy Item SerialNo.")
    'Private WithEvents mnuItemMenuSep1 As New MenuItem("-")
    'Private WithEvents mnuCopyItemBarcode As New MenuItem("Copy Item Barcode.")
    'Private WithEvents mnuItemMenuSep2 As New MenuItem("-")


    '-- mContextMenuPurchasesInfo-
    Private mContextMenuPurchasesInfo As ContextMenu

    Private WithEvents mnuCopyItemInvoiceNo As New MenuItem("Copy Item Invoice-No.")


    '= = = = = = = = = = = = = = = = = = = = = = = =


    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- P r o p e r t i e s --
    '-- P r o p e r t i e s --

    WriteOnly Property VersionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
        End Set
    End Property
    '= = = = = = = = = = = = = = 


    '-- Staff Id now comes from caller..--

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property StaffId() As Integer
        Set(ByVal Value As Integer)

            mIntStaff_id = Value
        End Set
    End Property '--id.-
    '= = = = = = = = = = = =  =

    '-- Sql Connection Info from Sub Main..-
    '-- Sql Connection Info from Sub Main..-

    WriteOnly Property SqlServer() As String
        Set(ByVal Value As String)
            msServer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    '== WriteOnly Property SqlServerComputer() As String
    '=     Set(ByVal Value As String)
    '==         msSqlServerComputer = Value
    '==     End Set
    '== End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property connectionSql() As OleDbConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =
    '= = = = = = =  = = = = =

    WriteOnly Property dbInfoSql() As Collection
        Set(ByVal Value As Collection)
            mColSqlDBInfo = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msSqlDbName = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = 

    WriteOnly Property form_top() As Integer
        Set(ByVal value As Integer)
            mIntForm_top = value
        End Set
    End Property  '--top-
    '= = = = = = = = = = = = = 

    WriteOnly Property form_left() As Integer
        Set(ByVal value As Integer)
            mIntForm_left = value
        End Set
    End Property  '--left-
    '= = = = = = = = = = = = = 

    '-AddNewStockOnly-

    WriteOnly Property AddNewStockOnly As Boolean
        Set(value As Boolean)
            mbAddNewStockOnly = value
        End Set
    End Property  '-AddNewCustomerOnly-
    '= = = = = = =  = = = = = = = = = = = =

    '-- F5 from GoodsReceived (New stock with AutoGen barcode)
    WriteOnly Property AddNewStockAutoGenBarcode As Boolean
        Set(value As Boolean)
            mbAddNewStockAutoGenBarcode = value
        End Set
    End Property  '-AddNewCustomerOnly-
    '= = = = = = =  = = = = = = = = = = = =

    '-- Barcode supplied from Goods Received.-
    WriteOnly Property AddNewStockBarcode As String
        Set(value As String)
            msAddNewStockBarcode = value
            txtBarcode.Text = msAddNewStockBarcode
        End Set
    End Property  '-barcode input-
    '= = = = = = = = = =  = == = = = =

    WriteOnly Property AddNewStockSupplier_id As Integer
        Set(value As Integer)
            mIntAddNewStockSupplier_id = value
            txtSupplier_id.Text = CStr(value)
        End Set
    End Property  '--supplier_id-
    '= = = = = = = = = = = = = ==

    WriteOnly Property AddNewStockSupplierName As String
        Set(value As String)
            txtSupplierName.Text = value
            msAddNewStockSupplierName = value
        End Set
    End Property  '-barcode input-
    '= = = = = = = = = =  = == = = = =
    '-===FF->

    '--results-

    '- result of selection..
    ReadOnly Property NewStock_id As Integer
        Get
            NewStock_id = mIntStock_id
        End Get
    End Property '-invoice-
    '= = = = = = = = = = = = = = = = = = = = = = = =

    ReadOnly Property wasCancelled As Boolean
        Get
            wasCancelled = mbIsCancelled
        End Get
    End Property  '-cancelled--
    '= = = = = = = = = = = = = = = = = = = = = = == = = 
    '-===FF->

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)
    '= = = = = = = = = = = = = = = = = = = = == = 


    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)
        Me.Width = intParentWidth - 11
        Me.Height = intParentHeight  '= - 11
        '-- resize our controls..
        '=4234.= DoEvents()
        '-- resize main box and top panel-
        '== panelBanner.Width = Me.Width - 11
        grpBoxStock.Width = Me.Width - 9 '= panelBanner.Width
        grpBoxStock.Height = Me.Height - 22  '= 93
        '=4234.= DoEvents()  '--time to adjust contents.

        '-resize browser box=
        frameBrowse.Width = ((grpBoxStock.Width \ 9) * 4)  '--four ninths of total-
        If (frameBrowse.Width < 440) Then frameBrowse.Width = 440
        frameBrowse.Height = grpBoxStock.Height - panelBanner.Height - 15
        panelBanner.Width = frameBrowse.Width

        '=4234.= DoEvents()  '--time to adjust contents.
        '-- RHS- Item info-
        tabControlStock.Left = frameBrowse.Left + frameBrowse.Width + 3
        tabControlStock.Width = grpBoxStock.Width - tabControlStock.Left - 11
        tabControlStock.Height = grpBoxStock.Height - tabControlStock.Top - 11
        '=4234.= DoEvents()  '--time to adjust contents.

        dgvStockList.Width = frameBrowse.Width - 10
        dgvStockList.Height = frameBrowse.Height - 105

        panelStockItem.Width = TabPageItemDetail.Width - 3
        '=4234.= DoEvents()  '--time to adjust contents.
        panelStockItem.Height = TabPageItemDetail.Height - panelStockItem.Top - 3
        '=4234.= DoEvents()  '--time to adjust contents.
        picProduct.Left = panelStockItem.Width - picProduct.Width - 10

        dgvSerials.Width = TabPageSerials.Width - 5
        dgvSerials.Height = TabPageSerials.Height - dgvSerials.Top - 7

        dgvGoods.Width = TabPagePurchases.Width - 7
        dgvGoods.Height = dgvSerials.Height

        dgvSales.Width = TabPageSales.Width - 7
        dgvSales.Height = TabPageSales.Height - dgvSales.Top - 7  '= dgvSerials.Height

        panelItemHdr.Left = tabControlStock.Left
        panelItemHdr.Width = tabControlStock.Width

        '=4234.= DoEvents()

        Me.Invalidate()
        Me.Update()

    End Sub  '-SubFormResized-
    '= = = = = = = = = = = =  === =

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click....

    Public Function SubFormCloseRequest() As Boolean

        '=- Return true if ok to Close.
        If (msModifiedControls <> "") Then
            If (MsgBox("Abandon changes ?", _
                 MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
                SubFormCloseRequest = False  '=bCancel = True   '--cant close yet--'--was mistake..  keep going..
            Else
                SubFormCloseRequest = True  '-ok to close.
            End If
        Else  '-- no changes-
            SubFormCloseRequest = True  '-ok to close.
        End If
        '= Me.Close()
        '- Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    ''' <summary>
    ''' Converts the Image File to array of Bytes
    ''' Thanks to:
    '''     http://www.codeproject.com/Articles/31921/Convert-Image-File-to-Bytes-and-Back  
    ''' </summary>
    ''' <param name="ImageFilePath">The path of the image file</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function mabConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(ImageFilePath) = True Then
            Throw New ArgumentNullException("Image File Name Cannot be Null or Empty", "ImageFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
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
    '-- Numeric test..

    Private Function mbIsNumeric(ByVal sInput As String) As Boolean
        mbIsNumeric = False

        If IsNumeric(sInput) Then  '--good start-
            '-  check for "+","-" that pass the isNumeric test, but fail in Sql Server. test.
            If (InStr(sInput, "+") <= 0) AndAlso (InStr(sInput, "-") <= 0) Then
                mbIsNumeric = True
            End If
        End If  '-numeric-
    End Function  '-is numeric-
    '= = = = = = = = = = =  = = = = =

    Private Function mDecComputeAmountExTax(ByVal decGrossAmount As Decimal) As Decimal

        mDecComputeAmountExTax = Decimal.Truncate((decGrossAmount * (100 / (100 + mDecGSTPercentage))) * 100) / 100
    End Function '-- mDecComputeAmountExTax-
    '= = = = = = = = = = = =  = = = == = ==

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =

    '=3519.0214-

    Private Function mDecGetRoundingAmount(ByVal decAmountToBeRounded As Decimal) As Decimal
        '-  Compute Rounding..--
        Dim decRounding As Decimal = 0
        Dim intCentsRounding As Integer = 0
        Dim intCents1 As Integer
        intCents1 = (decAmountToBeRounded * 100) Mod 10  '--get original cents.
        Select Case intCents1
            Case 1, 6 : intCentsRounding = -1
            Case 2, 7 : intCentsRounding = -2
            Case 3, 8 : intCentsRounding = 2
            Case 4, 9 : intCentsRounding = 1
        End Select
        decRounding = (intCentsRounding / 100)   '== make 0.0d  --
        mDecGetRoundingAmount = decRounding

    End Function  '-mIntGetRoundingCents-
    '= = = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- Get STRING Select Value -- (cmd.getScalar)--

    Private Function mbGetSqlScalarStringValue(ByRef cnnSql As OleDbConnection, _
                                           ByVal sSql As String, _
                                          ByRef strResult As String) As Boolean
        Dim sqlCmd1 As OleDbCommand
        '==Dim intResult As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbGetSqlScalarStringValue = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            strResult = Convert.ToString(sqlCmd1.ExecuteScalar)
            mbGetSqlScalarStringValue = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarStringValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    Private Function mbGetSqlScalarIntValue(ByRef cnnSql As OleDbConnection, _
                                             ByVal sSql As String, _
                                            ByRef intResult As Integer) As Boolean
        Dim sqlCmd1 As OleDbCommand
        '==Dim intResult As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbGetSqlScalarIntValue = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            intResult = sqlCmd1.ExecuteScalar
            mbGetSqlScalarIntValue = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarIntValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--show/print invoice- 
    '=---   ADDED to here 4201.1013=  13Oct2019=
    '--   
    '= 3311.226=  Add Selected printer names-

    Private Function mbShowInvoice(ByVal intInvoice_Id As Integer, _
                                   ByVal sTranType As String, _
                                   Optional ByVal bCaptureInvoicePDF As Boolean = False, _
                                     Optional ByVal bPrintInvoiceAnyway As Boolean = False, _
                                     Optional ByVal bReallyWantsA4Invoice As Boolean = False, _
                                       Optional ByVal sSelectedInvoicePrinterName As String = "",
                                          Optional ByVal sSelectedReceiptPrinterName As String = "") As Boolean
        Dim frmShowInvoice1 As frmShowInvoice
        Dim bIsQuote As Boolean = (LCase(sTranType) = "quote")
        Dim bIsLayby As Boolean = (LCase(sTranType) = "layby")

        frmShowInvoice1 = New frmShowInvoice
        frmShowInvoice1.connectionSql = mCnnSql
        frmShowInvoice1.InvoiceNo = intInvoice_Id
        frmShowInvoice1.isQuote = bIsQuote
        frmShowInvoice1.islayby = bIsLayby
        '-- can use main signon- mIntMainStaff_id -
        'If (mIntSaleStaff_id <= 0) Then
        frmShowInvoice1.Staff_id = mIntStaff_id  '- no current sale login.-
        'Else  '-ok- current sale.
        '    frmShowInvoice1.Staff_id = mIntSaleStaff_id
        'End If
        '=If bCaptureInvoicePDF Then
        frmShowInvoice1.CaptureInvoicePDF = bCaptureInvoicePDF  '--capture pdf for email..
        frmShowInvoice1.PrintInvoiceAnyway = bPrintInvoiceAnyway  '--if checked..
        frmShowInvoice1.A4InvoiceRequested = bReallyWantsA4Invoice
        '= End If
        '= NOT AVAILBALE-  frmShowInvoice1.UserLogo = mImageUserLogo
        '== ADDED 3401.319=
        frmShowInvoice1.selectedInvoicePrinterName = sSelectedInvoicePrinterName
        frmShowInvoice1.selectedReceiptPrinterName = sSelectedReceiptPrinterName

        frmShowInvoice1.ShowDialog()

    End Function  '--show invoice..-
    '= = = = = = = = = = = = = = = =
    '-===FF->


    '--  GET READY for Function KEY..
    '--- INITIALISE Stock Browser.for STOCK Lookup--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(Optional ByVal sSrchWhereCond As String = "") As Boolean

        '=Dim colPrefs As Collection
        Dim sHostTablename As String
        Dim sWhere As String

        mBrowse1 = New clsBrowse3 '== clsBrowse22
        '-- show full frame..-
        '== FrameBrowse.Height = (LabDescription.Top - FrameBrowse.Top - 4)
        '== DataGridView1.Height = (FrameBrowse.Height - DataGridView1.Top - 8)

        mBrowse1.connection = mCnnSql  '= mRetailHost1.connection
        mBrowse1.colTables = mColSqlDBInfo '= mRetailHost1.colTables 
        mBrowse1.IsSqlServer = True   '= mRetailHost1.IsSqlServer
        mBrowse1.DBname = msSqlDbName  '= mRetailHost1.DBname

        '--  get table/prefs info for this host..--

        '= If Not mRetailHost1.browseGetPrefColumns("stock", sHostTablename, colPrefs) Then
        '==  MsgBox("Can't translate table name to host table..", MsgBoxStyle.Exclamation)
        '== End If
        mBrowse1.tableName = "stock"  '==sHostTablename

        '= mBrowse1.FlexGrid = MSHFlexGrid1
        mBrowse1.DataGrid = dgvStockList

        '--  pass controls..--
        mBrowse1.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowse1.showFind = LabFind '--updates Sort Column display..
        mBrowse1.showTextFind = txtFind '--updates Sort Column display..
        '= sWhere = msMakeStockFilter()  '--service or not..-
        '-- add srch args..
        If (sSrchWhereCond <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= sSrchWhereCond
        End If
        mBrowse1.WhereCondition = sWhere
        mBrowse1.PreferredColumns = mColPrefsStock '== mColPrefs
        mBrowse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
        frameBrowse.Enabled = True

        mLngSelectedRow = -1
        mBrowse1.Activate() '-- go..--

        '== txtFind.Focus()

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  F2 was pressed..  Browse for stock code..--
    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--

    Private Function mbBrowseStockTable(Optional ByRef sSrchWhereCond As String = "") As Boolean
        Dim sWhere As String = ""

        If (mBrowse1 Is Nothing) Then
            Call mbInitialiseBrowse()
        Else
            sWhere = ""  '=LATER=   msMakeStockFilter()  '--service or not..-
            '-- add srch args..
            If (sSrchWhereCond <> "") Then
                If (sWhere <> "") Then
                    sWhere &= "AND "
                End If
                sWhere &= sSrchWhereCond
            End If
            '=3519.0213=
            'If (sWhere <> "") Then
            '== 3519.0227= MUST update WHERE each time so CLEAR works to re-fill Grid.
            mBrowse1.WhereCondition = sWhere '-- sWhere -
            'End If
            '= mBrowse1.refresh()
            '==3103-205==
            mBrowse1.Activate()  '==3103-205==
        End If
        txtFind.Focus()

        '= System.Windows.Forms.Application.DoEvents()
    End Function  ''--mbBrowseStockTable--
    '= = = = = =  = == =
    '-===FF->

    '-- refresh stock item combos..--

    Private Function mbRefreshFkeyCombos() As Boolean
        Dim sSql, s1, sMsg As String
        Dim dataTable1 As DataTable
        Dim row1 As DataRow

        '= mbItemIsLoading = True
        '==  Target is new Build 4251..  Started in here 19-May-2020
        '==   ALSO in this release- Load the cat1/Cat2 Combos SORTED in Alpha Order.
        '==
        '-- load Cat1-
        sSql = "SELECT cat1_key FROM category1 ORDER BY cat1_key; "
        If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
                                   (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            cboCat1.Items.Clear()
            For Each row1 In dataTable1.Rows
                cboCat1.Items.Add(row1.Item(0))
            Next
        Else '- no recs-
            sMsg = gsGetLastSqlErrorMessage()
            MsgBox("mbRefreshFkeyCombos:  No Cat1 record found.. !" & vbCrLf & sMsg, MsgBoxStyle.Exclamation)
        End If
        cboCat1.SelectedIndex = -1

        '-- load Cat2-
        sSql = "SELECT cat2_key FROM category2 ORDER BY cat2_key; "
        If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
                                   (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            cboCat2.Items.Clear()
            For Each row1 In dataTable1.Rows
                cboCat2.Items.Add(row1.Item(0))
            Next
        Else '- no recs-
            sMsg = gsGetLastSqlErrorMessage()
            MsgBox("mbRefreshFkeyCombos:  No Cat-2 record found.. !" & vbCrLf & sMsg, MsgBoxStyle.Exclamation)
        End If
        cboCat2.SelectedIndex = -1

        '-- load brands-
        sSql = "SELECT Brand_id, BrandName FROM stockBrands; "
        If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
                                   (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            cboBrand.Items.Clear()
            '=3519.0213-  ID now at RHS.
            For Each row1 In dataTable1.Rows
                '= cboBrand.Items.Add(RSet(row1.Item("Brand_id"), 4) & "; " & row1.Item("BrandName"))
                cboBrand.Items.Add(row1.Item("BrandName") & " ;" & row1.Item("Brand_id"))
            Next
        Else '- no recs-
            sMsg = gsGetLastSqlErrorMessage()
            MsgBox("mbRefreshFkeyCombos:  No StockBrand record found.. !" & vbCrLf & sMsg, MsgBoxStyle.Exclamation)
        End If
        cboBrand.SelectedIndex = -1

        '-- load SUPPLIERS-
        sSql = "SELECT Supplier_id, SupplierName FROM Supplier; "
        If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
                                   (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            cboSupplier.Items.Clear()
            '=3519.0213-  ID now at RHS.
            For Each row1 In dataTable1.Rows
                '== cboSupplier.Items.Add(RSet(row1.Item("Supplier_id"), 4) & "; " & row1.Item("SupplierName"))
                cboSupplier.Items.Add(row1.Item("SupplierName") & " ;" & row1.Item("Supplier_id"))
            Next
        Else '- no recs-
            sMsg = gsGetLastSqlErrorMessage()
            MsgBox("mbRefreshFkeyCombos:  No Supplier record found.. !" & vbCrLf & sMsg, MsgBoxStyle.Exclamation)
        End If
        cboSupplier.SelectedIndex = -1

        '=3101.1113= '-- load TAX codes-
        '=3101.1113= sSql = "SELECT code, description FROM TaxCodes; "
        '=3101.1113= If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
        '=3101.1113=                            (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
        '=3101.1113= cboGoodsTaxCodes.Items.Clear()
        '=3101.1113= cboSalesTaxCodes.Items.Clear()
        '=3101.1113= For Each row1 In dataTable1.Rows
        '=3101.1113=   cboGoodsTaxCodes.Items.Add(RSet(row1.Item("code"), 4) & "; " & row1.Item("description"))
        '=3101.1113=   cboSalesTaxCodes.Items.Add(RSet(row1.Item("code"), 4) & "; " & row1.Item("description"))
        '=3101.1113= Next
        '=3101.1113= Else '- no recs-
        '=3101.1113= sMsg = gsGetLastSqlErrorMessage()
        '=3101.1113= MsgBox("mbRefreshFkeyCombos:  No TaxCodes record found.. !" & vbCrLf & sMsg, MsgBoxStyle.Exclamation)
        '=3101.1113= End If
        '= mbItemIsLoading = False

    End Function  '--RefreshFkeyCombos--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- RefreshSerialsList --
    '-- RefreshSerialsList --

    Private Function mbRefreshSerialsList(ByVal intStock_Id As Integer) As Boolean
        Dim sTransaction, sTranList As String
        Dim sSql, sSerialNo, sTranType As String
        Dim intSerialCount, intSerial_id, intType_id As Integer
        Dim dataTable1 As DataTable
        Dim dtSerials, dtSerialTrail As DataTable
        Dim dgvRow1 As DataGridViewRow
        Dim intRowTemplateHt As Integer = dgvSerials.RowTemplate.Height

        '-- retrieve all serials currently on file for this stock item.. 
        dgvSerials.Rows.Clear()
        sSql = "SELECT * FROM serialAudit " & _
                "WHERE (stock_id= " & CStr(intStock_Id) & ") ORDER BY serialNumber ;"
        '-- get the recordset (datatable)..
        If Not gbGetDataTable(mCnnSql, dtSerials, sSql) Then
            MsgBox("Error in getting serialAudit table.." & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        Else
            If Not (dtSerials Is Nothing) AndAlso (dtSerials.Rows.Count > 0) Then
                '-- make a list of distinct serial nos. found...
                intSerialCount = 0
                '= txtSerialsOnFile.Text = ""
                For Each row1 As DataRow In dtSerials.Rows
                    dgvRow1 = New DataGridViewRow
                    dgvSerials.Rows.Add(dgvRow1)
                    With dgvSerials.Rows(intSerialCount)
                        intSerial_id = row1.Item("serial_id")
                        sSerialNo = row1.Item("serialNumber")
                        .Cells(DGV_SERIALS_NUMBER).Value = sSerialNo
                        .Cells(DGV_SERIALS_STATUS).Value = row1.Item("status")
                    End With
                    '-- get Trail transactions and Goods info..
                    '-- retrieve all trail trans. Serial item..
                    sTranList = ""
                    sSql = "SELECT * FROM serialAuditTrail " & _
                             " WHERE (serialAudit_id= " & CStr(intSerial_id) & ") ORDER BY trail_date;"
                    '-- get the recordset (datatable)..
                    If gbGetDataTable(mCnnSql, dtSerialTrail, sSql) Then
                        Dim intHeight, intTrailCount As Integer
                        For Each rowTrail As DataRow In dtSerialTrail.Rows
                            sTranType = rowTrail.Item("tran_type")
                            intType_id = CInt(rowTrail.Item("type_id"))
                            sTransaction = "-- " & Format(rowTrail.Item("trail_date"), "dd-MMM-yyyy")
                            sTransaction &= " " & LSet(sTranType, 8)
                            If ((VB.Left(LCase(sTranType), 8) = "goodsrec") Or _
                                                (LCase(sTranType) = "gr")) AndAlso (intType_id > 0) Then
                                sTransaction &= "- (" & intType_id & ") "
                                '- get goods info..  and supplier. to get Invoice Info..--
                                '=4234.= System.Windows.Forms.Application.DoEvents()
                                sSql = "SELECT Goods_Id, invoice_no, invoice_date, Supplier.supplierName AS Supplier  "
                                sSql &= " FROM GoodsReceived "
                                sSql &= "   LEFT JOIN Supplier ON (GoodsReceived.Supplier_Id=Supplier.Supplier_Id)  "
                                sSql &= "  WHERE (goods_id= " & CStr(intType_id) & ");"
                                If gbGetDataTable(mCnnSql, dataTable1, sSql) Then
                                    If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
                                        Dim rowGoods1 As DataRow = dataTable1.Rows(0)
                                        sTransaction &= Trim(VB.Left(rowGoods1.Item("supplier"), 16))
                                        sTransaction &= "- Invoice: " & rowGoods1.Item("invoice_no")
                                        sTransaction &= "  " & Format(rowGoods1.Item("invoice_date"), "dd-MMM-yyyy")
                                    End If  '-nothing-
                                Else  '-failed-
                                    MsgBox("Error in getting serialAudit Goods table.." & vbCrLf & _
                                                                    gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                                End If '-get-
                            ElseIf (VB.Left(LCase(sTranType), 8) = "sale") AndAlso (intType_id > 0) Then
                                sTransaction &= " InvoiceNo: " & intType_id
                            Else  '-show RM sales..-
                                sTransaction &= "-" & rowTrail.Item("RM_tr_detail")
                            End If  '--type-
                            '= 3103= 0221= Add RM Trail Info- (If was imported)-
                            If (sTranList <> "") Then sTranList &= vbCrLf
                            sTranList &= sTransaction
                            '--
                        Next rowTrail
                        '-- dump tranlist into grid row..
                        dgvSerials.Rows(intSerialCount).Cells(DGV_SERIALS_HISTORY).Value = sTranList
                        '-- Adjust row height to show multiple trail items.
                        intHeight = intRowTemplateHt   '= dgvSerials.Rows(intSerialCount).Height  '--current row ht.
                        intTrailCount = dtSerialTrail.Rows.Count
                        '-- make row deeper if more than one payment.
                        If (intTrailCount > 1) Then
                            dgvSerials.Rows(intSerialCount).Height = intHeight + (((intHeight \ 3) * 2) * intTrailCount)
                        End If '-count-
                    Else '-Failed=
                        MsgBox("Error in getting serialAuditTrail table.." & vbCrLf & _
                                                        gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                        Exit Function
                    End If
                    intSerialCount += 1
                Next row1  '--serial-
            ElseIf (dtSerials.Rows.Count <= 0) Then
                '--  no serials on record.
            End If '--nothing-
        End If  '--get table--

    End Function  '-mbRefreshSerialsList-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Refresh purchases (GOODS) list..
    '--  Refresh purchases (GOODS) list..

    Private Function mbRefreshGoodsList(ByVal intStock_Id As Integer) As Boolean
        Dim sSql, sSupplier, sInvoiceNo As String
        '==Dim intType_id As Integer
        Dim dataTable1 As DataTable
        Dim dgvRow1 As DataGridViewRow

        dgvGoods.Rows.Clear()
        '=4234.= System.Windows.Forms.Application.DoEvents()
        sSql = "SELECT GRL.stock_id, GRL.quantity, GRL.goods_id, GR.invoice_no, GR.invoice_date,   "
        sSql &= "    GR.goods_date, Supplier.supplierName AS Supplier  "
        sSql &= " FROM GoodsReceivedLine AS GRL "
        sSql &= "   LEFT JOIN GoodsReceived AS GR ON (GR.goods_id=GRL.goods_id)  "
        sSql &= "   LEFT JOIN Supplier ON (GR.Supplier_Id=Supplier.Supplier_Id)  "
        sSql &= "  WHERE (GRL.stock_id= " & CStr(intStock_Id) & ");"
        If gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
                For Each rowGoods1 As DataRow In dataTable1.Rows
                    sSupplier = VB.Left(rowGoods1.Item("supplier"), 16) & vbCrLf
                    sInvoiceNo = rowGoods1.Item("invoice_no")
                    '= sTransaction &= "  " & Format(rowGoods1.Item("invoice_date"), "dd-MMM-yyyy")
                    dgvRow1 = New DataGridViewRow
                    dgvGoods.Rows.Add(dgvRow1)
                    With dgvGoods.Rows(dgvGoods.Rows.Count - 1)   '= last row..   dgvRow1
                        .Cells(DGV_GOODS_ID).Value = rowGoods1.Item("goods_id")
                        .Cells(DGV_GOODS_DATE).Value = rowGoods1.Item("goods_date")
                        .Cells(DGV_GOODS_SUPPLIER).Value = sSupplier
                        .Cells(DGV_GOODS_INVOICENO).Value = sInvoiceNo
                        .Cells(DGV_GOODS_QTY).Value = rowGoods1.Item("quantity")
                    End With
                Next rowGoods1
            End If  '-nothing-
        Else  '-failed-
            MsgBox("Error in getting GoodsReceived table.." & vbCrLf & _
                                            gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        End If '-get-
    End Function  '--goods.-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Sales --

    Private Function mbRefreshSalesList(ByVal intStock_Id As Integer) As Boolean
        Dim sSql As String
        Dim dataTable1 As DataTable
        Dim dgvRow1 As DataGridViewRow

        dgvSales.Rows.Clear()
        '--List all Invoice LINES for this stock_id..-
        sSql = "SELECT  IL.stock_id, IL.invoice_id, IL.total_inc, IL.quantity,  "
        sSql &= "  invoice.transactionType AS Trancode, "
        sSql &= "  invoice.invoice_date, (firstName + ' ' + lastname) AS customer  "
        sSql &= "  FROM dbo.InvoiceLine AS IL "
        sSql &= "   LEFT OUTER JOIN invoice on (IL.invoice_id=invoice.invoice_id) "
        sSql &= "   LEFT OUTER JOIN customer on (invoice.customer_id=customer.customer_id) "
        sSql &= " WHERE (IL.stock_id= " & CStr(intStock_Id) & ") "
        sSql &= " ORDER BY IL.invoice_id;"

        '- get Query result and load datagrid..-
        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Error in SELECT for InvoiceLine table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If  '--get-

        '--get ok-
        If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            '-- DUMP the datatable into the Grid..
            For Each dataRow1 As DataRow In dataTable1.Rows
                dgvRow1 = New DataGridViewRow
                dgvSales.Rows.Add(dgvRow1)
                With dgvSales.Rows(dgvSales.Rows.Count - 1)   '= last row..  
                    .Cells("sale_invoice_no").Value = dataRow1.Item("invoice_id")
                    '==4201.1013=
                    .Cells("invoice_trancode").Value = dataRow1.Item("Trancode")     '==4201.1013=
                    .Cells("invoice_date").Value = Format(dataRow1.Item("invoice_date"), "dd-MMM-yyyy")
                    .Cells("customer").Value = dataRow1.Item("customer")
                    .Cells("sale_qty").Value = dataRow1.Item("quantity")
                    .Cells("sale_value").Value = FormatCurrency(dataRow1.Item("total_inc"), 2)
                End With
            Next dataRow1
        End If  '--nothing-
    End Function  '--sales-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- ClearAllTextFields-

    Private Function mbClearAllTextFields() As Boolean
        txtBarcode.Text = ""
        txtBarcode.ReadOnly = False   '--to be scanned in-
        chkAutoBarcode.Checked = False
        chkAutoBarcode.Enabled = True

        labQtyInStock.Text = ""
        txtReOrderLevel.Text = ""
        txtReOrderQty.Text = ""

        txtStock_id.Text = ""
        txtDescription.Text = ""
        txtCat1.Text = ""
        txtCat2.Text = ""
        txtInactive.Text = ""
        chkInactive.Checked = False

        '=3301.606= txtIsService.Text = "0"
        '=3301.606= txtIsLabour.Text = "0"
        '=3301.606= '== chkIsService.Checked = False
        '=3301.606= optTypeStock.Checked = True

        chkIsNonStockItem.Checked = False  '=3301.606=  Will import from "static_quantity"-
        txtIsNonStockItem.Text = ""

        txtTrackSerials.Text = ""
        chkTrackSerials.Checked = False

        txtRenaming.Text = ""
        chkRenaming.Checked = False

        txtBrand_id.Text = ""
        txtBrandName.Text = ""
        txtSupplier_id.Text = ""
        txtSupplierName.Text = ""

        '==  Target is new Build 4251..  Started in here 19-May-2020
        txtSupplierCode.Text = ""

        txtCostExTax.Text = ""
        chkGoodsTax.Checked = True
        txtGoodsTaxcode.Text = "GST"
        txtCostIncTax.Text = ""

        txtSellExTax.Text = ""
        chkSalesTax.Checked = True
        txtSalesTaxcode.Text = "GST"
        txtSellIncTax.Text = ""

        txtComments.Text = ""

        mByteImage1 = Nothing
        picProduct.Image = Nothing

    End Function '--mbClearAllTextFields-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-   SET data modified.-
    '--  Check if completion possible..-
    '-- "strControlName" ALREADY has term.semi-colon..

    Private Function mbSetDataModified(ByVal strControlName As String) As Boolean
        Dim sControlname, sColumnName As String
        Dim sFieldList As String = ""
        Dim txtdata As TextBox

        If Not (InStr(msModifiedControls, strControlName, CompareMethod.Text) > 0) Then
            '==  Target is new Build 4251..  Started in here 19-May-2020
            msModifiedControls &= strControlName  '--flag as being modified.
        End If
        If Not mbIsNewItem Then
            btnStockCommit.Enabled = True
        Else  '-new item
            '-- Iterate through all textbox controls in edit panel, and match against required list.
            For Each control1 As Control In panelStockItem.Controls
                sControlname = LCase(control1.Name)
                '-- check if in required list--
                sColumnName = LCase(control1.Tag)  '--retrieve DB column name-
                If (sColumnName <> "") Then
                    If (InStr(LCase(msRequiredFields), sColumnName & ";") > 0) Then  '--required-
                        txtdata = CType(control1, TextBox)
                        If txtdata.Text = "" Then  '--still missing-
                            sFieldList &= sColumnName & "; "
                        End If  '--missing-
                    End If
                End If
            Next control1
            If (sFieldList = "") Then
                labRequiredFields.Text = ""
                btnStockCommit.Enabled = True
            Else
                labRequiredFields.Text = "Still required: " & sFieldList
            End If
        End If  '--new-
        btnStockCancel.Enabled = True

    End Function  '-mbSetDataModified-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- show Selected stock item details in edit frame.--

    Private Function mbShowStockInfo(ByVal intStock_id As Integer) As Boolean
        '=Dim sBarcode, sSerialNo, sQty As String
        Dim sSql, s1, s2, sErrorMsg As String
        Dim dataTable1 As DataTable
        Dim row1 As DataRow
        Dim yBinaryData() As Byte
        Dim image1 As Image
        Dim decCost, decCostInc As Decimal
        Dim decSell, decSellInc As Decimal
        Dim iPos, intSuppl_id As Integer

        '--lookup stock-id-
        mbItemIsLoading = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '--  get recordset as collection for SELECT..--
        sSql = "SELECT * FROM [stock] WHERE (stock_id=" & CStr(intStock_id) & ");"
        If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
                               (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            '--have a row..-
            '- load column data to test boxes..
            '--   and Set control.tag to column-name-
            row1 = dataTable1.Rows(0)
            txtBarcode.Text = row1.Item("barcode")
            txtHdrBarcode.Text = txtBarcode.Text

            txtBarcode.ReadOnly = True  '--can't change in edit mode.-

            txtStock_id.Text = CStr(intStock_id)
            txtDescription.Text = row1.Item("description")
            labItemHeader.Text = txtDescription.Text

            labQtyInStock.Text = row1.Item("qtyInStock")
            txtReOrderLevel.Text = row1.Item("reorderlevel")
            txtReOrderQty.Text = row1.Item("order_quantity")

            txtCat1.Text = row1.Item("cat1")
            txtCat2.Text = row1.Item("cat2")

            '-- check boxes..-
            chkInactive.Checked = IIf(row1.Item("Inactive"), True, False)
            txtInactive.Text = IIf(row1.Item("Inactive"), "1", "0")  '--THIS for for sql UPDATE-

            '=3301.606= optTypeStock.Checked = False
            '=3301.606= optTypeService.Checked = False
            '=3301.606= optTypeLabour.Checked = False
            '=3301.606= txtIsService.Text = "0"  '--THIS for for sql UPDATE-
            '=3301.606= txtIsLabour.Text = "0"  '--THIS for for sql UPDATE-
            '=3301.606= If row1.Item("isServiceItem") Then
            '=3301.606= '= chkIsService.Checked = IIf(row1.Item("isServiceItem"), True, False)
            '=3301.606= optTypeService.Checked = True
            '=3301.606= txtIsService.Text = "1"  '--THIS for for sql UPDATE-
            '=3301.606= ElseIf row1.Item("isLabour") Then
            '=3301.606= optTypeLabour.Checked = True
            '=3301.606= txtIsLabour.Text = "1"  '--THIS for for sql UPDATE-
            '=3301.606= Else  '-stock
            '=3301.606= optTypeStock.Checked = True
            '=3301.606= End If
            '- From isNonStockItem -
            chkIsNonStockItem.Checked = False  '=3301.606=  Will import from "static_quantity"-
            txtIsNonStockItem.Text = "0"
            If (row1.Item("isNonStockItem") = True) Then
                chkIsNonStockItem.Checked = True
                txtIsNonStockItem.Text = "1"
                chkTrackSerials.Checked = False   '--can't be serialized.
                txtTrackSerials.Text = "0"
                chkTrackSerials.Enabled = False
            Else '--normal stock-
                chkTrackSerials.Checked = IIf(row1.Item("track_serial"), True, False)
                txtTrackSerials.Text = IIf(row1.Item("track_serial"), "1", "0")  '--THIS for for sql UPDATE-
            End If '-non-stock-

            chkRenaming.Checked = IIf(row1.Item("allow_renaming"), True, False)
            txtRenaming.Text = IIf(row1.Item("allow_renaming"), "1", "0")  '--THIS for for sql UPDATE-

            '--  Find Brand and Supplier--
            '--txtBrandName--
            '== txtBrand_id.Text = CStr(row1.Item("brand_id"))
            '== sSql = "SELECT brandname FROM stockBrands WHERE (Brand_id=" & txtBrand_id.Text & "); "
            '== If mbGetSqlScalarStringValue(mCnnSql, sSql, s1) Then
            '=   txtBrandName.Text = s1
            '== End If
            txtBrandName.Text = row1.Item("brandName")
            '-- txtSupplierName -
            txtSupplierName.Text = ""  '=3519.0214-
            intSuppl_id = row1.Item("supplier_id")
            '= txtSupplier_id.Text = Trim(CStr(row1.Item("supplier_id")))
            txtSupplier_id.Text = Trim(CStr(intSuppl_id))
            sSql = "SELECT supplierName FROM Supplier WHERE (supplier_id=" & txtSupplier_id.Text & "); "
            If mbGetSqlScalarStringValue(mCnnSql, sSql, s1) Then
                txtSupplierName.Text = s1
                '--set combo to correct item.
                If (cboSupplier.Items.Count > 0) Then
                    Dim intFoundId As Integer
                    Try
                        For idx2 As Integer = 0 To (cboSupplier.Items.Count - 1)
                            s1 = cboSupplier.Items(idx2)
                            iPos = InStrRev(s1, ";")
                            If (iPos > 1) Then
                                s2 = Trim(Mid(s1, iPos + 1))
                                If IsNumeric(s2) Then
                                    intFoundId = CInt(s2)
                                    If intFoundId = intSuppl_id Then
                                        cboSupplier.SelectedIndex = idx2
                                        Exit For
                                    End If
                                End If
                            End If '-ipos-
                        Next idx2
                        '= cboSupplier.SelectedItem = s1 & " ;" & txtSupplier_id.Text
                    Catch ex As Exception
                        MsgBox("Couldn't set Supplier Combo position.." & vbCrLf & _
                               " Processing will continue.", MsgBoxStyle.Exclamation)
                    End Try
                End If  '-count-
            End If

            '==  Target is new Build 4251..
            '==  Target is new Build 4251..  Started in here 19-May-2020
            '==
            '--  Retrieve Supplier Code if any..
            '--lookup Supcode-
            Dim dtSupCodes As DataTable
            txtSupplierCode.Text = ""
            sSql = "SELECT *  FROM dbo.SupplierCode AS SC "
            sSql &= "WHERE  (SC.stock_id=" & intStock_id & ") AND (SC.supplier_id=" & CStr(intSuppl_id) & ");"
            If gbGetDataTable(mCnnSql, dtSupCodes, sSql) Then
                '-ok-
                If (Not (dtSupCodes Is Nothing)) AndAlso (dtSupCodes.Rows.Count > 0) Then
                    '- got new record.
                    txtSupplierCode.Text = dtSupCodes.Rows(0).Item("supcode")  '-getsupcode.
                Else  '=If (dataTable1.Rows.Count <= 0) Then
                    '-- OK if no SupplierCode..
                    'MsgBox("ERROR- No SupplierCode data row returned for Stock_id: " & intStock_id & _
                    '            vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                End If  '-get=
            Else '--failed-
                sErrorMsg = gsGetLastSqlErrorMessage()
                MsgBox("ERROR- FAILED to Get SupplierCode data row for Stock_id: " & intStock_id & _
                            vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
            End If  '-get-
            mbSupplierCodeModified = False
            '== END stuff for Target new Build 4251..
            '== END stuff for Target new Build 4251..
            '== END stuff for Target new Build 4251..





            txtCostExTax.Text = FormatCurrency(row1.Item("costExTax"), 2)
            txtGoodsTaxcode.Text = UCase(row1.Item("Goods_taxcode"))
            chkGoodsTax.Checked = (txtGoodsTaxcode.Text = "GST")
            '-- compute cost inc..
            decCost = CDec(txtCostExTax.Text)
            decCostInc = decCost
            If chkGoodsTax.Checked Then
                decCostInc = decCost + (decCost * mDecGSTPercentage / 100)
            End If
            txtCostIncTax.Text = FormatCurrency(decCostInc, 2)

            txtSellExTax.Text = FormatCurrency(row1.Item("sellExTax"), 2)
            txtSalesTaxcode.Text = UCase(row1.Item("Sales_taxcode"))
            chkSalesTax.Checked = (txtSalesTaxcode.Text = "GST")
            '-- compute sell-inc..
            decSell = CDec(txtSellExTax.Text)
            decSellInc = decSell
            If chkSalesTax.Checked Then
                decSellInc = decSell + (decSell * mDecGSTPercentage / 100)
            End If
            '=3519.0214--- Rounding..
            '--  NOTE THAT THIS IS FOR DISPLAY ONLY.
            '--   MUST round to 2 decimals first.
            decSellInc = Decimal.Round(decSellInc, 2)
            decSellInc += mDecGetRoundingAmount(decSellInc)
            txtSellIncTax.Text = FormatCurrency(decSellInc, 2)

            '-- comments--
            txtComments.Text = row1.Item("comments")

            '--load pic if any..-
            '=4201.1111-- Clear old stock Picture first (if any)..
            picProduct.Image = Nothing

            picProduct.Tag = "productPicture"
            If Not IsDBNull(row1.Item("productPicture")) Then
                yBinaryData = row1.Item("productPicture") '==mRstEdit.Fields(sFldName).Value
                Try
                    '--- load picture from byte array..-
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(yBinaryData)
                    image1 = System.Drawing.Image.FromStream(ms)
                    picProduct.Image = image1
                    ms.Close()
                Catch ex As Exception
                    MsgBox("Failed to load product image from table.. " & vbCrLf & _
                                      "Error: " & ex.Message)
                End Try
                '--  save the byte array in static image collection..
                mByteImage1 = yBinaryData
                '== mColRowImages.Add(yBinaryData, sFldName)
            End If  '--null pic--

            btnEdit.Enabled = True
            msModifiedControls = ""

            '--SHOW Serials Info--
            labSerialsBarcode.Text = txtBarcode.Text
            labSerialsDescription.Text = txtDescription.Text
            Call mbRefreshSerialsList(intStock_id)

            '-- SHOW purchases (goods)--
            labGoodsBarcode.Text = txtBarcode.Text
            labGoodsDescription.Text = txtDescription.Text
            Call mbRefreshGoodsList(intStock_id)

            '--sales--
            labSalesBarcode.Text = txtBarcode.Text
            labSalesDescription.Text = txtDescription.Text
            Call mbRefreshSalesList(intStock_id)

            btnPrintLabels.Enabled = True
            txtlabelCount.Text = "1"

        Else '--not found..-
            btnPrintLabels.Enabled = False
            txtlabelCount.Text = ""
            '== ev.Cancel = True
            sErrorMsg = gsGetLastSqlErrorMessage()
            MsgBox("No Stock record found for stock_id: '" & intStock_id & "' !" & _
                                            vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
        End If  '-get--
        labItemAction.Text = "Loading Done.."
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        mbItemIsLoading = False
    End Function  '--ShowStockInfo-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '- setup for new stock item.

    Private Sub mSubSetupNewStock()

        mbIsNewItem = True
        mbItemIsLoading = True
        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnStockCancel.Enabled = True
        txtHdrBarcode.Text = ""
        labItemHeader.Text = ""

        '= listViewStock.Enabled = False
        frameBrowse.Enabled = False
        btnStockCommit.Enabled = False
        '--  clear all text fields..--
        Call mbClearAllTextFields()
        labItemAction.Text = "Creating new Item.."
        '- refresh all stock column combos..-
        Call mbRefreshFkeyCombos()
        panelStockItem.Enabled = True
        mbItemIsLoading = False

    End Sub  '-new setup-
    '= = = = = = = = = = = =
    '-===FF->

    '==dgv= '-- RefreshStockList--
    '==dgv= '--- Build and show listview list...
    '==dgv= Private Function mbRefreshStockList() As Boolean
    '==dgv= Dim dataTable1 As DataTable
    '==dgv= Dim row1 As DataRow
    '==dgv= Dim sSql, s1, sErrorMsg As String
    '==dgv= Dim item1 As ListViewItem
    '==dgv=     sSql = "SELECT * FROM [stock] ORDER BY cat1, cat2, description;"
    '==dgv=     If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
    '==dgv=                            (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
    '==dgv= '-- stuff all rows into combo box.-..
    '==dgv=         listViewStock.Items.Clear()
    '==dgv=         For Each row1 In dataTable1.Rows
    '==dgv=             s1 = CStr(row1.Item("stock_id"))
    '==dgv=             item1 = New ListViewItem(s1)
    '==dgv= '==item1.SubItems.Add(Format(row1.Item("invoice_date"), "dd-MMM-yyyy"))
    '==dgv=             item1.SubItems.Add(row1.Item("barcode"))
    '==dgv=             item1.SubItems.Add(row1.Item("cat1"))
    '==dgv=             item1.SubItems.Add(row1.Item("cat2"))
    '==dgv=             item1.SubItems.Add(row1.Item("description"))
    '==dgv=             listViewStock.Items.Add(item1)
    '==dgv=         Next  '-row1--
    '==dgv= '--wait for selection event.-
    '==dgv=     Else
    '==dgv=         sErrorMsg = gsGetLastSqlErrorMessage()
    '==dgv=         MsgBox("No STOCK datatable returned.." & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
    '==dgv=     End If '--get table-

    '==dgv= End Function  '--mbRefreshStockList-
    '= = = = = = = = = = = = = = =
    '-===FF->

    Private Sub frmChildStock_Load(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles MyBase.Load

        grpBoxStock.Text = ""
        '==grpBoxInStock.Text = ""
        labQtyInStock.Text = ""
        txtStockSearch.Text = ""
        labQtyInStock.Text = ""
        frameBrowse.Text = ""

        mIntFormDesignWidth = Me.Width  '--save starting dim.
        mIntFormDesignHeight = Me.Height  '--save starting dim.

        labItemHeader.Text = ""
        txtHdrBarcode.Text = ""
        chkAutoBarcode.Checked = False

        '== grpBoxGoods.Text = ""

        panelStockItem.Enabled = False

        btnEdit.Enabled = False
        btnNew.Enabled = False

        labSerialsBarcode.Text = ""
        labSerialsDescription.Text = ""
        labGoodsBarcode.Text = ""
        labGoodsDescription.Text = ""

        '==  Target is new Build 4251..
        '==  Target is new Build 4251..  Started in here 19-May-2020
        txtSupplierCode.Text = ""
        txtSupplierCode.Tag = "suppliercode"

        '- hide textboxes that capture checkbox results in the background..-
        '== txtInactive.Visible = False
        '== txtTrackSerials.Visible = False
        '= txtIsService.Visible = False
        '== txtRenaming.Visible = False

        '-- Tag all controls with DB Table column names..-
        txtBarcode.Tag = "barcode"
        txtStock_id.Tag = "stock_id"
        txtReOrderLevel.Tag = "reorderlevel"
        txtReOrderQty.Tag = "order_quantity"

        txtDescription.Tag = "description"
        txtCat1.Tag = "cat1"
        txtCat2.Tag = "cat2"
        txtInactive.Tag = "Inactive"
        picProduct.Tag = "productPicture"

        '=3301.606= txtIsService.Tag = "isServiceItem"
        '=3301.606= txtIsService.Tag = "isLabour"
        txtIsNonStockItem.Tag = "isNonStockItem"

        txtTrackSerials.Tag = "track_serial"
        txtRenaming.Tag = "allow_renaming"
        '=3101.1113= txtBrand_id.Tag = "brand_id"
        txtBrandName.Tag = "brandName"
        txtSupplier_id.Tag = "supplier_id"

        '==  NB ==
        '--  txtCostIncTax and txtSellIncTax are NOT DB table columns..
        '--   They are on the form for calculation fnctions..-

        txtCostExTax.Tag = "costExTax"
        txtGoodsTaxcode.Tag = "Goods_taxcode"
        txtSellExTax.Tag = "sellExTax"
        txtSalesTaxcode.Tag = "Sales_taxcode"

        '==  NB ==
        '--  txtCostIncTax and txtSellIncTax are NOT DB table columns..
        '--   They are on the form for calculation fnctions..-

        txtComments.Tag = "comments"

        '- set required fld list for New stock Records-
        '- BARCODE is checked in passing.
        msRequiredFields = "cat1; cat2; description; brandName; supplier_id; Goods_taxcode; Sales_taxcode; costExTax; "
        labRequiredFields.Text = ""

        '-- serials tab..-
        '-- wrap text for tran-list.-
        dgvSerials.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        '--  stock--
        mColPrefsStock = New Collection
        mColPrefsStock.Add("barcode")
        mColPrefsStock.Add("cat1")   '--fkey-
        mColPrefsStock.Add("cat2")   '-fkey-
        mColPrefsStock.Add("description")
        mColPrefsStock.Add("qtyInStock")
        mColPrefsStock.Add("brandName")
        '== mColPrefsStock.Add("productPicture")
        mColPrefsStock.Add("isNonStockItem")  '==3301.606= 
        '==3301.606=mColPrefsStock.Add("isLabour")
        mColPrefsStock.Add("stock_id")
        '== mColPrefsStock.Add("track_serial")
        '== mColPrefsStock.Add("inactive")
        '== mColPrefsStock.Add("supplier_id")
        '== mColPrefsStock.Add("costExTax")
        '== mColPrefsStock.Add("goods_TaxCode")
        '= mColPrefsStock.Add("sellExTax")
        '== mColPrefsStock.Add("sales_TaxCode")
        '== mColPrefsStock.Add("qtyInStock")
        '== mColPrefsStock.Add("allow_renaming")
        '== mColPrefsStock.Add("comments")

        '--  Categories--
        mColPrefsCategory1 = New Collection
        mColPrefsCategory1.Add("cat1_key")  '--PKEY--
        mColPrefsCategory1.Add("description")
        mColPrefsCategory1.Add("date_created")
        '- - - -- -
        mColPrefsCategory2 = New Collection
        mColPrefsCategory2.Add("cat2_key")  '--PKEY--
        mColPrefsCategory2.Add("description")
        mColPrefsCategory2.Add("date_created")
        '- - - -- - - - - - 

        mColPrefsBrands = New Collection
        mColPrefsBrands.Add("Brand_id")  '--PKEY--
        mColPrefsBrands.Add("BrandName")
        mColPrefsBrands.Add("date_created")

        '= labDLLversion.Text = msVersionPOS

        btnPrintLabels.Enabled = False
        txtlabelCount.Text = ""

        labItemAction.Text = "Loading.."

        '=3519.0214=
        '- refresh all stock column combos..-
        '-- DOESN't work here.. 
        '=  INPUT properties NOT loaded ??
        '= Call mbRefreshFkeyCombos()
        Me.Text = "- Stock Admin -"

        '=4201.1028=  Get Default printer.

        '- get printers collection and set up combos.
        '= cboInvoicePrinters.Items.Clear()
        '= cboReceiptPrinters.Items.Clear()
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            'For Each sName In colPrinters
            '    cboInvoicePrinters.Items.Add(sName)
            '    '= cboReceiptPrinters.Items.Add(sName)
            '    '=3411.0421- See below-
            '    '= If (InStr(LCase(sName), "adobe pdf") > 0) Then
            '    '=     msPdfPrinterName = sName  '-save PDF printer name--
            '    '= End If
            'Next sName
            'If (msDefaultPrinterName <> "") Then
            '    cboInvoicePrinters.SelectedItem = msDefaultPrinterName
            'Else
            '    cboInvoicePrinters.SelectedIndex = 0
            'End If
        End If '-getAvail.-  


        '=4201.0424=--  All stuff from SHOWN..
        '=4201.0424=--  All stuff from SHOWN..
        '=4201.0424=--  All stuff from SHOWN..
        Dim restrictions(3) As String
        Dim s1, sList As String
        Dim sSqlType As String
        Dim intADOtype, intSize, intPos As Integer

        labStaffName.Text = msStaffName
        labToday.Text = Format(Today, "ddd dd-MMM-yyyy")

        '- refresh all stock column combos..-
        Call mbRefreshFkeyCombos()

        '-- get schema info for table columns..-
        restrictions(1) = "dbo"
        restrictions(2) = "Stock"   '--get stock table cols info..
        Try
            mDataTableColumns = mCnnSql.GetSchema("columns", restrictions)
        Catch ex As Exception
            MsgBox("Failed to get columns schema info for Stock Table.." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            '==Me.Close()
            Call close_me()
        End Try

        '- save col info.
        sList = ""
        mColColumnDataTypes = New Collection
        If Not (mDataTableColumns Is Nothing) Then
            For Each row1 As DataRow In mDataTableColumns.Rows
                '== NB:  oleDb types come back as ADO type..
                intADOtype = CInt(row1.Item("data_type"))
                '-CHARACTER_MAXIMUM_LENGTH
                intSize = 0
                If Not IsDBNull(row1.Item("CHARACTER_MAXIMUM_LENGTH")) Then
                    intSize = CInt(row1.Item("CHARACTER_MAXIMUM_LENGTH"))
                End If
                sSqlType = gsGetSqlType(intADOtype, intSize)
                '-- drop size suffix for varchar.-
                intPos = InStr(sSqlType, "(")
                If (intPos > 0) Then
                    sSqlType = Trim(VB.Left(sSqlType, intPos - 1))
                End If
                sList &= "Col: " & row1.Item("column_name") & _
                             "-  DataType= " & sSqlType & vbCrLf
                mColColumnDataTypes.Add(UCase(sSqlType), LCase(row1.Item("column_name")))
            Next row1
        End If
        '--test-
        '== MsgBox("Found schema cols: " & vbCrLf & sList, MsgBoxStyle.Information)

        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        '=3301.428=Dim colSystemInfo As Collection
        '=3301.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--

        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")

        If mSysInfo1.contains("POS_BARCODEFONTNAME") Then
            msBarcodeFontName = mSysInfo1.item("POS_BARCODEFONTNAME")
            s1 = mSysInfo1.item("POS_BARCODEFONTSIZE")
            If IsNumeric(s1) Then
                mIntBarcodeFontSize = CInt(s1)
            End If
        Else
            '- used default-
        End If
        If mSysInfo1.contains("POS_SELL_MARGIN") AndAlso _
                                    IsNumeric(mSysInfo1.item("POS_SELL_MARGIN")) Then
            mDecSellMargin = CDec(mSysInfo1.item("POS_SELL_MARGIN"))
        End If
        If mSysInfo1.contains("GSTPercentage") AndAlso _
                                     IsNumeric(mSysInfo1.item("GSTPercentage")) Then
            mDecGSTPercentage = CDec(mSysInfo1.item("GSTPercentage"))
        End If
        '=3301.428= End If
        '-- show rates..-
        labRates.Text = "GST: " & Format(mDecGSTPercentage, "  0.00") & "%;  " & _
                                 " Sell Price: Cost+ " & Format(mDecSellMargin, "  0.00") & " %. "
        '-3501.-0826=
        If mbAddNewStockOnly Then  '-was called from Goods Received.
            Call mSubSetupNewStock()
            If mbAddNewStockAutoGenBarcode Then  '-came from Goods via F5-
                chkAutoBarcode.Checked = True
                chkAutoBarcode.Enabled = False
                txtBarcode.Text = ""
            Else
                '- these were cleared
                txtBarcode.Text = msAddNewStockBarcode
            End If
            txtBarcode.ReadOnly = True     '-can't change this in Stock Frm.. (esp. if autoGen)
            '--   (Go back to Goods for different barcode.)
            txtSupplier_id.Text = mIntAddNewStockSupplier_id
            txtSupplier_id.ReadOnly = True
            txtSupplierName.Text = msAddNewStockSupplierName
            '- set required fld list for New stock Records-
            msRequiredFields = "cat1; cat2; description; brandName; Goods_taxcode; Sales_taxcode; costExTax; "
            labRequiredFields.Text = ""
            cboCat1.Select()
            Exit Sub
        End If  '-new stock setup-

        btnStockCancel.Enabled = False
        '=dgv= Call mbRefreshStockList()  '--TEMP=

        Call mbInitialiseBrowse()
        If (dgvStockList.RowCount <= 0) Then
            MsgBox("Please note-  " & vbCrLf & "No stock items have been set up yet.." & vbCrLf & vbCrLf & _
                      "Use the 'New' button to create and edit a new stock item.", MsgBoxStyle.Information)
        End If

        '=4201.0727=
        '-- SerialNo Context menu..
        '- --  Popup menu for Right click on Serial Grid..-
        mContextMenuSerialInfo = New ContextMenu
        mnuCopyItemSerialNo.Name = "CopyItemSerialNo"
        mContextMenuSerialInfo.MenuItems.Add(mnuCopyItemSerialNo)

        '-- mContextMenuPurchasesInfo-
        mContextMenuPurchasesInfo = New ContextMenu
        mnuCopyItemInvoiceNo.Name = "CopyItemInvoiceNo"
        mContextMenuPurchasesInfo.MenuItems.Add(mnuCopyItemInvoiceNo)
        '-- done menus..


        labItemAction.Text = "Done.."

        btnNew.Enabled = True

    End Sub  '-- load --
    '= = = = = = = = = = = = = =  
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Form Resized --

    Private Sub frmChildStock_Resize(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        '== If mbIsInitialising Then Exit Sub

        'If (Me.WindowState = System.Windows.Forms.FormWindowState.Minimized) Then
        '    Exit Sub
        'End If
        ''-- We don't want user to move the child form.
        Me.Top = 0
        Me.Left = 0

        '-- see delegate called from Mother resize..

        '--  can't make smaller than original..-
        'If (Me.Height < mIntFormDesignHeight) Then
        '    Me.Height = mIntFormDesignHeight
        'End If
        'If (Me.Width < mIntFormDesignWidth) Then
        '    Me.Width = mIntFormDesignWidth
        'End If
        ' ''-- resize main box and top panel-
        'panelBanner.Width = Me.Width - 11
        'grpBoxStock.Width = panelBanner.Width
        'grpBoxStock.Height = Me.Height - 120  '= 93
        'DoEvents()  '--time to adjust contents.

        ''-resize browser box=
        'frameBrowse.Width = ((grpBoxStock.Width \ 9) * 4)  '--four ninths of total-
        'If (frameBrowse.Width < 440) Then frameBrowse.Width = 440
        'frameBrowse.Height = grpBoxStock.Height - 9
        'DoEvents()  '--time to adjust contents.
        ''-- RHS- Item info-
        'tabControlStock.Left = frameBrowse.Left + frameBrowse.Width + 3
        'tabControlStock.Width = grpBoxStock.Width - tabControlStock.Left - 11
        'tabControlStock.Height = grpBoxStock.Height - tabControlStock.Top - 11
        'DoEvents()  '--time to adjust contents.

        'dgvStockList.Width = frameBrowse.Width - 10
        'dgvStockList.Height = frameBrowse.Height - 100

        'panelStockItem.Width = TabPageItemDetail.Width - 3
        'DoEvents()  '--time to adjust contents.
        'panelStockItem.Height = TabPageItemDetail.Height - panelStockItem.Top - 3
        'DoEvents()  '--time to adjust contents.
        'picProduct.Left = panelStockItem.Width - picProduct.Width - 10

        'dgvSerials.Width = TabPageSerials.Width - 5
        'dgvSerials.Height = TabPageSerials.Height - dgvSerials.Top - 7

        'dgvGoods.Width = TabPagePurchases.Width - 7
        'dgvGoods.Height = dgvSerials.Height

        'dgvSales.Width = TabPageSales.Width - 7
        'dgvSales.Height = dgvSerials.Height

        'panelItemHdr.Left = tabControlStock.Left
        'panelItemHdr.Width = tabControlStock.Width

        'labDLLversion.Top = grpBoxStock.Top + grpBoxStock.Height + 3

    End Sub  '--resize-                                                                                                                                     
    '= = = = = = = = = = = = = =  
    '-===FF->

    '- S T O C K - stuff--
    '- S T O C K - stuff--
    '- S T O C K - stuff--
    '- S T O C K - stuff--

    '--BROWSING STOCK.. --

    '--  D at a  G r i d  E v e n t s..--
    '--  D at a  G r i d  E v e n t s..--

    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvStockList_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvStockList.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvStockList.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)
    End Sub
    '= = = = = = = = =  = = =
    '-===FF->
    '== Reference Tables-
    '--  New Cat1

    Private Sub btnNewCat1_Click(sender As Object, e As EventArgs) Handles btnNewCat1.Click
        '--  New Cat1
        Dim frmEdit1 As frmEdit

        frmEdit1 = New frmEdit
        frmEdit1.connection = mCnnSql '--job tracking sql connenction..-
        frmEdit1.colTables = mColSqlDBInfo
        frmEdit1.DBname = msSqlDbName
        frmEdit1.tableName = "Category1" '--""
        '== frmEdit1.IsSqlServer = True '--bIsSqlServer
        frmEdit1.newRecord = True
        frmEdit1.StaffId = mIntStaff_id
        frmEdit1.StaffName = msStaffName
        frmEdit1.versionPOS = msVersionPOS
        frmEdit1.PreferredColumns = mColPrefsCategory1
        frmEdit1.Title = "Add New Category1 record"
        frmEdit1.ShowDialog()

        If Not frmEdit1.cancelled Then
            '-- get new barcode..
            '=txtSupplierBarcode.Text = frmEdit1.selectedBarcode
        End If  '-cancelled.
        '- refresh all stock column combos..-
        Call mbRefreshFkeyCombos()
    End Sub '-btnNewCat1_Click-
    '= = = = = =  = = = = = = = =
    '--  New Cat2

    Private Sub btnNewCat2_Click(sender As Object, e As EventArgs) Handles btnNewCat2.Click
        '--  New Cat2
        Dim frmEdit1 As frmEdit

        frmEdit1 = New frmEdit
        frmEdit1.connection = mCnnSql '--job tracking sql connenction..-
        frmEdit1.colTables = mColSqlDBInfo
        frmEdit1.DBname = msSqlDbName
        frmEdit1.tableName = "Category2" '--""
        '== frmEdit1.IsSqlServer = True '--bIsSqlServer
        frmEdit1.newRecord = True
        frmEdit1.StaffId = mIntStaff_id
        frmEdit1.StaffName = msStaffName
        frmEdit1.versionPOS = msVersionPOS
        frmEdit1.PreferredColumns = mColPrefsCategory2
        frmEdit1.Title = "Add New Category2 record"
        frmEdit1.ShowDialog()

        If Not frmEdit1.cancelled Then
            '-- get new barcode..
            '=txtSupplierBarcode.Text = frmEdit1.selectedBarcode
        End If  '-cancelled.
        '- refresh all stock column combos..-
        Call mbRefreshFkeyCombos()

    End Sub '-btnNewCat2_Click-
    '= = = = = =  = = = = = = =
    '-===FF->

    '--new brand-

    Private Sub btnNewBrand_Click(sender As Object, e As EventArgs) Handles btnNewBrand.Click
        '--  New Brand
        Dim frmEdit1 As frmEdit

        frmEdit1 = New frmEdit
        frmEdit1.connection = mCnnSql '--job tracking sql connenction..-
        frmEdit1.colTables = mColSqlDBInfo
        frmEdit1.DBname = msSqlDbName
        frmEdit1.tableName = "StockBrands" '--""
        '== frmEdit1.IsSqlServer = True '--bIsSqlServer
        frmEdit1.newRecord = True
        frmEdit1.StaffId = mIntStaff_id
        frmEdit1.StaffName = msStaffName
        frmEdit1.versionPOS = msVersionPOS
        frmEdit1.PreferredColumns = mColPrefsBrands
        frmEdit1.Title = "Add New Brand record"
        frmEdit1.ShowDialog()

        If Not frmEdit1.cancelled Then
            '-- get new barcode..
            '=txtSupplierBarcode.Text = frmEdit1.selectedBarcode
        End If  '-cancelled.
        '- refresh all stock column combos..-
        Call mbRefreshFkeyCombos()
    End Sub  '-brands.
    '= = = = =  = = = =
    '-===FF->

    '--PrintLabels-

    Private Sub btnPrintLabels_Click(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles btnPrintLabels.Click
        Dim col1, colLabels As Collection
        Dim intCount, intNewCount As Integer
        Dim clsPrint1 As New clsPrintSaleDocs
        Dim frmGetPrinter1 As frmGetPrinter
        Dim msPrinterName As String

        If (txtlabelCount.Text = "") OrElse (Not IsNumeric(txtlabelCount.Text)) Then
            Exit Sub
        End If
        colLabels = New Collection
        intCount = CInt(txtlabelCount.Text)
        If (intCount > 0) Then
            frmGetPrinter1 = New frmGetPrinter
            frmGetPrinter1.WhichPrinter = "label"
            frmGetPrinter1.RequestedNumberOfLabels = intCount
            frmGetPrinter1.ShowDialog()
            If frmGetPrinter1.cancelled Then
                MsgBox("Request cancelled.", MsgBoxStyle.Information)
                Exit Sub
            End If
            intNewCount = frmGetPrinter1.NumberOfLabels
            msPrinterName = frmGetPrinter1.SelectedPrinterName
            If intNewCount > 0 Then
                For ix As Integer = 1 To intNewCount
                    col1 = New Collection
                    '== col1.Add("barcode", "name")
                    col1.Add(txtBarcode.Text, "barcode")
                    col1.Add(txtDescription.Text, "description")
                    '- txtSellIncTax.Text-
                    col1.Add(txtSellIncTax.Text, "price")
                    colLabels.Add(col1, CStr(ix))
                Next ix
                frmGetPrinter1.Close()
                '- do printing-
                Call clsPrint1.PrintStockLabels(colLabels, VB.Left(msBusinessShortName, 30), _
                                                 msPrinterName, msBarcodeFontName, mIntBarcodeFontSize)
            End If  '-new count-
        End If  '--count-

    End Sub  '--print-
    '= = = = = = = = =  = = =
    '-===FF->

    '=-- 4201.1013-=
    '--     Stock DGV..  Try and catch DownArrow..
    '--     Drop it if we are currently loading an item..
    '-- Catch ARROW keys-

    ' PreviewKeyDown is where you preview the key.
    ' Do not put any logic here, instead use the
    ' KeyDown event after setting IsInputKey to true.

    Private Sub dgvStockList_PreviewKeyDown(ByVal sender As Object, _
                                               ByVal e As PreviewKeyDownEventArgs) _
                                             Handles dgvStockList.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Enter, Keys.Escape, Keys.Up, Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub '-PreviewKeyDown-
    '= = = = = = = = = = == =

    '--ACTUAL Data GridView Control keyDown --
    '--- check for Fx  etc-
    '=-- 4201.1013-=
    '--     Stock DGV..  Try and catch DownArrow..

    Private Sub dgvStockList_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles dgvStockList.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        Dim intGridCol As Integer = Me.dgvStockList.CurrentCellAddress.X  '== eventArgs.ColumnIndex
        Dim intGridRow As Integer = Me.dgvStockList.CurrentCellAddress.Y  '= eventArgs.RowIndex

        If (KeyCode = System.Windows.Forms.Keys.Up) Or (KeyCode = System.Windows.Forms.Keys.Down) Then
            '-- check if we are busy.
            If mbItemIsLoading Then
                '-- drop the key so as not to stumble over loading the current item.
                eventArgs.Handled = True
            End If
        End If  '-up/down.
    End Sub  '-key down.
    '= = = = = = = = =  = = =
    '-===FF->

    '-- cell click.--
    '-- cell click.--

    Private Sub dgvStockList_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvStockList.CellMouseClick
        Dim lRow, lCol As Integer
        Dim intStock_id As Integer
        Dim colKeys, colRowValues As Collection

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (dgvStockList.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                intStock_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                If (intStock_id > 0) And (intStock_id <> mIntStock_id) Then '-- has changed..-
                    Call mbShowStockInfo(intStock_id)
                End If
            End If  '-selected-
        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--
    Private Sub dgvStockList_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvStockList.CellMouseDoubleClick
        Dim lRow As Integer
        Dim intStock_id As Integer
        Dim colKeys, colRowValues As Collection

        '== lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow >= 0) Then '--ok--
            mLngSelectedRow = lRow
            '--  get stock id and start edit.--
            If (lRow >= 0) And (dgvStockList.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                intStock_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                If (intStock_id > 0) And (intStock_id <> mIntStock_id) Then '-- has changed..-
                    Call mbShowStockInfo(intStock_id)
                End If
            End If  '-selected-
        End If '--row--
    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '-- serial grid- Selection changed.-
    '-- save time we got into row..
    '--   So we don't try and show stock info until selecting has settled.
    '--  set timer start time when row is selected
    Private dblTimerStart As Double = 0
    Private mIntCurrentStock_id As Integer = -1

    Private Sub timerGrid_Tick(sender As Object, ev As EventArgs) Handles timerGrid.Tick
        Dim dblTimerNow As Double
        Dim colKeys, colRowValues As Collection

        If dblTimerStart <= 0 Then
            Exit Sub   '--not active.
        End If
        dblTimerNow = VB.Timer
        If (dblTimerNow > (dblTimerStart + 0.3)) Then  '-
            dblTimerStart = 0
            timerGrid.Stop()
            If mIntCurrentStock_id > 0 Then
                Call mbShowStockInfo(mIntCurrentStock_id)
            End If
        End If
    End Sub '-timerGrid_Tick-
    '= = = = =  = = = = = = = = = = = ==  =

    '-- selection changed.

    'Private Sub dgvStockList_SelectionChanged(ByVal sender As Object, _
    '                               ByVal e As EventArgs) Handles dgvStockList.SelectionChanged
    '    Dim intStock_id As Integer
    '    Dim colKeys, colRowValues As Collection
    '    Dim dblTimerNow As Double

    '    If (dgvStockList.SelectedRows.Count > 0) Then
    '        Dim intRowIndex = dgvStockList.CurrentCell.RowIndex
    '        If (intRowIndex >= 0) Then
    '            mLngSelectedRow = intRowIndex
    '            Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
    '            intStock_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
    '            If (intStock_id > 0) And (intStock_id <> mIntStock_id) Then '-- has changed..-

    '                dblTimerStart = VB.Timer
    '                mIntCurrentStock_id = intStock_id  '-set current id for display.
    '                timerGrid.Enabled = True
    '                timerGrid.Start()

    '                'If (dblTimerNow > (dblTimerStart + 0.3)) Then  '-
    '                '= Call mbShowStockInfo(intStock_id)
    '                'End If
    '            End If
    '        End If  '-index-
    '    End If '-count-
    'End Sub  '-StockList_SelectionChanged-
    '== = = = =  == = = = = = = = = =  =

    '-- Row enter-

    Private Sub dgvStockList_RowEnter(ByVal sender As Object, _
                                        ByVal ev As DataGridViewCellEventArgs) _
                                        Handles dgvStockList.RowEnter

        Dim ix, intRow, intCol, intCustomer_id As Integer
        Dim intStock_id As Integer
        Dim s1 As String
        Dim colKeys, colRowValues As Collection

        intRow = ev.RowIndex
        intCol = ev.ColumnIndex
        mLngSelectedRow = intRow

        If (intRow >= 0) Then
            Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
            intStock_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
            If (intStock_id > 0) And (intStock_id <> mIntStock_id) Then '-- has changed..-
                dblTimerStart = VB.Timer
                mIntCurrentStock_id = intStock_id  '-set current id for display.
                timerGrid.Enabled = True
                timerGrid.Start()

                '= Call mbShowStockInfo(intStock_id)
            End If
        End If  '-row-
    End Sub  '-row enter-
    '= = = = = = = = = = = = == = = =
    '-===FF->

    '-- STOCK Browser.. txt FIND Activity.--
    '-- STOCK Browser.. txt FIND Activity.--
    '--BROWSING STOCK.. --

    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim intStock_id As Integer
        Dim colKeys, colRowValues As Collection

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If dgvStockList.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = dgvStockList.SelectedRows(0).Cells(0).RowIndex
                If (lRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '= Call mbSelectStockRow(mLngSelectedRow)
                    Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                    intStock_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                    If (intStock_id > 0) And (intStock_id <> mIntStock_id) Then '-- has changed..-
                        Call mbShowStockInfo(intStock_id)
                    End If

                End If '--row--
                iKeyAscii = 0 '--processed--
            End If '--count--

            eventArgs.KeyChar = Chr(iKeyAscii)
            If iKeyAscii = 0 Then
                eventArgs.Handled = True
            End If
        End If  '--Enter-
    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFind_Enter(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles txtFind.Enter
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        Call mBrowse1.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '=3411.0417-  Catch Enter Key on stock srch text-

    Private Sub txtStockSearch_keyPress(ByVal sender As System.Object, _
                                         ByVal EventArgs As KeyPressEventArgs) Handles txtStockSearch.KeyPress
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)
        Dim e2 As New EventArgs
        If keyAscii = 13 Then '--enter-
            Call cmdStockSearch_Click(cmdStockSearch, e2)
            keyAscii = 0 '--processed..-
        End If  '13-
        EventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            EventArgs.Handled = True
        End If

    End Sub  '-keypress.
    '= = = = = = = = == 

    '-- Stock Browser..  Full text Search..--
    '-- Stock Browser..  Full text Search..--

    Private Sub cmdStockSearch_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles cmdStockSearch.Click
        Dim sWhere As String = ""
        Dim sSql As String '--search sql..-- 
        '= Dim s1, s2 As String
        Dim asColumns As Object

        '--  rebuild Search Columns and call makeTextSearch...-

        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))
        '--  now in the Interface..--
        '== asColumns = mRetailHost1.stockSearchColumns()
        asColumns = New Object() _
                      {"barcode", "cat1", "cat2", "description"}

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtStockSearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere + " AND "
            sWhere = sWhere + sSql
        End If
        Call mbBrowseStockTable(sWhere)

    End Sub '-cmdStockSearch-
    '= = = = = = = = = = = = =  =

    Private Sub cmdClearStockSearch_Click(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles cmdClearStockSearch.Click
        txtStockSearch.Text = ""
        '=4234.= System.Windows.Forms.Application.DoEvents()
        Call cmdStockSearch_Click(cmdStockSearch, New System.EventArgs())

    End Sub  '-ClearStockSearch-
    '= = = = = = = = = = = = = = = =

    '==  END of stock BROWSING..--
    '==  END of stock BROWSING..--
    '-===FF->

    '== Serials and Purchases Grids.  
    '==     Context Menus..
    Private mIntContextMenuSerialsGridRow As Integer = -1
    Private mIntContextMenuPurchasesGridRow As Integer = -1

    '-- COPY SerialNo of selected part..-
    Public Sub mnuCopyItemSerialNo_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuCopyItemSerialNo.Click
        Dim sSerialNo As String

        If (mIntContextMenuSerialsGridRow >= 0) Then
            sSerialNo = Trim(dgvSerials.Rows(mIntContextMenuSerialsGridRow).Cells("SerialNumber").Value)
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            If (sSerialNo <> "") Then
                My.Computer.Clipboard.SetText(sSerialNo) '-- serial is first clumn..-.
            Else
                MessageBox.Show(Me, "No Serial-No..", _
                                  "JobMatixPOS Stock Serials", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
        End If
    End Sub '--part serial..-
    '= = = = = = = = = = = = = = == = 

    '-- COPY INVOICE-No of selected purchase..-

    Public Sub mnuCopyItemInvoiceNo_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuCopyItemInvoiceNo.Click
        Dim sInvoiceNo As String

        If (mIntContextMenuPurchasesGridRow >= 0) Then
            sInvoiceNo = Trim(dgvGoods.Rows(mIntContextMenuPurchasesGridRow).Cells("invoice_no").Value)
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            If (sInvoiceNo <> "") Then
                My.Computer.Clipboard.SetText(sInvoiceNo) '-- ..-.
            Else
                MessageBox.Show(Me, "No Invoice-No..", _
                                  "JobMatixPOS Stock Purchasess", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
        End If
    End Sub '--part serial..-
    '= = = = = = = = = = = = = = == = 
    '-===FF->

    '--Serials Cell Mouse-down..  Context menu.. 
    '--   CopySerialNo..

    Private Sub dgvSerials_CellMouseDown(ByVal sender As Object, _
                                              ByVal ev As DataGridViewCellMouseEventArgs) _
                                               Handles dgvSerials.CellMouseDown
        Dim intRow As Integer = ev.RowIndex
        Dim intColumn As Integer = ev.ColumnIndex
        Dim sSerialNo As String = ""
        Dim p1 = New Point(ev.X, ev.Y + ((intRow + 1) * dgvSerials.RowTemplate.Height))

        If (intRow >= 0) And (intColumn >= 0) Then
            If ev.Button = Windows.Forms.MouseButtons.Right Then
                sSerialNo = Trim(dgvSerials.Rows(intRow).Cells("SerialNumber").Value)
                If (sSerialNo <> "") Then
                    '-- select the row..
                    With Me.dgvSerials
                        .CurrentCell = .Rows(intRow).Cells(intColumn)
                        .Rows(intRow).Selected = True
                    End With
                    '-- wait for row to be selected.
                    '=4234.= DoEvents()
                    Thread.Sleep(100)
                    '=4234.= DoEvents()
                    '-- Avoid the 'disabled' gray text by locking updates
                    LockWindowUpdate(dgvSerials.Handle.ToInt32)
                    '---- A disabled TextBox will not display a context menu
                    dgvSerials.Enabled = False
                    '--- Give the previous line time to complete
                    '=4234.= System.Windows.Forms.Application.DoEvents()
                    '-- Display our own context menu
                    mIntContextMenuSerialsGridRow = intRow
                    mContextMenuSerialInfo.Show(CType(sender, Control), p1)
                    ' Enable the control again
                    dgvSerials.Enabled = True
                    '-- Unlock updates
                    LockWindowUpdate(0)
                End If  '-barcode-
            End If  '-right button.
        End If  '-intRow-
    End Sub  '-dgvSerials_CellMouseDown-
    '= = = = = = = = = = = = = = = ==  =
    '-===FF->

    '--Goods Cell Mouse-down..  Context menu.. 
    '--Goods Cell Mouse-down..  Context menu.. 
    '--Goods Cell Mouse-down..  Context menu.. 
    '--   Copy Suppl. InvoicelNo.. (invoice_no)

    Private Sub dgvGoods_CellMouseDown(ByVal sender As Object, _
                                              ByVal ev As DataGridViewCellMouseEventArgs) _
                                               Handles dgvGoods.CellMouseDown
        Dim intRow As Integer = ev.RowIndex
        Dim intColumn As Integer = ev.ColumnIndex
        Dim sInvoiceNo As String = ""
        Dim p1 = New Point(ev.X, ev.Y + ((intRow + 1) * dgvSerials.RowTemplate.Height))

        If (intRow >= 0) And (intColumn >= 0) Then
            If ev.Button = Windows.Forms.MouseButtons.Right Then
                sInvoiceNo = Trim(dgvGoods.Rows(intRow).Cells("invoice_no").Value)
                If (sInvoiceNo <> "") Then
                    '-- select the row..
                    With Me.dgvGoods
                        .CurrentCell = .Rows(intRow).Cells(intColumn)
                        .Rows(intRow).Selected = True
                    End With
                    '-- wait for row to be selected.
                    '=4234.= DoEvents()
                    Thread.Sleep(100)
                    '=4234.= DoEvents()
                    '-- Avoid the 'disabled' gray text by locking updates
                    LockWindowUpdate(dgvGoods.Handle.ToInt32)
                    '---- A disabled TextBox will not display a context menu
                    dgvGoods.Enabled = False
                    '--- Give the previous line time to complete
                    '=4234.= System.Windows.Forms.Application.DoEvents()
                    '-- Display our own context menu
                    mIntContextMenuPurchasesGridRow = intRow
                    mContextMenuPurchasesInfo.Show(CType(sender, Control), p1)
                    ' Enable the control again
                    dgvGoods.Enabled = True
                    '-- Unlock updates
                    LockWindowUpdate(0)
                End If  '-barcode-
            End If  '-right button.
        End If  '-intRow-

    End Sub '-dgvGoods_CellMouseDown-
    '= = = = = = = = = = = = = = = = = =
    '==  END of Serials Grid Context menu...--
    '-===FF->

    '==   -- 4201.1027/1028.  27-Oct-2019-  Started 27-Oct-2019-
    '==         Add DblClick to Item Purchases Grid to show GoodsReceived Trans. for that Item. .

    Private Sub dgvGoods_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                  Handles dgvGoods.CellMouseDoubleClick
        Dim intRow, intCol, intGoods_id As Integer
        Dim colKeyValues As Collection '--PKEYS of selected record-
        Dim colRowValues As Collection '--selected grid row-
        Dim clsGoodsInfo1 As clsGoodsInfo
        Dim colSelectedGoodsTransaction, colGoodsInfo As Collection
        Dim clsPrintGoods1 As clsPrintGoods
        Dim sSupplierName, sSupplierBarcode As String

        intCol = eventArgs.ColumnIndex
        intRow = eventArgs.RowIndex

        If (intRow >= 0) Then '--not header row--
            intGoods_id = CInt(dgvGoods.Rows(intRow).Cells("Goods_id").Value)

            '--init class for goods Info.
            clsGoodsInfo1 = New clsGoodsInfo(msServer, mCnnSql, msSqlDbName, msVersionPOS)

            If Not clsGoodsInfo1.GetCollectedGoodsInfo(intGoods_id, colSelectedGoodsTransaction) Then
                MsgBox("Lookup Goods: Error- Failed to get Goods Info.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            '- show Goods invoice Info.
            If (colSelectedGoodsTransaction IsNot Nothing) Then
                clsPrintGoods1 = New clsPrintGoods()
                colGoodsInfo = colSelectedGoodsTransaction.Item("Goods_Info")
                sSupplierName = colGoodsInfo.Item("supplierName")
                sSupplierBarcode = colGoodsInfo.Item("supplier_barcode")

                clsPrintGoods1.StaffName = msStaffName
                clsPrintGoods1.BusinessName = msBusinessName
                clsPrintGoods1.SupplierName = sSupplierName
                clsPrintGoods1.SupplierBarcode = sSupplierBarcode
                clsPrintGoods1.versionPOS = msVersionPOS

                '- Now can preview-
                If Not clsPrintGoods1.PrintGoodsReceived(colSelectedGoodsTransaction, _
                                                         msDefaultPrinterName, True) Then
                End If  '-print..
            End If   '-nothing.-
        End If  '-row

    End Sub  '-dgvGoods_CellMouseDblClickEvent-
    '= = = = = = = = = = = = 
    '-===FF->

    '==    -- 4201.1013.  13-Oct-2019..
    '==        -- Stock Admin.  On POS Item Sales Tab,  
    '==                      catch DoubleCiick event on invoice line to bring up  (show) the full invoice.

    '--mouse activity---  
    '-- select row to select Invoice to show..--

    Private Sub dgvSales_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvSales.CellMouseDoubleClick
        Dim lRow, lCol As Integer
        Dim colKeyValues As Collection '--PKEYS of selected record-
        Dim colRowValues As Collection '--selected grid row-

        lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow >= 0) Then '--not header row--
            Dim intInvoice_id As Integer
            Dim sTranType As String

            intInvoice_id = CInt(dgvSales.Rows(lRow).Cells("sale_invoice_no").Value)
            sTranType = dgvSales.Rows(lRow).Cells("invoice_trancode").Value
            Call mbShowInvoice(intInvoice_id, sTranType)
        End If  '-row-

    End Sub  '-dgvSales_CellMouseDblClickEvent-
    '= = = = = = = = = = = = == = = = === =
    '==  END of Sales Grid DblClick action...--
    '-===FF->

    '-- Edit-

    Private Sub btnEdit_Click(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles btnEdit.Click
        mbItemIsLoading = True
        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnStockCancel.Enabled = True

        '- refresh all stock column combos..-
        txtBarcode.ReadOnly = True
        labItemAction.Text = "Editing Item " & txtBarcode.Text & ".."

        mbIsNewItem = False
        btnStockCommit.Enabled = False

        '=dgv= listViewStock.Enabled = False
        frameBrowse.Enabled = False

        Call mbRefreshFkeyCombos()
        labRequiredFields.Text = ""
        panelStockItem.Enabled = True
        mbItemIsLoading = False
        cboCat1.Select()

    End Sub  '--edit-
    '= = = = = = = = = = = = 
    '-===FF->

    '-- new --

    Private Sub btnNew_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles btnNew.Click
        'mbIsNewItem = True
        'mbItemIsLoading = True
        'btnNew.Enabled = False
        'btnEdit.Enabled = False
        'btnStockCancel.Enabled = True
        'txtHdrBarcode.Text = ""
        'labItemHeader.Text = ""

        ''= listViewStock.Enabled = False
        'frameBrowse.Enabled = False
        'btnStockCommit.Enabled = False
        ''--  clear all text fields..--
        'Call mbClearAllTextFields()
        'labItemAction.Text = "Creating new Item.."
        ''- refresh all stock column combos..-
        'Call mbRefreshFkeyCombos()
        'panelStockItem.Enabled = True
        'mbItemIsLoading = False

        Call mSubSetupNewStock()
        '= txtBarcode.Select()   '--focus-
        chkAutoBarcode.Checked = False
        chkAutoBarcode.Enabled = True
        chkAutoBarcode.Select()

    End Sub  '--new-
    '= = = = = = = = = = = = = 

    '-- Tab Control Events..

    '-- deselecting..
    '-- Disallow if in NEW mode .

    Private Sub TabControlStock_Deselecting(sender As TabControl, ev As TabControlCancelEventArgs) _
                                                                    Handles tabControlStock.Deselecting
        If mbIsNewItem Then  '= LCase(ev.TabPage.Name) = "tabpageitemdetail" Then  '-TabPageItemDetail-
            '= If grpBoxManualCount.Enabled Then
            ev.Cancel = True
            '= End If
        End If
    End Sub  '--deselecting..
    '= = = = = = = = = = = == =  = =
    '-===FF->


    '-- Browse for Product picture--

    Private Sub picProduct_Click(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles picProduct.Click
        Dim sTitle, sStartPath, sFullPath As String
        Dim MyResult As System.Windows.Forms.DialogResult
        Dim byteImage1 As Byte()
        Dim image1 As Image

        '--  image column..--
        sTitle = ""
        sStartPath = ""
        '--  get actual (image File) location from operator..--
        openDlg1.Title = txtDescription.Text & ":  Select Image file for this record.."
        openDlg1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.ICO)|*.BMP;*.JPG;*.GIF;*.PNG;*.ICO|All files (*.*)|*.* "
        '= "SQL DB Backup Files (*.png)|*.jpg|All Files (*.*)|*.*"
        openDlg1.InitialDirectory = sStartPath '--msAppPath
        openDlg1.FileName = sStartPath & sTitle
        MyResult = openDlg1.ShowDialog
        '--check for cancel--
        If (MyResult <> System.Windows.Forms.DialogResult.OK) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        '== On Error GoTo 0
        sFullPath = openDlg1.FileName
        Try
            '--load image bytes..--
            byteImage1 = mabConvertImageFiletoBytes(sFullPath)
        Catch ex As Exception
            MsgBox("Failed to load image data from File: " & sFullPath & vbCrLf & _
                               "Error: " & ex.Message)
            Exit Sub
        End Try
        '-- save image data for this columns-
        mByteImage1 = byteImage1
        '== If mColRowImages.Contains(sFldName) Then
        '== mColRowImages.Remove(sFldName)
        '== End If
        '== mColRowImages.Add(byteImage1, sFldName)

        '--- load picture from byte array..-
        Try
            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(byteImage1)
            image1 = System.Drawing.Image.FromStream(ms)
            picProduct.Image = image1
            ms.Close()
            '= Call mbDataChanged(intIndex)
            Call mbSetDataModified("picProduct;")
        Catch ex As Exception
            MsgBox("Failed to load product image from MemStream: " & vbCrLf & _
                              "Error: " & ex.Message)
            Exit Sub
        End Try

    End Sub  '--pic--
    '= = = = = = = = = = = = 
    '-===FF->

    '-- Stock Item combo/text changes..--
    '-- Stock Item combo/text changes..--

    '- combo changes --
    '- combo changes --

    Private Sub cboCat1_SelectedIndexChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles cboCat1.SelectedIndexChanged
        If mbItemIsLoading Then Exit Sub
        With cboCat1
            If (.SelectedIndex >= 0) Then
                txtCat1.Text = .SelectedItem
                Call mbSetDataModified("txtCat1;")
                chkAutoBarcode.Enabled = False      '-- must cancel from here on to change this.
            End If
        End With
    End Sub '-Cat1_SelectedIndexChanged-
    '== = =  == = = = = = = = = = = =

    Private Sub cboCat2_SelectedIndexChanged(ByVal sender As System.Object, _
                                                    ByVal e As System.EventArgs) Handles cboCat2.SelectedIndexChanged
        If mbItemIsLoading Then Exit Sub
        With cboCat2
            If (.SelectedIndex >= 0) Then
                txtCat2.Text = .SelectedItem
                Call mbSetDataModified("txtCat2;")
            End If
        End With
    End Sub  '-Cat2_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = 

    Private Sub cboCat1_KeyPress(ByVal eventSender As System.Object, _
                           ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                             Handles cboCat1.KeyPress, cboCat2.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim cbo1 As ComboBox = CType(eventSender, ComboBox)
        If (keyAscii = 13) AndAlso (cbo1.SelectedIndex >= 0) Then '--enter- and something selected.
            keyAscii = 0
            eventArgs.Handled = True
            SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub  '- cboCat2.KeyPress_KeyPress-
    '= = = = = = = = = = = = 
    '-===FF->

    '- Brand-
    '=3519.0213-  ID now at RHS.

    Private Sub cboBrand_SelectedIndexChanged(ByVal sender As System.Object, _
                                                     ByVal e As System.EventArgs) _
                                                       Handles cboBrand.SelectedIndexChanged
        Dim s1 As String
        Dim iPos As Integer
        If mbItemIsLoading Then Exit Sub
        With cboBrand
            If (.SelectedIndex >= 0) Then
                s1 = .SelectedItem
                '-- separate id/name
                iPos = InStr(s1, ";")
                If (iPos > 1) Then
                    txtBrand_id.Text = Trim(Mid(s1, iPos + 1)) '=  Trim(VB.Left(s1, iPos - 1))
                    txtBrandName.Text = Trim(VB.Left(s1, iPos - 1)) '=  Trim(Mid(s1, iPos + 1))
                    Call mbSetDataModified("txtBrand_id;")
                End If '-ipos-
            End If  '--selected-
        End With
    End Sub '--cboBrand-
    '= = = = = = = = = = = = = = = = = =

    '=3519.0213-  ID now at RHS.

    Private Sub cboSupplier_SelectedIndexChanged(ByVal sender As System.Object, _
                                                      ByVal e As System.EventArgs) _
                                                       Handles cboSupplier.SelectedIndexChanged
        Dim s1 As String
        Dim iPos As Integer

        If mbItemIsLoading Then Exit Sub
        With cboSupplier
            If (.SelectedIndex >= 0) Then
                s1 = .SelectedItem
                '-- separate id/name
                iPos = InStrRev(s1, ";")
                If (iPos > 1) Then
                    txtSupplier_id.Text = Trim(Mid(s1, iPos + 1)) '= Trim(VB.Left(s1, iPos - 1))
                    txtSupplierName.Text = Trim(VB.Left(s1, iPos - 1)) '= Trim(Mid(s1, iPos + 1))
                    Call mbSetDataModified("txtSupplier_id;")
                End If '-ipos-
            End If  '--selected-
        End With
    End Sub  '--supplier-
    '= = = = = = = = = = = = = = = = = =

    Private Sub cboBrand_KeyPress(ByVal eventSender As System.Object, _
                       ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                         Handles cboBrand.KeyPress, cboSupplier.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim cbo1 As ComboBox = CType(eventSender, ComboBox)
        If (keyAscii = 13) AndAlso (cbo1.SelectedIndex >= 0) Then '--enter- and something selected.
            keyAscii = 0
            eventArgs.Handled = True
            SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub  '- cboBrand.KeyPress_KeyPress-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- S t o c k--
    '-- Check box changes--
    '-- Check box changes--

    Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles chkInactive.CheckedChanged
        If mbItemIsLoading Then Exit Sub
        '-  update BG text box.-
        txtInactive.Text = IIf(chkInactive.Checked, "1", "0")
        Call mbSetDataModified("txtInactive;")
    End Sub  '--chkInactive--
    '= = = = = = = = = = = = =

    Private Sub chkInactive_KeyPress(ByVal eventSender As System.Object, _
                              ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles chkInactive.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub  '-chInactive_KeyPress-
    '= = = = = = = = = = = = = = = == = = = = =
    '-===FF->

    '==3301.606=

    '-chkIsNonStockItem_CheckedChanged-
    Private Sub chkIsNonStockItem_CheckedChanged(sender As Object, ev As EventArgs) _
                                                  Handles chkIsNonStockItem.CheckedChanged
        If mbItemIsLoading Then Exit Sub
        If chkIsNonStockItem.Checked Then
            txtIsNonStockItem.Text = "1"
            chkTrackSerials.Checked = False   '--can't be serialized.
            txtTrackSerials.Text = "0"
            chkTrackSerials.Enabled = False
            Call mbSetDataModified("txtTrackSerials;")

        Else  '-normal-
            txtIsNonStockItem.Text = "0"
            '==  Target is new Build 4251..  Started in here 19-May-2020
            chkTrackSerials.Checked = True   '--CAN be serialized.
            '= txtTrackSerials.Text = "0"
            chkTrackSerials.Enabled = True
            Call mbSetDataModified("txtTrackSerials;")
        End If
        Call mbSetDataModified("txtIsNonStockItem;")

    End Sub  '-chkIsNonStockItem_CheckedChanged-
    '= = = = = = =  = = = = = = = = = == = = =  =

    Private Sub chkIsNonStockItem_KeyPress(ByVal eventSender As System.Object, _
                          ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles chkIsNonStockItem.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub  '-chInactive_KeyPress-
    '= = = = = = = = = = = = = = = == = = = = =
    '-===FF->

    Private Sub chkTrackSerials_CheckedChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) Handles chkTrackSerials.CheckedChanged
        If mbItemIsLoading Then Exit Sub
        '-  update BG text box.-
        txtTrackSerials.Text = IIf(chkTrackSerials.Checked, "1", "0")
        Call mbSetDataModified("txtTrackSerials;")
    End Sub  '--chkTrackSerials-
    '= = = =  = = = = = = = =  =

    Private Sub chkTrackSerials_KeyPress(ByVal eventSender As System.Object, _
                      ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles chkTrackSerials.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub  '-chTrackSerials_KeyPress-
    '= = = = = = = = = = = = = = = == = = = = =

    Private Sub chkRenaming_CheckedChanged(ByVal sender As System.Object, _
                                                 ByVal e As System.EventArgs) Handles chkRenaming.CheckedChanged
        If mbItemIsLoading Then Exit Sub
        '-  update BG text box.-
        txtRenaming.Text = IIf(chkRenaming.Checked, "1", "0")
        Call mbSetDataModified("txtRenaming;")

    End Sub  '--chkRenaming-
    '= = = = = = = = = = = = =

    Private Sub chkRenaming_KeyPress(ByVal eventSender As System.Object, _
                  ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles chkRenaming.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub  '-chRenaming_KeyPress-
    '= = = = = = = = = = = = = = = == = = = = =
    '-===FF->

    '-- S t o c k--

    '-- EDITABLE Text box changes--
    '-- EDITABLE Text box changes--

    '-- barcode only for NEW..--
    '-- barcode only for NEW..--

    '--chkAutoBarcode_CheckedChanged-

    Private Sub chkAutoBarcode_CheckedChanged(sender As Object, _
                                              ev As EventArgs) Handles chkAutoBarcode.CheckedChanged
        If chkAutoBarcode.Checked Then
            txtBarcode.Text = ""
            txtBarcode.Enabled = False
        Else
            txtBarcode.Enabled = True
        End If

    End Sub  '--chkAutoBarcode_CheckedChanged-
    '= = = = = = = = = = = = = = = = = = = = =

    Private Sub chkAutoBarcode_KeyPress(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles chkAutoBarcode.KeyPress

        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            If chkAutoBarcode.Checked Then
                txtReOrderLevel.Select()
            Else
                txtBarcode.Select()
            End If
        End If  '-13-

    End Sub  '-chkAutoBarcode_KeyPress-
    '= = = = = = = = = =  = = = = = = = = =


    '-- barcode only for NEW..--

    Private Sub txtBarcode_TextChanged(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles txtBarcode.TextChanged
        If mbItemIsLoading Then Exit Sub
        Call mbSetDataModified("txtBarcode;")
    End Sub  '--barcode-
    '= = = = = = = = = = =  = =

    '- ENTER key on barcode..-

    Private Sub txtBarcode_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                    Handles txtBarcode.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim sBarcode, sSql, sErrorMsg As String
        Dim datatable1 As DataTable

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            '--Just use the validating event...
            '-- go to next fld-
            '= controlParent.SelectNextControl(textbox1, True, True, True, True)

            SendKeys.Send("{TAB}")

            'sBarcode = Trim(txtBarcode.Text)
            'If mbIsNewItem AndAlso (sBarcode <> "") Then
            '    sSql = "SELECT * FROM stock WHERE (Barcode='" & sBarcode & "');"
            '    If gbGetDataTable(mCnnSql, datatable1, sSql) AndAlso _
            '                 (Not (datatable1 Is Nothing)) Then
            '        If (datatable1.Rows.Count > 0) Then
            '            MsgBox("There is already a stock item for barcode " & sBarcode & vbCrLf & _
            '            "Stock_id is " & datatable1.Rows(0).Item("stock_id") & ".." & vbCrLf & _
            '                      "Duplicates are not allowed." & vbCrLf, MsgBoxStyle.Exclamation)
            '            '== eventArgs.Cancel = True
            '        Else
            '            '--ok-
            '            cboCat1.Select()
            '            iKeyAscii = 0 '--processed--
            '        End If '--has rows-
            '    Else
            '        sErrorMsg = gsGetLastSqlErrorMessage()
            '        MsgBox("Error in reading Stock datatable for barcode: " & sBarcode & _
            '                  vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
            '    End If '--get table-
            'End If  '-new-
            iKeyAscii = 0 '--processed--
            eventArgs.KeyChar = Chr(iKeyAscii)
            If iKeyAscii = 0 Then
                eventArgs.Handled = True
            End If
        End If  '--Enter key-
    End Sub  '- txtBarcode_KeyPress--
    '= = =  = = = =  = = = = = = =
    '-===FF->

    '- check for duplicates-

    Private Sub txtBarcode_Validating(ByVal sender As System.Object, _
                                         ByVal ev As System.ComponentModel.CancelEventArgs) _
                                                Handles txtBarcode.Validating
        Dim sBarcode, sSql, sErrorMsg As String
        Dim datatable1 As DataTable

        sBarcode = Trim(txtBarcode.Text)

        If mbIsNewItem AndAlso (sBarcode <> "") Then
            sSql = "SELECT * FROM stock WHERE (Barcode='" & sBarcode & "');"
            If gbGetDataTable(mCnnSql, datatable1, sSql) AndAlso _
                         (Not (datatable1 Is Nothing)) Then
                If (datatable1.Rows.Count > 0) Then
                    MsgBox("There is already a stock item for barcode " & sBarcode & vbCrLf & _
                    "Stock_id is " & datatable1.Rows(0).Item("stock_id") & ".." & vbCrLf & _
                              "Duplicates are not allowed." & vbCrLf, MsgBoxStyle.Exclamation)
                    ev.Cancel = True
                Else
                    '--ok-
                End If '--has rows-
            Else
                sErrorMsg = gsGetLastSqlErrorMessage()
                MsgBox("Error in reading Stock datatable for barcode: " & sBarcode & _
                          vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
            End If '--get table-
        ElseIf mbIsNewItem AndAlso (sBarcode = "") AndAlso (Not chkAutoBarcode.Checked) Then
            MsgBox("Please enter a stock barcode.." & vbCrLf & _
                        "   NB: Duplicates are not allowed." & vbCrLf, MsgBoxStyle.Exclamation)
            ev.Cancel = True

        End If  '-new-
    End Sub  '--barcode-
    '= = = = = = = = = = =  = =
    '-===FF->

    '-Catch ReOrder flds..

    Private Sub txtReOrderLevel_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles txtReOrderLevel.TextChanged
        If mbItemIsLoading Then Exit Sub
        Call mbSetDataModified("txtReOrderLevel;")

    End Sub '-txtReOrderLevel-
    '= = = = = =  ==  ==  == = =

    Private Sub txtReOrderQty_TextChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles txtReOrderQty.TextChanged
        If mbItemIsLoading Then Exit Sub
        Call mbSetDataModified("txtReOrderQty;")

    End Sub '-txtReOrderQty-
    '= = = =  = = = ==  = ==  == ==  =

    '-Catch ENTER on ReOrder flds..

    Private Sub txtReOrderFlds_KeyPress(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                         Handles txtReOrderLevel.KeyPress, txtReOrderQty.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txtData As TextBox = CType(eventSender, System.Windows.Forms.TextBox)

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            SendKeys.Send("{TAB}")
            iKeyAscii = 0
            eventArgs.Handled = True

        End If '- Enter key-
    End Sub  '-reorder Enter-
    '= = =  = = = == = = = = == 

    '==FIXED 3519.0227-  Was leaving a blank fld and causing SQL error..

    Private Sub txtReOrder_validating(ByVal sender As System.Object, _
                                             ByVal ev As System.ComponentModel.CancelEventArgs) _
                                                 Handles txtReOrderLevel.Validating, txtReOrderQty.Validating
        Dim txtData As TextBox = CType(sender, System.Windows.Forms.TextBox)
        Dim errorMsg As String = ""

        'If (Trim(txtData.Text) <> "") Then
        If (Trim(txtData.Text) = "") OrElse (Not mbIsNumeric(txtData.Text)) Then
            errorMsg = "Please Note- This Field is needed, and is for NUMERIC data only."
        Else '-ok-
            txtData.Text = CStr(CInt(txtData.Text)) '--drop any fracrions..-
        End If '-numeric-
        '=End If '-empty-
        If errorMsg <> "" Then  '--error
            ev.Cancel = True
            MsgBox(errorMsg, MsgBoxStyle.Exclamation)
        End If
    End Sub  '--txtReOrder_validating-
    '= = = = = = = =  ==  = = = ==  =
    '-===FF->

    Private Sub txtDescription_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles txtDescription.TextChanged
        If mbItemIsLoading Then Exit Sub
        Call mbSetDataModified("txtDescription;")
    End Sub '-txtDescription-
    '= = = = = = = = = = = = ==

    '-Catch ENTER on Description flds..

    Private Sub txtDescription_KeyPress(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                         Handles txtDescription.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txtData As TextBox = CType(eventSender, System.Windows.Forms.TextBox)

        If (iKeyAscii = System.Windows.Forms.Keys.Return) AndAlso (Trim(txtData.Text) <> "") Then
            SendKeys.Send("{TAB}")
            iKeyAscii = 0
            eventArgs.Handled = True
        End If '- Enter key-
    End Sub  '-reorder Enter-
    '= = =  = = = == = = = = == 
    '-===FF->

    '--txtSupplierCode--
    '-- NB-  SupplierCode is not part of Stock Table,
    '--  So we don't put it in msModifiedFields.
    '-  WE update SupplierCode Table extras as part of Committing Transaction. 

    Private Sub txtSupplierCode_TextChanged(eventSender As Object, _
                                             EventArgs As EventArgs) Handles txtSupplierCode.TextChanged
        If mbItemIsLoading Then Exit Sub
        '=Dim txtData As TextBox = CType(eventSender, System.Windows.Forms.TextBox)

        mbSupplierCodeModified = True
    End Sub '-txtSupplierCode_TextChanged-
    '= = = = = = = = = = = = = = = = = = =

    Private Sub txtSupplierCode_KeyPress(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                               Handles txtSupplierCode.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txtData As TextBox = CType(eventSender, System.Windows.Forms.TextBox)

        If (iKeyAscii = System.Windows.Forms.Keys.Return) AndAlso (Trim(txtData.Text) <> "") Then
            SendKeys.Send("{TAB}")
            iKeyAscii = 0
            eventArgs.Handled = True
        End If '- Enter key-

    End Sub  '-txtSupplierCode_KeyPress-
    '= = = = =  = = = = = = = === = = =  =

    '--validate SupplierCode.

    Private Sub txtSupplierCode_Validating(ByVal sender As System.Object, _
                                              ByVal ev As System.ComponentModel.CancelEventArgs) _
                                             Handles txtSupplierCode.Validating
        '-check on txtSupplier_id.Text-
        Dim sText As String = Trim(txtSupplier_id.Text)
        If (sText = "") OrElse ((Not mbIsNumeric(sText)) OrElse (CInt(sText) <= 0)) Then
            '= ev.Cancel = True
            MsgBox("Supplier Id is missing or invalid..", MsgBoxStyle.Exclamation)
        End If

    End Sub  '-txtSupplierCode_Validating-
    '= = = = = = = = = = = = = = = = = == 
    '-===FF->

    '-txtCostExTax-
    Private Sub txtCostExTax_TextChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles txtCostExTax.TextChanged
        If mbItemIsLoading Then Exit Sub
        '==If IsNumeric(txtCostExTax.Text) Then
        Call mbSetDataModified("txtCostExTax;")
        '==End If
    End Sub  '-txtCostExTax-
    '= = = = = = = = = = = = =

    Private Sub chkGoodsTax_CheckedChanged(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles chkGoodsTax.CheckedChanged
        If mbItemIsLoading Then Exit Sub
        '-  update text box.-
        txtGoodsTaxcode.Text = IIf(chkGoodsTax.Checked, "GST", "NONE")
        Call mbSetDataModified("txtGoodsTaxcode;")

    End Sub  '-chkGoodsTax-
    '= = = = = = = = = = = = =

    Private Sub txtSellExTax_TextChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles txtSellExTax.TextChanged
        If mbItemIsLoading Then Exit Sub
        Call mbSetDataModified("txtSellExTax;")

    End Sub  '--txtSellExTax-
    '= = = = = = = = = = = = = = 

    Private Sub chkSalesTax_CheckedChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles chkSalesTax.CheckedChanged
        If mbItemIsLoading Then Exit Sub
        '-  update text box.-
        txtSalesTaxcode.Text = IIf(chkSalesTax.Checked, "GST", "NONE")
        Call mbSetDataModified("txtSalesTaxcode;")

    End Sub  '--chkSalesTax-
    '= = = = = = = = = = = = = =

    Private Sub chkGoodsTax_KeyPress(ByVal eventSender As System.Object, _
                          ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                           Handles chkGoodsTax.KeyPress, chkSalesTax.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub  '-chInactive_KeyPress-
    '= = = = = = = = = = = = = = = == = = = = =

    '-Catch ENTER on price flds..

    Private Sub txtNumericPriceFlds_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                    Handles txtCostExTax.KeyPress, txtCostIncTax.KeyPress, _
                                                txtSellExTax.KeyPress, txtSellIncTax.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txtData As TextBox = CType(eventSender, System.Windows.Forms.TextBox)

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            SendKeys.Send("{TAB}")
            iKeyAscii = 0
            eventArgs.Handled = True
        End If '- Enter key-
    End Sub  '--txtNumericPriceFlds_KeyPress-
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- txtCostExTax    VALIDATE numeric--
    '-- txtSellExTax    VALIDATE numeric--

    Private Sub txtNumericPriceFlds_validating(ByVal sender As System.Object, _
                                              ByVal ev As System.ComponentModel.CancelEventArgs) _
                                                  Handles txtCostExTax.Validating, txtCostIncTax.Validating, _
                                                            txtSellExTax.Validating, txtSellIncTax.Validating
        Dim txtData As TextBox = CType(sender, System.Windows.Forms.TextBox)
        Dim errorMsg As String = ""
        Dim decCost, decCostInc As Decimal
        Dim decSell, decSellInc As Decimal

        If (Trim(txtData.Text) <> "") Then
            '==If gbIsNumericType(sSqlType) Then
            If (Not IsNumeric(txtData.Text)) Then
                errorMsg = "Field is for NUMERIC data only."
            Else
                txtData.Text = FormatCurrency(CDec(txtData.Text), 2)
                If (LCase(txtData.Name) = "txtcostextax") Or (LCase(txtData.Name) = "txtcostinctax") Then
                    '--compute sell price..-
                    If (LCase(txtData.Name) = "txtcostextax") Then  '--compute cost inc. price..-
                        decCost = CDec(txtCostExTax.Text)
                        decCostInc = decCost + (decCost * mDecGSTPercentage / 100)
                        txtCostIncTax.Text = FormatCurrency(decCostInc, 2)
                    Else  '--cost inc was entered..  back-compute the cost-ex.
                        decCostInc = CDec(txtCostIncTax.Text)
                        If decCostInc = 0 Then
                            decCost = 0
                        Else
                            decCost = mDecComputeAmountExTax(decCostInc)
                        End If
                        txtCostExTax.Text = FormatCurrency(decCost, 2)
                    End If ' ==extax-
                    '--compute/show selling price..-
                    decSell = decCost + ((decCost * mDecSellMargin) / 100)
                    decSellInc = decSell + (decSell * mDecGSTPercentage / 100)
                    txtSellExTax.Text = FormatCurrency(decSell, 2)
                    txtSellIncTax.Text = FormatCurrency(decSellInc, 2)
                Else  '--sell price was entered-
                    '--compute ex/inc tax as needed.
                    If (LCase(txtData.Name) = "txtsellextax") Then  '--compute sell inc. price..-
                        decSell = CDec(txtSellExTax.Text)
                        decSellInc = decSell + (decSell * mDecGSTPercentage / 100)
                        '=3519.0214= ROUNDING..  Sell_inc IS FOR DISPLAY ONLY..
                        '--   MUST round to 2 decimals first.
                        decSellInc = Decimal.Round(decSellInc, 2)
                        decSellInc += mDecGetRoundingAmount(decSellInc)
                        txtSellIncTax.Text = FormatCurrency(decSellInc, 2)
                    Else '--sell inc was entered.  so back-compute the sell-ex.
                        decSellInc = CDec(txtSellIncTax.Text)
                        If decSellInc = 0 Then
                            decSell = 0
                        Else '-ok-
                            decSell = mDecComputeAmountExTax(decSellInc)
                        End If
                        txtSellExTax.Text = FormatCurrency(decSell, 2)
                    End If '-sell-
                End If  '--cost-
            End If  '-numeric-
            '==End If
        End If  '--empty-
        If errorMsg <> "" Then  '--error
            ev.Cancel = True
            MsgBox(errorMsg, MsgBoxStyle.Exclamation)
        End If
    End Sub '-validating-
    '= = = = = = = = = = = = == 
    '-===FF->


    Private Sub txtComments_TextChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles txtComments.TextChanged
        If mbItemIsLoading Then Exit Sub
        Call mbSetDataModified("txtComments;")

    End Sub '--txtComments--
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-stock commit-
    '-stock commit-

    Private Sub btnStockCommit_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles btnStockCommit.Click
        Dim sControlName, sColumnName As String
        Dim txtData As TextBox
        Dim sFldList As String = ""
        Dim sValueList As String = ""
        Dim sUpdate As String = ""
        Dim sSqlDataType, sFldData, sSql, sErrorMsg As String
        Dim ix, intAffected, intID, intStock_id As Integer
        Dim imageParameters1() = New OleDbParameter() {}  '--instantiates zero-length 1-dim array.--
        Dim parameter1 As OleDbParameter
        Dim cmd1 As OleDbCommand
        Dim sqlTransaction1 As OleDbTransaction
        Dim sSupCode As String = Trim(txtSupplierCode.Text)
        '==  Target is new Build 4251..  Started in here 19-May-2020
        Dim sSupcodeSql As String = ""

        If mbIsNewItem Then
            '-- Build sql INSERT field list for new item..-
            '-- Iterate through all textbox controls in edit panel, and match against list.

            '==  Target is new Build 4251..  Started in here 19-May-2020
            Dim intNewStock_id, intThisSupplier_id As Integer
            Dim sSupplierId As String = Trim(txtSupplier_id.Text)

            '==  Target is new Build 4251..  Started in here 19-May-2020
            If (sSupplierId = "") OrElse ((Not mbIsNumeric(sSupplierId)) OrElse (CInt(sSupplierId) <= 0)) Then
                '= sqlTransaction1.Rollback()
                MsgBox("Commit Stock- Supplier Id is missing or invalid..", MsgBoxStyle.Exclamation)
                Exit Sub
            Else  '-ok-
                intThisSupplier_id = CInt(sSupplierId)
            End If

            For Each control1 As Control In panelStockItem.Controls
                sControlName = LCase(control1.Name)
                '-- get new data for update-
                sColumnName = control1.Tag
                If (sColumnName <> "") AndAlso mColColumnDataTypes.Contains(sColumnName) Then
                    sSqlDataType = UCase(mColColumnDataTypes(sColumnName))
                    If (UCase(sSqlDataType) = "IMAGE") Or _
                                   (UCase(sSqlDataType) = "BINARY") Or (UCase(sSqlDataType) = "VARBINARY") Then
                        '--IMAGE column- can't use strings.--
                        '--  make SQL cmd parameter..-
                        '-- BUILD SQL cmd parameter for image byte[]...
                        If Not mByteImage1 Is Nothing Then
                            If (sFldList <> "") Then
                                sFldList = sFldList & ", "
                                sValueList = sValueList & ", "
                            End If
                            sFldList = sFldList & sColumnName
                            sValueList = sValueList & " ? "
                            parameter1 = New OleDbParameter("@" & sColumnName, SqlDbType.VarBinary)
                            parameter1.Value = mByteImage1  '= mColRowImages(sFldName)
                            Dim k As Integer = imageParameters1.Length + 1
                            ReDim Preserve imageParameters1(k - 1)
                            imageParameters1(k - 1) = parameter1
                        End If  '--nothing=
                    Else '-not image--
                        '-- must be textbox-
                        txtData = CType(control1, TextBox)
                        If gbIsDate(sSqlDataType) Then
                            sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                        ElseIf gbIsText(sSqlDataType) Then
                            sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                        Else  '--numeric-
                            sFldData = Replace(txtData.Text, ",", "")
                        End If '--type-
                        '- pick up barcode later.  (might be Autogen)
                        '==  Target is new Build 4251..  Started in here 19-May-2020
                        '==
                        '-- Supplier Code NOT part of Stock Table..
                        If (txtData.Text <> "") AndAlso _
                                (LCase(sColumnName) <> "stock_id") AndAlso _
                                  (LCase(sColumnName) <> "suppliercode") AndAlso _
                                                  (LCase(sColumnName) <> "barcode") Then
                            '--NOT included if no text or IDENTITY col.-
                            '--don't include ident flds--
                            If sFldList <> "" Then
                                sFldList = sFldList + ", " : sValueList = sValueList + ", "
                            End If
                            sFldList = sFldList + sColumnName : sValueList = sValueList + sFldData
                        End If  '--empty-
                    End If '--image/not image-
                End If  '-contains.-
            Next '--control1--
            '-- testing--
            '-- ok.. now INSERT New Record-
            If (sFldList <> "") Then '--something--
                '= sSql = "INSERT INTO [Stock] (" + sFldList + ")  VALUES (" + sValueList + ");"
                '== MsgBox("SQL Insert cmd is : " & vbCrLf & sSql, MsgBoxStyle.Information)
                If chkAutoBarcode.Checked Then
                    '-- special path for Auto Barcode..
                    Dim sFields2, sValues2, sSqlInsert As String
                    '= Dim sqlTransaction1 As OleDbTransaction
                    Dim intCount As Integer = 5 '--retry times..-
                    Dim datatable1 As DataTable
                    Dim bCompletedOk As Boolean = False
                    Dim intRowCount As Integer
                    '= MsgBox("AutoGen barcode is still to come.", MsgBoxStyle.Information)
                    While (intCount > 0) And (Not bCompletedOk)
                        intCount -= 1
                        '== MsgBox("SQL Insert cmd is : " & vbCrLf & sSql, MsgBoxStyle.Information)
                        Try  '-auto-
                            '-- New idea.. get list of existing numeric barcodes.
                            '--  And pick the lowest missing one..
                            sqlTransaction1 = mCnnSql.BeginTransaction
                            '-- SELECT to Lock the Table with HINT..

                            '= /****** Script for SelectTopNRows command from SSMS  ******/
                            sSql = "SELECT  [stock_id], [barcode], CAST(barcode as int) AS intBarcode"
                            sSql &= "  FROM [dbo].[Stock]  WITH (SERIALIZABLE) "
                            sSql &= "   WHERE (LEN(barcode)<=6) and (isnumeric(barcode)=1)"
                            sSql &= "   ORDER BY intBarcode; "
                            '- go-
                            If Not gbGetDataTableEx(mCnnSql, datatable1, sSql, sqlTransaction1) Then
                                '- was rolled back-
                                MsgBox("Sql Error in Barcode SELECT Stock List: " & vbCrLf & _
                                       gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                                Exit While
                            End If '-get-
                            If (datatable1 Is Nothing) Then '= OrElse (datatable1.Rows.Count <= 0) Then
                                '- Must roll back-
                                sqlTransaction1.Rollback()
                                MsgBox("Error- No rows returned for Barcode SELECT Stock List ! ", MsgBoxStyle.Exclamation)
                                Exit While
                            End If  '-nothing-
                            intRowCount = datatable1.Rows.Count
                            intID = -1
                            '-- srch list of barcode integers for the first available (missing) no.
                            If (intRowCount = 0) Then
                                intID = 1   '--first barcode..
                            ElseIf (intRowCount <= 20) Then
                                '-just pick next one.
                                intID = datatable1.Rows(intRowCount - 1).Item("intBarcode") + 1
                            Else  '-srch for for first gap.
                                For index As Integer = 0 To intRowCount - 2
                                    If (datatable1.Rows(index + 1).Item("intBarcode") <> _
                                                  datatable1.Rows(index).Item("intBarcode") + 1) Then
                                        '=Return sequence(index) + 1
                                        '- next in list is not next in sequence, so grab next in sequence.
                                        intID = datatable1.Rows(index).Item("intBarcode") + 1
                                        Exit For
                                    End If
                                Next index
                                If (intID < 0) Then  '-still not found..
                                    '-just pick next one.
                                    intID = datatable1.Rows(intRowCount - 1).Item("intBarcode") + 1
                                End If
                            End If  '-rowCount-
                            If (intID < 0) Then  '-still not found..
                                sqlTransaction1.Rollback()
                                MsgBox("Error- No Available Barcode no. could be found. ", MsgBoxStyle.Exclamation)
                                Exit While
                            End If
                            '- Now Have next avail no.
                            txtBarcode.Text = Trim(CStr(intID))
                            '-test-
                            '= MsgBox("Selected Barcode is: " & intID, MsgBoxStyle.Information)

                            '- Now to attempt INSERT-
                            sFields2 = sFldList & ", barcode "
                            sValues2 = sValueList & ", '" & txtBarcode.Text & "' "
                            '= sSqlInsert = "INSERT INTO [customer] (" & sFields2 + ")  VALUES (" + sValues2 + ");"
                            sSqlInsert = "INSERT INTO [Stock] (" & sFields2 & ")  VALUES (" & sValues2 & ");"
                            ''-- Now do the insert..
                            cmd1 = New OleDbCommand(sSqlInsert, mCnnSql, sqlTransaction1)
                            Try
                                cmd1.ExecuteNonQuery()
                                '-was good.. should commit now to bed in new ids.
                                '-- NOT YET !!
                                '==  Target is new Build 4251..  Started in here 19-May-2020
                                '==  Target is new Build 4251..  Started in here 19-May-2020
                                '==
                                '--  INSERT Supplier Code if any..  INTO SupplierCode Table..
                                '--  First Get Stock_id of new stock record..
                                Dim sSqlId As String
                                sSqlId = "SELECT CAST(SCOPE_IDENTITY () AS int);"
                                If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSqlId, True, sqlTransaction1, intID) Then
                                    intNewStock_id = intID
                                    '-- update display later..-
                                    mIntStock_id = intStock_id
                                    intStock_id = intNewStock_id
                                Else
                                    '-- rollback was done..
                                    MsgBox("Failed to retrieve latest Stock ID No..", MsgBoxStyle.Exclamation)
                                    Exit Sub
                                End If
                                '--get id. worked-
                                '-- Save SupplierCode..
                                If (sSupCode <> "") Then
                                    sSupcodeSql = "INSERT INTO [SupplierCode] (supcode, supplier_id, stock_id) " & _
                                                        " VALUES ('" & gsFixSqlStr(sSupCode) & "', " & _
                                                              CStr(intThisSupplier_id) & ", " & CStr(intNewStock_id) & ") " & vbCrLf
                                    '--update supcodes..
                                    If Not gbExecuteSql(mCnnSql, sSupcodeSql, True, sqlTransaction1, intAffected, sErrorMsg) Then
                                        '-- rollback was done.
                                        MsgBox("Saving SupplierCodes FAILED.." & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                                        Exit Sub
                                    End If  '--exec invoiceLine-
                                End If  '-supCode-
                                '--  Done new bit for new Build 4251..

                                '- OK. Now we can commit..
                                Try
                                    sqlTransaction1.Commit()
                                Catch ex As Exception
                                    MsgBox("Sql Error in COMMIT Auto Stock record.." & vbCrLf & _
                                             ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
                                    Exit Sub
                                End Try
                                '==  Target is new Build 4251..  Started in here 19-May-2020
                                '-- THIS was done above..
                                'sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
                                'If gbGetSqlScalarIntegerValue(mCnnSql, sSql, intID) Then
                                '    intStock_id = intID
                                '    mIntStock_id = intStock_id
                                'Else
                                '    '-- rollback was done..
                                '    MsgBox("Failed to retrieve latest Stock No..", MsgBoxStyle.Exclamation)
                                '    Exit While '= Sub
                                'End If
                                '--worked-
                                bCompletedOk = True
                                If mbAddNewStockOnly Then
                                    '= msSelectedStockBarcode = txtBarcode.Text
                                    mbIsCancelled = False
                                    '- now back to caller with new barcode. (stock_id)
                                    Me.Hide()
                                    Exit Sub
                                Else  '-stay with customer form.
                                    '=3404.731=-- show just new cust in browse..  selected-
                                    '= Call mbBrowseCustomerTable("(customer_id=" & CStr(intCustomer_id) & ")") '-sWhere-
                                    MsgBox("OK..  We Added new STOCK record..." & vbCrLf & _
                                              "Stock_id is: " & intStock_id & vbCrLf & _
                                              "Stock Barcode is: " & txtBarcode.Text & "..", MsgBoxStyle.Information)
                                End If
                            Catch ex As Exception
                                sqlTransaction1.Rollback()
                                MsgBox("Sql Error in INSERT Stock record: " & vbCrLf & _
                                              ex.Message & vbCrLf & vbCrLf & _
                                              "NB. AutoGen barcode must be UNIQUE and may have clashed.." & vbCrLf & _
                                                "We will Retry the Commit to overcome the problem." & vbCrLf & _
                                                "SQL Command was: " & vbCrLf & _
                                                   sSqlInsert & vbCrLf, MsgBoxStyle.Exclamation)
                                Thread.Sleep(1000)  '-msecs
                            End Try '-execute-
                        Catch ex As Exception
                            sqlTransaction1.Rollback()
                            '-- Assume failed because Is was already in use as a barcode.
                            MsgBox("Error INSERTing Stock record: " & vbCrLf & _
                                          ex.Message & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
                            Exit While
                        End Try  '-auto-
                    End While '--completed-
                Else '--normal-  barcode supplied.
                    sFldList &= ", barcode "
                    '==
                    '==   Updated.- 3519.0211 11-Feb-2019= 
                    '==     -- Fix Stock Admin Alpha barcode INSERT error.-  
                    '--  barcode is alpha, and Must have quotes..
                    '== sValueList &= ", " & txtBarcode.Text

                    '==  Target is new Build 4251..  Started in here 19-May-2020
                    '==  Target is new Build 4251..  Started in here 19-May-2020
                    '==
                    '--  INSERT Supplier Code if any..  INTO SupplierCode Table..
                    '== SO WE MUST have it in a TRANSACTION.
                    sqlTransaction1 = mCnnSql.BeginTransaction

                    sValueList &= ", '" & txtBarcode.Text & "' "
                    sSql = "INSERT INTO [Stock] (" + sFldList + ")  VALUES (" + sValueList + ");"
                    Try
                        cmd1 = New OleDbCommand(sSql, mCnnSql, sqlTransaction1)
                        If (imageParameters1.Length > 0) Then
                            For ix = 0 To (imageParameters1.Length - 1)
                                cmd1.Parameters.Add(imageParameters1(ix))
                            Next
                        End If
                        cmd1.ExecuteNonQuery()
                        '-  Retrieve Stock_id:  (IDENTITY of Stock record written.)-
                        sSql = "SELECT CAST(IDENT_CURRENT ('dbo.Stock') AS int);"
                        If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                            intStock_id = intID
                            mIntStock_id = intStock_id
                            intNewStock_id = intID
                            '-- update invoice display later..-

                            '==  Target is new Build 4251..  Started in here 19-May-2020
                            '==
                            '--  INSERT Supplier Code if any..  INTO SupplierCode Table..
                            '-- Save SupplierCode..
                            If (sSupCode <> "") Then
                                sSupcodeSql = "INSERT INTO [SupplierCode] (supcode, supplier_id, stock_id) " & _
                                                    " VALUES ('" & gsFixSqlStr(sSupCode) & "', " & _
                                                          CStr(intThisSupplier_id) & ", " & CStr(intNewStock_id) & ") " & vbCrLf
                                '--update supcodes..
                                If Not gbExecuteSql(mCnnSql, sSupcodeSql, True, sqlTransaction1, intAffected, sErrorMsg) Then
                                    '-- rollback was done.
                                    MsgBox("Saving SupplierCodes FAILED.." & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                                    Exit Sub
                                End If  '--exec invoiceLine-
                            End If  '-supCode-
                            '- OK. Now we can commit..
                            Try
                                sqlTransaction1.Commit()
                            Catch ex As Exception
                                MsgBox("Sql Error in COMMIT new Stock record.." & vbCrLf & _
                                         ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
                                Exit Sub
                            End Try
                            '--  Done new bit for new Build 4251..

                            MsgBox("OK..  Added new Stock record..." & vbCrLf & _
                                      "Stock_id is " & intStock_id, MsgBoxStyle.Information)
                            If mbAddNewStockOnly Then
                                mbIsCancelled = False

                                '==  Target-New-Build-4253..
                                '==  Target-New-Build-4253..
                                '==
                                '==   6.  StockAdmin- 
                                '==   frmStock Host form not closing when new stock item added via call from GoodsReceived..
                                '==
                                msModifiedControls = ""  '-- bypass "Abandon changes ?"
                                Call close_me()
                                '= Me.Hide()
                                '== END Target-New-Build-4253..

                                Exit Sub
                            End If
                        Else
                            MsgBox("Failed to retrieve latest invoice No..", MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If
                    Catch ex As Exception
                        sqlTransaction1.Rollback()
                        MsgBox("Sql Error in INSERT Stock record: " & vbCrLf & "SQL Command was: " & _
                                      sSql & vbCrLf & ex.Message & vbCrLf & vbCrLf & _
                                      "Note that barcodes must be UNIQUE..", MsgBoxStyle.Exclamation)
                    End Try  '-normal process-
                End If  '-normal-  barcode supplied.
            End If  '-sFldList-

        Else '-NOT new item. editing-
            '-  build sql UPDATE SET list for modified fields.--
            '- "msModifiedControls" has names of modified controls..-
            '-- Iterate through all textbox controls in edit panel, and match against list.
            For Each control1 As Control In panelStockItem.Controls
                sControlName = LCase(control1.Name)
                If (InStr(LCase(msModifiedControls), sControlName & ";") > 0) Then  '-was modified-
                    '-- get new data for update-
                    sColumnName = control1.Tag
                    If (sColumnName <> "") AndAlso mColColumnDataTypes.Contains(sColumnName) Then
                        sSqlDataType = UCase(mColColumnDataTypes(sColumnName))
                        If (UCase(sSqlDataType) = "IMAGE") Or _
                                     (UCase(sSqlDataType) = "BINARY") Or (UCase(sSqlDataType) = "VARBINARY") Then
                            '--IMAGE column- can't use strings.--
                            '--  make SQL cmd parameter..-
                            If sUpdate <> "" Then
                                sUpdate = sUpdate & ", "
                            End If
                            '== sUpdate = sUpdate & sColumnName & "=@" & sColumnName  '==parameter ref..-
                            sUpdate = sUpdate & sColumnName & "= ?"  '=oleDB =parameter ref..-
                            '-- BUILD SQL cmd parameter for image byte[]...
                            If Not mByteImage1 Is Nothing Then
                                parameter1 = New OleDbParameter("@" & sColumnName, SqlDbType.VarBinary)
                                parameter1.Value = mByteImage1  '= mColRowImages(sFldName)
                                Dim k As Integer = imageParameters1.Length + 1
                                ReDim Preserve imageParameters1(k - 1)
                                imageParameters1(k - 1) = parameter1
                            End If  '--nothing=
                        Else  '--not IMAGE-
                            '-- must be textbox-
                            txtData = CType(control1, TextBox)
                            If gbIsDate(sSqlDataType) Then
                                sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                            ElseIf gbIsText(sSqlDataType) Then
                                sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                            Else  '--numeric-
                                sFldData = msFixSqlStr(Replace(txtData.Text, ",", "")) '-clear commas from num.fld.-
                            End If
                            If sUpdate <> "" Then
                                sUpdate = sUpdate & ", "
                            End If
                            sUpdate = sUpdate + sColumnName + "=" + sFldData
                        End If  '--datatype-
                    End If  '--contains-
                End If  '--was modified-
            Next control1

            '-- testing--
            '== MsgBox("SQL Update list is : " & vbCrLf & sUpdate, MsgBoxStyle.Information)
            '--DO update-
            If (Len(sUpdate) > 0) Or (imageParameters1.Length > 0) _
                                             Or mbSupplierCodeModified Then '--some to do--
                '==  Target is new Build 4251..  Started in here 19-May-2020
                '==  Target is new Build 4251..  Started in here 19-May-2020
                '==
                '--  INSERT or UPDATE Supplier Code if any..  INTO SupplierCode Table..
                '== SO WE MUST have it in a TRANSACTION.
                sqlTransaction1 = mCnnSql.BeginTransaction

                sUpdate = "UPDATE [Stock]  SET " & sUpdate & _
                             " WHERE (stock_id=" & txtStock_id.Text & ");"
                '--  EXECUTE--
                Try
                    cmd1 = New OleDbCommand(sUpdate, mCnnSql, sqlTransaction1)
                    If (imageParameters1.Length > 0) Then
                        For ix = 0 To (imageParameters1.Length - 1)
                            cmd1.Parameters.Add(imageParameters1(ix))
                        Next
                    End If
                    intAffected = cmd1.ExecuteNonQuery()
                    '-ok-
                    '--  INSERT or UPDATE Supplier Code if any..  INTO SupplierCode Table..
                    If mbSupplierCodeModified And (sSupCode <> "") Then
                        '-change or update.
                        sSupcodeSql = "IF EXISTS (SELECT * FROM [SupplierCode] " & _
                                          "WHERE (supplier_id=" & txtSupplier_id.Text & ") AND " & _
                                                                      "(stock_id=" & txtStock_id.Text & ") )" & vbCrLf
                        sSupcodeSql &= " UPDATE [SupplierCode] SET " & _
                                                 " supcode='" & sSupCode & "', " & vbCrLf & _
                                                 " date_modified= CURRENT_TIMESTAMP " & vbCrLf & _
                                      "     WHERE (supplier_id=" & txtSupplier_id.Text & ") AND " & _
                                                                  "(stock_id=" & txtStock_id.Text & ")" & vbCrLf
                        sSupcodeSql &= "ELSE INSERT INTO [SupplierCode] (supcode, supplier_id, stock_id) " & _
                                            " VALUES ('" & sSupCode & "', " & txtSupplier_id.Text & ", " & txtStock_id.Text & ") " & vbCrLf
                        '--update supcodes..
                        If Not gbExecuteSql(mCnnSql, sSupcodeSql, True, sqlTransaction1, intAffected, sErrorMsg) Then
                            '-- rollback was done.
                            MsgBox("Saving SupplierCodes FAILED.." & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If  '--exec invoiceLine-
                    End If  '-modified..
                    '- now we can commit..
                    Try
                        sqlTransaction1.Commit()
                    Catch ex As Exception
                        MsgBox("Sql Error in COMMIT UPDATING Stock record.." & vbCrLf & _
                                 ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
                        Exit Sub
                    End Try
                    '--  Done new bit for new Build 4251..

                    msModifiedControls = ""  '=mbModified = False
                    MsgBox("Update completed.." & vbCrLf & _
                                 "(" & intAffected & " row(s) were affected..)", MsgBoxStyle.Information)
                Catch ex As Exception
                    sqlTransaction1.Rollback()
                    MsgBox("Sql Error in UPDATE record: " & vbCrLf & "SQL: " & _
                                sUpdate & vbCrLf + ex.Message & vbCrLf & _
                                "Rollback was executed..", MsgBoxStyle.Exclamation)
                End Try
            Else  '--no change--
                '==mbUpdateRecord = True
            End If  '--update--
        End If  '--new/edit--
        labItemAction.Text = "Commit Done.."
        panelStockItem.Enabled = False
        btnNew.Enabled = True
        btnEdit.Enabled = False
        btnStockCancel.Enabled = False

        '- refresh all stock column combos..-
        Call mbRefreshFkeyCombos()

        '== listViewStock.Enabled = True
        frameBrowse.Enabled = True

        msModifiedControls = ""
        '= Call mbRefreshStockList()
        '=3519.0213-
        mIntStock_id = -1  '= Allow to be selected.
        Call mbBrowseStockTable()  '-refresh browse-

    End Sub  '-stock commit-
    '= = = = = = = = = = = = 
    '-===FF->

    '- stock cancel --
    '- stock cancel --
    '- stock cancel --

    Private Sub btnStockCancel_Click(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles btnStockCancel.Click

        If (msModifiedControls <> "") Then
            If (MsgBox("Discard changes to this stock item ?", _
                 MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
                '==intCancel = 1  '--can't close yet--'--was mistake..  keep going..
                Exit Sub
            End If
        End If

        If mbAddNewStockOnly Then  '--cancel new item..
            '==  Target is new Build 4251..  Started in here 19-May-2020
            '==
            Call close_me()
            'mbIsCancelled = True
            'Me.Hide()
            Exit Sub
        End If
        '-- wants reset--
        panelStockItem.Enabled = False
        btnNew.Enabled = True
        btnEdit.Enabled = False
        btnStockCancel.Enabled = False

        '--  CLEAR all fields !! -
        Call mbClearAllTextFields()
        labItemAction.Text = "Edit cancelled.."

        '== listViewStock.Enabled = True
        frameBrowse.Enabled = True

        msModifiedControls = ""
        '=Call mbRefreshStockList()
        Call mbBrowseStockTable()  '-refresh-

    End Sub  '-cancel-
    '= = = = = = = = = = = 
    '= = = = = = = = = = = = 
    '-===FF->

    '--close-

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        '= Me.Close()
        Call close_me()

    End Sub '--close-
    '= = = = = = = ==  

    '--close-me-

    Private Sub close_me()
        Dim bCancel As Boolean = False '= = EventArgs.Cancel
        '= Dim intMode As System.Windows.Forms.CloseReason = EventArgs.CloseReason

        If (msModifiedControls <> "") Then
            If (MsgBox("Abandon changes ?", _
                 MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
                bCancel = True   '--cant close yet--'--was mistake..  keep going..
                Exit Sub
            End If
        End If
        If bCancel Then Exit Sub '--keep alive.-

        '- inform parent.-
        '- Report to Parent..-

        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

        'If Not bCancel Then  '--exiting.
        '     If Not (Me.delReport Is Nothing) Then
        '        delReport.Invoke(Me.Name, "FormClosed", "")
        '    End If
        'End If  '-cancel-
        'Me.Dispose()

    End Sub '--close me-
    '= = = = = = == = = = =

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    'Private Sub frmChildStock_FormClosing(ByVal eventSender As System.Object, _
    '                                         ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
    '                                            Handles Me.FormClosing
    '    Dim intCancel As Boolean = eventArgs.Cancel
    '    Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

    '    '== Call gbLogMsg(gsRuntimeLogPath, "== JobMatixPOS Stock form is closing.." & vbCrLf & vbCrLf & _
    '    '==                                                  "= = = = = = = = = = = =" & vbCrLf & vbCrLf)
    '    Select Case intMode
    '        Case System.Windows.Forms.CloseReason.WindowsShutDown, _
    '                  System.Windows.Forms.CloseReason.TaskManagerClosing, _
    '                           System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
    '            intCancel = 0 '--let it go--
    '        Case System.Windows.Forms.CloseReason.UserClosing
    '            If (msModifiedControls <> "") Then
    '                If (MsgBox("Abandon changes ?", _
    '                     MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
    '                    intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '                    Exit Sub
    '                End If
    '            End If
    '            intCancel = 0 '--let it go---
    '            '==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '        Case Else
    '            intCancel = 0 '--let it go--
    '    End Select '--mode--
    '    eventArgs.Cancel = intCancel

    '    '- Report to Parent..-
    '    If Not intCancel Then  '--exiting.
    '        '- Create an instance of the delegate.  
    '        '= Dim msd As subReportDelegate = AddressOf c1.Sub1
    '        ' Call the method.  
    '        '= msd.Invoke(Me.Name, "FormClosing")
    '        '= if (this.InvokeDel != null)
    '        '= InvokeDel.Invoke(this.txtMsg.Text);
    '        If Not (Me.delReport Is Nothing) Then
    '            delReport.Invoke(Me.Name, "FormClosing", "")
    '        End If
    '    End If  '-cancel-

    'End Sub '--closing--
    ''= = = = = = =  = = = = = = = == 

    ''-FormClosed-

    'Private Sub frmChildStock_FormClosed(ByVal eventSender As System.Object, _
    '                                     ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) _
    '                                        Handles Me.FormClosed
    '    If Not (Me.delReport Is Nothing) Then
    '        delReport.Invoke(Me.Name, "FormClosed", "")
    '    End If

    'End Sub  '-FormClosed-
    '= = = = = = = = = ==  = 


End Class  '--ucChildStockAdmin-
'== end form==


' Implements the manual sorting of items by columns.
'==dgv= Class ListViewItemComparer2
'==dgv= Private col As Integer
'==dgv= Private order As SortOrder

'==dgv= Public Sub New()
'==dgv=     col = 0
'==dgv=     order = SortOrder.Ascending
'==dgv= End Sub

'==dgv= Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
'==dgv=     col = column
'==dgv=     Me.order = order
'==dgv= End Sub

'==dgv= Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
'==dgv= Implements System.Collections.IComparer.Compare
'==dgv= Dim returnVal As Integer = -1
'==dgv=     returnVal = [String].Compare(CType(x, _
'==dgv=                     ListViewItem).SubItems(col).Text, _
'==dgv=                     CType(y, ListViewItem).SubItems(col).Text)
'==dgv= ' Determine whether the sort order is descending.
'==dgv=     If order = SortOrder.Descending Then
'==dgv= ' Invert the value returned by String.Compare.
'==dgv=         returnVal *= -1
'==dgv=     End If
'==dgv=     Return returnVal
'==dgv= End Function
'==dgv= End Class

'== the end ==