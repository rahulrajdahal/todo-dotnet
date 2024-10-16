using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

/// <summary>
/// Contains information about the TodoItem.
/// </summary>
public record class TodoItemDto(string Id, [Required][StringLength(50)] string Title, bool Completed, DateTime CreatedAt, DateTime UpdatedAt);