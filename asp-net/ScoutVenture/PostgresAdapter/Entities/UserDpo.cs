using Microsoft.AspNetCore.Identity;
using ScoutVenture.CoreContracts.User;

namespace ScoutVenture.PostgresAdapter.Entities
{
    public class UserDpo : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<UserMemberLinkDpo> MemberLinks { get; set; } = [];

        public User ToDomainObject()
        {
            var user = new User
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                LinkedMembers = MemberLinks.Select(l => l.Member.ToDomainObject())
            };

            return user;
        }
    }
}