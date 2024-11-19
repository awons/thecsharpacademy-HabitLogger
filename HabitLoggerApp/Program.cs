using Microsoft.Data.Sqlite;

using var connection = new SqliteConnection("Data Source = HabitLogger.db");

connection.Open();

var command = connection.CreateCommand();
command.CommandText = @"CREATE TABLE IF NOT EXISTS habit_logs (
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    habit TEXT NOT NULL,
    quantity INTEGER NOT NULL,
    habit_date DATE NOT NULL,
    UNIQUE(habit, quantity)
)";
command.ExecuteNonQuery();