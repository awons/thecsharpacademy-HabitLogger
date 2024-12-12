using FluentAssertions;
using HabitLoggerApp.Application.Handlers;
using HabitLoggerLibrary;
using HabitLoggerLibrary.Ui;
using HabitLoggerLibrary.Ui.Input;
using NSubstitute;

namespace HabitLoggerAppTests.Application.Handlers;

public class InsertHabitHandlerTests : IntegrationTests
{
    [Test]
    public void WillAddHabit()
    {
        var inputChoiceReader = Substitute.For<IInputChoiceReader>();
        inputChoiceReader.GetChoice().Returns(InputChoice.ConsoleInput);
        var inputReader = Substitute.For<IInputReader>();
        inputReader.GetInput().Returns("Some habit name", "kilometers");
        var inputReaderFactory = Substitute.For<IInputReaderFactory>();
        inputReaderFactory.Create(InputChoice.ConsoleInput).Returns(inputReader);
        var keyAwaiter = Substitute.For<IKeyAwaiter>();
        keyAwaiter.When(x => x.Wait()).Do(_ => { });
        var repository = CreateRepository();

        repository.HasHabitById(1).Should().BeFalse();
        var handler = new InsertHabitHandler(inputChoiceReader, inputReaderFactory, repository, keyAwaiter);
        handler.Handle();
        repository.HasHabitById(1).Should().BeTrue();
        repository.GetHabitById(1).Should().BeEquivalentTo(new Habit(1, "Some habit name", "kilometers"));
    }
}