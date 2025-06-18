using NamiClient;
using ScoutVenture.CoreContracts;
using ScoutVenture.CoreContracts.Member;

namespace ScoutVenture.Core
{
    public class MemberService(ITransactionManager transactionManager, IMemberRepository memberRepository)
        : IMemberService
    {
        public async Task ImportNamiAsync(string username, string password, string groupingId,
            CancellationToken cancellationToken = default)
        {
            using NamiRestClient namiClient = new(username, password);
            IList<Member> members = new List<Member>();

            NamiDataWrapper result = await namiClient.GetAllNamiMembersOfGrouping(groupingId);
            if (result.Data != null)
            {
                members = result.Data.Select(Member.FromNami).ToList();
            }

            await transactionManager.ExecuteAsync(
                async () => { await memberRepository.ImportMembersAsync(members, cancellationToken); },
                cancellationToken);
        }

        public Task<MemberOverview> MemberOverview(CancellationToken cancellationToken = default)
        {
            return memberRepository.GetMemberCountAsync(cancellationToken);
        }
    }
}