using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

/// <summary>
/// Contains information about the TodoItem being created.
/// </summary>
public record class CreateTodoItemDto([Required][StringLength(50)] string Title, bool Completed);