using FluentAssertions;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Menu;
using NSubstitute;

namespace HabitLoggerLibraryTests.Ui.Menu;

[TestFixture]
public class MenuChoiceReaderTests
{
    [Test]
    [TestCaseSource(nameof(_validCases))]
    public void WillReturnCorrectChoice((ConsoleKeyInfo input, MenuChoice expectedResult) valueTuple)
    {
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadKey().Returns(valueTuple.input);
        var reader = new MenuChoiceReader(consoleWrapper);

        reader.GetChoice().Should().Be(valueTuple.expectedResult);
    }

    [Test]
    public void WillKeepOnWaitingForValidChoice()
    {
        var consoleWrapper = Substitute.For<IConsoleWrapper>();
        consoleWrapper.ReadKey().Returns(new ConsoleKeyInfo('a', ConsoleKey.None, false, false, false),
            new ConsoleKeyInfo('b', ConsoleKey.None, false, false, false),
            new ConsoleKeyInfo('q', ConsoleKey.None, false, false, false));
        var reader = new MenuChoiceReader(consoleWrapper);

        reader.GetChoice().Should().Be(MenuChoice.Quit);
    }

    private static (ConsoleKeyInfo, MenuChoice)[] _validCases =
    [
        (new ConsoleKeyInfo('v', ConsoleKey.None, false, false, false), MenuChoice.ViewAllRecords),
        (new ConsoleKeyInfo('i', ConsoleKey.None, false, false, false), MenuChoice.InsertRecord),
        (new ConsoleKeyInfo('d', ConsoleKey.None, false, false, false), MenuChoice.DeleteRecord),
        (new ConsoleKeyInfo('e', ConsoleKey.None, false, false, false), MenuChoice.EditRecord),
        (new ConsoleKeyInfo('q', ConsoleKey.None, false, false, false), MenuChoice.Quit)
    ];
}