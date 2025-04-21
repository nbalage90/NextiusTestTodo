using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NexiusTestTodo.API.TodoItems.DeleteTodoItem;
using NexiusTestTodo.Data.Interfaces;

namespace NexiusTestTodo.API.UnitTests;

public class DeleteTodoItemHandlerTests
{
    [Test]
    public async Task Handle_DeletItem_NoError()
    {
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));
        var loggerMock = new Mock<ILogger<DeleteTodoItemHandler>>();

        var command = new DeleteTodoItemCommand(Guid.NewGuid());

        var handler = new DeleteTodoItemHandler(repositoryMock.Object, loggerMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public async Task Handle_DeleteItemWithDefaultId_ValidationError()
    {
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));
        var loggerMock = new Mock<ILogger<DeleteTodoItemHandler>>();

        var command = new DeleteTodoItemCommand(Guid.Empty);

        var handler = new DeleteTodoItemHandler(repositoryMock.Object, loggerMock.Object);

        Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
