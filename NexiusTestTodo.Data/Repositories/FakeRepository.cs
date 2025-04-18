using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data.Repositories;

public class FakeRepository : IRepository<Todo>
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

    public async Task<IEnumerable<Todo>> GetAllAsync(int? PageSize, int PageNumber)
    {
        IEnumerable<Todo> retVal;

        if (PageSize is not null)
        {
            retVal = todos.Skip((PageNumber - 1) * PageSize.Value).Take(PageSize.Value);
        }
        else
        {
            retVal = todos;
        }

        return retVal;
    }

    public async Task<Guid> CreateAsync(Todo entity)
    {
        entity.Id = Guid.NewGuid();
        todos.Add(entity);
        return entity.Id;
    }
}
