using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoEarthAppAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PastRecycledClassCount",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cat1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Cat2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Cat3 = table.Column<int>(type: "INTEGER", nullable: false),
                    Cat4 = table.Column<int>(type: "INTEGER", nullable: false),
                    Cat5 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastRecycledClassCount", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RecyclableMaterials",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Material = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclableMaterials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "UserCurrency",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCurrency", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserProfile_PastRecycledClassCount_UserId",
                        column: x => x.UserId,
                        principalTable: "PastRecycledClassCount",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfile_UserCurrency_UserId",
                        column: x => x.UserId,
                        principalTable: "UserCurrency",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PastRecycledClassCount",
                columns: new[] { "UserId", "Cat1", "Cat2", "Cat3", "Cat4", "Cat5" },
                values: new object[,]
                {
                    { 1, 0, 0, 0, 0, 0 },
                    { 2, 0, 0, 0, 0, 0 },
                    { 3, 0, 0, 0, 0, 0 },
                    { 4, 0, 0, 0, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "RecyclableMaterials",
                columns: new[] { "MaterialId", "CategoryId", "Material" },
                values: new object[,]
                {
                    { 1, 1, "pet" },
                    { 2, 1, "hdpe" },
                    { 3, 1, "pvc" },
                    { 4, 1, "ldpe" },
                    { 5, 1, "pp" },
                    { 6, 1, "ps" },
                    { 7, 1, "plastic" },
                    { 8, 2, "clear glass" },
                    { 9, 2, "green glass" },
                    { 10, 2, "brown glass" },
                    { 11, 2, "glass" },
                    { 12, 3, "aluminum" },
                    { 13, 3, "steel" },
                    { 14, 3, "metal" },
                    { 15, 4, "office paper" },
                    { 16, 4, "newspapers" },
                    { 17, 4, "magazines" },
                    { 18, 4, "paper" },
                    { 19, 5, "corrugated cardboard" },
                    { 20, 5, "paperboard" },
                    { 21, 5, "cardboard" }
                });

            migrationBuilder.InsertData(
                table: "UserCurrency",
                column: "UserId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4
                });

            migrationBuilder.InsertData(
                table: "UserProfile",
                column: "UserId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecyclableMaterials");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "PastRecycledClassCount");

            migrationBuilder.DropTable(
                name: "UserCurrency");
        }
    }
}
