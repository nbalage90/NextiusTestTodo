using NexiusTestTodo.Data.Models;

namespace NexiusTestTodo.API.TodoItems.GetAllTodoItems;

public record GetAllTodoItemsQuery(int? PageSize = null, int PageNumber = 1, bool? StatusFilter = null, string? DescriptionFilter = null) : IRequest<GetAllTodoItemsResult>;
public record GetAllTodoItemsResult(IEnumerable<TodoItem> TodoItems);

public class GetAllTodoItemQueryValidator : AbstractValidator<GetAllTodoItemsQuery>
{
    public GetAllTodoItemQueryValidator()
    {
        RuleFor(q => q.PageSize).GreaterThan(0).LessThanOrEqualTo(25).WithMessage("Page size should be between 0 and 25");
        RuleFor(q => q.PageNumber).GreaterThan(0).WithMessage("Page number should be a positive number");
    }
}

public class GetAllTodoItemsQueryHandler(ITodoItemRepository repository, ILogger<GetAllTodoItemsQueryHandler> logger) : IRequestHandler<GetAllTodoItemsQuery, GetAllTodoItemsResult>
{
    public async Task<GetAllTodoItemsResult> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Recieved a request for listing all the items.");

        var validator = new InputValidator<GetAllTodoItemsQuery, GetAllTodoItemQueryValidator>();
        validator.Validate(request);

        var repositoryRequest = new GetAllItemsRequest
        {
            PageSize = request.PageSize,
            PageNumber = request.PageNumber,
            StatusFilter = request.StatusFilter,
            DescriptionFilter = request.DescriptionFilter,
        };
        var todoItemEntities = await repository.GetAllAsync(repositoryRequest, cancellationToken);
        var todoItems = todoItemEntities.Adapt<IEnumerable<TodoItem>>();

        var command = new GetAllTodoItemsResult(todoItems);

        logger.LogInformation("The received elements were listed.");

        return command;
    }
}
