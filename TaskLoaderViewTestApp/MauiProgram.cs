
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Sharpnado.Tasks;

namespace TaskLoaderViewTestApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            TaskMonitorConfiguration.ConsiderCanceledAsFaulted = true;
            TaskMonitorConfiguration.LogStatistics = true;

            builder.Services.AddTransient<MasterPage, MasterPageViewModel>();
            builder.Services.AddTransient<BaseAsyncPage, BaseAsyncViewModel>();


            return builder.Build();
        }
    }
}