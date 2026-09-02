using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    private readonly JobDocumentPdfService _pdfService = new();
    private readonly JobTimeService _jobTimeService;
    private readonly ReferenceDataService _referenceDataService;
    private readonly Avalonia.Threading.DispatcherTimer _timerDisplayTick;
    private readonly System.Threading.Timer _onSiteReminderTimer;

    // Status → display bucket, in the order groups should appear. Mirrors the legacy
    // tree-style ticket list (New/In Progress/QA/etc. with a count per group) that this
    // port had dropped in favour of one flat list - restored per direct feedback
    // (2026-09-01). The "InProcess" locked variants fold into their parent bucket; the
    // 🔒 icon on each row (JobRecord.IsLocked) still shows which ones are locked.
    private static readonly (string Label, string[] Statuses)[] StatusBuckets =
    {
        ("Waitlisted", new[] { "05-WaitListed" }),
        ("New", new[] { "10-Created" }),
        ("Suspended", new[] { "20-Suspended", "23-InProcessSusp" }),
        ("In Progress", new[] { "30-Started", "33-InProcess" }),
        ("QA", new[] { "40-QA", "43-InProcessQA" }),
        ("Completed", new[] { "50-Completed" }),
    };

    private static string BucketLabelForStatus(string status)
    {
        foreach (var bucket in StatusBuckets)
        {
            if (bucket.Statuses.Contains(status))
                return bucket.Label;
        }
        return status; // unexpected status - show it under its own literal label rather than hiding it
    }

    public ObservableCollection<JobRecord> OpenJobs { get; } = new();

    // Rebuilt from OpenJobs whenever it changes (load/refresh-in-place/remove all raise
    // CollectionChanged) - see the subscription in the constructor.
    public ObservableCollection<JobStatusGroup> GroupedOpenJobs { get; } = new();

    private void RebuildGroupedOpenJobs()
    {
        var byLabel = new System.Collections.Generic.Dictionary<string, JobStatusGroup>();
        foreach (var job in OpenJobs)
        {
            var label = BucketLabelForStatus(job.JobStatus);
            if (!byLabel.TryGetValue(label, out var group))
            {
                group = new JobStatusGroup(label);
                byLabel[label] = group;
            }
            group.Jobs.Add(job);
        }

        GroupedOpenJobs.Clear();
        foreach (var bucket in StatusBuckets)
        {
            if (byLabel.TryGetValue(bucket.Label, out var group) && group.Jobs.Count > 0)
                GroupedOpenJobs.Add(group);
        }
        // Any status that didn't match a known bucket (data drift, or a status added
        // later without updating StatusBuckets) still shows up, just at the end.
        foreach (var kvp in byLabel)
        {
            if (!StatusBuckets.Any(b => b.Label == kvp.Key))
                GroupedOpenJobs.Add(kvp.Value);
        }
    }

    // Ticket search (ROADMAP.md / direct feedback 2026-09-01) - the Tickets tab had no
    // way to find a job outside the open-jobs list at all. Search results replace the
    // grouped tree while active; clearing the search restores it.
    [ObservableProperty]
    private string _ticketSearchText = "";

    [ObservableProperty]
    private bool _isSearchActive;

    public ObservableCollection<JobRecord> SearchResults { get; } = new();

    partial void OnTicketSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            IsSearchActive = false;
            SearchResults.Clear();
        }
    }

    [RelayCommand]
    private async Task SearchTickets()
    {
        if (string.IsNullOrWhiteSpace(TicketSearchText))
        {
            IsSearchActive = false;
            return;
        }

        IsSearchActive = true;
        var results = await _jobService.SearchJobsAsync(TicketSearchText.Trim());
        SearchResults.Clear();
        foreach (var job in results)
            SearchResults.Add(job);
        StatusMessage = $"Found {SearchResults.Count} matching ticket(s)";
    }

    [RelayCommand]
    private void ClearTicketSearch()
    {
        TicketSearchText = "";
        IsSearchActive = false;
        SearchResults.Clear();
    }
    public ObservableCollection<JobPartLine> Parts { get; } = new();

    // Quote Parts (Phase 2, 2026-09-01) - proposed components, not yet real Parts. Loaded
    // alongside Parts/Notes whenever a ticket is opened, and shown whenever there are any
    // (not gated to only "while Quotation Required") so the 7,842 rows of historical data
    // stay visible on tickets that have long since moved past that status.
    public ObservableCollection<QuotePartLine> QuoteParts { get; } = new();

    // Section shows whenever there's anything to see (current proposal or historical
    // record) OR the ticket is actively awaiting approval (so staff can start proposing
    // components on a fresh Quotation Required ticket that has none yet).
    public bool ShowQuotePartsSection => QuoteParts.Count > 0 || SelectedJob?.IsQuotationRequired == true;

    [ObservableProperty]
    private string _scanQuotePartBarcode = "";

    [ObservableProperty]
    private int _quotePartQuantity = 1;

    [RelayCommand]
    private async Task AddQuotePart()
    {
        if (SelectedJob == null || string.IsNullOrWhiteSpace(ScanQuotePartBarcode))
            return;

        var result = await _jobService.AddQuotePartByBarcodeAsync(SelectedJob.JobId, ScanQuotePartBarcode.Trim(), QuotePartQuantity < 1 ? 1 : QuotePartQuantity, _stockService);
        if (result == JobService.AddQuotePartResult.NotFound)
        {
            StatusMessage = $"No stock item found for barcode '{ScanQuotePartBarcode}'";
        }
        else
        {
            StatusMessage = $"Added quoted part '{ScanQuotePartBarcode}'";
            QuoteParts.Clear();
            foreach (var part in await _jobService.GetQuotePartsAsync(SelectedJob.JobId))
                QuoteParts.Add(part);
            ScanQuotePartBarcode = "";
            QuotePartQuantity = 1;
        }
    }

    [RelayCommand]
    private async Task RemoveQuotePart(QuotePartLine? part)
    {
        if (part == null || SelectedJob == null) return;
        await _jobService.RemoveQuotePartAsync(part.QuotePartId);
        QuoteParts.Remove(part);
    }

    // Ticket notes (job_notes) - a running log distinct from the single-value legacy
    // servicenotes/diagnosis fields, with a public/private flag so an internal-only note
    // can be told apart from a customer-facing one at a glance (color-coded in XAML).
    public ObservableCollection<JobNote> Notes { get; } = new();

    [ObservableProperty]
    private string _newNoteText = "";

    // Defaults to private - the more cautious default for an internal tool with no
    // customer-facing note channel wired up yet (a "public" note today just means
    // "written as if the customer might read it", not that it's actually sent anywhere).
    [ObservableProperty]
    private bool _newNoteIsPrivate = true;

    [RelayCommand]
    private async Task AddNote()
    {
        if (SelectedJob == null) return;
        if (string.IsNullOrWhiteSpace(NewNoteText))
        {
            StatusMessage = "Enter a note before adding it";
            return;
        }
        if (string.IsNullOrWhiteSpace(ActionStaffBarcode))
        {
            StatusMessage = "Enter your staff barcode to attribute this note";
            return;
        }

        var staff = await _staffService.FindStaffByBarcodeAsync(ActionStaffBarcode.Trim());
        if (staff == null)
        {
            StatusMessage = $"Staff not found for '{ActionStaffBarcode}'";
            return;
        }

        await _jobService.AddJobNoteAsync(SelectedJob.JobId, NewNoteText.Trim(), NewNoteIsPrivate, staff.DocketName);
        NewNoteText = "";
        await LoadNotesAsync(SelectedJob.JobId);
        StatusMessage = "Note added";
    }

    private async Task LoadNotesAsync(int jobId)
    {
        Notes.Clear();
        foreach (var note in await _jobService.GetJobNotesAsync(jobId))
            Notes.Add(note);
    }

    // Ticket time tracking (direct feedback, 2026-09-01) - concurrent by design, see
    // JobTimeService's own header comment. CurrentJobTimer is this ticket's own running
    // entry if one exists (any ticket can have at most one at a time; a whole separate
    // job can have its own independent one concurrently - see RunningTimers below).
    [ObservableProperty]
    private JobTimeEntry? _currentJobTimer;

    [ObservableProperty]
    private string _timerNoteText = "";

    public string CurrentJobTimerElapsedDisplay => CurrentJobTimer == null ? "" : FormatElapsed(CurrentJobTimer.Elapsed);

    // Avalonia's "!" binding negation only works on bool - binding IsVisible directly to
    // the nullable CurrentJobTimer (or "!CurrentJobTimer") silently fails the type
    // conversion and leaves both the "start" and "running" panels visible at once.
    public bool HasCurrentJobTimer => CurrentJobTimer != null;

    partial void OnCurrentJobTimerChanged(JobTimeEntry? value)
    {
        OnPropertyChanged(nameof(CurrentJobTimerElapsedDisplay));
        OnPropertyChanged(nameof(HasCurrentJobTimer));
    }

    private static string FormatElapsed(TimeSpan elapsed) =>
        elapsed.TotalHours >= 1 ? $"{(int)elapsed.TotalHours}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}" : $"{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";

    private async Task LoadCurrentJobTimerAsync(int jobId)
    {
        CurrentJobTimer = await _jobTimeService.GetRunningTimerForJobAsync(jobId);
        TimerNoteText = "";
    }

    [RelayCommand]
    private async Task StartTimer()
    {
        if (SelectedJob == null) return;
        if (string.IsNullOrWhiteSpace(ActionStaffBarcode))
        {
            StatusMessage = "Enter your staff barcode to start a timer";
            return;
        }
        var staff = await _staffService.FindStaffByBarcodeAsync(ActionStaffBarcode.Trim());
        if (staff == null)
        {
            StatusMessage = $"Staff not found for '{ActionStaffBarcode}'";
            return;
        }

        CurrentJobTimer = await _jobTimeService.StartTimerAsync(SelectedJob.JobId, staff.StaffId, staff.DocketName);
        StatusMessage = $"Timer started on job #{SelectedJob.JobId}";
        await RefreshRunningTimersAsync();
    }

    [RelayCommand]
    private async Task StopTimer()
    {
        if (SelectedJob == null || CurrentJobTimer == null) return;

        await _jobTimeService.StopTimerAsync(CurrentJobTimer.EntryId, TimerNoteText.Trim(), billable: true);

        // Auto-log a note summarizing the session, per direct feedback ("auto add time to
        // notes") - private by default, matching the same reasoning as every other
        // internal-only note default in this app.
        var elapsed = CurrentJobTimer.Elapsed;
        var summary = $"Tracked {FormatElapsed(elapsed)} ({CurrentJobTimer.StaffName})" +
                      (string.IsNullOrWhiteSpace(TimerNoteText) ? "" : $": {TimerNoteText.Trim()}");
        await _jobService.AddJobNoteAsync(SelectedJob.JobId, summary, isPrivate: true, CurrentJobTimer.StaffName);

        StatusMessage = $"Timer stopped - {FormatElapsed(elapsed)} logged";
        CurrentJobTimer = null;
        TimerNoteText = "";
        await LoadNotesAsync(SelectedJob.JobId);
        await RefreshRunningTimersAsync();
    }

    // Backs the status-bar "N running" indicator, refreshed after every start/stop plus
    // whenever the Tickets tab loads (MainWindowViewModel).
    public ObservableCollection<RunningTimerSummary> RunningTimers { get; } = new();

    public string RunningTimersCountDisplay => RunningTimers.Count switch
    {
        0 => "",
        1 => "⏱ 1 timer running",
        var n => $"⏱ {n} timers running"
    };

    public bool HasRunningTimers => RunningTimers.Count > 0;

    public async Task RefreshRunningTimersAsync()
    {
        var timers = await _jobTimeService.GetRunningTimersAsync();
        RunningTimers.Clear();
        foreach (var t in timers)
            RunningTimers.Add(t);
        OnPropertyChanged(nameof(RunningTimersCountDisplay));
        OnPropertyChanged(nameof(HasRunningTimers));
    }

    // The status-bar indicator's click-through - "a filtered list of the jobs with
    // timers". Reuses the existing search-results overlay rather than a new UI surface.
    [RelayCommand]
    public async Task ShowJobsWithRunningTimers()
    {
        SelectedJob = null;
        await RefreshRunningTimersAsync();
        IsSearchActive = true;
        TicketSearchText = "";
        SearchResults.Clear();
        foreach (var timer in RunningTimers)
        {
            var job = await _jobService.GetJobByIdAsync(timer.JobId);
            if (job != null) SearchResults.Add(job);
        }
        StatusMessage = $"Showing {SearchResults.Count} ticket(s) with a running timer";
    }

    [ObservableProperty]
    private JobRecord? _selectedJob;

    // New job intake fields
    [ObservableProperty]
    private string _newCustomerBarcode = "";

    [ObservableProperty]
    private string _newGoodsModel = "";

    [ObservableProperty]
    private string _newProblemShort = "";

    [ObservableProperty]
    private string _newProblemLong = "";

    // Goods type / Brand / Symptoms at intake - retired as free text 2026-09-02 in favour
    // of the GoodsTypes/Brands/Symptoms reference-data tables (ROADMAP.md line 164's
    // deferred follow-up). Options are loaded from the DB (LoadIntakeReferenceDataAsync),
    // not static/hardcoded like PriorityOptions/CustomerInstructionOptions above, since
    // they're user-maintained via the Reference Data tab. No FK on the jobs table for any
    // of these - CreateJob() just takes the selected item's Description into the existing
    // varchar column, same as before.
    public ObservableCollection<ReferenceItem> GoodsTypeOptions { get; } = new();
    public ObservableCollection<ReferenceItem> BrandOptions { get; } = new();
    public ObservableCollection<ReferenceItem> SymptomOptions { get; } = new();

    [ObservableProperty]
    private ReferenceItem? _selectedGoodsType;

    [ObservableProperty]
    private ReferenceItem? _selectedBrand;

    [ObservableProperty]
    private ReferenceItem? _selectedSymptom;

    // ComboBox.SelectedItem bound directly to a plain string only works against a real
    // ItemsSource of the same type - same fix as TransactionLookupViewModel's
    // LookupType/DatePeriod and ReturnAuthorizationViewModel's Origin/ReturnResult.
    public static readonly string[] PriorityOptions = { "H", "B" };

    [ObservableProperty]
    private string _newPriority = "H";

    [ObservableProperty]
    private bool _newDataBackupReqd;

    [ObservableProperty]
    private bool _newDataDiskReqd;

    [ObservableProperty]
    private bool _newSystemUnderWarranty;

    // On-site job scheduling (ROADMAP.md "What Changed" #7) - a nominated tech (resolved
    // to a real staff record, same barcode-lookup pattern as NewStaffBarcode/RcvdStaffName)
    // and a promised date/time, so a job flagged ON-SITE at intake (via the Goods Type
    // dropdown above) has a real recipient and due date for the reminder poller. Both
    // optional - only meaningful for on-site jobs, but not restricted to them since a
    // promised date is a generally useful field for any job.
    [ObservableProperty]
    private string _newNominatedTechBarcode = "";

    [ObservableProperty]
    private DateTimeOffset? _newDatePromised;

    [ObservableProperty]
    private string _newStaffBarcode = "";

    // Customer Instruction (direct feedback, 2026-09-01) - restores a legacy field this
    // port dropped: a required 3-way approval flag ("Quotation Required" / "Proceed with
    // Service" / "Proceed only to Cost Limit") baked into ProblemShort as a marker string.
    // See ProblemDescriptionHelper for the marker format shared with ~26k migrated jobs.
    public record CustomerInstructionOption(string Label, CustomerInstruction Instruction);

    public static ObservableCollection<CustomerInstructionOption> CustomerInstructionOptions { get; } = new()
    {
        new("Quotation Required", CustomerInstruction.QuotationRequired),
        new("Proceed with Service", CustomerInstruction.ProceedWithService),
        new("Proceed only to Cost Limit", CustomerInstruction.ProceedToLimit),
    };

    [ObservableProperty]
    private CustomerInstructionOption? _newCustomerInstruction;

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

    // Waitlisted -> New (direct feedback, 2026-09-01): the item hasn't physically arrived
    // yet while waitlisted, so nothing else should be actionable until it's checked in.
    public bool CanCheckIn => SelectedJob?.JobStatus == "05-WaitListed";
    public bool CanStartWork => SelectedJob != null && SelectedJob.JobStatus is "10-Created" or "20-Suspended" or "23-InProcessSusp";
    // Undo an accidental Start Work (direct feedback, 2026-09-01).
    public bool CanReturnToNew => SelectedJob != null && SelectedJob.JobStatus is "30-Started" or "33-InProcess";
    public bool CanSuspend => SelectedJob != null && SelectedJob.JobStatus is "30-Started" or "33-InProcess";
    public bool CanSendToQa => CanSuspend;
    public bool CanReopenFromQa => SelectedJob != null && SelectedJob.JobStatus is "40-QA" or "43-InProcessQA";
    public bool CanComplete => CanReopenFromQa;
    public bool CanDeliver => SelectedJob?.JobStatus == "50-Completed";
    public bool CanCancel => SelectedJob != null && SelectedJob.JobStatus is not ("50-Completed" or "70-Delivered" or "97-Cancelled");
    public bool CanAddParts => SelectedJob != null && !SelectedJob.JobStatus.StartsWith("70") && !SelectedJob.JobStatus.StartsWith("97");

    public JobViewModel(JobService jobService, CustomerService customerService, StaffService staffService, StockService stockService, SmsService smsService, EmailService emailService, JobTimeService jobTimeService, ReferenceDataService referenceDataService)
    {
        _jobService = jobService;
        _customerService = customerService;
        _staffService = staffService;
        _stockService = stockService;
        _smsService = smsService;
        _emailService = emailService;
        _jobTimeService = jobTimeService;
        _referenceDataService = referenceDataService;
        OpenJobs.CollectionChanged += (_, _) => RebuildGroupedOpenJobs();
        QuoteParts.CollectionChanged += (_, _) => OnPropertyChanged(nameof(ShowQuotePartsSection));

        // Ticks the live "elapsed" displays (the per-ticket running timer and every entry
        // in the global running-timers list) once a second - nothing here re-queries the
        // database, it just nudges bound properties that compute elapsed time from an
        // already-fetched StartTime against DateTime.Now.
        _timerDisplayTick = new Avalonia.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timerDisplayTick.Tick += (_, _) =>
        {
            if (CurrentJobTimer != null) OnPropertyChanged(nameof(CurrentJobTimerElapsedDisplay));
            OnPropertyChanged(nameof(RunningTimersCountDisplay));
        };
        _timerDisplayTick.Start();

        // Staff SMS reminder for on-site jobs (ROADMAP.md "What Changed" #7) - runs for the
        // app's whole lifetime, independent of which tab is selected, not just while the
        // Tickets tab happens to be open. A background ThreadPool timer, not the UI-thread
        // DispatcherTimer above - it does real DB/network work, so it must never block the
        // UI, and (per the legacy implementation's own hard-learned lesson,
        // clsStaffReminders.vb) opens its own short-lived DB connection per poll rather than
        // holding one open, to avoid transaction conflicts with whatever the UI thread is
        // doing. Every exception is caught inside RunOnSiteReminderPollAsync - a
        // System.Threading.Timer callback that throws would otherwise crash the process.
        _onSiteReminderTimer = new System.Threading.Timer(
            _ => _ = RunOnSiteReminderPollAsync(),
            null,
            TimeSpan.FromMinutes(1),
            TimeSpan.FromMinutes(15));
    }

    private async Task RunOnSiteReminderPollAsync()
    {
        try
        {
            if (!await _jobService.IsOnSiteSmsRemindersEnabledAsync())
                return;

            var today = DateTime.Now.Date;
            foreach (var job in await _jobService.GetOnSiteJobsDueForReminderAsync(today))
            {
                if (string.IsNullOrWhiteSpace(job.TechMobile))
                {
                    Console.WriteLine($"[ONSITE-REMINDER] Job #{job.JobId}: no resolvable tech mobile number, skipped");
                    continue;
                }

                var when = job.DatePromised.HasValue ? job.DatePromised.Value.ToString("h:mmtt") : "today";
                var message = $"On-site job #{job.JobId} {when} - {job.CustomerName}, {job.CustomerPhone}. - JobMatix";
                var result = await _smsService.SendSmsAsync(job.TechMobile, message);
                if (result.Success)
                {
                    await _jobService.MarkOnSiteReminderSentAsync(job.JobId, today);
                    await _jobService.AppendNotificationAsync(job.JobId, $"On-site reminder SMS sent to {job.TechLabel} ({job.TechMobile})");
                    Console.WriteLine($"[ONSITE-REMINDER] Sent for job #{job.JobId} to {job.TechMobile}");
                }
                else
                {
                    Console.WriteLine($"[ONSITE-REMINDER] Send failed for job #{job.JobId}: {result.ErrorMessage}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ONSITE-REMINDER] Poll failed: {ex.Message}");
        }
    }

    public async Task LoadOpenJobsAsync()
    {
        OpenJobs.Clear();
        foreach (var job in await _jobService.GetOpenJobsAsync())
            OpenJobs.Add(job);
    }

    public async Task LoadIntakeReferenceDataAsync()
    {
        GoodsTypeOptions.Clear();
        foreach (var item in await _referenceDataService.GetAllAsync(ReferenceTables.GoodsTypes))
            GoodsTypeOptions.Add(item);

        BrandOptions.Clear();
        foreach (var item in await _referenceDataService.GetAllAsync(ReferenceTables.Brands))
            BrandOptions.Add(item);

        SymptomOptions.Clear();
        foreach (var item in await _referenceDataService.GetAllAsync(ReferenceTables.Symptoms))
            SymptomOptions.Add(item);
    }

    // Cross-navigation entry point (e.g. clicking a ticket number on the Customer
    // screen's Tickets sub-tab) - the target job may be closed/delivered/cancelled and
    // therefore not in OpenJobs at all, so this fetches it directly rather than
    // requiring a prior list load. Goes through the normal SelectedJob setter so the
    // usual open/lock bookkeeping (HandleSelectionChangeAsync) still applies - that
    // logic already no-ops safely for a job that isn't in an editable status.
    public async Task OpenJobByIdAsync(int jobId)
    {
        var job = await _jobService.GetJobByIdAsync(jobId);
        if (job == null)
        {
            StatusMessage = $"Job #{jobId} not found";
            return;
        }

        SelectedJob = job;
        StatusMessage = $"Viewing job #{jobId}";
    }

    // "Back to list" - there was previously no way to deselect a ticket once opened.
    // Goes through the normal SelectedJob setter so the existing lock-release logic in
    // HandleSelectionChangeAsync still runs (it already handles a null newJob correctly).
    [RelayCommand]
    private void CloseTicket()
    {
        SelectedJob = null;
    }

    // Selecting a job to view it IS "opening it for edit" in the legacy app (there's no
    // separate view-only mode) - flips a locked "InProcess" status variant so anyone else
    // looking at the job list sees it's in use, and releases it when you move away.
    private int? _lockedJobId;

    // Set while we're re-assigning SelectedJob to a freshly-reloaded copy of the same job
    // (after a status change, or after applying the lock) - avoids re-entering the
    // open/close-lock dance for what isn't really a new selection.
    private bool _suppressSelectionSideEffects;

    // Drives the Tickets tab's layout: the intake form + search/grouped list (list mode)
    // vs. the full-width ticket detail (detail mode) are mutually exclusive - direct
    // feedback (2026-09-01) that viewing a ticket should use the whole tab, not just the
    // right-hand column, with the New Ticket form only shown in list mode.
    public bool IsViewingTicket => SelectedJob != null;

    partial void OnSelectedJobChanged(JobRecord? value)
    {
        RaiseCanExecuteChanged();
        OnPropertyChanged(nameof(IsViewingTicket));
        OnPropertyChanged(nameof(ShowQuotePartsSection));
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
        Notes.Clear();
        QuoteParts.Clear();
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
        foreach (var quotePart in await _jobService.GetQuotePartsAsync(newJob.JobId))
            QuoteParts.Add(quotePart);

        await LoadNotesAsync(newJob.JobId);
        await LoadCurrentJobTimerAsync(newJob.JobId);
    }

    private void RaiseCanExecuteChanged()
    {
        OnPropertyChanged(nameof(CanApproveQuote));
        OnPropertyChanged(nameof(CanCheckIn));
        OnPropertyChanged(nameof(CanStartWork));
        OnPropertyChanged(nameof(CanReturnToNew));
        OnPropertyChanged(nameof(CanSuspend));
        OnPropertyChanged(nameof(CanSendToQa));
        OnPropertyChanged(nameof(CanReopenFromQa));
        OnPropertyChanged(nameof(CanComplete));
        OnPropertyChanged(nameof(CanDeliver));
        OnPropertyChanged(nameof(CanCancel));
        OnPropertyChanged(nameof(CanAddParts));
        RebuildStatusOptions();
    }

    // Unified "change status" control (direct feedback, 2026-09-01): replaces the old row
    // of separate Start Work/Suspend/Send to QA/Back to Work/Deliver/Cancel buttons with
    // one dropdown - "valid moves only", not a free-form override. Each option wraps the
    // exact same existing method (same validation, same side effects, e.g. StartWork still
    // requires a staff barcode) - this only consolidates the UI trigger.
    public record StatusOption(string Label, Func<Task> Execute, bool RequiresServiceNotes = false);

    [ObservableProperty]
    private ObservableCollection<StatusOption> _availableStatusOptions = new();

    [ObservableProperty]
    private StatusOption? _selectedStatusOption;

    public bool ShowCompleteNotes => SelectedStatusOption?.RequiresServiceNotes == true;

    partial void OnSelectedStatusOptionChanged(StatusOption? value) => OnPropertyChanged(nameof(ShowCompleteNotes));

    private void RebuildStatusOptions()
    {
        AvailableStatusOptions.Clear();
        SelectedStatusOption = null;
        if (SelectedJob == null) return;

        if (CanCheckIn) AvailableStatusOptions.Add(new("Check In (arrived - move to New)", CheckIn));
        if (CanStartWork) AvailableStatusOptions.Add(new("Start Work", StartWork));
        if (CanReturnToNew) AvailableStatusOptions.Add(new("Move Back to New (undo Start)", ReturnToNew));
        if (CanSuspend) AvailableStatusOptions.Add(new("Suspend", Suspend));
        if (CanSendToQa) AvailableStatusOptions.Add(new("Send to QA", SendToQa));
        if (CanReopenFromQa) AvailableStatusOptions.Add(new("Back to Work (from QA)", ReopenFromQa));
        if (CanComplete) AvailableStatusOptions.Add(new("Complete", Complete, RequiresServiceNotes: true));
        if (CanDeliver) AvailableStatusOptions.Add(new("Deliver", Deliver));
        if (CanCancel) AvailableStatusOptions.Add(new("Cancel Ticket", CancelJob));
    }

    [RelayCommand]
    private async Task ChangeStatus()
    {
        if (SelectedStatusOption == null)
        {
            StatusMessage = "Select a status to change to";
            return;
        }
        await SelectedStatusOption.Execute();
        SelectedStatusOption = null;
    }

    [RelayCommand]
    private async Task CreateJob()
    {
        if (string.IsNullOrWhiteSpace(NewProblemShort))
        {
            StatusMessage = "Enter a problem description";
            return;
        }
        if (NewCustomerInstruction == null)
        {
            StatusMessage = "Select a Customer Instruction (Quotation Required / Proceed with Service / Proceed to Cost Limit)";
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
            GoodsInCare = SelectedGoodsType?.Description ?? "",
            GoodsBrand = SelectedBrand?.Description ?? "",
            GoodsModel = NewGoodsModel.Trim(),
            // Marker appended at the end, matching ucChildNewJob.vb's own convention
            // exactly - existing detection/stripping logic checks for the marker anywhere
            // in the string, but keeping the same shape as the ~26k migrated jobs avoids
            // any doubt about it.
            ProblemShort = $"{NewProblemShort.Trim()} {ProblemDescriptionHelper.MarkerFor(NewCustomerInstruction.Instruction)}".Trim(),
            ProblemLong = NewProblemLong.Trim(),
            ProblemSymptoms = SelectedSymptom?.Description ?? "",
            DataBackupReqd = NewDataBackupReqd,
            DataDiskReqd = NewDataDiskReqd,
            SystemUnderWarranty = NewSystemUnderWarranty,
            RcvdStaffName = staff.DocketName,
            DatePromised = NewDatePromised?.DateTime
        };

        if (!string.IsNullOrWhiteSpace(NewNominatedTechBarcode))
        {
            var nominatedTech = await _staffService.FindStaffByBarcodeAsync(NewNominatedTechBarcode.Trim());
            if (nominatedTech != null)
                job.NominatedTech = nominatedTech.DocketName;
        }

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
        SelectedGoodsType = null;
        SelectedBrand = null;
        NewGoodsModel = "";
        NewProblemShort = "";
        NewProblemLong = "";
        SelectedSymptom = null;
        NewDataBackupReqd = false;
        NewDataDiskReqd = false;
        NewSystemUnderWarranty = false;
        NewStaffBarcode = "";
        NewCustomerInstruction = null;
        NewNominatedTechBarcode = "";
        NewDatePromised = null;
    }

    // Job docket/quote printing (ROADMAP.md Phase 2/3) - PDF only for now, by direct
    // instruction (2026-09-01): real physical printer/cash-drawer hardware is a
    // separate, deferred feature (see JobDocumentPdfService's own header comment for
    // why). Opens the rendered PDF with the OS's default viewer so it can actually be
    // looked at, rather than just silently writing a file nobody sees.
    [RelayCommand]
    private async Task PrintDocket()
    {
        if (SelectedJob == null)
        {
            StatusMessage = "Select a job to print a docket for";
            return;
        }

        try
        {
            var rates = await _jobService.GetLabourRatesAsync();
            var path = _pdfService.RenderNewJobDocketToFile(SelectedJob, rates, "JobMatix");
            StatusMessage = $"Docket saved: {path}";
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }
        catch (System.Exception ex)
        {
            StatusMessage = $"Error printing docket: {ex.Message}";
        }
    }

    // Approve a quote awaiting customer confirmation (direct feedback, 2026-09-01) -
    // "standard service charge and quote on what it will cost to fix" scenario: the ticket
    // was created as Quotation Required, the customer calls back to approve, and this
    // upgrades it to Proceed with Service. Not shown for Proceed to Limit, since that's
    // already a form of approval.
    public bool CanApproveQuote => SelectedJob?.IsQuotationRequired == true;

    [RelayCommand]
    private async Task ApproveQuote()
    {
        if (SelectedJob == null) return;
        var jobId = SelectedJob.JobId;
        await _jobService.ApproveQuoteAsync(jobId);
        StatusMessage = $"Job #{jobId} approved - proceeding with service";
        await RefreshSelectedAsync();

        // ApproveQuoteAsync just converted any quote_job_parts into real parts - reload
        // both so the ticket page reflects that immediately (RefreshSelectedAsync only
        // reloads SelectedJob itself, not Parts/QuoteParts).
        Parts.Clear();
        foreach (var part in await _jobService.GetJobPartsAsync(jobId))
            Parts.Add(part);
        QuoteParts.Clear();
        foreach (var quotePart in await _jobService.GetQuotePartsAsync(jobId))
            QuoteParts.Add(quotePart);
    }

    [RelayCommand]
    private async Task CheckIn()
    {
        if (SelectedJob == null) return;
        try
        {
            await _jobService.CheckInAsync(SelectedJob.JobId);
            StatusMessage = $"Job #{SelectedJob.JobId} checked in";
            await RefreshSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
    }

    [RelayCommand]
    private async Task ReturnToNew()
    {
        if (SelectedJob == null) return;
        try
        {
            await _jobService.ReturnToNewAsync(SelectedJob.JobId);
            StatusMessage = $"Job #{SelectedJob.JobId} moved back to New";
            await RefreshSelectedAsync();
        }
        catch (System.Exception ex) { StatusMessage = $"Error: {ex.Message}"; }
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

        // Direct feedback, 2026-09-01: serials are "not required for the quote but are
        // required before job is completed" - real, blocking validation (not a warning),
        // checked first so it takes priority over the softer "no parts" warning below.
        var missingSerial = Parts.FirstOrDefault(p => p.MissingRequiredSerial);
        if (missingSerial != null)
        {
            StatusMessage = $"'{missingSerial.PartDescr}' needs a serial number before this job can be completed";
            return;
        }

        if (Parts.Count == 0 && string.IsNullOrWhiteSpace(ActionServiceNotes))
            StatusMessage = "Warning: no parts and no service notes recorded - completing anyway";

        // Attribute the note like historical data already does (real migrated
        // servicenotes are prefixed "StaffName: dd-MMM-yyyy  HH:mm") - the app itself
        // never wrote that prefix for a new completion until now, so every ticket
        // completed through this port showed notes with no author at all.
        if (string.IsNullOrWhiteSpace(ActionStaffBarcode))
        {
            StatusMessage = "Enter your staff barcode to attribute the service notes";
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
            var attributedNotes = string.IsNullOrWhiteSpace(ActionServiceNotes)
                ? ""
                : $"{staff.DocketName}: {System.DateTime.Now:dd-MMM-yyyy  HH:mm}\n{ActionServiceNotes.Trim()}";

            // Completion now also generates a real invoice from the job's parts + billable
            // time (Phase 4, 2026-09-01) - see JobService.CompleteJobAndInvoiceAsync for the
            // full design (mirrors SaleService.CommitSaleAsync's transaction shape).
            var invoiceId = await _jobService.CompleteJobAndInvoiceAsync(SelectedJob.JobId, staff.StaffId, staff.DocketName, attributedNotes);
            StatusMessage = $"Job #{SelectedJob.JobId} completed - invoice #{invoiceId}";
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

    // Two-step serial capture (direct feedback, 2026-09-01: serials "required before job is
    // completed") mirrors SaleViewModel's ItemRequiresSerial pattern - the common
    // non-serialized case still adds in one scan, same as before; a serialized item stops
    // and shows a serial field, then the same barcode is re-submitted with the serial filled
    // in (AddPartByBarcodeAsync's SerialRequired check runs before any INSERT, so re-calling
    // it after filling in the serial is safe - nothing was written on the first attempt).
    [ObservableProperty]
    private bool _partRequiresSerial;

    [ObservableProperty]
    private string _partSerialNumber = "";

    [RelayCommand]
    private async Task AddPart()
    {
        if (SelectedJob == null || string.IsNullOrWhiteSpace(ScanPartBarcode))
            return;

        var result = await _jobService.AddPartByBarcodeAsync(
            SelectedJob.JobId, ScanPartBarcode.Trim(), _stockService, null, "",
            PartRequiresSerial ? PartSerialNumber.Trim() : null);

        if (result == JobService.AddPartResult.NotFound)
        {
            StatusMessage = $"No stock item found for barcode '{ScanPartBarcode}'";
            PartRequiresSerial = false;
            PartSerialNumber = "";
        }
        else if (result == JobService.AddPartResult.SerialRequired)
        {
            PartRequiresSerial = true;
            StatusMessage = "This item needs a serial number - enter it and add again";
        }
        else
        {
            StatusMessage = $"Added part '{ScanPartBarcode}'";
            Parts.Clear();
            foreach (var part in await _jobService.GetJobPartsAsync(SelectedJob.JobId))
                Parts.Add(part);
            ScanPartBarcode = "";
            PartRequiresSerial = false;
            PartSerialNumber = "";
        }
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

// One status bucket in the Tickets tab's grouped view (e.g. "In Progress") - built fresh
// by JobViewModel.RebuildGroupedOpenJobs() each time, so Count/HeaderText don't need to
// be independently observable; only IsExpanded does. Toggled via a plain Button rather
// than Avalonia's built-in Expander - the Expander's chevron icon resource wasn't
// resolving in this environment (rendered its internal theme key as literal text,
// "Avalonia.Controls|Expander:down", with a stray gap above the list) - found by
// actually running the app, not assumed. A hand-rolled header sidesteps that entirely.
public partial class JobStatusGroup : ObservableObject
{
    public string StatusLabel { get; }
    public ObservableCollection<JobRecord> Jobs { get; } = new();
    public int Count => Jobs.Count;
    public string HeaderText => $"{StatusLabel} ({Count})";
    public string ExpandGlyph => IsExpanded ? "▼" : "▶"; // ▼ / ▶

    [ObservableProperty]
    private bool _isExpanded = true;

    public JobStatusGroup(string statusLabel)
    {
        StatusLabel = statusLabel;
    }

    partial void OnIsExpandedChanged(bool value) => OnPropertyChanged(nameof(ExpandGlyph));

    [RelayCommand]
    private void ToggleExpanded() => IsExpanded = !IsExpanded;
}
