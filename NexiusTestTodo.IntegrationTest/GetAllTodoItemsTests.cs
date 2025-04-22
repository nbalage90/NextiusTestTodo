using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NexiusTestTodo.API.TodoItems.GetAllTodoItems;
using NexiusTestTodo.Data;
using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Data.Models;
using NexiusTestTodo.Data.Repositories;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.IntegrationTest;

public class GetAllTodoItemsTests
{
    private readonly ITodoItemRepository _repository;
    private readonly ILogger<GetAllTodoItemsQueryHandler> _logger;

    public GetAllTodoItemsTests()
    {
        var options = new DbContextOptionsBuilder<NexiusTestTodoDbContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=NexiusTestTodoDb; Encrypt=False")
            .Options;
        using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                                               .SetMinimumLevel(LogLevel.Trace)
                                               .AddConsole());

        _repository = new TodoRepository(new NexiusTestTodoDbContext(options));
        _logger = loggerFactory.CreateLogger<GetAllTodoItemsQueryHandler>();
    }

    [Test]
    public async Task Handle_GetAllItem_ShouldReturnList()
    {
        var command = new GetAllTodoItemsQuery();
        var handler = new GetAllTodoItemsQueryHandler(_repository, _logger);

        var newItemId = await InsertOneItemIfDbIsEmptyAsync();

        var result = await handler.Handle(command, CancellationToken.None);

        if (newItemId != Guid.Empty)
        {
            await DeleteTestItemAsync(newItemId);
        }

        Assert.That(result.TodoItems, Is.Not.Null);
    }

    [Test]
    public async Task Handle_GetAllItemWithPaging_ShouldReturnList()
    {
        var command = new GetAllTodoItemsQuery(10, 1);
        var handler = new GetAllTodoItemsQueryHandler(_repository, _logger);

        var newItemId = await InsertOneItemIfDbIsEmptyAsync();

        var result = await handler.Handle(command, CancellationToken.None);

        if (newItemId != Guid.Empty)
        {
            await DeleteTestItemAsync(newItemId);
        }

        Assert.That(result.TodoItems, Is.Not.Null);
        Assert.That(result.TodoItems.ToList(), Has.Count.LessThanOrEqualTo(10));
    }

    [Test]
    public async Task Handle_GetAllItemWithFilteringOnStatus_ShouldReturnList()
    {
        var command = new GetAllTodoItemsQuery(StatusFilter: false);
        var handler = new GetAllTodoItemsQueryHandler(_repository, _logger);

        var newItemId = await InsertOneItemIfDbIsEmptyAsync();

        var result = await handler.Handle(command, CancellationToken.None);
        var doneStatusItems = result.TodoItems.Where(todo => todo.Status == true);

        if (newItemId != Guid.Empty)
        {
            await DeleteTestItemAsync(newItemId);
        }

        Assert.That(result.TodoItems, Is.Not.Null);
        Assert.That(doneStatusItems, Is.Empty);
    }

    [Test]
    public async Task Handle_GetAllItemWithFilteringOnDescription_ShouldReturnList()
    {
        var command = new GetAllTodoItemsQuery(DescriptionFilter: "Integration");
        var handler = new GetAllTodoItemsQueryHandler(_repository, _logger);

        // Insert a new item for testcase
        var newItemId = await _repository.CreateAsync(new Todo
        {
            Description = "Integration test description",
            Status = false
        }, CancellationToken.None);

        var result = await handler.Handle(command, CancellationToken.None);

        if (newItemId != Guid.Empty)
        {
            await DeleteTestItemAsync(newItemId);
        }

        Assert.That(result.TodoItems, Is.Not.Null);
    }

    [Test]
    public async Task Handle_GetAllItemWithPagingAndFiltering_ShouldReturnAnItem()
    {
        var command = new GetAllTodoItemsQuery(10, 1, StatusFilter: false, DescriptionFilter: "Integration");
        var handler = new GetAllTodoItemsQueryHandler(_repository, _logger);

        // Insert a new item for testcase
        var newItemId = await _repository.CreateAsync(new Todo
        {
            Description = "Integration test description",
            Status = false
        }, CancellationToken.None);

        var result = await handler.Handle(command, CancellationToken.None);

        if (newItemId != Guid.Empty)
        {
            await DeleteTestItemAsync(newItemId);
        }

        Assert.That(result.TodoItems, Is.Not.Null);
    }

    [Test]
    public async Task Handle_GetAllItemWithPagingAndFiltering_ShouldNotReturnAnyItems()
    {
        var command = new GetAllTodoItemsQuery(10, 1, StatusFilter: true, DescriptionFilter: "Integration");
        var handler = new GetAllTodoItemsQueryHandler(_repository, _logger);

        // Insert a new item for testcase
        var newItemId = await _repository.CreateAsync(new Todo
        {
            Description = "Integration test description",
            Status = false
        }, CancellationToken.None);

        var result = await handler.Handle(command, CancellationToken.None);

        if (newItemId != Guid.Empty)
        {
            await DeleteTestItemAsync(newItemId);
        }

        Assert.That(result.TodoItems, Is.Not.Null);
    }

    private async Task<Guid> InsertOneItemIfDbIsEmptyAsync()
    {
        var items = (await _repository.GetAllAsync(new GetAllItemsRequest(), CancellationToken.None)).ToList();
        if (items is null || items.Count == 0)
        {
            var newItemId = await _repository.CreateAsync(new Todo
            {
                Description = "Integration test description",
                Status = false
            }, CancellationToken.None);

            return newItemId;
        }

        return Guid.Empty;
    }

    private async Task DeleteTestItemAsync(Guid id)
    {
        await _repository.DeleteAsync(id, CancellationToken.None);
    }
}