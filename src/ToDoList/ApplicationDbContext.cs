using Microsoft.EntityFrameworkCore;
using ToDoList.Domain;

namespace ToDoList;

public class ApplicationDbContext(string connectionString) : DbContext
{
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
            .ToTable("Notes");
        
        modelBuilder.Entity<Note>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<Note>()
            .Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Note>()
            .Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired(false);
        
        modelBuilder.Entity<Note>()
            .Property(x => x.Category)
            .HasMaxLength(255)
            .IsRequired(false);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
    }
}