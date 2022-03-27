using System.Security.Principal;
using Labb2API.Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Labb2API.Pages;

public class UsersModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory; 
    private readonly ILogger<UsersModel> _logger;
    [BindProperty]
    public List<User> Users { get; set; }

    [BindProperty]
    public User User { get; set; }

    [BindProperty]
    public string UserEmail { get; set; }

    public UsersModel(ILogger<UsersModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Get, "controller/users");
        var response = await client.SendAsync(request);

        var content = await response.Content.ReadAsStreamAsync();
        try
        {
            Users = await JsonSerializer.DeserializeAsync<List<User>>(content);
        }
        catch (Exception e)
        {
            Users = new List<User>();
            Console.WriteLine(e);
        }
    }
    //GETS a user
    public async Task OnPostOneAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Get, $"controller/users/{UserEmail}");
        var response = await client.SendAsync(request);

        var content = await response.Content.ReadAsStreamAsync();
        try
        {
            User = await JsonSerializer.DeserializeAsync<User>(content);
        }
        catch (Exception e)
        {
            User = new User();
            Console.WriteLine(e);
        }

        if (User is not null && User.Id != 0)
        {
            Users.Add(User);
            Page();
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Delete, $"controller/users/{UserEmail}");
        var response = await client.SendAsync(request);
        
        return RedirectToPage();
    }
}