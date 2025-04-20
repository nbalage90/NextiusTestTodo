namespace NexiusTestTodo.API.TodoItems.DeleteTodoItem;

public record DeleteTodoItemCommand(Guid Id) : IRequest<DeleteTodoItemResult>;
public record DeleteTodoItemResult(bool IsSuccess);

public class DeleteTodoItemHandler(ITodoItemRepository repository) : IRequestHandler<DeleteTodoItemCommand, DeleteTodoItemResult>
{
    public async Task<DeleteTodoItemResult> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteAsync(request.Id, cancellationToken);

        return new DeleteTodoItemResult(result);
    }
}
