namespace NexiusTestTodo.Data.Models;

public struct GetAllItemsRequest
{
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
    public bool? StatusFilter { get; set; }
    public string? DescriptionFilter { get; set; }
}
