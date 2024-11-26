using FluentAssertions;
using HabitLoggerLibrary;
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

    [Test]
    public void WillReturnTrueIfHabitExists()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        repository.HasHabitById(1).Should().BeTrue();
    }

    [Test]
    public void WillReturnFalseIfHabitDoesNotExist()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        repository.HasHabitById(0).Should().BeFalse();
    }

    [Test]
    public void WIllDeleteExistingHabit()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        repository.HasHabitById(1).Should().BeTrue();
        repository.DeleteHabitById(1);
        repository.HasHabitById(1).Should().BeFalse();
    }

    [Test]
    public void WillThrowExceptionIfTryingToDeleteNotExistingHabit()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        var action = () => repository.DeleteHabitById(0);
        action.Should().Throw<LibraryRepository.HabitNotFoundException>();
    }

    [Test]
    public void ExistingHabitWillBeUpdated()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        var updatedHabit = repository.GetHabitById(1) with
        {
            HabitName = "updated_habit_name",
            Quantity = 10,
            HabitDate = new DateOnly(2024, 12, 31)
        };
        repository.UpdateHabit(updatedHabit);
        repository.GetHabitById(1).Should().BeEquivalentTo(updatedHabit);
    }

    [Test]
    public void WillThrowExceptionIfUpdatingNonExistingHabit()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        var updatedHabit = repository.GetHabitById(1) with { Id = 10 };

        var action = () => repository.UpdateHabit(updatedHabit);
        action.Should().Throw<LibraryRepository.HabitNotFoundException>();
    }

    [Test]
    public void WillAddNewHabit()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        repository.HasHabitById(4).Should().BeFalse();
        var habit = repository.AddHabit(new HabitDraft("added_habit_name", 8,
            new DateOnly(2024, 11, 30)));

        habit.Id.Should().Be(4);
        habit.HabitName.Should().Be("added_habit_name");
        habit.Quantity.Should().Be(8);
        habit.HabitDate.Should().Be(new DateOnly(2024, 11, 30));
        repository.HasHabitById(4).Should().BeTrue();
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