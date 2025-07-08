using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReparaStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionTablaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SEC_USUARIOS_Username",
                table: "SEC_USUARIOS");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "SEC_USUARIOS",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SEC_USUARIOS",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "SEC_USUARIOS",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "SEC_USUARIOS",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SEC_USUARIOS",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "SEC_USUARIOS",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "CreatedAt", "FirstName", "LastName", "Name", "Note", "PasswordHash", "PhoneNumber" },
                values: new object[] { "001", new DateTime(2025, 7, 8, 5, 26, 51, 195, DateTimeKind.Utc).AddTicks(5760), "admin", "admin", "admin", "Usuario Administrador", "$2a$11$FUMDx6NdzD6HiFIuGa0yXu4wQrPdop1Sd3LWiLQdJUS5vtkMAH0M.", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_SEC_USUARIOS_Code",
                table: "SEC_USUARIOS",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SEC_USUARIOS_Name",
                table: "SEC_USUARIOS",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SEC_USUARIOS_Code",
                table: "SEC_USUARIOS");

            migrationBuilder.DropIndex(
                name: "IX_SEC_USUARIOS_Name",
                table: "SEC_USUARIOS");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "SEC_USUARIOS");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "SEC_USUARIOS");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "SEC_USUARIOS");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SEC_USUARIOS");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "SEC_USUARIOS");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "SEC_USUARIOS",
                newName: "Username");

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Username" },
                values: new object[] { new DateTime(2025, 7, 2, 4, 52, 26, 749, DateTimeKind.Utc).AddTicks(2549), "$2a$11$/7aVnDBD3Kripb8YQcIBWuKEhubxhyMGiU.a.EoLimP5TC1M.faPq", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_SEC_USUARIOS_Username",
                table: "SEC_USUARIOS",
                column: "Username",
                unique: true);
        }
    }
}
