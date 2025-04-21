using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NexiusTestTodo.API.TodoItems.CreateTodoItem;
using NexiusTestTodo.API.TodoItems.ModifyTodoItem;
using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.API.UnitTests;

public class ModifyTodoItemHandlerTests
{
    [Test]
    public async Task Handle_ModifyItem_NoError()
    {
        var expectedGuidId = Guid.NewGuid();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.ModifyAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedGuidId));
        var loggerMock = new Mock<ILogger<ModifyTodoItemHandler>>();

        var command = new ModifyTodoItemCommand(Guid.NewGuid(), "", "");

        var handler = new ModifyTodoItemHandler(repositoryMock.Object, loggerMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result.Id, Is.EqualTo(expectedGuidId));
    }

    [Test]
    public async Task Handle_ModifyItemWithDefaultId_ValidationError()
    {
        var expectedGuidId = Guid.NewGuid();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.ModifyAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedGuidId));
        var loggerMock = new Mock<ILogger<ModifyTodoItemHandler>>();

        var command = new ModifyTodoItemCommand(Guid.Empty, "Title", "Test description");

        var handler = new ModifyTodoItemHandler(repositoryMock.Object, loggerMock.Object);

        Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
