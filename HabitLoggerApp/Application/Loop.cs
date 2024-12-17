using HabitLoggerApp.Application.Handlers;
using HabitLoggerLibrary.Ui.Menu;

namespace HabitLoggerApp.Application;

public class Loop(
    IMainMenuChoiceReader mainMenuChoiceReader,
    DeleteHabitHandler deleteHabitHandler,
    ViewHabitsHandler viewHabitsHandler,
    InsertHabitHandler insertHabitHandler,
    EditHabitHandler editHabitHandler)
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
                    viewHabitsHandler.Handle();
                    break;
                case MainMenuChoice.InsertHabit:
                    insertHabitHandler.Handle();
                    break;
                case MainMenuChoice.DeleteHAbit:
                    deleteHabitHandler.Handle();
                    break;
                case MainMenuChoice.EditHabit:
                    editHabitHandler.Handle();
                    break;
                case MainMenuChoice.HabitLogs:
                    //TODO Implement
                    break;
            }
        } while (true);
    }
}