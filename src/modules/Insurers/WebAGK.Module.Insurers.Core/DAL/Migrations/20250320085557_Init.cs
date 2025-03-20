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
                name: "Agents",
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
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Structure",
                schema: "Insurers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insurers",
                schema: "Insurers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    StructureId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<Guid>(type: "uuid", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurers_Structure_StructureId",
                        column: x => x.StructureId,
                        principalSchema: "Insurers",
                        principalTable: "Structure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                schema: "Insurers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ValueId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Left = table.Column<int>(type: "integer", nullable: false),
                    Right = table.Column<int>(type: "integer", nullable: false),
                    StructureId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_Agents_ValueId",
                        column: x => x.ValueId,
                        principalSchema: "Insurers",
                        principalTable: "Agents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nodes_Nodes_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Insurers",
                        principalTable: "Nodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nodes_Structure_StructureId",
                        column: x => x.StructureId,
                        principalSchema: "Insurers",
                        principalTable: "Structure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insurers_StructureId",
                schema: "Insurers",
                table: "Insurers",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_ParentId",
                schema: "Insurers",
                table: "Nodes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_StructureId",
                schema: "Insurers",
                table: "Nodes",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_ValueId",
                schema: "Insurers",
                table: "Nodes",
                column: "ValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insurers",
                schema: "Insurers");

            migrationBuilder.DropTable(
                name: "Nodes",
                schema: "Insurers");

            migrationBuilder.DropTable(
                name: "Agents",
                schema: "Insurers");

            migrationBuilder.DropTable(
                name: "Structure",
                schema: "Insurers");
        }
    }
}
