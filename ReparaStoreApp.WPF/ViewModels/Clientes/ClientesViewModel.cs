﻿using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ClientesService;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.WPF.Models;
using ReparaStoreApp.WPF.Properties;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System.Configuration;

namespace ReparaStoreApp.WPF.ViewModels.Clientes
{
    public class ClientesViewModel : BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientesService _ClientesService;
        private readonly IDataService<ClientesItem> _ClientesDataService;
        private readonly IMapper _mapper;
        private readonly GenericListViewModel<ClientesItem> _ClientesListViewModel;
        public GenericListViewModel<ClientesItem> ClientesList => _ClientesListViewModel;

        private ClientesItem _currentClientes;
        public ClientesItem CurrentClientes
        {
            get => _currentClientes;
            set
            {
                _currentClientes = value;
                _editCopy = value != null ? (ClientesItem)CurrentClientes.Clone() : new ClientesItem();
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => EditCopy);
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);
            }
        }

        private ClientesItem _editCopy;
        public ClientesItem EditCopy
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

        public ClientesViewModel(
                            IWindowManager windowManager,
                            IEventAggregator eventAggregator,
                            IDataService<ClientesItem> ClientesDataService,
                            IClientesService ClientesService,
                            IMapper mapper) : base(eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _ClientesDataService = ClientesDataService;
            _ClientesService = ClientesService;
            _mapper = mapper;

            _ClientesListViewModel = new GenericListViewModel<ClientesItem>(eventAggregator, ClientesDataService, mapper)
            {
                DisplayMemberPath = nameof(ClientesItem.Name),
                CodeMemberPath = nameof(ClientesItem.Code),
                PageSize = 15,
                SearchFunction = async (searchText) => await _ClientesDataService.SearchAsync(searchText, 1, 15),

                // Configurar el manejador de selección personalizado
                OnItemSelected = async (selectedItem) =>
                {
                    CurrentClientes = selectedItem;
                    await LoadClientesDetails(selectedItem.Id);
                }
            };
        }

        private async Task LoadClientesDetails(int ClientesId)
        {
            try
            {
                var ClientesDetails = await _ClientesDataService.GetByIdAsync(ClientesId);
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
                _ClientesListViewModel.IsListEnabled = false;

                var settings = new Settings();
                int userId = settings.UserId;

                EditCopy = new ClientesItem(); // Crear una copia vacía para edición
                EditCopy.Id = 0;
                EditCopy.FechaNacimiento = new DateTime(1990, 1, 1);
                EditCopy.UsuarioCreadorId = userId;
                EditCopy.Activo = true;

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
                _ClientesListViewModel.IsListEnabled = false;

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
                    await _ClientesService.SaveClientesAsync(EditCopy);

                    await ShowNotification("Registro creado exitosamente");
                }
                else if (EditMode)
                {
                    // Lógica para edición
                    await _ClientesService.UpdateClientesAsync(EditCopy);
                    CurrentClientes = EditCopy; // Actualizar el original
                    await ShowNotificationMessage("Registro actualizado exitosamente");
                    await ShowNotification("Registro actualizado exitosamente");
                }

                // Finalizar modo
                CreationMode = false;
                EditMode = false;
                _ClientesListViewModel.IsListEnabled = true;
                await _ClientesListViewModel.LoadData();
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
                await _ClientesService.ActivateClientesAsync(EditCopy);
                EditCopy.Activo = true;
                CurrentClientes.Activo = true;
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
                await _ClientesService.DeleteClientesAsync(EditCopy);
                EditCopy.Activo = false;
                CurrentClientes.Activo = false;
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
            if (CurrentClientes != null)
            {
                EditCopy = (ClientesItem)CurrentClientes.Clone();
            }

            CreationMode = false;
            EditMode = false;
            _ClientesListViewModel.IsListEnabled = true;
            NotifyOfPropertyChange(() => IsInEditOrCreationMode);
            return base.Undo();
        }

        public override async Task Update()
        {
            await _ClientesListViewModel.LoadData();
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

                if (string.IsNullOrEmpty(EditCopy.Telefono))
                {
                    validateForm.Success = false;
                    validateForm.ErrorMessage = "El número telefonico del Registro es obligatorio.";
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

        public async Task FormatNombres()
        {
            try
            {
                if (EditCopy == null) { return; }

                var PrimerNombre = (EditCopy?.PrimerNombre ?? "").Trim();
                var SegundoNombre = (EditCopy?.SegundoNombre ?? "").Trim();
                var PrimerApellido = (EditCopy?.PrimerApellido ?? "").Trim();
                var SegundoApellido = (EditCopy?.SegundoApellido ?? "").Trim();

                EditCopy.Name = $"{PrimerNombre} {SegundoNombre} {PrimerApellido} {SegundoApellido}";
            }
            catch (Exception)
            {

            }
        }
    }
}
