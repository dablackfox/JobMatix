using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class StocktakeViewModel : ViewModelBase
{
    private readonly StocktakeService _stocktakeService;
    private readonly StaffService _staffService;

    public ObservableCollection<StocktakeSession> Sessions { get; } = new();
    public ObservableCollection<StocktakeItem> Items { get; } = new();

    [ObservableProperty]
    private StocktakeSession? _selectedSession;

    [ObservableProperty]
    private string _staffBarcode = "";

    [ObservableProperty]
    private string _scanBarcode = "";

    [ObservableProperty]
    private string _statusMessage = "";

    public bool HasOpenSession => SelectedSession != null;

    public StocktakeViewModel(StocktakeService stocktakeService, StaffService staffService)
    {
        _stocktakeService = stocktakeService;
        _staffService = staffService;
    }

    public async Task LoadSessionsAsync()
    {
        Sessions.Clear();
        foreach (var session in await _stocktakeService.GetOpenStocktakesAsync())
            Sessions.Add(session);
    }

    [RelayCommand]
    private async Task NewStocktake()
    {
        var staff = await ResolveStaffAsync();
        if (staff == null)
            return;

        var session = await _stocktakeService.CreateStocktakeAsync(staff.DocketName);
        Sessions.Insert(0, session);
        SelectedSession = session;
        StatusMessage = $"Stocktake #{session.StocktakeId} started by {staff.DocketName}";
    }

    partial void OnSelectedSessionChanged(StocktakeSession? value)
    {
        OnPropertyChanged(nameof(HasOpenSession));
        _ = LoadItemsAsync();
    }

    private async Task LoadItemsAsync()
    {
        Items.Clear();
        if (SelectedSession == null)
            return;
        foreach (var item in await _stocktakeService.GetStocktakeItemsAsync(SelectedSession.StocktakeId))
            Items.Add(item);
    }

    [RelayCommand]
    private async Task ScanItem()
    {
        if (SelectedSession == null || string.IsNullOrWhiteSpace(ScanBarcode))
            return;

        var result = await _stocktakeService.ScanItemAsync(SelectedSession.StocktakeId, ScanBarcode.Trim());
        if (result == StocktakeService.ScanResult.NotFound)
        {
            StatusMessage = $"No stock item found for barcode '{ScanBarcode}'";
        }
        else
        {
            StatusMessage = $"Counted '{ScanBarcode}'";
            await LoadItemsAsync();
        }
        ScanBarcode = "";
    }

    [RelayCommand]
    private async Task CommitStocktake()
    {
        if (SelectedSession == null)
            return;

        var staff = await ResolveStaffAsync();
        if (staff == null)
            return;

        await _stocktakeService.CommitStocktakeAsync(SelectedSession.StocktakeId, staff.DocketName);
        StatusMessage = $"Stocktake #{SelectedSession.StocktakeId} committed by {staff.DocketName} - stock quantities adjusted";
        Sessions.Remove(SelectedSession);
        SelectedSession = null;
        Items.Clear();
    }

    [RelayCommand]
    private async Task CancelStocktake()
    {
        if (SelectedSession == null)
            return;

        await _stocktakeService.CancelStocktakeAsync(SelectedSession.StocktakeId);
        StatusMessage = $"Stocktake #{SelectedSession.StocktakeId} cancelled - no stock changes made";
        Sessions.Remove(SelectedSession);
        SelectedSession = null;
        Items.Clear();
    }

    private async Task<Staff?> ResolveStaffAsync()
    {
        if (string.IsNullOrWhiteSpace(StaffBarcode))
        {
            StatusMessage = "Enter your staff barcode first";
            return null;
        }

        var staff = await _staffService.FindStaffByBarcodeAsync(StaffBarcode.Trim());
        if (staff == null)
        {
            StatusMessage = $"Staff not found for '{StaffBarcode}'";
            return null;
        }

        return staff;
    }
}
