using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Api.Contracts.Todos;

public record UpdateTodoRequest(
    string Title,
    TodoStatus Status);