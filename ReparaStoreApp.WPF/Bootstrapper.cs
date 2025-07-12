using AutoMapper;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Common.Mappings;
using ReparaStoreApp.Core.Services.ClientesService;
using ReparaStoreApp.Core.Services.DispositivosService;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Data;
using ReparaStoreApp.Data.Repositories.ClientesRepository;
using ReparaStoreApp.Data.Repositories.DispositivosRepository;
using ReparaStoreApp.Data.Repositories.Login;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Security;
using ReparaStoreApp.Security.Security;
using ReparaStoreApp.WPF.ViewModels;
using ReparaStoreApp.WPF.ViewModels.Clientes;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using ReparaStoreApp.WPF.ViewModels.Dispositivos;
using ReparaStoreApp.WPF.ViewModels.Home;
using ReparaStoreApp.WPF.ViewModels.Login;
using ReparaStoreApp.WPF.ViewModels.Main;
using ReparaStoreApp.WPF.ViewModels.Services.Clientes;
using ReparaStoreApp.WPF.ViewModels.Services.Dispositivos;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using ReparaStoreApp.WPF.ViewModels.Configuracion;
using ReparaStoreApp.WPF.ViewModels.Users;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using Wpf.Ui;
using ReparaStoreApp.Data.Repositories.ServiciosRepository;
using ReparaStoreApp.Data.Repositories.ProductosRepository;
using ReparaStoreApp.WPF.ViewModels.Services.Productos;
using ReparaStoreApp.WPF.ViewModels.Services.Servicios;

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

            // Configuración de AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                // Asegúrate de usar el namespace correcto
                cfg.AddProfile<AppMappingProfile>();
            });

            // Create the IMapper instance using the CreateMapper method
            _container.Instance<IMapper>(mapperConfig.CreateMapper());

            // 3. Configuración de servicios
            _container
                .Singleton<IThemeService, ThemeService>()
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAuthService, AuthService>()
                .Singleton<IUserRepository, UserRepository>()
                .Singleton<IUserService, UserService>()
                .Singleton<IClientesRepository, ClientesRepository>()
                .Singleton<IClientesService, ClientesService>()
                .Singleton<IDispositivosRepository, DispositivosRepository>()
                .Singleton<IServiciosRepository, ServiciosRepository>()
                .Singleton<IProductosRepository, ProductosRepository>()
                .Singleton<IDispositivosService, DispositivosService>();

            // Nuevos servicios para el listado genérico
            _container.PerRequest<IDataService<UserItem>, UserDataService>();
            _container.PerRequest<GenericListViewModel<UserItem>>(); // ¡Esta línea es crucial!
            _container.PerRequest<IDataService<ClientesItem>, ClientesDataService>();
            _container.PerRequest<GenericListViewModel<ClientesItem>>();
            _container.PerRequest<IDataService<DispositivosItem>, DispositivosDataService>();
            _container.PerRequest<GenericListViewModel<DispositivosItem>>();
            _container.PerRequest<IDataService<ProductServiceItem>, ProductosDataService>();
            _container.PerRequest<IDataService<ProductServiceItem>, ServiciosDataService>();
            _container.PerRequest<GenericListViewModel<ProductServiceItem>>();

            // Registra tus ViewModels aquí
            _container
                .PerRequest<ShellViewModel>()
                .PerRequest<LoginViewModel>()
                .PerRequest<LogOutViewModel>()
                .PerRequest<MainViewModel>()
                .PerRequest<HomeViewModel>()
                .PerRequest<ClientesViewModel>()
                .PerRequest<DispositivosViewModel>()
                .PerRequest<SettingsViewModel>()
                .PerRequest<UserViewModel>();
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
            // Manejar migraciones si se pasa el parámetro
            if (e.Args.Contains("--migrate"))
            {
                ApplyDatabaseMigrations();

                // Si solo se pidió migrar, cerramos la app
                if (e.Args.Length == 1)
                {
                    MessageBox.Show("Migraciones aplicadas correctamente", "Éxito",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Shutdown();
                    return;
                }
            }

            DisplayRootViewForAsync<ShellViewModel>();
        }

        private void ApplyDatabaseMigrations()
        {
            try
            {
                // Reutilizamos la configuración ya existente en el Bootstrapper
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("MariaDB");
                var serverVersion = ServerVersion.AutoDetect(connectionString);

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseMySql(connectionString, serverVersion);

                using (var context = new AppDbContext(optionsBuilder.Options))
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error aplicando migraciones: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(1);
            }
        }
    }
}
