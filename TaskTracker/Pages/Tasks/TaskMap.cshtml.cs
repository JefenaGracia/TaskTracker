using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Pages.Tasks
{
    public class TaskMapModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public TaskMapModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TaskLocationViewModel> TaskLocations { get; set; } = new();

        public async Task OnGetAsync()
        {
            TaskLocations = await _context.TaskItems
                .Where(t => t.Latitude != null && t.Longitude != null)
                .Select(t => new TaskLocationViewModel
                {
                    Title = t.Title,
                    Lat = t.Latitude.Value,
                    Lng = t.Longitude.Value,

                })
                .ToListAsync();
        }
    }

    public class TaskLocationViewModel
    {
        public string Title { get; set; } = "";
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string? Address { get; set; }

    }
}
