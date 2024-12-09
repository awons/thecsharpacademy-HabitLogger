using HabitLoggerApp.Application;
using HabitLoggerApp.Application.Handlers;
using HabitLoggerApp.Fixtures;
using HabitLoggerLibrary.DbManager;
using HabitLoggerLibrary.Repository;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Habits;
using HabitLoggerLibrary.Ui.Input;
using HabitLoggerLibrary.Ui.Menu;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(builder =>
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>();
    }).ConfigureServices((context, services) =>
    {
        services.AddSingleton<App>();
        services.AddSingleton<DatabaseManagerFactory>();
        services.AddSingleton<RepositoryFactory>();
        services.AddSingleton<Loop>();
        services.AddSingleton<FixturesGenerator>();
        services.AddSingleton<IKeyAwaiter, ConsoleKeyAwaiter>();
        services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
        services.AddSingleton<IMainMenuChoiceReader, MainMenuChoiceReader>();
        services.AddSingleton<IDatabaseManager>(serviceProvider =>
            serviceProvider.GetService<DatabaseManagerFactory>()!.Create());
        services.AddSingleton<IHabitsRepository>(serviceProvider =>
            serviceProvider.GetService<RepositoryFactory>()!.CreateHabitsRepository());
        services.AddSingleton<IHabitChoiceReader, ConsoleHabitReader>();
        services.AddSingleton<DeleteHabitHandler>();
        services.AddSingleton<ViewHabitsHandler>();
        services.AddSingleton<InsertHabitHandler>();
        services.AddSingleton<IInputChoiceReader, ConsoleInputChoiceReader>();
    });

using var host = builder.Build();

var app = host.Services.GetService<App>()!;
app.Run();