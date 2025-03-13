using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAGK.Module.Insurers.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Insurers");

            migrationBuilder.CreateTable(
                name: "Agent",
                schema: "Insurers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<Guid>(type: "uuid", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AgentId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsurerDb",
                schema: "Insurers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurerDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NodeDb",
                schema: "Insurers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InsurerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Left = table.Column<int>(type: "integer", nullable: false),
                    Right = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeDb_Agent_AgentId",
                        column: x => x.AgentId,
                        principalSchema: "Insurers",
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_NodeDb_InsurerDb_InsurerId",
                        column: x => x.InsurerId,
                        principalSchema: "Insurers",
                        principalTable: "InsurerDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodeDb_NodeDb_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Insurers",
                        principalTable: "NodeDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NodeDb_AgentId",
                schema: "Insurers",
                table: "NodeDb",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeDb_InsurerId",
                schema: "Insurers",
                table: "NodeDb",
                column: "InsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeDb_Left_Right",
                schema: "Insurers",
                table: "NodeDb",
                columns: new[] { "Left", "Right" });

            migrationBuilder.CreateIndex(
                name: "IX_NodeDb_ParentId",
                schema: "Insurers",
                table: "NodeDb",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeDb",
                schema: "Insurers");

            migrationBuilder.DropTable(
                name: "Agent",
                schema: "Insurers");

            migrationBuilder.DropTable(
                name: "InsurerDb",
                schema: "Insurers");
        }
    }
}
