using Microsoft.Extensions.Logging;
using Moq;
using NexiusTestTodo.API.TodoItems.SetTodoItemStatus;
using NexiusTestTodo.Data.Interfaces;

namespace NexiusTestTodo.API.UnitTests;
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
}
