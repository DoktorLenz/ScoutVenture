namespace ScoutVenture.CoreContracts.User
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string id, CancellationToken cancellationToken);
        Task<List<UserListItem>> GetUserList();

        Task<bool> LinkMemberToUser(string userId, long memberId, string createdById,
            CancellationToken cancellationToken);

        Task<bool> UnlinkMemberFromUser(string userId, long memberId, CancellationToken cancellationToken);
    }
}