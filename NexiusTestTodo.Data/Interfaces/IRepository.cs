using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Data.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken, int? PageSize = null, int? PageNumber = null);
    Task<Guid> CreateAsync(Todo entity, CancellationToken cancellationToken);
    Task<Guid> SetStatusAsync(Guid id, bool status, CancellationToken cancellationToken);
    Task<Guid> ModifyAsync(Guid id, string title, string description, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
