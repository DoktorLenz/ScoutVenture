using Microsoft.EntityFrameworkCore;
using ScoutVenture.CoreContracts.Member;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture.PostgresAdapter
{
    public class MemberRepository(PostgresApplicationDbContext context) : IMemberRepository
    {
        public async Task ImportMembersAsync(IEnumerable<Member> members, CancellationToken cancellationToken = default)
        {
            // var importedList = members.ToList();
            // var importedIds = importedList.Select(m => m.MemberId).ToHashSet();
            //
            // var existingMembers = await context.Members.ToListAsync(cancellationToken);
            // var existingMap = existingMembers.ToDictionary(m => m.MemberId);
            //
            // var toDelete = existingMembers.Where(m => !importedIds.Contains(m.MemberId)).ToList();
            // context.Members.RemoveRange(toDelete);

            await context.Members.AddRangeAsync(members.Select(MemberDto.FromDo), cancellationToken);
        }
    }
}