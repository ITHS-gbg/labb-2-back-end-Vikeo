using Labb2API.Backend.DAL;
using Labb2API.Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labb2API.Backend.Controllers;

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
    public async Task<IActionResult> GetAllCoursesAsync()
    {
        var courses = await _unitOfWork.CourseRepository.GetAllCoursesAsync();

        if (courses.Count <= 0)
        {
            return NotFound();
        }

        return Ok(courses);
    }

    //GET, hämta en course.
    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetOneCourseAsync(int courseId)
    {
        var course = await _unitOfWork.CourseRepository.GetCourseAsync(courseId);

        if (course is null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    //POST, skapa en course.
    [HttpPost]
    public async Task<IActionResult> PostOneCourseAsync([FromBody] Course course)
    {
        //TODO Kan jag verkligen kolla ID:t här?
        if (course.Id < 0)
        {
            return BadRequest();
        }

        if (await _unitOfWork.CourseRepository.CreateCourseAsync(course))
        {
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        return Conflict();

    }

    [HttpPut("{courseId}")]
    public async Task<IActionResult> PutCourseAsync([FromBody] Course course, int courseId)
    {
        //TODO Kan jag verkligen kolla ID här, spelar det ens någon roll?
        if (course.Id < 0)
        {
            return BadRequest();
        }

        if (await _unitOfWork.CourseRepository.UpdateCourseAsync(courseId, course))
        {
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        return Conflict("Det finns redan an kurs med samma ID som det nya.");
    }

    //PATCH, uppdatera status på en course
    [HttpPatch("status/{courseId}")]
    public async Task<IActionResult> PatchCourseStatusAsync(int courseId, bool status)
    {
        var course = await _unitOfWork.CourseRepository.GetCourseAsync(courseId);
        if (course is null)
        {
            return NotFound();
        }

        if (await _unitOfWork.CourseRepository.UpdateCourseStatusAsync(course, status))
        {
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        return BadRequest();
    }

    //PATCH, uppdatera difficulty på en course
    [HttpPatch("difficulty/{courseId}")]
    public async Task<IActionResult> PatchCourseDifficultyAsync(int courseId, int difficulty)
    {
        var course = await _unitOfWork.CourseRepository.GetCourseAsync(courseId);
        if (course is null)
        {
            return NotFound();
        }

        if (await _unitOfWork.CourseRepository.UpdateCourseDifficultyAsync(course, difficulty))
        {
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        return BadRequest();
    }

    //DELETE, ta bort en kurs från courses.
    [HttpDelete("{courseId}")]
    public async Task<IActionResult> DeleteCourseAsync(int courseId)
    {
        if (await _unitOfWork.CourseRepository.DeleteCourseAsync(courseId))
        {
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        return NotFound();
    }
}