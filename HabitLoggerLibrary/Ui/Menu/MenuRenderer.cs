namespace HabitLoggerLibrary.Ui.Menu;

public static class MenuRenderer
{
    public static void Render()
    {
        Console.WriteLine($@"{Convert.ToChar(MenuChoice.ViewAllRecords)}: View all records
{Convert.ToChar(MenuChoice.InsertRecord)}: Insert record
{Convert.ToChar(MenuChoice.DeleteRecord)}: Delete record
{Convert.ToChar(MenuChoice.EditRecord)}: Edit record
{Convert.ToChar(MenuChoice.Quit)}: Quit");
    }
}