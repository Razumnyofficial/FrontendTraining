using FrontendTraining.Application.Todos.CreateTodo;
using FrontendTraining.Domain.Todos;
using Xunit;

namespace FrontendTraining.Application.Tests.Todos.CreateTodo;

public class CreateTodoHandlerTests
{
    [Fact]
    public async Task Should_Create_Todo()
    {
        // Arrange
        var repository = new FakeTodoRepository();
        var handler = new CreateTodoHandler(repository);

        var command = new CreateTodoCommand("Изучить React");

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(
            "Изучить React",
            result.Value.Title);

        Assert.Equal(
            TodoStatus.InProgress,
            result.Value.Status);

        Assert.Equal(
            result.Value,
            repository.AddedTodo);
    }
}

