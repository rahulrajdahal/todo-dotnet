using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Data;

/// <summary>
/// Extends the Database
/// </summary>
public static class DataExtensions
{
    /// <summary>
    /// Creates the database instance
    /// </summary>
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
        await dbContext.Database.MigrateAsync();
    }
}