using HabitLoggerLibrary.DbManager;

namespace HabitLoggerLibrary.Repository;

public class RepositoryFactory(IDatabaseManager databaseManager)
{
    public IRepository Create()
    {
        return new Repository(databaseManager.GetConnection());
    }
}