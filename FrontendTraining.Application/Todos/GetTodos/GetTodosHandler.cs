using FrontendTraining.Application.Common.Results;
using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Application.Todos.GetTodos;

public class GetTodosHandler
{
    private readonly ITodoRepository _repository;

    public GetTodosHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyList<Todo>>> Handle(
        GetTodosQuery query,
        CancellationToken cancellationToken)
    {
        var todos = await _repository.GetAllAsync(
            cancellationToken);

        return Result<IReadOnlyList<Todo>>.Success(todos);
    }
}