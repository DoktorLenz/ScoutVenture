using Microsoft.AspNetCore.Identity;

namespace ScoutVenture.PostgresAdapter.Entities
{
    public class UserDpo : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}