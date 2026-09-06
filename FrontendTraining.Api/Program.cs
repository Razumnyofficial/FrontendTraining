using System.Text.Json.Serialization;
using FrontendTraining.Application.Todos;
using FrontendTraining.Application.Todos.CreateTodo;
using FrontendTraining.Application.Todos.DeleteTodo;
using FrontendTraining.Application.Todos.GetTodo;
using FrontendTraining.Application.Todos.GetTodos;
using FrontendTraining.Application.Todos.UpdateTodo;
using FrontendTraining.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddScoped<CreateTodoHandler>();
builder.Services.AddScoped<GetTodoHandler>();
builder.Services.AddScoped<GetTodosHandler>();
builder.Services.AddScoped<DeleteTodoHandler>();
builder.Services.AddScoped<UpdateTodoHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();