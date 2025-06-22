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
    }
}