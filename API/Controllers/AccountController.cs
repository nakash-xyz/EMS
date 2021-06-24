using System.Threading.Tasks;
using API.BAL;
using API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBAL _accountBAL;

        public AccountController(IAccountBAL accountBAL)
        {
            _accountBAL = accountBAL;
        }

        /// <summary>
        /// Get Login Token
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _accountBAL.Login(loginDTO.Username, loginDTO.Password);

            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest("Invalid login attempt!");
        }
    }
}