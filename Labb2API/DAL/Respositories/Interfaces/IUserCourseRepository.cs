using Labb2API.DAL.Models;

namespace Labb2API.DAL.Respositories.Interfaces;

public interface IUserCourseRepository : IDisposable
{
    void Create(UserCourse userCourse);
    ICollection<UserCourse> GetAllUserCourses();
}