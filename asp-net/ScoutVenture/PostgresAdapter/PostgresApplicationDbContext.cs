using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture.PostgresAdapter
{
    public class PostgresApplicationDbContext(DbContextOptions<PostgresApplicationDbContext> options)
        : IdentityDbContext<UserDpo>(options)
    {
        public DbSet<MemberDpo> Members { get; set; }
    }
}