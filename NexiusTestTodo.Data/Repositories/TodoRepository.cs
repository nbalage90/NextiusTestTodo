using Microsoft.EntityFrameworkCore;
using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data.Repositories;

public class TodoRepository(NexiusTestTodoDbContext context) : ITodoItemRepository
{
    public async Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken, int? PageSize = null, int? PageNumber = null, bool? statusFilter = null, string? descriptionFilter = null)
    {
        IEnumerable<Todo> retVal;

        if (PageSize is not null && PageNumber is not null)
        {
            retVal = context.Todos.Skip((PageNumber.Value - 1) * PageSize.Value).Take(PageSize.Value);
        }
        else
        {
            retVal = context.Todos;
        }

        if (statusFilter is not null)
        {
            retVal = retVal.Where(todo => todo.Status == statusFilter.Value);
        }

        if (descriptionFilter is not null)
        {
            retVal = retVal.Where(todo => todo.Description.ToLower().Contains(descriptionFilter.ToLower()));
        }

        return retVal;
    }

    public async Task<Guid> CreateAsync(Todo entity, CancellationToken cancellationToken)
    {
        entity.Id = Guid.NewGuid();
        context.Todos.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<Guid> SetStatusAsync(Guid id, bool status, CancellationToken cancellationToken)
    {
        var todoItem = await context.Todos.SingleOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (todoItem is null)
        {
            throw new ArgumentOutOfRangeException();
        }

        todoItem.Status = status;

        await context.SaveChangesAsync(cancellationToken);

        return id;
    }

    public async Task<Guid> ModifyAsync(Guid id, string title, string description, CancellationToken cancellationToken)
    {
        var item = await context.Todos.SingleAsync(todo => todo.Id == id, cancellationToken);

        if (item is null)
        {
            throw new ArgumentOutOfRangeException();
        }

        item.Title = title is not null ? title : item.Title;
        item.Description = description is not null ? description : item.Description;

        await context.SaveChangesAsync(cancellationToken);

        return id;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await context.Todos.SingleOrDefaultAsync(todo => todo.Id == id, cancellationToken);

        if (item is null)
        {
            throw new ArgumentOutOfRangeException();
        }

        context.Todos.Remove(item);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
