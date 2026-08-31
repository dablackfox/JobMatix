using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class JobViewModel : ViewModelBase
{
    private readonly JobService _jobService;
    private readonly CustomerService _customerService;
    private readonly StaffService _staffService;
    private readonly StockService _stockService;
    private readonly SmsService _smsService;
    private readonly EmailService _emailService;

    public ObservableCollection<JobRecord> OpenJobs { get; } = new();
    public ObservableCollection<JobPartLine> Parts { get; } = new();

    [ObservableProperty]
    private JobRecord? _selectedJob;

    // New job intake fields
    [ObservableProperty]
    private string _newCustomerBarcode = "";

    [ObservableProperty]
    private string _newGoodsInCare = "";

    [ObservableProperty]
    private string _newGoodsBrand = "";

    [ObservableProperty]
    private string _newGoodsModel = "";

    [ObservableProperty]
    private string _newProblemShort = "";

    [ObservableProperty]
    private string _newProblemLong = "";

    [ObservableProperty]
    private string _newSymptoms = "";

    [ObservableProperty]
    private string _newPriority = "H";

    [ObservableProperty]
    private bool _newDataBackupReqd;

    [ObservableProperty]
    private bool _newDataDiskReqd;

    [ObservableProperty]
    private bool _newSystemUnderWarranty;

    [ObservableProperty]
    private string _newStaffBarcode = "";

    // Action-panel fields, reused across whichever action the selected job's status allows
    [ObservableProperty]
    private string _actionStaffBarcode = "";

    [ObservableProperty]
    private string _actionServiceNotes = "";

    [ObservableProperty]
    private string _scanPartBarcode = "";

    [ObservableProperty]
    private string _notifyMessage = "";

    [ObservableProperty]
    private string _notifyEmailSubject = "";

    [ObservableProperty]
    private string _statusMessage = "";

    public bool CanStartWork => SelectedJob != null && SelectedJob.JobStatus is "10-Created" or "20-Suspended" or "23-InProcessSusp";
    public bool CanSuspend => SelectedJob != null && SelectedJob.JobStatus is "30-Started" or "33-InProcess";
    public bool CanSendToQa => CanSuspend;
    public bool CanReopenFromQa => SelectedJob != null && SelectedJob.JobStatus is "40-QA" or "43-InProcessQA";
    public bool CanComplete => CanReopenFromQa;
    public bool CanDeliver => SelectedJob?.JobStatus == "50-Completed";
    public bool CanCancel => SelectedJob != null && SelectedJob.JobStatus is not ("50-Completed" or "70-Delivered" or "97-Cancelled");
    public bool CanAddParts => SelectedJob != null && !SelectedJob.JobStatus.StartsWith("70") && !SelectedJob.JobStatus.StartsWith("97");

    public JobViewModel(JobService jobService, CustomerService customerService, StaffService staffService, StockService stockService, SmsService smsService, EmailService emailService)
    {
        _jobService = jobService;
        _customerService = customerService;
        _staffService = staffService;
        _stockService = stockService;
        _smsService = smsService;
        _emailService = emailService;
    }

    public async Task LoadOpenJobsAsync()
    {
        OpenJobs.Clear();
        foreach (var job in await _jobService.GetOpenJobsAsync())
            OpenJobs.Add(job);
    }

    // Selecting a job to view it IS "opening it for edit" in the legacy app (there's no
    // separate view-only mode) - flips a locked "InProcess" status variant so anyone else
    // looking at the job list sees it's in use, and releases it when you move away.
    private int? _lockedJobId;

    // Set while we're re-assigning SelectedJob to a freshly-reloaded copy of the same job
    // (after a status change, or after applying the lock) - avoids re-entering the
    // open/close-lock dance for what isn't really a new selection.
    private bool _suppressSelectionSideEffects;

    partial void OnSelectedJobChanged(JobRecord? value)
    {
        RaiseCanExecuteChanged();
        if (_suppressSelectionSideEffects)
            return;
        _ = HandleSelectionChangeAsync(value);
    }

    private async Task HandleSelectionChangeAsync(JobRecord? newJob)
    {
        if (_lockedJobId.HasValue)
            await _jobService.CloseEditAsync(_lockedJobId.Value);
        _lockedJobId = null;

        Parts.Clear();
        if (newJob == null)
            return;

        await _jobService.OpenForEditAsync(newJob.JobId);
        _lockedJobId = newJob.JobId;

        var refreshed = await _jobService.GetJobByIdAsync(newJob.JobId);
        if (refreshed != null)
        {
            var index = OpenJobs.IndexOf(newJob);
            if (index >= 0) OpenJobs[index] = refreshed;

            _suppressSelectionSideEffects = true;
            SelectedJob = refreshed;
            _suppressSelectionSideEffects = false;
        }

        foreach (var part in await _jobService.GetJobPartsAsync(newJob.JobId))
            Parts.Add(part);
    }

    private void RaiseCanExecuteChanged()
    {
        OnPropertyChanged(nameof(CanStartWork));
        OnPropertyChanged(nameof(CanSuspend));
        OnPropertyChanged(nameof(CanSendToQa));
        OnPropertyChanged(nameof(CanReopenFromQa));
        OnPropertyChanged(nameof(CanComplete));
        OnPropertyChanged(nameof(CanDeliver));
        OnPropertyChanged(nameof(CanCancel));
        OnPropertyChanged(nameof(CanAddParts));
    }

    [RelayCommand]
    private async Task CreateJob()
    {
        if (string.IsNullOrWhiteSpace(NewProblemShort))
        {
            StatusMessage = "Enter a problem description";
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

        var job = new JobRecord
        {
            Priority = NewPriority,
            GoodsInCare = NewGoodsInCare.Trim(),
            GoodsBrand = NewGoodsBrand.Trim(),
            GoodsModel = NewGoodsModel.Trim(),
            ProblemShort = NewProblemShort.Trim(),
            ProblemLong = NewProblemLong.Trim(),
            ProblemSymptoms = NewSymptoms.Trim(),
            DataBackupReqd = NewDataBackupReqd,
            DataDiskReqd = NewDataDiskReqd,
            SystemUnderWarranty = NewSystemUnderWarranty,
            RcvdStaffName = staff.DocketName
        };

        if (!string.IsNullOrWhiteSpace(NewCustomerBarcode))
        {
            var customer = await _customerService.FindCustomerByBarcodeAsync(NewCustomerBarcode.Trim());
            if (customer != null)
            {
                job.CustomerBarcode = customer.Barcode;
                job.RmCustomerId = customer.CustomerId;
                job.CustomerName = customer.CustomerName;
                job.CustomerPhone = customer.BusinessPhone;
                job.CustomerMobile = customer.Mobile;
            }
        }

        var created = await _jobService.CreateJobAsync(job);
        OpenJobs.Insert(0, created);
        SelectedJob = created;
        StatusMessage = $"Job #{created.JobId} created";

        NewCustomerBarcode = "";
        NewGoodsInCare = "";
        NewGoodsBrand = "";
        NewGoodsModel = "";
        NewProblemShort = "";
        NewProblemLong = "";
        NewSymptoms = "";
        NewDataBackupReqd = false;
        NewDataDiskReqd = false;
        NewSystemUnderWarranty = false;
        NewStaffBarcode = "";
    }

    [RelayCommand]
    private async Task StartWork()
    {
        if (SelectedJob == null) return;
        if (string.IsNullOrWhiteSpace(ActionStaffBarcode))
        {
            StatusMessage = "Enter the technician's staff barcode";
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
            await _jobService.StartWorkAsync(SelectedJob.JobId, staff.DocketName, staff.StaffId);
            StatusMessage = $"Job #{SelectedJob.JobId} started by {staff.DocketName}";
            await RefreshSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task Suspend()
    {
        if (SelectedJob == null) return;
        try
        {
            await _jobService.SuspendAsync(SelectedJob.JobId);
            StatusMessage = $"Job #{SelectedJob.JobId} suspended";
            await RefreshSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task SendToQa()
    {
        if (SelectedJob == null) return;
        try
        {
            await _jobService.SendToQaAsync(SelectedJob.JobId);
            StatusMessage = $"Job #{SelectedJob.JobId} sent to QA";
            await RefreshSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task ReopenFromQa()
    {
        if (SelectedJob == null) return;
        try
        {
            await _jobService.ReopenFromQaAsync(SelectedJob.JobId);
            StatusMessage = $"Job #{SelectedJob.JobId} reopened from QA";
            await RefreshSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task Complete()
    {
        if (SelectedJob == null) return;
        if (Parts.Count == 0 && string.IsNullOrWhiteSpace(ActionServiceNotes))
            StatusMessage = "Warning: no parts and no service notes recorded - completing anyway";
        try
        {
            await _jobService.CompleteAsync(SelectedJob.JobId, ActionServiceNotes.Trim());
            StatusMessage = $"Job #{SelectedJob.JobId} completed";
            await RefreshSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task Deliver()
    {
        if (SelectedJob == null) return;
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
            await _jobService.DeliverAsync(SelectedJob.JobId, staff.DocketName, staff.StaffId);
            StatusMessage = $"Job #{SelectedJob.JobId} delivered to customer";
            OpenJobs.Remove(SelectedJob);
            SelectedJob = null;
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task CancelJob()
    {
        if (SelectedJob == null) return;
        try
        {
            await _jobService.CancelAsync(SelectedJob.JobId);
            StatusMessage = $"Job #{SelectedJob.JobId} cancelled";
            OpenJobs.Remove(SelectedJob);
            SelectedJob = null;
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task AddPart()
    {
        if (SelectedJob == null || string.IsNullOrWhiteSpace(ScanPartBarcode))
            return;

        var result = await _jobService.AddPartByBarcodeAsync(SelectedJob.JobId, ScanPartBarcode.Trim(), _stockService, null, "");
        if (result == JobService.AddPartResult.NotFound)
        {
            StatusMessage = $"No stock item found for barcode '{ScanPartBarcode}'";
        }
        else
        {
            StatusMessage = $"Added part '{ScanPartBarcode}'";
            Parts.Clear();
            foreach (var part in await _jobService.GetJobPartsAsync(SelectedJob.JobId))
                Parts.Add(part);
        }
        ScanPartBarcode = "";
    }

    [RelayCommand]
    private async Task RemovePart(JobPartLine? part)
    {
        if (part == null) return;
        await _jobService.RemovePartAsync(part.PartId);
        Parts.Remove(part);
    }

    [RelayCommand]
    private async Task SendSms()
    {
        if (SelectedJob == null) return;
        if (string.IsNullOrWhiteSpace(NotifyMessage))
        {
            StatusMessage = "Enter a message to send";
            return;
        }

        var result = await _smsService.SendSmsAsync(SelectedJob.CustomerMobile, NotifyMessage.Trim());
        if (!result.Success)
        {
            StatusMessage = string.IsNullOrWhiteSpace(result.ErrorMessage)
                ? $"SMS failed: {result.RawResponse}"
                : $"SMS failed: {result.ErrorMessage}";
            return;
        }

        await _jobService.AppendNotificationAsync(SelectedJob.JobId, $"SMS sent: {NotifyMessage.Trim()}");
        StatusMessage = $"SMS sent to {SelectedJob.CustomerMobile}";
        NotifyMessage = "";
    }

    [RelayCommand]
    private async Task SendEmail()
    {
        if (SelectedJob == null) return;
        if (string.IsNullOrWhiteSpace(NotifyMessage))
        {
            StatusMessage = "Enter a message to send";
            return;
        }
        if (SelectedJob.RmCustomerId == null)
        {
            StatusMessage = "This job has no linked customer to email";
            return;
        }

        var customer = await _customerService.GetCustomerByIdAsync(SelectedJob.RmCustomerId.Value);
        if (customer == null || string.IsNullOrWhiteSpace(customer.EmailAddress))
        {
            StatusMessage = "No email address on file for this customer";
            return;
        }

        string subject = string.IsNullOrWhiteSpace(NotifyEmailSubject) ? $"Your job #{SelectedJob.JobId}" : NotifyEmailSubject.Trim();
        var result = await _emailService.SendEmailAsync(customer.EmailAddress, subject, NotifyMessage.Trim());
        if (!result.Success)
        {
            StatusMessage = $"Email failed: {result.ErrorMessage}";
            return;
        }

        await _jobService.AppendNotificationAsync(SelectedJob.JobId, $"Email sent: {subject} - {NotifyMessage.Trim()}");
        StatusMessage = $"Email sent to {customer.EmailAddress}";
        NotifyMessage = "";
        NotifyEmailSubject = "";
    }

    private async Task RefreshSelectedAsync()
    {
        if (SelectedJob == null) return;
        int jobId = SelectedJob.JobId;

        // The status transition that just ran happened while this job was still open for
        // editing - re-apply the lock so its displayed status reflects that (matching the
        // legacy "in use" InProcess variants), rather than showing as unlocked until the
        // next time someone selects it.
        await _jobService.OpenForEditAsync(jobId);
        _lockedJobId = jobId;

        var refreshed = await _jobService.GetJobByIdAsync(jobId);
        if (refreshed == null) return;

        var existing = OpenJobs.FirstOrDefault(j => j.JobId == jobId);
        if (existing != null)
        {
            var index = OpenJobs.IndexOf(existing);
            OpenJobs[index] = refreshed;
        }

        // Suppress the lock-transfer logic in OnSelectedJobChanged - we're refreshing the
        // same job we already hold the lock for, not switching to a different one.
        _suppressSelectionSideEffects = true;
        SelectedJob = refreshed;
        _suppressSelectionSideEffects = false;
    }
}
