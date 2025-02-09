using Microsoft.AspNetCore.Identity;

namespace ScoutVenture.PostgresAdapter
{
    public static class SeedingData
    {
        public static readonly List<IdentityRole> Roles =
        [
            new() { Name = "Admin", NormalizedName = "ADMIN" },
            new() { Name = "User", NormalizedName = "USER" }
        ];
    }
}