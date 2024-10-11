using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
        await dbContext.Database.MigrateAsync();
    }
}