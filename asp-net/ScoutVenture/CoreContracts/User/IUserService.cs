namespace ScoutVenture.CoreContracts.User
{
    public interface IUserService
    {
        Task<List<UserListItem>> GetUserList(CancellationToken cancellationToken = default);

        Task SetPersonalData(string userId, PersonalData personalData, CancellationToken cancellationToken = default);
    }
}