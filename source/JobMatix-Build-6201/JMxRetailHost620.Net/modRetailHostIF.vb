
Option Strict Off
Option Explicit On
Imports System.Data
Imports System.Data.OleDb
Imports System.windows.forms

Public Module modRetailHostIF

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


    '--18-Jan-2018=  MOVED here from "clsRetailHost" to give it independenace..
    '--18-Jan-2018=  MOVED here from "clsRetailHost" to give it independenace..
    '--18-Jan-2018=  MOVED here from "clsRetailHost" to give it independenace..

    '==  NEWDLL- 4219 VERSION
    '==    Created- 4219.1122 22-Nov-2019= 
    '==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..
    '==

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (16-June-2021)
    '==   Target-New-Build-6201 --  (16-June-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    '-- Interface to standardise interface to Retail Host Database..-
    '---- THIS CLASS (module) for RetailManager ONLY.--

    '--  All Host classes will IMPLEMENT this interface..
    '--  All Host classes will IMPLEMENT this interface..

    Public Interface _clsRetailHost  '== 3101.1111 Now PUBLIC..--
        ReadOnly Property connection() As OleDbConnection   '== ADODB.Connection
        ReadOnly Property colTables() As Collection
        ReadOnly Property DBname() As String
        ReadOnly Property IsSqlServer() As Boolean
        ReadOnly Property CanTrackSerials() As Boolean
        ReadOnly Property SellPriceIncludesGST() As Boolean
        ReadOnly Property StockTableColumnNameCat1() As String
        ReadOnly Property StockTableColumnNameCat2() As String
        ReadOnly Property CustomerSearchColumns() As Object
        ReadOnly Property stockSearchColumns() As Object

        '-- Jet Connect for MYOB Retail manager.-
        Function connect(ByRef sServer As String, _
                          ByRef sDSN As String, _
                           ByRef sUid As String, _
                            ByRef sPwd As String, _
                            ByRef bNewPath As Boolean, _
                            ByRef strSchema As String) As Boolean

        '-- JobmatixPOS- Save Live Sql Connection Info..-
        Sub SetConnection(ByVal sServer As String, _
                             ByRef connection As OleDbConnection, _
                             ByRef colTables As Collection, _
                              ByVal DBname As String)

        Function browseGetPrefColumns(ByVal strTablename As String, _
                                       ByRef strHostTableName As String, _
                                        ByRef colPrefs As Collection) As Boolean

        Function getOriginalColumnName(ByVal strTableName As String, _
                                             ByVal strGridColName As String) As String

        Function browseGetSelectedRecord(ByVal strTablename As String, _
                                          ByRef colSelectedRow As Collection, _
                                            ByRef colFullRecord As Collection) As Boolean
        '= New 3311.228=
        Function staffLookup(ByRef colRecord As Collection) As Boolean
        Function staffGetStaffRecord(ByVal strBarcode As String, _
                                      ByVal lngId As Integer, _
                                        ByRef colStaffInfo As Collection) As Boolean
        '= New 3311.406=
        '-- User Can supply Docket name as key..
        Function staffGetStaffRecordEx(ByVal strBarcode As String, _
                                        ByVal lngId As Integer, _
                                        ByVal strDocketName As String, _
                                         ByRef colStaffInfo As Collection) As Boolean
        '= New 3311.731=
        '-- Get full staff list...
        Function staffGetStaffList(ByRef colRecordList As Collection) As Boolean

        Function customerLookup(ByRef colRecord As Collection) As Boolean
        Function customerGetCustomerList(ByRef colRecordList As Collection) As Boolean

        Function customerGetCustomerRecord(ByVal strBarcode As String, _
                                            ByVal lngId As Integer, _
                                              ByRef colRecord As Collection) As Boolean
        Function customerGetCustomerRecordEx(ByRef colBrowseRow As Collection, _
                                              ByRef colRecord As Collection) As Boolean
        Function customerGetSalesHistory(ByVal strBarcode As String, _
                                           ByVal lngId As Integer, _
                                             ByRef colRecordList As Collection) As Boolean
        Function stockLookup(ByVal bServiceChargeRequested As Boolean, _
                              ByVal sServiceChargeCat1 As String, _
                                ByVal sServiceChargeCat2 As String, _
                                  ByRef colSelectedRecord As Collection) As Boolean
        Function stockGetJobPartStockInfo(ByVal strBarcode As String, _
                                           ByVal lngId As Integer, _
                                             ByRef colRecord As Collection) As Boolean
        Function stockGetStockRecord(ByVal strBarcode As String, _
                                       ByVal lngId As Integer, _
                                         ByRef colRecord As Collection) As Boolean
        Function stockGetStockRecordEx(ByRef colBrowseRow As Collection, _
                                        ByRef colRecord As Collection) As Boolean
        Function stockGetStockList(ByRef colStockIdList As Collection, _
                                     ByRef colRecordList As Collection) As Boolean
        Function stockGetDistinctCategoryList(ByRef colCat1Cat2List As Collection) As Boolean

        Function invoiceGetInvoiceInfo(ByVal strBarcode As String, _
                                        ByVal lngGoodsId As Integer, _
                                         ByRef colInvoiceInfo As Collection) As Boolean
        Function invoiceGetStockItemInvoices(ByVal strBarcode As String, _
                                              ByVal lngStockId As Integer, _
                                               ByRef colRecordList As Collection) As Boolean
        Function supplierLookup(ByRef colRecord As Collection) As Boolean
        Function supplierGetSupplierRecord(ByVal strBarcode As String, _
                                            ByVal lngId As Integer, _
                                              ByRef colRecord As Collection) As Boolean
        Function serialGetAllSerials(ByRef sArg As String, _
                                      ByRef labStatus As Label, _
                                       ByRef bCancelFlag As Boolean, _
                                          ByRef colSerialsList As Collection, _
                                           ByRef strErrorReport As String) As Boolean
        '--= 3101.1024= Stock_id is optional (Send -1 if not being supplied)..
        Function serialGetSerialInfo(ByVal intStock_id As Integer, _
                                       ByVal sSerialNo As String, _
                                         ByRef colSerialInfo As Collection) As Boolean
        Function quoteGetAllQuotes(ByRef colQuotesList As Collection, _
                           Optional ByVal mlCustomerSearchId As Integer = -1) As Boolean

        Function quoteGetStocklist(ByRef order_id As Integer, _
                                    ByRef colStockList As Collection, _
                                     ByRef colExpandInstances As Collection) As Boolean
        Function closeConnection() As Boolean
    End Interface


End Module '-modRetailHostIF-
'= = = = = = = = = = = = = =
