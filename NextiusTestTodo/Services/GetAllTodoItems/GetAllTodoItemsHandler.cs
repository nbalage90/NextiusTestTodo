namespace NexiusTestTodo.Services.GetAllTodoItems;

public record GetAllTodoItemsQuery(int? PageSize = 25, int? PageNumber = 1) : IRequest<GetAllTodoItemsCommand>;
public record GetAllTodoItemsCommand(IEnumerable<TodoItem> TodoItems);

public class GetAllTodoItemsHandler : IRequestHandler<GetAllTodoItemsQuery, GetAllTodoItemsCommand>
{
    public async Task<GetAllTodoItemsCommand> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var command = new GetAllTodoItemsCommand
        (
            new List<TodoItem> {
                new() { Title = "Test1", Description = "Test description", Status = false },
                new() { Title = "Test2", Description = "Test description", Status = false },
                new() { Title = "Test3", Description = "Test description", Status = false },
                new() { Title = "Test4", Description = "Test description", Status = false },
            }
        );
        return command;
    }
}
