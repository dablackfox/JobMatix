using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using JMxPOS8.ViewModels;

namespace JMxPOS8.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AddHandler(KeyDownEvent, OnPreviewKeyDown, RoutingStrategies.Tunnel);
        DataContextChanged += (_, _) => WireAutoCompleteSearch();
    }

    // AutoCompleteBox's AsyncPopulator is a plain delegate property (not easily bindable
    // from XAML), so it's wired here in code-behind once the real MainWindowViewModel
    // (and its services) are available.
    private void WireAutoCompleteSearch()
    {
        if (DataContext is not MainWindowViewModel viewModel)
            return;

        var customerBox = this.FindControl<AutoCompleteBox>("autoCompleteCustomer");
        if (customerBox != null)
        {
            customerBox.AsyncPopulator = async (searchText, _) =>
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return Enumerable.Empty<object>();
                var matches = await viewModel.CustomerService.SearchCustomersAsync(searchText, 15);
                return matches.Cast<object>();
            };
        }

        var itemBox = this.FindControl<AutoCompleteBox>("autoCompleteItemBarcode");
        if (itemBox != null)
        {
            itemBox.AsyncPopulator = async (searchText, _) =>
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return Enumerable.Empty<object>();
                var matches = await viewModel.StockService.SearchStockAsync(searchText, 15);
                return matches.Cast<object>();
            };
        }

        var serialBox = this.FindControl<AutoCompleteBox>("autoCompleteSerialNumber");
        if (serialBox != null)
        {
            serialBox.AsyncPopulator = async (searchText, _) =>
            {
                // Scoped to whichever item is currently in the barcode field - re-resolves
                // it by barcode rather than tracking extra state, since that field always
                // holds the real barcode once an item's been found (see ApplyFoundStock).
                var barcode = viewModel.SaleViewModel.ItemBarcode;
                if (string.IsNullOrWhiteSpace(barcode))
                    return Enumerable.Empty<object>();
                var stock = await viewModel.StockService.FindStockByBarcodeAsync(barcode);
                if (stock == null)
                    return Enumerable.Empty<object>();
                var serials = await viewModel.SerialService.GetAvailableSerialsAsync(stock.StockId, searchText);
                return serials.Cast<object>();
            };
        }
    }

    private async void OnPreviewKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;
        if (DataContext is not MainWindowViewModel viewModel)
            return;
        if (e.Source is not Visual sourceVisual)
            return;

        if (sourceVisual.FindAncestorOfType<TextBox>(includeSelf: true) is { Name: "txtStaffNumber" })
        {
            await viewModel.SaleViewModel.ProcessStaffNumberCommand.ExecuteAsync(null);
            e.Handled = true;
            return;
        }

        if (sourceVisual.FindAncestorOfType<TextBox>(includeSelf: true) is { Name: "txtStaffOverrideBarcode" })
        {
            await viewModel.UnlockStaffAdminCommand.ExecuteAsync(null);
            e.Handled = true;
            return;
        }

        var saleVM = viewModel.SaleViewModel;

        if (sourceVisual.FindAncestorOfType<AutoCompleteBox>(includeSelf: true) is { } autoBox)
        {
            // While the suggestion dropdown is open, let AutoCompleteBox's own Enter
            // handling confirm the highlighted item instead of racing it with an
            // exact-barcode lookup against whatever partial text is currently typed.
            if (autoBox.IsDropDownOpen)
                return;

            if (autoBox.Name == "autoCompleteCustomer")
            {
                await saleVM.ProcessCustomerBarcodeCommand.ExecuteAsync(null);
                e.Handled = true;
            }
            else if (autoBox.Name == "autoCompleteItemBarcode")
            {
                await saleVM.ProcessItemBarcodeCommand.ExecuteAsync(null);
                e.Handled = true;
            }
            else if (autoBox.Name == "autoCompleteSerialNumber")
            {
                // Scanning (or picking) a serial then pressing Enter completes the add,
                // the same way scanning the item barcode itself does.
                await saleVM.AddItemCommand.ExecuteAsync(null);
                e.Handled = true;
            }
        }
    }
}