using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoutVenture.CoreContracts.User;
using ScoutVenture.Models;

namespace ScoutVenture.Controllers
{
    [Authorize]
    [ApiController]
    [Route("me")]
    public class UserController(IUserService userService) : Controller
    {
        [HttpPost("personal-data")]
        public async Task<IActionResult> SetPersonalData(PersonalDataDto personalData)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                            throw new NullReferenceException("ClaimType.NameIdentifier has no value");

            await userService.SetPersonalData(userId, personalData.ToDomainObject());
            return Ok();
        }
    }
}