using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAGK.Module.Users.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailConfirmExpires : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Expires",
                schema: "Users",
                table: "Users",
                newName: "RefreshExpires");

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmExpires",
                schema: "Users",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmExpires",
                schema: "Users",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RefreshExpires",
                schema: "Users",
                table: "Users",
                newName: "Expires");
        }
    }
}
