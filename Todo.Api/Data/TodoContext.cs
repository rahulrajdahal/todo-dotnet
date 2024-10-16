
using Microsoft.EntityFrameworkCore;
using Todo.Api.Entities;

namespace Todo.Api.Data;

/// <summary>
/// The database context
/// </summary>
public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    /// <summary>
    /// Collection of TodoItems.
    /// </summary>
    public DbSet<TodoItem> Todos { get; set; }

    /// <summary>
    /// Override the the database instances on database creation. 
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {
        modelBuilder
            .Entity<TodoItem>()
            .Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder
            .Entity<TodoItem>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("now()");
    }

}