using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class GoodsReceivedViewModel : ViewModelBase
{
    private readonly GoodsReceivedService _goodsReceivedService;
    private readonly SupplierService _supplierService;
    private readonly StockService _stockService;
    private readonly StaffService _staffService;

    public ObservableCollection<GoodsReceivedLine> Lines { get; } = new();
    public ObservableCollection<GoodsReceivedSummary> RecentReceipts { get; } = new();

    [ObservableProperty]
    private string _supplierBarcode = "";

    [ObservableProperty]
    private Supplier? _selectedSupplier;

    [ObservableProperty]
    private string _invoiceNo = "";

    [ObservableProperty]
    private DateTimeOffset? _invoiceDate = DateTimeOffset.Now;

    [ObservableProperty]
    private string _comments = "";

    [ObservableProperty]
    private string _staffBarcode = "";

    [ObservableProperty]
    private string _scanBarcode = "";

    [ObservableProperty]
    private string _statusMessage = "";

    public decimal SubtotalEx => Lines.Sum(l => l.LineTotalEx);
    public decimal EstimatedTotalInc => Math.Round(SubtotalEx * 1.10m, 2);

    public GoodsReceivedViewModel(GoodsReceivedService goodsReceivedService, SupplierService supplierService,
        StockService stockService, StaffService staffService)
    {
        _goodsReceivedService = goodsReceivedService;
        _supplierService = supplierService;
        _stockService = stockService;
        _staffService = staffService;

        Lines.CollectionChanged += (_, _) => RaiseTotalsChanged();
    }

    public async Task LoadRecentAsync()
    {
        RecentReceipts.Clear();
        foreach (var receipt in await _goodsReceivedService.GetRecentAsync())
            RecentReceipts.Add(receipt);
    }

    [RelayCommand]
    private async Task LookupSupplier()
    {
        if (string.IsNullOrWhiteSpace(SupplierBarcode))
            return;

        var supplier = await _supplierService.FindSupplierByBarcodeAsync(SupplierBarcode.Trim());
        SelectedSupplier = supplier;
        StatusMessage = supplier == null
            ? $"Supplier not found for '{SupplierBarcode}'"
            : $"Supplier: {supplier.SupplierName}";
    }

    [RelayCommand]
    private async Task ScanLine()
    {
        if (string.IsNullOrWhiteSpace(ScanBarcode))
            return;

        var stock = await _stockService.FindStockByBarcodeAsync(ScanBarcode.Trim());
        if (stock == null)
        {
            StatusMessage = $"No stock item found for barcode '{ScanBarcode}'";
            ScanBarcode = "";
            return;
        }

        var existing = Lines.FirstOrDefault(l => l.StockId == stock.StockId);
        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            var line = new GoodsReceivedLine
            {
                StockId = stock.StockId,
                Barcode = stock.Barcode,
                Description = stock.Description,
                Quantity = 1,
                CostEx = stock.CostPrice,
                RequiresSerial = stock.RequiresSerial
            };
            line.PropertyChanged += (_, _) => RaiseTotalsChanged();
            Lines.Add(line);
        }

        StatusMessage = $"Added '{stock.Description}'";
        ScanBarcode = "";
    }

    [RelayCommand]
    private void RemoveLine(GoodsReceivedLine? line)
    {
        if (line == null)
            return;
        Lines.Remove(line);
    }

    [RelayCommand]
    private async Task SubmitGoodsReceived()
    {
        if (SelectedSupplier == null)
        {
            StatusMessage = "Look up a supplier first";
            return;
        }
        if (Lines.Count == 0)
        {
            StatusMessage = "Scan at least one item";
            return;
        }
        if (string.IsNullOrWhiteSpace(InvoiceNo))
        {
            StatusMessage = "Enter the supplier invoice number";
            return;
        }
        if (string.IsNullOrWhiteSpace(StaffBarcode))
        {
            StatusMessage = "Enter your staff barcode";
            return;
        }

        var staff = await _staffService.FindStaffByBarcodeAsync(StaffBarcode.Trim());
        if (staff == null)
        {
            StatusMessage = $"Staff not found for '{StaffBarcode}'";
            return;
        }

        var (goodsId, warnings) = await _goodsReceivedService.ReceiveGoodsAsync(
            SelectedSupplier.SupplierId, staff.StaffId, InvoiceNo.Trim(),
            (InvoiceDate ?? DateTimeOffset.Now).DateTime, Lines.ToList(), Comments);

        StatusMessage = $"Goods received #{goodsId} recorded - stock and cost prices updated";
        if (warnings.Count > 0)
            StatusMessage += " | " + string.Join(" | ", warnings);

        SelectedSupplier = null;
        SupplierBarcode = "";
        InvoiceNo = "";
        Comments = "";
        Lines.Clear();
        await LoadRecentAsync();
    }

    private void RaiseTotalsChanged()
    {
        OnPropertyChanged(nameof(SubtotalEx));
        OnPropertyChanged(nameof(EstimatedTotalInc));
    }
}
