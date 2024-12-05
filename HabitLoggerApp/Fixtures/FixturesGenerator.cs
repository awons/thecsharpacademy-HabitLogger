using HabitLoggerLibrary;
using HabitLoggerLibrary.Repository;

namespace HabitLoggerApp.Fixtures;

public class FixturesGenerator(IHabitsRepository habitsRepository)
{
    private readonly HabitDraft[] _drafts =
    [
        new("Running", "kilometers"),
        new("Climbing", "meters"),
        new("Push-ups", "repetitions"),
        new("Drinking water", "glasses")
    ];

    public void Populate()
    {
        if (habitsRepository.GetHabitsCount() > 0) return;

        foreach (var habitDraft in _drafts)
        {
            var habit = habitsRepository.AddHabit(habitDraft);
            for (var i = 0; i < 100; i++)
            {
                //TODO
            }
        }
    }
}