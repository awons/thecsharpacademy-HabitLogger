using HabitLoggerApp.Application.Handlers;
using HabitLoggerLibrary.Ui.Menu;

namespace HabitLoggerApp.Application;

public class Loop(
    IMenuChoiceReader menuChoiceReader,
    DeleteRecordHandler deleteRecordHandler,
    ViewRecordsHandler viewRecordsHandler)
{
    public void Run()
    {
        do
        {
            Console.Clear();
            MenuRenderer.Render();
            var menuChoice = menuChoiceReader.GetChoice();
            switch (menuChoice)
            {
                case MenuChoice.Quit:
                    Console.Clear();
                    Console.WriteLine("Thank you for using the Habit Logger!");
                    return;
                case MenuChoice.ViewAllRecords:
                    viewRecordsHandler.Handle();
                    break;
                case MenuChoice.InsertRecord:
                    //TODO Implement
                    break;
                case MenuChoice.DeleteRecord:
                    deleteRecordHandler.Handle();
                    break;
                case MenuChoice.EditRecord:
                    //TODO Implement
                    break;
            }
        } while (true);
    }
}