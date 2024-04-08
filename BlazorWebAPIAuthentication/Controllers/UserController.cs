using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAPIAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("User data");
        }
    }
}
