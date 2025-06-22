using ScoutVenture.CoreContracts.User;

namespace ScoutVenture.Models
{
    public class UserListItemDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int LinkedMemberCount { get; set; }

        public static UserListItemDto FromDomainObject(UserListItem item)
        {
            return new UserListItemDto
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                LinkedMemberCount = item.LinkedMemberCount
            };
        }
    }
}