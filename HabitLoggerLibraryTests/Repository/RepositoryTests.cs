using FluentAssertions;
using HabitLoggerLibrary;
using LibraryRepository = HabitLoggerLibrary.Repository;

namespace HabitLoggerLibraryTests.Repository;

[TestFixture]
public class RepositoryTests : IntegrationTests
{
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

    [Test]
    public void WillGetCorrectHabitsCount()
    {
        var repository = CreateRepository();

        repository.GetHabits().Count().Should().Be(0);
        PopulateDatabase();
        repository.GetHabits().Count().Should().Be(3);
    }

    private LibraryRepository.IRepository CreateRepository()
    {
        return new LibraryRepository.Repository(DatabaseManager.GetConnection());
    }
}