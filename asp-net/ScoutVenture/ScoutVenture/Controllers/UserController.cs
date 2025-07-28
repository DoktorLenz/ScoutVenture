using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScoutVenture.CoreContracts.User;
using ScoutVenture.Models;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture.Controllers
{
    [Authorize]
    [ApiController]
    [Route("me")]
    public class UserController(IUserService userService, UserManager<UserDpo> userManager) : Controller
    {
        [HttpGet("info")]
        public async Task<ActionResult<UserInfoDto>> GetUserInfo()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                            throw new NullReferenceException("ClaimType.NameIdentifier has no value");

            UserDpo? user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            IList<string> roles = await userManager.GetRolesAsync(user);

            return new UserInfoDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? "",
                Roles = roles.ToArray()
            };
        }

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