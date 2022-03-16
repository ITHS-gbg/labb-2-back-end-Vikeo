using Labb2API.DAL.Storages;
using Labb2API.DAL;
using Labb2API.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb2API.Controllers
{
    [Route("controller/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserStorage _userStorage;
        private readonly CourseStorage _courseStorage;


        public UserController([FromServices] UserStorage userStorage, [FromServices] CourseStorage courseStorage)
        {
            _userStorage = userStorage;
            _courseStorage = courseStorage;
        }

        #region HTTP-methods

        //GET, hämta alla users
        [HttpGet]
        public IResult GetUsers()
        {
            var users = _userStorage.GetAllUsers();

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
            var user = _userStorage.GetUser(email);

            if (user is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user);
        }

        //POST, Skapa en user.
        [HttpPost]
        public IResult PostUser([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Email))
            {
                return Results.BadRequest();
            }

            if (_userStorage.CreateUser(user))
            {
                return Results.Ok();
            }
            return Results.Conflict();
        }

        //PUT, ändra en users alla properites.
        [HttpPut("{email}")]
        public IResult PutUser([FromBody] User user, string email)
        {
            if (user.Email != email)
            {
                return Results.BadRequest("Mailen i HTTP-requesten måste vara samma som mailen i body.");
            }

            return _userStorage.UpdateUser(user) ? Results.Ok() : Results.NotFound();
        }

        //POST, lägger till kurs till ActiveCourses
        [HttpPost("{email}, {courseId}")]
        public IResult PostActiveCourse(string email, int courseId){
            var course = _courseStorage.GetCourse(courseId);
            var user = _userStorage.GetUser(email);

            if (course is null || user is null)
            {
                return Results.NotFound();
            }

            return _userStorage.AddActiveCourse(user, course) ? Results.Ok() : Results.Conflict();
        }

        //DELETE, ta bort kurs från activecourse.
        [HttpDelete("{email}, {courseId}")]
        public IResult DeleteActiveCourse(string email, int courseId)
        {
            var course = _courseStorage.GetCourse(courseId);
            var user = _userStorage.GetUser(email);

            if (course is null || user is null)
            {
                return Results.NotFound();
            }

            return _userStorage.RemoveActiveCourse(user, course) ? Results.Ok() : Results.NotFound();
        }

        //DELETE, ta bort en användare.
        [HttpDelete("{email}")]
        public IResult DeleteUser(string email)
        {
            var user = _userStorage.GetUser(email);

            if (user is null)
            {
                return Results.NotFound(user);
            }

            _userStorage.RemoveUser(user);
            return Results.Ok();
        }

        #endregion
    }
}