using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Application.Todos.UpdateTodo;

public record UpdateTodoCommand(
    Guid Id,
    string Title,
    TodoStatus Status);