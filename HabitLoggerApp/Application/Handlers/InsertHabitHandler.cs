using HabitLoggerLibrary.Ui.Input;

namespace HabitLoggerApp.Application.Handlers;

public sealed class InsertHabitHandler(IInputChoiceReader inputChoiceReader)
{
    public void Handle()
    {
        Console.Clear();
        var inputChoice = inputChoiceReader.GetChoice();
    }
}