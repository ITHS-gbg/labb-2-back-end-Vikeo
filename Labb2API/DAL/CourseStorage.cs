using Labb2API.DAL.Enums;
using Labb2API.DAL.Models;

namespace Labb2API.DAL
{
    public class CourseStorage
    {
        private readonly IDictionary<int, Course> _courses;

        private int _id;

        public CourseStorage()
        {
            _courses = new Dictionary<int, Course>();
        }

        public bool CreateCourse(Course course)
        {
            if (_courses.Values.Contains(course))
            {
                return false;
            }
            _courses.Add(_id++, course);
            return true;
        }

        public ICollection<Course>? GetAllCourses()
        {
            return _courses.Values;
        }

        public Course GetCourse(int id)
        {
            if (!_courses.Keys.Contains(id))
            {
                return null;
            }

            return _courses[id];
        }

        public bool UpdateCourse(int id, Course course)
        {
            if (!_courses.Keys.Contains(id))
            {
                return false;
            }

            _courses[id] = course;
            return true;
        }

        public bool UpdateCourseStatus(int id, int status)
        {
            if (status > Enum.GetValues(typeof(CourseStatus)).Cast<int>().Max() ||
                status < Enum.GetValues(typeof(CourseStatus)).Cast<int>().Min())
            {
                return false;
            }

            _courses[id].Status = (CourseStatus)status;

            return true;
        }

        public bool UpdateCourseDifficulty(int id, int difficulty)
        {
            if (!_courses.Keys.Contains(id))
            {
                return false;
            }

            if (difficulty > Enum.GetValues(typeof(CourseDifficulty)).Cast<int>().Max() ||
                difficulty < Enum.GetValues(typeof(CourseDifficulty)).Cast<int>().Min())
            {
                return false;
            }

            _courses[id].Difficulty = (CourseDifficulty)difficulty;

            return true;
        }

        public bool DeleteUser(int id)
        {
            if (!_courses.Keys.Contains(id))
            {
                return false;
            }

            _courses.Remove(id);
            return true;
        }
    }
}