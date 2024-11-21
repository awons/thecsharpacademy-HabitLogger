using HabitLoggerLibrary.DbManager;

namespace HabitLoggerApp.Application;

public sealed class App(DatabaseManagerFactory databaseManagerFactory)
{
    public void Run()
    {
        databaseManagerFactory.Create();
    }
}