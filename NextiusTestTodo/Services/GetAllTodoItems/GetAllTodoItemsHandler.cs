namespace NexiusTestTodo.API.Services.GetAllTodoItems;

public record GetAllTodoItemsQuery(int? PageSize, int PageNumber = 1) : IRequest<GetAllTodoItemsResult>;
public record GetAllTodoItemsResult(IEnumerable<TodoItem> TodoItems);

// TODO: PageSize, PageNumber validation (max 25)

public class GetAllTodoItemsHandler(ITodoItemRepository repository) : IRequestHandler<GetAllTodoItemsQuery, GetAllTodoItemsResult>
{
    public async Task<GetAllTodoItemsResult> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var todoItemEntities = await repository.GetAllAsync(cancellationToken, request.PageSize, request.PageNumber);
        var todoItems = todoItemEntities.Adapt<IEnumerable<TodoItem>>();

        var command = new GetAllTodoItemsResult(todoItems);

        return command;
    }
}
