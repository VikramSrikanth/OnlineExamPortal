using ClassLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassLibAndJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UserController(AppDbContext context) { //, IConfiguration configuration) {
            _context = context;
            //_configuration = configuration;
        }

       // [HttpPost]
        //public IActionResult Create([FromForm]string Name, [FromForm] string Email, [FromForm] string Password, [FromForm] string RollType, [FromForm] DateTime RegDate)
        //{
        //    if (Name != null && Email != null && Password != null && RollType != null && RegDate != null)
        //    {
        //        return OK("Created");
        //    }
        //    return BadRequest("Enter all details");
        //}
        [HttpPost("create")]
        public IActionResult Create([FromBody]UserTable user)
        {
            if (user != null) {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("Created");
            }
            return BadRequest("Enter all details");
        }


           
        //private bool CompareWithDb(string username, string password)
        //{
        //    var Name = _context.Users.FirstOrDefault(u => u.Name==username && u.Password==password);
        //    if(Name!=null)
        //        return true;
        //    return false;
        //}
        
        [HttpGet("Details")]
        public IActionResult GetDetails()
        {
            //return Ok("Welcome ,"+ ClaimTypes.Name);
            var users = _context.Users.ToList(); // Fetch data from database
            return Ok(users);

          //  return Ok(_context.Users.ToString());

        }
        [HttpGet("fetch")]
       
        public ContentResult fetchFromClaim()
        {
            string useNAME = User.FindFirst(ClaimTypes.Name).Value.ToString();
            string useEMAIL = User.FindFirst(ClaimTypes.Email).Value.ToString();
            string useRole = User.FindFirst(ClaimTypes.Role).Value.ToString();
            string usePassword = User.FindFirst("Password")?.Value.ToString();
            string useRegDate = User.FindFirst("RegistrationDate")?.Value.ToString();

            return Content("Name:   " + useNAME + "     Email:  " + useEMAIL + "    Role:  " + useRole + "      Password:  " + usePassword + "      RegDate: " + useRegDate);
        }



    }
}
