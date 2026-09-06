using FrontendTraining.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrontendTraining.Infrastructure.Persistence;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<TodoEntity> Todos => Set<TodoEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TodoDbContext).Assembly);
    }
}