using Moq;
using NexiusTestTodo.API.Services.SetTodoItemStatus;
using NexiusTestTodo.Data.Interfaces;

namespace NexiusTestTodo.Data.UnitTests;
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

        var handler = new SetTodoItemStatusHandler(repositoryMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result.Id, Is.EqualTo(expectedGuidId));
    }
}
