using Microsoft.EntityFrameworkCore;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data;

public class NexiusTestTodoDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    public NexiusTestTodoDbContext(DbContextOptions<NexiusTestTodoDbContext> options) : base(options)
    {
        
    }
}
