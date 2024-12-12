namespace HabitLoggerLibrary.Ui.Input;

public sealed class InputReaderFactory(IConsoleWrapper consoleWrapper) : IInputReaderFactory
{
    public IInputReader Create(InputChoice choice)
    {
        return choice switch
        {
            InputChoice.ConsoleInput => new ConsoleInputReader(consoleWrapper),
            _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
        };
    }
}