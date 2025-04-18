using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data.Repositories;

public class FakeRepository : IRepository<Todo>
{
    public IEnumerable<Todo> GetAll()
    {
        return [
            new() { Title = "Test1", Description = "Test description", Status = false },
            new() { Title = "Test2", Description = "Test description", Status = false },
            new() { Title = "Test3", Description = "Test description", Status = false },
            new() { Title = "Test4", Description = "Test description", Status = false },
        ];
    }
}
