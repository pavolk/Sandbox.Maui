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
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            TelerikLocalizationManager.Manager = new MyTelerikLocalizationManager();

            return builder.Build();
        }
    }
}