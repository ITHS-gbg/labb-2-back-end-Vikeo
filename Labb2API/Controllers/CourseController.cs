using Labb2API.DAL.Enums;
using Labb2API.DAL.Models;
using Labb2API.DAL.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb2API.Controllers
{
    [Route("controller/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseStorage _courseStorage;

        public CourseController([FromServices] CourseStorage courseStorage)
        {
            _courseStorage = courseStorage;
        }

        //GET, hämta alla courses.
        [HttpGet]
        public IResult GetAllCourses()
        {
            var courses = _courseStorage.GetAllCourses();

            if (courses.Count <= 0)
            {
                return Results.NotFound();
            }

            return Results.Ok(courses);
        }

        //GET, hämta en course.
        [HttpGet("{courseId}")]
        public IResult GetOneCourse(int courseId)
        {
            var course = _courseStorage.GetCourse(courseId);

            if (course is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(course);
        }

        //POST, skapa en course.
        [HttpPost]
        public IResult PostOneCourse([FromBody] Course course)
        {
            //TODO Kan jag verkligen kolla ID:t här?
            if (course.Id < 0)
            {
                return Results.BadRequest();
            }

            if (_courseStorage.CreateCourse(course))
            {
                return Results.Ok();
            }

            return Results.Conflict();
        }

        [HttpGet("{courseId}")]
        public IResult PutCourse([FromBody] Course course, int courseId)
        {
            //TODO Kan jag verkligen kolla ID här, spelar det ens någon roll?
            if (course.Id < 0)
            {
                return Results.BadRequest();
            }

            if (_courseStorage.UpdateCourse(courseId, course))
            {
                return Results.Ok();
            }

            return Results.Conflict("Det finns redan an kurs med samma ID som det nya.");
        }


        //PATCH, uppdatera status på en course
        [HttpPatch("{courseId}")]
        public IResult PatchCourseStatus(int courseId, int status)
        {
            var course = _courseStorage.GetCourse(courseId);
            if (course is null)
            {
                return Results.NotFound();
            }

            return _courseStorage.UpdateCourseStatus(course, status) ? Results.Ok() : Results.BadRequest();
        }



        //TODO PATCH, uppdatera difficulty på en course
        [HttpPatch("{courseId}")]
        public IResult PatchCourseDifficulty(int courseId, int difficulty)
        {
            var course = _courseStorage.GetCourse(courseId);
            if (course is null)
            {
                return Results.NotFound();
            }

            return _courseStorage.UpdateCourseDifficulty(course, difficulty) ? Results.Ok() : Results.BadRequest();
        }


        //DELETE, ta bort en kurs från courses.
        [HttpDelete("{courseId}")]
        public IResult DeleteCourse(int courseId)
        {
            return _courseStorage.DeleteCourse(courseId) ? Results.Ok() : Results.NotFound();
        }
    }
}