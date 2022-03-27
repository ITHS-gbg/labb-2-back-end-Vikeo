using Labb2API.Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Labb2API.Pages
{
    public class EditCourseModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditCourseModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public List<Course> Courses { get; set; }
        [BindProperty]
        public Course SelectedCourse { get; set; }

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
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var body = JsonContent.Create(new
            {
                SelectedCourse.Title,
                SelectedCourse.Description,
                SelectedCourse.Duration,
                SelectedCourse.Difficulty,
                SelectedCourse.Status,
            });
            var client = _httpClientFactory.CreateClient("api");
            var request = new HttpRequestMessage(HttpMethod.Put, $"controller/courses/{SelectedCourse.Id}");
            request.Content = body;
            var response = await client.SendAsync(request);

            return RedirectToPage();
        }
    }
}
