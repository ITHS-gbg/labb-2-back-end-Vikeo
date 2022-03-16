using Labb2API.DAL.Storages;
using Labb2API.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb2API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserStorage _userStorage;

        public UserController([FromServices] UserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        [HttpGet]
        public IResult GetUsers()
        {
            var users = _userStorage.GetAllUsers();

            if (users.Count <= 0)
            {
                return Results.NotFound();
            }

            return Results.Conflict(users);
        }

        //[HttpGet("{id}")]

    }
            
            
            
    //app.MapGet("/users", ([FromServices] UserStorage userStorage) =>
    //{                                         
    //    var users = userStorage.GetAllUsers();
                                                
    //    if (users.Count <= 0)
    //    {                    
    //        return Results.NotFound();
    //    }                             
                                        
    //    return Results.Ok(users);
    //});
}

