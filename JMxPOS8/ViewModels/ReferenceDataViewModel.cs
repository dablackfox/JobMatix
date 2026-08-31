using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

// Generic editor for one flat id/description lookup table - one instance each for
// GoodsTypes/Brands/Symptoms/TaskTypes (see MainWindowViewModel), matching the legacy
// app's single parameterized frmListEdit form used for all four (ROADMAP.md Phase 3).
public partial class ReferenceDataViewModel : ViewModelBase
{
    private readonly ReferenceDataService _service;
    private readonly ReferenceTableConfig _config;

    public string Title { get; }
    public int MaxLength => _config.MaxLength;

    [ObservableProperty]
    private ObservableCollection<ReferenceItem> _items = new();

    [ObservableProperty]
    private ReferenceItem? _selectedItem;

    [ObservableProperty]
    private string _editText = string.Empty;

    [ObservableProperty]
    private bool _isEditing;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    private int _editingId;

    public ReferenceDataViewModel(ReferenceDataService service, ReferenceTableConfig config, string title)
    {
        _service = service;
        _config = config;
        Title = title;
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        try
        {
            var items = await _service.GetAllAsync(_config);
            Items = new ObservableCollection<ReferenceItem>(items);
            StatusMessage = $"{Items.Count} {Title.ToLower()}";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading {Title}: {ex.Message}";
        }
    }

    [RelayCommand]
    private void New()
    {
        _editingId = 0;
        EditText = string.Empty;
        IsEditing = true;
        StatusMessage = "Enter new entry";
    }

    [RelayCommand]
    private void Edit()
    {
        if (SelectedItem == null)
        {
            StatusMessage = "Select an item to edit";
            return;
        }

        _editingId = SelectedItem.Id;
        EditText = SelectedItem.Description;
        IsEditing = true;
        StatusMessage = $"Editing: {SelectedItem.Description}";
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        var text = EditText.Trim();
        if (string.IsNullOrWhiteSpace(text))
        {
            StatusMessage = "Description cannot be blank";
            return;
        }

        if (text.Length > _config.MaxLength)
            text = text.Substring(0, _config.MaxLength);

        try
        {
            if (_editingId == 0)
            {
                await _service.AddAsync(_config, text);
                StatusMessage = "Added";
            }
            else
            {
                await _service.UpdateAsync(_config, _editingId, text);
                StatusMessage = "Updated";
            }

            IsEditing = false;
            EditText = string.Empty;
            await LoadAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error saving: {ex.Message}";
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        IsEditing = false;
        EditText = string.Empty;
        StatusMessage = "Cancelled";
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (SelectedItem == null)
        {
            StatusMessage = "Select an item to delete";
            return;
        }

        try
        {
            await _service.DeleteAsync(_config, SelectedItem.Id);
            StatusMessage = "Deleted";
            await LoadAsync();
        }
        catch (Exception ex)
        {
            // Most likely a foreign-key violation - something still references this entry.
            StatusMessage = $"Failed to delete - it may still be in use ({ex.Message})";
        }
    }
}
