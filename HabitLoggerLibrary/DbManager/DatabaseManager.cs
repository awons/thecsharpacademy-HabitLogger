using System.Data;
using HabitLoggerLibrary.Repository;
using Microsoft.Data.Sqlite;

namespace HabitLoggerLibrary.DbManager;

internal sealed class DatabaseManager(SqliteConnection connection) : IDatabaseManager
{
    public void SetUp()
    {
        var command = connection.CreateCommand();
        command.CommandText = $@"CREATE TABLE IF NOT EXISTS {IRepository.TableName} (
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            habit TEXT NOT NULL,
            quantity INTEGER NOT NULL,
            habit_date DATE NOT NULL,
            UNIQUE(habit, quantity)
        )";
        command.ExecuteNonQuery();
    }

    public IDbConnection GetConnection()
    {
        return connection;
    }
}