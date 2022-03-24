using Labb2API.Backend.DAL.Enums;
using Labb2API.Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labb2API.Pages;

public class CreateCourseModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateCourseModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    //public void OnGet()
    //{
    //}

    public async Task OnPostAsync()
    {
        var body = JsonContent.Create(new
        {
            Title = Request.Form["courseTitle"].ToString(),
            Description = Request.Form["courseDescription"].ToString(),
            Duration = Request.Form["courseDuration"].ToString(),
            Difficulty = int.Parse(Request.Form["courseDifficulty"].ToString()),
            Status = int.Parse(Request.Form["courseStatus"].ToString())
        });
        var client = _httpClientFactory.CreateClient("api");

        var request = new HttpRequestMessage(HttpMethod.Post, "controller/courses");
        request.Content = body;
        var response = await client.SendAsync(request);
    }
}