using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoutVenture.Constants;
using ScoutVenture.CoreContracts.Member;
using ScoutVenture.CoreContracts.User;
using ScoutVenture.Models;

namespace ScoutVenture.Controllers
{
    [ApiController]
    [Route("administration")]
    [Authorize(Roles = Roles.Admin)]
    public class AdministrationController(IMemberService memberService, IUserService userService) : Controller
    {
        [HttpGet("nami/overview")]
        public async Task<ActionResult<MemberOverviewDto>> Overview()
        {
            MemberOverview overview = await memberService.MemberOverview();
            return new MemberOverviewDto
            {
                WoelflingCount = overview.WoelflingCount,
                JungpfadfinderCount = overview.JungpfadfinderCount,
                PfadiCount = overview.PfadiCount,
                RoverCount = overview.RoverCount,
                NoneCount = overview.NoneCount
            };
        }


        [HttpPost("nami/import")]
        public async Task<IActionResult> Import(NamiCredentialsDto namiCredentials)
        {
            await memberService.ImportNamiAsync(
                namiCredentials.MemberId,
                namiCredentials.Password,
                namiCredentials.GroupingId);
            return Ok();
        }

        [HttpGet("user-management/users")]
        public async Task<ActionResult<List<UserListItemDto>>> UserList()
        {
            List<UserListItem> list = await userService.GetUserList();
            return list.Select(UserListItemDto.FromDomainObject).ToList();
        }
    }
}