using Microsoft.AspNetCore.Mvc;
using WebAPICoreSQL.Service;
using static WebAPICoreSQL.Model.Login;

namespace WebAPICoreSQL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService tokenService;
        public AuthController(TokenService tokenService) 
        { 
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validate username and password (hardcoded for example)
            if (request.Username == "admin" && request.Password == "password")
            {
                var token = tokenService.GenerateToken(request.Username);
                return Ok(new LoginResponse { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }
    }
}
