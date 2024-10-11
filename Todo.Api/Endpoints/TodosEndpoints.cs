using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Dtos;
using Todo.Api.Entities;

namespace Todo.Api.Endpoints;

public static class TodosEndpoints
{
    public static RouteGroupBuilder MapTodosEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("todos");

        // GET 
        group.MapGet("/", async (TodoContext dbContext) => await dbContext.Todos.ToListAsync());

        // GET with Id
        group.MapGet("/{id}", async (string id, TodoContext dbContext) =>
        {
            TodoItem? todo = await dbContext.Todos.FindAsync(id);

            return todo is null ? Results.NotFound() : Results.Ok(todo);
        });

        // POST
        group.MapPost("/", async (CreateTodoItemDto newTodo, TodoContext dbContext) =>
        {
            TodoItem todo = new TodoItem() { Title = newTodo.Title, Completed = newTodo.Completed };

            dbContext.Todos.Add(todo);
            await dbContext.SaveChangesAsync();

            return Results.Created();
        });

        // PUT
        group.MapPut("/{id}", async (string id, PutTodoItemDto updateTodo, TodoContext dbContext) =>
        {
            TodoItem? existingTodo = await dbContext.Todos.FindAsync(id);

            if (existingTodo is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingTodo).CurrentValues.SetValues(new TodoItem() { Id = id, Title = updateTodo.Title, Completed = updateTodo.Completed });
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE 
        group.MapDelete("/{id}", async (string id, TodoContext dbContext) =>
        {
            await dbContext.Todos
             .Where(todo => String.Equals(todo.Id, id))
             .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}