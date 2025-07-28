using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoutVenture.PostgresAdapter.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUserWithUserDpo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            // Update the admin user with proper names
            migrationBuilder.Sql(@"
                UPDATE ""AspNetUsers"" 
                SET ""FirstName"" = 'System', ""LastName"" = 'Administrator'
                WHERE ""Id"" = '9fe88cee-9635-41be-988f-1cb08cd30a1d';
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
