namespace NexiusTestTodo.Domain;

public class Todo
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool Status { get; set; }
}
