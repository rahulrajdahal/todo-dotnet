using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
using Todo.Api.Dtos;
using Todo.Api.Entities;

namespace Todo.Test;

public class TodosTest
{
    readonly HttpClient client = new HttpClient();

    [Fact]
    public async Task GET_Todos_ReturnsOk()
    {
        // Arrange
        client.BaseAddress = new Uri("http://localhost:5038");

        // Act
        var response = await client.GetAsync("/todos");
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(responseData);
    }

    [Fact]
    public async Task POST_Todos_ReturnsCreated()
    {
        // Arrange
        client.BaseAddress = new Uri("http://localhost:5038");

        // Act
        var response = await client.PostAsJsonAsync("/todos", new TodoItem() { Title = "new todo unit test", Completed = false });
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotEmpty(responseData);
    }


    [Fact]
    public async Task GET_TodoWithId_ReturnsNotFound()
    {
        // Arrange
        client.BaseAddress = new Uri("http://localhost:5038");

        // Act
        var response = await client.GetAsync("/todos/somerandomId");
        var responseData = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Empty(responseData);
    }


    [Fact]
    public async Task GET_TodoWithId_ReturnsOKWithTodoItem()
    {
        // Arrange
        client.BaseAddress = new Uri("http://localhost:5038");

        var todoResponse = await client.PostAsJsonAsync("/todos", new TodoItem() { Title = "new todo unit test again", Completed = false });
        todoResponse.EnsureSuccessStatusCode();
        var todoResponseData = await todoResponse.Content.ReadAsStringAsync();
        TodoItem todo = JsonConvert.DeserializeObject<TodoItem>(todoResponseData)!;

        // Act
        var response = await client.GetAsync($"/todos/{todo.Id}");
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();
        var fetchedTodo = JsonConvert.DeserializeObject<TodoItem>(responseData)!;

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(fetchedTodo.Id, todo.Id);
        Assert.NotEmpty(responseData);
    }


    [Fact]
    public async Task PUT_TodoWithId_ReturnsNoContent()
    {
        // Arrange
        client.BaseAddress = new Uri("http://localhost:5038");

        var todosResponse = await client.GetAsync("/todos");
        todosResponse.EnsureSuccessStatusCode();
        var todosResponseData = await todosResponse.Content.ReadAsStringAsync();
        List<TodoItem> todos = JsonConvert.DeserializeObject<List<TodoItem>>(todosResponseData)!;

        string todoId = todos.Last().Id;

        // Act
        var response = await client.PutAsJsonAsync($"/todos/{todoId}", new TodoItem { Title = "new todo unit test update", Completed = true });
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(responseData);
    }

    [Fact]
    public async Task PATCH_TodoWithId_ReturnsNoContent()
    {
        // Arrange
        client.BaseAddress = new Uri("http://localhost:5038");

        var todoResponse = await client.PostAsJsonAsync("/todos", new TodoItem() { Title = "new todo unit test again", Completed = false });
        todoResponse.EnsureSuccessStatusCode();
        var todoResponseData = await todoResponse.Content.ReadAsStringAsync();
        TodoItem todo = JsonConvert.DeserializeObject<TodoItem>(todoResponseData)!;

        // Act
        var response = await client.PatchAsJsonAsync($"/todos/{todo.Id}", new TodoItem { Title = "new todo unit test update patch" });
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(responseData);
    }

    [Fact]
    public async Task DELETE_TodoWithId_ReturnsNoContent()
    {
        // Arrange
        client.BaseAddress = new Uri("http://localhost:5038");

        var todosResponse = await client.GetAsync("/todos");
        todosResponse.EnsureSuccessStatusCode();
        var todosResponseData = await todosResponse.Content.ReadAsStringAsync();
        List<TodoItem> todos = JsonConvert.DeserializeObject<List<TodoItem>>(todosResponseData)!;

        string todoId = todos.Last().Id;

        // Act
        var response = await client.DeleteAsync($"/todos/{todoId}");
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(responseData);
    }
}