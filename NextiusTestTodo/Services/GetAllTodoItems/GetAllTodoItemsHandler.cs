using NexiusTestTodo.Data.Interfaces;
using NexiusTestTodo.Domain;

namespace NexiusTestTodo.API.Services.GetAllTodoItems;

public record GetAllTodoItemsQuery(int? PageSize = 25, int? PageNumber = 1) : IRequest<GetAllTodoItemsCommand>;
public record GetAllTodoItemsCommand(IEnumerable<TodoItem> TodoItems);

public class GetAllTodoItemsHandler(IRepository<Todo> repository) : IRequestHandler<GetAllTodoItemsQuery, GetAllTodoItemsCommand>
{
    public async Task<GetAllTodoItemsCommand> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var todoItems = repository.GetAll().Adapt<IEnumerable<TodoItem>>();
        var command = new GetAllTodoItemsCommand(todoItems);
        return command;
    }
}
