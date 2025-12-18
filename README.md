# JobMatix - Advanced Point of Sale, Inventory and Job Tracking System

[![License: Apache 2.0](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Framework: .NET 3.5](https://img.shields.io/badge/.NET-3.5-blue.svg)](https://dotnet.microsoft.com/)
[![Database: SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red.svg)](https://www.microsoft.com/en-us/sql-server)

> **JobMatix Open Source** - First version based on beta version 4.2.4287. A comprehensive business management solution for computer service shops and similar businesses.

## 🎯 Overview

JobMatix is a Windows-based business management system that combines **Point of Sale (POS)**, **Inventory Management**, and **Job Tracking** functionality. Originally designed as a companion to MYOB Retail Manager, JobMatix has evolved into a complete standalone solution with its own integrated POS system.

### Key Features

- 🔧 **Job Tracking & Service Management** - Complete workflow management for repairs and system builds  
- 🛒 **Point of Sale System** - Full retail POS with inventory, sales, and customer management
- 📦 **Inventory Control** - Stock management, purchase orders, goods received, serial tracking
- 👥 **Customer Management** - Customer records, account sales, subscriptions, and statements
- 📋 **RMA Tracking** - Returns merchandise authorization management
- 📊 **Reporting** - Comprehensive sales, stock, and business reports
- 💾 **Database Integration** - MS SQL Server backend with multi-user support

## 🏗️ Architecture

JobMatix consists of several interconnected .NET modules:

```
JobMatix Solution Structure:
├── JobMatix62.Net/          # Main application launcher
├── JMxJT620.NET/            # Core Job Tracking module  
├── JMxPOS620.Net/           # Point of Sale system
├── JMxKeyGen420_OS/         # License key generation
├── JMxRetailHost620.Net/    # Retail system integration
├── JMxRAs62.Net/            # Returns/RMA management
├── JMxJT620ex.Net/          # Job Tracking extensions
├── backup-agent/            # Database backup utilities
├── runtime/                 # Deployment files
└── documentation/           # Project documentation
```

## 💻 System Requirements

### Minimum Requirements
- **OS**: Windows 7/10 or Windows Server
- **Database**: SQL Server 2008-R2 or later (Express edition supported)
- **Framework**: Microsoft .NET Framework 3.5
- **Hardware**: Standard business PC specifications

### Network Requirements
- Local network access for multi-user installations
- Network permissions for shared database access
- Optional: MYOB Retail Manager database access (for migration)

### Printer Support
- A4 printer for service agreements and reports
- Receipt/docket printer for customer receipts  
- Label printer (optional, for job/stock labels)

## 🚀 Getting Started

### Installation Options

1. **Runtime Installation** (Recommended for end users):
   - Extract `runtime/JobMatix-Runtime-Build-6201_xcopy/` 
   - Configure database connection
   - Run JobMatix62.exe

2. **Development Setup**:
   - Install Visual Studio 2017 or later
   - Install .NET Framework 3.5 SDK
   - Open `JobMatix62.Net/JobMatix62Main.sln`
   - Build solution

### Database Setup

1. Install SQL Server (Express edition acceptable)
2. Create new database for JobMatix
3. Configure connection strings in application
4. Run initial database setup through JobMatix admin tools

### First-Time Configuration

1. **Basic Setup**: Configure business details, printers, and basic settings
2. **Staff Setup**: Add staff members and permissions  
3. **Stock Migration**: Import existing stock data (if migrating from MYOB RM)
4. **Customer Migration**: Import customer database
5. **System Testing**: Perform test transactions and job entries

## 📚 Core Modules

### Job Tracking System
- **Service Jobs**: Equipment check-in, repair tracking, service agreements
- **Build Jobs**: New system construction from quotations
- **Workflow Management**: Queue → Started → QA → Completed → Delivered
- **Parts & Labour**: Track components and time spent on each job
- **Customer Notifications**: SMS and email notifications for job status

### Point of Sale (POS)
- **Sales Processing**: Cash and account sales, refunds, layaway
- **Customer Management**: Account customers, pricing grades, subscriptions
- **Payment Types**: Cash, EFTPOS, account, store credit
- **Inventory Integration**: Real-time stock updates, serial number tracking

### Inventory Management  
- **Stock Control**: Product records, categories, pricing, photos
- **Purchase Orders**: Supplier management, goods received processing
- **Serial Tracking**: Track items from purchase through to sale
- **Stocktaking**: Full and partial stocktake functionality

### RMA (Returns) System
- **Return Authorization**: Create RMAs for warranty returns
- **Supplier Integration**: Automatic supplier invoice lookup
- **Status Tracking**: Track returns through authorization to resolution
- **Package Management**: Group returns by supplier for shipping

## 🔧 Configuration

### Key Configuration Areas

1. **Database Connection**: Set up SQL Server connection parameters
2. **Printer Setup**: Configure receipt, report, and label printers  
3. **SMS Gateway**: Set up customer notification system
4. **Email Settings**: Configure SMTP for invoices and statements
5. **Business Rules**: Set pricing, taxes, minimum charges
6. **User Security**: Staff permissions and access control

### Migration from MYOB Retail Manager

JobMatix includes migration tools to import data from MYOB Retail Manager:

- ✅ Staff records
- ✅ Supplier information  
- ✅ Stock/inventory data
- ✅ Customer records
- ✅ Serial number history
- ❌ Sales invoices (not migrated)
- ❌ Payment history (not migrated)

## 📖 Documentation

- **[Detailed Documentation](documentation/JobMatix-docs.md)** - Complete feature documentation
- **[Migration Guide](documentation/Migrating_to_JobMatixPOS_6201.doc)** - MYOB RM to JobMatix migration
- **[What's New](documentation/NewInJobMatix62.htm)** - Version 6.2 feature updates
- **[License](documentation/LICENSE)** - Apache License 2.0 terms

## 🛠️ Development

### Building from Source

```bash
# Clone the repository
git clone https://github.com/dablackfox/JobMatix.git
cd JobMatix

# Open in Visual Studio
# File -> Open -> Project/Solution
# Select: JobMatix62.Net/JobMatix62Main.sln

# Build Solution (Ctrl+Shift+B)
```

### Project Structure

- **JobMatix62.Net**: Main application and startup logic
- **JMxJT620.NET**: Core job tracking business logic and forms
- **JMxPOS620.Net**: POS system implementation  
- **JMxKeyGen420_OS**: License management and key generation
- **JMxRetailHost620.Net**: Integration layer for external retail systems
- **JMxRAs62.Net**: RMA/Returns management system

### Technology Stack

- **Language**: Visual Basic .NET
- **Framework**: .NET Framework 3.5
- **Database**: SQL Server with ADO.NET
- **UI Framework**: Windows Forms
- **Reports**: Built-in reporting system
- **Architecture**: Multi-tier desktop application

## 📝 Version History

### Version 6.2.6201 (July 2021) - Open Source Release
- First open source release based on beta version 4.2.4287
- Added POS Reports: WhatsInStock by Supplier
- Added JobTracking: Optional Page 3 for Service Record printout

### Previous Major Versions
- **4.2.4287** (Jan 2021): POS Goods Received improvements, Customer Admin updates
- **4.2.4284**: Job Reports in main form, redesigned admin forms
- **3.5.3519**: Combined POS and JobTracking with tabbed interface
- **3.4.x**: Major POS functionality additions, customer subscriptions
- **3.3.x**: Attachments system, SMS notifications, Exchange calendar integration

## 🤝 Contributing

We welcome contributions to JobMatix! Please see our contributing guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)  
5. Open a Pull Request

### Areas for Contribution
- Modern UI improvements
- .NET Core/.NET 5+ migration
- Cloud database support
- Mobile companion app
- API development for integrations
- Documentation improvements

## 📧 Support & Contact

- **Issues**: [GitHub Issues](https://github.com/dablackfox/JobMatix/issues)
- **Discussions**: [GitHub Discussions](https://github.com/dablackfox/JobMatix/discussions)
- **Email**: grhaas@outlook.com

## ⚖️ License

This project is licensed under the Apache License 2.0 - see the [LICENSE](documentation/LICENSE) file for details.

## 🏢 Business Use

JobMatix is particularly well-suited for:

- **Computer Repair Shops**: Complete job tracking from check-in to delivery
- **Electronics Service Centers**: Warranty management and parts tracking  
- **Small Retail Businesses**: Integrated POS and inventory management
- **Service-Based Businesses**: Any business needing job workflow management

---

**Note**: This is the open source version based on the mature JobMatix 6.2 codebase. While the POS functionality was in beta at the time of open source release, the job tracking components are production-ready and have been used in businesses for many years.

*Copyright © grhaas@outlook.com 2014-2021. Released under Apache License 2.0.*