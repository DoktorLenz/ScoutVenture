namespace ScoutVenture.CoreContracts.Member
{
    public interface IMemberRepository
    {
        Task ImportMembersAsync(IEnumerable<Member> members, CancellationToken cancellationToken = default);

        Task<MemberOverview> GetMemberCountAsync(CancellationToken cancellationToken = default);
    }
}