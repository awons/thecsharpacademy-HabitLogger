namespace HabitLoggerLibrary.Ui;

public class KeyAwaiterFactory
{
    public IKeyAwaiter Create()
    {
        return new ConsoleKeyAwaiter();
    }
}