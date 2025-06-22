namespace ScoutVenture.CoreContracts.User
{
    public interface IUserService
    {
        Task<List<UserListItem>> GetUserList(CancellationToken cancellationToken = default);
    }
}