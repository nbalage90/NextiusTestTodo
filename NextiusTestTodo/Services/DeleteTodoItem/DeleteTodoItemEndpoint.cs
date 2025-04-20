namespace NexiusTestTodo.API.Services.DeleteTodoItem;

public record DeleteTodoItemResponse(bool IsSuccess);

public class DeleteTodoItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/todoItems/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteTodoItemCommand(id);

            var result = await sender.Send(command);

            return Results.Ok(result);
        });
    }
}
