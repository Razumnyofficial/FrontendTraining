using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Api.Contracts.Todos;

public record TodoResponse(
    Guid Id,
    string Title,
    TodoStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);