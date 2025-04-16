using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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
            var project = await _context.Projects.FindAsync(Project.ProjectId);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
