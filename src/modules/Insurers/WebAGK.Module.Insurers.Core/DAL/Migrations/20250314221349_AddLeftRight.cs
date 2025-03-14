using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAGK.Module.Insurers.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddLeftRight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Left",
                schema: "Insurers",
                table: "Structures",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Right",
                schema: "Insurers",
                table: "Structures",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Left",
                schema: "Insurers",
                table: "Structures");

            migrationBuilder.DropColumn(
                name: "Right",
                schema: "Insurers",
                table: "Structures");
        }
    }
}
