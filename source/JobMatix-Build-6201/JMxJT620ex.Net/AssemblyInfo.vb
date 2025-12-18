Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("JMxJT620ex")>
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("")>
<Assembly: AssemblyProduct("JmxJT420ex")>
<Assembly: AssemblyCopyright("Copyright © 2014..2019, 2020 grhaas@outlook.com")>
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("69a9580d-da61-4164-9e41-08560d92c7f6")>

'==
'== Updated 3411.0103  03Jan2018=  for Email Agent etc..
'==
'== Updated 3411.0108  08Jan2018=  Project Target CPU changed to x86...
'==
'== Updated 3411.0109  09Jan2018=  Fix DataDir logging path...
'==
'== -- Updated 3501.0615  09Jan2018=  JobTracking HOST program for JobTracking dll...
'==           - Use Combined File/Sql subs module...
'==      3501.0625= --  Fixes to modAllFileAndSqlSubs to Get correct appname for LocalDataDir..
'= = = = = = = = ==  =
'==
'== -- Updated 3501.0724  24July-2018= ...
'==           3501.0724= --  NewInJobMatix now loaded from Local Dir at runtime...
'==
'==
'==    3501.0806 06-August-2018= 
'==       -- Fixes to modAllFileAndSqlSubs for Speeding up getSchema at startup.-
'==
'==
'==    3501.0812 12-August-2018= 
'==       -- Updated revision no...
'==
'==
'==    3501.0814 14-August-2018= 
'==       -- Updated revision no...
'==
'==
'==    3501.1105 05/07-Nov-2018= 
'==       -- Updated revision no...
'==
'==    3501.1223 23-Dec-2018= 
'==       -- Updated revision no...
'==
'==    3519.0119 08/19-Jan-2019= 
'==       -- NEW Build...
'==                 (Fixes to Discovering users.)
'==
'==    3519.0129 29-Jan-2019= 
'==       -- Updated Build...
'==                 (Fixes to sAppPath Discovering Biz logo.)
'==
'==    3519.0414 14-Apr-2019= 
'==       -- Updated Build...
'==            (Fixes to Allow Direct Delivery even if  POS active.)
'==
'== -- Updated 4201.0627  27-June-2019= ...
'==         For New version 4.2 of POS--...
'==
'==
'==    Updated- 4201.0717 16-July-2019= 
'==       >> Job Search DataGridView.. ALSO Customer SearchGrid/Jobs Listview-  
'==            Add Right-click context menu for Job Actions, as per Active Jobs Tree treeview.
'==
'==
'==    Updated- 4201.1007-  Started 06-October-2019= 
'==     --  Fix to Amend start-up to Fix problem (AGAIN) about
'==              Extra 'Slashes' may appear in User Passwords in Job Record.
'==
'==  NEW BUILD- 4219 VERSION
'==    Updated- 4219.1130 21-Nov-2019= 
'==       >> clsPrintDocs- JobMaint Printing-  Fix Printing WorkHistory for Multiple Pages.
'==      --  MAKE Forms PUBLIC- NewJobForm and Maint Form-  "frmNewJob32" and "frmJobMaint32"
'==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..
'==      --  Update RAs reference to call "JobMatixRAs42.exe"..
'==
'==      -- More stuff....
'==
'==  NEW REVISION  4219.1216 VERSION started 08-Dec-2019=
'==    Updated- 4219.1208 08-Dec-2019= 
'==      --  MAKE Form "frmNewJob32" INTO USERCONTROL.
'==      --  MAKE NEW Form "frmNewJobBase" INTO USERCONTROL.
'--            SO THAT we show it as a Child in a POS TAB..
'==        From JobTracking we call frmNewJobBase, which is container for the UserControl.
'==
'==      --  Add a new 5-min. Timer to Main Form to check for Exchange-Calendar xml files that might have come from POS...
'==               -- If there is any, and BG-Worker exchange is not running, the Run It...
'==
'==  NEW Build 4221.0207  '- 07Feb2020.
'==        -- To Show Customer Tags on Main Form..
'==
'==
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'== Fixes to Build 4221.0207  
'==
'==   Target-New-Build 4262 -- (Started 12-Aug-2020)
'==   Target-New-Build 4262 -- (Started 12-Aug-2020)
'==
'==   Target-New-Build-4262 -- (Started 12-Aug-2020)
'==   Target-New-Build-4262 -- (Started 12-Aug-2020)
'==
'==      --  MAKE Form "frmJobMaint32" INTO USERCONTROL- ucChildJobMaint..
'==      --  MAKE NEW Form "frmJobMaintBase" to hold USERCONTROL.
'--            SO THAT we show it as a Child in a POS TAB..
'==              From JobTracking we call frmJobMaintBase, which is container for the UserControl.
'==      --  In Main Form- BG Worker Exchange201-.   see modOnsiteCalendar.vb   
'==             ie We need to put in more try/catch code into the Function mbFindJobAppointment  
'==                to catch Karens's string crash (Martin email 12-Aug-2020/)
'==
'==   --  In Main Form, cmdStopPress_click need to be updated..   
'==       1. The UPDATE statement clause
'==            sSql = sSql & " ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',4000) "
'==            Needs to be changed to this: (because ServiceNotes is now varchar(max).)
'==                  sSql = sSql & " ServiceNotes=(ServiceNotes+'" & sNewNote & "') "
'==       2. The procedure should check for external updates AFTER getting msg text from 	UI,
'==                	and inside a transaction..
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = == = = = = = = 
'==
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
'==
'==
'== Target-build-4267  (Started 07-Sep-2020)
'== Target-build-4267  (Started 07-Sep-2020)
'== Target-build-4267  (Started 07-Sep-2020)
'==
'==  --Main- Exchange201-  Completion of BG-worker..  
'==      NEW module "modMyMsgBox" with Function to create a Form on the fly with textbox to show accumulated Exchange results 
'==           (.Net MessageBox is not suitable for exended message..)   
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==
'== Target-Build-4284  (Re-Started 23-Nov-2020)
'== Target-Build-4284  (Re-Started 23-Nov-2020)
'== Target-Build-4284  (Re-Started 23-Nov-2020)
'==
'==   A. -- Bring JobReports into JobTracking Main as a UserControl..
'==
'==   B. -- Cancel Job (frmNewJob etc)..  Update Calendar to delete the Appointment..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==   Target-New-Build-6201 --  (16-June-2021)
'==   Target-New-Build-6201 --  (16-June-2021)
'==
'==  For JobMatix62Main- OPEN SOURCE version...
'==  For JobMatix62Main- OPEN SOURCE version...
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("6.2.6201.0718")>
'= <Assembly: AssemblyFileVersion("1.0.0.0")> 
'= = = = = = = = = = = == = = = = == = = = = = = = = = = = == = = = 
