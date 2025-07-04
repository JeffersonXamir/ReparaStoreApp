using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Data;
using ReparaStoreApp.Data.Repositories.Login;
using ReparaStoreApp.Security;
using ReparaStoreApp.Security.Security;
using ReparaStoreApp.WPF.ViewModels;
using ReparaStoreApp.WPF.ViewModels.Login;
using ReparaStoreApp.WPF.ViewModels.Main;
using System.IO;
using System.Windows;

namespace ReparaStoreApp.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            // Configuración de la base de datos
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MariaDB");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            _container = new SimpleContainer();
            _container.Instance(_container);

            // 2. Configuración EXPLÍCITA del DbContext
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion);

            // Registro CORRECTO:
            _container.Instance<AppDbContext>(new AppDbContext(optionsBuilder.Options));

            // Configuración de JWT - FORMA CORRECTA
            var jwtSettings = new JwtSettings
            {
                SecretKey = configuration["JwtSettings:SecretKey"] ??
                           throw new InvalidOperationException("SecretKey no configurado"),
                Issuer = configuration["JwtSettings:Issuer"] ?? "ReparaStoreApp",
                Audience = configuration["JwtSettings:Audience"] ?? "ReparaStoreUsers",
                ExpirationMinutes = int.TryParse(configuration["JwtSettings:ExpirationMinutes"],
                                   out var mins) ? mins : 120
            };

            _container.Instance(jwtSettings); // Registro CORRECTO

            // 3. Configuración de servicios
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAuthService, AuthService>()
                .Singleton<IUserRepository, UserRepository>()
                .Singleton<IUserService, UserService>();


            // Registra tus ViewModels aquí
            _container
                .PerRequest<ShellViewModel>()
                .PerRequest<LoginViewModel>()
                .PerRequest<MainViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
