namespace NexiusTestTodo.Data.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> GetAll(int? PageSize, int PageNumber);
}
