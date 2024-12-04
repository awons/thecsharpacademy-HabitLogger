using HabitLoggerLibrary.Repository;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Habits;

namespace HabitLoggerApp.Application.Handlers;

public sealed class ViewRecordsHandler(IRepository repository, IKeyAwaiter keyAwaiter)
{
    public void Handle()
    {
        Console.Clear();
        HabitsRenderer.RenderAll(repository.GetHabits());
        Console.WriteLine("Press any key to continue...");
        keyAwaiter.Wait();
    }
}