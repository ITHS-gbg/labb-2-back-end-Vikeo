using System.Text.RegularExpressions;
using Labb2API.DAL;
using Labb2API.DAL.Models;
using Labb2API.DAL.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb2API.Controllers
{
    [Route("controller/userCourses")]
    [ApiController]
    public class UserCourseController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        //TODO Behöver man [FromServices] här?
        public UserCourseController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region HTTP-methods
        [HttpGet]
        public IActionResult GetGroups()
        {
            return Ok(_unitOfWork.UserCourseRepository.GetAllUserCourses());
        }

        [HttpPost]
        public IActionResult SaveUserCourse([FromBody] UserCourse userCourse)
        {
            _unitOfWork.UserCourseRepository.Create(userCourse);
            _unitOfWork.Save();
            return Ok();
        }

        #endregion
    }
}