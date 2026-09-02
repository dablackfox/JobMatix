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
    private readonly EmailService _emailService;

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

    // Never loaded from the stored hash (see LoadStaffToForm) - blank means "leave the
    // existing password unchanged" when editing, or "no password set" for a new staff
    // member. Only a non-blank value here gets hashed and saved.
    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _passwordHint = string.Empty;

    private int _editingStaffId;
    private string _editingStaffOriginalPasswordHash = string.Empty;

    // SMS gateway settings (ROADMAP.md Phase 3) - lives on the Staff admin screen since it's
    // operational configuration, not a per-staff field. DirectSMS only - the real legacy
    // SystemInfo config (checked against the restored legacy database) shows it's the only
    // one of the 4 legacy-supported gateways that was ever actually configured/used.
    [ObservableProperty]
    private string _smsUsername = string.Empty;

    [ObservableProperty]
    private string _smsPassword = string.Empty;

    [ObservableProperty]
    private string _smsFromNumber = string.Empty;

    [ObservableProperty]
    private string _smsSettingsStatusMessage = string.Empty;

    // Email (SMTP) settings - same rationale as SMS above.
    [ObservableProperty]
    private string _emailHost = string.Empty;

    [ObservableProperty]
    private decimal _emailPort = 587;

    [ObservableProperty]
    private bool _emailUseSsl = true;

    [ObservableProperty]
    private string _emailUsername = string.Empty;

    [ObservableProperty]
    private string _emailPassword = string.Empty;

    [ObservableProperty]
    private string _emailFromAddress = string.Empty;

    [ObservableProperty]
    private string _emailSettingsStatusMessage = string.Empty;

    public StaffViewModel(StaffService staffService, SmsService smsService, EmailService emailService)
    {
        _staffService = staffService;
        _smsService = smsService;
        _emailService = emailService;
    }

    [RelayCommand]
    public async Task LoadSmsSettingsAsync()
    {
        var settings = await _smsService.GetSettingsAsync();
        SmsUsername = settings.Username;
        SmsPassword = settings.Password;
        SmsFromNumber = settings.FromNumber;
    }

    [RelayCommand]
    private async Task SaveSmsSettings()
    {
        await _smsService.SaveSettingsAsync(new SmsGatewaySettings
        {
            Username = SmsUsername.Trim(),
            Password = SmsPassword,
            FromNumber = SmsFromNumber.Trim()
        });
        SmsSettingsStatusMessage = "SMS gateway settings saved";
    }

    [RelayCommand]
    public async Task LoadEmailSettingsAsync()
    {
        var settings = await _emailService.GetSettingsAsync();
        EmailHost = settings.Host;
        EmailPort = settings.Port;
        EmailUseSsl = settings.UseSsl;
        EmailUsername = settings.Username;
        EmailPassword = settings.Password;
        EmailFromAddress = settings.FromAddress;
    }

    [RelayCommand]
    private async Task SaveEmailSettings()
    {
        await _emailService.SaveSettingsAsync(new EmailSettings
        {
            Host = EmailHost.Trim(),
            Port = (int)EmailPort,
            UseSsl = EmailUseSsl,
            Username = EmailUsername.Trim(),
            Password = EmailPassword,
            FromAddress = EmailFromAddress.Trim()
        });
        EmailSettingsStatusMessage = "Email (SMTP) settings saved";
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
                // Blank input preserves whatever hash (or empty string, for a new staff
                // member) was already on the record - never overwrite it with plaintext,
                // and never re-hash an already-hashed value just because the form round-
                // tripped it.
                Password = string.IsNullOrEmpty(Password) ? _editingStaffOriginalPasswordHash : PasswordHasher.Hash(Password),
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
        // Deliberately NOT Password = staff.Password - the stored value is a hash, not
        // something to round-trip back into an editable plaintext-entry field. Blank means
        // "unchanged" in SaveStaffAsync; the real stored hash is kept out-of-band here so
        // it's preserved correctly if the form is saved without touching this field.
        Password = string.Empty;
        _editingStaffOriginalPasswordHash = staff.Password;
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
        _editingStaffOriginalPasswordHash = string.Empty;
        PasswordHint = string.Empty;
    }
}
