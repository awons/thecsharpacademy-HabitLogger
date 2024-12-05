using HabitLoggerLibrary.Ui;

namespace HabitLoggerApp.Application.Handlers;

public sealed class InsertRecordHandler(IConsoleWrapper consoleWrapper)
{
    public void Handle()
    {
        consoleWrapper.Clear();
    }
}