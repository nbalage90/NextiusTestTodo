namespace NexiusTestTodo.API.TodoItems.CreateTodoItem;

public record CreateTodoItemRequest(string Description, bool Status);
public record CreateTodoItemResponse(Guid Id);

public class CreateTodoItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/todoItems", async (CreateTodoItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateTodoItemCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateTodoItemResponse>();

            return Results.Ok(response);
        });
    }
}
