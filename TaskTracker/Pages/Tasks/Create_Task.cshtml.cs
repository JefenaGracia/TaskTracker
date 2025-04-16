using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Pages.Tasks
{
    public class Create_TaskModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Create_TaskModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int ProjectId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = "";

        [BindProperty]
        public string Status { get; set; } = "Pending";

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; } = DateTime.Today;

        [BindProperty]
        public double? Latitude { get; set; }

        [BindProperty]
        public double? Longitude { get; set; }

        [BindProperty]
        public string? Address { get; set; }


        public void OnGet()
        {
            Console.WriteLine($"[DEBUG] Create_Task OnGet | ProjectId = {ProjectId}");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("[DEBUG] ModelState invalid. Returning page.");
                return Page();
            }

            var task = new TaskItem
            {
                Title = Title,
                Status = Status,
                DueDate = DueDate,
                ProjectId = ProjectId,
                Latitude = Latitude,
                Longitude = Longitude,
                Address = Address
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            Console.WriteLine("[DEBUG] Task saved ✅");
            return RedirectToPage("/Projects/Details", new { id = ProjectId });
        }
    }
}
