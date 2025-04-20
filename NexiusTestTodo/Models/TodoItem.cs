namespace NexiusTestTodo.API.Models;

public record TodoItem
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool Status { get; set; }
}
