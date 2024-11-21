using System.Data;

namespace HabitLoggerLibrary.DbManager;

public interface IDatabaseManager
{
    public void SetUp();

    public IDbConnection GetConnection();
}