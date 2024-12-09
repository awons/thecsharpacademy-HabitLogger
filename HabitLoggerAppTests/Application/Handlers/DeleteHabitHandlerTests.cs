using FluentAssertions;
using HabitLoggerApp.Application.Handlers;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Habits;
using NSubstitute;

namespace HabitLoggerAppTests.Application.Handlers;

[TestFixture]
public class DeleteHabitHandlerTests : IntegrationTests
{
    [SetUp]
    public void SetUp()
    {
        _keyAwaiter.When(x => x.Wait())
            .Do(_ => { });
    }

    private readonly IKeyAwaiter _keyAwaiter = Substitute.For<IKeyAwaiter>();

    [Test]
    [Timeout(1000)]
    public void WillKeepAskingForHabitIdUtilExistingIsFound()
    {
        PopulateDatabase();
        var choiceReader = Substitute.For<IHabitChoiceReader>();
        choiceReader.GetChoice().Returns(-1, 0, 25, 5, 2);

        var handler = new DeleteHabitHandler(choiceReader, CreateRepository(), _keyAwaiter);
        handler.Handle();

        ConsoleOutput.ToString().Split(Environment.NewLine)
            .Where(x => x == "Choose habit to delete. All logs for it will also be deleted.").Should()
            .HaveCount(5);
    }

    [Test]
    public void WillDeleteSelectedHabit()
    {
        PopulateDatabase();
        var choiceReader = Substitute.For<IHabitChoiceReader>();
        choiceReader.GetChoice().Returns(2);

        var repository = CreateRepository();
        repository.HasHabitById(2).Should().BeTrue();

        var handler = new DeleteHabitHandler(choiceReader, CreateRepository(), _keyAwaiter);
        handler.Handle();

        repository.HasHabitById(2).Should().BeFalse();
    }

    [Test]
    public void WillNotAllowToChooseHabitForDeletionIfNoHabitsExist()
    {
        var choiceReader = Substitute.For<IHabitChoiceReader>();
        choiceReader.DidNotReceive().GetChoice();

        var handler = new DeleteHabitHandler(choiceReader, CreateRepository(), _keyAwaiter);
        handler.Handle();
    }
}