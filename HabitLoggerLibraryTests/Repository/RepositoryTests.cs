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
    public void IsCreatedEmpty()
    {
        var repository = CreateRepository();
        repository.GetHabits().Should().BeEmpty();
    }

    private LibraryRepository.IRepository CreateRepository()
    {
        return new LibraryRepository.Repository((SqliteConnection)_databaseManager.GetConnection());
    }
}