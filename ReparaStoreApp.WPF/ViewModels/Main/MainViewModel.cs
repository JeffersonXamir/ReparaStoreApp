using Caliburn.Micro;
using ReparaStoreApp.Security.Security;
using ReparaStoreApp.WPF.ViewModels.Clientes;
using ReparaStoreApp.WPF.ViewModels.Configuracion;
using ReparaStoreApp.WPF.ViewModels.Dispositivos;
using ReparaStoreApp.WPF.ViewModels.Home;
using ReparaStoreApp.WPF.ViewModels.Login;
using ReparaStoreApp.WPF.ViewModels.Productos;
using ReparaStoreApp.WPF.ViewModels.Reparaciones;
using ReparaStoreApp.WPF.ViewModels.Servicios;
using ReparaStoreApp.WPF.ViewModels.Users;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace ReparaStoreApp.WPF.ViewModels.Main
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<ControlMessage>, IHandle<NavigationControlMessage>
    {
        private readonly IAuthService _authService;
        private readonly IWindowManager _windowManager;
        private readonly Dictionary<ToolbarButtonsAction, bool> _buttonStates = new();
        
        private bool _isNavigationEnabled = true;
        public bool IsNavigationEnabled
        {
            get => _isNavigationEnabled;
            set
            {
                _isNavigationEnabled = value;
                NotifyOfPropertyChange(() => IsNavigationEnabled);
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (Set(ref _searchQuery, value))
                    UpdateFilteredItems(_searchQuery);
            }
        }

        private List<NavigationViewItem> _menuItemsMaster = new();
        public ObservableCollection<NavigationViewItem> MenuItems { get; } = new();
        public ObservableCollection<NavigationViewItem> FooterMenuItems { get; } = new();

        public MainViewModel(IAuthService authService, IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _authService = authService;
            _windowManager = windowManager;

            eventAggregator.Subscribe(this);

            // Inicializar todos los botones como habilitados
            foreach (ToolbarButtonsAction action in Enum.GetValues(typeof(ToolbarButtonsAction)))
            {
                _buttonStates[action] = true;
            }

            DisplayName = "ReparaStore App";
            InitializeMenu();
            ActivateItemAsync(IoC.Get<HomeViewModel>()); // Vista inicial
        }

        private void InitializeMenu()
        {
            // Menú principal
            MenuItems.Add(CreateNavigationItem("Inicio", SymbolRegular.Home24, typeof(HomeViewModel)));
            MenuItems.Add(CreateNavigationItem("Clientes", SymbolRegular.People24, typeof(ClientesViewModel)));
            MenuItems.Add(CreateNavigationItem("Dispositivos", SymbolRegular.Phone24, typeof(DispositivosViewModel)));
            MenuItems.Add(CreateNavigationItem("Productos", SymbolRegular.Box24, typeof(ProductosViewModel)));
            MenuItems.Add(CreateNavigationItem("Servicios", SymbolRegular.Wrench24, typeof(ServiciosViewModel)));
            MenuItems.Add(CreateNavigationItem("Reparaciones", SymbolRegular.Toolbox24, typeof(ReparacionesViewModel)));

            // Menú de configuración (con subitems)
            var settingsItem = new NavigationViewItem
            {
                Content = "Configuración",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 }
            };

            settingsItem.MenuItems.Add(CreateNavigationItem("Usuarios", SymbolRegular.Person24, typeof(UserViewModel)));
            settingsItem.MenuItems.Add(CreateNavigationItem("Ajustes", SymbolRegular.Settings24, typeof(SettingsViewModel)));


            MenuItems.Add(settingsItem);

            // Menú footer
            FooterMenuItems.Add(CreateNavigationItem("Cerrar Sesión", SymbolRegular.SignOut24, typeof(LogOutViewModel)));

            foreach (var item in MenuItems)
            {
                _menuItemsMaster.Add(CloneNavigationItem(item));
            }
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

        // Métodos seguros para los botones de la barra de herramientas
        public void ExecuteNew() => SafeExecute(vm => vm.New());
        public void ExecuteCreate() => SafeExecute(vm => vm.Create());
        public void ExecuteEdit() => SafeExecute(vm => vm.Edit());
        public void ExecuteDelete() => SafeExecute(vm => vm.Delete());
        public void ExecuteActivate() => SafeExecute(vm => vm.Activate());
        public void ExecuteUpdate() => SafeExecute(vm => vm.Update());
        public void ExecutePrint() => SafeExecute(vm => vm.Print());
        public void ExecuteUndo() => SafeExecute(vm => vm.Undo());

        private void SafeExecute(Action<BaseViewModel> action)
        {
            try
            {
                if (ActiveItem is BaseViewModel baseViewModel)
                {
                    action?.Invoke(baseViewModel);
                }
                else
                {
                    //MessageBox.Show("La vista actual no soporta esta acción", "Advertencia",
                    //    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error al ejecutar la acción: {ex.Message}", "Error",
                //    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task HandleAsync(ControlMessage message, CancellationToken cancellationToken)
        {
            if (!message.Enable)
            {
                // Deshabilitar todos los botones
                foreach (var key in _buttonStates.Keys.ToList())
                {
                    _buttonStates[key] = false;
                }
            }
            else
            {
                // Habilitar solo los botones especificados
                foreach (var key in _buttonStates.Keys.ToList())
                {
                    _buttonStates[key] = message.Actions.Contains(key);
                }
            }

            // Notificar cambios
            NotifyOfPropertyChange(nameof(IsNewEnabled));
            NotifyOfPropertyChange(nameof(IsSaveEnabled));
            NotifyOfPropertyChange(nameof(IsEditEnabled));
            NotifyOfPropertyChange(nameof(IsDeleteEnabled));
            NotifyOfPropertyChange(nameof(IsRefreshEnabled));
            NotifyOfPropertyChange(nameof(IsPrintEnabled));
            NotifyOfPropertyChange(nameof(IsUndoEnabled));
        }

        public Task HandleAsync(NavigationControlMessage message, CancellationToken cancellationToken)
        {
            IsNavigationEnabled = message.IsEnabled;
            return Task.CompletedTask;
        }

        public bool IsNewEnabled => _buttonStates[ToolbarButtonsAction.New];
        public bool IsSaveEnabled => _buttonStates[ToolbarButtonsAction.Save];
        public bool IsEditEnabled => _buttonStates[ToolbarButtonsAction.Edit];
        public bool IsDeleteEnabled => _buttonStates[ToolbarButtonsAction.Delete];
        public bool IsRefreshEnabled => _buttonStates[ToolbarButtonsAction.Refresh];
        public bool IsPrintEnabled => _buttonStates[ToolbarButtonsAction.Print];
        public bool IsUndoEnabled => _buttonStates[ToolbarButtonsAction.Undo];


        private NavigationViewItem CloneNavigationItem(NavigationViewItem item)
        {
            var clone = new NavigationViewItem
            {
                Content = item.Content,
                Icon = item.Icon,
                Tag = item.Tag,
                TargetPageType = typeof(Page)
            };

            foreach (var subItem in item.MenuItems)
            {
                if (subItem is NavigationViewItem navSubItem)
                {
                    clone.MenuItems.Add(CloneNavigationItem(navSubItem));
                }
            }

            return clone;
        }


        private void UpdateFilteredItems(string query)
        {
            MenuItems.Clear();

            if (string.IsNullOrWhiteSpace(query))
            {
                foreach (var item in _menuItemsMaster)
                    MenuItems.Add(CloneNavigationItem(item));

                return;
            }

            var lowerQuery = query.ToLower();

            foreach (var item in _menuItemsMaster)
            {
                bool isParentMatch = item.Content.ToString().ToLower().Contains(lowerQuery);
                var matchedChildren = new List<NavigationViewItem>();

                foreach (var child in item.MenuItems.OfType<NavigationViewItem>())
                {
                    if (child.Content.ToString().ToLower().Contains(lowerQuery))
                    {
                        matchedChildren.Add(CloneNavigationItem(child));
                    }
                }

                if (matchedChildren.Any())
                {
                    var filteredParent = CloneNavigationItem(item);
                    filteredParent.MenuItems.Clear();
                    foreach (var child in matchedChildren)
                        filteredParent.MenuItems.Add(child);

                    MenuItems.Add(filteredParent);
                }
                else if (isParentMatch)
                {
                    MenuItems.Add(CloneNavigationItem(item));
                }
            }
        }


    }
}
