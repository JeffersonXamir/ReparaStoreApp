using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ClientesService;
using ReparaStoreApp.Core.Services.DispositivosService;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;
using ReparaStoreApp.WPF.Models;
using ReparaStoreApp.WPF.Properties;
using ReparaStoreApp.WPF.ViewModels.Controls.DialogControl;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using ReparaStoreApp.WPF.ViewModels.Services.Clientes;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReparaStoreApp.WPF.ViewModels.Reparaciones
{
    public class ReparacionesViewModel : BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientesService _ClientesService;
        private readonly IDispositivosService _DispositivosService;
        private readonly IUserService _UserService;
        private readonly IDataService<ClientesItem> _ClientesDataService;
        private readonly GenericSelectionDialogViewModel<ProductosItem> _productSelectionDialog;
        private readonly IMapper _mapper;

        public bool IsInEditOrCreationMode => CreationMode || EditMode;

        private ReparacionItem _currentRepair;
        public ReparacionItem CurrentRepair
        {
            get { return _currentRepair; }
            set { _currentRepair = value; NotifyOfPropertyChange(() => CurrentRepair); }
        }

        private BindableCollection<DispositivosItem> _availableDevices;
        public BindableCollection<DispositivosItem> AvailableDevices
        {
            get { return _availableDevices; }
            set { _availableDevices = value; NotifyOfPropertyChange(() => AvailableDevices); }
        }

        private DispositivosItem _deviceSelect;
        public DispositivosItem DeviceSelect
        {
            get { return _deviceSelect; }
            set { _deviceSelect = value; NotifyOfPropertyChange(() => DeviceSelect); _ = OnSelectDispositivo(); }
        }

        private BindableCollection<ClientesItem> _availableClients;
        public BindableCollection<ClientesItem> AvailableClients
        {
            get { return _availableClients; }
            set { _availableClients = value; NotifyOfPropertyChange(() => AvailableClients); }
        }

        private ClientesItem _clientSelect;
        public ClientesItem ClientSelect
        {
            get { return _clientSelect; }
            set { _clientSelect = value; NotifyOfPropertyChange(() => ClientSelect); }
        }

        private BindableCollection<UserItem> _availableTechnicians;
        public BindableCollection<UserItem> AvailableTechnicians
        {
            get { return _availableTechnicians; }
            set { _availableTechnicians = value; NotifyOfPropertyChange(() => AvailableTechnicians); }
        }

        private UserItem _technicianSelect;
        public UserItem TechnicianSelect
        {
            get { return _technicianSelect; }
            set { _technicianSelect = value; NotifyOfPropertyChange(() => TechnicianSelect); }
        }

        private BindableCollection<EstadosReparacionItem> _repairStates;
        public BindableCollection<EstadosReparacionItem> RepairStates
        {
            get { return _repairStates; }
            set { _repairStates = value; NotifyOfPropertyChange(() => RepairStates); }
        }

        private EstadosReparacionItem _repairStateSelect;
        public EstadosReparacionItem RepairStateSelect
        {
            get { return _repairStateSelect; }
            set { _repairStateSelect = value; NotifyOfPropertyChange(() => RepairStateSelect); _ = OnSelectState(); }
        }


        private int _tasaIVA;
        public int TasaIVA
        {
            get { return _tasaIVA; }
            set { _tasaIVA = value; NotifyOfPropertyChange(() => TasaIVA); }
        }

        private BindableCollection<ProductosItem> _selectedProducts = new BindableCollection<ProductosItem>();
        public BindableCollection<ProductosItem> SelectedProducts
        {
            get => _selectedProducts;
            set
            {
                _selectedProducts = value;
                NotifyOfPropertyChange(() => SelectedProducts);
            }
        }

        public ReparacionesViewModel(
                            IWindowManager windowManager,
                            IEventAggregator eventAggregator,
                            IDataService<ClientesItem> ClientesDataService,
                            IClientesService ClientesService,
                            IDispositivosService DispositivosService,
                            IUserService UserService,
                            GenericSelectionDialogViewModel<ProductosItem> ProductSelectionDialog,
                            IMapper mapper) : base(eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _ClientesDataService = ClientesDataService;
            _ClientesService = ClientesService;
            _DispositivosService = DispositivosService;
            _UserService = UserService;
            _productSelectionDialog = ProductSelectionDialog;
            _mapper = mapper;

            AvailableClients = new BindableCollection<ClientesItem>();


        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
        }
        public async Task CargarDispositivos()
        {
            try
            {
                var Details = await _DispositivosService.SearchDispositivossAsync("", 1, 100);
                AvailableDevices = _mapper.Map<BindableCollection<DispositivosItem>>(Details);
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage("Error al cargar detalles del Registro"));
            }

        }

        public async Task CargarTecnicos()
        {
            try
            {
                var filter = $"IsActive == true";
                var Details = await _UserService.GetAllUsersAsync(filter);
                AvailableTechnicians = _mapper.Map<BindableCollection<UserItem>>(Details);
            }
            catch (Exception ex)
            {
                HandleError(ex, "Error al cargar detalles del Registro");
            }

        }

        public async Task OnSelectDispositivo()
        {
            try
            {
                var item = await _ClientesService.GetClientesByIdAsync(DeviceSelect.ClienteId);
                if (item == null)
                    throw new Exception("Error");

                var itemMap = _mapper.Map<ClientesItem>(item);
                if (itemMap != null)
                    await CargarClienteDispositivo(itemMap);
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage("Error al cargar detalles del Registro"));
            }

        }

        public async Task CargarClienteDispositivo(ClientesItem cliente)
        {
            try
            {
                AvailableClients.Add(cliente);
                ClientSelect = cliente;
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage("Error al cargar detalles del Registro"));
            }

        }

        private async Task CargarEstados()
        {
            RepairStates = new BindableCollection<EstadosReparacionItem>
            {
                new EstadosReparacionItem { Id = 1, Code = "001", Name = "Pendiente", Estado = EstadoReparacion.Pendiente },
                new EstadosReparacionItem { Id = 2, Code = "002", Name = "Aprobado", Estado = EstadoReparacion.Aprobado},
                new EstadosReparacionItem { Id = 3, Code = "003", Name = "En Proceso", Estado = EstadoReparacion.EnProceso },
                new EstadosReparacionItem { Id = 4, Code = "004", Name = "Completado", Estado = EstadoReparacion.Completado },
                new EstadosReparacionItem { Id = 5, Code = "005", Name = "Rechazado", Estado = EstadoReparacion.Rechazado }
            };
            await Task.CompletedTask;
        }

        private async Task OnSelectState()
        {
            try
            {
                CurrentRepair.Estado = RepairStateSelect.Estado;
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage("Error al cargar detalles del Registro"));
            }
        }

        private async Task CambioEstado()
        {
            try
            {
                switch (CurrentRepair.Estado)
                {
                    case EstadoReparacion.Pendiente:
                        break;
                    case EstadoReparacion.Aprobado:
                        break;
                    case EstadoReparacion.EnProceso:
                        break;
                    case EstadoReparacion.Completado:
                        break;
                    case EstadoReparacion.Rechazado:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage("Error al cargar detalles del Registro"));
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            await CargarEstados();
            await CargarDispositivos();
            await CargarTecnicos();

            base.OnViewLoaded(view);
        }

        public override async Task New()
        {
            try
            {
                CreationMode = true;
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);


                var settings = new Settings();
                int userId = settings.UserId;

                CurrentRepair = new ReparacionItem(); // Crear una copia vacía para edición
                CurrentRepair.Id = 0;
                CurrentRepair.Numero = "000000000";
                CurrentRepair.Fecha = DateTime.Now.Date;
                CurrentRepair.FechaCreacion = DateTime.Now;
                CurrentRepair.UsuarioCreadorId = userId;
                //CurrentRepair.Estado = EstadoReparacion.Aprobado;

                RepairStateSelect = RepairStates?.FirstOrDefault(x => x.Estado == EstadoReparacion.Pendiente);

                var paramIVA = await _UserService.GetParamByCode("SYS-IVA");
                if (paramIVA == null)
                {
                    await ShowNotification("No se ha configurado la tasa del IVA");
                    return;
                }

                if (int.TryParse(paramIVA.Valor, out int tasaIVA))
                {
                    TasaIVA = tasaIVA;
                }
                else
                {
                    await ShowNotification("La tasa del IVA configurada no es válida");
                    return;
                }


            }
            catch (Exception ex)
            {
                HandleError(ex, "crear nuevo Registro");
            }

            await base.New();
        }

        public override async Task Edit()
        {
            try
            {
                EditMode = true;
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);


                var settings = new Settings();
                int userId = settings.UserId;
                //EditCopy.UsuarioEdicionId = userId;
            }
            catch (Exception ex)
            {
                HandleError(ex, "editar nuevo Registro");
            }
            await base.Edit();
        }
        public override async Task Create()
        {
            try
            {
                IsBusy = true;



                var validateForm = await ValidateForm();
                if (!validateForm.Success)
                {
                    await ShowNotificationMessage($"{validateForm.ErrorMessage}");
                    return;
                }

                if (CreationMode)
                {
                    // Lógica para nuevo Registro
                    //await _ClientesService.SaveClientesAsync(EditCopy);

                    await ShowNotification("Registro creado exitosamente");
                }
                else if (EditMode)
                {
                    // Lógica para edición
                    //await _ClientesService.UpdateClientesAsync(EditCopy);
                    //CurrentClientes = EditCopy; // Actualizar el original
                    await ShowNotificationMessage("Registro actualizado exitosamente");
                    await ShowNotification("Registro actualizado exitosamente");
                }

                // Finalizar modo
                CreationMode = false;
                EditMode = false;
                //_ClientesListViewModel.IsListEnabled = true;
                //await _ClientesListViewModel.LoadData();
                await base.Create();
            }
            catch (Exception ex)
            {
                await ShowNotification($"Error al guardar: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }

        }

        public override async Task Activate()
        {
            try
            {
                //await _ClientesService.ActivateClientesAsync(EditCopy);
                //EditCopy.Activo = true;
                //CurrentClientes.Activo = true;
                await ShowNotificationMessage("Registro activado exitosamente");
            }
            catch (Exception ex)
            {
                HandleError(ex, "eliminar Registro");
            }
            await base.Activate();
        }

        public override async Task Delete()
        {
            try
            {
                //await _ClientesService.DeleteClientesAsync(EditCopy);
                //EditCopy.Activo = false;
                //CurrentClientes.Activo = false;
                await ShowNotificationMessage("Registro desactivado exitosamente");
            }
            catch (Exception ex)
            {
                HandleError(ex, "eliminar Registro");
            }
            await base.Delete();

        }

        public override async Task Undo()
        {
            //// Restaurar valores originales
            //if (CurrentClientes != null)
            //{
            //    EditCopy = (ClientesItem)CurrentClientes.Clone();
            //}
            bool confirmado = await ShowConfirmationAsync(
            "Confirmar",
            "¿Está seguro de cancelar la edición del registro?");

            if (confirmado)
            {
                CreationMode = false;
                EditMode = false;
                //_ClientesListViewModel.IsListEnabled = true;
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);
                await base.Undo();
            }

            // Preparar las opciones
            //    var options = new List<OptionsItem>
            //{
            //     new OptionsItem { Id = 1, Code = "OPT1", Name = "Opción 1" },
            //     new OptionsItem { Id = 2, Code = "OPT2", Name = "Opción 2" },
            //     new OptionsItem { Id = 3, Code = "OPT3", Name = "Opción 3" },
            //     new OptionsItem { Id = 4, Code = "OPT4", Name = "Opción 4" },
            //     new OptionsItem { Id = 5, Code = "OPT5", Name = "Opción 5" },
            //};

            // // Versión 1: Selección múltiple
            // var opcionesSeleccionadas = await ShowOptionsDialogAsync(
            //     title: "Seleccione opciones",
            //     options: options,
            //     message: "Elija una o más opciones:",
            //     allowMultipleSelection: true);

            // if (opcionesSeleccionadas.Any())
            // {
            //     // Procesar múltiples selecciones
            // }

            // // Versión 2: Selección simple
            // var opcionSeleccionada = await ShowSingleOptionDialogAsync(
            //     title: "Seleccione una opción",
            //     options: options,
            //     message: "Elija solo una opción:");

            // if (opcionSeleccionada != null)
            // {
            //     // Procesar selección única
            // }

        }

        public override async Task Update()
        {
            //await _ClientesListViewModel.LoadData();
            await base.Update();
        }

        public async Task AddProduct()
        {

            try
            {
                // Configurar el diálogo
                var selectedProduct = await _productSelectionDialog.ShowDialogAsync(
                    title: "Seleccionar Producto",
                    message: "Haga doble clic en un producto para seleccionarlo",
                    configureList: list =>
                    {
                        list.PageSize = 10;
                        list.SearchText = "";
                        list.DisplayMemberPath = nameof(ProductosItem.Name);
                        list.CodeMemberPath = nameof(ProductosItem.Code);
                        _ = list.Search();
                    });

                if (selectedProduct != null)
                {
                    // Lógica para agregar el producto
                    if (SelectedProducts.Any(p => p.Id == selectedProduct.Id))
                    {
                        await ShowNotificationMessage(
                            "Este producto ya ha sido agregado");
                        return;
                    }

                    SelectedProducts.Add(selectedProduct);
                    CalculateTotals();
                }
            }
            catch (Exception ex)
            {
                await ShowNotificationMessage(
                    $"No se pudo agregar el producto: {ex.Message}");
            }

        }
        public async void RemoveProduct()
        {
            try
            {
                //if (prod == null) return;

                //try
                //{
                //    SelectedProducts.Remove(product);
                //    CalculateTotals();
                //}
                //catch (Exception ex)
                //{
                //    await _windowManager.ShowMessageBox("Error",
                //        $"No se pudo eliminar el producto: {ex.Message}");
                //}
            }
            catch (Exception ex)
            {
                HandleError(ex, "RemoveProduct");
            }
        }

        // Método para calcular totales (ejemplo)
        private void CalculateTotals()
        {
            // Implementa tu lógica de cálculo aquí
            // Ejemplo:
            //CurrentRepair.SubTotal = SelectedProducts.Sum(p => p.Price);
            //CurrentRepair.Tax = CurrentRepair.SubTotal * (TasaIVA / 100m);
            //CurrentRepair.Total = CurrentRepair.SubTotal + CurrentRepair.Tax;

            NotifyOfPropertyChange(() => CurrentRepair);
        }
        public override async Task<ValidateForm> ValidateForm()
        {
            ValidateForm validateForm = new ValidateForm();
            try
            {
                IsBusy = true;

                validateForm.Success = true;
                validateForm.ErrorMessage = string.Empty;

                //if (string.IsNullOrEmpty(EditCopy.Code))
                //{
                //    validateForm.Success = false;
                //    validateForm.ErrorMessage = "El código del Registro es obligatorio.";
                //    return validateForm;
                //}

                //if (string.IsNullOrEmpty(EditCopy.Name))
                //{
                //    validateForm.Success = false;
                //    validateForm.ErrorMessage = "El nombre del Registro es obligatorio.";
                //    return validateForm;
                //}

                //if (string.IsNullOrEmpty(EditCopy.Telefono))
                //{
                //    validateForm.Success = false;
                //    validateForm.ErrorMessage = "El número telefonico del Registro es obligatorio.";
                //    return validateForm;
                //}

                //if (CreationMode)
                //{
                //    if (string.IsNullOrEmpty(EditCopy.PasswordHash))
                //    {
                //        validateForm.Success = false;
                //        validateForm.ErrorMessage = "La contraseña del Registro es obligatorio.";
                //        return validateForm;
                //    }
                //}

                return validateForm;
            }
            catch (Exception ex)
            {
                validateForm.Success = false;
                validateForm.ErrorMessage = "Error al guardar la información.";
                await ShowNotification($"Error al guardar: {ex.Message}");
                return validateForm;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
