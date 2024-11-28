namespace HabitLoggerLibrary.Ui;

internal sealed class ConsoleWrapper : IConsoleWrapper
{
    public ConsoleKeyInfo ReadKey(bool intercept)
    {
        return Console.ReadKey(intercept);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}