using HabitLoggerApp.Application.Handlers;
using HabitLoggerLibrary.Repository;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Habits;
using HabitLoggerLibrary.Ui.Menu;

namespace HabitLoggerApp.Application;

public class Loop(
    IMenuChoiceReader menuChoiceReader,
    DeleteRecordHandler deleteRecordHandler,
    IRepository repository,
    IKeyAwaiter keyAwaiter)
{
    public void Run()
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
                    Console.WriteLine("Thank you for using the Habit Logger!");
                    return;
                case MenuChoice.ViewAllRecords:
                    Console.Clear();
                    HabitsRenderer.RenderAll(repository.GetHabits());
                    Console.WriteLine("Press any key to continue...");
                    keyAwaiter.Wait();
                    break;
                case MenuChoice.InsertRecord:
                    //TODO Implement
                    break;
                case MenuChoice.DeleteRecord:
                    deleteRecordHandler.Handle();
                    break;
                case MenuChoice.EditRecord:
                    break;
            }
        } while (true);
    }
}