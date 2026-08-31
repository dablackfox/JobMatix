using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class StaffViewModel : ViewModelBase
{
    private readonly StaffService _staffService;
    private readonly SmsService _smsService;

    [ObservableProperty]
    private ObservableCollection<Staff> _staffMembers = new();

    [ObservableProperty]
    private Staff? _selectedStaff;

    partial void OnSelectedStaffChanged(Staff? value)
    {
        if (value != null && !IsEditing)
        {
            LoadStaffToForm(value);
            StatusMessage = $"Viewing: {value.DocketName}";
        }
    }

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    // Staff detail fields for add/edit
    [ObservableProperty]
    private bool _isEditing;

    [ObservableProperty]
    private string _barcode = string.Empty;

    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _docketName = string.Empty;

    [ObservableProperty]
    private string _position = string.Empty;

    [ObservableProperty]
    private bool _isAdministrator;

    [ObservableProperty]
    private bool _inactive;

    [ObservableProperty]
    private DateTimeOffset? _dateOfBirth;

    [ObservableProperty]
    private string _address = string.Empty;

    [ObservableProperty]
    private string _suburb = string.Empty;

    [ObservableProperty]
    private string _state = string.Empty;

    [ObservableProperty]
    private string _postcode = string.Empty;

    [ObservableProperty]
    private string _homePhone = string.Empty;

    [ObservableProperty]
    private string _mobile = string.Empty;

    [ObservableProperty]
    private string _emailAddress = string.Empty;

    [ObservableProperty]
    private string _status = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _passwordHint = string.Empty;

    private int _editingStaffId;

    // SMS gateway settings (ROADMAP.md Phase 3) - lives on the Staff admin screen since it's
    // operational configuration, not a per-staff field.
    [ObservableProperty]
    private string _smsGatewaySelection = "SmsBoss";

    [ObservableProperty]
    private string _smsUsername = string.Empty;

    [ObservableProperty]
    private string _smsPassword = string.Empty;

    [ObservableProperty]
    private string _smsFromNumber = string.Empty;

    [ObservableProperty]
    private string _smsSettingsStatusMessage = string.Empty;

    public StaffViewModel(StaffService staffService, SmsService smsService)
    {
        _staffService = staffService;
        _smsService = smsService;
    }

    [RelayCommand]
    public async Task LoadSmsSettingsAsync()
    {
        var settings = await _smsService.GetSettingsAsync();
        SmsGatewaySelection = settings.Gateway.ToString();
        SmsUsername = settings.Username;
        SmsPassword = settings.Password;
        SmsFromNumber = settings.FromNumber;
    }

    [RelayCommand]
    private async Task SaveSmsSettings()
    {
        if (!Enum.TryParse<SmsGateway>(SmsGatewaySelection, out var gateway))
        {
            SmsSettingsStatusMessage = $"Unknown gateway '{SmsGatewaySelection}'";
            return;
        }

        await _smsService.SaveSettingsAsync(new SmsGatewaySettings
        {
            Gateway = gateway,
            Username = SmsUsername.Trim(),
            Password = SmsPassword,
            FromNumber = SmsFromNumber.Trim()
        });
        SmsSettingsStatusMessage = "SMS gateway settings saved";
    }

    partial void OnSearchTextChanged(string value)
    {
        _ = SearchStaffAsync();
    }

    [RelayCommand]
    public async Task LoadStaffAsync()
    {
        try
        {
            StatusMessage = "Loading staff...";
            var items = await _staffService.GetAllStaffAsync();

            await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
            {
                StaffMembers = new ObservableCollection<Staff>(items);
            });

            StatusMessage = $"Loaded {StaffMembers.Count} staff";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading staff: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task SearchStaffAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadStaffAsync();
                return;
            }

            StatusMessage = "Searching...";
            var items = await _staffService.SearchStaffAsync(SearchText);

            StaffMembers.Clear();
            foreach (var item in items)
                StaffMembers.Add(item);

            StatusMessage = $"Found {StaffMembers.Count} staff";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error searching: {ex.Message}";
        }
    }

    [RelayCommand]
    private void NewStaff()
    {
        ClearForm();
        IsEditing = true;
        _editingStaffId = 0;
        StatusMessage = "Enter staff details";
    }

    [RelayCommand]
    private void EditStaff()
    {
        if (SelectedStaff == null)
        {
            StatusMessage = "Please select a staff member to edit";
            return;
        }

        LoadStaffToForm(SelectedStaff);
        IsEditing = true;
        StatusMessage = "Editing staff member";
    }

    [RelayCommand]
    private async Task SaveStaffAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Barcode))
            {
                StatusMessage = "Barcode/staff number is required";
                return;
            }

            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                StatusMessage = "First and last name are required";
                return;
            }

            var staff = new Staff
            {
                StaffId = _editingStaffId,
                Barcode = Barcode.Trim(),
                FirstName = FirstName.Trim(),
                LastName = LastName.Trim(),
                DocketName = string.IsNullOrWhiteSpace(DocketName) ? FirstName.Trim() : DocketName.Trim(),
                Position = Position.Trim(),
                IsAdministrator = IsAdministrator,
                Inactive = Inactive,
                DateOfBirth = DateOfBirth?.DateTime,
                Address = Address.Trim(),
                Suburb = Suburb.Trim(),
                State = State.Trim(),
                Postcode = Postcode.Trim(),
                HomePhone = HomePhone.Trim(),
                Mobile = Mobile.Trim(),
                EmailAddress = EmailAddress.Trim(),
                Status = Status.Trim(),
                Password = Password,
                PasswordHint = PasswordHint.Trim()
            };

            if (_editingStaffId == 0)
            {
                await _staffService.AddStaffAsync(staff);
                StatusMessage = "Staff member added successfully";
            }
            else
            {
                await _staffService.UpdateStaffAsync(staff);
                StatusMessage = "Staff member updated successfully";
            }

            IsEditing = false;
            await LoadStaffAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error saving staff: {ex.Message}";
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
    private async Task DeleteStaffAsync()
    {
        if (SelectedStaff == null)
        {
            StatusMessage = "Please select a staff member to delete";
            return;
        }

        try
        {
            await _staffService.DeleteStaffAsync(SelectedStaff.StaffId);
            StatusMessage = "Staff member deactivated";
            await LoadStaffAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error deactivating staff: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        SearchText = string.Empty;
        await LoadStaffAsync();
    }

    private void LoadStaffToForm(Staff staff)
    {
        _editingStaffId = staff.StaffId;
        Barcode = staff.Barcode;
        FirstName = staff.FirstName;
        LastName = staff.LastName;
        DocketName = staff.DocketName;
        Position = staff.Position;
        IsAdministrator = staff.IsAdministrator;
        Inactive = staff.Inactive;
        DateOfBirth = staff.DateOfBirth.HasValue ? new DateTimeOffset(staff.DateOfBirth.Value) : null;
        Address = staff.Address;
        Suburb = staff.Suburb;
        State = staff.State;
        Postcode = staff.Postcode;
        HomePhone = staff.HomePhone;
        Mobile = staff.Mobile;
        EmailAddress = staff.EmailAddress;
        Status = staff.Status;
        Password = staff.Password;
        PasswordHint = staff.PasswordHint;
    }

    private void ClearForm()
    {
        _editingStaffId = 0;
        Barcode = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        DocketName = string.Empty;
        Position = string.Empty;
        IsAdministrator = false;
        Inactive = false;
        DateOfBirth = null;
        Address = string.Empty;
        Suburb = string.Empty;
        State = string.Empty;
        Postcode = string.Empty;
        HomePhone = string.Empty;
        Mobile = string.Empty;
        EmailAddress = string.Empty;
        Status = string.Empty;
        Password = string.Empty;
        PasswordHint = string.Empty;
    }
}
