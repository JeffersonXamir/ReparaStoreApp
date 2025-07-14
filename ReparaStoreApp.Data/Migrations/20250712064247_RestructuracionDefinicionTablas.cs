using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReparaStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestructuracionDefinicionTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STO_Inventario_STO_Productos_ProductoId",
                table: "STO_Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Kardex_STO_Productos_ProductoId",
                table: "STO_Kardex");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Reparaciones_STO_Servicios_ServicioId",
                table: "STO_Reparaciones");

            migrationBuilder.DropTable(
                name: "STO_DetallesFactura");

            migrationBuilder.DropTable(
                name: "STO_Productos");

            migrationBuilder.DropTable(
                name: "STO_Servicios");

            migrationBuilder.DropColumn(
                name: "MetodoPago",
                table: "STO_Facturas");

            migrationBuilder.RenameColumn(
                name: "ServicioId",
                table: "STO_Reparaciones",
                newName: "UsuarioReparadoId");

            migrationBuilder.RenameColumn(
                name: "FechaCompletado",
                table: "STO_Reparaciones",
                newName: "FechaReparado");

            migrationBuilder.RenameColumn(
                name: "Diagnostico",
                table: "STO_Reparaciones",
                newName: "NotasIngreso");

            migrationBuilder.RenameIndex(
                name: "IX_STO_Reparaciones_ServicioId",
                table: "STO_Reparaciones",
                newName: "IX_STO_Reparaciones_UsuarioReparadoId");

            migrationBuilder.RenameColumn(
                name: "NumeroFactura",
                table: "STO_Facturas",
                newName: "Numero");

            migrationBuilder.AlterColumn<int>(
                name: "TecnicoId",
                table: "STO_Reparaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "STO_Reparaciones",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CajeroId",
                table: "STO_Reparaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "STO_Reparaciones",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "FacturaId",
                table: "STO_Reparaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "STO_Reparaciones",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEdicion",
                table: "STO_Reparaciones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotasReparado",
                table: "STO_Reparaciones",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "STO_Reparaciones",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioAprobacionId",
                table: "STO_Reparaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreadorId",
                table: "STO_Reparaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioEdicionId",
                table: "STO_Reparaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "STO_Kardex",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEdicion",
                table: "STO_Kardex",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreadorId",
                table: "STO_Kardex",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioEdicionId",
                table: "STO_Kardex",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "STO_Inventario",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StockMaximo",
                table: "STO_Inventario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Impuesto",
                table: "STO_Facturas",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "STO_Facturas",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "STO_Facturas",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "STO_Facturas",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEdicion",
                table: "STO_Facturas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReparacionId",
                table: "STO_Facturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCreadorId",
                table: "STO_Facturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioEdicionId",
                table: "STO_Facturas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "STO_Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nota = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    TieneIVA = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioEdicionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_Items_SEC_USUARIOS_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_Items_SEC_USUARIOS_UsuarioEdicionId",
                        column: x => x.UsuarioEdicionId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SYS_PARAMETROS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nota = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioEdicionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_PARAMETROS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_PARAMETROS_SEC_USUARIOS_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYS_PARAMETROS_SEC_USUARIOS_UsuarioEdicionId",
                        column: x => x.UsuarioEdicionId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Facturas_Dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TasaIVA = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalIVA = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioEdicionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Facturas_Dt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_Facturas_Dt_SEC_USUARIOS_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_Facturas_Dt_SEC_USUARIOS_UsuarioEdicionId",
                        column: x => x.UsuarioEdicionId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STO_Facturas_Dt_STO_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "STO_Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_Facturas_Dt_STO_Items_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "STO_Items",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Reparaciones_Dt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReparacionId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TasaIVA = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalIVA = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    FechaEdicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UsuarioEdicionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Reparaciones_Dt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_Reparaciones_Dt_SEC_USUARIOS_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_Reparaciones_Dt_SEC_USUARIOS_UsuarioEdicionId",
                        column: x => x.UsuarioEdicionId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STO_Reparaciones_Dt_STO_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "STO_Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_Reparaciones_Dt_STO_Reparaciones_ReparacionId",
                        column: x => x.ReparacionId,
                        principalTable: "STO_Reparaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 12, 6, 42, 46, 80, DateTimeKind.Utc).AddTicks(5358), "$2a$11$OG51cr/x2bitWNHyBLZRN.kwpWzDJ/p7QB.2KDa1D/j33Cq/BBfiO" });

            migrationBuilder.UpdateData(
                table: "STO_Inventario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Activo", "StockMaximo" },
                values: new object[] { false, 0 });

            migrationBuilder.UpdateData(
                table: "STO_Inventario",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Activo", "StockMaximo" },
                values: new object[] { false, 0 });

            migrationBuilder.InsertData(
                table: "STO_Items",
                columns: new[] { "Id", "Activo", "Codigo", "Descripcion", "FechaCreacion", "FechaEdicion", "Nombre", "Nota", "Precio", "TieneIVA", "Tipo", "UsuarioCreadorId", "UsuarioEdicionId" },
                values: new object[,]
                {
                    { 1, true, "PROD-001", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pantalla iPhone X", "", 120.00m, true, "Producto", 1, null },
                    { 2, true, "PROD-002", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Batería Samsung S20", "", 45.00m, true, "Producto", 1, null },
                    { 3, true, "SERV-001", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Cambio de pantalla", "", 50.00m, true, "Servicio", 1, null }
                });

            migrationBuilder.InsertData(
                table: "SYS_PARAMETROS",
                columns: new[] { "Id", "Code", "FechaCreacion", "FechaEdicion", "Name", "Nota", "UsuarioCreadorId", "UsuarioEdicionId", "Valor" },
                values: new object[] { 1, "SYS-IVA", new DateTime(2025, 7, 12, 6, 42, 46, 80, DateTimeKind.Utc).AddTicks(6123), null, "IVA para la venta", "IVA para la venta", 1, null, "15" });

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_CajeroId",
                table: "STO_Reparaciones",
                column: "CajeroId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_UsuarioAprobacionId",
                table: "STO_Reparaciones",
                column: "UsuarioAprobacionId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_UsuarioCreadorId",
                table: "STO_Reparaciones",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_UsuarioEdicionId",
                table: "STO_Reparaciones",
                column: "UsuarioEdicionId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Kardex_UsuarioCreadorId",
                table: "STO_Kardex",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Kardex_UsuarioEdicionId",
                table: "STO_Kardex",
                column: "UsuarioEdicionId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_Numero",
                table: "STO_Facturas",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_ReparacionId",
                table: "STO_Facturas",
                column: "ReparacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_UsuarioCreadorId",
                table: "STO_Facturas",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_UsuarioEdicionId",
                table: "STO_Facturas",
                column: "UsuarioEdicionId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_Dt_FacturaId",
                table: "STO_Facturas_Dt",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_Dt_ProductoId",
                table: "STO_Facturas_Dt",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_Dt_UsuarioCreadorId",
                table: "STO_Facturas_Dt",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_Dt_UsuarioEdicionId",
                table: "STO_Facturas_Dt",
                column: "UsuarioEdicionId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Items_Codigo",
                table: "STO_Items",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STO_Items_UsuarioCreadorId",
                table: "STO_Items",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Items_UsuarioEdicionId",
                table: "STO_Items",
                column: "UsuarioEdicionId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_Dt_ItemId",
                table: "STO_Reparaciones_Dt",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_Dt_ReparacionId",
                table: "STO_Reparaciones_Dt",
                column: "ReparacionId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_Dt_UsuarioCreadorId",
                table: "STO_Reparaciones_Dt",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_Dt_UsuarioEdicionId",
                table: "STO_Reparaciones_Dt",
                column: "UsuarioEdicionId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_PARAMETROS_UsuarioCreadorId",
                table: "SYS_PARAMETROS",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_PARAMETROS_UsuarioEdicionId",
                table: "SYS_PARAMETROS",
                column: "UsuarioEdicionId");

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Facturas_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Facturas",
                column: "UsuarioCreadorId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Facturas_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Facturas",
                column: "UsuarioEdicionId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Facturas_STO_Reparaciones_ReparacionId",
                table: "STO_Facturas",
                column: "ReparacionId",
                principalTable: "STO_Reparaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Inventario_STO_Items_ProductoId",
                table: "STO_Inventario",
                column: "ProductoId",
                principalTable: "STO_Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Kardex_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Kardex",
                column: "UsuarioCreadorId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Kardex_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Kardex",
                column: "UsuarioEdicionId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Kardex_STO_Items_ProductoId",
                table: "STO_Kardex",
                column: "ProductoId",
                principalTable: "STO_Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_CajeroId",
                table: "STO_Reparaciones",
                column: "CajeroId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_UsuarioAprobacionId",
                table: "STO_Reparaciones",
                column: "UsuarioAprobacionId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Reparaciones",
                column: "UsuarioCreadorId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Reparaciones",
                column: "UsuarioEdicionId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_UsuarioReparadoId",
                table: "STO_Reparaciones",
                column: "UsuarioReparadoId",
                principalTable: "SEC_USUARIOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STO_Facturas_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Facturas_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Facturas_STO_Reparaciones_ReparacionId",
                table: "STO_Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Inventario_STO_Items_ProductoId",
                table: "STO_Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Kardex_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Kardex");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Kardex_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Kardex");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Kardex_STO_Items_ProductoId",
                table: "STO_Kardex");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_CajeroId",
                table: "STO_Reparaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_UsuarioAprobacionId",
                table: "STO_Reparaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_UsuarioCreadorId",
                table: "STO_Reparaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_UsuarioEdicionId",
                table: "STO_Reparaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_STO_Reparaciones_SEC_USUARIOS_UsuarioReparadoId",
                table: "STO_Reparaciones");

            migrationBuilder.DropTable(
                name: "STO_Facturas_Dt");

            migrationBuilder.DropTable(
                name: "STO_Reparaciones_Dt");

            migrationBuilder.DropTable(
                name: "SYS_PARAMETROS");

            migrationBuilder.DropTable(
                name: "STO_Items");

            migrationBuilder.DropIndex(
                name: "IX_STO_Reparaciones_CajeroId",
                table: "STO_Reparaciones");

            migrationBuilder.DropIndex(
                name: "IX_STO_Reparaciones_UsuarioAprobacionId",
                table: "STO_Reparaciones");

            migrationBuilder.DropIndex(
                name: "IX_STO_Reparaciones_UsuarioCreadorId",
                table: "STO_Reparaciones");

            migrationBuilder.DropIndex(
                name: "IX_STO_Reparaciones_UsuarioEdicionId",
                table: "STO_Reparaciones");

            migrationBuilder.DropIndex(
                name: "IX_STO_Kardex_UsuarioCreadorId",
                table: "STO_Kardex");

            migrationBuilder.DropIndex(
                name: "IX_STO_Kardex_UsuarioEdicionId",
                table: "STO_Kardex");

            migrationBuilder.DropIndex(
                name: "IX_STO_Facturas_Numero",
                table: "STO_Facturas");

            migrationBuilder.DropIndex(
                name: "IX_STO_Facturas_ReparacionId",
                table: "STO_Facturas");

            migrationBuilder.DropIndex(
                name: "IX_STO_Facturas_UsuarioCreadorId",
                table: "STO_Facturas");

            migrationBuilder.DropIndex(
                name: "IX_STO_Facturas_UsuarioEdicionId",
                table: "STO_Facturas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "CajeroId",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "FacturaId",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "FechaEdicion",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "NotasReparado",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "UsuarioAprobacionId",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "UsuarioCreadorId",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "UsuarioEdicionId",
                table: "STO_Reparaciones");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "STO_Kardex");

            migrationBuilder.DropColumn(
                name: "FechaEdicion",
                table: "STO_Kardex");

            migrationBuilder.DropColumn(
                name: "UsuarioCreadorId",
                table: "STO_Kardex");

            migrationBuilder.DropColumn(
                name: "UsuarioEdicionId",
                table: "STO_Kardex");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "STO_Inventario");

            migrationBuilder.DropColumn(
                name: "StockMaximo",
                table: "STO_Inventario");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "STO_Facturas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "STO_Facturas");

            migrationBuilder.DropColumn(
                name: "FechaEdicion",
                table: "STO_Facturas");

            migrationBuilder.DropColumn(
                name: "ReparacionId",
                table: "STO_Facturas");

            migrationBuilder.DropColumn(
                name: "UsuarioCreadorId",
                table: "STO_Facturas");

            migrationBuilder.DropColumn(
                name: "UsuarioEdicionId",
                table: "STO_Facturas");

            migrationBuilder.RenameColumn(
                name: "UsuarioReparadoId",
                table: "STO_Reparaciones",
                newName: "ServicioId");

            migrationBuilder.RenameColumn(
                name: "NotasIngreso",
                table: "STO_Reparaciones",
                newName: "Diagnostico");

            migrationBuilder.RenameColumn(
                name: "FechaReparado",
                table: "STO_Reparaciones",
                newName: "FechaCompletado");

            migrationBuilder.RenameIndex(
                name: "IX_STO_Reparaciones_UsuarioReparadoId",
                table: "STO_Reparaciones",
                newName: "IX_STO_Reparaciones_ServicioId");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "STO_Facturas",
                newName: "NumeroFactura");

            migrationBuilder.AlterColumn<int>(
                name: "TecnicoId",
                table: "STO_Reparaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Impuesto",
                table: "STO_Facturas",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "STO_Facturas",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "MetodoPago",
                table: "STO_Facturas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    Descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Productos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Servicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    Descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Servicios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_DetallesFactura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    ServicioId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_DetallesFactura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_DetallesFactura_STO_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "STO_Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_DetallesFactura_STO_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "STO_Productos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_STO_DetallesFactura_STO_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "STO_Servicios",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 11, 1, 26, 44, 980, DateTimeKind.Utc).AddTicks(2076), "$2a$11$REdnHzefByTd4yoBFAb/suFpdYhoV.RBAro7wuQ2vYk3.n1ctlMf2" });

            migrationBuilder.InsertData(
                table: "STO_Productos",
                columns: new[] { "Id", "Descripcion", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, "Pantalla original para iPhone X", "Pantalla iPhone X", 120.00m },
                    { 2, "Batería original para Samsung Galaxy S20", "Batería Samsung S20", 45.00m }
                });

            migrationBuilder.InsertData(
                table: "STO_Servicios",
                columns: new[] { "Id", "Descripcion", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, "Reemplazo de pantalla rota", "Cambio de pantalla", 50.00m },
                    { 2, "Reemplazo de batería defectuosa", "Cambio de batería", 30.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_STO_DetallesFactura_FacturaId",
                table: "STO_DetallesFactura",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_DetallesFactura_ProductoId",
                table: "STO_DetallesFactura",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_DetallesFactura_ServicioId",
                table: "STO_DetallesFactura",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Inventario_STO_Productos_ProductoId",
                table: "STO_Inventario",
                column: "ProductoId",
                principalTable: "STO_Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Kardex_STO_Productos_ProductoId",
                table: "STO_Kardex",
                column: "ProductoId",
                principalTable: "STO_Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STO_Reparaciones_STO_Servicios_ServicioId",
                table: "STO_Reparaciones",
                column: "ServicioId",
                principalTable: "STO_Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
