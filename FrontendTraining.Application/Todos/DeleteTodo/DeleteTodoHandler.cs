using FrontendTraining.Application.Common.Results;

namespace FrontendTraining.Application.Todos.DeleteTodo;

public class DeleteTodoHandler
{
    private readonly ITodoRepository _repository;

    public DeleteTodoHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(
        DeleteTodoCommand command,
        CancellationToken cancellationToken)
    {
        var deleted = await _repository.DeleteAsync(
            command.Id,
            cancellationToken);

        if (!deleted)
        {
            return Result.Failure(
                new Error(
                    "Todo.NotFound",
                    "Todo не найден"));
        }

        return Result.Success();
    }
}