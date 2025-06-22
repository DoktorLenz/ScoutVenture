using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoutVenture.CoreContracts.User;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture.PostgresAdapter
{
    public class UserRepository(PostgresApplicationDbContext dbContext) : IUserRepository
    {
        public async Task<User> GetUserById(string id, CancellationToken cancellationToken)
        {
            UserDpo user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken) ??
                           throw new KeyNotFoundException($"User with id: {id} was not found");
            return user.ToDomainObject();
        }

        public async Task<bool> LinkMemberToUser(string userId, long memberId, string createdById,
            CancellationToken cancellationToken = default)
        {
            UserMemberLinkDpo link = new()
            {
                UserId = userId,
                MemberId = memberId,
                CreatedDate = DateTime.UtcNow,
                CreatedById = createdById
            };
            EntityEntry<UserMemberLinkDpo> result = await dbContext.UserMemberLinks.AddAsync(link, cancellationToken);

            return result.State == EntityState.Added;
        }

        public async Task<bool> UnlinkMemberFromUser(string userId, long memberId,
            CancellationToken cancellationToken = default)
        {
            int result = await dbContext.UserMemberLinks
                .Where(x => x.UserId == userId && x.MemberId == memberId)
                .ExecuteDeleteAsync(cancellationToken);
            return result > 0;
        }
    }
}