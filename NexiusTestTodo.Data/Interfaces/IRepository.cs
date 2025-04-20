using NexiusTestTodo.Domain;

namespace NexiusTestTodo.Repository.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken, int? pageSize = null, int? pageNumber = null, bool? statusFilter = null, string? descriptionFilter = null);
    Task<Guid> CreateAsync(Todo entity, CancellationToken cancellationToken);
    Task<Guid> SetStatusAsync(Guid id, bool status, CancellationToken cancellationToken);
    Task<Guid> ModifyAsync(Guid id, string title, string description, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
