using ECommerceFashion.Interface;
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
        public async Task<IActionResult> AddUserDetails([FromBody]UserDetailsVM users)
        {
            try
            {
                var res = await _userService.AddUserDetails(users);
                if (res)
                    return StatusCode(StatusCodes.Status200OK,"Inserted Successfully.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
