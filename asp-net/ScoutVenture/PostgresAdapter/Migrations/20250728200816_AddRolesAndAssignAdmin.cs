using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoutVenture.PostgresAdapter.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesAndAssignAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert the three roles
            migrationBuilder.Sql(@"
                INSERT INTO ""AspNetRoles"" (""Id"", ""Name"", ""NormalizedName"", ""ConcurrencyStamp"")
                VALUES 
                    ('admin-role-id', 'Admin', 'ADMIN', 'admin-stamp'),
                    ('counselor-role-id', 'Counselor', 'COUNSELOR', 'counselor-stamp'),
                    ('member-role-id', 'Member', 'MEMBER', 'member-stamp')
                ON CONFLICT DO NOTHING;
            ");

            // Assign Admin role to the specific user
            migrationBuilder.Sql(@"
                INSERT INTO ""AspNetUserRoles"" (""UserId"", ""RoleId"")
                VALUES ('9fe88cee-9635-41be-988f-1cb08cd30a1d', 'admin-role-id')
                ON CONFLICT DO NOTHING;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove user role assignment
            migrationBuilder.Sql(@"
                DELETE FROM ""AspNetUserRoles"" 
                WHERE ""UserId"" = '9fe88cee-9635-41be-988f-1cb08cd30a1d' 
                AND ""RoleId"" = 'admin-role-id';
            ");

            // Remove roles
            migrationBuilder.Sql(@"
                DELETE FROM ""AspNetRoles"" 
                WHERE ""Id"" IN ('admin-role-id', 'counselor-role-id', 'member-role-id');
            ");
        }
    }
}
