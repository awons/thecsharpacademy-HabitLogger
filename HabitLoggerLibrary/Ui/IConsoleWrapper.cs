namespace HabitLoggerLibrary.Ui;

public interface IConsoleWrapper
{
    public ConsoleKeyInfo ReadKey(bool intercept);

    public string? ReadLine();
}