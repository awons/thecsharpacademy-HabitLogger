using HabitLoggerLibrary.Ui.Input;

namespace HabitLoggerApp.Application.Handlers;

public sealed class InsertHabitHandler(IInputChoiceReader inputChoiceReader)
{
    public void Handle()
    {
        Console.Clear();
        Console.WriteLine("How do you want to provide input?");
        Console.WriteLine($@"{Convert.ToChar(InputChoice.ConsoleInput)}: Console
{Convert.ToChar(InputChoice.SpeechInput)}: Speech");
        var inputChoice = inputChoiceReader.GetChoice();
        throw new NotImplementedException();
    }
}