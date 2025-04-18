using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data.Repositories;

public class FakeRepository : IRepository<Todo>
{
    IEnumerable<Todo> todos = [
            new() { Title = "Test1", Description = "Test description", Status = false },
            new() { Title = "Test2", Description = "Test description", Status = false },
            new() { Title = "Test3", Description = "Test description", Status = false },
            new() { Title = "Test5", Description = "Test description", Status = false },
            new() { Title = "Test6", Description = "Test description", Status = false },
            new() { Title = "Test7", Description = "Test description", Status = false },
            new() { Title = "Test8", Description = "Test description", Status = false },
            new() { Title = "Test9", Description = "Test description", Status = false },
            new() { Title = "Test10", Description = "Test description", Status = false },
            new() { Title = "Test11", Description = "Test description", Status = false },
            new() { Title = "Test12", Description = "Test description", Status = false },
            new() { Title = "Test13", Description = "Test description", Status = false },
            new() { Title = "Test14", Description = "Test description", Status = false },
            new() { Title = "Test15", Description = "Test description", Status = false },
            new() { Title = "Test16", Description = "Test description", Status = false },
            new() { Title = "Test17", Description = "Test description", Status = false },
            new() { Title = "Test18", Description = "Test description", Status = false },
            new() { Title = "Test19", Description = "Test description", Status = false },
            new() { Title = "Test20", Description = "Test description", Status = false },
            new() { Title = "Test21", Description = "Test description", Status = false },
            new() { Title = "Test22", Description = "Test description", Status = false },
            new() { Title = "Test23", Description = "Test description", Status = false },
            new() { Title = "Test24", Description = "Test description", Status = false },
            new() { Title = "Test25", Description = "Test description", Status = false },
            new() { Title = "Test26", Description = "Test description", Status = false },
            new() { Title = "Test27", Description = "Test description", Status = false },
            new() { Title = "Test28", Description = "Test description", Status = false },
            new() { Title = "Test29", Description = "Test description", Status = false },
        ];

    public IEnumerable<Todo> GetAll(int? PageSize, int PageNumber)
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
}
