using FrontendTraining.Application.Common.Results;
using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Application.Todos.UpdateTodo;

public class UpdateTodoHandler
{
    private readonly ITodoRepository _repository;

    public UpdateTodoHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Todo>> Handle(
        UpdateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(
            command.Id,
            cancellationToken);

        if (todo is null)
        {
            return Result<Todo>.Failure(
                new Error(
                    "Todo.NotFound",
                    "Todo не найден"));
        }

        todo.UpdateTitle(command.Title);
        todo.ChangeStatus(command.Status);

        await _repository.UpdateAsync(
            todo,
            cancellationToken);

        return Result<Todo>.Success(todo);
    }
}