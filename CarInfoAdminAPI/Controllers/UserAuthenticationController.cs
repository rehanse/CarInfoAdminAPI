using CarInfo.DataAccess.Domain.Identity;
using CarInfo.DataAccess.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarInfoAdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private IUserAuthenticationService authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            var result = await authService.RegisterAsync(registerModel);
            if(result.statusCode == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            var result = await authService.LoginAsync(model);
            return Ok(result);
        }
        [HttpGet("logOff")]
        public async Task<IActionResult> Logout()
        {
            var result = await authService.LogoutAsync();
            return Ok(result);
        }

    }
}
