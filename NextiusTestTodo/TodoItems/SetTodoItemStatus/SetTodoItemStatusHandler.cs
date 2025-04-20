namespace NexiusTestTodo.API.TodoItems.SetTodoItemStatus;

public record SetTodoItemStatusCommand(Guid Id, bool Status) : IRequest<SetTodoItemStatusResult>;
public record SetTodoItemStatusResult(Guid Id);

public class SetTodoItemStatusCommandValidator : AbstractValidator<SetTodoItemStatusCommand>
{
    public SetTodoItemStatusCommandValidator()
    {
        RuleFor(c => c.Id).Custom((guid, context) =>
        {
            if (guid == Guid.Empty)
            {
                context.AddFailure("Id should not be default");
            }
        });
    }
}

public class SetTodoItemStatusHandler(ITodoItemRepository repository) : IRequestHandler<SetTodoItemStatusCommand, SetTodoItemStatusResult>
{
    public async Task<SetTodoItemStatusResult> Handle(SetTodoItemStatusCommand request, CancellationToken cancellationToken)
    {
        // TODO: ha nincs elem ilyen id-val?
        var result = await repository.SetStatusAsync(request.Id, request.Status, cancellationToken);

        return new SetTodoItemStatusResult(result);
    }
}
