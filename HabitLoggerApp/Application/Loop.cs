using HabitLoggerLibrary.Repository;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Menu;

namespace HabitLoggerApp.Application;

public class Loop(IMenuChoiceReader menuChoiceReader, IRepository repository, IKeyAwaiter keyAwaiter)
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
                    RenderAllRecords();
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

    private void RenderAllRecords()
    {
        var habits = repository.GetHabits();
        foreach (var habit in habits) Console.WriteLine($"{habit.HabitName}; {habit.Quantity}; {habit.HabitDate}");
        Console.WriteLine("Press any key to continue...");
        keyAwaiter.Wait();
    }
}