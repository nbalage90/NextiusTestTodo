namespace NexiusTestTodo.API.Services.GetAllTodoItems;

public record GetAllTodoItemsQuery(int? PageSize, int PageNumber = 1) : IRequest<GetAllTodoItemsResult>;
public record GetAllTodoItemsResult(IEnumerable<TodoItem> TodoItems);

// TODO: PageSize, PageNumber validation (max 25)

public class GetAllTodoItemsHandler(IRepository<Todo> repository) : IRequestHandler<GetAllTodoItemsQuery, GetAllTodoItemsResult>
{
    public async Task<GetAllTodoItemsResult> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var todoItems = repository.GetAllAsync(request.PageSize, request.PageNumber).Adapt<IEnumerable<TodoItem>>();

        var command = new GetAllTodoItemsResult(todoItems);

        return command;
    }
}
