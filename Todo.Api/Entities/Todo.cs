namespace Todo.Api.Entities;

/// <summary>
/// Model for the TodoItem
/// </summary>
public class TodoItem
{
    /// <summary>
    /// The Identifier of the TodoItem
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// The title of the TodoItem
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// The status of the TodoItem
    /// </summary>
    public bool Completed { get; set; } = false;
    /// <summary>
    /// The DateTime for when the TodoItem was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// The DateTime for when the TodoItem was updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}