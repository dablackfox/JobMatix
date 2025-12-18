Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports system.data.sqlclient
Imports system.data.OleDb
Imports System.Math
Imports System.ComponentModel
'= Imports System.Windows.media
Imports System.Collections.Generic
Imports System.Threading

Public Class ucChildGoodsRecvd

    '-- JobMatix POS3--

    '- Goods Received Form.--
    '==
    '==  grh =V3.0.3012.704== 04-Jul-2014=
    '==      POS Main form.. Called from POS Main Admin...
    '==
    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==  grh. JobMatix 3.1.3103.0112 ---  12-Jan-2015 ===
    '==   >>  Call Label printing after Commit.... 
    '==
    '==  grh. JobMatix 3.1.3103.0118 ---  18-Jan-2015 ===
    '==   >>  Add button to call Stock Form...... 
    '==
    '==  grh. JobMatix 3.1.3107.0813 ---  13-Aug-2015 ===
    '==   >>  Form now DOUBLES as PO abd Goods Recvd.. 
    '==   >>  PO printing done in here.. 
    '==
    '==  grh. JobMatix 3.1.3107.0923 ---  23-Sep-2015 ===
    '==   >>  Finalise PO abd Goods Recvd Relations.. 
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '==     v3.3.3301.8816..  16-August-2016= ===
    '==        >> Fixes to frmGoodsRecvd- make sure stockAdmin accessible for NEW stock items..
    '==        >> Add Inputs for colPrefsSuppliers and colPrefsStock-
    '==
    '==     3403.615- 15Jun2017-
    '==      --  Goods Received..  Fixes to Tabbing.. 
    '==               PLUS SubClass DataGridView (clsDgvGoods) to capture enter key..
    '==               PLUS Lookup Form to see past GR transactions.
    '==
    '==-- (3411.0417 Was released to Precise..)
    '==-- (3411.0417 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - 
    '==
    '==   >> 3411.0420=  20-April-2018=
    '==     -- Re-interpreting Stock Columns for Purchase Orders.
    '==             1. "reOrderLevel" (from RM "order_threshold") Means Minimum to hold in stock..
    '==                 (Re-order when stock falls BELOW this level-)
    '==             2. "order_quqntity" (from RM "order_quantity") Means MAXIMUm qty to hold in stock..
    '==                 (Re-order sufficient to top up stock UP to this level-)
    '==       -- Finish off EMAILing Purchase Orders..
    '==
    '==
    '==    >> V3.5. 3501.0826- Called Stock (New) From Goods Rcvd.  
    '==         -- Add new Stock (Send barcode to Stock Form).
    '==         -- Startup checkbox Includes Tax as UNCHECKED.. 
    '==                  Then Disable when first barcode entered..
    '==         -- Fix updating of cost_ex or Cost_inc when other one is entered. 
    '==         -- ADD updating (on Commit) of cost_ex and sell_ex if they were changed when item entered.. 
    '==
    '==    >> V3.5. 3501.1029- 29-Oct-2018.  
    '==          --  dgvGoodsItems_CellValidated-  DROP trying to force current Cell onto Serials Column..
    '==                  as it crashes from being conflicted.
    '= = = = = =
    '==
    '==    >> V3.5. 3501.1104- 04-Nov.-2018.  
    '==     -- Goods Received. Use latest frmBrowse33 (class frmBrowse) so we get Full text Search
    '==            and move Supplier Name to 1st Col. of colPrefs for Supplier....
    '==            and move Description to 1st Col. of colPrefs for Stock Lookup...
    '==
    '== -- Updated 3501.1107  07-Nov-2018=  
    '==     -- Goods Received.  Show SerialsInput form after Qty validated in Grid Line. (For Stewart)
    '==  Released to Precise..
    '==
    '== -- Updated 3501.1220  20-Dec-2018=  
    '==     -- Goods Received.  Fix to Commit GoodsReceivedLine..
    '==             (Was writing out total-ex value instead of total-inc value)
    '==
    '==  IN PRODUCTION- 07-Feb-2019--
    '==  IN PRODUCTION- 07-Feb-2019--
    '==
    '==   Updated.- 3519.0207 07-Feb-2019= 
    '==     -- Fixes to GoodsReceived to strip leading zeroes from scanned barcode id=f necessary.-
    '==
    '==
    '==   Updated.- 3519.0214 12/14-Feb-2019= 
    '==     -- Fixes to Stock Admin for Grid browsing problems 
    '==              (Force a delay after selection changed before showing details.)..-
    '==     -- Updates to GoodsReceived to add Sell_inc editable column. 
    '==
    '==   Updated.- 3519.0224  Started 22-Feb-2019= 
    '==     -- Update to GoodsReceived to disable Grid while pre-loading PO details..... 
    '==
    '==
    '==   Updated.- 3519.0227  Started 26-Feb-2019= 
    '==     -- Update to GoodsReceived- Recalculate Extension when Cost validated, as well as when Qty is.
    '==                        AND ALSO re-calculate Sell_inc from Cost_inc +Margin, then re-cal. Sell_ex.... 
    '==
    '==
    '==   Updated.- 3519.0303  Started 02-March-2019= 
    '==     -- GoodsReceived- Add ContextMenu on Grid to Copy Barcode to ClipBoard....
    '==
    '==
    '==   Updated.-grh 3519.0311  Started 08-March-2019= 
    '==      >> Supplier Barcode now Auto-generated !!! (Updates to frmEdit)
    '==      >> GoodsReceived/PurchaseOrder-  Catch F5 on supplierBarcode for new Supplier.
    '==
    '==
    '==   Updated.-grh 3519.0317  Started 15-March-2019= 
    '==      >> Purchase Order Printing- Email text needs to have Our OrderNo (Suffix also.)
    '==
    '==   Updated.-grh 3519.0404  Started 02-Apri-2019= 
    '==      >>  Editing grid line. CHECK if Cost changed before updating Sell..
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
    '== NEW STUFF-
    '==    -- 4201.0519.  Purchase Orders to have txtStockItemSupplierCode textbox, 
    '==          and update of SupplierCode Table for New SupplierCode.....
    '==
    '==    -- 4201.0717.  17-July-2019-
    '==       -- SupplierCode Table-  DROP Primary Key (not needed), expand supCode col. to 40 chars..
    '==       -- PurchaseOrderLine Table-  Expand supplierCode col. to 40 chars..
    '== = = = =
    '==
    '== NEW revision-
    '==
    '==    -- 4201.0929.  Started 19-September-2019-
    '==        -- Fix PurchaseOrders printing- Capturing PDF for Email has corruoted PDF....
    '==                 (Hint- Must Wait for print Completion as per Print Sales Invoice..)
    '==
    '== NEW revision-
    '==    -- 4201.1013.  13-October-2019-  UPDATED TAB INDEXES..
    '==           ALSO-dtPickerInvoiceDate-
    '==            -- catch TAB key and re-check for Supplier Invoice No.
    '==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
    '==    
    '==   == 4221.0206.  06-Feb-2020- 
    '==
    '==-- 1.  IN GOODS REceived--  Printing PO..
    '--           set CORRECT printer selected..--
    '--      FIX to 4219.1216 !!! --   mPrintDocument1.PrinterSettings.PrinterName = msInvoicePrinterName
    '--      FIX to 4219.1216 !!! --   mPrintDocument1.PrinterSettings.PrinterName = msInvoicePrinterName
    '--      FIX to 4219.1216 !!! --   mPrintDocument1.PrinterSettings.PrinterName = msInvoicePrinterName
    '==
    '==    -- FIX to 4219.1216 !!! --
    '==      THIS is CORRECT= mPrintDocument1.PrinterSettings.PrinterName = sPrinterName '= '-- FIX to 4219.1216 !!! - msInvoicePrinterName
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '==
    '==  Target is new Build 4253..
    '==  Target is new Build 4253..
    '==
    '==  Target-New-Build-4253..
    '==  Target-New-Build-4253..
    '==
    '==   A. -- Purchase Orders-  NewSpecialOrder..  DO NOT adjust order Qty for qtyInStock-  Use DB order_quantity..
    '==
    '==   B. -- Purchase Orders-  NewSpecialOrder.. SupplierCode must be installed in Grid being auto-built.
    '==   
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '==
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==
    '== --  Purchase Orders- Auto  ordering..  
    '==       (Martin 22/9/2020.)  Qty to order to only be enough to make stock back up to Max level.. ?  
    '==       ie Max (DB order_quantity)..  Same as what MYOB does.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '== UPDATES to Build 4284.1124  
    '== UPDATES to Build 4284.1124  
    '== UPDATES to Build 4284.1124  
    '==
    '==   Target-New-Build-4287 --  (30-Jan-2021)
    '==
    '==  Goods Received Serials entry...  (Stewart 27-Jan-2021-)
    '==    The system won't let you put in a Serial that's already in the system, 
    '==    Or that you've already entered for that Invoice line.. 
    '==    But it will Let you enter a Serial that was entered In a previous line In that invoice.. 
    '==      (Does this mean you are trying To enter the same product twice In the one invoice ?)
    '== For the next release-  make sure that serial no's are unique over the whole Invoice, 
    '==    And still also not on file already in the system. 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==

    '==Public Const K_SAVESETTINGSPATH As String = "localPOSSettings.txt"
    Private Const k_invoicePrtSettingKey As String = "POS_INVOICEPRINTER"


    '-- GoodsReceived DataGridView columns.--
    Private Const k_GRIDCOL_BARCODE As Short = 0
    Private Const k_GRIDCOL_SUP_CODE As Short = 1
    Private Const k_GRIDCOL_CAT1 As Short = 2
    Private Const k_GRIDCOL_CAT2 As Short = 3
    Private Const k_GRIDCOL_DESCRIPTION As Short = 4
    Private Const k_GRIDCOL_COST_EX As Short = 5       '-- HIDDEN column.--
    Private Const k_GRIDCOL_TAX_CODE As Short = 6
    Private Const k_GRIDCOL_COST_INC As Short = 7
    Private Const k_GRIDCOL_SELL_EX As Short = 8
    Private Const k_GRIDCOL_SALES_TAX_CODE As Short = 9
    Private Const k_GRIDCOL_SELL_INC As Short = 10
    Private Const k_GRIDCOL_QTY As Short = 11
    Private Const k_GRIDCOL_EXTENSION As Short = 12  '--EX TAX--
    Private Const k_GRIDCOL_SERIALNOSREQUIRED As Short = 13      '--text to Show SerialNo form..-
    Private Const k_GRIDCOL_SERIALNOLIST As Short = 14     '-- SerialNos separated by vbcrlf..--
    Private Const k_GRIDCOL_STOCK_ID As Short = 15         '-- HIDDEN column.--
    Private Const k_GRIDCOL_TRACK_SERIAL As Short = 16     '-- HIDDEN column.--
    Private Const k_GRIDCOL_COST_TAX As Short = 17                 '-- HIDDEN column.-- 
    Private Const k_GRIDCOL_COST_TAX_EXTENDED As Short = 18         '-- HIDDEN column.--
    Private Const k_GRIDCOL_PO_LINE_ID As Short = 19         '-- PO line id if any..--
    Private Const k_GRIDCOL_ORIGINAL_COST_EX As Short = 20         '-- IN case Cost_ex was changed..--
    Private Const k_GRIDCOL_ORIGINAL_COST_INC As Short = 21         '--IN CASE Cost_ex was changed..--
    '-new_suppliercode-
    Private Const k_GRIDCOL_NEW_SUPPLIERCODE As Short = 22         '--IN CASE SupCoode new or changed..--
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =

    Private mbActivated As Boolean = False
    '- - - - -
    Private mbIsInitialising As Boolean = True
    Private mbActive As Boolean = False
    Private mbStartingUp As Boolean
    Private mbIsCancelling As Boolean = False

    Private mIntFormDesignHeight As Integer '-- starting dimensions..-
    Private mIntFormDesignWidth As Integer '-- starting dimensions..-

    Private mbIsPurchaseOrder As Boolean = False
    Private mbIsNewPO As Boolean = False

    Private mbMainLoadDone As Boolean = False

    '=3301.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private msServer As String = ""
    '-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""
    Private msSqlVersion As String = ""
  
    '== Private msDataSourceName As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private mbIsSqlAdmin As Boolean = False

    Private msVersionPOS As String = ""

    '=3301.816-
    Private mColPrefColumnsSuppliers As Collection
    Private mColPrefColumnsStock As Collection

    Private msComputerName As String '--local machine--
    Private msAppPath, msAppFullName As String
    Private msPDF_FilePath As String

    Private msLastSqlErrorMessage As String = ""

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection  '= SqlConnection '--

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1
    Private mImageUserLogo As Image

    Private msCreatedByStaffName As String = ""
    Private mDatePO_date_created As Date

    '= Private msColourPrtName As String = ""
    '=  Private msReceiptPrtName As String = ""
    '=  Private msLabelPrtName As String = ""

    Private mIntForm_top As Integer = -1
    Private mIntForm_left As Integer = -1

    Private mDecSell_margin As Decimal = 40D  '--Is a Percentage- This is the DEFAULT.
    Private mDecGST_rate As Decimal = 10D    '--temp. get from setup.
    Private mFreightTaxCode As String = "GST"  '--temp. get from SystemInfo.

    '-- Business Info-
    '--  Business Info-
    Private msBusinessABN As String = ""
    Private msBusinessDisplayABN As String = ""

    '== Private msBusinessUser As String
    Private msJT2SecurityIdOriginal As String '--as stored in SytemInfo in Row "JT2SecurityId"..-
    Private msJT2SecurityId As String '-- AS computed from ABN DateCreated.  --
    Private msBusinessName As String
    Private msBusinessAddress1 As String
    Private msBusinessAddress2 As String
    Private msBusinessShortName As String
    Private msBusinessPhone As String
    Private msBusinessPostCode As String
    Private msBusinessState As String
    Private msBusinessDeliveryAddress As String = ""

    Private mColStockImages As Collection

    '- Supplier--
    Private msSupplierBarcode As String = ""
    Private msSupplierName As String = ""
    Private mIntSupplier_id As Integer = -1
    Private msSupplierEmailAddress As String = ""  '=3411.0421-

    Private mDataTableSupplier As DataTable

    Private mIntSupplierDeliveryDays As Integer = -1

    Private mIntOurOrder_id As Integer = -1

    '-- GoodsReceived - PO completion..-
    Private mDicPO_ItemsOutstanding As Dictionary(Of Integer, Integer)


    '-- Goods (Invoice) totals-
    Private mDecTotalItemsTax As Decimal = 0
    Private mDecSubTotal As Decimal
    Private mDecSubTotalEx As Decimal

    Private mDecFreightEx As Decimal
    Private mDecFreightTax As Decimal
    Private mDecFreightInc As Decimal

    Private mDecDiscount As Decimal
    Private mDecDiscountTax As Decimal

    Private mDecNettTax As Decimal = 0

    Private mDecInvoiceTotal As Decimal  '--total Debits.-
    Private mDecGoodsTotalExpected As Decimal  '--total from Supplier.-

    '-- Current item Line data.--
    '-- Current item Line data.--

    '== Private msItemBarcode As String
    Private mIntQtyInStock As Integer

    '== Private mTxtBarcode As TextBox  '--text box calling stock lookup.-
    '= Private mIntGridRow, mIntGridCol As Integer

    '== PO Printing..--
    '== PO Printing..--
    Private msDefaultPrinterName As String = ""

    Private msPdfPrinterName As String = ""
    Private msInvoicePrinterName As String = ""

    Private msEmailQueueSharePath As String = ""

    '--  Main printer object..-- 
    '--    DOES ALL PRINTING..--
    Private WithEvents mPrintDocument1 As New System.Drawing.Printing.PrintDocument()

    Private mbPrintingCompleted As Boolean = False
    Private mIntPageNo As Integer = 0
    Private msOurOrderNumberString As String = ""

    Private mIntNoItemsStillToPrint As Integer = 0
    Private mIntGridLinesPrintCount As Integer = 0
    Private mIntActuaItemCount As Integer = 0
    '= = = = = = = = = = = = = = = = = = = = = = = = 


    '=3519.0303-- Context menu to Copy Grid Barcode..

    '--  Popup menu for Right click on Items Grid...-
    '--  Popup menu for Right click on Items Grid...-
    Private mContextMenuItemInfo As ContextMenu

    Private WithEvents mnuCopyItemDescription As New MenuItem("Copy Item Description.")
    Private WithEvents mnuItemMenuSep1 As New MenuItem("-")
    Private WithEvents mnuCopyItemBarcode As New MenuItem("Copy Item Barcode.")
    Private WithEvents mnuItemMenuSep2 As New MenuItem("-")

    '= = = = = = = = = = = = = = = = = = = = = = = =

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '= = = = = = = = = = = = = = = = = = = = = = = = = 

    '-===FF->

    '-version-
    WriteOnly Property versionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
            '= labVersion.Text = msVersionPOS
        End Set
    End Property  '--version--
    '= = = = = = = = = = = = = = = = = = == 

    '-- Staff Name/Id now comes from caller..--

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

    WriteOnly Property SqlServerComputer() As String
        Set(ByVal Value As String)
            msSqlServerComputer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property connectionSql() As OleDbConnection  '== SqlConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
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
    End Property  '--dbname--
    '= = = = = = = = = = = = = = = = = = = = =

    '=3301.816-
    '--accept list of preferred col-names.---
    WriteOnly Property PreferredColumnsSuppliers() As Collection
        Set(ByVal Value As Collection)

            mColPrefColumnsSuppliers = Value
        End Set
    End Property '--prefs..-
    '= = = = =  =  = =  = =

    '--accept list of preferred col-names.---
    WriteOnly Property PreferredColumnsStock() As Collection
        Set(ByVal Value As Collection)

            mColPrefColumnsStock = Value
        End Set
    End Property '--prefs..-
    '= = = = =  =  = =  = =

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

    '-- set user logo for printing..--
    WriteOnly Property UserLogo() As Image
        Set(ByVal Value As Image)

            mImageUserLogo = Value
        End Set
    End Property '--logo..--
    '= = = = = = = = = = = = = = = = =

    WriteOnly Property IsPurchaseOrder As Boolean
        Set(value As Boolean)
            mbIsPurchaseOrder = value
        End Set
    End Property  '--PO-
    '= = = = = = = = = = = = = = = = = = = = = =
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
        DoEvents()
        '-- resize main box and top panel-

        grpBoxGoods.Height = (Me.Height - 16)
        grpBoxGoods.Width = (Me.Width - 13)

        clsDgvGoodsItems.Width = grpBoxGoods.Width - 5 '= panelGoodsHdr.Width
        clsDgvGoodsItems.Height = _
                  (grpBoxGoods.Height - panelGoodsHdr.Height _
                       - panelStockLineEntry.Height - panelGoodsFooter.Height - 15)
        panelStockLineEntry.Width = clsDgvGoodsItems.Width

        panelGoodsFooter.Top = clsDgvGoodsItems.Top + clsDgvGoodsItems.Height + 3

        panelGoodsFooter.Width = grpBoxGoods.Width - 5 '= panelGoodsHdr.Width
        '= panelGoodsTotals.Left = panelGoodsFooter.Width - panelGoodsTotals.Width - 3

        '= btnLookupGoods.Left = panelGoodsFooter.Width - btnLookupGoods.Width - 20
        btnExit.Left = grpBoxGoods.Width - btnExit.Width - 20

        'btnGoodsCommit.Top = grpBoxGoods.Height - 41
        btnGoodsCommit.Left = panelGoodsFooter.Width - btnGoodsCommit.Width - 20
        btnGoodsCancel.Left = btnGoodsCommit.Left


    End Sub '-SubFormResized
    '= = = = = = = = = = = =  === =
    '-===FF->

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click....
    '- Return true if ok to cancel.

    Public Function SubFormCloseRequest() As Boolean

        '-- confirm cancel--
        If (clsDgvGoodsItems.Rows.Count > 0) And (mDecInvoiceTotal > 0) And _
              ((mbIsPurchaseOrder And mbIsNewPO) Or (Not mbIsPurchaseOrder)) Then
            If MsgBox("Abandon changes ", _
                     MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + +MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                SubFormCloseRequest = True '=bCancel = False  '--let it go---
                '==Me.Hide()
            Else  '-stay-
                SubFormCloseRequest = False  '= bCancel = True   '--cant close yet--'--was mistake..  keep going..
            End If
        Else  '--not modified
            '==mbCancelled = True
            SubFormCloseRequest = True  '=bCancel = False   '--let it go to close---
            '= Me.Hide()
        End If  '--modified-
        '==Me.Close()
        '= Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '--  LOCAL Settings file functions..--

    '--load local settings.. eg printer names, sql serrvername..
    '== GONE 3301.428 =
    Private Function xxxx_mbLoadSettings() As Boolean
     
 
    End Function '--load settings..--
    '= = = = = =  =  == = = == ==
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

    '-- Numeric test..

    Private Function mbIsNumeric(ByVal sInput As String) As Boolean
        mbIsNumeric = False

        If IsNumeric(sInput) Then  '--good start-
            '-  check for "+","-" that pass the isNumeric test, but fail in Sql Server. test.
            If (InStr(sInput, "+") <= 0) AndAlso (InStr(sInput, "+") <= 0) Then
                mbIsNumeric = True
            End If
        End If  '-numeric-
    End Function  '-is numeric-
    '= = = = = = = = = = =  = = = = =

    '-ComputeAmountExTax=

    Private Function mDecComputeAmountExTax(ByVal decGrossAmount As Decimal) As Decimal

        mDecComputeAmountExTax = Decimal.Truncate((decGrossAmount * (100 / (100 + mDecGST_rate))) * 100) / 100
    End Function '-- mDecComputeAmountExTax-
    '= = = = = = = = = = = =  = = = == = ==
    '-===FF->

    '-- Delegate to clear the Grid..

    Delegate Sub MyClearGridDelegate(ByRef myArg2 As DataGridView)

    ''-  Use Invoke to avoid dgv re-entrancy problem.-

    Private Sub mSubClearDataGrid(ByRef dgvX As DataGridView)
        'If Me.clsDgvGoodsItems.InvokeRequired Then
        '    Me.clsDgvGoodsItems.Invoke(Sub() mSetCurrentCell(cellX))
        '    Return
        'End If  '-required-
        '-ok-
        Try
            dgvX.Rows.Clear()
        Catch ex As Exception
            MessageBox.Show("ERROR in DELEGATE 'mSubClearDataGrid'-" & vbCrLf & _
                            " Failed to Clear the data grid-" & vbCrLf & _
                               ex.Message, "Grid Clear..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub  '-set-
    '= = = = = = = = == = =
    '= = = = = =  =  == = = == ==
    '-===FF->

    '-- Browse Selected table using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseTable(ByVal sTableName As String, _
                                      ByRef colPrefs As Collection, _
                                       ByRef sTitle As String, _
                                        ByRef sWhere As String, _
                                         ByRef colKeys As Collection, _
                                           ByRef colSelectedRow As Collection, _
                                           Optional ByVal bHideEditButtons As Boolean = False) As Boolean
        Dim frmBrowse1 As New frmBrowsePOS

        mbBrowseTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer
        frmBrowse1.lookupSelection = True

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        frmBrowse1.versionPOS = msVersionPOS

        If bHideEditButtons Then
            frmBrowse1.HideEditButtons = True
        End If

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()
    End Function '--browse.--
    '= = = = = = =
    '-===FF->

    '=3411.0313=
    '-- Browse  table using --
    '--  Separate BROWSE33 FORM, (Includes TEXT SEARCH) and provided sWhere condition)..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseAndSearchTable(ByRef colPrefs As Collection, _
                                           ByRef sTitle As String, _
                                            ByRef sWhere As String, _
                                            ByRef colKeys As Collection, _
                                            ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Supplier") As Boolean
        Dim frmBrowse1 As New frmBrowse  '--File: frmBrowse33 --

        mbBrowseAndSearchTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        'If bHideEditButtons Then  '=3403.715- Default has changed-
        '    frmBrowse1.lookupSelection = True
        '    frmBrowse1.HideEditButtons = True
        'Else '--need to edit..
        '    frmBrowse1.lookupSelection = False
        '    frmBrowse1.HideEditButtons = False
        'End If
        'frmBrowse1.lookupSelection = True

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) Then '= frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseAndSearchTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()


    End Function  '-mbBrowseAndSearchTable-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->


    '-- Execute SQL Command..--
    '-- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Private Function mbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction) As Boolean
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
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->
    '-- Select Purchase Order--
    '--  Show list and select from outstanding orders--
    '-- If Editing PO, then must be undeleivered (Not receiving)..
    '--  (else is Goods Received.)-

    Private Function mbSelectPO(ByVal intSupplier_id As Integer, _
                                   ByRef dataTable1 As DataTable, _
                                   ByRef index As Integer) As Boolean
        Dim sSql As String
        Dim frmListSelect1 As frmListSelect
        Dim dtLines As DataTable

        mbSelectPO = False

        sSql = "SELECT order_id, order_date, orderNoSuffix, PurchaseOrder.comments, "
        sSql &= " staff.docket_name, SPACE(30) AS FirstItem, '$0,000,000.00' AS total_inc "
        sSql &= " FROM dbo.PurchaseOrder "
        sSql &= "    JOIN staff ON PurchaseOrder.staff_id=staff.staff_id "
        sSql &= " WHERE (supplier_id=" & CStr(intSupplier_id) & ") "
        sSql &= "  AND (isCancelled=0) "
        If Not chkIncludeCompletedOrders.Checked Then  '--include outstanding orders only-
            sSql &= " AND (isCompleted=0)  "
        End If
        If Not chkIncludeClosedForBO.Checked Then  '--include only if b/o allowed- (ie not closed)-
            sSql &= " AND (isClosedForBackorders=0)  "
        End If

        If gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then  '-something-
                '-- fillin description from 1st line of each PO..
                dataTable1.Columns("FirstItem").ReadOnly = False  '-so we can plug in descr.
                dataTable1.Columns("total_inc").ReadOnly = False  '-so we can plug in PO value.
                Dim decTotInc As Decimal = 0

                Dim sSqlLine As String, intOrder_id, intRx As Integer
                intRx = 0  '--track PO rows-
                For Each row1 As DataRow In dataTable1.Rows
                    intOrder_id = row1.Item("order_id")
                    sSqlLine = "SELECT stock.stock_id, stock.description, POL.cost_inc, POL.quantity "
                    sSqlLine &= " FROM  dbo.PurchaseOrderLine AS POL "
                    sSqlLine &= " JOIN stock ON stock.stock_id= POL.stock_id "
                    sSqlLine &= " WHERE (POL.order_id=" & CStr(intOrder_id) & ") "
                    If gbGetDataTable(mCnnSql, dtLines, sSqlLine) Then
                        If (Not (dtLines Is Nothing)) AndAlso (dtLines.Rows.Count > 0) Then
                            '- pump first descr. into curent PO row..
                            dataTable1.Rows(intRx).Item("FirstItem") = VB.Left(dtLines.Rows(0).Item("description"), 30)
                            '- add value-
                            decTotInc = 0
                            For Each rowPO_line As DataRow In dtLines.Rows
                                decTotInc += CDec(rowPO_line.Item("cost_inc")) * CInt(rowPO_line.Item("quantity"))
                            Next rowPO_line
                            '-- show total-
                            dataTable1.Rows(intRx).Item("total_inc") = FormatCurrency(decTotInc, 2)
                        End If
                    Else '- get stock failed
                        MsgBox("Error in getting STOCK recordset for PO table. " & vbCrLf & _
                                                        gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
                    End If  '-get-
                    intRx += 1
                Next '-row1

                '- select-
                frmListSelect1 = New frmListSelect
                frmListSelect1.inData = dataTable1
                frmListSelect1.hdrText = "Outstanding Purchase Orders "
                frmListSelect1.Text = "Outstanding Purchase Orders "
                '== If mbIsRefund Then
                '== frmListSelect1.hdrText = "Sales of Serial No: " & msSerialNumber
                '== Else
                '== End If
                frmListSelect1.ShowDialog()
                If frmListSelect1.cancelled Then
                    '= mbCancelled = True  '== sError = "selection cancelled."
                    frmListSelect1.Close()
                    Exit Function
                End If
                index = frmListSelect1.selectedRow
                '- sent back index..
                mbSelectPO = True
                frmListSelect1.Close()
            Else
                '-- no data..-
                MsgBox("No PurchaseOrder Orders on file ! ")

            End If '-nothing-
        Else  '-failed-
            MsgBox("Error in getting recordset for PO table. " & vbCrLf & _
                                           gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
            Exit Function
        End If  '-get-
    End Function '-select PO-
    '= = = = = = = = = = = = = = = == 
    '-===FF->

    '-- clear Invoice..

    Private Function mbClearInvoice() As Boolean

        '=3501.0905=
        mbIsCancelling = True
        '-- First cancel edit if any..
        If clsDgvGoodsItems.IsCurrentCellInEditMode Then
            Try
                clsDgvGoodsItems.CancelEdit()
                '-temp=
                '= MsgBox("Edit mode was cancelled..", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox("ERROR in mbClearInvoice- " & vbCrLf & _
                          "Failed to Cancel Edit Mode.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If
        DoEvents()
        '-clear the grid..
        Try
            '=clsDgvGoodsItems.Rows.Clear()  '= dgvGoodsItems.Rows.Clear()
            Me.clsDgvGoodsItems.BeginInvoke(New MyClearGridDelegate(AddressOf mSubClearDataGrid), Me.clsDgvGoodsItems)
        Catch ex As Exception
            MsgBox("ERROR in mbClearInvoice- " & vbCrLf & _
                      "Failed to clear items Grid.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        mColStockImages = New Collection
        panelIncludes.Enabled = False

        labPO_id.Text = ""
        chkLoadPO.Checked = False
        btnLoadPO.Enabled = False
        chkPriceIncludesTax.Checked = False

        mDecSubTotal = 0
        mDecFreightEx = 0
        mDecFreightTax = 0
        mDecFreightInc = 0

        mDecDiscount = 0
        mDecDiscountTax = 0
        mDecNettTax = 0
        mDecInvoiceTotal = 0
        mDecGoodsTotalExpected = 0

        txtOrderNoSuffix.Text = ""
        txtSupplierInvoiceNo.Text = ""
        txtGoodsSubTotal.Text = ""
        txtGoodsTotalTax.Text = ""

        txtGoodsFreight.Text = ""
        txtFreightTax.Text = ""
        txtGoodsDiscount.Text = ""
        txtGoodsDiscountAnalysis.Text = ""
        txtGoodsTotal.Text = ""

        txtGoodsNettTax.Text = ""
        txtTotalExpected.Text = ""

        panelStockLineEntry.Enabled = False
        btnStockLineOk.Enabled = False

        clsDgvGoodsItems.Enabled = False '= dgvGoodsItems.Enabled = False
        panelGoodsFooter.Enabled = False
        '= grpBoxIncludesTax.Enabled = False
        panelInvoiceNo.Enabled = False
        '4201.1028=
        btnLookupGoods.Enabled = False

        btnGoodsCommit.Enabled = False
    End Function  '-clear-
    '= = = = = = = = = = = = =
    '-===FF->

    '- UpdateGoodsTotal-

    Private Function mbUpdateGoodsTotal() As Boolean
        Dim row1 As DataGridViewRow
        Dim decAmount, decOver As Decimal
        Dim decBalance As Decimal
        Dim s1 As String
        Dim intCount As Integer

        mDecSubTotal = 0
        mDecTotalItemsTax = 0
        btnGoodsCommit.Enabled = False
        intCount = 0
        If (clsDgvGoodsItems.Rows.Count > 0) Then '= (dgvGoodsItems.Rows.Count > 0) Then
            For Each row1 In clsDgvGoodsItems.Rows
                decAmount = CDec(row1.Cells(k_GRIDCOL_EXTENSION).Value)  '--EX TAX-
                mDecSubTotal += decAmount
                '--compute total tax..--
                decAmount = CDec(row1.Cells(k_GRIDCOL_COST_TAX_EXTENDED).Value)
                mDecTotalItemsTax += decAmount
                mDecSubTotal += decAmount  '--add tax to subtotal.
                intCount += 1
                If Not row1.IsNewRow Then
                    row1.HeaderCell.Value = Trim(CStr(intCount)) & "."
                End If
            Next row1
        End If '--count-
        mDecSubTotalEx = mDecSubTotal - mDecTotalItemsTax

        '-freight-
        mDecFreightEx = 0
        mDecFreightTax = 0
        mDecFreightInc = 0

        If (txtGoodsFreight.Text <> "") AndAlso IsNumeric(txtGoodsFreight.Text) Then
            If chkFreightIsIncl.Checked Then
                mDecFreightInc = CDec(txtGoodsFreight.Text)
                mDecFreightEx = Decimal.Truncate((mDecFreightInc * (100 / (100 + mDecGST_rate))) * 100) / 100
                mDecFreightTax = (mDecFreightEx * mDecGST_rate / 100)
                '= mDecFreightTax = (mDecFreightInc / ((100 + mDecGST_rate) / 100))
                '== mDecFreightEx = mDecFreightInc - mDecFreightTax
                labFreightTax.Text = "(Incl Tax:)"
                labFreightTax.ForeColor = Color.DarkGray
                txtFreightTax.ForeColor = Color.DarkGray
                txtFreightTax.Text = FormatCurrency(mDecFreightTax, 2)
            Else '--not included-
                '--compute tax-
                mDecFreightEx = CDec(txtGoodsFreight.Text)
                mDecFreightTax = (mDecFreightEx * mDecGST_rate / 100)
                mDecFreightInc = mDecFreightEx + mDecFreightTax
                '- show freight tax-
                labFreightTax.Text = "Plus Tax:"
                labFreightTax.ForeColor = Color.Black
                txtFreightTax.ForeColor = Color.Black
                txtFreightTax.Text = FormatCurrency(mDecFreightTax, 2)
            End If  '-checked-
        End If '-freight-
        mDecNettTax = mDecTotalItemsTax + mDecFreightTax - mDecDiscountTax

        mDecInvoiceTotal = mDecSubTotal + mDecFreightInc - mDecDiscount

        txtGoodsTotalTax.Text = FormatCurrency(mDecTotalItemsTax, 2)
        txtGoodsSubTotal.Text = FormatCurrency(mDecSubTotal, 2)

        txtGoodsNettTax.Text = FormatCurrency(mDecNettTax, 2)
        txtGoodsTotal.Text = FormatCurrency(mDecInvoiceTotal, 2)

        '- no commit for view PO..-
        If (mbIsPurchaseOrder And mbIsNewPO) OrElse _
                  (clsDgvGoodsItems.Rows.Count > 0) Then  '==  And (txtSupplierInvoiceNo.Text <> "")) Then
            btnGoodsCommit.Enabled = True
        End If

    End Function  '-UpdateGoodsTotal-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Item Qty changed..-
    '-  Update extension in stock info grid row..--

    Private Function mbUpdateGoodsStockItem(ByVal intGridRow As Integer, _
                                                  ByVal sQty As String) As Boolean
        Dim intStock_id As Integer
        Dim sBarcode As String = ""
        Dim decItemQty As Decimal
        Dim decCostExTax, decCostIncTax As Decimal
        Dim decItemExtensionInc, decItemExtensionEx As Decimal '== After extension incl..-"-
        Dim decCostTaxExtended As Decimal = 0 '== tax..-"-
        '= Dim decItemExtensionExTax As Decimal '== After extension incl..-"-

        decItemQty = CDec(sQty)

        With clsDgvGoodsItems.Rows(intGridRow)
            decCostExTax = CDec(.Cells(k_GRIDCOL_COST_EX).Value)
            decCostIncTax = CDec(.Cells(k_GRIDCOL_COST_INC).Value)
        End With

        '-- extension-
        decItemExtensionInc = decCostIncTax * decItemQty
        decItemExtensionEx = decCostExTax * decItemQty
        decCostTaxExtended = decItemExtensionInc - (decCostExTax * decItemQty)
        '= decItemExtensionExTax = decItemExtension - decSellActualTotalTax

        '--update
        With clsDgvGoodsItems.Rows(intGridRow)
            .Cells(k_GRIDCOL_EXTENSION).Value = FormatCurrency(decItemExtensionEx, 2)
            .Cells(k_GRIDCOL_COST_TAX_EXTENDED).Value = FormatCurrency(decCostTaxExtended, 2)
            '== .Cells(k_GRIDCOL_SELL_EXTAX_EXTENDED).Value = FormatCurrency(decItemExtensionExTax, 2)
        End With

    End Function  '--mbUpdateSaleStockItem-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-  load stock info grid row..--

    Private Function mbSetupGoodsStockItem(ByRef dataTable1 As DataTable, _
                                           ByVal intGridRow As Integer, _
                                           Optional ByVal decItemQty As Decimal = 1, _
                                           Optional ByVal decOriginalCost_ex As Decimal = -1, _
                                           Optional ByVal intPO_line_id As Integer = -1) As Boolean
        Dim row1 As DataRow
        Dim intStock_id As Integer
        Dim sBarcode As String = ""
        Dim sGoodsTaxcode, sSalesTaxCode As String
        Dim decCostExTax, decCostTax, decCostIncTax As Decimal
        Dim decSellExTax As Decimal  '--rrp ex tax-
        Dim decSellTaxAmount As Decimal = 0
        Dim decSellIncTax As Decimal '=="RRP"-
        '-- after pricing grade-
        '== Dim decSellActualExTax As Decimal '== After applying Pricing Grade..-"-
        '== Dim decSellActualTaxAmount As Decimal = 0 '== tax..-"-
        '== Dim decSellActualIncTax As Decimal '== Plus tax.-..-"-
        '= Dim decItemQty As Decimal = 1   '--default qty--
        Dim decItemExtensionInc, decItemExtensionExTax As Decimal '== After extension incl..-"-
        '== Dim decItemExtensionExTax As Decimal '== After extension incl..-"-
        Dim decCostTaxExtended As Decimal = 0 '== tax..-"-

        Dim yBinaryData() As Byte
        Dim image1 As Image

        row1 = dataTable1.Rows(0)
        '= Call mbClearItemEntry()
        intStock_id = row1.Item("stock_id")
        '- check if already set up..=
        If CInt(clsDgvGoodsItems.Rows(intGridRow).Cells(k_GRIDCOL_STOCK_ID).Value) = intStock_id Then
            Exit Function  '--don't over-write existing info..-
        End If

        sBarcode = row1.Item("barcode")
        sGoodsTaxcode = UCase(row1.Item("Goods_taxcode"))
        sSalesTaxCode = UCase(row1.Item("Sales_taxcode"))

        decCostExTax = CDec(row1.Item("costExTax"))
        If (decOriginalCost_ex >= 0) Then  '-use orig cost from PO lines..
            decCostExTax = decOriginalCost_ex
        End If
        decCostIncTax = decCostExTax
        decCostTax = 0
        If UCase(sGoodsTaxcode) = "GST" Then
            decCostTax = (decCostExTax * mDecGST_rate / 100)
            decCostIncTax = decCostExTax + decCostTax
        End If

        decSellExTax = CDec(row1.Item("sellExTax"))  '--rrp ex tax-
        '-- get GST rate for this tax code..
        '-mDecGST_rate-
        decSellTaxAmount = 0
        '- compute rrp incl. -
        '-3519.0212-
        If UCase(sSalesTaxCode) = "GST" Then
            decSellTaxAmount = ((decSellExTax * mDecGST_rate / 100))
        End If  '-gst-
        decSellIncTax = decSellExTax + decSellTaxAmount
        '-- ROUNDING-
        '=3519.0214= ROUNDING..  Sell_inc IS FOR DISPLAY ONLY..
        '--   MUST round to 2 decimals first.
        decSellIncTax = Decimal.Round(decSellIncTax, 2)
        decSellIncTax += mDecGetRoundingAmount(decSellIncTax)

        '-- extension-
        decItemExtensionInc = decCostIncTax * decItemQty
        decCostTaxExtended = decItemExtensionInc - (decCostExTax * decItemQty)
        decItemExtensionExTax = decCostExTax * decItemQty '= decItemExtensionInc - decSellActualTotalTax

        '- show values-
        Dim decQty As Decimal, intQty As Integer, decFraction As Decimal
        Dim sFormattedQty As String
        '-- format qty properly-
        decQty = decItemQty
        intQty = Int(Abs(decQty))
        decFraction = decQty - CDec(intQty)  '--see if any decimals..
        If (decFraction = 0) Then '-- integer only-
            sFormattedQty = CStr(intQty)  '--"qty"
        Else '--has fraction-
            sFormattedQty = Format(decQty, "  0.00")  '--"qty"
        End If
        With clsDgvGoodsItems.Rows(intGridRow)
            .HeaderCell.Value = (intGridRow + 1).ToString  '--number the rows.-
            .Cells(k_GRIDCOL_BARCODE).Value = row1.Item("barcode")

            '--This is done in calling rtn.
            'If Not IsDBNull(row1.Item("supcode")) Then
            '    .Cells(k_GRIDCOL_SUP_CODE).Value = row1.Item("supcode")
            'Else  '-is null- so use barcode-
            '    .Cells(k_GRIDCOL_SUP_CODE).Value = row1.Item("barcode")
            'End If
            .Cells(k_GRIDCOL_CAT1).Value = row1.Item("cat1")
            .Cells(k_GRIDCOL_CAT2).Value = row1.Item("cat2")
            .Cells(k_GRIDCOL_DESCRIPTION).Value = row1.Item("description")
            .Cells(k_GRIDCOL_COST_EX).Value = Replace(FormatCurrency(decCostExTax, 2), "$", "")

            .Cells(k_GRIDCOL_TAX_CODE).Value = row1.Item("Goods_TaxCode")
            .Cells(k_GRIDCOL_COST_TAX).Value = Replace(FormatCurrency(decCostTax, 2), "$", "")
            .Cells(k_GRIDCOL_COST_INC).Value = Replace(FormatCurrency(decCostIncTax, 2), "$", "")
            '= .Cells(k_GRIDCOL_COST_INC).Value = FormatCurrency(decCostIncTax, 2)

            .Cells(k_GRIDCOL_SELL_EX).Value = Replace(FormatCurrency(decSellExTax, 2), "$", "")
            .Cells(k_GRIDCOL_SALES_TAX_CODE).Value = row1.Item("sales_TaxCode")
            .Cells(k_GRIDCOL_SELL_INC).Value = Replace(FormatCurrency(decSellIncTax, 2), "$", "")
            .Cells(k_GRIDCOL_QTY).Value = sFormattedQty
            .Cells(k_GRIDCOL_EXTENSION).Value = Replace(FormatCurrency(decItemExtensionExTax, 2), "$", "")
            '= .Cells(k_GRIDCOL_SERIALNOs).Value = ""

            '-- Hidden--
            .Cells(k_GRIDCOL_SERIALNOLIST).Value = ""
            .Cells(k_GRIDCOL_STOCK_ID).Value = CStr(row1.Item("stock_id"))
            .Cells(k_GRIDCOL_COST_TAX_EXTENDED).Value = Replace(FormatCurrency(decCostTaxExtended, 2), "$", "")
            '=3107.919=
            .Cells(k_GRIDCOL_PO_LINE_ID).Value = intPO_line_id

            '-- Serial No.-
            If row1.Item("track_serial") Then  '--s/be boolean-
                .Cells(k_GRIDCOL_SERIALNOSREQUIRED).Value = "YES (Click)"   '-- wants serial no.-
                .Cells(k_GRIDCOL_SERIALNOSREQUIRED).Style.ForeColor = Color.Firebrick
                .Cells(k_GRIDCOL_SERIALNOLIST).Value = ""  '-- wants serial no.-
                .Cells(k_GRIDCOL_TRACK_SERIAL).Value = "1"    '--save flag in hidden grid column.-
                '--Item with serialno.  can only be ONE of per SerialNo.
            Else   '- no serials-
                .Cells(k_GRIDCOL_SERIALNOSREQUIRED).Value = "No"
                .Cells(k_GRIDCOL_SERIALNOLIST).Value = ""  '--no serial nos.-
                .Cells(k_GRIDCOL_TRACK_SERIAL).Value = "0"
            End If
            '- save original cost values.
            .Cells(k_GRIDCOL_ORIGINAL_COST_EX).Value = .Cells(k_GRIDCOL_COST_EX).Value
            .Cells(k_GRIDCOL_ORIGINAL_COST_INC).Value = .Cells(k_GRIDCOL_COST_INC).Value
            '-- get tax, price and picture.--
            '== txtItemTax.Text = row1.Item("sales_TaxCode")
        End With  '--dgvSaleItems.Rows-

        '-- show picture if any..
        If mColStockImages.Contains(CStr(intStock_id)) Then  '--we saved it.-
            image1 = mColStockImages.Item(CStr(intStock_id))
            picGoodsItem.Image = image1
        Else  '-dig it out of the datatable.
            If Not IsDBNull(row1.Item("productPicture")) Then
                yBinaryData = row1.Item("productPicture") '==mRstEdit.Fields(sFldName).Value
                Try
                    '--- load picture from byte array..-
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(yBinaryData)
                    image1 = System.Drawing.Image.FromStream(ms)
                    picGoodsItem.Image = image1
                    ms.Close()
                    '--  save the image in static image collection..
                    mColStockImages.Add(image1, CStr(intStock_id))
                Catch ex As Exception
                    MsgBox("Failed to load image from stock table.. " & vbCrLf & _
                                      "Error: " & ex.Message)
                End Try
            End If
        End If  '--contains-
        mIntQtyInStock = CInt(row1.Item("qtyInStock"))

        '-track-serial-
        '== If txtItemSerialNo.Enabled Then  '-track-serial-
        '== mDecItemExtension = mDecSellActual  '--Qty one only-
        '== txtItemExtension.Text = txtItemSellPrice.Text
        '== btnLineEnter.Enabled = True  '--can go now..-
        '== End If
        panelGoodsFooter.Enabled = True

        DoEvents()
    End Function  '--SetupGoodsStockItem-
    '= = = = = = = = = = = = = = = =  = =
    '-===FF->

    '- Pre-load grid with order details..
    ' For PO: Can be from Standard Order or Edited Order-
    '--  For GR- Can be from PO that is being delivered.
    '- Dictionary collection is Key(stock_id), value (qty)..

    Private Function mbPreLoadOrderItems(ByRef dicOrderItems As Dictionary(Of Integer, Integer)) As Boolean
        Dim intStock_id, intQty, intGridRx As Integer
        Dim dtStock As DataTable
        Dim sSql, sBarcode As String
        Dim gridrow1 As DataGridViewRow
        Dim dtRow1 As DataRow

        mbPreLoadOrderItems = False
        '-- Clear the grid.--
        '= clsDgvGoodsItems.Rows.Clear()
        Me.clsDgvGoodsItems.BeginInvoke(New MyClearGridDelegate(AddressOf mSubClearDataGrid), Me.clsDgvGoodsItems)
        intGridRx = 0  '--track grid rows.-
        Application.UseWaitCursor = True
        DoEvents()

        For Each kvp As KeyValuePair(Of Integer, Integer) In dicOrderItems
            '=Console.WriteLine("Key = {0}, Value = {1}", _
            '==     kvp.Key, kvp.Value)
            intStock_id = kvp.Key
            intQty = kvp.Value

            '- get stock record=
            sSql = "SELECT *, supcode FROM dbo.stock "
            sSql &= "  LEFT OUTER JOIN dbo.SupplierCode AS SC "
            sSql &= "      ON (SC.supplier_id=" & CStr(mIntSupplier_id) & ") "
            sSql &= "           AND (SC.stock_id=" & CStr(intStock_id) & ") "
            sSql &= "WHERE (stock.stock_id=" & CStr(intStock_id) & ");"
            If gbGetDataTable(mCnnSql, dtStock, sSql) AndAlso _
                                   (Not (dtStock Is Nothing)) AndAlso (dtStock.Rows.Count > 0) Then
                sBarcode = dtStock.Rows(0).Item("barcode")
                '-- Add a  grid row.-
                gridrow1 = New DataGridViewRow
                clsDgvGoodsItems.Rows.Add(gridrow1)
                clsDgvGoodsItems.Rows(intGridRx).Cells(k_GRIDCOL_BARCODE).Value = sBarcode
                DoEvents()

                '==  Target-New-Build-4253..
                '==  Target-New-Build-4253..
                '==   B. -- Purchase Orders-  NewSpecialOrder..
                '== SupplierCode must be installed in Grid being auto-built.
                '==   
                dtRow1 = dtStock.Rows(0)
                With clsDgvGoodsItems.Rows(intGridRx)
                    If Not IsDBNull(dtRow1.Item("supcode")) Then
                        .Cells(k_GRIDCOL_SUP_CODE).Value = dtRow1.Item("supcode")
                    Else  '-is null- so use barcode-
                        .Cells(k_GRIDCOL_SUP_CODE).Value = dtRow1.Item("barcode")
                    End If
                End With
                '==END  Target-New-Build-4253..
                '==END  Target-New-Build-4253..


                '--have a row..-
                Call mbSetupGoodsStockItem(dtStock, intGridRx, CDec(intQty))
                '- update invoice total--
                Call mbUpdateGoodsTotal()
                '- scroll to make visible
                If (intGridRx Mod 10) = 0 Then
                    clsDgvGoodsItems.FirstDisplayedScrollingRowIndex = intGridRx
                End If
                DoEvents()
                intGridRx += 1  '- count-
            Else '--not found..-
                Application.UseWaitCursor = False
                MsgBox("No Stock record found for Stock_id: '" & intStock_id & "' !" & _
                         vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                Exit Function
            End If  '-get--
        Next kvp
        '-done-
        clsDgvGoodsItems.FirstDisplayedScrollingRowIndex = (intGridRx - 1)
        DoEvents()
        mbPreLoadOrderItems = True
        clsDgvGoodsItems.Enabled = True
        Application.UseWaitCursor = False

    End Function  '-preLoad-
    '= = = = = = = = = = = = = = = =  = =
    '-===FF->

    '-- GET EXISTING Purchase Order Details-
    '--    and Load into GRID..
    '--  For PO (view/print) and GR (received stuff on order..)
    '--   (bShowAllItems will be on only for View/print PO)..

    Private Function mbGetOrderDetails(ByVal intPO_id As Integer, _
                                       Optional ByVal bShowAllItems As Boolean = False) As Boolean
        Dim sSql As String
        '=Dim dtPO As DataTable
        Dim dtLines, dtstock As DataTable
        Dim intStock_id, intLine_id, intQty, intGridRx As Integer
        Dim decCost_ex As Decimal
        Dim sBarcode, sSupCode As String
        Dim gridrow1 As DataGridViewRow

        mbGetOrderDetails = False
        '=3519.0224=
        clsDgvGoodsItems.Enabled = False
        clsDgvGoodsItems.SuspendLayout()

        '-- get PO..-
        '- Make list of stuff in THIS order..
        sSql = "SELECT *, PurchaseOrder.order_date, due_date orderNoSuffix, comments "
        sSql &= "  FROM dbo.PurchaseOrderLine AS POL "
        sSql &= "     JOIN dbo.PurchaseOrder ON (POL.order_id=PurchaseOrder.order_id )"
        sSql &= " WHERE (POL.order_id=" & CStr(intPO_id) & ") "

        If gbGetDataTable(mCnnSql, dtLines, sSql) Then
            If (Not (dtLines Is Nothing)) AndAlso (dtLines.Rows.Count > 0) Then  '-something-
                txtGoodsDeliveryAddress.Text = dtLines.Rows(0).Item("delivery_address")
                '- pick stock_id, cost_ex, qty  out of each PO-Line-
                '- SAVE cuurent Outstanding  qty's so we can deduce if PO gets Completed..
                mDicPO_ItemsOutstanding = New Dictionary(Of Integer, Integer)
                intGridRx = -1
                For Each datarow1 As DataRow In dtLines.Rows
                    intStock_id = datarow1.Item("stock_id")
                    intLine_id = datarow1.Item("line_id")
                    intQty = datarow1.Item("quantity")   '-original qty.-
                    If Not bShowAllItems Then  '-shsow balance only to com..
                        intQty -= datarow1.Item("qtyReceived")  '-subtract prev recvd.-
                    End If
                    If (intQty > 0) Or bShowAllItems Then  '-some still to come..
                        decCost_ex = datarow1.Item("cost_ex")
                        sSupCode = datarow1.Item("suppliercode")
                        '- get stock record -
                        sSql = "SELECT *,'--' AS supcode FROM dbo.stock "
                        sSql &= "WHERE (stock.stock_id=" & CStr(intStock_id) & ");"
                        If gbGetDataTable(mCnnSql, dtstock, sSql) AndAlso _
                                               (Not (dtstock Is Nothing)) AndAlso (dtstock.Rows.Count > 0) Then
                            sBarcode = dtstock.Rows(0).Item("barcode")
                            '-- Add a  grid row.-
                            intGridRx += 1
                            gridrow1 = New DataGridViewRow
                            clsDgvGoodsItems.Rows.Add(gridrow1)
                            clsDgvGoodsItems.Rows(intGridRx).Cells(k_GRIDCOL_BARCODE).Value = sBarcode
                            DoEvents()
                            '--have a row..-
                            Call mbSetupGoodsStockItem(dtstock, intGridRx, CDec(intQty), decCost_ex, intLine_id)
                            '-- FIX sup code in grid..-
                            '-  (Must come from PO line..)--
                            clsDgvGoodsItems.Rows(intGridRx).Cells(k_GRIDCOL_SUP_CODE).Value = sSupCode
                            '-- Add to list of outstanding items/qty's--
                            mDicPO_ItemsOutstanding.Add(intLine_id, intQty)
                            '- update PO Totals..-
                            Call mbUpdateGoodsTotal()
                        End If  '-get-
                    End If  '-qty-
                Next datarow1
                If (intGridRx >= 0) Then mbGetOrderDetails = True '-found some-
            Else  '-no record-
                MsgBox("No recordset for PO: " & intPO_id & vbCrLf, MsgBoxStyle.Exclamation)
                '= Exit Function
            End If
        Else  '-failed-
            MsgBox("Error in getting recordset for PO: " & intPO_id & vbCrLf & _
                                           gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
            '=Exit Function
        End If '-get PO-
        clsDgvGoodsItems.ResumeLayout()
        clsDgvGoodsItems.Enabled = True

    End Function  '-get order details-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- set up PO for Goods Received..-

    Private Function mbLoadGoodsPO(ByRef dataTable1 As DataTable, _
                                   ByVal index As Integer) As Boolean
        Dim intOrder_id As Integer
        Dim dataRow1 As DataRow = dataTable1.Rows(index)

        intOrder_id = dataRow1.Item("order_id")
        mIntOurOrder_id = intOrder_id

        '--  get all lines -
        '- SAVE date PO created !!!
        mDatePO_date_created = CDate(dataRow1.Item("order_date"))

        '--  get/load all lines -

        If Not mbGetOrderDetails(intOrder_id) Then
            MsgBox("No outstanding PO items for order id:  " & intOrder_id & ".", MsgBoxStyle.Exclamation)
            Call mbClearInvoice()
            Exit Function
        End If

        txtOrderNoSuffix.Text = dataRow1.Item("orderNoSuffix")
        labPO_id.Text = CStr(mIntOurOrder_id)

        txtGoodsComments.Text = dataRow1.Item("comments")
        '= txtGoodsDeliveryAddress.Text = dataRow1.Item("delivery_address")
        msCreatedByStaffName = dataRow1.Item("docket_name")
        clsDgvGoodsItems.Enabled = True   '- so he can scroll-
        panelGoodsFooter.Enabled = True
        MsgBox("Please Note:" & vbCrLf & _
                    " The Goods-Received data Grid has been loaded with those" & vbCrLf & _
                    " Items/Quantities still outstanding for the selected Purchase Order.." & vbCrLf & vbCrLf & _
                    "You should adjust the Quantities to reflect what is actually being received today.. ", MsgBoxStyle.Information)

    End Function  '-- load goods PO-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--SetupSupplier-
    '--SetupSupplier-

    Private Function mbSetupSupplier(ByRef colSelectedRow As Collection) As Boolean
        Dim sSql As String
        Dim index, intOrder_id As Integer
        Dim datatable1 As DataTable

        msSupplierName = colSelectedRow.Item("supplierName")("value")
        msSupplierBarcode = colSelectedRow.Item("barcode")("value")

        '== mbPO_isCompleted = False
        mIntOurOrder_id = -1
        mbIsCancelling = False

        '- get supplier details-
        If (mIntSupplier_id > 0) Then
            sSql = "SELECT * FROM dbo.Supplier "
            sSql &= " WHERE (supplier_id=" & CStr(mIntSupplier_id) & ");"
            If Not gbGetDataTable(mCnnSql, mDataTableSupplier, sSql) Then
                MsgBox("Error in getting recordset for Invoice table: " & vbCrLf & _
                                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '==Exit Function '--msg was displayed..
                '= Me.Close()
                Call close_me()
                Exit Function
            Else  '-ok-
                If (mDataTableSupplier Is Nothing) OrElse (mDataTableSupplier.Rows.Count <= 0) Then
                    MsgBox("Error- No data table data for Supplier-id: " & CStr(mIntSupplier_id), MsgBoxStyle.Exclamation)
                    '==Exit Function '--msg was displayed..
                    '= Me.Close()
                    Call close_me()
                    Exit Function
                End If
            End If '-get-
        Else  '-could be id-zero-
            MsgBox("Error- Not a valid supplier record- id is: " & CStr(mIntSupplier_id), MsgBoxStyle.Exclamation)
            '==Exit Function '--msg was displayed..
            '= Me.Close()
            Call close_me()
            Exit Function
        End If '-id-

        txtSupplierName.Text = msSupplierName
        txtSupplierBarcode.Text = msSupplierBarcode
        DoEvents()

        txtSupplierBarcode.Enabled = False
        '= btnLookupSupplier.Enabled = False
        mIntSupplierDeliveryDays = CInt(mDataTableSupplier.Rows(0).Item("deliveryDays"))
        msSupplierEmailAddress = mDataTableSupplier.Rows(0).Item("emailAddress")
        btnPrint.Enabled = False
        picEmailPO.Enabled = False
        btnCancelPO.Enabled = False
        btnLookupGoods.Enabled = True

        If mbIsPurchaseOrder Then
            btnViewPO.Enabled = True
            btnNewStandardOrder.Enabled = True
            btnNewBlankPO.Enabled = True
            btnNewStandardOrder.Select()
            panelIncludes.Enabled = True
            txtGoodsDeliveryAddress.Text = msBusinessDeliveryAddress

            '-reset these in case we did a PO View only..
            '= clsDgvGoodsItems.Columns(k_GRIDCOL_BARCODE).ReadOnly = False
            '= clsDgvGoodsItems.Columns(k_GRIDCOL_SUP_CODE).ReadOnly = False
            clsDgvGoodsItems.Columns(k_GRIDCOL_QTY).ReadOnly = False

        Else  '--GR-
            '-- GR only.. Load PO related to this Goods Invoice.
            '-- get all undelivered PO's for this supplier-
            '-- List them in listSelect  -  Get choice--

            Dim intCount As Integer = 0
            '- check if any PO's outstanding this Supplier.-
            sSql = "SELECT COUNT(*)   "
            sSql &= "  FROM dbo.PurchaseOrder "
            sSql &= " WHERE (supplier_id=" & CStr(mIntSupplier_id) & ") "
            sSql &= " AND (isCancelled=0) "
            If Not chkIncludeCompletedOrders.Checked Then  '--include outstanding orders only-
                sSql &= " AND (isCompleted=0)  "
            End If
            If Not chkIncludeClosedForBO.Checked Then  '--include only if b/o allowed- (ie not closed)-
                sSql &= " AND (isClosedForBackorders=0)  "
            End If

            If gbGetSqlScalarIntegerValue(mCnnSql, sSql, intCount) Then
                If (intCount > 0) Then
                    '-have some-
                    If MsgBox("There are " & intCount & " outstanding Purchase Orders" & vbCrLf & _
                               " for the Supplier:  " & vbCrLf & msSupplierName & vbCrLf & vbCrLf & _
                               "Do you want to choose one for this Goods Received invoice ?",
                                   MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                        '-- show and select.-
                        If mbSelectPO(mIntSupplier_id, datatable1, index) Then
                            Call mbLoadGoodsPO(datatable1, index)
                        Else
                            MsgBox("Nothing selected !", MsgBoxStyle.Exclamation)
                        End If  '-select
                    Else '-no-
                    End If  '-yes/no-
                End If  '-count-
            End If  '-get-
            txtOrderNoSuffix.Enabled = True
            panelInvoiceNo.Enabled = True
            chkPriceIncludesTax.Checked = False
            chkPriceIncludesTax.Enabled = True
            If (clsDgvGoodsItems.Rows.Count > 0) Then  '-loaded PO lines.
                clsDgvGoodsItems.Enabled = False   '--until Suppl. Invoice No entered.
                chkLoadPO.Enabled = True '= False
                btnLoadPO.Enabled = False
                panelIncludes.Enabled = False  '=True
                chkPriceIncludesTax.Select()
            Else '-no PO loaded-
                panelIncludes.Enabled = True
                chkLoadPO.Checked = False
                chkLoadPO.Enabled = True
                btnLoadPO.Enabled = False
                '= btnLoadPO.Select()
                chkLoadPO.Select()
            End If  '--count-
         End If  '-GR-

    End Function  '--setupSupplier-
    '= = = = = = = = = = = = = = = == 
    '-===FF->

    '-- L o a d --

    Private Sub ucChildGoodsRecvd_Load(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Load
        Dim colSystemInfo As Collection
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim s1, sName As String

        msAppPath = My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        msAppFullname = msAppPath & My.Application.Info.AssemblyName
        If (VB.Right(LCase(msAppFullname), 4) <> ".exe") Then
            msAppFullname &= ".exe"
        End If

        msPDF_FilePath = gsGetPDF_file_path()

        mIntFormDesignHeight = Me.Height '--save starting dimensions..-
        mIntFormDesignWidth = Me.Width '--save starting dimensions..-

        panelStockLineEntry.Enabled = False
        txtStockItemBarcode.Text = ""
        txtStockItemSupplierCode.Text = ""
        txtStockItemDescription.Text = ""

        grpBoxGoods.Text = ""
        clsDgvGoodsItems.Enabled = False
        panelGoodsFooter.Enabled = False
        labPO_id.Text = ""

        '= grpBoxIncludesTax.Text = "Please select:"
        '= grpBoxIncludesTax.Enabled = False
        panelInvoiceNo.Enabled = False
        txtOrderNoSuffix.Enabled = False

        '= labToday.Text = Format(CDate(DateTime.Today), "ddd dd-MMM-yyyy")
        '= labStaffName.Text = msStaffName

        '--  TEMP--
        '--  TEMP--
        '-- FOR NOW, must uses prices from Stck Table..
        '= clsDgvGoodsItems.Columns(k_GRIDCOL_COST_EX).ReadOnly = True
        '= clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).ReadOnly = True

        '=4201.0929--
        labCreatingPDF.Visible = False

        '== Adding PO stuff..-
        labHdrGR.Enabled = False
        labHdrPO.Enabled = False

        btnLoadPO.Enabled = False
        btnViewPO.Enabled = False
        btnNewStandardOrder.Enabled = False
        btnNewBlankPO.Enabled = False

        btnPrint.Enabled = False
        picEmailPO.Enabled = False
        btnCancelPO.Enabled = False
        panelIncludes.Enabled = False

        '=3501.1104- OVERRIDE colPrefs stuff.
        '-mColPrefColumnsSuppliers -
        '-- Supplier --
        mColPrefColumnsSuppliers = New Collection
        mColPrefColumnsSuppliers.Add("barcode")
        mColPrefColumnsSuppliers.Add("supplierName")
        mColPrefColumnsSuppliers.Add("contactName")
        mColPrefColumnsSuppliers.Add("contactPosition")
        mColPrefColumnsSuppliers.Add("supplier_id")
        mColPrefColumnsSuppliers.Add("address")
        '= mColPrefsSupplier.Add("main_addr2")
        '== mColPrefsSupplier.Add("main_addr3")
        mColPrefColumnsSuppliers.Add("suburb")
        mColPrefColumnsSuppliers.Add("state")
        mColPrefColumnsSuppliers.Add("postcode")
        mColPrefColumnsSuppliers.Add("country")
        mColPrefColumnsSuppliers.Add("phone")
        mColPrefColumnsSuppliers.Add("fax")
        mColPrefColumnsSuppliers.Add("emailAddress")
        mColPrefColumnsSuppliers.Add("webSiteURL")
        '== mColPrefsSupplier.Add("grade")
        mColPrefColumnsSuppliers.Add("inactive")
        mColPrefColumnsSuppliers.Add("freight_free")
        '==mColPrefsSupplier.Add("reject_backorders")
        mColPrefColumnsSuppliers.Add("deliveryDays")
        mColPrefColumnsSuppliers.Add("abn")
        mColPrefColumnsSuppliers.Add("comments")
        '= mColPrefColumnsSuppliers.Add("date_modified")

        '-mColPrefColumnsStock-
        '--  stock--
        mColPrefColumnsStock = New Collection
        mColPrefColumnsStock.Add("description")
        mColPrefColumnsStock.Add("barcode")
        mColPrefColumnsStock.Add("brandName")
        mColPrefColumnsStock.Add("cat1")   '--fkey-
        mColPrefColumnsStock.Add("cat2")   '-fkey-
        '= mColPrefColumnsStock.Add("productPicture")
        mColPrefColumnsStock.Add("stock_id")
        '=3301.606= mColPrefsStock.Add("isServiceItem")
        mColPrefColumnsStock.Add("isNonStockItem")
        mColPrefColumnsStock.Add("track_serial")
        mColPrefColumnsStock.Add("inactive")
        mColPrefColumnsStock.Add("supplier_id")
        mColPrefColumnsStock.Add("costExTax")
        mColPrefColumnsStock.Add("goods_TaxCode")
        mColPrefColumnsStock.Add("sellExTax")
        mColPrefColumnsStock.Add("sales_TaxCode")
        mColPrefColumnsStock.Add("qtyInStock")
        '== mColPrefsStock.Add("qtyOnLayby")
        mColPrefColumnsStock.Add("allow_renaming")
        mColPrefColumnsStock.Add("comments")



        If mbIsPurchaseOrder Then
            '- must re-enable these 
            '--   so TAB from supplier barcode works.
            btnNewStandardOrder.Enabled = True
            btnNewBlankPO.Enabled = True
            '== labHdrGR.Font = font1   '--drop bold-
            '== labHdrGR.ForeColor = Color.FromArgb(252, 252, 252)  '=Color.LightGray '-disable GR...-
            labHdrPO.Enabled = True
            chkLoadPO.Visible = False
            btnLoadPO.Visible = False
            labHdrGR.Visible = False
            btnCancelPO.Enabled = False
            btnPrint.Enabled = False
            picEmailPO.Enabled = False
            chkPriceIncludesTax.Visible = False

            panelInvoiceNo.Visible = False
            TabControlInvoice.SelectedTab = TabControlInvoice.TabPages("TabPagePrint")
            '= panelPrinting.Left = panelInvoiceNo.Left
            '= panelPrinting.Width = panelInvoiceNo.Width
            panelPrinting.Enabled = False
            btnCancelPO.Left = panelPrinting.Width - btnCancelPO.Width - 12

            '== btnEditPO.Enabled = True
            '= panelGoodsBanner.BackColor = Color.FromArgb(255, 178, 60) '--light orange ? -
            '= panelGoodsBanner.BackColor = Color.FromArgb(250, 242, 157) '--mustard  ? -
            panelGoodsBanner.BackColor = Color.NavajoWhite
            labHdrFormType.Text = "Purchase Order"

            clsDgvGoodsItems.Columns(k_GRIDCOL_SERIALNOSREQUIRED).Visible = False

            labFreight.Visible = False
            labFreightTax.Visible = False
            labDiscount.Visible = False
            labDiscountTax.Visible = False

            txtGoodsFreight.Enabled = False
            txtGoodsFreight.BorderStyle = BorderStyle.None
            txtGoodsDiscount.Enabled = False
            txtGoodsDiscount.BorderStyle = BorderStyle.None
            labExpected.Visible = False
            txtTotalExpected.Visible = False

            dtPickerInvoiceDate.Enabled = False
            chkFreightIsIncl.Visible = False
            txtSupplierInvoiceNo.Enabled = False
            If (mIntSupplierDeliveryDays > 0) Then
                DTPickerDueDate.Value = DateAdd(DateInterval.Day, mIntSupplierDeliveryDays, DateTime.Today)
            End If
            labStockLinePrompt.Text = "Scan, Enter or Lookup the Stock Barcode..  Then set the Quantity in the Data Grid Line.."
            txtStockItemSupplierCode.Enabled = True

        Else  '-goods received.-
            '= labHdrPO.Font = font1   '--drop bold-
            '= labHdrPO.ForeColor = Color.FromArgb(252, 252, 252)  '= Color.LightGray  '-disable PO.
            labHdrPO.Visible = False
            labHdrGR.Enabled = True
            labHdrGR.Left = labHdrPO.Left
            panelPrinting.Visible = False
            TabControlInvoice.SelectedTab = TabControlInvoice.TabPages("TabPageInvoice")

            txtOrderNoSuffix.Enabled = True

            labDueDate.Visible = False
            DTPickerDueDate.Visible = False

            '== btnLoadPO.Enabled = True
            btnViewPO.Visible = False
            btnNewStandardOrder.Visible = False
            btnNewBlankPO.Visible = False

            '- reposition load button-
            chkLoadPO.Top = btnNewStandardOrder.Top - 9
            chkLoadPO.Left = btnNewStandardOrder.Left
            chkLoadPO.Checked = False
            btnLoadPO.Top = chkLoadPO.Top
            btnLoadPO.Left = chkLoadPO.Left + chkLoadPO.Width + 3
            btnLoadPO.Enabled = False

            chkPriceIncludesTax.Top = btnLoadPO.Top
            chkPriceIncludesTax.Left = btnLoadPO.Left + btnLoadPO.Width + 7

            '=3501.0904=-- Why WAS this set to false ??
            '= clsDgvGoodsItems.Columns(k_GRIDCOL_SUP_CODE).Visible = False

            '-change hdr panel colour--
            panelGoodsBanner.BackColor = Color.FromArgb(255, 178, 60) '--light orange ? -
            txtGoodsDeliveryAddress.Text = ""
            labStockLinePrompt.Text = "Scan or Enter the Stock Barcode.. " & _
                                        "Then finish off the Cost, Quantity and Serials in the Data Grid.."
            txtStockItemSupplierCode.Enabled = False

        End If  '-PO-

        '-restore pic size..
        picGoodsItem.Height = panelGoodsHdr.Height - 14

        '=3300.428= Call mbLoadSettings()
        msSettingsPath = gsLocalSettingsPath() '= default to assembly=
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        '- get printers collection and set up combos.
        cboInvoicePrinters.Items.Clear()
        '= cboReceiptPrinters.Items.Clear()

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboInvoicePrinters.Items.Add(sName)
                '= cboReceiptPrinters.Items.Add(sName)
                '=3411.0421- See below-
                '= If (InStr(LCase(sName), "adobe pdf") > 0) Then
                '=     msPdfPrinterName = sName  '-save PDF printer name--
                '= End If
            Next sName

            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_invoicePrtSettingKey) AndAlso _
                             (mLocalSettings1.item(k_invoicePrtSettingKey) <> "") Then
                '== gbQueryLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, s1) AndAlso (s1 <> "") Then
                s1 = mLocalSettings1.item(k_invoicePrtSettingKey)
                If colPrinters.Contains(s1) Then '--set it- 
                    cboInvoicePrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboInvoicePrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboInvoicePrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
        End If '-getAvail.-  
        '=3411.0421=
        '= Dim colPrinters As Collection
        '= Dim intDefaultPrinterIndex As Integer
        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else  '--see below-
            'For Each sName As String In colPrinters
            '    If (InStr(LCase(sName), "adobe pdf") > 0) Or _
            '             ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
            '        msPdfPrinterName = sName  '-save PDF printer name--
            '    End If
            'Next sName
        End If  '- no printers-

        '=3411.0110= (Microsoft will be preferred)..
        If Not gsGetPdfPrinterName(msPdfPrinterName) Then
            MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
        End If  '-gety-

        If (msPdfPrinterName = "") Then
            MsgBox("Please Note: " & vbCrLf & "No PDF printer is installed on this system." & vbCrLf & _
                    "Invoices created here can not be stored for emailing)..", MsgBoxStyle.Information)
        End If
        labPdfPrinter.Text = msPdfPrinterName

        '=3403.616= Set rh Edit limit.
        If mbIsPurchaseOrder Then
            clsDgvGoodsItems.lastEditableColumn = k_GRIDCOL_EXTENSION
        Else  '--goods received.
            clsDgvGoodsItems.lastEditableColumn = k_GRIDCOL_SERIALNOSREQUIRED
        End If
        btnLookupGoods.Enabled = False

        '- set align.
        '= clsDgvGoodsItems.Columnheaders("sell_ex").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        clsDgvGoodsItems.Columns("sell_ex").HeaderCell.Style.Alignment = _
                                    DataGridViewContentAlignment.MiddleRight 'Aligns the Header Text
        clsDgvGoodsItems.Columns("sell_inc").HeaderCell.Style.Alignment = _
                                    DataGridViewContentAlignment.MiddleRight  'Aligns the Header Text

        '=3519.0303==
        '- --  Popup menu for Right click on Grid..-
        mContextMenuItemInfo = New ContextMenu

        mnuCopyItemBarcode.Name = "CopyPartBarcode"
        mContextMenuItemInfo.MenuItems.Add(mnuCopyItemBarcode)
        mnuItemMenuSep1.Name = "ItemMenuSep1"
        mContextMenuItemInfo.MenuItems.Add(mnuItemMenuSep1)

        mnuCopyItemDescription.Name = "CopyItemDescription"
        mContextMenuItemInfo.MenuItems.Add(mnuCopyItemDescription)
        mnuItemMenuSep2.Name = "ItemMenuSep2"
        mContextMenuItemInfo.MenuItems.Add(mnuItemMenuSep2)

        '-- menu done..

        '=4201.0425=--  All stuff from SHOWN..
        '=4201.0425=--  All stuff from SHOWN..

        '=Dim event1 As System.EventArgs
        Dim pd1 As New PrintDocument()
        '= Dim s1 As String
        '= Dim ix, L1 As Integer
        '=3301.428= Dim colSystemInfo As Collection

        msCurrentUserNT = msSqlServerComputer & "\" & msCurrentUserName

        '-- get system Info table data.-
        '=3300.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '=3300.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        '-- Format ABN for printing..-
        msBusinessDisplayABN = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & _
                  " " & Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)
        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
        msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
        msBusinessState = mSysInfo1.item("BUSINESSSTATE")
        msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
        msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")

        msBusinessDeliveryAddress = msBusinessAddress1 & vbCrLf & _
                                     IIf(msBusinessAddress2 = "", "", msBusinessAddress2 & vbCrLf) & _
                                     msBusinessState & "    " & msBusinessPostCode

        If mSysInfo1.contains("POS_SELL_MARGIN") Then
            s1 = mSysInfo1.item("POS_SELL_MARGIN")
            If mbIsNumeric(s1) Then
                mDecSell_margin = CDec(s1)
            End If
        End If
        If mSysInfo1.contains("GSTPercentage") Then
            s1 = mSysInfo1.item("GSTPercentage")
            '-mdecGST_percentage
            If IsNumeric(s1) Then
                mDecGST_rate = CDec(s1)
            End If
        End If
        '=3411.0421- Server Share Path for Email Queue.
        msEmailQueueSharePath = mSysInfo1.item("POS_EMAILQUEUE_SHAREPATH")

        Call mbClearInvoice()

        chkPriceIncludesTax.Checked = False   '-DEFAULT is cost_ex.  '= True
        If mbIsPurchaseOrder Then
            '=chkPriceIncludesTax.Enabled = False
            '--=3107.814=  FOR PO, must uses prices from Stock Table..
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_EX).ReadOnly = True
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).ReadOnly = True
            clsDgvGoodsItems.Columns(k_GRIDCOL_SELL_EX).ReadOnly = True
            clsDgvGoodsItems.Columns(k_GRIDCOL_SELL_INC).ReadOnly = True  '=3519.0212.
        Else
            '-Goods REceived- DEFAULT- can change cost_ex only.
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_EX).ReadOnly = False
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).ReadOnly = True
            'clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).HeaderCell.Style.ForeColor = _
            '                                 System.Drawing.ColorTranslator.FromOle(&H707070)
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).DefaultCellStyle.BackColor = _
                                                                 System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
            clsDgvGoodsItems.Columns(k_GRIDCOL_SELL_EX).ReadOnly = False
            clsDgvGoodsItems.Columns(k_GRIDCOL_SELL_INC).ReadOnly = False   '=3519.0212.
        End If
        labHelp.Text = "Select Supplier.."
        labHelp2.Text = ""
        mbIsInitialising = False

        '-- startup finished..---
        '- This edit mode for Stock barcode..
        '= clsDgvGoodsItems.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        txtSupplierBarcode.Select()


    End Sub  '--load--
    '= = = = = = = = = = = = = 
    '-===FF->

    '-- Form Activated -
    'Private Sub frmGoodsRecvd_Activated(ByVal sender As System.Object, _
    '                               ByVal e As System.EventArgs) Handles MyBase.Activated

    '    If mbActivated Then Exit Sub '-- do once only..--
    '    mbActivated = True
    '    If (mIntForm_top = -1) Or (mIntForm_left = -1) Then
    '        Call CenterForm(Me)
    '    Else
    '        '-- caller provided-
    '        Me.Top = mIntForm_top
    '        Me.Left = mIntForm_left
    '    End If
    '    Me.Update()
    'End Sub  '-Activated-
    '= = = = = = = = = == = 

    '-- Shown..-

    'Private Sub frmGoodsRecvd_Shown(ByVal sender As System.Object, _
    '                                   ByVal e As System.EventArgs) Handles MyBase.Shown

    '    Dim event1 As System.EventArgs
    '    Dim pd1 As New PrintDocument()
    '    Dim s1 As String
    '    Dim ix, L1 As Integer
    '    '=3301.428= Dim colSystemInfo As Collection

    '    msCurrentUserNT = msSqlServerComputer & "\" & msCurrentUserName

    '    '-- get system Info table data.-
    '    '=3300.428=
    '    mSysInfo1 = New clsSystemInfo(mCnnSql)

    '    '=3300.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
    '    msBusinessABN = mSysInfo1.item("BUSINESSABN")
    '    msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
    '    '-- Format ABN for printing..-
    '    msBusinessDisplayABN = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & _
    '              " " & Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)
    '    '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
    '    msBusinessName = mSysInfo1.item("BUSINESSNAME")
    '    msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
    '    msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
    '    msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
    '    msBusinessState = mSysInfo1.item("BUSINESSSTATE")
    '    msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
    '    msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")

    '    msBusinessDeliveryAddress = msBusinessAddress1 & vbCrLf & _
    '                                 IIf(msBusinessAddress2 = "", "", msBusinessAddress2 & vbCrLf) & _
    '                                 msBusinessState & "    " & msBusinessPostCode

    '    If mSysInfo1.contains("POS_SELL_MARGIN") Then
    '        s1 = mSysInfo1.item("POS_SELL_MARGIN")
    '        If mbIsNumeric(s1) Then
    '            mDecSell_margin = CDec(s1)
    '        End If
    '    End If
    '    If mSysInfo1.contains("GSTPercentage") Then
    '        s1 = mSysInfo1.item("GSTPercentage")
    '        '-mdecGST_percentage
    '        If IsNumeric(s1) Then
    '            mDecGST_rate = CDec(s1)
    '        End If
    '    End If
    '    '=3411.0421- Server Share Path for Email Queue.
    '    msEmailQueueSharePath = mSysInfo1.item("POS_EMAILQUEUE_SHAREPATH")

    '    Call mbClearInvoice()

    '    chkPriceIncludesTax.Checked = False   '-DEFAULT is cost_ex.  '= True
    '    If mbIsPurchaseOrder Then
    '        '=chkPriceIncludesTax.Enabled = False
    '        '--=3107.814=  FOR PO, must uses prices from Stock Table..
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_COST_EX).ReadOnly = True
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).ReadOnly = True
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_SELL_EX).ReadOnly = True
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_SELL_INC).ReadOnly = True  '=3519.0212.
    '    Else
    '        '-Goods REceived- DEFAULT- can change cost_ex only.
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_COST_EX).ReadOnly = False
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).ReadOnly = True
    '        'clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).HeaderCell.Style.ForeColor = _
    '        '                                 System.Drawing.ColorTranslator.FromOle(&H707070)
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).DefaultCellStyle.BackColor = _
    '                                                             System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_SELL_EX).ReadOnly = False
    '        clsDgvGoodsItems.Columns(k_GRIDCOL_SELL_INC).ReadOnly = False   '=3519.0212.
    '    End If
    '    labHelp.Text = "Select Supplier.."
    '    labHelp2.Text = ""
    '    mbIsInitialising = False

    '    '-- startup finished..---
    '    '- This edit mode for Stock barcode..
    '    '= clsDgvGoodsItems.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
    '    txtSupplierBarcode.Select()

    'End Sub  '--Shown.-
    '= = = = = = = = = =  = =
    '-===FF->

    '-- form resized --

    Private Sub ucChildGoodsRecvd_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        '= If (Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized) Then

        '--  cant make smaller than original..-
        '= If (Me.Height < mIntFormDesignHeight) Then Me.Height = mIntFormDesignHeight
        '= If (Me.Width < mIntFormDesignWidth) Then Me.Width = mIntFormDesignWidth

       'btnGoodsCancel.Top = btnGoodsCommit.Top

        'labHelp.Top = btnGoodsCommit.Top + 7
        'labHelp2.Top = labHelp.Top
        '= labVersion.Top = grpBoxGoods.Top + grpBoxGoods.Height + 5 '== Me.Height - 40

        DoEvents()
        '= End If '-win-state-
    End Sub '--resize--
    '= = = = = = = = = = = =
    '-===FF->

    '-btnLookupGoods-

    Private Sub btnLookupGoods_Click(sender As Object, e As EventArgs) Handles btnLookupGoods.Click

        Dim frmlookup1 As New frmLookupGoods

        '= frmlookup1.SqlServer = msServer
        frmlookup1.SqlServer = msServer
        frmlookup1.connectionSql = mCnnSql '-- sql connenction..-
        frmlookup1.dbInfoSql = mColSqlDBInfo

        frmlookup1.DBname = msSqlDbName
        frmlookup1.VersionPOS = msVersionPOS
        frmlookup1.supplier_id = mIntSupplier_id
        frmlookup1.SupplierName = txtSupplierName.Text
        frmlookup1.SupplierBarcode = msSupplierBarcode
        frmlookup1.staffNname = msStaffName
        frmlookup1.BusinessName = msBusinessName

        frmlookup1.ShowDialog()
        frmlookup1.Close()

    End Sub  '-btnLookupGoods-
    '= = = = = = =  = == = = = = 


    '--btnStockAdmin-

    Private Sub btnStockAdmin_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs)
        Dim frmStock1 As New frmStock

        frmStock1.StaffName = msStaffName

        frmStock1.SqlServer = msServer
        frmStock1.connectionSql = mCnnSql '--job tracking sql connenction..-
        frmStock1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..

        frmStock1.DBname = msSqlDbName
        frmStock1.VersionPOS = msVersionPOS
        '=frmBrowse1.tableName = sTableName '--"jobs"
        '== frmBrowse1.IsSqlServer = True '--bIsSqlServer
        frmStock1.form_left = Me.Left
        frmStock1.form_top = Me.Top + 70

        frmStock1.ShowDialog()

    End Sub '--btnStockAdmin-
    '= = = = = = =  = = = = == 
    '-===FF->

    Private Sub txtSupplierBarcode_TextChanged(ByVal sender As System.Object, _
                                                    ByVal e As System.EventArgs) Handles txtSupplierBarcode.TextChanged


    End Sub '--SupplierBarcode_TextChanged-
    '= = = = = = = = = = = =

    '-- Supplier Search (F2)..--
    '--  Barcode TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for Supplier Lookup--

    Public Sub txtSupplierBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                   Handles txtSupplierBarcode.KeyDown

        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        '= Dim intGridRow, intGridCol, intStock_id As Integer
        '= Dim dataTable1 As DataTable
        '= Dim row1 As DataRow
        Dim sSql, s1, sBarcode As String
        '= Dim colPrefs As Collection '== , colResult, colRecord As Collection
        '== Dim rect1 As Rectangle
        Dim textbox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textbox1.Parent

        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup Supplier--
            '--check if previous sale not committed..
            '-- NB: there is always one empty Grid row at the end waiting for new item.-
            '== With mFrmSale
            'If (clsDgvGoodsItems.Rows.Count > 1) Or ((clsDgvGoodsItems.Rows.Count = 1) And _
            '                 (clsDgvGoodsItems.Rows(0).Cells(k_GRIDCOL_BARCODE).Value <> "")) Then  '--not empty-  Then
            '    If MsgBox("Discard current invoice ?", _
            '          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
            '        Exit Sub
            '    End If
            '    '-- clear grid.-
            '    Call mbClearInvoice()
            'End If
            mIntSupplier_id = -1
            msSupplierName = ""
            '=3301.816=colPrefs = New Collection
            '=3301.816=
            '= colPrefs = mColPrefColumnsSuppliers   '== New Collection

            '=3501.1104--  Use FrmBrowse33..  for Search..
            If Not mbBrowseAndSearchTable(mColPrefColumnsSuppliers, "Lookup Supplier", "", colKeys, colSelectedRow, "Supplier") Then
                '= mbBrowseTable("Supplier", colPrefs, "Lookup Supplier", "", colKeys, colSelectedRow) Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else  '--selected-
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    '== MsgBox("Selected : " & colKeys(1))
                    mIntSupplier_id = CInt(colKeys(1))  '--save fkey as data..
                    '= Call mbSetupSupplier(colSelectedRow)
                    '-- get barcode and put in textbox..
                    sBarcode = colSelectedRow("barcode")("value")
                    textbox1.Text = sBarcode
                    '-- go to next fld- (force validation)
                    SendKeys.Send("{TAB}")
                    '==controlParent.SelectNextControl(textbox1, True, True, True, True)
                End If  '--keys-
            End If  '-browse- 
            '= End With '-frmsale-
        ElseIf (KeyCode = System.Windows.Forms.Keys.F3) And _
                            ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--SAME supplier--
            '-- nothing..
        ElseIf (KeyCode = System.Windows.Forms.Keys.F5) And _
                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--New supplier--
            '--F5-  New Supplier
            Dim frmEdit1 As frmEdit

            frmEdit1 = New frmEdit
            frmEdit1.connection = mCnnSql '--job tracking sql connenction..-
            frmEdit1.colTables = mColSqlDBInfo
            frmEdit1.DBname = msSqlDbName
            frmEdit1.tableName = "supplier" '--""
            '== frmEdit1.IsSqlServer = True '--bIsSqlServer
            frmEdit1.newRecord = True
            frmEdit1.StaffId = mIntStaff_id
            frmEdit1.StaffName = msStaffName
            frmEdit1.versionPOS = msVersionPOS

            frmEdit1.PreferredColumns = mColPrefColumnsSuppliers
            frmEdit1.Title = "Add New Supplier record"
            frmEdit1.ShowDialog()

            If Not frmEdit1.cancelled Then
                '-- get new barcode..
                txtSupplierBarcode.Text = frmEdit1.selectedBarcode
            End If  '-cancelled.

        End If '--keycode --
    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- SUPPLIER  Enter Pressed --

    Private Sub txtSupplierBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtSupplierBarcode.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1, sBarcode As String
        Dim sSql As String
        Dim colResult, colRecord, colField As Collection
        '==Dim v1 As Object
        Dim textbox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textbox1.Parent

        If keyAscii = 13 Then '--enter-
            '--Just use the validating event...
            '-- go to next fld-
            '= controlParent.SelectNextControl(textbox1, True, True, True, True)
            SendKeys.Send("{TAB}")
            'sBarcode = Trim(txtSupplierBarcode.Text)
            'If (sBarcode <> "") Then  '--have barcode-
            '    '--lookup barcode-
            '    '--  get recordset as collection for SELECT..--
            '    sSql = "SELECT * FROM [supplier] WHERE (barcode='" & sBarcode & "');"
            '    If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
            '                           (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
            '        '--have a row..-
            '        colRecord = colResult.Item(1)
            '        mIntSupplier_id = CInt(colRecord.Item("supplier_id")("value"))
            '        Call mbSetupSupplier(colRecord)
            '        '-- go to next fld-
            '        controlParent.SelectNextControl(textbox1, True, True, True, True)
            '    Else '--not found..-
            '        MsgBox("No supplier found for barcode: " & s1, MsgBoxStyle.Exclamation)
            '    End If  '-get--
            'End If  '--have barcode-
            keyAscii = 0
        End If  '--key ascii-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub  '--Supplier keypress=
    '= = = = = = = = = = = = = = = 

    '-Validating- in case of TAB key..

    Private Sub txtSupplierBarcode_Validating(ByVal sender As System.Object, _
                                               ByVal eventargs As CancelEventArgs) _
                                            Handles txtSupplierBarcode.Validating
        Dim s1, sBarcode As String
        Dim sSql As String
        Dim colResult, colRecord, colField As Collection
        sBarcode = Trim(txtSupplierBarcode.Text)
        If (sBarcode <> "") Then  '--have barcode-
            '--lookup barcode-
            '--  get recordset as collection for SELECT..--
            sSql = "SELECT * FROM [supplier] WHERE (barcode='" & sBarcode & "');"
            If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                   (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                '--have a row..-
                colRecord = colResult.Item(1)
                mIntSupplier_id = CInt(colRecord.Item("supplier_id")("value"))
                Call mbSetupSupplier(colRecord)
            Else '--not found..-
                eventargs.Cancel = True
                MsgBox("No supplier found for barcode: " & s1, MsgBoxStyle.Exclamation)
            End If  '-get--
        Else
            eventargs.Cancel = True
            MsgBox("No supplier barcode was entered..", MsgBoxStyle.Exclamation)
        End If  '--have barcode-
    End Sub '-txtSupplierBarcode_Validating-
    '= = = = = = = = = = = =  = = = = = = = =

    Private Sub txtSupplierBarcode_Validated(ByVal sender As System.Object, _
                                           ByVal eventargs As EventArgs) _
                                        Handles txtSupplierBarcode.Validated
        Dim textbox1 As TextBox = CType(sender, TextBox)
        Dim controlParent As Control = textBox1.Parent

        '-- go to next fld-
        '- DONE in setup Supplier !!
        '==controlParent.SelectNextControl(textbox1, True, True, True, True)

    End Sub '-txtSupplierBarcode_Validated-
    '= = = = = = =  = = = = = = = = = === 
    '-===FF->

    '-- Lookup Supplier--
    '-- Lookup Supplier--  GONE--

    'Private Sub btnLookupSupplier_Click(ByVal sender As System.Object, _
    '                                          ByVal e As System.EventArgs)
    '    Dim colPrefs, colKeys As Collection
    '    Dim colSelectedRow As Collection
    '    '== Dim sNameField As String = "supplierName"
    '    '== Dim sBarcodeField As String = "barcode"

    '    '==While (mIntStaffId <= 0)
    '    mIntSupplier_id = -1
    '    msSupplierName = ""
    '    '=3301.816=
    '    colPrefs = mColPrefColumnsSuppliers   '== New Collection

    '    If Not mbBrowseTable("Supplier", colPrefs, "Lookup Supplier", "", colKeys, colSelectedRow) Then
    '        MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
    '    Else '-ok-
    '        If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
    '            '== MsgBox("Selected : " & colKeys(1))
    '            mIntSupplier_id = CInt(colKeys(1))  '--save fkey as data..

    '            Call mbSetupSupplier(colSelectedRow)
    '        End If  '--keys- 
    '    End If  '--browse-
    'End Sub  '-btnLookupSupplier-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- GR only.
    '-- GR only.. Load PO related to this Goods nInvoice.

    Private Sub chkLoadPO_CheckedChanged(sender As Object, e As EventArgs) Handles chkLoadPO.CheckedChanged
        If chkLoadPO.Checked Then
            btnLoadPO.Enabled = True
        Else
            btnLoadPO.Enabled = False
        End If
    End Sub  '- chkLoadPO-
    '= = = = = = = = = = = =  =

    Private Sub chkloadPO_KeyPress(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles chkLoadPO.KeyPress

        '-chkPriceIncludesTax_KeyPress=

        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            If chkLoadPO.Checked Then
                btnLoadPO.Select()
            Else
                chkPriceIncludesTax.Select()
            End If

            '= labHelp.Text = "Enter Order Suffix.."
            '= txtOrderNoSuffix.Select()   '--focus-
        End If  '-13-

    End Sub  '-chkPriceIncludesTax_KeyPress-
    '= = = = = = = = = =  = = = = = = = = =

    '-- get all undelivered PO's for this supplier-
    '-- List them in listSelect  -  Get choice--

    Private Sub btnLoadPO_Click(sender As Object, e As EventArgs) Handles btnLoadPO.Click
        Dim dataTable1 As DataTable
        Dim index, intOrder_id As Integer

        If mbSelectPO(mIntSupplier_id, dataTable1, index) Then
            Call mbLoadGoodsPO(dataTable1, index)
            chkPriceIncludesTax.Select()
        Else
            MsgBox("No order selected !", MsgBoxStyle.Exclamation)
        End If  '-select

    End Sub  '--Load PO-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- chkPriceIncludesTax-

    Private Sub chkPriceIncludesTax_CheckedChanged(sender As Object, e As EventArgs) _
                                             Handles chkPriceIncludesTax.CheckedChanged

        '-- enable Cost_ex or cost_inc Grid columns depending..
        If chkPriceIncludesTax.Checked Then  '--can change cost_inc-
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_EX).ReadOnly = True
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_EX).DefaultCellStyle.BackColor = _
                                                                 System.Drawing.ColorTranslator.FromOle(&HE8E8E8)
            '-can edit cost_inc-
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).ReadOnly = False
        Else '-no-
            '-can change cost_ex only.
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_EX).ReadOnly = False
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).ReadOnly = True
            clsDgvGoodsItems.Columns(k_GRIDCOL_COST_INC).DefaultCellStyle.BackColor = _
                                                                 System.Drawing.ColorTranslator.FromOle(&HE8E8E8)
        End If
    End Sub  '-chkPriceIncludesTax=
    '= = = = = = = = = = = = = = = = =

    '-chkPriceIncludesTax_KeyPress=

    Private Sub chkPriceIncludesTax_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles chkPriceIncludesTax.KeyPress

        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            labHelp.Text = "Enter Order Suffix.."
            eventArgs.Handled = True
            txtOrderNoSuffix.Select()   '--focus-
        End If  '-13-

    End Sub  '-chkPriceIncludesTax_KeyPress-
    '= = = = = = = = = =  = = = = = = = = =
    '-===FF->

    '-- PO only..  Start NEW "Standard Order" PO..

    '= What needs to be ordered ??
    '-  Look through stock and current orders (This Supplier)-
    '--  for below-threshold levels not yet on order..
    '-- LOAD Grid with required order items..-

    Private Sub btnNewStandardOrder_Click(sender As Object, e As EventArgs) _
                                                  Handles btnNewStandardOrder.Click
        Dim dtStillOnOrder, dtNeeded As DataTable
        Dim sSql As String
        Dim dicOnOrder As New Dictionary(Of Integer, Integer)
        Dim dicItemsNeeded As New Dictionary(Of Integer, Integer)
        Dim intStock_id, intQty As Integer
        Dim sReport As String = "On Order:" & vbCrLf

        '- Make list of stuff still on order THIS SUPPLIER.
        sSql = "SELECT PurchaseOrder.order_id, order_date, orderNoSuffix, "
        sSql &= "  stock_id, POL.quantity, POL.qtyReceived "
        sSql &= "  FROM dbo.PurchaseOrderLine AS POL "
        '== sSql &= "  JOIN stock on stock.stock_id= POL.stock_id"
        sSql &= "     JOIN dbo.PurchaseOrder ON (POL.order_id=PurchaseOrder.order_id )"
        sSql &= " WHERE (PurchaseOrder.supplier_id=" & CStr(mIntSupplier_id) & ") "
        sSql &= "       AND (isCompleted=0)  AND (isClosedForBackorders=0)  AND (isCancelled=0) "
        sSql &= "       AND (qtyReceived < quantity)"
        sSql &= "  ORDER BY stock_id  "   '==  cat1, cat2 "
        If gbGetDataTable(mCnnSql, dtStillOnOrder, sSql) Then
            If (Not (dtStillOnOrder Is Nothing)) AndAlso (dtStillOnOrder.Rows.Count > 0) Then  '-something-
                '- load dictionary with on-order counts..-
                For Each row1 As DataRow In dtStillOnOrder.Rows
                    intStock_id = row1.Item("stock_id")
                    intQty = row1.Item("quantity") - row1.Item("qtyReceived") '-qty outstanding-
                    '- for debug-
                    '- add to dictionary of on-order stuff.-
                    If (intQty > 0) Then
                        If dicOnOrder.ContainsKey(intStock_id) Then  '- already have some.. add this lot.
                            '- have some.. add this lot.
                            dicOnOrder(intStock_id) = dicOnOrder(intStock_id) + intQty
                        Else '--add-
                            dicOnOrder.Add(intStock_id, intQty)
                        End If
                        sReport &= "Id_" & intStock_id & ":" & intQty & ";  "
                    End If '-qty-
                Next row1
            Else  '-empty-
            End If  '-count-
            '- ok.  now check stock table levels for stock needed..
            '-- UPDATED 20Apr2018-  3411.0420=
            '==    1. "reOrderLevel" (from RM "order_threshold") Means Minimum to hold in stock..
            '==         (Re-order when stock falls BELOW this level-)
            '==    2. "order_quantity" (from RM "order_quantity") Means MAXIMUm qty to hold in stock..
            '==          (Re-order sufficient to top up stock UP to this level-)
            sReport &= vbCrLf & "Gross to Order:" & vbCrLf
            sSql = "SELECT stock_id, description, qtyInStock, reOrderLevel, order_quantity "
            sSql &= "  FROM stock "
            sSql &= " WHERE (supplier_id=" & CStr(mIntSupplier_id) & ") "
            sSql &= "  AND (order_quantity >0) AND (qtyInStock < reOrderLevel) "
            sSql &= " ORDER BY cat1, cat2, description "
            '== sSql &= " ORDER BY stock_id "
            If gbGetDataTable(mCnnSql, dtNeeded, sSql) Then
                If (Not (dtNeeded Is Nothing)) AndAlso (dtNeeded.Rows.Count > 0) Then  '-something-
                    '-- build dic. of order qty's--
                    For Each row1 As DataRow In dtNeeded.Rows
                        intStock_id = row1.Item("stock_id")
                        '-- Order qty to TOP UP to MAX.. ie "order_quantity"-


                        '==  Target-New-Build-4253..
                        '==  Target-New-Build-4253..
                        '==
                        '==   A. -- Purchase Orders-  NewSpecialOrder.. 
                        '== DO NOT adjust order Qty for qtyInStock-  Use DB order_quantity..

                        '= intQty = row1.Item("order_quantity") - row1.Item("qtyInStock")  '-qty to order-
                        '== Target-New-Build-4267 --  intQty = row1.Item("order_quantity")  '= - row1.Item("qtyInStock")  '-qty to order-
                        '== END  Target-New-Build-4253..

                        '== Target-New-Build-4267 -- (Started 07-Sep-2020)
                        '== Target-New-Build-4267 -- (Started 07-Sep-2020)
                        '==   BACK to the FUTURE..

                        '== --  Purchase Orders- Auto  ordering..  
                        '==       (Martin 22/9/2020.)  Qty to order to only be enough to make stock back up to Max level.. ?  
                        '==  "order_quantity" is now the MAX stock to hold..
                        If (row1.Item("qtyInStock") >= 0) Then
                            intQty = row1.Item("order_quantity") - row1.Item("qtyInStock")  '-qty to order-
                        Else
                            '-  neg stock.. just order up to max..
                            intQty = row1.Item("order_quantity")
                        End If
 
                        '== END Target-New-Build-4267 -- (Started 07-Sep-2020)


                        '-- adjust for qty still on order-
                        If (intQty > 0) Then  '-can order this stock item.-
                            If dicOnOrder.ContainsKey(intStock_id) Then
                                '== intQty -= dicOnOrder(intStock_id)
                                '-- NO !!  IF any of this stock-id is still on order,
                                '--  THEN DON'T order it again !! -
                                intQty = 0  '- DON'T order it again !! -
                            End If
                        End If
                        If (intQty > 0) Then '--still need to order-
                            dicItemsNeeded.Add(intStock_id, intQty)
                            '- for debug-
                            sReport &= "Id_" & intStock_id & ":" & intQty & ";  "
                        End If
                    Next '-row1-
                Else '-no needed..-
                    MsgBox("Nothing needs to be ordered for this Supplier..", MsgBoxStyle.Information)
                    Exit Sub
                End If '-no needed.-
            Else  '--error=
                MsgBox("Error in getting recordset for Stock levels. " & vbCrLf & _
                                                gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
                Exit Sub
            End If  '--get needed.-
        Else
            MsgBox("Error in getting recordset for PurchaseOrderLine. " & vbCrLf & _
                                            gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '--get-

        '-- get results..-
        '== MsgBox("Debug Info: " & sReport, MsgBoxStyle.Information)
        If (dicItemsNeeded.Count > 0) Then  '-have something to order.-
            MsgBox("Have " & dicItemsNeeded.Count & " items to order..")
            '-- Fill grid with order details..-

            '-- see mbPreLoadOrderItems  --
            If Not mbPreLoadOrderItems(dicItemsNeeded) Then
                MsgBox("Failed to load PO grid..", MsgBoxStyle.Exclamation)
            End If
        End If
        txtOrderNoSuffix.Enabled = True
        mbIsNewPO = True
        btnViewPO.Enabled = False
        btnNewBlankPO.Enabled = False
        btnNewStandardOrder.Enabled = False

        panelIncludes.Enabled = False
        txtOrderNoSuffix.Select()

    End Sub  '-new Standard PO-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- PO only..
    '--  NewBlankPO -

    Private Sub btnNewBlankPO_Click(sender As Object, e As EventArgs) Handles btnNewBlankPO.Click


        btnViewPO.Enabled = False
        btnNewBlankPO.Enabled = False
        btnNewStandardOrder.Enabled = False
        txtOrderNoSuffix.Enabled = True
        mbIsNewPO = True
        panelIncludes.Enabled = False

        txtOrderNoSuffix.Select()

    End Sub '-new blank PO-
    '= = = = = = = = = = = = 

    '-- PO only..  View an existing PO..
    '-- PO only..  View an existing PO..  Print ???

    '-- get all undelivered PO's for this supplier-
    '-- List them in DGV PO's  -  Get choice--

    Private Sub btnViewPO_Click(sender As Object, e As EventArgs) _
                                 Handles btnViewPO.Click
        Dim dataTable1 As DataTable
        Dim index, intOrder_id As Integer

        mbIsNewPO = False
        If mbSelectPO(mIntSupplier_id, dataTable1, index) Then
            '== Dim dataRow1 As DataRow = dataTable1.Rows(0)
            Dim dataRow1 As DataRow = dataTable1.Rows(index)   '==3411.0423=
            intOrder_id = dataRow1.Item("order_id")
            mIntOurOrder_id = intOrder_id

            btnNewStandardOrder.Enabled = False
            btnViewPO.Enabled = False
            btnNewBlankPO.Enabled = False

            '- SAVE date PO created !!!
            mDatePO_date_created = CDate(dataRow1.Item("order_date"))

            '--  get/load all lines -

            If Not mbGetOrderDetails(intOrder_id, True) Then  '-- Show All..--
                MsgBox("No PO details to show.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            txtOrderNoSuffix.Text = dataRow1.Item("orderNoSuffix")
            labPO_id.Text = CStr(intOrder_id)

            txtGoodsComments.Text = dataRow1.Item("comments")
            '= txtGoodsDeliveryAddress.Text = dataRow1.Item("delivery_address")
            msCreatedByStaffName = dataRow1.Item("docket_name")

            '-- Set barcode, sup-code, qty cols to Read-only--
            clsDgvGoodsItems.Columns(k_GRIDCOL_BARCODE).ReadOnly = True
            clsDgvGoodsItems.Columns(k_GRIDCOL_SUP_CODE).ReadOnly = True
            clsDgvGoodsItems.Columns(k_GRIDCOL_QTY).ReadOnly = True

            clsDgvGoodsItems.Enabled = True   '- so he can scroll-
            panelGoodsFooter.Enabled = False
            btnGoodsCommit.Enabled = False

            panelPrinting.Enabled = True
            btnPrint.Enabled = True
            picEmailPO.Enabled = True
            btnCancelPO.Enabled = True
        Else
            MsgBox("Nothing selected !", MsgBoxStyle.Exclamation)
        End If  '-selected-

    End Sub  '-View PO-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    Private Sub txtOrderNoSuffix_TextChanged(sender As Object, _
                                              e As EventArgs) Handles txtOrderNoSuffix.TextChanged
        If mbIsInitialising Then Exit Sub

        If mbIsPurchaseOrder Then
            panelStockLineEntry.Enabled = True
            clsDgvGoodsItems.Enabled = True
            panelGoodsFooter.Enabled = True
        End If

    End Sub '-suffix-
    '= = = = = = = = =  =

    '-- OUR  InvoiceNo  Enter Pressed --

    Private Sub txtOrderNoSuffix_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtOrderNoSuffix.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            eventArgs.Handled = True
            If mbIsPurchaseOrder Then
                labHelp.Text = "Enter PO Items..."
                txtStockItemBarcode.Select()    '--focus-
            Else '--goods-
                labHelp.Text = "Select Invoice date.."
                '= dtPickerInvoiceDate.Select()   '--focus-
                txtSupplierInvoiceNo.Select()
            End If  '-po-
            '= SendKeys.Send("{TAB}")
        End If  '-13-
    End Sub '-txtOrderNoSuffix-keypress-
    '= = = = =  = = = = = = = = = = ==
    '-===FF->

    '-- catch TAB and ENTER so we can process in flight.

    Private Sub txtSupplierInvoiceNo_PreviewKeyDown(ByVal sender As Object, _
                                                      ByVal e As PreviewKeyDownEventArgs) _
                                                   Handles txtSupplierInvoiceNo.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Enter, Keys.Escape, Keys.Tab     '= Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub '-PreviewKeyDown-
    '= = = = = = = = = = == =


    '-- Textbox Enter control for txtSupplierInvoiceNo.

    Private Sub txtSupplierInvoiceNo_Enter(sender As Object, _
                                           ev As System.EventArgs) _
                                       Handles txtSupplierInvoiceNo.Enter, txtSupplierInvoiceNo.Click
        If mbIsInitialising Then Exit Sub

        If txtSupplierInvoiceNo.Text = "" Then
            txtSupplierInvoiceNo.Text = "InvoiceNo"
        End If
        txtSupplierInvoiceNo.SelectionStart = 0
        txtSupplierInvoiceNo.SelectionLength = Len(txtSupplierInvoiceNo.Text)

    End Sub  '--Enter-
    '= = = = = = =  = = = = 

    '-txtSupplierInvoiceNo_KeyDown-

    Private Sub txtSupplierInvoiceNo_KeyDown(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                            Handles txtSupplierInvoiceNo.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtInvoiceNo As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim sData As String = Trim(txtInvoiceNo.Text)
        If ((KeyCode = System.Windows.Forms.Keys.Tab) And _
                       ((Not eventArgs.Shift) And (Not eventArgs.Control))) Or _
                         (KeyCode = System.Windows.Forms.Keys.Enter) Then '--same as ENTER--
            '--wants to end edit and go fwds..
            '-- So validate.
            eventArgs.Handled = True
            If (sData = "") Or (LCase(sData) = "invoiceno") Then
                MsgBox("Please enter the Supplier's Invoice No.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            '-ok-
            dtPickerInvoiceDate.Select()
        End If  '-key code

    End Sub '-txtSupplierInvoiceNo_KeyDown-
    '= = = = = = = = =  = = = = = = == = 

    '-txtSupplierInvoiceNo_TextChanged-

    '-  mandatory fld..-

    Private Sub txtSupplierInvoiceNo_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles txtSupplierInvoiceNo.TextChanged
        If mbIsInitialising Then Exit Sub
        chkLoadPO.Enabled = False
        btnLoadPO.Enabled = False
        chkPriceIncludesTax.Enabled = False

        clsDgvGoodsItems.Enabled = True
        panelGoodsFooter.Enabled = True
        btnGoodsContinue.Enabled = True
        panelStockLineEntry.Enabled = True
        '== dgvGoodsItems.Select()   '--focus-
    End Sub  '-txtSupplierInvoiceNo_TextChanged-
    '= = = = = = = = = = = = =  = = = = = = = = =

    '-- SUPPLIER InvoiceNo  Enter Pressed --

    'Private Sub txtSupplierInvoiceNo_KeyPress(ByVal eventSender As System.Object, _
    '                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
    '                                       Handles txtSupplierInvoiceNo.KeyPress
    '    Dim keyAscii As Short = Asc(eventArgs.KeyChar)
    '    Dim sData As String = Trim(txtSupplierInvoiceNo.Text)

    '    If keyAscii = 13 Then '--enter-
    '        keyAscii = 0
    '        eventArgs.Handled = True
    '        SendKeys.Send("{TAB}")
    '        'If sData = "" Then
    '        '    MsgBox("Must have supplier Invoice No..", MsgBoxStyle.Exclamation)
    '        'Else
    '        '    '-- select next control.
    '        '    If btnGoodsContinue.Enabled Then
    '        '        btnGoodsContinue.Select()   '--focus-
    '        '    Else
    '        '        labHelp.Text = "Enter Stock Barcodes.."
    '        '        '= clsDgvGoodsItems.Select()   '--focus-
    '        '        txtStockItemBarcode.Select()
    '        '    End If
    '        'End If
    '    End If  '-13-
    'End Sub  '--txtSupplierInvoiceNo_KeyPress-
    '= = = = = = = = = = = = = = = = == =

    'Private Sub txtSupplierInvoiceNo_Validating(ByVal sender As System.Object, ByVal eventargs As CancelEventArgs) _
    '                                                                           Handles txtSupplierInvoiceNo.Validating
    '    Dim sData As String = Trim(txtSupplierInvoiceNo.Text)

    '    If sData = "" Then
    '        eventargs.Cancel = True
    '        MsgBox("Must have supplier Invoice No..", MsgBoxStyle.Exclamation)
    '    End If
    'End Sub  '--validating-
    '= = = = =  = = = = = == 

    'Private Sub txtSupplierInvoiceNo_Validated(ByVal sender As System.Object, ByVal eventargs As System.EventArgs) _
    '                                                                           Handles txtSupplierInvoiceNo.Validated
    '    Dim sData As String = Trim(txtSupplierInvoiceNo.Text)

    '    panelStockLineEntry.Enabled = True
    '    labHelp.Text = "Enter Stock Barcodes.."
    '    'If btnGoodsContinue.Enabled Then
    '    '    btnGoodsContinue.Select()   '--focus-
    '    'Else
    '    '    '= clsDgvGoodsItems.Select()   '--focus-
    '    '    txtStockItemBarcode.Select()
    '    'End If
    'End Sub  '--validating-
    '-===FF->


    '== - 4201.1013.  13-October-2019-  UPDATED TAB INDEXES..
    '-dtPickerInvoiceDate-
    '-- catch TAB key and re-check for Supplier Invoice No.

    Private Sub dtPickerInvoiceDate_PreviewkeyDown(ByVal sender As Object, _
                                                      ByVal ev As PreviewKeyDownEventArgs) _
                                                        Handles dtPickerInvoiceDate.PreviewKeyDown
        Select Case (ev.KeyCode)
            Case Keys.Enter, Keys.Escape, Keys.Tab     '= Keys.Down, Keys.Up
                ev.IsInputKey = True
        End Select
    End Sub  '-dtPickerInvoiceDate_PreviewkeyDown-
    '= = = = = = = = =  = = = == = = = = = = = =

    Private Sub dtPickerInvoiceDate_keyDown(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                                     Handles dtPickerInvoiceDate.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim sInvoiceData As String = Trim(txtSupplierInvoiceNo.Text)
        If ((KeyCode = System.Windows.Forms.Keys.Tab) And _
                       ((Not eventArgs.Shift) And (Not eventArgs.Control))) Or _
                         (KeyCode = System.Windows.Forms.Keys.Enter) Then '--same as ENTER--
            If (sInvoiceData = "") Or _
                    (LCase(sInvoiceData) = "invoiceno") Then
                MsgBox("Please enter the Supplier's Invoice No.", MsgBoxStyle.Exclamation)
                eventArgs.Handled = True
                txtSupplierInvoiceNo.Select()
            ElseIf btnGoodsContinue.Enabled Then
                btnGoodsContinue.Select()
                eventArgs.Handled = True
            End If
        End If  '-keycode-

    End Sub '-dtPickerInvoiceDate_keyDown-
    '= = = = = = = = = = = = = = === = = 

    '-dtPickerInvoiceDate_KeyPress-

    Private Sub dtPickerInvoiceDate_KeyPress(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                        Handles dtPickerInvoiceDate.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            keyAscii = 0
            labHelp.Text = "Enter Supplier Invoice No...."
            eventArgs.Handled = True
            SendKeys.Send("{TAB}")
            '= txtSupplierInvoiceNo.Select()
        End If  '-13-
    End Sub  '-dtPickerInvoice_KeyPress-
    '= = = = = = = = =  = = = = = = = =
    '-===FF->

    '-btnGoodsContinue-

    Private Sub btnGoodsContinue_PreviewKeyDown(ByVal sender As Object, _
                                                      ByVal ev As PreviewKeyDownEventArgs) _
                                                        Handles btnGoodsContinue.PreviewKeyDown
        Select Case (ev.KeyCode)
            Case Keys.Enter, Keys.Tab     '=Keys.Escape, Keys.Down, Keys.Up
                ev.IsInputKey = True
        End Select
    End Sub '-btnGoodsContinue_PreviewKeyDown-
    '= = = = = = = = = = = = = == =  == = = =

    Private Sub btnGoodsContinue_KeyDown(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                              Handles btnGoodsContinue.KeyDown
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
        Dim sInvoiceData As String = Trim(txtSupplierInvoiceNo.Text)
        If ((KeyCode = System.Windows.Forms.Keys.Tab) And _
                       ((Not EventArgs.Shift) And (Not EventArgs.Control))) Or _
                         (KeyCode = System.Windows.Forms.Keys.Enter) Then '--same as ENTER--
            If (sInvoiceData = "") Or _
                    (LCase(sInvoiceData) = "invoiceno") Then
                MsgBox("Please enter the Supplier's Invoice No.", MsgBoxStyle.Exclamation)
                EventArgs.Handled = True
                txtSupplierInvoiceNo.Select()
            Else  '-ok-
                '-- select next control.
                btnGoodsContinue.Enabled = False
                '= clsDgvGoodsItems.Select()   '--focus-
                txtStockItemBarcode.Select()
            End If  '-data-
        End If  '-keycode-

    End Sub '-key down.-
    '= = = = = = ==  == 

    Private Sub btnGoodsContinue_Click(sender As Object, e As EventArgs) Handles btnGoodsContinue.Click

        Dim sInvoiceData As String = Trim(txtSupplierInvoiceNo.Text)

        If (sInvoiceData = "") Or _
                      (LCase(sInvoiceData) = "invoiceno") Then
            MsgBox("Please enter the Supplier's Invoice No.", MsgBoxStyle.Exclamation)
            txtSupplierInvoiceNo.Select()
            Exit Sub
        End If  '-data-

        '-- select next control.
        btnGoodsContinue.Enabled = False
        '= clsDgvGoodsItems.Select()   '--focus-
        txtStockItemBarcode.Select()

    End Sub  '--goods Continue-
    '= = = = = = =  = = ==  = == 

    Private Sub btnGoodsContinue_KeyPress(sender As Object, _
                                          EventArgs As KeyPressEventArgs) Handles btnGoodsContinue.KeyPress
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)
        Dim sinvoiceData As String = Trim(txtSupplierInvoiceNo.Text)

        If keyAscii = 13 Then '--enter-
            If (sInvoiceData = "") Or _
                          (LCase(sInvoiceData) = "invoiceno") Then
                MsgBox("Please enter Supplier's Invoice No.", MsgBoxStyle.Exclamation)
                EventArgs.Handled = True
                txtSupplierInvoiceNo.Select()
                Exit Sub
            End If  '-data-
            labHelp.Text = "Enter Stock Barcodes.."
            keyAscii = 0
            EventArgs.Handled = True
            '= clsDgvGoodsItems.Select()   '--focus-
            txtStockItemBarcode.Select()
        End If  '-13- 
    End Sub  '-keypress-
    '= = = = =  = = = = = == 
    '-===FF->

    '--  Barcode Entry and Lookup..
    '--  Barcode Entry and Lookup..
    '--  Barcode Entry and Lookup..


    Private Sub txtStockItemBarcode_TextChanged(sender As Object, _
                                                 ev As EventArgs) Handles txtStockItemBarcode.TextChanged
        If mbIsInitialising Then Exit Sub

    End Sub  '-txtStockItemBarcode_TextChanged-
    '= = = = = =  = = = = = = = = = = = = 


    '-- Textbox Enter control for Item barcode.

    Private Sub txtStockItemBarcode_Enter(sender As Object, _
                                           ev As System.EventArgs) Handles txtStockItemBarcode.Enter
        If mbIsInitialising Then Exit Sub

        txtStockItemSupplierCode.Text = ""

        If txtStockItemBarcode.Text = "" Then
            txtStockItemBarcode.Text = "Barcode"
        End If
        txtStockItemBarcode.SelectionStart = 0
        txtStockItemBarcode.SelectionLength = Len(txtStockItemBarcode.Text)

    End Sub '-txtSaleItemBarcode_Enter-

    '==-- 15-Apr-2018- POS Sale- Catch Mouse-Click on Item Barcode to set Selection stuff....

    Private Sub txtStockItemBarcode_Click(sender As Object, _
                                          ev As System.EventArgs) Handles txtStockItemBarcode.Click

        If mbIsInitialising Then Exit Sub
        txtStockItemBarcode.SelectionStart = 0
        txtStockItemBarcode.SelectionLength = Len(txtStockItemBarcode.Text)

    End Sub  '-txtSaleItemBarcode_Click-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Static dataTable with current stock Item.
    Private mDataTableStockSelected As DataTable

    '-- Stock barcode validation.
    '-- Returns false if canceleld or invalid..
    '==
    '==   Updated.- 3519.0207 07-Feb-2019= 
    '==     -- Fixes to GoodsReceived to strip leading zeroes from scanned barcode id=f necessary.-
    '==

    Private Function mbStockBarcodeValidate(ByVal sInputBarcode As String, _
                                            ByRef sFinalBarcode As String) As Boolean
        Dim s1, sBarcode, sSql, sSerialNo, sError, sSerialTrail As String
        Dim bOk As Boolean = False
        Dim bCancelled As Boolean = False
        Dim intStock_id As Integer
        Dim bHasLeadZeroes As Boolean = False
        '= Dim bDoneLookUp As Boolean = False

        sBarcode = sInputBarcode
        mbStockBarcodeValidate = False
        bHasLeadZeroes = (VB.Left(sInputBarcode, 1) = "0")
        sBarcode = sInputBarcode
        '-- start with stripping leading zeroes..
        While (VB.Left(sBarcode, 1) = "0")
            sBarcode = Mid(sBarcode, 2)
        End While
        '--lookup barcode-
        '=3301.816== Give user the chance to update stock table-
        Dim result1 As MsgBoxResult = MsgBoxResult.Yes
        Dim frmStock1 As frmStock
        Do
            sSql = "SELECT *, supcode FROM dbo.stock "
            sSql &= "  LEFT OUTER JOIN dbo.SupplierCode AS SC "
            sSql &= "      ON (SC.supplier_id=" & CStr(mIntSupplier_id) & ") "
            sSql &= "           AND (SC.stock_id=stock.stock_id) "
            sSql &= "WHERE  (barcode='" & sBarcode & "');"

            If gbGetDataTable(mCnnSql, mDataTableStockSelected, sSql) AndAlso _
           (Not (mDataTableStockSelected Is Nothing)) AndAlso (mDataTableStockSelected.Rows.Count > 0) Then
                '- FOUND- have a row..-
                '- validated can finish..
                bCancelled = False   '=ev.Cancel = False   '--ok to go-
                result1 = MsgBoxResult.No  '--And drop out of loop..
            Else '--not found..-
                If bHasLeadZeroes And (VB.Left(sBarcode, 1) <> "0") Then  '-failed stripped version
                    sBarcode = sInputBarcode   '--try original unstripped..
                    result1 = MsgBoxResult.Yes  '-for retry.
                Else  '- no lead zeroes, or failed raw version.
                    result1 = MsgBox("No Stock record found for barcode: '" & sBarcode & "' !" & vbCrLf & _
                                    "Do you want to add this stock item?", _
                                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question)
                    If (result1 = MsgBoxResult.Yes) Then '- wants to add item.
                        '--call stock admin-
                        '=  Call btnStockAdmin_Click(btnStockAdmin, New EventArgs)
                        frmStock1 = New frmStock
                        frmStock1.StaffName = msStaffName
                        frmStock1.SqlServer = msServer
                        frmStock1.connectionSql = mCnnSql '--job tracking sql connenction..-
                        frmStock1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..

                        frmStock1.DBname = msSqlDbName
                        frmStock1.VersionPOS = msVersionPOS
                        '=frmBrowse1.tableName = sTableName '--"jobs"
                        '== frmBrowse1.IsSqlServer = True '--bIsSqlServer
                        frmStock1.form_left = Me.Left
                        frmStock1.form_top = Me.Top + 70
                        '- new item needed.
                        frmStock1.AddNewStockOnly = True
                        frmStock1.AddNewStockBarcode = sBarcode
                        frmStock1.AddNewStockSupplier_id = mIntSupplier_id
                        frmStock1.AddNewStockSupplierName = txtSupplierName.Text
                        frmStock1.ShowDialog()
                        If Not frmStock1.wasCancelled Then
                            '-was added..
                            '-- and loop around to do the lookup again.
                        Else
                            '-cancelled-
                            bCancelled = True   '=ev.Cancel = True
                            MsgBox("No Stock record found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
                            '= Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "No Stock record found for barcode: " & sBarcode
                            result1 = MsgBoxResult.No  '--And drop out of loop..
                        End If
                        frmStock1.Close()
                    Else  '-no- so -post error and exit
                        bCancelled = True   '= ev.Cancel = True
                        MsgBox("No Stock record found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
                    End If
                End If  '--zeroes-
            End If  '-get--
        Loop Until (result1 <> MsgBoxResult.Yes)
        If Not bCancelled Then
            sFinalBarcode = sBarcode
            mbStockBarcodeValidate = True
        End If
    End Function  '== barcode validate-
    '= = = = = = = = = =  = = = = = = = = =
    '-===FF->

    '-- Stock Item Search (F2)..--

    '-- Grid TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for STOCK Lookup--

    Private Sub txtStockItemBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                       Handles txtStockItemBarcode.KeyDown
        If mbIsInitialising Then Exit Sub

        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sBarcode, sBarcode0, sSql, s1, sErrorMsg As String
        Dim intStock_id As Integer
        '= Dim gridrow1 As DataGridViewRow
        Dim colPrefsStock As Collection = mColPrefColumnsStock  '=3301.816=

        intStock_id = -1
        msSupplierName = ""
        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup stock--
            If Not mbBrowseAndSearchTable(colPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, "stock") Then
                '= mbBrowseTable("Stock", colPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, True) Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else '-ok-
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    '== MsgBox("Selected : " & colKeys(1))
                    intStock_id = CInt(colKeys(1))  '--save pkey as data..
                    If colSelectedRow.Contains("barcode") Then
                        sBarcode0 = colSelectedRow.Item("barcode")("value")
                    Else
                        MsgBox("No value in selected row for: barcode..  ", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                    sBarcode = Replace(sBarcode0, "'", "")  ''--strip quotes-
                    If (sBarcode <> "") Then
                        '-- get selected stock details..-
                        '== sSql = "SELECT * FROM [stock] WHERE (barcode='" & sBarcode & "');"
                        sSql = "SELECT *, supcode FROM dbo.stock "
                        sSql &= "  LEFT OUTER JOIN dbo.SupplierCode AS SC "
                        sSql &= "      ON (SC.supplier_id=" & CStr(mIntSupplier_id) & ") "
                        sSql &= "           AND (SC.stock_id=stock.stock_id) "
                        sSql &= "WHERE  (barcode='" & sBarcode & "');"
                        If gbGetDataTable(mCnnSql, mDataTableStockSelected, sSql) Then
                            If (Not (mDataTableStockSelected Is Nothing)) AndAlso (mDataTableStockSelected.Rows.Count > 0) Then
                                '-- stuff selected barcode into grid's  textbox..
                                txtBarcode.Text = mDataTableStockSelected.Rows(0).Item("barcode")
                                '-- show description.. allow OK..
                                txtStockItemDescription.Text = mDataTableStockSelected.Rows(0).Item("description")
                                btnStockLineOk.Enabled = True
                                labHelp2.Text = ""
                                DoEvents()
                                btnStockLineOk.Select()
                            Else  '=If (dataTable1.Rows.Count <= 0) Then
                                MsgBox("No Stock data row returned for barcode: " & sBarcode & _
                                            vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                            End If  '-nothing-
                        Else
                            sErrorMsg = gsGetLastSqlErrorMessage()
                            MsgBox("ERROR: No Stock datatable returned for barcode: " & sBarcode & _
                                      vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                            '== End If
                        End If '--get table-
                    Else
                        MsgBox("ERROR: Invalid stock barcode: <<" & sBarcode0 & ">>", MsgBoxStyle.Exclamation)
                    End If  '--barcode-
                End If  '--keys-
            End If '-browse-
        ElseIf (KeyCode = System.Windows.Forms.Keys.F5) And _
                          ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '-ADD NEW stock (Autogen barcode.)--
            '--call stock admin-
            '-ADD NEW stock (Autogen barcode.)--
            Dim frmStock1 As frmStock
            '=  Call btnStockAdmin_Click(btnStockAdmin, New EventArgs)
            frmStock1 = New frmStock
            frmStock1.StaffName = msStaffName
            frmStock1.SqlServer = msServer
            frmStock1.connectionSql = mCnnSql '--job tracking sql connenction..-
            frmStock1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
            frmStock1.DBname = msSqlDbName
            frmStock1.VersionPOS = msVersionPOS
            '=frmBrowse1.tableName = sTableName '--"jobs"
            '== frmBrowse1.IsSqlServer = True '--bIsSqlServer
            frmStock1.form_left = Me.Left
            frmStock1.form_top = Me.Top + 70
            '- new item needed.
            frmStock1.AddNewStockOnly = True
            frmStock1.AddNewStockAutoGenBarcode = True
            '= frmStock1.AddNewStockBarcode = sBarcode
            frmStock1.AddNewStockSupplier_id = mIntSupplier_id
            frmStock1.AddNewStockSupplierName = txtSupplierName.Text
            frmStock1.ShowDialog()
            If Not frmStock1.wasCancelled Then
                '-was added..
                '-- Get New ID and Lookkup new Stock Record..
                intStock_id = frmStock1.NewStock_id
                Thread.Sleep(100)  '--wait 0.1 second for new record to settle.
                '--lookup barcode-
                sSql = "SELECT *, supcode FROM dbo.stock "
                sSql &= "  LEFT OUTER JOIN dbo.SupplierCode AS SC "
                sSql &= "      ON (SC.supplier_id=" & CStr(mIntSupplier_id) & ") "
                sSql &= "           AND (SC.stock_id=stock.stock_id) "
                sSql &= "WHERE  (stock.stock_id=" & intStock_id & ");"
                If gbGetDataTable(mCnnSql, mDataTableStockSelected, sSql) Then
                    '-ok-
                    If (Not (mDataTableStockSelected Is Nothing)) AndAlso (mDataTableStockSelected.Rows.Count > 0) Then
                        '- got new record.
                        txtBarcode.Text = mDataTableStockSelected.Rows(0).Item("barcode")  '-get created barcode.
                        '-- go to validating.
                        txtStockItemDescription.Text = mDataTableStockSelected.Rows(0).Item("description")
                        btnStockLineOk.Enabled = True
                        labHelp2.Text = ""
                        DoEvents()
                        btnStockLineOk.Select()
                    Else  '=If (dataTable1.Rows.Count <= 0) Then
                        MsgBox("ERROR- No Stock data row returned for Stock_id: " & intStock_id & _
                                    vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                    End If  '-get=
                Else '--failed-
                    sErrorMsg = gsGetLastSqlErrorMessage()
                    MsgBox("ERROR- FAILED to Get Stock data row for Stock_id: " & intStock_id & _
                                vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                End If  '-get-
            Else
                '-cancelled-
                '= bCancelled = True   '=ev.Cancel = True
                MsgBox("No New Stock record was Created..", MsgBoxStyle.Exclamation)
                '= Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "No Stock record found for barcode: " & sBarcode
                '== result1 = MsgBoxResult.No  '--And drop out of loop..
            End If  '-cancelled-
            frmStock1.Close()
        ElseIf (KeyCode = System.Windows.Forms.Keys.F10) And _
                          ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--goto commit--
            If btnGoodsCommit.Enabled Then
                btnGoodsCommit.Select()
            End If
        End If  '-keycode.
    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- Handle ENTER for all Line Item textboxes..
    '--   txtSaleItemBarcode-  Enter Pressed --

    Private Sub txtStockItemBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtStockItemBarcode.KeyPress
        If mbIsInitialising Then Exit Sub

        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        Dim sData As String = Trim(textBox1.Text)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1, sBarcode, sFinalBarcode, sSq As String

        If (keyAscii = 13) Then '--enter-
            '--  If this is barcode, then check if valid etc...-
            If (LCase(textBox1.Name) = "txtstockitembarcode") Then
                keyAscii = 0
                eventArgs.Handled = True
                '-- go to next fld-
                '--  Just use validating--
                '= controlParent.SelectNextControl(textBox1, True, True, True, True)

                SendKeys.Send("{TAB}")
                'If LCase(sBarcode) <> "barcode" And (sBarcode <> "") Then '-have a barcode-
                '    '--  Must validate here--
                '    If Not mbStockBarcodeValidate(sBarcode) Then
                '        MsgBox("ERROR: Invalid stock barcode: <<" & sBarcode & ">>", MsgBoxStyle.Exclamation)
                '    Else '--ok-
                '        txtStockItemDescription.Text = mDataTableStockSelected.Rows(0).Item("description")
                '        btnStockLineOk.Enabled = True
                '        btnStockLineOk.Select()
                '    End If
                'Else  '--empty-
                '    '- no commit for view PO..-
                '    If (mbIsPurchaseOrder And mbIsNewPO) And btnGoodsCommit.Enabled Then
                '        btnGoodsCommit.Select()
                '    ElseIf (Not mbIsPurchaseOrder) And panelGoodsFooter.Enabled Then
                '        txtTotalExpected.Select()
                '    End If
                'End If  '-have barcode-
                '= SendKeys.Send("{TAB}")
            End If  '-barcode-
        End If  '-13-
    End Sub  '-txtSaleItemBarcode_KeyPress-
    '= = = = = = = =  = = = = = = = = == == 
    '-===FF->

    '-- Handle Validating for Line Item barcode..

    Private Sub txtSaleItemBarcode_Validating(ByVal sender As System.Object, _
                                              ByVal ev As CancelEventArgs) _
                                                 Handles txtStockItemBarcode.Validating
        If mbIsInitialising Then Exit Sub

        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        Dim s1, sBarcode, sFinalBarcode, sSql, sSerialNo, sError, sSerialTrail As String
        Dim bOk As Boolean = False
        Dim intStock_id As Integer

        sError = ""
        '--  If this is barcode, then check if valid etc...-
        If (LCase(textBox1.Name) = "txtstockitembarcode") Then
            '--ENTER or TAB was pressed...
            sBarcode = sData
            If LCase(sBarcode) <> "barcode" And (sBarcode <> "") Then '-have a barcode-
                If Not mbStockBarcodeValidate(sBarcode, sFinalBarcode) Then
                    ev.Cancel = True
                    MsgBox("ERROR: Invalid stock barcode: <<" & sBarcode & ">>", MsgBoxStyle.Exclamation)
                Else '--ok-
                    btnStockLineOk.Enabled = True
                    textBox1.Text = sFinalBarcode  '- in case striiped.
                    btnStockLineOk.Select()
                End If
            Else  '--empty-  
                '- validated can move it on.
                '=txtTotalExpected.Select()
            End If  '-have barcode- 
        End If  '--barcode-

    End Sub  '--txtSaleItemBarcode_Validating-
    '= = = = = = = = = = = = = = = = = = = == 

    '==3307.0218 =
    '-- Handle txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..

    Private Sub txtStockItemBarcode_Validated(ByVal sender As System.Object, _
                                              ByVal ev As System.EventArgs) _
                                                 Handles txtStockItemBarcode.Validated
        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        Dim s1, sBarcode As String

        If mbIsInitialising Then Exit Sub
        sBarcode = sData
        If LCase(sBarcode) = "barcode" Or (sBarcode = "") Then '-empty barcode-
            If (mbIsPurchaseOrder And mbIsNewPO) And btnGoodsCommit.Enabled Then
                btnGoodsCommit.Select()
            ElseIf (Not mbIsPurchaseOrder) And panelGoodsFooter.Enabled Then
                txtTotalExpected.Select()
            End If
        Else  '-have barcode-
            If (mDataTableStockSelected IsNot Nothing) Then
                txtStockItemDescription.Text = mDataTableStockSelected.Rows(0).Item("description")
                '== NEW STUFF-
                '==    -- 4201.0519.  Purchase Orders to have txtStockItemSupplierCode textbox, 
                '==          and update of SupplierCode Table for New SupplierCode.....
                If Not IsDBNull(mDataTableStockSelected.Rows(0).Item("supcode")) Then
                    txtStockItemSupplierCode.Text = mDataTableStockSelected.Rows(0).Item("supcode")
                Else
                    '- no supplier code on file..
                    txtStockItemSupplierCode.Text = ""
                End If
                If txtStockItemSupplierCode.Enabled Then
                    txtStockItemSupplierCode.Select()
                Else  '--goods received-
                    btnStockLineOk.Enabled = True
                    btnStockLineOk.Select()
                End If
            End If
        End If  '-barcode-

    End Sub  '--txtSaleItemBarcode_Validated-
    '= = = = = = = = = = = = = = = = = = = ==  = = = = = = 
    '-===FF->

    '--supplier Code.. Enter-
    Private Sub txtStockItemSupplierCode_enter(sender As Object, _
                                                      ev As EventArgs) Handles txtStockItemSupplierCode.Enter

    End Sub  '--supplier Code..  entry.
    '= = = = = = =  = = = = = = = = = =

    '--supplier Code..
    Private Sub txtStockItemSupplierCode_TextChanged(sender As Object, _
                                                      ev As EventArgs) Handles txtStockItemSupplierCode.TextChanged

    End Sub  '--supplier Code..
    '= = = = = = =  = = = = = =

    '--ENTER key-  do a TAB.
    Private Sub txtStockItemSupplierCode_keyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                             Handles txtStockItemSupplierCode.KeyPress
        If mbIsInitialising Then Exit Sub

        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If (keyAscii = 13) Then '--enter-
            eventArgs.Handled = True
            '-- go to next fld-
            '--  Just use validating--
            '= controlParent.SelectNextControl(textBox1, True, True, True, True)

            SendKeys.Send("{TAB}")
        End If
    End Sub  '-keyPress-
    '= = = = = = = = = = =

    '--supplier Code..VALIDATED-

    Private Sub txtStockItemSupplierCode_validated(sender As Object, _
                                                      ev As EventArgs) Handles txtStockItemSupplierCode.Validated
        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        Dim s1, sSupCode, sBarcode As String

        '- if no supplier code, then insert local barcode. (unless it's too long.)
        If mbIsInitialising Then Exit Sub
        sSupCode = sData
        sBarcode = Trim(txtStockItemBarcode.Text)
        If (sSupCode = "") Then
            If (Len(sBarcode) <= 40) Then '=4201.0717=  (Len(sBarcode) <= 15) Then
                textBox1.Text = sBarcode
            End If
        End If

        btnStockLineOk.Enabled = True
        btnStockLineOk.Select()

    End Sub  '--supplier Code..
    '= = = = = = =  = = = = = =
    '-===FF->

    '-- btnStockLineOk_Click --

    '-- Add New Item to Grid..

    Private Sub btnStockLineOk_Click(sender As Object, _
                                     ev As EventArgs) Handles btnStockLineOk.Click
        Dim sBarcode, sOriginalSupcode, sNewSupcode, s1 As String
        Dim intNewCol As Integer

        sBarcode = mDataTableStockSelected.Rows(0).Item("barcode")
        sOriginalSupcode = ""
        If Not IsDBNull(mDataTableStockSelected.Rows(0).Item("supcode")) Then
            sOriginalSupcode = mDataTableStockSelected.Rows(0).Item("supcode")
        End If

        '-- Add a  grid row.-
        Dim intGridRx As Integer = clsDgvGoodsItems.Rows.Add()
        clsDgvGoodsItems.Rows(intGridRx).Cells(k_GRIDCOL_BARCODE).Value = sBarcode
        DoEvents()
        '- k_GRIDCOL_NEW_SUPPLIERCODE-
        '-- Save new OR CHANGED supCode (if any) for updating.
        If txtStockItemSupplierCode.Enabled Then  '-is PO-
            '- is PO..
            sNewSupcode = Trim(txtStockItemSupplierCode.Text)
            If ((sOriginalSupcode = "") And (sNewSupcode <> "")) Or _
                   ((sOriginalSupcode <> "") And (sNewSupcode <> sOriginalSupcode)) Then
                clsDgvGoodsItems.Rows(intGridRx).Cells(k_GRIDCOL_NEW_SUPPLIERCODE).Value = sNewSupcode
                '-- will update supCode table on Commit..
            End If  '-changed supcode-
        End If  '-PO-
        '-load grid with SupplierCode.. (if any.)
        clsDgvGoodsItems.Rows(intGridRx).Cells(k_GRIDCOL_SUP_CODE).Value = Trim(txtStockItemSupplierCode.Text)

        Call mbSetupGoodsStockItem(mDataTableStockSelected, intGridRx)
        '- update invoice total--
        Call mbUpdateGoodsTotal()

        btnStockLineOk.Enabled = False
        txtStockItemBarcode.Text = ""
        txtStockItemDescription.Text = ""

        clsDgvGoodsItems.Select()

        '-set cell focus..
        '-- barcode and other stuff all set up..  
        '-- So move to Cost_ex..
        If chkPriceIncludesTax.Checked Then
            intNewCol = k_GRIDCOL_COST_INC
        Else
            intNewCol = k_GRIDCOL_COST_EX
        End If
        Me.clsDgvGoodsItems.BeginInvoke(New MyDelegate(AddressOf mSetCurrentCell), _
                                        Me.clsDgvGoodsItems.Rows(intGridRx).Cells(intNewCol))

    End Sub  '-btnStockLineOk_Click-
    '= = = = = = =  = = = == = = = = =

    '-- END OF-  Barcode Entry and Lookup..
    '-- END OF-  Barcode Entry and Lookup..
    '-- END OF-  Barcode Entry and Lookup..
    '-- END OF-  Barcode Entry and Lookup..
    '= = = = = = = = = =  = = = = = = = = =
    '-===FF->

    '-- GOODS data grid events..--
    '-- GOODS data grid events..--

    '-- Mouse-down..  Context menu.. 
    '-- Mouse-down..  Context menu.. 

    Private mIntContextMenuGridRow As Integer = -1

    '-- COPY Barcode of selected part..-
    Public Sub mnuCopyItemBarcode_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuCopyItemBarcode.Click
        Dim sBarcode As String

        If (mIntContextmenuGridRow >= 0) Then
            sBarcode = Trim(clsDgvGoodsItems.Rows(mIntContextMenuGridRow).Cells(k_GRIDCOL_BARCODE).Value)
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            '=s1 = Trim(mItemSelectedPart.SubItems(2).Text) '-- barcode is 3rd..-.
            If (sBarcode <> "") Then
                My.Computer.Clipboard.SetText(sBarcode) '-- descr is item-2nd item..-.
            Else
                MessageBox.Show(Me, "No barcode..", _
                                  "JobMatixPOS Goods", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
        End If
    End Sub '--part barcode.-
    '= = = = = = = = = = =

    '-- COPY DESCRIPTION of selected part..-
    Public Sub mnuCopyItemDescription_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuCopyItemDescription.Click
        Dim sDescription As String

        If (mIntContextMenuGridRow >= 0) Then
            sDescription = Trim(clsDgvGoodsItems.Rows(mIntContextMenuGridRow).Cells(k_GRIDCOL_DESCRIPTION).Value)
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            '=s1 = Trim(mItemSelectedPart.SubItems(2).Text) '-- barcode is 3rd..-.
            If (sDescription <> "") Then
                My.Computer.Clipboard.SetText(sDescription) '-- descr is item-2nd item..-.
            Else
                MessageBox.Show(Me, "No sDescription..", _
                                  "JobMatixPOS Goods", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
        End If
    End Sub '--part descr..-
    '= = = = = = = = = = =

    '-- GOODS data grid events..--

    '-- Mouse-down..  Context menu.. 
    '--   Copy Barcode..

    Private Sub dgvGoodsItems_CellMouseDown(ByVal sender As Object, _
                                              ByVal ev As DataGridViewCellMouseEventArgs) _
                                               Handles clsDgvGoodsItems.CellMouseDown
        Dim intRow As Integer = ev.RowIndex
        Dim intColumn As Integer = ev.ColumnIndex
        Dim sBarcode As String = ""

        If (intRow >= 0) And (intColumn >= 0) Then
            If ev.Button = Windows.Forms.MouseButtons.Right Then
                sBarcode = Trim(clsDgvGoodsItems.Rows(intRow).Cells(k_GRIDCOL_BARCODE).Value)
                If (sBarcode <> "") Then
                    '-- select the row..
                    With Me.clsDgvGoodsItems
                        .CurrentCell = .Rows(intRow).Cells(intColumn)
                        .Rows(intRow).Selected = True
                    End With
                    '-- wait for row to be selected.
                    DoEvents()
                    Thread.Sleep(100)
                    DoEvents()
                    '-- Avoid the 'disabled' gray text by locking updates
                    LockWindowUpdate(clsDgvGoodsItems.Handle.ToInt32)
                    '---- A disabled TextBox will not display a context menu
                    clsDgvGoodsItems.Enabled = False
                    '--- Give the previous line time to complete
                    System.Windows.Forms.Application.DoEvents()
                    '-- Display our own context menu
                    '== mItemSelectedPart = item1 '--pass item to mnu routine..-
                    mIntContextMenuGridRow = intRow
                    mContextMenuItemInfo.Show(CType(sender, Control), ev.Location)
                    ' Enable the control again
                    clsDgvGoodsItems.Enabled = True
                    '-- Unlock updates
                    LockWindowUpdate(0)
                End If  '-barcode-
            End If  '-right button.
        End If  '-row-
    End Sub  '--cell mouse down..
    '= = = = = = = = = =  = = = = = = = = =
    '-===FF->

    '-- GOODS data grid events..--
    '-- GOODS data grid events..--

    '-  Use Invoke to avoid dgv re-entrancy problem.-
    Delegate Sub MyDelegate(ByRef cellX As DataGridViewCell)

    Private Sub mSetCurrentCell(ByRef cellX As DataGridViewCell)
        'Dim cellX2 As DataGridViewCell = cellX
        'If Me.clsDgvGoodsItems.InvokeRequired Then
        '    Me.clsDgvGoodsItems.Invoke(Sub() mSetCurrentCell(cellX2))
        '    Return
        'End If  '-required-
        '-ok-
        Try
            clsDgvGoodsItems.CurrentCell = cellX  '=clsDgvGoodsItems(colX, rowX)
        Catch ex As Exception
            MsgBox("Error in dgv.mSetCurrentCell." & vbCrLf & _
                                     ex.Message, MsgBoxStyle.Exclamation)
        End Try
        '= Call Me.clsDgvGoodsItems.BeginEdit(True)
    End Sub  '-set-
    '= = = = = = = = == = =

    '-- Grid TEXTBOX- Catch ENTER --
    '-- Catch Enter key-

    ' PreviewKeyDown is where you preview the key.
    ' Do not put any logic here, instead use the
    ' KeyDown event after setting IsInputKey to true.

    Private Sub dgvGoodsItems_PreviewKeyDown(ByVal sender As Object, _
                                               ByVal e As PreviewKeyDownEventArgs) _
                                             Handles clsDgvGoodsItems.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Enter, Keys.Escape, Keys.Up    '= Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub '-PreviewKeyDown-
    '= = = = = = = = = = == =

    '--ACTUAL Data GridView Control keyDown --
    '--- check for Fx  etc-

    Private Sub dgvGoodsItems_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles clsDgvGoodsItems.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        Dim intGridCol As Integer = Me.clsDgvGoodsItems.CurrentCellAddress.X  '== eventArgs.ColumnIndex
        Dim intGridRow As Integer = Me.clsDgvGoodsItems.CurrentCellAddress.Y  '= eventArgs.RowIndex

        If (KeyCode = System.Windows.Forms.Keys.F5) And _
                        ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--F5.. go back to new barcode entry.--
            If intGridCol = k_GRIDCOL_SERIALNOSREQUIRED Then
                '-- exit grid, back to new item..
                eventArgs.Handled = True
                txtStockItemBarcode.Select()
            End If  '-gridcol-
        ElseIf (KeyCode = System.Windows.Forms.Keys.Enter) Then
            If (intGridCol = k_GRIDCOL_SERIALNOSREQUIRED) Or _
                              (mbIsPurchaseOrder And (intGridCol = k_GRIDCOL_EXTENSION)) Then
                '-- exit grid, back to new item..
                eventArgs.Handled = True
                txtStockItemBarcode.Select()
            End If  '-gridcol-
        End If  '--keycode-
    End Sub  '-dgvGoodsItems_KeyDown-
    '= = = = = = = =  = = == = = = =

    '-- Enter a Row..  If new Row, then make Col-0 current..
    '--   else update picture.-

    Private Sub dgvGoodsItems_RowEnter(ByVal sender As Object, _
                                         ByVal ev As DataGridViewCellEventArgs) _
                                                      Handles clsDgvGoodsItems.RowEnter
        Dim ix, lRow, intCol As Integer

        lRow = ev.RowIndex
        intCol = ev.ColumnIndex
        Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = String.Empty
        Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(intCol).ErrorText = String.Empty

        '=3501.0831--- TEMP is REMOVED..

        'If (lRow >= 0) And (clsDgvGoodsItems.Rows.Count > 0) Then
        'Dim sStockId As String
        'Dim image1 As Image
        '    sStockId = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_STOCK_ID).Value
        '    If (sStockId <> "") Then
        '        '-- show picture if any..
        '        If mColStockImages.Contains(sStockId) Then  '--we saved it.-
        '            image1 = mColStockImages.Item(sStockId)
        '            picGoodsItem.Image = image1
        '        End If
        '    End If  '-id-
        'End If  '--selected a row.--
    End Sub  '--row enter-
    '= = = = = = = =  == = = =
    '-===FF->

    '-- Enter a Cell..  If new Row (no barcode), then make Col-0 current..

    Private Sub clsDgvGoodsItems_CellEnter(ByVal sender As Object, _
                                            ByVal ev As DataGridViewCellEventArgs) _
                                                         Handles clsDgvGoodsItems.CellEnter
        Dim intRow = ev.RowIndex
        Dim intCol = ev.ColumnIndex

        If Me.clsDgvGoodsItems.Rows(ev.RowIndex).IsNewRow Then
            If (intCol = k_GRIDCOL_BARCODE) Then
                '--entering barcode cell..  make sure editing mode is
                '= Me.clsDgvGoodsItems.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            Else  '-not barcode-
                '-- is new row.. (ENTER)-
                '-  If no barcode, go back to barcode..
                '-  Use Invoke to avoid dgv re-entrancy problem.-
                'MsgBox("Cell entered.. row/Col=" & intRow & "/" & intCol & vbCrLf & _
                '       "Current Cell is: " & _
                '       clsDgvGoodsItems.CurrentCell.RowIndex & "/" & _
                '                  clsDgvGoodsItems.CurrentCell.ColumnIndex, MsgBoxStyle.Information)
                'If (Trim(clsDgvGoodsItems.Rows(intRow).Cells(k_GRIDCOL_BARCODE).Value) = "") Then
                '    '= Call mSetCurrentCell(Me.clsDgvGoodsItems.Rows(intRow).Cells(k_GRIDCOL_BARCODE))
                '    Me.clsDgvGoodsItems.BeginInvoke(New MyDelegate(AddressOf mSetCurrentCell), _
                '                                    Me.clsDgvGoodsItems.Rows(intRow).Cells(k_GRIDCOL_BARCODE))
                'End If  '-value-
            End If '-barcode col.-
        End If  '-new row-
    End Sub  '-enter-
    '= = = = = = = = = = = = =

    '--CellBeginEdit-

    Private Sub clsDgvGoodsItems_CellBeginEdit(ByVal sender As Object, _
                                                ByVal ev As DataGridViewCellCancelEventArgs) _
                                                        Handles clsDgvGoodsItems.CellBeginEdit
        Dim intRow = ev.RowIndex
        Dim intCol = ev.ColumnIndex

        If mbIsCancelling Then
            '--get out of this one.
            ev.Cancel = True
            Exit Sub
        End If  '-cancelling.
        If clsDgvGoodsItems.Rows(intRow).IsNewRow Then
            If (intCol <> k_GRIDCOL_BARCODE) Then
                '-- might be new row.. (ENTER)-
                '-  If no barcode, go back to barcode..
                '-  Use Invoke to avoid dgv re-entrancy problem.-
                '= myTextBox.BeginInvoke(New MyDelegate(AddressOf DelegateMethod), myArray)

                If (Trim(clsDgvGoodsItems.Rows(intRow).Cells(k_GRIDCOL_BARCODE).Value) = "") Then
                    '==Me.clsDgvGoodsItems.EndEdit()  
                    '--Going back to Stock barcode column..
                    '= clsDgvGoodsItems.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2

                    '--get out of this one.
                    ev.Cancel = True
                    Me.clsDgvGoodsItems.BeginInvoke(New MyDelegate(AddressOf mSetCurrentCell), _
                                                Me.clsDgvGoodsItems.Rows(intRow).Cells(k_GRIDCOL_BARCODE))
                End If  '-value-
            End If '-barcode col.-
        End If  '-new row-
    End Sub  '-begin edit-
    '= = = = = = = == = = == 

    'Private Sub clsDgvGoodsItems_CellEndEdit(ByVal sender As Object, _
    '                                           ByVal ev As DataGridViewCellEventArgs) _
    '                                                   Handles clsDgvGoodsItems.CellEndEdit
    '    Dim intRow = ev.RowIndex
    '    Dim intCol = ev.ColumnIndex

    '    If clsDgvGoodsItems.Rows(intRow).IsNewRow Then
    '        If (intCol <> k_GRIDCOL_BARCODE) Then
    '            If (Trim(clsDgvGoodsItems.Rows(intRow).Cells(k_GRIDCOL_BARCODE).Value) = "") Then
    '                Me.clsDgvGoodsItems.BeginInvoke(New MyDelegate(AddressOf mSetCurrentCell), _
    '                        Me.clsDgvGoodsItems.Rows(intRow).Cells(k_GRIDCOL_BARCODE))
    '            End If  '-value-
    '        End If  '-barcode-
    '    End If '-new row.
    'End Sub  '-end edit.
    '= = = = = = == =  
    '-===FF->

    '-UserDeletingRow-
    '-  (via DELETE key..)

    Private Sub dgvGoodsItems_UserDeletingRow(ByVal sender As Object, _
                                           ByVal ev As DataGridViewRowCancelEventArgs) _
                                              Handles clsDgvGoodsItems.UserDeletingRow

        Dim sBarcode As String
        Dim rowX As Integer = ev.Row.Index   '= ht.RowIndex

        If mbIsCancelling Then Exit Sub

        If (rowX < 0) Or clsDgvGoodsItems.Rows(rowX).IsNewRow Then
            '-- silently.  MsgBox("Can't delete that row !", MsgBoxStyle.Exclamation)
            ev.Cancel = True
            Exit Sub
        End If
        sBarcode = ev.Row.Cells(k_GRIDCOL_BARCODE).Value
        If Not (MsgBox("Sure you want to delete the selected item: '" & sBarcode & "' ??", _
                                        vbYesNo + vbDefaultButton2 + vbQuestion) = vbYes) Then
            ev.Cancel = True
            Exit Sub
        End If
        '-- ok.. let it delete.-
        '- when done..  MUST update Invoice totals..

    End Sub  '-UserDeletingRow-
    '= = = = = = = = = = = = = = = = =

    '-User Deleted Row-  DONE-
    '-  (via DELETE key..)

    Public Sub dgvSaleItems_UserDeletedRow(ByVal sender As Object, _
                                              ByVal ev As DataGridViewRowEventArgs) _
         Handles clsDgvGoodsItems.UserDeletedRow
        Dim gridRow1 As DataGridViewRow = ev.Row

        '- Now deleted..  MUST update Invoice totals..
        '- update invoice total--
        Call mbUpdateGoodsTotal()

    End Sub  '--dgvSaleItems_UserDeletedRow-
    '= = = = = = = = = = = = = = = = == 
    '-===FF->

    '--mouse activity---  select row to edit--
    '== REDUNDANT ???  --

    Private Sub dgvGoodsItems_CellMouseClick(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                     Handles clsDgvGoodsItems.CellMouseClick
        Dim lRow, lCol, lngError As Integer
        Dim colRowValues As Collection
        '= dim row1 as DataGridViewRow 

        On Error GoTo datagridSale_Click_Error
        If (eventArgs.Button = MouseButtons.Left) Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If (lRow >= 0) Then '--NOT in header row--
                '--MsgBox " click on Row: " & lRow & ", col :" & lCol
                With clsDgvGoodsItems.Rows(lRow)
                End With
                '== mIntCurrentRowNo = lRow  '--current edit row.-
            End If '--row--
        End If  '--button.-
        Exit Sub

datagridSale_Click_Error:
        lngError = Err.Number()
        MsgBox("Runtime Error in JobMatixPOS datagridGoods_Click (" & lRow & "/" & lCol & ") sub.." & vbCrLf & _
                "Error is " & lngError & " = " & ErrorToString(lngError))
    End Sub  '--click-
    '= = = = = = = = = = == =
    '-===FF->

    '-- DataGridView Goods-
    '-== F2 Lookup Stock --

    '-- Textbox control has been activated on a cell.-
    '--  set event handlers to deal with the textbox..
    '-- to catch keypress...

    Private Sub dgvGoodsItems_EditingControlShowing(ByVal sender As Object, _
                                                    ByVal e As DataGridViewEditingControlShowingEventArgs) _
                                              Handles clsDgvGoodsItems.EditingControlShowing

        Dim text1 As TextBox = CType(e.Control, TextBox)
        If (text1 IsNot Nothing) Then
            '-- Remove an existing event-handler, if present, to avoid 
            '-- adding multiple handlers when the editing control is reused.
            RemoveHandler text1.KeyDown, _
                New KeyEventHandler(AddressOf dgvGoodsItems_Editing_KeyDown)
            '=3403.615=
            RemoveHandler text1.PreviewKeyDown, _
                          New PreviewKeyDownEventHandler(AddressOf dgvGoodsItems_Editing_PreviewKeyDown)
            '=3501.0901=
            RemoveHandler text1.Enter, _
                  New EventHandler(AddressOf dgvGoodsItems_EditingControlEnter)

        End If
        '-- Add the event handler. 
        AddHandler text1.KeyDown, _
             New KeyEventHandler(AddressOf dgvGoodsItems_Editing_KeyDown)
        '=3403.615=
        AddHandler text1.PreviewKeyDown, _
                      New PreviewKeyDownEventHandler(AddressOf dgvGoodsItems_Editing_PreviewKeyDown)
        '-- Add the event handler. 
        AddHandler text1.Enter, _
             New EventHandler(AddressOf dgvGoodsItems_EditingControlEnter)

    End Sub  '--EditingControlShowing-
    '= = = = = = =  = = = == = = = 

    '-- Grid TEXT box showing-  Enter-
    '--  This Editing textbox event captured to set TEXTBOX bg color..

    Private Sub dgvGoodsItems_EditingControlEnter(eventsender As Object, EventArgs As System.EventArgs)
        Dim text1 As DataGridViewTextBoxEditingControl = CType(eventsender, DataGridViewTextBoxEditingControl)

        Dim bHasFocus As Boolean = text1.Focused
        '= MsgBox("Textbox Enter Event- Text= " & text1.Text & vbCrLf & "Has Focus = " & bHasFocus)
        If (text1 IsNot Nothing) Then
            text1.BackColor = Color.PaleGoldenrod
            DoEvents()
            'text1.SelectionStart = 0
            'text1.SelectionLength = Len(text1.Text)
            '= text1.SelectAll()
        End If  '-nothing-
    End Sub  '-EditingControlEnter-
    '= = = = = = = = = =  = = ==  =

    '-- Grid TEXTBOX- Catch ENTER --
    '-- Catch Enter key-

    ' PreviewKeyDown is where you preview the key.
    ' Do not put any logic here, instead use the
    ' KeyDown event after setting IsInputKey to true.

    Private Sub dgvGoodsItems_Editing_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
        Select Case (e.KeyCode)
            Case Keys.Enter, Keys.Escape, Keys.Up    '= Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub '-PreviewKeyDown-
    '= = = = = = = = = = == =
    '-===FF->

    '-- Grid TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for STOCK Lookup--

    Private Sub dgvGoodsItems_Editing_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) '= Handles txtPartNo.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        '= Dim colPrefsStock, colKeys As Collection
        Dim colSelectedRow As Collection
        Dim intGridRow, intGridCol, intStock_id As Integer
        Dim dataTable1 As DataTable
        Dim row1 As DataRow
        Dim sSql, s1, sBarcode, sBarcode0, sErrorMsg As String

        intStock_id = -1
        msSupplierName = ""

        '= colPrefsStock = mColPrefColumnsStock  '=3301.816=

        '==mTxtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        '--  needs  current row/cell..--
        intGridCol = Me.clsDgvGoodsItems.CurrentCellAddress.X  '== eventArgs.ColumnIndex
        intGridRow = Me.clsDgvGoodsItems.CurrentCellAddress.Y  '= eventArgs.RowIndex

        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                               ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup stock--
            '= Call mbBrowseStockTable()
            If (intGridCol = k_GRIDCOL_BARCODE) Then  '--barcode-
                '-- Lookup Stock-
                '=3301.816=  NB: User must use StockAdmin to add New Stock Line (Item Type).
                'If Not mbBrowseTable("Stock", colPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, True) Then
                '    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                'Else '-ok-
                'End If '-browse-
            End If  '--is barcode col-
            eventArgs.Handled = True
        ElseIf (KeyCode = System.Windows.Forms.Keys.Enter) Then
            '=3403.615=
            '-- move to next column-
            '-- Validation should happen..

            If intGridCol = k_GRIDCOL_QTY Then  '-last editable col-
                '- go to next row-
                '-- ADD row if  not there.
                '= clsDgvGoodsItems.CurrentCell = clsDgvGoodsItems(0, intGridRow + 1)
            Else  '-move to next column-
                clsDgvGoodsItems.CurrentCell = clsDgvGoodsItems(intGridCol + 1, intGridRow)
            End If
            eventArgs.Handled = True
        ElseIf (KeyCode = System.Windows.Forms.Keys.Up) Then
            MsgBox("Got UpArrow key..", MsgBoxStyle.Information)
            If (intGridCol = k_GRIDCOL_BARCODE) Then  '--barcode-
                '= txtBarcode.Text = "&Up"
                '= clsDgvGoodsItems.EndEdit()
            End If
            eventArgs.Handled = True
        ElseIf (KeyCode = System.Windows.Forms.Keys.F10) Then
            '= MsgBox("Got F10 key..", MsgBoxStyle.Information)
            If (intGridCol = k_GRIDCOL_BARCODE) Then  '--barcode-
                '= txtBarcode.Text = "&F10"
                '= SendKeys.Send("{ENTER}")  '--close edit..
            End If
            eventArgs.Handled = True
        ElseIf (KeyCode = System.Windows.Forms.Keys.Escape) Then
            'MsgBox("Got Escape key..", MsgBoxStyle.Information)
            'eventArgs.Handled = True
        End If '--F2--
    End Sub '--keydown.-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- catch barcode scanned entry--
    '--- Goods Items- C e l l  V a l i d a t i n g--=  
    '--- Goods Items- C e l l  V a l i d a t i n g--=  

    Private Sub dgvGoodsItems_CellValidating(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellValidatingEventArgs) _
                                                  Handles clsDgvGoodsItems.CellValidating
        Dim lRow, lCol As Integer
        Dim s1, s2, sBarcode, sSerialNo, sQty As String
        Dim sCostEx, sCostInc As String
        Dim sSellEx, sSellInc As String
        Dim decCostEx, decCostInc As Decimal
        Dim decSellEx, decSellInc As Decimal
        Dim sSql As String
        Dim dataTable1 As DataTable
        Dim bIsNewRow As Boolean = False
        Dim sGoodsTaxCode, sSalesTaxCode As String


        If mbIsPurchaseOrder And (Not mbIsNewPO) Then Exit Sub
        If mbIsCancelling Then
            '--get out of this one.
            Exit Sub
        End If  '-cancelling.

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        '-TEMP- test-
        '= MsgBox("CellValidating Grid: Row: " & lRow & ",  Col: " & lCol, MsgBoxStyle.Information)
        Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = ""
        Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(lCol).ErrorText = ""
        bIsNewRow = (Trim(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SERIALNOSREQUIRED).Value) = "")

        If (lRow >= 0) And (clsDgvGoodsItems.Rows.Count > 0) Then  '--selected a row.--
            '- If view only-  no changing..
            If mbIsPurchaseOrder And (Not mbIsNewPO) Then
                ev.Cancel = True
                MsgBox("Can't change this Purchase order !", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            '= Dim sGoodsTaxode, sSalesTaxCode As String
            sGoodsTaxCode = UCase(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_TAX_CODE).Value)
            sSalesTaxCode = UCase(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SALES_TAX_CODE).Value)

            If (lCol = k_GRIDCOL_BARCODE) Then  '--barcode-
                sBarcode = ev.FormattedValue.ToString
                '-- Now done in text line above....

            ElseIf (lCol = k_GRIDCOL_COST_EX) And (Not chkPriceIncludesTax.Checked) Then
                '-- ASSUME cost-ex was changed..  Go update cost_inc column..
                '=3519.0404=  CHECK ! IF cost-ex was changed..
                '=3519.0404=  CHECK ! IF cost-ex was changed..
                Dim sPreviousCost As String = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_ORIGINAL_COST_EX).Value
                sCostEx = ev.FormattedValue.ToString
                If Not mbIsNumeric(sCostEx) Then
                    ev.Cancel = True
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "Cost must be numeric.. " & sCostEx
                Else  '--ok-
                    '- COST_EX -
                    '- COST_EX -
                    If mbIsNumeric(sPreviousCost) Then
                        If CDec(sCostEx) = CDec(sPreviousCost) Then
                            Exit Sub   '--ok.. no change..  let it go.
                        End If
                    End If
                    '--ok re-compute cost_inc..--
                    '=3519.0227=--  AND Extension, AND Sell_inc etc. USING mDecSell_margin..
                    '- Add .00 if needed.
                    If (InStr(sCostEx, ".") <= 0) Then   '-whole dollars was entered.
                        sCostEx &= ".00"
                    End If  '--.00-
                    decCostEx = CDec(sCostEx)
                    'If (decCostEx <> CDec(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_ORIGINAL_COST_EX).Value)) Then
                    '- new costEx
                    '= s1 = FormatCurrency(decCostEx, 2)
                    '= s1 = Replace(s1, "$", "")
                    s1 = FormatNumber(decCostEx, 2)
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_COST_EX).Value = s1 '= FormatCurrency(decCostEx, 2)
                    '=3519.0404=  cost-ex was changed..  now is new original.
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_ORIGINAL_COST_EX).Value = s1
                    decCostInc = decCostEx
                    If sGoodsTaxCode = "GST" Then
                        decCostInc = decCostEx + ((decCostEx * mDecGST_rate) / 100)
                    End If
                    '= s1 = FormatCurrency(decCostInc, 2)
                    '= s1 = Replace(s1, "$", "")
                    s1 = FormatNumber(decCostInc, 2)
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_COST_INC).Value = s1
                    '=3519.0227= RE-calculate Sell_inc and sell_ex.
                    '--  USING mDecSell_margin..
                    decSellInc = decCostInc + ((decCostInc * mDecSell_margin) / 100)
                    '=3519.0227= ROUNDING..  Sell_inc IS FOR DISPLAY ONLY..
                    '--   MUST round to 2 decimals first.
                    decSellInc = Decimal.Round(decSellInc, 2)
                    decSellInc += mDecGetRoundingAmount(decSellInc)
                    s1 = FormatCurrency(decSellInc, 2)
                    s1 = Replace(s1, "$", "")
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_INC).Value = s1
                    '- now MUST re-calculate Sell_ex..
                    decSellEx = decSellInc
                    If sSalesTaxCode = "GST" Then
                        decSellEx = (decSellInc / (1 + (mDecGST_rate / 100)))  '--1.1-
                    End If
                    s1 = Replace(FormatCurrency(decSellEx, 2), "$", "")
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_EX).Value = s1

                    sQty = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_QTY).Value
                    If mbIsNumeric(sQty) Then
                        '--ok re-compute extension and goods totals..--
                        Call mbUpdateGoodsStockItem(ev.RowIndex, sQty)
                        '- update invoice total--
                        Call mbUpdateGoodsTotal()
                        Me.clsDgvGoodsItems.Select()  '-bring focus back to grid.
                    End If  '-numeric-
                    '-- PHEW--
                    'End If  '-original-
                End If  '--numeric-
                '= Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_NEW_COST_EX).Value = "Y"  '-Mark row to update stock cost.
            ElseIf (lCol = k_GRIDCOL_COST_INC) And (chkPriceIncludesTax.Checked) Then
                '- IF cost_inc was changed..   go back and update cost_ex..
                '=3519.0404=  CHECK ! IF cost-inc was changed..
                '=3519.0404=  CHECK ! IF cost-inc was changed..
                Dim sPreviousCost As String = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_ORIGINAL_COST_INC).Value
                sCostInc = ev.FormattedValue.ToString
                If Not mbIsNumeric(sCostInc) Then
                    ev.Cancel = True
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "Cost must be numeric.. " & sCostInc
                Else  '--ok-
                    '- COST_INC-
                    If mbIsNumeric(sPreviousCost) Then
                        If CDec(sCostInc) = CDec(sPreviousCost) Then
                            Exit Sub   '--ok.. no change..  let it go.
                        End If
                    End If
                    '--ok re-compute cost_ex..-- AND everything else..
                    '- Add .00 if needed.
                    If (InStr(sCostInc, ".") <= 0) Then   '-whole dollars was entered.
                        sCostInc &= ".00"
                    End If  '--.00-
                    decCostInc = CDec(sCostInc)
                    'If (decCostInc <> CDec(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_ORIGINAL_COST_INC).Value)) Then
                    '- ASSUME new costInc- update costEx
                    '= s1 = FormatCurrency(decCostInc, 2)
                    '= s1 = Replace(s1, "$", "")
                    s1 = FormatNumber(decCostInc, 2)
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_COST_INC).Value = s1 '= FormatCurrency(decCostInc, 2)
                    '=3519.0404=  cost-inc was changed..  now is new original.
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_ORIGINAL_COST_INC).Value = s1
                    '= decCostEx = decCostInc - ((decCostInc * mDecGST_rate) / 100)
                    '= mDecSellActualExTax = (mDecSellActualIncTax / (1 + (mDecGST_rate / 100)))  '--1.1-
                    decCostEx = decCostInc
                    If sGoodsTaxCode = "GST" Then
                        decCostEx = (decCostInc / (1 + (mDecGST_rate / 100)))  '--1.1-
                    End If
                    '= s1 = FormatCurrency(decCostEx, 2)
                    '= s1 = Replace(s1, "$", "")
                    s1 = FormatNumber(decCostEx, 2)
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_COST_EX).Value = s1
                    '-- 
                    '=3519.0227= RE-calculate Sell_inc and sell_ex.
                    '--  USING mDecSell_margin..
                    decSellInc = decCostInc + ((decCostInc * mDecSell_margin) / 100)
                    '=3519.0227= ROUNDING..  Sell_inc IS FOR DISPLAY ONLY..
                    '--   MUST round to 2 decimals first.
                    decSellInc = Decimal.Round(decSellInc, 2)
                    decSellInc += mDecGetRoundingAmount(decSellInc)
                    s1 = FormatCurrency(decSellInc, 2)
                    s1 = Replace(s1, "$", "")
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_INC).Value = s1
                    '- now MUST re-calculate Sell_ex..
                    decSellEx = decSellInc
                    If sSalesTaxCode = "GST" Then
                        decSellEx = (decSellInc / (1 + (mDecGST_rate / 100)))  '--1.1-
                    End If
                    s1 = Replace(FormatCurrency(decSellEx, 2), "$", "")
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_EX).Value = s1
                    '--Extension-
                    sQty = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_QTY).Value
                    If mbIsNumeric(sQty) Then
                        '--ok re-compute extension and goods totals..--
                        Call mbUpdateGoodsStockItem(ev.RowIndex, sQty)
                        '- update invoice total--
                        Call mbUpdateGoodsTotal()
                        Me.clsDgvGoodsItems.Select()  '-bring focus back to grid.
                    End If  '-numeric-
                    '-- PHEW--
                    'End If  '--Original-
                End If  '--numeric-
                '= Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_NEW_COST_EX).Value = "Y"  '-Mark row to update stock cost.
            ElseIf (lCol = k_GRIDCOL_SELL_EX) Then
                '=3519.0212=-- IF sell-ex was changed..  Go update sell_inc column..
                '--SELL_EX-
                sSellEx = ev.FormattedValue.ToString
                If Not mbIsNumeric(sSellEx) Then
                    ev.Cancel = True
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "Sell_ex must be numeric.. " & sCostEx
                Else  '--ok-
                    '--re-compute Sell_inc.
                    '- Add .00 if needed.
                    If (InStr(sSellEx, ".") <= 0) Then   '-whole dollars was entered.
                        sSellEx &= ".00"
                    End If  '--.00-
                    decSellEx = CDec(sSellEx)
                    '=If (decSellEx <> CDec(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_EX).Value)) Then
                    '- new sellEx
                    '= s1 = FormatCurrency(decSellEx, 2)
                    '= s1 = Replace(s1, "$", "")
                    s1 = FormatNumber(decSellEx, 2)
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_EX).Value = s1  '= FormatCurrency(decSellEx, 2)
                    decSellInc = decSellEx
                    If (sSalesTaxCode = "GST") Then
                        decSellInc = decSellEx + ((decSellEx * mDecGST_rate) / 100)
                    End If
                    '=3519.0214= ROUNDING..  Sell_inc IS FOR DISPLAY ONLY..
                    '--   MUST round to 2 decimals first.
                    decSellInc = Decimal.Round(decSellInc, 2)
                    decSellInc += mDecGetRoundingAmount(decSellInc)
                    s1 = FormatCurrency(decSellInc, 2)
                    s1 = Replace(s1, "$", "")
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_INC).Value = s1
                    '=End If
                End If  '-numeric-
            ElseIf (lCol = k_GRIDCOL_SELL_INC) Then
                '=3519.0212=-- IF sell-inc was changed..  Go update sell_ex column..
                '-SELL_INC-
                sSellInc = ev.FormattedValue.ToString
                If Not mbIsNumeric(sSellInc) Then
                    ev.Cancel = True
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "Sell_inc must be numeric.. " & sCostEx
                Else  '--ok-
                    '--re-compute Sell_ex.
                    '- Add .00 if needed.
                    If (InStr(sSellInc, ".") <= 0) Then   '-whole dollars was entered.
                        sSellInc &= ".00"
                    End If  '--.00-
                    decSellInc = CDec(sSellInc)
                    '=If decSellInc <> CDec(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_INC).Value) Then
                    '- new sellInc
                    '= s1 = FormatCurrency(decSellInc, 2)
                    '= s1 = Replace(s1, "$", "")
                    s1 = FormatNumber(decSellInc, 2)
                    '-test--
                    '== MsgBox("vaiidated sell_inc is: " & s1)
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_INC).Value = s1 '= FormatCurrency(decSellInc, 2)
                    decSellEx = decSellInc
                    If sSalesTaxCode = "GST" Then
                        decSellEx = (decSellInc / (1 + (mDecGST_rate / 100)))  '--1.1-
                    End If
                    s1 = Replace(FormatCurrency(decSellEx, 2), "$", "")
                    Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SELL_EX).Value = s1
                    '= End If
                End If  '-numeric-
            ElseIf (lCol = k_GRIDCOL_QTY) Then
                '--re-calculate item line..
                sQty = ev.FormattedValue.ToString
                sBarcode = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_BARCODE).Value
                If (sBarcode = "") Then
                    If (sQty <> "") Then
                        ev.Cancel = True
                        Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "No Stock item selected.. " & sBarcode
                    End If
                Else '-check qty-
                    If (sQty = "") Then  '--error-
                        ev.Cancel = True
                        Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "No Quantity entered.. " & sBarcode
                    Else '-check if service-
                        '-- not service, Qty must be an integer--
                        If Not mbIsNumeric(sQty) Then
                            ev.Cancel = True
                            Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = "Quantity must be numeric.. " & sBarcode
                        Else  '--ok-
                            '--ok re-compute extension and goods totals..--
                            Call mbUpdateGoodsStockItem(ev.RowIndex, sQty)
                            '- update invoice total--
                            Call mbUpdateGoodsTotal()
                            Me.clsDgvGoodsItems.Select()  '-bring focus back to grid.
                        End If  '--numeric-
                    End If  '-some qty-
                End If
            End If  '--col-
        End If  '--row-- 
        '== If Not Integer.TryParse(ev.FormattedValue.ToString(), newInteger) _
        '==     OrElse newInteger < 0 Then
        '== ev.Cancel = True
        '== Me.dgvSaleItems.Rows(ev.RowIndex).ErrorText = "the value must be a non-negative integer"
        '== End If
    End Sub  '--cell validating.--
    '= = = = = = = = = = == =
    '-===FF->

    '=3501.1107=
    '--   Function to get/save SerialNos.
    '-mbGetGoodsSerialNos=
    '-mbGetGoodsSerialNos=

    '==
    '==   Target-New-Build-4287 --  
    '==      (28-Jan-2021)
    '==
    '==  Make sure that serial no's are unique over the whole Invoice, 
    '==    NOT just the current line..
    '==    And still also not on file already in the system. 
    '==


    Private Function mbGetGoodsSerialNos(ByVal intRowIndex As Integer) As Boolean
        Dim intStock_id, intQty As Integer
        Dim sBarcode, sDescription, sSerialList As String
        Dim sSerialNo As String
        Dim asSep As String() = {vbCrLf}
        Dim colSerials As Collection
        '= Dim dataTable1 As DataTable
        Dim frmSerials1 As frmGoodsSerials

        mbGetGoodsSerialNos = False

        sBarcode = Me.clsDgvGoodsItems.Rows(intRowIndex).Cells(k_GRIDCOL_BARCODE).Value
        sDescription = Me.clsDgvGoodsItems.Rows(intRowIndex).Cells(k_GRIDCOL_DESCRIPTION).Value
        intStock_id = CInt(Me.clsDgvGoodsItems.Rows(intRowIndex).Cells(k_GRIDCOL_STOCK_ID).Value)

        '- get current List for this item entry.
        sSerialList = Me.clsDgvGoodsItems.Rows(intRowIndex).Cells(k_GRIDCOL_SERIALNOLIST).Value
        '-- split the string-list to get serials, and make collection.-
        colSerials = New Collection
        Dim asSerials() As String = sSerialList.Split(asSep, StringSplitOptions.RemoveEmptyEntries)
        If (asSerials.Length > 0) Then
            For ix As Integer = 0 To (asSerials.Length - 1)
                If (Trim(asSerials(ix)) <> "") Then
                    colSerials.Add(Trim(asSerials(ix)))
                End If
            Next ix
        End If

        '==   Target-New-Build-4287 --  
        '==   Target-New-Build-4287 --  
        '-- make collections ALSO for all serials in OTHER lines of the Invoice..
        Dim colOtherList, colInvoiceLineInfo As Collection
        Dim colOtherSerialsThisInvoice As Collection
        colOtherSerialsThisInvoice = New Collection

        '--  make sure all serial no's have been entered..
        If (clsDgvGoodsItems.Rows.Count > 1) Then  '== must be more lines than just this one.
            For Each row1 As DataGridViewRow In clsDgvGoodsItems.Rows
                '-- BYPASS EMPTY edit row at the end..--
                '==  PICK only those invoice lines with same stock id..
                If (row1.Index <> intRowIndex) AndAlso (row1.Cells(k_GRIDCOL_BARCODE).Value <> "") AndAlso
                                              (CInt(row1.Cells(k_GRIDCOL_STOCK_ID).Value) = intStock_id) Then  '--same id. +not empty-
                    With row1
                        '- get current List for this item entry.
                        sSerialList = .Cells(k_GRIDCOL_SERIALNOLIST).Value
                        colOtherList = New Collection
                        asSerials = sSerialList.Split(asSep, StringSplitOptions.RemoveEmptyEntries)
                        If (asSerials.Length > 0) Then
                            For ix As Integer = 0 To (asSerials.Length - 1)
                                If (Trim(asSerials(ix)) <> "") Then
                                    colOtherList.Add(Trim(asSerials(ix)))
                                End If
                            Next ix
                        End If  '-- length-
                        '--Collect row index/no and serials for each Invoice Row.
                        colInvoiceLineInfo = New Collection
                        colInvoiceLineInfo.Add(colOtherList, "SerialsList")
                        colInvoiceLineInfo.Add(CStr(row1.Index + 1), "LineNo")
                        '-- add to "other" super collection.
                        colOtherSerialsThisInvoice.Add(colInvoiceLineInfo)
                    End With  '-- row1-
                End If
            Next row1
        End If  '==count-
        '== END  Target-New-Build-4287 --  
        '== END  Target-New-Build-4287 --  


        intQty = CInt(Me.clsDgvGoodsItems.Rows(intRowIndex).Cells(k_GRIDCOL_QTY).Value)
        '-load form to enter serials..-
        frmSerials1 = New frmGoodsSerials
        frmSerials1.connectionSql = mCnnSql
        frmSerials1.DBname = msSqlDbName
        frmSerials1.stock_id = intStock_id
        frmSerials1.stockBarcode = sBarcode
        frmSerials1.stockDescription = sDescription
        frmSerials1.quantity = intQty
        frmSerials1.colSerials = colSerials   '=frmSerials1.serialList = sSerialList

        '==   Target-New-Build-4287 --  
        frmSerials1.colOtherSerialsThisInvoice = colOtherSerialsThisInvoice   '=frmSerials1.serialList = sSerialList
        '==  END Target-New-Build-4287 --

        '==MsgBox("Serial Number Entry for " & intQty & " items, barcode: " & sBarcode, MsgBoxStyle.Information)
        frmSerials1.ShowDialog()

        If frmSerials1.cancelled Then
            '== MsgBox("Cancelled..", MsgBoxStyle.Exclamation)
            frmSerials1.Close()
            Exit Function
        End If
        '-result list from Grid..
        '-- Turn result collection back into string list..
        sSerialList = ""
        colSerials = frmSerials1.colSerials
        frmSerials1.Close()
        '-- check and save resulting serial collection..-
        If (Not (colSerials Is Nothing)) Then  '= AndAlso (colSerials.Count > 0) Then
            For Each sSerialNo In colSerials
                If sSerialList <> "" Then
                    sSerialList &= vbCrLf
                End If
                If (Trim(sSerialNo) <> "") Then
                    sSerialList &= Trim(sSerialNo)
                End If
            Next
            '- fix color if all entered..-
            With Me.clsDgvGoodsItems.Rows(intRowIndex).Cells(k_GRIDCOL_SERIALNOSREQUIRED)
                If (colSerials.Count <> intQty) Then
                    .Style.ForeColor = Color.Red
                Else '-ok.. all entered.-
                    .Style.ForeColor = Color.Black
                End If '-count-
            End With
        Else
            MsgBox("ERROR:  No object for colSerials..", MsgBoxStyle.Critical)
            Exit Function
        End If  '-nothing-
        '--save string list in grid row..
        mbGetGoodsSerialNos = True
        Me.clsDgvGoodsItems.Rows(intRowIndex).Cells(k_GRIDCOL_SERIALNOLIST).Value = sSerialList

    End Function  '-mbGetGoodsSerialNos=
    '= = = = = = = = = = == =
    '-===FF->

    '=3501.0827= -- Goods Items-
    '---     C e l l  V a l i d a t e d--=  

    Private Sub dgvGoodsItems_CellValidated(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellEventArgs) _
                                                  Handles clsDgvGoodsItems.CellValidated
        Dim intRow, intCol, intNewRow, intNewCol As Integer
        Dim sData, s1, sBarcode As String

        intRow = ev.RowIndex
        intCol = ev.ColumnIndex
        If mbIsCancelling Then
            '--get out of this one.
            Exit Sub
        End If  '-cancelling.
        Dim bIsNewRow As Boolean = (Trim(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SERIALNOSREQUIRED).Value) = "")

        If (intRow >= 0) And (clsDgvGoodsItems.Rows.Count > 0) Then  '--selected a row.--
            If Not mbIsPurchaseOrder Then
                If (intCol = k_GRIDCOL_BARCODE) Then  '--barcode-
                    sBarcode = clsDgvGoodsItems.Rows(intRow).Cells(intCol).Value
                ElseIf (intCol = k_GRIDCOL_QTY) Then  '--qty-
                    '= intNewCol = k_GRIDCOL_SERIALNOSREQUIRED
                    '= MsgBox("We can enter Serials here..", MsgBoxStyle.Information)
                    Dim sTrackSerial As String
                    '=3501.1107=
                    '--   Function to get/save SerialNos.
                    sTrackSerial = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_TRACK_SERIAL).Value
                    If sTrackSerial = "1" Then  '-can track-
                        '-- offer up the current serals list, if any, and get it updated..
                        If Not mbGetGoodsSerialNos(ev.RowIndex) Then
                            '= MsgBox("Serial Input was cancelled.", MsgBoxStyle.Exclamation)
                        Else  '-ok-
                            '-test-
                            MsgBox("Serials List for this item is now:" & vbCrLf & _
                                  Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SERIALNOLIST).Value, _
                                                                                            MsgBoxStyle.Information)
                        End If  '-get serials-
                    End If  '--can track.
                    '=3519.0227=
                    '=3519.0311= txtStockItemBarcode.Select()

                    '=3501.1029=
                    '--- DROP this.. it causes a crash when Trying to click back or onto another row..
                    '= Me.clsDgvGoodsItems.BeginInvoke(New MyDelegate(AddressOf mSetCurrentCell), _
                    '=                                 Me.clsDgvGoodsItems.Rows(intRow).Cells(intNewCol))
                End If  '-barcode-
            End If  '-po-
        End If  '-row-
    End Sub  '--validated.-
    '= = = = = = = = = = = = = =

    '-- Cell EndEdit..   Comes After Validation..-

    '= https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/walkthrough-validating-data-in-the-windows-forms-datagridview-control

    Private Sub dgvGoodsItems_CellEndEdit(ByVal sender As Object, ByVal ev As DataGridViewCellEventArgs) _
                                                                     Handles clsDgvGoodsItems.CellEndEdit
        Dim intRow, intCol, intNewCol As Integer
        Dim sData, s1 As String

        intRow = ev.RowIndex
        intCol = ev.ColumnIndex
        If clsDgvGoodsItems.Rows(intRow).IsNewRow Then
            If (intCol = k_GRIDCOL_BARCODE) Then  '--barcode-
                '= sData = clsDgvGoodsItems.Rows(intRow).Cells(intCol).Value
                '= MsgBox("CellEndEdit..", MsgBoxStyle.Information)
            End If  '--barcode-
        End If  '-new row-
    End Sub  '-cellEndEdit-
    '= = = = = = = = = = = == =
    '-===FF->

    '--  serial nos.cell click..==

    Private Sub dgvGoodsItems_CellClick(ByVal sender As Object, _
                                                   ByVal ev As DataGridViewCellEventArgs) _
                                                    Handles clsDgvGoodsItems.CellClick
        Dim ix, intRow, intCol, intStock_id, intQty As Integer
        Dim sBarcode, sSerialNo, sDescription As String
        Dim sTrackSerial As String
        Dim sSerialList As String
        Dim asSep As String() = {vbCrLf}
        '= Dim colSerials As Collection
        '= Dim dataTable1 As DataTable
        '= Dim frmSerials1 As frmGoodsSerials

        intRow = ev.RowIndex
        intCol = ev.ColumnIndex
        If (intRow >= 0) And (clsDgvGoodsItems.Rows.Count > 0) And (intCol >= 0) Then  '--selected a row.--
            If (intCol = k_GRIDCOL_SERIALNOSREQUIRED) Then  '--barcode-
                Me.clsDgvGoodsItems.Rows(ev.RowIndex).ErrorText = ""
                Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(intCol).ErrorText = ""
                sBarcode = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_BARCODE).Value
                sDescription = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_DESCRIPTION).Value
                intStock_id = CInt(Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_STOCK_ID).Value)
                sTrackSerial = Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_TRACK_SERIAL).Value
                If ((sBarcode <> "") And (intStock_id > 0)) Then  '--not empty line..-
                    If sTrackSerial = "1" Then  '-can track-
                        If Not mbGetGoodsSerialNos(ev.RowIndex) Then
                            '= MsgBox("Serial Input was cancelled.", MsgBoxStyle.Exclamation)
                        Else  '-ok-
                            '-test-
                            MsgBox("Serials List for this item is now:" & vbCrLf & _
                                  Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SERIALNOLIST).Value, MsgBoxStyle.Information)
                            '=3519.0227=
                            '=3519.0311= txtStockItemBarcode.Select()
                        End If  '-get serials-
                        '=3519.0311=
                        txtStockItemBarcode.Select()
                        ''--save string list in grid row..
                        'Me.clsDgvGoodsItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SERIALNOLIST).Value = sSerialList
                    End If  '--track serials this stock item..
                End If  '-empty-
            End If  '--button-
        End If
    End Sub  '--cell click.-
    '= = = = = = = = = =  = =
    '-===FF->

    '-- validate Row..-

    Public Sub dgvGoodsItems_RowValidating(ByVal sender As Object, _
                                            ByVal ev As DataGridViewCellCancelEventArgs) _
                                               Handles clsDgvGoodsItems.RowValidating
        Dim s1, sBarcode, sId As String
        Dim dataRow1 As DataGridViewRow = clsDgvGoodsItems.Rows(ev.RowIndex)

        If mbIsCancelling Then Exit Sub
        If mbIsPurchaseOrder And (Not mbIsNewPO) Then Exit Sub

        If dataRow1.IsNewRow Then Exit Sub
        '--check that row has barcode and stock_id..
        sBarcode = Trim(dataRow1.Cells(k_GRIDCOL_BARCODE).Value)
        sId = Trim(dataRow1.Cells(k_GRIDCOL_STOCK_ID).Value)
        If (sBarcode = "") Or (sId = "") Then
            ev.Cancel = True
            MsgBox("Invalid Stock barcode, or invalid data on row " & (ev.RowIndex + 1), MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        '--  Check that row needs Serials if this stock item Tracks Serials !!!!


    End Sub '-RowValidating-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Total extras..--
    '-- freight and discount---

    '-- Freight/DISCOUNT-  Enter Pressed --

    Private Sub txtGoodsFreight_Discount_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtGoodsDiscount.KeyPress, _
                                                      txtGoodsFreight.KeyPress, _
                                                          txtTotalExpected.KeyPress
        Dim text1 As TextBox = CType(eventSender, TextBox)
        Dim sData As String = text1.Text
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String

        If keyAscii = 13 Then '--enter-
            If (sData = "") OrElse IsNumeric(sData) Then
                '--ok-
                If (sData <> "") AndAlso IsNumeric(sData) Then
                    text1.Text = Format(CDec(sData), "  0.00")
                    If LCase(text1.Name) = "txtsalediscount" Then
                        mDecDiscount = CDec(sData)
                        '- split tax from discount-
                        txtGoodsDiscountAnalysis.Text = ""
                        mDecDiscountTax = mDecDiscount - mDecComputeAmountExTax(mDecDiscount)
                        '-- show discount/tax split.
                        If mDecDiscount > 0 Then
                            txtGoodsDiscountAnalysis.Text = _
                              FormatCurrency((mDecDiscount - mDecDiscountTax), 2) & "/" & FormatCurrency(mDecDiscountTax, 2)
                        End If '-zero-
                    ElseIf LCase(text1.Name) = "txttotalexpected" Then
                        mDecGoodsTotalExpected = CDec(sData)
                    End If
                ElseIf (sData = "") Then
                    If LCase(text1.Name) = "txtsalediscount" Then
                        mDecDiscount = 0
                        txtGoodsDiscountAnalysis.Text = ""
                    End If
                End If '--numeric
                '- update invoice total--
                Call mbUpdateGoodsTotal()
                '-- navigate--
                If LCase(text1.Name) = "txtgoodsfreight" Then
                    txtGoodsDiscount.Select()
                ElseIf LCase(text1.Name) = "txtgoodsdiscount" Then
                    txtTotalExpected.Select()
                ElseIf LCase(text1.Name) = "txttotalexpected" Then
                    btnGoodsCommit.Select()
                End If
            Else
                MsgBox("Amount must be numeric.  !", MsgBoxStyle.Exclamation)
            End If   '--ok-
            keyAscii = 0
        End If '-- enter-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub  '--keypress.-
    '= = = = = = = = = = == =


    Private Sub txtGoodsFreight_TextChanged(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles txtGoodsFreight.TextChanged

    End Sub  '-- freight--
    '= = = =  = = = = = == =

    Private Sub txtGoodsFreight_Validating(ByVal sender As System.Object, _
                                               ByVal ev As CancelEventArgs) _
                                               Handles txtGoodsFreight.Validating, _
                                                       txtGoodsDiscount.Validating, _
                                                       txtTotalExpected.Validating
        Dim text1 As TextBox = CType(sender, TextBox)
        Dim sData As String = text1.Text

        If (sData = "") OrElse IsNumeric(sData) Then  '== OrElse IsNumeric(sData) Then
            '--ok-
            '- update invoice total--
            '-- re-compute invoice-
            '==ok ---  Call mbUpdateGoodsTotal()
        ElseIf (sData <> "") Then
            ev.Cancel = True
            MsgBox("Amount must be numeric.  !", MsgBoxStyle.Exclamation)
        End If  '--numeric-
    End Sub  '--freight-
    '= = = = = = = = = = = =

    Private Sub chkFreightIsIncl_CheckedChanged(ByVal sender As System.Object, _
                                                 ByVal e As System.EventArgs) Handles chkFreightIsIncl.CheckedChanged
        '- update invoice total--
        Call mbUpdateGoodsTotal()

    End Sub '-chkFreightIsIncl-
    '= = = = = = = = = =  = =
    '-===FF->

    '-- Some txt input done..

    Private Sub txtTotalExpected_Validated(ByVal sender As System.Object, _
                                                 ByVal ev As System.EventArgs) _
                                                    Handles txtGoodsFreight.Validated, _
                                                             txtGoodsDiscount.Validated, _
                                                                txtTotalExpected.Validated
        Dim text1 As TextBox = CType(sender, TextBox)
        Dim sData As String = text1.Text

        If (sData <> "") AndAlso IsNumeric(sData) Then
            text1.Text = Format(CDec(sData), "  0.00")
            If LCase(text1.Name) = "txtgoodsdiscount" Then
                mDecDiscount = CDec(sData)
                '- split tax from discount-
                txtGoodsDiscountAnalysis.Text = ""
                mDecDiscountTax = mDecDiscount - mDecComputeAmountExTax(mDecDiscount)
                '-- show discount/tax split.
                If mDecDiscount > 0 Then
                    txtGoodsDiscountAnalysis.Text = _
                      FormatCurrency((mDecDiscount - mDecDiscountTax), 2) & "/" & FormatCurrency(mDecDiscountTax, 2)
                End If '-zero-
            ElseIf LCase(text1.Name) = "txtgoodsfreight" Then
                '== mDecCashout = CDec(sData)
            ElseIf LCase(text1.Name) = "txttotalexpected" Then
                mDecGoodsTotalExpected = CDec(sData)
                chkFreightIsIncl.Select()
            End If
        ElseIf (sData = "") Then
            text1.Text = "0.00"
            If LCase(text1.Name) = "txtsalediscount" Then
                mDecDiscount = 0
                txtGoodsDiscountAnalysis.Text = ""
            ElseIf LCase(text1.Name) = "txtgoodsfreight" Then
                '= mDecCashout = 0
            ElseIf LCase(text1.Name) = "txttotalexpected" Then
                mDecGoodsTotalExpected = 0
            End If
        End If '--numeric
        '- update invoice total--
        Call mbUpdateGoodsTotal()

    End Sub  '--expected-
    '= = = = = = = = = =  = =
    '-===FF->

    '=3107.821- PO Printing Events..==
    '=3107.821- PO Printing Events..==

    '--  Main print Page EVENT handler..--
    '--  Main print Page EVENT handler..--
    '--   FOR PO PRINT FUNCTION..--

    Private Sub mPrintDocument1_PrintPage(ByVal sender As Object, _
                                               ByVal ev As PrintPageEventArgs) _
                                            Handles mPrintDocument1.PrintPage
        Const k_WIDTH_SUPCODE = 120      '--width of Barcode column.-
        Const k_WIDTH_BC = 120  '--width of Description column.-
        Const k_WIDTH_DESCR = 240  '--width of Description column.-
        Const k_WIDTH_TAX = 50     '--width of TAXcode column.-
        Const k_WIDTH_PRICE = 80   '--width of Price column.-
        Const k_WIDTH_QTY = 50      '--width of Qty column.-
        Const k_WIDTH_TOTAL = 100  '--width of Ext.Total column.-

        Const k_PRTWIDTH = 760
        Const k_LEFTMARGIN = 32

        Dim intGreyBGColour As Integer = &HE0E0E0&
        Dim fillColor As Color
        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer
        Dim intYposTopDetails As Integer
        Dim intHdrX As Integer = 150
        Dim intHdrY As Integer = 50
        Dim intLeftMarg As Integer
        Dim L1, ix, x1, intError As Integer
        Dim rowSupplier, rowDetail, row1 As DataRow
        Dim s1, sInvoiceNo, sDocType As String
        '== Dim sJobNo As String = "--"
        Dim datePO As DateTime
        Dim sSupplierBarcode, sSupplierInfo As String
        Dim sDelivery As String
        Dim font1 As userFontDef
        Dim intInfoBoxWidth As Integer = 88  '= 120
        Dim intInfoBoxDepth As Integer = 27
        Dim intAddressBoxWidth As Integer = 340
        Dim intAddressBoxDepth As Integer = 140
        Dim intTermsBoxDepth As Integer = 100
        Dim intXposRhsBox, intXposPrice, intXposExt As Integer
        '-grid-
        Dim intGridYpos, intGridYdepth As Integer
        Dim intLinesAvailable = 40
        Dim penGrid As Pen = Pens.LightGray

        '= MsgBox("Ready to set up print page for PO..")

        fillColor = ColorTranslator.FromOle(intGreyBGColour)
        rowSupplier = mDataTableSupplier.Rows(0)   '--only row..-

        '= MsgBox("Ready to print Invoice to " & msSelectedPrinterName & "..", MsgBoxStyle.Information)
        sSupplierBarcode = rowSupplier.Item("barcode")
        sSupplierInfo = rowSupplier.Item("contactName") & vbCrLf & rowSupplier.Item("supplierName") & vbCrLf & _
                              rowSupplier.Item("address") & vbCrLf & rowSupplier.Item("suburb") & vbCrLf & _
                              rowSupplier.Item("state") & "  " & rowSupplier.Item("postcode") & vbCrLf & _
                               rowSupplier.Item("country") & vbCrLf & _
                               vbCrLf & "Phone:  " & rowSupplier.Item("Phone")
        sDelivery = txtGoodsDeliveryAddress.Text

        '== On Error GoTo PrintInvoice_Error
        intLeftMarg = k_LEFTMARGIN '== iixels-  (16 * PRT_UNIT) '- 240 twips..-
        '--  paint BIZ logo  top left.--
        x1 = 0
        If Not (mImageUserLogo Is Nothing) Then
            x1 = mImageUserLogo.Width
            ev.Graphics.DrawImage(mImageUserLogo, intLeftMarg + k_PRTWIDTH - x1, 0)
        End If
        intXposRhsBox = k_LEFTMARGIN + k_PRTWIDTH - intAddressBoxWidth

        '-- print biz name-
        intXpos = intLeftMarg
        intHdrX = intLeftMarg
        intYpos = intHdrY

        font1.sName = "Lucida Sans"     '== "Tahoma" '== Printer.FontName = "Tahoma"
        font1.lngSize = 18
        font1.bBold = False  '--reset to normal IN CASE LEFT OVER..-
        font1.bUnderline = False
        font1.bItalic = False
        intYpos = gIntPrintTextString(ev, msBusinessName, intHdrX, intYpos, font1)
        intYpos += 4  '- move below main biz name..
        '-- draw line under main name---
        Call gIntDrawLine(ev, intLeftMarg, intYpos, k_PRTWIDTH - x1)  '-- don't draw over logo..
        intYpos += 16  '- move below line..
        intYposTopDetails = intYpos  '--save for top order details pos..

        '-- Main biz Hdr stuff is on First page only..
        If (mIntPageNo <= 0) Then  '-first time-
            Try
                font1.lngSize = 9 '== Printer.FontSize = 8
                font1.bBold = True '== Printer.FontBold = True
                intYpos = gIntPrintTextString(ev, "ABN: " & msBusinessDisplayABN, intHdrX, intYpos, font1)
                '== intYpos = gIntPrintTextString(ev, msBusinessName, intHdrX, intYpos, font1)
                intYpos = gIntPrintTextString(ev, msBusinessAddress1, intHdrX, intYpos, font1)
                intYpos = gIntPrintTextString(ev, msBusinessAddress2, intHdrX, intYpos, font1)
                intYpos = gIntPrintTextString(ev, "Phone:  " & msBusinessPhone, intHdrX, intYpos, font1)
                '--blank line-
                intYpos = gIntPrintTextString(ev, "", intHdrX, intYpos, font1)
                font1.lngSize = 18 '==Printer.FontSize = 18

                intYpos = gIntPrintTextString(ev, "Purchase Order", intHdrX, intYpos, font1, textColour.magenta)
                '-- Blank line..
                intYpos = gIntPrintTextString(ev, "", intHdrX, intYpos, font1)
                font1.lngSize = 9 '== Printer.FontSize = 8

                '= decTotalTax = 0

                '-- Print PO No. and Date..
                intYpos2 = intYpos  '-- save Ypos forSupplier Info...
                '-- Print Order Info box-  top RHS..
                Dim intOrderBoxDepth = 120
                font1.bItalic = False
                intYpos = intYposTopDetails
                L1 = gIntPrintTextInBox(ev, "<b><ul>Order Details:", _
                         intXposRhsBox, intYpos, 16, intAddressBoxWidth, intOrderBoxDepth, True, , 9)
                '-- Print order details..-
                intYpos += 36   '--space under header..
                intXpos = intXposRhsBox + 24
                font1.bBold = True
                '--order no. line..
                L1 = gIntPrintTextString(ev, "Order No: ", intXpos, intYpos, font1)
                font1.bBold = False
                msOurOrderNumberString = FormatNumber(mIntOurOrder_id, "   000") & "/" & txtOrderNoSuffix.Text
                intYpos = gIntPrintTextString(ev, msOurOrderNumberString, intXpos + 112, intYpos, font1)

                intYpos += 6   '--more space under liner..
                '= intXpos = intXposRhsBox + 24

                '--Created Date line..
                font1.bBold = True
                L1 = gIntPrintTextString(ev, "Date Created: ", intXpos, intYpos, font1)
                font1.bBold = False
                s1 = Format(mDatePO_date_created, "dd-MMM-yyyy hh:mm tt")
                intYpos = gIntPrintTextString(ev, s1, intXpos + 112, intYpos, font1)

                '-- Expected --
                Dim datex As Date = DateAdd(DateInterval.Day, mIntSupplierDeliveryDays, Date.Today)
                font1.bBold = True
                L1 = gIntPrintTextString(ev, "Date Expected: ", intXpos, intYpos, font1)
                font1.bBold = False
                s1 = Format(datex, "dd-MMM-yyyy ")
                intYpos = gIntPrintTextString(ev, s1, intXpos + 112, intYpos, font1)

                '--Created by. line..
                font1.bBold = True
                L1 = gIntPrintTextString(ev, "Created By: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = gIntPrintTextString(ev, msCreatedByStaffName, intXpos + 112, intYpos, font1)

                '- print Supplier Name/address in box..--
                intYpos2 = intYpos  '-- set Ypos for Cust...

                '-  ???    mDefaultUserFont.lngSize = 9

                intXpos = intLeftMarg
                If Trim(txtGoodsComments.Text) <> "" Then
                    sDelivery &= vbCrLf & "<b>Comments:" & vbCrLf & txtGoodsComments.Text
                End If
                L1 = gIntPrintTextString(ev, "To:", intXpos + 60, intYpos + 59, font1)
                L1 = gIntPrintTextInBox(ev, sSupplierInfo, _
                                  intXpos, intYpos2 + 44, 85, intAddressBoxWidth, intAddressBoxDepth, True, , 9)

                '-- Delivery-- 9pt Font-
                L1 = gIntPrintTextInBox(ev, "<b>Deliver To:" & vbCrLf & sDelivery, _
                   intXpos + k_PRTWIDTH - intAddressBoxWidth, intYpos2 + 44, 6, _
                                                         intAddressBoxWidth, intAddressBoxDepth, True, , 9)
                '-hdr done.
                intLinesAvailable = 18
                intGridYdepth = 560
                '-- GRID for Detail Lines..--
                intGridYpos = intYpos2 + 44 + intAddressBoxDepth + 12

            Catch ex As Exception
                MsgBox("Error printing PO header." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                ev.HasMorePages = False  '--all done-
                Exit Sub
            End Try

        Else  '- second and subseq. pages.
            intLinesAvailable = 24
            intGridYdepth = 750
            font1.lngSize = 9
            intYpos = gIntPrintTextString(ev, "Order No: " & msOurOrderNumberString, intLeftMarg + 480, intYpos, font1)

            '-- GRID for Detail Lines..--
            intGridYpos = intYpos + 24
        End If  '-first page--

        '- All pages..-
        mIntPageNo += 1

        intXpos = intLeftMarg
        '--draw the "grid"..
        Call gIntDrawLine(ev, intXpos, intGridYpos, k_PRTWIDTH)  '--top bar-
        '--column lines- 8 spaces, nine lines-
        Dim arrayIntWidths() As Integer = _
                      {k_WIDTH_SUPCODE, k_WIDTH_BC, k_WIDTH_DESCR, k_WIDTH_TAX, k_WIDTH_PRICE, k_WIDTH_QTY, k_WIDTH_TOTAL}
        For ix = 0 To UBound(arrayIntWidths)
            ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
            intXpos += arrayIntWidths(ix)
        Next ix
        '--last vert line..
        ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)

        intXpos = intLeftMarg
        Call gIntDrawLine(ev, intXpos, intGridYpos + intGridYdepth, k_PRTWIDTH)  '--bottom bar-

        '-- column header text line..
        '-- Can't use TAB stops because some items are Left-just and some are right..-
        '-- fill column headers BG..
        Const k_HDR_HEIGHT = 22
        intXpos = intLeftMarg
        ev.Graphics.FillRectangle(New SolidBrush(fillColor), intXpos, intGridYpos, k_PRTWIDTH, k_HDR_HEIGHT)
        '-- box for col. hdrs. text-
        '=Dim rectHdr As New RectangleF(intXpos, intGridYpos, k_WIDTH_BC, k_HDR_HEIGHT)

        '-- PRINT column header TEXTS..
        '-- PRINT column header TEXTS..
        font1.lngSize = 8
        font1.bBold = True
        Call gIntPrintTextInRectangle(ev, " Sup.Code", intXpos, intGridYpos, _
                                                 k_WIDTH_BC, k_HDR_HEIGHT, font1, textColour.black, False, True)
        '== e.Graphics.DrawString("Bar Code", drawFont, drawBrush, drawRect, drawFormat)
        intXpos += k_WIDTH_SUPCODE
        Call gIntPrintTextInRectangle(ev, " Barcode", intXpos, intGridYpos, _
                                                 k_WIDTH_BC, k_HDR_HEIGHT, font1, textColour.black, False, True)
        intXpos += k_WIDTH_BC
        Call gIntPrintTextInRectangle(ev, " Description", intXpos, intGridYpos, _
                                                  k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False, True)
        intXpos += k_WIDTH_DESCR
        Call gIntPrintTextInRectangle(ev, "Tax", intXpos, intGridYpos, _
                                        k_WIDTH_TAX, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
        intXpos += k_WIDTH_TAX
        intXposPrice = intXpos  '--save for totals..-
        Call gIntPrintTextInRectangle(ev, "Price_ex", intXpos, intGridYpos, _
                                       k_WIDTH_PRICE, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
        intXpos += k_WIDTH_PRICE
        Call gIntPrintTextInRectangle(ev, "Qty", intXpos, intGridYpos, _
                                         k_WIDTH_QTY, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
        intXpos += k_WIDTH_QTY
        intXposExt = intXpos   '-save-
        L1 = gIntPrintTextInRectangle(ev, "Total $", intXpos, intGridYpos, _
                                         k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
        '-- L1 has line height.
        '--Print Actual detail lines..
        '-
        '- We wouldn't be here if there was nothing left to print..-
        font1.bBold = False
        intYpos = intGridYpos + k_HDR_HEIGHT + L1 '--vert. pos. to 1st detail line..-
        '-- make line rect deep enough for two lines..
        Dim intItemLineHeight As Integer = k_HDR_HEIGHT + (k_HDR_HEIGHT \ 4)
        Dim intCurrentYpos As Integer   '--save for shading-
        font1.lngSize = 9
        While (intLinesAvailable > 0) And (mIntNoItemsStillToPrint > 0)
            '- Fill page or run out all items..
            intXpos = intLeftMarg
            intCurrentYpos = intYpos  '--save for shading-
            '-- ptint items with '-no box-/vertical align at top.
            With clsDgvGoodsItems.Rows(mIntGridLinesPrintCount)
                If (.Cells(k_GRIDCOL_BARCODE).Value <> "") Then  '--not empty-
                    '--  shade alternate rows-
                    If (mIntGridLinesPrintCount Mod 2) = 1 Then
                        ev.Graphics.FillRectangle(New SolidBrush(ColorTranslator.FromOle(&HF0F0F0)), _
                                                             intLeftMarg, intCurrentYpos, k_PRTWIDTH, intItemLineHeight)
                    End If
                    Call gIntPrintTextInRectangle(ev, .Cells(k_GRIDCOL_SUP_CODE).Value, intXpos, intYpos, _
                                                 k_WIDTH_SUPCODE, intItemLineHeight, font1, textColour.black, False, , , True) '=-top-
                    intXpos += k_WIDTH_SUPCODE
                    Call gIntPrintTextInRectangle(ev, .Cells(k_GRIDCOL_BARCODE).Value, intXpos, intYpos, _
                                                k_WIDTH_BC, intItemLineHeight, font1, textColour.black, False, , , True) '-no box-
                    intXpos += k_WIDTH_BC
                    Call gIntPrintTextInRectangle(ev, .Cells(k_GRIDCOL_DESCRIPTION).Value, intXpos, intYpos, _
                                                 k_WIDTH_DESCR, intItemLineHeight, font1, textColour.black, False, , , True) '-no box-
                    intXpos += k_WIDTH_DESCR
                    Call gIntPrintTextInRectangle(ev, .Cells(k_GRIDCOL_TAX_CODE).Value, intXpos, intYpos, _
                                               k_WIDTH_TAX, intItemLineHeight, font1, textColour.black, True, , , True) '-no box-
                    intXpos += k_WIDTH_TAX
                    Call gIntPrintTextInRectangle(ev, .Cells(k_GRIDCOL_COST_EX).Value, intXpos, intYpos, _
                                               k_WIDTH_PRICE, intItemLineHeight, font1, textColour.black, True, , , True) '-no box-
                    intXpos += k_WIDTH_PRICE
                    Call gIntPrintTextInRectangle(ev, .Cells(k_GRIDCOL_QTY).Value, intXpos, intYpos, _
                                               k_WIDTH_QTY, intItemLineHeight, font1, textColour.black, True, , , True) '-no box-
                    intXpos += k_WIDTH_QTY
                    L1 = gIntPrintTextInRectangle(ev, .Cells(k_GRIDCOL_EXTENSION).Value, intXpos, intYpos, _
                                              k_WIDTH_TOTAL, intItemLineHeight, font1, textColour.black, True, , , True) '-no box-
                    mIntActuaItemCount += 1
                End If  '-empty row-
            End With  '-row-
            intYpos += intItemLineHeight  '=L1
            intLinesAvailable -= 1
            mIntNoItemsStillToPrint -= 1
            mIntGridLinesPrintCount += 1  '-starts from zero--
            If mIntNoItemsStillToPrint <= 0 Then
                '--all done
                '-- show totals-- JUST below grid..-
                intYpos = intGridYpos + intGridYdepth
                intYpos += 10
                font1.bBold = True
                font1.lngSize = 9
                Call gIntDrawLine(ev, intXposPrice, intYpos, k_WIDTH_PRICE + k_WIDTH_QTY + k_WIDTH_TOTAL)
                intYpos += 10
                L1 = gIntPrintTextInRectangle(ev, "Totals-  No. Items = " & mIntActuaItemCount & "..", _
                                            intXposPrice, intYpos, _
                                            k_WIDTH_PRICE + k_WIDTH_QTY + k_WIDTH_TOTAL, k_HDR_HEIGHT, _
                                               font1, textColour.black, False, False) '-no rt Align.-
                intYpos += L1 + 8
                '-- subtotal-
                Call gIntPrintTextInRectangle(ev, "Total (Ex)", _
                                           intXposPrice, intYpos, _
                                           k_WIDTH_PRICE + k_WIDTH_QTY, k_HDR_HEIGHT, _
                                              font1, textColour.black, False, False) '-no rt Align.-
                s1 = FormatCurrency(mDecSubTotal - mDecNettTax, 2)
                L1 = gIntPrintTextInRectangle(ev, s1, intXposExt, intYpos, _
                                        k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-no box-
                intYpos += L1
                '-- tax-
                Call gIntPrintTextInRectangle(ev, "Tax: ", _
                                           intXposPrice, intYpos, _
                                           k_WIDTH_PRICE + k_WIDTH_QTY, k_HDR_HEIGHT, _
                                              font1, textColour.black, False, False) '-Right Align.-
                s1 = FormatCurrency(mDecNettTax, 2)
                L1 = gIntPrintTextInRectangle(ev, s1, intXposExt, intYpos, _
                                        k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-no box-
                intYpos += L1
                '-- TOTAL-
                Call gIntPrintTextInRectangle(ev, "Total (inc): ", _
                                           intXposPrice, intYpos, _
                                           k_WIDTH_PRICE + k_WIDTH_QTY, k_HDR_HEIGHT, _
                                              font1, textColour.black, False, False) '-Right Align.-
                s1 = FormatCurrency(mDecInvoiceTotal, 2)
                L1 = gIntPrintTextInRectangle(ev, s1, intXposExt, intYpos, _
                                        k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-no box-
                intYpos += L1
                Call gIntDrawLine(ev, intXposPrice, intYpos + 12, k_WIDTH_PRICE + k_WIDTH_QTY + k_WIDTH_TOTAL)
                ev.HasMorePages = False  '--all done-
            Else  '- more to come-
                ev.HasMorePages = True   '- come back for more--
            End If
        End While '-lines-

        '-- Footer -- every page-
        Dim strPageNo As String = "Page: " & CStr(mIntPageNo)

        Call gbPageFooter(ev, msVersionPOS, strPageNo)


    End Sub 'page event..-
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '-- End pribt event..
    '-- Signal competion..

    Private Sub mPrintDocument1_EndPrint(ByVal sender As Object, _
                                           ByVal ev As PrintEventArgs) _
                                             Handles mPrintDocument1.EndPrint

        '=MsgBox("Printing Completed", MsgBoxStyle.Information)  '-test-
        mbPrintingCompleted = True

    End Sub  '-end print-
    '= = = = = = = = = = = = = = = = =

    '- do print --
    '-  bCapturePDF true if called from PO commit --

    Private Function mbPrintPurchaseOrder(Optional ByVal bCapturePDF As Boolean = False, _
                                          Optional ByVal strSelectedPrinterName As String = "") As Boolean
        Dim sPrinterName As String
        Dim sTitle As String
        Dim sPrintFileFullName As String = ""

        mIntPageNo = 0
        mIntGridLinesPrintCount = 0
        If clsDgvGoodsItems.Rows.Count <= 0 Then
            MsgBox("No items to print..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        mIntNoItemsStillToPrint = clsDgvGoodsItems.Rows.Count
        mIntActuaItemCount = 0
        If bCapturePDF Then  '- From activated startup-
            sPrinterName = msPdfPrinterName
            sTitle = "PurchaseOrder-" & Trim(CStr(mIntOurOrder_id)) & "_" & "Supplier-" & Trim(msSupplierBarcode) & ".pdf"
            sPrintFileFullName = msPDF_FilePath & "\" & sTitle
            '-- set registry key for Adobe pdf writer..
            '=3411.0109= Check if Microsoft PDF or Adobe.
            If (InStr(LCase(msPdfPrinterName), "adobe") > 0) Then
                If Not gbSetAdobeFileName(sPrintFileFullName, msAppFullName) Then
                    Exit Function
                End If
            Else '=Microsoft or other.  use PrintToFile setting.-
            End If
            'If Not gbSetAdobeFileName(sPrintFileFullName, msAppFullName) Then
            '    Exit Function
            'End If
            '-- delete old file if exists.
            If My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
                Try
                    My.Computer.FileSystem.DeleteFile(sPrintFileFullName)
                Catch ex As Exception
                    MsgBox("Failed to delete old file: " & sPrintFileFullName & vbCrLf & ex.Message)
                End Try
            End If
            '- Don't do this for adobe-
            If (InStr(LCase(msInvoicePrinterName), "adobe") <= 0) Then '-no adobe-
                '-- should be Microsoft pdf.
                mPrintDocument1.PrinterSettings.PrintToFile = True
                mPrintDocument1.PrinterSettings.PrintFileName = sPrintFileFullName
            End If
        Else  '-print request-
            mPrintDocument1.PrinterSettings.PrintToFile = False
            If msInvoicePrinterName = "" Then
                MsgBox("No Invoice printer selected..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            If (strSelectedPrinterName <> "") Then
                sPrinterName = strSelectedPrinterName
            Else  '-use dropdown selection
                sPrinterName = msInvoicePrinterName
            End If
        End If
        '-- go--
        Try
            '--  set printer selected..--
            '-- FIX to 4219.1216 !!! --   mPrintDocument1.PrinterSettings.PrinterName = msInvoicePrinterName
            '-- FIX to 4219.1216 !!! --
            mPrintDocument1.PrinterSettings.PrinterName = sPrinterName '= '-- FIX to 4219.1216 !!! -- msInvoicePrinterNamemsInvoicePrinterName
            '--  start the printer..--
            mbPrintingCompleted = False
            mPrintDocument1.Print()
        Catch ex As Exception
            '== MessageBox.Show(ex.Message)
            MsgBox("Error in printing PO.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            '== PrintSalesInvoice = False
        End Try

        '-- send to Email Queue if needed..
        Me.BringToFront()
        If bCapturePDF Then  '- Wait, then send PO to email queue..-
            Dim sSupplierName, sSubject, sPO_Date As String
            Dim sEmailText As String  '= = "Please find attached our Purchase Order No. " & mIntOurOrder_id
            '=msEmailTextInvoice
            '=4201.0929==  Show flag..
            labCreatingPDF.Visible = True
            DoEvents()

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '=3411.0110=- wait for Completion..
            Thread.Sleep(1500)  '--milliseconds..-
            Dim intStart, intFinish As Integer
            intStart = CInt(VB.Timer)
            intFinish = intStart + 60  '= 20
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            While (Not mbPrintingCompleted) And (CInt(VB.Timer) < intFinish)
                DoEvents()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Thread.Sleep(1000)  '--milliseconds..-
            End While
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            '=4201.0929==  Check for completion..
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If (Not mbPrintingCompleted) Then
                MsgBox("Error- PDF Printing not completed..", MsgBoxStyle.Exclamation)
                '=PrintSalesInvoice = False
                labCreatingPDF.Visible = False
                Exit Function
            End If
            '-- Print Completed may happen, but File still not finished.
            '== labCreatingPDF.BackColor = Color.WhiteSmoke
            '-- wait a bit, then test..

            '=3411.0110=- wait for File to appear..
            intStart = CInt(VB.Timer)
            intFinish = intStart + 60  '= 20
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            While Not My.Computer.FileSystem.FileExists(sPrintFileFullName) And (CInt(VB.Timer) < intFinish)
                DoEvents()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Thread.Sleep(1000)  '--milliseconds..-
            End While
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            labCreatingPDF.Visible = False

            If Not My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
                MsgBox("Error- Print file was not created..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            sPO_Date = Format(Today, "dd-MMM-yyyy")
            '-- save PDF ON SERVER SHARE--   NOT in database-
            'If Not gbSaveDocumentToDB(mCnnSql, sPrintFileFullName, sTitle, "Invoice No. " & mIntInvoice_id, "PDF", _
            '                             "INVOICE", mIntCustomer_id, -1, "Sale invoice..", msCustomerEmail, _
            '                                  msEmailTextInvoice) Then

            '==
            '==   Updated.-grh 3519.0317  Started 15-March-2019= 
            '==      >> Purchase Order Printing- Email text needs to have Our OrderNo (Suffix also.)
            '==
            Dim msOurOrderNumberString As String = FormatNumber(mIntOurOrder_id, "   000") & "/" & txtOrderNoSuffix.Text

            sEmailText = "Please find attached our Purchase Order No. " & msOurOrderNumberString
            '= sSubject = "Purchase Order- PO No: JMX " & mIntOurOrder_id & "  Dated :" & sPO_Date
            sSubject = "Purchase Order- PO No: JMX " & msOurOrderNumberString & "  Dated :" & sPO_Date
            sEmailText = Replace(sEmailText, "&&subject", "Re:" & sSubject, , , CompareMethod.Text)
            sEmailText = Replace(sEmailText, "&&greeting", "Dear " & msSupplierName, , , CompareMethod.Text)
            sEmailText = Replace(sEmailText, "&&BusinessName", msBusinessName, , , CompareMethod.Text)
            If Not gbSaveDocumentToEmailQueue(mCnnSql, sPrintFileFullName, sTitle, "PDF", _
                                             "PurchaseOrder", -1, mIntSupplier_id, mIntOurOrder_id, _
                                             sSubject, msSupplierName, _
                                              msSupplierEmailAddress, _
                                              sEmailText, msEmailQueueSharePath) Then
                MsgBox("Saving PO PDF file to Server Queue was not done..", MsgBoxStyle.Exclamation)
            Else  '-  ók=
                MsgBox("Pls Note- The Purchase Order PDF file: " & vbCrLf & sPrintFileFullName & vbCrLf & _
                      vbCrLf & " has been created OK, and queued for emailing.", MsgBoxStyle.Information)
            End If  '-save-
        End If  '-capture..-

    End Function  '-mbPrintPurchaseOrder=
    '= = = = = = = = = = = =  = = = = = = 
    '= = = = = = = = = = = = =  =
    '-===FF->

    '-- print button-

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Call mbPrintPurchaseOrder()  '-For selected printer.
    End Sub  '-Print-
    '= = = = = = = =  = = = = = = = = 

    '-- Email PO..

    Private Sub picEmailPO_Click(sender As Object, e As EventArgs) Handles picEmailPO.Click

        If (msSupplierEmailAddress = "") Then
            MsgBox("No supplier email address !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '--email addres-
        If (msPdfPrinterName = "") Then
            MsgBox("No PDF printer !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '--email addres-

        Call mbPrintPurchaseOrder(True)  '-For PDF printer.

    End Sub  '-picEmail-
    '= = = = = = = = == = = = = = =


    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cboInvoicePrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboInvoicePrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboInvoicePrinters.SelectedIndex >= 0) Then
            msInvoicePrinterName = cboInvoicePrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_invoicePrtSettingKey, msInvoicePrinterName) Then
                '== gbSaveLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, msInvoicePrinterName) Then
                MsgBox("Failed to save invoice printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  InvoicePrinters-
    '= = = = = = = = = = = = =  =
    '-===FF->

    '-- GoodsCancel --

    Private Sub btnGoodsCancel_Click(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) _
                                         Handles btnGoodsCancel.Click, btnGoodsCancel2.Click

        '-- confirm cancel--
        If (clsDgvGoodsItems.Rows.Count > 0) And (mDecInvoiceTotal > 0) And _
              ((mbIsPurchaseOrder And mbIsNewPO) Or (Not mbIsPurchaseOrder)) Then
            If (MsgBox("Discard current data ?", _
                        MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                Exit Sub
            End If
        End If  '--count-

        '- ok.. cancel this lot..
        '= MsgBox("Test.. cancelling..", MsgBoxStyle.Information)

        Call mbClearInvoice()
        '= grpBoxIncludesTax.Enabled = False
        panelInvoiceNo.Enabled = False
        chkLoadPO.Checked = False
        chkLoadPO.Enabled = True
        btnLoadPO.Enabled = False
        chkPriceIncludesTax.Enabled = True
        chkPriceIncludesTax.Checked = False

        btnViewPO.Enabled = False         '
        '-btnNewBlankPO.Enabled = False
        '- btnNewStandardOrder.Enabled = False '
        mbIsNewPO = False

        btnPrint.Enabled = False
        picEmailPO.Enabled = False
        btnCancelPO.Enabled = False

        mIntSupplier_id = -1
        mIntOurOrder_id = -1

        '= btnLookupSupplier.Enabled = True
        labHelp.Text = "Select Supplier.."

        txtSupplierName.Text = ""
        txtGoodsDeliveryAddress.Text = ""
        If mbIsPurchaseOrder Then
            btnNewStandardOrder.Enabled = True
            btnNewBlankPO.Enabled = True
        End If

        txtSupplierBarcode.Text = ""
        txtSupplierBarcode.Enabled = True

        txtSupplierBarcode.Select()

    End Sub '--GoodsCancel--
    '= = = = = = = = = = = = = 
    '-===FF->

    '-CancelPO=

    Private Sub btnCancelPO_Click(sender As Object, e As EventArgs) Handles btnCancelPO.Click

        If (mIntOurOrder_id <= 0) Then
            Exit Sub
        End If
        If MsgBox("Just to be sure ! " & vbCrLf & _
                                    "Are you sure it's OK to cancel this Purchase Order forever ?", _
          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
            Exit Sub
        End If
        '- mark as cancelled.
        Dim sSql, sUpdateList As String

        sUpdateList = "isCancelled=1, "
        '= sUpdateList &= " cancelled_staff_id= " & CStr(mIntStaff_id) & ", "
        sUpdateList &= " date_modified = CURRENT_TIMESTAMP "
        '--- finish update--
        sSql = "UPDATE dbo.PurchaseOrder SET " & sUpdateList
        sSql &= "  WHERE order_id= " & CStr(mIntOurOrder_id) & "; "
        If Not mbExecuteSql(mCnnSql, sSql, False, Nothing) Then
            MsgBox("Cancelling PO FAILED..", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            MsgBox("ok.. PO # " & mIntOurOrder_id & " was cancelled..", MsgBoxStyle.Exclamation)

        End If  '--exec invoice-

        labHelp2.Text = ""
        '=End If  '-can email-

        btnPrint.Enabled = False
        picEmailPO.Enabled = False
        panelPrinting.Enabled = False

        Call mbClearInvoice()
        labHelp.Text = "Select Supplier.."
        txtSupplierBarcode.Text = ""
        txtSupplierName.Text = ""
        txtSupplierBarcode.Enabled = True
        '= btnLookupSupplier.Enabled = True
        labPO_id.Text = ""
        btnNewStandardOrder.Enabled = True
        btnNewBlankPO.Enabled = True

        txtSupplierBarcode.Select()

    End Sub  '-CancelPO-
    '= = = = = = = = = = = =
    '-===FF->

    '-- Goods C o m m i t --
    '-- Goods C o m m i t --

    Private Sub btnGoodsCommit_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles btnGoodsCommit.Click
        Dim sqlTransaction1 As OleDbTransaction
        Dim s1, sSql, sQty, sStock_id As String
        Dim sSupcodeSql, sSupcode As String
        Dim sSelectedInvoicePrinterName As String = ""
        Dim v2 As Object
        Dim ix, intGoods_id, intID, intPO_id, intPO_line_id As Integer
        Dim intGoodsLine_id, intSerial_id
        Dim row1 As DataGridViewRow
        Dim decAmount, decCost_ex, decSell_ex, decOriginalCost_ex As Decimal
        Dim decTotal_ex As Decimal = (mDecSubTotalEx + mDecFreightEx - mDecDiscountTax)
        Dim decCost_ex_extended, decCost_inc_extended As Decimal
        Dim sBarcode, sDescription, sSerialList As String
        Dim sWarrantyDate As String
        '--serial collection.-
        Dim asSep As String() = {vbCrLf}
        Dim asNewSerials As String()
        '- GR updates PO..-
        Dim bIsReceiving As Boolean = False   '-to update PO if neeeded-
        Dim bPO_isCompleted As Boolean = True  '-- will be falsified after testing-
        Dim bCanEmail As Boolean = False
        Dim bCanPrint As Boolean = False

        If (Trim(txtOrderNoSuffix.Text) = "") Then
            MsgBox("Purchase Orders and Goods Received must have our Order No Suffix.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '-suffix-
        If mbIsPurchaseOrder Then
            '-- C o m m i t  Purchase Order ---
            '-- C o m m i t  Purchase Order ---
            '-- C o m m i t  Purchase Order ---
            mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-
            If mbIsNewPO Then
                If MsgBox("Important ! " & vbCrLf & _
                               " Purchase Order contents can't be changed later (except to be cancelled).." & vbCrLf & vbCrLf & _
                               " Once committed, the order will be considered to have been sent.." & vbCrLf & vbCrLf & _
                                     "Sure it's OK to commit this Purchase Order ?", _
                          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
                Dim dlgQueryCommit1 As dlgQueryCommit

                dlgQueryCommit1 = New dlgQueryCommit
                '=3411.0109=
                dlgQueryCommit1.labPdfPrinter.Text = msPdfPrinterName

                '-check Customer email address.
                dlgQueryCommit1.labEmail.Text = ""
                If (msSupplierEmailAddress <> "") And (msPdfPrinterName <> "") Then
                    dlgQueryCommit1.chkEmail.Checked = True
                    dlgQueryCommit1.labEmail.Text = msSupplierEmailAddress
                Else '-no emai-
                    dlgQueryCommit1.chkEmail.Enabled = False
                    dlgQueryCommit1.chkEmail.Checked = False
                End If
                dlgQueryCommit1.labDocType.Text = "Purchase Order"
                dlgQueryCommit1.labDocType.ForeColor = Color.SaddleBrown
                dlgQueryCommit1.labQuestion.Text = "OK to commit this Purchase Order ?"
                dlgQueryCommit1.labMessage.Text = "Total order is: " & vbCrLf & FormatCurrency(mDecInvoiceTotal, 2)
                '--  Make "auto" printing easy to flow on..
                dlgQueryCommit1.chkPrint.Checked = True
                dlgQueryCommit1.ShowDialog()
                If dlgQueryCommit1.DialogResult = DialogResult.Cancel Then
                    Exit Sub
                Else  '-ok-
                    '-save selections.
                    sSelectedInvoicePrinterName = dlgQueryCommit1.selectedInvoicePrinter
                    '= sSelectedReceiptPrinterName = dlgQueryCommit1.selectedReceiptPrinter
                End If
                '-ok=
                '--  Get checkbox results..
                '= MsgBox("TEST-  " & vbCrLf & "Email checked=" & dlgQueryCommit1.chkEmail.Checked & vbCrLf & _
                '=                 "Print checked= " & dlgQueryCommit1.chkPrint.Checked, MsgBoxStyle.Information)
                '- save print preferences..
                bCanPrint = dlgQueryCommit1.chkPrint.Checked
                bCanEmail = dlgQueryCommit1.chkEmail.Checked

                labHelp2.Text = "Saving New Purchase Order.."
                '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
                '- 2. INSERT Main PO record -
                '- 3. Retrieve order_id. (IDENTITY of PO record written.)-
                '- 4.     FOR EACH PO grid Line- INSERT PO-Line.. -
                '- 5.  Commit TRANSACTION.-

                '-ok-
                '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
                sqlTransaction1 = mCnnSql.BeginTransaction

                '- 2. INSERT Main PurchaseOrder Record record -  
                sSql = "INSERT INTO dbo.PurchaseOrder ("
                sSql &= "  staff_id, supplier_id, due_date, orderNoSuffix,  "
                sSql &= "  delivery_address, comments "
                sSql &= ") "
                sSql &= "VALUES ( " & CStr(mIntStaff_id) & ", " & CStr(mIntSupplier_id) & ", "
                sSql &= " '" & VB.Format(dtPickerInvoiceDate.Value, "dd-MMM-yyyy") & "', "
                sSql &= "'" & gsFixSqlStr(txtOrderNoSuffix.Text) & "', "
                sSql &= "'" & gsFixSqlStr(txtGoodsDeliveryAddress.Text) & "', "
                sSql &= "'" & gsFixSqlStr(txtGoodsComments.Text) & "' "
                sSql &= "); "
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    labHelp2.Text = "Saving PurchaseOrder FAILED.."
                    Exit Sub
                End If  '--exec PO-

                '- 3. Retrieve PO No. (IDENTITY of GOODS Invoice record written.)-
                sSql = "SELECT CAST(IDENT_CURRENT ('dbo.PurchaseOrder') AS int);"
                If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                    intPO_id = intID
                    '-- update invoice display later..-
                Else
                    MsgBox("Failed to retrieve latest PurchaseOrder ID No..", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                labPO_id.Text = CStr(intPO_id)
                MsgBox("PurchaseOrder-id is: " & intPO_id, MsgBoxStyle.Information)

                '- 4. FOR EACH PO Line- INSERT PO-Line.. -
                '--  4201.0520-  Make batch of lines..
                sSql = ""
                sSupcodeSql = ""
                If (clsDgvGoodsItems.Rows.Count > 0) Then
                    For Each row1 In clsDgvGoodsItems.Rows
                        '-- BYPASS EMPTY edit row at the end..--
                        If (row1.Cells(k_GRIDCOL_BARCODE).Value <> "") Then  '--not empty-
                            With row1
                                sQty = .Cells(k_GRIDCOL_QTY).Value
                                sStock_id = .Cells(k_GRIDCOL_STOCK_ID).Value
                                '= decCost_ex_extended = _
                                '=      CDec(.Cells(k_GRIDCOL_EXTENSION).Value) - CDec(.Cells(k_GRIDCOL_COST_TAX_EXTENDED).Value)
                                sSql &= "INSERT INTO dbo.PurchaseOrderLine ("
                                sSql &= " order_id, supplier_id, stock_id, suppliercode, goods_taxCode, "
                                sSql &= " cost_ex,  cost_inc, quantity "
                                sSql &= ") "
                                sSql &= "VALUES ( "
                                sSql &= CStr(intPO_id) & ", " & CStr(mIntSupplier_id) & ", " & sStock_id & ",  "
                                sSql &= "'" & gsFixSqlStr(.Cells(k_GRIDCOL_SUP_CODE).Value) & "', "
                                sSql &= "'" & gsFixSqlStr(.Cells(k_GRIDCOL_TAX_CODE).Value) & "', "
                                sSql &= Replace(.Cells(k_GRIDCOL_COST_EX).Value, ",", "") & ", "
                                '== sSql &= Replace(.Cells(k_GRIDCOL_COST_TAX).Value, ",", "") & ", "
                                sSql &= Replace(.Cells(k_GRIDCOL_COST_INC).Value, ",", "") & ", "
                                sSql &= sQty
                                sSql &= "); " & vbCrLf
                                ''-- insert this row..-
                                'If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                                '    labHelp2.Text = "Saving Goods LINE FAILED.."
                                '    Exit Sub
                                'End If  '--exec invoiceLine-
                                '- Update -supcode- if needed.
                                sSupcode = Trim(.Cells(k_GRIDCOL_NEW_SUPPLIERCODE).Value)
                                If (sSupcode <> "") Then
                                    '-change or update.
                                    sSupcodeSql &= "IF EXISTS (SELECT * FROM [SupplierCode] " & _
                                                      "WHERE (supplier_id=" & CStr(mIntSupplier_id) & ") AND " & _
                                                                                  "(stock_id=" & sStock_id & ") )" & vbCrLf
                                    sSupcodeSql &= " UPDATE [SupplierCode] SET " & _
                                                             " supcode='" & sSupcode & "', " & vbCrLf & _
                                                             " date_modified= CURRENT_TIMESTAMP " & vbCrLf & _
                                                  "     WHERE (supplier_id=" & CStr(mIntSupplier_id) & ") AND " & _
                                                                              "(stock_id=" & sStock_id & ")" & vbCrLf
                                    sSupcodeSql &= "ELSE INSERT INTO [SupplierCode] (supcode, supplier_id, stock_id) " & _
                                                        " VALUES ('" & sSupcode & "', " & CStr(mIntSupplier_id) & ", " & sStock_id & ") " & vbCrLf
                                End If  '--supcode
                                '- ok.. 
                            End With  '-row1-
                        End If  '-empty-
                    Next row1
                    If (sSql <> "") Then
                        '-- insert batch of lines...-
                        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                            labHelp2.Text = "Saving Goods LINE batch FAILED.."
                            Exit Sub
                        End If  '--exec invoiceLine-
                    End If  '-lines-
                    If (sSupcodeSql <> "") Then
                        '--update supcodes..
                        If Not mbExecuteSql(mCnnSql, sSupcodeSql, True, sqlTransaction1) Then
                            labHelp2.Text = "Saving SupplierCodes batch FAILED.."
                            Exit Sub
                        End If  '--exec invoiceLine-
                    End If  '-supcodes-
                End If '-rows count-
                '- 5.  Commit Full PO TRANSACTION.-
                Try
                    sqlTransaction1.Commit()
                    MsgBox("Transaction committed ok..  Purchase Order Id: " & intPO_id, MsgBoxStyle.Information)
                    labPO_id.Text = CStr(intPO_id)
                    mIntOurOrder_id = intPO_id
                    msCreatedByStaffName = msStaffName
                    mDatePO_date_created = DateTime.Today
                Catch ex As Exception
                    MsgBox("PO Transaction commit FAILED.. " & intPO_id, MsgBoxStyle.Exclamation)
                    labPO_id.Text = ""
                End Try
                If bCanEmail Then
                    Call mbPrintPurchaseOrder(True)  '-capture pdf-
                End If
                If bCanPrint Then
                    Call mbPrintPurchaseOrder(False, sSelectedInvoicePrinterName)  '-NO capture pdf-
                End If
            Else  '-updating a PO..  ONE DAY ??

                '==  Delete all old PO-lones -
                '== Update PO -
                '-- Add existing grid lines as PO-lines-
                '-- COMMIT-
            End If  '-new or update-

            '-- PO commits done--
            '-- PO commits done--
            labHelp2.Text = ""
            '=End If  '-can email-

            btnPrint.Enabled = False
            picEmailPO.Enabled = False
            panelPrinting.Enabled = False

            Call mbClearInvoice()

            labHelp.Text = "Select Supplier.."
            txtSupplierBarcode.Text = ""
            txtSupplierName.Text = ""
            txtSupplierBarcode.Enabled = True
            '= btnLookupSupplier.Enabled = True
            labPO_id.Text = ""

            txtSupplierBarcode.Select()

            '-- PO Commit done..--
            Exit Sub  '--PO Done..-
        End If '-IsPurchaseOrder-
        '--PO Done..- - - - - - - -

        '- G o o d s Received-
        '- G o o d s Received-
        '-- C o m m i t  G o o d s Received-
        '-- C o m m i t  G o o d s Received-
        bPO_isCompleted = True  '-- will be falsified after testing-
        '== make TEMP  warranty date.
        sWarrantyDate = Format(DateAdd(DateInterval.Month, 12, Today), "dd-MMM-yyyy")

        '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        '- 2. INSERT Main GoodsReceived record -
        '- 3. Retrieve Goods_id. (IDENTITY of Goods record written.)-
        '- 4. FOR EACH Goods Line- INSERT Invoice-Line.. -
        '--          And ->  Retrieve Goods Line_id (IDENTITY of Line record written.)- -
        '--                 ( for SerialAuditTrail)
        '--          And ->  UPDATE Stock Record with +/- Qty.  -
        '-           And ->  FOR EACH Goods Line- WITH Serial-Nos attached.. -
        '--                      FOR EACH SerialNo Item- 
        '--                             INSERT New SerialAudit RECORD;  status -> INSTOCK. -
        '--                                Retrieve SerialAudit RECORD Id... -
        '--                             And ->  INSERT SerialAuditTrail record.. (GOODSRECVD ).-
        '- -
        '- 5.  Commit TRANSACTION.-
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If (txtSupplierInvoiceNo.Text = "") Then
            MsgBox("Goods Received must have the Supplier's Invoice No..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '-supplier inv.

        If Not MsgBox("Goods Received records can't be reversed.." & vbCrLf & vbCrLf & _
                      "Make sure that all Serial-Numbers have been entered." & vbCrLf & vbCrLf & _
                      "Are you Sure it's OK to commit this Goods Received Invoice ?", _
                     MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            Exit Sub
        End If

        labHelp2.Text = "Checking Serials.."
        '--  make sure all serial no's have been entered..
        If (clsDgvGoodsItems.Rows.Count > 0) Then
            For Each row1 In clsDgvGoodsItems.Rows
                '-- BYPASS EMPTY edit row at the end..--
                If (row1.Cells(k_GRIDCOL_BARCODE).Value <> "") Then  '--not empty-
                    With row1
                        sBarcode = .Cells(k_GRIDCOL_BARCODE).Value
                        sDescription = .Cells(k_GRIDCOL_DESCRIPTION).Value
                        sQty = .Cells(k_GRIDCOL_QTY).Value
                        If (.Cells(k_GRIDCOL_TRACK_SERIAL).Value = "1") Then
                            sSerialList = .Cells(k_GRIDCOL_SERIALNOLIST).Value
                            '==USE .net string.split --
                            asNewSerials = sSerialList.Split(asSep, StringSplitOptions.RemoveEmptyEntries)
                            '==asNewSerials = Split(sSerialList, vbCrLf)
                            If (asNewSerials.Length < CInt(sQty)) Or (sSerialList = "") Then
                                MsgBox("Insufficient serial numbers for Barcode: " & vbCrLf & _
                                           sBarcode & ": " & sDescription, MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If  '-qty-
                        End If  '--track serial-
                    End With
                End If
            Next row1
        Else
            MsgBox("Error: No goods items found !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '--count-

        '-- Check expected total.. mDecGoodsTotalExpected vs mDecInvoice Total.--
        If (mDecGoodsTotalExpected <> mDecInvoiceTotal) Then
            If Not MsgBox("Important Note-" & vbCrLf & _
                           " The Goods Received Items total is different from the Suppliers Expected Total- " & vbCrLf & vbCrLf & _
                          "Make sure that all Received Stock Items have been entered with their latest prices.." & vbCrLf & vbCrLf & _
                          "Are you still sure it's OK now to commit this Goods Received Invoice ?", _
                         MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Exit Sub
            End If  '-yes-
        End If  '--  <>--
        labHelp2.Text = "Saving Goods Invoice.."
        mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-

        '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        sqlTransaction1 = mCnnSql.BeginTransaction

        '-- Goods (Invoice) totals-
        '=  mDecTotalItemsTax As Decimal = 0
        '=  mDecSubTotal As Decimal
        '=  mDecFreightEx As Decimal
        '=  mDecFreightTax As Decimal
        '=  mDecFreightInc As Decimal
        '= mDecInvoiceTotal As Decimal  '--total Debits.-
        '-- mDecGoodsTotalExpected

        '- 2. INSERT Main Goods-Invoice record -  '- mFreightTaxCode-
        sSql = "INSERT INTO dbo.GoodsReceived ("
        sSql &= "  staff_id, supplier_id, invoice_no, invoice_date, orderNoSuffix, order_id, "
        sSql &= "  subtotal_ex, subtotal_tax, subtotal_inc, "
        sSql &= "   freight_ex, freight_taxCode, freight_taxPercentage, freight_tax, freight_inc, "
        sSql &= "  discount_nett,  discount_tax, "
        sSql &= "  total_ex, total_tax, total_inc, total_expected, comments "
        sSql &= ") "
        sSql &= "VALUES ( " & CStr(mIntStaff_id) & ", " & CStr(mIntSupplier_id) & ", "
        sSql &= "'" & gsFixSqlStr(txtSupplierInvoiceNo.Text) & "', " & _
                                          " '" & VB.Format(dtPickerInvoiceDate.Value, "dd-MMM-yyyy") & "', "
        sSql &= "'" & gsFixSqlStr(txtOrderNoSuffix.Text) & "', " & CStr(mIntOurOrder_id) & ", "
        sSql &= CStr(mDecSubTotalEx) & ", " & CStr(mDecTotalItemsTax) & ", " & CStr(mDecSubTotal) & ", "
        sSql &= CStr(mDecFreightEx) & ", '" & mFreightTaxCode & "', " & CStr(mDecGST_rate) & ", "
        sSql &= CStr(mDecFreightTax) & ", " & CStr(mDecFreightInc) & ", "
        sSql &= CStr(mDecDiscount - mDecDiscountTax) & ", " & CStr(mDecDiscountTax) & ", "
        sSql &= CStr(decTotal_ex) & ", " & CStr(mDecNettTax) & ", " & CStr(mDecInvoiceTotal) & ", "
        sSql &= CStr(mDecGoodsTotalExpected) & ", "
        sSql &= "'" & gsFixSqlStr(txtGoodsComments.Text) & "' "
        sSql &= "); "
        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
            labHelp2.Text = "Saving Goods Invoice FAILED.."
            Exit Sub
        End If  '--exec invoice-

        '- 3. Retrieve Goods No. (IDENTITY of GOODS Invoice record written.)-
        sSql = "SELECT CAST(IDENT_CURRENT ('dbo.GoodsReceived') AS int);"
        If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
            intGoods_id = intID
            '-- update invoice display later..-
        Else
            MsgBox("Failed to retrieve latest GOODS Received ID No..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        '= MsgBox("Goods-id is: " & intGoods_id, MsgBoxStyle.Information)

        '- 4. FOR EACH Goods Line- INSERT Invoice-Line.. -
        '--          And ->  Retrieve Goods Line_id (IDENTITY of Line record written.)- -
        '--                 ( for SerialAuditTrail)
        If (clsDgvGoodsItems.Rows.Count > 0) Then
            For Each row1 In clsDgvGoodsItems.Rows
                '-- BYPASS EMPTY edit row at the end..--
                If (row1.Cells(k_GRIDCOL_BARCODE).Value <> "") Then  '--not empty-
                    With row1
                        sQty = .Cells(k_GRIDCOL_QTY).Value
                        sStock_id = .Cells(k_GRIDCOL_STOCK_ID).Value
                        decCost_ex = CDec(.Cells(k_GRIDCOL_COST_EX).Value)
                        decSell_ex = CDec(.Cells(k_GRIDCOL_SELL_EX).Value)
                        decOriginalCost_ex = CDec(.Cells(k_GRIDCOL_ORIGINAL_COST_EX).Value)
                        decCost_ex_extended = _
                            CDec(.Cells(k_GRIDCOL_EXTENSION).Value) '=  - CDec(.Cells(k_GRIDCOL_COST_TAX_EXTENDED).Value)
                        decCost_inc_extended = _
                             decCost_ex_extended + CDec(.Cells(k_GRIDCOL_COST_TAX_EXTENDED).Value)
                        sSql = "INSERT INTO dbo.GoodsReceivedLine ("
                        sSql &= " goods_id, stock_id, goods_taxCode, goods_taxPercentage, "
                        sSql &= " cost_ex, cost_tax, cost_inc, "
                        sSql &= " sell_ex, quantity, "
                        sSql &= " total_ex, total_tax, total_inc "
                        sSql &= ") "
                        sSql &= "VALUES ( "
                        sSql &= CStr(intGoods_id) & ", " & sStock_id & ",  "
                        sSql &= "'" & gsFixSqlStr(.Cells(k_GRIDCOL_TAX_CODE).Value) & "', " & CStr(mDecGST_rate) & ", "
                        sSql &= Replace(.Cells(k_GRIDCOL_COST_EX).Value, ",", "") & ", "
                        sSql &= Replace(.Cells(k_GRIDCOL_COST_TAX).Value, ",", "") & ", "
                        sSql &= Replace(.Cells(k_GRIDCOL_COST_INC).Value, ",", "") & ", "
                        sSql &= Replace(.Cells(k_GRIDCOL_SELL_EX).Value, ",", "") & ", " & sQty & ", "
                        sSql &= CStr(decCost_ex_extended) & ", "
                        sSql &= Replace(.Cells(k_GRIDCOL_COST_TAX_EXTENDED).Value, ",", "") & ", "
                        sSql &= CStr(decCost_inc_extended)
                        sSql &= "); "
                        '-- insert this row..-
                        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                            labHelp2.Text = "Saving Goods LINE FAILED.."
                            Exit Sub
                        End If  '--exec invoiceLine-
                        '- ok.. 
                        '-- FOR Serials ONLY- create SerialAudit Identity record-
                        '--  and Serial Audit Trail record.
                        If (.Cells(k_GRIDCOL_TRACK_SERIAL).Value = "1") Then
                            '-get ID of last line inserted.. (For Serial-Audit-Trail).
                            sSql = "SELECT CAST(IDENT_CURRENT ('dbo.GoodsReceivedLine') AS int);"
                            If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                                intGoodsLine_id = intID  '-- this goes into serialAuditTrail below..-
                            Else
                                MsgBox("Failed to read latest GOODS Received LINE-ID No..", MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If
                            asNewSerials = Split(.Cells(k_GRIDCOL_SERIALNOLIST).Value, vbCrLf)
                            If (asNewSerials.Length > 0) And (asNewSerials.Length <= CInt(sQty)) Then
                                For ix = 0 To (asNewSerials.Length - 1)
                                    '-- Insert Serial Audit record for this unique stock/serial combo..
                                    sSql = "INSERT INTO dbo.SerialAudit ("
                                    sSql &= " stock_id, SerialNumber, isInStock,"
                                    sSql &= " status, warranty_date"
                                    sSql &= ") "
                                    sSql &= "VALUES ( "
                                    sSql &= sStock_id & ", '" & gsFixSqlStr(asNewSerials(ix)) & "', 1, "  '--IS in Stock-
                                    sSql &= "'INSTOCK', '" & sWarrantyDate & "'"
                                    sSql &= "); "
                                    '-- insert this SERIAL..-
                                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                                        labHelp2.Text = "Saving Serial Audit record FAILED.."
                                        Exit Sub
                                    End If  '--exec serial-
                                    '-- get Audit Id--
                                    sSql = "SELECT CAST(IDENT_CURRENT ('dbo.SerialAudit') AS int);"
                                    If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                                        intSerial_id = intID  '-- this goes into serialAuditTrail below..-..-
                                    Else
                                        MsgBox("Failed to read latest Audit-ID No..", MsgBoxStyle.Exclamation)
                                        Exit Sub
                                    End If
                                    '--ok.. insert SerialAuditTrail record.-
                                    '- This is the "transaction" trail for this serial.-
                                    sSql = "INSERT INTO dbo.SerialAuditTrail ("
                                    sSql &= " stock_id, SerialAudit_id, "
                                    sSql &= "  tran_type, type_id, type_line_id, movement"
                                    sSql &= ") "
                                    sSql &= "VALUES ( "
                                    sSql &= sStock_id & ", " & CStr(intSerial_id) & ", "
                                    sSql &= "'GoodsReceived', " & CStr(intGoods_id) & ", " & CStr(intGoodsLine_id) & ", 1"
                                    sSql &= "); "
                                    '-- insert this TRAIL rec..-
                                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                                        labHelp2.Text = "Saving Serial Audit TRAIL record FAILED.."
                                        Exit Sub
                                    End If  '--exec serial-
                                Next ix  '--all (qty) serials for this goodsLine.
                            End If '-length/qty-
                        End If  '-TRACK_SERIAL-

                        '- Now update stock qty.. This Goods Line..-
                        sSql = "UPDATE dbo.stock SET qtyInStock=(qtyInStock +" & sQty & ")"
                        '=3501.0829- Update UNIT cost if changed..
                        '=If decCost_ex <> decOriginalCost_ex Then
                        '=End If
                        '-= 3519.0424-  UPDATE anyway.
                        sSql &= ", costExTax=" & CStr(decCost_ex)
                        sSql &= ", sellExTax=" & CStr(decSell_ex)
                        sSql &= "  WHERE (stock_id = " & sStock_id & " );"
                        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                            labHelp2.Text = "Update Stock Failed.."
                            Exit Sub
                        End If  '--exec Update stock qty-

                        '-- UPDATE PURCHASE ORDER LINES, and PO record..
                        '--  If this GoodsRcvd was stacked with our PO data, then
                        '--   update the relevent PO lines with qty received for this item..-
                        '= MsgBox("DEBUG- Updating original P/O no. " & mIntOurOrder_id & ";  Line-id: " & _
                        '=                           .Cells(k_GRIDCOL_PO_LINE_ID).Value, MsgBoxStyle.Information)
                        If (mIntOurOrder_id > 0) Then
                            If IsNumeric(.Cells(k_GRIDCOL_PO_LINE_ID).Value) Then
                                intPO_line_id = CInt(.Cells(k_GRIDCOL_PO_LINE_ID).Value)
                                If (intPO_line_id > 0) Then  '-have line-
                                    sSql = "UPDATE dbo.PurchaseOrderLine SET qtyReceived=(qtyReceived +" & sQty & ")"
                                    sSql &= "  WHERE (order_id = " & mIntOurOrder_id & " ) "
                                    sSql &= "    AND (line_id = " & intPO_line_id & " ); "
                                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                                        labHelp2.Text = "Update PO line Failed.."
                                        Exit Sub
                                    End If  '--exec Update stock qty-
                                    bIsReceiving = True
                                    '-- check mDicPO_ItemsOutstanding -
                                    '-- clear bPO_isCompleted if any qty still outstanding-
                                    If IsNumeric(sQty) Then
                                        If mDicPO_ItemsOutstanding.Item(intPO_line_id) > CInt(sQty) Then
                                            bPO_isCompleted = False   '-not exhausted this line-
                                        End If
                                    End If  '-numeric-
                                End If  '-have id-
                            End If '-id numeric-
                        End If '-order-id-
                    End With  '-row1
                End If  '--empty row-
            Next row1  '-- Item Line-
        End If  '--rows.count-

        '--4.2 -- 
        '--  Update Original PO if needed. 
        If (mIntOurOrder_id > 0) Then
            If bIsReceiving Or bPO_isCompleted Then
                sSql = ""
                If bIsReceiving Then
                    sSql &= "UPDATE dbo.PurchaseOrder SET IsReceiving=1 "
                    sSql &= "  WHERE (order_id = " & mIntOurOrder_id & " ); "
                End If  '--receiving-
                If bPO_isCompleted Then
                    sSql &= " UPDATE dbo.PurchaseOrder  SET isCompleted=1 "
                    sSql &= "  WHERE (order_id = " & mIntOurOrder_id & " ); "
                End If  '--receiving-
                If (sSql <> "") Then
                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                        labHelp2.Text = "Update PO receiving/completed status Failed.."
                        Exit Sub  '-was rolled back-
                    End If  '--exec Update PO-
                End If  '--sSql-
            End If  '-isReceiving-
        End If  '-order-

        '- 5.  Commit the complete TRANSACTION.-
        Try
            sqlTransaction1.Commit()
            MsgBox("Transaction committed ok..  Goods Invoice Id: " & intGoods_id & vbCrLf & _
                    "Now to print stock labels..", MsgBoxStyle.Information)

            '--  build collection of barcodes received..
            Dim colGridLabels, col1 As Collection
            Dim frmStockLabels1 As frmStockLabels

            colGridLabels = New Collection '--not to be empty-
            If (clsDgvGoodsItems.Rows.Count > 0) Then
                For Each row1 In clsDgvGoodsItems.Rows
                    '-- BYPASS EMPTY edit row at the end..--
                    col1 = New Collection
                    If (row1.Cells(k_GRIDCOL_BARCODE).Value <> "") Then  '--not empty-
                        With row1
                            col1.Add(.Cells(k_GRIDCOL_BARCODE).Value, "barcode")
                            col1.Add(.Cells(k_GRIDCOL_QTY).Value, "qty")
                        End With
                        colGridLabels.Add(col1)
                    End If '-barcode
                Next row1
                Dim frmDummy As New Form
                frmStockLabels1 = New frmStockLabels(frmDummy, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                  msVersionPOS, mImageUserLogo, mIntStaff_id, msStaffName, colGridLabels)
                frmStockLabels1.ShowDialog()
            End If  '-count-

        Catch ex As Exception
            MsgBox("Transaction commit FAILED.. " & intGoods_id, MsgBoxStyle.Exclamation)
        End Try
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        labHelp2.Text = ""

        Call mbClearInvoice()
        labHelp.Text = "Select Supplier.."
        txtSupplierBarcode.Enabled = True
        '= btnLookupSupplier.Enabled = True

        txtSupplierBarcode.Select()

    End Sub '-- goods commit--
    '= = = = = = = = = = = = =
    '-===FF->

    Private Sub close_me()
        Dim bCancel As Boolean = False '= = EventArgs.Cancel
        '= Dim intMode As System.Windows.Forms.CloseReason = EventArgs.CloseReason

        '-- confirm cancel--
        If (clsDgvGoodsItems.Rows.Count > 0) And (mDecInvoiceTotal > 0) And _
              ((mbIsPurchaseOrder And mbIsNewPO) Or (Not mbIsPurchaseOrder)) Then
            If MsgBox("Abandon changes ", _
                     MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + +MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                bCancel = False  '--let it go---
                '==Me.Hide()
            Else  '-stay-
                bCancel = True   '--cant close yet--'--was mistake..  keep going..
            End If
        Else  '--not modified
            '==mbCancelled = True
            bCancel = False   '--let it go to close---
            '= Me.Hide()
        End If  '--modified-

        If bCancel Then Exit Sub '--keep alive.-

        '- inform parent.-
        '- Report to Parent..-
        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

        '    If Not bCancel Then  '--exiting.
        '        '- Create an instance of the delegate.  
        '         If Not (Me.delReport Is Nothing) Then
        '            delReport.Invoke(Me.Name, "FormClosed", "")
        '        End If
        '    End If  '-cancel-
        '    Me.Dispose()
    End Sub '--close me-
    '= = = = = = == = = = =


    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    'Private Sub frmGoodsRecvd_FormClosing(ByVal eventSender As System.Object, _
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
    '            '-- confirm cancel--
    '            '-- confirm cancel--
    '            If (clsDgvGoodsItems.Rows.Count > 0) And (mDecInvoiceTotal > 0) And _
    '                  ((mbIsPurchaseOrder And mbIsNewPO) Or (Not mbIsPurchaseOrder)) Then
    '                If MsgBox("Abandon changes ", _
    '                         MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + +MsgBoxStyle.Question) = MsgBoxResult.Yes Then
    '                    intCancel = 0 '--let it go---
    '                    '==Me.Hide()
    '                Else  '-stay-
    '                    intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '                End If
    '            Else  '--not modified
    '                '==mbCancelled = True
    '                intCancel = 0 '--let it go---
    '                '= Me.Hide()
    '            End If  '--modified-
    '        Case Else
    '            intCancel = 0 '--let it go--
    '    End Select '--mode--
    '    eventArgs.Cancel = intCancel
    'End Sub '--closing--
    '= = = = = = =  = = = = = = = == 

    '--Exit--

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Call close_me()
    End Sub  '-exit=
    '= = = = = = = = =  === = 

End Class  '-ucChildGoodsRecvd-
'= = = = = = = = = = = = = = == 

'== end form..==