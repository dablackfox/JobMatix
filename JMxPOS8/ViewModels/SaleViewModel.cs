using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels
{
    public partial class SaleViewModel : ViewModelBase
    {
        private readonly SaleService _saleService;
        private readonly StockService _stockService;
        private readonly CustomerService _customerService;
        private readonly StaffService _staffService;

        [ObservableProperty]
        private string _staffNumber = "";

        [ObservableProperty]
        private string _customerBarcode = "";

        [ObservableProperty]
        private string _itemBarcode = "";

        [ObservableProperty]
        private string _itemDescription = "";

        [ObservableProperty]
        private decimal _itemQuantity = 1;

        [ObservableProperty]
        private decimal _itemPrice = 0;

        [ObservableProperty]
        private decimal _itemExtension = 0;

        [ObservableProperty]
        private Staff? _currentStaff;

        [ObservableProperty]
        private Customer? _currentCustomer;

        [ObservableProperty]
        private string _transactionType = "Sale";

        [ObservableProperty]
        private decimal _discountAmount = 0;

        [ObservableProperty]
        private string _statusMessage = "";

        public ObservableCollection<SaleLineItem> SaleItems { get; }

        public SaleViewModel(DatabaseService dbService, StockService stockService, 
                            CustomerService customerService, StaffService staffService)
        {
            _stockService = stockService;
            _customerService = customerService;
            _staffService = staffService;
            _saleService = new SaleService(dbService, stockService, customerService);
            
            SaleItems = _saleService.SaleItems;
            
            // Subscribe to collection changes to update totals
            SaleItems.CollectionChanged += (s, e) => UpdateTotals();
        }

        public string CustomerInfo => CurrentCustomer != null 
            ? $"{CurrentCustomer.CustomerName} - Balance: ${CurrentCustomer.AccountBalance:F2}" 
            : "Walk-in Customer";

        public decimal SubtotalEx => _saleService.SubtotalEx;
        public decimal TaxAmount => _saleService.TaxAmount;
        public decimal TotalInc => _saleService.TotalInc;
        public decimal AmountDue => _saleService.AmountDue;
        public decimal TotalPaid => _saleService.TotalPaid;
        public decimal Change => _saleService.Change;

        [RelayCommand]
        private async Task ProcessStaffNumber()
        {
            if (string.IsNullOrWhiteSpace(StaffNumber))
                return;

            try
            {
                if (int.TryParse(StaffNumber.Trim(), out int staffId))
                {
                    var staff = await _staffService.GetStaffByIdAsync(staffId);
                    if (staff != null)
                    {
                        CurrentStaff = staff;
                        _saleService.SetStaff(staff);
                        StatusMessage = $"Staff: {staff.DocketName}";
                    }
                    else
                    {
                        StatusMessage = "Staff not found!";
                    }
                }
                else
                {
                    StatusMessage = "Staff number must be numeric";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task ProcessCustomerBarcode()
        {
            if (string.IsNullOrWhiteSpace(CustomerBarcode))
                return;

            try
            {
                var customer = await _customerService.FindCustomerByBarcodeAsync(CustomerBarcode.Trim());
                if (customer != null)
                {
                    CurrentCustomer = customer;
                    _saleService.SetCustomer(customer);
                    OnPropertyChanged(nameof(CustomerInfo));
                    StatusMessage = $"Customer: {customer.CustomerName}";
                }
                else
                {
                    StatusMessage = "Customer not found!";
                    CurrentCustomer = null;
                    _saleService.SetCustomer(null!);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task ProcessItemBarcode()
        {
            if (string.IsNullOrWhiteSpace(ItemBarcode))
                return;

            try
            {
                var stock = await _stockService.FindStockByBarcodeAsync(ItemBarcode.Trim());
                if (stock != null)
                {
                    ItemDescription = stock.Description;
                    ItemPrice = stock.SellPrice;
                    ItemExtension = ItemQuantity * ItemPrice;
                    StatusMessage = $"Item found: {stock.Description}";
                    
                    // Auto-add the item
                    await AddItem();
                }
                else
                {
                    StatusMessage = "Item not found!";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task AddItem()
        {
            if (string.IsNullOrWhiteSpace(ItemBarcode))
            {
                StatusMessage = "Please scan or enter an item barcode";
                return;
            }

            try
            {
                bool added = await _saleService.AddItemByBarcodeAsync(ItemBarcode.Trim(), ItemQuantity);
                if (added)
                {
                    StatusMessage = $"Added: {ItemDescription}";
                    ClearItemEntry();
                    UpdateTotals();
                }
                else
                {
                    StatusMessage = "Failed to add item";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        private void SetTransactionType(string type)
        {
            TransactionType = type;
            _saleService.TransactionType = type;
            StatusMessage = $"Transaction type: {type}";
        }

        [RelayCommand]
        private void AddCashPayment()
        {
            if (AmountDue <= 0)
            {
                StatusMessage = "No amount due";
                return;
            }

            _saleService.AddPayment("CASH", AmountDue);
            UpdateTotals();
            StatusMessage = $"Cash payment added: ${AmountDue:F2}";
        }

        [RelayCommand]
        private void AddEftposPayment()
        {
            if (AmountDue <= 0)
            {
                StatusMessage = "No amount due";
                return;
            }

            _saleService.AddPayment("EFTPOS", AmountDue);
            UpdateTotals();
            StatusMessage = $"EFTPOS payment added: ${AmountDue:F2}";
        }

        [RelayCommand]
        private void AddCreditCardPayment()
        {
            if (AmountDue <= 0)
            {
                StatusMessage = "No amount due";
                return;
            }

            _saleService.AddPayment("CREDIT_CARD", AmountDue);
            UpdateTotals();
            StatusMessage = $"Credit card payment added: ${AmountDue:F2}";
        }

        [RelayCommand]
        private void ChargeToAccount()
        {
            if (CurrentCustomer == null || !CurrentCustomer.IsAccount)
            {
                StatusMessage = "Customer must be an account customer";
                return;
            }

            StatusMessage = "Sale will be charged to account";
        }

        [RelayCommand]
        private async Task CommitSale()
        {
            if (CurrentStaff == null)
            {
                StatusMessage = "Please sign in staff first";
                return;
            }

            if (SaleItems.Count == 0)
            {
                StatusMessage = "No items in sale";
                return;
            }

            try
            {
                _saleService.DiscountAmount = DiscountAmount;
                int invoiceId = await _saleService.CommitSaleAsync();
                StatusMessage = $"Sale committed! Invoice #{invoiceId}";
                
                // Clear the sale
                await Task.Delay(1500); // Show message briefly
                ClearSale();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error committing sale: {ex.Message}";
            }
        }

        [RelayCommand]
        private void ClearSale()
        {
            _saleService.ClearSale();
            ClearItemEntry();
            CustomerBarcode = "";
            CurrentCustomer = null;
            DiscountAmount = 0;
            UpdateTotals();
            StatusMessage = "Sale cleared";
        }

        private void ClearItemEntry()
        {
            ItemBarcode = "";
            ItemDescription = "";
            ItemQuantity = 1;
            ItemPrice = 0;
            ItemExtension = 0;
        }

        private void UpdateTotals()
        {
            // Force update of all computed properties
            OnPropertyChanged(nameof(SubtotalEx));
            OnPropertyChanged(nameof(TaxAmount));
            OnPropertyChanged(nameof(TotalInc));
            OnPropertyChanged(nameof(AmountDue));
            OnPropertyChanged(nameof(TotalPaid));
            OnPropertyChanged(nameof(Change));
        }

        partial void OnItemQuantityChanged(decimal value)
        {
            ItemExtension = ItemQuantity * ItemPrice;
            OnPropertyChanged(nameof(ItemExtension));
        }

        partial void OnItemPriceChanged(decimal value)
        {
            ItemExtension = ItemQuantity * ItemPrice;
            OnPropertyChanged(nameof(ItemExtension));
        }

        partial void OnDiscountAmountChanged(decimal value)
        {
            _saleService.DiscountAmount = value;
            UpdateTotals();
        }
    }
}
