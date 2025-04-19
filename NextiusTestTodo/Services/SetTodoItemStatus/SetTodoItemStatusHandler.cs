namespace NexiusTestTodo.API.Services.SetTodoItemStatus;

public record SetTodoItemStatusCommand(Guid Id, bool Status) : IRequest<SetTodoItemStatusResult>;
public record SetTodoItemStatusResult(Guid Id);

public class SetTodoItemStatusHandler(ITodoItemRepository repository) : IRequestHandler<SetTodoItemStatusCommand, SetTodoItemStatusResult>
{
    public async Task<SetTodoItemStatusResult> Handle(SetTodoItemStatusCommand request, CancellationToken cancellationToken)
    {
        // TODO: ha nincs elem ilyen id-val?
        var result = await repository.SetStatusAsync(request.Id, request.Status, cancellationToken);

        return new SetTodoItemStatusResult(result);
    }
}
