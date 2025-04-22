using Microsoft.EntityFrameworkCore;
using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Data.Models;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data.Repositories;

public class TodoRepository(NexiusTestTodoDbContext context) : ITodoItemRepository
{
    // TODO: less parameters
    public async Task<IEnumerable<Todo>> GetAllAsync(GetAllItemsRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<Todo> retVal;

        if (request.PageSize is not null && request.PageNumber is not null)
        {
            retVal = context.Todos.Skip((request.PageNumber.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value);
        }
        else
        {
            retVal = context.Todos;
        }

        if (request.StatusFilter is not null)
        {
            retVal = retVal.Where(todo => todo.Status == request.StatusFilter.Value);
        }

        if (request.DescriptionFilter is not null)
        {
            retVal = retVal.Where(todo => todo.Description.Contains(request.DescriptionFilter, StringComparison.CurrentCultureIgnoreCase));
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

    public async Task<Guid> SetStatusToAsync(Guid id, bool status, CancellationToken cancellationToken)
    {
        var todoItem = await context.Todos.SingleOrDefaultAsync(item => item.Id == id, cancellationToken) ?? throw new ArgumentOutOfRangeException();
        todoItem.Status = status;

        await context.SaveChangesAsync(cancellationToken);

        return id;
    }

    public async Task<Guid> ModifyDescriptionToAsync(Guid id, string description, CancellationToken cancellationToken)
    {
        var item = await context.Todos.SingleAsync(todo => todo.Id == id, cancellationToken) ?? throw new ArgumentOutOfRangeException();
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
