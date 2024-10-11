namespace Todo.Api.Entities;

public class TodoItem
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}