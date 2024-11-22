﻿using HabitLoggerApp.Application;
using HabitLoggerLibrary.DbManager;
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
    });

using var host = builder.Build();

var app = host.Services.GetService<App>()!;
app.Run();