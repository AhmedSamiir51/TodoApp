namespace TodoApp.Models;

public class TodoViewModel
{
    public List<TodoItem> Todos { get; set; } = new();
    public TodoItem NewTodo { get; set; } = new();
    public string CurrentFilter { get; set; } = "all";
    public int TotalCount { get; set; }
    public int ActiveCount { get; set; }
    public int CompletedCount { get; set; }
}
