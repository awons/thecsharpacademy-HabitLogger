namespace HabitLoggerLibrary.Ui;

internal sealed class ConsoleKeyAwaiter : IKeyAwaiter
{
    public void Wait()
    {
        Console.ReadKey();
    }
}