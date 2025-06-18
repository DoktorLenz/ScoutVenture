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

        public async Task<MemberOverview> GetMemberCountAsync(CancellationToken cancellationToken = default)
        {
            Dictionary<Rank, int> counts = await context.Members.GroupBy(m => m.Rank)
                .ToDictionaryAsync(g => g.Key, g => g.Count(), cancellationToken);

            return new MemberOverview
            {
                WoelflingCount = counts.GetValueOrDefault(Rank.Woelfling, 0),
                JungpfadfinderCount = counts.GetValueOrDefault(Rank.Jungpfadfinder, 0),
                PfadiCount = counts.GetValueOrDefault(Rank.Pfadi, 0),
                RoverCount = counts.GetValueOrDefault(Rank.Rover, 0)
            };
        }
    }
}