using FluentAssertions;
using HabitLoggerApp.Application.Handlers;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Habits;
using NSubstitute;

namespace HabitLoggerAppTests.Application.Handlers;

[TestFixture]
public class DeleteRecordHandlerTests : IntegrationTests
{
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        PopulateDatabase();
        _keyAwaiter.When(x => x.Wait())
            .Do(_ => { });
    }

    private readonly IKeyAwaiter _keyAwaiter = Substitute.For<IKeyAwaiter>();

    [Test]
    [Timeout(1000)]
    public void WillKeepAskingForHabitIdUtilExistingIsFound()
    {
        var choiceReader = Substitute.For<IHabitChoiceReader>();
        choiceReader.GetChoice().Returns(-1, 0, 25, 5, 2);

        var handler = new DeleteRecordHandler(choiceReader, CreateRepository(), _keyAwaiter);
        handler.Handle();

        ConsoleOutput.ToString().Split(Environment.NewLine).Where(x => x == "Choose record to delete").Should()
            .HaveCount(5);
    }

    [Test]
    public void WillDeleteSelectedHabit()
    {
        var choiceReader = Substitute.For<IHabitChoiceReader>();
        choiceReader.GetChoice().Returns(2);

        var repository = CreateRepository();
        repository.HasHabitById(2).Should().BeTrue();

        var handler = new DeleteRecordHandler(choiceReader, CreateRepository(), _keyAwaiter);
        handler.Handle();

        repository.HasHabitById(2).Should().BeFalse();
    }
}