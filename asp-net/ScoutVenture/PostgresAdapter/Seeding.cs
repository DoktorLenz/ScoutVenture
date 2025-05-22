using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ScoutVenture.PostgresAdapter
{
    public static class Seeding
    {
        public static void Seed(DbContext context, bool force = false)
        {
            SeedRoles(context);
            SeedAdmin(context);
            context.SaveChanges();
        }

        private static void SeedRoles(DbContext context)
        {
            var existingRoles = context.Set<IdentityRole>();
            var newRoles = SeedingData.Roles.Where(r => !existingRoles.Any((er => er.Name == r.Name)));
            context.Set<IdentityRole>().AddRange(newRoles);
        }

        private static void SeedAdmin(DbContext context)
        {
            var existingUsers = context.Set<IdentityUser>();
            var defaultAdmin = existingUsers.FirstOrDefault(u => u.Id == SeedingData.Admin.Id);
            if (defaultAdmin == null)
            {
                context.Set<IdentityUser>().Add(SeedingData.Admin);
            }
        }
    }
}