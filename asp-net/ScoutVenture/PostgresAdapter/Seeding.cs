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
            context.SaveChanges();
        }

        private static void SeedRoles(DbContext context)
        {
            var existingRoles = context.Set<IdentityRole>();
            var newRoles = SeedingData.Roles.Where(r => !existingRoles.Any((er => er.Name == r.Name)));
            context.Set<IdentityRole>().AddRange(newRoles);
        }
    }
}