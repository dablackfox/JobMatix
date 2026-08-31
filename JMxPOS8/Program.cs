using Avalonia;
using System;
using System.Threading.Tasks;

namespace JMxPOS8;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // Must run before any Services.DatabaseService is constructed, since its
        // connection string is read from these environment variables.
        Services.DatabaseService.LoadEnvironment();

        // Handle unhandled exceptions (especially DBus shutdown issues on Linux)
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            if (e.Exception.InnerException is TaskCanceledException)
            {
                // Suppress TaskCanceledException during shutdown (known Avalonia/DBus issue on Linux)
                e.SetObserved();
            }
        };

        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (TaskCanceledException)
        {
            // Suppress TaskCanceledException during shutdown
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
