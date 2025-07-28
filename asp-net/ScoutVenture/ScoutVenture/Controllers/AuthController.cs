using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ScoutVenture.CoreContracts.Exceptions;
using ScoutVenture.Models;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture.Controllers
{
    [ApiController]
    [Route("auth")]
    [EnableRateLimiting("AuthPolicy")]
    public class AuthController(UserManager<UserDpo> userManager) : ControllerBase
    {
        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto model)
        {
            UserDpo user = await userManager.FindByIdAsync(model.UserId) ??
                           throw new ConfirmationCodeException("Fehlerhafter Bestätigungslink.",
                               $"User with id {model.UserId} not found.");

            IdentityResult result = await userManager.ConfirmEmailAsync(user, model.Code);
            if (!result.Succeeded)
            {
                throw new ConfirmationCodeException("Fehlerhafter Bestätigungslink.",
                    $"Could not confirm email for user with id {model.UserId}");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            IdentityResult updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                throw new ConfirmationCodeException("Fehlerhafter Bestätigungslink.",
                    $"Email confirmed for user with id {model.UserId}, but failed to update user profile.");
            }

            return Ok();
        }
    }
}