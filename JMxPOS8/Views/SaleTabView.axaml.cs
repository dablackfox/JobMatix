using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using JMxPOS8.ViewModels;

namespace JMxPOS8.Views;

// Hosts a single independent sale document. Instantiated once per open sale tab (see
// MainWindowViewModel.OpenSales), each with its own SaleViewModel and therefore its own
// AutoCompleteBox lookups, key handling, etc. - nothing here is shared across tabs.
public partial class SaleTabView : UserControl
{
    public SaleTabView()
    {
        InitializeComponent();
        AddHandler(KeyDownEvent, OnPreviewKeyDown, RoutingStrategies.Tunnel);
        DataContextChanged += (_, _) => WireAutoCompleteSearch();
    }

    private void WireAutoCompleteSearch()
    {
        if (DataContext is not SaleViewModel saleVm)
            return;

        var customerBox = this.FindControl<AutoCompleteBox>("autoCompleteCustomer");
        if (customerBox != null)
        {
            customerBox.AsyncPopulator = async (searchText, _) =>
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return Enumerable.Empty<object>();
                var matches = await saleVm.CustomerService.SearchCustomersAsync(searchText, 15);
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
                var matches = await saleVm.StockService.SearchStockAsync(searchText, 15);
                return matches.Cast<object>();
            };
        }

        var serialBox = this.FindControl<AutoCompleteBox>("autoCompleteSerialNumber");
        if (serialBox != null)
        {
            serialBox.AsyncPopulator = async (searchText, _) =>
            {
                var barcode = saleVm.ItemBarcode;
                if (string.IsNullOrWhiteSpace(barcode))
                    return Enumerable.Empty<object>();
                var stock = await saleVm.StockService.FindStockByBarcodeAsync(barcode);
                if (stock == null)
                    return Enumerable.Empty<object>();
                var serials = await saleVm.SerialService.GetAvailableSerialsAsync(stock.StockId, searchText);
                return serials.Cast<object>();
            };
        }
    }

    private async void OnPreviewKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;
        if (DataContext is not SaleViewModel saleVm)
            return;
        if (e.Source is not Visual sourceVisual)
            return;

        if (sourceVisual.FindAncestorOfType<TextBox>(includeSelf: true) is { Name: "txtStaffNumber" })
        {
            await saleVm.ProcessStaffNumberCommand.ExecuteAsync(null);
            e.Handled = true;
            return;
        }

        if (sourceVisual.FindAncestorOfType<AutoCompleteBox>(includeSelf: true) is { } autoBox)
        {
            // While the suggestion dropdown is open, let AutoCompleteBox's own Enter
            // handling confirm the highlighted item instead of racing it with an
            // exact-barcode lookup against whatever partial text is currently typed.
            if (autoBox.IsDropDownOpen)
                return;

            if (autoBox.Name == "autoCompleteCustomer")
            {
                await saleVm.ProcessCustomerBarcodeCommand.ExecuteAsync(null);
                e.Handled = true;
            }
            else if (autoBox.Name == "autoCompleteItemBarcode")
            {
                await saleVm.ProcessItemBarcodeCommand.ExecuteAsync(null);
                e.Handled = true;
            }
            else if (autoBox.Name == "autoCompleteSerialNumber")
            {
                await saleVm.AddItemCommand.ExecuteAsync(null);
                e.Handled = true;
            }
        }
    }
}
