using ClassLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassLibAndJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            bool success = false;
            var Record = _context.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
            if (Record != null)
                success = true;


            if (success)
            {
                var JwtToken = GenerateToken(Record);

                return Ok(new { Token = JwtToken });
            }
            return Content("Account NOt Found .");
        }

        private object GenerateToken(UserTable user)
        {
            var claims = new List<Claim>()
            {

                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RollType),
                new Claim("Password", user.Password),
                new Claim("RegistrationDate", user.RegistrationDate.ToString()),

            };

            var key = _configuration.GetValue<string>("ApiSettings:Secret");
            var secured = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinData = new SigningCredentials(secured, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(

                issuer: "Online",
                audience: "",
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: signinData
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

    }
}
