namespace HabitLoggerLibrary;

public sealed record Habit(long Id, string HabitName, int Quantity, DateOnly HabitDate)
{
}