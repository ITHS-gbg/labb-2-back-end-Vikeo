using System.Text.Json;
using Labb2API.Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Labb2API.Pages
{
    public class EditUserModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditUserModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public List<User> Users { get; set; }
        [BindProperty]
        public User SelectedUser { get; set; }

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
        public async Task<IActionResult> OnPostAsync()
        {
            var body = JsonContent.Create(new
            {
                SelectedUser.FirstName,
                SelectedUser.LastName,
                SelectedUser.Email,
                SelectedUser.Phone,
                SelectedUser.Address,
                SelectedUser.ActiveCourses,

            });
            var client = _httpClientFactory.CreateClient("api");
            var request = new HttpRequestMessage(HttpMethod.Put, $"controller/users/{SelectedUser.Email}");
            request.Content = body;
            var response = await client.SendAsync(request);

            return RedirectToPage();
        }
    }
}
