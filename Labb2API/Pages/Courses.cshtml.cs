using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labb2API.Pages;

public class CoursesModel : PageModel
{

    private readonly ILogger<CoursesModel> _logger;

    public CoursesModel(ILogger<CoursesModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}