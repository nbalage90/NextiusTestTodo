namespace NexiusTestTodo.API.TodoItems.ModifyTodoItem;

public record ModifyTodoItemCommand(Guid Id, string Title, string Description) : IRequest<ModifyTodoItemResult>;
public record ModifyTodoItemResult(Guid Id);

public class ModifyTodoItemCommandValidator : AbstractValidator<ModifyTodoItemCommand>
{
    public ModifyTodoItemCommandValidator()
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

public class ModifyTodoItemHandler(ITodoItemRepository repository) : IRequestHandler<ModifyTodoItemCommand, ModifyTodoItemResult>
{
    public async Task<ModifyTodoItemResult> Handle(ModifyTodoItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new InputValidator<ModifyTodoItemCommand, ModifyTodoItemCommandValidator>();
        validator.Validate(request);

        var result = await repository.ModifyAsync(request.Id, request.Title, request.Description, cancellationToken);

        return new ModifyTodoItemResult(result);
    }
}
