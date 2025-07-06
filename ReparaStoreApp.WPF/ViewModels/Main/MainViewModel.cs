using Caliburn.Micro;
using ReparaStoreApp.Security.Security;
using ReparaStoreApp.WPF.ViewModels.Home;
using ReparaStoreApp.WPF.ViewModels.Users;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace ReparaStoreApp.WPF.ViewModels.Main
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IAuthService _authService;
        private readonly IWindowManager _windowManager;

        public ObservableCollection<NavigationViewItem> MenuItems { get; } = new();
        public ObservableCollection<NavigationViewItem> FooterMenuItems { get; } = new();

        public MainViewModel(IAuthService authService, IWindowManager windowManager)
        {
            _authService = authService;
            _windowManager = windowManager;

            DisplayName = "ReparaStore App";
            InitializeMenu();
            ActivateItemAsync(IoC.Get<HomeViewModel>()); // Vista inicial
        }

        private void InitializeMenu()
        {
            // Menú principal
            MenuItems.Add(CreateNavigationItem("Inicio", SymbolRegular.Home24, typeof(HomeViewModel)));
            MenuItems.Add(CreateNavigationItem("Clientes", SymbolRegular.People24, typeof(UserViewModel)));
            MenuItems.Add(CreateNavigationItem("Productos", SymbolRegular.Box24, typeof(HomeViewModel)));

            // Menú de configuración (con subitems)
            var settingsItem = new NavigationViewItem
            {
                Content = "Configuración",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 }
            };

            settingsItem.MenuItems.Add(CreateNavigationItem("Usuarios", SymbolRegular.Person24, typeof(HomeViewModel)));
            settingsItem.MenuItems.Add(CreateNavigationItem("Ajustes", SymbolRegular.Settings24, typeof(HomeViewModel)));


            MenuItems.Add(settingsItem);

            // Menú footer
            FooterMenuItems.Add(CreateNavigationItem("Cerrar Sesión", SymbolRegular.SignOut24, typeof(HomeViewModel)));
        }

        private NavigationViewItem CreateNavigationItem(string title, SymbolRegular icon, Type viewModelType)
        {
            return new NavigationViewItem
            {
                Content = title,
                Icon = new SymbolIcon { Symbol = icon },
                TargetPageType = typeof(Page),
                Tag = viewModelType
            };
        }

        public void OnNavigationChanged(NavigationView sender)
        {
            if (sender.SelectedItem is NavigationViewItem item && item.Tag is Type viewModelType)
            {
                var viewModel = IoC.GetInstance(viewModelType, null);
                if (viewModel is IScreen screen)
                {
                    ActivateItemAsync(screen);
                }
            }
        }
    }
}
