using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Draws",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrawerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrawId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Draws_DrawId",
                        column: x => x.DrawId,
                        principalTable: "Draws",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTeams_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Türkiye" },
                    { 2, "Almanya" },
                    { 3, "Fransa" },
                    { 4, "Hollanda" },
                    { 5, "Portekiz" },
                    { 6, "İtalya" },
                    { 7, "İspanya" },
                    { 8, "Belçika" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Adesso İstanbul" },
                    { 2, 1, "Adesso Ankara" },
                    { 3, 1, "Adesso İzmir" },
                    { 4, 1, "Adesso Antalya" },
                    { 5, 2, "Adesso Berlin" },
                    { 6, 2, "Adesso Frankfurt" },
                    { 7, 2, "Adesso Münih" },
                    { 8, 2, "Adesso Dortmund" },
                    { 9, 3, "Adesso Paris" },
                    { 10, 3, "Adesso Marsilya" },
                    { 11, 3, "Adesso Nice" },
                    { 12, 3, "Adesso Lyon" },
                    { 13, 4, "Adesso Amsterdam" },
                    { 14, 4, "Adesso Rotterdam" },
                    { 15, 4, "Adesso Lahey" },
                    { 16, 4, "Adesso Eindhoven" },
                    { 17, 5, "Adesso Lisbon" },
                    { 18, 5, "Adesso Porto" },
                    { 19, 5, "Adesso Braga" },
                    { 20, 5, "Adesso Coimbra" },
                    { 21, 6, "Adesso Roma" },
                    { 22, 6, "Adesso Milano" },
                    { 23, 6, "Adesso Venedik" },
                    { 24, 6, "Adesso Napoli" },
                    { 25, 7, "Adesso Sevilla" },
                    { 26, 7, "Adesso Madrid" },
                    { 27, 7, "Adesso Barselona" },
                    { 28, 7, "Adesso Granada" },
                    { 29, 8, "Adesso Brüksel" },
                    { 30, 8, "Adesso Brugge" },
                    { 31, 8, "Adesso Gent" },
                    { 32, 8, "Adesso Anvers" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_DrawId",
                table: "Groups",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeams_GroupId",
                table: "GroupTeams",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeams_TeamId",
                table: "GroupTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CountryId",
                table: "Teams",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTeams");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Draws");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
