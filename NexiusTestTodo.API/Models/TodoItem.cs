namespace NexiusTestTodo.API.Models;

public record TodoItem
{
    public required string Description { get; set; }
    public bool Status { get; set; }
}
