using Microsoft.Data.Sqlite;

namespace HabitLoggerLibrary.Repository;

internal sealed class Repository(SqliteConnection connection) : IRepository
{
    public List<Habit> GetHabits()
    {
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT id, habit, quantity, habit_date FROM {IRepository.TableName} ORDER BY id ASC";

        using var reader = command.ExecuteReader();
        var results = new List<Habit>();
        while (reader.Read())
            results.Add(new Habit(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2),
                DateOnly.FromDateTime(reader.GetDateTime(3))));

        return results;
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
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT id, habit, quantity, habit_date FROM {IRepository.TableName} WHERE id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) throw new HabitNotFoundException(id);

        reader.Read();

        return new Habit(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2),
            DateOnly.FromDateTime(reader.GetDateTime(3)));
    }

    public bool HasHabitById(int id)
    {
        throw new NotImplementedException();
    }
}