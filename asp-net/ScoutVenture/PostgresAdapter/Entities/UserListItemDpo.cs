using Microsoft.EntityFrameworkCore;
using ScoutVenture.CoreContracts.User;

namespace ScoutVenture.PostgresAdapter.Entities
{
    [Keyless]
    public class UserListItemDpo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int LinkedMemberCount { get; set; }

        public UserListItem ToDomainObject()
        {
            return new UserListItem
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                LinkedMemberCount = LinkedMemberCount
            };
        }
    }
}