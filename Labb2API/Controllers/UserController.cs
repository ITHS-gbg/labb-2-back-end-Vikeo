using Labb2API.DAL;
using Labb2API.DAL.Models;
using Labb2API.DAL.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb2API.Controllers
{
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
        public IResult GetUsers()
        {
            var users = _unitOfWork.UserRepository.GetAllUsers();
            
            if (users.Count <= 0)
            {
                return Results.NotFound("cont");
            }

            return Results.Ok(users);
        }

        //GET, hämta en user
        [HttpGet("{email}")]
        public IResult GetOneUser(string email)
        {
            var user = _unitOfWork.UserRepository.GetUser(email);

            if (user is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user);
        }

        //POST, Skapa en user.
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest();
            }

            if (_unitOfWork.UserRepository.CreateUser(user))
            {
                _unitOfWork.Save();
                return Ok();
            }
            return Conflict();
        }

        //PUT, ändra en users alla properites.
        [HttpPut("{email}")]
        public IResult PutUser([FromBody] User user, string email)
        {
            //TODO Tror att jag kolla om mailen stämmer eller inte 3 gånger.
            if (user.Email != email)
            {
                return Results.BadRequest("Mailen i HTTP-requesten måste vara samma som mailen i body.");
            }

            if (_unitOfWork.UserRepository.UpdateUser(user))
            {
                _unitOfWork.Save();
                return Results.Ok();
            }
            return Results.NotFound();
        }

        //POST, lägger till kurs till ActiveCourses
        [HttpPost("{email}/{courseId}")]
        public IResult PostActiveCourse(string email, int courseId)
        {
            var course = _unitOfWork.CourseRepository.GetCourse(courseId);
            var user = _unitOfWork.UserRepository.GetUser(email);

            if (course is null || user is null)
            {
                return Results.NotFound();
            }

            if (_unitOfWork.UserRepository.AddActiveCourse(user, course))
            {
                bool canSave = _unitOfWork.Save();

                if (!canSave)
                {
                    return Results.Conflict("You can't be registered to one specific course multiple times");
                }
                
                return Results.Ok();
            }

            return Results.Conflict();
        }

        //DELETE, ta bort kurs från activecourse.
        [HttpDelete("{email}/{courseId}")]
        public IResult DeleteActiveCourse(string email, int courseId)
        {
            var course = _unitOfWork.CourseRepository.GetCourse(courseId);
            var user = _unitOfWork.UserRepository.GetUser(email);

            if (course is null || user is null)
            {
                return Results.NotFound();
            }

            if (_unitOfWork.UserRepository.RemoveActiveCourse(user, course))
            {
                _unitOfWork.Save();
                return Results.Ok();
            }
            return Results.NotFound();
        }

        //DELETE, ta bort en användare.
        [HttpDelete("{email}")]
        public IResult DeleteUser(string email)
        {
            var user = _unitOfWork.UserRepository.GetUser(email);

            if (user is null)
            {
                return Results.NotFound(user);
            }

            _unitOfWork.UserRepository.RemoveUser(user);
            _unitOfWork.Save();
            return Results.Ok();
        }

        #endregion
    }
}