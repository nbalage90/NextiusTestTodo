namespace NexiusTestTodo.Data.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(int? PageSize, int PageNumber);
    Task<Guid> CreateAsync(T entity);
}
