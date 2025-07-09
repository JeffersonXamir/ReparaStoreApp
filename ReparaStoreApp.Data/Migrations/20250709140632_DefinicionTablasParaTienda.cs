using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReparaStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DefinicionTablasParaTienda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STO_Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Correo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Clientes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
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
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Servicios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Dispositivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroSerie = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Dispositivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_Dispositivos_STO_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "STO_Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroFactura = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Subtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_Facturas_SEC_USUARIOS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_Facturas_STO_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "STO_Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Inventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    StockMinimo = table.Column<int>(type: "int", nullable: false, defaultValue: 5)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Inventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_Inventario_STO_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "STO_Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Kardex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Tipo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Notas = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Kardex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_Kardex_SEC_USUARIOS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_Kardex_STO_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "STO_Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_Reparaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Diagnostico = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CostoEstimado = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CostoFinal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FechaAprobacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DispositivoId = table.Column<int>(type: "int", nullable: false),
                    TecnicoId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STO_Reparaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STO_Reparaciones_SEC_USUARIOS_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "SEC_USUARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STO_Reparaciones_STO_Dispositivos_DispositivoId",
                        column: x => x.DispositivoId,
                        principalTable: "STO_Dispositivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STO_Reparaciones_STO_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "STO_Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STO_DetallesFactura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    ServicioId = table.Column<int>(type: "int", nullable: true)
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
                values: new object[] { new DateTime(2025, 7, 9, 14, 6, 32, 427, DateTimeKind.Utc).AddTicks(6665), "$2a$11$yMIOkYUtDFFn3oIgez89UuN/L3DDZC5aheS5OKSb5XN1FoKC4dbTm" });

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

            migrationBuilder.InsertData(
                table: "STO_Inventario",
                columns: new[] { "Id", "Cantidad", "ProductoId", "StockMinimo" },
                values: new object[,]
                {
                    { 1, 10, 1, 5 },
                    { 2, 15, 2, 5 }
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

            migrationBuilder.CreateIndex(
                name: "IX_STO_Dispositivos_ClienteId",
                table: "STO_Dispositivos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_ClienteId",
                table: "STO_Facturas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Facturas_UsuarioId",
                table: "STO_Facturas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Inventario_ProductoId",
                table: "STO_Inventario",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Kardex_ProductoId",
                table: "STO_Kardex",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Kardex_UsuarioId",
                table: "STO_Kardex",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_DispositivoId",
                table: "STO_Reparaciones",
                column: "DispositivoId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_ServicioId",
                table: "STO_Reparaciones",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_STO_Reparaciones_TecnicoId",
                table: "STO_Reparaciones",
                column: "TecnicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STO_DetallesFactura");

            migrationBuilder.DropTable(
                name: "STO_Inventario");

            migrationBuilder.DropTable(
                name: "STO_Kardex");

            migrationBuilder.DropTable(
                name: "STO_Reparaciones");

            migrationBuilder.DropTable(
                name: "STO_Facturas");

            migrationBuilder.DropTable(
                name: "STO_Productos");

            migrationBuilder.DropTable(
                name: "STO_Dispositivos");

            migrationBuilder.DropTable(
                name: "STO_Servicios");

            migrationBuilder.DropTable(
                name: "STO_Clientes");

            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 8, 5, 26, 51, 195, DateTimeKind.Utc).AddTicks(5760), "$2a$11$FUMDx6NdzD6HiFIuGa0yXu4wQrPdop1Sd3LWiLQdJUS5vtkMAH0M." });
        }
    }
}
