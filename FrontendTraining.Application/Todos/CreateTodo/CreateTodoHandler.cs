using FrontendTraining.Application.Common.Results;
using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Application.Todos.CreateTodo;

public class CreateTodoHandler
{
    private readonly ITodoRepository _repository;

    public CreateTodoHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Todo>> Handle(
        CreateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var todo = new Todo(command.Title);

        await _repository.AddAsync(
            todo,
            cancellationToken);

        return Result<Todo>.Success(todo);
    }
}