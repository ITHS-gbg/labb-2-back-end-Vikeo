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


    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Get, "controller/courses");
        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStreamAsync();
        Courses = await JsonSerializer.DeserializeAsync<List<Course>>(content);

    }
}