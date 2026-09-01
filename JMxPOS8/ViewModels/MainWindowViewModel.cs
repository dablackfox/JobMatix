using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DatabaseService _dbService;
    private readonly StockService _stockService;
    private readonly CustomerService _customerService;
    private readonly StaffService _staffService;
    private readonly SerialService _serialService;

    [ObservableProperty]
    private string _statusText = "Ready";

    [ObservableProperty]
    private Staff? _currentStaff;

    [ObservableProperty]
    private string _currentTill = "A";

    [ObservableProperty]
    private int _selectedTabIndex;

    [ObservableProperty]
    private string _staffOverrideBarcode = "";

    [ObservableProperty]
    private string _staffOverrideStatusMessage = "";

    [ObservableProperty]
    private bool _isStaffAdminUnlocked;

    private int _lastInvoiceId;
    private int _nextSaleTabNumber = 1;

    // Multiple independent sales can be open at once (a communal till: a large sale can
    // sit untouched while a different staff member rings up something else on another
    // tab, matching how the legacy app's tab system worked - see ROADMAP notes). Each tab
    // strip entry is a fully independent SaleViewModel; there is always at least one.
    public ObservableCollection<SaleViewModel> OpenSales { get; } = new();

    [ObservableProperty]
    private SaleViewModel? _activeSaleDocument;

    // Only shown once there's a second sale open, so the common single-sale case looks
    // exactly as it always has - no tab-strip clutter for the 95% case.
    public bool HasMultipleSaleTabs => OpenSales.Count > 1;

    // This is a communal till shared by many staff across a shift - ordinary sales/stock/
    // customer/reports access has no login gate at all, matching real POS practice (staff
    // attribute each individual sale via the barcode field on the Sale tab instead, see
    // SaleViewModel.ProcessStaffNumber). Only the Staff admin area requires a manager
    // override, checked at the point of access rather than via a persistent session.
    // A PIN or other stronger override auth is deferred - barcode/staff-number entry is
    // the mechanism for now.

    partial void OnSelectedTabIndexChanged(int value)
    {
        _ = LoadTabDataAsync(value);
    }

    public CustomerViewModel CustomerViewModel { get; }
    public StockViewModel StockViewModel { get; }
    public ReportsViewModel ReportsViewModel { get; }
    public StaffViewModel StaffViewModel { get; }
    public StocktakeViewModel StocktakeViewModel { get; }
    public GoodsReceivedViewModel GoodsReceivedViewModel { get; }
    public ReturnAuthorizationViewModel ReturnAuthorizationViewModel { get; }
    public JobViewModel JobViewModel { get; }
    public ReferenceDataViewModel GoodsTypesViewModel { get; }
    public ReferenceDataViewModel BrandsViewModel { get; }
    public ReferenceDataViewModel SymptomsViewModel { get; }
    public ReferenceDataViewModel TaskTypesViewModel { get; }

    [ObservableProperty]
    private int _selectedReferenceSubTabIndex;

    public MainWindowViewModel()
    {
        DatabaseService.LoadEnvironment();
        _dbService = new DatabaseService();
        _stockService = new StockService(_dbService);
        _customerService = new CustomerService(_dbService);
        _staffService = new StaffService(_dbService);
        _serialService = new SerialService(_dbService);

        // Create ViewModels
        CustomerViewModel = new CustomerViewModel(_customerService);
        StockViewModel = new StockViewModel(_stockService);
        ReportsViewModel = new ReportsViewModel(_dbService, _stockService, _customerService, _staffService);
        TransactionLookupViewModel = new TransactionLookupViewModel(_dbService, _customerService, _staffService);
        var smsService = new SmsService(_dbService);
        var emailService = new EmailService(_dbService);
        StaffViewModel = new StaffViewModel(_staffService, smsService, emailService);
        StocktakeViewModel = new StocktakeViewModel(new StocktakeService(_dbService, _stockService), _staffService);
        var supplierService = new SupplierService(_dbService);
        GoodsReceivedViewModel = new GoodsReceivedViewModel(
            new GoodsReceivedService(_dbService), supplierService, _stockService, _staffService);
        ReturnAuthorizationViewModel = new ReturnAuthorizationViewModel(
            new ReturnAuthorizationService(_dbService), _stockService, supplierService, _customerService, _staffService);
        JobViewModel = new JobViewModel(new JobService(_dbService), _customerService, _staffService, _stockService, smsService, emailService, new JobTimeService(_dbService));
        var referenceDataService = new ReferenceDataService(_dbService);
        GoodsTypesViewModel = new ReferenceDataViewModel(referenceDataService, ReferenceTables.GoodsTypes, "Goods Accepted Types");
        BrandsViewModel = new ReferenceDataViewModel(referenceDataService, ReferenceTables.Brands, "Brands");
        SymptomsViewModel = new ReferenceDataViewModel(referenceDataService, ReferenceTables.Symptoms, "Problem Symptoms");
        TaskTypesViewModel = new ReferenceDataViewModel(referenceDataService, ReferenceTables.TaskTypes, "Task Types");

        // Clicking a ticket number on the Customer screen's Tickets sub-tab jumps to the
        // Tickets tab and opens that job - CustomerViewModel has no reference to the tab
        // strip or JobViewModel itself, so it just raises an event for this to handle.
        CustomerViewModel.TicketOpened += async jobId =>
        {
            SelectedTabIndex = 9;
            await JobViewModel.OpenJobByIdAsync(jobId);
        };

        OpenSales.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasMultipleSaleTabs));
        ActiveSaleDocument = CreateSaleDocument();

        StatusText = "Ready";
    }

    private SaleViewModel CreateSaleDocument()
    {
        var doc = new SaleViewModel(_dbService, _stockService, _customerService, _staffService)
        {
            TabNumber = _nextSaleTabNumber++
        };

        doc.SaleCommitted += invoiceId => _lastInvoiceId = invoiceId;
        // Status bar mirrors whoever is attributed to the *currently active* tab only -
        // display only, this drives no access control (see IsStaffAdminUnlocked instead).
        doc.StaffChanged += staff =>
        {
            if (doc == ActiveSaleDocument)
                CurrentStaff = staff;
        };

        OpenSales.Add(doc);
        return doc;
    }

    // Status-bar "N timers running" indicator's click-through, visible from any tab -
    // jump to Tickets and show the filtered "jobs with a running timer" list.
    [RelayCommand]
    private async Task ShowRunningTimers()
    {
        SelectedTabIndex = 9;
        await JobViewModel.ShowJobsWithRunningTimersCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private void NewSaleTab()
    {
        ActiveSaleDocument = CreateSaleDocument();
        SelectedTabIndex = 0;
        StatusText = "Starting new sale...";
    }

    [RelayCommand]
    private void SelectSaleTab(SaleViewModel? doc)
    {
        if (doc == null)
            return;
        ActiveSaleDocument = doc;
    }

    [RelayCommand]
    private void CloseSaleTab(SaleViewModel? doc)
    {
        if (doc == null)
            return;

        if (doc.SaleItems.Count > 0)
        {
            StatusText = "Hold or complete this sale before closing its tab";
            return;
        }

        int idx = OpenSales.IndexOf(doc);
        OpenSales.Remove(doc);

        if (OpenSales.Count == 0)
        {
            // Always keep at least one sale document open.
            ActiveSaleDocument = CreateSaleDocument();
        }
        else if (ActiveSaleDocument == doc)
        {
            ActiveSaleDocument = OpenSales[Math.Max(0, idx - 1)];
        }
    }

    partial void OnActiveSaleDocumentChanged(SaleViewModel? oldValue, SaleViewModel? newValue)
    {
        if (oldValue != null)
            oldValue.IsActive = false;
        if (newValue != null)
            newValue.IsActive = true;
        CurrentStaff = newValue?.CurrentStaff;
    }

    public string StaffInfo => CurrentStaff != null 
        ? $"Staff: {CurrentStaff.DocketName}" 
        : "Staff: Not signed in";

    public string TillInfo => $"Till: {CurrentTill}";

    public TransactionLookupViewModel TransactionLookupViewModel { get; }

    public StockService StockService => _stockService;
    public CustomerService CustomerService => _customerService;
    public StaffService StaffService => _staffService;
    public SerialService SerialService => _serialService;
    public DatabaseService DatabaseService => _dbService;

    [RelayCommand]
    private void Exit()
    {
        Environment.Exit(0);
    }


    [RelayCommand]
    private async Task StockList()
    {
        StatusText = "Loading stock list...";
        SelectedTabIndex = 1;
        await StockViewModel.LoadStockAsync();
    }

    [RelayCommand]
    private async Task CustomerList()
    {
        StatusText = "Loading customer list...";
        SelectedTabIndex = 2;
        await CustomerViewModel.LoadCustomersAsync();
    }

    [RelayCommand]
    private void Reports()
    {
        StatusText = "Select a report to run...";
        SelectedTabIndex = 4;
    }

    [RelayCommand]
    private void About()
    {
        StatusText = "JobMatix POS v8.0 - .NET 8 + Avalonia UI + PostgreSQL";
    }

    [RelayCommand]
    private void HoldSale()
    {
        SelectedTabIndex = 0;
        if (ActiveSaleDocument == null)
            return;
        ActiveSaleDocument.HoldSaleCommand.Execute(null);
        StatusText = ActiveSaleDocument.StatusMessage;
    }

    [RelayCommand]
    private async Task ShowLastInvoice()
    {
        if (_lastInvoiceId == 0)
        {
            StatusText = "No invoice committed yet this session";
            return;
        }

        SelectedTabIndex = 5; // Transactions tab
        StatusText = $"Looking up invoice #{_lastInvoiceId}...";
        await TransactionLookupViewModel.SelectInvoiceByIdAsync(_lastInvoiceId);
        StatusText = TransactionLookupViewModel.StatusMessage;
    }

    [RelayCommand]
    private async Task FindStock()
    {
        StatusText = "Loading stock list...";
        SelectedTabIndex = 1;
        await StockViewModel.LoadStockAsync();
    }

    [RelayCommand]
    private void NewStockItem()
    {
        SelectedTabIndex = 1;
        StockViewModel.NewStockCommand.Execute(null);
        StatusText = StockViewModel.StatusMessage;
    }

    [RelayCommand]
    private async Task FindCustomer()
    {
        StatusText = "Loading customer list...";
        SelectedTabIndex = 2;
        await CustomerViewModel.LoadCustomersAsync();
    }

    [RelayCommand]
    private void NewCustomerItem()
    {
        SelectedTabIndex = 2;
        CustomerViewModel.NewCustomerCommand.Execute(null);
        StatusText = CustomerViewModel.StatusMessage;
    }

    [RelayCommand]
    private async Task SalesReport()
    {
        SelectedTabIndex = 4;
        await ReportsViewModel.RunDailySalesReportCommand.ExecuteAsync(null);
        StatusText = ReportsViewModel.StatusMessage;
    }

    [RelayCommand]
    private async Task StockReport()
    {
        SelectedTabIndex = 4;
        await ReportsViewModel.RunStockValueReportCommand.ExecuteAsync(null);
        StatusText = ReportsViewModel.StatusMessage;
    }

    [RelayCommand]
    private async Task CustomerReport()
    {
        SelectedTabIndex = 4;
        await ReportsViewModel.RunCustomerAccountsReportCommand.ExecuteAsync(null);
        StatusText = ReportsViewModel.StatusMessage;
    }

    [RelayCommand]
    private void StaffList()
    {
        SelectedTabIndex = 3;
    }

    [RelayCommand]
    private void TransactionLookup()
    {
        SelectedTabIndex = 5;
    }

    [RelayCommand]
    private async Task Cashup()
    {
        SelectedTabIndex = 4;
        await ReportsViewModel.LoadCashupCommand.ExecuteAsync(null);
        StatusText = ReportsViewModel.CashupStatusMessage;
    }

    [RelayCommand]
    private async Task Stocktake()
    {
        SelectedTabIndex = 6;
        StatusText = "Loading open stocktakes...";
        await StocktakeViewModel.LoadSessionsAsync();
        StatusText = $"Stocktakes loaded: {StocktakeViewModel.Sessions.Count} open";
    }

    [RelayCommand]
    private async Task GoodsReceived()
    {
        SelectedTabIndex = 7;
        StatusText = "Loading recent goods received...";
        await GoodsReceivedViewModel.LoadRecentAsync();
        StatusText = $"Goods received loaded: {GoodsReceivedViewModel.RecentReceipts.Count} recent";
    }

    [RelayCommand]
    private async Task ReturnAuthorizations()
    {
        SelectedTabIndex = 8;
        StatusText = "Loading open return authorisations...";
        await ReturnAuthorizationViewModel.LoadOpenRAsAsync();
        StatusText = $"RAs loaded: {ReturnAuthorizationViewModel.OpenRAs.Count} open";
    }

    [RelayCommand]
    private async Task Jobs()
    {
        SelectedTabIndex = 9;
        StatusText = "Loading open jobs...";
        await JobViewModel.LoadOpenJobsAsync();
        StatusText = $"Jobs loaded: {JobViewModel.OpenJobs.Count} open";
    }

    private async Task OpenReferenceData(int subTabIndex)
    {
        SelectedTabIndex = 10;
        SelectedReferenceSubTabIndex = subTabIndex;
        StatusText = "Loading reference data...";
        await GoodsTypesViewModel.LoadAsync();
        await BrandsViewModel.LoadAsync();
        await SymptomsViewModel.LoadAsync();
        await TaskTypesViewModel.LoadAsync();
        StatusText = "Reference data loaded";
    }

    [RelayCommand]
    private Task OpenGoodsTypes() => OpenReferenceData(0);

    [RelayCommand]
    private Task OpenBrands() => OpenReferenceData(1);

    [RelayCommand]
    private Task OpenSymptoms() => OpenReferenceData(2);

    [RelayCommand]
    private Task OpenTaskTypes() => OpenReferenceData(3);

    [RelayCommand]
    private void NewStaffItem()
    {
        SelectedTabIndex = 3;
        if (IsStaffAdminUnlocked)
        {
            StaffViewModel.NewStaffCommand.Execute(null);
            StatusText = StaffViewModel.StatusMessage;
        }
    }

    partial void OnCurrentStaffChanged(Staff? value)
    {
        OnPropertyChanged(nameof(StaffInfo));
    }

    // Manager override for the Staff admin area - checked at the point of access, not tied
    // to whoever is currently attributed to the sale in progress (deliberately independent:
    // the last person to ring up a sale might not be who's authorizing this).
    [RelayCommand]
    private async Task UnlockStaffAdmin()
    {
        if (string.IsNullOrWhiteSpace(StaffOverrideBarcode))
            return;

        var staff = await _staffService.FindStaffByBarcodeAsync(StaffOverrideBarcode.Trim());
        if (staff == null)
        {
            StaffOverrideStatusMessage = $"Staff not found for '{StaffOverrideBarcode}'";
            return;
        }

        if (!staff.IsAdministrator)
        {
            StaffOverrideStatusMessage = $"{staff.DocketName} is not an administrator";
            return;
        }

        IsStaffAdminUnlocked = true;
        StaffOverrideBarcode = "";
        StaffOverrideStatusMessage = "";
        StatusText = $"Staff admin unlocked by {staff.DocketName}";
        await StaffViewModel.LoadStaffAsync();
        await StaffViewModel.LoadSmsSettingsAsync();
        await StaffViewModel.LoadEmailSettingsAsync();
    }

    [RelayCommand]
    private void LockStaffAdmin()
    {
        IsStaffAdminUnlocked = false;
        SelectedTabIndex = 0;
        StatusText = "Staff admin locked";
    }

    partial void OnCurrentTillChanged(string value)
    {
        OnPropertyChanged(nameof(TillInfo));
    }

    private async Task LoadTabDataAsync(int tabIndex)
    {
        try
        {
            switch (tabIndex)
            {
                case 0: // Sale tab
                    // Sale tab doesn't need auto-load
                    break;
                case 1: // Stock tab
                    StatusText = "Loading stock...";
                    await StockViewModel.LoadStockAsync();
                    StatusText = $"Stock loaded: {StockViewModel.StockItems.Count} items";
                    break;
                case 2: // Customers tab
                    StatusText = "Loading customers...";
                    await CustomerViewModel.LoadCustomersAsync();
                    StatusText = $"Customers loaded: {CustomerViewModel.Customers.Count} records";
                    break;
                case 3: // Staff tab
                    if (IsStaffAdminUnlocked)
                    {
                        StatusText = "Loading staff...";
                        await StaffViewModel.LoadStaffAsync();
                        await StaffViewModel.LoadSmsSettingsAsync();
                        await StaffViewModel.LoadEmailSettingsAsync();
                        StatusText = $"Staff loaded: {StaffViewModel.StaffMembers.Count} records";
                    }
                    break;
                case 4: // Reports tab
                    StatusText = "Select a report to run...";
                    break;
                case 6: // Stocktake tab
                    StatusText = "Loading open stocktakes...";
                    await StocktakeViewModel.LoadSessionsAsync();
                    StatusText = $"Stocktakes loaded: {StocktakeViewModel.Sessions.Count} open";
                    break;
                case 7: // Goods Received tab
                    StatusText = "Loading recent goods received...";
                    await GoodsReceivedViewModel.LoadRecentAsync();
                    StatusText = $"Goods received loaded: {GoodsReceivedViewModel.RecentReceipts.Count} recent";
                    break;
                case 8: // Return Authorisations tab
                    StatusText = "Loading open return authorisations...";
                    await ReturnAuthorizationViewModel.LoadOpenRAsAsync();
                    StatusText = $"RAs loaded: {ReturnAuthorizationViewModel.OpenRAs.Count} open";
                    break;
                case 9: // Jobs tab
                    StatusText = "Loading open jobs...";
                    await JobViewModel.LoadOpenJobsAsync();
                    StatusText = $"Jobs loaded: {JobViewModel.OpenJobs.Count} open";
                    break;
                case 10: // Reference Data tab
                    StatusText = "Loading reference data...";
                    await GoodsTypesViewModel.LoadAsync();
                    await BrandsViewModel.LoadAsync();
                    await SymptomsViewModel.LoadAsync();
                    await TaskTypesViewModel.LoadAsync();
                    StatusText = "Reference data loaded";
                    break;
            }
        }
        catch (Exception ex)
        {
            StatusText = $"Error loading data: {ex.Message}";
        }
    }

}
