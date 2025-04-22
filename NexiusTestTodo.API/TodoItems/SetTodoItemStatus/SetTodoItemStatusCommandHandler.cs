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

public class SetTodoItemStatusCommandHandler(ITodoItemRepository repository, ILogger<SetTodoItemStatusCommandHandler> logger) : IRequestHandler<SetTodoItemStatusCommand, SetTodoItemStatusResult>
{
    public async Task<SetTodoItemStatusResult> Handle(SetTodoItemStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recieved a request for setting an item to {Status} with id: {Id}.", request.Status, request.Id);

        var validator = new InputValidator<SetTodoItemStatusCommand, SetTodoItemStatusCommandValidator>();
        validator.Validate(request);

        var result = await repository.SetStatusToAsync(request.Id, request.Status, cancellationToken);

        logger.LogInformation("Id of modified object: {Id}", result);

        return new SetTodoItemStatusResult(result);
    }
}
