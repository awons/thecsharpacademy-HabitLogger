using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Menu;

namespace HabitLoggerApp.Application.Handlers;

public class HabitLogsMainMenuHandler(IKeyAwaiter keyAwaiter, IHabitLogsMenuChoiceReader choiceReader)
{
    public void Handle()
    {
        do
        {
            Console.Clear();

            Console.WriteLine("Choose what you want to do.");
            HabitLogsMenuRenderer.Render();
            var choice = choiceReader.GetChoice();
            // TODO Handle all menu choices
            keyAwaiter.Wait();
            break;
        } while (true);
    }
}