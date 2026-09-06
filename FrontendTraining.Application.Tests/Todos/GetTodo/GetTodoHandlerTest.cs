using FrontendTraining.Application.Todos;
using FrontendTraining.Application.Todos.GetTodo;
using FrontendTraining.Domain.Todos;

namespace FrontendTraining.Application.Tests.Todos.GetTodo;

public class GetTodoHandlerTests
{
    [Fact]
    public async Task Should_Return_Todo_When_Todo_Exists()
    {
        // Arrange
        var todo = new Todo("Изучить React");

        var repository = new FakeTodoRepository(todo);
        var handler = new GetTodoHandler(repository);

        var query = new GetTodoQuery(todo.Id);

        // Act
        var result = await handler.Handle(
            query,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(todo.Id, result.Value.Id);
        Assert.Equal("Изучить React", result.Value.Title);
    }

    [Fact]
    public async Task Should_Return_Failure_When_Todo_Does_Not_Exist()
    {
        // Arrange
        var repository = new FakeTodoRepository(null);
        var handler = new GetTodoHandler(repository);

        var query = new GetTodoQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(
            query,
            CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
        Assert.NotNull(result.Error);

        Assert.Equal(
            "Todo.NotFound",
            result.Error.Code);
    }
}
