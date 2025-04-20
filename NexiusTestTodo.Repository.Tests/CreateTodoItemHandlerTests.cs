using Moq;
using NexiusTestTodo.API.TodoItems.CreateTodoItem;
using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;
using Microsoft.Extensions.Logging;

namespace NexiusTestTodo.API.UnitTests;

public class CreateTodoItemHandlerTests
{
    [Test]
    public async Task Handle_SaveNewItem_WithNoError()
    {
        var expectedGuidId = Guid.NewGuid();
        var repositoryMock = new Mock<ITodoItemRepository>();
        repositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedGuidId));
        var loggerMock = new Mock<ILogger<CreateTodoItemHandler>>();

        var command = new CreateTodoItemCommand("Test title", "Test description", false);
        
        var handler = new CreateTodoItemHandler(repositoryMock.Object, loggerMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(expectedGuidId, Is.EqualTo(result.Id));
    }

    //[Test]
    //public async Task GetAllAsync_ReturnsAll_WithPaging()
    //{
    //    var repository = RepositoryFactory.CreateMock();
    //    var pageSize = 25;
    //    var pageNumber = 1;

    //    var todos = await repository.GetAllAsync(pageSize, pageNumber);

    //    Assert.That(todos, Is.Not.Null);
    //    Assert.That(todos.ToList(), Has.Count.EqualTo(25));
    //}

    //[Test]
    //public async Task GetAllAsync_ReturnsAll_WithPagingMax25Items()
    //{
    //    var repository = RepositoryFactory.CreateMock();
    //    var pageSize = 30;
    //    var pageNumber = 1;

    //    var todos = await repository.GetAllAsync(pageSize, pageNumber);

    //    Assert.That(todos, Is.Not.Null);
    //    Assert.That(todos.ToList(), Has.Count.EqualTo(25));
    //}

    //[Test]
    //public async Task CreateAsync_ReturnsNewGuid()
    //{
    //    var repository = RepositoryFactory.CreateMock();
    //    var newItem = new Todo
    //    {
    //        Title = "Test item",
    //        Description = "Test description"
    //    };

    //    var id = await repository.CreateAsync(newItem);

    //    Assert.That(id, Is.Not.Empty);
    //}
}
