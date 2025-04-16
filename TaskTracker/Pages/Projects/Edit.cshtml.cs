using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = new Project();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _context.Projects.FindAsync(id);
            if (Project == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Avoid tracking related entities
            Project.Tasks = null;

            _context.Attach(Project);
            _context.Entry(Project).Property(p => p.Name).IsModified = true;
            _context.Entry(Project).Property(p => p.Description).IsModified = true;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
