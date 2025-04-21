using Microsoft.Extensions.Logging;
using PMU_APP.Pages;
using PMU_APP.Services;

namespace PMU_APP;

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

        builder.Services.AddSingleton<CarDatabaseService>();
        builder.Services.AddTransient<AddVehicle>();
        builder.Services.AddTransient<CarListPage>();
        builder.Services.AddTransient<CarDetailPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        

        return builder.Build();
	}
}
