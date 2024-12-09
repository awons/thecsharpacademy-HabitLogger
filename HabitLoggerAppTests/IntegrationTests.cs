using System.Text;
using HabitLoggerLibrary.DbManager;
using LibraryRepository = HabitLoggerLibrary.Repository;

namespace HabitLoggerAppTests;

public abstract class IntegrationTests : ConsoleTest
{
    private IDatabaseManager DatabaseManager { get; set; }

    protected StringBuilder ConsoleOutput { get; private set; }

    [SetUp]
    public void SetUpDatabase()
    {
        DatabaseManager = new DatabaseManagerFactory().Create(true);
        DatabaseManager.GetConnection().Open();
        DatabaseManager.SetUp();

        ConsoleOutput = new StringBuilder();
        Console.SetOut(new StringWriter(ConsoleOutput));
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

    protected LibraryRepository.IHabitsRepository CreateRepository()
    {
        return new LibraryRepository.HabitsRepository(DatabaseManager.GetConnection());
    }
}