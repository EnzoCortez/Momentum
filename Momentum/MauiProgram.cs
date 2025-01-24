using Momentum.ViewModels;
using Momentum.Services;
using Momentum.View;

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
            });

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Momentum.db3");
        builder.Services.AddSingleton<TaskDatabase>(s => new TaskDatabase(dbPath));
        builder.Services.AddSingleton<TaskViewModel>();
        builder.Services.AddSingleton<TaskPage>();

        return builder.Build();
    }
}
