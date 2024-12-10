using ECommerceFashion.Interface;
using ECommerceFashion.Repositories;
using ECommerceFashion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ECommerceFashion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserMasterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var res = await _userService.GetUserListAsync();
                return Ok(new { users = res });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetAllUsers(int id)
        {
            try
            {
                var res = await _userService.GetUserById(id);
                return Ok(new { users = res });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> AddUserDetails([FromBody] UserDetailsVM users)
        {
            try
            {
                var res = await _userService.AddUserDetails(users);
                if (res == 1) 
                    return StatusCode(StatusCodes.Status200OK, new { statuscode = StatusCodes.Status200OK, message = "Inserted Successfully." });
                else if (res == -1)
                    return StatusCode(StatusCodes.Status302Found, new {statuscode= StatusCodes.Status302Found,message = "Email already exist." });
                else
                    return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserVM userDto)
        {
            var user = await _userService.GetUser(userDto.EmailAddress, userDto.Password);
            if (user == null)
                return Unauthorized();

            var token = _userService.GenerateToken(user);
            return StatusCode(StatusCodes.Status200OK, new { Token = token, message = "Login Successful." });
        }
    }
}
