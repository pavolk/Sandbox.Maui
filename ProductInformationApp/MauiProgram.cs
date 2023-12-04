using Microsoft.Extensions.Logging;

using ProductInformationApp.Services;
using CommunityToolkit.Maui.Markup;

namespace ProductInformationApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddPrinsService();

            builder.Services.AddTransient<MainPage>();
            
            builder.Services.AddTransient<ProductInformationViewModel>();
            builder.Services.AddTransient<SimpleProductInformationPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}