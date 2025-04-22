using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NexiusTestTodo.API.TodoItems.GetAllTodoItems;
using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Data.Models;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.API.UnitTest;

public class GetAllTodoItemsHandlerTests
{
    private Mock<ITodoItemRepository> _repositoryMock = new();
    private Mock<ILogger<GetAllTodoItemsQueryHandler>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
        _repositoryMock
            .Setup(repo => repo.GetAllAsync(It.IsAny<GetAllItemsRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<IEnumerable<Todo>>([
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                new Todo { Id = Guid.NewGuid(), Description = "Description", Status = false },
                ]));
        _loggerMock = new Mock<ILogger<GetAllTodoItemsQueryHandler>>();
    }

    [Test]
    public async Task Handle_GetAllItemWithoutPagingAndFiltering_NoError()
    {
        var command = new GetAllTodoItemsQuery();

        var handler = new GetAllTodoItemsQueryHandler(_repositoryMock.Object, _loggerMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result.TodoItems, Is.Not.Null);
        Assert.That(result.TodoItems.ToList(), Has.Count.EqualTo(10));
    }

    [Test]
    public async Task Handle_GetAllItemWithPageSizeGreaterThan25_ValidationError()
    {
        var command = new GetAllTodoItemsQuery(PageSize: 26, PageNumber: 1);

        var handler = new GetAllTodoItemsQueryHandler(_repositoryMock.Object, _loggerMock.Object);

        Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task Handle_GetAllItemWithPageSizeLessThan0_ValidationError()
    {
        var command = new GetAllTodoItemsQuery(PageSize: -1, PageNumber: 1);

        var handler = new GetAllTodoItemsQueryHandler(_repositoryMock.Object, _loggerMock.Object);

        Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task Handle_GetAllItemWithPageNumberLessThan0_ValidationError()
    {
        var command = new GetAllTodoItemsQuery(PageSize: 10, PageNumber: -1);

        var handler = new GetAllTodoItemsQueryHandler(_repositoryMock.Object, _loggerMock.Object);

        Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
