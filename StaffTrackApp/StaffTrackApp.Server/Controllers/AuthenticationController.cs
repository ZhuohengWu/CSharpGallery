using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace StaffTrackApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthenticationController(IUserAccount accountInterface) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(Register user)
        {
            if (user is null) return BadRequest("Empty Register model");
            var result = await accountInterface.CreateAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user is null) return BadRequest("Empty Login model");
            var result = await accountInterface.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token is null) return BadRequest("Empty RefreshToken model");
            var result = await accountInterface.RefreshTokenAsync(token);
            return Ok(result);
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await accountInterface.GetUsersAsync();
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPut("update-user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(ManageUser user)
        {
            var result = await accountInterface.UpdateUserAsync(user);
            return Ok(result);
        }

        [HttpGet("roles")]
        [Authorize]
        public async Task<IActionResult> GetRolesAsync()
        {
            var result = await accountInterface.GetRolesAsync();
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("delete-users/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await accountInterface.DeleteUserAsync(id);
            return Ok(result);
        }

        [HttpGet("user-image/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserImageAsync(int id)
        {
            var result = await accountInterface.GetUserImageAsync(id);
            return Ok(result);
        }

        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfileAsync(UserProfile user)
        {
            var result = await accountInterface.UpdateUserProfileAsync(user);
            return Ok(result);
        }
    }
}
