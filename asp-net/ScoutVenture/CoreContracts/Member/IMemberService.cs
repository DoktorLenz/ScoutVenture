namespace ScoutVenture.CoreContracts.Member
{
    public interface IMemberService
    {
        Task ImportNamiAsync(string username, string password, string groupingId,
            CancellationToken cancellationToken = default);

        Task<MemberOverview> MemberOverview(CancellationToken cancellationToken = default);
    }
}