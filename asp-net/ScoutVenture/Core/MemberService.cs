using NamiClient;
using ScoutVenture.CoreContracts;
using ScoutVenture.CoreContracts.Member;

namespace ScoutVenture.Core
{
    public class MemberService(ITransactionManager transactionManager, IMemberRepository memberRepository) : IMemberService
    {
        public async Task ImportNami(string username, string password, string groupingId)
        {
            using var namiClient = new NamiRestClient(username, password);
            IList<Member> members = new List<Member>();
            try
            {
                var result = await namiClient.GetAllNamiMembersOfGrouping(groupingId);
                if (result.Data != null)
                {
                    members = result.Data.Select(Member.FromNami).ToList();
                }
            }
            catch (Exception e)
            {
                
            }

            await transactionManager.ExecuteAsync(async () =>
            {
                await memberRepository.ImportMembersAsync(members);
            });
        }
    }
}