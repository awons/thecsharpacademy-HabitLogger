namespace HabitLoggerLibrary.Ui.Input.StringInput;

public interface IStringInputReaderFactory
{
    public IStringInputReader Create(InputChoice choice);
}