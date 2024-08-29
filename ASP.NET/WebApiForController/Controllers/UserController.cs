using Microsoft.AspNetCore.Mvc;
using WebApiForController.DTOs;
using WebApiForController.Models;
using WebApiForController.Services;

namespace WebApiForController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userServices;

        public UserController(ILogger<UserController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }

        [HttpPost("GetUser")]
        public IActionResult GetUser([FromBody]GetUserRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = _userServices.GetUser(request);
            return Ok(result);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var result = _userServices.GetAllUsers();
            return Ok(result);
        }

        [HttpPost("InserUsers")]
        public IActionResult InserUsers(InsertUserRequest request)
        {
            var result = _userServices.InserUsers(request);
            return Ok(result);
        }
    }
}
