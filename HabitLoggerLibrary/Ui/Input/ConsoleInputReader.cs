namespace HabitLoggerLibrary.Ui.Input;

public sealed class ConsoleInputReader(IConsoleWrapper consoleWrapper) : IInputReader
{
    public string GetInput()
    {
        string? input;
        var positionLeft = Console.CursorLeft;
        var positionRight = Console.CursorTop;
        do
        {
            Console.SetCursorPosition(positionLeft, positionRight);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(positionLeft, positionRight);
            Console.Write("> ");
            input = consoleWrapper.ReadLine();
        } while (input is null || input.Trim() == string.Empty);

        return input.Trim();
    }
}