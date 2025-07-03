using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReparaStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEC_ROLES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEC_ROLES", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEC_USUARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEC_USUARIOS", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SEC_USUARIO_ROLES",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEC_USUARIO_ROLES", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SEC_USUARIO_ROLES_SEC_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SEC_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SEC_USUARIO_ROLES_SEC_USUARIOS_UserId",
                        column: x => x.UserId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "SEC_ROLES",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Acceso total al sistema", "Administrador" },
                    { 2, "Manejo de transacciones financieras", "Cajero" },
                    { 3, "Reparación y mantenimiento", "Técnico" },
                    { 4, "Usuario final del sistema", "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "SEC_USUARIOS",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "PasswordHash", "Username" },
                values: new object[] { 1, new DateTime(2025, 7, 2, 4, 52, 26, 749, DateTimeKind.Utc).AddTicks(2549), "admin@reparastore.com", true, "$2a$11$/7aVnDBD3Kripb8YQcIBWuKEhubxhyMGiU.a.EoLimP5TC1M.faPq", "admin" });

            migrationBuilder.InsertData(
                table: "SEC_USUARIO_ROLES",
                columns: new[] { "RoleId", "UserId", "Id" },
                values: new object[] { 1, 1, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_SEC_USUARIO_ROLES_RoleId",
                table: "SEC_USUARIO_ROLES",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SEC_USUARIOS_Username",
                table: "SEC_USUARIOS",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SEC_USUARIO_ROLES");

            migrationBuilder.DropTable(
                name: "SEC_ROLES");

            migrationBuilder.DropTable(
                name: "SEC_USUARIOS");
        }
    }
}
