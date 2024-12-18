using HabitLoggerLibrary.Ui.Menu;

namespace HabitLoggerApp.Application.Handlers.HabitLogs;

public class HabitLogsMainMenuHandler(IHabitLogsMenuChoiceReader choiceReader)
{
    public void Handle()
    {
        do
        {
            Console.Clear();

            Console.WriteLine("Choose what you want to do.");
            HabitLogsMenuRenderer.Render();
            var choice = choiceReader.GetChoice();
            switch (choice)
            {
                case HabitLogsMenuChoice.GoBack:
                    return;
                case HabitLogsMenuChoice.DeleteLogEntry:
                    // TODO Implement
                    break;
                case HabitLogsMenuChoice.EditLogEntry:
                    // TODO Implement
                    break;
                case HabitLogsMenuChoice.InsertLogEntry:
                    // TODO Implement
                    break;
                case HabitLogsMenuChoice.ViewAllLogs:

                    break;
            }
        } while (true);
    }
}