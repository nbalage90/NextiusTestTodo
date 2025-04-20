namespace NexiusTestTodo.API.TodoItems.GetAllTodoItems;

public record GetAllTodoItemsQuery(int? PageSize, int PageNumber = 1, bool? StatusFilter = null, string? DescriptionFilter = null) : IRequest<GetAllTodoItemsResult>;
public record GetAllTodoItemsResult(IEnumerable<TodoItem> TodoItems);

// TODO: PageSize, PageNumber validation (max 25)

public class GetAllTodoItemsHandler(ITodoItemRepository repository) : IRequestHandler<GetAllTodoItemsQuery, GetAllTodoItemsResult>
{
    public async Task<GetAllTodoItemsResult> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var todoItemEntities = await repository.GetAllAsync(cancellationToken, request.PageSize, request.PageNumber, request.StatusFilter, request.DescriptionFilter);
        var todoItems = todoItemEntities.Adapt<IEnumerable<TodoItem>>();

        var command = new GetAllTodoItemsResult(todoItems);

        return command;
    }
}
