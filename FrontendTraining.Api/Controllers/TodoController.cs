using FrontendTraining.Api.Contracts.Common;
using FrontendTraining.Api.Contracts.Todos;
using FrontendTraining.Application.Todos.CreateTodo;
using FrontendTraining.Application.Todos.DeleteTodo;
using FrontendTraining.Application.Todos.GetTodo;
using FrontendTraining.Application.Todos.GetTodos;
using FrontendTraining.Application.Todos.UpdateTodo;
using Microsoft.AspNetCore.Mvc;

namespace FrontendTraining.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly CreateTodoHandler _createTodoHandler;
    private readonly GetTodoHandler _getTodoHandler;
    private readonly GetTodosHandler _getTodosHandler;
    private readonly DeleteTodoHandler _deleteTodoHandler;
    private readonly UpdateTodoHandler _updateTodoHandler;

    public TodoController(CreateTodoHandler createTodoHandler, GetTodoHandler getTodoHandler,
        GetTodosHandler getTodosHandler, DeleteTodoHandler deleteTodoHandler, UpdateTodoHandler updateTodoHandler)
    {
        _createTodoHandler = createTodoHandler;
        _getTodoHandler = getTodoHandler;
        _getTodosHandler = getTodosHandler;
        _deleteTodoHandler = deleteTodoHandler;
        _updateTodoHandler = updateTodoHandler;
    }

    [HttpPut("{id}")]
    [EndpointSummary("Обновить Todo")]
    [EndpointDescription("Обновляет название и статус Todo по указанному идентификатору.")]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateTodoRequest request)
    {
        var command = new UpdateTodoCommand(
            id,
            request.Title,
            request.Status);

        var result = await _updateTodoHandler.Handle(
            command,
            CancellationToken.None);

        if (result.IsFailure)
        {
            var errorResponse = new ErrorResponse(
                result.Error!.Code,
                result.Error.Message);

            return NotFound(errorResponse);
        }

        var todo = result.Value!;

        var response = new TodoResponse(
            todo.Id,
            todo.Title,
            todo.Status,
            todo.CreatedAt,
            todo.UpdatedAt);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [EndpointSummary("Удалить Todo")]
    [EndpointDescription("Удаляет Todo по указанному идентификатору.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTodoCommand(id);

        var result = await _deleteTodoHandler.Handle(
            command,
            CancellationToken.None);

        if (result.IsFailure)
        {
            var errorResponse = new ErrorResponse(
                result.Error!.Code,
                result.Error.Message);

            return NotFound(errorResponse);
        }

        return NoContent();
    }

    [HttpGet]
    [EndpointSummary("Получить все Todo")]
    [EndpointDescription("Возвращает список всех Todo.")]
    [ProducesResponseType(typeof(IReadOnlyList<TodoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetTodosQuery();

        var result = await _getTodosHandler.Handle(
            query,
            CancellationToken.None);

        var response = result.Value!
            .Select(todo => new TodoResponse(
                todo.Id,
                todo.Title,
                todo.Status,
                todo.CreatedAt,
                todo.UpdatedAt))
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Получить Todo по идентификатору")]
    [EndpointDescription("Возвращает Todo по указанному идентификатору.")]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetTodoQuery(id);
        var result = await _getTodoHandler.Handle(query, CancellationToken.None);

        if (result.IsFailure)
        {
            var errorResponse = new ErrorResponse(
                result.Error!.Code,
                result.Error.Message);

            return NotFound(errorResponse);
        }

        var todo = result.Value!;
        var response = new TodoResponse(
            todo.Id,
            todo.Title,
            todo.Status,
            todo.CreatedAt,
            todo.UpdatedAt
        );

        return Ok(response);
    }

    [HttpPost]
    [EndpointSummary("Создать Todo")]
    [EndpointDescription("Создаёт новую Todo и возвращает её идентификатор.")]
    [ProducesResponseType(typeof(CreateTodoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateTodoRequest request)
    {
        var command = new CreateTodoCommand(request.Title);

        var result = await _createTodoHandler.Handle(
            command,
            CancellationToken.None);

        if (result.IsFailure)
        {
            var errorResponse = new ErrorResponse(
                result.Error!.Code,
                result.Error.Message);

            return BadRequest(errorResponse);
        }

        var response = new CreateTodoResponse(
            result.Value!.Id);

        return StatusCode(StatusCodes.Status201Created, response);
    }
}

 