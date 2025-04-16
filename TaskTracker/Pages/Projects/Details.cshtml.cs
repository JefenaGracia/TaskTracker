using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Project Project { get; set; } = new Project();

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (Project == null)
                return NotFound();

            if (!string.IsNullOrEmpty(StatusFilter) && StatusFilter != "All")
            {
                Project.Tasks = Project.Tasks
                    ?.Where(t => t.Status == StatusFilter)
                    .ToList();
            }

            return Page();
        }

    }
}
