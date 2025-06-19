using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ScoutVenture.Controllers
{
    [ApiController]
    [Route("logout")]
    [Authorize]
    public class LogoutController(SignInManager<IdentityUser> signInManager) : ControllerBase
    {
        
        [HttpPost()]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }
}