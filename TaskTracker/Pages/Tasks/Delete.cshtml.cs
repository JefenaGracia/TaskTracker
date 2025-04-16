using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = new TaskItem();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _context.TaskItems.FindAsync(id);
            if (TaskItem == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var task = await _context.TaskItems
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TaskItemId == TaskItem.TaskItemId);

            if (task != null)
            {
                _context.Entry(task).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Projects/Details", new { id = TaskItem.ProjectId });
        }
    }
}
