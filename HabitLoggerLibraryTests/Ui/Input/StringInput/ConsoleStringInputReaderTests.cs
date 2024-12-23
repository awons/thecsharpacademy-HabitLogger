using FluentAssertions;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Input.StringInput;
using NSubstitute;

namespace HabitLoggerLibraryTests.Ui.Input.StringInput;

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