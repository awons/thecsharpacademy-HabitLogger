using HabitLoggerLibrary.Repository;

namespace HabitLoggerLibrary.Ui.HabitLogs;

public class ConsoleHabitLogChoiceReader(IHabitLogsRepository repository, IConsoleWrapper consoleWrapper)
    : IHabitLogChoiceReader
{
    public long GetChoice()
    {
        var positionLeft = Console.CursorLeft;
        var positionTop = Console.CursorTop;
        do
        {
            Console.SetCursorPosition(positionLeft, positionTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(positionLeft, positionTop);
            Console.Write("> ");
            var line = Console.ReadLine();

            if (!long.TryParse(line, out var choice)) continue;

            var currentPositionTop = Console.CursorTop;
            if (!repository.HasHabitLogById(choice))
            {
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(positionLeft, currentPositionTop);
                Console.WriteLine($"There is no habit log with id {choice}. Please try again...");
                continue;
            }

            Console.SetCursorPosition(positionLeft, currentPositionTop);
            Console.Write(new string(' ', Console.WindowWidth));

            return choice;
        } while (true);
    }
}