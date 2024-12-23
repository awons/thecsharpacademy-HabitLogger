using HabitLoggerLibrary.Repository;

namespace HabitLoggerLibrary.Ui.Habits;

public class ConsoleHabitChoiceReader(IConsoleWrapper consoleWrapper, IHabitsRepository repository) : IHabitChoiceReader
{
    public long GetChoice()
    {
        var positionLeft = Console.CursorLeft;
        var positionTop = Console.CursorTop;
        do
        {
            Console.SetCursorPosition(positionLeft, positionTop);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(positionLeft, positionTop);
            Console.Write("> ");
            var line = consoleWrapper.ReadLine();

            if (!long.TryParse(line, out var choice)) continue;

            var currentPositionTop = Console.CursorTop;
            if (!repository.HasHabitById(choice))
            {
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(positionLeft, currentPositionTop);
                Console.WriteLine($"There is no habit with id {choice}. Please try again...");
                continue;
            }

            Console.SetCursorPosition(positionLeft, currentPositionTop);
            Console.Write(new string(' ', Console.WindowWidth));

            return choice;
        } while (true);
    }
}