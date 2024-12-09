using HabitLoggerLibrary.Ui;

namespace HabitLoggerApp.Application.Handlers;

public sealed class InsertHabitHandler(IConsoleWrapper consoleWrapper)
{
    public void Handle()
    {
        consoleWrapper.Clear();
    }
}