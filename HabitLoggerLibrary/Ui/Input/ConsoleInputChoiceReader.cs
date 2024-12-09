namespace HabitLoggerLibrary.Ui.Input;

public sealed class ConsoleInputChoiceReader(IConsoleWrapper consoleWrapper)
    : IInputChoiceReader
{
    public InputChoice GetChoice()
    {
        var positionLeft = Console.CursorLeft;
        var positionTop = Console.CursorTop;
        Console.WriteLine("How do you want to provide input?");
        char choice;
        do
        {
            Console.SetCursorPosition(positionLeft, positionTop);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.Write("> ");
            choice = consoleWrapper.ReadKey().KeyChar;
        } while (!Enum.IsDefined(typeof(InputChoice), (int)choice));

        return (InputChoice)choice;
    }
}