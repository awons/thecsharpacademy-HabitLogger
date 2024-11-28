using HabitLoggerLibrary.DbManager;

namespace HabitLoggerApp.Application;

public sealed class App(DatabaseManagerFactory databaseManagerFactory)
{
    public void Run()
    {
        var dbManager = databaseManagerFactory.Create();
        dbManager.GetConnection().Open();

        RunLoop();
    }

    private void RunLoop()
    {
        do
        {
            Console.Clear();
            RenderMenu();


            break;
        } while (true);
    }

    private void RenderMenu()
    {
        // TODO Implement
    }
}