﻿namespace NexiusTestTodo.API.TodoItems.DeleteTodoItem;

public record DeleteTodoItemResponse(bool IsSuccess);

public class DeleteTodoItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/todoItems/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteTodoItemCommand(id);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteTodoItemResponse>();

            return Results.Ok(result);
        });
    }
}
