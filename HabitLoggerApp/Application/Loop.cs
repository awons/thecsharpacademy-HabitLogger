using HabitLoggerApp.Application.Handlers;
using HabitLoggerLibrary.Ui.Menu;

namespace HabitLoggerApp.Application;

public class Loop(
    IMainMenuChoiceReader mainMenuChoiceReader,
    DeleteRecordHandler deleteRecordHandler,
    ViewRecordsHandler viewRecordsHandler)
{
    public void Run()
    {
        do
        {
            Console.Clear();
            MainMenuRenderer.Render();
            var menuChoice = mainMenuChoiceReader.GetChoice();
            switch (menuChoice)
            {
                case MainMenuChoice.Quit:
                    Console.Clear();
                    Console.WriteLine("Thank you for using the Habit Logger!");
                    return;
                case MainMenuChoice.ViewAllHabits:
                    viewRecordsHandler.Handle();
                    break;
                case MainMenuChoice.InsertHabit:
                    //TODO Implement
                    break;
                case MainMenuChoice.DeleteHAbit:
                    deleteRecordHandler.Handle();
                    break;
                case MainMenuChoice.EditHabit:
                    //TODO Implement
                    break;
                case MainMenuChoice.HabitLogs:
                    //TODO Implement
                    break;
            }
        } while (true);
    }
}