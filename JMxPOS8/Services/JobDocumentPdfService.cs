using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using JMxPOS8.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace JMxPOS8.Services;

// Job docket/quote printing (ROADMAP.md Phase 2/3). Renders a real PDF of the intake
// docket (the legacy app's "Customer Service Agreement", clsPrintDocs3.vb's
// mbPrintNewJobForm_PageEvent) using the actual job data - not a mechanical
// pixel-for-pixel port of that GDI+ layout, but the same content.
//
// Deliberately PDF-only for now, per direct instruction (2026-09-01): physical
// printer/cash-drawer hardware output is a separate, DEFERRED feature - real-world
// experience with services like Syncro's print integration (cash drawers routinely
// failing to open) means that needs its own real evaluation, not a rushed add-on
// here. This is the seam where that plugs in later: a hardware print path would
// render from the same JobRecord data this class already assembles (either by
// printing this same PDF via CUPS, or building a parallel ESC/POS renderer) rather
// than needing a different data pipeline. Nothing here is stubbed - every document
// this renders is a complete, real PDF with real job data.
public class JobDocumentPdfService
{
    private static bool _fontResolverRegistered;

    public JobDocumentPdfService()
    {
        // PDFsharp 6.x has no font enumeration outside Windows GDI - without a
        // resolver, XFont throws on Linux/macOS the first time a page is drawn.
        // GlobalFontSettings is process-wide, so only register once.
        if (!_fontResolverRegistered)
        {
            PdfSharp.Fonts.GlobalFontSettings.FontResolver = new PdfFontResolver();
            _fontResolverRegistered = true;
        }
    }

    // Legacy's 5 other document types (job labels, receipt, quote, service/delivery
    // record, customer maintenance report - clsPrintDocs3.vb) are mechanical
    // extensions of this same pattern (new Render* method, same PdfDocumentBuilder
    // helpers) - not built yet. Quote printing specifically also needs the
    // underlying job-quote workflow built first (no "awaiting quote" job state
    // exists yet), which is real feature work beyond printing itself.
    public byte[] RenderNewJobDocket(JobRecord job, IReadOnlyDictionary<string, decimal> labourRates, string businessName)
    {
        using var document = new PdfDocument();
        var page = document.AddPage();
        page.Size = PdfSharp.PageSize.A4;
        using var gfx = XGraphics.FromPdfPage(page);

        var fontTitle = new XFont("Arial", 16, XFontStyleEx.Bold);
        var fontHeading = new XFont("Arial", 11, XFontStyleEx.Bold);
        var fontBody = new XFont("Arial", 9, XFontStyleEx.Regular);
        var fontSmall = new XFont("Arial", 8, XFontStyleEx.Regular);

        double margin = 40;
        double width = page.Width.Point - margin * 2;
        double y = margin;

        y = DrawHeader(gfx, businessName, job, fontTitle, fontSmall, margin, width, y);
        y = DrawCustomerSection(gfx, job, fontHeading, fontBody, margin, width, y);
        y = DrawGoodsSection(gfx, job, fontHeading, fontBody, margin, width, y);

        if (!string.IsNullOrWhiteSpace(job.Username))
            y = DrawUserLogonSection(gfx, job, fontHeading, fontBody, margin, width, y);

        y = DrawProblemSection(gfx, job, fontHeading, fontBody, margin, width, y);
        y = DrawFlagsAndTerms(gfx, job, labourRates, fontHeading, fontBody, margin, width, y);
        DrawSignatureBlock(gfx, fontBody, margin, width, page.Height.Point - margin - 60);

        using var stream = new MemoryStream();
        document.Save(stream, false);
        return stream.ToArray();
    }

    // Renders and writes to a file under the user's home directory (the "print to
    // PDF for now" destination) so it can be opened with the OS's own PDF viewer -
    // see JobViewModel.PrintDocketCommand for how it's opened.
    public string RenderNewJobDocketToFile(JobRecord job, IReadOnlyDictionary<string, decimal> labourRates, string businessName)
    {
        var bytes = RenderNewJobDocket(job, labourRates, businessName);
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "JobMatixDocuments");
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, $"Job-{job.JobId}-Docket-{DateTime.Now:yyyyMMdd-HHmmss}.pdf");
        File.WriteAllBytes(path, bytes);
        return path;
    }

    private static double DrawHeader(XGraphics gfx, string businessName, JobRecord job, XFont fontTitle, XFont fontSmall,
        double margin, double width, double y)
    {
        gfx.DrawString(businessName, fontTitle, XBrushes.Black, new XRect(margin, y, width, 24), XStringFormats.TopLeft);
        gfx.DrawString("Customer Service Agreement", fontTitle, XBrushes.DarkSlateGray,
            new XRect(margin, y, width, 24), XStringFormats.TopRight);
        y += 28;

        gfx.DrawString($"Job #{job.JobId}    Received: {job.DateCreated:dd-MMM-yyyy}    Received by: {job.RcvdStaffName}",
            fontSmall, XBrushes.Black, new XRect(margin, y, width, 14), XStringFormats.TopLeft);
        y += 16;

        gfx.DrawLine(XPens.Black, margin, y, margin + width, y);
        return y + 10;
    }

    private static double DrawCustomerSection(XGraphics gfx, JobRecord job, XFont fontHeading, XFont fontBody,
        double margin, double width, double y)
    {
        gfx.DrawString("Customer", fontHeading, XBrushes.Black, new XPoint(margin, y));
        y += 16;

        var customerLine = string.IsNullOrWhiteSpace(job.CustomerCompany) || job.CustomerCompany is "N/A" or "--"
            ? job.CustomerName
            : $"{job.CustomerCompany} ({job.CustomerName})";
        y = DrawWrappedLine(gfx, $"{customerLine}   [{job.CustomerBarcode}]", fontBody, margin, width, y);
        y = DrawWrappedLine(gfx, $"Phone: {job.CustomerPhone}    Mobile: {job.CustomerMobile}", fontBody, margin, width, y);

        var techLine = string.IsNullOrWhiteSpace(job.NominatedTech) || job.NominatedTech == "N/A"
            ? "Priority: " + PriorityLabel(job.Priority) + "   (job not reserved to a specific technician)"
            : $"Priority: {PriorityLabel(job.Priority)}   Nominated Tech: {job.NominatedTech}";
        y = DrawWrappedLine(gfx, techLine, fontBody, margin, width, y);

        return y + 8;
    }

    private static double DrawGoodsSection(XGraphics gfx, JobRecord job, XFont fontHeading, XFont fontBody,
        double margin, double width, double y)
    {
        gfx.DrawString("Goods In Care", fontHeading, XBrushes.Black, new XPoint(margin, y));
        y += 16;

        y = DrawWrappedLine(gfx, $"Type: {job.GoodsInCare}    Brand: {job.GoodsBrand}    Model: {job.GoodsModel}",
            fontBody, margin, width, y);

        if (!string.IsNullOrWhiteSpace(job.GoodsOther) && job.GoodsOther != "N/A")
            y = DrawWrappedLine(gfx, $"Other goods/extras in care: {job.GoodsOther}", fontBody, margin, width, y);

        return y + 8;
    }

    private static double DrawUserLogonSection(XGraphics gfx, JobRecord job, XFont fontHeading, XFont fontBody,
        double margin, double width, double y)
    {
        gfx.DrawString("User Logon Details", fontHeading, XBrushes.Black, new XPoint(margin, y));
        y += 16;
        y = DrawWrappedLine(gfx, $"Username: {job.Username}    Password: {job.UserPassword}", fontBody, margin, width, y);
        return y + 8;
    }

    private static double DrawProblemSection(XGraphics gfx, JobRecord job, XFont fontHeading, XFont fontBody,
        double margin, double width, double y)
    {
        gfx.DrawString("Problem Reported", fontHeading, XBrushes.Black, new XPoint(margin, y));
        y += 16;

        if (!string.IsNullOrWhiteSpace(job.ProblemSymptoms))
            y = DrawWrappedLine(gfx, $"Symptoms: {job.ProblemSymptoms}", fontBody, margin, width, y);

        y = DrawWrappedLine(gfx, job.ProblemShort, fontBody, margin, width, y);
        if (!string.IsNullOrWhiteSpace(job.ProblemLong))
            y = DrawWrappedLine(gfx, job.ProblemLong, fontBody, margin, width, y);

        var flags = new List<string>();
        if (job.DataBackupReqd) flags.Add("Data backup required");
        if (job.DataDiskReqd) flags.Add("Data disk required");
        if (flags.Count > 0)
            y = DrawWrappedLine(gfx, string.Join("   |   ", flags), fontBody, margin, width, y);

        return y + 8;
    }

    private static double DrawFlagsAndTerms(XGraphics gfx, JobRecord job, IReadOnlyDictionary<string, decimal> labourRates,
        XFont fontHeading, XFont fontBody, double margin, double width, double y)
    {
        if (job.SystemUnderWarranty)
        {
            gfx.DrawRectangle(XBrushes.Gold, margin, y, width, 18);
            gfx.DrawString("** System Under Warranty **", fontBody, XBrushes.Black,
                new XRect(margin, y, width, 18), XStringFormats.Center);
            y += 24;
        }

        gfx.DrawString("Terms and Conditions", fontHeading, XBrushes.Black, new XPoint(margin, y));
        y += 16;

        var rate = RateForPriority(job.Priority, labourRates);
        var rateText = rate > 0 ? rate.ToString("C", CultureInfo.GetCultureInfo("en-AU")) + " per hour" : "n/a";
        y = DrawWrappedLine(gfx,
            $"Standard service fee (Priority {PriorityLabel(job.Priority)}): {rateText}. " +
            "Goods left uncollected beyond 30 days of notification may incur storage fees. " +
            "[Placeholder terms - replace with this business's actual terms and conditions text.]",
            fontBody, margin, width, y);

        return y + 8;
    }

    private static void DrawSignatureBlock(XGraphics gfx, XFont fontBody, double margin, double width, double y)
    {
        gfx.DrawLine(XPens.Black, margin, y, margin + width * 0.55, y);
        gfx.DrawString("Customer signature (print name)", fontBody, XBrushes.Black, new XPoint(margin, y + 12));

        var dateX = margin + width * 0.65;
        gfx.DrawLine(XPens.Black, dateX, y, margin + width, y);
        gfx.DrawString("Date submitted", fontBody, XBrushes.Black, new XPoint(dateX, y + 12));
    }

    // Simple word-wrap at the given width - PDFsharp's DrawString with a bounding
    // XRect clips rather than wraps, so multi-line body text needs manual wrapping.
    private static double DrawWrappedLine(XGraphics gfx, string text, XFont font, double x, double width, double y)
    {
        if (string.IsNullOrWhiteSpace(text))
            return y;

        // Several legacy free-text fields (goodsincare's packed Type/Brand/Model/Serial
        // format especially - see ROADMAP.md Phase 0.4) embed raw tabs/newlines as
        // field separators. PDFsharp's DrawString doesn't render tabs as visible
        // whitespace, so "Black\tGENERIC" silently became "BlackGENERIC" until this -
        // found by actually opening a rendered PDF against real data, not assumed.
        text = text.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ');
        while (text.Contains("  "))
            text = text.Replace("  ", " ");

        var lineHeight = font.GetHeight() + 2;
        var words = text.Split(' ');
        var line = "";

        foreach (var word in words)
        {
            var candidate = line.Length == 0 ? word : $"{line} {word}";
            if (gfx.MeasureString(candidate, font).Width > width && line.Length > 0)
            {
                gfx.DrawString(line, font, XBrushes.Black, new XPoint(x, y));
                y += lineHeight;
                line = word;
            }
            else
            {
                line = candidate;
            }
        }

        if (line.Length > 0)
        {
            gfx.DrawString(line, font, XBrushes.Black, new XPoint(x, y));
            y += lineHeight;
        }

        return y;
    }

    private static string PriorityLabel(string priority) => priority switch
    {
        "3" => "3 (Urgent)",
        "2" => "2 (Standard)",
        "H" => "High",
        "B" => "Backorder/On-hold",
        _ => priority,
    };

    // internal (not private) so JobService.CompleteJobAndInvoiceAsync can reuse the exact
    // same priority->rate mapping for the labour invoice line, rather than duplicating it.
    internal static decimal RateForPriority(string priority, IReadOnlyDictionary<string, decimal> rates)
    {
        var key = priority switch
        {
            "3" => "LabourHourlyRatePriority3",
            "2" => "LabourHourlyRatePriority2",
            _ => "LabourHourlyRatePriority1",
        };
        return rates.TryGetValue(key, out var rate) ? rate : 0m;
    }
}
