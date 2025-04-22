namespace NexiusTestTodo.API.TodoItems.CreateTodoItem;

public record CreateTodoItemCommand(string Description, bool Status) : IRequest<CreateTodoItemResult>;
public record CreateTodoItemResult(Guid Id);

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(c => c.Description).NotEmpty().WithMessage("Description should not be empty");
    }
}

public class CreateTodoItemCommandHandler(ITodoItemRepository repository, ILogger<CreateTodoItemCommandHandler> logger) : IRequestHandler<CreateTodoItemCommand, CreateTodoItemResult>
{
    public async Task<CreateTodoItemResult> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recieved a creation request: {Description}, {Status}.", request.Description, request.Status);

        var validator = new InputValidator<CreateTodoItemCommand, CreateTodoItemCommandValidator>();
        validator.Validate(request);

        var newTodoItemEntity = request.Adapt<Todo>();

        var result = await repository.CreateAsync(newTodoItemEntity, cancellationToken);

        logger.LogInformation("Id of created object: {Id}", result);

        return new CreateTodoItemResult(result);
    }
}
