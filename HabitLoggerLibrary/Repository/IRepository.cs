namespace HabitLoggerLibrary.Repository;

public interface IRepository
{
    public const string TableName = "habit_logs";

    public List<Habit> GetHabits();

    public Habit AddHabit(HabitDraft draft);

    public void UpdateHabit(Habit habit);

    public void DeleteHabitById(long id);

    public Habit GetHabitById(long id);

    public bool HasHabitById(long id);
}