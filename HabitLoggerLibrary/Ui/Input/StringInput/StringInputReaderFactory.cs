namespace HabitLoggerLibrary.Ui.Input.StringInput;

public sealed class StringInputReaderFactory(IConsoleWrapper consoleWrapper) : IStringInputReaderFactory
{
    public IStringInputReader Create(InputChoice choice)
    {
        return choice switch
        {
            InputChoice.ConsoleInput => new ConsoleStringInputReader(consoleWrapper),
            _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
        };
    }
}