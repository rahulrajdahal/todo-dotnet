using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;


/// <summary>
/// Contains information about the TodoItem being updated.
/// </summary>
public record class PutTodoItemDto([Required][StringLength(50)] string Title, [Required] bool Completed);