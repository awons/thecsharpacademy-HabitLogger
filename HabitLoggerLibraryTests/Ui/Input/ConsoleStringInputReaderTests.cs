using FluentAssertions;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Input;
using NSubstitute;

namespace HabitLoggerLibraryTests.Ui.Input;

public class ConsoleStringInputReaderTests : ConsoleTest
{
    [Test]
    public void WillReturnProvidedInput()
    {
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadLine().Returns("Provided text");

        var reader = new ConsoleStringInputReader(consoleWrapper);
        reader.GetInput().Should().Be("Provided text");
    }

    [Test]
    public void WillKeepAskingForInputUntilProvidedInputAfterTrimIsNotEmpty()
    {
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadLine().Returns("", " ", "    ", "Provided text");

        var reader = new ConsoleStringInputReader(consoleWrapper);
        reader.GetInput().Should().Be("Provided text");
    }
}