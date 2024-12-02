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
            MenuRenderer.Render();
            var menuChoice = menuChoiceReader.GetChoice();
            switch (menuChoice)
            {
                case MenuChoice.Quit:
                    Console.Clear();
                    Console.WriteLine("Thank you for use the Habit Logger!");
                    return;
                case MenuChoice.ViewAllRecords:
                    // TODO Implement
                    break;
                case MenuChoice.InsertRecord:
                    //TODO Implement
                    break;
                case MenuChoice.DeleteRecord:
                    // TODO Implement
                    break;
                case MenuChoice.EditRecord:
                    break;
            }
        } while (true);
    }
}