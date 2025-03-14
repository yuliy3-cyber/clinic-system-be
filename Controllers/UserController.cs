using clinic_system_be.DTOs.User;
using clinic_system_be.Models;
using clinic_system_be.Services;
using Microsoft.AspNetCore.Mvc;

namespace clinic_system_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();
            return Ok(response);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDTO user)
        {
            var response = await _userService.AddUser(user);
            return Ok(response);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            user.UserId = id;
            var response = await _userService.UpdateUser(user);
            return Ok(response);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            return Ok(response);
        }
        [HttpGet("GetUsersByRole/{role}")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            var response = await _userService.GetUsersByRole(role);
            return Ok(response);
        }
    }
}
