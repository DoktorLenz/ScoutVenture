using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScoutVenture.CoreContracts.Member;

#nullable disable

namespace ScoutVenture.PostgresAdapter.Migrations
{
    /// <inheritdoc />
    public partial class InitMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "divers,female,male")
                .Annotation("Npgsql:Enum:rank", "jungpfadfinder,pfadi,rover,woelfling");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Rank = table.Column<Rank>(type: "rank", nullable: true),
                    Gender = table.Column<Gender>(type: "gender", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:gender", "divers,female,male")
                .OldAnnotation("Npgsql:Enum:rank", "jungpfadfinder,pfadi,rover,woelfling");
        }
    }
}
