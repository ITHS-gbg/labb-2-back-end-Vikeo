using Labb2API.Backend.DAL;
using Labb2API.Backend.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labb2API.Backend.Controllers;

[Route("controller/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;

    //TODO Behöver man [FromServices] här?
    public UserController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region HTTP-methods

    //GET, hämta alla users
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAllUsersAsync();

        if (users.Count <= 0)
        {
            return NotFound("cont");
        }

        return Ok(users);
    }

    //GET, hämta en user
    [HttpGet("{email}")]
    public async Task<IActionResult> GetOneUser(string email)
    {
        var user = await _unitOfWork.UserRepository.GetUserAsync(email);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    //POST, Skapa en user.
    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] User user)
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            return BadRequest();
        }

        if (await _unitOfWork.UserRepository.CreateUserAsync(user))
        {
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        return Conflict();
    }

    //PUT, ändra en users alla properites.
    [HttpPut("{email}")]
    public async Task<IActionResult> PutUser([FromBody] User user, string email)
    {
        //TODO Tror att jag kolla om mailen stämmer eller inte 3 gånger.
        if (user.Email != email)
        {
            return BadRequest("Mailen i HTTP-requesten måste vara samma som mailen i body.");
        }

        if (await _unitOfWork.UserRepository.UpdateUserAsync(user))
        {
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        return NotFound();
    }

    //POST, lägger till kurs till ActiveCourses
    [HttpPost("{email}/{courseId}")]
    public async Task<IActionResult> PostActiveCourse(string email, int courseId)
    {
        var course = await _unitOfWork.CourseRepository.GetCourseAsync(courseId);
        var user = await _unitOfWork.UserRepository.GetUserAsync(email);

        if (course is null || user is null)
        {
            return NotFound();
        }

        if (await _unitOfWork.UserRepository.AddActiveCourseAsync(user, course))
        {
            bool canSave = await _unitOfWork.SaveAsync();

            if (!canSave)
            {
                return Conflict("You can't be registered to one specific course multiple times");
            }

            return Ok();
        }

        return Conflict();
    }

    //DELETE, ta bort kurs från activecourse.
    [HttpDelete("{email}/{courseId}")]
    public async Task<IActionResult> DeleteActiveCourse(string email, int courseId)
    {
        var course = await _unitOfWork.CourseRepository.GetCourseAsync(courseId);
        var user = await _unitOfWork.UserRepository.GetUserAsync(email);

        if (course is null || user is null)
        {
            return NotFound();
        }

        if (await _unitOfWork.UserRepository.RemoveActiveCourseAsync(user, course))
        {
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        return NotFound();
    }

    //DELETE, ta bort en användare.
    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUser(string email)
    {
        var user = await _unitOfWork.UserRepository.GetUserAsync(email);

        if (user is null)
        {
            return NotFound(user);
        }

        await _unitOfWork.UserRepository.RemoveUserAsync(user);
        await _unitOfWork.SaveAsync();
        return Ok();
    }

    #endregion
}
