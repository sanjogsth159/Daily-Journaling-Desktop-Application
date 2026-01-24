using DailyJournal.Service;
using Microsoft.Extensions.Logging;
using DailyJournal.Database;
using System.Threading.Tasks;

namespace DailyJournal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // Register services
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<EntryService>();

            // Register the singleton AppDatabase instance so it can be injected if needed
            builder.Services.AddSingleton(_ => AppDatabase.Instance);

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // Ensure DB tables exist at startup — start initialization in background to avoid blocking UI/launcher.
            _ = Task.Run(async () => await AppDatabase.Instance.InitializeAsync());

            return builder.Build();
        }
    }
}
