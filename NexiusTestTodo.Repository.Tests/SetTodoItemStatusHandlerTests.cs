using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NexiusTestTodo.API.TodoItems.SetTodoItemStatus;
using NexiusTestTodo.Data.Interfaces;

namespace NexiusTestTodo.API.UnitTest;
public class SetTodoItemStatusHandlerTests
{
    [Test]
    public async Task Handle_SetStatusToTrue_NoError()
    {
        var expectedGuidId = Guid.NewGuid();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.SetStatusAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedGuidId));
        var command = new SetTodoItemStatusCommand(expectedGuidId, true);

        var loggerMock = new Mock<ILogger<SetTodoItemStatusHandler>>();

        var handler = new SetTodoItemStatusHandler(repositoryMock.Object, loggerMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result.Id, Is.EqualTo(expectedGuidId));
    }

    [Test]
    public async Task Handle_SetStatusToTrueWithoutGuid_ValidationError()
    {
        var expectedGuidId = Guid.NewGuid();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.SetStatusAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedGuidId));
        var loggerMock = new Mock<ILogger<SetTodoItemStatusHandler>>();

        var command = new SetTodoItemStatusCommand(Guid.Empty, true);

        var handler = new SetTodoItemStatusHandler(repositoryMock.Object, loggerMock.Object);

        Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
