using Todo.Api.Dtos;
using Todo.Api.Entities;

namespace Todo.Api.Mappings;

static class TodosMappings
{
    public static TodoItem ToEntity(this CreateTodoItemDto newTodo)
    {
        return new TodoItem
        {
            Title = newTodo.Title,
            Completed = newTodo.Completed,
        };
    }

    public static TodoItem ToEntity(this PutTodoItemDto updateTodo, string id, DateTime createdAt)
    {
        return new TodoItem
        {
            Id = id,
            Title = updateTodo.Title,
            Completed = updateTodo.Completed,
            CreatedAt = createdAt,
        };
    }

    public static TodoItem ToEntity(this PatchTodoItemDto updateTodo, string id, DateTime createdAt)
    {
        return new TodoItem
        {
            Id = id,
            Title = updateTodo.Title,
            Completed = updateTodo.Completed,
            CreatedAt = createdAt,
        };
    }

    public static TodoItemDto ToDetails(this TodoItem todo)
    {
        return new(todo.Id!, todo.Title!, todo.Completed, todo.CreatedAt, todo.UpdatedAt);
    }
}