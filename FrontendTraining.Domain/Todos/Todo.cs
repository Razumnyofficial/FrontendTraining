namespace FrontendTraining.Domain.Todos;

public class Todo
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public TodoStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Todo(string title)
    {
        ValidateTitle(title);
        
        Id = Guid.NewGuid();
        Title = title;
        Status = TodoStatus.InProgress;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string title)
    {
        ValidateTitle(title);

        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void ChangeStatus(TodoStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Todo title is required.",
                nameof(title));
        }
        
        if (title.Length < 2 || title.Length > 64)
        {
            throw new ArgumentException(
                "Todo title must be between 2 and 64 characters.",
                nameof(title));
        }
    }
}

 