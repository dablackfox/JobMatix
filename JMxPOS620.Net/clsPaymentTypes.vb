
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'= Imports System.Reflection
'=Imports System.IO
'=Imports System.Threading
'=Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Windows.Forms.Application

Public Class clsPaymentTypes

    '-- Encapsulates and returns List od Payments Types..
    '-- Also will handle Local prefs for re-ordering Types List..

    '==
    '== grh 02-July-2019..
    '==
    '= = = = = = = = = = = = = =

    '-- re-order the list from local settings key order if any..

    Const k_localPayTypesListKey As String = "POS_LocalPayTypesList"

    Private msSettingsPath As String = ""
    Private mLocalSettings1 As clsLocalSettings

    '-- check local settings (prefs) for paylist order...
    '=     msSettingsPath = gsLocalSettingsPath() '= default Jobmatix33=
    '=3300.428= 
    '=     mLocalSettings1 = New clsLocalSettings(msSettingsPath)

    Public Sub New(ByVal sSettingsPath As String)

        MyBase.New()

        msSettingsPath = sSettingsPath
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

    End Sub '-new-
    '= = = = = = = = = == =  == =  = == =
    '= = = = = = = = = = == = = = = =  =
    '-===FF->

    '- Get standard Type List..

    Private Function mColGetSystemPaymentTypes() As Collection
        Dim colResult As Collection
        Dim colItem As Collection
        Dim ix As Integer

        colResult = New Collection
        '== payment type array has pairs of (Key, Description)
        Dim strPaymentTypes() As String = _
                   {"Cash", "Cash In", _
                     "EftPos_Dr", "EftPos Dr (Chq/Saving)", _
                     "EftPos_Cr", "EftPos Cr (Credit)", _
                      "ZipPay", "Zip Pay", _
                      "Bank-Dep", "Bank Deposit", _
                      "Cheque", "Cheque (Customer)", _
                        "Amex", "Amex Chg Card", _
                        "Diners", "Diners Chg Card", _
                         "Other_Chg", "Other Chg Card"}

        '==  make array into collection of collections..
        ix = 0
        While ix <= UBound(strPaymentTypes)
            colItem = New Collection
            colItem.Add(strPaymentTypes(ix), "key")
            colItem.Add(strPaymentTypes(ix + 1), "description")
            colResult.Add(colItem)
            ix += 2
        End While
        mColGetSystemPaymentTypes = colResult

    End Function  '-mColGetSystemPaymentTypes-
    '= = = = = = = == = = = = = = ==  == == 
    '= = = = = = = = = = == = = = = =  =
    '-===FF->

    '-- Change local Prefs for Order of Payment types.

    Public Function UpdatePaymentPrefs() As Boolean

        Dim s1, sKey As String
        Dim sSystemPaylist As String
        Dim sLocalPaylist As String
        Dim rx As Integer = 0
        '=Dim frmPrefOrder1 As frmPrefPayTypes

        '-- get standard payment types..
        Dim col1 As Collection
        Dim colSystemPaymentTypesDefined As Collection = mColGetSystemPaymentTypes() '= gColPaymentTypes()  '--3101.1206= Get collection of types.
        Dim colSystemPaymentTypesDefinedWithKeys As Collection  '-original with keys.
        '= Dim colSystemPaymentTypesResult As Collection '--returns to caller for Payments DGV..

        Dim colSystemPayKeyList As Collection
        Dim colPrefPayKeyList As Collection
        Dim colResultPayKeyList As Collection

        colSystemPaymentTypesDefinedWithKeys = New Collection  '-for direct access to original-
        colSystemPayKeyList = New Collection

        For Each col1 In colSystemPaymentTypesDefined
            'row1 = New DataGridViewRow
            'mDgvSalePaymentDetails.Rows.Add(row1)
            'With mDgvSalePaymentDetails.Rows(rx)
            '    .Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value = col1("description")
            '    .Cells(k_PAYGRIDCOL_AMOUNT).Value = "0.00"
            '    .Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value = col1("key")
            'End With
            sKey = col1("key")
            colSystemPayKeyList.Add(col1("key"))
            colSystemPaymentTypesDefinedWithKeys.Add(col1, sKey)
            rx += 1
        Next col1
        DoEvents()
        colPrefPayKeyList = colSystemPayKeyList

        '-refresh to get latest.
        mLocalSettings1.refreshAll()

        '-prefs-
        If mLocalSettings1.exists(k_localPayTypesListKey) AndAlso _
                 (mLocalSettings1.item(k_localPayTypesListKey) <> "") Then
            '-- get list and make into collection..
            '--  ie colPrefPayKeyList-
            s1 = mLocalSettings1.item(k_localPayTypesListKey)
            Dim asTypes() As String = Split(s1, ";")
            colPrefPayKeyList = New Collection
            For Each sType As String In asTypes
                If Trim(sType) <> "" Then
                    colPrefPayKeyList.Add(Trim(sType))
                End If
            Next sType
        Else  '-no local list-
            '-- ask user to make one.
            colPrefPayKeyList = colSystemPayKeyList
        End If  '--settings-

        '- now show to user for re-ordering.

        Dim frmPrefOrder1 As frmPrefPayTypes = New frmPrefPayTypes(colPrefPayKeyList)

        frmPrefOrder1.ShowDialog()
        If Not frmPrefOrder1.cancelled Then
            '-new order.
            '-- save new key order as string in local settings.
            Dim sNewPrefs As String = ""
            If (Not (frmPrefOrder1.newPayList Is Nothing)) AndAlso (frmPrefOrder1.newPayList.Count > 0) Then
                For Each sType As String In frmPrefOrder1.newPayList
                    sNewPrefs &= sType & ";"
                Next sType
                '= MsgBox("New list is:" & vbCrLf & sNewPrefs, MsgBoxStyle.Information)

                '-- save new list in settings..
                If Not mLocalSettings1.SaveSetting(k_localPayTypesListKey, sNewPrefs) Then
                    '= gbSaveLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, msReceiptPrinterName) Then
                    MsgBox("Failed to save Payment Prefs. setting.", MsgBoxStyle.Exclamation)
                Else
                    '-ok-
                    MsgBox("Payment Prefs. setting saved ok..", MsgBoxStyle.Information)
                End If
            End If  '-nothing..
        Else
            '--cancelled..  nothing to do ?
        End If  '-cancelled.

    End Function  '-UpdatePaymentPrefs-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Get Full collection of PaymentTypes fpr Sales/payments..
    '-- re-order full collection if user has Prefs..

    Public Function getColPaymentTypes() As Collection

        '-- get standard payment types..
        Dim s1, sKey As String
        Dim col1 As Collection
        '= gColPaymentTypes()  '--3101.1206= Get collection of types.
        Dim colSystemPaymentTypesDefinedWithKeys As Collection  '-original with keys.
        Dim colSystemPaymentTypesResult As Collection '--returns to caller for Payments DGV..
        Dim colPrefPayKeyList As Collection
        Dim colResultPayKeyList As Collection

        Dim colSystemPaymentTypesDefined As Collection = mColGetSystemPaymentTypes()

        '-Get prefs-
        If mLocalSettings1.exists(k_localPayTypesListKey) AndAlso _
                 (mLocalSettings1.item(k_localPayTypesListKey) <> "") Then
            '-- get user's pref. list and make into collection..
            '--  ie colPrefPayKeyList-
            s1 = mLocalSettings1.item(k_localPayTypesListKey)
            Dim asTypes() As String = Split(s1, ";")
            colPrefPayKeyList = New Collection
            For Each sType As String In asTypes
                If (Trim(sType) <> "") Then
                    colPrefPayKeyList.Add(Trim(sType))
                End If
            Next sType
            '-- make new Full collection in order of user prefs..
            colSystemPaymentTypesDefinedWithKeys = New Collection  '-for direct access to original-
            '-- make a keyed original collectio.
            For Each col1 In colSystemPaymentTypesDefined
                sKey = col1("key")
                '=colSystemPayKeyList.Add(col1("key"))
                colSystemPaymentTypesDefinedWithKeys.Add(col1, sKey)
            Next col1
            DoEvents()

            colSystemPaymentTypesResult = New Collection
            '- if collection size doesn't match the Prefs, just reurn the original system list.
            If (colPrefPayKeyList.Count <> colSystemPaymentTypesDefinedWithKeys.Count) Then
                MsgBox("Sorry, Can't match preferences list to original Payment Types." & vbCrLf & _
                           "Using the original standard collection..", MsgBoxStyle.Exclamation)
                '-- Just send back the standard collection.
                getColPaymentTypes = colSystemPaymentTypesDefined

            Else '- ok-
                '- pick out Types in Prefs order.
                For Each sPrefsKey As String In colPrefPayKeyList
                    If colSystemPaymentTypesDefinedWithKeys.Contains(sPrefsKey) Then
                        col1 = colSystemPaymentTypesDefinedWithKeys.Item(sPrefsKey)
                        colSystemPaymentTypesResult.Add(col1)
                    End If  '-contains.
                Next sPrefsKey
                '- check that we got them all.
                If colSystemPaymentTypesResult.Count <> colSystemPaymentTypesDefined.Count Then
                    MsgBox("Got Uneven preferences list of Payment Types." & vbCrLf & _
                           "Using the original standard collection..", MsgBoxStyle.Exclamation)
                    '-- Just send back the standard collection.
                    getColPaymentTypes = colSystemPaymentTypesDefined
                Else '-ok. send back re-ordered collection.
                    getColPaymentTypes = colSystemPaymentTypesResult
                End If '-count-
            End If '-match count.
        Else  '-no local list-
            '-- Just send back the standard collection.
            getColPaymentTypes = colSystemPaymentTypesDefined
        End If  '--settings-

    End Function '-getColPaymentTypes-
    '= = = = =  = = = = = = = = = = = =


End Class  '-clsPaymentTypes-
'= = = = = = = = = = = = = = = = = = =
