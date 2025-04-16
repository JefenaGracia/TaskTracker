using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTracker.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Project> Projects { get; set; } = new List<Project>();

        public async Task OnGetAsync()
        {
            Projects = await _context.Projects
                        .Include(p => p.Tasks)
                        .ToListAsync();
        }
    }
}
