namespace HabitLoggerLibrary.Repository;

public sealed class HabitNotFoundException(int id) : Exception($"Habit with id: {id} was not found.");