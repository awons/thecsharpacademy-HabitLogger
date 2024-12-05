using HabitLoggerLibrary.Sqlite;
using Microsoft.Data.Sqlite;

namespace HabitLoggerLibrary.Repository;

public sealed class HabitLogsRepository(SqliteConnection connection) : IHabitLogsRepository
{
    public List<HabitLog> GetHabitLogs()
    {
        var command = connection.CreateCommand();
        command.CommandText =
            $"SELECT id, habit_id, quantity, habit_date FROM {IHabitLogsRepository.TableName} ORDER BY id";

        using var reader = command.ExecuteReader();
        var results = new List<HabitLog>();
        while (reader.Read())
            results.Add(new HabitLog(reader.GetInt64(0), reader.GetInt64(1), reader.GetInt32(2),
                DateOnly.FromDateTime(reader.GetDateTime(3))));

        return results;
    }

    public HabitLog AddHabitLog(HabitLogDraft draft)
    {
        var command = connection.CreateCommand();
        command.CommandText =
            $"INSERT INTO {IHabitLogsRepository.TableName} (habit_id, quantity, habit_date) VALUES (@HabitId, @Quantity, @Date);";
        command.Parameters.AddWithValue("@HabitId", draft.HabitId);
        command.Parameters.AddWithValue("@Quantity", draft.Quantity);
        command.Parameters.AddWithValue("@Date", draft.HabitDate);
        command.ExecuteNonQuery();

        return GetHabitLogById(connection.GetLastInsertRowId());
    }

    public void UpdateHabit(HabitLog habitLog)
    {
        var command = connection.CreateCommand();
        command.CommandText =
            $"UPDATE {IHabitLogsRepository.TableName} SET habit_id = @HabitId, quantity = @UnitOfMeasure, habit_date = @Date WHERE id = @Id";

        command.Parameters.AddWithValue("@HabitId", habitLog.HabitId);
        command.Parameters.AddWithValue("@UnitOfMeasure", habitLog.Quantity);
        command.Parameters.AddWithValue("@Date", habitLog.HabitDate);
        command.Parameters.AddWithValue("@Id", habitLog.Id);
        var updatedCount = command.ExecuteNonQuery();
        if (updatedCount == 0) throw new HabitLogNotFoundException(habitLog.Id);
    }

    public void DeleteHabitLogById(long id)
    {
        if (!HasHabitLogById(id)) throw new HabitLogNotFoundException(id);

        var command = connection.CreateCommand();
        command.CommandText = $"DELETE FROM {IHabitLogsRepository.TableName} WHERE id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }

    public HabitLog GetHabitLogById(long id)
    {
        var command = connection.CreateCommand();
        command.CommandText =
            $"SELECT id, habit_id, quantity, habit_date FROM {IHabitLogsRepository.TableName} WHERE id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) throw new HabitLogNotFoundException(id);

        reader.Read();

        return new HabitLog(reader.GetInt64(0), reader.GetInt64(1), reader.GetInt32(2),
            DateOnly.FromDateTime(reader.GetDateTime(3)));
    }

    public bool HasHabitLogById(long id)
    {
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT id FROM {IHabitLogsRepository.TableName} WHERE id = @Id";
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();

        return reader.HasRows;
    }

    public long GetHabitLogsCount()
    {
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT COUNT(id) FROM {IHabitLogsRepository.TableName}";

        using var reader = command.ExecuteReader();
        reader.Read();

        return reader.GetInt64(0);
    }
}