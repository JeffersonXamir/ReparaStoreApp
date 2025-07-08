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
            //var connectionString = "Server=localhost;Database=ReparaStoreDb;User=root;Password=cas1001ja;Port=3306;";

            //var builder = new DbContextOptionsBuilder<AppDbContext>();
            ////var connectionString = configuration.GetConnectionString("MariaDB");

            //builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            //return new AppDbContext(builder.Options);

            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
            var basePath = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json", optional: true);

            var config = builder.Build();
            var connectionString = config.GetConnectionString("MariaDB");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
