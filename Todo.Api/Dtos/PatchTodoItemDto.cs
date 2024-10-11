using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

public record class PatchTodoItemDto(string Title, bool Completed);