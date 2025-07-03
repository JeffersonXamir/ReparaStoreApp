using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ReparaStoreApp.Data
{
    public  class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Configuración para el entorno de diseño
            //IConfiguration configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .AddUserSecrets<DesignTimeDbContextFactory>()
            //    .Build();
            var connectionString = "Server=localhost;Database=ReparaStoreDb;User=root;Password=cas1001ja;Port=3306;";

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            //var connectionString = configuration.GetConnectionString("MariaDB");

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(builder.Options);
        }
    }
}
