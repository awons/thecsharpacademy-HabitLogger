using HabitLoggerLibrary.DbManager;
using HabitLoggerLibrary.Ui.Menu;

namespace HabitLoggerApp.Application;

public sealed class App(DatabaseManagerFactory databaseManagerFactory, IMenuChoiceReader menuChoiceReader)
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
            var menuChoice = menuChoiceReader.GetChoice();
            switch (menuChoice)
            {
                case MenuChoice.Quit:
                    Console.Clear();
                    Console.WriteLine("Thank you for use the Habit Logger!");
                    return;
            }

            break;
        } while (true);
    }

    private void RenderMenu()
    {
        // TODO Implement
    }
}