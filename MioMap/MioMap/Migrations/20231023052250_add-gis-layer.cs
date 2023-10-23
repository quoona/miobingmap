using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MioMap.Migrations
{
    /// <inheritdoc />
    public partial class addgislayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GisLayerId",
                table: "WaterPiplines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GisLayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GisLayer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WaterPiplines_GisLayerId",
                table: "WaterPiplines",
                column: "GisLayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterPiplines_GisLayer_GisLayerId",
                table: "WaterPiplines",
                column: "GisLayerId",
                principalTable: "GisLayer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterPiplines_GisLayer_GisLayerId",
                table: "WaterPiplines");

            migrationBuilder.DropTable(
                name: "GisLayer");

            migrationBuilder.DropIndex(
                name: "IX_WaterPiplines_GisLayerId",
                table: "WaterPiplines");

            migrationBuilder.DropColumn(
                name: "GisLayerId",
                table: "WaterPiplines");
        }
    }
}
