namespace HabitLoggerLibrary.Ui.Habits;

public class ConsoleHabitReader(IConsoleWrapper consoleWrapper) : IHabitChoiceReader
{
    public long GetChoice()
    {
        var positionLeft = Console.CursorLeft;
        var positionTop = Console.CursorTop;
        string? line;
        long choice;
        do
        {
            Console.SetCursorPosition(positionLeft, positionTop);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(positionLeft, positionTop);
            Console.Write("> ");
            line = consoleWrapper.ReadLine();
        } while (!long.TryParse(line, out choice));

        return choice;
    }
}