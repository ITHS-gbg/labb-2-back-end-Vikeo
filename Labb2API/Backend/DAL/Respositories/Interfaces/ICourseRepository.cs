using Labb2API.Backend.DAL.Models;

namespace Labb2API.Backend.DAL.Respositories.Interfaces;

public interface ICourseRepository
{
    //TODO Maybe make async
    Task<bool> CreateCourseAsync(Course course);
    Task<List<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseAsync(int id);
    Task<bool> UpdateCourseAsync(int id, Course course);
    Task<bool> UpdateCourseStatusAsync(Course course, bool status);
    Task<bool> UpdateCourseDifficultyAsync(Course course, int difficulty);
    Task<bool> DeleteCourseAsync(int id);
}