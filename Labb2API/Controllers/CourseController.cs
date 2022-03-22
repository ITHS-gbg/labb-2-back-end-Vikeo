using Labb2API.DAL;
using Labb2API.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labb2API.Controllers;

[Route("controller/courses")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;

    //TODO Behöver man [FromServices] här?
    public CourseController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //GET, hämta alla courses.
    [HttpGet]
    public IActionResult GetAllCourses()
    {
        var courses = _unitOfWork.CourseRepository.GetAllCourses();

        if (courses.Count <= 0)
        {
            return NotFound();
        }

        return Ok(courses);
    }

    //GET, hämta en course.
    [HttpGet("{courseId}")]
    public IActionResult GetOneCourse(int courseId)
    {
        var course = _unitOfWork.CourseRepository.GetCourse(courseId);

        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    //POST, skapa en course.
    [HttpPost]
    public IActionResult PostOneCourse([FromBody] Course course)
    {
        //TODO Kan jag verkligen kolla ID:t här?
        if (course.Id < 0)
        {
            return BadRequest();
        }

        if (_unitOfWork.CourseRepository.CreateCourse(course))
        {
            _unitOfWork.Save();
            return Ok();
        }

        return Conflict();

    }

    [HttpPut("{courseId}")]
    public IActionResult PutCourse([FromBody] Course course, int courseId)
    {
        //TODO Kan jag verkligen kolla ID här, spelar det ens någon roll?
        if (course.Id < 0)
        {
            return BadRequest();
        }

        if (_unitOfWork.CourseRepository.UpdateCourse(courseId, course))
        {
            _unitOfWork.Save();
            return Ok();
        }

        return Conflict("Det finns redan an kurs med samma ID som det nya.");
    }

    //PATCH, uppdatera status på en course
    [HttpPatch("status/{courseId}")]
    public IActionResult PatchCourseStatus(int courseId, int status)
    {
        var course = _unitOfWork.CourseRepository.GetCourse(courseId);
        if (course is null)
        {
            return NotFound();
        }

        if (_unitOfWork.CourseRepository.UpdateCourseStatus(course, status))
        {
            _unitOfWork.Save();
            return Ok();
        }

        return BadRequest();
    }

    //PATCH, uppdatera difficulty på en course
    [HttpPatch("difficulty/{courseId}")]
    public IActionResult PatchCourseDifficulty(int courseId, int difficulty)
    {
        var course = _unitOfWork.CourseRepository.GetCourse(courseId);
        if (course is null)
        {
            return NotFound();
        }

        if (_unitOfWork.CourseRepository.UpdateCourseDifficulty(course, difficulty))
        {
            _unitOfWork.Save();
            return Ok();
        }

        return BadRequest();
    }


    //DELETE, ta bort en kurs från courses.
    [HttpDelete("{courseId}")]
    public IActionResult DeleteCourse(int courseId)
    {
        if (_unitOfWork.CourseRepository.DeleteCourse(courseId))
        {
            _unitOfWork.Save();
            return Ok();
        }
        return NotFound();
    }
}