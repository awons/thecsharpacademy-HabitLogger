namespace HabitLoggerLibrary.Ui;

public class ConsoleWrapperFactory
{
    public IConsoleWrapper Create()
    {
        return new ConsoleWrapper();
    }
}