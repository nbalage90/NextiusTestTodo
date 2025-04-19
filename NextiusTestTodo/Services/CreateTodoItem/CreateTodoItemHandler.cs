namespace NexiusTestTodo.API.Services.CreateTodoItem;

public record CreateTodoItemCommand(string Title, string Description, bool Status) : IRequest<CreateTodoItemResult>;
public record CreateTodoItemResult(Guid Id);

public class CreateTodoItemHandler(ITodoItemRepository repository) : IRequestHandler<CreateTodoItemCommand, CreateTodoItemResult>
{
    public async Task<CreateTodoItemResult> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        TodoItem newTodoItemDto = new()
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
        };

        var newTodoItemEntity = newTodoItemDto.Adapt<Todo>();

        var result = await repository.CreateAsync(newTodoItemEntity, cancellationToken);

        return new CreateTodoItemResult(result);
    }
}
