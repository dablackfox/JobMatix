
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'= Imports VB6 = Microsoft.VisualBasic.Compatibility
'== Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application


Public Class clsKeyGen42

    '-- DLL JMxKeyGen420_OS 
    '==   This is the FREE licence compute routine for the Open Source JobMatix..
    '-- DLL JMxKeyGen420_OS 
    '==   This is the FREE licence compute routine for the Open Source JobMatix..
    '==   This is the FREE licence compute routine for the Open Source JobMatix..
    '==   This is the FREE licence compute routine for the Open Source JobMatix..
    '==   This is the FREE licence compute routine for the Open Source JobMatix..

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

    '== Target-New-Build-6201 --  (18-July-2021) for Open Source..
    '==  No need for popup re: Licence Key..

    Private mbLicenceRequired As Boolean = False   '== Open Source Licence not needed.

    '-- Original DB name before upgrading (migtating) to POS.
    Private msOriginalJobMatixDBName As String = ""

    '-- wait form--
    Private mFormWait1 As frmWait

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String,
                      Optional ByVal sHeader As String = "JobMatix62 Exe")

        mFormWait1 = New frmWait
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.labHdr.Text = sHeader
        mFormWait1.waitTitle = "JobMatix EndUser Licence.."
        mFormWait1.Show()
        '= DoEvents()
    End Sub '- mWaitFormOn-
    '-= = = = =  = = = = = =

    '-- kill (hide) wait form--
    Private Sub mWaitFormOff()

        mFormWait1.Hide()
        mFormWait1.Close()
        mFormWait1.Dispose()
        '= DoEvents()
    End Sub  '--wait--
    '= = = = = = = = = = = = = = = = =

    '-- Original DB name before upgrading (migrating) to POS.

    Public WriteOnly Property OriginalJobMatixDBName() As String
        Set(ByVal value As String)
            msOriginalJobMatixDBName = value
        End Set
    End Property
    '= = = = = = = = 

    '-LicenceRequired=

    Public ReadOnly Property LicenceRequired As Boolean
        Get
            LicenceRequired = mbLicenceRequired
        End Get
    End Property  '==LicenceRequired=
    '= = = = = = = = == = =  =


    '-- C O M P U T E   L I C E N C E ----
    '-- C O M P U T E   L I C E N C E ----

    '-- Dummy for JobMatix42 licencing on OPEN SOURCE JobMatixPOS etc project...
    '-- Dummy for JobMatix42 licencing on OPEN SOURCE JobMatixPOS etc project...
    '-- Dummy for JobMatix42 licencing on OPEN SOURCE JobMatixPOS etc project...

    Public Function ComputePosKey(ByVal sBusinessABN As String,
                                    ByVal sBusinessPostCode As String,
                                    ByVal sBusinessShortName As String,
                                    ByVal sSqlDbName As String,
                                    ByVal bIsPosSystem As Boolean,
                                      ByRef sComputedKeyUnlimited As String,
                                        ByRef sComputedKeyLevel2 As String,
                                         ByRef sComputedKeyThreeUser As String,
                                          ByRef sComputedKeyTwoUser As String,
                                             ByRef sComputedKeySingleUser As String) As Boolean
        sComputedKeyUnlimited = ""
        sComputedKeyLevel2 = ""
        sComputedKeyThreeUser = ""
        '-- etc..
        sComputedKeyTwoUser = ""
        sComputedKeySingleUser = ""

        'MsgBox("Please Note-" & vbCrLf &
        '           "This is where JobMatix checks if End User has a valid Licence. " &
        '           "No End User Licence system is defined for the Open Source JobMatix." &
        '           "  Developers can add code here to implement a Licence key system.", MsgBoxStyle.Information)


        '== Target-New-Build-6201 --  (18-July-2021) for Open Source..
        '==  No need for popup re: Licence Key..

        'Call mWaitFormOn("Please Note-" & vbCrLf &
        '           "This is where JobMatix checks for any End User Licence. " & vbCrLf &
        '           "  Developers can add code here to implement an EUL Licence key.", "JobMatix Keygen..")


        'DoEvents()
        'Dim lngStart As Integer = CInt(VB.Timer()) '--starting seconds.-
        'While (CInt(VB.Timer()) <= lngStart + 3)
        '    System.Windows.Forms.Application.DoEvents()
        'End While
        'Call mWaitFormOff()

        ComputePosKey = True

    End Function  '==ComputePosKey-
    '= = = = == = = = 

    '-- CheckLicenceKey --
    '-- CheckLicenceKey --

    '-- Dummy for JobMatix42 licencing on OPEN SOURCE JobMatixPOS etc project...
    '-- Dummy for JobMatix42 licencing on OPEN SOURCE JobMatixPOS etc project...

    Public Function CheckLicenceKey(ByVal sTestKey As String,
                                       ByVal sComputedKeyUnlimited As String,
                                              ByVal sComputedKeyLevel2 As String,
                                            ByVal sComputedKeyThreeUser As String,
                                               ByVal sComputedKeyTwoUser As String,
                                             ByVal sComputedKeySingleUser As String,
                                                  ByRef bIsLevel2Licence As Boolean,
                                                 ByRef intMaxUsersLicenced As Integer) As Boolean
        Dim intNoUsers As Integer
        Dim bLicenceOk As Boolean = False

        CheckLicenceKey = False
        bIsLevel2Licence = False
        '== intNoUsers = K_ABSOLUTE_MAX_USERS_PERMITTED
        intNoUsers = -1  '== No limit- 

        'Select Case UCase(sTestKey)
        '    '= Case sComputedPreciseShortKey
        '    '=     bLicenceOk = True
        '    Case sComputedKeyUnlimited
        bLicenceOk = True
        '    Case sComputedKeyLevel2
        '        bLicenceOk = True
        '        bIsLevel2Licence = True
        '    Case sComputedKeyThreeUser
        '        bLicenceOk = True
        '        intNoUsers = 3
        '    Case sComputedKeyTwoUser
        '        bLicenceOk = True
        '        intNoUsers = 2
        '    Case sComputedKeySingleUser
        '        bLicenceOk = True
        '        intNoUsers = 1
        '    Case Else
        '        intNoUsers = 0
        'End Select
        CheckLicenceKey = bLicenceOk
        intMaxUsersLicenced = intNoUsers

    End Function  '--check key-
    '= = = = = = = = =  = =


End Class  '-clsKeyGen42
'= = = = = = = =  ==  =
