using System;
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
    }

    // Everything sale-specific (staff/customer/item AutoCompleteBox wiring, Enter-key
    // handling) now lives in SaleTabView, once per open sale document - this window only
    // still handles the one field that's actually app-shell-level: the Staff admin
    // manager-override prompt.
    private async void OnPreviewKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;
        if (DataContext is not MainWindowViewModel viewModel)
            return;
        if (e.Source is not Visual sourceVisual)
            return;

        if (sourceVisual.FindAncestorOfType<TextBox>(includeSelf: true) is { Name: "txtStaffOverrideBarcode" })
        {
            await viewModel.UnlockStaffAdminCommand.ExecuteAsync(null);
            e.Handled = true;
        }
    }
}
