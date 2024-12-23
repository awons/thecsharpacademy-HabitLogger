using HabitLoggerLibrary.Ui.Input.StringInput;

namespace HabitLoggerLibrary.Ui.Input;

public interface IInputReaderSelector
{
    public IStringInputReader GetInputReader();
}