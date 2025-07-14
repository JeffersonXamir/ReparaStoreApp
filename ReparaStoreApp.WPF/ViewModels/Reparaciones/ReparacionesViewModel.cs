using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ClientesService;
using ReparaStoreApp.Core.Services.DispositivosService;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Core.Services.ReparacionesService;
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
        private readonly IReparacionesService _ReparacionesService;
        private readonly IUserService _UserService;
        private readonly IDataService<ClientesItem> _ClientesDataService;
        private readonly GenericSelectionDialogViewModel<ProductosItem> _productSelectionDialog;
        private readonly DocumentSelectionDialogViewModel<ReparacionItem> _documentDialog;
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
            set { _deviceSelect = value; NotifyOfPropertyChange(() => DeviceSelect); _= OnSelectDispositivo(); }
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

        private ReparacionDetalleItem _selectedDetail;
        public ReparacionDetalleItem SelectedDetail
        {
            get { return _selectedDetail; }
            set { _selectedDetail = value; NotifyOfPropertyChange(() => SelectedDetail); }
        }


        private decimal _totalDocument;
        public decimal TotalDocument
        {
            get { return _totalDocument; }
            set { _totalDocument = value; NotifyOfPropertyChange(() => TotalDocument); }
        }

        private decimal _totalIVADocument;
        public decimal TotalIVADocument
        {
            get { return _totalIVADocument; }
            set { _totalIVADocument = value; NotifyOfPropertyChange(() => TotalIVADocument); }
        }

        private decimal _subTotalDocument;
        public decimal SubTotalDocument
        {
            get { return _subTotalDocument; }
            set { _subTotalDocument = value; NotifyOfPropertyChange(() => SubTotalDocument); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; NotifyOfPropertyChange(()=> IsLoading); }
        }

        public ReparacionesViewModel(
                            IWindowManager windowManager,
                            IEventAggregator eventAggregator,
                            IDataService<ClientesItem> ClientesDataService,
                            IClientesService ClientesService,
                            IDispositivosService DispositivosService,
                            IReparacionesService ReparacionesService,
                            IUserService UserService,
                            GenericSelectionDialogViewModel<ProductosItem> ProductSelectionDialog,
                            DocumentSelectionDialogViewModel<ReparacionItem> documentDialog,
                            IMapper mapper) : base(eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _ClientesDataService = ClientesDataService;
            _ClientesService = ClientesService;
            _DispositivosService = DispositivosService;
            _ReparacionesService = ReparacionesService;
            _UserService = UserService;
            _productSelectionDialog = ProductSelectionDialog;
            _documentDialog = documentDialog;
            _mapper = mapper;

            AvailableClients = new BindableCollection<ClientesItem>();
            SelectedDetail = new ReparacionDetalleItem();

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
                if (IsLoading) return;
                if ((DeviceSelect?.ClienteId ?? 0) == 0) return;

                var item = await _ClientesService.GetClientesByIdAsync(DeviceSelect.ClienteId);
                if (item == null)
                    throw new Exception("Error");

                var itemMap = _mapper.Map<ClientesItem>(item);
                if (itemMap != null)
                    await CargarClienteDispositivo(itemMap);

                CurrentRepair.DispositivoId = DeviceSelect.Id;
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
                AvailableClients?.Clear();
                AvailableClients?.Add(cliente);
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
                new EstadosReparacionItem { Id = 1, Code = "001", Name = "Ingresado", Estado = EstadoReparacion.Ingresado },
                new EstadosReparacionItem { Id = 2, Code = "002", Name = "Pendiente", Estado = EstadoReparacion.Pendiente },
                new EstadosReparacionItem { Id = 3, Code = "003", Name = "Aprobado", Estado = EstadoReparacion.Aprobado},
                new EstadosReparacionItem { Id = 4, Code = "004", Name = "En Proceso", Estado = EstadoReparacion.EnProceso },
                new EstadosReparacionItem { Id = 5, Code = "005", Name = "Completado", Estado = EstadoReparacion.Completado },
                new EstadosReparacionItem { Id = 6, Code = "006", Name = "Rechazado", Estado = EstadoReparacion.Rechazado }
            };
            await Task.CompletedTask;
        }

        private async Task OnSelectState()
        {
            try
            {
                if (RepairStateSelect == null) return;

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

                await ClearForm();

                var settings = new Settings();
                int userId = settings.UserId;

                CurrentRepair = new ReparacionItem(); // Crear una copia vacía para edición
                CurrentRepair.Id = 0;
                CurrentRepair.Numero = "000000000";
                CurrentRepair.Fecha = DateTime.Now.Date;
                CurrentRepair.FechaCreacion = DateTime.Now;
                CurrentRepair.UsuarioCreadorId = userId;
                CurrentRepair.CajeroId = userId;
                //CurrentRepair.Estado = EstadoReparacion.Aprobado;

                CurrentRepair.Detalles?.Clear();

                RepairStateSelect = RepairStates?.FirstOrDefault(x => x.Estado == EstadoReparacion.Ingresado);

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
                    var response = await _ReparacionesService.CreateAsync(CurrentRepair);
                    if (!response.Success)
                    {
                        await ShowNotification(response.Error);
                        return;
                    }

                    await ShowNotification("Registro creado exitosamente");
                }
                else if (EditMode)
                {
                    // Lógica para edición
                    await _ReparacionesService.UpdateAsync(CurrentRepair);
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
                await ClearForm();
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
                    if (CurrentRepair.Detalles.Any(p => p.ItemId == selectedProduct.Id))
                    {
                        await ShowNotificationMessage(
                            "Este producto ya ha sido agregado");
                        return;
                    }

                    var detalle = new ReparacionDetalleItem();
                    detalle.Id = 0;
                    detalle.ReparacionId = CurrentRepair.Id;
                    detalle.ItemId = selectedProduct.Id;
                    detalle.Item = new Entities.Models.Inventario.ItemEntity();
                    detalle.Item.Id = selectedProduct.Id;
                    detalle.Item.Codigo = selectedProduct.Code;
                    detalle.Item.Nombre = selectedProduct.Name;
                    detalle.Cantidad = 1;
                    detalle.Activo = true;
                    detalle.PrecioUnitario = selectedProduct.Precio;
                    detalle.TasaIVA = TasaIVA;
                    detalle.TotalIVA = ((detalle.Cantidad * detalle.PrecioUnitario) * detalle.TasaIVA / 100);
                    detalle.SubTotal = (detalle.Cantidad * detalle.PrecioUnitario);
                    detalle.Total = detalle.SubTotal + detalle.TotalIVA;
                    detalle.FechaCreacion = DateTime.Now;
                    detalle.UsuarioCreadorId = CurrentRepair.UsuarioCreadorId;

                    CurrentRepair.Detalles.Add(detalle);
                    NotifyOfPropertyChange(() => CurrentRepair.Detalles);
                    NotifyOfPropertyChange(() => CurrentRepair);
                    //SelectedProducts.Add(selectedProduct);
                    await CalculateTotals();
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
                if (SelectedDetail == null) return;

                try
                {
                    CurrentRepair.Detalles?.Remove(SelectedDetail);
                    await CalculateTotals();
                }
                catch (Exception ex)
                {
                    await ShowNotificationMessage($"No se pudo eliminar el producto: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                HandleError(ex, "RemoveProduct");
            }
        }

        // Método para calcular totales
        public async Task CalculateTotals()
        {
            var subtotal = CurrentRepair.Detalles.Sum(p => p.SubTotal);
            var totalIVA = CurrentRepair.Detalles.Sum(P => P.TotalIVA);
            var total = CurrentRepair.Detalles.Sum(P => P.Total);
            CurrentRepair.CostoEstimado = total;

            SubTotalDocument = subtotal;
            TotalIVADocument = totalIVA;
            TotalDocument = total;

            NotifyOfPropertyChange(() => CurrentRepair);
            await Task.CompletedTask;
        }

        public async Task ClearForm()
        {
            try
            {
                CurrentRepair?.Detalles?.Clear();
                CurrentRepair = new ReparacionItem();

                TechnicianSelect = null;
                ClientSelect = null;
                DeviceSelect = null;
                RepairStateSelect = null;
            }
            catch (Exception ex)
            {
                await ShowNotificationMessage($"No se pudo limpiar el formulario: {ex.Message}");
            }

        }

        public async Task SearchDocument()
        {
            var selectedDocument = await _documentDialog.ShowDialogAsync(
                "Seleccionar Documento",
                "Seleccione un documento de la lista");

            if (selectedDocument != null)
            {
                //CurrentRepair.Numero = selectedDocument.Numero;
                //CurrentRepair.DocumentoId = selectedDocument.Id;
                // Actualizar cualquier otra propiedad relacionada con el documento

                await LoadDocument(selectedDocument.Id);
            }
        }

        public async Task LoadDocument(int documentId)
        {
            try
            {
                //IsLoading = true;
                await ClearForm();

                await CargarEstados();
                await CargarDispositivos();
                await CargarTecnicos();

                if (documentId == 0) return;

                var response = await _ReparacionesService.GetByIdAsync(documentId);
                if (response == null) throw new Exception("No se pudo obtener el documento.");

                CurrentRepair = response;

                var Item = AvailableDevices;
                TechnicianSelect = _mapper.Map<UserItem>(response.Tecnico);
                //ClientSelect = _mapper.Map<ClientesItem>(response.Tecnico);
                DeviceSelect = AvailableDevices?.FirstOrDefault(x => x.Id == response.Dispositivo.Id);
                //ClientSelect = _mapper.Map<ClientesItem>(response.Dispositivo.Cliente);
                RepairStateSelect = RepairStates?.FirstOrDefault(x => x.Estado == response.Estado);

                await CalculateTotals();
                IsLoading = false;

            }
            catch (Exception ex)
            {
                await ShowNotificationMessage($"No se pudo cargar el formulario.");
                await ShowNotification($"No se pudo cargar el formulario: {ex.Message}");
            }
            finally { IsLoading = false; }
        }


        public override async Task<ValidateForm> ValidateForm()
        {
            ValidateForm validateForm = new ValidateForm();
            try
            {
                IsBusy = true;

                validateForm.Success = true;
                validateForm.ErrorMessage = string.Empty;

                if (CurrentRepair.DispositivoId == 0)
                {
                    validateForm.Success = false;
                    validateForm.ErrorMessage = "Debe seleccionar un dispositivo.";
                    return validateForm;
                }

                if ((CurrentRepair.TecnicoId ?? 0) == 0)
                {
                    validateForm.Success = false;
                    validateForm.ErrorMessage = "Debe seleccionar un técnico.";
                    return validateForm;
                }

                switch (CurrentRepair.Estado)
                {
                    case EstadoReparacion.Ingresado:

                        if (CurrentRepair.Detalle?.Length == 0 || string.IsNullOrEmpty(CurrentRepair.Detalle))
                        {
                            validateForm.Success = false;
                            validateForm.ErrorMessage = "Debe ingresar una descripción.";
                            return validateForm;
                        }

                        if (CurrentRepair.NotasIngreso?.Length == 0 || string.IsNullOrEmpty(CurrentRepair.NotasIngreso))
                        {
                            validateForm.Success = false;
                            validateForm.ErrorMessage = "Debe ingresar una nota de ingreso o problema del dispositivo.";
                            return validateForm;
                        }

                        break;
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
