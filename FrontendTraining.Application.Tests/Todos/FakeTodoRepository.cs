using FrontendTraining.Application.Todos;
using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Application.Tests.Todos;

public class FakeTodoRepository : ITodoRepository
{
    private readonly List<Todo> _todos = new();

    public Todo? AddedTodo { get; private set; }

    public FakeTodoRepository(Todo? todo = null)
    {
        if (todo is not null)
        {
            _todos.Add(todo);
        }
    }

    public Task AddAsync(
        Todo todo,
        CancellationToken cancellationToken)
    {
        _todos.Add(todo);
        AddedTodo = todo;

        return Task.CompletedTask;
    }

    public Task<Todo?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var todo = _todos.FirstOrDefault(
            todo => todo.Id == id);

        return Task.FromResult(todo);
    }

    public Task<IReadOnlyList<Todo>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyList<Todo>>(_todos);
    }

    public Task UpdateAsync(
        Todo todo,
        CancellationToken cancellationToken)
    {
        var index = _todos.FindIndex(
            existingTodo => existingTodo.Id == todo.Id);

        if (index >= 0)
        {
            _todos[index] = todo;
        }

        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var removed = _todos.RemoveAll(
            todo => todo.Id == id);

        return Task.FromResult(removed > 0);
    }
}