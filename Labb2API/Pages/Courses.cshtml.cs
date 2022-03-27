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
    public Course Course { get; set; }

    [BindProperty]
    public int CourseId { get; set; }

    [BindProperty]
    public bool CourseStatus { get; set; }

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

    //GETS a course
    public async Task OnPostOneAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Get, $"controller/courses/{CourseId}");
        var response = await client.SendAsync(request);

        var content = await response.Content.ReadAsStreamAsync();
        try
        {
            Course = await JsonSerializer.DeserializeAsync<Course>(content);
        }
        catch (Exception e)
        {
            Course = new Course();
            Console.WriteLine(e);
        }

        if (Course is not null && Course.Id != 0)
        {
            Courses.Add(Course);
            Page();
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Delete, $"controller/courses/{CourseId}");
        var response = await client.SendAsync(request);
        
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostChangeStatusAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Patch, $"controller/courses/status/{CourseId}?status={CourseStatus}");
        var response = await client.SendAsync(request);

        return RedirectToPage();
    }
}