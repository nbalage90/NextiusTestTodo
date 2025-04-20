
namespace NexiusTestTodo.API.Services.ModifyTodoItem;

public record ModifyTodoItemCommand(Guid Id, string Title, string Description) : IRequest<ModifyTodoItemResult>;
public record ModifyTodoItemResult(Guid Id);

public class ModifyTodoItemHandler(ITodoItemRepository repository) : IRequestHandler<ModifyTodoItemCommand, ModifyTodoItemResult>
{
    public async Task<ModifyTodoItemResult> Handle(ModifyTodoItemCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.ModifyAsync(request.Id, request.Title, request.Description, cancellationToken);

        return new ModifyTodoItemResult(result);
    }
}
