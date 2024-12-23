namespace HabitLoggerLibrary.Ui.Input;

public interface IInputReaderFactory
{
    public IStringInputReader Create(InputChoice choice);
}