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

        public static readonly IdentityUser Admin = new IdentityUser
        {
            Id= "4040b3b8-3a81-40b3-9f8f-868355ef829e",
            UserName = "admin@scoutventure",
            NormalizedUserName = "ADMIN@SCOUTVENTURE",
            Email = "admin@scoutventure",
            NormalizedEmail = "ADMIN@SCOUTVENTURE",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEMkTc3RxxC5AN4h2RWXMybdMzszzhe3E7t9y+LwxgZkak/64681vS96tG1eadERFOw==",
            SecurityStamp = "V5MWSFUZXYN6HCSTUBJ77BRPHY6POKS5",
            ConcurrencyStamp = "947a6e8b-dfc0-4e20-bfeb-db89973d36aa",
            LockoutEnabled = true,
            AccessFailedCount = 0,
        };
    }
}