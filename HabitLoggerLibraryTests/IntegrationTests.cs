using HabitLoggerLibrary.DbManager;
using Microsoft.Data.Sqlite;
using LibraryRepository = HabitLoggerLibrary.Repository;

namespace HabitLoggerLibraryTests;

public abstract class IntegrationTests
{
    protected IDatabaseManager DatabaseManager { get; private set; }

    [SetUp]
    public void SetUp()
    {
        DatabaseManager = new DatabaseManagerFactory().Create(true);
        DatabaseManager.GetConnection().Open();
        DatabaseManager.SetUp();
    }

    protected void PopulateDatabase()
    {
        var command = DatabaseManager.GetConnection().CreateCommand();
        command.CommandText = $@"INSERT INTO {LibraryRepository.IRepository.TableName} 
            (habit, quantity, habit_date) 
            VALUES (@Name, @Quantity, @Date)";
        command.Parameters.Add(new SqliteParameter("@Name", SqliteType.Text) { Value = "test_habit_1" });
        command.Parameters.Add(new SqliteParameter("@Quantity", SqliteType.Integer) { Value = 5 });
        command.Parameters.Add(new SqliteParameter("@Date", SqliteType.Text) { Value = new DateTime(2024, 11, 25) });
        command.ExecuteNonQuery();

        command.Parameters.Clear();
        command.Parameters.Add(new SqliteParameter("@Name", SqliteType.Text) { Value = "test_habit_2" });
        command.Parameters.Add(new SqliteParameter("@Quantity", SqliteType.Integer) { Value = 4 });
        command.Parameters.Add(new SqliteParameter("@Date", SqliteType.Text) { Value = new DateTime(2024, 11, 26) });
        command.ExecuteNonQuery();

        command.Parameters.Clear();
        command.Parameters.Add(new SqliteParameter("@Name", SqliteType.Text) { Value = "test_habit_3" });
        command.Parameters.Add(new SqliteParameter("@Quantity", SqliteType.Integer) { Value = 6 });
        command.Parameters.Add(new SqliteParameter("@Date", SqliteType.Text) { Value = new DateTime(2024, 11, 27) });
        command.ExecuteNonQuery();
    }
}