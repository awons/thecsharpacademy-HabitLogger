namespace HabitLoggerLibrary.Repository;

public interface IRepository
{
    public const string TableName = "habit_logs";

    public List<Habit> GetHabits();

    public Habit AddHabit(HabitDraft draft);

    public Habit UpdateHabit(Habit habit);

    public Habit DeleteHabit(Habit habit);

    public Habit GetHabitById(int id);

    public bool HasHabitById(int id);
}