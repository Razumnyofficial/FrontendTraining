using FrontendTraining.Domain.Todos;
using FrontendTraining.Infrastructure.Persistence.Entities;

namespace FrontendTraining.Infrastructure.Persistence.Mappers;

public static class TodoMapper
{
    public static TodoEntity ToEntity(Todo todo)
    {
        return new TodoEntity
        {
            Id = todo.Id,
            Title = todo.Title,
            Status = todo.Status,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public static Todo ToDomain(TodoEntity entity)
    {
        return Todo.Rehydrate(
            entity.Id,
            entity.Title,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}