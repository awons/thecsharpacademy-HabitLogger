namespace HabitLoggerLibrary;

public sealed record Habit(int Id, string HabitName, int Quantity, DateOnly HabitDate)
{
}