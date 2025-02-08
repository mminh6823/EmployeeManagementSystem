using Backend_Library.Repositories.Contracts;
using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController(IUserAccount accountInteface ) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(Register user)
        {
            if (user == null ) return BadRequest("Model trống");
            var result = await accountInteface.CreateAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(Login user)
        {
            if (user == null) return BadRequest("Model trống");
            var result = await accountInteface.SingInAsync(user);
            return Ok(result);
        }
        
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null) return BadRequest("Model trống");
            var result = await accountInteface.RefreshTokenAsync(token);
            return Ok(result);
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var result = await accountInteface.GetUsers();
            if(result == null ) return NotFound();
            return Ok(result);
        }
        [HttpPut("update-user")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(ManageUser user)
        {
            if (user == null) return BadRequest("Model trống");
            var result = await accountInteface.UpdateUser(user);
            return Ok(result);
        }
        [HttpGet("roles")]
        [Authorize]
        public async Task<IActionResult> GetRoles()
        {
            var result = await accountInteface.GetRoles();
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpDelete("delete-user/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0) return BadRequest("Id trống");
            var result = await accountInteface.DeleteUser(id);
            return Ok(result);
        }
        [HttpGet("user-image/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserImage(int id)
        {
            var result = await accountInteface.GetUserImage(id);
            return Ok(result);
        }
        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UserProfile profile)
        {
            var result = await accountInteface.UpdateProfile(profile);
            return Ok(result);
        }
    }
}
