using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReparaStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionTablaDispositivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "STO_Dispositivos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "STO_Dispositivos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEdicion",
                table: "STO_Dispositivos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "STO_Dispositivos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreadorId",
                table: "STO_Dispositivos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioEdicionId",
                table: "STO_Dispositivos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nota",
                table: "STO_Clientes",
                type: "varchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "STO_Clientes",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "STO_Clientes",
                type: "varchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "STO_Clientes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Identificacion",
                table: "STO_Clientes",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 11, 1, 26, 44, 980, DateTimeKind.Utc).AddTicks(2076), "$2a$11$REdnHzefByTd4yoBFAb/suFpdYhoV.RBAro7wuQ2vYk3.n1ctlMf2" });

            migrationBuilder.CreateIndex(
                name: "IX_STO_Dispositivos_UsuarioCreadorId",
                table: "STO_Dispositivos",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Dispositivos_UsuarioEdicionId",
                table: "STO_Dispositivos",
                column: "UsuarioEdicionId");

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Dispositivos_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Dispositivos",
                column: "UsuarioCreadorId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Dispositivos_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Dispositivos",
                column: "UsuarioEdicionId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STO_Dispositivos_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Dispositivos");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Dispositivos_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Dispositivos");

            migrationBuilder.DropIndex(
                name: "IX_STO_Dispositivos_UsuarioCreadorId",
                table: "STO_Dispositivos");

            migrationBuilder.DropIndex(
                name: "IX_STO_Dispositivos_UsuarioEdicionId",
                table: "STO_Dispositivos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "STO_Dispositivos");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "STO_Dispositivos");

            migrationBuilder.DropColumn(
                name: "FechaEdicion",
                table: "STO_Dispositivos");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "STO_Dispositivos");

            migrationBuilder.DropColumn(
                name: "UsuarioCreadorId",
                table: "STO_Dispositivos");

            migrationBuilder.DropColumn(
                name: "UsuarioEdicionId",
                table: "STO_Dispositivos");

            migrationBuilder.DropColumn(
                name: "Identificacion",
                table: "STO_Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "Nota",
                table: "STO_Clientes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1024)",
                oldMaxLength: 1024)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "STO_Clientes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "STO_Clientes",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1024)",
                oldMaxLength: 1024)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "STO_Clientes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 10, 14, 13, 58, 335, DateTimeKind.Utc).AddTicks(8963), "$2a$11$WqWyP9S/3VdwN.UVTHSD/OaLJtXTD6kwQGf72U7DpPddyaNTRneD2" });
        }
    }
}
