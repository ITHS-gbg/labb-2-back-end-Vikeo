using Labb2API.Backend.DAL.Models;

namespace Labb2API.Backend.DAL.Respositories.Interfaces;

public interface ICourseRepository
{
    //TODO Maybe make async
    bool CreateCourse(Course course);
    List<Course> GetAllCourses();
    Course? GetCourse(int id);
    bool UpdateCourse(int id, Course course);
    bool UpdateCourseStatus(Course course, int status);
    bool UpdateCourseDifficulty(Course course, int difficulty);
    bool DeleteCourse(int id);
}