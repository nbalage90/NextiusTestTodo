namespace NexiusTestTodo.API.Services.SetTodoItemStatus;

public record SetTodoItemStatusRequest(Guid Id, bool Status);
public record SetTodoItemStatusResponse(Guid Id);

public class SetTodoItemStatusEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/todoItems", async ([AsParameters] SetTodoItemStatusRequest request, ISender sender) =>
        {
            var command = request.Adapt<SetTodoItemStatusCommand>();
            
            var result = await sender.Send(command);

            return Results.Ok(result);
        });
    }
}
