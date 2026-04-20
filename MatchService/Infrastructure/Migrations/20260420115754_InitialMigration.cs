using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeamPublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayTeamPublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StadiumPublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AwayTeamName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StadiumName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HomePoints = table.Column<int>(type: "int", nullable: true),
                    AwayPoints = table.Column<int>(type: "int", nullable: true),
                    IsForfeit = table.Column<bool>(type: "bit", nullable: false),
                    ForfeitLoser = table.Column<int>(type: "int", nullable: true),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
