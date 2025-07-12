using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ServiciosService;
using ReparaStoreApp.WPF.Models;
using ReparaStoreApp.WPF.Properties;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Servicios
{
    public class ServiciosViewModel : BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiciosService _ServiciosService;
        private readonly IDataService<ServiciosItem> _ServiciosDataService;
        private readonly IMapper _mapper;
        private readonly GenericListViewModel<ServiciosItem> _ServiciosListViewModel;
        public GenericListViewModel<ServiciosItem> ServiciosList => _ServiciosListViewModel;

        private ServiciosItem _currentServicios;
        public ServiciosItem CurrentServicios
        {
            get => _currentServicios;
            set
            {
                _currentServicios = value;
                _editCopy = value != null ? (ServiciosItem)CurrentServicios.Clone() : new ServiciosItem();
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => EditCopy);
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);
            }
        }

        private ServiciosItem _editCopy;
        public ServiciosItem EditCopy
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

        public ServiciosViewModel(
                            IWindowManager windowManager,
                            IEventAggregator eventAggregator,
                            IDataService<ServiciosItem> ServiciosDataService,
                            IServiciosService ServiciosService,
                            IMapper mapper) : base(eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _ServiciosDataService = ServiciosDataService;
            _ServiciosService = ServiciosService;
            _mapper = mapper;

            _ServiciosListViewModel = new GenericListViewModel<ServiciosItem>(eventAggregator, ServiciosDataService, mapper)
            {
                DisplayMemberPath = nameof(ServiciosItem.Name),
                CodeMemberPath = nameof(ServiciosItem.Code),
                PageSize = 15,
                SearchFunction = async (searchText) => await _ServiciosDataService.SearchAsync(searchText, 1, 15),

                // Configurar el manejador de selección personalizado
                OnItemSelected = async (selectedItem) =>
                {
                    CurrentServicios = selectedItem;
                    await LoadServiciosDetails(selectedItem.Id);
                }
            };
        }

        private async Task LoadServiciosDetails(int ServiciosId)
        {
            try
            {
                var ServiciosDetails = await _ServiciosDataService.GetByIdAsync(ServiciosId);
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
                _ServiciosListViewModel.IsListEnabled = false;

                var settings = new Settings();
                int userId = settings.UserId;

                EditCopy = new ServiciosItem(); // Crear una copia vacía para edición
                EditCopy.Id = 0;
                EditCopy.UsuarioCreadorId = userId;
                EditCopy.Tipo = Entities.Models.Inventario.TipoItem.Servicio;
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
                _ServiciosListViewModel.IsListEnabled = false;

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
                    await _ServiciosService.SaveServiciosAsync(EditCopy);

                    await ShowNotification("Registro creado exitosamente");
                }
                else if (EditMode)
                {
                    // Lógica para edición
                    await _ServiciosService.UpdateServiciosAsync(EditCopy);
                    CurrentServicios = EditCopy; // Actualizar el original
                    await ShowNotificationMessage("Registro actualizado exitosamente");
                    await ShowNotification("Registro actualizado exitosamente");
                }

                // Finalizar modo
                CreationMode = false;
                EditMode = false;
                _ServiciosListViewModel.IsListEnabled = true;
                await _ServiciosListViewModel.LoadData();
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
                await _ServiciosService.ActivateServiciosAsync(EditCopy);
                EditCopy.Activo = true;
                CurrentServicios.Activo = true;
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
                await _ServiciosService.DeleteServiciosAsync(EditCopy);
                EditCopy.Activo = false;
                CurrentServicios.Activo = false;
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
            if (CurrentServicios != null)
            {
                EditCopy = (ServiciosItem)CurrentServicios.Clone();
            }

            CreationMode = false;
            EditMode = false;
            _ServiciosListViewModel.IsListEnabled = true;
            NotifyOfPropertyChange(() => IsInEditOrCreationMode);
            return base.Undo();
        }

        public override async Task Update()
        {
            await _ServiciosListViewModel.LoadData();
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

                if (EditCopy.Precio <= 0)
                {
                    validateForm.Success = false;
                    validateForm.ErrorMessage = "El precio debe ser mayor a 0.";
                    return validateForm;
                }

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

