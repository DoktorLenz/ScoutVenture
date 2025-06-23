using ScoutVenture.CoreContracts;
using ScoutVenture.CoreContracts.User;

namespace ScoutVenture.Core
{
    public class UserService(ITransactionManager transactionManager, IUserRepository userRepository) : IUserService
    {
        public Task<List<UserListItem>> GetUserList(CancellationToken cancellationToken = default)
        {
            return transactionManager.ExecuteAsync(
                userRepository.GetUserList,
                cancellationToken);
        }

        public async Task SetPersonalData(string userId, PersonalData personalData,
            CancellationToken cancellationToken = default)
        {
            await transactionManager.ExecuteAsync(
                async () =>
                {
                    User user = await userRepository.GetUserById(userId, cancellationToken);
                    user.FirstName = personalData.FirstName;
                    user.LastName = personalData.LastName;
                }, cancellationToken);
        }
    }
}