using Backend_Library.Repositories.Contracts;
using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IUserAccount accountInteface ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register user)
        {
            if (user == null ) return BadRequest("Model trống");
            var result = await accountInteface.CreateAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Login user)
        {
            if (user == null) return BadRequest("Model trống");
            var result = await accountInteface.SingInAsync(user);
            return Ok(result);
        }
        
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null) return BadRequest("Model trống");
            var result = await accountInteface.RefreshTokenAsync(token);
            return Ok(result);
        }

    }
}
