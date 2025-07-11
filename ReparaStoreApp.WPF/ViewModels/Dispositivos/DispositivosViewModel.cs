using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ClientesService;
using ReparaStoreApp.Core.Services.DispositivosService;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.WPF.Models;
using ReparaStoreApp.WPF.Properties;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Dispositivos
{
    public class DispositivosViewModel : BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDispositivosService _DispositivosService;
        private readonly IClientesService _ClientesService;
        private readonly IDataService<DispositivosItem> _DispositivosDataService;
        private readonly IMapper _mapper;
        private readonly GenericListViewModel<DispositivosItem> _DispositivosListViewModel;
        public GenericListViewModel<DispositivosItem> DispositivosList => _DispositivosListViewModel;

        private DispositivosItem _currentDispositivos;
        public DispositivosItem CurrentDispositivos
        {
            get => _currentDispositivos;
            set
            {
                _currentDispositivos = value;
                _editCopy = value != null ? (DispositivosItem)CurrentDispositivos.Clone() : new DispositivosItem();
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => EditCopy);
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);
            }
        }

        private DispositivosItem _editCopy;
        public DispositivosItem EditCopy
        {
            get => _editCopy;
            set
            {
                _editCopy = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsInEditOrCreationMode => CreationMode || EditMode;

        private DateTime _fechaActual;

        public DateTime FechaActual
        {
            get { return _fechaActual; }
            set { _fechaActual = value; NotifyOfPropertyChange(() => FechaActual); }
        }

        private BindableCollection<ClientesItem> _vailableClients;
        public BindableCollection<ClientesItem> AvailableClients
        {
            get { return _vailableClients; }
            set { _vailableClients = value; NotifyOfPropertyChange(() => AvailableClients); }
        }

        private ClientesItem _selectedClient;
        public ClientesItem SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; NotifyOfPropertyChange(() => SelectedClient); }
        }


        public DispositivosViewModel(
                            IWindowManager windowManager,
                            IEventAggregator eventAggregator,
                            IDataService<DispositivosItem> DispositivosDataService,
                            IDispositivosService DispositivosService,
                            IClientesService ClientesService,
                            IMapper mapper) : base(eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _DispositivosDataService = DispositivosDataService;
            _DispositivosService = DispositivosService;
            _ClientesService = ClientesService;
            _mapper = mapper;

            _DispositivosListViewModel = new GenericListViewModel<DispositivosItem>(eventAggregator, DispositivosDataService, mapper)
            {
                DisplayMemberPath = nameof(DispositivosItem.Name),
                CodeMemberPath = nameof(DispositivosItem.Code),
                PageSize = 15,
                SearchFunction = async (searchText) => await _DispositivosDataService.SearchAsync(searchText, 1, 15),

                // Configurar el manejador de selección personalizado
                OnItemSelected = async (selectedItem) =>
                {
                    CurrentDispositivos = selectedItem;
                    await LoadDispositivosDetails(selectedItem.Id);
                }
            };

            CargarClientes();
        }

        public async Task CargarClientes()
        {
            try
            {
                var ClientesDetails = await _ClientesService.SearchClientessAsync("", 0, 100);
                AvailableClients =  _mapper.Map<BindableCollection<ClientesItem>>(ClientesDetails);
                
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage("Error al cargar detalles del Registro"));
            }

        }

        private async Task LoadDispositivosDetails(int DispositivosId)
        {
            try
            {
                var DispositivosDetails = await _DispositivosDataService.GetByIdAsync(DispositivosId);
                // Actualiza las propiedades necesarias aquí
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage("Error al cargar detalles del Registro"));
            }
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
        }

        public override Task New()
        {
            try
            {
                CreationMode = true;
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);

                // Deshabilitar la lista durante la edición
                _DispositivosListViewModel.IsListEnabled = false;

                var settings = new Settings();
                int userId = settings.UserId;

                EditCopy = new DispositivosItem(); // Crear una copia vacía para edición
                EditCopy.Id = 0;
                //EditCopy.FechaNacimiento = new DateTime(1990, 1, 1);
                EditCopy.UsuarioCreadorId = userId;

            }
            catch (Exception ex)
            {
                HandleError(ex, "crear nuevo Registro");
            }

            return base.New();
        }

        public override Task Edit()
        {
            try
            {
                EditMode = true;
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);
                // Deshabilitar la lista durante la edición
                _DispositivosListViewModel.IsListEnabled = false;

                var settings = new Settings();
                int userId = settings.UserId;
                EditCopy.UsuarioEdicionId = userId;
            }
            catch (Exception ex)
            {
                HandleError(ex, "editar nuevo Registro");
            }
            return base.Edit();
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
                    await _DispositivosService.SaveDispositivosAsync(EditCopy);

                    await ShowNotification("Registro creado exitosamente");
                }
                else if (EditMode)
                {
                    // Lógica para edición
                    await _DispositivosService.UpdateDispositivosAsync(EditCopy);
                    CurrentDispositivos = EditCopy; // Actualizar el original
                    await ShowNotificationMessage("Registro actualizado exitosamente");
                    await ShowNotification("Registro actualizado exitosamente");
                }

                // Finalizar modo
                CreationMode = false;
                EditMode = false;
                _DispositivosListViewModel.IsListEnabled = true;
                await _DispositivosListViewModel.LoadData();
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
                await _DispositivosService.ActivateDispositivosAsync(EditCopy);
                EditCopy.Activo = true;
                CurrentDispositivos.Activo = true;
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
                await _DispositivosService.DeleteDispositivosAsync(EditCopy);
                EditCopy.Activo = false;
                CurrentDispositivos.Activo = false;
                await ShowNotificationMessage("Registro desactivado exitosamente");
            }
            catch (Exception ex)
            {
                HandleError(ex, "eliminar Registro");
            }
            await base.Delete();

        }

        public override Task Undo()
        {
            // Restaurar valores originales
            if (CurrentDispositivos != null)
            {
                EditCopy = (DispositivosItem)CurrentDispositivos.Clone();
            }

            CreationMode = false;
            EditMode = false;
            _DispositivosListViewModel.IsListEnabled = true;
            NotifyOfPropertyChange(() => IsInEditOrCreationMode);
            return base.Undo();
        }

        public override async Task Update()
        {
            await _DispositivosListViewModel.LoadData();
            await base.Update();
        }

        public override async Task<ValidateForm> ValidateForm()
        {
            ValidateForm validateForm = new ValidateForm();
            try
            {
                IsBusy = true;

                validateForm.Success = true;
                validateForm.ErrorMessage = string.Empty;

                if (string.IsNullOrEmpty(EditCopy.Code))
                {
                    validateForm.Success = false;
                    validateForm.ErrorMessage = "El código del Registro es obligatorio.";
                    return validateForm;
                }

                if (string.IsNullOrEmpty(EditCopy.Name))
                {
                    validateForm.Success = false;
                    validateForm.ErrorMessage = "El nombre del Registro es obligatorio.";
                    return validateForm;
                }

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
