using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Seed sample data
        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem
            {
                Id = 1,
                Title = "Welcome to TodoApp! 🎉",
                Description = "This is your first todo item. You can edit or delete it.",
                IsCompleted = false,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TodoItem
            {
                Id = 2,
                Title = "Learn ASP.NET Core MVC",
                Description = "Build modern web applications with .NET 10",
                IsCompleted = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CompletedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new TodoItem
            {
                Id = 3,
                Title = "Set up Entity Framework Core",
                Description = "Configure database with EF Core and PostgreSQL",
                IsCompleted = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CompletedAt = new DateTime(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new TodoItem
            {
                Id = 4,
                Title = "Build a beautiful UI",
                Description = "Create a modern, responsive design with Bootstrap 5",
                IsCompleted = false,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TodoItem
            {
                Id = 5,
                Title = "Deploy to production",
                Description = "Ship the app to the cloud ☁️",
                IsCompleted = false,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
