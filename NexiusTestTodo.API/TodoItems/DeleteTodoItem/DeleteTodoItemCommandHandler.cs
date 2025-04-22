namespace NexiusTestTodo.API.TodoItems.DeleteTodoItem;

public record DeleteTodoItemCommand(Guid Id) : IRequest<DeleteTodoItemResult>;
public record DeleteTodoItemResult(bool IsSuccess);

public class DeleteTodoItemCommandValidator : AbstractValidator<DeleteTodoItemCommand>
{
    public DeleteTodoItemCommandValidator()
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

public class DeleteTodoItemCommandHandler(ITodoItemRepository repository, ILogger<DeleteTodoItemCommandHandler> logger) : IRequestHandler<DeleteTodoItemCommand, DeleteTodoItemResult>
{
    public async Task<DeleteTodoItemResult> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recieved a deletion request: {Id}.", request.Id);

        var validator = new InputValidator<DeleteTodoItemCommand, DeleteTodoItemCommandValidator>();
        validator.Validate(request);

        var result = await repository.DeleteAsync(request.Id, cancellationToken);

        logger.LogInformation("Id of created object: {Id}", result);

        return new DeleteTodoItemResult(result);
    }
}
