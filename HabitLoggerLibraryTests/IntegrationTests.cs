using HabitLoggerLibrary.DbManager;
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
        command.CommandText =
            $"INSERT INTO {LibraryRepository.IHabitsRepository.TableName} (habit, unit_of_measure) VALUES (@Name, @UnitOfMeasure)";

        command.Parameters.AddWithValue("@Name", "Running");
        command.Parameters.AddWithValue("@UnitOfMeasure", "kilometers");
        command.ExecuteNonQuery();

        command.Parameters.Clear();
        command.Parameters.AddWithValue("@Name", "Swimming");
        command.Parameters.AddWithValue("@UnitOfMeasure", "meters");
        command.ExecuteNonQuery();

        command.Parameters.Clear();
        command.Parameters.AddWithValue("@Name", "Drinking water");
        command.Parameters.AddWithValue("@UnitOfMeasure", "glasses");
        command.ExecuteNonQuery();
    }
}