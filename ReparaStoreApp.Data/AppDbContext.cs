using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Factura;
using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        #region Tienda
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Dispositivos> Dispositivos { get; set; }
        public DbSet<Reparacion> Reparaciones { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Kardex> Kardex { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }
        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table Names Configuration

            #region Seguridad
            modelBuilder.Entity<User>().ToTable("SEC_USUARIOS");
            modelBuilder.Entity<Role>().ToTable("SEC_ROLES");
            modelBuilder.Entity<UserRole>().ToTable("SEC_USUARIO_ROLES");
            #endregion

            #region Tienda
            modelBuilder.Entity<Clientes>().ToTable("STO_Clientes");
            modelBuilder.Entity<Dispositivos>().ToTable("STO_Dispositivos");
            modelBuilder.Entity<Reparacion>().ToTable("STO_Reparaciones");
            modelBuilder.Entity<Servicio>().ToTable("STO_Servicios");
            modelBuilder.Entity<Producto>().ToTable("STO_Productos");
            modelBuilder.Entity<Inventario>().ToTable("STO_Inventario");
            modelBuilder.Entity<Kardex>().ToTable("STO_Kardex");
            modelBuilder.Entity<Factura>().ToTable("STO_Facturas");
            modelBuilder.Entity<DetalleFactura>().ToTable("STO_DetallesFactura");
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

            #region Configuración de Reparación
            modelBuilder.Entity<Reparacion>(entity =>
            {
                entity.Property(e => e.Diagnostico).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.CostoEstimado).HasColumnType("decimal(10,2)");
                entity.Property(e => e.CostoFinal).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasConversion<string>();
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.FechaAprobacion).IsRequired(false);
                entity.Property(e => e.FechaCompletado).IsRequired(false);

                entity.HasOne(r => r.Dispositivo)
                    .WithMany(d => d.Reparaciones)
                    .HasForeignKey(r => r.DispositivoId);

                entity.HasOne(r => r.Tecnico)
                    .WithMany()
                    .HasForeignKey(r => r.TecnicoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Servicio)
                    .WithMany()
                    .HasForeignKey(r => r.ServicioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion

            #region Configuración de Servicio
            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Activo).HasDefaultValue(true);
            });
            #endregion

            #region Configuración de Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Activo).HasDefaultValue(true);
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

            #region Configuración de Factura
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.Property(e => e.NumeroFactura).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Fecha).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Impuesto).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
                entity.Property(e => e.MetodoPago).HasMaxLength(50);

                entity.HasOne(f => f.Cliente)
                    .WithMany()
                    .HasForeignKey(f => f.ClienteId);

                entity.HasOne(f => f.Usuario)
                    .WithMany()
                    .HasForeignKey(f => f.UsuarioId);
            });
            #endregion

            #region Configuración de DetalleFactura
            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Factura)
                    .WithMany(f => f.Detalles)
                    .HasForeignKey(d => d.FacturaId);

                entity.HasOne(d => d.Producto)
                    .WithMany()
                    .HasForeignKey(d => d.ProductoId)
                    .IsRequired(false);

                entity.HasOne(d => d.Servicio)
                    .WithMany()
                    .HasForeignKey(d => d.ServicioId)
                    .IsRequired(false);
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
            modelBuilder.Entity<Servicio>().HasData(
                new Servicio { Id = 1, Nombre = "Cambio de pantalla", Descripcion = "Reemplazo de pantalla rota", Precio = 50.00m },
                new Servicio { Id = 2, Nombre = "Cambio de batería", Descripcion = "Reemplazo de batería defectuosa", Precio = 30.00m }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Pantalla iPhone X", Descripcion = "Pantalla original para iPhone X", Precio = 120.00m },
                new Producto { Id = 2, Nombre = "Batería Samsung S20", Descripcion = "Batería original para Samsung Galaxy S20", Precio = 45.00m }
            );

            modelBuilder.Entity<Inventario>().HasData(
                new Inventario { Id = 1, ProductoId = 1, Cantidad = 10, StockMinimo = 5 },
                new Inventario { Id = 2, ProductoId = 2, Cantidad = 15, StockMinimo = 5 }
            );
            #endregion
            #endregion
        }
    }
}
