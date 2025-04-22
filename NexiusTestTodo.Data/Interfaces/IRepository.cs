using NexiusTestTodo.Data.Models;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<Todo>> GetAllAsync(GetAllItemsRequest request, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(Todo entity, CancellationToken cancellationToken);
    Task<Guid> SetStatusToAsync(Guid id, bool status, CancellationToken cancellationToken);
    Task<Guid> ModifyDescriptionToAsync(Guid id, string description, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
