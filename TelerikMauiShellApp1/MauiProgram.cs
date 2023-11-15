using CommunityToolkit.Maui.Markup;
using System.Diagnostics;
using Telerik.Maui;
using Telerik.Maui.Controls.Compatibility;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TelerikMauiShellApp1
{
    public static class MauiProgram
    {
        class MyTelerikLocalizationManager : TelerikLocalizationManager
        {
            public override string GetString(string key)
            {
                Debug.WriteLine($"key={key}");
                return key;
            }
        }


        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseTelerik()
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMarkup()
                .UseSentry(options => {
                    // The DSN is the only required setting.
                    options.Dsn = "https://2d3133ae1c91390a36849a192e1dfd99@o4506226446761984.ingest.sentry.io/4506226450694144";

                    // Use debug mode if you want to see what the SDK is doing.
                    // Debug messages are written to stdout with Console.Writeline,
                    // and are viewable in your IDE's debug console or with 'adb logcat', etc.
                    // This option is not recommended when deploying your application.
                    options.Debug = true;

                    // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
                    // We recommend adjusting this value in production.
                    options.TracesSampleRate = 1.0;

                    // Other Sentry options can be set here.
                })
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            TelerikLocalizationManager.Manager = new MyTelerikLocalizationManager();

            return builder.Build();
        }
    }
}