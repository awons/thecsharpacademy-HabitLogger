using FluentAssertions;
using HabitLoggerLibrary.DbManager;
using Microsoft.Data.Sqlite;
using LibraryRepository = HabitLoggerLibrary.Repository;

namespace HabitLoggerLibraryTests.Repository;

[TestFixture]
public class RepositoryTests
{
    [SetUp]
    public void SetUp()
    {
        _databaseManager = new DatabaseManagerFactory().Create(true);
        _databaseManager.GetConnection().Open();
        _databaseManager.SetUp();
    }

    private IDatabaseManager _databaseManager;

    [Test]
    public void CollectionIsEmptyWhenThereAreNoResults()
    {
        var repository = CreateRepository();
        repository.GetHabits().Should().BeEmpty();
    }

    [Test]
    public void CollectionContainsAllResultsInDb()
    {
        PopulateDatabase();
        var repository = CreateRepository();
        repository.GetHabits().Should().HaveCount(3);
    }

    [Test]
    public void WillReturnHabitById()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        var habit = repository.GetHabitById(2);

        habit.Id.Should().Be(2);
        habit.HabitName.Should().Be("test_habit_2");
        habit.Quantity.Should().Be(4);
        habit.HabitDate.Should().Be(new DateOnly(2024, 11, 26));
    }

    [Test]
    public void WillThrowExceptionIfHabitDoesNotExist()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        Action action = () => repository.GetHabitById(0);
        action.Should().Throw<LibraryRepository.HabitNotFoundException>();
    }

    private LibraryRepository.IRepository CreateRepository()
    {
        return new LibraryRepository.Repository((SqliteConnection)_databaseManager.GetConnection());
    }

    private void PopulateDatabase()
    {
        var command = _databaseManager.GetConnection().CreateCommand();
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