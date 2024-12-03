using FluentAssertions;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Habits;
using NSubstitute;

namespace HabitLoggerLibraryTests.Ui.Habits;

[TestFixture]
public class ConsoleHabitReaderTests
{
    [Test]
    public void WillReturnCorrectValue()
    {
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadLine().Returns("a", "ab", "12.23", "22,11", "26");

        var reader = new ConsoleHabitReader(consoleWrapper);
        reader.GetChoice().Should().Be(26);
    }
}