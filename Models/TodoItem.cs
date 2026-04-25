using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;

public class TodoItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    [Display(Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Completed")]
    public bool IsCompleted { get; set; }

    [Display(Name = "Created")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Completed At")]
    public DateTime? CompletedAt { get; set; }
}
