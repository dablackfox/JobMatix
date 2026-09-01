using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class ReturnAuthorizationViewModel : ViewModelBase
{
    private readonly ReturnAuthorizationService _raService;
    private readonly StockService _stockService;
    private readonly SupplierService _supplierService;
    private readonly CustomerService _customerService;
    private readonly StaffService _staffService;

    public ObservableCollection<ReturnAuthorization> OpenRAs { get; } = new();

    [ObservableProperty]
    private ReturnAuthorization? _selectedRa;

    // ComboBox.SelectedItem bound directly to a plain string only works against a real
    // ItemsSource of the same type - inline <ComboBoxItem> children (the previous XAML)
    // have ComboBoxItem as their actual item type, so SelectedItem could never match the
    // bound string either way. Same fix as TransactionLookupViewModel's LookupType/DatePeriod.
    public static readonly string[] OriginOptions = { "Counter", "Stock", "Job" };
    public static readonly string[] ReturnResultOptions = { "Replaced", "Repaired", "Returned", "Credited", "Other" };

    // New RA form fields
    [ObservableProperty]
    private string _newOrigin = "Counter";

    [ObservableProperty]
    private string _newCustomerBarcode = "";

    [ObservableProperty]
    private string _newSupplierBarcode = "";

    [ObservableProperty]
    private string _newItemBarcode = "";

    [ObservableProperty]
    private string _newItemDescription = "";

    [ObservableProperty]
    private string _newSerialNumber = "";

    [ObservableProperty]
    private string _newProblemDescription = "";

    [ObservableProperty]
    private string _newSymptoms = "";

    [ObservableProperty]
    private string _newStaffBarcode = "";

    // Action-panel fields, reused across whichever action the selected RA's status allows
    [ObservableProperty]
    private string _actionNotes = "";

    [ObservableProperty]
    private string _actionCourierBarcode = "";

    [ObservableProperty]
    private string _actionReturnResult = "Replaced";

    [ObservableProperty]
    private bool _actionGoodsReceivedBack;

    [ObservableProperty]
    private string _actionStaffBarcode = "";

    [ObservableProperty]
    private string _statusMessage = "";

    public bool CanRequestFromSupplier => SelectedRa?.RaStatus == "10-Created";
    public bool CanGrantRma => SelectedRa?.RaStatus == "20-RMA-Requested";
    public bool CanSendToSupplier => SelectedRa?.RaStatus == "30-RMA-Granted";
    public bool CanComplete => SelectedRa?.RaStatus == "50-GoodsSentToSupplier";
    public bool CanCloseOut => SelectedRa != null && SelectedRa.RaStatus is not ("70-GoodsCompleted" or "95-RMA-Refused" or "97-RMA-Cancelled");

    public ReturnAuthorizationViewModel(ReturnAuthorizationService raService, StockService stockService,
        SupplierService supplierService, CustomerService customerService, StaffService staffService)
    {
        _raService = raService;
        _stockService = stockService;
        _supplierService = supplierService;
        _customerService = customerService;
        _staffService = staffService;
    }

    partial void OnSelectedRaChanged(ReturnAuthorization? value)
    {
        OnPropertyChanged(nameof(CanRequestFromSupplier));
        OnPropertyChanged(nameof(CanGrantRma));
        OnPropertyChanged(nameof(CanSendToSupplier));
        OnPropertyChanged(nameof(CanComplete));
        OnPropertyChanged(nameof(CanCloseOut));
        ActionNotes = "";
        ActionCourierBarcode = "";
    }

    public async Task LoadOpenRAsAsync()
    {
        OpenRAs.Clear();
        foreach (var ra in await _raService.GetOpenAsync())
            OpenRAs.Add(ra);
    }

    [RelayCommand]
    private async Task CreateRa()
    {
        if (string.IsNullOrWhiteSpace(NewItemDescription) && string.IsNullOrWhiteSpace(NewItemBarcode))
        {
            StatusMessage = "Enter an item description or scan an item barcode";
            return;
        }
        if (string.IsNullOrWhiteSpace(NewStaffBarcode))
        {
            StatusMessage = "Enter your staff barcode";
            return;
        }

        var staff = await _staffService.FindStaffByBarcodeAsync(NewStaffBarcode.Trim());
        if (staff == null)
        {
            StatusMessage = $"Staff not found for '{NewStaffBarcode}'";
            return;
        }

        var ra = new ReturnAuthorization
        {
            Origin = NewOrigin,
            ItemDescription = NewItemDescription.Trim(),
            SerialNumber = NewSerialNumber.Trim(),
            ProblemDescription = NewProblemDescription.Trim(),
            RaSymptoms = NewSymptoms.Trim(),
            StaffIdCreated = staff.StaffId,
            StaffNameCreated = staff.DocketName
        };

        if (!string.IsNullOrWhiteSpace(NewCustomerBarcode))
        {
            var customer = await _customerService.FindCustomerByBarcodeAsync(NewCustomerBarcode.Trim());
            if (customer != null)
            {
                ra.CustomerBarcode = customer.Barcode;
                ra.CustomerName = customer.CustomerName;
            }
        }

        if (!string.IsNullOrWhiteSpace(NewSupplierBarcode))
        {
            var supplier = await _supplierService.FindSupplierByBarcodeAsync(NewSupplierBarcode.Trim());
            if (supplier != null)
            {
                ra.SupplierId = supplier.SupplierId;
                ra.SupplierName = supplier.SupplierName;
            }
        }

        if (!string.IsNullOrWhiteSpace(NewItemBarcode))
        {
            var stock = await _stockService.FindStockByBarcodeAsync(NewItemBarcode.Trim());
            if (stock != null)
            {
                ra.RmStockId = stock.StockId;
                ra.ItemBarcode = stock.Barcode;
                if (string.IsNullOrWhiteSpace(ra.ItemDescription))
                    ra.ItemDescription = stock.Description;
            }
        }

        if (string.IsNullOrWhiteSpace(ra.ItemDescription))
        {
            StatusMessage = $"No stock item found for barcode '{NewItemBarcode}' - enter a description manually";
            return;
        }

        var created = await _raService.CreateAsync(ra);
        OpenRAs.Insert(0, created);
        SelectedRa = created;
        StatusMessage = $"RA #{created.RaId} created";

        NewCustomerBarcode = "";
        NewSupplierBarcode = "";
        NewItemBarcode = "";
        NewItemDescription = "";
        NewSerialNumber = "";
        NewProblemDescription = "";
        NewSymptoms = "";
        NewStaffBarcode = "";
    }

    [RelayCommand]
    private async Task RequestFromSupplier()
    {
        if (SelectedRa == null) return;
        try
        {
            await _raService.RequestFromSupplierAsync(SelectedRa.RaId, ActionNotes.Trim());
            StatusMessage = $"RA #{SelectedRa.RaId} marked as requested from supplier";
            await ReloadSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task GrantRma()
    {
        if (SelectedRa == null) return;
        if (string.IsNullOrWhiteSpace(ActionNotes))
        {
            StatusMessage = "Enter the supplier's RMA number";
            return;
        }
        try
        {
            await _raService.GrantRmaAsync(SelectedRa.RaId, ActionNotes.Trim());
            StatusMessage = $"RA #{SelectedRa.RaId} - supplier RMA recorded";
            await ReloadSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task SendToSupplier()
    {
        if (SelectedRa == null) return;
        if (string.IsNullOrWhiteSpace(ActionStaffBarcode))
        {
            StatusMessage = "Enter your staff barcode";
            return;
        }
        var staff = await _staffService.FindStaffByBarcodeAsync(ActionStaffBarcode.Trim());
        if (staff == null)
        {
            StatusMessage = $"Staff not found for '{ActionStaffBarcode}'";
            return;
        }
        try
        {
            await _raService.SendToSupplierAsync(SelectedRa.RaId, ActionCourierBarcode.Trim(), staff.DocketName);
            StatusMessage = $"RA #{SelectedRa.RaId} sent to supplier - stock adjusted";
            await ReloadSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task CompleteRa()
    {
        if (SelectedRa == null) return;
        try
        {
            await _raService.CompleteAsync(SelectedRa.RaId, ActionReturnResult, ActionNotes.Trim(), ActionGoodsReceivedBack);
            StatusMessage = $"RA #{SelectedRa.RaId} completed - {ActionReturnResult}";
            OpenRAs.Remove(SelectedRa);
            SelectedRa = null;
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task RefuseRa()
    {
        if (SelectedRa == null) return;
        try
        {
            await _raService.RefuseAsync(SelectedRa.RaId);
            StatusMessage = $"RA #{SelectedRa.RaId} marked as refused";
            OpenRAs.Remove(SelectedRa);
            SelectedRa = null;
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task CancelRa()
    {
        if (SelectedRa == null) return;
        try
        {
            await _raService.CancelAsync(SelectedRa.RaId);
            StatusMessage = $"RA #{SelectedRa.RaId} cancelled";
            OpenRAs.Remove(SelectedRa);
            SelectedRa = null;
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    private async Task ReloadSelectedAsync()
    {
        int? id = SelectedRa?.RaId;
        await LoadOpenRAsAsync();
        SelectedRa = id.HasValue ? System.Linq.Enumerable.FirstOrDefault(OpenRAs, r => r.RaId == id.Value) : null;
    }
}
