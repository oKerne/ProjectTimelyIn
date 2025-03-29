using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectTimelyIn.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectTimelyIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel.FirstName == "malkabr" && loginModel.LastName == "broke" && loginModel.Password == "123456")
            {
                var tokenString = GenerateToken("malkabr", "teacher");
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }

     
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            return Ok(new { Message = "User registered successfully!" });
        }

        [HttpGet("refresh-token")]
        public IActionResult RefreshToken()
        {
            var tokenString = GenerateToken("malkabr", "teacher");
            return Ok(new { Token = tokenString });
        }

        [HttpDelete("logout")]
        public IActionResult Logout()
        {
            return Ok(new { Message = "User logged out successfully!" });
        }

        private string GenerateToken(string username, string role)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(6),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}
