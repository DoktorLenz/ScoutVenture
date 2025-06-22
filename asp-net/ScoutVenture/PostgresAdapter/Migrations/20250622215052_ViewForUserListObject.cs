using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoutVenture.PostgresAdapter.Migrations
{
    /// <inheritdoc />
    public partial class ViewForUserListObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW ""UserList"" AS
SELECT
    u.""Id"" as ""Id"",
    u.""FirstName"" as ""FirstName"",
    u.""LastName"" as ""LastName"",
    u.""Email"" as ""Email"",
    COUNT(uml.""MemberId"") as ""LinkedMemberCount""
FROM
    ""AspNetUsers"" u
LEFT JOIN
    ""UserMemberLinks"" uml ON uml.""UserId"" = u.""Id""
GROUP BY
    u.""Id"";
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS \"UserList\"");
        }
    }
}
