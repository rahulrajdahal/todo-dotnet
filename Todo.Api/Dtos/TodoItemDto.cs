using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

public record class TodoItemDto(int Id, [Required][StringLength(50)] string Title, bool Completed, DateTime CreatedAt, DateTime UpdatedAt);