using Microsoft.Data.Sqlite;

namespace HabitLoggerLibrary.Repository;

internal sealed class Repository(SqliteConnection connection) : IRepository
{
    public List<Habit> GetHabits()
    {
        throw new NotImplementedException();
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT * FROM {IRepository.TableName}";

        var reader = command.ExecuteReader();
        var record = reader.Read();

        return [];
    }

    public Habit AddHabit(HabitDraft draft)
    {
        throw new NotImplementedException();
    }

    public Habit UpdateHabit(Habit habit)
    {
        throw new NotImplementedException();
    }

    public Habit DeleteHabit(Habit habit)
    {
        throw new NotImplementedException();
    }

    public Habit GetHabitById(int id)
    {
        throw new NotImplementedException();
    }

    public bool HasHabitById(int id)
    {
        throw new NotImplementedException();
    }
}