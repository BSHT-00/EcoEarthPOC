using EcoEarthPOC.Components.Services;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace EcoEarthPOC
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
                    fonts.AddFont("Minecraft.ttf", "Minecraft");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<OFFPackaging>();
            builder.Services.AddHttpClient();
            builder.UseMauiApp<App>().UseBarcodeReader(); // Corrected method call

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
