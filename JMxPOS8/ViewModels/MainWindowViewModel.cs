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

    [ObservableProperty]
    private string _statusText = "Ready";

    [ObservableProperty]
    private Staff? _currentStaff;

    [ObservableProperty]
    private string _currentTill = "A";

    [ObservableProperty]
    private int _selectedTabIndex;

    partial void OnSelectedTabIndexChanged(int value)
    {
        _ = LoadTabDataAsync(value);
    }

    public SaleViewModel SaleViewModel { get; }
    public CustomerViewModel CustomerViewModel { get; }
    public StockViewModel StockViewModel { get; }
    public ReportsViewModel ReportsViewModel { get; }

    public MainWindowViewModel()
    {
        DatabaseService.LoadEnvironment();
        _dbService = new DatabaseService();
        _stockService = new StockService(_dbService);
        _customerService = new CustomerService(_dbService);
        _staffService = new StaffService(_dbService);

        // Create ViewModels
        SaleViewModel = new SaleViewModel(_dbService, _stockService, _customerService, _staffService);
        CustomerViewModel = new CustomerViewModel(_customerService);
        StockViewModel = new StockViewModel(_stockService);
        ReportsViewModel = new ReportsViewModel(_dbService, _stockService, _customerService);
        ReportsViewModel = new ReportsViewModel(_dbService, _stockService, _customerService);

        LoadTestData();
    }

    public string StaffInfo => CurrentStaff != null 
        ? $"Staff: {CurrentStaff.DocketName}" 
        : "Staff: Not signed in";

    public string TillInfo => $"Till: {CurrentTill}";

    public StockService StockService => _stockService;
    public CustomerService CustomerService => _customerService;
    public StaffService StaffService => _staffService;
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
        SelectedTabIndex = 3;
    }

    [RelayCommand]
    private void About()
    {
        StatusText = "JobMatix POS v8.0 - .NET 8 + Avalonia UI + PostgreSQL";
    }

    partial void OnCurrentStaffChanged(Staff? value)
    {
        OnPropertyChanged(nameof(StaffInfo));
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
                case 3: // Reports tab
                    StatusText = "Select a report to run...";
                    break;
            }
        }
        catch (Exception ex)
        {
            StatusText = $"Error loading data: {ex.Message}";
        }
    }

    private async void LoadTestData()
    {
        try
        {
            // Try to load first staff member (staff_id = 1)
            var staff = await _staffService.GetStaffByIdAsync(1);
            if (staff != null)
            {
                CurrentStaff = staff;
                StatusText = $"Ready - Staff #{staff.StaffId} {staff.DocketName}";
            }
            else
            {
                StatusText = "Ready - Please sign in";
            }
        }
        catch (Exception ex)
        {
            StatusText = $"Ready - {ex.Message}";
        }
    }
}
