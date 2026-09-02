using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace JMxPOS8.Services;

// Attachment storage (ROADMAP.md Phase 0.4/1) needs a real file-open dialog, but
// ViewModels in this app have no reference to a Window/Control to get an
// IStorageProvider from (no DI container here - MainWindowViewModel constructs
// everything directly). This grabs the desktop lifetime's MainWindow the same way
// App.axaml.cs itself does, rather than threading a TopLevel reference through every
// ViewModel that might ever need a file dialog.
public static class FilePickerService
{
    public record PickedFile(string FileName, byte[] Content);

    public static async Task<PickedFile?> PickFileAsync(string title)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow == null)
            return null;

        var files = await desktop.MainWindow.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false
        });

        var file = files.FirstOrDefault();
        if (file == null)
            return null;

        await using var stream = await file.OpenReadAsync();
        using var memoryStream = new System.IO.MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return new PickedFile(file.Name, memoryStream.ToArray());
    }
}
