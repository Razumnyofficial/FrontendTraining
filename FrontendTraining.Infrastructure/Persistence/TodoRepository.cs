using FrontendTraining.Application.Todos;
using FrontendTraining.Domain.Todos;
using FrontendTraining.Infrastructure.Persistence.Entities;
using FrontendTraining.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FrontendTraining.Infrastructure.Persistence;

public class TodoRepository(TodoDbContext dbContext) : ITodoRepository
{
    private readonly TodoDbContext _dbContext = dbContext;
    
    public async Task AddAsync(
        Todo todo,
        CancellationToken cancellationToken)
    {
        var entity = TodoMapper.ToEntity(todo);
        await _dbContext.Todos.AddAsync(
            entity,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
       var entity = await _dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id, cancellationToken);
      
       if(entity == null) return null;
       
       var todo = TodoMapper.ToDomain(entity);
       return todo;
    }

    public async Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Todos.ToListAsync(cancellationToken);
        var todos = entities.Select(TodoMapper.ToDomain).ToList();
        return todos;
    }

    public async Task UpdateAsync(
        Todo todo,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Todos.FindAsync(
            [todo.Id],
            cancellationToken);

        if (entity is null)
        {
            return;
        }

        entity.Title = todo.Title;
        entity.Status = todo.Status;
        entity.UpdatedAt = todo.UpdatedAt;

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var affectedRows = await _dbContext.Todos
            .Where(todo => todo.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        return affectedRows > 0;
    }
}