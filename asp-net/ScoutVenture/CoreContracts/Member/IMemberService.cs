namespace ScoutVenture.CoreContracts.Member
{
    public interface IMemberService
    {
        Task ImportNami(string username, string password, string groupingId);
    }
}
