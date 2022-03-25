using Labb2API.Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Labb2API.Pages;

public class CoursesModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CoursesModel> _logger;

    public CoursesModel(ILogger<CoursesModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    [BindProperty]
    public List<Course> Courses { get; set; }

    [BindProperty]
    public int CourseId { get; set; }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Get, "controller/courses");
        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();
        try
        {
            Courses = await JsonSerializer.DeserializeAsync<List<Course>>(content);
        }
        catch (Exception e)
        {
            Courses = new List<Course>();
            Console.WriteLine(e);
            //throw;
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Delete, $"controller/courses/{CourseId}");
        var response = await client.SendAsync(request);
        
        return RedirectToPage();
    }
}