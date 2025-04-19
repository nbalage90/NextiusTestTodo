using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data.Repositories;

public class FakeRepository : ITodoItemRepository
{
    ICollection<Todo> todos = [
            new() { Id = Guid.NewGuid(), Title = "Test1", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test2", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test3", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test5", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test6", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test7", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test8", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test9", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test10", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test11", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test12", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test13", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test14", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test15", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test16", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test17", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test18", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test19", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test20", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test21", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test22", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test23", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test24", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test25", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test26", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test27", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test28", Description = "Test description", Status = false },
            new() { Id = Guid.NewGuid(), Title = "Test29", Description = "Test description", Status = false },
        ];

    public async Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken, int? PageSize, int? PageNumber)
    {
        IEnumerable<Todo> retVal;

        if (PageSize is not null && PageNumber is not null)
        {
            retVal = todos.Skip((PageNumber.Value - 1) * PageSize.Value).Take(PageSize.Value);
        }
        else
        {
            retVal = todos;
        }

        return retVal;
    }

    public async Task<Guid> CreateAsync(Todo entity, CancellationToken cancellationToken)
    {
        entity.Id = Guid.NewGuid();
        todos.Add(entity);
        return entity.Id;
    }

    public async Task<Guid> SetStatusAsync(Guid id, bool status, CancellationToken cancellationToken)
    {
        var todoItem = todos.SingleOrDefault(item => item.Id == id);
        if (todoItem is null)
        {
            throw new ArgumentException();
        }

        todoItem.Status = status;

        return id;
    }

    public async Task<Guid> ModifyItemAsync(Guid id, string title, string description, CancellationToken cancellationToken)
    {
        var item = todos.Single(todo => todo.Id == id);
        item.Title = title is not null ? title : item.Title;
        item.Description = description is not null ? description : item.Description;
        return id;
    }
}
