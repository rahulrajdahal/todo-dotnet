using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

public record class CreateTodoItemDto([Required][StringLength(50)] string Title, bool Completed);