using Microsoft.AspNetCore.Mvc;
using PaparaHomeworkAPI.Attributes;
using PaparaHomeworkAPI.Entities;
using PaparaHomeworkAPI.Services;

namespace PaparaHomeworkAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var entity = _loginService.Authenticate(user.Username, user.Password);
            if (entity == null) 
                return Unauthorized();

            return Ok("Fake login successfull");
        }
    }
}