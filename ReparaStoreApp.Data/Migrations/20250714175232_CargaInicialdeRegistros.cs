using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReparaStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CargaInicialdeRegistros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SEC_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(1452), "$2a$11$aIIpAZ.kxu45ZPQe6DZKS.OWNbo4vvAOLV2KTf2RXTkVMG/.I41AC" });

            migrationBuilder.InsertData(
                table: "STO_Clientes",
                columns: new[] { "Id", "Activo", "Codigo", "Correo", "Direccion", "FechaCreacion", "FechaEdicion", "FechaNacimiento", "Identificacion", "Nombre", "Nota", "PrimerApellido", "PrimerNombre", "SegundoApellido", "SegundoNombre", "Telefono", "UsuarioCreadorId", "UsuarioEdicionId" },
                values: new object[,]
                {
                    { 1, true, "CLI-001", "juan@example.com", "Calle Principal #123", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2201), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0924875614", "Juan Pérez", "", "Pérez", "Juan", "Segundo", "Pablo", "809-555-1234", 1, null },
                    { 2, true, "CLI-002", "maria@example.com", "Avenida Libertad #456", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2204), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "0924846591", "María Rodríguez", "", "Rodríguez", "María", "Alvarez", "Lucía", "809-555-9876", 1, null },
                    { 3, true, "CLI-003", "carlos@example.com", "Calle Las Flores #789", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2207), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1982, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0924859632", "Carlos Sánchez", "", "Sánchez", "Carlos", "González", "Tyron", "829-555-4567", 1, null }
                });

            migrationBuilder.UpdateData(
                table: "STO_Inventario",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Cantidad", "StockMaximo" },
                values: new object[] { 100, 400 });

            migrationBuilder.UpdateData(
                table: "STO_Inventario",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Cantidad", "StockMaximo", "StockMinimo" },
                values: new object[] { 150, 300, 20 });

            migrationBuilder.UpdateData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(1925));

            migrationBuilder.UpdateData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(1926));

            migrationBuilder.InsertData(
                table: "STO_Items",
                columns: new[] { "Id", "Activo", "Codigo", "Descripcion", "FechaCreacion", "FechaEdicion", "Nombre", "Nota", "Precio", "TieneIVA", "Tipo", "UsuarioCreadorId", "UsuarioEdicionId" },
                values: new object[,]
                {
                    { 4, true, "PROD-003", "", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(1927), null, "Cargador USB-C", "", 15.00m, true, "Producto", 1, null },
                    { 5, true, "PROD-004", "", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(1929), null, "Cable Lightning", "", 10.00m, true, "Producto", 1, null },
                    { 6, true, "SERV-002", "", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(1930), null, "Cambio de conector de carga", "", 35.00m, true, "Servicio", 1, null }
                });

            migrationBuilder.InsertData(
                table: "STO_Kardex",
                columns: new[] { "Id", "Cantidad", "Fecha", "FechaCreacion", "FechaEdicion", "Notas", "PrecioUnitario", "ProductoId", "Tipo", "Total", "UsuarioCreadorId", "UsuarioEdicionId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2025, 7, 9, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2132), new DateTime(2025, 7, 9, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2142), null, "Carga inicial Productos", 120.00m, 1, "Ingreso", 12000.00m, 1, null, 1 },
                    { 2, 150, new DateTime(2025, 7, 11, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2154), new DateTime(2025, 7, 11, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2156), null, "Carga inicial Productos", 45.00m, 2, "Ingreso", 6750.00m, 1, null, 1 }
                });

            migrationBuilder.UpdateData(
                table: "SYS_PARAMETROS",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2258));

            migrationBuilder.InsertData(
                table: "STO_Dispositivos",
                columns: new[] { "Id", "Activo", "ClienteId", "Codigo", "Descripcion", "Estado", "FechaCreacion", "FechaEdicion", "Marca", "Modelo", "Nombre", "NumeroSerie", "UsuarioCreadorId", "UsuarioEdicionId" },
                values: new object[,]
                {
                    { 1, true, 1, "DIS-001", "", "Ingresado", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2235), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", "iPhone X", "iPhone X de Juan", "A123456789", 1, null },
                    { 2, true, 2, "DIS-002", "", "Ingresado", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2238), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", "Galaxy S21", "Samsung Galaxy S21 de María", "SM123456789", 1, null },
                    { 3, true, 3, "DIS-003", "", "EnReparacion", new DateTime(2025, 7, 14, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2239), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", "iPad Pro 12.9\"", "iPad Pro de Carlos", "IPD987654321", 1, null }
                });

            migrationBuilder.InsertData(
                table: "STO_Inventario",
                columns: new[] { "Id", "Activo", "Cantidad", "ProductoId", "StockMaximo", "StockMinimo" },
                values: new object[,]
                {
                    { 3, false, 200, 4, 500, 15 },
                    { 4, false, 500, 5, 800, 10 }
                });

            migrationBuilder.InsertData(
                table: "STO_Kardex",
                columns: new[] { "Id", "Cantidad", "Fecha", "FechaCreacion", "FechaEdicion", "Notas", "PrecioUnitario", "ProductoId", "Tipo", "Total", "UsuarioCreadorId", "UsuarioEdicionId", "UsuarioId" },
                values: new object[,]
                {
                    { 3, 200, new DateTime(2025, 7, 12, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2157), new DateTime(2025, 7, 12, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2158), null, "Carga inicial Productos", 15.00m, 4, "Ingreso", 3000.00m, 1, null, 1 },
                    { 4, 500, new DateTime(2025, 7, 13, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2159), new DateTime(2025, 7, 13, 17, 52, 28, 717, DateTimeKind.Utc).AddTicks(2161), null, "Carga inicial Productos", 10.00m, 5, "Ingreso", 5000.00m, 1, null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "STO_Dispositivos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "STO_Dispositivos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "STO_Dispositivos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "STO_Inventario",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "STO_Inventario",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "STO_Kardex",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "STO_Kardex",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "STO_Kardex",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "STO_Kardex",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "STO_Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "STO_Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "STO_Clientes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 5);

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
                columns: new[] { "Cantidad", "StockMaximo" },
                values: new object[] { 10, 0 });

            migrationBuilder.UpdateData(
                table: "STO_Inventario",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Cantidad", "StockMaximo", "StockMinimo" },
                values: new object[] { 15, 0, 5 });

            migrationBuilder.UpdateData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "STO_Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SYS_PARAMETROS",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2025, 7, 12, 6, 42, 46, 80, DateTimeKind.Utc).AddTicks(6123));
        }
    }
}
