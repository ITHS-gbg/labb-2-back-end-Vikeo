using Labb2API.DAL.Enums;
using Labb2API.DAL.Models;

namespace Labb2API.DAL.Storages
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
            //TODO Kolla value eller key?
            if (_courses.Values.Contains(course)) return false;

            //TODO Fråga Niklas om det är för fult?
            if (_id == 0)
            {
                _id = 0;
                course.Id = _id;
                _courses.Add(_id++, course);
                return true;
            }
            _id = _courses.Keys.Max() + 1;
            course.Id = _id;


            _courses.Add(_id++, course);
            return true;
        }

        public ICollection<Course> GetAllCourses()
        {
            return _courses.Values;
        }

        public Course? GetCourse(int id)
        {
            if (!_courses.Keys.Contains(id)) return null;

            return _courses[id];
        }

        public bool UpdateCourse(int id, Course course)
        {
            if (!_courses.Keys.Contains(id)) return false;
            course.Id = id;
            _courses[id] = course;
            return true;
        }

        public bool UpdateCourseStatus(Course course, int status)
        {
            if (status > Enum.GetValues(typeof(CourseStatus)).Cast<int>().Max() ||
                status < Enum.GetValues(typeof(CourseStatus)).Cast<int>().Min()) return false;

            course.Status = (CourseStatus)status;

            return true;
        }

        //TODO Ska jag ta in courseId eller Course????
        public bool UpdateCourseDifficulty(Course course, int difficulty)
        {
            if (!_courses.Values.Contains(course)) return false;

            if (difficulty > Enum.GetValues(typeof(CourseDifficulty)).Cast<int>().Max() ||
                difficulty < Enum.GetValues(typeof(CourseDifficulty)).Cast<int>().Min()) return false;

            course.Difficulty = (CourseDifficulty)difficulty;

            return true;
        }

        public bool DeleteCourse(int id)
        {
            if (!_courses.Keys.Contains(id)) return false;

            _courses.Remove(id);
            return true;
        }
    }
}