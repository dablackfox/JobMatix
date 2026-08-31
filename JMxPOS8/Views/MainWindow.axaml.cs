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
    // still handles fields that are actually app-shell-level: the Staff admin and Void
    // manager-override prompts.
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
            return;
        }

        if (sourceVisual.FindAncestorOfType<TextBox>(includeSelf: true) is { Name: "txtVoidOverrideBarcode" })
        {
            await viewModel.TransactionLookupViewModel.ConfirmVoidCommand.ExecuteAsync(null);
            e.Handled = true;
        }
    }
}
