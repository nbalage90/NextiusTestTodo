﻿using Moq;
using NexiusTestTodo.API.TodoItems.CreateTodoItem;
using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace NexiusTestTodo.API.UnitTest;

public class CreateTodoItemHandlerTests
{
    [Test]
    public async Task Handle_CreateNewItem_WithNoError()
    {
        var expectedGuidId = Guid.NewGuid();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedGuidId));
        var loggerMock = new Mock<ILogger<CreateTodoItemCommandHandler>>();

        var command = new CreateTodoItemCommand("Test description", false);
        
        var handler = new CreateTodoItemCommandHandler(repositoryMock.Object, loggerMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result.Id, Is.EqualTo(expectedGuidId));
    }

    [Test]
    public async Task Handle_CreateNewItemWithNoDescription_ValidationError()
    {
        var expectedGuidId = Guid.NewGuid();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedGuidId));
        var loggerMock = new Mock<ILogger<CreateTodoItemCommandHandler>>();

        var command = new CreateTodoItemCommand("", false);

        var handler = new CreateTodoItemCommandHandler(repositoryMock.Object, loggerMock.Object);

        Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
