using Counter.Services;
using Counter.ViewModels;
using Counter.Views;
using Microsoft.Extensions.Logging;

namespace Counter {
	public static class MauiProgram {
		public static MauiApp CreateMauiApp() {
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts => {
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			// Register services
			builder.Services.AddSingleton<DataService>();

			// Register ViewModels
			builder.Services.AddSingleton<CountersViewModel>();
			builder.Services.AddSingleton<AddCounterFormViewModel>(serviceProvider => {
				var countersViewModel = serviceProvider.GetRequiredService<CountersViewModel>();
				return new AddCounterFormViewModel(countersViewModel.AddCounter);
			});

			// Register Views
			builder.Services.AddSingleton<CountersView>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
