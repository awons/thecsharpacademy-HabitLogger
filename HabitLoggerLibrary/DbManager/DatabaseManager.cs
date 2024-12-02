using HabitLoggerLibrary.Repository;
using Microsoft.Data.Sqlite;

namespace HabitLoggerLibrary.DbManager;

internal sealed class DatabaseManager(SqliteConnection connection) : IDatabaseManager
{
    public void SetUp()
    {
        var command = connection.CreateCommand();
        command.CommandText = $@"CREATE TABLE IF NOT EXISTS {IRepository.TableName} (
            id INTEGER PRIMARY KEY,
            habit TEXT NOT NULL,
            quantity INTEGER NOT NULL,
            habit_date DATE NOT NULL,
            UNIQUE(habit, habit_date)
        )";
        command.ExecuteNonQuery();
    }

    public SqliteConnection GetConnection()
    {
        return connection;
    }
}