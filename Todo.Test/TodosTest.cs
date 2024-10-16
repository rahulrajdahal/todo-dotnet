using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Todo.Api.Entities;

namespace Todo.Test;

public class TodosTest
{
    [Fact]
    public async Task GET_Todos_ReturnsOk()
    {
        // Arrange
        var client = new HttpClient();
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
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5038");

        // Act
        var response = await client.PostAsJsonAsync("/todos", new TodoItem() { Title = "new todo test", Completed = false });
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Empty(responseData);
    }

    [Fact]
    public async Task GET_TodoWithId_ReturnsNotFound()
    {
        // Arrange
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5038");

        // Act
        var response = await client.GetAsync("/todos/someId");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}