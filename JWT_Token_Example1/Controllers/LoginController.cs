using JWT_Token_Example1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Token_Example1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private Users AuthenticateUser(Users user)
        {
            Users _user = null;
            if (user.Username == "Admin" && user.Password == "123456")
            {
                _user = new Users { Username = "Surya" };
            }

            if (user.Username =="User" &&  user.Password == "test")
            {
                _user = new Users { Username = "Surya11" };
            }
            return _user;
        }

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims, expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users users)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(users);
            if (user != null)
            {
                var token = GenerateToken(user);
                response = Ok(new { token });
            }
            return response;
        }
    }
}
