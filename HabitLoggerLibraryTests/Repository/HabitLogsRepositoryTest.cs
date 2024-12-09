using FluentAssertions;
using HabitLoggerLibrary;
using LibraryRepository = HabitLoggerLibrary.Repository;

namespace HabitLoggerLibraryTests.Repository;

[TestFixture]
public class HabitLogsRepositoryTest : IntegrationTests
{
    [Test]
    public void CollectionIsEmptyWhenThereAreNoResults()
    {
        var repository = CreateRepository();
        repository.GetHabitLogs().Should().BeEmpty();
    }

    [Test]
    public void CollectionContainsAllResultsInDb()
    {
        PopulateDatabase();
        var repository = CreateRepository();
        repository.GetHabitLogs().Should().HaveCount(9);
    }

    [Test]
    public void WillReturnHabitLogById()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        var habit = repository.GetHabitLogById(4);

        habit.Id.Should().Be(4);
        habit.HabitId.Should().Be(2);
        habit.Quantity.Should().Be(6);
        habit.HabitDate.Should().Be(new DateOnly(2020, 4, 1));
    }

    [Test]
    public void WillThrowExceptionIfHabitDoesNotExist()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        Action action = () => repository.GetHabitLogById(0);
        action.Should().Throw<LibraryRepository.HabitLogNotFoundException>();
    }

    [Test]
    public void WillReturnTrueIfHabitExists()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        repository.HasHabitLogById(1).Should().BeTrue();
    }

    [Test]
    public void WillReturnFalseIfHabitDoesNotExist()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        repository.HasHabitLogById(0).Should().BeFalse();
    }

    [Test]
    public void WIllDeleteExistingHabit()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        repository.HasHabitLogById(1).Should().BeTrue();
        repository.DeleteHabitLogById(1);
        repository.HasHabitLogById(1).Should().BeFalse();
    }

    [Test]
    public void WillThrowExceptionIfTryingToDeleteNotExistingHabitLog()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        var action = () => repository.DeleteHabitLogById(0);
        action.Should().Throw<LibraryRepository.HabitLogNotFoundException>();
    }

    [Test]
    public void ExistingHabitWillBeUpdated()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        var updatedHabit = repository.GetHabitLogById(1) with
        {
            Quantity = 55,
            HabitDate = new DateOnly(2024, 2, 12)
        };
        repository.UpdateHabitLog(updatedHabit);
        repository.GetHabitLogById(1).Should().BeEquivalentTo(updatedHabit);
    }

    [Test]
    public void WillThrowExceptionIfUpdatingNonExistingHabit()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        var updatedHabit = repository.GetHabitLogById(1) with { Id = 100 };

        var action = () => repository.UpdateHabitLog(updatedHabit);
        action.Should().Throw<LibraryRepository.HabitLogNotFoundException>();
    }

    [Test]
    public void WillAddNewHabit()
    {
        PopulateDatabase();
        var repository = CreateRepository();

        repository.HasHabitLogById(10).Should().BeFalse();
        var habit = repository.AddHabitLog(new HabitLogDraft(1, 21, new DateOnly(2024, 2, 22)));

        habit.Id.Should().Be(10);
        habit.HabitId.Should().Be(1);
        habit.Quantity.Should().Be(21);
        repository.HasHabitLogById(10).Should().BeTrue();
    }

    [Test]
    public void WillGetCorrectHabitsCount()
    {
        var repository = CreateRepository();

        repository.GetHabitLogs().Count().Should().Be(0);
        PopulateDatabase();
        repository.GetHabitLogs().Count().Should().Be(9);
    }

    private LibraryRepository.IHabitLogsRepository CreateRepository()
    {
        return new LibraryRepository.HabitLogsRepository(DatabaseManager.GetConnection());
    }
}