using System.Data;
using HabitLoggerLibrary.DbManager;
using HabitLoggerLibrary.Repository;

namespace HabitLoggerLibraryTests;

public class TestDatabaseManager(IDatabaseManager previous) : IDatabaseManager
{
    public void SetUp()
    {
        GetConnection().Open();
        previous.SetUp();
    }

    public IDbConnection GetConnection()
    {
        return previous.GetConnection();
    }

    public void TearDown()
    {
        var command = GetConnection().CreateCommand();
        command.CommandText = $"DROP TABLE IF EXISTS {IRepository.TableName}";
    }
}