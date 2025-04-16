using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(TaskItem);

            _context.Entry(TaskItem).Property(t => t.Title).IsModified = true;
            _context.Entry(TaskItem).Property(t => t.Status).IsModified = true;
            _context.Entry(TaskItem).Property(t => t.DueDate).IsModified = true;

            _context.Entry(TaskItem).Property(t => t.Latitude).IsModified = true;
            _context.Entry(TaskItem).Property(t => t.Longitude).IsModified = true;
            _context.Entry(TaskItem).Property(t => t.Address).IsModified = true;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Projects/Details", new { id = TaskItem.ProjectId });
        }
    }
}
