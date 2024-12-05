using HabitLoggerLibrary.Repository;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Habits;

namespace HabitLoggerApp.Application.Handlers;

public sealed class ViewRecordsHandler(IHabitsRepository habitsRepository, IKeyAwaiter keyAwaiter)
{
    public void Handle()
    {
        Console.Clear();
        HabitsRenderer.RenderAll(habitsRepository.GetHabits());
        Console.WriteLine("Press any key to continue...");
        keyAwaiter.Wait();
    }
}