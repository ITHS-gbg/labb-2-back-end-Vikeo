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

        [BindProperty]
        public List<Course> Courses { get; set; }
        [BindProperty]
        public int SelectedCourseId { get; set; }

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

            client = _httpClientFactory.CreateClient("api");
            request = new HttpRequestMessage(HttpMethod.Get, "controller/courses");
            response = await client.SendAsync(request);

            content = await response.Content.ReadAsStreamAsync();
            try
            {
                Courses = await JsonSerializer.DeserializeAsync<List<Course>>(content);
            }
            catch (Exception e)
            {
                Courses = new List<Course>();
                Console.WriteLine(e);
            }
        }

        public async Task<IActionResult> OnPostUserAsync()
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

        public async Task<IActionResult> OnPostCourseAsync()
        {
            var client = _httpClientFactory.CreateClient("api");
            var request = new HttpRequestMessage(HttpMethod.Post, $"controller/users/{SelectedUser.Email}/{SelectedCourseId}");
            var response = await client.SendAsync(request);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var email = Request.Form["deleteCourseEmail"].ToString();
            var id = Request.Form["deleteCourseId"].ToString();

            var client = _httpClientFactory.CreateClient("api");
            var request = new HttpRequestMessage(HttpMethod.Delete, $"controller/users/{Request.Form["deleteCourseEmail"].ToString()}/{Request.Form["deleteCourseId"].ToString()}");
            var response = await client.SendAsync(request);

            return RedirectToPage();
        }
    }
}
