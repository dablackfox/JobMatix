using System;
using System.IO;
using PdfSharp.Fonts;

namespace JMxPOS8.Services;

// PDFsharp 6.x has no built-in font enumeration outside Windows GDI - without this,
// XFont throws "No appropriate font found" on Linux/macOS (hit immediately when
// testing JobDocumentPdfService against real data). Resolves the "Arial"
// regular/bold requested there to whatever's actually on disk for the current OS -
// Liberation Sans is metrically compatible with Arial and ships on most Linux
// distros; falls back to DejaVu Sans, then real Arial where present (Windows/macOS).
public class PdfFontResolver : IFontResolver
{
    private static readonly string[] RegularCandidates =
    {
        "/usr/share/fonts/liberation-sans-fonts/LiberationSans-Regular.ttf",
        "/usr/share/fonts/truetype/liberation/LiberationSans-Regular.ttf",
        "/usr/share/fonts/dejavu-sans-fonts/DejaVuSans.ttf",
        "/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf",
        @"C:\Windows\Fonts\arial.ttf",
        "/Library/Fonts/Arial.ttf",
        "/System/Library/Fonts/Supplemental/Arial.ttf",
    };

    private static readonly string[] BoldCandidates =
    {
        "/usr/share/fonts/liberation-sans-fonts/LiberationSans-Bold.ttf",
        "/usr/share/fonts/truetype/liberation/LiberationSans-Bold.ttf",
        "/usr/share/fonts/dejavu-sans-fonts/DejaVuSans-Bold.ttf",
        "/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf",
        @"C:\Windows\Fonts\arialbd.ttf",
        "/Library/Fonts/Arial Bold.ttf",
        "/System/Library/Fonts/Supplemental/Arial Bold.ttf",
    };

    public string DefaultFontName => "PdfDefault";

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        return new FontResolverInfo(isBold ? "PdfDefaultBold" : "PdfDefault");
    }

    public byte[] GetFont(string faceName)
    {
        var candidates = faceName == "PdfDefaultBold" ? BoldCandidates : RegularCandidates;
        foreach (var path in candidates)
        {
            if (File.Exists(path))
                return File.ReadAllBytes(path);
        }

        throw new InvalidOperationException(
            $"No usable font file found for '{faceName}' - checked: {string.Join(", ", candidates)}");
    }
}
