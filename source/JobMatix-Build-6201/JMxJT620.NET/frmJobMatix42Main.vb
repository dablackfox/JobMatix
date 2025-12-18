Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Reflection
Imports System.IO
Imports System.Threading
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.ComponentModel
Imports System.Data.OleDb
'== 3411.1221= Imports clsJMxPOS31
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports System.Xml
Imports System.Text

Public Class frmJobMatix42Main
	Inherits System.Windows.Forms.Form
    '= = = = = = =
    '===== JobTracking ==== main form ==
    '- - -
    '======= grh == ===

    '== grh 18-June-2021-  This is the MAIN App form for the Open Source JobMatix JobTracking...
    '== grh 18-June-2021-  This is the MAIN App form for the Open Source JobMatix JobTracking...

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


    '==  02-May-2010==  Changes for Windows Authentication.---==
    '==  03-May-2010==  Removed ComputeKey menu..---==
    '==  05-May-2010==  TAB Control for Jobs/RAs Browsers---==
    '==  14-May-2010==  Fix to RAs Browser- (selecting wrong record.)--==
    '==  14-May-2010==  Jobs Browser- Show Job and RA Details.--==
    '==  21-May-2010==  RAs Browser- RE-arrange columns order...--==
    '====   -------       Also added CMD Button for New Booking..--
    '==  27-May-2010==  Updates to SecurityId for V2 Licence checking...--=
    '==  29-May-2010==   Browser gridlines downgraded.--=
    '==  15-Jun-2010==   Rev-2449.- fix colours and Parts menu.- Add RM SerailAudit..=
    '==  30-Jun-2010==   Rev-2452.- No more Sub Main..  THIS is now the startup form...=
    '==  15-Jul-2010==   Rev-2455.- Changed Licence Key to MD5..--
    '==  27-Jul-2010==   Rev-2457.- Do not destroy Browser objects (Keep using same object.)..--
    '-----         ------- Timer Routine Refreshes all Tab-Browsers every minute..-
    '----          ------- Text Search for RA's..---
    '==  21-Aug-2010==   Rev-2463.- "Refresh" browse after update/view.. (Do not call toolBar-click.)..--
    '==  29-Aug-2010==   Rev-2465.- For Jobs Browser- Job_id must be 1st Column for deliverable, viewAll..--
    '==  31-Aug-2010==   Rev-2467.- Barcode Font for all Maint calls..--
    '==  05-Sep-2010==   Rev-2469.- QuoteJobs amendments/reprint.. etc..--
    '==  07-Sep-2010==   Rev-2470.- Show QuoteNo and Jobs in browser Job preview..--
    '= ==== = === = === == ==

    '-- "J o b M a t i x 2" ---
    '-- "J o b M a t i x 2" ---
    '==  12-Sep-2010==   Rev-2771.- Now "JobMatix2" with Jobs TreeView....--
    '==  16-Sep-2010==   (EX JobTracking2) -Rev-2772.- Extract Actual Server-Name from "SQL-Server\InstanceName" ex Login...--
    '==  19-Sep-2010==   (EX JobTracking2) -Rev-2777.- SystemInfo Edit-  maxlength now 150.....--
    '------        -------  ALSO-  Fix backup- detect systemInfo paths properly for Client-side op..
    '==  25-Nov-2010==   -Rev-2788.- New PRINTER for Job Labels.......--
    '==  29-Nov-2010==   -Rev-2788.- Notify from any status.--
    '==  01-Dec-2010==   -Rev-2788.- Can Update GoodsIncare from main screen...--
    '==  15-Dec-2010==   -Rev-2788.- Can Update PRIORITY from main screen...--
    '==  16-Dec-2010==   -Rev-2790.- RA TreeView..  Sort by RA_Id, add DateUpdated....--
    '==  25-Dec-2010==   -Rev-2791.- Final fixes...--
    '==  07-Jan-2011==   -Rev-2792.- Clear search text on sign-on...--
    '==  12-Jan-2011==   -Rev-2796.- Use full day-of-week..--
    '----    ----     ----  PLUS Apply Alert ICON for UnProcessed StopPress..
    '----    ----     ----  PLUS MyJobs determined by "NominatedTech" ONLY..
    '==  18-Jan-2011==   -Rev-2798.- Fixes Explorer crash for Delivered+REturned jobs (Old icon).--
    '----  AND Toolbar button not set..--
    '==  19-Jan-2011==   -Rev-2799.- MyJob Checkbox must do refresh..--
    '==  30-Jan-2011==   -Rev-2802.- StoPress Error.. Don't refresh Grid if not visible.--
    '==  30-Mar-2011==
    '==  30-Mar-2011==   -Rev-2804.- Option buttons for Explorer Jobs Order..--
    '==  31-Mar-2011==   -Rev-2804.- INITIALISE Jobs Tree after Signon, Refresh, Amend and Updates...--
    '==  04-Apr-2011==   -Rev-2806.- Fix crash for blank Labour Rates...--

    '== V2.2.2900/2911 ====
    '---- From: 28-Apr-2011==  V2.2.2900 -- Add tables for Job Service Checklists..
    '----        == Also extra table for Future extra Job Info.. --
    '----    AND Reports as separate EXE..
    '----    AND  DB Backup file name shows -V22-
    '----    AND  RE-vamped Main JobDetails display using RICH-TEXT box..-
    '----    AND ToolTips window subclass for MULTILINE tooltips..-
    '-----   AND minor fixes to RA's (strip barcode lead-zeroes, Transfer into Stock.)
    '-----   AND Extra Status (40-QualityAssurance) prior to Completion..
    '-----   AND  NewPart Form -- Catch MYOB "allow_renaming" field on looked up part...-
    '----    ---  IF true, then allow user to change description/price..--
    '---------------   ALSO, NewPart now shows AND returns sell price incl GST..-
    '-----   AND  =22Jun2011= ALERTs now NOT flagged for COMPLETED Jobs..
    '= = = = = =  == = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== V2.2.2912/2916 etc..  ====
    '---- From: 21-Jul-2011==  V2.2.2912 -- Add Startup and SqlSubs fixes for SQL-Server2008..(inc 64-bit.).
    '----        AND  -- Disable all JobDetail Status bar option buttons..
    '----        AND  -- New SystemInfo entries for DocketFooters and ServiceNotificationCostLimit..
    '-----   AND Rev-2918 -- 06Aug2011==  DROP Status bar & status buttons..
    '-----   AND         -- 06Aug2011==  JobMatix2Terms.txt pre-defined CONSTANT
    '-------           from gsGetTermsAndConditions at design time..
    '-----              FOR loading into systemInfo "TermsAndConditions" if not there yet..
    '----    AND  Fix crash when NO JOBS for MyJobs..
    '= = = = = =  == = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== V2.2.2920/2921...  ====
    '--- 19-Aug-2011==  SystemInfo setup/update has it's own form..
    '----     AND load system info setting split off into a function so we can call it whenever..


    '== V3.0.3010...  ====
    '--- 05-Oct-2011== NEW VERSION 3 for Multiple Retail Hosts..-..
    '--- 07-Oct-2011== ssTabMain replaces by frames.
    '== 19-Nov-2011==  FOR vb.net..  no more GOSUB's..--
    '= = = = = =  == = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== V3.0.3023...  ====
    '== 02-Dec-2011==  FOR vb.net.. SeparateEXE for RA's...--
    '== 06-Dec-2011==  Version UPGRADED to vb.net...--
    '== 17-Dec-2011==  Fixes for QB-POS...--

    '== 21-Dec-2011==  Job_MainDetails.RTF as embedded resource.....--
    '---- Customer Id now needed for BuildQuotes...
    '== 09-Jan-2012==  Toolstrip fpr Job Actions.......--

    '== V3.0.3031...  ====
    '== 29-Feb-2012==  MSHFlexGrid-Jobs and --.Cust REPLACED with DataGridView class.--
    '== 03-Mar-2012==     clsStrDictionary  REPLACES  Scripting.Dictionary..- 
    '=                 AND..  add "DaysInCustody"  to Jobs tree..--
    '== 06-Mar-2012==  Fixing flicker using tranparency key (ON MAIN  Screen only.).. 
    '==                  see   http://bytes.com/topic/c-sharp/answers/720155-combating-flicker
    '==
    '== V3.0.3031.1/2..  ====
    '=   JobMaint33 re-worked for smaller footprint..
    '==
    '==  AND "Notify" NO LONGER calls JobMaint..--
    '==   and  send Top/left values to Jobmaint form..
    '==    and   -  Jobs Tree.-- Order by COMBO..-
    '==  =15Mar2012= Full Text Search for customers.-
    '==   AND 19-Mar-2012-== updates to RTF details..
    '==    and DROP "Close" button..  
    '==
    '== V3.0.3031.3..  ====
    '==   Fix Licence test..  Precise V1 short key made OK for Level-1 licence..
    '==
    '== V3.0.3031.7..  ====
    '==   Licence test..  
    '==     STOP showing Licence Source etc on Debug...
    '==
    '== V3.0.3031.8..  ====
    '==   DROP Shape Connection (not used..)
    '==     Fix "Today" problems..
    '== V3.0.3031.11..  ====
    '===  Fix RA's Transaction hangup..
    '==
    '== NEW BUILD-  V3.0.3037.0..  ====
    '===  Bring QUOTES listView to Main Screen.
    '==    Select Quote before building..
    '=== ALSO..  add try/catch to PRINTER commands.. (clsPrintDocs.)
    '==
    '=== V3.0.3037.1.
    '==     Variable font sizes for Tree/Grids..
    '== 
    '=== V3.0.3041.1.
    '==     Fixes to clsBrowse-refresh to maintain position..
    '==     DB-name to show at bottom of form..
    '==    AND new SystemInfo  "SERVICECHARGESINFOTEXT" --
    '== 
    '=== V3.0.3043.0.
    '==     Fixes to launch frmJobMaint a  bit higher up..
    '==    and-  reduce me.height to 707px (to fit in 1024x768 and above TASK bar).. .
    '==     Fix Setup Form to add "SERVICECHARGESINFOTEXT" text box..
    '--    and PASS resfreshed customer details to frmJobMaint..-- 
    '--    AND '== 3043=  AFTER Job Amend/update, refresh detail info.--
    '==
    '==3043.2== 21Apr2012=
    '--     PrtSelect now shows all THREE Printer functions..
    '==  
    '==3043.3== 22Apr2012=
    '--   Re-position bottom business/info labels for Win-7..
    '==
    '==3047.1/ 3049.0 == 28Apr2012=
    '--  Jobs Treeview..  catch selection by arrows..
    '--  New Part Form:   Tidy up ENTER key processing..
    '--  Setup Form:   Tidy up ENTER key processing..
    '==   AND  chooseFont sizes to apply to Job-Detail-RTF...
    '==   AND Update Job details panel after StopPress.--
    '==
    '==3049.0 == 01May2012=
    '==  Fix repainting of sign-on areas after timeout while in BG..
    '==
    '==3049.3 == 03May2012=
    '==  Fix Exit options in JobUpdate..
    '==
    '==3053.0 == 16May2012=
    '==  Fixes to Job Label printing.
    '--  clsBrowse-  Load grid even if no R/set records..
    '--  Fix to make Label printer selection stick..
    '--  Tidy up New Job..
    '==
    '==3053.1 == 21May2012=
    '==  Fix to NewJob Amend: Enable FrameGoods   !!  --
    '==
    '== 3057.0  ==23May2012=
    '==  Fix timer loop for Customer Grid to stop repeating jon re-paint..
    '==
    '== 3057.1  ==29May2012=
    '==   NewJob Goods-  Show prev jobs-goods if any..=..
    '==   AND- Show startup info message..
    '==
    '== grh - =02-Jun-2012= Build 3059.1== 
    '==         For RM. CustomerHistory:  Retrieve job list via "Customer_id" as PREFERENCE..-
    '==         AND- Fix gsFormat for null RM Docket-lines crash..=
    '==
    '==  grh -=V:3.0.3061.0= 09-Jun-2012= 
    '==      -- Fixes index crash in Parts Search form...=" 
    '==      -- Revamps Service Checklist ModelEdit form-
    '==                      to show model checklists while browsing ServiceStock grid....
    '==      -- Fixes frmJobMaint to NOT ignore first service item for JobUpdate checklist re-write..
    '==      -- Jobmatix Main Customer Grid..  make first JobsListview item the SELECTED item.
    '--              and access this item for cmdNotify, cmdStopPress.
    '==
    '==  grh -=V:3.0.3061.1= 13-Jun-2012= 
    '==      -- Fixes "Delete-Lastline" crash in Service Model Edit form...=" 
    '==    and:  Add Job-Count to QA node of Jobs-Treeview..
    '==
    '==  grh =V:3.0.3063.0= Built:01Jul2012= 
    '==   >> Fixes index crash in modMD5 (B-zero byte in result)..=
    '==   >> frmJobMaint-  Shows corrects check/unchecked icons in listViewQuotes.
    '==
    '== grh-=V:3.0.3067.0= Built:16Jul2012=
    '==    >> Add help File..  
    '==    >> Revamp NewJob Symptoms..=
    '==    >> Set HelpFileName as global function..
    '==    >> ISSUE Starting-up maximised causes Activated event to accur BEFORE Load event...
    '==         AS well as other issues.  
    '==        USER should NOT start up Jobmatix in the Maximised state-
    '==               (eg by setting Shortcut properties.)
    '==    >> Preference setting.. Start-up maximised..
    '==    >> For Selected Quote, count chassis's, Show no. Jobs, and warn if no chassis.. 
    '==    >> Fix:  42 day trial must have full functions..--
    '==
    '== grh-=V:3.0.3069.0/3071.0= Built:11Oct2012=
    '==    >> Add Three-User Licence..  
    '==            and show logged-in users..
    '==    >> Add New User:  Add permissions for VIEW SERVER STATE --
    '==
    '== grh-=V:3.0.3072/3073.0= Built: 13-Feb-2013.=
    '==    >> Add Two-User Licence..  AND Single-User Licence.
    '==    >>  No-Licence after Eval-period- Reverts to Single-user with all functions-
    '==              (Except still nags and overprints EVAL stuff...)
    '==    >>  New-Job form make-over with separate form for GoodsInCare..
    '==    >>  GetRecordset ERRORS-  Attempt to re-connect to SQL Server..
    '==
    '== grh-=V:3.0.3073.309= Built: 09-Mar-2013.=
    '==    >> Fix RESTORE to check if Database exists..  More Logging..
    '==    >>         ALSO-  RESTORE-  Add current user to SQL Logins and DB Access.
    '==    >> Shell to RA's..  ADD cmdline parm:  "/Licenced=YES/NO"  
    '==    >> 3073.311= 11-Mar2013=  Admin/AddUser- 
    '==        now calls 'gbCheckUserAccess' --
    '==
    '== grh-=V:3.0.3077.519/521= Built: 19/21-May-2013.=
    '==    >> Fix "gbGrantVWSSPermission" to retry "USE Master" until it succeeds..
    '==    >> Fix AddNewUser to re-issue "USE msSqlDBName" at end to restore JM as current DB..
    '==    >> Fix Main-Startup to upgrade all users VWSS perms if needed(incl sysadmin users). .
    '==                      Also- drop attempts to remove orphaned users..
    '==    >> Fix RM-Serial-Search/List (frmFindPart) to add Cancel button and SPEED-UP..
    '==    >> Fix NewJob form- 
    '==           Move ReturnJob and cboPriority to end of 2nd Tab (improve intuitive flow)..
    '==    >> DB Backup.. If running on Server.. Create .bak on local directory first if exists.
    '==                                           Then copy to target..
    '==    >>  and 20-May-2013== Build 3077.520==
    '--       >>  ALLOW leading zeroes on PRODUCT BARCODE..--
    '==               ( JobUpdate/NewPart and RAs/NewRA..=)
    '==
    '==  grh =V:3.0.3077.601/2= Built:01-June-2013= 
    '==     >> Fixes for DB Backup FileCopy failing..-
    '==
    '==  grh =V3.0.3077.611= 11-June-2013=
    '==           >>  1. Job Part: option to enable/disable stripping leading leading zeroes on barcode.
    '==           >>  2. Main Job Preview Panel:  Show JobPart price NOT RM stock price.. (as per JobMaint.)-
    '==
    '==  grh =V3.0.3083.204= Started 03-Feb-2014=
    '==           >>  1. Action Context Menu for jobs in JobsTree;
    '==           >>  2. Rework Main Job Customer Panel;
    '==           >>  3. Rework Main Job Preview Panel;
    '==           >>  4. Staff SignOn via new FORM; 
    '==           >>  5. Startup- Search SQL Servers if needed; 
    '==           >>  6. JobsTree checkbox to show CompanyName 1st on Job Node text;
    '==                         (default is Customer name..)
    '==           >>  7. "DatePromised" on New Job and Job Detail. 
    '==           >>  8. "NewJob" form now has THREE panels. 
    '==           >>  9. New Job Type:  ON-SITE Visit. Priority Type 'S'; 
    '==                      -- Now NOT in Priority..  GoodsInCare=ON-SITE JOB;  ====
    '==           >>  10. Startup- Show What's New in 3083. 
    '==                  (In WebBrowser box with hyperlink to JobMatix.com.au== )
    '==           >>  11. 3083.321- clsRetailHost..  Add "date_modified" to Customer collection... 
    '==
    '==                  >> 3083.329  Delete spurious button on main toolbar. 
    '==                  >> 3083.401  Fix NewJob Form (Activated) ONSITE. Overflow Tomorrow's date calc.
    '==                  >> 3083.402  Fix NewJob Form Previous Goods.. Filter ONSITE Jobs.
    '==                  >> 3083.402  clsBrowse3: New input property:  
    '==                                       InitialOrderIsDescending.. (For date_modified)
    '==           >>  12.  3083.404  JobsTree Reminder to Notify cust..
    '==
    '==  grh =V3.0.3083.618= 18-Jun-2014=
    '==     Tidying up 3083.405..
    '==           1. Cust.Browse- Restore Surname to 1st Column (ASCENDING).;
    '==           2. Main Screen Job Detail-  Add HH:mm to Date Created.; 
    '==           3. Sign-on Form.. Add QUIT button...; 
    '==           4. New Job: Move JobReturned to top of 1st panel,
    '==                   and save previous JobNo in GoodsOther textbox....; 
    '==
    '==   grh =V:3.0.3083.717= Built:17Jul2014= 
    '==         1.NewJob Form; Add UpDown control for No.of Labels to print. --
    '==            AND use U/D control value as No Of Labels to send to the PrintLabels Function..
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW VERSION 3.1 --
    '== 
    '==   grh =V:3.1.3101.907= Built: 07-Sep-2014= 
    '==
    '==         1. This version built to accommodate JobMatixPOS as alternate Retail Host.. --
    '==         2. Startup RE-WORKED to lose Splash form and speed stuff up..
    '== 
    '==  grh. JobMatix 3.1 ---  12-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. (For Jet OleDb driver).
    '== 
    '==  grh. JobMatix 3.1.3101.927 ---  27-Sep-2014 ===
    '==        >>  Add JobMatixPOS as alt. Retail Host.
    '==
    '==  grh. JobMatix 3.1.3101.1009 ---  09-Oct-2014 ===
    '==        >>  Add MAIN TAB Control and POS Sales --
    '==
    '==  grh. JobMatix 3.1.3101.1110 ---  10-Nov-2014 ===
    '==        >>  Standardise on "modFileSupport31" and "modSqlSupport31" --
    '==                For JobMatix Main, POS..
    '==        >> RAs now compiled into main program assembly.
    '==
    '==  grh. JobMatix 3.1.3101.1226 ---  26-Dec-2014 ===
    '==        >> Updated JMxPOS31.dll --
    '== 
    '==  grh. JobMatix 3.1.3103.0115 ---  15-Jan2015 ===
    '==        >> Updated JMxPOS31.dll --
    '==        >> staffTimeout..  test POS for timeoutSuspended
    '==        >> For POS Delivery- point user to POS Sales Tab..
    '==        >> For JobMaint (Update)- Allow Zeroizing SessionTimes..
    '==        >>  3103.129 Jan29.  Make 'Salë' the default Transaction.
    '==
    '==  grh. JobMatix 3.1.3103.0205 ---  03Feb2005 ===
    '==   >>  - Added gbIsSqlServer2008Plus()
    '==   >>  WhoUsing now uses "sp_who" for sql-2000 AND sql-2005-
    '==   >>  Fixes to JmxPos31.dll  -> now 3103.203..
    '==
    '==  grh. JobMatix 3.1.3103.0224 ---  24Feb2005 ===
    '==   >>  - Added Restore-Upgrade-ExRM Migration-
    '==
    '==  grh. JobMatix 3.1.3103.0304 ---  04Mar2005 ===
    '==   >>  - UPDATES to-Upgrade-ExRM Migration/Import-
    '==   >>  =  Also..(ALL users)  Expand Cols ProblemLong & Notifications
    '==   >>  =  Also..(MIGRATION DB's only)  
    '==            Expand Stock Barcode Cols in JobParts, QuoteParts, RA_Items tables..
    '==
    '==   >>  3103.305== Create Licence Key for OriginalDBName
    '==                so licence is still valid after Migration..
    '==
    '==  grh JobMatix31 3.1.3103.0423 -  23-Apr-2015
    '==       >> Add Combo to select Discount %.  -
    '==        >>  New-RA- Add F2 Lookup for barcode text box...
    '==
    '= = = =  = = = = = = = =  = = = = = = = = = =  = = = = = = = =
    '==
    '==   grh JobMatix31  V:3.1.3107.0517= 17May2015= 
    '==     Release Build-
    '==       >> More HelpLabels. 
    '==       >> Add Total Cost to Job Delivery printout. 
    '==       >> Expand cols (ProblemLong, ServiceNotes, SessionTimes, Notifications)- to 4000.
    '==       >> Job Maint- Log to work notes if session times cleared..
    '==       >> Job Maint-  Fix bug- current Session Hours were double added to msSessionTimesTodate..
    '==
    '==  grh. JobMatix 3.1.3107.611 ---  11-Jun-2015 ===
    '==        >>  FrmMaint- 
    '==              - mbCheckCompletion- Add mCurLabour and txtWorkDetails to things to enable optMarkAsComplete.
    '==              - On Completion- Query if No Tasks...
    '==              -  View Only- Disable "Clear" Hours button.
    '==        >>  CreateQuoteJobs-  Fix Save error-  Must include Transaction object in Execute..
    '==        >>  JobMatix Main..  Fix server re-connection problem (delete sign-off call)..
    '==
    '==  grh. JobMatix 3.1.3107.707 ---  07-Jul-2015 ===
    '==       >>  Startup- If "IsInstalling" and no db's, then go straight to CREATE setup form..
    '==       >> modCreateJobs:  Expand cols (ProblemLong, ServiceNotes, SessionTimes, Notifications)- to 4000.
    '==       >> QuotesList:  Make RecentQuotesOnly the default..  AND fix sorting of order_id col.
    '==       >> Quotes:  Check/set quoteChassis default values...
    '==
    '==  grh. JobMatix 3.1.3107.727 ---  27-Jul-2015 ===
    '==       >>  UPGRADED to vs-2013 --..
    '==       >>  UPGRADED to vs-2013 --..
    '==       >>  UPGRADED to vs-2013 --..
    '==       >>  UPGRADED to vs-2013 --..
    '==       >>  Pass Tooltip1 to clsPos31Main----..
    '==       >>  Add btnDiscountPC and click event.----..
    '==       >>  Add Row-Delete BUTTON column to SALE Items dgv.----..
    '==       >>  Limit input sizes on text boxes.----..
    '==
    '==  grh. JobMatix 3.1.3107.0801 ---  01-Aug-2015 ===
    '==   >>  Job Settings and log files now in CommonApplicationData--
    '==   >>   Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)   
    '==   >>   gbLogMsg and glSaveTextFile wasn't/weren't reporting open error..
    '==          SEE  gsLocalJobsSettingsPath()  in File Support..-
    '==
    '==  grh. JobMatix 3.1.3107.0803 ---  03-Aug-2015 ===
    '==   >>  Get the POSSettings dir from clsWinSpecial--
    '==   >>  Moved up to .Net 4.5.2..
    '==   >>  Moved up to .Net 4.5.2..
    '==   >>  Moved up to .Net 4.5.2..
    '==
    '==  grh. JobMatix 3.1.3107.0901 ---  01-Sep-2015 ===
    '==   >>  BACK to .Net 4.5..
    '==   >>  Startup- If no Sql Server found in srch- 
    '==           Show InputBox to input Server\Inst name to try...
    '==   >>  Startup- Use BackgroundWorker for Sql Connect.- 
    '==
    '==  grh. JobMatix 3.1.3107.0907 ---  07-Sep-2015 ===
    '==   >>  Rich Text Job Detail-..
    '==         Context menu stuff to copy selection--
    '==   >>  Fix ON-SITE box colour on Job Service Printout.-..
    '==
    '==  grh. JobMatix 3.1.3107.0911 ---  11-Sep-2015 ===
    '==   >>  BACK to .Net 3.5..
    '==   >>  BACK to .Net 3.5..
    '==   >>  BACK to .Net 3.5...
    '==
    '==  grh. JobMatix 3.1.3107.0922 ---  22-Sep-2015 ===
    '==   >>  Updated PO dll with PO additions.
    '==   >> Fixed startup SQL Search bug 
    '==                     (was failing when found only ONE SQL Server..)
    '==   >> Previous invoices--
    '==   >>  -- Now use command button to pop up list of invoices.
    '==
    '==  grh JobMatix 3.1.3107.1013-  13-Oct-2015  ==
    '==    >> For Gavin (LE Charlestown). Notify Cust. (SMS) 
    '==            Allow user to insert Cust Name in SMS.. (keywords &&FIRSTNAME, &&LASTNAME)
    '==    >> SO we- must pass collection of RM Cust Info as input to Notify...
    '==
    '==   grh =V:3.1.3107.1015= Built: 15-Oct-2015 = 
    '==         >> Fix Jobs Tree refresh-  was not clearing Cancelled Nodes.. --
    '==
    '==   grh =V:3.1.3107.1213= Built: 13-Dec-2015 = 
    '==         >> Updated Staff SignOn text msg... --
    '== 
    '==
    '==  NEW VERSION 3.2.1229-  29-Dec-2015=
    '==  NEW VERSION 3.2.1229-  29-Dec-2015=
    '==
    '==   With Attachments Table and CLASS plus frmAttachments -
    '==    >> Add new Main TAB Control To enclose Progress Tabset and Attachments Tab---
    '==    >>  Add Attachments button on Job Detail pane..
    '==
    '==   grh =V:3.2.3203.105= Built: 05-Jan-2016 = 
    '==    >>  In Load Event-  Check for Attachments Table BEFORE loading Schema Collections.
    '==    >>  refresh local setting vars before calling PrinterPrefs Form..
    '==     >> 09Jan2016-  Separate Attachments Tables for JOBS and RAs..
    '==     >> 18Jan2016- V2 Upgrade TRANSACTION- Includes
    '==               New Jobs Column (SystemUnderWarranty) 
    '==                  + Attachments Tables for JOBS and RAs..
    '==     >>  Also Priority Colours re-organised.
    '==            Updated JobDetail for SystemUnderWarranty..
    '==     >>  18Jan2016-  Update P2 icon to be PINK. ("Rose in Paint prog.).
    '==     >>  24Jan2016-  BookJob now decided in NewJOb Form...
    '=      >> 24Jan2016-  Main JobsTree- 
    '=                    Added "Send back to Input Queue" to TreeView Context menu.
    '==                        and JobActions TooStrip...
    '==
    '==     >> 11Feb2016-  3203.211- 
    '==                -- GoodsIncare Entry Form- 
    '==                     (SubClass DataGridView to change ENTER key to TAB)..
    '==                -- JobMatix Setup Form- ADD Checkbox [Do not enforce MinCharge].
    '==                         ( Default is unchecked =No)
    '==                -- JobMatix Jobs Tree. Lose the ATTEMTION icon..
    '==                -- Queued Job can go back to Wait-listed.
    '==                -- CheckBox to Hide (dim) Wait-listed Jobs.
    '==
    '==   grh =V:3.2.3203.219= Built: 19-Feb-2016 = 
    '==             >>- 3203.219- Add "Clear Alert"to Job Context menu..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '==
    '==  NEW VERSION 3.3.3311-  24-Feb-2016=
    '==  NEW VERSION 3.3.3311-  24-Feb-2016=
    '==  NEW VERSION 3.3.3311-  24-Feb-2016=
    '==
    '==    >> Update Attachments to include MS Word and Excel documents -
    '==    >> Split RAs back into separate EXE..
    '==    >> All systemInfo work now via Class clsSystemInfo ..
    '==    >> All localSettings work now via Class clsLocalSettings..
    '==    >> RAs Launch button.. Fix RA colour (255,96,48)..
    '==
    '==  BACKTRACKING-  3.3.3311.308-  08-Mar-2016=
    '==      INCORPORATING RAs Main Panels (from frmRAsMain) -
    '==       Into JobMatix main screen. "TabPageRAs"- holding TabControlRAs-
    '==     3311.321-
    '==        >>-  RA's Suppliers Grid to choose RAs Package via supplier.   
    '==     3311.327-
    '==        >>-  RA's New RAs re-vamped.   
    '==        >>-  POS or RM.. Setup (Install) and frmStartup have to choose this..   
    '==        >>-  Setup (Updating info) must pass RetailHost1 object to SetupDB..   
    '==
    '==     3311.330-
    '==        >>-  Labour prices now from Retail Host Stock..
    '==                    We just keep barcodes for P1,P2,P3...   
    '==        >>  Fixing User Logo app path.. + add .PNG..
    '==
    '==     3311.402-
    '==        >>-  Reminder Panel.. Include Onsite jobs for Signed-on tech.
    '==     3311.405-
    '==        >>- BG Task- Reminder SMS's for Onsite jobs for all techs.
    '==
    '==     3311.410-
    '==        >>- Now referencing JMxPOS330.dll..
    '==     3311.422-
    '==        >>- Jobs Tree- Job Action Context Menu-  FIXED "View" item...
    '==               (ie Delivered Jobs had no context menu items at all)..
    '==
    '==   '==3311.423= for AutoAssign Jobs..
    '== -- mnuAutoAssignOrphanJobsOnUpdate -
    '==        If mSysInfo1.exists("AutoAssignOrphanJobsOnUpdate") AndAlso _
    '==                     UCase(mSysInfo1.item("AutoAssignOrphanJobsOnUpdate")) = "Y" Then
    '==
    '==  grh 3311.507-  07may2016-
    '==  -------------
    '==         >> Add "Sale" RoadioButton to POS Sale Tab for updated POS dll.
    '==         >> Add "JobNo" to ON-SITE SMS..
    '==
    '==     3311.508-
    '==        >>- BG Task- Reminder SMS's for Onsite jobs NOE GONE to clsStaffReminders..
    '==
    '== = = NEW RELEASE  ++++++
    '==
    '==  -- 3311.708- 08July2016-
    '==          >> Update SALE frame for redsigned POS Item Entry via Textboxes..
    '==              (No more editing in Sale Items Grid..)
    '==
    '==  -- 3311.710- 10July2016-
    '==        >> Fixes to Main CustomerGrid/Cust-Jobs Listview-
    '==                 (Making DblClick work to View Job Service Record..)
    '==
    '==  -- 3311.731- 31July2016-
    '==        >> Updates to Onsite SMS Reminder label-
    '==        >> At staff sign-on, Save updated collection of staff mobiles (key=nomTech)
    '==        >>  New Job Status:  
    '==             k_statusInProcessQA As String = "43-InProcessQA" '=3311.731=--locked during QA jobMaint..-
    '==
    '==  -- 3311.802- 02Aug2016-
    '==        >> Disable Update etc in JobActions if Job is IN USE (323, 33 or 43)-
    '==
    '==
    '==  -- =V:3.3.3311.0817- 17Aug2016-
    '==           >> 3311.817= Don't refresh JobsGrid after doing stuff !!!.
    '==               AND Don't refresh Jobs TreeView if we're not in Treeview Frame..
    '==                 AND reset Staff Timer to full tank after any activity. 
    '==                 AND Disable Search/Clear Grid Buttons during search. (stops multiple hits).
    '==
    '==  -- 3311.0831- 31Aug2016-
    '==         >>-- Fixes to JobMatix Main- 
    '==                 A) JobDetails priority Combo was overpopulated.
    '==              ALSO- B) Fix to stop SecurityId TEST popup msg on Startup-
    '==                And C) Checking that JOB-DETAIL refreshes ok. after Job Update etc..
    '==                 And D) Add Link to JobReports33.exe (.Net).. if installed.
    '==                 And E) Fix "labFindCust" label size in Cust. Grid Panel (courtesy Gavin).
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW BUILD 3323 --
    '==
    '==  -- 3323.1111- 11-Nov-2016-
    '==         >>-- NEW Jobs Tree Context menu Option-- 
    '==               TRANSFER Job to different Customer..
    '==
    '==       RELEASED to Precise PCs ONLY..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==   ANOTHER NEW BUILD 3327 --
    '==   ANOTHER NEW BUILD 3327 --
    '==
    '==  -- 3327.0117- 17-Jan-2017-
    '==         >>-- Update to go with Updated POS 3303.-- 
    '==               (With Cashup and Improved Reports)...
    '==         >>  Also fixing Alt-F4 escape from Sogn-on..
    '==         >>  Also Fix to frmNewPart ( getting quantity combo value)-
    '==
    '==  -- 3327.0119- 19/20-Jan-2017-
    '==         >>--includes POS Update to fix frmImport for Staff Columns..-- 
    '==         >>-- Fixes SQL ERROR in clsStaffReminders (INSERT not escaping apostrophes.)..-- 
    '==         >> -- Set RED forecolour for SMS BG msg if ERROR--
    '==         >>-- TABLE RAItems-  Expand column RA_Symptoms from 240 to 511 chars-..-- 
    '==
    '==     THIS IS for GENERAL RELEASE.. (Done 30Jan2017=
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW BUILD 3357-
    '==
    '==    -- 3357.0205- 05-Feb-2017-
    '==         >>-- Update to go with Updated POS 3307==-- 
    '==             (SupplierReturns Update Function to be called from RAs.)...
    '==         >>--RAs-  Add column RM_SerialAudit_id" "to RAItems Table ==-- 
    '==               and Expand RM_ItemBarcode to 40 chars.
    '==         >>--RAs-  IF Retail is JobMatix POS, then call PO-GoodsReturned when Goods Sent. ==-- 
    '==
    '==    --  v33.3.3357.0219/0223 =
    '==      >> Handle the POS txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..
    '==      >> txtSaleItemBarcode_TextChanged Event now captured for ItemBarcode ONLY--
    '==      >> -- chkMyJobs is now a combo- [MyJobs, All Jobs, Unclaimes Jobs] --
    '==      >> 23Feb2017= JobMaint- Update--  Timestamp a ServiceNote and save for any status change
    '==               eg.  back to bench from QA.. -
    '==      >> 23Feb2017= main Screen Job Details- ESCAPE backslash chars for RichText Box.
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW VERSION 3.4.3401-  15-Mar-2017=
    '==    NEW VERSION 3.4.3401-  15-Mar-2017=
    '==      NEW VERSION 3.4.3401-  15-Mar-2017=
    '==
    '==    1. - Re-design main form (POS Panel) and fix startup Code for POS Multi-Sale presentation. -
    '==    2.  This Main form is now re-named to "frmJobMatixMain34" ---
    '==    3. -3401.0411 Updates- Replace Prev.Inv. RadioButtons with Buttons. -
    '==       - Replace Trans.Selection  RadioButtons with Combo DropDpwn.
    '==    4. -3401.0415 Backup Function updated and moved to "modCreateJobs" module. . -
    '==       - Recognition of Thin Clients..
    '==
    '==
    '==   v3.4.3401.0424 -- 24Apr2017= Extra fix-
    '==         -- frmSetupJobsDB3..-  New MYOB-RM users must have "_jobtracking" DB name..
    '==                  AND NO POS tables will be included in DB..
    '==         -- frmMigration..  New db name (x_jmpos) set up is now fixed (user can't change)..     
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  New Build 3403 24-May-2017=
    '==
    '==
    '==   v3.4.3403.0524 -- 24may2017= x-
    '==         -- Main Form updates for POS Laybys..
    '==         --  3403.0609- JobActionReturnToQueue- Ask if Job is to be UN-ASSIGNED..
    '==
    '==
    '==   v3.4.3403.0627 -- 27Jun2017= - Tidy UP for release.
    '==         -- Fixes to (RAS's) to Prompt0.. ...
    '==         --  Fix our About Menu. 
    '==    3403.719 
    '==    -- Add event for POS Item Line CLEAR button..
    '==
    '==   v3.4.3403.0801 -- 01Aug2017= - 
    '==    --  3403.801  Re-arrange POS HOLD/Restore buttons.(to right SideBar columns)..
    '==
    '==  Build REVISED AGAIN for fixes...
    '==
    '==   v3.4.3403.0919 -- 19Sep2017= - 
    '==      -- Just for Fixes to POS Sales-Invoice Report, and Adding POS Customer pricing Grades....
    '==      --  Slight re-modelling of JobTracking Customer Grid Header..
    '==
    '==   == THIS now upgraded to Build 3411--
    '==      -- 21 -Dec-2017=
    '==   >> 3411.1221 -- Form frmPOS34Main (Sale form) was moved into MxPOS340 dll assembly .
    '==                --  and is so compiled into POS349ex EXE to be launched from JobTracking.
    '==                --  SO- all POS Tab-Panel and stuff is stripped from JobMatix- 
    '==                      and we have have POS Button JmxPOS349ex EXE can be launched from JobTracking.
    '==
    '==      -- 21 -Dec-2017=
    '==
    '==   >> 3411.0113 -- 13-Jan-2018= Update for POS subscriptions/Emails.
    '==
    '==   >> 3411.0118 -- 18Jan2018-  
    '==        -- Another attempt to move RA's to it's own Process/EXE.
    '==   >> 3411.0121  DROP all the RAs Tab Panel..
    '==        --   and Add Button to Launch RAs .exe..
    '==   >> 3411.0129  29-Jan-2018
    '==       -- Update What's new for release..
    '==
    '==
    '==  >> 3411.0214=  14-Feb-2018= Updated StartUp design...
    '==          -- frmStartup- Added Tab Control for New/Old users... 
    '==          --  Always Show Startup Form if NEW user, as MYOB/POS decision now in Startup Form
    '==  >> 3411.0217=  17-Feb-2018= ..
    '==          -- (POS only)  Add (in Customer-Frame) Button to add New Customer... (calls POS).
    '==
    '==--     (3411.0217 Was released to Precise..)
    '==
    '==   >> 3411.0228=  26/28-Feb-2018= 
    '==        -- Dropping SignOn Form....
    '==        -- SignOff/SignOn now on Main Page. 
    '==        -- SignOff timer can be short (15 secs) or long (300)... 
    '==
    '==
    '==-- ----(3411.0228 Was released to Precise..)
    '==-- ---- (3411.0228 Was released to Precise..)
    '==
    '==   >> 3411.0302=  02/05-Mar-2018= ..
    '==         -- Remember local user's sign-off Timeout preference in local settings..
    '==         -- Reset staff timeout on keyboard/mouse activity.. 
    '==
    '==   >> 3411.0314=  14-Mar-2018= ..
    '==         --Use "GetLastInputInfo". (User32.dll) to measure Idle Time..
    '==               (dropped Form-keyboard event method)...
    '== - - - - - - - - - - - - - - - - - - -- - 
    '==   >> 3411.0411=  11-Apr-2018= ..
    '==      -- Re-work design of "mbRefreshJobsTreeView". to speed up.
    '==      -- Use MyTreeView (subClassed) for tvwJobs..
    '==      -- Jobs TreeView major change.. ClearAll and Rebuild Every time..
    '==          (Subclassed TreeView for double- buffering.)
    '==      --   (Now shows only three months of cancelled jobs.)-
    '==           AND- 3411.0415= Exclude WAIT-LISTED from Unclaimed Jobs.-
    '==
    '==-- (3411.0417 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - 
    '==
    '==   >> 3411.0423=  23-Apr-2018= ..
    '==        -- Update just for POS PurchaseOrders changes..
    '==        --  Swap JobParts sub menus around.
    '==
    '==-- (3411.0423 Was released to Precise..)
    '==-- (3411.0423 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - - - - - - -- - 
    '==
    '==  NEW BUILD-
    '==
    '==    >> 3431.0427=  27/28-April-2018..
    '==         -- FIX ALL FORMS to replace "msgbox"  with .Net "MessageBox"..
    '==         -- FIX ALL FORMS to MOVE "Activated" event stuff to "Shown" event..
    '==        -- Convert MAIN Startup to "Sub Main"..
    '==     -- 3431.0505- New Job/Amend- Send Exchange calendar update to DISK BG Queue..
    '==             And send FileName back to main Form- 
    '==             And on main Form-  Add BG Exchange Update BackgroundWorker to do Exchange Work..
    '==     -- 3431.0513-
    '==           Increase ServiceNotes width to varchar(MAX)..
    '==    
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==-- (3431.0515 Was released to Precise..)
    '==-- (3431.0515 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - - - - - - -- - 
    '==
    '==    >> 3431.0523- 23-may-2018=
    '==         --  Add Create Trigger (modAlterTableTrigger) for ALTER_TABLE..
    '==            This trigger is to detect previous builds of JobMatix -
    '---                  - from restoring old columns widths (4000) for the columns-
    '==             ProblemLong, ServiceNotes, SessionTimes or Notifications..
    '==
    '==-- (3431.0527 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - - - - - - -- - 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '==  DLL version of JobTracking. 10-June-2018=
    '==  JMxJT350.dll 
    '==   3501.0610 -- -  
    '==      Main form now called from the Base EXE JMxJT350Ex..
    '==       --  SQLServer searching and DB Selection done by JobMatix35/POS startup shell. ....
    '==       --  Database Name also comes in as input Property....
    '==       --  Removed all "End" statements.....
    '==       --  Removed module modJobMatixMain.....
    '==       --  DLL Version/Build No to be used now.......
    '==
    '== grh JobMatix v3.5.3501.0615
    '==   -- 15-June-2018=
    '==     --  Created New Module "modAllFileAndSqlSubs"..
    '==           to combine All fileSupport and sql support Functions.
    '==        (Takes all content from and obsoletes modFileSupport33, modSqlSupport31, and modSqlInfoSchema31.)
    '==    3501.0617 --  
    '==       --  On-site Jobs Panel.. Add Context menu to update Job etc....
    '==       --  On-site Jobs Panel.. RE-Order- NOW is date DESCENDING....
    '==
    '==
    '==    >> 3501.0625 25-June-2018= (ported from 3431.0622- )
    '==       --  Exchange BG task- Detect invalid XML file data eg. reserved chars (eg Amoersand etc.)...
    '==       --  NewJob/Amend- Fix (cleanup) XML file data for reserved chars (eg Ampersand etc.)...
    '==       --  Deliver Job from JobsTree Context menu..  Add Option to Expedite mark as Delivered..
    '==
    '==    3501.0708 08-July-2018= (Updates from 3431.0707- )
    '==       -- Exchange201 Updates from 3431.0707-
    '==             (More reporting.)..
    '==
    '==
    '==    3501.0713 13-July-2018= (Updates from 3431.0712- )
    '==       -- frmMigration and frmImportRM Updates from 3431.0712-
    '==       -- Updated Reports Launch Function to pass across Labour Rates-
    '==
    '==    3501.0724 24-July-2018= 
    '==       -- "NewInJobMatix35.htm" is now picked at runtime from Runtime Directory.-
    '==       -- Dropped POS launch button..-
    '==
    '==
    '== -- Updated 3501.1105  05-Nov-2018=  
    '==     -- (a)  DB RESTORE-  make sure both db-names "_jobtracking" and "_Jmpos" are accepted.
    '==     -- (b)  IMPORT latest browser form frmBrowse33 and  clsBrowse34 class from POS latest.
    '==     -- (c)  New Job Form (ON-SITE Jobs.)  Add  a numericUpDown control to select Job Duration in hours. 
    '==            ALSO, then add this value as Meeting Duration in Exchange Calendar update.
    '==               ie.. ExchangeBG Worker will pass an extra parameter (Duration) to the Exchange module.
    '==     -- (d)  New Job Form-  Enforce Yes/No entry for Charger/PwerSupply included or not with Goods.
    '==
    '==
    '==    New Build No.- 3519.0108 08-Jan-2019= 
    '==      --  (Fixes to Discovering users for JobTracking only)
    '==            ( Only count those users for this program.)
    '==
    '==    Updated- 3519.0122 22-Jan-2019= 
    '==           - Fixes to Discovering and fixing Updating Wrong Job Problem (Job No mismatch.)..
    '==           - Now using TabControl for TreeView/OnSite/Jobs/Customer panels..
    '==
    '==    Updated- 3519.0129 29-Jan-2019= 
    '==           - Fixes to Startup AppPath to correctly find user Biz logo...
    '==  RELEASED as 3519.0130--
    '==
    '==    Updated- 3519.0414 14-April-2019= 
    '==       (a)  FROM POS--     GET Updated version of frmBrowse33 (to accept User's Select-Sql)..
    '==       (b)  Allow JobTracking to deliver Job directiy if no value, and user confirms.
    '==       (c) Extra 'Slashes' may appear in User Passwords in Job Record.. 
    '==                Detect these in the Job Amend Shown event, and reset password fields on the new Job Form.  
    '==               ie. In the code below, if extra slashes gives more users or passwords then there are text boxes, 
    '==                     then clear all users/passwords and tell user to enter then again.   
    '==                  ALSO, check for them in the User/Password textbox validations
    '==
    '==
    '==  NEW VERION..
    '==    Updated- 4201.0627 27-June-2019= 
    '==       Just FOR NEW POS version 4.2..
    '==
    '==    Updated- 4201.0717 16-July-2019= 
    '==       >> Job Search DataGridView.. ALSO the Customer SearchGrid/Jobs Listview-  
    '==          --  Add Right-click context menu for Job Actions, as per Active Jobs Tree treeview.
    '==
    '==    Updated- 4201.0727 27-July-2019= 
    '==     --  Fix to Timer/activity sensor to stop immediate timing out after staff sign-on
    '==
    '==
    '==  NEW BUILD-    4219 VERSION
    '==    Updated- 4219.1128 21-Nov-2019= 
    '==      -- clsPrintDocs- JobMaint Printing-  Fix Printing WorkHistory for Multiple Pages.
    '==      --  MAKE Forms PUBLIC- NewJobForm and Maint Form-  "frmNewJob32" and "frmJobMaint32"
    '==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..
    '==      --  Update RAs reference to call "JobMatixRAs42.exe"..
    '==      -- Drop Error Popup "There is no Job No or details selected." 
    '==                 Happens when DoubleClicking on Tree Section. 
    '==
    '==  NEW REVISION  4219.1214 VERSION started 08-Dec-2019=
    '==    Updated- 4219.1208 08-Dec-2019= 
    '==      --  MAKE Form "frmNewJob32" INTO USERCONTROL.
    '==      --  MAKE NEW Form "frmNewJobBase" INTO USERCONTROL.
    '--            SO THAT we show it as a Child in a POS TAB..
    '==             From JobTracking we call frmNewJobBase, which is container for the UserControl.
    '==      --  Add a new 5-min. Timer to Main Form to check for Exchange-Calendar xml files that might have come from POS...
    '==               -- If there is any, and BG-Worker exchange is not running, the Run It...
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
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
    '==         1. The UPDATE statement clause
    '==            sSql = sSql & " ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',4000) "
    '==            Needs to be changed to this: (because ServiceNotes is now varchar(max).)
    '==                  sSql = sSql & " ServiceNotes=(ServiceNotes+'" & sNewNote & "') "
    '==         2. The procedure should check for external updates AFTER getting msg text from 	UI,
    '==                	and inside a transaction..  (NOT DONE !!)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = == = = = = = = 
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '==
    '==
    '== Target-build-4267  (Started 07-Sep-2020)
    '== Target-build-4267  (Started 07-Sep-2020)
    '== Target-build-4267  (Started 07-Sep-2020)
    '==
    '==  -- A. Exchange201-  Completion of BG-worker..  
    '==         NEW module "modMyMsgBox" with Function to create a Form on the fly with textbox to show accumulated Exchange results 
    '==           (MessageBox not suitable for exended message..) 
    '==
    '==   -- B. Starting Up-  If POS is mbIsJobmatixPOS, and Stock Not imported from MYOB,
    '==                             then set up SysInfo barcodes for Labour Rates..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '== Target-Build-4284  (Re-Started 23-Nov-2020)
    '== Target-Build-4284  (Re-Started 23-Nov-2020)
    '== Target-Build-4284  (Re-Started 23-Nov-2020)
    '==
    '==   A. -- Bring JobReports into JobTracking Main as a UserControl..  DONE..
    '==
    '==   B. -- Cancel Job (frmNewJob etc)..  Update Calendar to delete the Appointment..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-6201 --  (18-June-2021)
    '==   Target-New-Build-6201 --  (18-June-2021)
    '==
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==  For JobMatix62Main- OPEN SOURCE version...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    '--  STUFF for TreeView Backcolour..--= = = = = = = = =
    '--  STUFF for TreeView Backcolour..--= = = = = = = ==
    Private Declare Function SendMessage Lib "user32" _
                                           Alias "SendMessageA" (ByVal hwnd As Integer, _
                                                                  ByVal wMsg As Integer, _
                                                                   ByVal wParam As Integer, _
                                                                    ByVal lParam As Integer) As Integer
	
    Private Declare Function InvalidateRect Lib "user32" (ByVal hwnd As Integer, _
                                                         ByVal lpRect As Integer, _
                                                             ByVal bErase As Integer) As Integer
	
	Private Declare Function UpdateWindow Lib "user32" (ByVal hwnd As Integer) As Integer
	
    Private Declare Function GetWindowLong Lib "user32" _
                                  Alias "GetWindowLongA" (ByVal hwnd As Integer, _
                                                         ByVal nIndex As Integer) As Integer
	
    Private Declare Function SetWindowLong Lib "user32" _
                                   Alias "SetWindowLongA" (ByVal hwnd As Integer, _
                                                          ByVal nIndex As Integer, _
                                                          ByVal dwNewLong As Integer) As Integer

	Private Const GWL_STYLE As Short = -16
	Private Const TVM_SETBKCOLOR As Short = 4381
	Private Const TVM_GETBKCOLOR As Short = 4383
	Private Const TVS_HASLINES As Short = 2
	'-- redraw--
    Private Const WM_SETREDRAW As Integer = &HBS

    '=3411.1221=
    '-SetForegroundWindow-  To switch to POS process..
    Declare Function SetForegroundWindow Lib "user32.dll" (ByVal hwnd As Integer) As Integer

    '=3411.0314=  Idle Time..

    '==  The following code calls the library user32.dll GetLastInputInfo and function. 
    '==    [DllImport("user32.dll")] 
    '==     public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii); 

    '-  http://vbnet.mvps.org/index.html?code/system/getlastinputinfo.htm 

    '- https://stackoverflow.com/questions/6289078/can-i-retrieve-the-user32-dll-getlastinputtime-from-a-system-service 

    <StructLayout(LayoutKind.Sequential)> _
    Structure LASTINPUTINFO
        <MarshalAs(UnmanagedType.U4)> _
        Public cbSize As Integer
        <MarshalAs(UnmanagedType.U4)> _
        Public dwTime As Integer
    End Structure

    '= Private Declare Function GetTickCount Lib "kernel32" () As Long
    '==
    '==    Updated- 4201.0727 27-July-2019= 
    '==     --  Fix to Timer/activity sensor to stop immediate timing out after staff sign-on
    Private Declare Function GetTickCount Lib "kernel32" () As Integer


    '== Declare Function GetLastInputInfo Lib "user32.dll" (ByVal hwnd As Integer) As Integer
    Declare Function GetLastInputInfo Lib "user32.dll" (ByRef plii As LASTINPUTINFO) As Boolean

	'= = = = = = = = = = = = = = = = = = = = ==  =

	Const K_SAVEJETPATH As String = "lastJetPath.txt"
	'--Const K_SAVESQLSERVERNAME = "lastSqlServerName.txt"
	
    '== 3107.801 - Const K_SAVESETTINGSPATH As String = "localJobSettings.txt"
	Const K_FINDACTIVEBG As Integer = &HC0FFFF
	Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--
	
	Const K_JOBSTABSNO As Short = 1 '-no of tabs for jobs..--[0..0]
	Const K_MAXJOBTABS As Short = 0 '-highest tabno for jobs..--
    Const K_EXPLORER_TOOLTIP As String = "Jobs Explorer shows active and recent Jobs " & vbCrLf & _
                                      "arranged according to Job status (stage) in the Job processing cycle.."
    Const K_SEARCH_TOOLTIP As String = "Jobs Search Panel and grid can show all jobs according to status, " & vbCrLf & _
                                      " and provides ""full-text"" search on all Jobs currently on file.."

    Const K_GRIDFONTSIZE_SETTINGNAME As String = "GRIDFONTSIZE"
    Const K_STARTUPFULLSCREEN_SETTINGNAME As String = "STARTUPFULLSCREEN"

    Const K_ABSOLUTE_MAX_USERS_PERMITTED = 31

    Private Const K_GOODS_ONSITEJOB = "ON-SITE JOB;"   '--3083.312--
    '= = = = = = = = = = = = = = = = = = = = = =

    '=3411.0303=
    Private Const k_StaffTimeoutInterval_long As Integer = 300  '--seconds-
    Private Const k_StaffTimeoutInterval_short As Integer = 30  '--seconds-
    Private Const k_AutoSignOffSettingsName As String = "AutoSignOffOption"


    '--Public gbDebug As Boolean
    '--Public gbDevel As Boolean
    '- - - - -
    Private mbIsInitialising As Boolean = True
    Private mbActive As Boolean = False
    Private mbStartingUp As Boolean
    Private mbMainLoadDone As Boolean = False
    Private mbFormClosing As Boolean = False

    Private mbIsInstalling As Boolean = False
    Private mbInstallingJobMatixPOS As Boolean = False

    Private mbCreateMode As Boolean = False
    Private mbSuperAdmin As Boolean = False
    Private mbV13Only As Boolean = False
    Private mbV20_Only As Boolean = False

    '=3401.415= 
    Private msMachineName As String = "" '--local machine (Host for RDS ot Standalone---
    Private msComputerName As String = "" '--client or Fat machine--
    Private mbIsThinClient As Boolean = False

    Private msServer As String = ""
    Public gbIsSqlServer As Boolean = False
    Public gbIsJetDB As Boolean = False
    '-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""
    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    '=3501.0616=
    Private msJobmatixAppName As String = ""

    Private msJobsUserName As String '--normal user login..--
	Private msJobsUserPwd As String
	
	'--  Business Info-
	'--  Business Info-
	Private msBusinessABN As String
	Private msBusinessUser As String
	Private msJT2SecurityIdOriginal As String '--as stored in SytemInfo in Row "JT2SecurityId"..-
	Private msJT2SecurityId As String '-- AS computed from ABN DateCreated.  --
	Private msBusinessName As String
	Private msBusinessAddress1 As String
	Private msBusinessAddress2 As String
	Private msBusinessShortName As String
	Private msBusinessPhone As String
	Private msBusinessPostCode As String
    Private msBusinessState As String

    Private msUserLogoPath As String = ""

    '--  L i c e n c e --
    '--  L i c e n c e --
    Private mdDateCreated As Date
    Private msLicenceKey As String = ""
    '== Private msLicenceKeyLevel2 As String = ""
    Private mbIsFullLicence As Boolean = False
    '== 3072/3 == Private mbIsThreeUserLicence As Boolean = False
    Private mbLicenceOK As Boolean = False
    Private mIntMaxUsersPermitted As Integer = 0   '--none--

	Private msGSTPercentage As String
	Private mCurGSTPercentage As Decimal
	
    '==3311.331= Private mCurLabourHourlyRateP1 As Decimal
    '==3311.331= Private mCurLabourHourlyRateP2 As Decimal
    '==3311.331= Private mCurLabourHourlyRateP3 As Decimal
	Private mCurLabourMinCharge As Decimal
	
    '==3311.331= Private msDescriptionPriority1 As String
    '==3311.331= Private msDescriptionPriority2 As String
    '==3311.331= Private msDescriptionPriority3 As String
	'-- quotes--
	Private msQuoteChassisCat1 As String '--eg 'MOTHER'--
	Private msQuoteChassisCat2 As String
	'--Barcodes..-
	Private msItemBarcodeFontName As String
	Private mlItemBarcodeFontSize As Integer
	'-- service categories..--
	Private msServiceChargeCat1 As String
	Private msServiceChargeCat2 As String
	'-- Job Label Dimensions..-
	'===Private msJobLabelPrintDepth As String '--SystemInfo.. label actual depth mm..--
	'===Private msJobLabelGapDepth As String    '-- label GAP depth mm..--
	
	'--  Rev-2916--
	'--  Rev-2916--
	Private msNewJobDocketFootnote As String
	Private msDeliveryDocketFootnote As String
	Private mCurServiceNotificationCostLimit As Decimal
	
    Private msTermsText As String
    Private msServiceChargesInfoText As String = ""
	'----------------------------
	
	Private msTableName As String
	
	'--Dim mColSqlTables As Collection  '--current sql "catalogue'--
	Private msSqlUid As String
	Private msSqlPwd As String
	Private msJetUid As String '--jet-
	Private msJetPwd As String '--jet-
	
    '=3401.415= Private msComputerName As String '--local machine--
	Private msAppPath As String
	Private msSaveJetPath As String
	Private msSaveSqlServerName As String
	'-Dim mbLoggedOn As Boolean
	
    '--- Actual connections for JOBS ---
	'--- Actual connections for JOBS ---
	'--- Actual connections for JOBS ---

    Private msSqlDbName As String = ""
	Private mColSqlDBInfo As Collection '--  jobs DB info--
    Private mCnnSql As OleDbConnection '= ADODB.Connection '-- 
    Private msOriginalJobMatixDBName As String = ""

    '==  WAS for REPORTS..=  Private mCnnShape As ADODB.Connection '--for jobs SHAPE commands..--
    Private mbSqlConnectionFailure As Boolean = False
    Private msSqlVersion As String = ""
    Private mbIsSqlAdmin As Boolean = False

    '-- Multi-Retail-Host--
    '=4219.1122= Private mRetailHost1 As _clsRetailHost
    Private mRetailHost1 As JMxRetailHost._clsRetailHost

    Private msRetailHostname As String
    Private msProviderCode As String '--RM  or PBPOS or JMPOS..--
    '= = = = = = = = = = = = = = = = = = = = = = = = =  = = = =

    '--Private mColDBMSJet As Collection  '--Retail manager DBMS--
	Private msJetDbName As String '--full MDB path---
	'== Private mColJetDBInfo As Collection  '--  RM DB info--
	'== Private mCnnJet As ADODB.connection                      '--
	Private msJetPathInfoKey As String '--  eg- "RM_JetPath_computername" --
	
	Private mColPrefs As Collection '--new service ..=
	Private mColPrefs2 As Collection '--  service update -
	Private mColPrefsDeliver As Collection '--  view and deliver-
	Private mColPrefsRAs As Collection '--  all statuses..-
	
	'---  printers..--
    '==3043.1== Private miPrtIndex As Short

    Private msDefaultPrinterName As String

	Private msColourPrtName As String
	Private msReceiptPrtName As String
	Private msLabelPrtName As String
	
	'= = = = = = = = =  = = =  = =
	
    '== Private mSdSettings As Scripting.Dictionary '--  holds local job settings..
    '== Private mSdSettings As clsStrDictionary '--  holds local job settings..
    Private msSettingsPath As String = ""
    '==3311.224=
    Private mLocalSettings1 As clsLocalSettings

    '=3311=  Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    '=3311== Private mColSystemInfo As Collection      '--  holds COLLECTION system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo

    '--Private msResults As String    '-- for clipboard..-
    '= = = = = = = =
	Private mbPwCancelled As Boolean
	Private mbPwCompletedOK As Boolean
	Private mdStartTime As Date '--startup time--
	'= = = = = = =  = = =  =
	
	'--staff--
    Private mlStaffId As Integer = -1
    Private msStaffBarcode As String = ""
    Private msStaffName As String = ""
    Private mlStaffTimeout As Integer '--auto sign-off period..--
    '=3411.0228=
    Private mIntStaffTimeoutInterval As Integer = 300  '--can be 15 or 300 secs
	
	Private msSignOnMsg As String
	Private msUseBrowserMsg As String
	
	'-- Results box position..-
	Private mlResultsTop As Integer
	Private mlResultsLeft As Integer
	
	Private mlFormDesignHeight As Integer '-- starting dimensions..-
	Private mlFormDesignWidth As Integer '-- starting dimensions..-
	
	'--Private miReportJobStatus As Integer
	Private mDateOldest As Date
	Private mlDatabaseDays As Integer
	'-- Browse Object..--
    Private mBrowseJobs As clsBrowse3 '== clsBrowse22
    Private mBrowseOnSiteJobs As clsOnSiteJobs  '== 3083==

	'== Private mBrowseRAs As clsBrowse3  '== clsBrowse22
	Private mBrowseCust As clsBrowse3 '== clsBrowse22  '== clsBrowseHost
	
	'===Private mbBrowseCompleted As Boolean
	'===Private mbBrowseCancelled As Boolean
	'===      Private mLngSelectedRow(0 To K_MAXJOBTABS) As Long   '--selected browse row..-
    Private mLngSelectedRow As Integer = -1 '--selected browse row..-
    Private mLngSelectedRowOnSite As Integer = -1 '--selected browse row..-
    Private mLngSelectedRowRAs As Integer = -1 '--selected browse row..-
    Private mLngSelectedRowCust As Integer = -1
	
	Private mLngJobsTreeBGColour As Integer
	'== Private mLngRAsTreeBGColour As Long
	
	Private mLngGridBGColour As Integer
	'= Private mLngGridBGColourRAs As Long
	
	'=====Private mButtonCurrentBrowse(0 To K_MAXJOBTABS) As MSComctlLib.Button
	Private mButtonCurrentBrowse As System.Windows.Forms.ToolStripButton
	'== Private mButtonCurrentBrowseRAs As MSComctlLib.Button
	
	Private miLastTabNo As Short
	'== Private mlRAId As Long
	
	Private msInstallPath As String
	'= = = = = = = = = = = = = = = =
	
    '--  Current CUSTOMER Info..--
    Private mColRMCustomerDetails As Collection

    Private msCustomerName As String = ""
    Private msCustomerCompany As String = ""
    Private mlCustomerId As Integer = -1
    Private msCustomerBarcode As String = ""
    Private msCustomerPhone As String = ""
    Private msCustomerMobile As String = ""
    Private msCustomerEmail As String = ""
    '=3057.1= All prev.jobs goods this Cust.--
    Private mColCustomerJobsGoods As Collection

	
	'--  Current Job Info..--
	'--  Current Job Info..--
	Private mColJobFields As Collection '==== (0 To K_MAXJOBTABS) As Collection  '--current job on show..--
    Private mlJobId As Integer
    Private mbJobReturned As Boolean = False
    Private mbSystemUnderWarranty As Boolean = False
	
	Private msSessionTimesToDate As String
	Private msTimeSpent As String
	
	'-- To SHOW current JOB PARTDS..--
	'-- Caller's SCRATCHPAD for Changes..-
	'===Private mlTempPartsCount As Long   '--array count..-
	'===Private malTempPartsIndexes() As Long   '-- -1=Deleted, 0=NewRecord, 1..n=ExistingTableRecordId  --
	'==='-------                                      (for NewRecord, collection is in mavTempNewData at same index..)-
	'===Private mavTempNewParts() As Variant    '-- FldCollection  if this PART has been added, or Nothing.-
	'==Private maCurTempPartsCosts() As Currency   '-- cost of parts in parts listbox..--
	'===Private mabTempPartsCostUpdated() As Boolean '-- cost of part has changed since PartRecord last written...--
	'==
	Private mCurOrigParts As Decimal '--  Original Job Parts costs..--
	Private mCurParts As Decimal '--  has to be updated in table..--
	
	Private mCurTotalChargeableHours As Decimal
	Private mCurLabour As Decimal
	Private mCurLabourHourlyRate As Decimal '--rate selected for this job..--
	'= = = = = = = = = == = = = = =
	'-- END of  SCRATCHPAD for Changes..-
	'= = = = = = = = = = =  =  = =  =
	Private mColPriorities As Collection
	
    Private mDateDeliveredStart, mDateCancelledStart As Date
    Private miDeliveredMonths As Short
    Private mIntCancelledMonths As Integer
	'= = = = =  = = = = = = = = =
	
	'--save startup JOB RTF contents..-
    Private rtfTemplateJobs As String    '--will be set by user-selectio: 8,9,10.-

    Private rtfTemplateJobs_8 As String
    Private rtfTemplateJobs_9 As String
    Private rtfTemplateJobs_10 As String
    '= = = = = = = = = = = = = = = = = = =

    Private strAboutJobMatix3HTML As String  '--html text for About 3083--
    '= = = = = = = = = = = = = = = = = =  =

    Private mNodeActiveRoot, mNodeDeliveredRoot As System.Windows.Forms.TreeNode
    Private mNodeCancelledRoot As System.Windows.Forms.TreeNode

    '--  QUOTES listing.--
    Private mColQuoteRecords As Collection
    Private mlSortKey As Integer = -1 '--col index for sort..-
    Private mlSortOrder As Integer '-asc/desc-
    Private mlOrderId As Integer = -1
    Private mColSelectedQuoteRow As Collection
    Private mColQuoteItems As Collection

    '== Build 3037.1 == Font sizes..
    '== Private mFontTahoma8pt As Font
    '== Private mFontTahoma9pt As Font
    '== Private mFontTahoma10pt As Font

    '== Private mFontVerdana8pt As Font
    '== Private mFontVerdana9pt As Font
    '== Private mFontVerdana10pt As Font

    Private mFontTvwJobs As Font   '-- Jobs Treeview.. Current Font..-
    Private mSingleNewFontSize As Single

    '-- datagrid default cellstyles..

    Private mDataGridViewCellStyleHdr As DataGridViewCellStyle
    Private mDataGridViewCellStyleData As DataGridViewCellStyle

    '== 3083 ==
    '--  Popup menu for Right click on Job Node in Jobs TreeView..-
    Private mContextMenuJobsTreeNodeAction As ContextMenu
    Private WithEvents mnuJobActionTfrToNewCust As New MenuItem("Transfer Job To New Cust.") '==3323.1109=
    Private WithEvents mnuJobActionCheckIn As New MenuItem("Check-in Job")
    Private WithEvents mnuJobActionReturnToWaitlist As New MenuItem("Return Job To WaitList")  '=3203.211=
    Private WithEvents mnuJobActionAmend As New MenuItem("Amend Service Agreement")
    Private WithEvents mnuJobActionStart As New MenuItem("Start Job")
    Private WithEvents mnuJobActionUpdate As New MenuItem("Update Job")
    Private WithEvents mnuJobActionReturnToQueue As New MenuItem("Return Job To Queue")  '=3203.124=
    Private WithEvents mnuJobActionReOpen As New MenuItem("ReOpen Job")
    Private WithEvents mnuJobActionDeliver As New MenuItem("Deliver Job")
    Private WithEvents mnuJobActionNotify As New MenuItem("Notify Customer")
    Private WithEvents mnuJobActionStopPress As New MenuItem("StopPress")
    Private WithEvents mnuJobActionClearAlert As New MenuItem("ClearAlert")  '== 3203.219=
    Private WithEvents mnuJobActionView As New MenuItem("View Service Record")

    Private WithEvents mnuJobActionSep1 As New MenuItem("-")
    Private WithEvents mnuJobActionSep2 As New MenuItem("-")
    Private WithEvents mnuJobActionSep3 As New MenuItem("-")
    Private WithEvents mnuJobActionSep4 As New MenuItem("-")

    '-- Save TreeView Node right-clicked..-
    Private mNodeJobsTreeContextMenuSelected As TreeNode
    '-- Save ONSITE Jobs Grid right-clicked..-
    Private mIntOnsiteJobNoContextMenuSelected As Integer = -1

    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =

    '-- v3.1--
    '-- v3.1--
    '-- v3.1--

    '-- wait form--
    Private mFormWait1 As frmWait
    Private mColSQLServerInstances As Collection = Nothing

    '-- Background Worker results..-
    '-- Background Worker results..-

    Private mbSqlServerSearchOk As Boolean = False
    Private mbSqlServerGetSchemaOK As Boolean = False
    Private msBuildSchemaLog As String = ""

    Private msSqlConnect As String = ""  '-- Connect string.-
    Private mbSqlServerConnectOK As Boolean = False
    Private msSqlServerConnectResult As String = ""

    '= = = = = = = = = = = = = = = = =
    Private mbLoginRequested As Boolean
    Private msJobMatixVersion As String

    Private mIntJobMatixDBid As Integer = -1

    '== S A L E =
    '--  Call with Sale code ..-
    '== Private mClsSale1 As JMxPOS330.clsPOS31Main
    '-- SEE BELOW=  Private mClsSale1 As JMxPOS330.clsPOS34Sale

    '--=3107=  help label stuff.
    Private mStrLasthelpTextPOS As String = ""
    Private mStrLasthelpTextJobs As String = ""
    Private mStrLasthelpTextQuotes As String = ""

    '--=3107.907=
    '=  Context menu for RichText Info-
    '--  Popup menu for Right click on RTF Job Info..-
    Private mContextMenuRichTextInfo As ContextMenu
    Private WithEvents mnuCopyRichTextSelection As New MenuItem("Copy Selection.")


    '=3311.309= In-house RA's..=
    Private mLngRAsTreeBGColour As Integer
    '= 3411.0121-  RAs Gone- to EXE.. 
    '--Private mClsRAsMain1 As clsRAsMain33

    '==3311.405=
    '-- SMS Reminders..
    '== Private msReminderStatus As String = ""
    Private clsStaffReminders1 As clsStaffReminders
    Private mColFullStaffList As Collection   '=3311.731= --for Reminder SMS..
    '--      because we can't use mRetailHost1 in background Thread !!!!

    '=3519.0108=
    Private msCurrentProcessName As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->


    '= 3401.315= -  M a i n --

    '== POS  S A L E Stuff =
    '== POS  S A L E Stuff =
    '== POS  S A L E Stuff =

    '=3411.1221=  ALL POS stuff GONE to its own EXE..
    '=3411.1221=  ALL POS stuff GONE to its own EXE..
    '=3411.1221=  ALL POS stuff GONE to its own EXE..

    Private msVersionPOS As String = ""
    ''-- Actual Class Instances with Sale states ..-
    ''-- These are always loaded instanced..
    'Private mClsSale_A As clsPOS34Sale
    'Private mClsSale_B As clsPOS34Sale
    'Private mClsSale_C As clsPOS34Sale

    'Private mColFreeSalesInstances As Collection '--holds the free class instance ptrs.-

    ''-- Point to current Instance that has the Screen.
    'Private mClsSale1 As clsPOS34Sale
    ''-- Point to who is in Held-1.
    'Private mClsSaleHeld1 As clsPOS34Sale
    ''-- Point to who is in Held-2 slot.
    'Private mClsSaleHeld2 As clsPOS34Sale
    ''--  ID (A,B,C) of who is in the slots..
    'Private msSaleCurrent_ID As String = ""  '-(A,B or C)-
    'Private msSaleHeld1_ID As String = ""  '-(A,B or C)-
    'Private msSaleHeld2_ID As String = ""  '-(A,B or C)-
    ''- hold temp
    'Private mClsSaleHeldTemp As clsPOS34Sale

    ''-- POS Licence--
    'Private mClsJmxPOS31_Licence As clsJMxPOS31
    'Private mbIsEvaluating As Boolean = False

    '=3411.1221= END OF  POS stuff GONE to its own EXE..
    '=3411.1221= END OF  POS stuff GONE to its own EXE..

    '= = = = = = = = = = = = = = = = =

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->
    '--sub new-
    '--sub new-
    '--sub new-
    '--sub new-

    Public Sub New(ByRef cnnSql As OleDbConnection, _
                    ByVal strSqlServerName As String, _
                    ByVal strSqlServerComputer As String, _
                    ByVal strSqlServerInstance As String, _
                    ByVal strSqlConnectString As String, _
                    ByVal strSqlDB_name As String, _
                    ByVal strJobmatixAppName As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mCnnSql = cnnSql
        msServer = strSqlServerName
        '-- now split server/instance..--
        '= Private msSqlServerComputer As String = ""
        '= Private msSqlServerInstance As String = ""
        msSqlServerComputer = strSqlServerComputer
        msSqlServerInstance = strSqlServerInstance

        '- msSqlConnect-
        msSqlConnect = strSqlConnectString
        '=3501.0610=
        msSqlDbName = strSqlDB_name
        msJobmatixAppName = strJobmatixAppName

    End Sub  '-new-
    '= = = = = = = = == =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->


    '==  4219.1128=  The Functions gsAssembly..." stuff to be private only, as users of this DLL have 
    '==              make their own arrangements about assembly infos..
    '==
    '==

    '-- Assembly name..
    '=Public Function gsAssemblyName() As String
    Private Function msAssemblyName() As String

        '=3501.0611-  MUST get actual assembly ie DLL..
        Dim assemblyThis As Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim assName As AssemblyName
        assName = assemblyThis.GetName
        '= modFileSupport33-
        msAssemblyName = Replace(Replace(assName.Name, ".exe", ""), ".dll", "")  '-replace in case.
        'With assName.Version
        '    msJobMatixVersion = gsAssemblyName & " v:" & CStr(.Major) & "." & CStr(.Minor) & ". Build: " & _
        '                          CStr(.Build) & "." & CStr(.Revision)
        'End With
    End Function  '-assembly name-
    '= = = = = = = == = = = = =  =

    '-- Assembly Version..
    '= Public Function gsAssemblyVersion() As String
    Private Function msAssemblyVersion() As String
        Dim sAssemblyName As String

        '=3501.0611-  MUST get actual assembly ie DLL..
        Dim assemblyThis As Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim assName As AssemblyName
        assName = assemblyThis.GetName
        '= modFileSupport33-
        sAssemblyName = Replace(Replace(assName.Name, ".exe", ""), ".dll", "")  '-replace in case.
        With assName.Version
            msAssemblyVersion = "v:" & CStr(.Major) & "." & CStr(.Minor) & ". Build: " & _
                                                               CStr(.Build) & "." & CStr(.Revision)
        End With
    End Function  '-assembly name-
    '= = = = = = = == = = = = =  =
    '-===FF->

	'--  clean up sql string data ..--
	Private Function msFixSqlStr(ByRef sInstr As String) As String
		
		msFixSqlStr = Replace(sInstr, "'", "''")
		
	End Function '--fixSql-
	'= = = = = = = = = = = =

    '-- Is RetailManager--

    Private Function mbIsRetailManager() As Boolean

        mbIsRetailManager = (LCase(msRetailHostname) = "retailmanager")
    End Function  '- mbIsRetailManager--
    '= = = = = = = = = = = = = = = = == 

    '-- IsIsJobmatixPOS--

    Private Function mbIsJobmatixPOS() As Boolean

        mbIsJobmatixPOS = (LCase(msRetailHostname) = "jobmatixpos")
    End Function  '- mbIsJobmatixPOS--
    '= = = = = = = = = = = = = = = = == 

    '--  change treeView Backcolour..--
	
	Private Sub mSetTreeViewColour(ByRef TreeView1 As System.Windows.Forms.TreeView, ByRef lngColour As Integer)
		Dim lngStyle As Integer
		
		Call SendMessage(TreeView1.Handle.ToInt32, TVM_SETBKCOLOR, 0, lngColour) '====  ByVal RGB(255, 0, 0))  'Change the backgroundcolor to red.
		
		' Now reset the style so that the tree lines appear properly
		lngStyle = GetWindowLong(TreeView1.Handle.ToInt32, GWL_STYLE)
		Call SetWindowLong(TreeView1.Handle.ToInt32, GWL_STYLE, lngStyle - TVS_HASLINES)
		Call SetWindowLong(TreeView1.Handle.ToInt32, GWL_STYLE, lngStyle)
		
	End Sub '--set.-
	'= = = = = = = =
	
	'--get day of week--
	Private Function msDayOfWeek(ByRef date1 As Date) As String
		Dim sDay As String
		
		Select Case DatePart(Microsoft.VisualBasic.DateInterval.WeekDay, date1)
			Case 1 : sDay = "Sunday"
			Case 2 : sDay = "Monday"
			Case 3 : sDay = "Tuesday"
			Case 4 : sDay = "Wednesday"
			Case 5 : sDay = "Thursday"
			Case 6 : sDay = "Friday"
			Case 7 : sDay = "Saturday"
		End Select
		
		msDayOfWeek = sDay
		
	End Function '--day--
    '= = = = = = =  =  = =
    '-===FF->

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    '==   Target-New-Build-6201 --  (18-June-2021)
    'Private Sub mWaitFormOn(ByVal sMsg As String,
    '                  Optional ByVal sHeader As String = "JobMatix42")
    Private Sub mWaitFormOn(ByVal sMsg As String,
                      Optional ByVal sHeader As String = "JobMatix62")

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = msJobMatixVersion
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.labHdr.Text = sHeader
        mFormWait1.Show(Me)
        DoEvents()
    End Sub '- mWaitFormOn-
    '-= = = = =  = = = = = = = = = = =

    '-- kill (hide) wait form--
    Private Sub mWaitFormOff()

        mFormWait1.Hide()
        mFormWait1.Close()
        mFormWait1.Dispose()
        DoEvents()
    End Sub  '--wait--
    '= = = = = = = = = = = = = = = = =

    '= mShowTopHelp  =

    Private Sub mShowTopHelp(ByVal strHelpText As String)

        labStatus.BackColor = System.Drawing.ColorTranslator.FromHtml("#FAFAD2")  '= Color.Goldenrodyellow
        labStatus.Text = strHelpText

    End Sub  '-mShowTopHelp-
    '= = = = = = = = = = = = = == = = =
    '-===FF->

    '-- Load/reload static systemInfo stuff..--
	
	Private Function mbLoadAllSystemInfo() As Boolean
		Dim sMsg As String
		Dim s1, s2 As String
		Dim L1 As Integer
		Dim curOldLabourRate As Decimal
        '= Dim colSystemInfo As Collection
		
		mbLoadAllSystemInfo = False
        '== If gbLoadsystemInfo(mCnnSql, mColSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        '-- =11July2010=  replace all the ELSIFs with accesses to the SysInfo Dictionary..
        curOldLabourRate = 0
        If mSysInfo1.exists("LABOURHOURLYRATE") Then
            If (mSysInfo1.item("LABOURHOURLYRATE") <> "") Then _
                        curOldLabourRate = CDec(mSysInfo1.item("LABOURHOURLYRATE"))
        End If
        '==3311.331= If mSysInfo1.exists("LABOURHOURLYRATEPRIORITY1") Then
        '==3311.331= If (mSysInfo1.item("LABOURHOURLYRATEPRIORITY1") <> "") Then _
        '==3311.331= mCurLabourHourlyRateP1 = CDec(mSysInfo1.item("LABOURHOURLYRATEPRIORITY1"))
        '==3311.331= End If
        '==3311.331= If mSysInfo1.exists("LABOURHOURLYRATEPRIORITY2") Then
        '==3311.331= If (mSysInfo1.item("LABOURHOURLYRATEPRIORITY2") <> "") Then _
        '==3311.331= mCurLabourHourlyRateP2 = CDec(mSysInfo1.item("LABOURHOURLYRATEPRIORITY2"))
        '==3311.331= End If
        '==3311.331= If mSysInfo1.exists("LABOURHOURLYRATEPRIORITY3") Then
        '==3311.331= If (mSysInfo1.item("LABOURHOURLYRATEPRIORITY3") <> "") Then _
        '==3311.331= mCurLabourHourlyRateP3 = CDec(mSysInfo1.item("LABOURHOURLYRATEPRIORITY3"))
        '==3311.331= End If
        If mSysInfo1.exists("LABOURMINCHARGE") Then
            If (mSysInfo1.item("LABOURMINCHARGE") <> "") Then _
                                      mCurLabourMinCharge = CDec(mSysInfo1.item("LABOURMINCHARGE"))
        End If

        '==3311.331= msDescriptionPriority1 = mSysInfo1.item("DESCRIPTIONPRIORITY1")
        '==3311.331= msDescriptionPriority2 = mSysInfo1.item("DESCRIPTIONPRIORITY2")
        '==3311.331= msDescriptionPriority3 = mSysInfo1.item("DESCRIPTIONPRIORITY3")

        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        msBusinessUser = mSysInfo1.item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
        msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
        msBusinessState = mSysInfo1.item("BUSINESSSTATE")
        msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
        msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")


        '--Rev-2916--
        '---  ADD new system info entries if not there yet..-
        '---  ADD new system info entries if not there yet..-
        '=Private msNewJobDocketFootnote As String
        '= Private msDeliveryDocketFootnote As String
        '=Private mCurServiceNotificationCostLimit As Currency
        sMsg = ""
        s1 = Replace(msBusinessShortName, "_", " ") & " will contact you when Job is ready..  Account is payable on collection."
        If Not mSysInfo1.exists("NEWJOBDOCKETFOOTNOTE") Then '--must add..--
            If Not mSysInfo1.UpdateSystemInfo(New Object() {"NewJobDocketFootnote", s1}) Then
                MsgBox("Failed to update NewJob docket footer in systemInfo table..", MsgBoxStyle.Exclamation)
            Else '--ok-
                msNewJobDocketFootnote = s1
                sMsg = sMsg & vbCrLf & "NewJobDocketFootnote = " & s1
            End If
        Else '--already exists--
            msNewJobDocketFootnote = mSysInfo1.item("NEWJOBDOCKETFOOTNOTE")
        End If
        '-- Delivery Docket Footer..-
        s1 = Replace(msBusinessShortName, "_", " ") & " will cover all work on this service for 30 days from date of invoice.."
        If Not mSysInfo1.exists("DELIVERYDOCKETFOOTNOTE") Then
            If Not mSysInfo1.UpdateSystemInfo(New Object() {"DeliveryDocketFootnote", s1}) Then
                MsgBox("Failed to update Delivery Docket footer in systemInfo table..", MsgBoxStyle.Exclamation)
            Else '--ok-
                msDeliveryDocketFootnote = s1
                sMsg = sMsg & vbCrLf & "DeliveryDocketFootnote = " & s1
            End If
        Else '--already exists--
            msDeliveryDocketFootnote = mSysInfo1.item("DELIVERYDOCKETFOOTNOTE")
        End If
        '-- service notification limit..
        s1 = "65.00"
        s2 = "10" '--in case..
        mCurServiceNotificationCostLimit = 10.0# '--in case-
        If Not mSysInfo1.exists("SERVICENOTIFICATIONCOSTLIMIT") Then
            If Not mSysInfo1.UpdateSystemInfo(New Object() {"ServiceNotificationCostLimit", s1}) Then
                MsgBox("Failed to update ServiceNotificationCostLimit in systemInfo table..", MsgBoxStyle.Exclamation)
            Else '--ok-
                s2 = s1
                sMsg = sMsg & vbCrLf & "ServiceNotificationCostLimit = " & s1
            End If
        Else '--already exists--
            s2 = mSysInfo1.item("SERVICENOTIFICATIONCOSTLIMIT")
        End If
        If IsNumeric(s2) Then
            mCurServiceNotificationCostLimit = CDec(s2)
        End If
        '--  SHOW new entries if any..
        If sMsg <> "" Then
            MsgBox("For Information only.." & vbCrLf & _
                     "These new entries have been added to the JobMatix SystemInfo table:" & vbCrLf & _
                                                                    sMsg & vbCrLf, MsgBoxStyle.Information)
        End If

        '--Rev-2918-- 06Aug2011==
        '---  ADD TERMS TEXT system info entry if not there yet..-
        '---  ADD TERMS TEXT system info entry if not there yet..-
        If Not mSysInfo1.exists("TERMSANDCONDITIONS") Then
            s1 = gsGetTermsAndConditions()
            If Not mSysInfo1.UpdateSystemInfo(New Object() {"TermsAndConditions", s1}) Then
                MsgBox("Failed to update TermsAndConditions in systemInfo table..", MsgBoxStyle.Exclamation)
            Else
                msTermsText = s1
                MsgBox("For Information only:" & vbCrLf & _
                            "Terms and Condtions have been updated..: " & vbCrLf & s1, MsgBoxStyle.Information)
            End If
        Else '--exists--
            msTermsText = mSysInfo1.item("TERMSANDCONDITIONS")
        End If '--terms.
        '-- UPDATES FINISHED..--
        '-- UPDATES FINISHED..--

        '---msServiceChargesInfoText--
        If mSysInfo1.exists("SERVICECHARGESINFOTEXT") Then
            msServiceChargesInfoText = mSysInfo1.item("SERVICECHARGESINFOTEXT")
        End If

        msLicenceKey = mSysInfo1.item("LICENCEKEY")
        msGSTPercentage = mSysInfo1.item("GSTPERCENTAGE")
        If IsNumeric(msGSTPercentage) Then mCurGSTPercentage = CDec(msGSTPercentage)

        msQuoteChassisCat1 = mSysInfo1.item("QUOTECHASSISCAT1")
        msQuoteChassisCat2 = mSysInfo1.item("QUOTECHASSISCAT2")
        msItemBarcodeFontName = mSysInfo1.item("ITEMBARCODEFONTNAME")
        s1 = mSysInfo1.item("ITEMBARCODEFONTSIZE")
        If IsNumeric(s1) Then
            L1 = CInt(s1)
            If (L1 > 3) And (L1 < 36) Then
                mlItemBarcodeFontSize = L1
            End If
        End If
        s1 = mSysInfo1.item("STOCKSERVICECHARGECAT1")
        If (s1 <> "") Then
            msServiceChargeCat1 = s1
            msServiceChargeCat2 = mSysInfo1.item("STOCKSERVICECHARGECAT2")
        End If
        '--  in case new priority rates not defined..--
        '==3311.331= If (mCurLabourHourlyRateP1 = 0) And (curOldLabourRate > 0) Then '--3-tier not not defined.-
        '==3311.331= mCurLabourHourlyRateP1 = curOldLabourRate
        '==3311.331= mCurLabourHourlyRateP2 = curOldLabourRate
        '==3311.331= mCurLabourHourlyRateP3 = curOldLabourRate
        '==3311.331= End If

        '--Label stuff.-
        If mSysInfo1.exists("JOBLABELPRINTDEPTH") Then
        End If
        If mSysInfo1.exists("JOBLABELGAPDEPTH") Then
            '===s1 = mSysInfo1.Item("JOBLABELGAPDEPTH")
            '===If IsNumeric(s1) Then msJobLabelGapDepth = s1
        End If
        If mSysInfo1.exists("RETAILHOSTNAME") Then
            msRetailHostname = mSysInfo1.item("RETAILHOSTNAME")
        End If
        '== msOriginalJobMatixDBName -
        '=3103.305==
        If mSysInfo1.exists("ORIGINALJOBMATIXDBNAME") Then
            msOriginalJobMatixDBName = mSysInfo1.item("ORIGINALJOBMATIXDBNAME")
        End If

        mbLoadAllSystemInfo = True
        '== End If '--load..-

        '-- 3107.707= Check quoteChassis values..
        sMsg = "PLease Note: " & vbCrLf & vbCrLf & _
               "The JobMatix setup values for 'QuoteChassisCat1' and  'QuoteChassisCat2'" & vbCrLf & _
                " are incomplete.  These values set the base (foundation) component " & vbCrLf & _
                " on which a complete system can be built." & vbCrLf & vbCrLf & _
                    "Default values of 'MOTHER' (Cat1) and 'SK' [****] (Cat2) will be used.."
        If (msQuoteChassisCat1 = "") Or (msQuoteChassisCat2 = "") Then
            '==ToolTip1.SetToolTip(cmdRestoreChassisDefs, sMsg)
            msQuoteChassisCat1 = "MOTHER"
            msQuoteChassisCat2 = "SK"
            If mbStartingUp Then MsgBox(sMsg)
        End If '--cat1-

    End Function '-LoadAllSystemInfo-
	'= = = = = = =  =  = =
    '-===FF->

    '-- Update one setting..--
    '----change the setting in the static var dictionary..--
    '----- Write the dictionary back to disk..--

    Private Function mbSaveSetting(ByVal sKey As String, ByVal sValue As String) As Boolean
        '=3311.225= Dim asKeys As ICollection
        '=3311.225= Dim sKey1 As String
        '=3311.225= Dim sPath As String
        '=3311.225= Dim sNewFileData As String
        '=3311.225= Dim ix, lResult As Integer

        If Not mLocalSettings1.SaveSetting(sKey, sValue) Then

        End If
        '=3311.225= '--if key exists..  remove it..--
        '=3311.225= sNewFileData = ""
        '=3311.225= sPath = gsLocalJobsSettingsPath()  '=msAppPath & K_SAVESETTINGSPATH
        '=3311.225= If mSdSettings.Exists(UCase(sKey)) Then mSdSettings.Remove((UCase(sKey)))
        '=3311.225= '-- add key and new value..--
        '=3311.225= mSdSettings.Add(UCase(sKey), sValue)
        '=3311.225= '--make string of key=value cr/lf key=value crlf etc --
        '=3311.225= '--- over write file with new string of all settings..--
        '=3311.225= If mSdSettings.Count > 0 Then
        '=3311.225= asKeys = mSdSettings.Keys
        '=3311.225= For Each sKey1 In asKeys
        '=3311.225= sNewFileData = sNewFileData & sKey1 & "=" + mSdSettings.Item(sKey1).ToString + vbCrLf
        '=3311.225= Next
        '=3311.225= lResult = glSaveTextFile(sPath, sNewFileData) '-- As Long
        '=3311.225= If lResult <> 0 Then
        '=3311.225=   MsgBox("Failed to save: " & sPath & vbCrLf, MsgBoxStyle.Exclamation)
        '=3311.225= End If
        '=3311.225= End If '--count..--

    End Function '--save setting.--
    '- - - -  - - - -  --
    '-===FF->

    '-- DO NOT SHOW dialogue..
    '-- DO NOT SHOW dialogue..
    '-- Show msg and record DO NOT SHOW result..-

    Private Function mbDoNotShowMsgDialogue(ByVal sShowMsgKey As String, _
                                            ByVal sSubHdr As String, _
                                            ByVal sMsgText As String, _
                                            Optional ByVal bIsRichText As Boolean = False, _
                                            Optional ByVal bHideDoNotShowCheckbox As Boolean = False, _
                                            Optional ByVal bCanViewWhatsNew As Boolean = False, _
                                            Optional ByVal sMainTitle As String = "JobMatix Message") As Boolean
        Dim bShow As Boolean = False
        Dim dlgDoNotShow1 As dlgNoShow
        Dim s1 As String

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        mbDoNotShowMsgDialogue = False   '--wasn't shown--
        bShow = True
        If Not bHideDoNotShowCheckbox Then
            If mLocalSettings1.queryLocalSetting(sShowMsgKey, s1) Then '= mSdSettings.Exists(sShowMsgKey) Then
                '= s1 = UCase(mLocalSettings1.Item(sShowMsgKey))  '==  "DONOTSHOW_NEWUSERINFO1"-
                If UCase(VB.Left(s1, 1)) = "Y" Then
                    bShow = False
                End If  '-y-
            End If  '-query-
        End If
        If bShow Then
            mbDoNotShowMsgDialogue = True    '--was shown--
            dlgDoNotShow1 = New dlgNoShow
            dlgDoNotShow1.BackColor = Color.WhiteSmoke
            dlgDoNotShow1.MainTitle = sMainTitle
            dlgDoNotShow1.SubHdr = sSubHdr
            dlgDoNotShow1.isRichText = bIsRichText
            dlgDoNotShow1.Message = sMsgText
            dlgDoNotShow1.HideDoNotShowControl = bHideDoNotShowCheckbox
            dlgDoNotShow1.CanViewWhatsNew = bCanViewWhatsNew
            dlgDoNotShow1.ShowDialog()
            If Not bHideDoNotShowCheckbox Then
                If dlgDoNotShow1.DoNotShow = True Then
                    '--don't show it again.-
                    Call mbSaveSetting(sShowMsgKey, "Y")
                End If
            End If
            dlgDoNotShow1.Close()
            dlgDoNotShow1.Dispose()
        End If  '--show..-
        mlStaffTimeout = 0 '--now timing out..--
    End Function  '--mbDoNotShowMsgDialogue--
    '= = = = = = = = == = = == = = = = = 
    '= = = = = = = = = =
    '-===FF->

    '-- S h o w S q l I n f o --

    Private Function mbShowSqlInfo() As Boolean
        Dim kx As Integer

        Call mbSaveSetting("SQLSERVER", msServer)
        If Not gbIsSqlAdmin() Then
            Call mbSaveSetting("SQLUSERNAME", msSqlUid)
            Call mbSaveSetting("SQLUSERPWD", msSqlPwd)
        End If '--sa--
        '--- Separate the "SQL-Server\InstanceName" bits-- if needed..
        If (msServer = "\") Then
            msSqlServerComputer = msComputerName
        Else
            kx = InStr(msServer, "\")
            If (kx > 0) Then '-have instance..--
                msSqlServerComputer = VB.Left(msServer, kx - 1)
                msSqlServerInstance = Mid(msServer, kx + 1)
                If msSqlServerComputer = "" Then '--local instance..
                    msSqlServerComputer = msComputerName
                End If
            Else '--default instance,..-
                msSqlServerComputer = msServer '--no instance name included..-
            End If
        End If '--ni name-
        '== labSqlServerVersion.Text = "Sql Server  v: " & gsSqlVersion()
        '== LabServer.Text = msServer & " (DB::" & msSqlDbName & ")."
        '== labSqlLogin.Text = msSqlServerComputer & "\" & msCurrentUserName
        msCurrentUserNT = msSqlServerComputer & "\" & msCurrentUserName
        _toolbarJobView_ButtonBackup.ToolTipText = "Backup the JobMatix Database:" & _
                                             vbCrLf & msServer & "-" & vbCrLf & _
                                             "DB: " & gbIntJobMatixDBid() & " [" & msSqlDbName & "].."
    End Function '--mbShowSqlInfo-
    '= = = = = = = = = = = = = = 
    '= = = = = = = = = =
    '-===FF->

    '= 3107.902 ==
    '-- SQL C o n n e c t--
    '--   Worker Thread..

    Private Sub BackgroundWorkerSqlConnect_DoWork(sender As Object, _
                                                   ev As DoWorkEventArgs) _
                                                Handles BackgroundWorkerSqlConnect.DoWork
        '-- Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

         ' RunWorkerCompleted eventhandler.
        '== mbSqlServerSearchOk = gbSQL_Enumerate_Main(mColSQLServerInstances)
        mbSqlServerConnectOK = gbConnectSql(mCnnSql, msSqlConnect)

    End Sub '=SqlConnect_DoWork=
    '= = = = = = =  = = = = = = = =

    '-- SQL C o n n e c t--
    '-- SQL C o n n e c t--

    Private Function mbSqlConnect(ByVal sServer As String) As Boolean
        Dim sConnect As String

        mbSqlConnect = False
        sConnect = "Provider=SQLOLEDB; Server=" & msServer & _
                   "; Trusted_Connection=true; Integrated Security=SSPI; ConnectionTimeout=10; "
        msSqlConnect = sConnect  '= pass to async event..-

        Call mWaitFormOn("Connecting to Sql Server: " & vbCrLf & msServer & "..")
        mFormWait1.BackColor = Color.LavenderBlush

        mbSqlServerConnectOK = False
        '- Start BG worker- to CONNECT-
        '-- Start the Sql Search operation in the background.
        Me.BackgroundWorkerSqlConnect.RunWorkerAsync()
        '-- wait for completion-
        '-- ie Wait for the BackgroundWorker to finish the Search.
        While Me.BackgroundWorkerSqlConnect.IsBusy
            '- Keep UI messages moving, so the WAIT form remains 
            '-     responsive during the asynchronous operation.
            Application.DoEvents()
        End While
        Call mWaitFormOff()
        Application.DoEvents()

        If mbSqlServerConnectOK Then  '== gbConnectSql(mCnnSql, sConnect) Then
            '= Call mWaitFormOff()
            mbSqlConnect = True
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '= Call mWaitFormOff()
            If MsgBox("Login to Sql-Server '" & msServer & "' has failed." & vbCrLf & _
                   "Error text:" & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf & _
              "Check that your Windows Logon has been added to SQL-Server logins.." & vbCrLf & vbCrLf & _
                "Do you want to retry ?", _
                     MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) <> MsgBoxResult.Yes Then
                '=3107.902= Me.Close()
                Exit Function
            End If
            '-- go around again..  UNTIL cancelled or ok..
        End If '--connected-

    End Function '--mbSqlConnect-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--  RE-Connect to SQL Server.--
    Private Function mbReConnectSqlServer() As Boolean
        Dim bLoggedIn As Boolean
        Dim sServer As String
        Dim L1 As Integer
        Dim sErrorMsg As String

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        sServer = msServer
        '==3083== txtStaffName.Enabled = False
        If (Not (mCnnSql Is Nothing)) AndAlso ((mCnnSql.State And 1) <> 0) Then '--open-
            mCnnSql.Close()
        End If
        labStatus.Text = "Logging in to Sql Server:" & vbCrLf & "'" & sServer & "'.."
        bLoggedIn = mbSqlConnect(msServer)  '- gbSqlServerLogin(mCnnSql, sServer, True, labStatus)
        If Not bLoggedIn Then
            MsgBox("No Sql Server connection.." & vbCrLf & _
                    "JobMatix is closing.", MsgBoxStyle.Exclamation)
            '== frmSplash1.Close()
            Me.Close()
            mlStaffTimeout = 0 '--NOW timing out..--
            Exit Function '-- End
        End If
        '-- re-connected..
        labStatus.Text = vbCrLf & "Server: " & msServer & vbCrLf & "   re-connected ok.."
        msServer = sServer
        mbSqlConnectionFailure = False
        If Not gbExecuteCmd(mCnnSql, "USE " & msSqlDbName & vbCrLf, L1, sErrorMsg) Then
            MsgBox(vbCrLf & "Failed USE for DATABASE: " & vbCrLf & _
            "'" & msSqlDbName & "' " & vbCrLf & sErrorMsg & vbCrLf & vbCrLf & _
                    "Jobmatix must be re-started..", MsgBoxStyle.Critical)
            Me.Close()
            mlStaffTimeout = 0 '--NOW timing out..--
            Exit Function '-- End
        End If  '--USE-
        If Not gbExecuteCmd(mCnnSql, "SET LOCK_TIMEOUT 15000" & vbCrLf, L1, sErrorMsg) Then _
                                     MsgBox(vbCrLf & "Failed SQL SET TIMEOUT: '" & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        Call mbShowSqlInfo()
        '== txtStaffName.Enabled = True
        mlStaffTimeout = 0 '--now timing out..--
        Call mbStaffSignOn()

    End Function '--ReConnectSql-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-3519.0108-  Special whoUsing for including program name..

    '--  Who Using this DB.. --
    '--  Who Using this DB.. --
    '-- call sp_who to get list of current users of DBname --
    '-- Rev-2912.. 21-Jul-2011==  ONLY worry about tasks that are "runnable"..-
    '-- Rev-3069.. 16-Oct-2011==  Worry about all tasks that are NOT "dormant"..-
    '--  ALSO-=3071=  
    '==     For SQL Server 2005 and later..  Use "sys.sysprocesses" system view..
    '==
    '--  NEW--=3103.0203=  
    '==     For SQL Server 2008 and later ONLY..  Use "sys.sysprocesses" system view..
    '==   NB: SQL Server 2005 with oledb has problem with "sys.sysprocesses" system view..
    '==          ("Protocol Error in TDS data stream" )
    '== = =

    '-3519.0108-  Special LOCAL Who-Using version -
    '-- for including program name in returned collection..
    '==  NOTE- THIS Means that SqlServer2005 will not be returnibg full info..
    '-  NB sys.sysprocesses still works in Sql Server 2017..

    Public Function mbWhoUsingEx(ByRef cnn1 As OleDbConnection, _
                              ByVal sDBName As String, _
                               ByRef colWhichUsers As Collection) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim sSql, s1, sErrors As String
        Dim sLoginName, sHostName, sStatus As String
        Dim rs1 As OleDbDataReader  '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim colUser As Collection

        '= msLastSqlErrorMessage = ""
        '--USE--
        '== s1 = " USE master " '-- + sDBName
        '==3069=  If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then MsgBox("Failed sql: " & s1)
        mbWhoUsingEx = False
        If gbIsSqlServer2008Plus() Then  '=If gbIsSqlServer2005Plus() Then
            If gbIntJobMatixDBid() <= 0 Then Exit Function '-- NO DB ID was saved !!-
            sSql = "SELECT loginame, dbid, hostname, nt_domain, status, program_name " & vbCrLf & _
                   "FROM sys.sysprocesses; "
            If Not gbGetReader(cnn1, rs1, sSql) Then  '= gbGetRst(cnn1, rs1, sSql) Then
                s1 = "Failed to retrieve list of users.." & vbCrLf & _
                      "Error msg: " & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf
                '= If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
                MsgBox(s1, MsgBoxStyle.Exclamation)
                '=msLastSqlErrorMessage = s1
            Else
                '--check recordset--
                If Not (rs1 Is Nothing) Then
                    colWhichUsers = New Collection
                    If rs1.HasRows Then  '== Not (rs1.BOF And rs1.EOF) Then  '--not empty-
                        '== If rs1.BOF Then rs1.MoveFirst()
                        While rs1.Read  '= Not rs1.EOF
                            sStatus = ""
                            If (Not IsDBNull(rs1.Item("status"))) Then
                                sStatus = CStr(rs1.Item("status"))
                            End If
                            If (Not IsDBNull(rs1.Item("loginame"))) And (Not IsDBNull(rs1.Item("dbid"))) Then
                                '== colUser = New Collection '--each user is collection..-
                                sLoginName = CStr(rs1.Item("loginame"))
                                If (CInt(rs1.Item("dbid")) = gbIntJobMatixDBid()) And _
                                                                        (sStatus <> "dormant") Then  '--our DB--
                                    colUser = New Collection '--each user is collection..-
                                    colUser.Add(sLoginName, "LOGINAME")
                                    If Not IsDBNull(rs1.Item("hostname")) Then
                                        colUser.Add(CStr(rs1.Item("hostname")), "HOSTNAME")
                                    Else
                                        colUser.Add("", "HOSTNAME")
                                    End If '--null-
                                    '=3519.0108= GET program name also..
                                    If Not IsDBNull(rs1.Item("program_name")) Then
                                        colUser.Add(CStr(rs1.Item("program_name")), "PROGRAM_NAME")
                                    Else
                                        colUser.Add("", "PROGRAM_NAME")
                                    End If '--null-
                                    colWhichUsers.Add(colUser)
                                End If
                            End If  '--null login.-
                            '== rs1.MoveNext()
                        End While  '-read-
                    End If  '--empty- 
                    rs1.Close()
                    mbWhoUsingEx = True
                End If  '--nothing..-
            End If  '--get rset-
        Else  '-sql server 20002005 --
            lResult = glExecSP(cnn1, "sp_who", "", sErrors, rs1)
            If lResult <> 0 Then
                s1 = "Failed to retrieve list of users.." & vbCrLf & _
                       "Error msg: " & vbCrLf & sErrors & vbCrLf
                '= If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
                MsgBox(s1, MsgBoxStyle.Exclamation)
                '= msLastSqlErrorMessage = s1
                '==MsgBox(sErrors, MsgBoxStyle.Critical)
            Else '--  check rs1 for users.--
                '--check recordset--
                If Not (rs1 Is Nothing) Then
                    colWhichUsers = New Collection
                    If rs1.HasRows Then  '= Not (rs1.BOF And rs1.EOF) Then  '--not empty-
                        '= If rs1.BOF Then rs1.MoveFirst()
                        While rs1.Read  '== Not rs1.EOF
                            If Not IsDBNull(rs1.Item("dbname")) Then
                                '=3069.0= If (UCase(rs1.Fields("dbname").Value) = UCase(sDBName)) And _
                                '=3069.0=               (LCase(rs1.Fields("status").Value) = "runnable") Then '-- this one is using....--
                                If (UCase(rs1.Item("dbname")) = UCase(sDBName)) And _
                                           (LCase(rs1.Item("status")) <> "dormant") And _
                                                 (LCase(rs1.Item("status")) <> "background") Then '-- this one is using..--
                                    colUser = New Collection '--each user is collection..-
                                    '--MsgBox "Found user: " + rs1("LoginName")
                                    colUser.Add(CStr(rs1.Item("loginame")), "LOGINAME")
                                    If Not IsDBNull(rs1.Item("hostname")) Then
                                        colUser.Add(CStr(rs1.Item("hostname")), "HOSTNAME")
                                    Else
                                        colUser.Add("", "HOSTNAME")
                                    End If '--null-
                                    colUser.Add("", "PROGRAM_NAME")  '-No prog name returned bu sp_who-
                                    colWhichUsers.Add(colUser)
                                End If '--this db--
                            End If '--isnull-
                            '== rs1.MoveNext()
                        End While
                    End If  '--empty..-
                    rs1.Close()
                    mbWhoUsingEx = True
                End If '--nothing..-
            End If '--exec ok-
        End If  '--sql 2005--
        rs1 = Nothing
        colUser = Nothing
    End Function '--WhoUsingEx..-
    '= = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  get current DISTINCT logged-in users...
    '--   User can have multiple sessions..--
    '--  Uses "sp_who":
    '--    in sql server 2000-  defaults to PUBLIC role..
    '--    in Sql Server 2005 needs VIEW ANY DATABASE permission
    '--      and "The public role is granted VIEW ANY DATABASE permission."
    '--        SEE:   http://msdn.microsoft.com/en-us/library/ms175892(v=sql.90).aspx   --

    '-3519.0108-  Special LOCAL Who-Using version -
    '-- for including program name in returned collection..
    '--  So that we only look at JobTracking users (for licence restrictions.)
    '==  NOTE- THIS Means that SqlServer2005 will not be returning full info..
    '-    NB: USES sys.sysprocesses- still works in Sql Server 2017..


    Private Function mbShowLoggedInUsers(ByRef colWhichUsers As Collection, _
                                          ByRef strUserList As String) As Boolean

        Dim col1 As Collection
        Dim colAllProcesses As Collection
        Dim sLogin, sHost, sItem, sProgram_name As String
        Dim sMsg, sDistinctUsers As String

        mbShowLoggedInUsers = False
        sDistinctUsers = ";"
        strUserList = ""
        '== ToolTip1.SetToolTip(labLoggedInUsers, "")
        If Not mbWhoUsingEx(mCnnSql, msSqlDbName, colAllProcesses) Then
            '= gbWhoUsing(mCnnSql, msSqlDbName, colAllProcesses) Then
            MsgBox("Failed to get user list.." & vbCrLf & _
                    "Sql cmd was 'exec sp_who'..  " & vbCrLf & vbCrLf & _
                    gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
        Else '--ok--
            '= Dim processThis As Process = Process.GetCurrentProcess
            Dim sOurName As String = msCurrentProcessName  '= processThis.ProcessName

            sMsg = "Current JobTracking users are: " & vbCrLf & vbCrLf
            colWhichUsers = New Collection
            If (colAllProcesses.Count > 0) Then
                For Each col1 In colAllProcesses
                    sLogin = Trim(col1.Item("LOGINAME"))
                    sHost = Trim(col1.Item("HOSTNAME"))
                    sProgram_name = Trim(col1.Item("program_name"))
                    '- check if this is a jobTracking program user.
                    If LCase(sProgram_name) = LCase(sOurName) Then
                        '-- IS Job Tracking User
                        sItem = LCase(sHost & "!" & sLogin)
                        If Not (InStr(sDistinctUsers, sItem & ";") > 0) Then  '--new-
                            sDistinctUsers = sDistinctUsers & LCase(sItem) & ";"
                            sMsg = sMsg & sLogin & " on: " & sHost & ".." & vbCrLf
                            colWhichUsers.Add(col1)
                        End If
                    End If  '-our name-
                Next col1 '--col1-
                '== labLoggedInUsers.Text = "Who's using JobMatix-" & vbCrLf & _
                '==                                colWhichUsers.Count & " user(s) logged in.."
                '== ToolTip1.SetToolTip(labLoggedInUsers, sMsg)
                strUserList = sMsg
            Else
                '== labLoggedInUsers.Text = vbCrLf & "No User.."
            End If  '--count.-
            Application.DoEvents()
            mbShowLoggedInUsers = True
        End If  '--who--
    End Function  '--show users.-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- Check LoggedIn Users --
    Private Function mbCheckLoggedInUsers() As Boolean
        Dim colWhichUsers As Collection
        Dim sMsg, s1, s2 As String
        Dim sUserList As String = ""
        Dim col1 As Collection

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        mbCheckLoggedInUsers = False
        '-  Check who's logged in..
        If mbShowLoggedInUsers(colWhichUsers, sUserList) Then
            '-- if ThreeUser Licence, then report and retry/cancel.
            '==3072/3== If mbIsThreeUserLicence Then
            If (colWhichUsers.Count > mIntMaxUsersPermitted) Then   '- (was 3)-our login is included in count..--
                sMsg = "Current logins are (incl. this one): " & vbCrLf & vbCrLf
                For Each col1 In colWhichUsers
                    s1 = Trim(col1.Item("LOGINAME"))
                    s2 = Trim(col1.Item("HOSTNAME"))
                    sMsg = sMsg & s1 & " on: " & s2 & ".." & vbCrLf
                Next col1
                While (colWhichUsers.Count > mIntMaxUsersPermitted)
                    If (MsgBox("This JobMatix Licence supports only " & mIntMaxUsersPermitted & _
                             " concurrent user(s) .." & vbCrLf & vbCrLf & sMsg & vbCrLf & vbCrLf & _
                             "To continue, close one of the listed users, and retry.." & vbCrLf & _
                               "Do you want to retry?", _
                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                    Else  '--no-
                        Me.Close()
                        Exit Function   '=End
                    End If
                    Call mbShowLoggedInUsers(colWhichUsers, sUserList)
                End While
            Else  '-ok-
                mbCheckLoggedInUsers = True
            End If  '--check max-
            '==3072/3== End If  '--three-
        Else  '--show failed-
        End If
        mlStaffTimeout = 0 '--now timing out..--
    End Function  '--check users..-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- CheckLicenceKey --
    '-- CheckLicenceKey --

    'Private Function mbCheckLicenceKey(ByVal sTestKey As String, _
    '                                   ByVal sComputedPreciseShortKey As String, _
    '                                          ByVal sComputedKeyUnlimited As String, _
    '                                          ByVal sComputedKeyLevel2 As String, _
    '                                        ByVal sComputedKeyThreeUser As String, _
    '                                           ByVal sComputedKeyTwoUser As String, _
    '                                         ByVal sComputedKeySingleUser As String, _
    '                                              ByRef bIsLevel2Licence As Boolean, _
    '                                             ByRef intMaxUsersLicenced As Integer) As Boolean
    '    Dim intNoUsers As Integer
    '    Dim bLicenceOk As Boolean = False

    '    mbCheckLicenceKey = False
    '    bIsLevel2Licence = False
    '    intNoUsers = K_ABSOLUTE_MAX_USERS_PERMITTED

    '    Select Case UCase(sTestKey)
    '        Case sComputedPreciseShortKey
    '            bLicenceOk = True
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
    '    mbCheckLicenceKey = bLicenceOk
    '    intMaxUsersLicenced = intNoUsers

    'End Function  '--check key-
    '= = = = = = = = =  = =
    '-===FF->

    '-- disable menus..-

    Private Function mbDisableMenus() As Boolean
        Dim ix As Short

        mnuFile.Enabled = False
        '===mnuQuotes.Enabled = False
        mnuReference.Enabled = False
        '= mnuReports.Enabled = False
        mnuDatabase.Enabled = False
        mnuAdmin.Enabled = False

        mnuParts.Enabled = False
        mnuSerialAudit.Enabled = False

        mnuCustomers.Enabled = False
        mnuJobs.Enabled = False

        mnuAbout.Enabled = False

        '== mnuCreateQuoteJobs.Enabled = False
        '=3101= _toolbarJobView_ButtonQuotes.Enabled = False

        '=3411.1221= POS Tab GONE==
        '=3411.1221= POS Tab GONE==

        '=3411.0226.= 
        TabControlMain.Enabled = False

        '==3083== frameSignOn.Visible = True
        '==3083== frameSignOn.Invalidate()

        Picture1.Invalidate()    '--force repinting logo..--

        Me.Update()   '--repaint --
        Application.DoEvents()

    End Function '--disable..-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- Enable menus..-

    Private Function mbEnableMenus() As Boolean
        Dim ix As Short

        mnuFile.Enabled = True
        mnuReference.Enabled = True
        '= mnuReports.Enabled = True
        mnuDatabase.Enabled = True
        mnuAdmin.Enabled = True
        mnuParts.Enabled = True
        mnuJobs.Enabled = True
        mnuCustomers.Enabled = True
        '==3073= If mbLicenceOK Or (mlDatabaseDays <= 60) Then mnuJobs.Enabled = True

        mnuSerialAudit.Enabled = False
        mnuAbout.Enabled = True

        '=3101= _toolbarJobView_ButtonQuotes.Enabled = False
        frameQuotes.Enabled = False
        frameQuoteDetails.Enabled = False
 
        If (mbIsRetailManager() Or mbIsJobmatixPOS()) Then mnuSerialAudit.Enabled = True
        If (mbIsRetailManager() Or mbIsJobmatixPOS()) And mbIsFullLicence Then
            '=3101= _toolbarJobView_ButtonQuotes.Enabled = True
            '== _toolbarJobView_ButtonRAs.Enabled = True
            frameQuotes.Enabled = True
            frameQuoteDetails.Enabled = True
        End If

        If Not mbV13Only Then '--V2 ok..-
            '==mnuQuotes.Enabled = True
            If (mbIsRetailManager() Or mbIsJobmatixPOS()) And mbIsFullLicence Then
                '==3311.405= _toolbarJobView_ButtonRAs.Enabled = True
                '=3311.227= mnuRAS.Enabled = True
                If mbIsJobmatixPOS() Then
                    '== cmdPOS.Enabled = True
                    '=3411.1221= POS Tab GONE==
                    '= grpboxSale.Enabled = True
                End If
            End If
            '=== txtRASearch.Text = ""
        End If '-v2-
        txtSearch.Text = ""

        '--  TEMP  enable all buttone anyway..--
        '=If mbLicenceOK Or (mlDatabaseDays <= 60) Then
        '= frameMainCmds.Enabled = True
        '= toolbarNewJob2.Enabled = True
        '==3311.405= cmdLaunchRAs.Enabled = True      '=3103.423=

        TabControlMain.Enabled = True

        If (mbIsRetailManager() Or mbIsJobmatixPOS()) And mbIsFullLicence Then
            cmdBuildQuote.Enabled = True
            '== cmdLaunchRAs.Enabled = True
        End If
        '=End If
        frameJobsTab.Visible = True

    End Function '--enable..-
    '= = = = = = = = = = = = =
    '-===FF->

    '--Set Grid Font Size -

    Private Function mbSetGridFontSize(ByVal intNewSize As Integer) As Boolean

        '--clear all checked..-
        mnuGridFont_8.Checked = False
        mnuGridFont_9.Checked = False
        mnuGridFont_10.Checked = False

        Select Case intNewSize
            Case 8
                mnuGridFont_8.Checked = True
                tvwJobs.Font = New Font(mFontTvwJobs.FontFamily, 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                mSingleNewFontSize = 8.25
                rtfTemplateJobs = rtfTemplateJobs_8
            Case 9
                mnuGridFont_9.Checked = True
                tvwJobs.Font = New Font(mFontTvwJobs.FontFamily, 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                '== mFontTvwJobs = mFontVerdana9pt
                mSingleNewFontSize = 9.0
                rtfTemplateJobs = rtfTemplateJobs_9
            Case 10
                mnuGridFont_10.Checked = True
                '= mFontTvwJobs = mFontVerdana10pt
                tvwJobs.Font = New Font(mFontTvwJobs.FontFamily, 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                mSingleNewFontSize = 9.75
                rtfTemplateJobs = rtfTemplateJobs_10
            Case Else
        End Select
        '--set dataGridView fonts..
        mDataGridViewCellStyleData.Font = _
                       New Font(mFontTvwJobs.FontFamily, mSingleNewFontSize, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ListViewSalesOrders.Font = _
                      New Font(mFontTvwJobs.FontFamily, mSingleNewFontSize, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ListViewQuote.Font = _
                      New Font(mFontTvwJobs.FontFamily, mSingleNewFontSize, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))

        If (mlJobId > 0) Then Call mbShowJobInfoRTF(mlJobId)

        System.Windows.Forms.Application.DoEvents()
        '==ToolStripLabelTextSize.Text = CStr(intNewSize)
        ToolStripDropDownGridFont.ToolTipText = "Grid Font size (" & CStr(intNewSize) & "pt)"
    End Function  '--SetGridFontSize--
    '= = = = = = = = = = = = =
    '-===FF->

    '--hide notify reminder..-

    Private Function mbHideNotifyReminder() As Boolean

        panelReminder.Visible = False
        tvwJobs.Top = 50  '==(labJobsExplorer.Top + labJobsExplorer.Height) '== FrameJobsTree2.Top
        tvwJobs.Height = FrameJobsTree2.Height - labJobsExplorer.Height - 34 '==   - 240

    End Function  '--hide--
    '= = = = = = = = = = = = =

    '--SHOW notify reminder..-
    Private mColorReminderPanel As Color

    '==3311.402=  include any ONSITE jobs for signed-on tech..

    Private Function mbShowNotifyReminder() As Boolean
        Dim s1, sSql As String
        Dim vResult As Object
        Dim intCountToNotify, intCountOnSite As Integer

        intCountToNotify = 0
        intCountOnSite = 0
        If Not panelReminder.Visible Then Exit Function
        sSql = "SELECT count (*) from [Jobs] "
        sSql &= " WHERE (LEFT(JobStatus,2)='50') AND (LOWER(Notifications) NOT LIKE '%notified ok%');"
        '== sSql &= " WHERE (LEFT(JobStatus,2)='50') AND NOT CONTAINS(LOWER(Notifications),'notified ok');"
        If mbGetSelectValue(sSql, vResult) Then
            intCountToNotify = CInt(vResult)   '-save-
            '-- FIRST  see if any ONSITE jobs for signed-on.. DatePromised=today.
            sSql = "SELECT count (*) from [Jobs] "
            sSql &= " WHERE (LEFT(JobStatus,2)<='10') " '-not started.-
            sSql &= "    AND (NominatedTech LIKE '%" & msStaffName & "%') "
            sSql &= "    AND (UPPER(GoodsInCare) = '" & K_GOODS_ONSITEJOB & "') "
            sSql &= "    AND (DATEDIFF(day,CURRENT_TIMESTAMP,DatePromised)=0) "  '-today-
            If Not mbGetSelectValue(sSql, vResult) Then
                Call mbHideNotifyReminder()  '-failed-
            Else  '-ok-
                intCountOnSite = CInt(vResult)   '-save-
                If (intCountOnSite > 0) Then  '--have onsite today..
                    panelReminder.BackColor = Color.Goldenrod
                    labReminder.Visible = False
                    labOnSiteReminder.Visible = True
                    labOnSiteReminder.Text = "Hey " & msStaffName & "- You have " & intCountOnSite & _
                                          " ON-SITE Job(s) for today. Check OnSite panel.."
                Else  '-no onsite-
                    If (intCountToNotify > 0) Then
                        panelReminder.BackColor = mColorReminderPanel '--was designed colour.
                        labOnSiteReminder.Visible = False
                        labReminder.Visible = True
                        labReminder.Text = "Note: There are " & intCountToNotify & _
                                              " completed Jobs to be notified to Customers.."
                        If (intCountToNotify = 1) Then
                            labReminder.Text = "Note: There is " & intCountToNotify & _
                                                  " completed Job to be notified to Customer.."
                        End If
                    Else
                        Call mbHideNotifyReminder()
                    End If '=intCountToNotify-
                End If  '-intCountOnSite-
            End If '-- get onsite-
        Else  '- get failed-
            '--hide notify reminder..-
            Call mbHideNotifyReminder()
        End If  '--get select Notify..-
        '-- fix size and pos..
        If (intCountOnSite > 0) Or (intCountToNotify > 0) Then  '--have something to show..
            panelReminder.Visible = True
            tvwJobs.Top = panelReminder.Top + panelReminder.Height + 3
            tvwJobs.Height = FrameJobsTree2.Height - labJobsExplorer.Height - 34 - panelReminder.Height
        End If
    End Function  '-show--
    '= = = = = = = = = = = = =
    '-===FF->

    '-- S t a f f S i g n O n  --
    '-- 3411.0226=
    '--  Now is local here on Main Form.
    '-- NOW is in TWO parts.. SignOff/SignOn.

    Private mbStaffIsSignedOn As Boolean = False

    Private Function mbStaffSignOn() As Boolean
        Dim colWhichUsers As Collection
        '== Dim dlgDoNotShow1 As dlgNoShow
        Dim bShow As Boolean = False
        Dim bDone As Boolean = False
        Dim s1, sHdr, sSql As String
        Dim sShowMsgKey, sMsgText As String   '==  = "DONOTSHOW_NEWUSERINFO1"
        '== Dim frmSignOn As New frmStaffSignOn
        Dim vResult As Object

        mlStaffTimeout = -1 '--to sign off--
        Timer1.Stop()   '-stop all of it..

        cmdSignoff.Enabled = False
        '==3083=  Me.BackColor = System.Drawing.ColorTranslator.FromOle(&HE4E4E4)
        mlStaffId = -1
        msStaffName = ""
        msStaffBarcode = ""
        LabStaffName2.Text = ""
        labStaffTimeRemaining.Text = ""  '=3411.0302=

        '=3411.0226-  This back.
        Call mbDisableMenus() '--general menus..-
        '= picLogoPOS.Enabled = False
        btnLaunchRAs.Enabled = False

        txtCustSearch.Text = ""
        txtSearch.Text = ""
        '== labStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&H808080) '---  &H800000      '--vbBlue
        labStatus.BackColor = Color.Transparent
        '== labStatus.ForeColor = System.Drawing.Color.White
        labStatus.Font = VB6.FontChangeBold(labStatus.Font, True)
        labStatus.ForeColor = Color.DarkBlue
        '== labStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter '--centre..-
        labStatus.Text = msSignOnMsg
        '==If Not mbSqlConnectionFailure Then Call mbShowLoggedInUsers(colWhichUsers)
        If Not mbSqlConnectionFailure Then
            If Not mbCheckLoggedInUsers() Then
                Me.Close()
                Exit Function '=End
            End If
        End If
        '- Set up Staff Barcode and Wait for ENTER/verify..
        cmdSignoff.Text = ""
        '=mbStaffIsSignedOn = False
        LabStaffName2.Text = "-SignOn-"

        txtStaffBarcode.Text = "Barcode"
        panelSignOn.BackColor = Color.LavenderBlush
        txtStaffBarcode.Enabled = True

        txtStaffBarcode.SelectionStart = 0
        txtStaffBarcode.SelectionLength = Len(txtStaffBarcode.Text)

        txtStaffBarcode.Focus()
        '= txtStaffBarcode.Select()  '== !!  .Select() doesn't workk..
        '-Wait for staff to sign-on.. 
 
    End Function '- mbStaffSignOn-
    '= = = = = = = = = = = = = === = =
    '-===FF->

    '-- PART II.. when staff signed onn..

    Private Function mbStaffSignOnCompletion() As Boolean
        Dim colWhichUsers As Collection
        '== Dim dlgDoNotShow1 As dlgNoShow
        Dim bShow As Boolean = False
        Dim bDone As Boolean = False
        Dim s1, sHdr, sSql As String
        Dim sShowMsgKey, sMsgText As String   '==  = "DONOTSHOW_NEWUSERINFO1"
        '== Dim frmSignOn As New frmStaffSignOn
        Dim vResult As Object

        '-- First- Sign Off..  Then wait for sign On..

        'mlStaffTimeout = -1 '--to sign off--
        'cmdSignoff.Enabled = False
        '    '==3083=  Me.BackColor = System.Drawing.ColorTranslator.FromOle(&HE4E4E4)
        'mlStaffId = -1
        'msStaffName = ""
        'msStaffBarcode = ""
        'LabStaffName2.Text = ""

        ''=3411.0226-  This back.
        'Call mbDisableMenus() '--general menus..-
        'txtCustSearch.Text = ""
        'txtSearch.Text = ""
        ''== labStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&H808080) '---  &H800000      '--vbBlue
        'labStatus.BackColor = Color.Transparent
        ''== labStatus.ForeColor = System.Drawing.Color.White
        'labStatus.Font = VB6.FontChangeBold(labStatus.Font, True)
        'labStatus.ForeColor = Color.DarkBlue
        ''== labStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter '--centre..-
        'labStatus.Text = msSignOnMsg
        ''==If Not mbSqlConnectionFailure Then Call mbShowLoggedInUsers(colWhichUsers)
        'If Not mbSqlConnectionFailure Then
        '    If Not mbCheckLoggedInUsers() Then
        '        Me.Close()
        '        End
        '    End If
        'End If

        '    '- Set up Staff Barcode and Wait for ENTER/verify..
        '    '=mbStaffIsSignedOn = False
        'txtStaffBarcode.Text = ""
        'panelSignOn.BackColor = Color.LavenderBlush
        'txtStaffBarcode.Select()
        '    '-Wait for staff to sign-on.. 
        'Exit Function


        '-OK- -Signed On.. Complete the work..
        '-OK- -Signed On.. Complete the work..
        '-OK- -Signed On.. Complete the work..


        panelSignOn.BackColor = Color.WhiteSmoke

        'frmSignOn.retailHost = mRetailHost1
        'frmSignOn.SignOnMsg = msSignOnMsg
        'bDone = False
        'Timer1.Stop()  '--no timing out whwn signed off.-
        'While Not bDone
        '    frmSignOn.Cancelled = False
        '    frmSignOn.ShowDialog()
        '    '--  if cancelled, then EXIT..
        '    If frmSignOn.Cancelled Then  '--3083.618- Now is Quit--
        '        Me.Close()
        '        End
        '    Else
        '        bDone = True   '--ok-
        '    End If
        'End While
        ''-- get staff info..-
        'mlStaffId = frmSignOn.Staff_id   '==3101.1226==Boxing day.-
        'msStaffName = frmSignOn.StaffName
        'msStaffBarcode = frmSignOn.StaffBarcode

        '==3101.1226==Boxing day.-
        '--  Update POS dll with new signon.--

        '=3411.1221=  POS stuff GONE to its own EXE..
        'If mbIsJobmatixPOS() Then
        '    Call mClsSale1.staffSignedOn(mlStaffId, msStaffName)
        'End If

        '=3311.311=
        '-- Give clsRAsMain the staff.
        '= 3411.0121-  RAs Gone- to EXE.. 
        '=  mClsRAsMain1.staffSignedOn(mlStaffId, msStaffBarcode, msStaffName)

        '== LabStaffName2.Text = msStaffName

        '-- Continue--
        '-- Continue--
        '-- Continue--

        msStaffName = LabStaffName2.Text
        msStaffBarcode = Trim(txtStaffBarcode.Text)

        Call mbEnableMenus()
        '= picLogoPOS.Enabled = True
        btnLaunchRAs.Enabled = True

        cmdSignoff.Enabled = True
        '===LabEnterStaffId.Visible = False
        '==3083== LabEnterStaffId.Enabled = False
        labStatus.BackColor = Color.Transparent  '== System.Drawing.ColorTranslator.FromOle(&HF0F8F8)
        labStatus.ForeColor = System.Drawing.Color.Black
        labStatus.Font = VB6.FontChangeBold(labStatus.Font, False)
        '== labStatus.Text = msUseBrowserMsg
        labStatus.Text = ""

        msCustomerBarcode = ""   '--must now select a customer..-
        Call mbClearDetailsFrame() '--clear details..-
        '== Call mbInitialiseJobsTreeView(tvwJobs)
        Call mbRefreshJobsTreeView(True)

        txtDetailsHdr.Text = vbCrLf & "A Customer needs to be selected before a New Job can be accepted.."
        '--  click the jobs button..-
        '--  show treeview..
        '==  CType(toolbarJobView.Items.Item("_toolbarJobView_Button1"), ToolStripButton).Checked = True
        'Call toolbarJobView_ButtonClick(CType(toolbarJobView.Items.Item("_toolbarJobView_Button1"), ToolStripButton), _
        '                                                                               New System.EventArgs())
        ''--make sure we register the click..-
        If gbDebug Then MsgBox("Jobs treeview initialised.." & vbCrLf & _
                                 "  Treeview has " & tvwJobs.Nodes.Count & " root nodes.." & vbCrLf & _
                                   "Visible=" & IIf(tvwJobs.Visible = True, "Y", "N"), MsgBoxStyle.Information)
        Call gbLogMsg(gsRuntimeLogPath, "= Staff: " & msStaffName & " has signed on..")

        Application.DoEvents()

        '--  Warning about the need to select customer before NewJob..-
        sShowMsgKey = "DONOTSHOW_USERINFO_WHATSNEW"
        sHdr = ""  '== "JobMatix 3083"
        '== sMsgText = "Note:  Creating new Jobs: " & vbCrLf & vbCrLf & _
        '==        "A Customer has to be selected before clicking on ""New Job"".. " & vbCrLf & vbCrLf & _
        '==        "To select a customer, click on the ""Customers"" Tab on the main screen, " & _
        '==        "and browse the retail customer list.." & vbCrLf & _
        '==        "If the customer is not found, then switch over to Retail Manager to create the new customer; " & _
        '==        " then browse again in JobMatix to select the customer for the job."
        '== Call mbDoNotShowMsgDialogue(sShowMsgKey, sHdr, sMsgText)
        '==3083==
        Call mbDoNotShowMsgDialogue(sShowMsgKey, sHdr, strAboutJobMatix3HTML, _
                                                  True, False, True, "About " & msJobMatixVersion)

        mlStaffTimeout = -1 '--KEEP SUSPENDing timing out..--
        If Not mnuDontShowNotifyReminder.Checked Then  '--can show--
            '==3083.404= show reminder to notify if any..
            '-- count jobs still to notify..-
            panelReminder.Visible = True
            Call mbShowNotifyReminder()
        Else
            '--hide notify reminder..-
            Call mbHideNotifyReminder()
        End If
        cmdSignoff.Text = "Sign Off"
        cmdSignoff.Enabled = True
        Timer1.Start()   '--ok to go-
        mlStaffTimeout = 1 '--NOW is timing out..--
        tvwJobs.Focus()

    End Function '--mbStaffSignon--
    '= = = = = = = = = = = = =
    '-===FF->

    '--  get value of 1st rst item for SELECT..--

    Private Function mbGetSelectValue(ByVal sSql As String, ByRef vResult As Object) As Boolean
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim sErrorMsg As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mbGetSelectValue = False
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                '== If Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                '== rs1.MoveFirst()
                Dim datarow1 As DataRow = rs1.Rows(0)  '--1st row-
                If Not IsDBNull(datarow1.Item(0)) Then
                    vResult = datarow1.Item(0)
                    mbGetSelectValue = True '--got something..-
                End If '--null.-
                '== End If '--bof-
                '= rs1.Close()
            End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSelect..-
    '= = = = = = = = = = = = = =

    '--  get value of 1st rst column (all rows) for SELECT..--

    Private Function mbGetSelectValueList(ByVal sSql As String, _
                                            ByRef colResult As Collection) As Boolean
        Dim rs1 As DataTable  '=  ADODB.Recordset
 
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mbGetSelectValueList = False
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            MsgBox("Failed to get SELECT recordset.." & vbCrLf & "SQL was:" & vbCrLf & sSql, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                '== If Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                colResult = New Collection
                '==rs1.MoveFirst()
                For Each datarow1 As DataRow In rs1.Rows
                    If Not IsDBNull(datarow1.Item(0)) Then
                        colResult.Add(datarow1.Item(0))
                    End If
                Next datarow1
                 mbGetSelectValueList = True '--got something..-
                '== End If '--bof-
                '= rs1.Close()
            End If '--nothing 
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '--getSElectList..-
    '= = = = = = = = = = = = = =
    '-===FF->
    '--  get recordset as collection for SELECT..--
    '--  get recordset as collection for SELECT..--

    Private Function mbGetRecordCollection(ByRef cnnSQL As OleDbConnection, _
                                            ByVal sSql As String, _
                                            ByRef colResult As Collection) As Boolean
        '=v3.1.3101=
        mbGetRecordCollection = gbGetRecordCollection(cnnSQL, sSql, colResult)
    End Function '--getRecordColl..-
    '= = = = = = = = = = = = = = = = = = = =

    '== - 3431.0513-
    '==  Find DEFAULT CONSTRAINT name --  (so we can Increase ServiceNotes width to MAX)..

    Private Function mbFindDefaultConstraintName(ByVal strTablename As String, _
                                                 ByVal strColumnName As String, _
                                                 ByRef sConstraintName As String) As Boolean
        Dim sSql As String
        Dim datatable1 As DataTable

        mbFindDefaultConstraintName = False

        sSql = "select def.object_id , col.name, def.definition, def.name AS constraint_name "
        sSql &= " FROM sys.default_constraints def "
        sSql &= "   inner join sys.columns col "
        sSql &= "    ON (def.parent_object_id = col.object_id) "
        sSql &= " inner join sys.tables st"
        sSql &= "   ON (def.parent_object_id = st.object_id) "
        sSql &= " WHERE  (st.name = '" & strTablename & "') "
        sSql &= "       AND (col.column_id = def.parent_column_id) AND (col.name='" & strColumnName & "') ;"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
            MessageBox.Show("Failed to get CONSTRAINTS recordset.." & vbCrLf & _
                             gsGetLastSqlErrorMessage() & vbCrLf,
                             "Find Default Constraint Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        Else  '- ok-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If datatable1 IsNot Nothing AndAlso (datatable1.Rows.Count > 0) Then
                sConstraintName = datatable1.Rows(0).Item("constraint_name")
                mbFindDefaultConstraintName = True
            End If  '-nothing.
        End If  'get-

    End Function '-mbFindDefaultConstraintName-
    '= = = = = = = = = = = ==== == =  = =
    '-===FF->

    '-- Expand Column With Default  -

    Private Function msExpandColumnWithDefault(ByVal strTablename As String, _
                                                 ByVal strColumnName As String, _
                                                ByVal intNewWidth As Integer) As String
        Dim sSql As String = ""
        Dim sConstraintName As String

        msExpandColumnWithDefault = ""

        '--    first drop the DEFAULT constraint..
        If mbFindDefaultConstraintName(strTablename, strColumnName, sConstraintName) Then
            sSql &= "ALTER TABLE dbo." & strTablename & " DROP " & sConstraintName & " ;" & vbCrLf
        End If
        sSql &= "ALTER TABLE dbo." & strTablename & _
                    " ALTER COLUMN " & strColumnName & " VARCHAR(" & CStr(intNewWidth) & ") NOT NULL ; " & vbCrLf
        '-- add DEFAULT-
        sSql &= "ALTER TABLE dbo." & strTablename & _
                      " ADD CONSTRAINT DF_Jobs_" & strColumnName & " DEFAULT '' FOR " & strColumnName & " ;  " & vbCrLf

        '- return new sql stuff.
        msExpandColumnWithDefault = sSql

    End Function '-msExpandColumnWithDefault-
    '= = = = = = = = = = = ==== == =  = =
    '-===FF->

    '--  Get list of Jobs for given Quote.--

    Private Function mbGetQuoteJobs(ByRef lngOrderId As Integer, ByRef colResults As Collection) As Boolean
        Dim sSql As String
        Dim v1 As Object
        mbGetQuoteJobs = False
        sSql = "SELECT DISTINCT QuotePart_JobId,QuotePart_OrderId, Jobs.JobStatus  "
        sSql = sSql & " FROM [QuoteJobParts] LEFT JOIN [Jobs] ON (Jobs.Job_Id=QuoteJobParts.QuotePart_JobId)  "
        sSql = sSql & " WHERE (QuotePart_OrderId= " & CStr(lngOrderId) & "); "
        If mbGetRecordCollection(mCnnSql, sSql, colResults) Then
            mbGetQuoteJobs = True
        End If '--select..-
    End Function '--get jobs..-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  Get list of JobList for ALL Quotes.--
    '--- SEND back dictionary:  Key-OrderNo,  Data= Joblist.

    Private Function mbMainGetQuoteJobLists(ByRef sdJobLists As clsStrDictionary) As Boolean
        Dim sSql As String
        Dim v1 As Object
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim sErrorMsg As String
        Dim sJobList As String
        Dim intId, intSaveId As Integer

        mbMainGetQuoteJobLists = False
        sdJobLists.Clear()

        sSql = "SELECT DISTINCT QuotePart_JobId AS JobId,QuotePart_OrderId AS OrderId, Jobs.JobStatus  "
        sSql = sSql & " FROM [QuoteJobParts] LEFT JOIN [Jobs] ON (Jobs.Job_Id=QuoteJobParts.QuotePart_JobId)  "
        '==   sSql = sSql & " WHERE (QuotePart_OrderId= " & CStr(lngOrderId) & ") AND (LEFT(Jobs.JobStatus,2) <'90'); "
        sSql = sSql & " WHERE  (LEFT(Jobs.JobStatus,2) <'90') "
        sSql = sSql & " ORDER BY OrderId ASC; "
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            MsgBox("Failed to get Quote Jobs recordset..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--get first selected value.-....-
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                '== If Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                '== rs1.MoveFirst()
                intSaveId = -1   '-- for compare.-
                sJobList = ""
                For Each datarow1 As DataRow In rs1.Rows
                    intId = CInt(datarow1.Item("OrderId"))       '--comparing id's.
                    If (intId > intSaveId) Then  '--start new order..
                        If (intSaveId >= 0) Then
                            '--save current joblist..--
                            sdJobLists.Add(CStr(intSaveId), sJobList)
                            sJobList = ""
                        End If
                        intSaveId = intId  '--save new order no..
                    End If  '--new-
                    '-- store this jobno..
                    If (sJobList <> "") Then sJobList = sJobList & ", "
                    sJobList = sJobList & CStr(datarow1.Item("JobId"))

                Next datarow1
                '-- save last joblist..
                If sJobList <> "" Then
                    sdJobLists.Add(CStr(intSaveId), sJobList)
                End If
                '== End If '--EMPTY. bof-
                '== rs1.Close()
                mbMainGetQuoteJobLists = True
        End If '--nothing
        End If '--get-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function '--get jobs..-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  LOCAL Settings file functions..--
    '--  LOCAL Settings file functions..--
    '--  LOCAL Settings file functions..--

    '--load local settings.. eg printer names, sql serrvername..
    '= 3311.225= Private Function mbLoadSettings() As Boolean
    '= 3311.225= Dim handle_Renamed As Short
    '= 3311.225= Dim lResult, k2 As Integer
    '= 3311.225= Dim sPath As String
    '= 3311.225= Dim sLine As String
    '= 3311.225= Dim sKey, sValue As String

    '= 3311.225= sPath = gsLocalJobsSettingsPath()  '= msAppPath & K_SAVESETTINGSPATH
    '= 3311.225= mSdSettings = New clsStrDictionary  '== Scripting.Dictionary '--  holds local job settings..
    '= 3311.225= '--check if exists..--
    '= 3311.225= If (Dir(sPath) <> "") Then '--exists-
    '= 3311.225= '--lResult = openByte(sPath, "RS", handle)
    '= 3311.225= handle_Renamed = FreeFile()
    '= 3311.225= On Error Resume Next
    '= 3311.225= FileOpen(handle_Renamed, sPath, OpenMode.Input)
    '= 3311.225= lResult = Err().Number
    '= 3311.225= If (lResult <> 0) Then
    '= 3311.225= MsgBox("Can't open settings file.." & vbCrLf & _
    '= 3311.225= sPath & vbCrLf & "Error is: " & lResult & _
    '= 3311.225= "=" & ErrorToString(lResult), MsgBoxStyle.Critical)
    '= 3311.225= Else '--ok-
                '--read all lines into dictionary..--
    '= 3311.225= While Not EOF(handle_Renamed)
    '= 3311.225= sLine = LineInput(handle_Renamed)
    '= 3311.225= '--sep lhs/rhs--
    '= 3311.225= sValue = "" : sKey = ""
    '= 3311.225= k2 = InStr(1, sLine, "=")
    '= 3311.225= If (k2 > 1) Then '--we have lhs/rhs
    '= 3311.225= sKey = UCase(Trim(VB.Left(sLine, k2 - 1))) '--get name--
    '= 3311.225= sValue = Trim(Mid(sLine, k2 + 1)) '--get value--
    '= 3311.225= '--colItems.Add sValue, sName                 '--name is key--
    '= 3311.225= Else
    '= 3311.225= sKey = UCase(Trim(sLine)) '--  no rhs-
    '= 3311.225= End If
    '= 3311.225= If sKey <> "" Then mSdSettings.Add(sKey, sValue)
    '= 3311.225= End While '--lines-
    '= 3311.225= mbLoadSettings = True
    '= 3311.225= FileClose(handle_Renamed)
    '= 3311.225= End If '--open..-
    '= 3311.225= End If '--exists--
    '= 3311.225= End Function '--load settings..--
    '= = = = = =  =  == = = == ==
    '-===FF->

    '--  select printer.-
    '--  select printer.-

    Private Function mbSelectPrinter(Optional ByVal index As Short = 0) As Boolean
        Dim sName, sReceiptName, sLabelName As String
        '==3043.1== Dim sKey As String
        Dim frmPrtSelect1 As frmPrtSelect

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        '==3043.1==  miPrtIndex = index

        '= Call mbLoadSettings()  '--refresh..-
        labStatus.Text = vbCrLf & "  Checking Job Printers..."
        msColourPrtName = mLocalSettings1.item("PRTCOLOUR") '=mSdSettings.Item("PRTCOLOUR")
        msReceiptPrtName = mLocalSettings1.item("PRTRECEIPT") '= mSdSettings.Item("PRTRECEIPT")
        msLabelPrtName = mLocalSettings1.item("PRTLABEL") '= mSdSettings.Item("PRTLABEL")

        '==3043.1==  SEND current settings..--
        frmPrtSelect1 = New frmPrtSelect
        frmPrtSelect1.OriginalPrinterName = msColourPrtName
        frmPrtSelect1.OriginalReceiptPrinterName = msReceiptPrtName
        frmPrtSelect1.OriginalLabelPrinterName = msLabelPrtName
        frmPrtSelect1.ShowDialog()

        If Not frmPrtSelect1.cancelled Then
            '--  get any changed values..
            sName = frmPrtSelect1.PrinterName
            sReceiptName = frmPrtSelect1.ReceiptPrinterName
            sLabelName = frmPrtSelect1.LabelPrinterName

            '==3043.1==  Call mbSaveSetting(sKey, sName)
            '--   save all changes..-
            If (sName <> "") Then
                Call mbSaveSetting("PRTCOLOUR", sName)
                msColourPrtName = sName
            End If
            If (sReceiptName <> "") Then
                Call mbSaveSetting("PRTRECEIPT", sReceiptName)
                msReceiptPrtName = sReceiptName
            End If
            If (sLabelName <> "") Then
                Call mbSaveSetting("PRTLABEL", sLabelName)
                msLabelPrtName = sLabelName
            End If
        End If '--cancelled-
        frmPrtSelect1.Close()
        mlStaffTimeout = 0 '--now timing out..--

    End Function '--prt select--
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- L o a d  ANY JobTracking R e c o r d..-
    '-- L o a d  ANY JobTracking R e c o r d..-
    '-- L o a d  ANY JobTracking R e c o r d..-

    Private Function mbGetJobTrackingRecord(ByVal sSql As String, _
                                         ByRef colJobFields As Collection) As Boolean
        Dim RsJob As DataTable '== ADODB.Recordset
        '--Dim fld1 As ADODB.Field
        Dim dataCol1 As DataColumn
        Dim sName As String
        Dim colFld As Collection

        mbGetJobTrackingRecord = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnSql, RsJob, sSql) Then
            MsgBox("Failed to get JOB recordset.." & vbCrLf & _
                    gsGetLastSqlErrorMessage() & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            Exit Function
        End If
        '--txtMessages.Text = ""
        colJobFields = New Collection
        If (Not (RsJob Is Nothing)) AndAlso (RsJob.Rows.Count > 0) Then
            '== If RsJob.BOF And (Not RsJob.EOF) Then
            '== RsJob.MoveFirst()
            '== End If
            '== If (Not RsJob.EOF) Then '---And (cx < 100)
            '--return complete row..-
            Dim datarow1 As DataRow = RsJob.Rows(0)  '-first row.-
            For Each dataCol1 In RsJob.Columns  '= fld1 In RsJob.Fields
                colFld = New Collection
                sName = dataCol1.ColumnName
                colFld.Add(sName, "name")
                colFld.Add(datarow1.Item(sName), "value")
                colJobFields.Add(colFld, sName)
            Next dataCol1 '-fld1
            mbGetJobTrackingRecord = True
            '== Else '--not found-
            '== End If '-eof-
            '== RsJob.Close()
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--InitialiseJobsTree--
    '----  Build all Root nodes..--

    Private Function mbInitialiseJobsTreeView(ByRef tvwJobs As System.Windows.Forms.TreeView) As Boolean
        Dim nodeX, nodeY As System.Windows.Forms.TreeNode
        Dim sDateOldest, sCancelledOldest As String
        Dim fontBold As New Font("Verdana", 8, FontStyle.Bold)

        LabTreeStatus.Text = "Clearing Jobs Tree.."
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        miDeliveredMonths = 2
        mDateDeliveredStart = DateAdd(Microsoft.VisualBasic.DateInterval.Month, -2, CDate(DateTime.Today))
        '-- Oldest delivery to include..  two months ago..--
        sDateOldest = VB6.Format(mDateDeliveredStart, "dd-mmm-yyyy")
        '=3411.0414= -- cancelled period=
        mIntCancelledMonths = 3
        mDateCancelledStart = DateAdd(DateInterval.Month, -mIntCancelledMonths, CDate(DateTime.Today))
        '-- Oldest Cancelled to include..  two months ago..--
        sCancelledOldest = VB6.Format(mDateCancelledStart, "dd-mmm-yyyy")
        '==FormWaitOn "Clearing Jobs Tree.."
        System.Windows.Forms.Application.DoEvents()
        tvwJobs.Nodes.Clear()
        System.Windows.Forms.Application.DoEvents()
        LabTreeStatus.Text = "Building Tree Root Nodes.."
        '==FormWaitOn "Building Tree Root Nodes.."
        System.Windows.Forms.Application.DoEvents()
        '==='--  add new roots to treeView for job types.--
        mNodeActiveRoot = tvwJobs.Nodes.Add("ActiveRoot", "Active QUOTES and SERVICE Jobs", "viewall") '--1st/next --
        mNodeActiveRoot.NodeFont = fontBold
        mNodeActiveRoot.Expand()
        mNodeActiveRoot.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)

        mNodeDeliveredRoot = tvwJobs.Nodes.Add("DeliveredRoot", "Jobs Delivered since " & sDateOldest, "viewall") '--1st/next --
        mNodeDeliveredRoot.NodeFont = fontBold '== VB6.FontChangeBold(nodeX.NodeFont, True)
        mNodeDeliveredRoot.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)

        mNodeCancelledRoot = tvwJobs.Nodes.Add("CancelledRoot", "Jobs Cancelled Since " & sCancelledOldest, "viewall") '--1st/next --
        mNodeCancelledRoot.NodeFont = fontBold '==  VB6.FontChangeBold(nodeX.NodeFont, True)
        mNodeCancelledRoot.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)

        '--Root for Current SERVICE Jobs==
        nodeY = mNodeActiveRoot.Nodes.Add("Queued", "Queued")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)
        nodeY.Expand()
        'UPGRADE_WARNING: Add method behavior has changed Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="DBD08912-7C17-401D-9BE9-BA85E7772B99"'
        nodeY = mNodeActiveRoot.Nodes.Add("Suspended", "Suspended")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)
        '==nodeY.Bold = True
        '===nodeY.Sorted = True
        '==nodeY.Image = "queued"
        nodeY = mNodeActiveRoot.Nodes.Add("Started", "Started")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)
        '-- NEW Q.C. --
        nodeY = mNodeActiveRoot.Nodes.Add("QualityAssurance", "Quality Assurance")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)
        '==nodeY.Bold = True
        '===nodeY.Sorted = True
        '==nodeY.Image = "started"
        nodeY = mNodeActiveRoot.Nodes.Add("Notify", "To Notify/Deliver")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)
        '==nodeY.Image = "notify"
        nodeY = mNodeActiveRoot.Nodes.Add("Deliver", "To Deliver")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)
        '==nodeY.Image = "deliver"

        '--Root for Delivered SERVICE Jobs==
        nodeY = mNodeDeliveredRoot.Nodes.Add("ServiceDelivered", "Service Jobs")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)
        '==nodeY.Sorted = True
        '--Root for Delivered QUOTE Jobs==
        nodeY = mNodeDeliveredRoot.Nodes.Add("QuotesDelivered", "Quote Jobs")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)

        '--Root for Delivered ON-SITE Jobs==
        nodeY = mNodeDeliveredRoot.Nodes.Add("OnSiteDelivered", "ON-SITE Jobs")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)

        '== 3107.1015=
        '--SUB Root for CANCELLED Jobs==
        nodeY = mNodeCancelledRoot.Nodes.Add("CancelledRoot2", "All Cancelled Jobs")
        nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)

        LabTreeStatus.Text = "done.."
        tvwJobs.Visible = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function '--init tree..-
    '= = = = = = = = = = =  =  = =  =

    Private Sub mbSaveExpansionRecursive(ByVal n As TreeNode, _
                                          ByRef colNodesExpanded As Collection)
        '==System.Diagnostics.Debug.WriteLine(n.Text)
        '==MessageBox.Show(n.Text)
        Dim nodeX As TreeNode
        For Each nodeX In n.Nodes
            '== PrintRecursive(aNode)
            If nodeX.IsExpanded Then
                colNodesExpanded.Add(nodeX)
            End If
            mbSaveExpansionRecursive(nodeX, colNodesExpanded)
        Next
    End Sub
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '--  Refresh TreeView of Jobs..-
    '--  Refresh TreeView of Jobs..-

    '--  Refresh IN PLACE TreeView of Jobs.. (ie Minimum movement..)---
    '== - - - - - - - - - - - - - - - - - - -- - 
    '==   >> 3411.0411=  11-Apr-2018= ..
    '==     -- Re-work design  to speed up.
    '==     -- major change.. ClearAll PLUS Rebuild Exery time..

    Private Function mbRefreshJobsTreeView(Optional ByVal bCanClearAll As Boolean = False) As Boolean
        Dim rs1 As New DataTable  '= ADODB.Recordset
        Dim nodeGrandParent, nodeParent As System.Windows.Forms.TreeNode
        Dim nodeActiveRoot, nodeDeliveredRoot As System.Windows.Forms.TreeNode
        Dim nodeX, nodeY As System.Windows.Forms.TreeNode
        Dim nodeResult() As System.Windows.Forms.TreeNode
        '==Dim asExpanded() As String
        Dim colNodesExpanded As Collection  '--of nodes..--
        Dim sSelectedNodeKey As String
        Dim sSql As String
        Dim sWhere, sWhereMyJobs As String
        Dim sRootName As String
        Dim sDateDeliveredStart As String
        '=3411.0411- Dim sdJobs As clsStrDictionary  '==  Scripting.Dictionary
        '=3411.0411-
        '=3411.0413- Dim listTreeNodes As SortedList(Of Integer, String)
        '=3411.0413- Dim colRemovals As Collection
        Dim s1, s2 As String
        Dim s3, s4, sSpace As String
        Dim L1, lngJobs, ix, L2 As Integer
        Dim intDaysDiff As Integer
        Dim lngExpanded As Integer
        Dim lngCountx, lngCountQueued As Integer
        Dim lngCountSuspended As Integer
        Dim lngCountStarted, lngCountNotify As Integer
        Dim lngCountDeliver As Integer
        Dim lngCountOnSiteDelivered As Integer
        Dim lngCountServiceDelivered, lngCountQuotesDelivered As Integer
        Dim lngCountCancelled As Integer
        Dim sPriority As String
        Dim sJobStatus, sJobId, sTech, sTechX As String
        Dim sCustodyDays As String
        '== Dim sCustomerCompany As String
        '== Dim sCustomerName As String
        Dim sCustomer As String
        Dim sServiceNotes As String
        '== Dim sParentCaption As String
        Dim sCaption As String
        Dim sCaptionPrefix As String
        Dim sKey As String
        '= Dim sLastKey As String
        Dim sParentKey As String
        '== Dim sLastKeyQuotes As String
        Dim bSystemUnderWarranty As Boolean   '=3203.121=
        Dim bIsOnSiteJob, bToNotify As Boolean
        Dim bJobReturned, bJobDue, bOverDue As Boolean
        Dim bNewNode As Boolean
        Dim v1 As Object
        '--  text stuff..-- 
        Dim lineHeight As Single = 0
        Dim minWidth As Single
        Dim graphics1 As Graphics  '== = tvwJobs.CreateGraphics()
        Dim font1 As Font
        Dim rectF As SizeF
        Dim TextSizeF As New System.Drawing.SizeF
        '-- 3083--
        Dim colJobTag As Collection
        Dim sActionList As String = ""
        Dim intMaxCaption = tvwJobs.Width - 100

        Dim nodeQueued As TreeNode
        Dim nodeSuspended As TreeNode
        Dim nodeStarted As TreeNode
        Dim nodeQualityAssurance As TreeNode
        Dim nodeNotify As TreeNode
        Dim nodeDeliver As TreeNode
        Dim nodeCancelledRoot2 As TreeNode

        '=3411.0411- For re-inserting jobs..
        '- =3411.0413- NO MORE  Dim colJobsToInsert As Collection 
        '= SortedList(Of Integer, Integer) '--list of Jobs row nos to insert into tree.
        Dim intJobNo As Integer

        '==TextSizeF = graphics1.MeasureString("Hello World!", TextFont)
        mbRefreshJobsTreeView = False

        '==3311.817= Don't refresh Jobs TreeView if we're not in Treeview Frame..
        If Not FrameJobsTree.Visible Then
            Exit Function
        End If
        font1 = New Font(mFontTvwJobs.FontFamily, tvwJobs.Font.Size, FontStyle.Regular)

        '--graphicsTvwJobs is set dynamically..-
        graphics1 = tvwJobs.CreateGraphics()
        lineHeight = font1.GetHeight(graphics1)

        '--  BOX for Measure method is deliberately made too big-
        '---  so that our own line width calc. is allowed to trigger first..
        rectF.Width = tvwJobs.Width * 2
        rectF.Height = lineHeight

        lngExpanded = 0
        sSelectedNodeKey = ""
        nodeActiveRoot = tvwJobs.Nodes("ActiveRoot")

        '--  save settings..--
        nodeX = tvwJobs.SelectedNode
        If Not (nodeX Is Nothing) Then '--was a selection..-
            sSelectedNodeKey = nodeX.Name
        End If

        '-- save expansion condition.
        '--  HAS TO BE recursive..--
        colNodesExpanded = New Collection
        If tvwJobs.Nodes.Count > 0 Then
            For Each nodeX In tvwJobs.Nodes
                If nodeX.IsExpanded Then '--add to saved list.-
                    colNodesExpanded.Add(nodeX)
                End If
                '--  do recursion for its chlidren..-
                mbSaveExpansionRecursive(nodeX, colNodesExpanded)
            Next nodeX '---nodex.-
            '--Initialise Jobs TreeView.-
        End If '--count-

        '= 3411.0413= If bClearAll Then
        tvwJobs.BeginUpdate()
        '==Call mbInitialiseJobsTreeView(tvwJobs)
        For Each nodeParent In mNodeActiveRoot.Nodes
            nodeParent.Nodes.Clear()
        Next nodeParent
        For Each nodeParent In mNodeDeliveredRoot.Nodes
            nodeParent.Nodes.Clear()
        Next nodeParent
        '=3107.1015= = restore=
        For Each nodeParent In mNodeCancelledRoot.Nodes
            nodeParent.Nodes.Clear()
        Next nodeParent
        '==3107.1015=  mNodeCancelledRoot.Nodes.Clear()
        '= End If '-- clearAll init..--
        lngCountQueued = 0
        sDateDeliveredStart = VB6.Format(mDateDeliveredStart, "dd-mmm-yyyy")
        sWhere = ""
        sWhereMyJobs = ""

        '= 3357.0219=
        '-- chkMyJobs is now a combo- [MyJobs, All Jobs, Unclaimes Jobs] --
        If (cboJobsFilter.SelectedIndex = 0) Then  '- My Jobs-
            sWhereMyJobs = " (NominatedTech LIKE '%" & msStaffName & "%') AND  " '--MY JOBS..-
        ElseIf (cboJobsFilter.SelectedIndex = 1) Then  '--all Jobs-
            sWhereMyJobs = ""  '- no tech filter-
        ElseIf (cboJobsFilter.SelectedIndex = 2) Then  '-unclaimed-
            '== 3411.0415= Exclude WAIT-LISTED from Unclaimed.-
            sWhereMyJobs = " ((NominatedTech = '' ) AND (LEFT(JobStatus,2)>='10') ) AND  " '--if no Tech..-
        Else  '-- index-3= Waitlisted.
            sWhereMyJobs = " (LEFT(JobStatus,2)<='05') AND  " '--Wait-list.-
        End If  '-cbo Filter-

        '-   (Now only last x months of cancelled jobs.)-
        sWhere &= sWhereMyJobs & "  ( "  '==start-
        sWhere &= "   ((LEFT(JobStatus,2)<'70') ) "
        sWhere &= "     OR ( (LEFT(JobStatus,2)='70') "
        sWhere &= "          AND (DateDelivered > DATEADD(month,-" & CStr(miDeliveredMonths) & ",CURRENT_TIMESTAMP)) ) "
        '- 3411.0414=  Only last x months of cancelled jobs..
        sWhere &= "   OR  ( (LEFT(JobStatus,2)>='90') "
        sWhere &= "          AND (DateUpdated > DATEADD(month,-" & CStr(mIntCancelledMonths) & ",CURRENT_TIMESTAMP)) ) "
        sWhere &= " )"  '-end of where.
        '-- CURRENT JOBS ONLY...  ..-
        sSql = " SELECT job_id, DateCreated, DatePromised, DateUpdated, DateCompleted, NominatedTech, JobStatus, Priority, "
        '===sSql = sSql & " CustomerCompany,  CustomerName, "
        sSql &= " DATEDIFF(day,dateCreated, ISNULL(DateDelivered, CURRENT_TIMESTAMP)) AS DaysInCustody, "

        If (chkShowCompany1st.CheckState <> 1) Then '--NOT checked.-- SHOW cust name first.-
            sSql &= " (CASE CustomerCompany "
            sSql &= "         WHEN 'n/a' THEN CustomerName "
            sSql &= "         WHEN '--' THEN CustomerName "
            sSql &= "         WHEN '' THEN CustomerName "
            sSql &= "      ELSE (CustomerName + '; ' + CustomerCompany)  END) AS Customer, "
        Else  '-- Checked- show company first.-
            sSql &= " (CASE CustomerCompany "
            sSql &= "         WHEN 'n/a' THEN CustomerName "
            sSql &= "         WHEN '--' THEN CustomerName "
            sSql &= "         WHEN '' THEN CustomerName "
            sSql &= "      ELSE (CustomerCompany + '; '  + CustomerName)  END) AS Customer, "
        End If
        sSql &= " CustomerBarcode AS CustBarcode, GoodsInCare,  TechStaffName, "
        sSql &= " SystemUnderWarranty, JobReturned, ServiceNotes, Notifications "
        sSql &= " FROM Jobs "
        sSql &= " WHERE " & sWhere
        '--  SELECT ORDER..-  Rev-2804..-
        '======If (ChkCustomerOrder.Value = 1) Then '--checked..-

        '== If (OptJobsOrder(2).Checked = True) Then '--cust checked..-
        If (cboJobsOrder.SelectedIndex = 2) Then '--cust checked..-
            sSql = sSql & " ORDER BY Customer ASC, job_id ASC "
            '== ElseIf (OptJobsOrder(1).Checked = True) Then  '--priority checked..-
        ElseIf (cboJobsOrder.SelectedIndex = 1) Then  '--priority checked..-
            sSql = sSql & " ORDER BY Priority DESC, job_id ASC "
        Else
            sSql = sSql & " ORDER BY job_id ASC "
        End If

        lngJobs = 0
        LabTreeStatus.Text = "Building current jobs.."
        '- FIRST- Find all Lowest parent nodes so we can hang children off them.
        Try
            nodeQueued = tvwJobs.Nodes.Find("Queued", True)(0)
            nodeSuspended = tvwJobs.Nodes.Find("Suspended", True)(0)
            nodeStarted = tvwJobs.Nodes.Find("Started", True)(0)
            nodeQualityAssurance = tvwJobs.Nodes.Find("QualityAssurance", True)(0)
            nodeNotify = tvwJobs.Nodes.Find("Notify", True)(0)
            nodeDeliver = tvwJobs.Nodes.Find("Deliver", True)(0)
            nodeCancelledRoot2 = tvwJobs.Nodes.Find("CancelledRoot2", True)(0)
        Catch ex As Exception
            MsgBox("Failed to find TreeNode Parent- " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '- DO EVENTS resets CURSOR !!!!--
        '== System.Windows.Forms.Application.DoEvents()  
        If gbDebug Then
            MsgBox("Getting jobs recordset..  sql is:" & vbCrLf & _
                       sSql, MsgBoxStyle.Information, "mbRefreshJobsTreeView") '--test--
        End If
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            '== MsgBox("Failed to get Jobs recordset..", MsgBoxStyle.Exclamation)
            LabTreeStatus.Text = "No Jobs recordset.."
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("SQL Get Recordset ERROR:" & vbCrLf & _
                   "No records due to SQL error.." & vbCrLf & vbCrLf & _
                   "(Possible Server Connection failure.)", MsgBoxStyle.Exclamation, "Refresh TreeView")
            mbSqlConnectionFailure = True
            Exit Function
        Else '--build session records....-
            '==Set colReportLines = New Collection   '--  all session lines..-
            If Not (rs1 Is Nothing) Then
                If (rs1.Rows.Count <= 0) Then
                    '== MsgBox("No current/recent jobs found.", MsgBoxStyle.Information)   '=== Rev-2918== 
                    LabTreeStatus.Text = "No current/recent jobs found."
                    '== Rev-2918== Call mbInitialiseJobsTreeView(tvwJobs)
                    '== Rev-2918== Exit Function
                Else '-show jobs..-
                    '--service Jobs.-
                    '=3411.0411- sdJobs = New clsStrDictionary  '==  Scripting.Dictionary
                    '=3411.0411-  Copy complete tree into sorted list (JobId, ParentSectionName).
                    '=3411.0413- listTreeNodes = New SortedList(Of Integer, String)
                    '-build new dictionary list of jobs..-
                    '==rs1.MoveFirst()
                    '=3411.0411=
                    'For Each dataRow1 As DataRow In rs1.Rows
                    '    '--add to dict. list of all jobs in r'set..--
                    '    sJobId = CStr(dataRow1.Item("job_id"))
                    '    sStatus = VB.Left(dataRow1.Item("JobStatus"), 2)
                    '    sKey = "job-" & sJobId
                    '    sdJobs.Add(sKey, sStatus & ";" & LCase(dataRow1.Item("Notifications"))) '--save statues & notif.--
                    'Next dataRow1

                    '=3411.0413-  NO MORE of this.
                    '--  Now we rebuild EACH time.

                    '-- Match the sorted ("node") list with Jobs- datatable (on Job_id)..
                    '-- Both structures are in JobNo order..
                    '-- Result is a list of Jobs Ids that need to be re-added to TreeView.
                    '--  and a collection of node names to be removed,

                    '=3411.0413-  NO MORE of this.
                    '--  Now we rebuild EACH time.

                    Dim intJobsRow, intListIndex, intNodeJob As Integer
                    Dim dataRow1 As DataRow
                    Dim colOrphans As New Collection
                    Dim sStatus2 As String
                    'intJobsRow = 0
                    'intListIndex = 0
                    'colRemovals = New Collection
                    'colJobsToInsert = New Collection

                    '--  ok.. add any jobs that still appear in dictionary..-
                    '== rs1.MoveFirst()
                    '= While (Not rs1.EOF) '---

                    '=3411.0413-  BUILD all nodes.-
                    For Each dataRow1 In rs1.Rows
                        lngJobs = lngJobs + 1
                        intJobNo = dataRow1.Item("job_id")
                        sJobId = CStr(intJobNo)
                        sJobStatus = dataRow1.Item("JobStatus")
                        sPriority = UCase(dataRow1.Item("Priority"))
                        '=3103.0115==
                        sTech = Trim(VB.Left(dataRow1.Item("NominatedTech"), 7)) '=3103.0115==
                        '== 3083.312.==
                        bIsOnSiteJob = (UCase(dataRow1.Item("GoodsInCare")) = K_GOODS_ONSITEJOB)
                        '== sCustodyDays = CStr(rs1.Fields("DaysInCustody").Value)
                        '==3083==
                        intDaysDiff = gIntDateDiffDays(CDate(dataRow1.Item("DateCreated")), DateTime.Today)
                        '== sCustodyDays = VB6.Format(DateDiff(DateInterval.Day, CDate(rs1.Fields("DateCreated").Value), Now), "##0")
                        If (VB.Left(sJobStatus, 2) >= "90") Then  '--cancelled..-
                            intDaysDiff = gIntDateDiffDays(CDate(dataRow1.Item("DateCreated")), CDate(dataRow1.Item("DateUpdated")))
                        End If
                        sCustodyDays = VB6.Format(intDaysDiff, "##0")
                        bJobDue = False : bOverDue = False
                        intDaysDiff = gIntDateDiffDays(DateTime.Today, CDate(dataRow1.Item("DatePromised")))
                        '==  L1 = DateDiff(DateInterval.Day, Today, CDate(rs1.Fields("DatePromised").Value))
                        If (intDaysDiff = 0) Then
                            bJobDue = True
                        ElseIf (intDaysDiff < 0) Then
                            bOverDue = True
                        End If
                        sCustomer = VB.Left(Trim(dataRow1.Item("Customer")), 52) '= was left-32-
                        sServiceNotes = dataRow1.Item("ServiceNotes")
                        sKey = "job-" & sJobId
                        '=3411.0413-  NO MORE- If colJobsToInsert.Contains(intJobNo) Then 

                        '-- EVERYONE-
                        '= sdJobs.Exists(sKey) Then  
                        '--survived, so must be added.-
                        colJobTag = New Collection
                        colJobTag.Add(sJobId, "JobNo")  '--3083- for Context menu--
                        If (sPriority = "Q") Then
                            s1 = "QUOTE:"
                        ElseIf bIsOnSiteJob Then  '==(sPriority = "S") Then
                            s1 = "ON-SITE Job:"
                        ElseIf (VB.Left(sJobStatus, 2) <= "05") Then
                            s1 = "W-LIST:"
                        Else
                            s1 = "Service:" '--job type.--
                        End If
                        s2 = Space(5)
                        s2 = RSet(VB6.Format(CInt(dataRow1.Item("job_id")), "##000"), Len(s2))
                        minWidth = 188   '-- min 1st column.-
                        If (font1.Size >= 9) Then
                            minWidth = 192
                            If (font1.Size > 9) Then
                                minWidth = 220
                            End If
                        End If
                        '--  fill jobno/descr. to align Customer..
                        '--  NO custody days shown for ONSITE jobs..-
                        '== sCaptionPrefix = s2 & " " & s1   '==  & IIf(bIsOnSiteJob, "", " +" & sCustodyDays & " days ")
                        ix = 0
                        sSpace = "."
                        sTechX = IIf((sTech = ""), "--", sTech)
                        sCaptionPrefix = s2 & "  " & s1  '--1st pass-
                        TextSizeF = graphics1.MeasureString(sCaptionPrefix, font1, rectF, StringFormat.GenericTypographic)
                        While (TextSizeF.Width < minWidth) And (ix < 100)
                            '--  NB !  MeasureString seems to ignore trailing spaces..  !! --
                            sSpace = sSpace & "."
                            '== sCaptionPrefix = sCaptionPrefix & "^"
                            If Not bIsOnSiteJob Then
                                sCaptionPrefix = s2 & "  " & s1 & " +" & sCustodyDays & " [" & sTechX & "] " & sSpace & "."
                            Else  '--on-site-
                                sCaptionPrefix = s2 & "  " & s1 & " [" & sTechX & "] " & "."
                            End If
                            TextSizeF = graphics1.MeasureString(sCaptionPrefix, font1, rectF, StringFormat.GenericTypographic)
                            '== MsgBox("Caption: " & sCaptionPrefix & vbCrLf & _
                            '==           "TextSizeF width is: " & TextSizeF.Width, MsgBoxStyle.Information)
                            ix += 1
                        End While
                        sCaptionPrefix = Replace(sCaptionPrefix, ".", " ")  '--put back actual spaces..

                        sCaption = sCaptionPrefix & sCustomer

                        '== sCaption = sCaption & ". |++ " & sCustodyDays & " days..="
                        '-- fill shading to equalise all.--
                        '---- picture2 must have same font as TreeView..--
                        TextSizeF = graphics1.MeasureString(sCaption, font1, rectF, StringFormat.GenericTypographic)
                        '== While (Picture2.TextWidth(sCaption) < 4600)
                        While (TextSizeF.Width < intMaxCaption) And (ix < 100)  '--was 440--
                            sCaption = sCaption & "|"
                            TextSizeF = graphics1.MeasureString(sCaption, font1, rectF, StringFormat.GenericTypographic)
                            ix += 1
                        End While
                        '--truncate to fit..-
                        TextSizeF = graphics1.MeasureString(sCaption, font1, rectF, StringFormat.GenericTypographic)
                        While (TextSizeF.Width > intMaxCaption)  '--was 440--
                            sCaption = VB.Left(sCaption, Len(sCaption) - 1)
                            TextSizeF = graphics1.MeasureString(sCaption, font1, rectF, StringFormat.GenericTypographic)
                        End While
                        '--  space out the padd chars..-
                        sCaption = Replace(sCaption, "|", "  ")

                        bToNotify = (InStr(LCase(dataRow1.Item("Notifications")), "notified ok") <= 0) '--not found.. still to notify..-
                        bJobReturned = (UCase(dataRow1.Item("JobReturned")) = "Y")
                        bSystemUnderWarranty = (dataRow1.Item("SystemUnderWarranty") <> 0)
                        '===If (sPriority <> "Q") Then '--service--
                        bNewNode = True '-- not delivered.-
                        If (VB.Left(sJobStatus, 2) <= "10") Then '--created..-
                            '= nodeY = tvwJobs.Nodes.Find("Queued", True)(0).Nodes.Add(sKey, sCaption)
                            nodeY = nodeQueued.Nodes.Add(sKey, sCaption)
                            lngCountQueued = lngCountQueued + 1
                            lngCountx = lngCountQueued
                            If (VB.Left(sJobStatus, 2) = "05") Then '--waitlisted..-
                                nodeY.ImageKey = "hourglass"
                                nodeY.SelectedImageKey = "hourglass"
                                sActionList = "checkin;amend;tfrtonewcustomer;"  '--=3323=
                            Else   '-- status 10-
                                sActionList = "returntowaitlist;amend;start;tfrtonewcustomer;"  '--=3083= 3203.211--
                            End If
                        ElseIf (VB.Left(sJobStatus, 2) >= "20") And (VB.Left(sJobStatus, 2) < "30") Then  '--Suspended....--
                            '= nodeY = tvwJobs.Nodes.Find("Suspended", True)(0).Nodes.Add(sKey, sCaption)
                            nodeY = nodeSuspended.Nodes.Add(sKey, sCaption)
                            lngCountSuspended = lngCountSuspended + 1
                            lngCountx = lngCountSuspended
                            sActionList = "amend;update;tfrtonewcustomer;"  '--=3083=
                        ElseIf (VB.Left(sJobStatus, 2) > "10") And (VB.Left(sJobStatus, 2) <= "33") Then  '--started, not completed..--
                            '= nodeY = tvwJobs.Nodes.Find("Started", True)(0).Nodes.Add(sKey, sCaption)
                            nodeY = nodeStarted.Nodes.Add(sKey, sCaption)
                            lngCountStarted = lngCountStarted + 1
                            lngCountx = lngCountStarted
                            sActionList = "amend;update;returntoqueue;tfrtonewcustomer;"  '--=3083=
                        ElseIf (VB.Left(sJobStatus, 2) >= "40") And (VB.Left(sJobStatus, 2) <= "49") Then  '--QC.--
                            '= nodeY = tvwJobs.Nodes.Find("QualityAssurance", True)(0).Nodes.Add(sKey, sCaption)
                            nodeY = nodeQualityAssurance.Nodes.Add(sKey, sCaption)
                            lngCountStarted = lngCountStarted + 1
                            lngCountx = lngCountStarted
                            sActionList = "amend;update;returntoqueue;tfrtonewcustomer;"  '--=3083=
                        ElseIf (VB.Left(sJobStatus, 2) = "50") Then  '-- completed..  not delivered..--
                            '-- !!  Notifications..
                            sActionList = "reopen;deliver;tfrtonewcustomer;"  '--=3083=
                            If bToNotify Then
                                '== nodeY = tvwJobs.Nodes.Find("Notify", True)(0).Nodes.Add(sKey, sCaption)
                                nodeY = nodeNotify.Nodes.Add(sKey, sCaption)
                                lngCountNotify = lngCountNotify + 1
                                lngCountx = lngCountNotify
                            Else '--been notified.-
                                '--  show as Deliverable
                                '= nodeY = tvwJobs.Nodes.Find("Deliver", True)(0).Nodes.Add(sKey, sCaption)
                                nodeY = nodeDeliver.Nodes.Add(sKey, sCaption)
                                lngCountDeliver = lngCountDeliver + 1
                                lngCountx = lngCountDeliver
                            End If
                            '--  DON'T add delivered jobs until next round..--
                            '===ElseIf (Left(sStatus, 2) = "70") Then '--  delivered..--
                            '===     If (sPriority = "Q") Then
                            '===       Set nodeY = tvwJobs.Nodes.Add("QuotesDelivered", tvwChild, sKey, sCaption)
                            '===       lngCountQuotesDelivered = lngCountQuotesDelivered + 1
                            '===       lngCountx = lngCountQuotesDelivered
                            '===     Else
                            '===          Set nodeY = tvwJobs.Nodes.Add("ServiceDelivered", tvwChild, sKey, sCaption)
                            '===          lngCountServiceDelivered = lngCountServiceDelivered + 1
                            '===          lngCountx = lngCountServiceDelivered
                            '===     End If
                        ElseIf (VB.Left(sJobStatus, 2) >= "90") Then  '-- cancelled..--
                            Try
                                '= nodeY = tvwJobs.Nodes.Find("CancelledRoot2", True)(0).Nodes.Add(sKey, sCaption)
                                nodeY = nodeCancelledRoot2.Nodes.Add(sKey, sCaption)
                                lngCountCancelled = lngCountCancelled + 1
                                lngCountx = lngCountCancelled
                            Catch ex As Exception
                                '== msgbox =
                                '== TEST-
                                nodeY = mNodeCancelledRoot.Nodes.Add(sKey, "TEST- " & sCaption)
                            End Try
                        Else '--delivered-
                            bNewNode = False '--no new node to fiddle with..-
                        End If '--service status..-
                        '-- finish action perms..-
                        If (VB.Left(sJobStatus, 2) <= "50") Then  '--  not delivered..--
                            sActionList &= "notify;stoppress;"  '--=3083=
                        End If
                        '-- All can be viewed.. 
                        sActionList &= "view;"  '--=3083=
                        If bNewNode Then  '--have node--
                            '--3083-  Save action list for RightClick-
                            colJobTag.Add(sActionList, "ActionList")
                            nodeY.Tag = colJobTag

                            '-- OK.   set priority flag..--
                            If (sPriority = "Q") Then '--service--
                                '===nodeY.BackColor = vbYellow
                            ElseIf bJobReturned Then
                                nodeY.ImageKey = "returned" '-- "alert_red"
                                nodeY.SelectedImageKey = "returned" '-- "alert_red"
                                '===nodeY.BackColor = RGB(255, 0, 0)  '--red.-
                            ElseIf (sPriority = "1") Or (sPriority = "H") Then
                                '==== nodeY.Image = "priority1"  '===nodeY.BackColor = &HC0C0C0  '--grey-ish.--
                            ElseIf (sPriority = "2") Or (sPriority = "B") Then
                                nodeY.ImageKey = "alert_p2_pink" '===  "priority2"
                                '== nodeY.SelectedImageKey = "alert_p2" '===  "priority2"
                                nodeY.SelectedImageKey = "alert_p2_pink" '= 3203.121 ==  "priority2"
                            ElseIf (sPriority = "3") Then
                                nodeY.ImageKey = "alert_p3" '== "priority3"
                                nodeY.SelectedImageKey = "alert_p3" '== "priority3"
                                '=== nodeY.BackColor = &HCBC0FF       '--pink.- rgb(FF,C0,CB)-
                            End If
                            '-- Warranty overrides-
                            If bSystemUnderWarranty Then
                                nodeY.ImageKey = "alert_wty"
                                nodeY.SelectedImageKey = "alert_wty"
                            End If
                            If (lngCountx Mod 2) = 0 Then '--even.-
                                nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour) '==&HC0C0C0  '--grey-ish.--
                            Else
                                nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(&HE8E8E8)
                            End If '--count..-
                            '==3083==
                            If bJobDue Then
                                nodeY.ForeColor = Color.DarkOrange
                            ElseIf bOverDue Then
                                nodeY.ForeColor = Color.Crimson
                            End If
                        End If '--new node..-
                        '=3411.0413-  NO MORE- End If '--contains-- exists in dictionary.-
                        '--set alert whether new or not..--
                        '== On Error Resume Next

                        '=3411.0413=  WE already have the node..

                        '=3411.0413= nodeResult = tvwJobs.Nodes.Find(sKey, True)  '==.Item(sKey)
                        '=3411.0413= If (nodeResult.Length > 0) Then  '--found..-
                        '=3411.0413= nodeY = nodeResult(0)  '==.Item(sKey)
                        If Not (nodeY Is Nothing) Then
                            '=3203.211-  Dim out Waitlist if checked..
                            If (VB.Left(sJobStatus, 2) = "05") Then '--waitlisted..-
                                If chkHideWaitList.Checked Then
                                    nodeY.ForeColor = System.Drawing.ColorTranslator.FromOle(&HA0A0A0)
                                End If
                            End If
                            '-- IF NOT COMPLETED..  Stop Press Alert overides all others..--
                            If (InStr(sServiceNotes, "** ALERT!") > 0) And (VB.Left(sJobStatus, 2) <= "49") Then
                                '== nodeY.ImageKey = "alert_original" '-- alert triangle--"
                                '== nodeY.SelectedImageKey = "alert_original" '-- alert triangle--"
                                nodeY.ForeColor = Color.DarkBlue
                                '--=3203.219=   Save this fact for context menu..
                                If Not nodeY.Tag Is Nothing Then
                                    Dim col1 As Collection = nodeY.Tag
                                    If col1.Contains("ActionList") Then
                                        '-replace list with list + added bit.
                                        Dim sList As String = col1.Item("ActionList")
                                        col1.Remove("ActionList")
                                        col1.Add(sList & "clearalert;", "ActionList")
                                        nodeY.Tag = col1   '-update..-
                                    End If  '--contains.                                        
                                End If '-nothing-
                            End If '--alert..-
                        Else  '--can't find job node..--
                        End If '--found.-
                        '=3411.0413= End If  '-->0-
                        '== rs1.MoveNext()
                    Next dataRow1
                    '= End While '-eof service..-
                End If '--empty-
                '== rs1.Close()
                '--  show counts.--
                '= nodeX = tvwJobs.Nodes.Find("Queued", True)(0)  '==.Item("Queued")
                nodeQueued.Text = "Queued (" & CStr(nodeQueued.GetNodeCount(False)) & ")-"
                '= nodeX = tvwJobs.Nodes.Find("Suspended", True)(0)  '==.Item("Suspended")
                nodeSuspended.Text = "Suspended (" & CStr(nodeSuspended.GetNodeCount(False)) & ")-"
                '= nodeX = tvwJobs.Nodes.Find("Started", True)(0)  '==.Item("Started")
                nodeStarted.Text = "Started (" & CStr(nodeStarted.GetNodeCount(False)) & ")-"
                '== 3061.1-= QA--
                '= nodeX = tvwJobs.Nodes.Find("QualityAssurance", True)(0)  '==.Item("Started")
                nodeQualityAssurance.Text = "Quality Assurance (" & CStr(nodeQualityAssurance.GetNodeCount(False)) & ")-"

                '= nodeX = tvwJobs.Nodes.Find("Notify", True)(0)  '==.Item("Notify")
                nodeNotify.Text = "To Notify/Deliver (" & CStr(nodeNotify.GetNodeCount(False)) & ")-"
                '= nodeX = tvwJobs.Nodes.Find("Deliver", True)(0)  '==.Item("Deliver")
                nodeDeliver.Text = "To Deliver (" & CStr(nodeDeliver.GetNodeCount(False)) & ")-"
                Try
                    '= nodeX = tvwJobs.Nodes.Find("CancelledRoot2", True)(0)  '==.Item("CancelledRoot")
                    nodeCancelledRoot2.Text = "Cancelled Jobs (" & CStr(nodeCancelledRoot2.GetNodeCount(False)) & ")-"
                Catch ex As Exception
                    '== msgbox =
                End Try
                '==sTest = "Found Sessions: " + vbCrLf
            End If '--nothing.-
        End If '--get
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '-- NOW..  ADD in recently delivered jobs..  DESCENDING order-
        sSql = " SELECT job_id, DateDelivered, DateCompleted, NominatedTech, JobStatus, Priority, "
        '==sSql = sSql & " CustomerCompany,  CustomerName, "
        sSql = sSql & " (CASE CustomerCompany "
        sSql = sSql & "         WHEN 'n/a' THEN CustomerName "
        sSql = sSql & "         WHEN '--' THEN CustomerName "
        sSql = sSql & "         WHEN '' THEN CustomerName "
        sSql = sSql & "      ELSE CustomerCompany  END) AS Customer, "
        sSql = sSql & " CustomerBarcode AS CustBarcode, GoodsInCare, TechStaffName, JobReturned, Notifications "
        sSql = sSql & " FROM Jobs "
        sSql = sSql & " WHERE  " & sWhereMyJobs
        sSql = sSql & "    ( (LEFT(JobStatus,2)='70') "
        sSql = sSql & "       AND (DateDelivered > DATEADD(month,-" & CStr(miDeliveredMonths) & ",CURRENT_TIMESTAMP) ) ) "
        '====If (ChkCustomerOrder.Value = 1) Then '--checked..-

        '== If (OptJobsOrder(2).Checked = True) Then '--cust checked..-
        If (cboJobsOrder.SelectedIndex = 2) Then '--cust checked..-
            sSql = sSql & " ORDER BY Customer ASC, DateDelivered DESC "
        Else
            sSql = sSql & " ORDER BY DateDelivered DESC "
        End If
        '===lngJobs = 0
        LabTreeStatus.Text = "Building Delivered jobs.."

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '= System.Windows.Forms.Application.DoEvents()
        '--MsgBox "Getting jobs recordset..  sql is:" + vbCrLf + sSql, vbInformation '--test--
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            '== MsgBox("Failed to get Jobs recordset..", MsgBoxStyle.Exclamation)
            LabTreeStatus.Text = "Failed to get Jobs recordset.."
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--build session records....-
            Dim nodeQuotesDelivered As TreeNode
            Dim nodeOnSiteDelivered As TreeNode
            Dim nodeServiceDelivered As TreeNode
            Try
                nodeQuotesDelivered = tvwJobs.Nodes.Find("QuotesDelivered", True)(0)
                nodeOnSiteDelivered = tvwJobs.Nodes.Find("OnSiteDelivered", True)(0)
                nodeServiceDelivered = tvwJobs.Nodes.Find("ServiceDelivered", True)(0)
            Catch ex As Exception
                MsgBox("Failed to find Delivered TreeNode Parent- " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                Exit Function
            End Try
            '==Set colReportLines = New Collection   '--  all session lines..-
            If Not (rs1 Is Nothing) Then
                If (rs1.Rows.Count <= 0) Then
                    '==MsgBox "No recent Delivered jobs found.", vbInformation
                Else '-show jobs..-
                    '--  ok.. add any DELIVERED jobs that still appear in dictionary..-
 
                    '=rs1.MoveFirst()
                    For Each dataRow1 As DataRow In rs1.Rows
                        sJobId = CStr(dataRow1.Item("job_id"))
                        sJobStatus = dataRow1.Item("JobStatus")
                        sPriority = UCase(dataRow1.Item("Priority"))
                        sCustomer = VB.Left(Trim(dataRow1.Item("Customer")), 32) '==Space(34)
                        '=3103.0115==
                        sTech = Trim(VB.Left(dataRow1.Item("NominatedTech"), 7)) '=3103.0115==
                        bIsOnSiteJob = (UCase(dataRow1.Item("GoodsInCare")) = K_GOODS_ONSITEJOB)
                        sKey = "job-" & sJobId
                        '=3411.0413= If colJobsToInsert.Contains(intJobNo) Then '=  sdJobs.Exists(sKey) Then  
                        '--survived, so must be added.-
                        s2 = Space(5)
                        s2 = RSet(VB6.Format(CInt(dataRow1.Item("job_id")), "##000"), Len(s2))
                        s3 = Space(6)
                        s3 = RSet(VB.Left(VB6.Format(dataRow1.Item("DateDelivered"), "dd-mmm-yyyy"), 6), Len(s3))
                        s1 = "Svce"
                        If (sPriority = "Q") Then
                            s1 = "QTE"
                        ElseIf bIsOnSiteJob Then
                            s1 = "ON-SITE:"
                        End If
                        sTechX = IIf((sTech = ""), "--", sTech)
                        sCaptionPrefix = s3 & " " & s2 & " " & s1 & " [" & sTechX & "] "
                        '--  fill jobno/descr. to align Customer..
                        TextSizeF = graphics1.MeasureString(sCaptionPrefix, font1)
                        ix = 0
                        While (TextSizeF.Width < 130) And (ix < 100)
                            sCaptionPrefix = sCaptionPrefix & "^"
                            TextSizeF = graphics1.MeasureString(sCaptionPrefix, font1)
                            ix += 1
                        End While

                        sCaption = sCaptionPrefix & sCustomer
                        '-- fill shading to equalise all.--
                        '---- picture2 must have same font as TreeView..--
                        TextSizeF = graphics1.MeasureString(sCaption, font1, rectF, StringFormat.GenericTypographic)
                        While (TextSizeF.Width < intMaxCaption) And (ix < 100)
                            sCaption = sCaption & "^"
                            TextSizeF = graphics1.MeasureString(sCaption, font1, rectF, StringFormat.GenericTypographic)
                            ix += 1
                        End While
                        '--truncate to fit..-
                        TextSizeF = graphics1.MeasureString(sCaption, font1, rectF, StringFormat.GenericTypographic)
                        While (TextSizeF.Width > intMaxCaption)
                            sCaption = VB.Left(sCaption, Len(sCaption) - 1)
                            TextSizeF = graphics1.MeasureString(sCaption, font1, rectF, StringFormat.GenericTypographic)
                        End While
                        '--  space out the padd chars..-
                        sCaption = Replace(sCaption, "^", " ")

                        bJobReturned = (UCase(dataRow1.Item("JobReturned")) = "Y")
                        If (VB.Left(sJobStatus, 2) = "70") Then '--  delivered..--
                            If (sPriority = "Q") Then
                                '= nodeY = tvwJobs.Nodes.Find("QuotesDelivered", True)(0).Nodes.Add(sKey, sCaption)
                                nodeY = nodeQuotesDelivered.Nodes.Add(sKey, sCaption)
                                lngCountQuotesDelivered += 1
                                lngCountx = lngCountQuotesDelivered
                            ElseIf bIsOnSiteJob Then
                                '= nodeY = tvwJobs.Nodes.Find("OnSiteDelivered", True)(0).Nodes.Add(sKey, sCaption)
                                nodeY = nodeOnSiteDelivered.Nodes.Add(sKey, sCaption)
                                lngCountOnSiteDelivered += 1
                                lngCountx = lngCountOnSiteDelivered
                            Else
                                '= nodeY = tvwJobs.Nodes.Find("ServiceDelivered", True)(0).Nodes.Add(sKey, sCaption)
                                nodeY = nodeServiceDelivered.Nodes.Add(sKey, sCaption)
                                lngCountServiceDelivered += 1
                                lngCountx = lngCountServiceDelivered
                            End If
                            If bJobReturned Then
                                nodeY.ImageKey = "returned"
                            End If
                            If (lngCountx Mod 2) = 0 Then '--even.-
                                nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour) '---&HC0C0C0  '--grey-ish.--
                            Else
                                nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(&HE8E8E8)
                            End If
                        End If '--status..-
                        '==3311.422=
                        '- add Action list to view-
                        '-- All can be viewed.. 
                        sActionList = "view;"  '--=3311.422=  VIEW ONLY-
                        colJobTag = New Collection
                        colJobTag.Add(sJobId, "JobNo")  '--3083- for Context menu--
                        '--3311.422-  Save action list for RightClick-
                        colJobTag.Add(sActionList, "ActionList")
                        nodeY.Tag = colJobTag
                        '=3411.0413=  End If '--contains-- exists..-
                    Next dataRow1

                    '==While (Not rs1.EOF)
                    '==rs1.MoveNext()
                    '== End While '--eof..--
                End If '--empty..
                '--  show counts..-
                '= nodeX = tvwJobs.Nodes.Find("ServiceDelivered", True)(0)  '==.Item("ServiceDelivered")
                nodeServiceDelivered.Text = "Service Jobs  (" & CStr(nodeServiceDelivered.GetNodeCount(False)) & ")-"
                '= nodeX = tvwJobs.Nodes.Find("QuotesDelivered", True)(0)  '==.Item("QuotesDelivered")
                nodeQuotesDelivered.Text = "Quote Jobs  (" & CStr(nodeQuotesDelivered.GetNodeCount(False)) & ")-"
                '= nodeX = tvwJobs.Nodes.Find("OnSiteDelivered", True)(0)
                nodeOnSiteDelivered.Text = "ON-SITE Jobs  (" & CStr(nodeOnSiteDelivered.GetNodeCount(False)) & ")-"
            End If '--nothing.-
        End If '--get rst..-

        '--  TRY and restore current (saved ) expansion...
        L1 = colNodesExpanded.Count
        If (L1 > 0) Then
            For Each nodeX In colNodesExpanded
                nodeX.Expand()
            Next
        Else
            '--nothing expanded..  expand active root.
            nodeX = tvwJobs.Nodes("ActiveRoot")
            nodeX.Expand()
        End If

        LabTreeStatus.Text = "" & lngJobs & " job records read." '--  ""
        '--  set up selected node again.--
        If (sSelectedNodeKey <> "") Then
            nodeResult = tvwJobs.Nodes.Find(sSelectedNodeKey, True)  '==.Item(sKey)
            If (nodeResult.Length > 0) Then  '--found..-
                nodeX = nodeResult(0)  '==.Item(sKey)
                If Not (nodeX Is Nothing) Then
                    tvwJobs.SelectedNode = nodeX
                End If
            End If '--length-
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '--  Don't try and re-paint if we are closing..- 
        If mbFormClosing Then Exit Function

        '==LabTreeStatus.Caption = "" & tvwJobs.Nodes.Count & " nodes/ " & lngJobs & " job records read."  '--  ""
        '-- RE-ENABLE  redraw..-
        tvwJobs.Visible = True

        '= If bClearAll Then
        tvwJobs.EndUpdate()
        '== If bInitialise Then
        '== SendMessage(FrameJobsTree2.Handle.ToInt32, WM_SETREDRAW, 1, 0) '-- RE-ENABLE  redraw..-
        '== Call InvalidateRect(FrameJobsTree2.Handle.ToInt32, 0, 1) '--clear and repaint.-
        '== Call UpdateWindow(FrameJobsTree2.Handle.ToInt32)
        '== End If '--init..--
        '== If (lngJobs <= 0) Then MsgBox("No current/recent jobs found.", MsgBoxStyle.Information)

        '= sdJobs = Nothing
        '= colRemovals = Nothing
        nodeX = Nothing
        nodeY = Nothing

        graphics1.Dispose()
        font1.Dispose()
        colNodesExpanded = Nothing
        '--  show/update if visible--
        Call mbShowNotifyReminder()

    End Function '--refresh..
    '= = = = = = = = = = =  = = = =
    '-===FF->

    '-- Show and Setup selected Customer--

    Private Function mbSetupCustomerInfo(ByRef colFields As Collection, _
                                      ByVal bCustomerClicked As Boolean) As Boolean
        Dim s1 As String
        Dim s2 As String
        Dim sName, sValue As String
        Dim s3, s4 As String
        Dim lngJobId, lngCount As Integer
        Dim sSql As String
        Dim sWhere As String
        Dim sGoodsLine, sGoodsMsg As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim bJobsListOK As Boolean
        Dim colJobGoods As Collection
        Dim colGoodsInCare As Collection
        Dim item1 As System.Windows.Forms.ListViewItem

        mbSetupCustomerInfo = False
        If bCustomerClicked Then Call mbClearDetailsFrame()

        frameMainCmds.Enabled = True '--customer/cmds..-
        '== cmdDetailCustHistory.Enabled = True
        '== cmdDetailNotify_X.Enabled = True
        labEmptyJobPanel.Visible = False

        '-- set Current Customer Record..
        mColRMCustomerDetails = colFields

        msCustomerName = ""
        msCustomerCompany = "" : s3 = "" : s4 = ""
        mlCustomerId = CInt(colFields.Item("customer_id")("value"))
        msCustomerBarcode = colFields.Item("barcode")("value")
        msCustomerCompany = colFields.Item("company")("value")
        If (msCustomerCompany = "") Then '--no company name..--
            msCustomerCompany = "--" '--"n/a" '==  don't want blank company..--
        End If
        sName = msCustomerCompany
        s3 = colFields.Item("given_names")("value")
        s4 = colFields.Item("surname")("value")
        msCustomerPhone = colFields.Item("phone")("value")
        If Len(msCustomerPhone) > 20 Then
            msCustomerPhone = VB.Left(Replace(msCustomerPhone, " ", ""), 20)
        End If
        msCustomerMobile = colFields.Item("mobile")("value")
        If Len(msCustomerMobile) > 20 Then
            msCustomerMobile = VB.Left(Replace(msCustomerMobile, " ", ""), 20)
        End If
        msCustomerEmail = colFields.Item("email")("value")
        '-- save customer info..--
        '---msCustomerName = Left(s3 + " " + s4, 50)   '--max 50--
        msCustomerName = s4 '--max 50--SURNAME, GivenNames --
        If (msCustomerName <> "") And (s3 <> "") Then msCustomerName = msCustomerName & ", "
        msCustomerName = VB.Left(UCase(msCustomerName) & s3, 50) '--max 50--SURNAME, GivenNames --
        txtDetailsHdr.ForeColor = Color.Black
        picNoCustRecord.Visible = False

        txtDetailsHdr.Text = msCustomerName & "[" & msCustomerBarcode & "]  " & vbCrLf
        s1 = msCustomerCompany
        s2 = Trim(msCustomerMobile)
        If (s1 <> "") Then
            txtDetailsHdr.Text &= s1 & vbCrLf
        End If
        txtDetailsHdr.Text &= "Phone: " & msCustomerPhone & IIf((s2 <> ""), " [" & s2 & "]", "")

        '==
        '==  NEW Build 4221.0207  '- 07Feb2020.
        '==        -- To Show Customer Tags on Main Form..
        '==
        Dim sList, sErrorMsg As String '= = ""
        labMainCustTags.Text = ""  '--clear-
        ToolTipMain.SetToolTip(labMainCustTags, "")
        If mbIsJobmatixPOS() Then
            '=4221.0207=
            '==  Show Customer Tags..
            Dim clsTags1 As clsTags
            Dim colTags As Collection
            sList = ""
            clsTags1 = New clsTags(mCnnSql)
            If clsTags1.GetCustomerTags(mlCustomerId, s1, s2, colTags) Then
                For Each sTag As String In colTags
                    If sList <> "" Then
                        sList &= vbCrLf
                    End If
                    sList &= sTag
                Next sTag
                ToolTip1.SetToolTip(labMainCustTags, "Tags are:" & vbCrLf & sList)
                labMainCustTags.Text = sList
            Else '--not found..- 
                sErrorMsg = gsGetLastSqlErrorMessage()
                MsgBox("Failed to get TAGS for Customer_id: '" & mlCustomerId & "' !" & _
                           vbCrLf & sErrorMsg & _
                          "Make sure that POS 4221 was run first to update Cust.Table schema.", MsgBoxStyle.Exclamation)
            End If  '-get-- 
        End If  '--IS Pos..

        '== END Of TAGS for   NEW Build 4221.0207  '- 07Feb2020.
        '== END Of TAGS for   NEW Build 4221.0207  '- 07Feb2020.
        '== END Of TAGS for   NEW Build 4221.0207  '- 07Feb2020.

        '-=3057.1=  Get all jobs this cust..
        '--         In case of calling NewJob..--
        sWhere = " WHERE (RMCustomer_Id= " & CStr(mlCustomerId) & ") "
        If (mlCustomerId < 0) Then   '-- qbpos has no cust_id.-
            sWhere = " WHERE (CustomerBarcode= '" & msCustomerBarcode & "') "
        End If
        sSql = " SELECT Job_id, JobStatus,  GoodsInCare,  "
        sSql = sSql & "  ProblemSymptoms, Priority, DateUpdated, TechStaffName AS Tech,  RMCustomer_Id "
        sSql = sSql & "  FROM Jobs " & sWhere
        sSql = sSql & "   ORDER BY Job_Id DESC; "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        bJobsListOK = gbGetDataTable(mCnnSql, rs1, sSql)
        If bJobsListOK Then
            '--  build goods collection for all jobs this cust..-
            mColCustomerJobsGoods = New Collection
            sGoodsMsg = ""  '-tester-
            If Not (rs1 Is Nothing) Then
                If (rs1.Rows.Count > 0) Then  '= Not (rs1.BOF And rs1.EOF) Then '--ie.. not empty..-
                    '== colResult = New Collection
                    '== rs1.MoveFirst()
                    For Each dataRow1 As DataRow In rs1.Rows
                        colJobGoods = New Collection
                        sGoodsLine = CStr(dataRow1.Item("GoodsIncare"))
                        If (UCase(dataRow1.Item("Priority")) <> "Q") Then  '--ignore quote jobs..-
                            '--  get all goods lines this Job..--
                            sGoodsMsg = sGoodsMsg & vbCrLf & sGoodsLine  '--test.--
                            If gbDecodeGoodsIncare(sGoodsLine, colGoodsInCare) Then
                                colJobGoods.Add(CStr(dataRow1.Item("Job_Id")), "Job_Id")
                                colJobGoods.Add(VB.Format(CDate(dataRow1.Item("DateUpdated")), "dd-MMM-yyyy"), "DateUpdated")
                                colJobGoods.Add(colGoodsInCare, "ColGoodsInCare")
                                mColCustomerJobsGoods.Add(colJobGoods, CStr(dataRow1.Item("Job_Id")))
                            End If '--decode
                        End If  '--priority-
                    Next dataRow1
                    '== While Not rs1.EOF
                    '==   rs1.MoveNext()  '--next job..-
                    '== End While '--eof.-
                End If '--EMPTY. bof-
                '== rs1.Close()
            End If '--nothing
        Else '--failed..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Jobs recordset for customer: " & mlCustomerId & "..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '== MsgBox("Testing:  Found Jobs Goods.." & vbCrLf & sGoodsMsg, MsgBoxStyle.Information)

        '-- IF NEEDED-   refresh Jobs ListView for this customer..--
        If (bCustomerClicked And bJobsListOK) Then
            If gbGetDataTable(mCnnSql, rs1, sSql) Then '--ok-
                '--refresh listview..-
                lngCount = gbLoadListView(rs1, listViewCustJobs)
                '--  tag all job items with cust_id..-
                If (lngCount > 0) Then
                    For Each item1 In listViewCustJobs.Items
                        item1.Tag = msCustomerBarcode  '== Trim(CStr(mlCustomerId))
                    Next item1 '--iem.-
                    '--  show first job..--
                    item1 = listViewCustJobs.Items(0)
                    '==If (lngJobId <> mlJobId) Then '--different panel or row..-
                    If IsNumeric(item1.Text) Then
                        lngJobId = CLng(item1.Text)
                        If (lngJobId > 0) Then
                            Call mbShowJobInfoRTF(lngJobId)
                        End If
                    End If  '--numeric.-
                Else
                    labEmptyJobPanel.Visible = True
                    labEmptyJobPanel.Text = "No Jobs to show..-"
                End If
                labJobHistory.Text = "Job Work History (" & lngCount & " Jobs)."
            End If
            '== 3061.0 SELECT first job, if any.-
            If (listViewCustJobs.Items.Count > 0) Then
                listViewCustJobs.Items(0).Selected = True
            End If
        End If '--clicked-

        mbSetupCustomerInfo = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '-- mbSetupCustomer--
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '-- Clear the Customers/Jobs Details frame..--
    '-- Clear the Details frame..--

    Private Function mbClearDetailsFrame() As Boolean

        LabDetailPriority.Text = ""
        FrameJobDetails.Text = ""

        '== LabNominTech.Text = ""
        txtDetailsHdr.Text = "No Customer selected.."
        rtfJobDetails.Text = "" '-- and clear the rtf text box..

        btnAmend.Enabled = False
        '==cmdStart.Enabled = False
        btnUpdate.Enabled = False
        '== cmdDetailNotify.Enabled = False
        btnDeliver.Enabled = False
        btnReOpen.Enabled = False
        btnStopPress.Enabled = False
        '==cmdViewRecord.Enabled = False
        '== cmdDetailCustHistory.Enabled = False
        ToolStripJobAction.Enabled = False

        FrameJobDetails.Visible = False
        '== frameJobHdr.Visible = False  '== 3083==

        labEmptyJobPanel.Visible = True
        labEmptyJobPanel.Text = "No Job selected.."

        mlCustomerId = -1
        msCustomerBarcode = ""
        mlJobId = -1

        System.Windows.Forms.Application.DoEvents()
    End Function '--clear..=
    '= = = = = = = = = = =

    '--Show Job Priority--

    Private Function mbShowJobPriority(ByVal sPriority As String, _
                                       ByVal bIsOnSite As Boolean) As Boolean

        If (sPriority = "1") Or (sPriority = "H") Then
            LabDetailPriority.BackColor = System.Drawing.ColorTranslator.FromOle(gIntPriority1Colour) '--grey-ish.--
            '==3311.331= LabDetailPriority.Text = msDescriptionPriority1 '-- "Priority:  1."
            '==3311.331= mCurLabourHourlyRate = mCurLabourHourlyRateP1
        ElseIf (sPriority = "2") Or (sPriority = "B") Then
            LabDetailPriority.BackColor = System.Drawing.ColorTranslator.FromOle(gIntPriority2Colour) '--(Medium) LightYellow..--
            '==3311.331= LabDetailPriority.Text = msDescriptionPriority2 '=="Priority:  2."
            '==3311.331= mCurLabourHourlyRate = mCurLabourHourlyRateP2
        ElseIf (sPriority = "3") Then
            LabDetailPriority.BackColor = System.Drawing.ColorTranslator.FromOle(gIntPriority3Colour) '--pink.- rgb(FF,C0,CB)-
            '==3311.331= LabDetailPriority.Text = msDescriptionPriority3 '-- "Priority:  3."
            '==3311.331= mCurLabourHourlyRate = mCurLabourHourlyRateP3
        End If
        '-- TEST new system..
        Dim s1 As String
        If gbGetPriorityInfo(mCnnSql, sPriority, bIsOnSite, mRetailHost1, mCurLabourHourlyRate, s1) Then
            '= MsgBox("Labour Price is: " & _
            '=           FormatCurrency(mCurLabourHourlyRate, 2) & vbCrLf & s1, MsgBoxStyle.Information)
            LabDetailPriority.Text = s1
        End If  '-get-

    End Function '--show priority..-
    '= = = = = = = = = = =
    '-===FF->

    '-- Load RTF Job INFO FOR Selected  R e c o r d..-
    '-- Load RTF Job INFO FOR Selected  R e c o r d..-
    '-- Load RTF Job INFO FOR Selected  R e c o r d..-

    Private Function mbShowJobInfoRTF(ByVal lngJobId As Integer) As Boolean
        Dim s1, s2 As String
        Dim sSql As String
        Dim sStatus As String
        Dim sDate, sComments As String
        Dim sStaff, sDescr As String
        Dim sPriority As String
        Dim sShowCost, sPriceNote As String
        Dim sPriceEndNote1, sPriceEndNote2 As String
        Dim sListItem As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim col1 As Collection
        Dim colCustomer As Collection
        Dim ColJobFields As Collection
        Dim colJobList As Collection
        Dim colJobItems As Collection
        Dim colPart As Collection
        Dim vResult As Object
        Dim bOk As Boolean  '=, bJobReturned As Boolean
        Dim bPromised, bIsOnSiteJob As Boolean
        Dim bUpdatePrices As Boolean
        Dim bQuotation As Boolean
        Dim bToNotify, bAllowRenaming As Boolean
        '--  RTF--
        Dim ix, L1, intDaysDue As Integer
        Dim sNewText As String
        Dim strDateDescription As String
        Dim strDateCreated, strDatePromised As String
        Dim strDateUpdated As String
        Dim strStaffCreated As String
        Dim strStaffUpdated As String

        Dim strDetailsGoods As String
        Dim strDetailsProblem As String
        Dim strDetailsNotifications As String
        Dim strDetailsWork As String
        Dim strParts As String
        Dim strFullCost As String
        Dim sNewLine As String
        Dim curCost, curNewCost As Decimal
        Dim date1 As Date

        '--  text stuff..-- 
        Dim graphics1 As Graphics = rtfJobDetails.CreateGraphics()
        Dim TextSizeF As New System.Drawing.SizeF

        '== labStatus.BackColor = Color.Transparent
        '== labStatus.Text = ""
        '= Dim font1 As Font = New Font("Verdana", 8) '---  = rtfJobDetails.Font
        '-Lucida Sans-
        Dim font1 As Font = New Font("Lucida Sans", 8) '---  = rtfJobDetails.Font
        Dim fontRtfPara As Font = New Font("Verdana", 8, FontStyle.Bold) '---  = rtfJobDetails.Font

        sNewLine = "\" & vbCrLf
        FrameJobDetails.Visible = True
        '== frameJobHdr.Visible = True   '==3083=
        labEmptyJobPanel.Visible = False
        '== Call mbClearDetailsFrame()
        bQuotation = False
        bUpdatePrices = True
        sPriceEndNote1 = ""
        sPriceEndNote2 = ""

        '==LabJobReturned.Visible = False
        picJobDetailReturned.Visible = False  '==3083==

        strDetailsWork = ""
        System.Windows.Forms.Application.DoEvents()
        sSql = "SELECT * from [jobs]  "
        sSql = sSql & " WHERE (job_id=" & CStr(lngJobId) & ")  " & vbCrLf
        '--If mbGetJobRecord(lngJobId, mColJobFields(tabIndex)) Then
        If mbGetJobTrackingRecord(sSql, mColJobFields) Then
            mlJobId = lngJobId
            ColJobFields = mColJobFields '==(tabIndex) '--easier--
            '==sNewText = rtfTemplateJobs '--get original template.-
            '== labDetailHdrQuote.Visible = False   '==3083==
            labDetailOnSiteJob.Visible = False
            labDetailWarrantyJob.Visible = False  '-3203.119=

            sStatus = Trim(ColJobFields.Item("JobStatus")("value"))
            s2 = VB.Left(sStatus, 2)
            LabDetailStatus2.Text = ""
            LabDetailStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) '--default..-
            If (s2 >= "90") Then '--cancelled--
                '==PicDetailStatus.Visible = False
                LabDetailStatus.Text = " =JOB CANCELLED="
                LabDetailStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                '==LabDetailStatus.width = 3600
                '--not cancelled..-
            ElseIf (s2 = "05") Then
                LabDetailStatus.Text = " Wait-listed."
                LabDetailStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFF00FF)
                LabDetailStatus2.Text = "Customer standing by.."
            ElseIf (s2 <= "10") Then  '--created, not started..-
                LabDetailStatus.Text = "  Created/Queued."
                LabDetailStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HED9564)  '--blue accept buton.-
                LabDetailStatus2.Text = "Job is ready to start.."
            ElseIf (s2 <= "29") Then  '--Suspended..-
                LabDetailStatus.Text = "Job Suspended."
                LabDetailStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            ElseIf (s2 <= "39") Then  '--started.. not completed..-
                LabDetailStatus.Text = "Job Started."
                LabDetailStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&H80FFFF)
            ElseIf (s2 <= "49") Then  '--QC..-
                LabDetailStatus.Text = " Waiting for Q.A."
                LabDetailStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFC0FF)
            ElseIf (s2 = "50") Then  '--completed..-
                LabDetailStatus.Text = " Completed"
                LabDetailStatus.BackColor = System.Drawing.Color.Lime
            Else
                LabDetailStatus.Text = " Delivered."
            End If
            '==FrameJobDetails.Caption = "  Job # " & lngJobId    '== & "  [" & Mid(sStatus, 4) & "]  "
            LabDetailsJobNo.Text = VB6.Format(lngJobId, "###00")

            strDateCreated = Format(CDate(ColJobFields.Item("DateCreated")("value")), "ddd dd-MMM-yyyy hh:mm tt")
            strDatePromised = "--"
            bPromised = False
            date1 = CDate(mColJobFields.Item("DatePromised")("value"))
            If (DateDiff(DateInterval.Month, DateTime.Today, date1) < 12) Then  '--was promised if date is within a year.-
                strDatePromised = Format(CDate(ColJobFields.Item("DatePromised")("value")), "ddd dd-MMM-yyyy hh:mm tt")
                bPromised = True
                '==intDaysDue = DateDiff(DateInterval.Day, Today, CDate(ColJobFields.Item("DatePromised")("value")))
                intDaysDue = gIntDateDiffDays(DateTime.Today, CDate(ColJobFields.Item("DatePromised")("value")))
            End If
            '==3083.312==
            bIsOnSiteJob = (UCase(ColJobFields.Item("GoodsInCare")("value")) = K_GOODS_ONSITEJOB)
            strStaffCreated = ColJobFields.Item("RcvdStaffName")("value")
            strDateDescription = "Last Updated:"
            strDateUpdated = Format(CDate(ColJobFields.Item("DateUpdated")("value")), "ddd dd-MMM-yyyy hh:mm tt")
            strStaffUpdated = "" & ColJobFields.Item("TechStaffName")("value")
            If (VB.Left(sStatus, 2) <= "10") Then
                '== strDateDescription = "Date Created:"
                strStaffUpdated = "" & ColJobFields.Item("RcvdStaffName")("value")
            ElseIf (VB.Left(sStatus, 2) = "70") Then
                strDateDescription = "Date Delivered:"
                If Not IsDBNull(ColJobFields.Item("DateDelivered")("value")) Then
                    strDateUpdated = Format(CDate(ColJobFields.Item("DateDelivered")("value")), "ddd dd-MMM-yyyy hh:mm tt")
                End If
                strStaffUpdated = "" & ColJobFields.Item("DeliveredStaffName")("value")
                '== LabStaffUpdated.Text = "" & ColJobFields.Item("DeliveredStaffName")("value")
            End If
            '== LabDateDescription.Text = strDateDescription  '--temp-
            '== LabDateUpdated.Text = strDateUpdated       '--temp.-
            '== LabStaffUpdated.Text = strStaffUpdated       '--temp-

            '== frameMainCmds.Enabled = True '--customer/cmds..-
            mlCustomerId = CInt(ColJobFields.Item("RMCustomer_Id")("value"))
            msCustomerBarcode = ColJobFields.Item("CustomerBarcode")("value")
            '==3043.0=  get customer details from Retailmnager..--
            msCustomerName = ColJobFields.Item("CustomerName")("value")
            msCustomerPhone = ColJobFields.Item("CustomerPhone")("value")
            msCustomerMobile = Trim(ColJobFields.Item("CustomerMobile")("value"))
            msCustomerCompany = Trim(ColJobFields.Item("CustomerCompany")("value"))
            mColRMCustomerDetails = Nothing

            '--  lookup customer--
            '== labThisCustomer.Text = "This Customer"
            mColCustomerJobsGoods = Nothing  '--drop prev cust.-
            If (mlCustomerId > 0) Then  '--use _id.. -
                bOk = mRetailHost1.customerGetCustomerRecord("", mlCustomerId, colCustomer)
            Else  '--no id..-
                '--  MUST Force to use BARCODE to get cust..
                bOk = mRetailHost1.customerGetCustomerRecord(msCustomerBarcode, -1, colCustomer)
            End If
            If Not bOk Then
                txtDetailsHdr.Text = msCustomerName & "  [" & msCustomerBarcode & "]" & vbCrLf
                s2 = msCustomerMobile
                If (msCustomerCompany <> "") Then txtDetailsHdr.Text = txtDetailsHdr.Text & msCustomerCompany & vbCrLf
                txtDetailsHdr.Text = txtDetailsHdr.Text & "Phone:  " & _
                                                msCustomerPhone & IIf((s2 <> ""), "  [" & s2 & "]", "") & vbCrLf
                txtDetailsHdr.Text = txtDetailsHdr.Text & "NB: No Retail customer record for this barcode !"
                '==labThisCustomer.Text = "Customer (No Retail cust. record for this barcode !)"
                txtDetailsHdr.ForeColor = Color.DarkRed
                frameMainCmds.Enabled = False
                picNoCustRecord.Visible = True
                '== MsgBox("Processing Job No: " & lngJobId & _
                '==      "  No Retail Host customer record for cust. barcode: " & msCustomerBarcode, MsgBoxStyle.Exclamation)
                '--  setup customer--
            ElseIf Not mbSetupCustomerInfo(colCustomer, False) Then
                frameMainCmds.Enabled = False
                MsgBox("Failed to set-up customer/jobs-list for cust. barcode: " & msCustomerBarcode, MsgBoxStyle.Exclamation)
            Else  '--ok-
                '--  WE HAVE a CUSTOMER..-- 
                frameMainCmds.Enabled = True '--customer/cmds..-
                txtDetailsHdr.ForeColor = Color.Black
                picNoCustRecord.Visible = False
            End If

            sPriority = UCase(ColJobFields.Item("priority")("value"))
            msSessionTimesToDate = ColJobFields.Item("sessiontimes")("value") '-- to compute cost to date..-
            msTimeSpent = VB6.Format(ColJobFields.Item("totalservicetime")("value"), "0.00")

            '--   set priority colour..--
            '= 3203.119 revised.==
            mbJobReturned = (UCase(ColJobFields.Item("JobReturned")("value")) = "Y")
            mbSystemUnderWarranty = (ColJobFields.Item("SystemUnderWarranty")("value") <> 0)
            If mbJobReturned Then
                '== LabJobReturned.Visible = True
                picJobDetailReturned.Visible = True  '==3083=
            End If
            '--dates--3083==
            labDetailJobDue.Text = ""
            labDetailDateCreated.Text = strDateCreated & " by: " & strStaffCreated
            labDetailDatePromised.Text = strDatePromised
            If bPromised Then
                labDetailJobDue.Visible = True
                If intDaysDue = 0 Then
                    labDetailJobDue.Text = "Job Due"
                    labDetailJobDue.ForeColor = Color.DarkOrange
                ElseIf intDaysDue < 0 Then
                    labDetailJobDue.Text = "Overdue"
                    labDetailJobDue.ForeColor = Color.Crimson
                Else
                    labDetailJobDue.Text = "[" & intDaysDue & " days]"
                    labDetailJobDue.ForeColor = Color.DarkSlateGray
                End If
            Else
                labDetailJobDue.Visible = False
            End If
            labDetailTech.Text = ColJobFields.Item("NominatedTech")("value")
            labDetailUpdatedDescription.Text = strDateDescription
            labDetailDateUpdated.Text = strDateUpdated & "  by: " & strStaffUpdated

            If (sPriority = "Q") Then '--Quote..-
                '= labDetailHdrQuote.Visible = True   '==3083==
                '-- get QuoteNo..--
                bQuotation = True
                sSql = "SELECT QuotePart_OrderId,QuotePart_JobId FROM [QuoteJobParts] WHERE (QuotePart_JobId=" & CStr(lngJobId) & ")"
                If mbGetSelectValue(sSql, vResult) Then
                    '==txtDetailsGoods.Text = "QUOTATION No: " & CStr(vResult) & vbCrLf & "Full List Jobs/Status:"
                    '-  OVER-RIDE Priority display..-
                    LabDetailPriority.Text = "QUOTE No: " & CStr(vResult)
                    LabDetailPriority.BackColor = System.Drawing.ColorTranslator.FromOle(RGB(255, 204, 0)) '--orang-ish..--
                    strDetailsGoods = "QUOTATION No: " & CStr(vResult) & vbCrLf & "Full List Jobs involved:"
                    '--  get all job-nos for this quote..-
                    If mbGetQuoteJobs(CInt(vResult), colJobList) Then
                        '--show all jobs and statuses.--
                        s1 = ""
                        For Each col1 In colJobList
                            s1 = s1 & vbCrLf & CStr(col1.Item("quotepart_jobid")("value")) & "  " & col1.Item("jobstatus")("value")
                        Next col1 '--col1..-
                        '== txtDetailsGoods.Text = txtDetailsGoods.Text & s1
                        strDetailsGoods = strDetailsGoods & s1
                        '== labDetailHdrQuote.Text = "-Quote: " & CStr(vResult) & "-" & vbCrLf & "[" & colJobList.Count & " Job(s).]"
                    End If '--list..-
                Else
                    strDetailsGoods = "QUOTATION No: UNKNOWN.."
                End If '--getSelect..-
                strDetailsProblem = strDetailsProblem & vbCrLf & "Special Comments: " & vbCrLf & _
                                                                            ColJobFields.Item("Diagnosis")("value")
            Else '--service OR ON-SITE..-
                '==txtDetailsGoods.Text = ColJobFields("GoodsInCare")("value") & vbCrLf & ColJobFields("GoodsOther")("value")
                '-- RE-INSTATE when integrated..--
                Call mbShowJobPriority(sPriority, bIsOnSiteJob)
                If bIsOnSiteJob Then  '==(sPriority = "S") Then  '--ON-SITE-
                    '== LabDetailPriority.Text = "ON-SITE JOB " & CStr(vResult)
                    '= LabDetailPriority.BackColor = System.Drawing.Color.Goldenrod
                    labDetailOnSiteJob.Visible = True
                    '== Else  '--workshop..-
                ElseIf mbSystemUnderWarranty Then
                    labDetailWarrantyJob.Visible = True
                End If
                strDetailsGoods = ColJobFields.Item("GoodsInCare")("value") & vbCrLf & ColJobFields.Item("GoodsOther")("value")
                s1 = UCase(Trim(ColJobFields.Item("GoodsBrand")("value")))
                '--  in case of old-style newJob form..-
                If (s1 <> "") And (s1 <> "N/A") Then '--some old-style brand..-
                    strDetailsGoods = strDetailsGoods & vbCrLf & ColJobFields.Item("GoodsBrand")("value") & " (" & ColJobFields.Item("GoodsModel")("value") & ")"
                End If
                s1 = ColJobFields.Item("GoodsExtras")("value")
                '===If (s1 <> "") Then txtDetailsGoods.Text = txtDetailsGoods.Text & vbCrLf & "EXTRAS: " & s1
                If (s1 <> "") Then strDetailsGoods = strDetailsGoods & vbCrLf & "EXTRAS: " & s1
                If VB.Right(strDetailsGoods, 2) = vbCrLf Then '--drop it-
                    strDetailsGoods = VB.Left(strDetailsGoods, Len(strDetailsGoods) - 2)
                End If

                '-- PROBLEM--
                strDetailsProblem = ColJobFields.Item("ProblemSymptoms")("value") & vbCrLf  '==3067.0==
                s1 = ColJobFields.Item("ProblemShort")("value")
                '===If (s1 <> "N/A") And (s1 <> "") Then txtDetailsProblem.Text = s1 + vbCrLf
                If (s1 <> "N/A") And (s1 <> "") Then
                    strDetailsProblem = strDetailsProblem & "NB:  " & Replace(s1, ";", "") & vbCrLf & vbCrLf
                End If
                '==3067.0== strDetailsProblem = strDetailsProblem + ColJobFields.Item("ProblemSymptoms")("value")
                s1 = ColJobFields.Item("ProblemLong")("value")
                If (s1 <> "") Then
                    strDetailsProblem = strDetailsProblem & vbCrLf & "NOTES:" & vbCrLf & s1
                End If
                strDetailsProblem = strDetailsProblem & vbCrLf & "DIAGNOSIS:  " & ColJobFields.Item("Diagnosis")("value")
            End If '--quote..-
            System.Windows.Forms.Application.DoEvents()

            strDetailsNotifications = ColJobFields.Item("Notifications")("value")
            If VB.Right(strDetailsNotifications, 2) = vbCrLf Then '--drop it-
                strDetailsNotifications = VB.Left(strDetailsNotifications, Len(strDetailsNotifications) - 2)
            End If

            '--  F I X  Hours  !!  ---

            '====== labDetailsWork.Caption = "Tasks and Work History  ( " & _
            ''======                       Format(CCur(ColJobFields("TotalServiceTime")("value")), "##0.00") & " Hours)"
            strDetailsWork = strDetailsWork & _
                            "(Total: " & VB6.Format(CDec(ColJobFields.Item("TotalServiceTime")("value")), "##0.00") & " Hours logged..)" & vbCrLf
            '-- Tasks.-
            '-- NOW V22 for Quotes also..-
            '==== If (sPriority <> "Q") Then '-- NOT Quote..-
            '--  Show all Service tasks..--
            sSql = "Select * from [jobtasks] WHERE (taskJob_id=" & CStr(lngJobId) & ")"
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
                MsgBox("Failed to get JobTasks recordset.." & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Else '--build list box list of tasks performed so far..-
                If Not (rs1 Is Nothing) Then
                    '== If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                    For Each dataRow1 As DataRow In rs1.Rows
                        '--add to list box for job..
                        sDate = Format(CDate(dataRow1.Item("DateCreated")), "dd-MMM-yyyy")
                        '--s1 = Space(24)
                        '--LSet
                        s1 = Trim(dataRow1.Item("description"))
                        sStaff = Space(8)
                        sStaff = LSet(Trim(dataRow1.Item("performedByStaffName")), Len(sStaff))
                        '=== txtDetailsWork.Text = txtDetailsWork.Text + sStaff + "- " + sDate + ": " + s1 + vbCrLf
                        strDetailsWork = strDetailsWork & sStaff & "- " & sDate & ": " & s1 & vbCrLf
                    Next dataRow1
                    '== While (Not rs1.EOF) '---And (cx < 100)
                    '== rs1.MoveNext()
                    '== End While '-eof-
                    '== rs1.Close()
                End If '--rs-
            End If ''--get rs-

            '==== Else  '--is quote..-
            '==== -Quote- Checklist NO MORE IN V2.2 ==-
            '== end if '-- quote--

            '--  SHOW all parts.--
            Call gbShowAllParts(mCnnSql, mRetailHost1, mCurGSTPercentage, lngJobId, colJobItems, mCurOrigParts, mCurParts)
            '==ListParts.Clear
            strParts = ""
            For Each colPart In colJobItems
                sListItem = colPart.Item("Cat1")
                '-- align the v/pitch font..-
                '== While (Picture2.TextWidth(sListItem) < 741) '--Verdana 8pt..--
                TextSizeF = graphics1.MeasureString(sListItem, font1)
                While (TextSizeF.Width < 50) '--Verdana 8pt..--
                    '--  NB !! CAN'T PAD WITH SPACES fpr MeasureString..--
                    sListItem = sListItem & "~"
                    TextSizeF = graphics1.MeasureString(sListItem, font1)
                End While
                sListItem = sListItem & "  " & VB.Left(Trim(colPart.Item("Description")), 28)
                '== While (Picture2.TextWidth(sListItem) < 4200) '--Verdana 8pt..--
                TextSizeF = graphics1.MeasureString(sListItem, font1)
                While (TextSizeF.Width < 280) '--Verdana 8pt..--
                    sListItem = sListItem & "~"
                    TextSizeF = graphics1.MeasureString(sListItem, font1)
                End While

                '== 3077.610 =
                '== 3077.610 =
                '==  Show price on Job record,
                '==   unless MYOB price different AND (NOT allowRenaming..)(ie NOT possibly a Special price).
                bAllowRenaming = False
                If (colPart.Item("allow_renaming") <> 0) Then  '--  MYOB stock flag..--
                    bAllowRenaming = True
                End If
                curCost = colPart.Item("Orig_SellPrice") '--price from Job Part record.-
                curNewCost = colPart.Item("Upd_SellPrice") '== MYOB stock price--
                '==NOW ALREADY includes GST.. '== curNewCost = curNewCost + ((curNewCost * mCurGST) / 100)
                sPriceNote = ""
                '--  stock item may have been deleted..--
                '---  OR JobTracking DB and RM DB can be out of sync.-
                If (curNewCost <> curCost) Then
                    If (curNewCost <> curCost) And (Not bAllowRenaming) Then '--cost changed AND NOT SpecialPrice..-
                        '= bUpdatedCost = True
                        curCost = curNewCost '--use latest cost..-
                        sPriceNote = " -NMP-"
                        sPriceEndNote1 = " 'NMP'=New MYOB Price; "
                    ElseIf (curNewCost <> curCost) And bAllowRenaming Then  '--ASSUME Special Price-
                        sPriceNote = " -SP-"
                        sPriceEndNote2 = " 'SP'=Special custom Price."
                    End If
                End If  '--price changed..-

                s1 = FormatCurrency(curCost, 2) & sPriceNote  '=3077.610= Trim(colPart.Item("Upd_SellPrice")) 
                '--  in any case, if delivered then use price on job record..-
                If (VB.Left(sStatus, 2) >= "70") Then '--finished..  use orig job price..-
                    s1 = Trim(colPart.Item("Orig_SellPrice"))  '--price from Job Part record.-
                End If
                '== END  3077.610 =
                '== END  3077.610 =

                '--- rtf..  DROP BARCODE.--
                TextSizeF = graphics1.MeasureString(s1, font1)
                While (TextSizeF.Width < 76) '--Verdana 8pt..--
                    s1 = "~" & s1 '--Right align.-
                    TextSizeF = graphics1.MeasureString(s1, font1)
                End While
                sListItem = sListItem & s1 '===== & "  " & "B/Code: " & Trim(colPart("Barcode"))
                '==ListParts.AddItem sListItem
                '--  clear dummy cahr..--
                sListItem = Replace(sListItem, "~", " ")
                If (strParts <> "") Then strParts = strParts & vbCrLf
                strParts = strParts & sListItem
            Next colPart '--part..-

            '== more 3077.610 =
            If (sPriceEndNote1 <> "") Or (sPriceEndNote2 <> "") Then
                strParts = strParts & vbCrLf & "[Note: " & sPriceEndNote1 & sPriceEndNote2 & "]"
            End If

            strDetailsWork = strDetailsWork & vbCrLf & ColJobFields.Item("ServiceNotes")("value")
            System.Windows.Forms.Application.DoEvents()

            '--  Show full cost.--
            mCurTotalChargeableHours = gCurComputeChargeableHours(msSessionTimesToDate) '--hours to date--
            '===curSessionTime = CCur(CDbl(msSessionTime))   '--current session..-
            '===If (optChargeable(0).Value = True) Then '--Charge this session..-
            '===    mCurTotalChargeableHours = (mCurTotalChargeableHours + curSessionTime)  '-- total hours..-
            '===End If
            mCurLabour = (mCurTotalChargeableHours * mCurLabourHourlyRate) '--chargeable labour cost.. carry fwd old cost..-
            '--  REPLACE with Min Charge if Applicable.=
            If (mCurTotalChargeableHours > 0) And (mCurLabour < mCurLabourMinCharge) Then
                mCurLabour = mCurLabourMinCharge
                '===sMinCharge = "(Minimum Charge: " & Format(mCurLabourMinCharge, "$###0.00") & ")."
            End If
            '==sShowCost = Space(10)
            sShowCost = FormatCurrency(mCurParts, 2)
            '=====labFullCost.Caption = "Items: " & sShowCost & "; "
            strFullCost = "Items: " & sShowCost & "; "
            '==sShowCost = Space(10)
            sShowCost = FormatCurrency(mCurLabour, 2)
            '=====labFullCost.Caption = labFullCost.Caption & "Labour: " & sShowCost & "; "
            strFullCost = strFullCost & "Labour: " & sShowCost & "; "
            '==sShowCost = Space(10)
            sShowCost = FormatCurrency(mCurParts + mCurLabour, 2)
            '====labFullCost.Caption = labFullCost.Caption & "Total: " & sShowCost
            strFullCost = strFullCost & "Total: " & sShowCost

            '-- load all details into rich-text box..--
            sNewText = rtfTemplateJobs '--get original template.-
            '--  NOTE:  Replace all vbCrLf's with rtf "\line"..--

            '-- Job Dates now at top of RTF.--
            '== sNewText = Replace(sNewText, "&&&JOB_TECH", ColJobFields.Item("NominatedTech")("value"))

            '== sNewText = Replace(sNewText, "&&&JOB_DATE_CREATED", strDateCreated)
            '== sNewText = Replace(sNewText, "&&&JOB_DATE_PROMISED", strDatePromised)
            '== sNewText = Replace(sNewText, "&&&JOB_STAFF_CREATED", strStaffCreated)
            '== sNewText = Replace(sNewText, "&&&JOB_LABDATEDESCR", strDateDescription)
            '== sNewText = Replace(sNewText, "&&&JOB_DATE_UPDATED", strDateUpdated)
            '=== sNewText = Replace(sNewText, "&&&JOB_STAFF_UPDATED", strStaffUpdated)

            '=3357.223=
            '-  ESCAPE backslashes first...
            strDetailsGoods = Replace(strDetailsGoods, "\", "\\")
            strDetailsProblem = Replace(strDetailsProblem, "\", "\\")
            strDetailsNotifications = Replace(strDetailsNotifications, "\", "\\")
            strParts = Replace(strParts, "\", "\\")
            strFullCost = Replace(strFullCost, "\", "\\")
            strDetailsWork = Replace(strDetailsWork, "\", "\\")

            sNewText = Replace(sNewText, "&&&JOB_GOODSINCARE", Replace(strDetailsGoods, vbCrLf, sNewLine))
            sNewText = Replace(sNewText, "&&&JOB_PROBLEM", Replace(strDetailsProblem, vbCrLf, sNewLine))
            sNewText = Replace(sNewText, "&&&JOB_NOTIFICATIONS", Replace(strDetailsNotifications, vbCrLf, sNewLine))
            sNewText = Replace(sNewText, "&&&JOB_CHARGES", Replace(strParts, vbCrLf, sNewLine))
            sNewText = Replace(sNewText, "&&&JOB_COST", Replace(strFullCost, vbCrLf, sNewLine))
            sNewText = Replace(sNewText, "&&&JOB_WORKHISTORY", Replace(strDetailsWork, vbCrLf, sNewLine))

            '== rtfJobDetails.Text = sNewText '--load updated details.--
            rtfJobDetails.Rtf = sNewText '--load updated details.--
  
            ToolStripJobAction.Enabled = True
            '--  new toolStrip--
            '== enable appropriate action button..-
            btnDetailNotify.Enabled = False
            btnStopPress.Enabled = False
            btnAmend.Enabled = False
            btnCheckIn.Enabled = False
            btnUpdate.Enabled = False
            btnReturnToQueue.Enabled = False
            btnDeliver.Enabled = False
            btnReOpen.Enabled = False

            '=cmdViewRecord.Enabled = False
            '=3311.331=  Load combo here now that we have actual Job Info.
            If gbGetPriorityDescriptorsEx(mCnnSql, bIsOnSiteJob, mRetailHost1, mColPriorities) Then
                '=3311.331= '= gbGetPriorityDescriptors(mCnnSql, mColPriorities) Then
                '=3311.831-  Must CLEAR combo first..
                cboPriority.Items.Clear()
                For Each s1 In mColPriorities
                    cboPriority.Items.Add(CStr(s1))
                Next s1 '--v1-
            End If
            '===cmdUpdateGoods.Enabled = False
            cmdChangePriority.Enabled = False
            If (sPriority <> "Q") Then '-- NOT Quote..-
                cmdChangePriority.Enabled = True
            End If
            '==  sStatus = Trim(mColJobFields("JobStatus")("value"))
            s1 = Trim(ColJobFields.Item("Notifications")("value"))
            bToNotify = (InStr(LCase(s1), "notified ok") <= 0) '--not found.. still to notify..-
            Dim strStatusPrefix As String = VB.Left(sStatus, 2)
            '-- Enable buttons only if Job Not in Use..
            '--    ie  Inprocess, inProcessSusp or inProcessQA-
            If (strStatusPrefix <> "23") And (strStatusPrefix <> "33") And (strStatusPrefix <> "43") Then
                If (VB.Left(sStatus, 2) <= "05") Then '--WaitListed..--
                    '--  new toolStrip--
                    btnCheckIn.Enabled = True
                    btnAmend.Enabled = True
                ElseIf (VB.Left(sStatus, 2) <= "10") Then  '--Accepted, NOT started..--
                    btnAmend.Enabled = True
                    btnUpdate.Enabled = True
                ElseIf (VB.Left(sStatus, 2) <= "40") And (VB.Left(sStatus, 2) > "10") Then  '--started, not completed..--
                    btnAmend.Enabled = True
                    btnUpdate.Enabled = True
                    btnReturnToQueue.Enabled = True
                ElseIf (VB.Left(sStatus, 2) = "50") Then  '-- completed..  not delivered..--
                    cmdChangePriority.Enabled = False
                    If bToNotify Then
                        '--  new toolStrip--
                        btnReOpen.Enabled = True
                        btnDeliver.Enabled = True
                    Else '--was notified..
                        '--  new toolStrip--
                        btnReOpen.Enabled = True
                        btnDeliver.Enabled = True
                    End If '-notify..-
                Else '--delivered or cancelled..
                    cmdChangePriority.Enabled = False
                End If '-status--
                '--  new toolStrip--
                btnStopPress.Enabled = True '-- Hell with it..  Everybody..
                btnDetailNotify.Enabled = True
            End If  '-not in use-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
            rs1 = Nothing
            System.Windows.Forms.Application.DoEvents()
        Else '--failed  get job....-

        End If
    End Function '--show job..-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--  Q U O T E --  Show selected Quote details..-
    '--  Q U O T E --  Show selected Quote details..-
    '------ EX: BuildQuote form..----
    Private msChassisStockIds As String
    Private mlQuoteChassisQty, mlNoChassisTypes As Integer
    Private mlNoJobs As Integer

    Private Function mbLoadQuoteInfo() As Boolean
        Dim ix, iq, lngCount As Integer
        Dim lngChassisQty, lngNoCols As Integer
        Dim lngType, lngSize As Integer
        Dim lngStockId As Integer
        Dim lngQuotePartsCount As Integer
        Dim lngQty As Integer
        '== Dim sSql, sMsg As String
        Dim s1, s2, sMsg As String
        Dim sWhereList As String
        '--Dim sQtyField As String
        Dim item1 As System.Windows.Forms.ListViewItem
        '== Dim col1 As Collection
        Dim colItem As Collection
        '== Dim ColQuote As Collection
        '== Dim colResult As Collection
        Dim colQuoteLines As Collection
        '==Dim fldx As ADODB.Field
        Dim colFldx As Collection
        Dim col1 As Collection
        Dim v1 As Object
        Dim CurTotalValue As Decimal = 0
        Dim sTotalValue, sJobList As String
        Dim listItems As ListView.SelectedListViewItemCollection = Me.ListViewSalesOrders.SelectedItems

        mbLoadQuoteInfo = False
        lngQuotePartsCount = 0
        '== cmdBuildQuote.Enabled = False
        labQuoteCanBuild.Text = "Can't build.."
        labQuoteCanBuild.BackColor = Color.LightPink

        '==  now in line.   Call mbLoadQuoteHeaderInfo() '--load hdr stuff.-
        '==item1 = ListViewSalesOrders.FocusedItem

        If (listItems.Count <= 0) Then
            '==If (item1 Is Nothing) Then '--no selection..-
            MsgBox("No item is selected..", MsgBoxStyle.Exclamation)
            Exit Function
        Else
            item1 = listItems(0)   '--first selected.-
            '- return collection of flds selected item..--
            mColSelectedQuoteRow = New Collection
            If ListViewSalesOrders.Columns.Count > 0 Then
                For ix = 0 To (ListViewSalesOrders.Columns.Count - 1)
                    col1 = New Collection
                    '== s1 = mRstQuote.Fields(ix).Name
                    '--   s1 = ListView1.ColumnHeaders(ix).Text  '--col hdr is fld key.-
                    '==col1.Add s1, "name"
                    If ix = 0 Then '--first col.-
                        '--col header as key.- (Hdrs collection is 1-based.)-
                        'UPGRADE_WARNING: Lower bound of collection 
                        '==    ListViewSalesOrders.ColumnHeaders has changed from 1 to 0. Click for more: 
                        '=='ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
                        s1 = ListViewSalesOrders.Columns.Item(0).Text
                        s2 = item1.Text
                    Else
                        s2 = item1.SubItems(ix).Text
                         s1 = ListViewSalesOrders.Columns.Item(ix).Text
                    End If '--first.-
                    col1.Add(s1, "name")
                    col1.Add(s2, "value")
                    mColSelectedQuoteRow.Add(col1, LCase(s1)) '--add column to result..-
                Next ix
            End If '--count.-
        End If
        '--mbStartupDone = False
        '--clear cancel f;ag--

        sMsg = ""
        '--mlOrderId = -1
        msCustomerBarcode = ""
        CurTotalValue = 0
        sTotalValue = ""
        '==mlNoJobs = 0
        '==mlQuoteChassisQty = 0 '--total of m/boards in quote..-
        '==mlNoChassisTypes = 0
        msCustomerName = mColSelectedQuoteRow.Item("surname")("value") & ", " & _
                                                                mColSelectedQuoteRow.Item("given_names")("value")
        If Trim(msCustomerName) = "," Then msCustomerName = ""
        msCustomerCompany = mColSelectedQuoteRow.Item("company")("value")
        '--If Not bCancelled Then   '--selected--
        '--show selected row..--
        For Each col1 In mColSelectedQuoteRow
            sMsg = sMsg + col1.Item("name") & " = " & col1.Item("value") & vbCrLf
            If (LCase(col1.Item("name")) = "order_id") Then
                mlOrderId = CInt(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "custid") Then
                mlCustomerId = CInt(col1.Item("value"))
                '--  save cust no also..--
            ElseIf (LCase(col1.Item("name")) = "custbarcode") Then
                msCustomerBarcode = CStr(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "custcompany") Then
                msCustomerCompany = CStr(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "custphone") Then
                msCustomerPhone = CStr(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "custmobile") Then
                msCustomerMobile = CStr(col1.Item("value"))
            ElseIf (LCase(col1.Item("name")) = "total_inc") Then
                CurTotalValue = CDec(col1.Item("value"))
            End If
        Next col1 '--col1-
        sJobList = CStr(mColSelectedQuoteRow.Item("JobList")("value"))
        labQuoteOrderNo.Text = CStr(mlOrderId)

        '-- SHOW CUSTOMER..--
        txtQuoteDetailsHdr.Text = msCustomerName & "   [" & msCustomerBarcode & "]" & vbCrLf
        s1 = msCustomerCompany
        s2 = Trim(msCustomerMobile)
        If (s1 <> "") Then
            txtQuoteDetailsHdr.Text &= s1 & vbCrLf
        End If
        txtQuoteDetailsHdr.Text &= "Phone: " & msCustomerPhone & IIf((s2 <> ""), " [" & s2 & "]", "")

        '--End If
        sTotalValue = VB6.Format(CurTotalValue, "    $0.00")
        labOrderDetail.Text = "Total Value: " & sTotalValue
        labJoblist.Text = IIf((sJobList <> ""), "Has Jobs: " & sJobList & ". =", "No Job..")

        ListViewQuote.Items.Clear()

        If Not mRetailHost1.quoteGetStocklist(mlOrderId, colQuoteLines, mColQuoteItems) Then
            MsgBox("No Quote stock items returned..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If (mColQuoteItems Is Nothing) Then
            MsgBox("No Quote items collection returned..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        lngNoCols = 0
        '==  DONE  === (retailHost)   Set mColQuoteItems = New Collection '--  build static collection all items..--

        '-- NB: mColQuoteItems is the expanded list..
        '--- "colQuoteLines" is the base collection of the SalesLines recordset..-
        frameQuoteDetails.Visible = True

        '-- set up listview..--
        '--  Build column headers..-
        If (mColQuoteItems.Count() > 0) Then '--have some records.-
            ListViewQuote.Columns.Clear()
            colItem = mColQuoteItems.Item(1) '--get 1st record..-
            '= For Each fldx In mRsItems.Fields
            For Each colFldx In colItem
                lngNoCols = lngNoCols + 1
                '== s1 = "" & CStr(fldx.Name)
                s1 = Trim(CStr(colFldx.Item("name")))
                If LCase(s1) = "description" Then
                    ListViewQuote.Columns.Add("", s1, (CInt(ListViewQuote.Width) \ 4))
                Else
                    ListViewQuote.Columns.Add("", s1, (CInt(ListViewQuote.Width) \ 8))
                End If
                '--If LCase(s1) = "orderqty" Then sQtyField = s1
            Next colFldx '--fldx  --
        End If '--count..-

        '-- Load main items collection and PARTS listView..--

        lngCount = 0
        mlQuoteChassisQty = 0
        mlNoChassisTypes = 0
        mlNoJobs = 0
        '== If Not (mRsItems.BOF And mRsItems.EOF) Then   '--ok.. not empty--
        If (mColQuoteItems.Count() > 0) Then '--have some records.-
            '--  NOTE..  collection now include MULTIPLE items-
            '-- according to OrderQty..-

            '-- 1st Pass through items to get Chassis ino..--
            For Each colItem In mColQuoteItems
                lngChassisQty = 0 '--temp store.-
                If msQuoteChassisCat1 <> "" Then
                    If InStr(LCase(colItem.Item("cat1")("value")), LCase(msQuoteChassisCat1)) > 0 Then '--cat1 fits..-
                        If (msQuoteChassisCat2 = "") Then '--enough id..-
                            '== lngChassisQty = CLng(mRsItems("OrderQty"))  '-- ie., no of m/boards = the no of jobs..-
                            lngChassisQty = CInt(colItem.Item("OrderQty")("value")) '-- ie., no of m/boards = the no of jobs..-
                        Else '--cat2 must fit..-
                            '==  If InStr(LCase(mRsItems("cat2")), LCase(msChassisCat2)) > 0 Then '--cat2 also fits..-
                            If InStr(LCase(colItem.Item("cat2")("value")), LCase(msQuoteChassisCat2)) > 0 Then '--cat2 also fits..-
                                lngChassisQty = CInt(colItem.Item("OrderQty")("value")) '== CLng(mRsItems("OrderQty"))
                            End If
                        End If
                    End If '--cat1..-
                End If '--chassis1--
                If (lngChassisQty > 0) Then '--found a m/board..-
                    mlQuoteChassisQty = mlQuoteChassisQty + 1  '== lngChassisQty '--count ALL m/board instances..-
                    '--  add to stock-id list if nor alreday seen..
                    s1 = "/" & Trim(CStr(colItem.Item("stock_id")("Value"))) & "/"
                    If (InStr(msChassisStockIds, s1) <= 0) Then  '--Not found.. is new type--
                        mlNoChassisTypes = mlNoChassisTypes + 1 '--count m/board TYPES (ie stock types.-)..-
                        '==  count every chassis as a job..-
                        msChassisStockIds = msChassisStockIds & Trim(CStr(colItem.Item("stock_id")("Value"))) & "/"
                    End If
                    mlNoJobs = mlNoJobs + 1  '==lngChassisQty
                    '==End If
                End If
            Next colItem  '--ist pass.

            '--  2nd pass to load listview.-
            For Each colItem In mColQuoteItems
                '--  load current item into listView..-
                lngQty = 1 '--assume no qty fld..--
                lngQty = CInt(colItem.Item("OrderQty")("value")) '== CLng(mRsItems("OrderQty").Value)  '--parts listview.-
                '--  !!!!  NB-  NEED multiple instances of item as per OrderQty--
                '----  So it can correspond to ListView..--
                '-- Make a main collection item for each instance of part..--
                '== If (lngQty > 0) Then
                lngQuotePartsCount = lngQuotePartsCount + 1  '= + lngQty
                '== For iq = 1 To lngQty '--make listView row for each instance of part..-
                lngCount = lngCount + 1
                '== item1 = ListViewQuote.Items.Add()
                ix = 0
                For Each colFldx In colItem
                    v1 = colFldx.Item("value")
                    lngType = CInt(colFldx.Item("type"))
                    lngSize = CInt(colFldx.Item("definedSize"))
                    s1 = gsFormat(v1, lngType, lngSize)
                    If ix = 0 Then
                        '== item1.Text = s1
                        item1 = ListViewQuote.Items.Add(s1)
                    Else
                        item1.SubItems.Add(s1)
                    End If
                    ix = ix + 1
                Next colFldx '== ix
                '===  item1.Tag = CStr(lngCount) + "/" + Trim(CStr(mRsItems("stock_id").Value))
                item1.Tag = CStr(lngCount - 1) & "/" & Trim(CStr(colItem.Item("stock_id")("Value")))
            Next colItem  '--2nd pass.-
            If (mlNoJobs > 0) Then  '--can build-
                labQuoteCanBuild.Text = "Can build " & mlNoJobs & " job(s) ok.."
                labQuoteCanBuild.BackColor = Color.Lime
                '== cmdBuildQuote.Enabled = True
            End If  '--build-
        Else '--no records..-
            MsgBox("No parts defined for this quote..", MsgBoxStyle.Exclamation)
            Exit Function
        End If  '--count.-
    End Function  '--mbLoadQuoteInfo-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- lookup RM Staff to given long ID..--
    '-- lookup RM Staff to given BARCODE.--

    Private Function mbLookupStaff(ByRef sBarcode As String, _
                                    ByRef colFields As Collection) As Boolean
        '=== Dim colFld As Collection  '--"name"=, "value"-
        '== Dim fld1 As ADODB.Field
        Dim s1 As String
 
        mbLookupStaff = False

        '--staff Signon..--
        If mRetailHost1.staffGetStaffRecord(sBarcode, -1, colFields) Then '--found..--
            s1 = colFields.Item("docket_name")("value")
            '=== txtResults.Text = txtResults.Text & "-- Staff record: --" & vbCrLf & _
            ''===     "Staff-id: " & colRecord("staff_id")("value") & "; docket_name is: " & s1 & vbCrLf & _
            ''===     "Surname: " & colRecord("surname")("value") & vbCrLf & "= = = = = = " & vbCrLf
            '== MsgBox "Found: " & s1, vbInformation
            mbLookupStaff = True
        Else
            MsgBox("Staff code not found.", MsgBoxStyle.Exclamation)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

    End Function '--staff lookup..--
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Browse Jobs or Parts table using --
    '--  Separate BROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseTable(ByRef colPrefs As Collection, _
                                    ByRef sTitle As String, _
                                      ByRef sWhere As String, _
                                      ByRef colKeys As Collection, _
                                      ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Jobs") As Boolean
        Dim frmBrowse1 As New frmBrowse

        mbBrowseTable = False

        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle

        frmBrowse1.ShowDialog(Me)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

        '--  get selected record key..--
        colKeys = frmBrowse1.selectedKey
        colSelectedRow = frmBrowse1.selectedRow
        mbBrowseTable = True
        frmBrowse1.Close()

        frmBrowse1.Dispose()
    End Function '--browse.--
    '= = = = = = =
    '-===FF->

    '-- BROWSE ON-SITE Jobs using Browse Class and our local dataGrid..-
    '-- BROWSE ON-SITE Jobs using Browse Class and our local dataGrid..-
    '-- BROWSE.. Show ONLY (No wait..) using Browse Class..
    '---  show recordset, and enable Grid..--
    Private Function mbShowOnSiteJobsBrowse() As Boolean
        '== Dim colRowValues As Collection
        Dim sWhere, sMsg As String
        Dim ix As Integer

        mbShowOnSiteJobsBrowse = False
        mlStaffTimeout = -1 '--disable timer..-
        labRecCountOnSite.Text = ""
        '--- Keep same object..--
        '---- can Re-Activate after setting/resetting properties..--
        '==If (maBrowse1 Is Nothing) Then
        '==     Set maBrowse1 = New clsBrowse
        '-- - --- MUST set first time--
        mBrowseOnSiteJobs.connection = mCnnSql '--job tracking sql connenction..-
        mBrowseOnSiteJobs.DBname = msSqlDbName
        '== mBrowseJobs.FlexGrid = MSHFlexGridJobs
        mBrowseOnSiteJobs.DataGrid = dataGridViewOnSite
        '--  pass controls..--
        mBrowseOnSiteJobs.showRecCount = labRecCountOnSite '--updates rec. retrieval..
        '==End If
        frameOnSite.Enabled = True
        dataGridViewOnSite.Enabled = True
        '==  LabTitle.Text = sTitle
        mLngSelectedRowOnSite = -1

        On Error GoTo BrowseOnSite_Activate_Error

        '-- go..--
        '-- go --
        If Not mBrowseOnSiteJobs.Activate() Then
            sMsg = mBrowseOnSiteJobs.LastErrorMsg
            MsgBox(sMsg, MsgBoxStyle.Information)
            MsgBox("ONSITE Jobs.. Possible Sql Connection Failure.." & vbCrLf & _
                      "JobMatix will attempt to renew Connection..", _
                           MsgBoxStyle.Exclamation, "mbShowOnSiteJobsBrowse")
            mbSqlConnectionFailure = True
        End If
        On Error GoTo 0
        txtFind.Focus()
        mlStaffTimeout = 0 '-- timer can start..-
        mbShowOnSiteJobsBrowse = True
        Exit Function

BrowseOnSite_Activate_Error:
        MsgBox("ONSITE Jobs.. Possible Sql Connection Failure.." & vbCrLf & _
                  "JobMatix will attempt to renew Connection..", _
                       MsgBoxStyle.Exclamation, "mbShowOnSiteJobsBrowse")
        mbSqlConnectionFailure = True
        mlStaffTimeout = 0 '-- timer MUST start..-
    End Function '-- Show ON-SITE Browse..--
    '= = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- ON-SITE Browse. Refresh --

    Private Sub cmdRefreshOnSite_Click(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles cmdRefreshOnSite.Click

        mlStaffTimeout = -1 '--disable timer..-
        If Not mBrowseOnSiteJobs.refresh Then
            MsgBox("Possible Sql Connection Failure.." & vbCrLf & _
                      "JobMatix will attempt to renew Connection..", _
                           MsgBoxStyle.Exclamation, "mbShowJobsBrowse")
            mbSqlConnectionFailure = True
        End If
        mlStaffTimeout = 0 '-- timer MUST start..-
    End Sub '--RefreshOnSite-
    '= = = = = = = = = = = = = ==
    '-===FF->

    '-- BROWSE Jobs using Browse Class and our local FlexGrid..-
    '-- BROWSE Jobs using Browse Class and our local FlexGrid..-

    '-- BROWSE Jobs.. Show ONLY (No wait..) using Browse Class..
    '---  show recordset, and enable Grid..--

    Private Function mbShowJobsBrowse(ByRef colPrefs As Collection, _
                                        ByRef sTitle As String, _
                                          ByRef sWhereCond As String) As Boolean
        '== Dim colRowValues As Collection
        Dim sWhere As String
        Dim ix As Integer

        mbShowJobsBrowse = False
        mlStaffTimeout = -1 '--disable timer..-
        '--- Keep same object..--
        '---- can Re-Activate after setting/resetting properties..--
        '==If (maBrowse1 Is Nothing) Then
        '==     Set maBrowse1 = New clsBrowse
        '-- - --- MUST set first time--
        mBrowseJobs.connection = mCnnSql '--job tracking sql connenction..-
        mBrowseJobs.colTables = mColSqlDBInfo
        mBrowseJobs.DBname = msSqlDbName
        mBrowseJobs.tableName = "jobs"
        mBrowseJobs.IsSqlServer = True '--bIsSqlServer
        '== mBrowseJobs.FlexGrid = MSHFlexGridJobs
        mBrowseJobs.DataGrid = DataGridViewJobs

        '== mBrowseJobs.ArrowUp = PicArrowUp
        '== mBrowseJobs.ArrowDown = PicArrowDown
        '--  pass controls..--
        mBrowseJobs.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowseJobs.showFind = LabFind '--updates Sort Column display..
        mBrowseJobs.showTextFind = txtFind '--updates Sort Column display..
        '==End If
        '--- set WHERE condition for jobStatus..--
        sWhere = sWhereCond
        mBrowseJobs.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        mBrowseJobs.PreferredColumns = colPrefs
        mBrowseJobs.ShowPreferredColumnsOnly = True
        FrameBrowse.Enabled = True
        DataGridViewJobs.Enabled = True
        '= LabTitle.Text = sTitle
        mLngSelectedRow = -1

        On Error GoTo BrowseJobs_Activate_Error
        mBrowseJobs.Activate() '-- go..--
        On Error GoTo 0
        txtFind.Focus()
        mlStaffTimeout = 0 '-- timer can start..-
        mbShowJobsBrowse = True
        Exit Function

BrowseJobs_Activate_Error:
        MsgBox("Possible Sql Connection Failure.." & vbCrLf & _
                  "JobMatix will attempt to renew Connection..", _
                       MsgBoxStyle.Exclamation, "mbShowJobsBrowse")
        mbSqlConnectionFailure = True
        mlStaffTimeout = 0 '-- timer MUST start..-
    End Function '-- Show Browse..--
    '= = = = = = =
    '-===FF->

    '--=3072/3= R e f r e s h-
    '--=3072/3= R e f r e s h-
    '--=3072/3= R e f r e s h-

    Private Function mbJobsBrowseRefresh() As Boolean

        mbJobsBrowseRefresh = False
        '==3311.817= Don't refresh JobsGrid if we're not in Jobs Grid.
        If Not FrameBrowse.Visible Then
            Exit Function
        End If
        On Error GoTo BrowseJobs_Refresh_Error
        Call mBrowseJobs.refresh() '-- Rev-2804--refresh--
        mbJobsBrowseRefresh = True
        Exit Function

BrowseJobs_Refresh_Error:
        MsgBox("Possible Sql Connection Failure.." & vbCrLf & _
                  "JobMatix will attempt to renew Connection..", _
                       MsgBoxStyle.Exclamation, "mbJobsBrowseRefresh")
        mbSqlConnectionFailure = True

    End Function  '--Refresh-
    '= = = = = = =
    '-===FF->

    '--  CUSTOMER BROWSE..--
    '--  C U S T O M E R --  BROWSING..

    '-- Start/Refresh customer browse..-
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbRefreshCustomerBrowse(Optional ByRef sWhereCond As String = "") As Boolean

        Dim cx, j, i, k, fx As Integer
        Dim lngId As Integer
        Dim s1, s2 As String
        Dim sHostTablename As String
        Dim colPrefs As Collection
        '== Dim colSelectedRow As Collection

        mlStaffTimeout = -1 '--disable timer..-
        '== mBrowseCust.retailHost = mRetailHost1
        '--  get table/prefs info for this host..--
        mBrowseCust.connection = mRetailHost1.connection
        mBrowseCust.colTables = mRetailHost1.colTables
        mBrowseCust.IsSqlServer = mRetailHost1.IsSqlServer
        mBrowseCust.DBname = mRetailHost1.DBname

        If Not mRetailHost1.browseGetPrefColumns("customer", sHostTablename, colPrefs) Then
            MsgBox("Can't translate table name to host table..", MsgBoxStyle.Exclamation)
        End If
        mBrowseCust.tableName = sHostTablename

        '=3501.1105= !!  mBrowseCust.IsSqlServer = False '--JET/ R-M..--
        '==mBrowseCust.FlexGrid = MSHFlexGridCust
        mBrowseCust.DataGrid = DataGridViewCust
        '== mBrowseCust.ArrowUp = PicArrowUp
        '== mBrowseCust.ArrowDown = PicArrowDown
        '--  pass controls..--
        mBrowseCust.showRecCount = labRecCountCust '--updates rec. retrieval..
        mBrowseCust.showFind = labFindCust '--updates Sort Column display..
        mBrowseCust.showTextFind = txtFindCust '--updates Sort Column display..
        '===sWhere = sWhereCond
        mBrowseCust.WhereCondition = sWhereCond '-- sWhere  '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        mBrowseCust.PreferredColumns = colPrefs
        mBrowseCust.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
        '--=3083.402== InitialOrderIsDescending -
        mBrowseCust.InitialOrderIsDescending = False

        '==FrameBrowse.Enabled = True
        DataGridViewCust.Enabled = True
        '== mLngSelectedRow = -1
        On Error GoTo CustBrowse_Activate_Error
        mBrowseCust.Activate() '-- go..--
        On Error GoTo 0
        '====txtFind.Text = "A" '--SURNAME is FIRST columns..--
        txtFindCust.Focus()
        mlStaffTimeout = 0 '-- timer can start..-
        Exit Function

CustBrowse_Activate_Error:
        MsgBox("Possible RetailManager Connection Failure.." & vbCrLf & _
                  "Please Check availability of RM database..", _
                       MsgBoxStyle.Exclamation, "mbShowCustBrowse")
        mlStaffTimeout = 0 '-- timer can start..-
    End Function '--keyup-
    '= = = =  = =  = =
    '-===FF->

    '--  load  Q U O T E S  listView (sales orders)...
    '--  load  Q U O T E S  listView (sales orders)...

    Private Function mbRefreshQuotesList() As Boolean
        Dim s1 As String
        Dim sFullJobList, sWhereList As String
        '== Dim fldx As ADODB.Field
        Dim lngNoCols, ix As Integer
        Dim lngType, lngSize As Integer
        Dim lCount, intOrderId As Integer
        Dim intDaysDiff As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim sdQuoteJobLists As clsStrDictionary
        Dim colRecord As Collection
        Dim colResult As Collection
        Dim colFldx As Collection
        Dim v1 As Object
        Dim bIncludeQuote As Boolean
        Dim date1 As Date

        mbRefreshQuotesList = False
        labLoadingQuotes.Text = "Getting quote records.."
        System.Windows.Forms.Application.DoEvents()
        If Not mRetailHost1.quoteGetAllQuotes(mColQuoteRecords) Then
            labLoadingQuotes.Text = "No quote records.."
            System.Windows.Forms.Application.DoEvents()
            MsgBox("No quotes.", MsgBoxStyle.Exclamation)
            '== Me.Hide()
            Exit Function
        Else  '--ok-
            '--  load listview..-
            '== LabOrderDetail.Text = vbCrLf & "Loading Listview for customer:" & vbCrLf & msCustomerSearchName
            frameQuoteDetails.Visible = False
            System.Windows.Forms.Application.DoEvents()
            '==  Call mbLoadRMQuotesListView(mColQuoteRecords, ListViewSalesOrders)

            lngNoCols = 0
            lCount = 0
            '== mbLoadRMQuotesListView = False
            '-- create column headers...--
            ListViewSalesOrders.Items.Clear()
            ListViewSalesOrders.Columns.Clear()
            If mColQuoteRecords Is Nothing Then Exit Function
            If mColQuoteRecords.Count() > 0 Then

                sdQuoteJobLists = New clsStrDictionary
                '-- Get joblists for all quotes..-
                Call mbMainGetQuoteJobLists(sdQuoteJobLists)
                '--OK. get header texts from 1st record..-
                '--  and build ListView headers..
                colRecord = mColQuoteRecords.Item(1)
                For Each colFldx In colRecord
                    s1 = "" & CStr(colFldx.Item("name"))
                    ListViewSalesOrders.Columns.Add(s1) '--, ListView1.Width \ 8
                    '--set width.-
                    If LCase(s1) = "order_id" Then
                        ListViewSalesOrders.Columns(lngNoCols).Width = 70
                    ElseIf LCase(s1) = "order_date" Then
                        ListViewSalesOrders.Columns(lngNoCols).Width = 80
                    ElseIf LCase(s1) = "surname" Then
                        ListViewSalesOrders.Columns(lngNoCols).Width = 100
                    ElseIf LCase(s1) = "company" Then
                        ListViewSalesOrders.Columns(lngNoCols).Width = 130
                    End If
                    lngNoCols = lngNoCols + 1
                Next colFldx '--col1  --
                '-- ADD extra column for jobs..--
                ListViewSalesOrders.Columns.Add("JobList") '--, ListView1.Width \ 8
                lngNoCols = lngNoCols + 1
                '--MsgBox "Headers loaded...", vbInformation
                ListViewSalesOrders.ListViewItemSorter = New ListViewItemComparerQ3037(0, SortOrder.Descending)
                '--fill list box with record fields--
                '--scan colection and load--
                labLoadingQuotes.Text = "Loading ListView..."
                System.Windows.Forms.Application.DoEvents()
                For Each colRecord In mColQuoteRecords
                    intOrderId = CInt(colRecord("order_id")("value"))
                    date1 = CDate(colRecord("order_date")("value"))
                    '-- IF we want new items only, check for existing jobs..--
                    sFullJobList = ""
                    If sdQuoteJobLists.Exists(CStr(intOrderId)) Then
                        sFullJobList = sdQuoteJobLists.Item(CStr(intOrderId))
                    End If
                    bIncludeQuote = True
                    If (chkNewQuotes.Checked = True) Then
                        '--check for jobs..--
                        If (sFullJobList <> "") Then
                            bIncludeQuote = False   '--don't want quotes with jobs..
                        End If
                    End If  '--checked..-
                    If chkRecentQuotes.Checked Then  '--only recent quotes wanted.
                        '--must be recent..--
                        intDaysDiff = gIntDateDiffDays(date1, DateTime.Today)
                        If (intDaysDiff > 60) Then
                            bIncludeQuote = False   '--don't want OLD quotes..
                        End If
                        '==If (DateDiff(DateInterval.Day, date1, Now) > 60) Then
                        '==bIncludeQuote = False   '--don't want OLD quotes..
                        '==End If
                    End If
                    '---load current item-- 
                    ix = 0
                    If bIncludeQuote Then
                        item1 = New ListViewItem
                        For Each colFldx In colRecord
                            v1 = colFldx.Item("value")
                            lngType = CInt(colFldx.Item("type"))
                            lngSize = CInt(colFldx.Item("definedSize"))
                            '-- try and right justify order_id-
                            If (LCase(colFldx.Item("name")) = "order_id") Then
                                s1 = RSet(CStr(v1), 5)
                            Else
                                s1 = gsFormat(v1, lngType, lngSize)
                            End If
                            If ix = 0 Then
                                item1.Text = s1
                                '== item1 = ListViewSalesOrders.Items.Add(s1)
                            Else
                                item1.SubItems.Add(s1)
                            End If
                            ix = ix + 1
                        Next colFldx '== ix
                        '--ADD LobList column.-
                        item1.SubItems.Add(sFullJobList)
                        lCount = lCount + 1
                        item1.Tag = CStr(lCount) '--ID of this part item..-
                        '--  add to listview items colection.-
                        ListViewSalesOrders.Items.Add(item1)
                    End If  '--include..-
                Next colRecord '-record.-
            Else
                MsgBox("No items to show...", MsgBoxStyle.Exclamation)
            End If '--not empty--
            labQuoteCount.Text = CStr(lCount)
            mbRefreshQuotesList = True
        End If
        labLoadingQuotes.Text = ""
        ListViewSalesOrders.Enabled = True
        If (ListViewSalesOrders.Items.Count > 0) Then
            ListViewSalesOrders.FocusedItem = ListViewSalesOrders.Items.Item(0)
        End If
    End Function  '--mbRefreshQuotesList--
    '= = = =  = =  = =
    '-===FF->

    '-- get currently selected Job Row..--

    Private Function mlGetCurrentJobRow(ByRef colKeys As Collection, _
                                         ByRef colRowValues As Collection) As Integer
        Dim lRow, lCol As Integer
        mlGetCurrentJobRow = -1 '--no selection..-
        If (DataGridViewJobs.SelectedRows.Count > 0) Then
            '--  use 1st selected row only.
            lRow = DataGridViewJobs.SelectedRows(0).Cells(0).RowIndex
            If lRow >= 0 Then '--ok row--
                '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                mLngSelectedRow = lRow
                Call mBrowseJobs.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                If colKeys Is Nothing Then
                    MsgBox("Nothing selected.", MsgBoxStyle.Exclamation)
                Else '--ok
                    mlGetCurrentJobRow = lRow
                End If
            End If '--header-
        End If '--sel.count..-
    End Function '--get current row..-
    '= = = = = = = = = = = = =

    '-- get currently selected ONSITE Job Row..--

    Private Function mlGetCurrentOnSiteRow(ByRef colKeys As Collection, _
                                         ByRef colRowValues As Collection) As Integer
        Dim lRow, lCol As Integer

        mlGetCurrentOnSiteRow = -1 '--no selection..-
        If (dataGridViewOnSite.SelectedRows.Count > 0) Then
            '--  use 1st selected row only.
            lRow = dataGridViewOnSite.SelectedRows(0).Cells(0).RowIndex
            If (lRow >= 0) Then '--ok row--
                '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                mLngSelectedRowOnSite = lRow
                Call mBrowseOnSiteJobs.SelectRecord(mLngSelectedRowOnSite, colKeys, colRowValues)
                If colKeys Is Nothing Then
                    MsgBox("Nothing selected.", MsgBoxStyle.Exclamation)
                Else '--ok
                    mlGetCurrentOnSiteRow = lRow
                End If
            End If '--header-
        End If '--sel.count..-
    End Function '--get current row..-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  get jobno of selected Browse-row or TreeNode..-
    '-- Updated 3519.0122=  for checking we have the right job..-
    '-- Updated 3519.0122=  for checking we have the right job..-

    Private Function mbGetSelectedJobNo(ByRef lngJobId As Integer, _
                                        ByRef sStatus As String) As Boolean
        Dim lRow, lCol, intDetailJobNo As Integer
        Dim index As Integer = TabControlJobTracking.SelectedIndex
        Dim pageX As TabPage = TabControlJobTracking.TabPages(index) '= ev.TabPage
        Dim colRowValues As Collection
        Dim ColJobFields As Collection
        Dim colKeys As Collection
        Dim sKey, sJobId As String
        Dim sSql As String
        Dim nodeX As System.Windows.Forms.TreeNode
        Dim item1 As ListViewItem

        mbGetSelectedJobNo = False
        If (Not FrameJobDetails.Visible) OrElse (Not IsNumeric(Trim(LabDetailsJobNo.Text))) Then
            '=4219.1128- DROP POpup-
            '==                        into JMxRetailHost.dll so EVERyONE  (RAs) can use it.
            '==      -- JobMatixMain- Drop Error Popup "There is no Job No or details selected." 
            '==            Happens when DoubleClicking on Tree Section. 

            'Beep()
            'MessageBox.Show("Error-" & vbCrLf & _
            '                "There is no Job No or details selected.", _
            '                   "Checking Job No.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Exit Function
        End If  '-visible..
        '- Get Job No showing in RH panel.
        intDetailJobNo = CInt(Trim(LabDetailsJobNo.Text))
        If (LCase(pageX.Name) = "tabpagejobsgrid") Then  '= FrameBrowse.Visible Then '--using browser..-
            lRow = mlGetCurrentJobRow(colKeys, colRowValues)
            If (lRow >= 0) Then '==real row.-
                lngJobId = CInt(colKeys.Item(1))
                sStatus = Trim(colRowValues.Item("JobStatus")("value"))
                If (lngJobId > 0) AndAlso (lngJobId = intDetailJobNo) Then
                    mbGetSelectedJobNo = True
                Else
                    Beep()
                    MessageBox.Show("Selection Error-" & vbCrLf & _
                                      "  The Detail Job No doesn't match with JobNo from Grid." & vbCrLf & vbCrLf & _
                                       "  Please retry the Job selection.", _
                                       "Checking Job No.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                '==MsgBox "Found jobno: " & lngJobId, vbInformation
            End If
        ElseIf (LCase(pageX.Name) = "tabpageonsite") Then '= frameOnSite.Visible Then
            lRow = mlGetCurrentOnSiteRow(colKeys, colRowValues)
            If (lRow >= 0) Then '==real row.-
                sJobId = colKeys.Item(1)
                If IsNumeric(sJobId) AndAlso (CInt(sJobId) > 0) _
                                                    AndAlso (CInt(sJobId) = intDetailJobNo) Then
                    lngJobId = CInt(sJobId)
                    sStatus = Trim(colRowValues.Item("JobStatus")("value"))
                    '==sStatus = Trim(colRowValues.Item(5)("value"))
                    mbGetSelectedJobNo = True
                Else
                    Beep()
                    MessageBox.Show("Selection Error- " & vbCrLf & _
                                    "  The Detail Job No doesn't match with JobNo from the ONSITE Grid." & vbCrLf & vbCrLf & _
                                    "   Please retry the Job selection.", _
                                       "Checking Job No.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                '==lngJobId = CInt(colKeys.Item(1))
                '== If (lngJobId > 0) Then mbGetSelectedJobNo = True
                '==MsgBox "Found jobno: " & lngJobId, vbInformation
            End If
        ElseIf (LCase(pageX.Name) = "tabpagejobstree") Then '= FrameJobsTree.Visible Then  '--tree..-
            '--get current node..-
            nodeX = tvwJobs.SelectedNode
            If nodeX Is Nothing Then
                Beep()
            Else
                sKey = LCase(nodeX.Name)
                If VB.Left(sKey, 3) = "job" Then
                    lngJobId = CInt(Mid(sKey, 5)) '--bypass "job-" --
                    '===Call mbShowJobInfo(0, lngJobId)
                    If (lngJobId > 0) AndAlso (lngJobId = intDetailJobNo) Then
                        sSql = "SELECT * from [jobs]  "
                        sSql = sSql & " WHERE (job_id=" & CStr(lngJobId) & ")  " & vbCrLf
                        If mbGetJobTrackingRecord(sSql, mColJobFields) Then
                            ColJobFields = mColJobFields '--easier--
                            mbGetSelectedJobNo = True
                            sStatus = "" & ColJobFields.Item("JobStatus")("value")
                        End If '--record..-
                    Else
                        Beep()
                        MessageBox.Show("Selection Error- " & vbCrLf & _
                                        "  The Detail Job No doesn't match with the JobNo from selected Jobs Tree Node." & vbCrLf & _
                                        vbCrLf & "  Please retry the Job selection.", _
                                           "Checking Job No.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If  '-jobNo-
                End If '--key/job..-
            End If '--nothing.-
        ElseIf (LCase(pageX.Name) = "tabpagecustomers") Then  '= frameCustomers.Visible Then  '--showing CUSTOMERS..-
            '--  Look in jobs listview.-
            If (listViewCustJobs.Items.Count > 0) Then '--have jobs in view..-
                '== item1 = listViewCustJobs.Items.Item(0) '--look at 1st item..-
                '==3061.0= item1 = listViewCustJobs.FocusedItem
                If (Me.listViewCustJobs.SelectedItems.Count > 0) Then
                    item1 = Me.listViewCustJobs.SelectedItems(0)    '-- first (and only) selected item..
                End If
                If (item1 Is Nothing) Then '--no selection..-
                    Exit Function
                Else
                    If IsNumeric(item1.Text) Then
                        lngJobId = CLng(item1.Text)
                        If (lngJobId > 0) AndAlso (lngJobId = intDetailJobNo) Then
                            sStatus = item1.SubItems(1).Text '=3061.1==NOW the second column.. '== FOURTH column.-
                            mbGetSelectedJobNo = True
                        Else
                            Beep()
                            MessageBox.Show("Selection Error-" & vbCrLf & _
                                            "  The Detail Job No doesn't match with JobNo from the selected Jobs ListView row." & vbCrLf & vbCrLf & _
                                            "  Please retry the Job selection.", _
                                               "Checking Job No.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If  '--numeric..-
                End If  '--nothing.-
            End If  '--job count-
        Else
            Beep()
        End If '--browse/tree..-
        ColJobFields = Nothing
        colRowValues = Nothing
    End Function '--get jobno.-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Load jobMaint form -- Multiple Uses..--
    '-- Load jobMaint form -- Multiple Uses..--

    Private Function mbLoadJobMaintForm(ByRef lngJobNo As Integer, _
                                       ByRef frmJobMaint3A As frmJobMaint3) As Boolean '===, frmJobMaint As Form) As Boolean
        Dim L1 As Integer
        '== Dim frmJobMaint3A As New frmJobMaint3

        On Error GoTo loadMaintError
        '== Set frmJobMaint = New frmJobMaint2
 
        frmJobMaint3A.JobNo = lngJobNo
        frmJobMaint3A.connectionSql = mCnnSql
        '==frmJobMaint2.connectionJet = mCnnJet
        frmJobMaint3A.retailHost = mRetailHost1

        frmJobMaint3A.dbInfoSql = mColSqlDBInfo
        '== frmJobMaint2.dbInfoJet = mColJetDBInfo
        frmJobMaint3A.ServiceUpdate = False '-- request service type--
        frmJobMaint3A.DeliveryUpdate = False '-- delivery type--
        '== frmJobMaint3A.NotifyUpdate = False

        '== 3202.107== NOW DISCOVERED from within MAINT FORM.
        '== frmJobMaint3A.ColourPrinterName = msColourPrtName
        '= frmJobMaint3A.ReceiptPrinterName = msReceiptPrtName

        frmJobMaint3A.StaffId = mlStaffId
        frmJobMaint3A.StaffName = msStaffName
        frmJobMaint3A.StaffBarcode = msStaffBarcode

        frmJobMaint3A.UserLogo = Picture2.Image '--pass logo..-

        '== 3202.116== BUSINESS INFO NOW DISCOVERED from within MAINT FORM.
        '== frmJobMaint3A.GSTPercentage = msGSTPercentage

        '== 3202.107== BUSINESS INFO NOW DISCOVERED from within MAINT FORM.
        '== frmJobMaint3A.LabourHourlyRatePriority1 = mCurLabourHourlyRateP1
        '== frmJobMaint3A.LabourHourlyRatePriority2 = mCurLabourHourlyRateP2
        '== frmJobMaint3A.LabourHourlyRatePriority3 = mCurLabourHourlyRateP3
        '== frmJobMaint3A.LabourMinCharge = mCurLabourMinCharge

        '== frmJobMaint3A.DescriptionPriority1 = msDescriptionPriority1
        '== frmJobMaint3A.DescriptionPriority2 = msDescriptionPriority2
        '== frmJobMaint3A.DescriptionPriority3 = msDescriptionPriority3

        '== 3202.116== BUSINESS INFO NOW DISCOVERED from within MAINT FORM.
        '== frmJobMaint3A.BusinessABN = msBusinessABN
        '== frmJobMaint3A.BusinessName = msBusinessName
        '== frmJobMaint3A.BusinessShortName = msBusinessShortName
        '== frmJobMaint3A.BusinessAddress1 = msBusinessAddress1
        '== frmJobMaint3A.BusinessAddress2 = msBusinessAddress2
        '== frmJobMaint3A.BusinessState = msBusinessState
        '== frmJobMaint3A.BusinessPostCode = msBusinessPostCode
        '== frmJobMaint3A.BusinessPhone = msBusinessPhone

        '= frmJobMaint3A.ServiceChargeCat1 = msServiceChargeCat1
        '= frmJobMaint3A.ServiceChargeCat2 = msServiceChargeCat2
        '= frmJobMaint3A.NotificationCostLimit = mCurServiceNotificationCostLimit
        '= frmJobMaint3A.DeliveryDocketFootnote = msDeliveryDocketFootnote

        mbLoadJobMaintForm = True
        Exit Function

loadMaintError:
        L1 = Err().Number
        MsgBox("Runtime Error in Load jobMaint form function.." & vbCrLf & "Error is " & L1 & " = " & ErrorToString(L1))

        mbLoadJobMaintForm = False

    End Function '--loadJobMaint..-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  SHOW jobMaint Form..  All functions..--
    '--   form is load and primed, as above..-

    Private Function mbShowJobMaintform(ByVal bService As Boolean, _
                                          ByVal bDelivery As Boolean) As Boolean
        Dim lRow, lCol As Integer
        Dim s1, sStatus As String
        Dim lngJobId As Integer
        '== Dim frmJobMaint As Form
        Dim frmJobMaint3A As New frmJobMaint3

        mbShowJobMaintform = False
        lngJobId = -1
        If (msStaffName = "") Then Exit Function '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        '==3073==
        If Not mbLicenceOK Then
            MsgBox("No valid licence found for this JobMatix site.." & vbCrLf & _
                   "(See JobMatix website for Licence details..) ", MsgBoxStyle.Exclamation)
        End If

        If mbGetSelectedJobNo(lngJobId, sStatus) Then
            '==If (lngJobId > 0) Then
            mlJobId = lngJobId
            '-- Can't start if job still WaitListed..--
            '====s1 = Trim(ColRowValues("JobStatus")("value"))
            If (VB.Left(sStatus, 2) < "10") Then '-- '05' means still wait-listed..-
                MsgBox("This job can't start.. it is still Wait-Listed!", MsgBoxStyle.Exclamation)
                mlStaffTimeout = 0 '--NOW is timing out..--
                Exit Function
            End If
            '-- call Job Edit with colKeys..--
            '=====Call mbLoadJobMaintForm(CLng(colKeys(1)), frmJobMaint)
            Call mbLoadJobMaintForm(lngJobId, frmJobMaint3A) '==, frmJobMaint)
            frmJobMaint3A.CustomerDetails = mColRMCustomerDetails

            If bService Then
                frmJobMaint3A.ServiceUpdate = True '-- request service type--
                '==            ElseIf bNotify Then
                '==               frmJobMaint3A.NotifyUpdate = True
            ElseIf bDelivery Then
                frmJobMaint3A.DeliveryUpdate = True '-- delivery type--
            End If
            '==If Not (bService Or bNotify) Then

            '== 3202.116== BUSINESS INFO NOW DISCOVERED from within MAINT FORM.
            '== frmJobMaint3A.ItemBarcodeFontName = msItemBarcodeFontName
            '== frmJobMaint3A.ItemBarcodeFontSize = mlItemBarcodeFontSize

            '--position--
            frmJobMaint3A.MandatedFormTop = Me.Top + 30  '== 170  '== (Me.Height \ 5) + 50
            frmJobMaint3A.MandatedFormLeft = Me.Left + (Me.Width \ 5)

            VB6.ShowForm(frmJobMaint3A, VB6.FormShowConstants.Modal, Me)

            frmJobMaint3A.Close()
            '==Else  '--non-modal.-
            '==   frmJobMaint.Show vbModeless, Me
            '==End If
            frmJobMaint3A.Dispose()
            mbShowJobMaintform = True
            '==End If  '--count.-
        End If '--jobId..- '--not nothing, have keys..-
        '====End If  '--row ok--
        mlStaffTimeout = 0 '--NOW is timing out..--
    End Function '--show job..-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- Connect to Retail Manager..--
    '-- MULTI-HOST version--
    '-- MULTI-HOST version--
    '-- MULTI-HOST version--

    '-- sProviderCode is "RM" or "QBPOS".. --

    Private Function mbRMConnect(ByVal sProviderCode As String, _
                                 Optional ByVal bNewPath As Boolean = False) As Boolean
        '==Dim sSettingsDB As String
        Dim sSettingsUid As String
        Dim sSettingsPwd As String
        Dim bUpdateJet, bConnected As Boolean
        '== Dim bNewPath As Boolean
        Dim strSchema As String

        '== bNewPath = False
        mbRMConnect = False

        '== msJetDbName = mSdSystemInfo.Item(UCase(msJetPathInfoKey))
        '== msJetUid = mSdSystemInfo.Item("RM_JETUSERNAME")
        '== msJetPwd = mSdSystemInfo.Item("RM_JETPASSWORD")

        If sProviderCode = "RM" Then
            msJetPathInfoKey = "RM_JetPath_" & msComputerName
            sSettingsUid = "RM_JETUSERNAME"
            sSettingsPwd = "RM_JETPASSWORD"
        ElseIf (sProviderCode = "QBPOS") Then
            msJetPathInfoKey = "QBPOS_JetPath_" & msComputerName
            sSettingsUid = "QBPOS_JETUSERNAME"
            sSettingsPwd = "QBPOS_JETPASSWORD"
            '==3083== labRetailHostPrompt.Text = "QuickbooksPOS Database:"
            '==3083== labRetailHostPrompt.BackColor = Color.Orange
        Else
            MsgBox("Invalid Provider code in RMConnect..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        msJetDbName = mSysInfo1.item(UCase(msJetPathInfoKey))
        msJetUid = mSysInfo1.item(sSettingsUid)
        msJetPwd = mSysInfo1.item(sSettingsPwd)

        '--  can request browse for new DB..
        If bNewPath Then msJetDbName = "" '--force new path..-

        '--connect to jet db..--
        '==3083== labRetailHostPrompt.Visible = True
        bUpdateJet = False
        If (msJetDbName = "") Then bUpdateJet = True

        bConnected = False
        While Not bConnected
            If Not mRetailHost1.connect("", msJetDbName, msJetUid, msJetPwd, bNewPath, strSchema) Then
                If (MsgBox("No Retail Host connection.." & vbCrLf & _
                             "Do you want to retry", _
                              MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) <> MsgBoxResult.Yes) Then
                    Exit Function
                End If
            Else  '--ok-
                bConnected = True
            End If
        End While

        '==   Target-New-Build-6201 --  (18-June-2021)
        Call glSaveTextFile(gsJobMatixLocalDataDir("JobMatix62") & "\RetailM_schema.txt", strSchema)
        '= Call glSaveTextFile(gsJobMatixLocalDataDir("JobMatix42") & "\RetailM_schema.txt", strSchema)
        '==  END Target-New-Build-6201 --  (18-June-2021)

        On Error GoTo 0

        '-- save jet parms in sysinfo..--
        If bUpdateJet Or bNewPath Then
            If Not mSysInfo1.UpdateSystemInfo(New Object() {msJetPathInfoKey, msJetDbName, sSettingsUid, msJetUid, sSettingsPwd, msJetPwd}) Then
                MsgBox("Failed to update JET details in systemInfo table..", MsgBoxStyle.Critical)
            End If
         End If '--update jet..-

        mbRMConnect = True
    End Function '--RM-connect..-
    '= = = = = = =
    '-===FF->

    '- B a c k g r o u n d  W o r k e r  Threads --
    '- B a c k g r o u n d  W o r k e r  Threads --


    '- Background worker thread to look for Sql servers..--
    '--  Started from Form Load event routine..
    '-   This event handler is where the actual BG work is done.
    '-- http://msdn.microsoft.com/en-us/library/System.ComponentModel.BackgroundWorker(v=vs.80).aspx  

    Private Sub backgroundWorkerSearch_DoWork(ByVal sender As Object, _
                                               ByVal e As DoWorkEventArgs) _
                                              Handles BackgroundWorkerSearch.DoWork

        ' Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        ' Assign the result of the computation
        ' to the Result property of the DoWorkEventArgs
        ' object. This is will be available to the 
        ' RunWorkerCompleted eventhandler.
        '== e.Result = ComputeFibonacci(e.Argument, worker, e)

        mbSqlServerSearchOk = gbSQL_Enumerate_Main(mColSQLServerInstances)

    End Sub '- backgroundWorkerSearch_DoWork
    '= = = = = = = = = = = = = = = = = = = =  =

    '- Background worker thread to Sql server DB schema..--
    '--  Started from Form Load event routine..
    Private Sub backgroundWorkerGetSchema_DoWork(ByVal sender As Object, _
                                              ByVal e As DoWorkEventArgs) _
                                             Handles BackgroundWorkerGetSchema.DoWork
        ' Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        mbSqlServerGetSchemaOK = gbGetSqlSchemaInfo(mCnnSql, msSqlDbName, mColSqlDBInfo, msBuildSchemaLog)

    End Sub  '- backgroundWorkerGetSchema_DoWork--
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '=BackgroundWorker Exchange Calendar Updating..
    '--  Re- Started from coming back from NewJob Form with file name propery. set up...
    '-- SQL connection is argument.
    '==    >> 3501.0625 25-June-2018= (ported from 3431.0622- )
    '==       --  Exchange BG task- Detect invalid XML file data eg. reserved chars (eg Amoersand etc.)...
    '==
    '==    3501.0708 08-July-2018= (Updates from 3431.0707- )
    '==       -- Exchange201 Updates from 3431.0707-
    '==
    '== Target-Build-4284  (Started 23-Nov-2020)
    '==   -- Cancel Job (frmNewJob etc)..  Update Calendar to delete the Appointment..
    '==

    Private mbExchange201WorkerIsActive As Boolean = False  ''-Updated ONLY by REportProgress.
    Private mIntInitialSleepSeconds As Integer = 120
    '-- Searches Data dir for Xml Calendar Update files..

    Private Sub backgroundWorkerExchange201_DoWork(ByVal sender As Object, _
                                              ByVal ev As DoWorkEventArgs) _
                                             Handles BackgroundWorkerExchange201.DoWork
        ' Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        Dim directory1 As DirectoryInfo
        Dim aFiles() As FileInfo
        Dim xmlReader1 As XmlReader
        Dim dtFileDate As DateTime
        Dim sXmlFileFullPath As String
        Dim sResult As String = ""
        Dim sResult2 As String = ""
        Dim sFailedJobs As String = ""
        '-xml stuff-
        Dim settings As New XmlReaderSettings()
        '=Dim sBody, sSubject, sEmailText As String
        Dim clsSystemInfo1 As clsSystemInfo
        Dim cnnSql As OleDbConnection = CType(ev.Argument, OleDbConnection)
        Dim colResults As New Collection

        worker.ReportProgress(0)  '--say started (sleeping)...  1%..
        '-- Wait for 10 secs to not conflict over active flag..
        '-- Startup stuff first--
        Call gbLogMsg(gsRuntimeLogPath, "Exchange201 BG Worker Thread started.." & vbCrLf)
        Thread.Sleep(mIntInitialSleepSeconds * 1000)  '--msecs-
        '==
        worker.ReportProgress(1)  '--say started (working)...  1%..

        '-- look for xml calendar update files-

        settings.ConformanceLevel = ConformanceLevel.Fragment
        settings.IgnoreWhitespace = True
        settings.IgnoreComments = True

        Dim sMsg As String
        Dim sExchangeFullPath As String = Trim(gsJobMatixLocalDataDir(msJobmatixAppName))  '-defaults to Assembly name.
        '= Dim sXmlFileTitle As String = "Exchange20_Appt_JobNo_" & CStr(mlJobId) & ".xml"
        Dim sXmlFileXml As String = ""
        sExchangeFullPath &= "\temp"

        '=3431.0523=
        '= If no directory, then nothing's happened yet..
        If Not My.Computer.FileSystem.DirectoryExists(sExchangeFullPath) Then  '-no work.. we can exit..-
            worker.ReportProgress(99)  '--say exited.
            Exit Sub  '-My.Computer.FileSystem.CreateDirectory(sExchangeFullPath)
        End If '-- exists dir.-

        Try
            directory1 = New DirectoryInfo(sExchangeFullPath)
        Catch ex As Exception
            sMsg = "** ERROR: Exchange20 Calendar BG Worker- " & vbCrLf & _
                    "  Failed to get ExchangeFullPath Directory Info.." & vbCrLf & ex.Message & vbCrLf
            colResults.Add(sMsg, "results_msg")
            ev.Result = colResults
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            worker.ReportProgress(99)  '--say exited.
            Exit Sub
        End Try
        '--get list of files.
        aFiles = directory1.GetFiles("Exchange20*.xml")
        '-- exit if nothing to do..
        If (aFiles.Length <= 0) Then
            colResults.Add("", "results_msg")
            ev.Result = colResults
            worker.ReportProgress(99)  '--say exited.
            Exit Sub
        End If
        '-- stuff to do.. 
        worker.ReportProgress(2)  '--say started (working)...  1%..

        '-- SORT by date modified..
        '== FROM the Net-  I’ve been forced in to using vb.net for a windows service project which scans a folder of xml 
        '== files, they need to be processed in order of the files modified date & time.  Directory.GetFiles() 
        '== returns an array of filenames in alphabetic order, I could find precious little information on the 
        '== net so I thought I’d share what I came up with :-
        '==  http://geekswithblogs.net/ntsmith/archive/2006/08/17/88250.aspx
        Array.Sort(aFiles, New clsCompareFileInfo)

        'For Each fl As FileInfo In aFiles
        '    MsgBox(fl.FullName.ToString() & "; " & fl.LastWriteTime.ToString, MsgBoxStyle.Information)
        'Next
        Dim datePromisedOld, datePromisedNew As Date
        Dim bIsNewJob, bStartedOk As Boolean
        Dim sContent, sName, sTextMsg As String
        Dim intJobNo As Integer
        Dim sBody, sCust, sLocation, sTechName As String
        Dim strDatePromisedOld, strDatePromisedNew As String
        Dim decDuration As Decimal
        Dim colFailedFiles As Collection

        '== Target-Build-4284  (Started 23-Nov-2020)
        Dim sRequestType As String
        '== END Target-Build-4284  (Started 23-Nov-2020)


        clsSystemInfo1 = New clsSystemInfo(cnnSql)
        colFailedFiles = New Collection

        '-- get each file and Make/Update Appointment from xml info..
        For Each file1 As FileInfo In aFiles
            sXmlFileFullPath = sExchangeFullPath & "\" & file1.Name
            dtFileDate = File.GetLastWriteTime(sXmlFileFullPath)
            '= MsgBox("Found: " & sFileFullPath & vbCrLf & _
            '=                  "Last Modified: " & Format(dtFileDate, "dd-MMM-yyyy hh:mm tt"), MsgBoxStyle.Information)
            '-- Get Job info from xml file and send to updater....
            '-- don't process if truncated-
            If (file1.Length <= 0) Then
                Continue For  '--got to next-
            End If
            Try
                xmlReader1 = XmlReader.Create(sXmlFileFullPath, settings)
            Catch ex As Exception
                sMsg = "** ERROR: Failed to Create XML reader for Exchange Update file- " & vbCrLf & _
                         "'" & sXmlFileFullPath & "' " & vbCrLf & ex.Message & vbCrLf
                colResults.Add(sMsg, "results_msg")
                ev.Result = colResults
                Call gbLogMsg(gsRuntimeLogPath, sMsg)
                worker.ReportProgress(99)  '--say exited.
                Exit Sub  '-ABORT_  Continue For '= Exit Function
            End Try
            sBody = ""
            sCust = "" : sTechName = ""  '= : sEmailText = ""

            '== Target-Build-4284  (Started 23-Nov-2020)
            sRequestType = ""
            '== END Target-Build-4284  (Started 23-Nov-2020)

            decDuration = 1  '--default-
            '= sTargetName = ""
            bStartedOk = False
            Try
                While xmlReader1.Read()
                    If xmlReader1.IsStartElement() Then
                        If xmlReader1.IsEmptyElement Then
                            sTextMsg &= xmlReader1.Name & vbCrLf '= Console.WriteLine("<{0}/>", xmlReader1.Name)
                        Else
                            sName = xmlReader1.Name
                            sTextMsg &= sName & vbCrLf '= Console.Write("<{0}> ", xmlReader1.Name)
                            If (LCase(sName) = "exchange_appointment") Then
                                bStartedOk = True   ''-- read to first inside.
                            ElseIf bStartedOk Then  '-into the subs..
                                xmlReader1.Read() ' Read the start tag.
                                If xmlReader1.IsStartElement() Then ' Handle nested elements.
                                    sTextMsg &= xmlReader1.Name & vbCrLf
                                    '=   Console.Write(vbCr + vbLf + "<{0}>", xmlReader1.Name)
                                End If
                                '-- Read the text content of the element.
                                sContent = Trim(xmlReader1.ReadString)  '= & vbCrLf
                                sTextMsg &= sContent & vbCrLf  '= Console.WriteLine(xmlReader1.ReadString()) 
                                Select Case LCase(sName)
                                    Case "appt_jobno"
                                        intJobNo = CInt(sContent)
                                    Case "appt_isnewjob"
                                        bIsNewJob = IIf((UCase(sContent) = "Y"), True, False)
                                    Case "appt_techname"
                                        sTechName = sContent
                                    Case "appt_body"
                                        sBody = sContent
                                    Case "appt_cust"
                                        sCust = sContent
                                    Case "appt_datepromisedold"
                                        strDatePromisedOld = sContent
                                        datePromisedOld = CDate(strDatePromisedOld)
                                    Case "appt_datepromisednew"
                                        strDatePromisedNew = sContent
                                        datePromisedNew = CDate(strDatePromisedNew)
                                    Case "appt_duration"
                                        If IsNumeric(sContent) Then
                                            decDuration = CDec(sContent)
                                        End If
                                    Case "appt_requesttype"

                                        '== Target-Build-4284  (Started 23-Nov-2020)
                                        sRequestType = sContent
                                        '== END Target-Build-4284  (Started 23-Nov-2020)

                                    Case Else
                                End Select
                            Else  '-wrong-  no opening tag.

                            End If
                        End If  '-empty-
                    End If  '-start element=
                End While '-read-
            Catch ex As Exception
                xmlReader1.Close()
                sMsg = "** ERROR: Failed in reading xml Calendar update file for Onsite Job..- " & vbCrLf & _
                                    "'" & sXmlFileFullPath & "' " & vbCrLf & ex.Message & vbCrLf & _
                                       "(File will be renamed with .Error suffix..)" & vbCrLf
                '- rename file..
                Try
                    My.Computer.FileSystem.RenameFile(sXmlFileFullPath, file1.Name & ".Error")
                Catch ex2 As Exception
                    sMsg &= vbCrLf & "==== ERROR- FAILED to rename file- " & vbCrLf & _
                             "'" & sXmlFileFullPath & "' " & vbCrLf & ex2.Message & vbCrLf & _
                                  vbCrLf & "This xml file should be deleted by operator.."
                End Try
                colResults.Add(sMsg, "results_msg")
                ev.Result = colResults
                Call gbLogMsg(gsRuntimeLogPath, sMsg)
                worker.ReportProgress(99)  '--say exited.
                Exit Sub  '-ABORT_  Continue For '= Exit Function
            End Try
            xmlReader1.Close()
            '-- ok.. send to calendar.

            '== Target-Build-4284  (Started 23-Nov-2020)
            '-- TEMP--  Override bIsNewJob by "RequestType"..
            '-    THEN  Send RequestType to updater....
            bIsNewJob = False
            If (LCase(sRequestType) = "newjob") Then
                bIsNewJob = True
            End If
            '== END Target-Build-4284  (Started 23-Nov-2020)

            '== Target-Build-4284  (Started 04-Nov-2020)
            '==   Replaces bIsNewJob with sRequestType..
            If Not gbUpdateOnsiteCalendar(intJobNo, sTechName,
                                      sBody, sCust, "--",
                                            datePromisedOld, datePromisedNew, decDuration, clsSystemInfo1, sRequestType) Then
                sMsg = "Exchange20 Calendar ERROR: " & vbCrLf &
                        "Failed to add/Update ON-SITE Appointment for JobNo: " & intJobNo & ".." &
                                                                   vbCrLf & gsGetLastExchangeErrorMsg()
                sResult &= sMsg
                '= ev.Result = sMsg
                '-- add to failed list for later.
                If (sFailedJobs <> "") Then sFailedJobs &= ", "
                sFailedJobs &= CStr(intJobNo)
                colFailedFiles.Add(sXmlFileFullPath)
                Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & "= = = =")
                '= Exit Sub
            Else '-- ok- 
                sMsg = "OK- Exchange20 ON-SITE Appointment for Job: " & intJobNo & vbCrLf & _
                       " was added/updated to the Calendar." & vbCrLf & _
                                       "Result Text is:" & vbCrLf & gsGetLastExchangeErrorMsg()
                sResult &= sMsg
                Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & "= = = =")
                '-- Can delete xml file now..-
                '-- delete PDF and XML files..
                '= fsXml.SetLength(0)  '--kill the content before we close and remove.
                '- fsXml.Close()
                Try
                    File.Delete(sXmlFileFullPath)
                Catch ex As Exception
                    sMsg = "** ERROR- Failed to DELETE Exchange20 Xml file.." & vbCrLf & ex.Message & vbCrLf & _
                             vbCrLf & "Another task may be accessing the file.."
                    Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & "= = = =")
                End Try

            End If  '-send-

            '- separate job results..
            '== Target-build-4267  (Started 07-Sep-2020)
            '== Target-build-4267  (Started 07-Sep-2020)
            '- separate all the job results..
            sResult &= vbCrLf & "= = = =" & vbCrLf
            '== END Target-build-4267  (Started 07-Sep-2020)
            '== END Target-build-4267  (Started 07-Sep-2020)

        Next file1

        '-- when done-
        sResult2 = "Exchange20 BG Worker Thread is exiting.." & vbCrLf & _
                    "Results are:" & vbCrLf & sResult & vbCrLf & _
                    "(Results are also logged on the JobMatix runtime log:" & vbCrLf & "'" & gsRuntimeLogPath & "'..)" & vbCrLf
        colResults.Add(sResult2, "results_msg")
        If (sFailedJobs <> "") Then
            colResults.Add(sFailedJobs, "results_failed")
            'sResult2 &= vbCrLf & "Note:  Jobs Nos. " & sFailedJobs & " failed to update the calendar." & vbCrLf & _
            '                    "-- Checking the Exchange user credentials may be useful.."
            colResults.Add(colFailedFiles, "FailedFiles")
        End If
        ev.Result = colResults  '= sResult2
        worker.ReportProgress(100)  '--say finished.  100%..

    End Sub  '- backgroundWorkerGetSchema_DoWork--
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->


    Private Function mbFindSQlServer(ByRef sServerInstance As String) As Boolean
        Dim ix As Integer
        Dim col1 As Collection
        Dim sMsg, s1 As String

        mbFindSQlServer = False
        '-- Now.. Find an Sql Server on the network..-
        Call mWaitFormOn("Pls Wait.  Checking for Sql Servers.." & vbCrLf & _
                           "   This might take a moment.")
        '-- search..-
        mbSqlServerSearchOk = False
        '==  bOk = gbSQL_Enumerate_Main(mColSQLServerInstances)
        '-- Start the Sql Search operation in the background.
        Me.BackgroundWorkerSearch.RunWorkerAsync()
        '-- wait for completion-
        '-- ie Wait for the BackgroundWorker to finish the Search.
        While Me.BackgroundWorkerSearch.IsBusy
            '- Keep UI messages moving, so the WAIT form remains 
            '-     responsive during the asynchronous operation.
            Application.DoEvents()
        End While

        Call mWaitFormOff()  '--hide wait form-
        Application.DoEvents()
        If Not mbSqlServerSearchOk Then
            MsgBox("ERROR- Sql Server Search Failed !!!", MsgBoxStyle.Exclamation)
            '==  Me.Close()
            Exit Function
        End If
        If (Not (mColSQLServerInstances Is Nothing)) AndAlso (mColSQLServerInstances.Count > 0) Then
            '- CHOOSE a server and do the CONNECT..--
            If (mColSQLServerInstances.Count = 1) Then  '--only one..-
                col1 = mColSQLServerInstances(1)
                '== msServer = col1("ServerName") & "\" & col1("InstanceName")
                '= 3107.922- Fixed..
                sServerInstance = col1("ServerName") & "\" & col1("InstanceName")
                mbFindSQlServer = True
                Application.DoEvents()
                '== MsgBox("Found one SQL server..", MsgBoxStyle.Information)
            Else  '--more than 1..  must choose..
                '== MsgBox("Found " & mColSQLServerInstances.Count & " SQL servers..", MsgBoxStyle.Information) 'TEST-
                sMsg = ""
                ix = 0
                For Each col1 In mColSQLServerInstances
                    ix += 1
                    sMsg &= vbCrLf & CStr(ix) & ". " & col1("ServerName") & "\" & col1("InstanceName")
                Next
                sMsg = "Found " & mColSQLServerInstances.Count & " SQL servers.." & vbCrLf & _
                                     "Please choose a server instance." & vbCrLf & vbCrLf & sMsg
                '-- Choose.--
                s1 = InputBox(sMsg, "JobMatixPOS Sql Servers")
                If (s1 <> "") AndAlso IsNumeric(s1) AndAlso _
                             (CInt(s1) > 0) AndAlso (CInt(s1) <= mColSQLServerInstances.Count) Then
                    col1 = mColSQLServerInstances(CInt(s1))
                    sServerInstance = col1("ServerName") & "\" & col1("InstanceName")
                    mbFindSQlServer = True
                ElseIf (s1 = "") Then
                    Me.Close()
                    Exit Function
                End If
            End If '-only one-
            '-- connect to selected sql server
        Else  '--no servers.
        End If  '-found server..
    End Function  '-- mbFindSQlServer-
    '= = = = = =  = = = = = = = = =  
    '-===FF->

    '-- SELECT DB  -

    '--SelectDatabase--
    '--  Get all accessible POS DB's, and select one..

    '== 3501.0610=  10June-2018=
    '--  ALL this done by the shared common POS/JT startup (project JobMatix35.)


    '--startup jobs===
    '--startup jobs===

    '-- Form now called from Sub Main.
    '--  Sql Server and Connection comes in with constructor..

    Private Sub frmJobMatix3_Load(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '==Private Sub mbOriginal_Load()



        '==
        '==  NEW BUILD- 4219 VERSION
        '==    Updated- 4219.1122 22-Nov-2019= 
        '==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll..

        '=-TEESTING DLL JMxRetailHost.dll --
        '= Dim clsRetailHost_class1 As JMxRetailHost.RetaiHost_Class1
        '= clsRetailHost_class1 = New JMxRetailHost.RetaiHost_Class1
        '-- done.



        '= Dim ix, lngStart As Integer
        '= Dim iPos As Short
        '= Dim lngError, lngResult As Integer
        Dim s1, sAppPath, sErrors As String
        Dim sShowMsgKey, sHdr, sMsgText As String
        Dim sName, sCmdLine, sConnect, sSchemaInfo As String
        '== Dim nodeX, nodeY As System.Windows.Forms.TreeNode
        '== Dim dlgDoNotShow1 As dlgNoShow
        Dim bShow As Boolean = False
        Dim bConnected As Boolean = False
        '= Dim bESCapeRequested As Boolean
        Dim rs1 As DataTable   '== ADODB.Recordset
        '= Dim colAllJobsDBs, colUserJobsDBs As Collection
        Dim v1 As Object

        mbIsInitialising = True   '--static vlaue didn't work.
        Me.Text = "Can't start JobMatix maximised.."
        If (Me.WindowState = FormWindowState.Maximized) Then
            '==MsgBox("Can't start up maximised..", MsgBoxStyle.Exclamation)
            Beep()
            Me.Close()
            Me.Dispose()
            Exit Sub '=End
            '== Exit Sub '--still loading..-
        End If
        Me.Text = "JobMatix JobTracking.."
        '== mbActive = False
        '== mbCreateMode = False
        '=  Me.Opacity = 0
        '== Me.Visible = False
        mbStartingUp = True

        txtStaffBarcode.Enabled = False  '--Start disabled to stop Enter..

        mbSuperAdmin = False
        mbPwCancelled = False
        mbPwCompletedOK = False
        mbIsInstalling = False  '=3107.706=
        '-- setup.. user chooses..
        mbInstallingJobMatixPOS = False   '=3311.328-
        '-- check if not already running..--
        '--  SEE "Multiple Instances" property on Project "Applocation" Tab..--
        '== If App.PrevInstance Then
        '== MsgBox("JobTracking program is already running..", MsgBoxStyle.Exclamation)
        '== Me.Close()
        '== End
        '== End If
        sCmdLine = Trim(VB.Command())
        gbVerbose = False
        If InStr(1, UCase(sCmdLine), "/V") > 0 Then gbVerbose = True
        gbDebug = False
        If InStr(1, UCase(sCmdLine), "/SUPERADMIN") > 0 Then gbDebug = True
        gbDevel = gbDebug
        '=3107.706=
        If gbGetCmd(sCmdLine, "IsInstalling", s1) Then
            mbIsInstalling = True
            '=3311.328-  Setup sends choice- RM/JobMatixPOS-
            If gbGetCmd(sCmdLine, "InstallJobMatixPOS", s1) Then
                mbInstallingJobMatixPOS = True
            End If
        End If

        'sAppPath = My.Application.Info.DirectoryPath
        'If VB.Right(sAppPath, 1) <> "\" Then sAppPath = sAppPath & "\"
        'gsAppPath = sAppPath
        '==

        '=3501.0616=
        '-- MUST SET THIS FIRST..
        '-- Appname (top level assembly name) now comes in as New Constructor parameter.
        Call gSubSetAppName(msJobmatixAppName)

        sAppPath = gsAppPath()
        If VB.Right(sAppPath, 1) <> "\" Then sAppPath = sAppPath & "\"
        msAppPath = sAppPath
        '== msJobmatixAppName = gsGetAppName()

        '-  new log each month..-
        s1 = Format(CDate(DateTime.Today), "yyyy-MM-dd")

        '= 3107.801= gsErrorLogPath = gsAppPath & "JTv3-Runtime-" & VB.Left(s1, 7) & ".log"

        '== grh JobMatix v3.5.3501.0615
        '==   -- 15-June-2018=
        '==     --  Created New Module "modAllFileAndSqlSubs"..
        '==           to combine All fileSupport and sql support Functions.
        '=--- gsErrorLogPath and gsRuntimeLogPath are now public FUNCTIONS..
        '== gsErrorLogPath = gsJobMatixLocalDataDir("JobMatix35") & "\JobMatix35-Runtime-" & VB.Left(s1, 7) & ".log"
        '== gsRuntimeLogPath = gsErrorLogPath  '--gsAppPath & "JTv3_Runtime.log"

        Call gbLogMsg(gsRuntimeLogPath, "=== JMxJT420 Main form is loading..")

        '--mbLoggedOn = False
        '--cmdMaint.Enabled = False
        '== mnuDatabase.Visible = False

        mnuAdmin.Enabled = False
        '= mnuStaySignedOn.Checked = True
        '= mnuAutoSignOffOptions.CheckState = CheckState.Unchecked
        mnuLongSignOff.Checked = True
        mIntStaffTimeoutInterval = 300

        mnuDontShowNotifyReminder.Checked = True
        '= mnuDontShowNotifyReminder.CheckState = CheckState.Unchecked
        Timer1.Enabled = False
        labStaffTimeRemaining.Text = ""  '=3411.0302-

        '-- set default Jobs Tree Font..-
        mFontTvwJobs = New Font("Lucida Sans", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        tvwJobs.Font = New Font("Lucida Sans", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        '== mnuGridFont_8.Checked = True
        '== mSingleNewFontSize = 8.25!
        mnuGridFont_9.Checked = True
        mSingleNewFontSize = 9  '==8.25!

        '-- FORCE datagridView cell style to stick..-
        mDataGridViewCellStyleHdr = New DataGridViewCellStyle
        mDataGridViewCellStyleData = New DataGridViewCellStyle

        '--  dataGrids..  set header row styles..
        mDataGridViewCellStyleHdr.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        mDataGridViewCellStyleHdr.BackColor = System.Drawing.SystemColors.Control
        mDataGridViewCellStyleHdr.Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleHdr.ForeColor = System.Drawing.SystemColors.WindowText
        mDataGridViewCellStyleHdr.SelectionBackColor = System.Drawing.SystemColors.Highlight
        mDataGridViewCellStyleHdr.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        mDataGridViewCellStyleHdr.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        '- set for Jobs.-
        Me.DataGridViewJobs.ColumnHeadersDefaultCellStyle = mDataGridViewCellStyleHdr
        Me.DataGridViewJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize

        '=3431.0430-- drop the cell style and the sizing mode thing..
        '==Me.DataGridViewCust.ColumnHeadersDefaultCellStyle = mDataGridViewCellStyleHdr
        '= Me.DataGridViewCust.ColumnHeadersHeightSizeMode = _
        '=          System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize

        '--  dataGrids..  set DATA row styles..
        mDataGridViewCellStyleData.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        mDataGridViewCellStyleData.BackColor = System.Drawing.SystemColors.Window
        '= mDataGridViewCellStyleData.Font = New Font("Verdana", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleData.Font = New Font("Lucida Sans", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleData.ForeColor = System.Drawing.SystemColors.ControlText
        mDataGridViewCellStyleData.SelectionBackColor = System.Drawing.SystemColors.Highlight
        mDataGridViewCellStyleData.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        mDataGridViewCellStyleData.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewJobs.DefaultCellStyle = mDataGridViewCellStyleData
        Me.DataGridViewCust.DefaultCellStyle = mDataGridViewCellStyleData

        DataGridViewJobs.MultiSelect = False   '==3061.0 ==

        '==ON-SITE- 3083=
        Me.dataGridViewOnSite.ColumnHeadersDefaultCellStyle = mDataGridViewCellStyleHdr
        Me.dataGridViewOnSite.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.dataGridViewOnSite.ColumnHeadersHeight = 33

        mDataGridViewCellStyleData.WrapMode = System.Windows.Forms.DataGridViewTriState.True
        Me.dataGridViewOnSite.DefaultCellStyle = mDataGridViewCellStyleData
        '== Me.dataGridViewOnSite.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
        Me.dataGridViewOnSite.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dataGridViewOnSite.MultiSelect = False

        '--LabReportName.Visible = False
        labStatus.Text = ""
        '---labStatus.Top = LabReportName.Top

        msJetDbName = ""
        '== msServer = ""

        msJobsUserName = "" '--"precise7"   '--normal Jobs user login..--
        msJobsUserPwd = "" '--"stillworkingonit"

        msSqlUid = "" '--msJobsUserName  '--normal signon if server name exits..--
        msSqlPwd = "" '--msJobsUserPwd

        msJetUid = "admin" '--jet-
        msJetPwd = "" '--jet-
        '-- Prefs for browser....-

        '-- JOBMATIX2--   Prefs for Deliver/Viewing..-
        Call gbMakeCollection(New Object() {"Job_id", "CustomerName", "CustomerCompany", "Priority", _
                                                         "JobStatus", "DateCreated", "DateUpdated"}, mColPrefsDeliver)

        '--CmdCopy.Enabled = False
        ListResults.Visible = False
        msBusinessUser = ""
        msBusinessABN = ""
        msGSTPercentage = "0"
        mCurGSTPercentage = 0
        '==3311.331= mCurLabourHourlyRateP1 = 0
        '==3311.331= mCurLabourHourlyRateP2 = 0
        '==3311.331= mCurLabourHourlyRateP3 = 0
        mCurLabourMinCharge = 0

        mlItemBarcodeFontSize = 10 '--default..--
        msServiceChargeCat1 = "SERVCE" '--default..-
        msServiceChargeCat2 = ""
        '===msJobLabelPrintDepth = ""   '-- label actual depth mm..--
        '===msJobLabelGapDepth = ""     '-- label GAP depth mm..--

        msLicenceKey = ""
        '== msLicenceKeyLevel2 = ""
        mbLicenceOK = False
        mbIsFullLicence = False
        '===cmdClose.Enabled = False

        '--  !! SEE  Designer module (initialise)..-
        '-== mlResultsTop = VB6.PixelsToTwipsY(frameJobsTab.Top) '==SSTabMain.Top    '-- FrameBrowse.Top    '--save..--
        '== mlResultsLeft = VB6.PixelsToTwipsX(frameJobsTab.Left) '== SSTabMain.Left     '--  FrameBrowse.Left  '--save..--

        msStaffBarcode = ""
        '==3083== txtStaffName.Text = ""
        '==3083== LabStaffName.Text = ""
        '==3083== LabEnterStaffId.Visible = True
        '==3083== LabEnterStaffId.Enabled = False
        mlStaffTimeout = -1 '--not timing out..--

        msColourPrtName = ""
        msReceiptPrtName = ""
        msLabelPrtName = ""

        '-=mlFormDesignHeight = VB6.PixelsToTwipsY(Me.Height) '--save starting dimensions..-
        '= mlFormDesignWidth = VB6.PixelsToTwipsX(Me.Width) '--save starting dimensions..-

        '-- 3401.315-  Now in pixels-
        mlFormDesignHeight = Me.Height '--save starting dimensions..-
        mlFormDesignWidth = Me.Width '--save starting dimensions..-


        '= mnuReports.Enabled = False
        '==3083== txtStaffName.Enabled = False
        labReminder.Text = ""
        mColorReminderPanel = panelReminder.BackColor  '--save for Normal remider.
        '==331.402=
        labOnSiteReminder.Text = ""
        labOnSiteReminder.Left = labReminder.Left
        labOnSiteReminder.Visible = False

        FrameBrowse.Text = ""
        DataGridViewJobs.Enabled = False
        DataGridViewCust.Enabled = False

        '== FrameBrowse.Visible = False
        '==3311.405= cmdLaunchRAs.Enabled = False   '=3103.423=
        TabControlMain.Enabled = False

        mLngGridBGColour = System.Drawing.ColorTranslator.ToOle(DataGridViewJobs.BackColor) '--save.-

        labFindCust.Text = ""
        labRecCountCust.Text = ""
        txtSearch.Text = ""
        txtCustSearch.Text = ""

        msQuoteChassisCat1 = ""
        msQuoteChassisCat2 = ""

        '==NOT USED=  cmdClose.Enabled = False
        '==3083== labRetailHostPrompt.Visible = False

        msUseBrowserMsg = "The Jobs Explorer Tree shows jobs in groups based on JobStatus (workflow).  Right-click on any job to " & _
                                "see the menu of available actions."
        txtDetailsHdr.Text = ""
        '=====txtDetailsNotifications.Text = ""
        labMainCustTags.Text = ""  '==4221.0207-

        frameJobsTab.Text = ""
        FrameJobsTree.Text = ""
        FrameJobsTree2.Text = ""
        '== frameMainCmds.Text = ""
        frameLegend.Text = ""
        '=3083.404=
        panelReminder.Visible = False
        frameQuoteCustomer.Text = ""

        FrameJobDetails.Visible = False
        '==frameJobHdr.Visible = False     '==3083==

        '--  load user logo..--
        Picture2.Visible = False '--container only..-
        '-- load biz. logo FOR PRINTING..--
        '--  can accept GIF, JPG or BMP..--
        msInstallPath = sAppPath  '= ""
        '==3311.330= s1 = gsGetInstallAppPath("JobMatix33.exe") '--get jobtracking install path.
        '==3311.330= If (s1 <> "") Then '--extract path.-
        '==3311.330= iPos = InStr(LCase(s1), "JobMatix33.exe")
        '==3311.330= If iPos > 0 Then
        '==3311.330= msInstallPath = VB.Left(s1, iPos - 1) '--get directory..-
        '==3311.330= If (VB.Right(msInstallPath, 1) <> "\") Then msInstallPath = msInstallPath & "\"
        '==3311.330= End If
        '==3311.330= End If
        sName = Dir(sAppPath & "userlogo*.gif") '--First look in current diectory..-
        If (sName = "") Then sName = Dir(sAppPath & "userlogo*.jpg")
        If (sName = "") Then sName = Dir(sAppPath & "userlogo*.bmp")
        If (sName = "") Then sName = Dir(sAppPath & "userlogo*.png")
        '==3311.330= If (sName = "") And (msInstallPath <> "") Then '--look in install path.-
        '==3311.330= sName = Dir(msInstallPath & "userlogo*.gif")
        '==3311.330= If (sName = "") Then sName = Dir(msInstallPath & "userlogo*.jpg")
        '==3311.330= If (sName = "") Then sName = Dir(msInstallPath & "userlogo*.bmp")
        '==3311.330= End If
        If (sName = "") Then '--no logo-
            '--Picture1.Visible = False  '--Kepp jobMatix..-
            Picture2.Image = Picture3.Image '=== Picture1.Picture  '--use jobmatix for print.-
        Else '--ok-
            '==On Error Resume Next
            Try
                Picture2.Image = System.Drawing.Image.FromFile(sName)
                msUserLogoPath = sName
            Catch ex As Exception
                MsgBox("Failed to load user business logo: '" & sName & "'.." & vbCrLf & _
                        "Error: " & ex.Message, MsgBoxStyle.Exclamation)
                Picture2.Image = Picture3.Image '=== Picture1.Picture  '--use jobmatix for print.-
            End Try
        End If '--dir ok..-
        LabDeliveredOrder.Text = "NOTE:" & vbCrLf & _
                               " '+ nn' Number of days in Custody."

        '== 3067.0 ==
        s1 = Dir(msAppPath & "JobMatix.chm")
        If (s1 <> "") Then
            HelpProvider1.HelpNamespace = s1
            HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
            HelpProvider1.SetHelpKeyword(Me, "JT3-MainScreen.htm")
            Call gbSetHelpFileName("JobMatix3.chm")
        End If
        LabBusinessId.Text = ""

        '=3501.0611-  MUST get actual assembly version ie DLL..  SEE BELOW !!
        'msJobMatixVersion = "JMxJT350.dll-  v" & CStr(My.Application.Info.Version.Major) & "." & _
        '        My.Application.Info.Version.Minor & ". Build: " & _
        '        My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
        'labVersion.Text = msJobMatixVersion

        '= 3203.119==
        labDetailOnSiteJob.Visible = False
        labDetailWarrantyJob.Visible = False
        '== labDetailWarrantyJob.Top = labDetailOnSiteJob.Top
        '== labDetailWarrantyJob.Left = labDetailOnSiteJob.Left

        '= MsgBox("Application assembly name: " & My.Application.Info.AssemblyName, MsgBoxStyle.Information)
        '= modFileSupport33-
        '== gsAssemblyName = Replace(My.Application.Info.AssemblyName, ".exe", "")  '-replace in case.

        '=3501.0611-  MUST get actual assembly ie DLL..

        '-- SEE modAllFileAndSqlsubs
        'Dim assemblyThis As Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        'Dim assName As AssemblyName
        'assName = assemblyThis.GetName
        ''= modFileSupport33-
        'gsAssemblyName = Replace(assName.Name, ".exe", "")  '-replace in case.
        'With assName.Version
        '    msJobMatixVersion = gsAssemblyName & " v:" & CStr(.Major) & "." & CStr(.Minor) & ". Build: " & _
        '                          CStr(.Build) & "." & CStr(.Revision)
        'End With
        '-- SEE modAllFileAndSqlsubs
        msJobMatixVersion = msAssemblyName() & ": " & msAssemblyVersion()
        labVersion.Text = msJobMatixVersion

        '=4219.1128=
        Call gSubSetAppVersion(msJobMatixVersion)

        '-- SQL SERVER CONNECTION..--

        '--  NEW- v3.1.3101.907 ---
        '-- === DO all startup here..  ====
        '-- === DO all startup here..  ====
        '-- 
        '--  NEW- v3.4.3431.0429 ---
        '--  Connection/msServer come in with New Constructor.
        '--  Connection/msServer come in with New Constructor.
        '=3501.0610=
        '--  Connection/msServer/DB-name come in with New Constructor.
        If (mCnnSql Is Nothing) Or (msServer = "") Or (msSqlDbName = "") Then
            MessageBox.Show("No sql server or DB-name found." & vbCrLf & _
                            "Jobmatix can't continue..", _
                                 "JobMatix Load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Beep()
            Me.Close()
            '= Me.Dispose()
            '= End
            Exit Sub
        End If  '-nothing-

        Try '--main-
            '--load local settings..--
            '== Call mbLoadSettings()
            '== msServer = ""

            '==   Target-New-Build-6201 --  (18-June-2021)
            '= msSettingsPath = gsLocalJobsSettingsPath("JobMatix42") '= msAppPath & K_SAVESETTINGSPATH
            msSettingsPath = gsLocalJobsSettingsPath("JobMatix62") '= msAppPath & K_SAVESETTINGSPATH
            '==  END Target-New-Build-6201 --  (18-June-2021)

            '==3311.214= Load up Settings.
            mLocalSettings1 = New clsLocalSettings(msSettingsPath)

            '-- ALL this gone to Sub Main..
            '-- ALL this gone to Sub Main..
            '-- ALL this gone to Sub Main..

            'If mLocalSettings1.queryLocalSetting("sqlserver", s1) Then
            '     msServer = s1
            'End If
            '--  check local settings for sql server name..
            '==3311.214=  If gbQueryLocalSetting(msSettingsPath, "sqlserver", s1) Then
            '==3311.214= msServer = s1
            '==3311.214= End If

            'mbLoginRequested = False

            ''= Me.KeyPreview = True
            'Call mWaitFormOn("Press ESC for SQL Server Search.", "JobMatix is starting..")
            'DoEvents()
            'lngStart = CInt(VB.Timer()) '--starting seconds.-
            'While (CInt(VB.Timer()) <= lngStart + 3)
            '    System.Windows.Forms.Application.DoEvents()
            'End While
            ''==bLoginRequested = frmSplash1.loginRequested '--get request if any..-
            'mbLoginRequested = mFormWait1.loginRequested
            'Call mWaitFormOff()

            'mCnnSql = New OleDbConnection  '= ADODB.Connection
            ''== mCnnSql.ConnectionTimeout = 10 '--seconds..-
            'Dim bRetrying As Boolean = False
            'While Not bConnected
            '    If (msServer = "") Or mbLoginRequested Then  '-- no server defined..
            '        '- go search network.
            '        If mbFindSQlServer(s1) Then
            '            msServer = s1
            '        Else  '-nothing-
            '            MsgBox("No SQL server found..", MsgBoxStyle.Information)
            '            msServer = ""  '--force input box name entry.
            '        End If
            '    End If
            '    If (msServer = "") Or bRetrying Then  '--still no server defined.. get name.
            '        '==   MsgBox("No SQL server found..", MsgBoxStyle.Exclamation)
            '        '=3107.902= --Give Option to eneter a server name..
            '        msServer = Trim(InputBox("No SQL server connected.." & vbCrLf & _
            '                      "Enter 'Server\Instance' name if available..", "Sql Server connect-", msServer))
            '        If (VB.Left(msServer, 2) = "\\") Then
            '            msServer = VB.Mid(msServer, 3)  '--drop  the double slash..
            '        End If
            '        If (msServer = "") Then '-no name-
            '            Call gbLogMsg(gsRuntimeLogPath, "No Sql server available.. Exiting JobMatix." & vbCrLf)
            '            Me.Close()
            '            Exit Sub
            '        End If
            '    End If
            '    '-- Found Server- try connect-
            '    bConnected = mbSqlConnect(msServer)
            '    bRetrying = True   '--in case failed.-
            'End While  '-Not bConnected-
            'Call gbSetupSqlVersion(mCnnSql)


            '--END OF-   ALL this gone to Sub Main..
            '--END OF-   ALL this gone to Sub Main..
            '--END OF-   ALL this gone to Sub Main..

            '-3519.0108= - But we need this here-
            Call gbSetupSqlVersion(mCnnSql)

            msSqlVersion = gsGetSqlVersion()

            '--  check if we are sqlAdmin privileged..--
            mbIsSqlAdmin = gbTestSqlUser(mCnnSql, "SELECT IS_SRVROLEMEMBER ('sysadmin'); ")
            '-- save as global--
            Call gbSetIsSqlAdmin(mbIsSqlAdmin)
            Call gbLogMsg(gsRuntimeLogPath, "Logged in OK to SQL Server Instance:  " & msServer & vbCrLf & _
                                              "SQL-Server Version: " & msSqlVersion & vbCrLf)
            msCurrentUserName = gsGetCurrentUser()
            '-- SUB MAIN Logged on ok--
            '-- SUB MAIN Logged on ok--
            Call mbShowSqlInfo()

            '=3519.0108=
            Dim processThis As Process = Process.GetCurrentProcess
            msCurrentProcessName = processThis.ProcessName

            '--  WARN against old versions of Sql Server.
            If Not gbIsSqlServer2008Plus() Then  '=If gbIsSqlServer2005Plus() Then
                MsgBox("PLease Note- " & vbCrLf & _
                       "JobMatix now needs a minimum Sql Version of 2008-R2" & vbCrLf & vbCrLf & _
                       "To update, Backup your JobMatix database, " & vbCrLf & _
                       "    install Sql Server 2008R2 or later, " & _
                       "    and RESTORE the backup DB to the new server..", MsgBoxStyle.Exclamation)
            End If

            '==  3501.0610= 
            '--  ALL THIS DONE by Caller to this dll..
            '--  ALL THIS DONE by Caller to this dll..
            '--  ALL THIS DONE by Caller to this dll..

            '--  NOW find databases.-- and select..
            'sMsgText = "-- Server is: " & msServer & vbCrLf
            'sMsgText &= "-- Checking for JobMatix databases.. " & vbCrLf
            'If mbIsSqlAdmin Then
            '    sMsgText &= vbCrLf & "  (Press ESC for DB-Admin functions..)"
            'End If  '-admin-
            'Call mWaitFormOn(sMsgText, msJobMatixVersion)
            'lngStart = CInt(VB.Timer()) '-- TEST starting seconds.-
            'If Not gbGetJobmatixDatabases(mCnnSql, colAllJobsDBs, colUserJobsDBs) Then
            '    Call mWaitFormOff()
            '    MsgBox("getting database list has failed." & vbCrLf & _
            '               "Error text:" & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf & _
            '          "Please check that your Windows Logon has been added to SQL-Server logins..", _
            '           MsgBoxStyle.Exclamation)
            '    Me.Close()
            '    Exit Sub
            'End If '--get..-

            ''- TESTING-  give 2 secs-
            'If mbIsSqlAdmin Then
            '    While (CInt(VB.Timer()) <= lngStart + 3)
            '        System.Windows.Forms.Application.DoEvents()
            '    End While
            'End If

            'bESCapeRequested = mFormWait1.loginRequested
            'Call mWaitFormOff()
            ''--testing-
            ''== MsgBox("Found " & colUserJobsDBs.Count & "  databases", MsgBoxStyle.Information)

            ''-- Choose database..-
            'msSqlDbName = ""
            ''-- No dbs in list.. if Admin, Offer CREATE/RESTORE and Wait..
            ''--                  Else msg: Must be admin for Create.
            'If (colUserJobsDBs.Count <= 0) Then  '- no DB-
            '    If mbIsSqlAdmin Then
            '        If Not mbIsInstalling Then
            '            MsgBox("No JobMatix database could be found at the SQL Server: " & msServer & vbCrLf & _
            '                      " Select from Create or Restore a database..", MsgBoxStyle.Information)
            '        End If
            '        If mbSelectDatabase(colUserJobsDBs, mbIsSqlAdmin, mbIsInstalling, mbInstallingJobMatixPOS, s1) Then
            '            msSqlDbName = s1
            '        Else  '-nothing -(could be Upgrade/migration)- will exit- 
            '            Me.Close()
            '            Exit Sub
            '        End If  '-select -
            '    Else  '--not admin-
            '        MsgBox("No JobMatix database could be found at Server: " & msServer & vbCrLf & _
            '                "For NT user " & msCurrentUserNT & vbCrLf & _
            '                "Check that your Windows Logon has been added to SQL-Server logins.", MsgBoxStyle.Exclamation)
            '        Me.Close()
            '        Exit Sub
            '    End If '-admin-
            'ElseIf (colUserJobsDBs.Count = 1) Then  '- ONE DB-
            '    '-- IF 1 DB, NOT Admin-login,  then go ahead into application..
            '    '-- IF 1 DB, plus Admin-login,  If ESC pressed then Show Create/Restore + DB list (1).
            '    '--                             Else Continue to open the only database..
            '    If mbIsSqlAdmin Then
            '        If bESCapeRequested Then
            '            '-- show DB name and CREATE/RESTORE options.
            '            If mbSelectDatabase(colUserJobsDBs, mbIsSqlAdmin, mbIsInstalling, mbInstallingJobMatixPOS, s1) Then
            '                msSqlDbName = s1
            '            Else  '-nothing -- will exit- 
            '            End If '-select-
            '        Else  '-- no ESC request..  GO ahead with the ONLY DB-
            '            msSqlDbName = colUserJobsDBs(1)("dbname")
            '        End If  '-- ÉSC-
            '    Else  '-- not admin..  GO ahead with the ONLY DB-
            '        msSqlDbName = colUserJobsDBs(1)("dbname")
            '    End If '--admin
            'Else  '--Multiple DBs--
            '    '-- If >1 jobs db's-- Admin-login, we show DB list..--
            '    '--  Admin- show DB LIST and CREATE/RESTORE options.
            '    '--   Not Admin- show DB LIST Only.
            '    If mbSelectDatabase(colUserJobsDBs, mbIsSqlAdmin, mbIsInstalling, mbInstallingJobMatixPOS, s1) Then
            '        msSqlDbName = s1
            '    Else  '-nothing -- will exit- 
            '    End If '-select-
            'End If  '--DB count-

            '-- END OF-  ALL THIS DONE by Caller to this dll..
            '-- END OF-  ALL THIS DONE by Caller to this dll..
            '-- END OF-  ALL THIS DONE by Caller to this dll..


            If (msSqlDbName = "") Then
                MsgBox("Can't find any JobTracking database for user: '" & _
                       msCurrentUserName & "'.." & vbCrLf & vbCrLf & _
                       "(JobMatix Admin user may need to add '" & msCurrentUserName & _
                       "' to JobMatix users..)", MsgBoxStyle.Exclamation)
                Me.Close()
                Exit Sub
            End If

            '-- Now set current database..--
            '-  !! MUST retry because for non-admin user -
            '----  it doesn't stick the first time..--

            '-- USE Jobs DB and check result..-
            If Not gbSetCurrentDatabase(mCnnSql, msSqlDbName) Then
                '== If (LCase(sCurrentDB) <> LCase(sDBnameJobs)) Then
                MsgBox("= Failed to set current Database for: " & msSqlDbName & vbCrLf & sErrors, MsgBoxStyle.Exclamation)
                Call gbLogMsg(gsRuntimeLogPath, "= Failed to set current Database for: " & msSqlDbName & vbCrLf & _
                                                   "Jobmatix is closing down..")
                Me.Close()
                Exit Sub '-- False..-
            End If
            '--  USE must have worked..-
            Call gbLogMsg(gsRuntimeLogPath, "= OK.. current Database is set to: [" & msSqlDbName & "].." & vbCrLf)
            '-- SAVE id for who_using.. mIntJobMatixDBid --
            If gbGetSelectValueEx(mCnnSql, "SELECT DB_ID() AS current_db_id;", v1) Then
                If Not (v1 Is Nothing) Then
                    mIntJobMatixDBid = CLng(v1)
                End If
            End If  '--get-
            Call gSetJobMatixDBid(mIntJobMatixDBid)
            '= Call gbLogMsg(gsRuntimeLogPath, "= OK.. current Database is set to: [" & msSqlDbName & "].." & vbCrLf & _
            '=                                   "  == and it took " & msSqlDbName & " USE attempts..")
            ''-- Connect and DB choice done..

            Call gbLogMsg(gsRuntimeLogPath, "-- SqlAdmin: Checking user permissions for VWSS.. " & vbCrLf)
            '--  CHECK VSS PERMISSIONS--
            If Not gbCheckVWSSpermissions(mCnnSql, mbIsSqlAdmin, msSqlDbName, msCurrentUserNT) Then
                Me.Close()
                Exit Sub '-- False..-
            End If  '--check-
            Call gbLogMsg(gsRuntimeLogPath, "-- SqlAdmin: Done Checking permissions.. " & vbCrLf)

            '-  GET Sql Schema..--
            '-- AGAIN..  Must USE Jobs DB and check result..-
            If Not gbSetCurrentDatabase(mCnnSql, msSqlDbName) Then
                '== If (LCase(sCurrentDB) <> LCase(sDBnameJobs)) Then
                MsgBox("= Failed to set current Database for: " & msSqlDbName & vbCrLf & sErrors, MsgBoxStyle.Exclamation)
                Call gbLogMsg(gsRuntimeLogPath, "= Failed to set current Database for: " & msSqlDbName & vbCrLf & _
                                                   "Jobmatix is closing down..")
                Me.Close()
                Exit Sub '-- False..-
            End If
            '--  USE must have worked..-

            '== 3203.118= CHECK for V3.2 Attachments Table..
            '== 3203.118= CHECK for V3.2 Attachments Table..

            '== MsgBox("Load Event: SQL Connected ok. " & vbCrLf & _
            '==          "  Check here for v3.2 and Attachments Table..", MsgBoxStyle.Information)
            Dim clsAttachmentsJobs1 As New clsAttachments(Me, mCnnSql, "JOB", openDlg1)
            Dim bDoCreateV2 As Boolean = False
            If Not clsAttachmentsJobs1.DoesAttachmentTableExist("JOB") Then
                '--  Needs to upgrade..
                If (MsgBox("Attachments Tables don't exist yet.." & vbCrLf & _
                          "  These are needed for Upgrade to JobMatix v3.2." & vbCrLf & _
                          "  (Otherwise you need to stick with your Previous version of JobMatix).." & vbCrLf & vbCrLf & _
                              "OK to Upgrade now ? ", _
                           MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                    bDoCreateV2 = True
                Else  '-no-
                    Me.Close()
                    Exit Sub
                End If
            End If  '-exists.-
            '= Do Upgrade mods as ONE Transaction.--
            If bDoCreateV2 Then
                Dim sqlTran1 As OleDbTransaction = mCnnSql.BeginTransaction
                Dim bCreateOK As Boolean
                '-- Create JOB Attachments..
                bCreateOK = clsAttachmentsJobs1.CreateAttachmentTable("JOB", True, sqlTran1)
                If Not bCreateOK Then  '=  RollBack was done..-
                    MsgBox("Failed to create JOB Attachments Table.", MsgBoxStyle.Exclamation)
                Else '-ok-
                    '--And Create RA Attachments..
                    Dim clsAttachmentsRAs1 As New clsAttachments(Me, mCnnSql, "RA", openDlg1)
                    bCreateOK = clsAttachmentsJobs1.CreateAttachmentTable("RA", True, sqlTran1)
                    If Not bCreateOK Then  '= was rolled back-
                        MsgBox("Failed to create RAs Attachments Table.", MsgBoxStyle.Exclamation)

                    Else  '-ok-
                        '-- ADD Jobs Column "SystemUnderWarranty"..--
                        Dim sSql, sErrorMsg As String
                        Dim intAffected As Integer
                        sSql = "ALTER TABLE dbo.Jobs ADD SystemUnderWarranty BIT NOT NULL DEFAULT 0;"
                        bCreateOK = gbExecuteSql(mCnnSql, sSql, True, sqlTran1, intAffected, sErrorMsg)
                        If Not bCreateOK Then
                            MsgBox("Failed to add new column to Jobs Table." & vbCrLf & vbCrLf & _
                                                                        sErrorMsg, MsgBoxStyle.Exclamation)
                        End If
                    End If  '--RA's ok -- 
                End If '-Jobs ok.
                '==MsgBox("Upgrade completed ok..", MsgBoxStyle.Information)
                If bCreateOK Then
                    sqlTran1.Commit()
                    MsgBox("JobMatix V2 (Attachments) Upgrade was completed ok..", MsgBoxStyle.Information)
                Else
                    MsgBox("Errors have occurred.. Database was not Upgraded..", MsgBoxStyle.Exclamation)
                    Me.Close()
                    '= End
                    Exit Sub
                End If ' ok.
            End If  '-create V2-

            '--ok. Now can Get DB schema info..-- 
            '-- Load DB-Info for selected DB..--
            Call mWaitFormOn("-- Server: " & msServer & vbCrLf & vbCrLf & _
                            "-- Getting schema  " & vbCrLf & " For DB '" & msSqlDbName & "'..", msJobMatixVersion)
            System.Windows.Forms.Application.DoEvents()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '-- BG Worker to get schema.--
            mbSqlServerGetSchemaOK = False
            '-- start worker thread-
            '-- Start the Sql GetSchema operation in the background.
            Me.BackgroundWorkerGetSchema.RunWorkerAsync()
            '-- wait for completion-
            '-- ie Wait for the BackgroundWorker to finish the Search.
            While Me.BackgroundWorkerGetSchema.IsBusy
                '- Keep UI messages moving, so the WAIT form remains 
                '-     responsive during the asynchronous operation.
                Application.DoEvents()
            End While

            If mbSqlServerGetSchemaOK Then  '= gbGetSchemaInfo(mCnnSql, msSqlDbName, mColSqlDBInfo, True) Then 
                Call mWaitFormOff()
                msBuildSchemaLog &= vbCrLf & "= loaded info for DATABASE:  " & msSqlDbName & "  = = = =" & vbCrLf
                '==colDBs.Add colDBInfo, LCase$(sName)
            Else
                Call mWaitFormOff()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                msBuildSchemaLog &= " *** ERROR- FAILED to build  SQL catalogue for DB: " & msSqlDbName & " ==" & vbCrLf
                MsgBox(" *** ERROR- FAILED to build  SQL catalogue for DB: " & msSqlDbName & " ==" & vbCrLf)
                Me.Close()
                Exit Sub '-- False..-
            End If '--ok--
            '-- ok to go.-

            '== labStatus.Text = "Logging SQL-Jobs DB schema info..."
            '== 3101= Call gbMain_showSqlSchema(msSqlDbName, mColSqlDBInfo) '--log all schema info-- 

            sSchemaInfo = gsShowSqlSchema(msSqlDbName, mColSqlDBInfo) '--display all schema info-- 
            sSchemaInfo &= vbCrLf & "= = = Log Saved: " & _
                                                VB.Format(Now, "dd-MMM-yy hh:mm") & " = = =" & vbCrLf
            Call glSaveTextFile(gsJobMatixLocalDataDir("JMxJT420") & "\" & msSqlDbName & "_schema.txt", _
                                       msBuildSchemaLog & vbCrLf & "= = = = =" & vbCrLf & vbCrLf & sSchemaInfo)
            '-- Connect and DB Startup COMPLETED..

            '-- get all system info..--
            '---  19Aug2011= all loads gone to function 'mbLoadAllSystemInfo'---
            msProviderCode = ""

            '==3311= SysInfo. use class instance.
            mSysInfo1 = New clsSystemInfo(mCnnSql)
            If Not mbLoadAllSystemInfo() Then
            End If

            '-- Default Retail Host..--
            If (msRetailHostname = "") Then
                msRetailHostname = "RetailManager" '--default..--
            End If
            If (LCase(msRetailHostname) = "jobmatixpos") Then  '--JM POS--
                '=3411.1221= POS Tab GONE==
                '= grpboxSale.Enabled = True
                '= picLogoPOS.Visible = True
            Else   '-RM-
                '= grpboxSale.Enabled = False
                '= picLogoPOS.Visible = False
                '=3411.1221=  Dim tabpagePos As TabPage = TabControlMain.TabPages("TabPagePOS")
                '=3411.1221=  TabControlMain.TabPages.Remove(tabpagePos)
            End If
            '=3311.711== 
            '--   Show JobTracking page FIRST, anyway.  (otherwise stuffs up Notify Panel..-
            Dim tabpageJobs As TabPage = TabControlMain.TabPages("TabPageJobTracking")
            Me.TabControlMain.SelectedTab = tabpageJobs

            '=3411.1221= POS Tab GONE==
            '== grpboxSale.Text = ""

            mLngJobsTreeBGColour = &HFFFFFF  '=3083 is WHITE= &HF8F8F8 '== &HF0F0F0
            Call mSetTreeViewColour(tvwJobs, mLngJobsTreeBGColour) '== &HE4E4E4) '== RGB(&HB0, &HC4, &HDE))  '--lt steel blue.-
            tvwJobs.HotTracking = False

            '--Initialise Jobs TreeView.-
            '--  MUST do this here.  else tree stuff invisible..-
            Call mbInitialiseJobsTreeView(tvwJobs)

            LabDetailPriority.Text = ""
            LabTreeStatus.Text = ""

            '== LabJobReturned.Visible = False
            picJobDetailReturned.Visible = False  '==3083=
            LabStaffName2.Text = ""

            cboPriority.Items.Clear()
            cboPriority.Visible = False

            '==3083==
            frameOnSite.Text = ""
            '== frameOnSiteHdr.Text = ""
            '= frameOnSite.Visible = False

            '==3083== frameJobsTab.Visible = False
            '= frameCustomers.Visible = False
            frameCustomers.Text = ""
            '==3083== frameMainCmds.Enabled = False

            '== 3083 == 
            '==frameJobHdr.Text = ""
            LabDetailsJobNo.Text = ""
            LabDetailStatus.Text = ""
            LabDetailStatus2.Text = ""

            listViewCustJobs.HideSelection = False
            listViewCustJobs.MultiSelect = False

            frameQuotes.Text = ""
            '=3101= frameQuotes.Visible = False
            ListViewSalesOrders.Enabled = False
            chkNewQuotes.Checked = True
            chkRecentQuotes.Checked = True  '=3107.706=   '=False
            labLoadingQuotes.Text = ""

            frameQuoteDetails.Text = ""
            '=3101= frameQuoteDetails.Visible = False
            labQuoteOrderNo.Text = ""
            labQuoteCount.Text = ""
            labJoblist.Text = ""

            cmdSignoff.Enabled = False
            txtStaffBarcode.Text = ""
            '==3083== frameSignOn.Text = ""
            '==3083== frameSignOn.Visible = True

            '--  save rtf template for displaying job details..--
            '--  IS NOW an EMBEDDED resource..  see Activated Sub..
            rtfJobDetails.Text = "" '-- and clear the rtf text box..

            '--  use combo..-
            cboJobsOrder.SelectedIndex = 0    '-- show 1st item.--
            cboJobsFilter.SelectedIndex = 1   '--3357.0219 FILTER- show 2nd item. (All jobs)--
            Call CenterForm(Me)

            '--load local settings..--
            '==3311.214= Call mbLoadSettings()

            '-- This gone down to SHOWN..
            'If (msUserLogoPath = "") Then
            '    sShowMsgKey = "DONOTSHOW_NOUSERLOGO"
            '    sHdr = "Your business Logo missing ?"  '--hdr--
            '    sMsgText = "Please note:   No user business logo with a name like:" & vbCrLf & vbCrLf & _
            '           "    ""userlogo* [.jpg],  [.gif]  or  [.bmp]"" " & vbCrLf & vbCrLf & _
            '           "could be found in the folder:" & vbCrLf & "   '" & msAppPath & "'.." & vbCrLf & vbCrLf & _
            '           "The JobMatix logo will be used for document printing.."
            '    Call mbDoNotShowMsgDialogue(sShowMsgKey, sHdr, sMsgText)
            'End If  '--logo path.-

            '--  Main toolbar--
            'toolbarJobView.Items("_toolbarJobView_Button1").ToolTipText = "Active Jobs Tree: " & vbCrLf & _
            '                                                     "Shows all current Jobs- waiting and in-progress.."
            'toolbarJobView.Items("_toolbarJobView_Button2").ToolTipText = "Jobs Browse/Search Grid: " & vbCrLf & _
            '                                                     "Browse ALL Jobs by Status- " & vbCrLf & _
            '                                                      "or use Full-text Search to find Jobs.."
            'toolbarJobView.Items("_toolbarJobView_Button3").ToolTipText = "Customer Selection Grid: " & vbCrLf & _
            '                                                     "Choose a customer for a New Job.. " & vbCrLf & vbCrLf & _
            '                                                     "(Browse Retail Customer Table- " & vbCrLf & _
            '                                                      "or use Full-text Search to find a Customer.)"
            '-- New Job  tooltips--
            '==toolbarNewJob2.Items("btnBookJob").ToolTipText = vbCrLf & "Set up a booking for a new job." & vbCrLf & _
            '==                                                  "(Job will be wait-listed..)" & vbCrLf
            toolbarNewJob2.Items("btnAcceptJob").ToolTipText = vbCrLf & "Create a new Workshop Job and service agreement." & vbCrLf & _
                                                             "    (Book-in or Accepting Job now..)" & vbCrLf

            '-- =3083=   
            '-- Popup Action menu for Right click on Jobs TreeView Node..-
            mContextMenuJobsTreeNodeAction = New ContextMenu
            '-- add all menu items..

            mnuJobActionCheckIn.Name = "NodeActionCheckIn"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionCheckIn)

            '=3203.211-
            mnuJobActionReturnToWaitlist.Name = "NodeActionReturnToWaitlist"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionReturnToWaitlist)

            mnuJobActionAmend.Name = "NodeActionAmend"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionAmend)

            '==3323.1109=
            '-mnuJobActionTfrToNewCust
            mnuJobActionTfrToNewCust.Name = "NodeActionTfrToNewCustomer"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionTfrToNewCust)

            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionSep1)

            mnuJobActionStart.Name = "NodeActionStart"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionStart)

            mnuJobActionUpdate.Name = "NodeActionUpdate"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionUpdate)
            '==  mnuJobActionReturnToQueue As New MenuItem("Return Job To Queue")  '=3203.124=
            mnuJobActionUpdate.Name = "NodeActionReturnToQueue"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionReturnToQueue)
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionSep2)

            mnuJobActionReOpen.Name = "NodeActionReOpen"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionReOpen)

            mnuJobActionDeliver.Name = "NodeActionDeliver"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionDeliver)
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionSep3)

            mnuJobActionNotify.Name = "NodeActionNotify"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionNotify)

            mnuJobActionStopPress.Name = "NodeActionStopPress"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionStopPress)
            '=3203.219=
            mnuJobActionClearAlert.Name = "NodeActionClearAlert"  '=3203.219=
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionClearAlert)

            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionSep4)

            mnuJobActionView.Name = "NodeActionView"
            mContextMenuJobsTreeNodeAction.MenuItems.Add(mnuJobActionView)

            '--  done that menu.--

            '--=3107.907=
            '=  Context menu for RichText Info-
            '--  Popup menu for Right click on RTF Job Info..-
            mContextMenuRichTextInfo = New ContextMenu
            mnuCopyRichTextSelection.Name = "CopyRichTextSelection"
            mContextMenuRichTextInfo.MenuItems.Add(mnuCopyRichTextSelection)

            '--  done that menu.--

            '== labDetailHdrQuote.Text = "-Quote-"
            '== labDetailHdrQuote.Visible = False   '==3083==
            labDetailTech.Text = ""
            labDetailDateCreated.Text = ""
            labDetailDatePromised.Text = ""
            labDetailUpdatedDescription.Text = ""

            '-- 3.2.1229= 
            '--  TooTip see if this works..
            '== ToolTipMain.SetToolTip(Me.btnAttachments, "Show/Add Attachment Docs this Job..")
            ToolTipMain.SetToolTip(Me.picAttachments, "Show/Add Attachments for this Job..")
            '==3311.405= ToolTipMain.SetToolTip(Me.cmdLaunchRAs, "Track RA's (Returns to Suppliers)..")

            '==3311.309== RAs In-house.-
            '- Initialising RAs Main Controls.

            '==        -- 3411.0121  DROP all the RAs Tan Panel..
            '==        --   and Add Button to Launch RAs .exe..
            '==
            '=  MORE init stuff in clsRAsMain (Sub New) --
            '== frameRAsTab.Visible = True

            '=3311.409= -- Check if SMS Reminders enabled..--

            labReminderStatus.Text = "SMS Reminders.."  '==3311.408=
            labReminderStatus.Enabled = False
            '-EnableOnSiteSmsReminders=
            If mSysInfo1.exists("EnableOnSiteSmsReminders") AndAlso _
                        UCase(mSysInfo1.item("EnableOnSiteSmsReminders")) = "Y" Then
                mnuEnableSMSReminders.Checked = True
                labReminderStatus.Enabled = True
            Else
                mnuEnableSMSReminders.Checked = False
            End If '-exists-

            '==3311.423= Same for AutoAssign Jobs..
            '-- mnuAutoAssignOrphanJobsOnUpdate -
            If mSysInfo1.exists("AutoAssignOrphanJobsOnUpdate") AndAlso _
                         UCase(mSysInfo1.item("AutoAssignOrphanJobsOnUpdate")) = "Y" Then
                mnuAutoAssignOrphanJobsOnUpdate.Checked = True
            Else
                mnuAutoAssignOrphanJobsOnUpdate.Checked = False
            End If '-exists-

            '==  JobMatix  P O S  TAB CONTROL  -
            '==  JobMatix  P O S  TAB CONTROL  -
            '- Draw Tab Tops..

            '=3411.1221= POS Tab GONE==
            '=3411.1221= POS Tab GONE==

            '== TabControlPOS.DrawMode = TabDrawMode.OwnerDrawFixed
            '== grpBoxPosAdmin.Text = ""

            '=3411.0303--  Get SignOn/SignOff Settings--
            If mLocalSettings1.queryLocalSetting(k_AutoSignOffSettingsName, s1) Then
                '= mSdSettings.Exists(sShowMsgKey) Then
                '= s1 = UCase(mLocalSettings1.Item(sShowMsgKey))  '==  "DONOTSHOW_NEWUSERINFO1"-
                If (UCase(s1) = "STAYSIGNEDON") Then  '-StaySignedOn-
                    mnuStaySignedOn.Checked = True
                    mnuShortSignOff.Checked = False
                    mnuLongSignOff.Checked = False
                ElseIf (UCase(s1) = "SHORTTIMEOUT") Then  '-check timeout-
                    mnuStaySignedOn.Checked = False
                    mnuShortSignOff.Checked = True
                    mnuLongSignOff.Checked = False
                ElseIf (UCase(s1) = "LONGTIMEOUT") Then  '-check timeout-
                    mnuStaySignedOn.Checked = False
                    mnuShortSignOff.Checked = False
                    mnuLongSignOff.Checked = True
                End If  '-y-
            Else '--no entry-
                mnuStaySignedOn.Checked = False
                mnuShortSignOff.Checked = False
                mnuLongSignOff.Checked = True  '-DEFAULT..
            End If  '-query-

            mIntStaffTimeoutInterval = k_StaffTimeoutInterval_long  '-DEFAULT-
            '= set correct timeout..
            If mnuLongSignOff.Checked Then
                mIntStaffTimeoutInterval = k_StaffTimeoutInterval_long  '-secs-
            ElseIf mnuShortSignOff.Checked Then
                mIntStaffTimeoutInterval = k_StaffTimeoutInterval_short   '-30 secs-
            End If

            '--Tab Controls..
            TabControlMain.DrawMode = TabDrawMode.OwnerDrawFixed
            TabControlJobTracking.DrawMode = TabDrawMode.OwnerDrawFixed

            '== Target-build-4284  (Started 23-Nov-2020)
            '== Target-build-4284  (Started 23-Nov-2020)
            '==    DEV- Bring JobReports into JobTracking Main as a UserControl..

            '--  ADD a new Main Tab page, and (Later Below) load JobReports userControl..
            '= Dim ucChildJobReports1 As ucChildJobReports42
            '= Dim sReportArgs As String
            Dim tabPageJobReports As TabPage

            tabPageJobReports = New TabPage
            tabPageJobReports.Text = "Job Reports"
            tabPageJobReports.BackColor = Color.Magenta
            tabPageJobReports.Name = "tabPageJobReports"
            TabControlMain.TabPages.Add(tabPageJobReports)

            '--load reports usercontrol below in "Shown"..
            '== END Target-build-4284  (Started 23-Nov-2020)

            mbMainLoadDone = True


        Catch ex As Exception
            MsgBox("Error in JobMatix Main Load function.." & vbCrLf & ex.Message)
            Me.Close()
            '= End
        End Try  '--main-

    End Sub '--load--
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--WAS ORIGINALLY  Activate --
    '-- Activate --

    Private Sub frmJobMatix3_Activated(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If mbActive Then   '--try to repaint to get rid of left-over stuff.-
            Me.Update()
            Application.DoEvents()
            Exit Sub
        End If
        Me.Text = "Can't start JobMatix maximised.."
        If (Not mbMainLoadDone) Or (Me.WindowState = FormWindowState.Maximized) Then
            '==MsgBox("Can't start up maximised..", MsgBoxStyle.Exclamation)
            Beep()
            Me.Close()
            Me.Dispose()
            Exit Sub '= End
            '== Exit Sub '--still loading..-
        End If
        mbActive = True

    End Sub '-activated-
    '= = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--Shown replaces Activated..
    '-- Happens once only..

    Private Sub frmJobMatix3_Shown(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles MyBase.Shown

        '== Dim sDB, sCreateLog As String
        Dim s1, s2 As String
        Dim s3, sResult, sList As String
        Dim sCmdLine As String
        '== Dim sConnect As String
        Dim sABNWeightedSum As String
        Dim asSalt(5) As String
        Dim sComputedKey As String
        Dim sComputedPreciseShortKey As String
        Dim sComputedKeyLevel2 As String
        Dim sComputedKeyThreeUser As String
        Dim sComputedKeyTwoUser As String
        Dim sComputedKeySingleUser As String
        Dim binMD5Digest() As Byte
        Dim sMsg, sMsg2, sMsg3, sId, sIdLevel2, sIdThreeUser As String
        Dim sIdTwoUser, sIdSingleUser As String
        Dim sDefault, sErrorMsg As String
        Dim sPath As String
        Dim ok, bIsLevel2Licence As Boolean
        '== Dim bUpdateJet As Boolean
        Dim bool1 As Boolean
        '== Dim iDBFerrors As Short
        Dim lngStart, lngWeightedSum As Integer
        Dim lResult, ix, L1, lCount, kx, lErrors As Integer
        '====Dim lngCrcA, lngCrcB As Long
        Dim col1 As Collection
        Dim colTable, colFields, colFieldx As Collection
        Dim sServer, sSql As String
        Dim mbLoggedon As Boolean '--TEMP !!!-
        Dim bContinue As Boolean
        Dim bUpgradeNeeded As Boolean
        Dim bCompleted As Boolean
        Dim bRestartNeeded As Boolean
        '== Dim colSystemInfo As Collection
        Dim v1 As Object
        Dim dateOldest, dateX As Date
        Dim curOldLabourRate As Decimal
        '== Dim nodeX As System.Windows.Forms.TreeNode
        Dim pd1 As New PrintDocument()
        '--  Resources..-
        Dim asm As Assembly = Assembly.GetExecutingAssembly()
        Dim streamRtf As Stream
        '==Dim colWhichUsers As Collection
        Dim sShowMsgKey, sHdr, sMsgText As String

        Try  '--main outer try.-
            'If mbActive Then   '--try to repaint to get rid of left-over stuff.-
            '    Me.Update()
            '    Application.DoEvents()
            '    Exit Sub
            'End If
            'Me.Text = "Can't start JobMatix maximised.."
            'If (Not mbMainLoadDone) Or (Me.WindowState = FormWindowState.Maximized) Then
            '    '==MsgBox("Can't start up maximised..", MsgBoxStyle.Exclamation)
            '    Beep()
            '    Me.Close()
            '    Me.Dispose()
            '    End
            '    '== Exit Sub '--still loading..-
            'End If
            'mbActive = True
            Me.BringToFront()

            If (msUserLogoPath = "") Then
                sShowMsgKey = "DONOTSHOW_NOUSERLOGO"
                sHdr = "Your business Logo missing ?"  '--hdr--
                sMsgText = "Please note:   No user business logo with a name like:" & vbCrLf & vbCrLf & _
                       "    ""userlogo* [.jpg],  [.gif]  or  [.bmp]"" " & vbCrLf & vbCrLf & _
                       "could be found in the folder:" & vbCrLf & "   '" & msAppPath & "'.." & vbCrLf & vbCrLf & _
                       "The JobMatix logo will be used for document printing.."
                Call mbDoNotShowMsgDialogue(sShowMsgKey, sHdr, sMsgText)
            End If  '--logo path.-


            '---- .Text = "Jobs Tracking Starting up.." + vbCrLf + k_version + vbCrLf
            System.Windows.Forms.Application.DoEvents()

            '-- NB.--
            '--  SUB MAIN has already logged on to SQL Server, and
            '-------has supplied Server name, login info and DBname..--
            mBrowseJobs = New clsBrowse3 '== clsBrowse22
            mBrowseOnSiteJobs = New clsOnSiteJobs
            mBrowseCust = New clsBrowse3 '== clsBrowse22 

            curOldLabourRate = 0 '--in case 3-tier rates not there.--
            sCmdLine = Trim(VB.Command())
            '== gbVerbose = False
            '== If InStr(1, UCase(sCmdLine), "/V") > 0 Then gbVerbose = True
            '== gbDebug = False
            '== If InStr(1, UCase(sCmdLine), "/D") > 0 Then gbDebug = True
            '--gbDebug = True  '---TESTING--

            '---check if create or Maint mode--
            '--Find local ComputerName as server default--
            'L1 = gsRegQueryValue(HKEY_LOCAL_MACHINE, _
            '                     "SYSTEM\CurrentControlSet\Control\ComputerName\ComputerName", "ComputerName", s1)
            'If L1 = 0 Then
            '    '--MsgBox "Local ComputerName is :" + vbCrLf + s1
            '    msComputerName = s1
            'Else
            '    MsgBox("Can't find computer name..  Reg error: " & L1)
            'End If

            '-3401.415=
            '--  Get actual machine running this app process. (NOT the remote client).
            msMachineName = My.Computer.Name  '- for actual machine running this app process. (NOT the remote client).
            '--  get thin client if any=
            ' get the workstation name...
            mbIsThinClient = False
            msComputerName = Environment.GetEnvironmentVariable("clientname")
            ' if not a thin client, previous step returns nothing,
            ' this will get the name of a fat client...
            If (msComputerName IsNot Nothing) AndAlso (msComputerName <> "") Then
                mbIsThinClient = True
            Else '-no "client"  is Fat..
                '= machinename = Environment.GetEnvironmentVariable("computername")
                msComputerName = My.Computer.Name
            End If


            Me.Text = msJobmatixAppName & "- Quotes and Service Jobs.." & labVersion.Text
            LabBusiness.Text = "Starting.." '--  "Precise PCs"  '-- SYSINFO  --  !!--

            '== labStatus.Text = "Initialising Jobs TreeView."
            System.Windows.Forms.Application.DoEvents()

            '== mLngJobsTreeBGColour = &HF8F8F8 '== &HF0F0F0
            '= Call mSetTreeViewColour(tvwJobs, mLngJobsTreeBGColour) '== &HE4E4E4)   
            '= tvwJobs.HotTracking = False
            '== '--Initialise Jobs TreeView.-
            '== Call mbInitialiseJobsTreeView(tvwJobs)
            System.Windows.Forms.Application.DoEvents()

            '== msAppPath = My.Application.Info.DirectoryPath  '-INSTALL PATH..-

            msAppPath = My.Computer.FileSystem.CurrentDirectory
            If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"

            '--  load rtf file for test..--
            '--  load rtf file for test..--
            '--  load rtf file for test..--
            labStatus.Text = vbCrLf & " Loading Job Details RTF format.."
            If gbDebug Then MsgBox(" Loading Job Details RTF format..", MsgBoxStyle.Information)
            System.Windows.Forms.Application.DoEvents()
            '==  rtfJobDetails.LoadFile("Job_MainDetails.rtf")
            '==Me.Opacity = 0.7

            '==  LOAD 10-pt..--
            Try
                streamRtf = asm.GetManifestResourceStream("JobMatix3.Job_MainDetails_10.rtf")
                If Not (streamRtf Is Nothing) Then
                    Me.rtfJobDetails.LoadFile(streamRtf, RichTextBoxStreamType.RichText)
                Else
                    MsgBox("No RTF stream returned..", MsgBoxStyle.Exclamation)
                End If
            Catch ex As Exception    ' Catch the error.
                '--error-
                MsgBox("Error loading RTF resource file.." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
            End Try
            rtfTemplateJobs_10 = rtfJobDetails.Rtf '--save original format as TEMPLATE..--

            '==  LOAD 9-pt..--
            Try
                streamRtf = asm.GetManifestResourceStream("JobMatix3.Job_MainDetails_9.rtf")
                If Not (streamRtf Is Nothing) Then
                    Me.rtfJobDetails.LoadFile(streamRtf, RichTextBoxStreamType.RichText)
                Else
                    MsgBox("No RTF stream returned..", MsgBoxStyle.Exclamation)
                End If
            Catch ex As Exception    ' Catch the error.
                '--error-
                MsgBox("Error loading RTF resource file.." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
            End Try
            rtfTemplateJobs_9 = rtfJobDetails.Rtf '--save original format as TEMPLATE..--

            '==  LOAD 8-pt..--
            Try
                streamRtf = asm.GetManifestResourceStream("JobMatix3.Job_MainDetails_8.rtf")
                If Not (streamRtf Is Nothing) Then
                    Me.rtfJobDetails.LoadFile(streamRtf, RichTextBoxStreamType.RichText)
                Else
                    MsgBox("No RTF stream returned..", MsgBoxStyle.Exclamation)
                End If
            Catch ex As Exception    ' Catch the error.
                '--error-
                MsgBox("Error loading RTF resource file.." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
            End Try
            rtfTemplateJobs_8 = rtfJobDetails.Rtf '--save original format as TEMPLATE..--

            '--  set default as 8 pt..-
            rtfTemplateJobs = rtfTemplateJobs_8

            '==3083==
            '==  load NewIn31 text for help==
            '==    3501.0724 24-July-2018= 
            '==       -- "NewInJobMatix35.htm" is now picked at runtime from Runtime Directory.-

            '==   Target-New-Build-6201 --  (18-June-2021)
            '= Dim sNewInJobMatixPath = msAppPath & "NewInJobMatix42.htm"
            Dim sNewInJobMatixPath = msAppPath & "NewInJobMatix62.htm"
            '== END Target-New-Build-6201 --  (18-June-2021)

            Try
                strAboutJobMatix3HTML = My.Computer.FileSystem.ReadAllText(sNewInJobMatixPath)
            Catch ex As Exception
                '==   Target-New-Build-6201 --  (18-June-2021)
                MsgBox("ERROR- No 'NewInJobMatix62' HTML file returned.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try

            'Try
            '    streamRtf = asm.GetManifestResourceStream("JobMatix3.NewInJobMatix35.htm")
            '    If Not (streamRtf Is Nothing) Then
            '        '== Me.rtfJobDetails.LoadFile(streamRtf, RichTextBoxStreamType.RichText)
            '        Dim stream_reader As New StreamReader(streamRtf)
            '        strAboutJobMatix3HTML = stream_reader.ReadToEnd()
            '        stream_reader.Close()
            '    Else
            '        MsgBox("No 'NewInJobMatix35' HTML resource stream returned..", MsgBoxStyle.Exclamation)
            '    End If
            'Catch ex As Exception    ' Catch the error.
            '    '--error-
            '    MsgBox("Error loading NewInJobMatix35 HTML resource file.." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
            'End Try
            '== strNewInJobMatix3083HTML = rtfJobDetails.Rtf '--save for dialog..--
            '== done ==

            labStatus.Text = labStatus.Text & vbCrLf & ".."
            System.Windows.Forms.Application.DoEvents()
            If gbDebug Then MsgBox(" RTF format loaded..", MsgBoxStyle.Information)

            '--attach help file..-
            '== s1 = Dir(msAppPath & "JT2*.chm")
            '== 'UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 
            'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
            '== If s1 <> "" Then App.HelpFile = s1 & "::/JT2-MainScreen.htm"
            '--  SEE "HelpProvider" for forms..-- 
            '---   SEE  "Load"  event sub..

            '==s1 = msDayOfWeek(Today)
            s1 = msDayOfWeek(CDate(DateTime.Today))

            '== If gbDebug Then MsgBox(" day is: " & s1 & "....", MsgBoxStyle.Information)
            LabToday.Text = s1 & ", " & vbCrLf & Format(CDate(DateTime.Today), "dd-MMM-yyyy")
            '== Timer1.Enabled = True
            System.Windows.Forms.Application.DoEvents()

            '== If gbDebug Then MsgBox(" Getting time of day....", MsgBoxStyle.Information)
            mdStartTime = TimeOfDay
            If gbDebug Then MsgBox(" Time of day done..", MsgBoxStyle.Information)

            '== labStatus.BackColor = &HC0FFFF     '--light yellow-
            '== labStatus.Caption = " Getting avail. SQL database names.."
            '==3067.0==
            '= mSdSettings.Exists(K_STARTUPFULLSCREEN_SETINGNAME) Then
            If mLocalSettings1.queryLocalSetting(K_STARTUPFULLSCREEN_SETTINGNAME, s1) Then
                '= s1 = UCase(mSdSettings(K_STARTUPFULLSCREEN_SETTINGNAME))
                If (UCase(s1) = "Y") Then
                    Me.WindowState = FormWindowState.Maximized
                    mnuStartupMaximised.Checked = True
                    '== mnuStartupMaximised.CheckState = CheckState.Checked
                End If  '--yes-
            End If  '--exists-
            '--- Is now connected to sql server --
            lCount = 0

            '--  check current db..-
            '--  check current db..-
            '--  check current db..-
            If mbGetSelectValue("SELECT DB_NAME() AS current_db_name;", v1) Then
                sResult = CStr(v1)
            End If
            sList = ""
            sMsg = "CurrentDB is:  [" & sResult & "]; " & vbCrLf & "= Tables are: "
            Call gbLogMsg(gsRuntimeLogPath, "= Jobmatix is starting up.." & vbCrLf & _
                                           "  Current Database set to: " & sResult & vbCrLf)
            For Each col1 In mColSqlDBInfo
                sMsg = sMsg & col1.Item("TABLENAME") & "; "
                sList = sList & col1.Item("TABLENAME") & "; "
            Next
            '=If gbDebug Then
            '=MsgBox("Jobs DB collection has " & mColSqlDBInfo.Count & " tables.." & vbCrLf & sMsg & vbCrLf & vbCrLf & _
            '=         "Jobs SQL Schema info logged to:" & vbCrLf & _
            '=               gsJobMatixLocalDataDir() & "\" & msSqlDbName & "_Schema.txt", MsgBoxStyle.Information)
            '= End If
            '--  now check it..--
            If Trim(UCase(msSqlDbName)) <> Trim(UCase(sResult)) Then
                MsgBox("User has no access to JobTracking database:  [" & msSqlDbName & "]..", MsgBoxStyle.Critical)
                Me.Close()
                Exit Sub  '=End
            End If
            '----sSql = "USE " + msSqlDBName + vbCrLf
            '==GONE= labStatus.Text = "Creating Reporting Shape Connection to database: " & msSqlDbName

            If Not gbExecuteCmd(mCnnSql, "USE " & msSqlDbName & vbCrLf, L1, sErrorMsg) Then _
                                           MsgBox(vbCrLf & "Failed USE for DATABASE: '" & vbCrLf & _
                                                   msSqlDbName & "' " & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            If Not gbExecuteCmd(mCnnSql, "SET LOCK_TIMEOUT 15000" & vbCrLf, L1, sErrorMsg) Then _
                                         MsgBox(vbCrLf & "Failed SQL SET TIMEOUT: '" & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)

            '==GONE= labStatus.Text = "Shape Connection completed.."
            System.Windows.Forms.Application.DoEvents()


            '-- V2.0 = DB Upgrade- 09Dec2009 --
            '-- V2.0 = DB Upgrade- 09Dec2009 --
            '--  To ADD Tables for Quotes and RAs..-
            '--- DROP  FKEY CONSTRAINT from column taskType_id [JobTasks]..  Not done in Prev upgrade.-..
            '-- 1st to TEST if needed..--

            mbV20_Only = False
            '== mnuV21_Upgrade.Enabled = False

            '-- V2.1 = EXTRA DB Upgrade- 31Jan2010--
            '-- V2.1 = EXTRA DB Upgrade- 31Jan2010 --
            '--- DROP  FKEY CONSTRAINT from RA column RA_JobId [RAItems]..  Not all RAs have Jobs..--

            '== 3103.304 =
            '-- LATEST DB updates..--
            '-- LATEST DB updates..--
            '==  (ALL users)  Expand Cols ProblemLong & Notifications--
            '= 3431.0515= UPGRADE all AGAIN..  15May2018=

            sSql = "" : sMsg = "" : sMsg2 = ""
            If mColSqlDBInfo.Contains("jobs") Then  '-just in case..
                '= Dim colTable, colFields, colFieldx As Collection
                Dim sConstraintName As String
                colTable = mColSqlDBInfo.Item("jobs")
                colFields = colTable.Item("FIELDS")
                If colFields.Contains("ProblemLong") Then  '-expand to 4000 if not done..
                    colFieldx = colFields.Item("ProblemLong")
                    If (CInt(colFields.Item("Notifications")("CHAR_MAX_LENGTH")) < 5000) Or _
                         (CInt(colFields.Item("SessionTimes")("CHAR_MAX_LENGTH")) < 5000) Or _
                          (CInt(colFieldx.Item("CHAR_MAX_LENGTH")) < 5000) Then '-AND problemLong-- all need to expand-
                        '=3431.0515= sSql &= "ALTER TABLE dbo.jobs ALTER COLUMN ProblemLong VARCHAR (4000); " & vbCrLf
                        sSql &= msExpandColumnWithDefault("Jobs", "ProblemLong", 5000)
                        '=  see below=  sSql &= "ALTER TABLE dbo.jobs ALTER COLUMN ServiceNotes VARCHAR (4000) NOT NULL; " & vbCrLf
                        '= sSql &= "ALTER TABLE dbo.jobs ALTER COLUMN SessionTimes VARCHAR (4000); " & vbCrLf
                        sSql &= msExpandColumnWithDefault("Jobs", "SessionTimes", 5000)
                        '= Sql &= "ALTER TABLE dbo.jobs ALTER COLUMN Notifications VARCHAR (4000); " & vbCrLf
                        sSql &= msExpandColumnWithDefault("Jobs", "Notifications", 5000)
                        sMsg = "Note: For the Jobs Table columns: " & vbCrLf & _
                                  "  (ProblemLong, SessionTimes, Notifications)- " & vbCrLf & _
                                  "Column capacities are now 5000 chars.." & vbCrLf & vbCrLf
                    End If
                End If  '-ProblemLong-
                '= 3431.0513-
                '--   Increase ServiceNotes width to MAX..
                If colFields.Contains("ServiceNotes") Then  '-expand to MAX if not done..
                    Dim intNotesSize As Integer = CInt(colFields.Item("ServiceNotes")("CHAR_MAX_LENGTH"))
                    If (intNotesSize > 0) And (intNotesSize < 8000) Then
                        '--    first drop the DEFAULT constraint..
                        If mbFindDefaultConstraintName("Jobs", "ServiceNotes", sConstraintName) Then
                            sSql &= "ALTER TABLE dbo.Jobs DROP " & sConstraintName & " ;" & vbCrLf
                        End If
                        sSql &= "ALTER TABLE dbo.jobs ALTER COLUMN ServiceNotes VARCHAR(MAX) NOT NULL ; " & vbCrLf
                        '-- add DEFAULT-
                        sSql &= "ALTER TABLE dbo.Jobs ADD CONSTRAINT DF_Jobs_ServiceNotes DEFAULT '' FOR ServiceNotes ;  " & vbCrLf
                        sMsg2 = "Note2: The Jobs Table column 'ServiceNotes' " & vbCrLf & _
                                  "Column width is now VARCHAR(max) chars.." & vbCrLf
                    End If  '--size-
                End If  '--contains.
                '-- do it..
                If (sSql <> "") Then '-something to do..
                    '-- Ask if upgrade allowed.
                    If MessageBox.Show("Note: The Jobs column 'ServiceNotes' needs to be expanded to MAX capacity." & vbCrLf & _
                                       "Please backup the JobMatix database beforehand.." & vbCrLf & _
                                       "OK to update now ?", _
                                       "DB Column update", _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                       MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        '= sSql &= "ALTER TABLE dbo.jobs ALTER COLUMN ServiceNotes VARCHAR(max) NOT NULL; " & vbCrLf
                        '- ok to go ahead.. 
                    Else  '-no-
                        MessageBox.Show("ok-This update needs to be done " & vbCrLf & _
                                        "before proceeding with this version of JobMatix." & vbCrLf & _
                                        "JobMatix is closing..", _
                                        "Update needed..", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Hide()
                        Exit Sub
                    End If  '-yes-
                    '-- go-
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrorMsg) Then
                        s1 = "ERROR: Failed to update Jobs Columns widths.. " & vbCrLf & sErrorMsg
                        Call gbLogMsg(gsRuntimeLogPath, s1 & vbCrLf)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        MsgBox(s1, MsgBoxStyle.Critical)
                        Me.Hide()
                        Exit Sub
                    Else '--ok-
                        's1 = "Note: The Jobs Table columns: " & vbCrLf & _
                        '        "  (ProblemLong, ServiceNotes, SessionTimes, Notifications)- " & vbCrLf & _
                        '       "Column widths are now 4000 chars.." & vbCrLf
                        Call gbLogMsg(gsRuntimeLogPath, sMsg & sMsg2)
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        MessageBox.Show(sMsg & sMsg2, "JobMatix Main.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If '-exec-
                End If  '--sSql-
            End If  '--jobs-

            '== 3327.0119 = 19Jan2017=
            '-- MORE DB updates..--
            '==  (ALL users)  Expand Cols RA_Symptoms--
            sSql = ""
            If mColSqlDBInfo.Contains("RAItems") Then  '-just in case..
                colTable = mColSqlDBInfo.Item("RAItems")
                colFields = colTable.Item("FIELDS")
                If colFields.Contains("RA_Symptoms") Then  '-expand to 1000 if not done..
                    If (CInt(colFields.Item("RA_Symptoms")("CHAR_MAX_LENGTH")) < 511) Then
                        sSql &= "ALTER TABLE dbo.RAItems ALTER COLUMN RA_Symptoms VARCHAR (511) NOT NULL; " & vbCrLf
                    End If  '--1000-
                End If  '-RA_Symptoms-
                '--  Do it..
                If (sSql <> "") Then '-something to do..
                    If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrorMsg) Then
                        s1 = "ERROR: Failed to update RA_Symptoms Columns widths.. " & vbCrLf & sErrorMsg
                        Call gbLogMsg(gsRuntimeLogPath, s1 & vbCrLf)
                        MsgBox(s1, MsgBoxStyle.Critical)
                    Else '--ok-
                        s1 = "Note: The RAItems Table column: " & vbCrLf & _
                                "  (RA_Symptoms)- " & vbCrLf & _
                               "Column width is now 511 chars.." & vbCrLf
                        Call gbLogMsg(gsRuntimeLogPath, s1 & vbCrLf)
                        MsgBox(s1, MsgBoxStyle.Information)
                    End If '-exec-
                End If  '--sSql-
            End If  '-ra items-
            '==  END OF - Expand Cols RA_Symptoms--

            '== 3.3.3357.0205  =05-Feb-2017=
            '== MORE Updates to RAs- 
            '-- 1. expand RM_Item Barcode to 40 chars-- (WAS also done in "frmMigration" !!-
            '-- 2.  Add column RM_SerialAudit_id" "to RAItems Table ==--

            '== sSql &= "ALTER TABLE dbo.RAItems ALTER COLUMN RM_ItemBarcode VARCHAR (40) NOT NULL; " & vbCrLf
            '== sSql &= "ALTER TABLE dbo.RAItems ADD RM_SerialAudit_id int NOT NULL DEFAULT -1; "
            sSql = ""
            sMsg = ""
            If mColSqlDBInfo.Contains("RAItems") Then  '-just in case..
                colTable = mColSqlDBInfo.Item("RAItems")
                colFields = colTable.Item("FIELDS")
                If colFields.Contains("RM_ItemBarcode") Then  '-expand to 1000 if not done..
                    If (CInt(colFields.Item("RM_ItemBarcode")("CHAR_MAX_LENGTH")) < 40) Then
                        sSql &= "ALTER TABLE dbo.RAItems ALTER COLUMN RM_ItemBarcode VARCHAR (40) NOT NULL; " & vbCrLf
                        sMsg &= "TABLE dbo.RAItems-  COLUMN RM_ItemBarcode was expanded to (40) chars.." & vbCrLf
                    End If
                End If '-barcode-
                '-- add model No col if not there..
                If (Not colFields.Contains("RM_SerialAudit_id")) Then  '-must add-
                    sSql &= "ALTER TABLE dbo.RAItems ADD RM_SerialAudit_id int NOT NULL DEFAULT -1; " & vbCrLf
                    sMsg &= "TABLE dbo.RAItems-  COLUMN RM_SerialAudit_id was ADDED.." & vbCrLf
                End If
                If (sSql <> "") Then '-something to do..
                    If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrorMsg) Then
                        s1 = "ERROR: Failed to update ADD RAItems RM_SerialAudit_id Column.. " & vbCrLf & sErrorMsg
                        Call gbLogMsg(gsRuntimeLogPath, s1 & vbCrLf)
                        MsgBox(s1, MsgBoxStyle.Critical)
                    Else '--ok-
                        Call gbLogMsg(gsRuntimeLogPath, s1 & vbCrLf)
                        s1 = "Pls note: RAItems Table def. has been updated." & vbCrLf & sMsg & vbCrLf
                        MsgBox(s1, MsgBoxStyle.Information)
                    End If '-exec-
                End If  '-ssql-
            End If  '-Contains("RAItems")-
            '- end 3357 RA updates..-

            '==
            '==  3431.0523- 23-may-2018=
            '==  3431.0523- 23-may-2018=
            '==  3431.0523- 23-may-2018=
            '==  3431.0523- 23-may-2018=
            '==    --  Add Create Trigger (modAlterTableTrigger) for ALTER_TABLE..
            Dim bTriggerUpdated As Boolean = False

            If gbCreateAlterTableTrigger(mCnnSql, bTriggerUpdated) Then
                If bTriggerUpdated Then
                    MessageBox.Show("Info only- The AlterTable DB Trigger was updated ok.", _
                                    msJobMatixVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If  '-create-

            '-- 
            '-- END of DB updates..--
            '-- END of DB updates..--

            Call gbLogMsg(gsRuntimeLogPath, "Checking Licence.. " & vbCrLf)

            '--  Get Path to RetailManager (Jet) database..--
            '--every client m/c has its own Jet path key info systemInfo..--
            '--- because it has its own unique path to the server..--
            '==  SEE RMConnect ====    msJetPathInfoKey = "RM_JetPath_" + msComputerName

            '= Me.Opacity = 0.9
            '-- get job system settings..--
            labStatus.Text = "Loading Jobs DB SystemInfo.. "


            '==   Target-New-Build-6201 --  (18-June-2021)
            '-- ccharge up the MD5 constant..--
            '==  END Target-New-Build-6201 --  (18-June-2021)

            '= get Create Date..
            '==3311.224-For Each col1 In mColSystemInfo
            '==3311.224-    If col1.Item("systemkey") = "JT2SECURITYID" Then '--user created at CREATE time..
            '==3311.224-      msJT2SecurityIdOriginal = col1.Item("systemvalue")
            '==3311.224-      mdDateCreated = CDate(col1.Item("datecreated"))
            '==3311.224-    End If '--key..-
            '==3311.224-Next col1 '--col1-

            '==3311.224-
            '==- new systemInfo class.
            If mSysInfo1.itemDateCreated("JT2SECURITYID", mdDateCreated) Then
                '-ok-
                msJT2SecurityIdOriginal = mSysInfo1.item("JT2SECURITYID")
                '==3311.831- MsgBox("TEST- Found JT2SECURITYID= " & msJT2SecurityIdOriginal & vbCrLf & _
                '==3311.831-          "Date Created=" & VB.Format(mdDateCreated, "dd-MMM-yyyy HH:mm "))
            ElseIf (msBusinessABN <> "82563967866") Then  '-only Precise doen't have this.
                MsgBox("ERROR- Failed to retrieve SystemInfo DateCreated..", MsgBoxStyle.Exclamation)
            End If '-itemdate

            '-- C O M P U T E   L I C E N C E ----
            '-- C O M P U T E   L I C E N C E ----
            '-- C O M P U T E   L I C E N C E ----
            '-- C O M P U T E   L I C E N C E ----
            mIntMaxUsersPermitted = K_ABSOLUTE_MAX_USERS_PERMITTED

            '==   Target-New-Build-6201 --  (18-June-2021)
            '--  Licence computing now done in clsKeygen42..
            '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
            '==
            '==   Target-New-Build-6201 --  (18-June-2021)
            '==   Target-New-Build-6201 --  (15-June-2021)
            '==
            '==  For JobMatix62Main- OPEN SOURCE version...
            '==  For JobMatix62Main- OPEN SOURCE version...
            '==
            '==  FOR LicenceKey- ComputePosKey now points to dummy in clsKeygen42 in DLL JMxKeyGen420_OS
            '==
            '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


            Dim clsKeyGen1 As JMxKeyGen420.clsKeyGen42
            Dim bIsPosSys As Boolean = False   '-- Is JobTrackinng.

            clsKeyGen1 = New JMxKeyGen420.clsKeyGen42
            '-- NOW used class for compute..
            '--  First- Send msOriginalJobMatixDBName==
            clsKeyGen1.OriginalJobMatixDBName = msOriginalJobMatixDBName

            If Not clsKeyGen1.ComputePosKey(msBusinessABN, msBusinessPostCode, msBusinessShortName,
                                       msSqlDbName, bIsPosSys, sComputedKey,
                                      sComputedKeyLevel2, sComputedKeyThreeUser, sComputedKeyTwoUser, sComputedKeySingleUser) Then
                MsgBox("Failed to get Licence Keys..", MsgBoxStyle.Exclamation)

            Else  '-computed ok..
                sMsg = "Level-1 Computed FULL key is: " & vbCrLf & sComputedKey & vbCrLf & vbCrLf &
                 "Level-2 Computed key is: " & vbCrLf & sComputedKeyLevel2 & vbCrLf & vbCrLf &
                    "ThreeUser Computed key is: " & vbCrLf & sComputedKeyThreeUser & vbCrLf & vbCrLf &
                    "TwoUser Computed key is: " & vbCrLf & sComputedKeyTwoUser & vbCrLf & vbCrLf &
                     "Single-User Computed key is: " & vbCrLf & sComputedKeySingleUser & vbCrLf
                '=-- TEST- 
                '== MsgBox(sMsg, MsgBoxStyle.Information)
                '- Check Site POS Licence if any..
                '- Check Site POS Licence if any..
            End If  '-compute-

            '== END Target-New-Build-6201 --  (18-June-2021)
            '== END Target-New-Build-6201 --  (18-June-2021)

            '--   L I C E N C E  C H E C K ----
            '--   L I C E N C E  C H E C K ----
            '--   L I C E N C E  C H E C K ----

            '--  GET DateCreated of "BusinessABN" info row..-
            '----- and check licence date and key..--
            '-------  note SERVER Name was used in PRECISE Licence build..====
            '-- RECREATE securityId and dateCreated from ABN Date in systemInfo.-
            msJT2SecurityId = gsMakeSecurityId(mCnnSql, dateX) '--re-computed for checking..
            '-- -- From[modCreateJobs3]-] --

            s3 = "Re-computed SecurityId is: " & msJT2SecurityId & vbCrLf & _
                    "DateCreated is: " & Format(mdDateCreated, "dd-MMM-yyyy, HH:mm:ss")
            '== If gbDebug Then
            '== MsgBox(s3, MsgBoxStyle.Information)
            '== End If
            '-- NB: if (msJT2SecurityId <> msJT2SecurityIdOriginal) then TAMPERING has neen done !!..--

            If (msLicenceKey <> "") Then '--have licence..  check it..-
                '== Target-New-Build-6201 --  (18-June-2021)
                'mbLicenceOK = mbCheckLicenceKey(msLicenceKey, sComputedPreciseShortKey,
                '                               sComputedKey, sComputedKeyLevel2,
                '                                sComputedKeyThreeUser, sComputedKeyTwoUser,
                '                                  sComputedKeySingleUser,
                '                                    bIsLevel2Licence, mIntMaxUsersPermitted)
                mbLicenceOK = clsKeyGen1.CheckLicenceKey(msLicenceKey,
                                           sComputedKey, sComputedKeyLevel2,
                                            sComputedKeyThreeUser, sComputedKeyTwoUser,
                                              sComputedKeySingleUser,
                                                bIsLevel2Licence, mIntMaxUsersPermitted)
                '== END Target-New-Build-6201 --  (18-June-2021)
                If Not mbLicenceOK Then
                    MsgBox("NB- The Licence key on file is not a valid JobTracking key for this installation.." & vbCrLf &
                                           "Please enter a valid licence key..", MsgBoxStyle.Information)
                    msLicenceKey = ""
                Else  '--ok-
                    If Not bIsLevel2Licence Then mbIsFullLicence = True

                    '== Target-New-Build-6201 --  (23-June-2021)
                    If (mIntMaxUsersPermitted = -1) Then  '--  means no limit..
                        mIntMaxUsersPermitted = K_ABSOLUTE_MAX_USERS_PERMITTED
                    End If
                    '== END Target-New-Build-6201 --  (18-June-2021)

                End If  '-ok-
            End If  '--no licence..-

            '==3083==mlDatabaseDays = DateDiff(Microsoft.VisualBasic.DateInterval.Day, mdDateCreated, CDate(DateTime.Today))
            mlDatabaseDays = gIntDateDiffDays(mdDateCreated, DateTime.Today)

            '- Enter licence if none yet..-
            '== Target-New-Build-6201 --  (15-June-2021)
            If (msLicenceKey = "") And (Not clsKeyGen1.LicenceRequired) Then '--no licence..-
                '--not required--  Open Source version..
                mbLicenceOK = True
                mbIsFullLicence = True
                '= mIntMaxUsersPermitted = -1  '-unlimited-
                mIntMaxUsersPermitted = K_ABSOLUTE_MAX_USERS_PERMITTED
                MsgBox("Open Source Version-  No EUL needed.", MsgBoxStyle.Information)
                '== END  Target-New-Build-6201 --  (15-June-2021)
            ElseIf (msLicenceKey = "") Then '--no licence..-  IS required..
                ok = False
                If (mlDatabaseDays <= 42) Then
                    If (MsgBox("There are " & CStr(42 - mlDatabaseDays) & " days left for this evaluation.." & vbCrLf &
                                 "Do you have a valid Licence Key to enter? ",
                                 MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                        mbLicenceOK = True '--bypass licence for now..-
                        mbIsFullLicence = True  '==3067.0== --trial has full functions..--
                        ok = True
                    End If
                Else '--expired..-
                    MsgBox("Evaluation period has expired..", MsgBoxStyle.Exclamation)
                End If
                lCount = 0
                While (Not ok) And (lCount < 3)
                    '====Else   '--expired..--
                    s1 = InputBox("Please enter a valid Licence Key:", "JobMatix Licence.")
                    s1 = Replace(s1, " ", "") '--delete blanks..--
                    If s1 = "" Then '--nag..-
                        lCount = lCount + 1
                        If (lCount >= 3) Then mbLicenceOK = False
                    Else '--check licence..--
                        '== Target-New-Build-6201 --  (18-June-2021)
                        'mbLicenceOK = mbCheckLicenceKey(s1, sComputedPreciseShortKey,
                        '                                 sComputedKey, sComputedKeyLevel2,
                        '                                  sComputedKeyThreeUser, sComputedKeyTwoUser,
                        '                                    sComputedKeySingleUser,
                        '                                      bIsLevel2Licence, mIntMaxUsersPermitted)
                        mbLicenceOK = clsKeyGen1.CheckLicenceKey(s1,
                                           sComputedKey, sComputedKeyLevel2,
                                            sComputedKeyThreeUser, sComputedKeyTwoUser,
                                              sComputedKeySingleUser,
                                                bIsLevel2Licence, mIntMaxUsersPermitted)
                        '== END Target-New-Build-6201 --  (18-June-2021)
                        If mbLicenceOK Then
                            msLicenceKey = s1
                            If bIsLevel2Licence Then
                                ok = True
                            Else  '--full licence-
                                If mbIsRetailManager() Or mbIsJobmatixPOS() Then
                                    mbIsFullLicence = True
                                    ok = True
                                Else
                                    MsgBox("Note: Only MYOB or JobMatixPOS installations can have full licence.." & vbCrLf &
                                           vbCrLf & "Please enter a Level-2 licence key..", MsgBoxStyle.Exclamation)
                                End If
                            End If   '--level2--
                            '--   UPDATE systemInfo..-
                            If Not mSysInfo1.UpdateSystemInfo(New Object() {"LicenceKey", msLicenceKey}) Then
                                MsgBox("Failed to update LicenceKey details in systemInfo table..", MsgBoxStyle.Critical)
                            End If

                            '== Target-New-Build-6201 --  (23-June-2021)
                            If (mIntMaxUsersPermitted = -1) Then  '--  means no limit..
                                mIntMaxUsersPermitted = K_ABSOLUTE_MAX_USERS_PERMITTED
                            End If
                            '== END Target-New-Build-6201 --  (18-June-2021)

                        Else  '--invalid..--
                            MsgBox("Key does not seem to be valid for this installation..", MsgBoxStyle.Exclamation)
                            lCount = lCount + 1
                            If (lCount >= 3) Then mbLicenceOK = False
                        End If
                    End If '--empty-
                End While '--OK..----End If  '--days..-
            End If '--no licence..-

            '-- NB: if (msJT2SecurityId <> msJT2SecurityIdOriginal) then TAMPERING has neen done !!..--
            If (msJT2SecurityId <> msJT2SecurityIdOriginal) And (msBusinessABN <> "82563967866") Then
                '--EXCLUDES PRECISE..  -- TAMPERING has neen done !!..--
                MsgBox("Note: Important licence information has been changed.." & vbCrLf & _
                            "Licence can not be validated.", MsgBoxStyle.Exclamation)
                mbLicenceOK = False
            End If
            '--don't show added underscores in ShortName....-
            LabBusiness.Text = Replace(msBusinessShortName, "_", " ") & " " & msBusinessState & " " & msBusinessPostCode
            LabBusiness.Text &= "; DB: " & msSqlDbName & "  Copyright © Aztrinity 2014-2019.."

            If Not mbLicenceOK Then
                mIntMaxUsersPermitted = 1
                MsgBox("Product not licenced.." & vbCrLf & vbCrLf & _
                        "(See JobMatix website for Licence details..) ", MsgBoxStyle.Exclamation)
            End If
            '== labStatus.Text = "Connecting to RetailManager database: " & msJetDbName

            If (mIntMaxUsersPermitted <= 3) Then  '-restricted..
                LabBusiness.Text = LabBusiness.Text & " =Max: " & mIntMaxUsersPermitted & " user(s).="
            End If  '--users.-
            System.Windows.Forms.Application.DoEvents()
            Call gbLogMsg(gsRuntimeLogPath, "Finished checking Licence.. " & vbCrLf)

            '--  JobMatix V3..---05-Oct-2011==
            '--  NEW  Multi-Host ----
            '--  NEW  Multi-Host ----
            '--  NEW  Multi-Host ----
            bool1 = True

            If (LCase(msRetailHostname) = "retailmanager") Then
                '-- SET RETAIL MANAGER as Host Provider (Class)--
                msProviderCode = "RM"
                '= cmdPOS.Visible = False
                labStatus.Text = "Connecting to RetailManager database: " & msJetDbName
                LabQuotesHdr.Text = "RetailManager Quotes"
                System.Windows.Forms.Application.DoEvents()
                '=4219.1122= mRetailHost1 = New clsRetailHost
                mRetailHost1 = New JMxRetailHost.clsRetailHost
                If Not mbRMConnect("RM") Then
                    MsgBox("No RM connection..", MsgBoxStyle.Critical)
                    Me.Close()
                    Exit Sub
                    '==   End
                Else
                    Call gbLogMsg(gsRuntimeLogPath, "Connected to RetailManager database: " & msJetDbName & vbCrLf)
                End If
            ElseIf (LCase(msRetailHostname) = "jobmatixpos") Then  '--JM POS--
                labStatus.Text = "Setting Connection to JobMatix POS database.. "
                LabQuotesHdr.Text = "JobMatix POS Quotes"
                System.Windows.Forms.Application.DoEvents()
                msProviderCode = "JMPOS"
                '== cmdPOS.Visible = True
                '--  create new JobMatixPOS interface class..
                '=4219.1122= mRetailHost1 = New clsRetailHostJMPOS
                mRetailHost1 = New JMxRetailHost.clsRetailHostJMPOS

                '--Initialise:  Pass sql connection, dbname and colTables..
                Call mRetailHost1.SetConnection(msServer, mCnnSql, mColSqlDBInfo, msSqlDbName)

            Else '--Northwind..--
                msSqlDbName = "Northwind"
                '-  use NORTHWIND class.. (SQL Server) --
                '==  TEMP..  NO Northwind..-
                '=== Set mRetailHost1 = New clsRetailHostNW
                '=== If Not mRetailHost1.connect(msServer, msSqlDbName, s1, s1, bool1) Then
                '===       Exit Sub
                '=== End If
            End If '--host..-

            If mbIsRetailManager() Then
                btnCustNewCustomer.Visible = False   '-3411.0217=
                msSignOnMsg = "Note:  " & vbCrLf & _
                          "RM Staff SignOn (RetailManager Staff Barcode) is needed to access to JobMatix Functions."
            ElseIf mbIsJobmatixPOS() Then
                msSignOnMsg = "Note:  " & vbCrLf & _
                          "Staff SignOn (JobMatixPOS Staff Barcode) is needed to access to JobMatix Functions."
            Else
                '==3083== LabEnterStaffId.Text = "Enter Quickbooks staff ID: "
                msSignOnMsg = "Please Note:" & vbCrLf & _
                           "Staff SignOn (Quickbooks PersonID)  needed to access JobMatix Functions."
            End If

            '-- =END=   NEW  Multi-Host ----
            '-- =END=   NEW  Multi-Host ----
            '-- =END=   NEW  Multi-Host ----

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '== Call main_showSQL(mCnnJet, msJetDbName, mColJetDBInfo)  '--log all Jet schema info--
            '--  JET schema was saved by Retail-host..
            labStatus.Text = "  DB connections complete.."
            '==3083== txtJetDBName.Text = msJetDbName

            '---------If Not gbDebug Then mnuTest.Enabled = False
            s1 = "=V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & _
                                                           ", Rev: " & My.Application.Info.Version.Revision & "="
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '==331- Load RAs Shipping-label printer Combo-
            '= 3411.121= cboRAs_A4Printers.Items.Clear()
            labStatus.Text = vbCrLf & "  Checking Default Printer.."
            msDefaultPrinterName = ""  '== Printer.DeviceName '--  save name of original default printer..-
            '--  find name of default printer..--
            For ix = 0 To (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count - 1)
                s1 = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Item(ix)
                '--  set printer selected so we can test....--
                pd1.PrinterSettings.PrinterName = s1
                If pd1.PrinterSettings.IsDefaultPrinter Then
                    msDefaultPrinterName = s1
                    '= Exit For
                End If
                '== ListBox1.Items.Add(pkInstalledPrinters)
                '== cboPrtSelect.Items.Add(pkInstalledPrinters)
                '= 3411.121= cboRAs_A4Printers.Items.Add(s1)
            Next ix
            '== L1 = Err().Number
            '== On Error GoTo activate_error
            If (msDefaultPrinterName = "") Then '== L1 <> 0 Then '--no printer..-
                MsgBox("Error in getting Default Printer.." & vbCrLf & _
                       "Error is " & L1 & " = " & ErrorToString(L1) & vbCrLf & vbCrLf & _
                          "Printers may not be set up in Windows.." & vbCrLf & _
                             "JobTracking printing may not be available..", MsgBoxStyle.Exclamation)
            End If

            '-- check out if "JobPrinters.txt" exists in local (app) directory..--
            '---  it should have  "prtAgreement=devicename" --
            '----    and "prtReceiptt=devicename" -----
            '-- for each function, find printer via printers collection..-
            '--- and set mPrtAgreement or mPrtReceipt to the related printer object..--
            '--Call mbLoadSettings  '--refresh..-
            labStatus.Text = vbCrLf & "  Checking Job Printers..."
            msColourPrtName = mLocalSettings1.item("PRTCOLOUR") '= mSdSettings.Item("PRTCOLOUR")
            msReceiptPrtName = mLocalSettings1.item("PRTRECEIPT") '= mSdSettings.Item("PRTRECEIPT")
            msLabelPrtName = mLocalSettings1.item("PRTLABEL") '= mSdSettings.Item("PRTLABEL")

            '--  if no saved printer names, then..---
            If (msColourPrtName = "") Then
                '== Call mbSetPrinter(msDefaultPrinter, 0)
                msColourPrtName = msDefaultPrinterName
                Call mbSaveSetting("PRTCOLOUR", msDefaultPrinterName)
                MsgBox("Service agreements will print to your default printer !!" & vbCrLf & _
                       "Unless you change the JobMatix printer setup.." & vbCrLf & _
                           "(Main Menu ->File/Printers..)", MsgBoxStyle.Exclamation)
            End If
            '=3311.319- select current printer if any.
            '= 3411.121= Gone-
            'If (cboRAs_A4Printers.Items.Count > 0) Then
            '    cboRAs_A4Printers.SelectedIndex = 0   '-case-
            '    For Each s1 In cboRAs_A4Printers.Items
            '        If (LCase(msColourPrtName) = LCase(s1)) Then
            '            cboRAs_A4Printers.SelectedItem = s1
            '        End If
            '    Next s1
            'End If
            '-- done shipping printer.
            If (msReceiptPrtName = "") Then
                '== Call mbSetPrinter(msDefaultPrinter, 1)
                Call mbSaveSetting("PRTRECEIPT", msDefaultPrinterName)
                MsgBox("Job RECEIPTS will print to your default printer !!" & vbCrLf & _
                       "Unless you change theJobMatix printer setup.. ." & vbCrLf & _
                          "(Main Menu ->File/Printers..)", MsgBoxStyle.Exclamation)
            End If
            If (msLabelPrtName = "") Then
                '==Call mbSetPrinter(msDefaultPrinter, 2)
                '==Call mbSaveSetting("PRTLABEL", msDefaultPrinter)
                MsgBox("No Job LABELS Printer is set up.. " & vbCrLf & vbCrLf & _
                        "You can set it up in printer setup..." & vbCrLf & _
                           "(Main Menu ->File->Printers..)", MsgBoxStyle.Information)
            End If

            mnuFile.Enabled = True
            '--mnuQueries.Enabled = True
            mnuAdmin.Enabled = False
            '-- admin available only on server machine..--
            '== mnuDatabase.Visible = False
            '====If (LCase(msSqlUid) = "sa") Then
            If gbIsSqlAdmin() Then
                mnuAdmin.Visible = True
                mnuAdmin.Enabled = False '--until signon..-
                '==If (UCase(msServer) <> UCase(msComputerName)) Then
                '==   mnuRestoreJobsDB = False
                '==End If  '--server..
            Else '--normal user..-
                '== 3083== mnuDatabase.Visible = True
                mnuAdmin.Visible = False
            End If '--real admin..--
            mnuReference.Enabled = False
            '===mnuAdmin.Visible = False
            '--  load cboMonths combo with month job history..-
            '--  load cboMonths combo with month job history..-
            '--CboMonths.Clear
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            labStatus.Text = vbCrLf & "Checking Jobs history.. "
            System.Windows.Forms.Application.DoEvents()
            '-- REPORTS.. find oldest job date..-
            mDateOldest = (DateAdd(Microsoft.VisualBasic.DateInterval.Year, -1, CDate(DateTime.Today))) '--default to 1 year ago..-
            sSql = "SELECT MIN(DateCreated) as OldestJobDate FROM [Jobs]; "
            If mbGetSelectValue(sSql, v1) Then
                mDateOldest = CDate(v1)
            Else
                '=MsgBox("No Job date info found..", MsgBoxStyle.Exclamation)
                MsgBox("No Jobs Found.." & vbCrLf & vbCrLf & _
                          "Click on the Customer Tab to select a Retail Customer; " & vbCrLf & _
                          vbCrLf & "Then click 'New Workshop Job' to start a job.", MsgBoxStyle.Information)
            End If '--get-

            '==lCount = 24 '-maxx 24 months offered..-
            '==dateX = Date
            '==While (lCount > 0) And (dateX >= dateOldest)
            '==     CboMonths.AddItem Format(dateX, "yyyy-mmm")
            '==     dateX = DateAdd("m", -1, dateX) '--go back a week at a time..-
            '==     lCount = lCount - 1
            '==Wend
            s1 = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & " " & _
                                                            Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)
            s2 = ""
            If mbLicenceOK And (Not mbIsFullLicence) Then s2 = "[L2]"
            LabBusinessId.Text = "Bus.Id: ABN- " & s1 & "==" & msJT2SecurityId & "." & s2 & "."
            '--  Precise only..-
            '--   ---  ABN=82563967866;
            '==If (LCase(msSqlUid) = "sa") And (msBusinessABN = "82563967866") Then
            If gbIsSqlAdmin() And (msBusinessABN = "82563967866") Then
                Call gbReformatJobCustomerName(mCnnSql) '--check for old custname format..-
            End If
            labStatus.Text = vbCrLf & "Startup is completing for user: " & msCurrentUserName & ".."
            System.Windows.Forms.Application.DoEvents()
            '-- wait 3 seconds..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            lngStart = CInt(VB.Timer()) '--starting seconds.-
            While CInt(VB.Timer()) < lngStart + 3
                System.Windows.Forms.Application.DoEvents()
            End While
            '-- load priorities from SystemInfo..--
            '== MOVE to Show Job Info-
            '=3311.331= If gbGetPriorityDescriptorsEx(mCnnSql, mbOnSiteJob, mRetailHost1, mColPriorities) Then
            '=3311.331= '= gbGetPriorityDescriptors(mCnnSql, mColPriorities) Then
            '=3311.331= For Each v1 In mColPriorities
            '=3311.331= cboPriority.Items.Add(CStr(v1))
            '=3311.331= Next v1 '--v1-
            '=3311.331= End If
            cboPriority.SelectedIndex = -1 '--not selected..-
            cboPriority.Enabled = False
            cboPriority.Visible = False

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            ToolTip1.SetToolTip(labJobsExplorer, K_EXPLORER_TOOLTIP)
            miLastTabNo = 0
            colFields = Nothing
            '==NOT USED=  cmdClose.Enabled = True

            '-- Set user font size..--
            s1 = "9"  '--default..- for Lucida Sans.
            If mLocalSettings1.queryLocalSetting(K_GRIDFONTSIZE_SETTINGNAME, s2) Then '= mSdSettings.Exists(K_GRIDFONTSIZE_SETTINGNAME) Then
                s1 = s2  '=mSdSettings(K_GRIDFONTSIZE_SETTINGNAME)
            End If  '--exists-
            If IsNumeric(s1) Then
                Call mbSetGridFontSize(CInt(s1))
            End If '-- numeric-

            '==   Target-New-Build-6201 --  (18-June-2021)
            '= Me.Text = "JobMatix42- Jobs, Quotes and RA's.." & "     [This Computer: " & msComputerName '== & "]"
            Me.Text = "JobMatix62- Jobs, Quotes and RA's.." & "     [This Computer: " & msComputerName '== & "]"
            '== END  Target-New-Build-6201 --  (18-June-2021)

            If mbIsThinClient Then
                Me.Text &= " (Thin client)] "
            Else
                Me.Text &= "] "
            End If
            '-- check for view server permission.-
            '==If (Not gbIsSqlAdmin()) AndAlso (Not gbIsPermissionGranted(mCnnSql, msCurrentUserName, "VIEW SERVER STATE")) Then
            '==MsgBox("User must have VIEW SERVER STATE sql permission", MsgBoxStyle.Exclamation)
            '==Me.Close()
            '==End
            '==End If

            '-  Check who's logged in..
            If Not mbCheckLoggedInUsers() Then
                Me.Close()
                Exit Sub '=End
            End If  '--users..-
            '--ok--
            '== cmdRefreshLogins.Enabled = True
            If (mIntMaxUsersPermitted <= 3) Then
                Call gbLogMsg(gsRuntimeLogPath, "= JobMatix Startup: MAX: " & _
                                           mIntMaxUsersPermitted & " user(s).. " & vbCrLf & _
                                                         "== Main timer is starting.." & vbCrLf)
            Else
                Call gbLogMsg(gsRuntimeLogPath, "= JobMatix Startup.. " & vbCrLf & _
                                "== Main timer is starting.." & vbCrLf)
            End If
            '-- Timer can start..--
            '= not yet..Timer1.Enabled = True

            '== Me.Opacity = 1
            '===txtStaffName.SetFocus '--  must enter staff no..--
            frameJobsTab.Visible = True
            System.Windows.Forms.Application.DoEvents()
            '== MsgBox("Testing.. New clsPOS31Main..", MsgBoxStyle.Information)

            '==3101.1226==Boxing day.-
            '=  NB:  For POS, staff_id not available, as no sign-on yet..

            '==3401.0315==  Mult-POS Sales...-
            '=  NB:  For POS, can have multiple Sales..
            '==   Swapping buttons are monitored here in JobMatix Main....

            If mbIsJobmatixPOS() Then
                'mClsSale1 = New clsPOS31Main(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
                '                  mColSqlDBInfo, msJobMatixVersion, Picture2.Image, _
                '                         mlStaffId, msStaffName)
                '-- POS set up Sale stuff..--
                '-- POS set up Sale stuff..--
                '-- POS set up Sale stuff..--

                '=  3401.307=  Base class renamed..
                '-- AND we have THREE Sale Instances..

                '=3411.1221= POS stuff now GONE to its own EXE..
                '=3411.1221= POS stuff now GONE to its own EXE..

                'btnSaleHold.Enabled = False
                'btnSaleRestore1.Enabled = False
                'btnSaleRestore1.Text = ""
                'btnSaleRestore1.BackColor = Color.LightGray '-enabled = cornflowerBlue-

                'btnSaleRestore2.Enabled = False
                'btnSaleRestore2.Text = ""
                'btnSaleRestore2.BackColor = Color.LightGray  '-enabled = Plum-

                'labSaleHeld1Lab.Enabled = False
                'labSaleHeld2Lab.Enabled = False
                msVersionPOS = msJobMatixVersion

                '- Make them all now so they're ready.
                'mClsSale_A = New clsPOS34Sale(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
                '                              mColSqlDBInfo, msVersionPOS, Picture2.Image, _
                '                                 mlStaffId, msStaffName)
                'mClsSale_B = New clsPOS34Sale(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
                '                              mColSqlDBInfo, msVersionPOS, Picture2.Image, _
                '                                 mlStaffId, msStaffName)
                'mClsSale_C = New clsPOS34Sale(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
                '                              mColSqlDBInfo, msVersionPOS, Picture2.Image, _
                '                                 mlStaffId, msStaffName)
                ''--make collection-
                'mColFreeSalesInstances = New Collection
                ''== start with  Inst. _A for first transaction current..
                'mClsSale1 = mClsSale_A
                'msSaleCurrent_ID = "A"  '-A instance is current.-
                ''=
                ''- B and C are still free.
                'mColFreeSalesInstances.Add(mClsSale_B)
                'mColFreeSalesInstances.Add(mClsSale_C)
                ''-- POS Licence--
                'mClsJmxPOS31_Licence = New clsJMxPOS31(mCnnSql, msServer, msSqlDbName, mColSqlDBInfo, gsErrorLogPath)
                ''=Private mbIsEvaluating As Boolean = False
                'If Not mClsJmxPOS31_Licence.LicenceCheckPOS(mbIsEvaluating) Then
                '    MsgBox("No POS Licence..", MsgBoxStyle.Information)
                'ElseIf mbIsEvaluating Then
                '    MsgBox("Still evaluating POS Licence..", MsgBoxStyle.Information)
                'End If
                ''-- Check which till..-
                ''-- Startup- Check we have Till assigned..
                'Dim strOurTillId As String = ""
                'If Not mClsJmxPOS31_Licence.StartupTillCheck(strOurTillId) Then
                '    txtSaleCustBarcode.Text = "NO TILL!"
                '    txtSaleCustBarcode.ReadOnly = True
                '    txtSaleStaffBarcode.ReadOnly = True
                'Else  '--ok-
                '    labSaleTillID.Text = "- Till-" & strOurTillId & " -"
                '    MsgBox("You are currently assigned to Till-" & strOurTillId, MsgBoxStyle.Information)
                'End If  '-till check-
                'btnSaleHold.Enabled = True

                '=3411.1221= END OF POS stuff now GONE to its own EXE..

            Else  '-retail manager.  no JMPOS-
                '== Dim tabpagePos As TabPage = TabControlMain.TabPages("TabPagePOS")
                '= TabControlMain.TabPages.Remove(tabpagePos)
            End If '-pos-
            System.Windows.Forms.Application.DoEvents()

            '=3311.309= In-house RAs..--
            '-- Start RA Main.
            '= 3411.121= frameRAsTab.Visible = True
            '= 3411.0121-  RAs Gone- to EXE.. 
            'mClsRAsMain1 = New clsRAsMain33(Me, Picture2.Image, labStatus, _
            '                                     msServer, labVersion.Text, mCnnSql, msSqlDbName, mColSqlDBInfo, _
            '                                       msStaffBarcode, True, mRetailHost1)
            labStatus.Text &= " Done.."
            DoEvents()  '--let it settle..

            '==3311.401=
            '--  Must re-configure priority/Labour Prices from Retail barcodes.
            '--   If not yet done..
            s1 = mSysInfo1.item("LabourRateP1RetailBarcode")
            '-- TEST if barcodes setup yet..
            If ((Not mSysInfo1.exists("LabourRateP1RetailBarcode")) Or _
                        (mSysInfo1.exists("LabourRateP1RetailBarcode") And (s1 = ""))) Or _
                 (Not mSysInfo1.exists("LabourRateOnSiteP1RetailBarcode")) Or _
                    (Not mSysInfo1.exists("LabourRateOnSiteP2RetailBarcode")) Or _
                        (Not mSysInfo1.exists("LabourRateOnSiteP3RetailBarcode")) Then

                '== Target-build-4267  (Started 07-Sep-2020)
                '== Target-build-4267  (Started 07-Sep-2020)
                '==   -- If POS is mbIsJobmatixPOS, and Stock Not imported from MYOB,
                '==              then set up SysInfo barcodes for Labour Rates..
                Dim bLabourMsgNeeded As Boolean = True
                Dim dtLabourRates As DataTable

                If mbIsJobmatixPOS() Then
                    '-- check Stock Table for Labour Rates..
                    If gbGetDataTable(mCnnSql, dtLabourRates, _
                                        "SELECT * FROM dbo.stock WHERE (barcode LIKE '01-LAB-HRLY-%') ") Then
                        If (dtLabourRates IsNot Nothing) AndAlso (dtLabourRates.Rows.Count > 0) Then
                            '-set up barcodes in jobTracking.
                            For Each rowStock As DataRow In dtLabourRates.Rows
                                Select Case UCase(rowStock.Item("barcode"))
                                    Case "01-LAB-HRLY-WKSH-P1"
                                        Call mSysInfo1.UpdateSystemInfo(New Object() {"LabourRateP1RetailBarcode", "01-LAB-HRLY-WKSH-P1"})
                                    Case "01-LAB-HRLY-WKSH-P2"
                                        Call mSysInfo1.UpdateSystemInfo(New Object() {"LabourRateP2RetailBarcode", "01-LAB-HRLY-WKSH-P2"})
                                    Case "01-LAB-HRLY-WKSH-P3"
                                        Call mSysInfo1.UpdateSystemInfo(New Object() {"LabourRateP3RetailBarcode", "01-LAB-HRLY-WKSH-P3"})
                                    Case "01-LAB-HRLY-ONST-P1"
                                        Call mSysInfo1.UpdateSystemInfo(New Object() {"LabourRateOnSiteP1RetailBarcode", "01-LAB-HRLY-ONST-P1"})
                                    Case "01-LAB-HRLY-ONST-P2"
                                        Call mSysInfo1.UpdateSystemInfo(New Object() {"LabourRateOnSiteP2RetailBarcode", "01-LAB-HRLY-ONST-P2"})
                                    Case "01-LAB-HRLY-ONST-P3"
                                        Call mSysInfo1.UpdateSystemInfo(New Object() {"LabourRateOnSiteP3RetailBarcode", "01-LAB-HRLY-ONST-P3"})
                                End Select
                            Next rowStock
                            bLabourMsgNeeded = False
                            MsgBox("Info only-  We Updated " & dtLabourRates.Rows.Count & " Labour Rates.." & vbCrLf & _
                                      "For actual rates, check/update rates in the POS Stock Table.", MsgBoxStyle.Information)
                        End If  '-nothing-
                    Else '-error-
                        MessageBox.Show("Failed to get Stock Labour Rates.." & vbCrLf & _
                                   gsGetLastSqlErrorMessage() & vbCrLf,
                                    "Labour Rates setup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If  '-get-
                Else
                    '-- MYOB or Imported from MYOB.
                End If  'POS-

                If bLabourMsgNeeded Then
                    MsgBox("NB: Important Information. " & vbCrLf & vbCrLf & _
                              " All six Labour rates (workshop and ONSITE for the three priority levels) must now be defined " & _
                              " in the Retail Stock Table (RetailManager or JobMatixPOS) as Service Items." & vbCrLf & vbCrLf & _
                               "JobMatix JobTracking settings will store only the stock barcodes for the six price levels." & vbCrLf & vbCrLf & _
                               "The JobMatix Settings must be updated to save these barcodes. " & vbCrLf & _
                               "Until this is done, Job labour hours may not be correctly priced.  " & vbCrLf & vbCrLf & _
                              " To stop seeing this message: " & vbCrLf & _
                                  " 1. Make sure ""SERVCE"" is defined in both Category1 and Category2 tables; " & vbCrLf & _
                                  " 2. Set up all SIX labour rates as SERVCE items in the Retail Stock System; " & vbCrLf & _
                                  " 3. THEN set up the stock barcodes in the JobMatix Settings Screen (Labour Rates)." & vbCrLf & _
                                          " (both tabs, Workshop and ONSITE)..", MsgBoxStyle.Information)
                    MsgBox("NB: Until this is done- " & vbCrLf & "No new Job can be set up.", MsgBoxStyle.Information)
                End If
                '== END Target-build-4267  (Started 07-Sep-2020)
                '== END Target-build-4267  (Started 07-Sep-2020)


            End If '-exists-
            '= If Not gbGetPriorityInfo(mCnnSql, "1", False, mRetailHost1, mCurLabourHourlyRate, s1) Then
            '= End If  '-get-

            '=3311.405= 
            '- -- Start the STAFF REMINDER (SMS) operation in the background.
            '-- SHOULD run on SERVER, but server may not always be runing Jobmatix.
            '-- get sysInfo parameters. (User Biz Must define WakeUp Time)-
            If mSysInfo1.exists("OnSiteSmsWakeUpTime") AndAlso _
                     Trim(mSysInfo1.item("OnSiteSmsWakeUpTime")) <> "" Then '-- ok-
                '-ok -  Me.BackgroundWorkerStaffReminders.RunWorkerAsync()
            Else  '--no SMS wakeup time defined.
                MsgBox("Jobmatix starts sending reminder SMS's at WakeUp Time., " & vbCrLf & _
                       " And so 'OnSiteSmsWakeUpTime' (HHMM) (24-hour format) " & vbCrLf & _
                       " must be defined in SystemInfo." & vbCrLf & vbCrLf & _
                       "For now we will set it up at '0730' (7:30am.)..", MsgBoxStyle.Information)
                Call mSysInfo1.UpdateSystemInfo(New Object() {"OnSiteSmsWakeUpTime", "0730"})
                mSysInfo1.refreshAll()
            End If  '-exists.-

            '=3311.409=
            '-- OK.. Always start the BG task, so user can enable/disable at will.
            '== Me.BackgroundWorkerStaffReminders.RunWorkerAsync()
            '-- clsStaffReminders now has the BG worker thread..
            '=3311.731-  Pass StaffMobiles collection-
            If Not mRetailHost1.staffGetStaffList(mColFullStaffList) Then
                MsgBox("ERROR: Failed to get full stafflist.", MsgBoxStyle.Exclamation)
            Else '-ok-
                '-TESTING-
                s1 = ""
                For Each colStaffInfo As Collection In mColFullStaffList
                    s1 &= colStaffInfo.Item("docket_name")("value") & "; " & _
                                 colStaffInfo.Item("mobile")("value") & "; " & vbCrLf
                Next colStaffInfo
                '== MsgBox("Found staff list: (Docket-name; mobile;)" & vbCrLf & s1, MsgBoxStyle.Information)
            End If
            clsStaffReminders1 = New clsStaffReminders(msSqlConnect, msSqlDbName, mColFullStaffList, K_GOODS_ONSITEJOB, labReminderStatus)

            '=
            '-- Start the Exchange operation in the background.
            '-- In case some updates are still on file to go..
            Me.BackgroundWorkerExchange201.RunWorkerAsync(mCnnSql)
            mbExchange201WorkerIsActive = True   '-- flag as active..

            '=3401.411-  For F6 POS Hold..
            Me.KeyPreview = True


            '== Target-build-4284  (Started 23-Nov-2020)
            '== Target-build-4284  (Started 23-Nov-2020)
            '==    DEV- Bring JobReports into JobTracking Main as a UserControl..

            '--  ADD a new Main Tab page, and load JobReports userControl..
            Dim ucChildJobReports1 As ucChildJobReports42
            Dim sReportArgs As String
            Dim tabPageJobReports As TabPage = TabControlMain.TabPages("tabPageJobReports")

            sReportArgs = " /server=" & msServer & "  /DBName=" & msSqlDbName

            ucChildJobReports1 = New ucChildJobReports42
            ucChildJobReports1.InputCmdline = sReportArgs
            ucChildJobReports1.Sql_Connection = mCnnSql
            ucChildJobReports1.sqlDbname = msSqlDbName
            ucChildJobReports1.clsOurRetailHost = mRetailHost1

            ucChildJobReports1.Name = "ucChildJobReports1" '= strFormClassName & "_" & CStr(mIntChildCount)
            ucChildJobReports1.Text = ucChildJobReports1.Name
            ucChildJobReports1.Dock = DockStyle.Fill
            ucChildJobReports1.AutoSize = False

            ucChildJobReports1.Parent = tabPageJobReports
            tabPageJobReports.Controls.Add(ucChildJobReports1)

            '== END Target-build-4284  (Started 23-Nov-2020)
            '== END Target-build-4284  (Started 23-Nov-2020)


            '== MsgBox("Testing.. Startup is done..", MsgBoxStyle.Information)
            mbIsInitialising = False
            mbStartingUp = False
            DoEvents()

            '-- Wait for system to settle down..
            Dim startTimer As Single = Microsoft.VisualBasic.DateAndTime.Timer
            '= VB.Timer '- Microsoft.Visualbasic.dateandtime.timer '=
            While (VB.Timer < (startTimer + 3))  '-secs-
                DoEvents()
            End While
            mIntInitialSleepSeconds = 10  '-reset from initial 120..

            '=4219.1216-
            '-- Set up 5-min timer to watch for Xml Exchange Files from POS process.
            timerExchange2.Interval = 300000  '==300 secs..
            timerExchange2.Start()

            Call mbStaffSignOn() '--sign off & show signon FORM..--

            '- Now waiting for User Sign-on..
            '-- Timer will be started by SignOnCompletion....--
            '== Timer1.Enabled = True
            Exit Sub

        Catch ex As Exception    '--main outer try.-
            '==activate_error:
            '== L1 = Err().Number
            MsgBox("Runtime Error in JobMatix Main Activate sub.." & vbCrLf & "Error is " & ex.ToString)
            colFields = Nothing
            '==Me.Hide
            Me.Close() '=End
        End Try  '--main outer try.-
    End Sub '--SHOWN-   was Activate--
    '= = = = = = = = = == = = = = = = =
    '-===FF->

    '-- Mdi Main form resized..--
    '--  form resized..--

    '==   Target-New-Build-4284-EXTRA --  (23-Nov-2020)
    '==   New RAs DLL for RAs to come into the tent..
    '-- DELEGATE for Resizing Child..
    Public Delegate Sub SubFormResized(ByVal intParentWidth As Integer,
                                        ByVal intParentHeight As Integer)
    '-- This is instantiated below.
    Public delResized As SubFormResized '--    = AddressOf frmPosMainMdi.subChildReport
    '== END Target-New-Build-4284-EXTRA --  (23-Nov-2020)

    '--  form resized..--
    '--  form resized..--

    Private Sub frmJobMatix3_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        If Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized Then
            '--  cant make smaller than original..-

            If (Me.Height < mlFormDesignHeight) Then Me.Height = mlFormDesignHeight
            If (Me.Width < mlFormDesignWidth) Then Me.Width = mlFormDesignWidth

            TabControlMain.Width = (Me.Width - 13)
            TabControlMain.Height = (Me.Height - 127)   '--110-

            '== frameJobsTab.SetBounds(8, 65, (Me.Width - 23), (Me.Height - 118))
            frameJobsTab.Width = TabControlMain.Width - 13
            frameJobsTab.Height = TabControlMain.Height - 37

            '== labReminderStatus.Left = panelSignOn.Left - labReminderStatus.Width - 5
            labReminderStatus.Left = Me.Width - labReminderStatus.Width - 5

            '=3519.-122==  NEW TAB CONTROL for JobTracking..
            TabControlJobTracking.Height = frameJobsTab.Height - 26
            TabControlJobTracking.Width = frameJobsTab.Width - FrameJobDetails.Width - 14

            '-- move Details Frame Horiz ONLY...--
            FrameJobsTree.Height = TabControlJobTracking.Height - 34  '= (frameJobsTab.Height - 50) '== SSTabMain.Height - 840
            FrameJobsTree.Width = TabControlJobTracking.Width - 11
            '= (frameJobsTab.Width - FrameJobDetails.Width - 20)

            labEmptyJobPanel.Left = FrameJobsTree.Left + FrameJobsTree.Width + 100

            frameLegend.Top = FrameJobsTree.Height - 65
            frameLegend.Left = 0

            '--inner frame-
            FrameJobsTree2.Top = 0
            FrameJobsTree2.Left = 0 '-- 60
            FrameJobsTree2.Height = FrameJobsTree.Height - frameLegend.Height - 8
            '-- =3083==
            FrameJobsTree2.Width = FrameJobsTree.Width - 8

            '==3083.404=
            panelReminder.Left = 0
            panelReminder.Width = FrameJobsTree2.Width
            cmdDismissReminder.Left = FrameJobsTree2.Width - cmdDismissReminder.Width - 12

            If panelReminder.Visible Then
                tvwJobs.Top = panelReminder.Top + panelReminder.Height
                tvwJobs.Height = FrameJobsTree2.Height - labJobsExplorer.Height - 34 - panelReminder.Height
            Else
                tvwJobs.Top = 50  '==(labJobsExplorer.Top + labJobsExplorer.Height) '== FrameJobsTree2.Top
                tvwJobs.Height = FrameJobsTree2.Height - labJobsExplorer.Height - 34 '==   - 240
            End If
            tvwJobs.Left = FrameJobsTree2.Left + 1
            tvwJobs.Width = FrameJobsTree2.Width - 7 '==   - 240

            frameLegend.Width = FrameJobsTree2.Width

            '=3083= ON-SITE=
            '= frameOnSite.Top = FrameJobsTree.Top '-- frame overlays tree frame..--
            frameOnSite.Height = TabControlJobTracking.Height - 33  '= FrameJobsTree.Height
            frameOnSite.Width = TabControlJobTracking.Width - 11 '= FrameJobsTree.Width
            '== frameOnSiteHdr.Width = FrameJobsTree.Width
            dataGridViewOnSite.Height = frameOnSite.Height - 80
            dataGridViewOnSite.Width = frameOnSite.Width - 13  '==) - 240)

            FrameJobDetails.Height = (frameJobsTab.Height - FrameJobDetails.Top - 6) '== SSTabMain.Height - 480
            FrameJobDetails.Left = frameJobsTab.Width - FrameJobDetails.Width - 6
            '== FrameJobDetails.Width = (frameJobsTab.Width - FrameJobsTree.Width - 20)
            frameMainCmds.Left = FrameJobDetails.Left
            '== frameMainCmds.Left = FrameJobDetails.Left
            '== frameJobHdr.Left = FrameJobDetails.Left
            toolbarJobView.Left = FrameJobDetails.Left

            '== rtfJobDetails.Top = (picActionCmds.Top + picActionCmds.Height + 8)
            rtfJobDetails.Width = (FrameJobDetails.Width - 14)
            rtfJobDetails.Height = (FrameJobDetails.Height - rtfJobDetails.Top - 14)

            '- Jobs-
            '= FrameBrowse.Top = FrameJobsTree.Top '--search frame overlays tree frame..--
            FrameBrowse.Height = TabControlJobTracking.Height - 33 '= FrameJobsTree.Height '== SSTabMain.Height - 960
            FrameBrowse.Width = TabControlJobTracking.Width - 11  '= FrameJobsTree.Width

            DataGridViewJobs.Height = (FrameBrowse.Height - DataGridViewJobs.Top - 30)
            DataGridViewJobs.Width = FrameBrowse.Width - 11  '= VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(FrameBrowse.Width) - 240)

            labRecCount.Top = FrameBrowse.Height - 20 '= VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(FrameBrowse.Height) - 280)

            '--  new Customer frame..-
            '= frameCustomers.Top = FrameJobsTree.Top '--search frame overlays tree frame..--
            frameCustomers.Height = TabControlJobTracking.Height - 33 '= FrameJobsTree.Height '== SSTabMain.Height - 960
            frameCustomers.Width = TabControlJobTracking.Width - 11 '=FrameJobsTree.Width

            '--  divide customer frame between Flexgid and Jobs listView.--
            DataGridViewCust.Height = (((frameCustomers.Height - DataGridViewCust.Top) \ 3) * 2)
            DataGridViewCust.Width = frameCustomers.Width - 11 '= VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(frameCustomers.Width) - 240)

            '= labJobHistory.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(DataGridViewCust.Top) + _
            '=                                                                 VB6.PixelsToTwipsY(DataGridViewCust.Height) + 240)
            labJobHistory.Top = DataGridViewCust.Top + DataGridViewCust.Height + 13

            'listViewCustJobs.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(labJobHistory.Top) + _
            '                                                                    VB6.PixelsToTwipsY(labJobHistory.Height))
            listViewCustJobs.Top = labJobHistory.Top + labJobHistory.Height

            'listViewCustJobs.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(frameCustomers.Height) - _
            '                                                                  VB6.PixelsToTwipsY(listViewCustJobs.Top) - 120)
            listViewCustJobs.Height = frameCustomers.Height - listViewCustJobs.Top - 14
            listViewCustJobs.Width = DataGridViewCust.Width

            '--3037-- QUOTES panel..
            '--3037-- QUOTES panel..
            '==frameQuotes.Top = FrameJobsTree.Top '--search frame overlays tree frame..--
            frameQuotes.Height = TabPageQuoteJobs.Height - 16  '=FrameJobsTree.Height
            frameQuotes.Width = FrameJobsTree.Width
            ListViewSalesOrders.Height = frameQuotes.Height - ListViewSalesOrders.Top - 8
            ListViewSalesOrders.Width = frameQuotes.Width - 16
            '--detail-
            frameQuoteCustomer.Left = frameQuotes.Left + frameQuotes.Width + 8
            '= frameQuoteDetails.Top = FrameJobDetails.Top '==frameJobHdr.Top  '==3083=  FrameJobDetails.Top
            frameQuoteDetails.Left = frameQuotes.Left + frameQuotes.Width + 8  '= FrameJobDetails.Left
            '== frameQuoteDetails.Height = frameJobHdr.Height + FrameJobDetails.Height  '==3083==
            frameQuoteDetails.Top = frameQuoteCustomer.Top + frameQuoteCustomer.Height + 6
            frameQuoteDetails.Height = TabPageQuoteJobs.Height - frameQuoteCustomer.Height - 16   '==3103==
            ListViewQuote.Height = frameQuoteDetails.Height - ListViewQuote.Top - 16

            '-- POS controls--

            '=3411.1221= POS Tab GONE==
            '=3411.1221= POS Tab GONE==

    
            LabBusiness.Top = TabControlMain.Top + TabControlMain.Height + 3  '== Me.Height - 39
            labVersion.Top = LabBusiness.Top  '==Me.Height - 39
            LabBusinessId.Top = LabBusiness.Top '== Me.Height - 39

            '==   Target-New-Build-4284-EXTRA --  (23-Nov-2020)
            '==   Target-New-Build-4284-EXTRA --  (23-Nov-2020)
            '==
            '==   New RAs DLL for RAs to come into the tent..
            '==       INCLUDING. New Child USERCONTROL to bring RAs Main into Main Tab Control.
            '--   MUST Resize RAs tab..
            ''--mUcChildRAs1-

            '==  RA's  NOT COMING YET..
            '==  RA's  NOT COMING YET..
            '==  RA's  NOT COMING YET..

            'If (mUcChildRAs1 IsNot Nothing) Then
            '    delResized = New SubFormResized(AddressOf mUcChildRAs1.SubFormResized)
            '    Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
            'End If
            '== END  Target-New-Build-4284-EXTRA --  (23-Nov-2020)


            System.Windows.Forms.Application.DoEvents()
        End If '--minimized..-

        '= 331.309= 
        '-- Resizing RA's TabPage..
        '-- Resizing RA's TabPage..
        '= 3411.121=   all gone-

    End Sub '--resize..-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- TabControlMain_DrawItem--

    '= I found this Sub somewhere and changed it slightly. Add this sub to your program. 
    '= It colors the Tab (not the page) the same colors as the background

    '= of the tab, if it is the one selected. The other tabs are colored a standard white with black text. 
    '= You can modify it anyway you want, but it seems to work very well for coloring the tabs.
    '= https://social.msdn.microsoft.com/Forums/en-US/77f5b624-45af-492b-aef0-55e9f6176128/how-to-change-the-color-of-the-tab-control-in-vbnet?forum=vbide&forum=vbide


    Private Sub TabControlMain_DrawItem(ByVal sender As Object, _
                                        ByVal ev As System.Windows.Forms.DrawItemEventArgs) _
                                    Handles TabControlMain.DrawItem, TabControlJobTracking.DrawItem

        Dim TabControl1 As TabControl = CType(sender, TabControl)
        Dim g As Graphics = ev.Graphics
        Dim tp As TabPage = TabControl1.TabPages(ev.Index)
        Dim br As Brush
        Dim sf As New StringFormat
        Dim r As New RectangleF(ev.Bounds.X, ev.Bounds.Y + 2, ev.Bounds.Width, ev.Bounds.Height - 2)
        sf.Alignment = StringAlignment.Center
        Dim strTitle As String = tp.Text
        Dim fontX As Font

        '-  If the current index is the Selected Index, change the color
        If (TabControl1.SelectedIndex = ev.Index) Then  ''selected-
            fontX = New Font(TabControl1.Font, FontStyle.Bold)
            '- this is the background color of the tabpage
            '- you could make this a stndard color for the selected page
            '= br = New SolidBrush(tp.BackColor)
            '- Different Colour for Main TabControl and JobTracking Control..
            If (LCase(TabControl1.Name) = "tabcontrolmain") Then
                br = New SolidBrush(ColorTranslator.FromOle(RGB(255, 241, 168)))  '-light yellow.
            Else  '-jobTracking.
                br = New SolidBrush(ColorTranslator.FromOle(RGB(204, 230, 255)))  '-lightblue. 90%..
            End If  '-name-
            g.FillRectangle(br, ev.Bounds)
            br = New SolidBrush(tp.ForeColor)
            g.DrawString(strTitle, TabControl1.Font, br, r, sf)
            '= BOLD makes it too big..   g.DrawString(strTitle, fontX, br, r, sf)
        Else  '-not selected-
            fontX = New Font(TabControl1.Font, FontStyle.Regular)
            '= these are the standard colors for the unselected tab pages
            br = New SolidBrush(Color.WhiteSmoke)
            g.FillRectangle(br, ev.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, TabControl1.Font, br, r, sf)
            '== BOLD makes it too big.  g.DrawString(strTitle, fontX, br, r, sf)

        End If '-selected-
    End Sub  '-draw-item
    '= = = = = = = = = = == 
    '-===FF->


    '= Exchange201 ProgressChanged updating.-

    'Private Sub backgroundWorkerExchange201_ProgressChanged(ByVal sender As Object, _
    '                                          ByVal ev As ProgressChangedEventArgs) _
    '                                      Handles BackgroundWorkerExchange201.ProgressChanged
    '    Dim intResult As Integer = ev.ProgressPercentage
    '    If intResult = 100 Then  '-done-
    '        mbExchange201WorkerIsActive = False
    '    ElseIf intResult = 1 Then  '-started-
    '        labStatus.Text = "Exchange20 BG Thread started: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
    '    ElseIf intResult = 2 Then  '-started-
    '        labStatus.Text = "Exchange20 BG Thread working: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
    '    ElseIf intResult = 3 Then  '-started-
    '        labStatus.Text = "Exchange20 BG Thread exited: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
    '    ElseIf intResult = 100 Then  '-started-
    '        labStatus.Text = "Exchange20 BG Calendar updated: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
    '    End If
    'End Sub '== backgroundWorkerExchange201 Progress-\
    '= = = = = =

    '= Exchange201 ProgressChanged updating.-
    '-- Updated 3501.0708=  (From 3431.0707).

    Private Sub backgroundWorkerExchange201_ProgressChanged(ByVal sender As Object, _
                                              ByVal ev As ProgressChangedEventArgs) _
                                          Handles BackgroundWorkerExchange201.ProgressChanged
        Dim intResult As Integer = ev.ProgressPercentage
        If intResult = 100 Then  '-done-
            mbExchange201WorkerIsActive = False
            labStatus.Text &= "Exchange20 BG Calendar updated: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
        ElseIf intResult = 0 Then  '-started-
            labStatus.Text = "Exchange20 BG Thread pausing: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
        ElseIf intResult = 1 Then  '-started-
            labStatus.Text = "Exchange20 BG Thread working: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
        ElseIf intResult = 2 Then  '-working-
            labStatus.Text &= "Exchange20 BG updating calendar: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
        ElseIf intResult = 99 Then  '-exited-
            labStatus.Text = "Exchange20 BG Thread exited: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
        ElseIf intResult = 100 Then  '-started-
            labStatus.Text = "Exchange20 BG Calendar updated: " & Format(TimeOfDay, "hh:mm:ss tt") & ".." & vbCrLf
        End If
    End Sub '== backgroundWorkerExchange201 Progress-\
    '= = = = = =
    '-===FF->

    '-Exchange RunWorkerCompleted-
    '-Exchange RunWorkerCompleted-

    '= https://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker(v=vs.110).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-1

    Private Sub backgroundWorkerExchange201_RunWorkerCompleted(ByVal sender As Object, _
                                                          ByVal ev As RunWorkerCompletedEventArgs) _
                                                     Handles BackgroundWorkerExchange201.RunWorkerCompleted
        Dim sResultText, sFailedJobs, sMsg As String

        mlStaffTimeout = -1 '--STOP timing out.. --

        '-- when done-
        Dim colResults As New Collection
        '= colResults.Add(colFailedFiles, "FailedFiles")
        Dim colFailedFiles As New Collection
        sResultText = ""

        colResults = ev.Result
        '= colFailedFiles = colResults.Item("FailedFiles")

        mbExchange201WorkerIsActive = False

        ' First, handle the case where an exception was thrown.
        If (ev.Error IsNot Nothing) Then
            MessageBox.Show(ev.Error.Message)
            sResultText = ev.Error.Message
        ElseIf ev.Cancelled Then
            ' Next, handle the case where the user canceled the 
            ' operation.
            ' Note that due to a race condition in 
            ' the DoWork event handler, the Cancelled
            ' flag may not have been set, even though CancelAsync was called.
            sResultText = "Operation was Cancelled.."
        Else
            ' Finally, handle the case where the operation succeeded.
            If (colResults IsNot Nothing) Then
                sResultText = colResults.Item("results_msg")  '= Trim(ev.Result.ToString())
            End If
        End If

        labStatus.Text &= "Exchange20 BG Thread completed- " & Format(TimeOfDay, "hh:mm tt") & ".." & vbCrLf
        '--  Don't report if null result (No action happenend).
        If (sResultText <> "") Then

            '== Target-build-4267  (Started 07-Sep-2020)
            '== Target-build-4267  (Started 07-Sep-2020)

            'MessageBox.Show("Background Calendar Message:" & vbCrLf & sResultText, _
            '                   "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim dlgResult As DialogResult = myMessageBox(sResultText, "Background Calendar Message", "JobMatix")

            '== END Target-build-4267  (Started 07-Sep-2020)
            '== END Target-build-4267  (Started 07-Sep-2020)


        End If
        If ((colResults Is Nothing) OrElse (colResults.Count <= 0)) Or (sResultText = "") Then
            Exit Sub
        End If
        '-- Show error if any..
        If colResults.Contains("results_failed") Then
            sFailedJobs = colResults.Item("results_failed")
            If (MessageBox.Show("Note: Jobs Nos. " & sFailedJobs & " failed to update the calendar." & vbCrLf & _
                                    "-- Checking the Exchange user credentials may be useful.." & vbCrLf & _
                                    vbCrLf & "Do you want to retry ? " & vbCrLf & _
                                    "(Updater thread will pause for 2 mins.) " & vbCrLf & _
                                    "(ELSE- If No, these updates will be abandoned..)", _
                                    "Exchange Calendar Updating..", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question, _
                               MessageBoxDefaultButton.Button1) <> Windows.Forms.DialogResult.Yes) Then
                '- no retry-  so delete the queue..
                colFailedFiles = colResults.Item("FailedFiles")
                '= Dim sExchangeFullPath As String = Trim(gsJobMatixLocalDataDir())  '-defaults to Assembly name.
                '= sExchangeFullPath &= "\temp"
                For Each sExchangeFullPath As String In colFailedFiles
                    Try
                        File.Delete(sExchangeFullPath)
                        MessageBox.Show("The file: " & sExchangeFullPath & " was deleted..", _
                                        "Exchange Calendar Updating..", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        sMsg = "** ERROR- Failed to DELETE Failed Exchange20 Xml file.." & vbCrLf & _
                                 ex.Message & vbCrLf & _
                                 vbCrLf & "Another task may be accessing the file.."
                        MessageBox.Show(sMsg, "Exchange Calendar Updating..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Call gbLogMsg(gsRuntimeLogPath, sMsg & vbCrLf & "= = = =")
                    End Try
                Next
            Else  'yes-  retry..
                '--restart the background worker..
                mIntInitialSleepSeconds = 120
                '-- Start the Exchange operation again in the background.

                '== Target-build-4267  (Started 07-Sep-2020)
                '== Target-build-4267  (Started 07-Sep-2020)
                '==  NOT if it is already started..

                '= Me.BackgroundWorkerExchange201.RunWorkerAsync(mCnnSql)
                '= mbExchange201WorkerIsActive = True   '-- flag as active..

                If Not mbExchange201WorkerIsActive Then
                    Try
                        Me.BackgroundWorkerExchange201.RunWorkerAsync(mCnnSql)
                        mbExchange201WorkerIsActive = True   '-- flag as active..
                    Catch ex As Exception
                        MessageBox.Show("Exchange Calendar Couldn't restart BG Task.." & vbCrLf & _
                                        ex.Message & vbCrLf & "Will retry later.", _
                                        "Exchange Calendar Completion..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End Try
                End If
                '== END Target-build-4267  (Started 07-Sep-2020)
                '== END Target-build-4267  (Started 07-Sep-2020)


            End If
        End If  '--results_failed-.
        mlStaffTimeout = 0 '--NOW is timing out.. Allow full interval--

    End Sub 'backgroundWorker1_RunWorkerComplete
    '= = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-timerExchange2_Tick-
    '-  Ever 2 mins..  Check for xml files..
    '--  Restart Worker thread if needed..

    Private Sub timerExchange2_Tick(sender As Object, e As EventArgs) Handles timerExchange2.Tick
        Dim sMsg As String
        Dim directory1 As DirectoryInfo
        Dim aFiles() As FileInfo

        Dim sExchangeFullPath As String = Trim(gsJobMatixLocalDataDir(msJobmatixAppName))  '-defaults to Assembly name.
        '= Dim sXmlFileTitle As String = "Exchange20_Appt_JobNo_" & CStr(mlJobId) & ".xml"
        Dim sXmlFileXml As String = ""
        sExchangeFullPath &= "\temp"

        '-- nothing to do if bg worker is running..
        If mbExchange201WorkerIsActive Or Me.BackgroundWorkerExchange201.IsBusy Then
            Exit Sub
        End If

        '=3431.0523=
        '= If no directory, then nothing's happened yet..
        If Not My.Computer.FileSystem.DirectoryExists(sExchangeFullPath) Then  '-no work.. we can exit..-
            '= worker.ReportProgress(99)  '--say exited.
            Exit Sub  '-My.Computer.FileSystem.CreateDirectory(sExchangeFullPath)
        End If '-- exists dir.-

        Try
            directory1 = New DirectoryInfo(sExchangeFullPath)
        Catch ex As Exception
            sMsg = "** ERROR: Exchange20 Calendar BG Worker- " & vbCrLf & _
                    "  Failed to get ExchangeFullPath Directory Info.." & vbCrLf & ex.Message & vbCrLf
            '= colResults.Add(sMsg, "results_msg")
            '= ev.Result = colResults
            Call gbLogMsg(gsRuntimeLogPath, sMsg)
            '= worker.ReportProgress(99)  '--say exited.
            Exit Sub
        End Try
        '--get list of files.
        aFiles = directory1.GetFiles("Exchange20*.xml")
        '-- exit if nothing to do..
        If (aFiles.Length <= 0) Then
            Exit Sub
        Else  '-have files.. restart the worker..
            '-- Start the Exchange operation again in the background.
            Me.BackgroundWorkerExchange201.RunWorkerAsync(mCnnSql)
            mbExchange201WorkerIsActive = True   '-- flag as active..
        End If
    End Sub  '-timerExchange2_Tick-
    '= = = = = = = = =  == == =  =
    '-===FF->


    Private Sub cmdDismissReminder_Click(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles cmdDismissReminder.Click
        Call mbHideNotifyReminder()

    End Sub  '--dismiss--
    '= = = = = = = = = = = = 

    '--R e f r e s h L o g i n s--

    '== Private Sub cmdRefreshLogins_Click(ByVal sender As System.Object, _
    '==                                          ByVal e As System.EventArgs)
    '== Dim colWhichUsers As Collection
    '==     Call mbShowLoggedInUsers(colWhichUsers)
    '==     colWhichUsers = Nothing
    '== End Sub  '--cmdRefreshLogins-
    '= = = = = = = = = = = = = = =

    '= 3401.411=
    '-- FORM- Key Down..-
    '=  Reset timer on k/b Activity.

    '-- catch F6..-

    Private Sub frmJobMatix33_KeyDown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                               Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        '=3411.0302=
        '= NOW USING GetLastInputInfo(lastInputInfo1) User32-dll-
        '-- no longer needed..
        '=If (mlStaffTimeout >= 0) And (Not mnuStaySignedOn.Checked) Then  '-timeout active..
        '=    mlStaffTimeout = 0   '-- reset to stop timing out while user active..
        '= End If

        If (KeyCode = System.Windows.Forms.Keys.F6) And _
                        ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--Hold--
            '-- If Sale Tab is open, and Hold button if enabled..
            '- let it go if we're clicking on different page.
            '-- First- mTabControlMain
            'If (mTabControlMain IsNot Nothing) Then
            '    If LCase(mTabControlMain.SelectedTab.Name) <> "tabpagepos" Then
            '        Exit Sub
            '    End If
            'End If
            ''- we're ON POS page if we're in JobMatix..
            '=3411.1221= POS Tab GONE==
            '=3411.1221= POS Tab GONE==

            'If LCase(TabControlPOS.SelectedTab.Name) = "tabpagesale" Then
            '    '=Exit Sub
            '    If btnSaleHold.Visible AndAlso btnSaleHold.Enabled Then
            '        Call btnSaleHold_Click(btnSaleHold, New System.EventArgs)
            '        eventArgs.Handled = True
            '    End If
            'End If '-sale page-
        End If  '--F6-
    End Sub '--keypress..-
    '= = = = = = = = = = == =
    '-===FF->

    '-- Form KeyPress..-
    '==     labStaffTimeRemaining.Text = ""  '=3411.0302-
    '=  Reset timer on k/b Activity.

    Private Sub frmJobMatix33_KeyPress(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                     Handles MyBase.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        'If keyAscii = 27 Then '--ESC-
        '    mbLoginRequested = True
        '    keyAscii = 0
        '    labStatus.Text = ""
        '    '= Me.KeyPreview = False
        '    '--test-
        '    MsgBox("ESC handled..", MsgBoxStyle.Information)
        'End If

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = = = = == =

    '--form mouse activity---  select menu--
    '-- for admin only..--

    Private Sub frmJobMatix3_MouseUp(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.MouseEventArgs) _
                                          Handles MyBase.MouseUp
        Dim iButton As Short = eventArgs.Button \ &H100000
        Dim iShift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)

        '== If iButton = 2 Then '--right --
        If eventArgs.Button = System.Windows.Forms.MouseButtons.Right Then
            '===== If mbSuperAdmin Then Me.PopupMenu mnuCompute

        ElseIf eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then

            '=3411.0302=  Left-click can reset timer..
            '= NOW USING GetLastInputInfo(lastInputInfo1) User32-dll-
            '-- This no longer needed..
            'If (mlStaffTimeout >= 0) And (Not mnuStaySignedOn.Checked) Then  '-timeout active..
            '    mlStaffTimeout = 0   '-- reset to stop timing out while user active..
            'End If

        End If '--button--
    End Sub '--mouse--
    '= = = = = = = = =
    '-===FF->

    '- Staff Sign-On..

    Private Sub txtStaffBarcode_TextChanged(sender As Object, e As EventArgs) Handles txtStaffBarcode.TextChanged

    End Sub  '- Staff Sign-On..
    '= = = = = = = = = = = = = = =

    '-- staff ID entry box..--
    '-- staff ID entry box..--
    '-- staff ID entry box..--

    Private Sub txtStaffBarcode_Enter(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles txtStaffBarcode.Enter

        '==If mbIsInitialising Then Exit Sub

        '= txtStaffBarcode.Text = "Barcode"
        txtStaffBarcode.SelectionStart = 0
        txtStaffBarcode.SelectionLength = Len(txtStaffBarcode.Text)
        '==LabStaffName.Caption = ""
        '== LabStaffName2.Text = "-SignOn-"

    End Sub '--staff name got focus--
    '= = = =  = =  = =

    Private Sub txtStaffBarcode_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles txtStaffBarcode.Click
        txtStaffBarcode.SelectionStart = 0
        txtStaffBarcode.SelectionLength = Len(txtStaffBarcode.Text)

    End Sub  '-txtStaffBarcode_Click-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- trap ENTER key on staff Id..--
    '-- trap ENTER key on staff  !!  BARCODE  !!!.--
    '--  lookup staff name--

    Private Sub txtStaffBarcode_KeyPress(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                            Handles txtStaffBarcode.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1, sBarcode As String
        Dim sName As String
        Dim colFields As Collection

        sBarcode = Trim(txtStaffBarcode.Text)
        sName = ""
        If keyAscii = 13 Then '--enter-
            If (sBarcode <> "") Then '--lookup-
                '--lookup RM staff table--
                If mRetailHost1.staffGetStaffRecord(sBarcode, -1, colFields) Then '--found..
                    '=-- mbLookupStaff(s1, colFields) Then
                    sName = colFields.Item("docket_name")("value")
                    msStaffBarcode = colFields.Item("barcode")("value")
                    mlStaffId = CInt(colFields.Item("staff_id")("value"))
                    msStaffName = sName
                    txtStaffBarcode.ForeColor = System.Drawing.Color.Black
                    LabStaffName2.Text = sName
                    If (sName = "") Then
                        txtStaffBarcode.ForeColor = System.Drawing.Color.Red
                        MsgBox("Please Note: " & vbCrLf & _
                                  "This MYOB RM staff record " & vbCrLf & _
                                  "  is missing it's Docket Name (required).", MsgBoxStyle.Exclamation)
                    Else '--ok-
                        txtStaffBarcode.Enabled = False
                    End If '--name ok..-
                Else  '-not found-
                    txtStaffBarcode.ForeColor = System.Drawing.Color.Red
                    LabStaffName2.Text = "Not Found"
                End If '-- lookup--
                'ElseIf (sBarcode <> "") And (mlStaffId >= 0) Then  '--keep prev entry..--
                '    sName = msStaffName
                '    LabStaffName2.Text = sName
            Else
                '= No entry-
                sName = "" '--loop again..-
            End If '--lookup-
            keyAscii = 0 '--we processed the key..-
            'If (sName = "") Then
            '    txtStaffBarcode.ForeColor = System.Drawing.Color.Red
            '    '--KeepFocus = True '-txtRcvdName.SetFocus
            '    '== 3107.1213 = Report missing docket name.=
            '    MsgBox("Please Note: " & vbCrLf & _
            '              "This MYOB RM staff record " & vbCrLf & _
            '              "  is missing it's Docket Name (required).", MsgBoxStyle.Exclamation)
            'Else '--ok-
            '    '==  LabStaffName2.Text = sName
            '    txtStaffBarcode.Enabled = False
            'End If '--name ok..-
        End If '--13-

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
        If (sName <> "") Then '--entered ok..-
            mbStaffIsSignedOn = True
            Call mbStaffSignOnCompletion()
            '==Me.Close()
        Else
            txtStaffBarcode.Select()
        End If
    End Sub '-- ENTER staff namekeypress--
    '= = = = = = = = = = =  =  = =  

    '--clear text if clicked..--
    Private Sub txtStaffNameClick(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) _
                                             Handles txtStaffBarcode.Click

        '= txtStaffBarcode.Text = ""
    End Sub  '--click--
    '= = = = = = = = = = = =


    Private Sub cmdSignoff_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles cmdSignoff.Click
        Call mbStaffSignOn() '--sign off.--
        '== cmdSignoff.Enabled = False
    End Sub '--sign-off--
    '= = = = = = = = = = = = =

    Private Sub LabStaffName2_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles LabStaffName2.Click
        '==    Call mbStaffSignOn  '--sign off.--
    End Sub '--name2..-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '- picLogoPOS_Click-
    '-- LAUNCH Pos340ex-
    '-- LAUNCH Pos340ex-

    Private Sub picLogoPOS_Click(sender As Object, e As EventArgs)

        '= MsgBox("THis will launch POS..", MsgBoxStyle.Information)

        Dim intResult As Integer  '- Double
        Dim sPOS340Name, sPOS340Path, sPOS340Args As String

        sPOS340Name = "JmxPOS340ex"
        sPOS340Path = msAppPath & sPOS340Name & ".exe"

        '-- send  form pos also....
        sPOS340Args = " /server=" & msServer & _
         " /POS_dbname=" & msSqlDbName & " /StaffBarcode=" & msStaffBarcode & _
         " /formTop=" & CStr(Me.Top + 60) & " /formleft=" & CStr(Me.Left + 30)

        '-- check if process already running..
        Dim ap As Process() = Process.GetProcessesByName(sPOS340Name)
        If (ap IsNot Nothing) AndAlso (ap.Length > 0) Then
            '-- is alive.. so switch to it..
            Dim intId As IntPtr = ap(0).MainWindowHandle
            '-found-  Make it active-
            intResult = SetForegroundWindow(intId)
        Else '-not there.. so can launch..-
            Try
                Process.Start(sPOS340Path, sPOS340Args)
            Catch ex As Exception
                MsgBox("ERROR executing StartProcess cmd: " & vbCrLf & sPOS340Path & vbCrLf & vbCrLf & _
                       "Error: " & vbCrLf & ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
            End Try
        End If  '-nothing-
        Exit Sub

    End Sub  '- picLogoPOS_Click-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '==3411.0121=
    '-- Launch RA's  --

    '--btnLaunchRAs_Click -

    Private Sub btnLaunchRAs_Click(sender As Object, e As EventArgs) Handles btnLaunchRAs.Click

        '= MsgBox("THis will launch RAs..", MsgBoxStyle.Information)

        Dim intResult As Integer  '- Double
        Dim sRAs34Name, sRAs34Path, sRAs34Args As String

        '==   Target-New-Build-6201 --  (26-June-2021)
        '= sRAs34Name = "JobMatixRAs42"
        sRAs34Name = "JobMatixRAs62"
        '==  END Target-New-Build-6201 --  (26-June-2021)

        sRAs34Path = msAppPath & sRAs34Name & ".exe"
        '- try v4.2 first.
        If Dir(sRAs34Path) = "" Then
            sRAs34Name = "JobMatixRAs35"
            sRAs34Path = msAppPath & sRAs34Name & ".exe"
        End If

        '-- send  form pos also....
        sRAs34Args = " /server=" & msServer & _
         " /RAs_dbname=" & msSqlDbName & " /StaffBarcode=" & msStaffBarcode & _
         " /formTop=" & CStr(Me.Top + 60) & " /formleft=" & CStr(Me.Left + 30)

        '-- check if process already running..
        Dim ap As Process() = Process.GetProcessesByName(sRAs34Name)
        If (ap IsNot Nothing) AndAlso (ap.Length > 0) Then
            '-- is alive.. so switch to it..
            Dim intId As IntPtr = ap(0).MainWindowHandle
            '-found-  Make it active-
            intResult = SetForegroundWindow(intId)
        Else '-not there.. so can launch..-
            Try
                Process.Start(sRAs34Path, sRAs34Args)
            Catch ex As Exception
                MsgBox("ERROR executing StartProcess cmd: " & vbCrLf & sRAs34Path & vbCrLf & vbCrLf & _
                       "Error: " & vbCrLf & ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
            End Try
        End If  '-nothing-
    End Sub  '-btnLaunchRAs_Click-
    '= = = = = = = = = = = == = == 
    '-===FF->

    '-- POS Page selected-
    '=3411.1221= POS Tab GONE==
    '=3411.1221= POS Tab GONE==

    'Private Sub tabPagePos_gotFocus(ByVal sender As Object, _
    '                                  ByVal e As System.EventArgs)
    '    If Trim(txtSaleCustBarcode.Text = "") Then
    '        '== MsgBox("POS page selected..", MsgBoxStyle.Information)
    '        '= TabControlMain.Update()
    '        txtSaleCustBarcode.Focus()
    '    End If

    'End Sub '-gotFocus-
    '= = = = = = = = = = =  =
    '-===FF->

    '-- Tab Page Changed..-

    Private Sub TabControlMain_SelectedIndexChanged(ByVal sender As Object, _
                                                     ByVal ev As System.EventArgs) _
                                                    Handles TabControlMain.SelectedIndexChanged
        Dim index As Integer = TabControlMain.SelectedIndex
        Dim pageX As TabPage '==  = TabControlMain.TabPages(index) '= ev.TabPage
        Dim sOldStatus As String = ""
        Dim sNewHelp As String = ""

        If mbIsInitialising Then Exit Sub
        '= sOldHelp = mStrLasthelpText   '--get last-
        If index >= 0 Then
            sOldStatus = labStatus.Text
            Call mShowTopHelp("")
            pageX = TabControlMain.TabPages(index)
            If LCase(pageX.Name) = "tabpagepos" Then
                '=3411.1221= POS Tab GONE==
                '=3411.1221= POS Tab GONE==

                'If Trim(txtSaleCustBarcode.Text = "") Then
                '    '== MsgBox("POS page selected..", MsgBoxStyle.Information)
                '    '-restore last help this tab page-
                '    If (mStrLasthelpTextPOS = "") Then  '-restore last help if any-
                '        mStrLasthelpTextPOS = "JobMatix POS Sales and Returns."
                '    End If
                '    Call mShowTopHelp(mStrLasthelpTextPOS)
                '    TabControlMain.Update()
                '    txtSaleStaffBarcode.Select()   '=  txtSaleCustBarcode.Select()
                'End If
            ElseIf LCase(pageX.Name) = "tabpagejobtracking" Then
                '-- check if jobs tree or customers current.
                '-restore last help this tab page-
                If (mStrLasthelpTextJobs = "") Then  '-restore last help if any-
                    mStrLasthelpTextJobs = "JobMatix Jobs Tree-" & vbCrLf & "Shows all Jobs in Workflow view.."
                End If
                Call mShowTopHelp(mStrLasthelpTextJobs)

            ElseIf LCase(pageX.Name) = "tabpagequotejobs" Then
                '-restore last help this tab page-
                If (mStrLasthelpTextQuotes = "") Then  '-restore last help if any-
                    mStrLasthelpTextQuotes = "JobMatix Quotes-" & vbCrLf & "Shows all Retail Quotes for Job Creation.."
                End If
                Call mShowTopHelp(mStrLasthelpTextQuotes)
                If ListViewSalesOrders.Columns.Count <= 0 Then
                    Call mbRefreshQuotesList()
                End If
            End If '-tab page-
        End If  '-index-

        '=3311.817= Any activity resets to full timer-
        mlStaffTimeout = 0 '--NOW is timing out.. Allow full interval--

    End Sub '--TabControlMain_Selected-
    '= = = = = = = = = = == = = = = = = 
    '-===FF->

    '== Main timer Tick interrupt ==
    '== 3411/0302-     
    '--   Update labStaffTimeRemaining.Text = ""  '=3411.0302-

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles Timer1.Tick

        '== Dim dtime1, dTime2 As Date
        Dim ix, lDuration As Integer
        Dim tabIndex_Renamed As Short
        Dim s1 As String
        Dim sBarcode, sJobId As String
        Dim sName, sValue As String
        '== Dim form1 As Form
        Dim lRow, lCol As Integer
        Dim lngJobId, lngCustomerId, lngRAId As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection
        Dim colRecord As Collection
        Dim nodeX As System.Windows.Forms.TreeNode
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim orderId As Integer
        '=3411.0314= Checking Idle Time..
        Dim lastInputInfo1 As LASTINPUTINFO
        Dim intIdleTime As Integer  '-seconds-

        lastInputInfo1.cbSize = Len(lastInputInfo1)

        '--  Timer was disabled in FormClosing, but may not stop immediately..
        If (mCnnSql Is Nothing) Or mbFormClosing Then   '-adStateClosed-
            Exit Sub
        End If

        '=3327.119= 
        '-- Set RED forecolour for SMS BG msg if ERROR--
        If (InStr(UCase(labReminderStatus.Text), "ERROR") > 0) Then
            labReminderStatus.ForeColor = Color.Red
        End If
        '== If ((mlStaffTimeout Mod 3) = 0) Then labReminderStatus.Text = msReminderStatus
        '--signon can only be idle for 300 secs..--
        If (mlStaffTimeout < 0) Then
            '=3411.1221=  Or ((Not (mClsSale1 Is Nothing)) AndAlso mClsSale1.staffTimeoutSuspended) Then
            '--JobMatix or POS has SUSPENDED timing out..--
            Exit Sub  '=3411.0227=
        Else '--timer active..-
            Timer1.Stop()   '-- STOPS us being re-entered while we are hung up on msgbox..
            mlStaffTimeout = mlStaffTimeout + 1 '--count timing out period in SECONDS..--
            '=3411.0302=
            '- get idle time.
            Call GetLastInputInfo(lastInputInfo1)
            'With Label1
            '    .Caption = "system idle (secs): " & FormatNumber((GetTickCount() - lii.dwTime) / 1000, 2)
            '    .Refresh()
            'End With
            If (GetTickCount() >= lastInputInfo1.dwTime) Then
                intIdleTime = ((GetTickCount() - lastInputInfo1.dwTime) \ 1000)  '--msecs to secs..
            Else
                intIdleTime = 0
            End If

            '--  Update timer count down.
            If (Not mnuStaySignedOn.Checked) Then
                labStaffTimeRemaining.Text = CStr(mIntStaffTimeoutInterval - intIdleTime)
                labStaffTimeRemaining.Refresh()
                '= CStr(mIntStaffTimeoutInterval - mlStaffTimeout)
            End If

            '-- update reminder status-
            '--don't timeout if forms are open..-  OR StaySignedOn is checked..--
            If ((mCnnSql.State And 1) = 0) Or mbSqlConnectionFailure Then '--cnn is closed.-(adStateOpen=1)--
                '--  must re-connect if possible.-
                '=3107.611= Call mbStaffSignOn() '--sign off.--
                MsgBox("Sql Server connection has been lost.." & vbCrLf & _
                        " JobMatix will try re-connecting..", MsgBoxStyle.Exclamation)
                mbReConnectSqlServer()
            ElseIf (intIdleTime > mIntStaffTimeoutInterval) And (Not mnuStaySignedOn.Checked) Then
                '= (mlStaffTimeout > mIntStaffTimeoutInterval) And (Not mnuStaySignedOn.Checked) Then
                Call mbStaffSignOn() '--sign off.--
                '- DO NOT restart Timer here-
                Exit Sub
                '-ELSE- -not MAIN timeout..-
            ElseIf ((mlStaffTimeout Mod 60) = 0) Then  '--refresh time..-
                mlStaffTimeout = mlStaffTimeout + 1 '--make sure we don't repeat it immediately...--
                Call mbRefreshJobsTreeView()
                '=3107= labStatus.BackColor = Color.Transparent
                '=3107= labStatus.Text = ""
                '== If (ChkAutoRefreshRAs.Value = 1) Then Call mbRefreshRAsTreeView
            Else '--no timeouts..-
                '==tabIndex = SSTabMain.Tab
                '==If (tabIndex <= K_MAXJOBTABS) Then   '--showing jobs..-
                If frameJobsTab.Visible Then '--showing jobs/customers..-
                    tabIndex_Renamed = 0
                    '=== If MSHFlexGrid1(tabIndex).Enabled Then
                    If FrameBrowse.Visible Then '--showing browse..-
                        '--show current job..-
                        If (DataGridViewJobs.SelectedRows.Count > 0) Then
                            '--  use 1st selected row only.
                            lRow = DataGridViewJobs.SelectedRows(0).Cells(0).RowIndex
                            If (lRow >= 0) Then '--NOT in header row--
                                mLngSelectedRow = lRow
                                Call mBrowseJobs.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                                If Not (colKeys Is Nothing) Then
                                    '==MsgBox "Nothing selected.", vbExclamation
                                    If colKeys.Count() > 0 Then '--we have selection..-
                                        '-- Passing JOB-NO to get details....--
                                        lngJobId = CInt(colKeys.Item(1))
                                        If (lngJobId <> mlJobId) Then '--different panel or row..-
                                            Call mbShowJobInfoRTF(lngJobId)
                                        End If
                                    End If '--keys.-
                                End If '--nothing.-
                            Else '--no row..-
                                Call mbClearDetailsFrame() '--clear details..-
                            End If '--row 0--
                        End If '-sel count.-
                    ElseIf frameOnSite.Visible Then  '==3083==
                        If (dataGridViewOnSite.SelectedRows.Count > 0) Then
                            lRow = dataGridViewOnSite.SelectedRows(0).Cells(0).RowIndex
                            If (lRow < 0) Then '--in header row--
                                Call mbClearDetailsFrame() '--clear details..-
                            Else
                                '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
                                mLngSelectedRowOnSite = lRow
                                Call mBrowseOnSiteJobs.SelectRecord(mLngSelectedRowOnSite, colKeys, colRowValues)
                                If Not (colKeys Is Nothing) Then
                                    '==MsgBox "Nothing selected.", vbExclamation
                                    If colKeys.Count() > 0 Then '--we have selection..-
                                        '-- Passing JOB-NO to get details....--
                                        '--  bypass Date "hdr" rows..
                                        sJobId = colKeys.Item(1)
                                        If IsNumeric(sJobId) AndAlso (CInt(sJobId) > 0) Then
                                            lngJobId = CInt(colKeys.Item(1))
                                            If (lngJobId <> mlJobId) Then '--different panel or row..-
                                                Call mbShowJobInfoRTF(lngJobId)
                                            End If
                                        Else
                                            Call mbClearDetailsFrame() '-- "date HDR".. clear details..-
                                        End If
                                    End If '--keys.-
                                End If '--nothing.-
                            End If '--row--
                        End If
                    ElseIf frameCustomers.Visible Then  '--showing CUSTOMERS..-
                        '--show current CUSTOMER..-
                        If DataGridViewCust.SelectedRows.Count > 0 Then
                            '--  use 1st selected row only.
                            lRow = DataGridViewCust.SelectedRows(0).Cells(0).RowIndex
                            If (lRow >= 0) Then '--NOT in header row--
                                mLngSelectedRowCust = lRow
                                Call mBrowseCust.SelectRecord(mLngSelectedRowCust, colKeys, colRowValues)
                                If Not (colRowValues Is Nothing) Then
                                    '==MsgBox "Nothing selected.", vbExclamation
                                    If (colRowValues.Count() > 0) Then '--we have selection..-
                                        '-- Passing JOB-NO to get details....--
                                        lngCustomerId = CInt(colRowValues.Item("customer_id")("value"))

                                        '===  PROBLEM HERE with customer_id  for  QBPOS..-
                                        '==  QBPOS Browse columns must include "CustomerId" (varchar) twice..
                                        '==    THE SECOND time  "AS barcode"..  --
                                        sBarcode = colRowValues.Item("barcode")("value")
                                        '==  NOTE: if a (MYOB-RM) customer has different barcodes on different jobs,--
                                        '--    then this timer cane keep refreshing the same job display..-
                                        '== 3057.0  update first condition..  should fix..-
                                        If ((lngCustomerId > 0) AndAlso (lngCustomerId <> mlCustomerId)) Or _
                                                 ((lngCustomerId = -1) And (sBarcode <> msCustomerBarcode)) Then '--different panel or row..-
                                            '--  show customer..-
                                            Call mbClearDetailsFrame()
                                            labEmptyJobPanel.Text = ""
                                            '== If Not mRetailHost1.customerGetCustomerRecord(sBarcode, lngCustomerId, colRecord) Then
                                            '== '--not found..--
                                            '== MsgBox("Failed to retrieve customer record (Id " & lngCustomerId & ") " & vbCrLf & _
                                            '==             " for Barcode: '" & sBarcode & "'..", MsgBoxStyle.Exclamation)
                                            sName = CStr(colRowValues.Item(1)("name"))
                                            sValue = CStr(colRowValues.Item(1)("value"))
                                            If sBarcode = "" Then
                                                msCustomerBarcode = ""   '-- don't come back until diferent..
                                                labEmptyJobPanel.Text = "No ID or Barcode for customer: " & vbCrLf & _
                                                                                       " ( " & sName & "= " & sValue & ") .."
                                            Else  '--ok..-
                                                If Not mRetailHost1.customerGetCustomerRecordEx(colRowValues, colRecord) Then
                                                    labEmptyJobPanel.Text = "Failed to retrieve customer record: " & vbCrLf & _
                                                                                               " ( " & sName & "= " & sValue & ") .."
                                                Else '--ok--
                                                    '--set up customer details.-
                                                    If (listViewCustJobs.Items.Count > 0) Then '--have jobs in view..-
                                                        '-- load the ListviewCustJobs.--
                                                        '-- update jobs for this customer if needed..
                                                        item1 = listViewCustJobs.Items.Item(0) '--look at 1st item..-
                                                        '== If (CInt(item1.Tag) <> lngCustomerId) Then '--jobs need updating.-
                                                        If (item1.Tag <> sBarcode) Then '--jobs need updating.-
                                                            Call mbSetupCustomerInfo(colRecord, True) '--needs job reload..-
                                                        Else '-- jobs in l/v belong to this customer..-
                                                            Call mbSetupCustomerInfo(colRecord, False) '--no job reload..-
                                                        End If
                                                    Else '--no jobs in view..
                                                        Call mbSetupCustomerInfo(colRecord, True) '--so job reload..-
                                                    End If '--have jobs..-
                                                End If '--get customer..-
                                            End If  '--blaenk barcode
                                            '== Call mbShowJobInfoRTF(lngJobId)
                                        End If '-same cust..-
                                        item1 = listViewCustJobs.FocusedItem
                                        If Not (item1 Is Nothing) Then
                                            lngJobId = CInt(item1.Text) '--  job no is first column..-
                                            If (lngJobId <> mlJobId) Then '--different panel or row..-
                                                Call mbShowJobInfoRTF(lngJobId)
                                            End If
                                        End If
                                    End If '--keys.-
                                End If '--nothing.-
                            Else '--no row..-
                                Call mbClearDetailsFrame() '--clear details..-
                            End If '--row 0--
                        End If  '--sel count.-
                    ElseIf FrameJobsTree.Visible Then  '--showing Treeview..-
                        '--get current node..-
                        nodeX = tvwJobs.SelectedNode
                        If Not nodeX Is Nothing Then
                            s1 = LCase(nodeX.Name)
                            If VB.Left(s1, 3) = "job" Then
                                lngJobId = CInt(Mid(s1, 5)) '--bypass "job-" --
                                If (lngJobId <> mlJobId) Then '--different panel or row..-
                                    Call mbShowJobInfoRTF(lngJobId)
                                End If
                            End If '--key..-
                        End If '--node--
                    ElseIf frameQuotes.Visible Then  '--showing Quotes..-
                        '--  update quote info display if selection has moved..--
                        If ListViewSalesOrders.Enabled Then '--looking up list of quotes..-
                            '--update quote display..--
                            System.Windows.Forms.Application.DoEvents()
                            item1 = ListViewSalesOrders.FocusedItem
                            If (item1 Is Nothing) Then '--no selection..-
                                Exit Sub
                            Else
                                orderId = CInt(item1.Text) '--1st column has to be order_id..--
                                If orderId <> mlOrderId Then '-- has changed..-
                                    '==3067.0=  See SelectedIndexChanged event..
                                    '== Call mbLoadQuoteInfo()
                                End If
                            End If '--selected..-

                        End If '--enabled..-
                    End If '--browse/customers.-
                End If '--jobs/RAs.-
            End If '--timeout..-
            Timer1.Start()
        End If '--active-
        nodeX = Nothing
        item1 = Nothing
        colKeys = Nothing
        colRowValues = Nothing
        colRecord = Nothing
    End Sub '--timer--
    '= = = = =  = = =
    '-===FF->

    '--JOBS TreeView Node Click--
    '--JOBS TreeView Node Click--

    Private Sub tvwJobs_NodeClick(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.TreeNodeMouseClickEventArgs) _
                                        Handles tvwJobs.NodeMouseClick

        Dim nodeX As System.Windows.Forms.TreeNode = eventArgs.Node
        Dim sKey, sCnn As String
        Dim s1 As String
        '== Dim sStatus As String
        Dim lngJobId As Integer
        '== Dim bToNotify As Boolean
        sKey = LCase(nodeX.Name)
        '--MsgBox sKey + vbCrLf + "was clicked..", vbInformation
        If VB.Left(sKey, 3) = "job" Then
            '--lngJobId = CLng(Mid(sKey, 5)) '--bypass "job-" --
            s1 = Mid(sKey, 5)
            If IsNumeric(s1) Then
                lngJobId = CInt(s1)
                '==  3047.1== Call mbShowJobInfoRTF(lngJobId)
            End If '--numeric.-
        Else
            '==  3047.1== Call mbClearDetailsFrame() '--clear details..-
        End If
        '-- Rev-2918--
        '--  Don't restart timer if DBL-click (View) came first..
        '==  3047.1== If (mlStaffTimeout > 0) Then mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub '--node click..
    '= = = = = = = = = = = =

    '==  3047.1== Catch selection by Arrow keys..-

    Private Sub tvwJobs_AfterSelect(ByVal sender As System.Object, _
                                     ByVal EventArgs As System.Windows.Forms.TreeViewEventArgs) _
                                      Handles tvwJobs.AfterSelect

        Dim nodeX As System.Windows.Forms.TreeNode = EventArgs.Node
        Dim sKey, sCnn As String
        Dim s1 As String
        '== Dim sStatus As String
        Dim lngJobId As Integer

        '== MsgBox("Selected node: " & nodeX.Name, MsgBoxStyle.Information)
        sKey = LCase(nodeX.Name)
        '--MsgBox sKey + vbCrLf + "was clicked..", vbInformation
        If VB.Left(sKey, 3) = "job" Then
            '--lngJobId = CLng(Mid(sKey, 5)) '--bypass "job-" --
            s1 = Mid(sKey, 5)
            If IsNumeric(s1) Then
                lngJobId = CInt(s1)
                Call mbShowJobInfoRTF(lngJobId)

            End If '--numeric.-
        Else
            Call mbClearDetailsFrame() '--clear details..-
        End If
        '-- Rev-2918--
        '--  Don't restart timer if DBL-click (View) came first..
        If (mlStaffTimeout > 0) Then mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub  '--AfterSelect--
    '= = = = = = = = = = = = = 
    '-===FF->

    '-- View Job from tree dbl-click or Enter..-..

    Private Sub tvwJobs_DoubleClick(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles tvwJobs.DoubleClick
        Dim nodeX As System.Windows.Forms.TreeNode
        nodeX = tvwJobs.SelectedNode
        If Not (nodeX Is Nothing) Then
            Call cmdViewRecord_Click() '--view job record..
        End If
    End Sub '--keypress..-
    '= = = = = =  = = =

    Private Sub tvwJobs_KeyPress(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                        Handles tvwJobs.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim nodeX As System.Windows.Forms.TreeNode

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            nodeX = tvwJobs.SelectedNode
            If Not (nodeX Is Nothing) Then
                Call cmdViewRecord_Click() '--view job record..
            End If
            iKeyAscii = 0 '--done..-
        End If
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = =  = = =
    '-===FF->

    '--  Jobs.-- My Jobs ONLY..--

    'Private Sub chkMyJobs_CheckStateChanged(ByVal eventSender As System.Object, _
    '                                             ByVal eventArgs As System.EventArgs)

    '    Dim bOk As Boolean

    '    '--initialse and show..-
    '    If mbIsInitialising Or mbStartingUp Then Exit Sub
    '    '==3073= Stop from re-entering..--21Feb2013=
    '    chkMyJobs.Enabled = False   '-- 3073--
    '    bOk = mbRefreshJobsTreeView(True)
    '    If Not bOk Then '--failed or empty..
    '    Else '--ok-
    '    End If
    '    chkMyJobs.Enabled = True
    '    mlStaffTimeout = 0 '--restart timing out..  normal--
    '    tvwJobs.Focus()
    'End Sub '-my jobs.-
    '= = = = = = = = =

    '=3357.0219=
    '-- cboJobsFilter_SelectedIndexChanged-

    Private Sub cboJobsFilter_SelectedIndexChanged(sender As Object, e As EventArgs) _
                                                      Handles cboJobsFilter.SelectedIndexChanged
        If mbIsInitialising Or mbStartingUp Then Exit Sub
        '==3073= Stop from re-entering..--21Feb2013=
        cboJobsFilter.Enabled = False
        Call mbRefreshJobsTreeView(True) '--initialse and show..-
        cboJobsFilter.Enabled = True
        mlStaffTimeout = 0 '--restart timing out..  normal--
        tvwJobs.Select()   '= Focus()
    End Sub  '-cboJobsFilter_SelectedIndexChanged-
    '= = = = = = = =  = = = = = = = = = = == == 

    '--  Jobs Tree.-- Order by COMBO..-

    Private Sub cboJobsOrder_SelectedIndexChanged(ByVal sender As System.Object, _
                                                   ByVal e As System.EventArgs) _
                                                    Handles cboJobsOrder.SelectedIndexChanged
        If mbIsInitialising Or mbStartingUp Then Exit Sub
        '==3073= Stop from re-entering..--21Feb2013=
        cboJobsOrder.Enabled = False
        Call mbRefreshJobsTreeView(True) '--initialse and show..-
        cboJobsOrder.Enabled = True
        mlStaffTimeout = 0 '--restart timing out..  normal--
        tvwJobs.Select()  '=Focus()
    End Sub  '--cboJobsOrder--
    '= = = = = = = = = = = = = 

    '== 3083 ==
    Private Sub chkShowCompany1st_CheckedChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) Handles chkShowCompany1st.CheckedChanged

        If mbIsInitialising Or mbStartingUp Then Exit Sub
        '==3073= Stop from re-entering..--21Feb2013=
        chkShowCompany1st.Enabled = False
        Call mbRefreshJobsTreeView(True)
        chkShowCompany1st.Enabled = True
    End Sub  '--ShowCompany1st_CheckedChanged-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- R e f r e s h J o b s T r e e --
    '-- R e f r e s h J o b s T r e e --

    Private Sub cmdRefreshJobsTree_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) _
                                             Handles cmdRefreshJobsTree.Click
        If mbIsInitialising Then Exit Sub
        '==3073= Stop from re-entering..--21Feb2013=
        cmdRefreshJobsTree.Enabled = False
        Call mbRefreshJobsTreeView()  '--also initialise..
        '== If mbRefreshJobsTreeView(True) Then '--also initialise..

        cmdRefreshJobsTree.Enabled = True
        mlStaffTimeout = 0 '--restart timing out..  normal--
        tvwJobs.Focus()
        '== End If
    End Sub '--refresh..-
    '= = = = = = = = = =
    '-===FF->

    '--TabControlJobTracking_SelectedIndexChanged-
    '-- NEW !! 
    '--   Rotate between Jobs Tree and Search browser and CUSTOMERS and QUOTES..--

    Private Sub TabControlJobTracking_SelectedIndexChanged(ByVal sender As Object, _
                                                     ByVal ev As System.EventArgs) _
                                                 Handles TabControlJobTracking.SelectedIndexChanged
        Dim index As Integer = TabControlJobTracking.SelectedIndex
        Dim pageX As TabPage = TabControlJobTracking.TabPages(index) '= ev.TabPage
        Dim sOldStatus As String = ""
        Dim sNewHelp As String = ""

        If mbIsInitialising Then Exit Sub
        '= sOldHelp = mStrLasthelpText   '--get last-
        If (index >= 0) Then
            If LCase(pageX.Name) = "tabpagejobstree" Then

                Call mShowTopHelp("Jobs Tree shows all jobs collected in order of job workflow progress..")
            ElseIf LCase(pageX.Name) = "tabpageonsite" Then

                Call mbShowOnSiteJobsBrowse()
                Call mShowTopHelp("On-site Jobs Grid shows all ON-SITE jobs collected in order of date created...")
            ElseIf LCase(pageX.Name) = "tabpagejobsgrid" Then

                Call mShowTopHelp("Jobs Grid shows all jobs known to the system.." & vbCrLf & _
                                       "Click on job status button to see those jobs.")
                If mButtonCurrentBrowse Is Nothing Then    '--startup.. show all jobs..-
                    Call ToolbarJobs_ButtonClick(ToolbarJobs.Items("_ToolbarJobs_Button6"), New System.EventArgs()) '--refresh browse grid..-
                End If

            ElseIf LCase(pageX.Name) = "tabpagecustomers" Then
                If Not DataGridViewCust.Enabled Then
                    Call mbRefreshCustomerBrowse()
                End If
                Call mShowTopHelp("Customer Grid shows all customers known to your Retail POS system...")
            End If  '-name=
        End If  '-index-
    End Sub   'TabControlJobTracking_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = = = == =  =
    '-===FF->

    '-- Rotate between Jobs Tree and Search browser and CUSTOMERS and QUOTES..--

    '=3519.0122-  - THIS is now for backup only

    Private Sub toolbarJobView_ButtonClick(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) _
        Handles _toolbarJobView_ButtonBackup.Click

        '==ToolStripMenuItemFontSize_8.Click, ToolStripMenuItemFontSize_9.Click, ToolStripMenuItemFontSize_10.Click
        Dim Button As System.Windows.Forms.ToolStripItem
        '===        CType(eventSender, System.Windows.Forms.ToolStripItem)
        Dim sName As String
        Dim L1 As Integer

        If mbIsInitialising Then Exit Sub

        '=  -3519.0122-

        On Error GoTo toolbarJobView_button_error

        '== sName = CType(eventSender, Control).Name
        Button = CType(eventSender, System.Windows.Forms.ToolStripItem)

        If Button Is Nothing Then Exit Sub
        Call mbClearDetailsFrame()
        '--unclick all three buttons..-

        'CType(toolbarJobView.Items.Item("_toolbarJobView_Button1"), ToolStripButton).Checked = False
        'CType(toolbarJobView.Items.Item("_toolbarJobView_ButtonOnSite"), ToolStripButton).Checked = False
        'CType(toolbarJobView.Items.Item("_toolbarJobView_Button2"), ToolStripButton).Checked = False
        'CType(toolbarJobView.Items.Item("_toolbarJobView_Button3"), ToolStripButton).Checked = False
        '== CType(toolbarJobView.Items.Item("_toolbarJobView_ButtonQuotes"), ToolStripButton).Checked = False
        '==3311.405= CType(toolbarJobView.Items.Item("_toolbarJobView_ButtonRAs"), ToolStripButton).Checked = False

        '-- check this button.- 
        CType(Button, ToolStripButton).Checked = True

        labEmptyJobPanel.Text = "- No Job selected.."
        Call mShowTopHelp("")
        If LCase(Button.Tag) = "treeview" Then
            '=3101= frameQuotes.Visible = False
            'frameCustomers.Visible = False
            'FrameBrowse.Visible = False
            'FrameJobsTree.Visible = True
            ''=3101= frameQuoteDetails.Visible = False
            ''== FrameJobDetails.Visible = True
            'frameOnSite.Visible = False
            Call mShowTopHelp("Jobs Tree shows all jobs collected in order of job workflow progress..")
        ElseIf LCase(Button.Tag) = "onsitejobs" Then
            '=3101= frameQuotes.Visible = False
            'frameCustomers.Visible = False
            'FrameBrowse.Visible = False
            'FrameJobsTree.Visible = False
            ''=3101= frameQuoteDetails.Visible = False
            'frameOnSite.Visible = True
            '--  call onSite browse.. (cf Jobs browse.)--
            Call mbShowOnSiteJobsBrowse()
            Call mShowTopHelp("On-site Jobs Grid shows all ON-SITE jobs collected in order of date created...")

        ElseIf LCase(Button.Tag) = "jobsearch" Then
            '=frameOnSite.Visible = False
            '=3101= frameQuotes.Visible = False
            'FrameJobsTree.Visible = False
            'frameCustomers.Visible = False
            'FrameBrowse.Visible = True
            '=3101= frameQuoteDetails.Visible = False
            '== FrameJobDetails.Visible = True
            Call mShowTopHelp("Jobs Grid shows all jobs known to the system.." & vbCrLf & _
                                   "Click on job status button to see those jobs.")
            If mButtonCurrentBrowse Is Nothing Then    '--startup.. show all jobs..-
                Call ToolbarJobs_ButtonClick(ToolbarJobs.Items("_ToolbarJobs_Button6"), New System.EventArgs()) '--refresh browse grid..-
            End If
        ElseIf LCase(Button.Tag) = "customers" Then
            'frameOnSite.Visible = False
            ''=3101= frameQuotes.Visible = False
            'FrameJobsTree.Visible = False
            'FrameBrowse.Visible = False
            'frameCustomers.Visible = True
            '=3101= frameQuoteDetails.Visible = False
            '== FrameJobDetails.Visible = True
            '== If Not MSHFlexGridCust.Enabled Then
            If Not DataGridViewCust.Enabled Then
                Call mbRefreshCustomerBrowse()
            End If
            Call mShowTopHelp("Customer Grid shows all customers known to your Retail POS system...")

         ElseIf LCase(Button.Tag) = "backup" Then
            '== Call mnuBackupJobsDB_Click(mnuBackupJobsDB, New System.EventArgs())
            Call mbBackupJobsDB()
        End If '--key..-
        mStrLasthelpTextJobs = labStatus.Text
        '=3311.817= Any activity resets to full timer-
        mlStaffTimeout = 0 '--NOW is timing out.. Allow full interval--

        Exit Sub

toolbarJobView_button_error:
        L1 = Err().Number
        MsgBox("Runtime Error in ToolbarJobView button_click sub..  [" & sName & "]" & vbCrLf & _
                          "Error is " & L1 & " = " & ErrorToString(L1) & vbCrLf & vbCrLf & _
                             "Action may not have been carried out..", MsgBoxStyle.Exclamation)
    End Sub '--toolbarJobView--
    '= = = = = = = = = = = = = 
    '-===FF->

    '--  Toolbar JobView..--catch text menu.--
    '--  Toolbar JobView..--catch text menu.--

    Private Sub toolbarJobView_MenuClick(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) _
              Handles ToolStripMenuItemFontSize_8.Click, ToolStripMenuItemFontSize_9.Click, ToolStripMenuItemFontSize_10.Click
        Dim L1 As Integer
        Dim sName As String
        Dim menu1 As System.Windows.Forms.ToolStripMenuItem
        '=== CType(eventSender, System.Windows.Forms.ToolStripMenuItem)

        On Error GoTo toolbarJobView_Menu_error
        menu1 = CType(eventSender, System.Windows.Forms.ToolStripMenuItem)
        sName = menu1.Name
        If menu1 Is Nothing Then Exit Sub

        If IsNumeric(menu1.Tag) Then
            Call mbSetGridFontSize(CInt(menu1.Tag))
            '--  save new setting.-
            Call mbSaveSetting(K_GRIDFONTSIZE_SETTINGNAME, menu1.Tag)

        End If  '--numeric--
        Exit Sub
toolbarJobView_Menu_error:
        L1 = Err().Number
        MsgBox("Runtime Error in Toolbar Font_click sub..  [" & sName & "]" & vbCrLf & _
                          "Error is " & L1 & " = " & ErrorToString(L1) & vbCrLf & vbCrLf & _
                             "Font may not be changed..", MsgBoxStyle.Exclamation)
    End Sub '-text menu.-
    '= = = = = = = = = = = = = =
    '-===FF->

    '- O N S I T E Jobs  B r o w s e r --
    '- O N S I T E Jobs  B r o w s e r --
    '- O N S I T E Jobs  B r o w s e r --

    '--mouse activity---  select row to SHOW--
    '--mouse activity---  select row to SHOW--

    '== Private Sub MSHFlexgridJobs_ClickEvent(ByVal eventSender As System.Object, _
    '==                                          ByVal eventArgs As System.EventArgs)

    Private Sub DataGridViewOnSite_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                    Handles dataGridViewOnSite.CellMouseClick

        Dim lCol, lRow, lngJobId As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection
        Dim sJobId As String

        If (eventArgs.Button = MouseButtons.Left) Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If (lRow < 0) Then '--in header row--
                Call mbClearDetailsFrame() '--clear details..-
            Else
                '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
                mLngSelectedRowOnSite = lRow
                Call mBrowseOnSiteJobs.SelectRecord(mLngSelectedRowOnSite, colKeys, colRowValues)
                If Not (colKeys Is Nothing) Then
                    '==MsgBox "Nothing selected.", vbExclamation
                    If colKeys.Count() > 0 Then '--we have selection..-
                        '-- Passing JOB-NO to get details....--
                        '--  bypass Date "hdr" rows..
                        sJobId = colKeys.Item(1)
                        If IsNumeric(sJobId) AndAlso (CInt(sJobId) > 0) Then
                            lngJobId = CInt(colKeys.Item(1))
                            If (lngJobId <> mlJobId) Then '--different panel or row..-
                                Call mbShowJobInfoRTF(lngJobId)
                            End If
                        Else
                            Call mbClearDetailsFrame() '-- "date HDR".. clear details..-
                        End If
                    End If '--keys.-
                End If '--nothing.-
            End If '--row--
            mlStaffTimeout = 0 '--restart timing out..  normal--
        ElseIf (eventArgs.Button = MouseButtons.Right) Then '--left --
            '= MessageBox.Show("Right Clicked..", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If  '--button.-
    End Sub '--click--
    '= = = = = = = = = = = = = =
    '-===FF->

    '=3501.0611-
    '--  ON-SITE Double click..

    Private Sub DataGridViewOnSite_CellMouseDoubleClickEvent(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                    Handles dataGridViewOnSite.CellMouseDoubleClick
        Dim lCol, lRow, lngJobId As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection
        Dim sJobId As String

        If (eventArgs.Button = MouseButtons.Left) Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If (lRow < 0) Then '--in header row--
                Call mbClearDetailsFrame() '--clear details..-
            Else
                mLngSelectedRowOnSite = lRow
                Call cmdViewRecord_Click()
            End If  '-row-

        End If  '-left-

    End Sub  '-DataGridViewOnSite_CellMouseDoubleClick-
    '= = = = = = = = = = = = = =
    '-===FF->
    '- J o b s  B r o w s e r --
    '- J o b s  B r o w s e r --
    '- J o b s  B r o w s e r --
    '- J o b s  B r o w s e r --

    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dataGridViewJobs_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles DataGridViewJobs.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = DataGridViewJobs.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowseJobs.SortColumn(sName)
        '==  Me.DataGridView1.FirstDisplayedCell = Me.DataGridView1.CurrentCell
        mlStaffTimeout = 0 '--restart timing out..  normal--

    End Sub  '--sorted..-
    '= = = = = = = = =  = = =
    '-===FF->

    '--mouse activity---  select row to SHOW--
    '--mouse activity---  select row to SHOW--

    '== Private Sub MSHFlexgridJobs_ClickEvent(ByVal eventSender As System.Object, _
    '==                                          ByVal eventArgs As System.EventArgs)

    Private Sub DataGridViewJobs_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                    Handles DataGridViewJobs.CellMouseClick

        Dim lCol, lRow, lngJobId As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection

        '== lRow = MSHFlexGridJobs.MouseRow
        '== lCol = MSHFlexGridJobs.MouseCol
        If (eventArgs.Button = MouseButtons.Left) Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If (lRow < 0) Then '--in header row--
                Call mbClearDetailsFrame() '--clear details..-
            Else
                '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
                mLngSelectedRow = lRow
                Call mBrowseJobs.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                If Not (colKeys Is Nothing) Then
                    '==MsgBox "Nothing selected.", vbExclamation
                    If colKeys.Count() > 0 Then '--we have selection..-
                        '-- Passing JOB-NO to get details....--
                        lngJobId = CInt(colKeys.Item(1))
                        If (lngJobId <> mlJobId) Then '--different panel or row..-
                            Call mbShowJobInfoRTF(lngJobId)
                        End If
                    End If '--keys.-
                End If '--nothing.-
            End If '--row--
            mlStaffTimeout = 0 '--restart timing out..  normal--
        End If  '--button.-
    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '--mouse activity---  select row to edit--

    '== Private Sub MSHFlexgridJobs_dblClick(ByVal eventSender As System.Object, _
    '==                                          ByVal eventArgs As System.EventArgs)

    Private Sub DataGridViewJobs_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                    Handles DataGridViewJobs.CellMouseDoubleClick
        Dim lRow, lCol As Integer
        '= lRow = MSHFlexGridJobs.MouseRow
        '= lCol = MSHFlexGridJobs.MouseCol
        lRow = eventArgs.RowIndex

        If lRow >= 0 Then '--ok row--
            '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
            Call cmdViewRecord_Click() '--view job record..
        End If '--row--
        mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub '--dbl-click--
    '= = = = = = = = = =

    '--key activity---  select row to edit--
    '--  CATCH  << CTL-ENTER >>  ---

    '==FlexGrid==Private Sub MSHFlexgridJobs_KeyPressEvent(ByVal eventSender As System.Object, _
    '==FlexGrid==                        ByVal eventArgs As AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent)

    Private Sub DataGridViewJobs_KeyUp(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As KeyEventArgs) Handles DataGridViewJobs.KeyUp

        Dim lRow, lCol As Integer

        '== lRow = MSHFlexGridJobs.Row
        '== lCol = MSHFlexGridJobs.Col

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        '== If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then
        If eventArgs.Control AndAlso (eventArgs.KeyCode = Keys.Enter) Then
            If lRow >= 0 Then '--ok row--
                '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                '==========  mLngSelectedRow = lRow
                Call cmdViewRecord_Click() '--view job record..
            End If '--row--
            eventArgs.Handled = True '--processed--
        End If '--enter--

    End Sub '--keypress--
    '= = = = = = = = = = =
    '-===FF->

    '-- JOBS Browser.. txt FIND Activity.--
    '-- JOBS Browser.. txt FIND Activity.--
    '--BROWSING JOBS.. --

    '--JOBS key activity---  select row to edit--

    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                              ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer

        '= lRow = MSHFlexGridJobs.Row
        '== lCol = MSHFlexGridJobs.Col
        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If DataGridViewJobs.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = DataGridViewJobs.SelectedRows(0).Cells(0).RowIndex
                If lRow >= 0 Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow

                    '=== ??  =

                End If '--row--
                iKeyAscii = 0 '--processed--
            End If  '--count-
        End If '--enter--

        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--click--
    '= = = = = = = = = = =
    '-===FF->

    '-- JOBS-  Highlight FIND entry.--
    '--  JOBS- Highlight FIND entry.--

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

    '--  J O B S    BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        Call mBrowseJobs.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '== CUSTOMERS --
    '--  C U S T O M E R --  BROWSING..
    '--  C U S T O M E R --  BROWSING..
    '--  C U S T O M E R --  BROWSING..


    '--BROWSING CUSTOMERS.. --

    '--  F l e x G r i d  E v e n t s..--
    '--  F l e x G r i d  E v e n t s..--

    '-- CUSTOMER Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '-- set new sort column--


    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dataGridViewCust_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles DataGridViewCust.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = DataGridViewCust.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText
        Call mBrowseCust.SortColumn(sName)
        '==  Me.DataGridView1.FirstDisplayedCell = Me.DataGridView1.CurrentCell
        mlStaffTimeout = 0 '3411.0305= --restart timing out..  normal--
    End Sub
    '= = = = = = = = =  = = =
    '-===FF->

    '--mouse activity---  select row to SHOW--
    '--mouse activity---  select row to SHOW--

    '== Private Sub MSHFlexgridCust_ClickEvent(ByVal eventSender As System.Object, _
    '==                                            ByVal eventArgs As System.EventArgs)

    Private Sub DataGridViewCust_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                     Handles DataGridViewCust.CellMouseClick

        Dim lngCustId, lRow, lCol, lngError As Integer
        Dim colRowValues As Collection
        Dim sBarcode As String
        Dim colKeys As Collection
        Dim colRecord As Collection
        Dim sName, sValue As String

        On Error GoTo datagridCust_Click_Error
        '== lRow = MSHFlexGridCust.MouseRow
        '== lCol = MSHFlexGridCust.MouseCol
        If (eventArgs.Button = MouseButtons.Left) Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If (lRow >= 0) Then '--NOT in header row--
                '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
                mLngSelectedRowCust = lRow
                Call mBrowseCust.SelectRecord(mLngSelectedRowCust, colKeys, colRowValues)
                If Not (colRowValues Is Nothing) Then
                    If (colRowValues.Count > 0) Then
                        sName = CStr(colRowValues.Item(1)("name"))
                        sValue = CStr(colRowValues.Item(1)("value"))
                        If Not mRetailHost1.customerGetCustomerRecordEx(colRowValues, colRecord) Then
                            MsgBox("Failed to retrieve customer record ( " & _
                                                       sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
                        Else '--ok--
                            '--set up customer details.-
                            '-- Get all jobs for this customer, and load the ListviewCustJobs.--
                            Call mbSetupCustomerInfo(colRecord, True)
                        End If '--get customer..-
                    End If  '--count-
                End If '--nothing.-
            End If '--row--
            mlStaffTimeout = 0 '3411.0305= --restart timing out..  normal--
        End If  '--button.-
        Exit Sub

datagridCust_Click_Error:
        lngError = Err.Number()
        MsgBox("Runtime Error in JobMatix datagridCust_Click (" & lRow & "/" & lCol & ") sub.." & vbCrLf & _
                "Error is " & lngError & " = " & ErrorToString(lngError))

    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '--mouse activity---  select row to edit--
    '== Private Sub MSHFlexgridCust_dblClick(ByVal eventSender As System.Object, _
    '==                                         ByVal eventArgs As System.EventArgs)

    Private Sub DataGridViewCust_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                            Handles DataGridViewCust.CellMouseDoubleClick
        Dim lCol, lRow, lngId As Integer
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sBarcode As String

        '== lRow = MSHFlexGridCust.MouseRow
        '== lCol = MSHFlexGridCust.MouseCol
        lRow = eventArgs.RowIndex
        If lRow >= 0 Then '--ok row--
            '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
            mLngSelectedRowCust = lRow
            Call mBrowseCust.SelectRecord(mLngSelectedRowCust, colKeys, colSelectedRow)

            If colSelectedRow.Count() > 0 Then
                sBarcode = colSelectedRow.Item("barcode")("value")
                lngId = CInt(colSelectedRow.Item("customer_id")("value"))
                '==If Not mbLookupCustomerId(lngId, colRecord) Then
                If Not mRetailHost1.customerGetCustomerRecord(sBarcode, lngId, colRecord) Then '--not found..--
                    MsgBox("Failed to retrieve customer record (Id " & lngId & ") " & vbCrLf & " for Barcode: '" & sBarcode & "'..", MsgBoxStyle.Exclamation)
                Else '--ok--
                    '--set up customer details.-

                    '== =    ????  ==  Call mbSetupCustomer(colRecord)

                    '-- Get all jobs for this customer, and load the ListviewCustJobs.--

                    '==FrameBrowse.Visible = False
                End If
            Else
                If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
            End If '--got row..--
        End If '--row--
    End Sub '--dbl-click--
    '= = = = = = = = = =
    '-===FF->

    '--key activity---  select row to edit--
    '--  CATCH  << CTL-ENTER >>  ---

    '== Private Sub MSHFlexgridCust_KeyPressEvent(ByVal eventSender As System.Object, _
    '==                         ByVal eventArgs As AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent)

    Private Sub DataGridViewCust_KeyUp(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As KeyEventArgs) Handles DataGridViewCust.KeyUp

        Dim lCol, lRow, lngId As Integer
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sName, sValue As String

        '== lRow = MSHFlexGridCust.Row
        '== lCol = MSHFlexGridCust.Col
        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        '== If eventArgs.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
        If eventArgs.Control AndAlso (eventArgs.KeyCode = Keys.Enter) Then
            If DataGridViewCust.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = DataGridViewCust.SelectedRows(0).Cells(0).RowIndex
                If lRow >= 0 Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRowCust = lRow
                    Call mBrowseCust.SelectRecord(mLngSelectedRowCust, colKeys, colSelectedRow)
                    If colSelectedRow.Count() > 0 Then
                        sName = CStr(colSelectedRow.Item(1)("name"))
                        sValue = CStr(colSelectedRow.Item(1)("value"))
                        If Not mRetailHost1.customerGetCustomerRecordEx(colSelectedRow, colRecord) Then
                            MsgBox("Failed to retrieve customer record ( " & sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
                        Else '--ok--
                            '--set up customer details.-
                            '-- Get all jobs for this customer, and load the ListviewCustJobs.--
                            Call mbSetupCustomerInfo(colRecord, True)
                        End If '--get customer..-
                    Else
                        If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                    End If '--got row..--
                End If '--row--
                eventArgs.Handled = True '--processed--
            End If  '--count-
            '== ElseIf eventArgs.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Escape) Then
        End If '--enter--
    End Sub '--click--
    '= = = = = = = = = = =
    '-===FF->

    '-- CUSTOMER Browser.. txt FIND Activity.--
    '-- CUSTOMER Browser.. txt FIND Activity.--
    '--BROWSING CUSTOMERS.. --

    '--CUST key activity---  select row to edit--
    Private Sub txtFindCust_KeyPress(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                               Handles txtFindCust.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sName, sValue As String

        '== lRow = MSHFlexGridCust.Row
        '== lCol = MSHFlexGridCust.Col
        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            lRow = DataGridViewCust.CurrentRow.Cells(0).RowIndex
            If lRow >= 0 Then '--ok row--
                '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                mLngSelectedRowCust = lRow
                '== Call MSHFlexgridCust_KeyPressEvent(MSHFlexGridCust, _
                '==                              New AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent(iKeyAscii))
                Call mBrowseCust.SelectRecord(mLngSelectedRowCust, colKeys, colSelectedRow)
                If colSelectedRow.Count() > 0 Then
                    sName = CStr(colSelectedRow.Item(1)("name"))
                    sValue = CStr(colSelectedRow.Item(1)("value"))
                    If Not mRetailHost1.customerGetCustomerRecordEx(colSelectedRow, colRecord) Then
                        MsgBox("Failed to retrieve customer record ( " & sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
                    Else '--ok--
                        '--set up customer details.-
                        '-- Get all jobs for this customer, and load the ListviewCustJobs.--
                        Call mbSetupCustomerInfo(colRecord, True)
                    End If '--get customer..-
                Else
                    If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                End If '--got row..--

            End If '--row--
            iKeyAscii = 0 '--processed--
        End If '--enter--
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--click--
    '= = = = = = = = = = =
    '-===FF->

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFindCust_Enter(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles txtFindCust.Enter
        labFindCust.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        labFindCust.Font = VB6.FontChangeBold(labFindCust.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFindCust_Leave(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles txtFindCust.Leave
        labFindCust.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        labFindCust.Font = VB6.FontChangeBold(labFindCust.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFindCust.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub txtFindCust_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles txtFindCust.TextChanged

        Call mBrowseCust.Find(txtFindCust)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =

    '=3411.0217=
    '-btnCustNewCustomer-

    Private Sub btnCustNewCustomer_Click(sender As Object, e As EventArgs) Handles btnCustNewCustomer.Click
        '-- make new customer..
        Dim sSql, s1 As String
        Dim colResult, colRecord As Collection
        Dim frmCust1 As New frmCustomer
        frmCust1.StaffId = mlStaffId '= mIntMainStaff_id
        frmCust1.StaffName = msStaffName   '= msMainStaffName
        frmCust1.SqlServer = msServer
        frmCust1.connectionSql = mCnnSql '--job tracking sql connenction..-
        frmCust1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..

        frmCust1.DBname = msSqlDbName
        frmCust1.VersionPOS = "JobTracking"  '- msDLLVersion
        frmCust1.AddNewCustomerOnly = True
        '= frmCust1.form_left = mFrmSale.Left '=  Me.Left
        '= frmCust1.form_top = mFrmSale.Top = 30 '=  Me.Top + 30
        frmCust1.ShowDialog()
        If Not frmCust1.wasCancelled Then
            s1 = frmCust1.selectedBarcode
            If (s1 <> "") Then
                '-- lookup for details..
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [customer] WHERE (barcode='" & s1 & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    '= Call mbSetupSaleCustomer(colRecord)
                    MsgBox("JobTracking- Added new Customer: " & s1 & ".." & vbCrLf & _
                                colRecord.Item("firstName")("value") & " " & _
                                   colRecord.Item("lastName")("value"), MsgBoxStyle.Information)
                    Call mbRefreshCustomerBrowse()
                Else '--not found..-
                    MsgBox("No Customer found for barcode: " & s1, MsgBoxStyle.Exclamation)
                End If  '-get--
            Else
                MsgBox("No Selection", MsgBoxStyle.Exclamation)
            End If
        End If  '-cancelled-

    End Sub  '-btnCustNewCustomer-
    '= = = = = = = = = = == = = ==  
    '-===FF->

    '--  Cust/Jobs listview--
    '--  Show selected job detail..--

    'UPGRADE_ISSUE: MSComctlLib.ListView event listViewCustJobs.ItemClick was not upgraded. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'

    'Private Sub listViewCustJobs_ItemClick(ByVal item1 As System.Windows.Forms.ListViewItem)
    '    Dim lngJobId As Integer

    '    If Not (item1 Is Nothing) Then
    '        lngJobId = CInt(item1.Text) '--  job no is first column..-
    '        Call mbShowJobInfoRTF(lngJobId)
    '    End If '--nothing..--
    'End Sub '--item click..--
    '= = = = = = = = =  == ==

    '--listViewJobs_Click--

    'Private Sub listViewCustJobs_Click(ByVal eventSender As System.Object, _
    '                                   ByVal eventArgs As System.EventArgs) Handles listViewCustJobs.Click
    '    Dim item1 As System.Windows.Forms.ListViewItem
    '    Dim lngJobId As Integer
    '    '--  update quote info display if selection has moved..--
    '    item1 = listViewCustJobs.FocusedItem
    '    If (item1 Is Nothing) Then '--no selection..-
    '        Exit Sub
    '    Else
    '        lngJobId = CInt(item1.Text) '--1st column has to be job_id..--
    '        Call mbShowJobInfoRTF(lngJobId)
    '    End If '--selected..-
    'End Sub '--listViewJobs_Click--
    '= = = = = = = =  =

    '3311.710= - THIS instead of "click", which stuffs up the ItemActivate event..

    '==   ORIGINAL from MSDN-  Uses the SelectedItems property to retrieve and tally the price 
    '- of the selected menu items.
    Private Sub listViewCustJobs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                           Handles listViewCustJobs.SelectedIndexChanged
        'Dim breakfast As ListView.SelectedListViewItemCollection = _
        '    Me.ListView1.SelectedItems
        'Dim item As ListViewItem
        'Dim price As Double = 0.0
        'For Each item In breakfast
        '    price += Double.Parse(item.SubItems(1).Text)
        'Next
        '' Output the price to TextBox1.
        'TextBox1.Text = CType(price, String)
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngJobId As Integer
        '--  update quote info display if selection has moved..--
        item1 = listViewCustJobs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            lngJobId = CInt(item1.Text) '--1st column has to be job_id..--
            Call mbShowJobInfoRTF(lngJobId)
        End If '--selected..-
    End Sub  '-listViewCustJobs_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = == = = = = = = = = 

    '-- 3311.710=  DOESN'T work..  see ItemActivate below..
    Private Sub listViewCustJobs_dblClick(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles listViewCustJobs.DoubleClick
        Dim item1 As System.Windows.Forms.ListViewItem
        item1 = listViewCustJobs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            Call cmdViewRecord_Click()
        End If
    End Sub  '-listViewCustJobs_dblClick-

    '-- Cust-Jobs- ItemActivate..

    Private Sub listViewCustJobs_ItemActivate(sender As Object, e As EventArgs) _
                                                Handles listViewCustJobs.ItemActivate
        '== MessageBox.Show("You are in the ListView.ItemActivate event.")
        Dim item1 As System.Windows.Forms.ListViewItem
        item1 = listViewCustJobs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            Call cmdViewRecord_Click()
        End If
    End Sub  '-ItemActivate-
    '= = = = = = = = = = = = = = = =

    '--  END OF  -  C U S T O M E R --  BROWSING..
    '--  END OF  -  C U S T O M E R --  BROWSING..
    '--  END OF  -  C U S T O M E R --  BROWSING..
    '--  END OF  -  C U S T O M E R --  BROWSING..
    '= = = = = = = = = = = =
    '-===FF->

    '--  Q U O T E S  E v e n t s ---
    '--  Q U O T E S  E v e n t s ---
    '--  Q U O T E S  E v e n t s ---

    Private Sub cmdRefreshQuotes_Click(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles cmdRefreshQuotes.Click
        Call mbRefreshQuotesList()

    End Sub  '--cmdRefreshQuotes_Click--
    '= = = = = = = = = = = = = = = = = = 

    '-- New Quotes only..-- 

    Private Sub chkNewQuotes_CheckedChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles chkNewQuotes.CheckedChanged
        '== Call mbRefreshQuotesList()
    End Sub  '--chkNewQuotes_CheckedChanged-
    '= = = = = = = = = = = = = = = = = = = =

    '-- RECENT Quotes only..--

    Private Sub chkRecentQuotes_CheckedChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) Handles chkRecentQuotes.CheckedChanged
        '== Call mbRefreshQuotesList()
    End Sub '--chkRecentQuotes--
    '= = = = = = = = = = = = = 
    '-===FF->

    '--re-sort on selected column..-
    '--re-sort on selected column..-

    Private Sub listViewSalesOrders_ColumnClick(ByVal eventSender As System.Object, _
                                                   ByVal eventArgs As System.Windows.Forms.ColumnClickEventArgs) _
                                                     Handles ListViewSalesOrders.ColumnClick
        Dim lngNewKey As Integer
        Dim colHdr1 As System.Windows.Forms.ColumnHeader = ListViewSalesOrders.Columns(eventArgs.Column)

        lngNewKey = eventArgs.Column   '== colHdr1.Index - 1 '-- get zero index of column clicked..-
        '--MsgBox "Clicked col-no: " & lngNewKey
        With ListViewSalesOrders
            ListViewSalesOrders.Sort()
            If (lngNewKey = mlSortKey) Then '--same col clicked again..-
                If mlSortOrder = System.Windows.Forms.SortOrder.Ascending Then
                    mlSortOrder = System.Windows.Forms.SortOrder.Descending '--invert order..-
                    colHdr1.ImageKey = "ArrowDown"
                Else
                    mlSortOrder = System.Windows.Forms.SortOrder.Ascending
                    colHdr1.ImageKey = "ArrowUp"
                End If
                '== .Sort()
                .Sorting = mlSortOrder
            Else '--changed col.--
                '--  clear arrow from old column.-
                If (mlSortKey >= 0) Then  '--was sorted.-
                    ListViewSalesOrders.Columns(mlSortKey).ImageKey = ""
                End If
                .Sorting = System.Windows.Forms.SortOrder.Ascending  '--start asc..-
                mlSortOrder = System.Windows.Forms.SortOrder.Ascending  '--remember.-
                colHdr1.ImageKey = "ArrowUp"
            End If
        End With
        mlSortKey = lngNewKey '--remember current column.-
        ListViewSalesOrders.ListViewItemSorter = New ListViewItemComparerQ3037(eventArgs.Column, ListViewSalesOrders.Sorting)
    End Sub '--colClick..-
    '= = = = = = = = =  =

    '--Quote list Click--
    '---If any item is  selected, then show Quote details..--

    Private Sub listViewSalesOrders_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles ListViewSalesOrders.Click

        '== Call mbLoadQuoteInfo()
        '== cmdBuildQuote.Enabled = True

    End Sub '--Click--
    '= = = = = = = =  = = =

    '--dblClick--

    Private Sub listViewSalesOrders_DoubleClick(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) Handles ListViewSalesOrders.DoubleClick
        '==   Call cmdQuoteOk_Click(cmdQuoteOK, New System.EventArgs())
    End Sub '--dblClick--
    '= = = = = = = = =  =

    '---If any item is  selected, then show Quote details..--
    Private Sub ListViewSalesOrders_SelectedIndexChanged _
                           (ByVal sender As Object, ByVal e As System.EventArgs) _
                                    Handles ListViewSalesOrders.SelectedIndexChanged

        Dim listItems As ListView.SelectedListViewItemCollection = Me.ListViewSalesOrders.SelectedItems

        '== MsgBox("Index changed event..", MsgBoxStyle.Information)
        If (listItems.Count > 0) Then  '--have selection..-
            Call mbLoadQuoteInfo()
            cmdBuildQuote.Enabled = True
        End If
    End Sub  '-index changed.-
    '= = = = = = = = = = = = 

    '-- END of  Q U O T E S  E v e n t s ---
    '-- END of  Q U O T E S  E v e n t s ---
    '-- END of  Q U O T E S  E v e n t s ---
    '-- END of  Q U O T E S  E v e n t s ---
    '-===FF->

    '-- Amend new job..--

    Private Function mbAmendAgreement(ByVal bCheckingIn As Boolean) As Boolean
        Dim sStatus As String
        Dim lngJobId As Integer
        Dim frmNewJob3A As New frmNewJobBase  '=4219.1214= was frmNewJob32

        If (msStaffName = "") Then Exit Function '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        '=====    If (lngJobId > 0) Then '--we have selection..-
        If mbGetSelectedJobNo(lngJobId, sStatus) Then
            '-- call New Job with colKeys for RE-EDIT..-

            '--  CHECK that job is not in use..  --

            frmNewJob3A.connectionSql = mCnnSql
            '== frmNewJob3.connectionJet = mCnnJet
            frmNewJob3A.dbInfoSql = mColSqlDBInfo
            '== frmNewJob3.dbInfoJet = mColJetDBInfo
            frmNewJob3A.retailHost = mRetailHost1

            '--  New Job needs to access both printers..--
            '--  New Job needs to access both printers..--
            '== 3203.107=  Printers are NOW discovered within target FORM code...
            '== frmNewJob3A.ColourPrinterName = msColourPrtName
            '== frmNewJob3A.ReceiptPrinterName = msReceiptPrtName
            '== frmNewJob3A.LabelPrinterName = msLabelPrtName

            '=331.331= frmNewJob3A.LabourHourlyRatePriority1 = mCurLabourHourlyRateP1
            '=331.331= frmNewJob3A.LabourHourlyRatePriority2 = mCurLabourHourlyRateP2
            '=331.331= frmNewJob3A.LabourHourlyRatePriority3 = mCurLabourHourlyRateP3
            '===frmNewJob2.DescriptionPriority1 = msDescriptionPriority1
            '===frmNewJob2.DescriptionPriority2 = msDescriptionPriority2
            '===frmNewJob2.DescriptionPriority3 = msDescriptionPriority3
            frmNewJob3A.LabourMinCharge = mCurLabourMinCharge
            '--label stuff.-
            '==3072== frmNewJob3A.LabelBarcodeFontName = msItemBarcodeFontName
            '==3072== frmNewJob3A.LabelBarcodeFontSize = mlItemBarcodeFontSize
            '===frmNewJob2.JobLabelPrintDepth = msJobLabelPrintDepth
            '===frmNewJob2.JobLabelGapDepth = msJobLabelGapDepth
            frmNewJob3A.NotificationCostLimit = mCurServiceNotificationCostLimit
            '==3072== frmNewJob3A.NewJobDocketFootnote = msNewJobDocketFootnote
            '==3072== frmNewJob3A.TermsText = msTermsText
            '-msServiceChargesInfoText-
            '==3072== frmNewJob3A.ServiceChargesInfoText = msServiceChargesInfoText

            frmNewJob3A.LicenceOK = mbLicenceOK
            frmNewJob3A.StaffId = mlStaffId
            frmNewJob3A.StaffName = msStaffName
            '-- Passing JOB-NO Indicate RE-EDIT existing Agreement..--
            frmNewJob3A.JobId = lngJobId '==== CLng(colKeys(1))
            frmNewJob3A.UserLogo = Picture2.Image '--pass logo..-
            If bCheckingIn Then frmNewJob3A.IsCheckIn = True
            '== Me.Opacity = 0.8

            frmNewJob3A.ShowDialog()
            '= 3431.0505=  Check if Calendar update needed..
            Dim sXmlPath As String = frmNewJob3A.ExchangeCalendarUpdateXmlFileName
            If (sXmlPath <> "") Then
                '- start bg worker if needed..
                If Not mbExchange201WorkerIsActive Then
                    '-- Re-Start the Exchange operation in the background.
                    Me.BackgroundWorkerExchange201.RunWorkerAsync(mCnnSql)
                    mbExchange201WorkerIsActive = True   '-- flag as active..
                End If
            End If

            frmNewJob3A.Close()
            '== Call mBrowseJobs.refresh() '-- Rev-2804--refresh--
            '=3311.817= Stop doing This!=  Call mbJobsBrowseRefresh()  '==3072/3==

            Call mbRefreshJobsTreeView(True) '--clear.--
            If (lngJobId > 0) Then  '--refresh details..--
                Call mbShowJobInfoRTF(mlJobId)
            End If
            '====End If  '--keys-
        End If '--get job.-row--
        frmNewJob3A.Dispose()
        '== Me.Opacity = 1
        mlStaffTimeout = 0  '= allow full time to run.  280 '--NOW is timing out..  very short--
    End Function '-- amend..-
    '= = = = = = = = =
    '-===FF->

    '--  C o m m a n d s --

    '-- Amend new job..--
    '-- Amend new job..--

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs)  '==   No Checkin..
        Call mbAmendAgreement(False)
        '==Call mbRefreshJobsTreeView
    End Sub '--amend..--
    '= = = = = = = = = = =

    '-- Check-In Booked  job..--
    '-- Check-In Booked  job..--

    Private Sub cmdCheckIn_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs)  '==    (index As Integer)
        Call mbAmendAgreement(True)
        '==Call mbRefreshJobsTreeView
    End Sub '--amend..--
    '= = = = = = = = = = =

    '--  P r i o r i t y  --
    '--  P r i o r i t y  --
    Private Sub cboPriority_Validating(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                               Handles cboPriority.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        cboPriority.Enabled = False
        cboPriority.Visible = False
        cmdChangePriority.Enabled = True
        eventArgs.Cancel = keepfocus
    End Sub '--validate--
    '= = = = = = = = = = = =

    Private Sub cboPriority_KeyPress(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                          Handles cboPriority.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        If keyAscii = 27 Then '--ESC..--
            cboPriority.Enabled = False
            cboPriority.Visible = False
            cmdChangePriority.Enabled = True
            keyAscii = 0
        End If
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..--
    '= = = = = = = = = = =
    '-===FF->

    '-- cboPriority_Click--

    'UPGRADE_WARNING: Event cboPriority.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboPriority_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                   ByVal eventArgs As System.EventArgs) _
                                                      Handles cboPriority.SelectedIndexChanged
        Dim sDescr As String
        Dim sStatus As String
        Dim sOldPriority As String
        Dim sPriority As String
        Dim sServiceNotes As String
        Dim sNewNote As String
        Dim sSql As String
        Dim sErrorMsg As String
        Dim lngJobId, lngaffected As Integer
        Dim intKey As Short
        Dim bIsOnSiteJob As Boolean

        If mbGetSelectedJobNo(lngJobId, sStatus) Then
            If (cboPriority.SelectedIndex >= 0) Then
                '--get selected priority and update Job..--
                sDescr = cboPriority.Text '--selected itm.--
                sServiceNotes = mColJobFields.Item("ServiceNotes")("value")
                sOldPriority = mColJobFields.Item("Priority")("value")
                bIsOnSiteJob = (UCase(mColJobFields.Item("GoodsInCare")("value")) = K_GOODS_ONSITEJOB)
                If IsNumeric(VB.Left(sDescr, 1)) Then
                    sPriority = VB.Left(sDescr, 1) '--selected.. -
                    Call mbShowJobPriority(sPriority, bIsOnSiteJob)
                    sNewNote = ""
                    '===If sServiceNotes <> "" Then sNewNote = vbCrLf
                    If (sServiceNotes <> "") And (VB.Right(sServiceNotes, 2) <> vbCrLf) Then sNewNote = vbCrLf
                    sNewNote = sNewNote & msStaffName & ": " & Format(Now, "dd-MMM-yyyy hh:mm tt") & _
                                                      ".- " & "Job Priority Changed from: " & sOldPriority & _
                                                                            " to " & msFixSqlStr(sPriority) & "." & vbCrLf
                    '--  Update Jobs Table..--
                    sSql = "UPDATE [Jobs] "
                    sSql = sSql & " SET Priority='" & msFixSqlStr(sPriority) & "'"
                    sSql = sSql & ", ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',3249) "
                    sSql = sSql & ", TechStaffName=' " & msFixSqlStr(msStaffName) & "'"
                    sSql = sSql & ", DateUpdated= CURRENT_TIMESTAMP "
                    sSql = sSql & " WHERE Job_Id=" & CStr(lngJobId) & ";"
                    If Not gbExecuteCmd(mCnnSql, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                        MsgBox("ERROR- Failed SQL UPDATE: SQL was:" & vbCrLf & sSql & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                    Else '--ok-
                        If gbDebug Then MsgBox("OK.. Job " & lngJobId & " updated..." & vbCrLf & "  and " & lngaffected & " records were affected..", MsgBoxStyle.Information)
                    End If '-exec..-
                    '-show updated job Info..--
                    Call mbShowJobInfoRTF(lngJobId)
                End If
                cboPriority.Enabled = False
                cboPriority.Visible = False
                cmdChangePriority.Enabled = True
                cmdChangePriority.Focus()
            End If '--index.-
        Else '-- no job.-
            intKey = 27
            Call cboPriority_KeyPress(cboPriority, New System.Windows.Forms.KeyPressEventArgs(Chr(intKey)))
        End If '--job..-
    End Sub '--cboPriority_Click--
    '= = = = = = = = = = =

    '--ChangePriority ---
    '--ChangePriority ---

    Private Sub cmdChangePriority_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles cmdChangePriority.Click
        cmdChangePriority.Enabled = False
        cboPriority.Visible = True
        cboPriority.Enabled = True
        cboPriority.Focus()
        mlStaffTimeout = 0 '--NOW is timing out.. Allow full interval--
    End Sub '--change priority..-
    '= = = = = = = = = = = =
    '-===FF->

    '--  C o m m a n d s --
    '--  C o m m a n d s --

    '-- Start--

    Private Sub cmdStart_Click() '==(index As Integer)

        '== Call mbShowJobMaintform(True, False, False) '--service=true, notify=false- delivery=false, ALWAYS-MODAL-
        Call mbShowJobMaintform(True, False) '--service=true, delivery=false, ALWAYS-MODAL-

        '== Call mBrowseJobs.refresh() '-- Rev-2804--refresh--
        '=3311.817= Stop doing This!=  Call mbJobsBrowseRefresh()  '==3072/3==

        Call mbRefreshJobsTreeView(True) '--initialise.--

        '== 3043= update detail info.--
        If (mlJobId > 0) Then Call mbShowJobInfoRTF(mlJobId)

        '=3311.817= Any activity resets to full timer-
        mlStaffTimeout = 0 '--NOW is timing out.. Allow full interval--

    End Sub '--start--
    '= = = = = = = = = = =

    '--Update --

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs)  '===(index As Integer)
        Call cmdStart_Click() '===(index)
    End Sub '--start--
    '= = = = = = = = = = =
    '-===FF->

    '= 3203.124= - RETURN to Queue..
    '= 3203.124= - RETURN to Queue.. 
    '=- 3203.211-  OR WAITLIST -
    '=
    '=- 3403.0609-  Ask if Job is to be UN-ASSIGNED..
    '==

    Private Function mbJobActionReturnToQueue(Optional ByVal bToWaitlist As Boolean = False) As Boolean
        Const k_statusCreated As String = "10-Created"
        Const k_statusWaitListed As String = "05-WaitListed"

        Dim node1 As TreeNode
        Dim intJobNo, lngaffected As Integer
        Dim s1, s2, sStatus, sSql, sNewNote, sErrorMsg As String
        Dim sNewStatus As String = k_statusCreated
        Dim sTargetMsg As String = "Input Queue"
        Dim sNominTech As String = ""
        Dim sSetNomTech As String = ""  '= ", NominatedTech='' "  '-clear Tech in Job.

        If bToWaitlist Then
            sNewStatus = k_statusWaitListed
            sTargetMsg = "WaitList"
        End If
        '-- set up selected Job-
        mlStaffTimeout = -1 '-SUSPEND timing out..--
        '=If mbJobActionSetup("ReturnToQueue", node1, intJobNo) Then
        If mbGetSelectedJobNo(intJobNo, sStatus) Then
            '=sStatus = Trim(mColJobFields.Item("JobStatus")("value"))
            s2 = VB.Left(sStatus, 2)
            If (s2 = "33") Or (s2 = "43") Then
                MsgBox("That Job Record is in use.." & vbCrLf & "Check with other users..", MsgBoxStyle.Exclamation)
                mlStaffTimeout = 0 '--NOW is timing out..--
                Exit Function
            Else '-ok-
                '- mColJobFields.Item("JobStatus")("value")
                '=3403.609= -check if we need to un-assign Tech-
                If (mColJobFields IsNot Nothing) AndAlso _
                          (mColJobFields.Item("job_id")("value") = intJobNo) Then
                    '-- got the right record-
                    sNominTech = Trim(mColJobFields.Item("nominatedTech")("value"))
                End If '-right job-
                '- done with tech-

                sNewNote = vbCrLf & "** NB: JOB Returned to " & sTargetMsg & ":" & vbCrLf & _
                     Format(Now, "dd-MMM-yyyy hh:mm tt") & ", " & _
                         "; " & msStaffName & "-" & vbCrLf
                If MsgBox("Sure you want to RETURN Job No: " & intJobNo & " To the " & sTargetMsg & " ?", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then

                    If (sNominTech <> "") Then
                        '--Job has tech-  Ask if we need to UN-assign-
                        If MsgBox("Set Tech owner for Job back to UNASSIGNED ?", _
                               MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                            sSetNomTech = ", NominatedTech='' "  '-clear Tech in Job.
                        End If
                    End If  '-tech job-

                    '-- OK.. update the status back to "10" to go back in Queue..
                    '--   or "05" to get waitlisted..
                    sSql = "UPDATE dbo.Jobs SET "
                    sSql &= " JobStatus='" & sNewStatus & "'"
                    sSql &= sSetNomTech
                    sSql &= ", ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',4000)"
                    sSql &= ", DateUpdated= CURRENT_TIMESTAMP "
                    sSql &= "  WHERE Job_Id=" & CStr(intJobNo) & ";"

                    If Not gbExecuteCmd(mCnnSql, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                        MsgBox("ERROR- Failed SQL Job Status UPDATE:" & vbCrLf & _
                                 "SQL was:" & vbCrLf & sSql & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                    Else '--ok-
                        MsgBox("OK.. Job " & intJobNo & " Job Status was updated.." & vbCrLf & _
                                    vbCrLf & vbCrLf & "  and " & lngaffected & " records were affected..", MsgBoxStyle.Information)
                    End If '-exec..-
                End If '--yes.-
            End If  '--33-
            '= End If  '-setup-
            '=3311.817= Stop doing This!=  Call mbJobsBrowseRefresh()  '==3072/3==

            Call mbRefreshJobsTreeView(True) '--initialise.--
            If (mlJobId > 0) Then
                Call mbShowJobInfoRTF(mlJobId)
            End If
        End If '-get selected-
        mlStaffTimeout = 0 '--NOW is timing out..--

    End Function '--Return to Queue..
    '= = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--Notify-
    '--Notify-
    '--  With NEW FORM for NOTIFY..--

    Private Sub cmdDetailNotify_Click()
        '== Handles cmdDetailNotify_X.Click
        Dim lngJobId As Integer
        Dim lRow, lCol As Integer
        Dim s1 As String
        Dim sStatus As String
        Dim sCustname As String
        Dim sPhone, sMobile As String
        Dim sReason As String
        Dim colRowValues As Collection
        Dim colKeys As Collection
        Dim ColJobFields As Collection
        Dim frmNotifyCust1 As New frmNotifyCust

        '====== Call mbShowJobMaintform(index, False, True, False) '--service=false, notify=true- delivery=false-
        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        If mbGetSelectedJobNo(lngJobId, sStatus) Then
            sReason = "Job can start.." '--if Waitlisted.. status "05" ==
            If (VB.Left(sStatus, 2) = "50") Then
                sReason = "Job completed.."
            ElseIf (VB.Left(sStatus, 2) <= "10") Then  '--queued..-
                sReason = "Job in Queue"
            ElseIf (VB.Left(sStatus, 2) < "50") Then
                sReason = "Job Started"
            ElseIf (VB.Left(sStatus, 2) = "70") Then
                sReason = "Job Delivered"
            Else
                sReason = "No Job Status info.."
            End If
            '===sStatus = Trim(ColRowValues("JobStatus")("value"))
            '===lngJobId = CLng(Trim(ColRowValues("Job_id")("value")))
            If (lngJobId <> mlJobId) Then '--different  row..-
                Call mbShowJobInfoRTF(lngJobId)
            End If
            ColJobFields = mColJobFields '--easier--

            '--  get Cust-info from current job collection..--
            sCustname = "[" & ColJobFields.Item("CustomerBarcode")("value") & "] " & ColJobFields.Item("CustomerName")("value") & vbCrLf & ColJobFields.Item("CustomerCompany")("value") & vbCrLf
            sPhone = ColJobFields.Item("CustomerPhone")("value")
            sMobile = ColJobFields.Item("CustomerMobile")("value")
            '== Load(frmNotifyCust)

            frmNotifyCust1.connection = mCnnSql

            frmNotifyCust1.Previous = ColJobFields.Item("Notifications")("value") '== "Previous lots of stuff.."
            frmNotifyCust1.CustomerName = sCustname '==txtCustCompany.Text & vbCrLf & txtCustName.Text   '=== "Customer Jim"
            frmNotifyCust1.CustomerPhone = sPhone '== txtCustPhone.Text     '=="02 1234 5678"
            '==frmNotifyCust.CustomerMobile = txtCustMobile.Text   '== "0424 101 360"
            frmNotifyCust1.CustomerMobile = sMobile
            frmNotifyCust1.CustomerEmail = msCustomerEmail

            '==3107.1013 - Pass on the full RM cust stuff for given names etc..
            frmNotifyCust1.RMCustomerDetails = mColRMCustomerDetails

            frmNotifyCust1.Reason = sReason '=="Job completed.."
            frmNotifyCust1.JobId = lngJobId '--  789   '- 2777
            frmNotifyCust1.StaffName = msStaffName

            frmNotifyCust1.ShowDialog()

            frmNotifyCust1.Close()
            Call mbShowJobInfoRTF(lngJobId) '-update details..-
            '===Call maBrowse1(intTabIndex).refresh
            '===End If  '--lrow..-
        End If '--selected job.-
        frmNotifyCust1.Dispose()
        mlStaffTimeout = 0 '--NOW is timing out..--
    End Sub '--notify-
    '= = = = = = = =  =
    '-===FF->

    '--  Reopen --
    '--  Reopen --
    '-- NO loading of maint form --!!

    Private Sub cmdReOpen_Click()  '==(index As Integer)
        Dim sStatus As String
        Dim sWhere As String
        Dim sSql As String
        Dim sErrorMsg As String
        Dim job_id As Integer
        Dim lngaffected As Integer

        '-- Get selection..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        If mbGetSelectedJobNo(job_id, sStatus) Then

            '===If colKeys.Count > 0 Then '--we have selection..-
            '-- update status..--
            '===job_id = CLng(colKeys(1))
            sSql = "UPDATE [Jobs] SET "
            sSql = sSql & " JobStatus='30-Started' "
            sSql = sSql & ", TechStaffName='" & msFixSqlStr(msStaffName) & "'"
            sSql = sSql & ", DateUpdated= CURRENT_TIMESTAMP "
            sSql = sSql & " WHERE Job_Id=" & CStr(job_id) & ";"
            If MsgBox("Sure you want to re-open Job No: " & job_id & " for Service?", _
                                 MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If Not gbExecuteCmd(mCnnSql, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                    MsgBox("ERROR- Failed SQL UPDATE: SQL was:" & vbCrLf & sSql & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                Else '--ok-
                    MsgBox("OK.. Job " & job_id & " status changed to '30-Started'.." & vbCrLf & _
                                                "  and " & lngaffected & " records were affected..", MsgBoxStyle.Information)
                End If '-exec..-
            End If '--yes.-
            '===End If  '--keys-
        End If '--nothing..-
        '===End If  '--browse..-

        '== Call mBrowseJobs.refresh() '-- Rev-2804--refresh--
        '=3311.817= Stop doing This!=  Call mbJobsBrowseRefresh()  '==3072/3==

        Call mbRefreshJobsTreeView(True) '--initialise.--

        mlStaffTimeout = 0 '--NOW is timing out..--

    End Sub  '-re-open-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Deliver--

    Private Sub cmdDeliver_Click()  '====(index As Integer)

        '== Call mbShowJobMaintform(False, False, True) '--service=false, notify=false- delivery=true-
        Call mbShowJobMaintform(False, True) '--service=false,  delivery=true-

        '== Call mBrowseJobs.refresh() '-- Rev-2804--refresh--
        '=3311.817= Stop doing This!=  Call mbJobsBrowseRefresh()  '==3072/3==

        Call mbRefreshJobsTreeView(True) '--initialise.--

        '== mlStaffTimeout = 280 '--NOW is timing out..  VERY SHORT--
        '=3311.817= Any activity resets to full timer-
        mlStaffTimeout = 0 '--NOW is timing out.. Allow full interval--

    End Sub '--  Deliver--
    '= = = = = = =  = = =

    '-- stopPress-
    '-- stopPress-
    '--  Add a note to service notes..
    '==
    '==   Target-New-Build-4262 -- (Started 12-Aug-2020)
    '==
    '==   --  In Main Form, cmdStopPress_click need to be updated..   
    '==         1. The UPDATE statement clause
    '==            sSql = sSql & " ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',4000) "
    '==            Needs to be changed to this: (because ServiceNotes is now varchar(max).)
    '==                  sSql = sSql & " ServiceNotes=(ServiceNotes+'" & sNewNote & "') "
    '==         2. The procedure should check for external updates AFTER getting msg text from 	UI,
    '==                	and inside a transaction..
    '==


    Private Sub cmdStopPress_Click()  '==(index As Integer)

        Dim sKey As String
        Dim sStatus As String
        Dim sServiceNotes As String
        Dim sNewNote As String
        Dim sWhere As String
        Dim sSql As String
        Dim sText As String
        Dim sErrorMsg As String
        Dim job_id As Integer
        Dim L1, lngaffected As Integer
        Dim ColJobFields As Collection

        '-- Get selection..-

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        If mbGetSelectedJobNo(job_id, sStatus) Then
            mlJobId = job_id
            '==Set ColJobFields = mColJobFields '--easier--
            '==sStatus = "" & ColJobFields("JobStatus")("value")
            If (VB.Left(sStatus, 2) = "33") Or (VB.Left(sStatus, 2) = "43") Then
                MsgBox("That Job Record is in use.." & vbCrLf & "Check with other users..", MsgBoxStyle.Exclamation)
                '===Call Toolbar2_ButtonClick(index, mButtonCurrentBrowse(index))  '--refresh browse grid..-
                '==Call maBrowse1(index).refresh
                mlStaffTimeout = 0 '--NOW is timing out..--
                Exit Sub
            End If
            sText = InputBox("Enter a short note to add to Service Notes:", "Stop Press-  Job No: " & job_id, "Stop Press-")
            If (sText <> "") Then
                sServiceNotes = mColJobFields.Item("ServiceNotes")("value")
                sNewNote = ""
                '--  keep excess linefeeds to a min..-
                If (sServiceNotes <> "") And (VB.Right(sServiceNotes, 2) <> vbCrLf) Then sNewNote = vbCrLf
                If VB.Right(sText, 2) <> vbCrLf Then sText = sText & vbCrLf
                sNewNote = sNewNote & "** ALERT! STOP PRESS:" & vbCrLf & _
                              Format(Now, "dd-MMM-yyyy hh:mm tt") & ", " & _
                                   "; " & msStaffName & "-" & vbCrLf & msFixSqlStr(sText)
                sSql = "UPDATE [Jobs] SET "
                '= 3323.1110= sSql = sSql & " ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',3249) "
                '==   Target-New-Build-4262 -- (Started 12-Aug-2020)
                '=sSql = sSql & " ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',4000) "
                sSql &= " ServiceNotes=(ServiceNotes+'" & sNewNote & "') "
                sSql = sSql & ", TechStaffName='" & msFixSqlStr(msStaffName) & "'"
                sSql = sSql & ", DateUpdated= CURRENT_TIMESTAMP "
                sSql = sSql & "  WHERE Job_Id=" & CStr(job_id) & ";"
                If MsgBox("Sure you want to UPDATE Job No: " & job_id & " with the Note:" & vbCrLf & vbCrLf & _
                                sText, MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    If Not gbExecuteCmd(mCnnSql, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                        MsgBox("ERROR- Failed SQL UPDATE:" & vbCrLf & _
                                    "SQL was:" & vbCrLf & sSql & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                    Else '--ok-
                        If gbDebug Then MsgBox("OK.. Job " & job_id & " updated with note:" & vbCrLf & _
                               sText & vbCrLf & vbCrLf & "  and " & lngaffected & " records were affected..", MsgBoxStyle.Information)
                    End If '-exec..-
                End If '--yes.-
            End If '--text..-
            '===End If  '--keys-
        Else
            MsgBox("Nothing selected.", MsgBoxStyle.Exclamation)
        End If '--nothing..-
        '===End If  '--browse..-
        '=3311.817= Stop doing This!=  Call mbJobsBrowseRefresh()  '==3072/3==

        Call mbRefreshJobsTreeView() '--initialise.--
        If (mlJobId > 0) Then Call mbShowJobInfoRTF(mlJobId)

        mlStaffTimeout = 0 '--NOW is timing out..--

    End Sub '-- stopPress-
    '= = = = = = = = = =
    '-===FF->

    '-  View/print only.-
    '-  View/print only.-

    Private Sub cmdViewRecord_Click() '==(index As Integer)

        '== Call mbShowJobMaintform(False, False, False) '--service=false, notify=false- delivery=false-
        Call mbShowJobMaintform(False, False) '--service=false,  delivery=false-

    End Sub '--view.-
    '= = = = = = = = = =

    '-- Show Customer History..-
    '-- Show Customer History..-
    '--- Cust. from Current Job Details..-

    Private Sub cmdDetailCustHistory_Click(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) _
                                                '==(index As Integer)
        Dim sBarcode As String
        Dim lCol, lRow, lngCustId As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection
        Dim frmCustHistory1 As New frmCustHistory

        '==lRow = MSHFlexGrid1(index).Row
        '==lCol = MSHFlexGrid1(index).Col
        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        If (mlJobId > 0) Then '--active.-
            sBarcode = mColJobFields.Item("CustomerBarcode")("value")
            lngCustId = CInt(mColJobFields.Item("RMCustomer_Id")("value"))
            '==MsgBox "Cust barcode is:" & sBarcode
            '== Load(frmCustHistory)

            '== frmCustHistory.connectionJet = mCnnJet
            frmCustHistory1.connectionSql = mCnnSql
            '== frmCustHistory.dbInfoJet = mColJetDBInfo
            frmCustHistory1.dbInfoSql = mColSqlDBInfo

            frmCustHistory1.retailHost = mRetailHost1

            frmCustHistory1.BusinessName = msBusinessName
            '=3059.1-  Restore passing ID..--
            frmCustHistory1.CustomerId = lngCustId
            frmCustHistory1.CustomerBarcode = sBarcode
            frmCustHistory1.ShowDialog()

            frmCustHistory1.Close()
        End If
        frmCustHistory1.Dispose()
        mlStaffTimeout = 0 '--NOW is timing out..--
    End Sub '--history..-
    '= = = = = = = = = = = =
    '-===FF->

    '-- Main Commands---
    '-- Main Commands---

    '-- Accept new job..--
    '-- Accept new job..--

    Private Sub mbNewJob_Click(Optional ByVal bIsOnSiteBooking As Boolean = False)
        Dim frmNewJob3A As New frmNewJobBase  '=4219.1214= was frmNewJob32
        Dim bIsWorkshopBooking As Boolean = False  '-redundant.-

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        If msCustomerBarcode = "" Then
            MsgBox("No customer selected.." & vbCrLf & vbCrLf & _
                    "Click on the Customer Tab to look up Retail Customers.    ", MsgBoxStyle.Exclamation)
            mlStaffTimeout = 0 '--NOW is timing out..--
            Exit Sub
        End If

        frmNewJob3A.connectionSql = mCnnSql
        '== frmNewJob3.connectionJet = mCnnJet

        frmNewJob3A.dbInfoSql = mColSqlDBInfo
        '=== frmNewJob3.dbInfoJet = mColJetDBInfo
        frmNewJob3A.retailHost = mRetailHost1

        frmNewJob3A.CustomerBarcode = msCustomerBarcode
        frmNewJob3A.CustomerId = mlCustomerId
        frmNewJob3A.CustomerCompany = msCustomerCompany
        frmNewJob3A.CustomerName = msCustomerName
        frmNewJob3A.CustomerPhone = msCustomerPhone
        frmNewJob3A.CustomerMobile = msCustomerMobile

        '--  New Job needs to access both printers..--
        '--  New Job needs to access both printers..--

        '== 3203.107=  Printers are NOW discovered within target FORM code...

        '--  notify if NO LICENCE..  !!!--
        '--  notify if NO LICENCE..  !!!--
        '--  notify if NO LICENCE..  !!!--
        If Not mbLicenceOK Then
            MsgBox("No valid licence found for this JobMatix site.." & vbCrLf & _
                   "(See JobMatix website for Licence details..) ", MsgBoxStyle.Exclamation)
        End If
        '=331.331= frmNewJob3A.LabourHourlyRatePriority1 = mCurLabourHourlyRateP1
        '=331.331= frmNewJob3A.LabourHourlyRatePriority2 = mCurLabourHourlyRateP2
        '=331.331= frmNewJob3A.LabourHourlyRatePriority3 = mCurLabourHourlyRateP3
        frmNewJob3A.LabourMinCharge = mCurLabourMinCharge
        '--label stuff.-
        frmNewJob3A.NotificationCostLimit = mCurServiceNotificationCostLimit

        frmNewJob3A.LicenceOK = mbLicenceOK
        frmNewJob3A.StaffId = mlStaffId
        frmNewJob3A.StaffName = msStaffName
        If bIsWorkshopBooking Then
            '=3203.124=  Booking Decision made by NewJob Form--
            '==frmNewJob3A.IsBooking = True
        ElseIf bIsOnSiteBooking Then
            frmNewJob3A.IsOnSiteJob = True
        End If
        '== frmNewJob3A.IsBooking = bIsWorkshopBooking
        frmNewJob3A.UserLogo = Picture2.Image '--pass logo..-
        If mColCustomerJobsGoods IsNot Nothing Then
            frmNewJob3A.CustomerJobsGoods = mColCustomerJobsGoods
        End If  '--nothing-
        frmNewJob3A.ShowDialog()
        '= 3431.0505=  Check if Calendar update needed..
        Dim sXmlPath As String = frmNewJob3A.ExchangeCalendarUpdateXmlFileName
        If (sXmlPath <> "") Then
            '- start bg worker if needed..
            If Not mbExchange201WorkerIsActive Then
                '-- Re-Start the Exchange operation in the background.
                Me.BackgroundWorkerExchange201.RunWorkerAsync(mCnnSql)
                mbExchange201WorkerIsActive = True   '-- flag as active..
            End If
        End If

        frmNewJob3A.Close()
        frmNewJob3A.Dispose()
        '=3311.817= Stop doing This!=  Call mbJobsBrowseRefresh()  '==3072/3==

        Call mbRefreshJobsTreeView(True) '--initialise.--
        '= mlStaffTimeout = 280 '--NOW is timing out..  very short--

        '=3311.817= Any activity resets to full timer-
        mlStaffTimeout = 0 '--NOW is timing out.. Allow full interval--

    End Sub '-new job--
    '= = = = = = =  =

    '-- CMD Accept new job..--
    '-- CMD Accept new job..--

    '== Private Sub cmdAccept_Click(ByVal eventSender As System.Object, _
    '==                                 ByVal eventArgs As System.EventArgs)  '-not booking..-
    '==     Call mbNewJob_Click(False)
    '== End Sub '--new--
    '= = = = = = = = = =

    '-- Job Booking --
    '== Private Sub cmdNewBooking_Click(ByVal eventSender As System.Object, _
    '==                                    ByVal eventArgs As System.EventArgs)
    '==     Call mbNewJob_Click(True) '--booking..-
    '== End Sub '--booking.-
    '= = = = = = = = = =

    Public Sub mnuNewJob_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles mnuNewJob.Click

        Call mbNewJob_Click() '--normal new job..-
    End Sub '--mnu New.-
    '= = = =  = = = = ==

    Private Sub NewOnSiteJobToolStripMenuItem_Click(sender As Object, ev As EventArgs) _
                                                     Handles NewOnSiteJobToolStripMenuItem.Click
        Call mbNewJob_Click(True) '-not-normal, IS OnSite..-

    End Sub '-on-site job..-
    '= = = = = = = = = = = = =

    '--MENU Job Booking --
    '== Public Sub mnuJobBooking_Click(ByVal eventSender As System.Object, _
    '==                                          ByVal eventArgs As System.EventArgs) Handles mnuJobBooking.Click
    '==     Call mbNewJob_Click(True) '--BOOKING new job..-
    '== End Sub '--mnu New.-
    '= = = =  = = = = ==
    '-===FF->

    '-- New Booking/ New Job Toolstrip.--

    Private Sub toolbarNewJob2_ButtonClick(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) _
                        Handles btnHistory.Click, btnAcceptJob.Click, btnOnSiteJob.Click
        Dim button1 As System.Windows.Forms.ToolStripButton = CType(eventSender, System.Windows.Forms.ToolStripButton)

        Select Case LCase(button1.Tag)

            Case "history"
                Call cmdDetailCustHistory_Click(button1, eventArgs)
                '= Case "bookjob"
                '=   Call cmdNewBooking_Click(button1, eventArgs)
            Case "acceptjob"
                '== cmdAccept_Click(button1, eventArgs)
                Call mbNewJob_Click(False)  '- workshop Job.-
            Case "onsitejob"
                '== cmdAccept_Click(button1, eventArgs)
                '== MsgBox("On site job to come..", MsgBoxStyle.Information)
                Call mbNewJob_Click(True) '-not-normal, IS OnSite..-
            Case Else
        End Select
    End Sub  '--Job Action.-
    '= = = = = = = = = = = = =
    '= = = = = = = = = = = =
    '-===FF->

    '-- Browse job parts.--
    '-- Browse job parts.--

    Public Sub mnuShowJobParts_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles mnuShowJobParts.Click

        Dim colKeys As Collection '--primary keys of selected record-
        Dim sWhere As String
        Dim colPrefs As Collection
        Dim colSelectedRow As Collection
        Dim frmJobMaint3A As New frmJobMaint3

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        sWhere = "" '----" (LEFT(jobStatus,2)='50')" '-- completed jobs..--
        '-- Prefs for Viewing..-
        Call gbMakeCollection(New Object() {"RMCat1", "RMDescription", "PartSerialNumber", _
                           "RMBarcode", "RMStock_Id", "PartJob_id", "RMSell", "DateCreated", "ServicedByStaffName"}, colPrefs)
        If mbBrowseTable(colPrefs, "All Parts in Jobs..", sWhere, colKeys, colSelectedRow, "JobParts") Then
            If colKeys.Count() > 0 Then '--we have selection..-
                '-- call Job Edit with selected jobno..--
                frmJobMaint3A.JobNo = CInt(colSelectedRow.Item("PartJob_Id")("value"))
                frmJobMaint3A.connectionSql = mCnnSql
                '== frmJobMaint2.connectionJet = mCnnJet
                frmJobMaint3A.retailHost = mRetailHost1

                frmJobMaint3A.dbInfoSql = mColSqlDBInfo
                '== frmJobMaint2.dbInfoJet = mColJetDBInfo

                '-- V I E W  O N L Y --
                frmJobMaint3A.ServiceUpdate = False '-- NOT service type--
                frmJobMaint3A.DeliveryUpdate = False '--NOT delivery type--
                '== frmJobMaint3A.NotifyUpdate = False

                '--Set Printer = mPrtColour  '--vb to default to colour printer..-
                '-- DONE DISCOVERED by form..
                '== frmJobMaint3A.ColourPrinterName = msColourPrtName
                '== frmJobMaint3A.ReceiptPrinterName = msReceiptPrtName

                frmJobMaint3A.StaffId = mlStaffId
                frmJobMaint3A.StaffName = msStaffName
                frmJobMaint3A.StaffBarcode = msStaffBarcode

                frmJobMaint3A.ShowDialog(Me)
                frmJobMaint3A.Close()
            End If '--keys-
        End If '-browse..-
        frmJobMaint3A.Dispose()
        mlStaffTimeout = 0 '--NOW is timing out..--
    End Sub '-- ViewAllParts-
    '= = = = = = = =  =
    '-===FF->

    '-- TextSearch for Part by barcode,Serial etc. --
    '-- TextSearch for Part by barcode,Serial etc. --

    Public Sub mnuFindPart_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles mnuFindPart.Click
        Dim frmFindPart1 As New FrmFindPart

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        frmFindPart1.sqlConnection = mCnnSql
        frmFindPart1.SerialAudit = False
        '==FrmFindPart.JetConnection = mCnnJet
        frmFindPart1.retailHost = mRetailHost1
        frmFindPart1.ColSqlDBInfo = mColSqlDBInfo
        '-more-
        frmFindPart1.Staff_id = mlStaffId
        frmFindPart1.StaffBarcode = msStaffBarcode
        frmFindPart1.StaffName = msStaffName

        '= VB6.ShowForm(frmFindPart1, VB6.FormShowConstants.Modal, Me)
        frmFindPart1.ShowDialog(Me)
        frmFindPart1.Close()

        frmFindPart1.Dispose()
        mlStaffTimeout = 0 '--NOW is timing out..--

    End Sub '--find..-
    '= = = = = = = = =

    '--Retail Manager Serials..--

    Public Sub mnuSerialAudit_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles mnuSerialAudit.Click
        Dim frmFindPart1 As New FrmFindPart

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        frmFindPart1.sqlConnection = mCnnSql
        frmFindPart1.SerialAudit = True
        '==FrmFindPart.JetConnection = mCnnJet
        frmFindPart1.retailHost = mRetailHost1

        '= VB6.ShowForm(frmFindPart1, VB6.FormShowConstants.Modal, Me)
        frmFindPart1.ShowDialog(Me)

        frmFindPart1.Close()
        frmFindPart1.Dispose()

        mlStaffTimeout = 0 '--NOW is timing out..--

    End Sub '--SerialAudit--
    '= = = = = = = = = = = =
    '-===FF->

    '-- M E N U -CustomerHistory--
    '-- M E N U -CustomerHistory--

    Public Sub mnuCustomerHistory_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuCustomerHistory.Click
        Dim frmCustHistory1 As New frmCustHistory

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        '== frmCustHistory.connectionJet = mCnnJet
        frmCustHistory1.connectionSql = mCnnSql
        '== frmCustHistory.dbInfoJet = mColJetDBInfo
        frmCustHistory1.dbInfoSql = mColSqlDBInfo

        frmCustHistory1.retailHost = mRetailHost1

        frmCustHistory1.BusinessName = msBusinessName
        frmCustHistory1.ShowDialog()

        frmCustHistory1.Close()
        frmCustHistory1.Dispose()

        mlStaffTimeout = 0 '--NOW is timing out..--

    End Sub '---CustomerHistory--
    '= = = = = = = = = = ==
    '-===FF->

    '--EDIT any REF table..--
    '--EDIT any REF table..--

    '---- pass to ListEdit form..--
    Private Function mbEditRefTable(ByVal sTableName As String, _
                                  ByVal sDescrColName As String, _
                                    ByVal lngMaxLength As Integer, _
                                     ByVal bDeletePermitted As Boolean) As Boolean

        '--Dim sIdColName As String
        Dim colTable As Collection
        Dim colPrimaryKey As Collection
        Dim colFields As Collection
        Dim colFldx As Collection
        Dim sKeyCol As String
        Dim blnIdIsNumericType As Boolean
        Dim isx As Integer
        Dim frmListEdit1 As New frmListEdit

        mbEditRefTable = False
        If (msStaffName = "") Then Exit Function '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        '-- Discover primary key column..--
        For isx = 1 To mColSqlDBInfo.Count()
            colTable = mColSqlDBInfo.Item(isx)
            '--Set colPrimaryKey = colTable(3)
            If UCase(sTableName) = UCase(colTable.Item("TABLENAME")) Then '--found.-
                '--Set colTableInfo = colTable("SOURCEINFO")
                colFields = colTable.Item("FIELDS")
                colPrimaryKey = colTable.Item("PRIMARYKEYS")
                Exit For
            End If
        Next isx
        sKeyCol = "" '--Name of KEY column-
        If Not (colPrimaryKey Is Nothing) Then
            If (colPrimaryKey.Count() > 0) Then sKeyCol = colPrimaryKey.Item(1)
        End If '--pkey-
        If sKeyCol = "" Then
            MsgBox("Table '" & sTableName & "' has no Primary Key defined.", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        colFldx = colFields.Item(sKeyCol) '--get fld info for pkey..-
        blnIdIsNumericType = gbIsNumericType(colFldx.Item("TYPE_NAME"))

        frmListEdit1.maxLength = lngMaxLength
        frmListEdit1.tableName = sTableName
        frmListEdit1.PrimaryKeyColName = sKeyCol
        frmListEdit1.DescrColumn = sDescrColName
        '-- CAUTION !  if PKEY is numeric column. we ASSUME it is Auto-IDENT col --
        '-- CAUTION !  if PKEY is numeric column. we ASSUME it is Auto-IDENT col --
        If blnIdIsNumericType Then frmListEdit1.IdColumn = sKeyCol '--if set, means pkey is Auto-IDENT col..-
        '== frmListEdit1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + 3000)
        '== frmListEdit1.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Left) + 700)
        frmListEdit1.Top = (Me.Top + 200)
        frmListEdit1.Left = (Me.Left + 50)
        frmListEdit1.connection = mCnnSql
        frmListEdit1.deletionsOK = bDeletePermitted
        '--MsgBox "Calling edit for: " + sTableName, vbInformation
        frmListEdit1.ShowDialog()
        '== If Not frmListEdit.cancelled Then '--update..-
        '== End If '--cancelled..-
        frmListEdit1.Close()
        frmListEdit1.Dispose()

        colTable = Nothing

        colPrimaryKey = Nothing
        colFields = Nothing
        colFldx = Nothing
        mlStaffTimeout = 0 '--start timing out..--

        '--Set rs1 = Nothing
    End Function '--edit..--
    '= = = = = = = = = =
    '-===FF->

    '--menu clicks--
    '--menu clicks--


    '--EDIT Goods table..--
    '--EDIT Goods table..--

    Public Sub mnuGoodsAcceptedTypes_Click(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) _
                                                Handles mnuGoodsAcceptedTypes.Click
        Call mbEditRefTable("GoodsTypes", "GoodsTypeDescription", 24, True)

    End Sub '--brands..-
    '= = = = = = = = = = =

    '--EDIT Brands table..--
    '--EDIT Brands table..--

    Public Sub mnuBrands_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles mnuBrands.Click
        Call mbEditRefTable("JobBrands", "BrandName", 24, True)

    End Sub '--brands..-
    '= = = = = = = = = = =

    '--EDIT Symptoms table..--

    Public Sub mnuProblemSymptoms_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuProblemSymptoms.Click

        Call mbEditRefTable("Symptoms", "symptomDescr", 36, True)

    End Sub '--symptoms.-
    '= = = = = = = = = =

    '-- edit ServiceTaskTypes.--
    '--- V2 Can now DELETE since we have dropped taskType_id FKEY column from JobTasks.--

    Public Sub mnuServiceTaskTypes_Click(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) Handles mnuServiceTaskTypes.Click
        Call mbEditRefTable("JobTaskTypes", "TaskTypeDescription", 48, True)

    End Sub '--tasks.-
    '= = = = = = = = = =  =

    '--- edit Model Checklist..-

    Public Sub mnuModelCheckList_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuModelCheckList.Click
        Dim frmModelEdit1 As New frmModelEdit
        '=======Call mbEditRefTable("ModelCheckList", "CheckListDescription", 36, True)

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        '- V22---  ok..--

        frmModelEdit1.connectionSql = mCnnSql
        frmModelEdit1.dbInfoSql = mColSqlDBInfo

        '== frmModelEdit.connectionJet = mCnnJet
        '== frmModelEdit.dbInfoJet = mColJetDBInfo
        frmModelEdit1.retailHost = mRetailHost1

        frmModelEdit1.ServiceChargeCat1 = msServiceChargeCat1
        frmModelEdit1.ServiceChargeCat2 = msServiceChargeCat2

        frmModelEdit1.ShowDialog()

        frmModelEdit1.Close()
        frmModelEdit1.Dispose()
        mlStaffTimeout = 0 '--start timing out..--

    End Sub '--edit checklist..-
    '= = = = = = = = =  =
    '-===FF->

    '--  lookup/ browse for a quote..-
    '----  Returns mColQuote.  and
    '----          mColQuotelines --

    Public Sub mnuCreateQuoteJobs_Click()
        Dim sSql As String
        Dim bCancelled As Boolean
        '--Dim colSelRow As Collection
        Dim col1 As Collection
        Dim sMsg As String
        Dim lngOrderId As Integer
        Dim sCustBarcode, sCustName As String
        Dim frmQuoteJobs1 As frmQuoteJobs

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        If (msCustomerBarcode = "") Then
            MsgBox("No customer selected..", MsgBoxStyle.Exclamation)
            mlStaffTimeout = 0 '--NOW is timing out..--
            Exit Sub
        End If
        If (msCustomerCompany <> "") Then
            sCustName = msCustomerCompany & "; " & msCustomerName
        Else
            sCustName = msCustomerName
        End If
        If Not mbLicenceOK Then
            MsgBox("No valid licence found for this JobMatix site.." & vbCrLf & _
                   "(See JobMatix website for Licence details..) ", MsgBoxStyle.Exclamation)
        End If

        frmQuoteJobs1 = New frmQuoteJobs
        '== frmQuoteJobs1.RMconnection = mCnnJet
        frmQuoteJobs1.retailHost = mRetailHost1

        frmQuoteJobs1.JobsConnection = mCnnSql
        '== frmQuoteJobs1.prtcolour = mPrtColour '--printout to use to colour printer..-
        frmQuoteJobs1.ColourPrtName = msColourPrtName

        '--frmBuildQuotedJobs.Header1 = "Builds jobs from Quotations.."
        frmQuoteJobs1.ChassisCat1 = msQuoteChassisCat1 '--"mother"
        frmQuoteJobs1.ChassisCat2 = msQuoteChassisCat2 '--"skt"  '--socket type-
        frmQuoteJobs1.StaffId = mlStaffId
        frmQuoteJobs1.StaffName = msStaffName
        frmQuoteJobs1.BusinessName = msBusinessName
        frmQuoteJobs1.BusinessAddress1 = msBusinessAddress1
        frmQuoteJobs1.BusinessAddress2 = msBusinessAddress2

        frmQuoteJobs1.OrderId = mlOrderId
        frmQuoteJobs1.ColSelectedRow = mColSelectedQuoteRow

        frmQuoteJobs1.CustomerId = mlCustomerId
        frmQuoteJobs1.CustomerName = msCustomerName

        frmQuoteJobs1.ShowDialog()

        frmQuoteJobs1.Close()

        '== Call mBrowseJobs.refresh() '-- Rev-2804--refresh--
        '== Call mbJobsBrowseRefresh()  '==3072/3==

        '== "Clear" kills Quote detail panel with JobDetail-    
        '== Call mbRefreshJobsTreeView(True) '--initialise.--
        Call mbRefreshJobsTreeView(False) '--initialise.--
        frmQuoteJobs1 = Nothing

        mlStaffTimeout = 0 '--start timing out..--
    End Sub '--create Quote--
    '= = = = = = =  = = =

    '-- Cmd..-
    '-- Cmd..-

    Private Sub cmdBuildQuote_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles cmdBuildQuote.Click

        Call mnuCreateQuoteJobs_Click()

    End Sub '--Quote.-
    '= = = = = = = = = =
    '-===FF->

    '--  Go to POS Main -- (Sales etc)-

    '== Private Sub cmdPOS_Click(ByVal sender As System.Object, _
    '==                            ByVal e As System.EventArgs) Handles cmdPOS.Click
    '== Dim clsJMx31 As New clsJMxPOS31.clsJMxPOS31    '== frmMain As New frmPOS3Main  

    '==     Call clsJMx31.JMx31ShowPOS(mCnnSql, msServer, msSqlDbName, mColSqlDBInfo, mlStaffId, msStaffName)

    '== End Sub  '--cmdPOS_Click-
    '= = = = = = = = = = = = = = =

    '-- R A 's -----
    '-- R A 's -----

    '==   SHELL to ras.exe  .-3101-NO MORE Shell.----
    '==   SHELL to ras.exe  ..-3101-NO MORE Shell---

    'Public Sub mnuLaunchRAs_Click(ByVal eventSender As System.Object, _
    '                                    ByVal eventArgs As System.EventArgs)
    '    Dim vResult As Double
    '    Dim sRAsPath, s1 As String
    '    Dim lngError As Integer

    '    If Not mbLicenceOK Then
    '        MsgBox("No valid licence found for this JobMatix site.." & vbCrLf & _
    '               "(See JobMatix website for Licence details..) ", MsgBoxStyle.Exclamation)
    '    End If
    '    '= 3311.224=
    '    '==  RAs now outsourced to JMxRAs330.exe -
    '    '=

    'End Sub '--Launch RAs-
    '= = = = = = = = = = = = =

    '--  LAUNCH RAs TOP command Button.-

    Private Sub cmdLaunchRAs_Click(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs)

        '=3311.227= Call mnuLaunchRAs_Click(mnuLaunchRAs, New System.EventArgs())
    End Sub  '--cmd RAs.-
    '= = = = =  = = = == =  ====

    '== Private Sub cmdLaunchRAs_Click(ByVal eventSender As System.Object, _
    '==                               ByVal eventArgs As System.EventArgs)

    '==     Call mnuLaunchRAs_Click(mnuLaunchRAs, New System.EventArgs())
    '== End Sub '--new-
    '= = = = = = = = = = = =
    '-===FF->

    '--All Reports.--
    '--All Reports.--

    '--- V2.2--  Separate EXE..--
    '==  3311.0831- 31Aug2016-
    '==     Add Link to JobReports33.exe (.Net).. if installed.

    '    Public Sub mnuAllReports_Click(ByVal eventSender As System.Object, _
    '                                      ByVal eventArgs As System.EventArgs) Handles mnuAllReports.Click
    '        Dim vReports As Double
    '        Dim sReportPath As String
    '        Dim s1, sAllPaths As String
    '        Dim lngError As Integer

    '        '-- First check for later (.Net) version-
    '        sReportPath = msAppPath & "JobReports33.exe "
    '        sAllPaths = sReportPath & vbCrLf  '--for error msg.-

    '        If (Dir(sReportPath) = "") Then  '-not found-
    '            '-- No- so look for original VB6 file ..--
    '            sReportPath = msAppPath & "JobReports.exe "  '=3311.831-
    '            '==s1 = gsGetInstallAppPath("JobReports.exe") '--get jobtracking install path.
    '            If Dir(sReportPath) = "" Then '=not found-
    '                MsgBox("Can't find the file 'JobReports.exe'.. " & vbCrLf & _
    '                         "The JobReports application may need to be installed..", MsgBoxStyle.Exclamation)
    '                Exit Sub
    '            Else '-vb6 ok-
    '            End If
    '        Else '= ok- .Net version found-
    '        End If  '--reportPath-

    '        On Error GoTo mnuAllReports_error
    '        sReportPath = sReportPath & " /server=" & msServer & "  /DBName=" & msSqlDbName
    '        vReports = Shell(sReportPath, AppWinStyle.NormalFocus)
    '        Exit Sub
    'mnuAllReports_error:
    '        lngError = Err().Number
    '        MsgBox("ERROR executing JobReports Shell cmd: " & vbCrLf & sReportPath & vbCrLf & vbCrLf & _
    '                  "Error No: " & lngError & vbCrLf & ErrorToString(lngError) & vbCrLf)
    '    End Sub '--all reports..-
    '== = = = = =  = = = ==  =
    '= = = = = = = = =  = =
    '-===FF->

    '=3501.0713-New version-  with Labour rates..

    '=3431.0712=
    '--  For Reports- User should send labour rates om cmd line..
    '==     -- Caller must send in SIX labour rates on Cmd Line parms...-==
    '==        ie. HrlyRateP1,  HrlyRateP2,  HrlyRateP3,  HrlyRateOnSiteP1,  HrlyRateOnSiteP2,  HrlyRateOnSiteP3

    '== Target-build-4284  (Started 31-Oct-2020)
    '== Target-build-4284  (Started 31-Oct-2020)
    '==    DEV- Bring JobReports into JobTracking Main as a UserControl..
    '---  THIS MENU is obsolete and will disppear.


    'Public Sub mnuAllReports_Click(ByVal eventSender As System.Object, _
    '                                  ByVal eventArgs As System.EventArgs) Handles mnuAllReports.Click
    '    Dim vReports As Double
    '    Dim sReportName, sReportPath, sReportArgs, s1 As String
    '    '= Dim s1, sAllPaths As String
    '    Dim lngError, intResult As Integer
    '    '== Latest labour rates-
    '    Dim decHrlyRateP1, decHrlyRateP2, decHrlyRateP3, decHrlyRateOnSiteP1, decHrlyRateOnSiteP2, decHrlyRateOnSiteP3 As Decimal

    '    '-testing-
    '    '= decHrlyRateP1 = 200 : decHrlyRateP2 = 300 : decHrlyRateP3 = 400
    '    '= decHrlyRateOnSiteP1 = 220 : decHrlyRateOnSiteP2 = 320 : decHrlyRateOnSiteP3 = 420

    '    '-- get current rates..
    '    Dim sDescr As String
    '    Dim bIsOnSite As Boolean = False
    '    Call gbGetPriorityInfo(mCnnSql, "1", bIsOnSite, mRetailHost1, decHrlyRateP1, sDescr)
    '    Call gbGetPriorityInfo(mCnnSql, "2", bIsOnSite, mRetailHost1, decHrlyRateP2, sDescr)
    '    Call gbGetPriorityInfo(mCnnSql, "3", bIsOnSite, mRetailHost1, decHrlyRateP3, sDescr)
    '    bIsOnSite = True
    '    Call gbGetPriorityInfo(mCnnSql, "1", bIsOnSite, mRetailHost1, decHrlyRateOnSiteP1, sDescr)
    '    Call gbGetPriorityInfo(mCnnSql, "2", bIsOnSite, mRetailHost1, decHrlyRateOnSiteP2, sDescr)
    '    Call gbGetPriorityInfo(mCnnSql, "3", bIsOnSite, mRetailHost1, decHrlyRateOnSiteP3, sDescr)

    '    '-- First check for later (.Net) version-
    '    sReportName = "JobReports33"
    '    sReportPath = msAppPath & sReportName & ".exe "
    '    '= sAllPaths = sReportPath & vbCrLf  '--for error msg.-

    '    If (Dir(sReportPath) = "") Then  '-not found-
    '        MessageBox.Show("Report program not found!", "JobMatix Reports", _
    '                                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End If  '--reportPath-

    '    '= On Error GoTo mnuAllReports_error
    '    sReportArgs = " /server=" & msServer & "  /DBName=" & msSqlDbName
    '    '== 3431.0712=
    '    '== vReports = Shell(sReportPath, AppWinStyle.NormalFocus)
    '    '-- ADD labour rates..
    '    sReportArgs &= " /HrlyRateP1= " & CStr(decHrlyRateP1 & " ")
    '    sReportArgs &= " /HrlyRateP2= " & CStr(decHrlyRateP2 & " ")
    '    sReportArgs &= " /HrlyRateP3= " & CStr(decHrlyRateP3 & " ")
    '    sReportArgs &= " /HrlyRateOnSiteP1= " & CStr(decHrlyRateOnSiteP1 & " ")
    '    sReportArgs &= " /HrlyRateOnSiteP2= " & CStr(decHrlyRateOnSiteP2 & " ")
    '    sReportArgs &= " /HrlyRateOnSiteP3= " & CStr(decHrlyRateOnSiteP3 & " ")

    '    '-- check if process already running..
    '    Dim ap As Process() = Process.GetProcessesByName(sReportName)
    '    If (ap IsNot Nothing) AndAlso (ap.Length > 0) Then
    '        '-- is alive.. so switch to it..
    '        Dim intId As IntPtr = ap(0).MainWindowHandle
    '        '-found-  Make it active-
    '        intResult = SetForegroundWindow(intId)
    '    Else '-not there.. so can launch..-
    '        Try
    '            Process.Start(sReportPath, sReportArgs)
    '        Catch ex As Exception
    '            MsgBox("ERROR executing StartProcess cmd: " & vbCrLf & sReportPath & vbCrLf & vbCrLf & _
    '                   "Error: " & vbCrLf & ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
    '        End Try
    '    End If  '-nothing-
    '    Exit Sub
    '    'mnuAllReports_error:
    '    '        lngError = Err().Number
    '    '        MsgBox("ERROR executing JobReports Shell cmd: " & vbCrLf & sReportPath & vbCrLf & vbCrLf & _
    '    '                  "Error No: " & lngError & vbCrLf & ErrorToString(lngError) & vbCrLf)
    'End Sub '--all reports..-
    '= = = = = = = = =  = =
    '-===FF->


    '-- A d m i n --
    '-- A d m i n --

    '--  change Retail Manager JET DB Path/filename..
    '--  change Retail Manager JET DB Path/filename..

    '-- CLEAR ALL jet-Path values in systemInfo, and --
    '--  force a restart of jobs..

    Public Sub mnuResetJetPath_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles mnuResetJetPath.Click
        Dim sSql As String
        Dim sKey, sValue As String
        Dim L1, ix As Integer
        Dim sErrors As String

        sSql = sSql & "    UPDATE [SystemInfo] SET systemValue='' " & vbCrLf & _
                                      " WHERE (SystemKey LIKE 'RM_JetPath%')" & vbCrLf

        If Not MsgBox("This will reset all Jet (Retail Host) DB paths.." & vbCrLf & _
                                          "You will need to restart Jobs application and log-on again." & vbCrLf & _
                                             "do you want to continue and do this ??", _
                    MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '--Unload Me
            '--End
            '== Call cmdClose_Click(cmdClose, New System.EventArgs())
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
            MsgBox("!! Failed in UPDATE jobTracking JETPATH systemInfo.." & vbCrLf & sErrors & vbCrLf, MsgBoxStyle.Critical)
        Else '--ok--
            MsgBox("Ok..  please restart the JobTracking application..", MsgBoxStyle.Information)
            '== Call cmdClose_Click(cmdClose, New System.EventArgs())
            Me.Close()
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub '--change jet..-
    '= = = = =  = =  = = =
    '= = = = = = = = = =
    '-===FF->

    '-- B a c k u p  JOBS DB --
    '-- B a c k u p  JOBS DB --

    '-- Build backup file-name as "JobsBackup_ddmmmyyyy-tttt"  ---  "tttt" is 24-hr time.-
    '---  Use SAVE-AS dialogue to get backup File-spec..--

    '== Build 3077.519 19-May2013=  
    '--     DB Backup.. If running on Server.. Create .bak on local directory first if exists.
    '==                                           Then copy to target..
    Private Function mbBackupJobsDB() As Boolean

        Dim sFinalBackupPath As String = ""

        If (msStaffName = "") Then Exit Function '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        '=3401.415- CALL BACKUP Function..=
        '-- 4219.1125-  New parm JobMatix AppName... 
        '--  and drop Label Parm..
 
        If Not gbBackupJobsDatabase(Me, msSqlServerComputer, mCnnSql, msMachineName, msComputerName, _
                                     msJobmatixAppName, msCurrentUserNT, _
                                      msSqlDbName, dlg1Save, sFinalBackupPath) Then
            MsgBox("Backup not done.", MsgBoxStyle.Exclamation)
        Else  '-ok-
            MsgBox("ok- the DB '" & msSqlDbName & "' was backed up successfully to: " & vbCrLf & sFinalBackupPath, MsgBoxStyle.Information)
        End If  '-backup-

        Me.BringToFront()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        mlStaffTimeout = 0 '--start timing out..--
        labStatus.Text = ""
        '= sdSysInfo = Nothing
    End Function  '--backup--
    '= = = = = = = = = = = = 
    '= = = = = = = = =  = =
    '-===FF->

    '-- Command Button to Backup..--  See toobarJobView ..--

    '--  NEW-- 2921..--
    '----  Show form to update systemInfo..

    Public Sub mnuSetupInfo_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles mnuSetupInfo.Click
        Dim colDBlist As New Collection
        Dim frmSetupJobsDB1 As New frmSetupJobsDB

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        '--  Update.--
        frmSetupJobsDB1.CreateNewDB = False '--update existing db..

        frmSetupJobsDB1.sqlConnection = mCnnSql
        frmSetupJobsDB1.SqlServer = msServer
        frmSetupJobsDB1.ServerComputerName = msSqlServerComputer

        frmSetupJobsDB1.DatabaseName = msSqlDbName
        frmSetupJobsDB1.DBNames = colDBlist '--send list of all DB's-
        '==3311.329=
        frmSetupJobsDB1.retailHost = mRetailHost1

        frmSetupJobsDB1.ShowDialog()

        frmSetupJobsDB1.Close()
        frmSetupJobsDB1.Dispose()

        '-- refresh our static vars.--
        Call mbLoadAllSystemInfo()
        mlStaffTimeout = 0 '--start timing out..--

    End Sub '--update
    '= = = = = = = = = = = =

    '-- update SMS ---

    Public Sub mnuSMSUpdate_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles mnuSMSUpdate.Click
        Dim frmSMSUpdate1 As New frmSMSUpdate

        mlStaffTimeout = -1 '--SUSPEND timing out..--

        frmSMSUpdate1.connection = mCnnSql
        frmSMSUpdate1.ShowDialog()
        frmSMSUpdate1.Close()
        frmSMSUpdate1.Dispose()

        mlStaffTimeout = 0 '--start timing out..--

    End Sub  '-mnuSMSUpdate-
    '= = = = = = = = = = = = =
    '-===FF->

    '==3311.409=
    '-- Enable/disable SMS reminders..

    Private Sub mnuEnableSMSReminders_Click(sender As Object, ev As EventArgs) _
                                                          Handles mnuEnableSMSReminders.Click
        '- update systemInfo-
        '---  "EnableOnSiteSmsReminders"

        If mnuEnableSMSReminders.Checked Then
            Call mSysInfo1.UpdateSystemInfo(New Object() {"EnableOnSiteSmsReminders", "Y"})
            MsgBox("OnSite Sms Reminders are now ENABLED..", MsgBoxStyle.Information)
        Else  '-not checked-
            Call mSysInfo1.UpdateSystemInfo(New Object() {"EnableOnSiteSmsReminders", "N"})
            MsgBox("OnSite Sms Reminders have now been DISABLED..", MsgBoxStyle.Information)
        End If  '--checked-
        mSysInfo1.refreshAll()
    End Sub  '- mnuEnableSMSReminders-
    '= = = = = = = = = =  = = = = = =

    '==3311.423=
    '-- Enable/disable Auto Assign Jobs..

    Private Sub mnuAutoAssignOrphanJobsOnUpdate_Click(sender As Object, e As EventArgs) _
                                                        Handles mnuAutoAssignOrphanJobsOnUpdate.Click
        If mnuAutoAssignOrphanJobsOnUpdate.Checked Then
            Call mSysInfo1.UpdateSystemInfo(New Object() {"AutoAssignOrphanJobsOnUpdate", "Y"})
        Else '-not checked
            Call mSysInfo1.UpdateSystemInfo(New Object() {"AutoAssignOrphanJobsOnUpdate", "N"})
        End If
    End Sub  '--mnuAutoAssignOrphanJobs-
    '= = = = = = = = = = = = = = = = = = =

    '--  show all sys parms..--

    Public Sub mnuShowSystemInfo_Click(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles mnuShowSystemInfo.Click

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        '-- warning on system settings..--
        MsgBox("View/Edit JobTracking System Settings:" & vbCrLf & vbCrLf & _
                        "Inserts/Deletions are not available.." & vbCrLf & vbCrLf & _
                          "(NB- Some settings are basic to the database Identity, and must not be changed..)", _
                                                                                         MsgBoxStyle.Information)
        Call mbEditRefTable("systemInfo", "SystemValue", 50, False) '--no deletions..-

        '-- refresh our static vars.--
        Call mbLoadAllSystemInfo()
        mlStaffTimeout = 0 '--start timing out..--

    End Sub '--show--
    '= = = = = = = = = = = =  =
    '-===FF->

    '--  update systemInfo --
    '--  update systemInfo --
    Public Sub mnuUpdSysInfo_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles mnuUpdSysInfo.Click
        Dim ans As Short
        Dim ix, kx As Integer
        Dim sKey As String
        Dim sCurrentKey As String
        Dim sValue As String
        Dim s1, sSql As String
        Dim L1 As Integer
        Dim sErrors As String

        ans = MsgBoxResult.Yes
        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        sCurrentKey = "" '--for retry..-
        While ans = MsgBoxResult.Yes '--multiple updates..-
            sValue = ""
            s1 = InputBox("Enter SystemInfo Item key and value as:" & vbCrLf & _
                    "     key = value  ", "Update SystemInfo Item.", sCurrentKey & "=")
            kx = InStr(s1, "=")
            If kx > 1 Then
                sKey = Trim(VB.Left(s1, kx - 1))
                sValue = Trim(Mid(s1, kx + 1))
                '--"businessshortname"--- can be added only for Precise..--
                '----  short dbname=PRECISE--  (no shortName prefix..--
                sCurrentKey = sKey '--for retry..
                If ((LCase(sKey) = "businessshortname") And (LCase(msSqlDbName) = "jobtracking")) Then '--OK.. short dbname=PRECISE--
                    If ((LCase(sValue) = "precise") Or (LCase(sValue) = "precise_pcs")) Then '--OK. short name is precise..-
                    Else
                        MsgBox("Precise PCs shortname must be either ""Precise"" or ""Precise_PCS""..", MsgBoxStyle.Exclamation)
                        sKey = ""
                    End If '--precise-
                ElseIf (LCase(sKey) = "businessusername") Or (LCase(sKey) = "businessabn") Or (LCase(sKey) = "businesspostcode") Or (LCase(sKey) = "businessstate") Or (LCase(sKey) = "businessshortname") Then
                    MsgBox("Can't change that value..", MsgBoxStyle.Exclamation)
                    mlStaffTimeout = 0 '--start timing out..--
                    sKey = "" '--  Exit Sub
                ElseIf ((LCase(sKey) = "labourhourlyrate") Or (LCase(sKey) = "labourmincharge")) Then  '--must be numeric..--
                    If Not IsNumeric(sValue) Then
                        MsgBox("Value must be numeric {eg $$$.cc..", MsgBoxStyle.Exclamation)
                        sKey = "" '
                    End If '--numeric.-
                End If
                If (sKey <> "") Then
                    If (sValue = "") Then
                        If Not MsgBox("This will set the systemInfo item '" & sKey & "' value to the empty string.." & vbCrLf & "Is this the intention?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            sKey = "" '--don't do it..--
                        End If
                    End If
                End If '--key-
                If (sKey <> "") Then '--still going..-
                    sSql = "IF EXISTS (SELECT * FROM [SystemInfo] WHERE (SystemKey='" & sKey & "'))" & vbCrLf
                    sSql = sSql & "    UPDATE [SystemInfo] SET systemValue='" & msFixSqlStr(sValue) & "', " & vbCrLf & "dateUpdated=CURRENT_TIMESTAMP" & " WHERE (SystemKey='" & sKey & "')" & vbCrLf
                    sSql = sSql & "ELSE INSERT INTO [SystemInfo] (SystemKey,systemValue,dateUpdated) " & " VALUES ('" & sKey & "', '" & msFixSqlStr(sValue) & "',CURRENT_TIMESTAMP) " & vbCrLf
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    If Not gbExecuteCmd(mCnnSql, sSql, L1, sErrors) Then
                        MsgBox("!! Failed in UPDATE jobTracking systemInfo.." & vbCrLf & sErrors & vbCrLf, MsgBoxStyle.Critical)
                    Else '--ok--
                        MsgBox("SystemInfo updated ok with: " & sKey & " = " & sValue, MsgBoxStyle.Information)
                    End If
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    sCurrentKey = ""
                End If
            Else
                MsgBox("Invalid entry..", MsgBoxStyle.Exclamation)
            End If
            ans = MsgBox("Do more updates?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo)
        End While '--ans--
        '-- refresh our static vars.--
        Call mbLoadAllSystemInfo()
        mlStaffTimeout = 0 '--start timing out..--

    End Sub '--update--
    '= = = = = = = = = = =
    '-===FF->

    '-- A b o u t --
    '-- A b o u t --

    Private Sub mnuAbout_Click(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs)  '= event disabled..== Handles mnuAbout.Click
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        Call mbDoNotShowMsgDialogue("", "", strAboutJobMatix3HTML, True, True, False, "About JobMatix- " & msJobMatixVersion)

        mlStaffTimeout = 0 '--start timing out..--
    End Sub
    '= = = = = = = = = = =

    Private Sub mnuAboutJobMatix32ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
                                                            Handles AboutJobMatix32ToolStripMenuItem.Click
        mlStaffTimeout = -1 '--SUSPEND timing out..--

        Call mbDoNotShowMsgDialogue("", "", strAboutJobMatix3HTML, True, True, False, "About " & msJobMatixVersion)

        mlStaffTimeout = 0 '--start timing out..--

    End Sub  '- mnuAboutJobMatix32-
    '= = = = = = = = =  = = = = == =  = == = =
    '-===FF->

    '--  Users --
    Public Sub mnuShowUsers_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles mnuShowUsers.Click
        Dim colUsers As Collection
        Dim sMsg As String
        Dim v1 As Object
        Dim col1 As Collection

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        If gbGetUsersEx(mCnnSql, msSqlDbName, colUsers) Then
            If Not (colUsers Is Nothing) Then
                sMsg = "For Database: '" & msSqlDbName & "':" & vbCrLf & _
                    "Current process: " & msCurrentProcessName & vbCrLf & _
                       "-- DB UserNames [LoginName] are:" & vbCrLf & vbCrLf
                For Each col1 In colUsers
                    sMsg = sMsg & col1.Item("USERNAME") & _
                       "  [" & col1.Item("LOGINNAME") & "] Member of: " & col1.Item("GROUPNAME") & vbCrLf
                Next col1 '--col1-
                MsgBox(sMsg, MsgBoxStyle.Information)
            Else
                MsgBox("No users to show..", MsgBoxStyle.Exclamation)
            End If '--nothing..
        Else
            MsgBox("Failed to get users to show..", MsgBoxStyle.Exclamation)
        End If
        mlStaffTimeout = 0 '--start timing out..--
    End Sub '--show users..-
    '= = = = = = = = = = = =  =
    '= = = = = = = = = = =
    '-===FF->

    '--  add Windows User Logon to SQL logins..-
    '--  add Windows User Logon to SQL logins..-

    '=-- 1. Create SQL Login if it doesn't exist..
    '=-- 2. Add security account to our DB. (gbGrantDBAccess)..
    '=-- 3. Add db_owner role to the security account in our DB. (gbAddRoleMember)..
    '=-- 4. Add VIEW SERVER STATE permission the new Login.. (for "who_using")..

    Public Sub mnuAddNewUser_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles mnuAddNewUser.Click

        Const k_getLoginName As String = "Enter the Windows Username to be given access to JobTracking Database- " & vbCrLf & _
                                     vbCrLf & "(NB: User must exist as Windows user on the Server machine, " & _
                                                                          "or in the server domain.)" & vbCrLf & vbCrLf & _
                                             "Enter name as 'Domain\Username' :" & vbCrLf & "     eg: << JobsServer\Fred >>  "
        Dim sLoginName, s1 As String
        Dim ans As Short
        Dim sResult, sErrorResult As String
        Dim bOk As Boolean
        Dim bHasAccess, bIsDbOwner As Boolean
        Dim colUserNames As Collection
        Dim col1 As Collection
        Dim v1 As Object

        '-- load/show user details form..--
        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        '== GoSub NewUser_getLoginName
        sLoginName = InputBox(k_getLoginName, "Add Windows User to SQL Server logins..", msSqlServerComputer & "\")
        If (sLoginName = "") Then
            mlStaffTimeout = 0 '--start timing out..--
            Exit Sub
        End If
        sErrorResult = ""
        '-- ok.. first check if server user and login exists..-
        sResult = ""
        '==bOk = True
        bOk = False : ans = MsgBoxResult.Yes

        '==3073.311=  NEW ==
        '==3073.311=  NEW ==
        '-- 'gbAddNewUser' does GrantLogin and DB Access.--
        While (Not bOk) And (ans = MsgBoxResult.Yes)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            bOk = gbAddNewUser(mCnnSql, msSqlDbName, sLoginName, sResult, sErrorResult)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not bOk Then  '--retry ?--
                ans = MsgBox("Failed to add new Login '" & sLoginName & "' .." & vbCrLf & vbCrLf & _
                             sErrorResult & vbCrLf & vbCrLf & _
                             "Do you want to retry?", _
                               MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question)
                If (ans <> MsgBoxResult.Yes) Then
                    mlStaffTimeout = 0 '--start timing out..--
                    Exit Sub
                Else '--retry.-
                    sLoginName = InputBox(k_getLoginName, "Add Windows User to SQL Server logins..")
                    If (sLoginName = "") Then
                        mlStaffTimeout = 0 '--start timing out..--
                        Exit Sub
                    End If
                End If
            End If  '--ok--
        End While
        If bOk Then
            If (sErrorResult <> "") Then
                MsgBox("Done with errors:" & vbCrLf & _
                                   sResult & vbCrLf & sErrorResult & vbCrLf, MsgBoxStyle.Information)
            Else  '--done ok..
                MsgBox("Done:" & vbCrLf & sResult, MsgBoxStyle.Information)
            End If
        Else  '-not ggood..-
            MsgBox("Errors were encountered:" & vbCrLf & vbCrLf & _
                      sResult & vbCrLf & "ERRORS:" & vbCrLf & sErrorResult & vbCrLf, MsgBoxStyle.Exclamation)
        End If '--ok-
        '--End If  '--add-

        '--3077.519- must set current DB back to Jobmatix..-
        If Not gbSetCurrentDatabase(mCnnSql, msSqlDbName) Then
            s1 = "AddNewUser- Failed USE for: " & msSqlDbName & ".."
            MsgBox(s1 & vbCrLf, MsgBoxStyle.Exclamation)
            '==msLastSqlErrorMessage = s1
        End If
        '-End If
        mlStaffTimeout = 0 '--start timing out..--
        Exit Sub

    End Sub '--add user..-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- Database menu --
    '-- Database menu --
    '-- Database menu --

    '-- User Menu to Backup..--
    '-- User Menu to Backup..--

    Private Sub mnuBackupJobTrackingDB_Click_1(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles mnuBackupJobTrackingDB.Click

        '== Call mnuBackupJobsDB_Click(mnuBackupJobTrackingDB, New System.EventArgs())
        Call mbBackupJobsDB()
    End Sub  '-- backup..--
    '= = = = = = = = = = = = =

    '--show slq server version--

    Private Sub mnuSqlServer_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles mnuSqlServer.Click
        Dim sMsg As String

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        sMsg = "Sql Server version: " & msSqlVersion
        sMsg &= vbCrLf & msServer & " (Database: " & msSqlDbName & ")."
        sMsg &= vbCrLf & vbCrLf & "Login: " & msSqlServerComputer & "\" & msCurrentUserName

        MsgBox(sMsg, MsgBoxStyle.Information, "JobMatix Sql Server Info")
        mlStaffTimeout = 0 '--RESTART timing out..--

    End Sub  '--mnuSqlServer--
    '= = = = = = = = = = = = = =

    Private Sub mnuWhoUsing_Click(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles mnuWhoUsing.Click
        Dim strUserList As String
        Dim col1 As Collection

        mlStaffTimeout = -1 '--SUSPEND timing out..--
        Call mbShowLoggedInUsers(col1, strUserList)
        MsgBox(strUserList & vbCrLf & vbCrLf & vbCrLf & _
               "Current process is: " & msCurrentProcessName & vbCrLf, _
                  MsgBoxStyle.Information, "Who's using JobMatix")
        mlStaffTimeout = 0 '--RESTART timing out..--

    End Sub '--whoUsing.-
    '= = = = = = = = = = = = 

    '-- From Database menu..-
    Private Sub mnuDbRetailManager_Click(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles mnuDbRetailManager.Click
        If mbIsRetailManager() Then
            Call mbMenuRetailManagerDB()
        ElseIf mbIsJobmatixPOS() Then
            MsgBox("Retail system DB is JobmatixPOS- " & vbCrLf & _
                     " (Part of Jobmatix Database)", MsgBoxStyle.Information)
        End If
    End Sub '--db Retail manager--
    '= = = = = = = = = = = = = = = =  =
    '-===FF->

    '--F i l e M e n u--
    '--F i l e M e n u--
    '--F i l e M e n u--
    '--F i l e M e n u--

    '--  Printers..--

    '--  All Printer Assignments --
    Private Sub mnuPrinterAssignments_Click(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles mnuPrinterAssignments.Click
        Call mbSelectPrinter()
    End Sub  '-PrinterAssignments-
    '= = = = = = = = = = 


    '-- preferences..--

    '-- Staff Sign Off preferences..

    '--  Flip status..--

    'Public Sub mnuStaySignedOn_Click(ByVal eventSender As System.Object, _
    '                                      ByVal eventArgs As System.EventArgs) Handles mnuAutoSignOffOptions.Click

    '    If mnuAutoSignOffOptions.Checked = True Then
    '        mnuAutoSignOffOptions.Checked = False
    '    Else
    '        mnuAutoSignOffOptions.Checked = True
    '    End If
    'End Sub '--stay signed on..-
    '= = = = ==  = = == ==== =

    '-- THESE three are all mutually exclusve..

    '-mnuStaySignedOn-

    Private Sub mnuAutoSignOffOptions_Click(sender As Object, e As EventArgs) _
                                       Handles mnuStaySignedOn.Click, mnuLongSignOff.Click, mnuShortSignOff.Click

        Dim item1 As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

        If (Not item1.Checked) Then
            '--uncheck the others, check this..
            For Each itemX As ToolStripMenuItem In mnuAutoSignOffOptions.DropDownItems
                If itemX.Name <> item1.Name Then
                    itemX.Checked = False
                End If
            Next itemX
            item1.Checked = True
        Else
            '==  clicking item already checked.. (ignore)-
            Exit Sub
        End If
        '- update stuff-
        Dim sNewSettingsvalue As String = "LongTimeout"  '-default-
        mIntStaffTimeoutInterval = k_StaffTimeoutInterval_long  '-DEFAULT-
        '= set correct timeout..
        If mnuStaySignedOn.Checked Then
            sNewSettingsvalue = "StaySignedOn"
            labStaffTimeRemaining.Text = ""
        ElseIf mnuLongSignOff.Checked Then
            mIntStaffTimeoutInterval = k_StaffTimeoutInterval_long  '-secs-
            sNewSettingsvalue = "LongTimeout"
        ElseIf mnuShortSignOff.Checked Then
            mIntStaffTimeoutInterval = k_StaffTimeoutInterval_short   '-30 secs-
            sNewSettingsvalue = "ShortTimeout"
        End If
        '- save new setting..
        If Not mLocalSettings1.SaveSetting(k_AutoSignOffSettingsName, sNewSettingsvalue) Then
            MsgBox("Failed to save AutoSignOff setting.", MsgBoxStyle.Information)
        End If

    End Sub  '-mnuStaySignedOn=
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--  Flip status..--

    Public Sub mnuDontShowNotifyReminder_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuDontShowNotifyReminder.Click

        If mnuDontShowNotifyReminder.Checked = True Then
            mnuDontShowNotifyReminder.Checked = False
        Else
            mnuDontShowNotifyReminder.Checked = True
        End If
    End Sub '--stay signed on..-
    '= = = = ==  = = == ==== =


    '--  Flip status..--

    Public Sub mnuStartupMaximised_Click(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles mnuStartupMaximised.Click
        Dim sNewValue As String

        If mnuStartupMaximised.Checked = True Then
            mnuStartupMaximised.Checked = False
        Else
            mnuStartupMaximised.Checked = True
        End If
        sNewValue = IIf(mnuStartupMaximised.Checked, "Y", "N")
        Call mbSaveSetting(K_STARTUPFULLSCREEN_SETTINGNAME, sNewValue)

    End Sub '--stay signed on..-
    '= = = = ==  = = == ==== =
    '-===FF->

    '-- preferences..--
    '--- 3037.1  ====
    '-- change grid fonts..--

    Private Sub mnuGridFont_8_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) _
                         Handles mnuGridFont_8.Click, mnuGridFont_9.Click, mnuGridFont_10.Click
        Dim L1 As Integer
        Dim menu1 As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

        On Error GoTo GridFont_error

        If IsNumeric(menu1.Tag) Then
            Call mbSetGridFontSize(CInt(menu1.Tag))

            '--  asve new setting.-
            Call mbSaveSetting(K_GRIDFONTSIZE_SETTINGNAME, menu1.Tag)

        End If  '--numeric--
        Exit Sub

GridFont_error:
        L1 = Err().Number
        MsgBox("Runtime Error in GridFont_click sub..  [" & menu1.Name & "]" & vbCrLf & _
                          "Error is " & L1 & " = " & ErrorToString(L1) & vbCrLf & vbCrLf & _
                             "Font may not be changed..", MsgBoxStyle.Exclamation)
    End Sub '-- Grid font..--
    '= = = = = = = = = = = =
    '-===FF->

    '--  Help --
    '--  Call HTML Help API..-

    Private Sub cmdHelp_Click()

        Call HH_DISPLAY_Click(Me.Handle.ToInt32, "JT2-MainScreen.htm")

    End Sub
    '= = = = = = =
    '-===FF->

    '-- Retail manager DB..--

    Private Function mbMenuRetailManagerDB() As Boolean
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        mbMenuRetailManagerDB = False
        If MsgBox("The current RetailManager database is: " & vbCrLf & vbCrLf & msJetDbName & vbCrLf & vbCrLf & _
                    "Do you want to close this database, " & vbCrLf & _
                  " and Browse for a different RM database?", _
                           MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '== mCnnJet.Close
            msJetDbName = ""  '--force browse..
            If msProviderCode = "RM" Then
                labStatus.Text = "Connecting to RetailManager database: " & msJetDbName
            ElseIf (msProviderCode = "QBPOS") Then
                labStatus.Text = "Connecting to QBPOS database: " & msJetDbName
            End If  '--provider- 
            '-- Connect to Retail Host Jet Database --
            '--  force new path.
            If Not mbRMConnect(msProviderCode, True) Then
                '-  Retail host failed to connect, and didn't get alternate path..--
                MsgBox("JobMatix can't continue without RetailManager DB Connection.", MsgBoxStyle.Exclamation)
                '==Me.Hide
                Me.Close() '=End
                Exit Function
            End If
            labStatus.Text = "Logging all DB schema info..."
            labStatus.Text = "  DB connections complete.."
            '==3083== txtJetDBName.Text = msJetDbName
            labStatus.Text = "Done.."
            mbMenuRetailManagerDB = True
        End If '--yes..-
        mlStaffTimeout = 0 '--RESTART timing out..--
    End Function

    '=3083==
    '--RetailManager database--

    Private Sub mnuFileRetailManagerDb_Click(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) _
                                                     Handles mnuFileRetailManagerDb.Click
        If mbIsRetailManager() Then
            Call mbMenuRetailManagerDB()
        ElseIf mbIsJobmatixPOS() Then
            MsgBox("Retail system DB is JobmatixPOS- " & vbCrLf & _
                     " (Part of Jobmatix Database)", MsgBoxStyle.Information)
        End If
    End Sub  '--RM db --
    '= = = = = =  = =  =
    '-===FF->

    '---Exit--
    Public Sub mnuExit_Click(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles mnuExit.Click
        '== Call cmdClose_Click(cmdClose, New System.EventArgs())
        '== mCnnSql.Close()
        '==GONE= mCnnShape.Close()
        '== mRetailHost1.closeConnection()
        Me.Close()
    End Sub '--exit--
    '= = = = = = = = = = = =
    '-===FF->

    '--  =3083=
    '--  Jobs TreeView Context menu stuff--

    '-- menu click events..-
    '--   Setup function-
    '-- menu click events..-

    Private Function mbJobActionSetup(ByVal sCmd As String, _
                                       ByRef nodeSelectedJob As TreeNode, _
                                        ByRef intJobNo As Integer) As Boolean
        Dim s1 As String = ""
        Dim node1 As TreeNode
        Dim col1 As Collection

        mbJobActionSetup = False
        If Not (mNodeJobsTreeContextMenuSelected Is Nothing) Then
            node1 = mNodeJobsTreeContextMenuSelected
            '-- set job node as SELECTED.--
            tvwJobs.SelectedNode = mNodeJobsTreeContextMenuSelected
            col1 = node1.Tag
            If Not (col1 Is Nothing) Then
                s1 = col1("JobNo")
                '-- show job..--
                If IsNumeric(s1) Then
                    Call mbShowJobInfoRTF(CLng(s1))
                End If  '--numeric-
                '== MsgBox(sCmd & "JobNo: " & s1 & vbCrLf & node1.Text)
                '-- USER calls action command--
                nodeSelectedJob = mNodeJobsTreeContextMenuSelected
                intJobNo = CLng(s1)
                mbJobActionSetup = True
            End If  '--nothing-
        ElseIf (mIntOnsiteJobNoContextMenuSelected > 0) Then  '-onsite context menu-
            '= Call mbShowJobInfoRTF(mIntOnsiteJobNoContextMenuSelected)
            mbJobActionSetup = True
        End If  '- node nothing-
    End Function  '--mbJobActionSetup--
    '= = = = = = = = = = = = = = = =
    '= = = = = = = = = = = =
    '-===FF->

    '-- menu click events..-
    '-- menu click events..-
    '-- menu click events..-

    '-- Check-In..-
    Public Sub mnuJobActionCheckIn_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionCheckIn.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("CheckIn", node1, intJobNo) Then
            '--  call action command--
            Call mbAmendAgreement(True)
        End If
    End Sub '--check-in.-
    '= = = = = = = = = = = =

    '-- Amend..-
    Public Sub mnuJobActionAmend_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionAmend.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("Amend", node1, intJobNo) Then
            '--  call action command--
            Call mbAmendAgreement(False)
        End If
    End Sub '--Amend.-
    '= = = = = = = = = = = =

    '-- Start..-
    Public Sub mnuJobActionStart_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionStart.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("Start", node1, intJobNo) Then
            '--  call action command--
            Call cmdStart_Click() '===(index)
        End If
    End Sub '--Start.
    '= = = = = = = = = = = =

    '-- Update --

    Public Sub mnuJobActionUpdate_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionUpdate.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("Update", node1, intJobNo) Then
            '--  call action command--
            Call cmdStart_Click() '===(index)
        End If
    End Sub '--Update.
    '= = = = = = = = = = = =
    '-===FF->

    '-- SEND job back..-
    '-- to WAITLIST--

    Public Sub mnuJobActionReturnToWaitlist_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) _
                                                 Handles mnuJobActionReturnToWaitlist.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("ReturnToWaitList", node1, intJobNo) Then
            Call mbJobActionReturnToQueue(True)  '- TRUE is To WaitList
        End If
    End Sub '--Return toWaitlist..
    '= = = = = = = = = = = = = = = = = = = = = =

    '-- SEND job back..-
    '-- to   Input QUEUE--

    Public Sub mnuJobActionReturnToQueue_Click(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) _
                                                  Handles mnuJobActionReturnToQueue.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("ReturnToQueue", node1, intJobNo) Then
            Call mbJobActionReturnToQueue()
        End If
    End Sub '--Return to Queue..
    '= = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '== 3323.1109=  TRANSFER Job to new (different) Customer-

    Public Sub mnuJobActionTfrToNewCust_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) _
                                              Handles mnuJobActionTfrToNewCust.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer
        Dim s1, s2, s3, s4, sStatus As String
        Dim intOldCust_id, intNewCust_id, lngaffected As Integer
        Dim sOldCustbarcode, sNewCustBarcode As String
        Dim sOldCustFullName, sNewCustCompany, sNewCustName, sNewCustFullName As String
        Dim sNewCustPhone, sNewCustMobile As String

        If mbJobActionSetup("TfrToNewCustomer", node1, intJobNo) Then
            '-==Node is now selected..

            mlStaffTimeout = -1 '--SUSPEND timing out..--
            '- get status and current customer..
            If Not mbGetSelectedJobNo(intJobNo, sStatus) Then
                mlStaffTimeout = 0 '--NOW is timing out..--
                Exit Sub
            End If
            '- mColJobFields NOW has job record..-
            '==sStatus = "" & ColJobFields("JobStatus")("value")
            If (VB.Left(sStatus, 2) = "33") Or (VB.Left(sStatus, 2) = "43") Then
                MsgBox("That Job Record is in use.." & vbCrLf & "Check with other users..", MsgBoxStyle.Exclamation)
                mlStaffTimeout = 0 '--NOW is timing out..--
                Exit Sub
            End If
            intOldCust_id = CInt(mColJobFields.Item("RMCustomer_Id")("value"))
            sOldCustbarcode = mColJobFields.Item("CustomerBarcode")("value")
            '==3043.0=  get customer details from Retailmnager..--
            sOldCustFullName = mColJobFields.Item("CustomerName")("value")
            s1 = Trim(mColJobFields.Item("CustomerCompany")("value"))
            If (s1 <> "") And (s1 <> "--") Then  '-have company-
                sOldCustFullName = s1 & " +" & sOldCustFullName
            End If
            If MsgBox("Job " & intJobNo & " is currently assigned to: " & vbCrLf & _
                         sOldCustFullName & ".." & vbCrLf & vbCrLf & _
                      "Please Select new Customer to transfer Job " & intJobNo & "  to..", _
                               MsgBoxStyle.OkCancel + MsgBoxStyle.Information) = MsgBoxResult.Ok Then
                '--customer lookup (ext. browse)..---
                Dim colSelectedRow As Collection
                Dim colFields As Collection
                Dim sName, sValue, sNewNote, sSql, sErrorMsg As String

                If mRetailHost1.customerLookup(colSelectedRow) Then '--found..--
                    '==  Call mbSetupCustomer(colRecord)
                    If (colSelectedRow.Count > 0) Then
                        sName = CStr(colSelectedRow.Item(1)("name"))
                        sValue = CStr(colSelectedRow.Item(1)("value"))
                        If Not mRetailHost1.customerGetCustomerRecordEx(colSelectedRow, colFields) Then
                            MsgBox("Failed to retrieve customer record ( " & sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
                            mlStaffTimeout = 0 '--NOW is timing out..--
                            Exit Sub
                        Else '--ok--
                            '--set up customer details.-
                            '==Call mbSetupCustomer(colRecord)
                            intNewCust_id = CInt(colFields.Item("customer_id")("value"))
                            sNewCustBarcode = colFields.Item("barcode")("value")
                            If (intNewCust_id = intOldCust_id) Then
                                MsgBox("Can't transfer to same customer:  B/Code: " & sNewCustBarcode & vbCrLf, MsgBoxStyle.Exclamation)
                                mlStaffTimeout = 0 '--NOW is timing out..--
                                Exit Sub
                            End If
                            sNewCustCompany = colFields.Item("company")("value")
                            s3 = colFields.Item("given_names")("value")
                            s4 = colFields.Item("surname")("value")
                            sNewCustPhone = colFields.Item("phone")("value")
                            If Len(sNewCustPhone) > 20 Then
                                sNewCustPhone = VB.Left(Replace(sNewCustPhone, " ", ""), 20)
                            End If
                            sNewCustMobile = colFields.Item("mobile")("value")
                            If Len(sNewCustMobile) > 20 Then
                                sNewCustMobile = VB.Left(Replace(sNewCustMobile, " ", ""), 20)
                            End If
                            sNewCustName = s4 '--max 50--SURNAME, GivenNames --
                            If (sNewCustName <> "") And (s3 <> "") Then
                                sNewCustName = sNewCustName & ", "
                            End If
                            sNewCustName = VB.Left(UCase(sNewCustName) & s3, 50) '--max 50--SURNAME, GivenNames --
                            '-- make combined name for Service Notes..
                            sNewCustFullName = sNewCustName
                            If (sNewCustCompany <> "") Then
                                sNewCustFullName = sNewCustCompany & " +" & sNewCustFullName
                            End If
                            '- make note for service record..
                            sNewNote = vbCrLf & "** $$CUST-TFR TO: #" & sNewCustBarcode & " " & msFixSqlStr(sNewCustFullName) & _
                                              "; FROM: #" & sOldCustbarcode & " " & msFixSqlStr(sOldCustFullName) & _
                                              "; by " & msFixSqlStr(msStaffName) & "; " & Format(Now, "dd-MMM-yyyy hh:mm tt") & vbCrLf
                            If (sNewCustCompany = "") Then '--no company name..--
                                sNewCustCompany = "--" '--"n/a" '==  don't want blank company..--
                            End If
                            '-- update Job record..-
                            sSql = "UPDATE [Jobs] SET "
                            sSql &= " CustomerBarcode='" & msFixSqlStr(sNewCustBarcode) & "'"
                            sSql &= ", RMCustomer_Id=" & CStr(intNewCust_id)
                            sSql &= ", CustomerCompany='" & msFixSqlStr(sNewCustCompany) & "'"
                            sSql &= ", CustomerName='" & msFixSqlStr(sNewCustName) & "'"
                            sSql &= ", CustomerPhone='" & msFixSqlStr(sNewCustPhone) & "'"
                            sSql &= ", CustomerMobile='" & msFixSqlStr(sNewCustMobile) & "'"
                            sSql &= ", ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',4000) "
                            '= sSql = sSql & ", TechStaffName='" & msFixSqlStr(msStaffName) & "'"
                            sSql &= ", DateUpdated= CURRENT_TIMESTAMP "
                            sSql &= "  WHERE Job_Id=" & CStr(intJobNo) & ";"
                            If MsgBox("Sure you want to TRANSFER Job No: " & intJobNo & " to the new Customer:" & _
                                         vbCrLf & vbCrLf & sNewCustFullName, _
                                          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                                If Not gbExecuteCmd(mCnnSql, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                                    MsgBox("ERROR- Failed SQL UPDATE:" & vbCrLf & "SQL was:" & _
                                                vbCrLf & sSql & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
                                Else '--ok-
                                    '= If gbDebug Then
                                    MsgBox("OK.. Job " & intJobNo & " was updated with New Customer:" & _
                                               vbCrLf & sNewCustFullName & vbCrLf & vbCrLf & _
                                               "  and " & lngaffected & " record(s) were affected..", MsgBoxStyle.Information)
                                    '= End If
                                End If '-exec..-
                            End If '--yes.-
                        End If  '-got record-
                    Else
                        If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                        mlStaffTimeout = 0 '--NOW is timing out..--
                        Exit Sub
                    End If '--got row..--
                End If '--lookup.-
                Call mbRefreshJobsTreeView(True) '--initialise.--
                If (mlJobId > 0) Then Call mbShowJobInfoRTF(mlJobId)
            End If  '=ok-
            mlStaffTimeout = 0 '--NOW is timing out..--
        End If '-job action-

    End Sub '-Tfr..
    '= = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Re-open--

    Public Sub mnuJobActionReopen_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles mnuJobActionReOpen.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("Reopen", node1, intJobNo) Then
            '--  call action command--
            Call cmdReOpen_Click()
        End If
    End Sub '--reopen.
    '= = = = = = = = = = = =

    '-- Deliver --
    '-- Deliver --

    'Public Sub mnuJobActionDeliver_Click(ByVal eventSender As System.Object, _
    '                                        ByVal eventArgs As System.EventArgs) Handles mnuJobActionDeliver.Click
    '    Dim node1 As TreeNode
    '    Dim intJobNo As Integer

    '    If mbJobActionSetup("Deliver", node1, intJobNo) Then

    '        '--=3101.117=
    '        '- only call if NOT JobMatixPOS..
    '        '--  else show msg diverting user to POS Sales Tab..
    '        If mbIsJobmatixPOS() Then
    '            MsgBox("Please Note: Job Delivery in this system is done in the Sales Screen-" & vbCrLf & _
    '                    " Please go to the POS Sales Tab, select the customer, and select 'Sales'..  " & vbCrLf & vbCrLf & _
    '                    "Job details will be inserted in the Sales grid ready for payment.", MsgBoxStyle.Information)
    '            Exit Sub
    '        End If

    '        '--Not JobmatixPOS-  call action command--
    '        Call cmdDeliver_Click()
    '    End If
    'End Sub '--Deliver.
    '= = = = = = = = = = = =
    '-===FF->

    '-- Deliver --
    '-- Deliver --
    '=3501.0625=  Rescued from- 3431.0622= -- 
    '=  Deliver Job from JobsTree Context menu.. 
    '==  Add Option to Expedite mark as Delivered..
    '==3519.0414= make it OPTIONAL !.

    Public Sub mnuJobActionDeliver_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionDeliver.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer
        Dim sWhere As String
        Dim sSql As String
        Dim sMsg, sErrorMsg As String
        Dim lngaffected As Integer

        If mbJobActionSetup("Deliver", node1, intJobNo) Then
            mlStaffTimeout = -1 '--SUSPEND timing out..--
            '--=3101.117=
            '- only call if NOT JobMatixPOS..
            '--  else show msg diverting user to POS Sales Tab..
            If mbIsJobmatixPOS() Then
                sMsg = "Please Note: " & vbCrLf & _
                        " Job Delivery in this system should be done in the POS Sales Screen-" & vbCrLf & _
                        " Please go to the POS Sales Tab, select the customer, Job, and then 'Sale'..  " & vbCrLf & vbCrLf & _
                        "  -- Job details will be then inserted in the Sales grid ready for payment."
                If (MessageBox.Show(sMsg & vbCrLf & vbCrLf & _
                                   "Are you sure you want to deliver this Job now, without a sales invoice?", _
                                   "Immediate Job Delivery", MessageBoxButtons.YesNo, _
                                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes) Then
                    mlStaffTimeout = 0 '--NOW is timing out..--
                    Exit Sub
                End If '-show-
                'MsgBox("Please Note: Job Delivery in this system is done in the Sales Screen-" & vbCrLf & _
                '        " Please go to the POS Sales Tab, select the customer, and select 'Sales'..  " & vbCrLf & vbCrLf & _
                '        "Job details will be inserted in the Sales grid ready for payment.", MsgBoxStyle.Information)
            End If
            '--Not JobmatixPOS-  call action command--
            '-- Check if Quick delivery wanted..
            If MessageBox.Show("Quick Delivery update (no printing) ?", _
                               "JobMatix Job Delivery.", _
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                '- mark as delivered..-
                sSql = "UPDATE [Jobs] SET "
                sSql &= " JobStatus='70-Delivered', "
                sSql &= " DeliveredStaffName='" & msFixSqlStr(msStaffName) & "', "
                sSql &= " DeliveredRMStaff_id=" & CStr(mlStaffId) & ", "
                sSql &= " DateDelivered= CURRENT_TIMESTAMP, "
                sSql &= " DateUpdated= CURRENT_TIMESTAMP "
                sSql &= "   WHERE Job_Id=" & CStr(intJobNo) & ";"
                If Not gbExecuteCmd(mCnnSql, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                    MsgBox("ERROR- Failed SQL UPDATE: SQL was:" & vbCrLf & _
                                sSql & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                    mlStaffTimeout = 0 '--NOW is timing out..--
                    Exit Sub
                Else '--ok-
                    '= MsgBox("OK.. Job " & intJobNo & " status changed to '70-Delivered'.." & vbCrLf & _
                    '=                             "  and " & lngaffected & " records were affected..", MsgBoxStyle.Information)
                End If '-exec..-
                Call mbRefreshJobsTreeView() '--initialise.--
            Else
                '--Call Maint form.
                Call cmdDeliver_Click()
            End If '-yes-
            mlStaffTimeout = 0 '--NOW is timing out..--
        End If '-setup-
    End Sub '--Deliver.
    '= = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->


    '-- Notify --

    Public Sub mnuJobActionNotify_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionNotify.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("Notify", node1, intJobNo) Then
            '--  call action command--
            Call cmdDetailNotify_Click()
        End If
    End Sub '--Notify.
    '= = = = = = = = = = = =
    '-===FF->

    '-- Stop Press --

    Public Sub mnuJobActionStopPress_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionStopPress.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("StopPress", node1, intJobNo) Then
            '--  call action command--
            Call cmdStopPress_Click()
        End If
    End Sub '--StopPress.
    '= = = = = = = = = = = =

    '=3203.219= -- Clear Alert..

    Public Sub mnuJobActionClearAlert_Click(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) Handles mnuJobActionClearAlert.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer
        Dim sSql As String
        Dim sErrorMsg As String
        Dim L1, lngaffected As Integer

        If mbJobActionSetup("ClearAlert", node1, intJobNo) Then
            '== Do it here-
            mlJobId = intJobNo  '-make sure it's current..
            mlStaffTimeout = -1 '--SUSPEND timing out..--
            sSql = "UPDATE [Jobs] SET "
            sSql = sSql & " ServiceNotes=REPLACE(ServiceNotes, '** ALERT!','--') "
            sSql = sSql & ", DateUpdated= CURRENT_TIMESTAMP "
            sSql = sSql & "  WHERE Job_Id=" & CStr(intJobNo) & ";"
            If Not gbExecuteCmd(mCnnSql, sSql & vbCrLf, lngaffected, sErrorMsg) Then
                MsgBox("ERROR- Failed SQL UPDATE:" & vbCrLf & "SQL was:" & vbCrLf & _
                                         sSql & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
            Else '--ok-
                MsgBox("OK.. Alert on Job No " & intJobNo & " has been cleared.", _
                                            MsgBoxStyle.Information, "Ghost Busters are go.")
            End If '-exec..-
            '=3311.817= Stop doing This!=  Call mbJobsBrowseRefresh()  '==3072/3==
            Call mbRefreshJobsTreeView() '--initialise.--
            If (mlJobId > 0) Then Call mbShowJobInfoRTF(mlJobId)
            mlStaffTimeout = 0 '--NOW is timing out..--
        End If  '--setup
    End Sub '--Clear Alert.
    '= = = = = = = = = = = =

    '-- View --

    Public Sub mnuJobActionView_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionView.Click
        Dim node1 As TreeNode
        Dim intJobNo As Integer

        If mbJobActionSetup("View", node1, intJobNo) Then
            '--  call action command--
            Call cmdViewRecord_Click() '--view job record..
        End If
    End Sub '--View.
    '= = = = = = = = = == 
    '-===FF->

    '-- SET Context menu options according to Action List..

    Private Sub mSubSetContextmenuOptions(ByVal sActionList As String)
        Dim sActions As String = sActionList
        Dim item1 As MenuItem

        For Each item1 In mContextMenuJobsTreeNodeAction.MenuItems
            If item1.Text <> "-" Then
                item1.Enabled = False
            End If
        Next
        If (InStr(sActions, "checkin;") > 0) Then
            mnuJobActionCheckIn.Enabled = True
        End If
        If (InStr(sActions, "amend;") > 0) Then
            mnuJobActionAmend.Enabled = True
        End If
        If (InStr(sActions, "returntowaitlist;") > 0) Then
            mnuJobActionReturnToWaitlist.Enabled = True
        End If
        '==3323.1109=  tfrToNewCustomer;--
        If (InStr(sActions, "tfrtonewcustomer;") > 0) Then
            mnuJobActionTfrToNewCust.Enabled = True
        End If

        If (InStr(sActions, "start;") > 0) Then
            mnuJobActionStart.Enabled = True
        End If
        If (InStr(sActions, "update;") > 0) Then
            mnuJobActionUpdate.Enabled = True
        End If
        '=3203.124=
        If (InStr(sActions, "returntoqueue;") > 0) Then
            mnuJobActionReturnToQueue.Enabled = True
        End If
        If (InStr(sActions, "reopen;") > 0) Then
            mnuJobActionReOpen.Enabled = True
        End If
        If (InStr(sActions, "deliver;") > 0) Then
            mnuJobActionDeliver.Enabled = True
        End If
        If (InStr(sActions, "notify;") > 0) Then
            mnuJobActionNotify.Enabled = True
        End If
        If (InStr(sActions, "stoppress;") > 0) Then
            mnuJobActionStopPress.Enabled = True
        End If
        '=3203.219=
        If (InStr(sActions, "clearalert;") > 0) Then
            mnuJobActionClearAlert.Enabled = True
        End If
        '=3311.422=
        If (InStr(sActions, "view;") > 0) Then
            mnuJobActionView.Enabled = True
        End If
        '-- View ok Anyway ?? -

    End Sub  '-set options-
    '= = = = = = = = = == 
    '-===FF->


    '--  =3083=
    '--  Jobs TreeView Context menu stuff--

    '--mouse activity---
    '-- Popup Node Job Action menu..--
    '-- Popup Node Job Action menu..--

    '==3203.124= Add"ReturnToQueue" Action UNLESS  "33-InProcess" '--locked during jobMaint..-

    Private Sub tvwJobs_MouseDown(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.MouseEventArgs) _
                                     Handles tvwJobs.MouseDown

        Dim iButton As Short = eventArgs.Button \ &H100000
        Dim iShift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        Dim lRow, ix, lCol As Integer
        Dim sName, sKey, sActions As String
        Dim j, i, k As Integer
        Dim index As Short
        Dim node1 As TreeNode
        Dim p1 = New Point(eventArgs.X, eventArgs.Y)
        Dim col1 As Collection
        Dim s1 As String
        Dim item1 As MenuItem

        node1 = tvwJobs.GetNodeAt(p1)
        If Not (node1 Is Nothing) Then '--item selected.. -
            If iButton = 1 Then '--left --
                '-- MsgBox "Left Mouse clicked on row: " & index & "..", vbInformation
            ElseIf iButton = 2 Then  '--right..-
                '== MessageBox.Show(node1.Text)  '--TEST--
                mlStaffTimeout = -1 '--SUSPEND timing out..--
                '--  check if job node.-
                sKey = LCase(node1.Name)
                If (LCase(VB.Left(sKey, 3)) <> "job") Then
                    mlStaffTimeout = 0 '--NOW is timing out..--
                    Exit Sub  '--no menu for parent nodes..
                End If  '--job-
                '-- Avoid the 'disabled' gray text by locking updates
                LockWindowUpdate(tvwJobs.Handle.ToInt32)
                '---- A disabled TextBox will not display a context menu
                tvwJobs.Enabled = False
                '--- Give the previous line time to complete
                System.Windows.Forms.Application.DoEvents()
                '-- Display our own context menu
                mNodeJobsTreeContextMenuSelected = node1 '--pass item to mnu routine..-
                '-- set job node as SELECTED.--
                tvwJobs.SelectedNode = mNodeJobsTreeContextMenuSelected
                Application.DoEvents()   '----let it paint..-

                '--  enable/disable menu items according to Job Status..
                col1 = node1.Tag
                If Not (col1 Is Nothing) Then
                    s1 = col1("JobNo")
                    '-- show job..--
                    If IsNumeric(s1) Then
                        Call mbShowJobInfoRTF(CLng(s1))
                    End If  '--numeric-
                    sActions = col1("ActionList")
                    '== MsgBox("Amend:    " & "JobNo: " & sActions & vbCrLf & node1.Text)
                    '-- first disable all..-
                    Call mSubSetContextmenuOptions(sActions)
                End If  '--nothing-
                mContextMenuJobsTreeNodeAction.Show(CType(eventSender, Control), p1)
                ' Enable the control again
                tvwJobs.Enabled = True
                '-- Unlock updates
                LockWindowUpdate(0)
                '--End If  '--yes..-
            End If '--button..-
        End If '--nothing--
        mlStaffTimeout = 0 '--NOW is timing out..--
    End Sub '--mouse..-
    '= = = = = = = = =  =
    '-===FF->

    '-- choose actions for Grid click..

    Private Function msJobActionList(ByVal sJobStatus As String) As String
        Dim sActionList As String = ""

        '-- select permitted actions.
        If (VB.Left(sJobStatus, 2) <= "10") Then '--created..-
            '= nodeY = tvwJobs.Nodes.Find("Queued", True)(0).Nodes.Add(sKey, sCaption)
            If (VB.Left(sJobStatus, 2) = "05") Then '--waitlisted..-
                sActionList = "checkin;amend;tfrtonewcustomer;"  '--=3323=
            Else   '-- status 10-
                sActionList = "returntowaitlist;amend;start;tfrtonewcustomer;"  '--=3083= 3203.211--
            End If
        ElseIf (VB.Left(sJobStatus, 2) >= "20") And (VB.Left(sJobStatus, 2) < "30") Then  '--Suspended....--
            sActionList = "amend;update;tfrtonewcustomer;"  '--=3083=
        ElseIf (VB.Left(sJobStatus, 2) > "10") And (VB.Left(sJobStatus, 2) <= "33") Then  '--started, not completed..--
            sActionList = "amend;update;returntoqueue;tfrtonewcustomer;"  '--=3083=
        ElseIf (VB.Left(sJobStatus, 2) >= "40") And (VB.Left(sJobStatus, 2) <= "49") Then  '--QC.--
            sActionList = "amend;update;returntoqueue;tfrtonewcustomer;"  '--=3083=
        ElseIf (VB.Left(sJobStatus, 2) = "50") Then  '-- completed..  not delivered..--
            '-- !!  Notifications..
            sActionList = "reopen;deliver;tfrtonewcustomer;"  '--=3083=
        ElseIf (VB.Left(sJobStatus, 2) >= "90") Then  '-- cancelled..--
        Else '--delivered-
            '=bNewNode = False '--no new node to fiddle with..-
        End If '--service status..-
        '-- finish action perms..-
        If (VB.Left(sJobStatus, 2) <= "50") Then  '--  not delivered..--
            sActionList &= "notify;stoppress;"  '--=3083=
        End If
        '-- All can be viewed.. 
        sActionList &= "view;"  '--=3083=

        msJobActionList = sActionList

    End Function  '-msJobActionList-
    '= = = = = = = = =  =
    '-===FF->

    '=3501.0618==
    '--ON-SITE Jobs DataGrid Context menu stuff--
    '--mouse activity---
    '-- Popup ON-SITE Job Action menu..--
    '==
    '==    Updated- 4201.0717 16-July-2019= 
    '==       >> Job Search DataGridView.. ALSO the Customer SearchGrid/Jobs Listview-  
    '==          --  Add Right-click context menu for Job Actions, as per Active Jobs Tree treeview.
    '==

    Private Sub DataGridViewOnSite_CellMouseDown(eventSender As Object,
                                                     EventArgs As DataGridViewCellMouseEventArgs) _
                                                        Handles dataGridViewOnSite.CellMouseDown, DataGridViewJobs.CellMouseDown
        Dim intRowIndex As Integer = EventArgs.RowIndex
        Dim gridRow1 As DataGridViewRow
        Dim intJobNo As Integer
        Dim s1, sJobNo, sJobStatus As String
        Dim sActionList As String = ""
        '= Dim p1 = New Point(EventArgs.X, EventArgs.Y)

        Dim dgv1 As DataGridView = CType(eventSender, DataGridView)
        Dim sGridName As String = dgv1.Name

        If EventArgs.Button = Windows.Forms.MouseButtons.Right Then
            If (intRowIndex >= 0) Then
                gridRow1 = dgv1.Rows(intRowIndex)
                If (LCase(sGridName) = "datagridviewonsite") Then
                    sJobNo = gridRow1.Cells("jobNo").Value
                Else  '-all jobs-
                    sJobNo = CStr(gridRow1.Cells("job_id").Value)
                End If
                sJobStatus = gridRow1.Cells("JobStatus").Value
                '-TEST-
                '= MessageBox.Show("Found Job " & sJobNo & ";   JobStatus: " & sJobStatus, _
                '=                   "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If IsNumeric(sJobNo) Then
                    intJobNo = CInt(sJobNo)
                Else
                    MessageBox.Show("No JobNo..", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                '-- get actual cell pos..
                Dim intX, intY As Integer
                '= Dim rectThisCell As Rectangle = dataGridViewOnSite.GetCellDisplayRectangle(EventArgs.ColumnIndex, EventArgs.RowIndex, True)
                Dim rectThisCell As Rectangle = dgv1.GetCellDisplayRectangle(EventArgs.ColumnIndex, EventArgs.RowIndex, True)
                intX = rectThisCell.Location.X
                intY = rectThisCell.Location.Y + rectThisCell.Height

                Dim p1 As Point = New Point(intX, intY)
                '-TEST-
                '= MessageBox.Show("Cell Pos (x,y) is " & p1.x & ", " & p1.y, _
                '=                   "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ''-- select permitted actions.
                'If (VB.Left(sJobStatus, 2) <= "10") Then '--created..-
                '    '= nodeY = tvwJobs.Nodes.Find("Queued", True)(0).Nodes.Add(sKey, sCaption)
                '    If (VB.Left(sJobStatus, 2) = "05") Then '--waitlisted..-
                '        sActionList = "checkin;amend;tfrtonewcustomer;"  '--=3323=
                '    Else   '-- status 10-
                '        sActionList = "returntowaitlist;amend;start;tfrtonewcustomer;"  '--=3083= 3203.211--
                '    End If
                'ElseIf (VB.Left(sJobStatus, 2) >= "20") And (VB.Left(sJobStatus, 2) < "30") Then  '--Suspended....--
                '    sActionList = "amend;update;tfrtonewcustomer;"  '--=3083=
                'ElseIf (VB.Left(sJobStatus, 2) > "10") And (VB.Left(sJobStatus, 2) <= "33") Then  '--started, not completed..--
                '    sActionList = "amend;update;returntoqueue;tfrtonewcustomer;"  '--=3083=
                'ElseIf (VB.Left(sJobStatus, 2) >= "40") And (VB.Left(sJobStatus, 2) <= "49") Then  '--QC.--
                '    sActionList = "amend;update;returntoqueue;tfrtonewcustomer;"  '--=3083=
                'ElseIf (VB.Left(sJobStatus, 2) = "50") Then  '-- completed..  not delivered..--
                '    '-- !!  Notifications..
                '    sActionList = "reopen;deliver;tfrtonewcustomer;"  '--=3083=
                'ElseIf (VB.Left(sJobStatus, 2) >= "90") Then  '-- cancelled..--
                'Else '--delivered-
                '    '=bNewNode = False '--no new node to fiddle with..-
                'End If '--service status..-
                ''-- finish action perms..-
                'If (VB.Left(sJobStatus, 2) <= "50") Then  '--  not delivered..--
                '    sActionList &= "notify;stoppress;"  '--=3083=
                'End If
                ''-- All can be viewed.. 
                'sActionList &= "view;"  '--=3083=

                sActionList = msJobActionList(sJobStatus)

                mIntOnsiteJobNoContextMenuSelected = intJobNo  '-for menu set up routine..
                mNodeJobsTreeContextMenuSelected = Nothing
                '-- Select this row..
                dgv1.CurrentCell = dgv1.Rows(EventArgs.RowIndex).Cells(EventArgs.ColumnIndex)

                '- Show Job details..
                Call mbShowJobInfoRTF(intJobNo)

                '-- set menu options..
                Call mSubSetContextmenuOptions(sActionList)

                '-show context menu.
                mContextMenuJobsTreeNodeAction.Show(CType(eventSender, Control), p1)

            End If  '-row-
        End If  '-right click-

    End Sub  '-DataGridViewOnSite_CellMouseDown-
    '= = = = = = = = =  = = = = = = = = = = == 
    '-===FF->

    '== Cust/Jobs ListView-  MouseDown..
    '==    Updated- 4201.0717 16-July-2019= 
    '==       >> Job Search DataGridView.. ALSO the Customer SearchGrid/Jobs Listview-  
    '==          --  Add Right-click context menu for Job Actions, as per Active Jobs Tree treeview.
    '==

    Private Sub listViewCustJobs_MouseDown(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As MouseEventArgs) Handles listViewCustJobs.MouseDown
        Dim mouseX As Integer = eventArgs.X
        Dim mouseY As Integer = eventArgs.Y
        '= Dim idxView As Short = ListViewParts.GetIndex(eventSender)
        '= Dim lRow, ix, lCol As Integer
        Dim sName, sJobStatus, sActionList As String
        Dim intJobId, j, i, k As Integer
        '= Dim index As Short
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim itemRect As New Rectangle
        Dim itemX, itemY, itemHeight As Integer  '--location.

        item1 = listViewCustJobs.FocusedItem

        If eventArgs.Button = MouseButtons.Right Then
            '=MsgBox("Mouse clicked at (x,y): " & mouseX & "," & mouseY, MsgBoxStyle.Information)
            If Not (item1 Is Nothing) Then '--item selected.. -
                itemRect = item1.GetBounds(ItemBoundsPortion.Entire)
                '- item rectangle location X,Y is relative to top of items area.
                itemX = itemRect.X
                itemY = itemRect.Y '= + 20  '--make it start from top of hdt row..
                itemHeight = itemRect.Height
                '--We make sure mouse was clicked on focused item..
                '-test
                'MsgBox("Focussed rect is at (x,y):" & itemX & "," & itemY & vbCrLf & _
                '         "Mouse clicked at (x,y): " & mouseX & "," & mouseY, MsgBoxStyle.Information)
                If ((mouseX >= itemX) And (mouseX <= (itemX + itemRect.Width))) And _
                       (mouseY >= itemY) And (mouseY <= (itemY + itemRect.Height)) Then
                    '-ok..  clicked inside focussed item.
                    '== MsgBox("Focussed item clicked..", MsgBoxStyle.Information)
                    '- get Job No..
                    intJobId = CInt(item1.Text) '--1st column has to be job_id..--
                    Call mbShowJobInfoRTF(intJobId)

                    sJobStatus = item1.SubItems(1).Text  '--second column.
                    '= MsgBox("Selected Job is: " & intJobId & "/" & sJobStatus, MsgBoxStyle.Information)
                    '- get list of permitted actions..
                    sActionList = msJobActionList(sJobStatus)

                    mIntOnsiteJobNoContextMenuSelected = intJobId  '-for menu set up routine..
                    mNodeJobsTreeContextMenuSelected = Nothing
                    '-- get postition to show menu..
                    Dim p1 As Point = New Point(itemX, itemY)
                    '-- set menu options..
                    Call mSubSetContextmenuOptions(sActionList)

                    '-show context menu.
                    mContextMenuJobsTreeNodeAction.Show(CType(eventSender, Control), p1)
                End If  '- in bounds-
            End If  '-nothing.
        End If  '-right button.-
    End Sub  '- listViewCustJobs.MouseDown-
    '= = = = = = = = = = = = == = = = = = = = =  = 
    '-===FF->

    '--=3107.907=  Rich Text Context menu stuff--

    '-- menu click-
    Public Sub mnuCopyRichTextSelection_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles mnuCopyRichTextSelection.Click
        Dim sText As String = rtfJobDetails.SelectedText

        If (Trim(sText) <> "") Then  '-copy to clipboard-
            My.Computer.Clipboard.SetText(sText) '-- descr is item-2nd item..-.
        End If

    End Sub  '-mnuCopyRichTextSelection- click-
    '= = = = = = = = = = = = = = = =  = = = = = = =

    '-  MOUSE ACTION- rtfJobDetails-

    Private Sub rtfJobDetails_MouseUp(sender As Object, _
                                         ev As MouseEventArgs) Handles rtfJobDetails.MouseUp
        ' If the right mouse button was clicked and released, 
        ' display the shortcut menu assigned to the TreeView.  
        If ev.Button = MouseButtons.Right Then
            mContextMenuRichTextInfo.Show(rtfJobDetails, New Point(ev.X, ev.Y))
        End If
    End Sub  '-rtf mouse up-
    '= = = = = = = = = == 
    '-===FF->

    '-- Job Action ToolStrip..--

    Private Sub ToolStripJobAction_ButtonClick(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) _
                                         Handles btnStopPress.Click, btnCheckIn.Click, btnAmend.Click, btnUpdate.Click, _
                                                  btnReturnToQueue.Click, btnReOpen.Click, btnDeliver.Click, btnDetailNotify.Click
        Dim button1 As System.Windows.Forms.ToolStripButton = CType(eventSender, System.Windows.Forms.ToolStripButton)

        Select Case LCase(button1.Tag)

            Case "stoppress"
                Call cmdStopPress_Click()
            Case "checkin"
                Call mbAmendAgreement(True)
            Case "amend"
                Call mbAmendAgreement(False)
            Case "update"
                Call cmdStart_Click()
            Case "returntoqueue"
                Call mbJobActionReturnToQueue()
            Case "reopen"
                Call cmdReOpen_Click()
            Case "deliver"
                '--=3103.227=
                '- only call if NOT JobMatixPOS..
                '--  else show msg diverting user to POS Sales Tab..
                If mbIsJobmatixPOS() Then
                    MsgBox("Please Note: Job Delivery in this system is done in the Sales Screen-" & vbCrLf & _
                            " Please go to the POS Sales Tab, select the customer, and select 'Sales'..  " & vbCrLf & vbCrLf & _
                            "Job details will be inserted in the Sales grid ready for payment.", MsgBoxStyle.Information)
                    Exit Sub
                End If
                '--Not JobmatixPOS-  call action command--
                Call cmdDeliver_Click()
            Case "notify"
                Call cmdDetailNotify_Click()
            Case Else

        End Select
    End Sub  '--Job Action.-
    '= = = = = = = = = = = = =

    '== Show Attachments -- v3.2.1229-

    Private Sub picAttachments_Click(sender As Object, e As EventArgs) Handles picAttachments.Click
        Dim frmDocs1 As frmAttachments

        If (mlJobId > 0) Then '-have Job..-
            mlStaffTimeout = -1 '--SUSPEND timing out..--
            frmDocs1 = New frmAttachments(Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                        "JOB", mlJobId, txtDetailsHdr.Text, mlStaffId, msStaffName, msJobMatixVersion)
            frmDocs1.ShowDialog()
            mlStaffTimeout = 0 '--NOW is timing out..--
        End If  '-id-
    End Sub  '--picAttachments-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- J o b s  S e a r c h --
    '-- J o b s  S e a r c h --

    Private Sub ToolbarJobs_ButtonClick(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) _
    Handles _ToolbarJobs_Button1.Click, _ToolbarJobs_Button2.Click, _ToolbarJobs_Button3.Click, _ToolbarJobs_ButtonQA.Click, _ToolbarJobs_Button4.Click, _ToolbarJobs_Button5.Click, _ToolbarJobs_Button6.Click
        Dim Button As System.Windows.Forms.ToolStripItem = CType(eventSender, System.Windows.Forms.ToolStripItem)
        Dim colKeys As Collection '--primary keys of selected record-
        Dim s1, s2 As String
        Dim ix, cx As Integer
        Dim sWhere As String
        Dim sWhere2 As String
        Dim sHdr As String
        Dim sSql As String
        Dim bShowQuotes As Boolean
        Dim colPrefs As Collection
        Dim table1 As Collection
        Dim column1 As Collection
        '===Dim asArgs As Variant
        Dim asColumns() As String

        If msStaffName = "" Then Exit Sub '--was signed off..-
        If Button Is Nothing Then Exit Sub

        '--unclick all six buttons..-

        CType(ToolbarJobs.Items.Item("_toolbarJobs_Button1"), ToolStripButton).Checked = False
        CType(ToolbarJobs.Items.Item("_toolbarJobs_Button2"), ToolStripButton).Checked = False
        CType(ToolbarJobs.Items.Item("_toolbarJobs_Button3"), ToolStripButton).Checked = False
        CType(ToolbarJobs.Items.Item("_toolbarJobs_ButtonQA"), ToolStripButton).Checked = False
        CType(ToolbarJobs.Items.Item("_toolbarJobs_Button4"), ToolStripButton).Checked = False
        CType(ToolbarJobs.Items.Item("_toolbarJobs_Button5"), ToolStripButton).Checked = False
        CType(ToolbarJobs.Items.Item("_toolbarJobs_Button6"), ToolStripButton).Checked = False

        '-- check this button.- 
        CType(Button, ToolStripButton).Checked = True

        txtFind.Enabled = True
        bShowQuotes = False
        '===LabJobType(index).Caption = ""
        sWhere = "" '--All jobs.-
        mButtonCurrentBrowse = Button '--save button so we can repeat browse..
        colPrefs = mColPrefsDeliver

        Select Case LCase(Button.Tag)
            Case "queued"
                sHdr = "NEW Jobs in Queue."
                If bShowQuotes Then sHdr = "QUOTE Jobs for  SERVICE."
                sWhere = " (LEFT(jobStatus,2)<='10') " '--WaitListed or Accepted, NOT started..--
                '===Call mbShowJobsBrowse(mColPrefs, sHdr, sWhere)
                colPrefs = mColPrefsDeliver
            Case "suspended"
                sHdr = "Suspended Jobs."
                sWhere = " (LEFT(jobStatus,2)<'30') and (LEFT(jobStatus,2)>='20') "

            Case "started" '--show jobs being serviced..-
                sHdr = "Jobs in Progress."
                If bShowQuotes Then sHdr = "QUOTE Jobs Currently in Progress.."
                '--  07Aug2011-- Include "40" for Q.A..
                sWhere = " (LEFT(jobStatus,2)<'40') and (LEFT(jobStatus,2)>='30') " '--started, not completed..--
                '==Call mbShowJobsBrowse(mColPrefs2, sHdr, sWhere)
                colPrefs = mColPrefsDeliver
            Case "qa" '--show jobs being checked..-
                sHdr = "Jobs in QA."
                If bShowQuotes Then sHdr = "QUOTE Jobs Currently in QA.."
                '--  07Aug2011-- Include "40" for Q.A..
                sWhere = " (LEFT(jobStatus,2)='40')"   '== and (LEFT(jobStatus,2)>='30') " '--started, not completed..--
                '==Call mbShowJobsBrowse(mColPrefs2, sHdr, sWhere)
                colPrefs = mColPrefsDeliver
            Case "completed" '--show completed (UnNotified) jobs ..-
                sHdr = "Completed Jobs to be Notified."
                If bShowQuotes Then sHdr = "Completed QUOTE Jobs yet to be Notified."
                sWhere = " (LEFT(jobStatus,2)='50') AND (Notifications NOT LIKE '%Notified OK%') " '-- completed jobs.. No contact..--
                '==Call mbShowJobsBrowse(mColPrefsDeliver, sHdr, sWhere)
                colPrefs = mColPrefsDeliver
            Case "deliverable" '--show jobs to be delivered..-
                sHdr = "Completed Jobs to be Delivered."
                If bShowQuotes Then sHdr = "Completed QUOTE Jobs yet to be Delivered."
                sWhere = " (LEFT(jobStatus,2)='50') " '-- completed jobs not yet delivered..--
                '====Call mbShowJobsBrowse(mColPrefsDeliver, sHdr, sWhere)
                colPrefs = mColPrefsDeliver
            Case "viewall"
                sWhere = Replace(sWhere2, "AND", "") '----all jobs..--
                sHdr = "All jobs.."
                If bShowQuotes Then sHdr = "Select from All QUOTE jobs.."
                '==Call mbShowJobsBrowse(mColPrefsDeliver, sHdr, sWhere)    '-- Then
                colPrefs = mColPrefsDeliver
            Case Else
                MsgBox("Toolstrip button '" & Button.Name & "' not recognized.", MsgBoxStyle.Exclamation)
                Exit Sub
        End Select
        '-- if MY-JOBS etc then add srch argument..--
        If (sWhere2 <> "") Then
            sWhere = sWhere & " AND " & sWhere2
        End If
        '-- Add TEXT SRCH parameters if any..-

        '--TEXT SEARCH request--
        '--TEXT SEARCH request--
        '-- build query to srch all text cols..--
        '=======sSearchArg = Trim(txtSearch.Text)    '==UCase(request.Form("selTextSearchArg"))
        sSql = "" '---"SELECT * FROM " + msProductTableName + " WHERE "
        s1 = "" : s2 = "" '--result-
        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))
        table1 = mColSqlDBInfo.Item("JOBS") '--- mCat1.Tables(msProductTableName)
        cx = 0 : Erase asColumns
        '-- get list of text-type columns..-
        For Each column1 In table1.Item("FIELDS")
            '===If gbIsText(gsGetSqlType(column1.Type, column1.DefinedSize)) Then  '-- is text col..-
            If gbIsText(column1.Item("TYPE_NAME")) Then '-- is text col..-
                ReDim Preserve asColumns(cx)
                asColumns(cx) = column1.Item("NAME")
                cx = cx + 1
            End If
        Next column1 '--
        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtSearch.Text), asColumns)
        '===If UBound(asColumns) > 0 Then '--we have some text cols..--
        '===   For ix = 0 To UBound(asArgs)  '--for each arg fragment..--
        '===       If (ix > 0) Then sSql = sSql + " AND "
        '===       s1 = "" '-- sub-clause for this Arg..
        '===       If UBound(asColumns) > 1 Then s1 = s1 + "("   '-- for multiple columns..--
        '===       For cx = 1 To UBound(asColumns) '---Each column1 In table1.Columns
        '===         s2 = asColumns(cx)
        '===         '---If gbIsText(gsGetSqlType(column1.Type, column1.DefinedSize)) Then  '-- is text col..-
        '===         If (cx > 1) Then s1 = s1 + " OR "
        '===           '-- all cols are in OR relation with this Arg...--
        '===         s1 = s1 + "(" + s2 + " LIKE '%" + asArgs(ix) + "%' )"
        '===       Next cx '--column-
        '===       If UBound(asColumns) > 1 Then s1 = s1 + ")"
        '===       sSql = sSql + s1
        '===   Next ix
        '===Else  '--no text cols..  no search..
        '===End If  '--text--
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere & " AND "
            sWhere = sWhere & sSql
        End If

        '-- DO IT...-
        Call mbShowJobsBrowse(colPrefs, sHdr, sWhere) '-- Then--
        '===cmdCustHistory(index).Enabled = True
        mlJobId = -1 '--15/July/2010==force refresh of job details.--

        If gbDebug Then MsgBox("SQL Select Condition was:" & vbCrLf & mBrowseJobs.FinalWhere)
        colPrefs = Nothing

        '=3311.817= Any activity resets to full timer-
        mlStaffTimeout = 0 '--NOW is timing out.. Allow full interval--

    End Sub '--ToolbarJobs_ButtonClick--
    '= = = = = = = = = = = = = = = = = 

    '-- J o b s  S e a r c h --

    Private Sub txtSearch_Enter(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles txtSearch.Enter '==(index As Integer)
        '===txtSearch(index).SelStart = 0
        '==txtSearch(index).SelLength = Len(txtSearch(index).Text)
        txtSearch.SelectionStart = 0
        txtSearch.SelectionLength = Len(txtSearch.Text)
    End Sub '--got focus..-
    '= = = = = = = = = = =

    '===Private Sub txtSearch_keyPress(index As Integer,
    Private Sub txtSearch_KeyPress(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            Call cmdSearch_Click(cmdSearch, New System.EventArgs()) '====(index)
            iKeyAscii = 0 '--done..-
        End If '--CR-
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = =

    '-- Clear--
    Private Sub cmdClearSearch_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles cmdClearSearch.Click '==(index As Integer)

        '==3311.817= stop multiple hits.
        cmdClearSearch.Enabled = False
        txtSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        '===Call mbRefreshJobsTreeView
        Call cmdSearch_Click(cmdSearch, New System.EventArgs())
        cmdClearSearch.Enabled = True

    End Sub '--clear--
    '= = = = = = =
    '-===FF->

    '-- Search add some crteria --
    '--  to the Browse parameters..-
    '----  AND Initiate BROWSE..---

    Private Sub cmdSearch_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click '===(index As Integer)
        Dim sWhere As String
        Dim sHdr As String
        Dim s1, s2 As String
        Dim cx, ix, index As Integer
        Dim table1 As Collection
        Dim column1 As Collection
        '===Dim asArgs As Variant
        Dim asColumns() As String

        If mButtonCurrentBrowse Is Nothing Then
            '==MsgBox "Select a status filter first.", vbInformation
        Else
            '==3311.817= stop multiple hits.
            cmdSearch.Enabled = False
            Call ToolbarJobs_ButtonClick(mButtonCurrentBrowse, New System.EventArgs()) '--refresh browse grid..-
            '-  added 3501.1106 - 06Nov2018-
            If (DataGridViewJobs.Rows.Count > 0) Then  '-have rowes..
                '-- select the top one after a search
                DataGridViewJobs.Rows(0).Selected = True
                DataGridViewJobs.Select()
            End If  '-count-
            cmdSearch.Enabled = True
        End If

    End Sub '--search..-
    '= = = = = = = = == =
    '= = = = = = ==
    '-===FF->

    '-- CUSTOMER Full text search..-
    '-- CUSTOMER Full text search..-

    Private Sub txtCustSearch_Enter(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtCustSearch.Enter
        txtCustSearch.SelectionStart = 0
        txtCustSearch.SelectionLength = Len(txtCustSearch.Text)
    End Sub '--got focus..-
    '= = = = = = = = = = =

    Private Sub txtCustSearch_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                          Handles txtCustSearch.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            Call cmdCustSearch_Click(cmdCustSearch, New System.EventArgs())
            iKeyAscii = 0 '--done..-
        End If '--CR-
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = =

    '-- Clear--
    Private Sub cmdClearCustSearch_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles cmdClearCustSearch.Click
        '==3311.817= stop multiple hits.
        cmdClearCustSearch.Enabled = False
        txtCustSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdCustSearch_Click(cmdCustSearch, New System.EventArgs())
        cmdClearCustSearch.Enabled = True
    End Sub '--clear--
    '= = = = = = =
    '-===FF->

    '-- Search add some more crteria --
    '--  to the current Browse parameters..-

    Private Sub cmdCustSearch_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles cmdCustSearch.Click
        Dim sWhere As String
        '==Dim sTitle As String
        '==Dim colPrefs As Collection
        '=  Dim cx As Integer
        '== Dim colJetDBInfo As Collection
        '== Dim table1 As Collection
        '= Dim column1 As Collection
        Dim sSql As String '--search sql..--
        Dim s1, s2 As String
        Dim asColumns As Object

        '--  rebuild Search Columns and call makeTextSearch...-
        '-- arg can be multiple tokens..--
        '= colJetDBInfo = mRetailHost1.colTables
        '= table1 = colJetDBInfo("customer")

        '= cx = 0 : Erase asColumns

        '==  !!!  Make columns array-- 
        '== THIS MUST go to retailHost..--
        '== THIS MUST go to retailHost..--
        '-- get list of text-type columns..-

        '- WAS  TEMP for MYOB..--
        '==asColumns = New Object() {"surname", "given_names", "company", "addr1", "addr2", "addr3", "suburb", "email"}

        '--  now in the Interface..--
        '==3311.817= stop multiple hits.
        cmdCustSearch.Enabled = False

        asColumns = mRetailHost1.CustomerSearchColumns()

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtCustSearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere + " AND "
            sWhere = sWhere + sSql
        End If
        mbRefreshCustomerBrowse(sWhere)
        cmdCustSearch.Enabled = True
        '-  added 3501.1105 - 05Nov2018-
        If (DataGridViewCust.Rows.Count > 0) Then  '-have roes..
            '-- select the top one after a search
            DataGridViewCust.Rows(0).Selected = True
            DataGridViewCust.Select()
        End If  '-count-

    End Sub '--Cust.search..-
    '= = = = = = = = == = = = =
    '-===FF->


    '==  JobMatix  P O S  Sales Events -
    '==  JobMatix  P O S  Sales Events -
    '==3401.315= Sales HOLD/RESTORE Manager--

    '--  Hold- Restore- etc..
    '--   AND we have THREE Sale Instances..
    '-All Sale swapping done HERE in MAIN form..

    '-- Hold-

    '= NEW version ==

    '=  3401.411= 
    '-- Multi-Sale- Swapping..

    '=3411.1221= POS stuff now GONE to its own EXE..
    '=3411.1221= POS stuff now GONE to its own EXE..
    '=3411.1221= POS stuff now GONE to its own EXE..
    '=3411.1221= POS stuff now GONE to its own EXE..


    '--close--
    '--close--
    '---- NOTE: The sub-classed window CRASHES the debugger for uncaught runtime errors..

    Private Sub frmJobMatix3_FormClosed(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        '== UnhookToolTips()

    End Sub '--unload..--
    '= = = = = = = = = =

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmJobMatix3_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim sMsg As String
        Dim ix As Integer

        '===sMsg = " Are you sure you want to abandon this new job ?"
        mbFormClosing = True
        Timer1.Enabled = False   '--stop spurious activity..-
        Call gbLogMsg(gsRuntimeLogPath, "== JobMatix Main form is closing.." & vbCrLf & vbCrLf & _
                                                         "= = = = = = = = = = = =" & vbCrLf & vbCrLf)
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                      System.Windows.Forms.CloseReason.TaskManagerClosing, _
                               System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
                '== If Not mCnnSql Is Nothing Then mCnnSql.Close()
                If (Not (mCnnSql Is Nothing)) AndAlso ((mCnnSql.State And 1) <> 0) Then '--open-
                    mCnnSql.Close()
                End If
                '==GONE= If Not mCnnShape Is Nothing Then mCnnShape.Close()
                If Not mRetailHost1 Is Nothing Then mRetailHost1.closeConnection()

                mBrowseJobs = Nothing
                '== Set mBrowseRAs = Nothing
                mBrowseCust = Nothing
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                If (Not (mCnnSql Is Nothing)) AndAlso ((mCnnSql.State And 1) <> 0) Then '--open-
                    mCnnSql.Close()
                End If
                '== If Not mCnnSql Is Nothing Then mCnnSql.Close()
                '==GONE= If Not mCnnShape Is Nothing Then mCnnShape.Close()
                If Not mRetailHost1 Is Nothing Then mRetailHost1.closeConnection()
                '==For ix = 0 To K_MAXJOBTABS
                mBrowseJobs = Nothing
                mBrowseCust = Nothing
                '==Next ix
                '== Set mBrowseRAs = Nothing
                mRetailHost1 = Nothing

                '== Me.Close()
                intCancel = 0 '--let it go--
                '==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--unload--

    '=== End of form ==
    '= = = = end form= = =
    '= = = = end form= = =
    '= = = = end form= = =


End Class  '-jobmatix-
'= = = = = = = = == =  =


' Implements the manual sorting of items by columns.
Class ListViewItemComparerQ3037
    Implements IComparer
    Private col As Integer
    Private order As SortOrder

    Public Sub New()
        col = 0
        order = SortOrder.Ascending
    End Sub

    Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
        col = column
        Me.order = order
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
                        Implements System.Collections.IComparer.Compare
        Dim returnVal As Integer = -1

        Try
            returnVal = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, _
                                                     CType(y, ListViewItem).SubItems(col).Text)
        Catch ex As Exception
            MsgBox("Error sorting Quotes Listview.." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
        End Try

        '-- Determine whether the sort order is descending.
        If order = SortOrder.Descending Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If

        Return returnVal
    End Function
End Class
'==  the end (2)..==