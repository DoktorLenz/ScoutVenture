using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture.PostgresAdapter
{
    public class PostgresApplicationDbContext(DbContextOptions<PostgresApplicationDbContext> options)
        : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<MemberDto> Members { get; set; }
    }
}