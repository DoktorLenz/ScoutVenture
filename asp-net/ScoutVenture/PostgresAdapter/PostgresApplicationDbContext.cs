using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScoutVenture.PostgresAdapter.Entities;

namespace ScoutVenture.PostgresAdapter
{
    public class PostgresApplicationDbContext(DbContextOptions<PostgresApplicationDbContext> options)
        : IdentityDbContext<UserDpo, IdentityRole, string>(options)
    {
        public DbSet<MemberDpo> Members { get; set; }
        public DbSet<UserMemberLinkDpo> UserMemberLinks { get; set; }
        public DbSet<UserListItemDpo> UserList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserMemberLinkDpo>()
                .HasKey(x => new { x.UserId, x.MemberId });

            builder.Entity<UserMemberLinkDpo>()
                .HasOne(x => x.User)
                .WithMany(u => u.MemberLinks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes link if User is deleted

            builder.Entity<UserMemberLinkDpo>()
                .HasOne(x => x.Member)
                .WithMany()
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes link if Member is deleted

            builder.Entity<UserListItemDpo>()
                .HasNoKey()
                .ToView("UserList");
        }
    }
}