using FrontendTraining.Application.Common.Results;
using FrontendTraining.Application.Todos;
using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Application.Todos.GetTodo;

public class GetTodoHandler
{
    private readonly ITodoRepository _repository;

    public GetTodoHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Todo>> Handle(
        GetTodoQuery query,
        CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(
            query.Id,
            cancellationToken);

        if (todo is null)
        {
            return Result<Todo>.Failure(
                new Error("Todo.NotFound", "Todo не найден"));
        }

        return Result<Todo>.Success(todo);
    }
}