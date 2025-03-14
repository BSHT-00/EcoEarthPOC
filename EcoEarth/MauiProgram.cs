using EcoEarthPOC.Components.Services;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using EcoEarthPOC.Components.Services.EcoEarthAPI_Services;
using CommunityToolkit.Maui.Core;

namespace EcoEarthPOC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("Minecraft.ttf", "Minecraft");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<OFFPackaging>();
            builder.Services.AddHttpClient();
            builder.UseMauiApp<App>().UseMauiCommunityToolkitCore();

            builder.Services.AddHttpClient<IsProductRecyclableService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:7111/api");
            });

            builder.UseMauiApp<App>().UseBarcodeReader(); 

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IAppService, AppService>();

            return builder.Build();
        }
    }
}
