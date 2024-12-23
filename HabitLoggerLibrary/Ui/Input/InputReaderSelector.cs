using HabitLoggerLibrary.Ui.Input.StringInput;

namespace HabitLoggerLibrary.Ui.Input;

public class InputReaderSelector(
    IStringInputReaderFactory stringInputReaderFactory,
    IInputChoiceReader inputChoiceReader)
    : IInputReaderSelector
{
    public IStringInputReader GetInputReader()
    {
        Console.Clear();
        Console.WriteLine("How do you want to provide input?");
        Console.WriteLine($@"{Convert.ToChar(InputChoice.ConsoleInput)}: Console
{Convert.ToChar(InputChoice.SpeechInput)}: Speech");

        var inputChoice = inputChoiceReader.GetChoice();

        return stringInputReaderFactory.Create(inputChoice);
    }
}