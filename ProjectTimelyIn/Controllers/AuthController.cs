using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectTimelyIn.Api.Models;
//using ProjectTimelyIn.Data.Entities;
using ProjectTimelyIn.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tweetinvi.Core.Models;

namespace ProjectTimelyIn.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


            [HttpPost]
            public IActionResult Login([FromBody] LoginModel loginModel)
            {
                if (loginModel.FirstName == "malkabr" && loginModel.LastName == "broke" && loginModel.Password == "123456")
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, "malkabr"),
                        new Claim(ClaimTypes.Role, "teacher")
                    };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration.GetValue<string>("JWT:Issuer"),
                    audience: _configuration.GetValue<string>("JWT:Audience"),
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
            }
        
        //public IActionResult Login([FromBody] User loginUser)
        //{
        //    var user = _.Users.FirstOrDefault(u =>
        //        u.FirstName == loginUser.FirstName &&
        //        u.LastName == loginUser.LastName &&
        //        u.Password == loginUser.Password);

        //    if (user == null)
        //        return Unauthorized(new { message = "?? ????? ?? ????? ??????" });

        //    var token = GenerateToken(user.FirstName, "teacher"); // ??????? ??????
        //    return Ok(new { Token = token });
        //}


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
