using FluentAssertions;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Menu;
using NSubstitute;

namespace HabitLoggerLibraryTests.Ui;

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

    private static (ConsoleKeyInfo, MenuChoice)[] _validCases =
    [
        (new ConsoleKeyInfo('v', ConsoleKey.None, false, false, false), MenuChoice.ViewAllRecords),
        (new ConsoleKeyInfo('i', ConsoleKey.None, false, false, false), MenuChoice.InsertRecord),
        (new ConsoleKeyInfo('d', ConsoleKey.None, false, false, false), MenuChoice.DeleteRecord),
        (new ConsoleKeyInfo('e', ConsoleKey.None, false, false, false), MenuChoice.EditRecord),
        (new ConsoleKeyInfo('q', ConsoleKey.None, false, false, false), MenuChoice.Quit)
    ];
}