using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

public record class PutTodoItemDto([Required][StringLength(50)] string Title, [Required] bool Completed);