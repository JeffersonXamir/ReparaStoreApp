using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Security;
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

        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configuraciones de modelo
        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.HasIndex(e => e.Username).IsUnique();
        //        entity.Property(e => e.PasswordHash).IsRequired();
        //    });
        //}

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table Names Configuration
            modelBuilder.Entity<User>().ToTable("SEC_USUARIOS");
            modelBuilder.Entity<Role>().ToTable("SEC_ROLES");
            modelBuilder.Entity<UserRole>().ToTable("SEC_USUARIO_ROLES");
            #endregion

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

            #region Seed Data
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
        }
    }
}
