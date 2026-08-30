using FrontendTraining.Domain.Todos;
using Xunit;

namespace FrontendTraining.Domain.Tests.Todos;

public class TodoTests
{
    // =========================================================
    // CREATE
    // =========================================================

    [Fact]
    public void Should_Create_Todo_With_Valid_Title()
    {
        // Arrange
        var title = "Изучить React";

        // Act
        var todo = new Todo(title);

        // Assert
        Assert.Equal(title, todo.Title);
        Assert.Equal(TodoStatus.InProgress, todo.Status);
        Assert.NotEqual(Guid.Empty, todo.Id);
        Assert.NotEqual(default, todo.CreatedAt);
        Assert.Null(todo.UpdatedAt);
    }

    [Fact]
    public void Should_Reject_Empty_Title()
    {
        // Arrange
        var title = "";

        // Act
        var action = () => new Todo(title);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Should_Reject_Whitespace_Title()
    {
        // Arrange
        var title = "   ";

        // Act
        var action = () => new Todo(title);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Should_Reject_Title_Shorter_Than_2_Characters()
    {
        // Arrange
        var title = "A";

        // Act
        var action = () => new Todo(title);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Should_Accept_Title_With_Exactly_2_Characters()
    {
        // Arrange
        var title = "AB";

        // Act
        var todo = new Todo(title);

        // Assert
        Assert.Equal(title, todo.Title);
    }

    [Fact]
    public void Should_Reject_Title_Longer_Than_64_Characters()
    {
        // Arrange
        var title = new string('A', 65);

        // Act
        var action = () => new Todo(title);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Should_Accept_Title_With_Exactly_64_Characters()
    {
        // Arrange
        var title = new string('A', 64);

        // Act
        var todo = new Todo(title);

        // Assert
        Assert.Equal(title, todo.Title);
    }


    // =========================================================
    // UPDATE TITLE
    // =========================================================

    [Fact]
    public void Should_Update_Title()
    {
        // Arrange
        var todo = new Todo("Изучить React");
        var newTitle = "Изучить TypeScript";

        // Act
        todo.UpdateTitle(newTitle);

        // Assert
        Assert.Equal(newTitle, todo.Title);
        Assert.NotNull(todo.UpdatedAt);
    }

    [Fact]
    public void Should_Reject_Empty_Title_When_Updating()
    {
        // Arrange
        var todo = new Todo("Изучить React");

        // Act
        var action = () => todo.UpdateTitle("");

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Should_Reject_Too_Short_Title_When_Updating()
    {
        // Arrange
        var todo = new Todo("Изучить React");

        // Act
        var action = () => todo.UpdateTitle("A");

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Should_Reject_Too_Long_Title_When_Updating()
    {
        // Arrange
        var todo = new Todo("Изучить React");
        var newTitle = new string('A', 65);

        // Act
        var action = () => todo.UpdateTitle(newTitle);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Should_Not_Update_Title_When_New_Title_Is_Invalid()
    {
        // Arrange
        var oldTitle = "Изучить React";
        var todo = new Todo(oldTitle);

        // Act
        var action = () => todo.UpdateTitle("");

        // Assert
        Assert.Throws<ArgumentException>(action);
        Assert.Equal(oldTitle, todo.Title);
    }


    // =========================================================
    // UPDATE STATUS
    // =========================================================

    [Fact]
    public void Should_Change_Status_To_Completed()
    {
        // Arrange
        var todo = new Todo("Изучить React");

        // Act
        todo.ChangeStatus(TodoStatus.Completed);

        // Assert
        Assert.Equal(TodoStatus.Completed, todo.Status);
        Assert.NotNull(todo.UpdatedAt);
    }

    [Fact]
    public void Should_Change_Status_To_InProgress()
    {
        // Arrange
        var todo = new Todo("Изучить React");

        todo.ChangeStatus(TodoStatus.Completed);

        // Act
        todo.ChangeStatus(TodoStatus.InProgress);

        // Assert
        Assert.Equal(TodoStatus.InProgress, todo.Status);
        Assert.NotNull(todo.UpdatedAt);
    }


    // =========================================================
    // TIMESTAMPS
    // =========================================================

    [Fact]
    public void Should_Set_CreatedAt_When_Todo_Is_Created()
    {
        // Arrange
        var before = DateTime.UtcNow;

        // Act
        var todo = new Todo("Изучить React");

        var after = DateTime.UtcNow;

        // Assert
        Assert.InRange(todo.CreatedAt, before, after);
    }

    [Fact]
    public void Should_Set_UpdatedAt_When_Title_Is_Changed()
    {
        // Arrange
        var todo = new Todo("Изучить React");
        var before = DateTime.UtcNow;

        // Act
        todo.UpdateTitle("Изучить TypeScript");

        var after = DateTime.UtcNow;

        // Assert
        Assert.NotNull(todo.UpdatedAt);
        Assert.InRange(todo.UpdatedAt.Value, before, after);
    }

    [Fact]
    public void Should_Set_UpdatedAt_When_Status_Is_Changed()
    {
        // Arrange
        var todo = new Todo("Изучить React");
        var before = DateTime.UtcNow;

        // Act
        todo.ChangeStatus(TodoStatus.Completed);

        var after = DateTime.UtcNow;

        // Assert
        Assert.NotNull(todo.UpdatedAt);
        Assert.InRange(todo.UpdatedAt.Value, before, after);
    }
}