using HabitLoggerLibrary;
using HabitLoggerLibrary.Repository;

namespace HabitLoggerApp.Fixtures;

public class FixturesGenerator(IRepository repository)
{
    private readonly HabitDraft[] _drafts =
    [
        new("Habit 1", 3, new DateOnly(2024, 12, 02)),
        new("Habit 1", 2, new DateOnly(2024, 12, 01)),
        new("Habit 2", 4, new DateOnly(2024, 12, 02)),
        new("Habit 3", 2, new DateOnly(2024, 12, 01)),
        new("Habit 2", 1, new DateOnly(2024, 11, 30)),
        new("Habit 2", 2, new DateOnly(2024, 11, 29)),
        new("Habit 2", 1, new DateOnly(2024, 11, 28)),
        new("Habit 3", 3, new DateOnly(2024, 11, 27))
    ];

    public void Populate()
    {
        if (repository.GetHabitsCount() > 0) return;

        foreach (var habitDraft in _drafts) repository.AddHabit(habitDraft);
    }
}