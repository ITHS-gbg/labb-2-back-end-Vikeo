using Labb2API.DAL.Models;

namespace Labb2API.DAL.Respositories.Interfaces;

public interface ICourseRepository : IDisposable
{
    bool CreateCourse(Course course);
    List<Course> GetAllCourses();
    Course? GetCourse(int id);
    bool UpdateCourse(int id, Course course);
    bool UpdateCourseStatus(Course course, int status);
    bool UpdateCourseDifficulty(Course course, int difficulty);
    bool DeleteCourse(int id);
}