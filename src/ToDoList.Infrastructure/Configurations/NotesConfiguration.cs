using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain;

namespace ToDoList.Infrastructure.Configurations;

public class NotesConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> modelBuilder)
    {
        modelBuilder.ToTable("Notes");
        
        modelBuilder.HasKey(x => x.Id);
        
        modelBuilder
            .Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder
            .Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired(false);
        
        modelBuilder
            .Property(x => x.Category)
            .HasMaxLength(255)
            .IsRequired(false);
    }
}