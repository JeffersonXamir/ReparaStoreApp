using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Common;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;
using ReparaStoreApp.Entities.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region DbSets

        #region Seguridad
        public DbSet<Params> ParamsDb { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        #region Tienda
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Dispositivos> Dispositivos { get; set; }
        public DbSet<ItemEntity> ItemsEntity { get; set; }
        public DbSet<Reparacion> Reparaciones { get; set; }
        public DbSet<ReparacionDetalle> ReparacionDetalles { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Kardex> Kardex { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table Names Configuration

            #region Seguridad
            modelBuilder.Entity<Params>().ToTable("SYS_PARAMETROS");
            modelBuilder.Entity<User>().ToTable("SEC_USUARIOS");
            modelBuilder.Entity<Role>().ToTable("SEC_ROLES");
            modelBuilder.Entity<UserRole>().ToTable("SEC_USUARIO_ROLES");
            #endregion

            #region Tienda
            modelBuilder.Entity<Clientes>().ToTable("STO_Clientes");
            modelBuilder.Entity<Dispositivos>().ToTable("STO_Dispositivos");
            modelBuilder.Entity<ItemEntity>().ToTable("STO_Items");
            modelBuilder.Entity<Reparacion>().ToTable("STO_Reparaciones");
            modelBuilder.Entity<ReparacionDetalle>().ToTable("STO_Reparaciones_Dt");
            modelBuilder.Entity<Inventario>().ToTable("STO_Inventario");
            modelBuilder.Entity<Kardex>().ToTable("STO_Kardex");
            modelBuilder.Entity<Factura>().ToTable("STO_Facturas");
            modelBuilder.Entity<FacturaDetalle>().ToTable("STO_Facturas_Dt");
            #endregion

            #endregion

            #region Restricciones de Tablas

            #region User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique();
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(500);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Note).IsRequired().HasMaxLength(200);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });
            #endregion

            #region Role Configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
            });
            #endregion

            #region UserRole Configuration
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });
            #endregion

            #region Configuración de Parametros
            modelBuilder.Entity<Params>(entity =>
            {
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Valor).IsRequired().HasMaxLength(1024);
                entity.Property(e => e.Nota).HasMaxLength(1024);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Activo).HasDefaultValue(true);
            });
            #endregion

            #region Configuración de Cliente
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Identificacion).IsRequired().HasMaxLength(13);
                entity.Property(e => e.Correo).HasMaxLength(100);
                entity.Property(e => e.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Direccion).HasMaxLength(1024);
                entity.Property(e => e.Nota).HasMaxLength(1024);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Activo).HasDefaultValue(true);
            });
            #endregion

            #region Configuración de Dispositivo
            modelBuilder.Entity<Dispositivos>(entity =>
            {
                entity.Property(e => e.Marca).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NumeroSerie).HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasConversion<string>();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Cliente)
                    .WithMany(c => c.Dispositivos)
                    .HasForeignKey(d => d.ClienteId);
            });
            #endregion

            #region Configuración de Item (Productos/Servicios)
            modelBuilder.Entity<ItemEntity>(entity =>
            {
                entity.HasIndex(e => e.Codigo).IsUnique();
                entity.Property(e => e.Codigo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.Nota).HasMaxLength(1024);
                entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Tipo).HasConversion<string>().HasMaxLength(20);
                entity.Property(e => e.Activo).HasDefaultValue(true);
            });
            #endregion

            #region Configuración de Reparación
            modelBuilder.Entity<Reparacion>(entity =>
            {
                entity.Property(e => e.NotasIngreso).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.CostoEstimado).HasColumnType("decimal(10,2)");
                entity.Property(e => e.CostoFinal).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Estado).HasConversion<string>().HasMaxLength(20);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.FechaAprobacion).IsRequired(false);
                entity.Property(e => e.FechaReparado).IsRequired(false);

                entity.HasOne(r => r.Dispositivo)
                    .WithMany(d => d.Reparaciones)
                    .HasForeignKey(r => r.DispositivoId);

                entity.HasOne(r => r.Tecnico)
                    .WithMany()
                    .HasForeignKey(r => r.TecnicoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Cajero)
                    .WithMany()
                    .HasForeignKey(r => r.CajeroId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Factura)
                    .WithOne(f => f.Reparacion)
                    .HasForeignKey<Factura>(f => f.ReparacionId);
            });
            #endregion

            #region Configuración de ReparacionDetalle
            modelBuilder.Entity<ReparacionDetalle>(entity =>
            {
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10,2)");

                entity.HasOne(rd => rd.Reparacion)
                    .WithMany(r => r.Detalles)
                    .HasForeignKey(rd => rd.ReparacionId);

                entity.HasOne(rd => rd.Item)
                    .WithMany()
                    .HasForeignKey(rd => rd.ItemId);
            });
            #endregion

            #region Configuración de Factura
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasIndex(e => e.Numero).IsUnique();
                entity.Property(e => e.Numero).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Estado).HasConversion<string>().HasMaxLength(20);
                entity.Property(e => e.Subtotal).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            #endregion

            #region Configuración de FacturaDetalle
            modelBuilder.Entity<FacturaDetalle>(entity =>
            {
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10,2)");

                entity.HasOne(fd => fd.Factura)
                    .WithMany(f => f.Detalles)
                    .HasForeignKey(fd => fd.FacturaId);

                entity.HasOne(fd => fd.Producto)
                    .WithMany()
                    .HasForeignKey(fd => fd.ProductoId);
            });
            #endregion

            #region Configuración de Inventario
            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.StockMinimo).IsRequired().HasDefaultValue(5);

                entity.HasOne(i => i.Producto)
                    .WithMany()
                    .HasForeignKey(i => i.ProductoId);
            });
            #endregion

            #region Configuración de Kardex
            modelBuilder.Entity<Kardex>(entity =>
            {
                entity.Property(e => e.Fecha).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasConversion<string>();
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Notas).HasMaxLength(500);

                entity.HasOne(k => k.Producto)
                    .WithMany()
                    .HasForeignKey(k => k.ProductoId);

                entity.HasOne(k => k.Usuario)
                    .WithMany()
                    .HasForeignKey(k => k.UsuarioId);
            });
            #endregion

            #endregion

            #region Seed Data

            #region Seguridad
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrador", Description = "Acceso total al sistema" },
                new Role { Id = 2, Name = "Cajero", Description = "Manejo de transacciones financieras" },
                new Role { Id = 3, Name = "Técnico", Description = "Reparación y mantenimiento" },
                new Role { Id = 4, Name = "Cliente", Description = "Usuario final del sistema" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Code = "001",
                    Name = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123*"),
                    Email = "admin@reparastore.com",
                    PhoneNumber = "1234567890",
                    Note = "Usuario Administrador",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 } // Admin tiene rol Administrador
            );
            #endregion

            #region Tienda
            modelBuilder.Entity<ItemEntity>().HasData(
                new ItemEntity { Id = 1, Codigo = "PROD-001", Nombre = "Pantalla iPhone X", Tipo = TipoItem.Producto, Precio = 120.00m, UsuarioCreadorId = 1, FechaCreacion = DateTime.UtcNow, Activo = true },
                new ItemEntity { Id = 2, Codigo = "PROD-002", Nombre = "Batería Samsung S20", Tipo = TipoItem.Producto, Precio = 45.00m, UsuarioCreadorId = 1, FechaCreacion = DateTime.UtcNow, Activo = true },
                new ItemEntity { Id = 3, Codigo = "SERV-001", Nombre = "Cambio de pantalla", Tipo = TipoItem.Servicio, Precio = 50.00m, UsuarioCreadorId = 1, FechaCreacion = DateTime.UtcNow, Activo = true },
                new ItemEntity
                {
                    Id = 4,
                    Codigo = "PROD-003",
                    Nombre = "Cargador USB-C",
                    Tipo = TipoItem.Producto,
                    Precio = 15.00m,
                    UsuarioCreadorId = 1,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                },
                new ItemEntity
                {
                    Id = 5,
                    Codigo = "PROD-004",
                    Nombre = "Cable Lightning",
                    Tipo = TipoItem.Producto,
                    Precio = 10.00m,
                    UsuarioCreadorId = 1,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                },
                new ItemEntity
                {
                    Id = 6,
                    Codigo = "SERV-002",
                    Nombre = "Cambio de conector de carga",
                    Tipo = TipoItem.Servicio,
                    Precio = 35.00m,
                    UsuarioCreadorId = 1,
                    FechaCreacion = DateTime.UtcNow,
                    Activo = true
                }
            );

            modelBuilder.Entity<Inventario>().HasData(
                new Inventario { Id = 1, ProductoId = 1, Cantidad = 100, StockMinimo = 5, StockMaximo = 400 },
                new Inventario { Id = 2, ProductoId = 2, Cantidad = 150, StockMinimo = 20, StockMaximo = 300 },
                new Inventario { Id = 3, ProductoId = 4, Cantidad = 200, StockMinimo = 15 , StockMaximo = 500 },
                new Inventario { Id = 4, ProductoId = 5, Cantidad = 500, StockMinimo = 10 , StockMaximo = 800 }
            );

            modelBuilder.Entity<Kardex>().HasData(
                // Movimientos de kardex para los productos
                new Kardex
                {
                    Id = 1,
                    Fecha = DateTime.UtcNow.AddDays(-5),
                    Tipo = TipoMovimientoKardex.Ingreso,
                    ProductoId = 1,
                    Cantidad = 100,
                    PrecioUnitario = 120.00m,
                    Total = 12000.00m,
                    Notas = "Carga inicial Productos",
                    UsuarioId = 1,
                    UsuarioCreadorId = 1,
                    FechaCreacion = DateTime.UtcNow.AddDays(-5)
                },
                new Kardex
                {
                    Id = 2,
                    Fecha = DateTime.UtcNow.AddDays(-3),
                    Tipo = TipoMovimientoKardex.Ingreso,
                    ProductoId = 2,
                    Cantidad = 150,
                    PrecioUnitario = 45.00m,
                    Total = 6750.00m,
                    Notas = "Carga inicial Productos",
                    UsuarioId = 1,
                    UsuarioCreadorId = 1,
                    FechaCreacion = DateTime.UtcNow.AddDays(-3)
                },
                new Kardex
                {
                    Id = 3,
                    Fecha = DateTime.UtcNow.AddDays(-2),
                    Tipo = TipoMovimientoKardex.Ingreso,
                    ProductoId = 4,
                    Cantidad = 200,
                    PrecioUnitario = 15.00m,
                    Total = 3000.00m,
                    Notas = "Carga inicial Productos",
                    UsuarioId = 1,
                    UsuarioCreadorId = 1,
                    FechaCreacion = DateTime.UtcNow.AddDays(-2)
                },
                new Kardex
                {
                    Id = 4,
                    Fecha = DateTime.UtcNow.AddDays(-1),
                    Tipo = TipoMovimientoKardex.Ingreso,
                    ProductoId = 5,
                    Cantidad = 500,
                    PrecioUnitario = 10.00m,
                    Total = 5000.00m,
                    Notas = "Carga inicial Productos",
                    UsuarioId = 1,
                    UsuarioCreadorId = 1,
                    FechaCreacion = DateTime.UtcNow.AddDays(-1)
                }
            );

            modelBuilder.Entity<Clientes>().HasData(
                new Clientes
                {
                    Id = 1,
                    Codigo = "CLI-001",
                    Nombre = "Juan Pérez",
                    Identificacion = "0924875614",
                    PrimerNombre = "Juan",
                    PrimerApellido = "Pérez",
                    SegundoNombre = "Pablo",
                    SegundoApellido = "Segundo",
                    FechaNacimiento = new DateTime(1985, 5, 15),
                    Correo = "juan@example.com",
                    Telefono = "809-555-1234",
                    Direccion = "Calle Principal #123",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreadorId = 1,
                    Nota = string.Empty,
                    Activo = true
                },
                new Clientes
                {
                    Id = 2,
                    Codigo = "CLI-002",
                    Nombre = "María Rodríguez",
                    Identificacion = "0924846591",
                    PrimerNombre = "María",
                    PrimerApellido = "Rodríguez",
                    SegundoNombre = "Lucía",
                    SegundoApellido = "Alvarez",
                    FechaNacimiento = new DateTime(1990, 8, 22),
                    Correo = "maria@example.com",
                    Telefono = "809-555-9876",
                    Direccion = "Avenida Libertad #456",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreadorId = 1,
                    Nota = string.Empty,
                    Activo = true
                },
                new Clientes
                {
                    Id = 3,
                    Codigo = "CLI-003",
                    Nombre = "Carlos Sánchez",
                    Identificacion = "0924859632",
                    PrimerNombre = "Carlos",
                    PrimerApellido = "Sánchez",
                    SegundoNombre = "Tyron",
                    SegundoApellido = "González",
                    FechaNacimiento = new DateTime(1982, 3, 10),
                    Correo = "carlos@example.com",
                    Telefono = "829-555-4567",
                    Direccion = "Calle Las Flores #789",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreadorId = 1,
                    Nota = string.Empty,
                    Activo = true
                }
            );

            modelBuilder.Entity<Dispositivos>().HasData(
                new Dispositivos
                {
                    Id = 1,
                    Codigo = "DIS-001",
                    Nombre = "iPhone X de Juan",
                    Marca = "Apple",
                    Modelo = "iPhone X",
                    NumeroSerie = "A123456789",
                    Descripcion = string.Empty,
                    Estado = EstadoDispositivo.Ingresado,
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreadorId = 1,
                    ClienteId = 1,
                    Activo = true
                },
                new Dispositivos
                {
                    Id = 2,
                    Codigo = "DIS-002",
                    Nombre = "Samsung Galaxy S21 de María",
                    Marca = "Samsung",
                    Modelo = "Galaxy S21",
                    NumeroSerie = "SM123456789",
                    Descripcion = string.Empty,
                    Estado = EstadoDispositivo.Ingresado,
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreadorId = 1,
                    ClienteId = 2,
                    Activo = true
                },
                new Dispositivos
                {
                    Id = 3,
                    Codigo = "DIS-003",
                    Nombre = "iPad Pro de Carlos",
                    Marca = "Apple",
                    Modelo = "iPad Pro 12.9\"",
                    NumeroSerie = "IPD987654321",
                    Descripcion = string.Empty,
                    Estado = EstadoDispositivo.EnReparacion,
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreadorId = 1,
                    ClienteId = 3,
                    Activo = true
                }
            );

            #endregion

            #region Params
            modelBuilder.Entity<Params>().HasData(
                new Params
                {
                    Id = 1,
                    Code = "SYS-IVA",
                    Name = "IVA para la venta",
                    Valor = "15",
                    Nota = "IVA para la venta",
                    FechaCreacion = DateTime.UtcNow,
                    UsuarioCreadorId = 1
                }
            );
            #endregion
            #endregion
        }
    }
}
