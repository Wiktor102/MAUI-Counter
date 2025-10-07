using Counter.Services;
using Counter.ViewModels;
using Counter.Views;
using Microsoft.Extensions.Logging;

namespace Counter
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

builder.Services.AddSingleton<DataService>();
            builder.Services.AddSingleton<CountersViewModel>(serviceProvider =>
            {
                var dataService = serviceProvider.GetRequiredService<DataService>();
                return new CountersViewModel(dataService);
            });
            builder.Services.AddSingleton<CountersView>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
