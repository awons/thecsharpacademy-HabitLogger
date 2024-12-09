using HabitLoggerLibrary.Repository;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Habits;

namespace HabitLoggerApp.Application.Handlers;

public sealed class DeleteHabitHandler(
    IHabitChoiceReader habitChoiceReader,
    IHabitsRepository habitsRepository,
    IKeyAwaiter keyAwaiter)
{
    public void Handle()
    {
        Console.Clear();
        do
        {
            var habits = habitsRepository.GetHabits();
            if (habits.Count == 0)
            {
                Console.WriteLine("No habits found. Press any key to continue....");
                keyAwaiter.Wait();
                return;
            }

            HabitsRenderer.RenderAll(habits);
            Console.WriteLine("Choose habit to delete. All logs for it will also be deleted.");
            var recordId = habitChoiceReader.GetChoice();
            if (!habitsRepository.HasHabitById(recordId))
            {
                Console.Clear();
                Console.WriteLine($"There is no habit with id {recordId}. Please try again.");
                continue;
            }

            habitsRepository.DeleteHabitById(recordId);
            Console.WriteLine("Habit deleted. Press any key to continue...");
            keyAwaiter.Wait();
            break;
        } while (true);
    }
}