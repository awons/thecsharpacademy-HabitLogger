namespace HabitLoggerLibrary.Repository;

public interface IHabitLogsRepository
{
    public const string TableName = "habit_logs";

    public List<HabitLog> GetHabitLogs();

    public HabitLog AddHabitLog(HabitLogDraft draft);

    public void UpdateHabit(HabitLog habitLog);

    public void DeleteHabitLogById(long id);

    public HabitLog GetHabitLogById(long id);

    public bool HasHabitLogById(long id);

    public long GetHabitLogsCount();
}