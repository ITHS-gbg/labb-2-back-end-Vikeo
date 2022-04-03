using Labb2API.Backend.DAL.Contexts;
using Labb2API.Backend.DAL.Enums;
using Labb2API.Backend.DAL.Models;
using Labb2API.Backend.DAL.Respositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2API.Backend.DAL.Respositories;

public class CourseRepository : ICourseRepository
{
    private readonly WebsiteContext _websiteContext;

    public CourseRepository(WebsiteContext websiteContext)
    {
        _websiteContext = websiteContext;
    }

    public async Task<bool> CreateCourseAsync(Course course)
    {
        //TODO Kollar hela kursen, kanske borde kolla vissa delar bara.

        if (await _websiteContext.Courses.ContainsAsync(course)) return false;

        await _websiteContext.Courses.AddAsync(course);
        return true;
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        return await _websiteContext.Courses.ToListAsync();
    }

    public async Task<Course?> GetCourseAsync(int id)
    {
        var existingCourse = await _websiteContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (existingCourse == null) return null;
        if (existingCourse.Id != id) return null;

        return existingCourse;
    }

    public async Task<bool> UpdateCourseAsync(int id, Course course)
    {
        var existingCourse = await _websiteContext.Courses.FirstOrDefaultAsync(c => c.Id == id);

        if (existingCourse.Id != id) return false;

        course.Id = id;

        existingCourse.Id = course.Id;
        existingCourse.Title = course.Title;
        existingCourse.Description = course.Description;
        existingCourse.Duration = course.Duration;
        existingCourse.Difficulty = course.Difficulty;
        existingCourse.Status = course.Status;

        return true;
    }

    //TODO Ska jag ta in courseId eller Course????
    public async Task<bool> UpdateCourseStatusAsync(Course course, bool status)
    {
        if (!await _websiteContext.Courses.ContainsAsync(course)) return false;

        if (status == course.Status)
        {
            return false;
        }

        course.Status = status;

        return true;
    }

    public async Task<bool> UpdateCourseDifficultyAsync(Course course, int difficulty)
    {
        if (!await _websiteContext.Courses.ContainsAsync(course)) return false;

        if (difficulty > Enum.GetValues(typeof(CourseDifficulty)).Cast<int>().Max() ||
            difficulty < Enum.GetValues(typeof(CourseDifficulty)).Cast<int>().Min()) return false;

        course.Difficulty = (CourseDifficulty)difficulty;

        return true;
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var existingCourse = await _websiteContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (existingCourse == null) return false;
        if (existingCourse.Id != id) return false;

        _websiteContext.Courses.Remove(existingCourse);
        return true;
    }
}
