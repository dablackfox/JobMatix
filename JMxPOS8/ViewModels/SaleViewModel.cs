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
        private int _nextHoldId = 1;

        // Raised with the invoice id whenever a sale is successfully committed, so other
        // parts of the app (e.g. "Show Last Invoice") can react without polling.
        public event Action<int>? SaleCommitted;

        // Raised whenever the staff attributed to the current sale changes, so the app
        // shell can mirror it in the status bar without owning staff identity itself -
        // attribution is per-sale here (communal till, quick-swap between staff), not a
        // persistent app-wide session.
        public event Action<Staff?>? StaffChanged;

        public ObservableCollection<HeldSale> HeldSales { get; } = new();
        public bool HasHeldSales => HeldSales.Count > 0;

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
        private string _itemSerialNumber = "";

        [ObservableProperty]
        private bool _itemRequiresSerial = false;

        [ObservableProperty]
        private Staff? _currentStaff;

        [ObservableProperty]
        private Customer? _currentCustomer;

        [ObservableProperty]
        private SaleLineItem? _selectedSaleItem;

        [ObservableProperty]
        private string _transactionType = "Sale";

        public string StaffDisplay => CurrentStaff != null 
            ? $"{CurrentStaff.DocketName} (ID: {CurrentStaff.StaffId})" 
            : "";

        public string CustomerDisplay => CurrentCustomer != null 
            ? $"{CurrentCustomer.CustomerName} (ID: {CurrentCustomer.CustomerId})" 
            : "Walk-in Customer";

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
            HeldSales.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasHeldSales));
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

        partial void OnCurrentStaffChanged(Staff? value)
        {
            OnPropertyChanged(nameof(CustomerInfo));
            OnPropertyChanged(nameof(StaffDisplay));
            StaffChanged?.Invoke(value);
        }

        partial void OnCurrentCustomerChanged(Customer? value)
        {
            OnPropertyChanged(nameof(CustomerInfo));
            OnPropertyChanged(nameof(CustomerDisplay));
        }

        [RelayCommand]
        private async Task ProcessStaffNumber()
        {
            if (string.IsNullOrWhiteSpace(StaffNumber))
                return;

            try
            {
                // Staff attribute each sale by their own barcode/employee number (e.g.
                // "3"), not the internal database staff_id - those are unrelated.
                var staff = await _staffService.FindStaffByBarcodeAsync(StaffNumber.Trim());
                if (staff != null)
                {
                    CurrentStaff = staff;
                    _saleService.SetStaff(staff);
                    StatusMessage = $"Staff: {staff.DocketName}";
                }
                else
                {
                    StatusMessage = $"Staff not found for number '{StaffNumber}'";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        [ObservableProperty]
        private Customer? _selectedCustomerSuggestion;

        // Fired when a customer is picked from the type-ahead suggestion list (as opposed
        // to an exact barcode scan handled by ProcessCustomerBarcode below).
        partial void OnSelectedCustomerSuggestionChanged(Customer? value)
        {
            if (value != null)
                ApplyFoundCustomer(value);
        }

        [RelayCommand]
        private async Task ProcessCustomerBarcode()
        {
            if (string.IsNullOrWhiteSpace(CustomerBarcode))
                return;

            Console.WriteLine($"[SALE] Processing customer barcode: {CustomerBarcode}");
            try
            {
                var customer = await _customerService.FindCustomerByBarcodeAsync(CustomerBarcode.Trim());
                if (customer != null)
                {
                    ApplyFoundCustomer(customer);
                }
                else
                {
                    StatusMessage = $"Customer not found for barcode '{CustomerBarcode}' - try typing part of their name instead";
                    CurrentCustomer = null;
                    _saleService.SetCustomer(null!);
                    Console.WriteLine($"[SALE] ❌ Customer not found for barcode: {CustomerBarcode}");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                Console.WriteLine($"[SALE] ❌ ERROR in ProcessCustomerBarcode: {ex.Message}");
            }
        }

        private void ApplyFoundCustomer(Customer customer)
        {
            CurrentCustomer = customer;
            _saleService.SetCustomer(customer);
            CustomerBarcode = customer.CustomerName;
            OnPropertyChanged(nameof(CustomerInfo));
            StatusMessage = $"Customer: {customer.CustomerName}";
            Console.WriteLine($"[SALE] ✅ Customer found: {customer.CustomerName} (ID: {customer.CustomerId}, Barcode: {customer.Barcode})");
            Console.WriteLine($"[SALE]    Account: {customer.IsAccount}, Balance: ${customer.AccountBalance:F2}, Credit Limit: ${customer.CreditLimit:F2}");
        }

        [RelayCommand]
        private async Task ProcessItemBarcode()
        {
            if (string.IsNullOrWhiteSpace(ItemBarcode))
                return;

            Console.WriteLine($"[SALE] Processing item barcode: {ItemBarcode}");
            try
            {
                var stock = await _stockService.FindStockByBarcodeAsync(ItemBarcode.Trim());
                if (stock != null)
                {
                    await ApplyFoundStock(stock);
                }
                else
                {
                    StatusMessage = $"Item not found for barcode '{ItemBarcode}' - try typing part of the description instead";
                    Console.WriteLine($"[SALE] ❌ Item not found for barcode: {ItemBarcode}");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                Console.WriteLine($"[SALE] ❌ ERROR in ProcessItemBarcode: {ex.Message}");
            }
        }

        [ObservableProperty]
        private StockItem? _selectedStockSuggestion;

        // Fired when an item is picked from the type-ahead suggestion list (as opposed to
        // an exact barcode scan handled by ProcessItemBarcode above).
        partial void OnSelectedStockSuggestionChanged(StockItem? value)
        {
            if (value != null)
                _ = ApplyFoundStock(value);
        }

        private async Task ApplyFoundStock(StockItem stock)
        {
            ItemBarcode = stock.Barcode;
            ItemDescription = stock.Description;
            ItemPrice = stock.SellPrice;
            ItemExtension = ItemQuantity * ItemPrice;
            ItemRequiresSerial = stock.RequiresSerial;
            StatusMessage = $"Item found: {stock.Description}";
            Console.WriteLine($"[SALE] ✅ Item found: {stock.Description} (Stock ID: {stock.StockId}, Barcode: {stock.Barcode})");
            Console.WriteLine($"[SALE]    Price: ${stock.SellPrice:F2}, Qty in Stock: {stock.QuantityInStock}, Requires Serial: {stock.RequiresSerial}");

            if (stock.RequiresSerial)
            {
                // Don't auto-add: wait for the serial number to be entered first.
                StatusMessage = $"{stock.Description} requires a serial number - enter it, then click Add";
            }
            else
            {
                await AddItem();
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

            Console.WriteLine($"[SALE] Adding item to sale: Barcode={ItemBarcode}, Qty={ItemQuantity}, Price=${ItemPrice:F2}, Serial={ItemSerialNumber}");
            try
            {
                var result = await _saleService.AddItemByBarcodeAsync(ItemBarcode.Trim(), ItemQuantity, ItemSerialNumber);
                switch (result)
                {
                    case SaleService.AddItemResult.Added:
                        StatusMessage = $"Added: {ItemDescription}";
                        Console.WriteLine($"[SALE] ✅ Item added to sale: {ItemDescription} x{ItemQuantity} = ${ItemExtension:F2}");
                        Console.WriteLine($"[SALE]    Sale now has {SaleItems.Count} items, Total: ${TotalInc:F2}");
                        ClearItemEntry();
                        UpdateTotals();
                        break;
                    case SaleService.AddItemResult.SerialRequired:
                        StatusMessage = $"{ItemDescription} requires a serial number";
                        Console.WriteLine($"[SALE] ❌ Serial number required for: {ItemBarcode}");
                        break;
                    case SaleService.AddItemResult.SerialAlreadyInSale:
                        StatusMessage = $"Serial {ItemSerialNumber} is already in this sale";
                        Console.WriteLine($"[SALE] ❌ Duplicate serial within current sale: {ItemSerialNumber}");
                        break;
                    case SaleService.AddItemResult.SerialAlreadySold:
                        StatusMessage = $"Serial {ItemSerialNumber} has already been sold";
                        Console.WriteLine($"[SALE] ❌ Serial already sold: {ItemSerialNumber}");
                        break;
                    default:
                        StatusMessage = "Failed to add item";
                        Console.WriteLine($"[SALE] ❌ Failed to add item: {ItemBarcode}");
                        break;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                Console.WriteLine($"[SALE] ❌ ERROR in AddItem: {ex.Message}");
            }
        }

        [RelayCommand]
        private void SetTransactionType(string type)
        {
            TransactionType = type;
            _saleService.TransactionType = type;
            StatusMessage = $"Transaction type: {type}";
            Console.WriteLine($"[SALE] Transaction type set to: {type}");
        }

        [RelayCommand]
        private void AddCashPayment()
        {
            if (AmountDue <= 0)
            {
                StatusMessage = "No amount due";
                Console.WriteLine($"[SALE] ⚠️ Cannot add cash payment - no amount due");
                return;
            }

            Console.WriteLine($"[SALE] Adding CASH payment: ${AmountDue:F2}");
            _saleService.AddPayment("CASH", AmountDue);
            UpdateTotals();
            StatusMessage = $"Cash payment added: ${AmountDue:F2}";
            Console.WriteLine($"[SALE] ✅ Cash payment added. Total paid: ${TotalPaid:F2}, Change: ${Change:F2}");
        }

        [RelayCommand]
        private void AddEftposPayment()
        {
            if (AmountDue <= 0)
            {
                StatusMessage = "No amount due";
                Console.WriteLine($"[SALE] ⚠️ Cannot add EFTPOS payment - no amount due");
                return;
            }

            Console.WriteLine($"[SALE] Adding EFTPOS payment: ${AmountDue:F2}");
            _saleService.AddPayment("EFTPOS", AmountDue);
            UpdateTotals();
            StatusMessage = $"EFTPOS payment added: ${AmountDue:F2}";
            Console.WriteLine($"[SALE] ✅ EFTPOS payment added. Total paid: ${TotalPaid:F2}, Change: ${Change:F2}");
        }

        [RelayCommand]
        private void AddCreditCardPayment()
        {
            if (AmountDue <= 0)
            {
                StatusMessage = "No amount due";
                Console.WriteLine($"[SALE] ⚠️ Cannot add credit card payment - no amount due");
                return;
            }

            Console.WriteLine($"[SALE] Adding CREDIT_CARD payment: ${AmountDue:F2}");
            _saleService.AddPayment("CREDIT_CARD", AmountDue);
            UpdateTotals();
            StatusMessage = $"Credit card payment added: ${AmountDue:F2}";
            Console.WriteLine($"[SALE] ✅ Credit card payment added. Total paid: ${TotalPaid:F2}, Change: ${Change:F2}");
        }

        [RelayCommand]
        private void ChargeToAccount()
        {
            if (CurrentCustomer == null || !CurrentCustomer.IsAccount)
            {
                StatusMessage = "Customer must be an account customer";
                Console.WriteLine($"[SALE] ⚠️ Cannot charge to account - customer is not an account customer");
                return;
            }

            Console.WriteLine($"[SALE] Charging ${AmountDue:F2} to account: {CurrentCustomer.CustomerName} (ID: {CurrentCustomer.CustomerId})");
            StatusMessage = "Sale will be charged to account";
        }

        [RelayCommand]
        private async Task CommitSale()
        {
            if (CurrentStaff == null)
            {
                StatusMessage = "Please sign in staff first";
                Console.WriteLine($"[SALE] ❌ Cannot commit - no staff signed in");
                return;
            }

            if (SaleItems.Count == 0)
            {
                StatusMessage = "No items in sale";
                Console.WriteLine($"[SALE] ❌ Cannot commit - no items in sale");
                return;
            }

            Console.WriteLine($"\n[SALE] ═══════════════════════════════════════════════════════");
            Console.WriteLine($"[SALE] 🛒 COMMITTING SALE");
            Console.WriteLine($"[SALE] ═══════════════════════════════════════════════════════");
            Console.WriteLine($"[SALE] Staff: {CurrentStaff.DocketName} (ID: {CurrentStaff.StaffId})");
            Console.WriteLine($"[SALE] Customer: {(CurrentCustomer != null ? CurrentCustomer.CustomerName : "Walk-in")} (ID: {CurrentCustomer?.CustomerId ?? 0})");
            Console.WriteLine($"[SALE] Transaction Type: {TransactionType}");
            Console.WriteLine($"[SALE] Items: {SaleItems.Count}");
            foreach (var item in SaleItems)
            {
                Console.WriteLine($"[SALE]   - {item.Description} x{item.Quantity} @ ${item.UnitPrice:F2} = ${item.Extension:F2}");
            }
            Console.WriteLine($"[SALE] Subtotal (ex): ${SubtotalEx:F2}");
            Console.WriteLine($"[SALE] Tax: ${TaxAmount:F2}");
            Console.WriteLine($"[SALE] Total (inc): ${TotalInc:F2}");
            Console.WriteLine($"[SALE] Discount: ${DiscountAmount:F2}");
            Console.WriteLine($"[SALE] Total Paid: ${TotalPaid:F2}");
            Console.WriteLine($"[SALE] Change: ${Change:F2}");
            
            try
            {
                _saleService.DiscountAmount = DiscountAmount;
                Console.WriteLine($"[SALE] Calling SaleService.CommitSaleAsync()...");
                int invoiceId = await _saleService.CommitSaleAsync();
                StatusMessage = $"Sale committed! Invoice #{invoiceId}";
                Console.WriteLine($"[SALE] ✅ Sale committed successfully! Invoice ID: {invoiceId}");
                Console.WriteLine($"[SALE] ═══════════════════════════════════════════════════════\n");
                SaleCommitted?.Invoke(invoiceId);

                // Clear the sale
                await Task.Delay(1500); // Show message briefly
                ClearSale();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error committing sale: {ex.Message}";
                Console.WriteLine($"[SALE] ❌ ERROR committing sale: {ex.Message}");
                Console.WriteLine($"[SALE] Stack trace: {ex.StackTrace}");
                Console.WriteLine($"[SALE] ═══════════════════════════════════════════════════════\n");
            }
        }

        [RelayCommand]
        public void HoldSale()
        {
            if (SaleItems.Count == 0)
            {
                StatusMessage = "No sale in progress to hold";
                return;
            }

            if (!_saleService.TryHoldCurrentSale(_nextHoldId, CurrentStaff?.DocketName ?? "", out var held) || held == null)
            {
                StatusMessage = "Cannot hold a sale that already has a payment applied - commit or clear it instead";
                Console.WriteLine("[SALE] ❌ Hold rejected - payment already applied");
                return;
            }

            _nextHoldId++;
            HeldSales.Add(held);
            ClearItemEntry();
            CustomerBarcode = "";
            CurrentCustomer = null;
            DiscountAmount = 0;
            UpdateTotals();
            StatusMessage = $"Sale held ({held.Summary}). Ready for next customer.";
            Console.WriteLine($"[SALE] ✅ {held.Summary} - {SaleItems.Count} items now active");
        }

        [RelayCommand]
        private void ResumeHeldSale(HeldSale? held)
        {
            if (held == null)
                return;

            if (SaleItems.Count > 0)
            {
                StatusMessage = "Finish or hold the current sale before resuming another";
                return;
            }

            _saleService.ResumeHeldSale(held);
            CurrentCustomer = held.Customer;
            TransactionType = held.TransactionType;
            DiscountAmount = held.DiscountAmount;
            HeldSales.Remove(held);
            UpdateTotals();
            StatusMessage = $"Resumed hold #{held.HoldId}";
            Console.WriteLine($"[SALE] ✅ Resumed hold #{held.HoldId} - {SaleItems.Count} items restored");
        }

        [RelayCommand]
        private void RemoveItem(SaleLineItem? item)
        {
            if (item != null)
            {
                Console.WriteLine($"[SALE] Removing item: {item.Description} (Line {item.LineNumber})");
                _saleService.RemoveItem(item);
                
                // Renumber remaining items
                for (int i = 0; i < SaleItems.Count; i++)
                {
                    SaleItems[i].LineNumber = i + 1;
                }
                
                UpdateTotals();
                StatusMessage = $"Removed: {item.Description}";
                Console.WriteLine($"[SALE] ✅ Item removed. Sale now has {SaleItems.Count} items");
            }
        }

        [RelayCommand]
        private void ClearSale()
        {
            Console.WriteLine($"[SALE] Clearing sale (had {SaleItems.Count} items)");
            _saleService.ClearSale();
            ClearItemEntry();
            CustomerBarcode = "";
            CurrentCustomer = null;
            DiscountAmount = 0;
            UpdateTotals();
            StatusMessage = "Sale cleared";
            Console.WriteLine($"[SALE] ✅ Sale cleared and ready for next transaction\n");
        }

        private void ClearItemEntry()
        {
            ItemBarcode = "";
            ItemDescription = "";
            ItemQuantity = 1;
            ItemPrice = 0;
            ItemExtension = 0;
            ItemSerialNumber = "";
            ItemRequiresSerial = false;
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
