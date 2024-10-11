using Todo.Api.Data;
using Todo.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DATABASE");
builder.Services.AddNpgsql<TodoContext>(connectionString);

var app = builder.Build();

app.MapTodosEndpoints();

await app.MigrateDbAsync();
await app.RunAsync();
