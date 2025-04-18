namespace NexiusTestTodo.API.Services.GetAllTodoItems;

public record GetAllTodoItemsRequest(int? PageSize = 25, int? PageNumber = 1);
public record GetAllTodoItemsResponse(IEnumerable<TodoItem> TodoItems);

public class GetAllTodoItemsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/todoItems", async ([AsParameters] GetAllTodoItemsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetAllTodoItemsQuery>();

            var response = await sender.Send(query);

            var products = response.Adapt<GetAllTodoItemsResponse>();

            return Results.Ok(products);
        });
    }
}
