using HabitsTracker.Persistence;
using Microsoft.Extensions.Logging;

namespace HabitsTracker;

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

        ConfigurePersistenceServices(builder.Services);

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
    }

    private static void ConfigurePersistenceServices(IServiceCollection services)
    {
		var databasePath = Path.Combine(FileSystem.AppDataDirectory, "TodoSQLite.db3");
        services.AddSingleton<HabitDatabase>(serviceProvider => new HabitDatabase(databasePath));
    }
}
