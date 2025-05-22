using BarberTermine.Models;
using BerberTermine.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberTermine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "Adhurim" && request.Password == "admin2025")
            {
                return Ok(new { message = "Login i suksesshëm", isAdmin = true });
            }

            return Unauthorized(new { message = "Kredencialet janë të pasakta" });
        }
    }
}