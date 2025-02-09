using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ScoutVenture.PostgresAdapter
{
    public static class SeedingAsync
    {
        public static Task SeedAsync(DbContext context, bool force = false,
            CancellationToken cancellationToken = default)
        {
            SeedRolesAsync(context, cancellationToken);
            return context.SaveChangesAsync(cancellationToken);
        }

        private static void SeedRolesAsync(DbContext context, CancellationToken cancellationToken = default)
        {
            var existingRoles = context.Set<IdentityRole>();
            var newRoles = SeedingData.Roles.Where(r => !existingRoles.Any((er => er.Name == r.Name)));
            context.Set<IdentityRole>().AddRangeAsync(newRoles, cancellationToken);
        }
    }
}