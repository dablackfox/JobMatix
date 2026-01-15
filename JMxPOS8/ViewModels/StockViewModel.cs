using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class StockViewModel : ViewModelBase
{
    private readonly StockService _stockService;

    [ObservableProperty]
    private ObservableCollection<StockItem> _stockItems = new();

    [ObservableProperty]
    private StockItem? _selectedStock;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    // Stock detail fields for add/edit
    [ObservableProperty]
    private bool _isEditing;

    [ObservableProperty]
    private string _barcode = string.Empty;

    [ObservableProperty]
    private string _stockCode = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private string _category = string.Empty;

    [ObservableProperty]
    private decimal _quantityInStock;

    [ObservableProperty]
    private decimal _costPrice;

    [ObservableProperty]
    private decimal _sellPrice;

    [ObservableProperty]
    private bool _inactive;

    [ObservableProperty]
    private bool _requiresSerial;

    [ObservableProperty]
    private decimal _reorderLevel;

    [ObservableProperty]
    private decimal _reorderQuantity;

    [ObservableProperty]
    private string _supplier = string.Empty;

    [ObservableProperty]
    private string _location = string.Empty;

    [ObservableProperty]
    private string _notes = string.Empty;

    private int _editingStockId;

    public StockViewModel(StockService stockService)
    {
        _stockService = stockService;
    }

    partial void OnSearchTextChanged(string value)
    {
        _ = SearchStockAsync();
    }

    [RelayCommand]
    public async Task LoadStockAsync()
    {
        try
        {
            StatusMessage = "Loading stock items...";
            Console.WriteLine("[LOAD STOCK] Starting...");
            var items = await _stockService.GetAllStockAsync();
            Console.WriteLine($"[LOAD STOCK] Fetched {items.Count} items from database");
            
            await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
            {
                Console.WriteLine($"[LOAD STOCK] Setting new collection");
                StockItems = new ObservableCollection<StockItem>(items);
                Console.WriteLine($"[LOAD STOCK] Collection now has {StockItems.Count} items");
            });
            
            StatusMessage = $"Loaded {StockItems.Count} stock items";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LOAD STOCK ERROR] {ex.Message}");
            StatusMessage = $"Error loading stock: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task SearchStockAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadStockAsync();
                return;
            }

            StatusMessage = "Searching...";
            var items = await _stockService.SearchStockAsync(SearchText);
            
            StockItems.Clear();
            foreach (var item in items)
            {
                StockItems.Add(item);
            }
            
            StatusMessage = $"Found {StockItems.Count} stock items";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error searching: {ex.Message}";
        }
    }

    [RelayCommand]
    private void NewStock()
    {
        ClearForm();
        IsEditing = true;
        _editingStockId = 0;
        StatusMessage = "Enter stock item details";
    }

    [RelayCommand]
    private void EditStock()
    {
        if (SelectedStock == null)
        {
            StatusMessage = "Please select a stock item to edit";
            return;
        }

        LoadStockToForm(SelectedStock);
        IsEditing = true;
        StatusMessage = "Editing stock item";
    }

    [RelayCommand]
    private async Task SaveStockAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Barcode))
            {
                StatusMessage = "Barcode is required";
                return;
            }

            if (string.IsNullOrWhiteSpace(StockCode))
            {
                StatusMessage = "Stock code is required";
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                StatusMessage = "Description is required";
                return;
            }

            var stockItem = new StockItem
            {
                StockId = _editingStockId,
                Barcode = Barcode.Trim(),
                StockCode = StockCode.Trim(),
                Description = Description.Trim(),
                Category = Category.Trim(),
                QuantityInStock = QuantityInStock,
                CostPrice = CostPrice,
                SellPrice = SellPrice,
                Inactive = Inactive,
                RequiresSerial = RequiresSerial,
                ReorderLevel = ReorderLevel,
                ReorderQuantity = ReorderQuantity,
                Supplier = Supplier.Trim(),
                Location = Location.Trim(),
                Notes = Notes.Trim()
            };

            if (_editingStockId == 0)
            {
                await _stockService.AddStockAsync(stockItem);
                StatusMessage = "Stock item added successfully";
            }
            else
            {
                await _stockService.UpdateStockAsync(stockItem);
                StatusMessage = "Stock item updated successfully";
            }

            IsEditing = false;
            await LoadStockAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error saving stock: {ex.Message}";
        }
    }

    [RelayCommand]
    private void CancelEdit()
    {
        IsEditing = false;
        ClearForm();
        StatusMessage = "Edit cancelled";
    }

    [RelayCommand]
    private async Task DeleteStockAsync()
    {
        if (SelectedStock == null)
        {
            StatusMessage = "Please select a stock item to delete";
            return;
        }

        try
        {
            // Note: You may want to add a confirmation dialog here
            await _stockService.DeleteStockAsync(SelectedStock.StockId);
            StatusMessage = "Stock item deleted successfully";
            await LoadStockAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error deleting stock: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        SearchText = string.Empty;
        await LoadStockAsync();
    }

    [RelayCommand]
    private void AdjustQuantity()
    {
        if (SelectedStock == null)
        {
            StatusMessage = "Please select a stock item to adjust quantity";
            return;
        }

        // This would typically open a dialog for quantity adjustment
        // For now, just load it for editing
        EditStock();
        StatusMessage = "Adjust quantity in the form";
    }

    private void LoadStockToForm(StockItem stock)
    {
        _editingStockId = stock.StockId;
        Barcode = stock.Barcode;
        StockCode = stock.StockCode;
        Description = stock.Description;
        Category = stock.Category;
        QuantityInStock = stock.QuantityInStock;
        CostPrice = stock.CostPrice;
        SellPrice = stock.SellPrice;
        Inactive = stock.Inactive;
        RequiresSerial = stock.RequiresSerial;
        ReorderLevel = stock.ReorderLevel;
        ReorderQuantity = stock.ReorderQuantity;
        Supplier = stock.Supplier;
        Location = stock.Location;
        Notes = stock.Notes;
    }

    private void ClearForm()
    {
        _editingStockId = 0;
        Barcode = string.Empty;
        StockCode = string.Empty;
        Description = string.Empty;
        Category = string.Empty;
        QuantityInStock = 0;
        CostPrice = 0;
        SellPrice = 0;
        Inactive = false;
        RequiresSerial = false;
        ReorderLevel = 0;
        ReorderQuantity = 0;
        Supplier = string.Empty;
        Location = string.Empty;
        Notes = string.Empty;
    }
}
