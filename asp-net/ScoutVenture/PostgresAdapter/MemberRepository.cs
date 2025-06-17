using Microsoft.EntityFrameworkCore;
using ScoutVenture.CoreContracts.Member;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture.PostgresAdapter
{
    public class MemberRepository(PostgresApplicationDbContext context) : IMemberRepository
    {
        public async Task ImportMembersAsync(IEnumerable<Member> importList,
            CancellationToken cancellationToken = default)
        {
            List<Member> importMembers = importList.ToList();
            List<long> importMemberIds = importMembers.Select(m => m.MemberId).ToList();

            await context.Members.Where(m => !importMemberIds.Contains(m.MemberId))
                .ExecuteDeleteAsync(cancellationToken);

            List<MemberDto> existingMembers = await context.Members.ToListAsync(cancellationToken);

            List<MemberDto> newMembers = [];
            foreach (Member import in importMembers)
            {
                MemberDto? existing = existingMembers.FirstOrDefault(m => m.MemberId == import.MemberId);
                if (existing != null)
                {
                    // Update properties
                    existing.ApplyUpdate(import);
                }
                else
                {
                    newMembers.Add(MemberDto.FromDo(import));
                }
            }

            context.Members.AddRange(newMembers);
        }
    }
}