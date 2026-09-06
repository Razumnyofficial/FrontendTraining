using FrontendTraining.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontendTraining.Infrastructure.Persistence.Configurations;

public class TodoEntityConfiguration : IEntityTypeConfiguration<TodoEntity>
{
    public void Configure(EntityTypeBuilder<TodoEntity> builder)
    {
        builder.HasKey(todo => todo.Id);

        builder.Property(todo => todo.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(todo => todo.Status)
            .IsRequired();

        builder.Property(todo => todo.CreatedAt)
            .IsRequired();

        builder.Property(todo => todo.UpdatedAt)
            .IsRequired(false);
    }
}