using Labb2API.DAL.Contexts;
using Labb2API.DAL.Models;
using Labb2API.DAL.Respositories.Interfaces;

namespace Labb2API.DAL.Respositories
{
    public record UserCourseRepository : IUserCourseRepository
    {
        public readonly WebsiteContext _websiteContext;

        public UserCourseRepository(WebsiteContext websiteContext)
        {
            _websiteContext = websiteContext;
        }

        public void Create(UserCourse userCourse)
        {
            _websiteContext.UserCourses.Add(userCourse);
        }

        public ICollection<UserCourse> GetAllUserCourses()
        {
            return _websiteContext.UserCourses.ToList();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
