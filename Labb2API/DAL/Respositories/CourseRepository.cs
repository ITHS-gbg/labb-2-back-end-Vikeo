using Labb2API.DAL.Contexts;
using Labb2API.DAL.Enums;
using Labb2API.DAL.Models;
using Labb2API.DAL.Respositories.Interfaces;

namespace Labb2API.DAL.Respositories;

public class CourseRepository : ICourseRepository
{
    private readonly WebsiteContext _websiteContext;

    public CourseRepository(WebsiteContext websiteContext)
    {
        _websiteContext = websiteContext;
    }

    public bool CreateCourse(Course course)
    {
        //TODO Kollar hela kursen, kanske borde kolla vissa delar bara.

        if (_websiteContext.Courses.Contains(course)) return false;

        _websiteContext.Courses.Add(course);
        return true;
    }

    public List<Course> GetAllCourses()
    {
        return _websiteContext.Courses.ToList();
    }

    public Course? GetCourse(int id)
    {
        var existingCourse = _websiteContext.Courses.FirstOrDefault(c => c.Id == id);
        if (existingCourse == null) return null;
        if (existingCourse.Id != id) return null;

        return existingCourse;
    }

    public bool UpdateCourse(int id, Course course)
    {
        var existingCourse = _websiteContext.Courses.FirstOrDefault(c => c.Id == id);

        if (existingCourse.Id != id) return false;

        if (existingCourse.Id != course.Id) return false;

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
    public bool UpdateCourseStatus(Course course, int status)
    {
        if (!_websiteContext.Courses.Contains(course)) return false;

        if (status > Enum.GetValues(typeof(CourseStatus)).Cast<int>().Max() ||
            status < Enum.GetValues(typeof(CourseStatus)).Cast<int>().Min()) return false;

        course.Status = (CourseStatus)status;

        return true;
    }

    public bool UpdateCourseDifficulty(Course course, int difficulty)
    {
        if (!_websiteContext.Courses.Contains(course)) return false;

        if (difficulty > Enum.GetValues(typeof(CourseDifficulty)).Cast<int>().Max() ||
            difficulty < Enum.GetValues(typeof(CourseDifficulty)).Cast<int>().Min()) return false;

        course.Difficulty = (CourseDifficulty)difficulty;

        return true;
    }

    public bool DeleteCourse(int id)
    {
        var existingCourse = _websiteContext.Courses.FirstOrDefault(c => c.Id == id);

        if (existingCourse.Id != id) return false;

        _websiteContext.Courses.Remove(existingCourse);
        return true;
    }
}
