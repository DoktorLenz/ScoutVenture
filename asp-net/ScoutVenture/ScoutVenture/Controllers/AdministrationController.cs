using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoutVenture.CoreContracts.Member;
using ScoutVenture.Models;

namespace ScoutVenture.Controllers
{
    [ApiController]
    [Route("administration")]
    [Authorize]
    public class AdministrationController(IMemberService memberService) : Controller
    {

        [HttpGet("nami/overview")]
        public async Task<IActionResult> Overview()
        {
            return Ok();
        }
        
        
        
        [HttpPost("nami/import")]
        public async Task<IActionResult> Import(NamiCredentialsDto namiCredentials)
        {
            await memberService.ImportNami(
                namiCredentials.MemberId, 
                namiCredentials.Password,
                namiCredentials.GroupingId);
            return Ok();
        }
    }
}