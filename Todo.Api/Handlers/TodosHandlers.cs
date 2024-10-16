using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Api.Dtos;
using Todo.Api.Entities;
using Todo.Api.Mappings;

namespace Todo.Api.Handlers;

static class TodosHandlers
{

    /// <summary> 
    /// Get a list of Todos
    /// </summary>
    /// <returns> List of Todos </returns>
    /// <response code="200">OK - Returns a list of Todos</response>
    public static async Task<IResult> GetAllTodos(TodoContext dbContext)
    {
        List<TodoItem> todos = await dbContext.Todos.ToListAsync();
        return TypedResults.Ok(todos);
    }

    /// <summary> Get a TodoItem with specified id </summary>
    /// <returns> TodoItem for the specified id </returns>
    /// <param name="id"></param>
    /// <response code="200">Success - OK, Returns TodoItem for the specified TodoItem</response>
    /// <response code="404">Error - Not Found</response>
    public static async Task<IResult> GetTodo(string id, TodoContext dbContext)
    {
        TodoItem? todo = await dbContext.Todos.FindAsync(id);

        if (todo is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(todo.ToDetails());
    }

    /// <summary> Create a new TodoItem. </summary>
    /// <returns> A newly created TodoItem </returns>
    /// <remarks>
    ///     Example Request:
    ///         {
    ///             "title": "Example task title"
    ///         }
    /// </remarks>
    /// <response code="201">Success - Created, Returns the newly created TodoItem.</response>
    public static async Task<IResult> CreateTodo(CreateTodoItemDto newTodo, TodoContext dbContext)
    {
        TodoItem todo = newTodo.ToEntity();

        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync();

        return TypedResults.Created($"{todo.Id}", todo.ToDetails());
    }

    /// <summary> Update a TodoItem with specified id </summary>
    /// <returns> Empty Content </returns>
    /// <param name="id"></param>
    /// <remarks>
    ///     Example Request:
    ///         {
    ///             "title": "Example task title."
    ///         }
    /// </remarks>
    /// <response code="204">Success - No Content</response>
    /// <response code="404">Error - Not Found</response>
    public static async Task<IResult> PutTodo(string id, PutTodoItemDto updateTodo, TodoContext dbContext)
    {
        TodoItem? existingTodo = await dbContext.Todos.FindAsync(id);

        if (existingTodo is null)
        {
            return TypedResults.NotFound();
        }

        dbContext.Entry(existingTodo).CurrentValues.SetValues(updateTodo.ToEntity(id, existingTodo.CreatedAt));
        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    /// <summary> Update a TodoItem with specified id </summary>
    /// <returns> Empty Content </returns>
    /// <param name="id"></param>
    /// <remarks>
    ///     Example Request:
    ///         {
    ///             "title": "Example task title"
    ///         }
    /// </remarks>
    /// <response code="204">Success - No Content</response>
    /// <response code="404">Error - Not Found</response>
    public static async Task<IResult> PatchTodo(string id, PatchTodoItemDto updateTodo, TodoContext dbContext)
    {
        TodoItem? existingTodo = await dbContext.Todos.FindAsync(id);

        if (existingTodo is null)
        {
            return TypedResults.NotFound();
        }

        dbContext.Entry(existingTodo).CurrentValues.SetValues(updateTodo.ToEntity(id, existingTodo.CreatedAt));
        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    /// <summary> Delete a TodoItem with specified id </summary>
    /// <returns> Empty Content </returns>
    /// <param name="id"></param>
    /// <response code="204">Success - No Content</response>
    public static async Task<IResult> DeleteTodo(string id, TodoContext dbContext)
    {
        await dbContext.Todos.Where(todo => string.Equals(todo.Id, id)).ExecuteDeleteAsync();

        return Results.NoContent();
    }

}