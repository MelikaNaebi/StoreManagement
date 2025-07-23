using Microsoft.AspNetCore.Mvc;
using APIStoreManagement.Models;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthController(JwtTokenHelper jwtTokenHelper)
        {
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Validate user credentials (usually by checking against a database)
            if (model.Username == "testuser" && model.Password == "password") // Simplified
            {
                var token = _jwtTokenHelper.GenerateToken("user_id_123", "UserRole");
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }

}
