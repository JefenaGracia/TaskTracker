using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class TestPostModel : PageModel
{
    [BindProperty]
    public string Message { get; set; } = "";

    public void OnGet()
    {
        Console.WriteLine(" GET TestPost loaded");
    }

    public void OnPost()
    {
        Console.WriteLine($"POST triggered — Message = {Message}");
    }
}
