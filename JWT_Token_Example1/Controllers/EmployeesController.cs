using JWT_Token_Example1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Token_Example1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public string GetData()
        {
            return "Authenticated using the Jwt token";
        }

        [HttpGet]
        [Route("GetDetails")]
        public /*IActionResult*/ string Details()
        {
            //var result = Ok(new { Message = "Authenticated with Jwt" });

            //Response.Headers.Add("Strict-Tansport-Security", "max-age=31536000; includeSubDomains");

            //return result;

            return "Not Authenticated";
        }

        [Authorize]
        [HttpPost]
        public string PostData(Users user)
        {
            return "New User created with the username " + user.Username;
        }
    }
}
