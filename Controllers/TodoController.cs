using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

public class TodoController : Controller
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Todo or /Todo/Index?filter=all|active|completed
    public async Task<IActionResult> Index(string filter = "all")
    {
        var query = _context.TodoItems.AsQueryable();

        var allItems = await _context.TodoItems.ToListAsync();
        var totalCount = allItems.Count;
        var activeCount = allItems.Count(t => !t.IsCompleted);
        var completedCount = allItems.Count(t => t.IsCompleted);

        List<TodoItem> todos = filter.ToLower() switch
        {
            "active" => allItems.Where(t => !t.IsCompleted).OrderByDescending(t => t.CreatedAt).ToList(),
            "completed" => allItems.Where(t => t.IsCompleted).OrderByDescending(t => t.CompletedAt ?? t.CreatedAt).ToList(),
            _ => allItems.OrderByDescending(t => t.CreatedAt).ToList()
        };

        var viewModel = new TodoViewModel
        {
            Todos = todos,
            CurrentFilter = filter.ToLower(),
            TotalCount = totalCount,
            ActiveCount = activeCount,
            CompletedCount = completedCount
        };

        return View(viewModel);
    }

    // POST: /Todo/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TodoViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.NewTodo.Title))
        {
            TempData["Error"] = "Title is required!";
            return RedirectToAction(nameof(Index));
        }

        var todo = new TodoItem
        {
            Title = model.NewTodo.Title.Trim(),
            Description = model.NewTodo.Description?.Trim(),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Todo added successfully!";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Todo/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null) return NotFound();

        return View(todo);
    }

    // POST: /Todo/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TodoItem todo)
    {
        if (id != todo.Id) return NotFound();

        if (string.IsNullOrWhiteSpace(todo.Title))
        {
            ModelState.AddModelError("Title", "Title is required!");
            return View(todo);
        }

        try
        {
            var existingTodo = await _context.TodoItems.FindAsync(id);
            if (existingTodo == null) return NotFound();

            existingTodo.Title = todo.Title.Trim();
            existingTodo.Description = todo.Description?.Trim();

            await _context.SaveChangesAsync();
            TempData["Success"] = "Todo updated successfully!";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.TodoItems.AnyAsync(e => e.Id == id))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // POST: /Todo/ToggleComplete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleComplete(int id, string? returnFilter)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null) return NotFound();

        todo.IsCompleted = !todo.IsCompleted;
        todo.CompletedAt = todo.IsCompleted ? DateTime.UtcNow : null;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { filter = returnFilter ?? "all" });
    }

    // POST: /Todo/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, string? returnFilter)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null) return NotFound();

        _context.TodoItems.Remove(todo);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Todo deleted successfully!";
        return RedirectToAction(nameof(Index), new { filter = returnFilter ?? "all" });
    }

    // POST: /Todo/ClearCompleted
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ClearCompleted()
    {
        var completedTodos = await _context.TodoItems.Where(t => t.IsCompleted).ToListAsync();
        _context.TodoItems.RemoveRange(completedTodos);
        await _context.SaveChangesAsync();

        TempData["Success"] = $"Cleared {completedTodos.Count} completed todos!";
        return RedirectToAction(nameof(Index));
    }
}
