using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReparaStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionTablaClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "STO_Clientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEdicion",
                table: "STO_Clientes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "STO_Clientes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nota",
                table: "STO_Clientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PrimerApellido",
                table: "STO_Clientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PrimerNombre",
                table: "STO_Clientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SegundoApellido",
                table: "STO_Clientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SegundoNombre",
                table: "STO_Clientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreadorId",
                table: "STO_Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioEdicionId",
                table: "STO_Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 10, 14, 13, 58, 335, DateTimeKind.Utc).AddTicks(8963), "$2a$11$WqWyP9S/3VdwN.UVTHSD/OaLJtXTD6kwQGf72U7DpPddyaNTRneD2" });

            migrationBuilder.CreateIndex(
                name: "IX_STO_Clientes_UsuarioCreadorId",
                table: "STO_Clientes",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Clientes_UsuarioEdicionId",
                table: "STO_Clientes",
                column: "UsuarioEdicionId");

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Clientes_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Clientes",
                column: "UsuarioCreadorId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Clientes_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Clientes",
                column: "UsuarioEdicionId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STO_Clientes_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Clientes_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Clientes");

            migrationBuilder.DropIndex(
                name: "IX_STO_Clientes_UsuarioCreadorId",
                table: "STO_Clientes");

            migrationBuilder.DropIndex(
                name: "IX_STO_Clientes_UsuarioEdicionId",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "FechaEdicion",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "Nota",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "PrimerApellido",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "PrimerNombre",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "SegundoApellido",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "SegundoNombre",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "UsuarioCreadorId",
                table: "STO_Clientes");

            migrationBuilder.DropColumn(
                name: "UsuarioEdicionId",
                table: "STO_Clientes");

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 9, 14, 6, 32, 427, DateTimeKind.Utc).AddTicks(6665), "$2a$11$yMIOkYUtDFFn3oIgez89UuN/L3DDZC5aheS5OKSb5XN1FoKC4dbTm" });
        }
    }
}
