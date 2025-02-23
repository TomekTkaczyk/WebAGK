using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAGK.Module.Users.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                schema: "Users",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Users",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "Users",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                schema: "Users",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                schema: "Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "Users",
                table: "Users");
        }
    }
}
