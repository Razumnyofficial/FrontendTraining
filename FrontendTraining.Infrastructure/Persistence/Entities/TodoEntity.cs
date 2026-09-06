using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Infrastructure.Persistence.Entities;

public class TodoEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TodoStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}