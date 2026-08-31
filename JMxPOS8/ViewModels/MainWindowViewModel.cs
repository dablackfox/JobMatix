using System;
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

    public SaleViewModel SaleViewModel { get; }
    public CustomerViewModel CustomerViewModel { get; }
    public StockViewModel StockViewModel { get; }
    public ReportsViewModel ReportsViewModel { get; }
    public StaffViewModel StaffViewModel { get; }

    public MainWindowViewModel()
    {
        DatabaseService.LoadEnvironment();
        _dbService = new DatabaseService();
        _stockService = new StockService(_dbService);
        _customerService = new CustomerService(_dbService);
        _staffService = new StaffService(_dbService);
        _serialService = new SerialService(_dbService);

        // Create ViewModels
        SaleViewModel = new SaleViewModel(_dbService, _stockService, _customerService, _staffService);
        CustomerViewModel = new CustomerViewModel(_customerService);
        StockViewModel = new StockViewModel(_stockService);
        ReportsViewModel = new ReportsViewModel(_dbService, _stockService, _customerService);
        TransactionLookupViewModel = new TransactionLookupViewModel(_dbService, _customerService, _staffService);
        StaffViewModel = new StaffViewModel(_staffService);

        SaleViewModel.SaleCommitted += invoiceId => _lastInvoiceId = invoiceId;
        // Status bar mirrors whoever is currently attributed to the sale in progress -
        // display only, this drives no access control (see IsStaffAdminUnlocked instead).
        SaleViewModel.StaffChanged += staff => CurrentStaff = staff;

        StatusText = "Ready";
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
    private void NewSale()
    {
        StatusText = "Starting new sale...";
        SelectedTabIndex = 0;
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
        SaleViewModel.HoldSaleCommand.Execute(null);
        StatusText = SaleViewModel.StatusMessage;
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
                        StatusText = $"Staff loaded: {StaffViewModel.StaffMembers.Count} records";
                    }
                    break;
                case 4: // Reports tab
                    StatusText = "Select a report to run...";
                    break;
            }
        }
        catch (Exception ex)
        {
            StatusText = $"Error loading data: {ex.Message}";
        }
    }

}
