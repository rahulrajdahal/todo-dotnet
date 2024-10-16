using dotenv.net;
using Microsoft.OpenApi.Models;
using System.Reflection;

using Todo.Api.Data;
using Todo.Api.Endpoints;

// Load the Environment Variables.
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Inject the postgresql connection.
builder.Services.AddNpgsql<TodoContext>(Environment.GetEnvironmentVariable("DATABASE_URL"));

builder.Services.AddEndpointsApiExplorer();
// Add and Configure Swagger.
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Todo app API Documentation",
        Description = "A simple API documentation for Todo app",
        Contact = new OpenApiContact
        {
            Name = "Rahul Raj Dahal",
            Email = "rahulrajdahal@gmail.com",
            Url = new Uri(Environment.GetEnvironmentVariable("CONTACT_URL")!)
        }
    });

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Map Todos Endpoints.
app.MapTodosEndpoints();

// Perform migrations before running the Application.
await app.MigrateDbAsync();
// Run the Application.
await app.RunAsync();
