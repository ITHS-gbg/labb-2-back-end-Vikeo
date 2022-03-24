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

    public UsersModel(ILogger<UsersModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public void OnGet()
    {
        var client = _httpClientFactory.CreateClient("api");
        var request = new HttpRequestMessage(HttpMethod.Get, "controller/users");
        var response = client.Send(request);

        var content = response.Content.ReadAsStream();
        Users = JsonSerializer.Deserialize<List<User>>(content);
    }
}