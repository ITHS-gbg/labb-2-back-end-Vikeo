using Labb2API.Backend.DAL.Enums;
using Labb2API.Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labb2API.Pages;

public class CreateUserModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateUserModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    //public void OnGet()
    //{
    //}
    [BindProperty]
    public new User User { get; set; }

    public async Task OnPostAsync()
    {
        var body = JsonContent.Create(new
        {
            User.FirstName,
            User.LastName,
            User.Email,
            User.Phone,
            User.Address,
            ActiveCourses = new List<Course>(),
            
        });
        var client = _httpClientFactory.CreateClient("api");

        var request = new HttpRequestMessage(HttpMethod.Post, "controller/users");
        request.Content = body;
        var response = await client.SendAsync(request);
    }
}