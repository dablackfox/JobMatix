using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using JMxPOS8.ViewModels;
using System;

namespace JMxPOS8.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AddHandler(KeyDownEvent, OnPreviewKeyDown, RoutingStrategies.Tunnel);
    }

    private async void OnPreviewKeyDown(object? sender, KeyEventArgs e)
    {
        if (DataContext is not MainWindowViewModel viewModel)
            return;

        var saleVM = viewModel.SaleViewModel;

        // Handle Enter key in barcode textboxes
        if (e.Key == Key.Enter)
        {
            if (e.Source is TextBox textBox)
            {
                if (textBox.Name == "txtStaffNumber")
                {
                    await saleVM.ProcessStaffNumberCommand.ExecuteAsync(null);
                    e.Handled = true;
                }
                else if (textBox.Name == "txtCustomerBarcode")
                {
                    await saleVM.ProcessCustomerBarcodeCommand.ExecuteAsync(null);
                    e.Handled = true;
                }
                else if (textBox.Name == "txtItemBarcode")
                {
                    await saleVM.ProcessItemBarcodeCommand.ExecuteAsync(null);
                    e.Handled = true;
                }
            }
        }
    }
}