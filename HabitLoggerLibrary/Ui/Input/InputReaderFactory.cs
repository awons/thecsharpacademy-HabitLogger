namespace HabitLoggerLibrary.Ui.Input;

public sealed class InputReaderFactory(IConsoleWrapper consoleWrapper) : IInputReaderFactory
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