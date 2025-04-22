namespace NexiusTestTodo.API.TodoItems.ModifyTodoItem;

public record ModifyTodoItemRequest(string Description, bool Status);
public record ModifyTodoItemResponse(Guid Id);

public class ModifyTodoItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/todoItems/{id}", async (Guid id, ModifyTodoItemRequest request, ISender sender) =>
        {
            var command = new ModifyTodoItemCommand(id, request.Description);

            var result = await sender.Send(command);

            var response = result.Adapt<ModifyTodoItemResponse>();

            return Results.Ok(response);
        });
    }
}
