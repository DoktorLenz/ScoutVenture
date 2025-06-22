using Microsoft.AspNetCore.Identity;
using ScoutVenture.CoreContracts.User;

namespace ScoutVenture.PostgresAdapter.Entities
{
    public class UserDpo : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public List<UserMemberLinkDpo> MemberLinks { get; set; } = [];

        public User ToDomainObject()
        {
            User user = new()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                LinkedMembers = MemberLinks.Select(l => l.Member.ToDomainObject())
            };

            return user;
        }
    }
}