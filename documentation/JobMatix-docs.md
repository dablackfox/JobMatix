Documentation saved from JobMatix web site..  19-July-2021.
= = = =

## JobMatix

### Advanced Point of Sale, Inventory and JobTracking

JobMatix JobTracking started life as a job tracking support system for MYOB Retail Manager;  MYOB RM provided the Customer, Stock and Sales functionality, and JobMatix the job tracking. Now JobMatix has its own Stock/POS functionality, JobMatixPOS.

The JobMatix complete Windows application now includes new Retail POS modules designed to take the place of the core Retail Manager Stock/Sales/Purchases functionality,   and so now has both the POS and JobTracking functionality..

JobMatix uses MS-SQL Server on the local network as the DBMS host for the JobMatixPOS database for all POS, jobs and support functions.

### Reliable JobTracking & Parts Tracking Functionality for JobMatixPOS and MYOB RetailManager

JobMatix Job Tracking creates and tracks new system builds and service jobs for workshops that use JobMatixPOS (or MYOB Retail Manager) for the POS and stock management.

JobMatix job tracking tracks job workflow, job stock items (parts/charges), and labour assigned to Jobs, and tracks service jobs (repairs and upgrades), as well as systems built from quotations.

JobMatix also creates and tracks RMA records (Returned Merchandise Authorizations) for stock and customer items returned to suppliers for warranty action.

### JobMatix and RetailManager

JobMatix JobTracking can work with MYOB Retail Manager V10, V11 or V12 for shops that choose to stay with RM. The MYOB RM (MS-Access) database is used as a read-only reference base for quotations (sales orders), customer and stock details, staff names, etc. No data is updated in the MYOB database by JobMatix.

### JobMatixPOS

JobMatixPOS is a new retail POS system designed as a replacement retail/stock system to MYOB Retail Manager, and as an alternative retail/stock partner system for JobMatix JobTracking. Most core retail POS functions are implemented, including supplier info, stock control (purchase orders and goods received), item serial tracking, account and cash sales, laybys, cash and EftPos payment types for Cashup, account payments and debtors reports and statements.. POS also has Customer Subscriptions functionality to support rentals and Managed Services.

Existing MYOB Retail-Manager shops (with or without JobMatix JobTracking) can migrate the MYOB-RM database tables (ie. staff, suppliers, stock, serials, and customer tables from the recent.mdb database) to JobMatixPOS using the migration feature. (NB: RM Sales Invoices and Payments Received are not migrated).

For JobMatix POS users, JobTracking job delivery/sale would thereafter be done directly via the POS Sale screen, bypassing the need to scan in the list of job parts (barcodes) into the sale.

(Important Note: The POS functionality in JobMatixPOS dll 4.2.4287 was currently at Beta+ stage, and not yet in completely full release mode. Users should test JobMatixPOS in trial mode first, before using it as a live production system.) 

= = = = = 

## Jobmatix Features

## Job Tracking

### Creating, updating and delivering Jobs

**Service Jobs and New Systems.** 	A Jobmatix job can be a system checked-in for repair, service, or upgrade (Service Jobs), or a new system being built as part of one or more systems resulting from a quotation prepared in Retail Manager.
**Job Check-in with printed Service Agreement** 	Service Jobs are checked-in and the job record created using the New Service Agreement form.  This provides for the entry of relevant information from the customer (ie type and brand of equipment, problem description, etc.).  Customer details are retrieved from the Retail Manager customer table.   A Service Agreement form and customer Job Ticket are printed for the new job. A job can be assigned one of three priority levels..

**Jobs can book-in ahead.** 	Service Jobs can be booked in ahead of presentation so that they can get into the service queue (“wait-listed”) prior to physical presentation..

**Building New Systems from Quotation** 	Quotations for new systems (including parts lists) can be turned into jobs when they are ready to be built.  The quote is prepared as usual in RetailManager.  JobMatix will create new job records from the RM quote so that the new build(s) can be updated and tracked in the same way as service jobs.  The Job record will carry the list of parts/items needed to fulfil the building of the individual system for reference during the workshop process. When the quote indicates multiple systems, JobMatix will attempt to assign/distribute parts and create multiple jobs records where possible.

**Job Life Cycle** 	Every job goes through a workflow cycle. From being Queued, then Created, the job goes through to Started, Quality Assurance, Completion and Delivery.

**Finding Jobs** 	The Main Jobs Tree shows all active jobs in a familiar Windows explorer style.   Job are arranged in order of Job Status, or where they are in the cycle.. The Search Browser panel is used to find any job quickly (data grid can sort on any column on demand). Text searching on all text columns enables fast access to any job record in the system.

**Updating Jobs** 	Staff update the job record as work is done, and stock items and labour are assigned to the job.  JobMatix can record scanned items, or look up items in the Retail Manager stock table as required, and record them against the job.  Serial Numbers can be scanned in and recorded against the job, after checking against the Retail Manager SerialAudit Table. User-defined Task Checklists can be applied to Service-type items, and these can help to track detailed job progress.  Labour time can be recorded for each work session completed.Jobs can be assigned to the QA (Quality Assurance) status as required before being marked as completed and ready for delivery..

**Delivery and Billing** 	Completed jobs produce a printed Service Record ready for Delivery, and a customer receipt showing job details..   This includes all information need to produce a Retail Manager Sale transaction to sell the Job out at the counter.   NB:  All actual MYOB Stock Table updates are done by Retail Manager as part of the job sale transaction.
JobMatix also prints out the item barcodes (product codes and serial-nos) for all chargeable items assigned to the job so that they can be easily scanned into the Retail Manager sales invoice when the job is being sold.

**Customer History** 	Customer History lookup can show on one screen all jobs and Retail Manager sales for a selected customer.

= = = = =  = = =


## RMA Tracking

### Creating, updating and tracking Returns

(RAs, RMAs) 
**New RA Form** helps create RA Record for any item that was received into the MYOB RetailManager stock base via a Goods Received invoice. Scanning SerialNo into RA record immediately locates the original supplier invoice for compiling the RMA request to the supplier.

**RA Record** can be updated to track the life of the RMA as it goes through the various stages of supplier authorisation, goods return and replacement.
**RA Browser** quickly locates items based on status or other RA information.

= = = =  = = = =

## System Requirements-  JobMatix System Requirements

1. Windows 7/10 or Windows Server
2. SQL Server (+- Express) 2008-R2 or better.. 
3. JobMatix needs Microsoft .Net Framework 3.5 to be installed.
4. Retail Manager- (For sites using MYOB REtail Manager as Retail system)- JobMatix also needs access to the local network with the appropriate network permissions to read the MYOB Retail Manager recent.mdb database file.

**System Printers** JobMatix prints service agreements and service delivery records on A4 sheets, and will print customer receipts on a tear-off receipt (docket) printer. These printouts are directed by default to the system default printer until they are re-directed (in JobMatix) to the user-preferred device. Job labels can also be printed if a suitable label printer is installed and set up with the appropriate Windows driver.

# JobMatix POS  Introducing JobMatix POS

JobMatix JobTracking started life as a job tracking support system to go with MYOB Retail Manager;  MYOB RM provided the Customers, Stock and Sales functionality. Now that it seems like Retail Manager is approaching its end of life, JobMatix needs its own Stock/POS functionality.

JobMatix now includes a set of new Retail POS modules designed to replace the core Retail Manager Stock/Sales/Purchases functionality. .  This provides an escape route out of Retail Manager, and of course a new Retail-POS partner for JobMatix JobTracking.

JobMatixPOS supports most of the important POS/stock functions of Retail Manager.  These include stock control, purchasing and goods received, serial tracking, customer account and cash sales, most payment types, etc., as well as account payments and debtors statements.. Retail Manager shps will be able to migrate suppliers, stock, serials, and basic customer information from their current RM recent.mdb database to JobMatixPOS using the migration feature.

For JobMatix JobTracking users with JobMatix POS functionality,  JobTracking job delivery and sale is done directly via the POS Sale screen, bypassing the need to scan in the list of job parts (barcodes) into the sale.

(NB: POS functionality in JobMatixPOS dll 4.2.4282.1025 is at Beta +stage, and not yet in full release mode. Users should evaluate JobMatixPOS first in a separate (non-production) test environment.)

Built on Microsoft .Net Framework and MS SQL Server desktop client-server technology,  JobMatix Point of Sale (POS) is a secure user-friendly comprehensive sales, customer and inventory system.

## JobMatix POS features and functions:

1. **Advanced Stock/Inventory system-**  includes product photos, item serial number tracking from purchase to sale, Purchase Orders and Goods Received, and Stock Barcode Labels; stock records can be migrated from Retail Manager content where available.
2. **Customers, Staff and Supplier records-** migrated from Retail Manager content where available.
3. **Cash and Account Sales, Refunds, Laybys and Quotes**  Credit Notes (store credit) available for registered customers;  Invoices and receipts printed as needed.
4. **Customer Subscriptions–**  supports periodic billing for managed services and rentals. Fully integrated with debtors ledger.
5. **Debtors Statements**  and reports for account customers;
6. **Emailing:** Customer invoices and Statements can be sent by Email.
7. **Daily Cashup–** includes Till Balance and Payments Reports.
8. **Reports-** Sales, Payments and Stock Reports;
9. **Advanced Stocktaking-** (Full or Partial Stocktakes)

JobMatix POS is fully compatible and integrated with JobMatix JobTracking. The JobMatix graduated Site-Licence makes for simple, affordable licencing..  Check out our licencing page for current pricing.

## Migration from Retail Manager to JobMatix POS

How to migrate from Retail Manager to JobMatixPOS

You can download "Migrating_to_JobMatixPOS_6201.doc" from this repo..

= = = = = = = =

Updated 27-July-2021 6:56 pm AEST..

Copyright © grhaas@outlook.com 2014, 2015, 2016, 2017, 2018, 2019, 2020,2021..

= = = the end = =

