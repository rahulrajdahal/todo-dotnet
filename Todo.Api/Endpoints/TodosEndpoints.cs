using Microsoft.EntityFrameworkCore;
using Todo.Api.Handlers;
using Todo.Api.Data;
using Todo.Api.Dtos;
using Todo.Api.Entities;

namespace Todo.Api.Endpoints;

/// <summary>
/// The endpoints for TodoItems
/// </summary>
public static class TodosEndpoints
{
    /// <summary>
    /// Route Group for TodoItems
    /// </summary>
    /// <returns>The Route Group for TodosEndpoints</returns>
    public static RouteGroupBuilder MapTodosEndpoints(this WebApplication app)
    {
        RouteGroupBuilder? group = app.MapGroup("todos");

        // the TodoItem id
        string id = "/{id}";

        // Get All Todos
        group.MapGet("/", TodosHandlers.GetAllTodos);

        // Get a TodoItem with specified id.
        group.MapGet(id, TodosHandlers.GetTodo);

        // Create a new TodoItem.
        group.MapPost("/", TodosHandlers.CreateTodo);

        // Update a TodoItem with specified id.
        group.MapPut(id, TodosHandlers.PutTodo);

        // Update a TodoItem with specified id.
        group.MapPatch(id, TodosHandlers.PutTodo);

        // Delete a TodoItem with specified id.
        group.MapDelete(id, TodosHandlers.DeleteTodo);

        return group;
    }
}