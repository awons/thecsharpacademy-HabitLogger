using HabitLoggerLibrary.Repository;
using HabitLoggerLibrary.Ui;

namespace HabitLoggerApp.Application.Handlers.HabitLogs;

public sealed class ViewHabitLogsHandler(IHabitLogsRepository repository, IKeyAwaiter keyAwaiter)
{
    public void Handle()
    {
        Console.Clear();
        var logs = repository.GetHabitLogs();
        foreach (var habitLog in logs)
            Console.WriteLine(
                $"{habitLog.Id}; {habitLog.HabitName}; {habitLog.HabitUnitOfMeasure}: {habitLog.Quantity}; {habitLog.HabitDate}");
        Console.WriteLine("Press any key to continue...");
        keyAwaiter.Wait();
    }
}