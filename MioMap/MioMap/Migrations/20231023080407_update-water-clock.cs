using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MioMap.Migrations
{
    /// <inheritdoc />
    public partial class updatewaterclock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "WaterClocks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "InWaterClock",
                table: "WaterClocks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OutWaterClock",
                table: "WaterClocks",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "WaterClockStatus",
                table: "WaterClocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "WaterClocks");

            migrationBuilder.DropColumn(
                name: "InWaterClock",
                table: "WaterClocks");

            migrationBuilder.DropColumn(
                name: "OutWaterClock",
                table: "WaterClocks");

            migrationBuilder.DropColumn(
                name: "WaterClockStatus",
                table: "WaterClocks");
        }
    }
}
