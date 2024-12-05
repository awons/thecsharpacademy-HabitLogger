namespace HabitLoggerLibrary.Ui.Input;

public sealed class ConsoleInputChoiceReader(IConsoleWrapper consoleWrapper, IKeyAwaiter keyAwaiter)
    : IInputChoiceReader
{
    public InputChoice GetChoice()
    {
        throw new NotImplementedException();
        consoleWrapper.Clear();
        Console.WriteLine("Choose how you want to add new habit:");
        Console.WriteLine($"{Convert.ToChar(InputChoice.ConsoleInput): Console}");
        Console.WriteLine($"{Convert.ToChar(InputChoice.SpeechInput): Console}");
    }
}