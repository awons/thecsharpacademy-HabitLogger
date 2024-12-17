using HabitLoggerLibrary;
using HabitLoggerLibrary.Repository;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Input;

namespace HabitLoggerApp.Application.Handlers;

public sealed class InsertHabitHandler(
    IInputReaderSelector inputReaderSelector,
    IHabitsRepository repository,
    IKeyAwaiter keyAwaiter)
{
    public void Handle()
    {
        var inputReader = inputReaderSelector.GetInputReader();
        Console.Clear();
        Console.WriteLine("Provide habit name");
        var habitName = inputReader.GetInput();

        Console.Clear();
        Console.WriteLine("Provide unit of measure");
        var unitOfMeasure = inputReader.GetInput();

        var habit = repository.AddHabit(new HabitDraft(habitName, unitOfMeasure));

        Console.Clear();
        Console.WriteLine($"Habit added: {habit.HabitName}; {habit.UnitOfMeasure};");
        Console.WriteLine("Press any key to continue...");
        keyAwaiter.Wait();
    }
}