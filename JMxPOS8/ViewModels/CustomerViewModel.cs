using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class CustomerViewModel : ViewModelBase
{
    private readonly CustomerService _customerService;

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = new();

    [ObservableProperty]
    private Customer? _selectedCustomer;

    public ObservableCollection<CustomerInvoiceSummary> Invoices { get; } = new();
    public ObservableCollection<CustomerItemSaleSummary> ItemSales { get; } = new();
    public ObservableCollection<CustomerPaymentSummary> Payments { get; } = new();
    public ObservableCollection<CustomerInvoiceSummary> Quotes { get; } = new();
    public ObservableCollection<CustomerJobSummary> Jobs { get; } = new();

    // Raised when a ticket number is clicked on the Tickets sub-tab - MainWindowViewModel
    // subscribes to switch to the Tickets tab and load that job, since this ViewModel has
    // no reference to the main tab strip or JobViewModel itself.
    public event Action<int>? TicketOpened;

    // Same cross-nav pattern for invoices (direct feedback, 2026-09-01: "on the customer
    // tab the invoices should link to an invoice like the tickets... item sales should also
    // link to the corresponding invoice, payments should reference which invoice it was
    // made against for reconciliation") - MainWindowViewModel subscribes to switch to the
    // Transactions tab and load that invoice there, since this ViewModel has no reference
    // to the main tab strip or TransactionLookupViewModel itself.
    public event Action<int>? InvoiceOpened;

    [RelayCommand]
    private void OpenJob(CustomerJobSummary? job)
    {
        if (job != null)
            TicketOpened?.Invoke(job.JobId);
    }

    [RelayCommand]
    private void OpenInvoice(int invoiceId)
    {
        if (invoiceId > 0)
            InvoiceOpened?.Invoke(invoiceId);
    }

    partial void OnSelectedCustomerChanged(Customer? value)
    {
        IsDeleteArmed = false; // switching records disarms any pending delete confirmation
        if (value != null && !IsEditing)
        {
            LoadCustomerToForm(value);
            StatusMessage = $"Viewing: {value.CustomerName}";
            _ = LoadCustomerHistoryAsync(value.CustomerId);
        }
    }

    private async Task LoadCustomerHistoryAsync(int customerId)
    {
        Invoices.Clear();
        ItemSales.Clear();
        Payments.Clear();
        Quotes.Clear();
        Jobs.Clear();

        try
        {
            foreach (var i in await _customerService.GetCustomerInvoicesAsync(customerId)) Invoices.Add(i);
            foreach (var i in await _customerService.GetCustomerItemSalesAsync(customerId)) ItemSales.Add(i);
            foreach (var p in await _customerService.GetCustomerPaymentsAsync(customerId)) Payments.Add(p);
            foreach (var q in await _customerService.GetCustomerQuotesAsync(customerId)) Quotes.Add(q);
            foreach (var j in await _customerService.GetCustomerJobsAsync(customerId)) Jobs.Add(j);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading customer history: {ex.Message}";
        }
    }

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    // Customer detail fields for add/edit
    [ObservableProperty]
    private bool _isEditing;

    [ObservableProperty]
    private string _barcode = string.Empty;

    [ObservableProperty]
    private string _customerName = string.Empty;

    [ObservableProperty]
    private string _companyName = string.Empty;

    [ObservableProperty]
    private string _grade = string.Empty;

    [ObservableProperty]
    private bool _inactive;

    [ObservableProperty]
    private string _contactName = string.Empty;

    [ObservableProperty]
    private string _contactPosition = string.Empty;

    [ObservableProperty]
    private string _address = string.Empty;

    [ObservableProperty]
    private string _suburb = string.Empty;

    [ObservableProperty]
    private string _state = string.Empty;

    [ObservableProperty]
    private string _postcode = string.Empty;

    [ObservableProperty]
    private string _country = string.Empty;

    [ObservableProperty]
    private string _businessPhone = string.Empty;

    [ObservableProperty]
    private string _homePhone = string.Empty;

    [ObservableProperty]
    private string _fax = string.Empty;

    [ObservableProperty]
    private string _mobile = string.Empty;

    [ObservableProperty]
    private string _emailAddress = string.Empty;

    [ObservableProperty]
    private string _website = string.Empty;

    [ObservableProperty]
    private string _abn = string.Empty;

    [ObservableProperty]
    private string _taxCode = string.Empty;

    [ObservableProperty]
    private bool _isAccount;

    [ObservableProperty]
    private decimal _accountBalance;

    [ObservableProperty]
    private decimal _creditLimit;

    [ObservableProperty]
    private string _notes = string.Empty;

    private int _editingCustomerId;

    public CustomerViewModel(CustomerService customerService)
    {
        _customerService = customerService;
    }

    partial void OnSearchTextChanged(string value)
    {
        _ = SearchCustomersAsync();
    }

    [RelayCommand]
    public async Task LoadCustomersAsync()
    {
        try
        {
            StatusMessage = "Loading customers...";
            Console.WriteLine("[LOAD CUSTOMERS] Starting...");
            var customers = await _customerService.GetAllCustomersAsync();
            Console.WriteLine($"[LOAD CUSTOMERS] Fetched {customers.Count} customers from database");
            
            await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
            {
                Console.WriteLine($"[LOAD CUSTOMERS] Setting new collection");
                Customers = new ObservableCollection<Customer>(customers);
                Console.WriteLine($"[LOAD CUSTOMERS] Collection now has {Customers.Count} customers");
            });
            
            StatusMessage = $"Loaded {Customers.Count} customers";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LOAD CUSTOMERS ERROR] {ex.Message}");
            StatusMessage = $"Error loading customers: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task SearchCustomersAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadCustomersAsync();
                return;
            }

            StatusMessage = "Searching...";
            var customers = await _customerService.SearchCustomersAsync(SearchText);
            
            Customers.Clear();
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
            
            StatusMessage = $"Found {Customers.Count} customers";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error searching: {ex.Message}";
        }
    }

    [RelayCommand]
    private void NewCustomer()
    {
        ClearForm();
        IsEditing = true;
        _editingCustomerId = 0;
        StatusMessage = "Enter customer details";
    }

    [RelayCommand]
    private void EditCustomer()
    {
        if (SelectedCustomer == null)
        {
            StatusMessage = "Please select a customer to edit";
            return;
        }

        LoadCustomerToForm(SelectedCustomer);
        IsEditing = true;
        StatusMessage = "Editing customer";
    }

    [RelayCommand]
    private async Task SaveCustomerAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Barcode))
            {
                StatusMessage = "Barcode is required";
                return;
            }

            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                StatusMessage = "Customer name is required";
                return;
            }

            var customer = new Customer
            {
                CustomerId = _editingCustomerId,
                Barcode = Barcode.Trim(),
                CustomerName = CustomerName.Trim(),
                CompanyName = CompanyName.Trim(),
                Grade = Grade.Trim(),
                Inactive = Inactive,
                ContactName = ContactName.Trim(),
                ContactPosition = ContactPosition.Trim(),
                Address = Address.Trim(),
                Suburb = Suburb.Trim(),
                State = State.Trim(),
                Postcode = Postcode.Trim(),
                Country = Country.Trim(),
                BusinessPhone = BusinessPhone.Trim(),
                HomePhone = HomePhone.Trim(),
                Fax = Fax.Trim(),
                Mobile = Mobile.Trim(),
                EmailAddress = EmailAddress.Trim(),
                Website = Website.Trim(),
                Abn = Abn.Trim(),
                TaxCode = TaxCode.Trim(),
                IsAccount = IsAccount,
                AccountBalance = AccountBalance,
                CreditLimit = CreditLimit,
                Notes = Notes.Trim()
            };

            if (_editingCustomerId == 0)
            {
                await _customerService.AddCustomerAsync(customer);
                StatusMessage = "Customer added successfully";
            }
            else
            {
                await _customerService.UpdateCustomerAsync(customer);
                StatusMessage = "Customer updated successfully";
            }

            IsEditing = false;
            await LoadCustomersAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error saving customer: {ex.Message}";
        }
    }

    [RelayCommand]
    private void CancelEdit()
    {
        IsEditing = false;
        ClearForm();
        StatusMessage = "Edit cancelled";
    }

    // A misclick here was a real risk - one click, no confirmation, straight to
    // DeleteCustomerAsync (direct feedback, 2026-09-01). First click just arms it and
    // swaps the button for a "Confirm Delete?" one; only a second, deliberate click
    // actually deletes. Switching the selected customer disarms it again.
    [ObservableProperty]
    private bool _isDeleteArmed;

    [RelayCommand]
    private void ArmDeleteCustomer()
    {
        if (SelectedCustomer == null)
        {
            StatusMessage = "Please select a customer to delete";
            return;
        }
        IsDeleteArmed = true;
        StatusMessage = $"Click 'Confirm Delete?' to deactivate {SelectedCustomer.CustomerName} - click elsewhere to cancel";
    }

    [RelayCommand]
    private async Task DeleteCustomerAsync()
    {
        if (SelectedCustomer == null)
        {
            StatusMessage = "Please select a customer to delete";
            return;
        }

        try
        {
            await _customerService.DeleteCustomerAsync(SelectedCustomer.CustomerId);
            StatusMessage = "Customer deleted successfully";
            IsDeleteArmed = false;
            await LoadCustomersAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error deleting customer: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        SearchText = string.Empty;
        await LoadCustomersAsync();
    }

    private void LoadCustomerToForm(Customer customer)
    {
        _editingCustomerId = customer.CustomerId;
        Barcode = customer.Barcode;
        CustomerName = customer.CustomerName;
        CompanyName = customer.CompanyName;
        Grade = customer.Grade;
        Inactive = customer.Inactive;
        ContactName = customer.ContactName;
        ContactPosition = customer.ContactPosition;
        Address = customer.Address;
        Suburb = customer.Suburb;
        State = customer.State;
        Postcode = customer.Postcode;
        Country = customer.Country;
        BusinessPhone = customer.BusinessPhone;
        HomePhone = customer.HomePhone;
        Fax = customer.Fax;
        Mobile = customer.Mobile;
        EmailAddress = customer.EmailAddress;
        Website = customer.Website;
        Abn = customer.Abn;
        TaxCode = customer.TaxCode;
        IsAccount = customer.IsAccount;
        AccountBalance = customer.AccountBalance;
        CreditLimit = customer.CreditLimit;
        Notes = customer.Notes;
    }

    private void ClearForm()
    {
        _editingCustomerId = 0;
        Barcode = string.Empty;
        CustomerName = string.Empty;
        CompanyName = string.Empty;
        Grade = string.Empty;
        Inactive = false;
        ContactName = string.Empty;
        ContactPosition = string.Empty;
        Address = string.Empty;
        Suburb = string.Empty;
        State = string.Empty;
        Postcode = string.Empty;
        Country = "Australia";
        BusinessPhone = string.Empty;
        HomePhone = string.Empty;
        Fax = string.Empty;
        Mobile = string.Empty;
        EmailAddress = string.Empty;
        Website = string.Empty;
        Abn = string.Empty;
        TaxCode = "GST";
        IsAccount = false;
        AccountBalance = 0;
        CreditLimit = 0;
        Notes = string.Empty;
    }
}
