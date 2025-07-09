using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Security.Security;
using ReparaStoreApp.WPF.Models;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Users
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserService _userService;
        private readonly IDataService<UserItem> _userDataService;
        private readonly IMapper _mapper;
        private readonly GenericListViewModel<UserItem> _userListViewModel;
        public GenericListViewModel<UserItem> UserList => _userListViewModel;

        private UserItem _currentUser;
        public UserItem CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                _editCopy = value != null ? (UserItem)CurrentUser.Clone() : new UserItem();
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => EditCopy);
                NotifyOfPropertyChange(() => IsInEditOrCreationMode);
            }
        }

        private UserItem _editCopy;
        public UserItem EditCopy
        {
            get => _editCopy;
            set
            {
                _editCopy = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsInEditOrCreationMode => CreationMode || EditMode;


        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; NotifyOfPropertyChange(() => Titulo); }
        }

        public UserViewModel(IAuthService authService,
                            IWindowManager windowManager,
                            IEventAggregator eventAggregator,
                            IDataService<UserItem> userDataService,
                            IUserService userService,
                            IMapper mapper) : base(eventAggregator)
        {
            _authService = authService;
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _userDataService = userDataService;
            _userService = userService;
            _mapper = mapper;

            Titulo = "Gestión de Usuarios";

            _userListViewModel = new GenericListViewModel<UserItem>(eventAggregator, userDataService, mapper)
            {
                DisplayMemberPath = nameof(UserItem.Name),
                CodeMemberPath = nameof(UserItem.Code),
                PageSize = 15,
                SearchFunction = async (searchText) => await _userDataService.SearchAsync(searchText, 1, 15),

                // Configurar el manejador de selección personalizado
                OnItemSelected = async (selectedItem) =>
                {
                    CurrentUser = selectedItem;
                    await LoadUserDetails(selectedItem.Id);
                }
            };
        }


        private UserItem CloneUserItem(UserItem source)
        {
            return new UserItem
            {
                Id = source.Id,
                Code = source.Code,
                Name = source.Name,
                //Email = source.Email
                // Copiar todas las propiedades relevantes
            };
        }




        private async Task LoadUserDetails(int userId)
        {
            try
            {
                var userDetails = await _userDataService.GetByIdAsync(userId);
                // Actualiza las propiedades necesarias aquí
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage("Error al cargar detalles del usuario"));
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
                _userListViewModel.IsListEnabled = false;

                EditCopy = new UserItem(); // Crear una copia vacía para edición
                EditCopy.Id = 0;

                // Lógica específica para nuevo usuario
                // Ejemplo: _windowManager.ShowDialogAsync(new NewUserViewModel());
            }
            catch (Exception ex)
            {
                HandleError(ex, "crear nuevo usuario");
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
                _userListViewModel.IsListEnabled = false;

                // Lógica específica para nuevo usuario
                // Ejemplo: _windowManager.ShowDialogAsync(new NewUserViewModel());
            }
            catch (Exception ex)
            {
                HandleError(ex, "editar nuevo usuario");
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
                    // Lógica para nuevo usuario
                    await _userService.SaveUserAsync(EditCopy);
                    
                    await ShowNotification("Usuario creado exitosamente");
                }
                else if (EditMode)
                {
                    // Lógica para edición
                    await _userService.UpdateUserAsync(EditCopy);
                    CurrentUser = EditCopy; // Actualizar el original
                    await ShowNotificationMessage("Usuario actualizado exitosamente");
                    await ShowNotification("Usuario actualizado exitosamente");
                }

                // Finalizar modo
                CreationMode = false;
                EditMode = false;
                _userListViewModel.IsListEnabled = true;
                await _userListViewModel.LoadData();
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
                await _userService.ActivateUserAsync(EditCopy);
                EditCopy.IsActive = true;
                CurrentUser.IsActive = true;
                await ShowNotificationMessage("Registro activado exitosamente");
            }
            catch (Exception ex)
            {
                HandleError(ex, "eliminar usuario");
            }
            await base.Activate();
        }

        public override async Task Delete()
        {
            try
            {
                await _userService.DeleteUserAsync(EditCopy);
                EditCopy.IsActive = false;
                CurrentUser.IsActive = false;
                await ShowNotificationMessage("Registro desactivado exitosamente");
            }
            catch (Exception ex)
            {
                HandleError(ex, "eliminar usuario");
            }
            await base.Delete();

        }

        public override Task Undo()
        {
            // Restaurar valores originales
            if (CurrentUser != null)
            {
                EditCopy = (UserItem)CurrentUser.Clone();
            }

            CreationMode = false;
            EditMode = false;
            _userListViewModel.IsListEnabled = true;

            return base.Undo();
        }

        public override async Task Update()
        {
            await _userListViewModel.LoadData();
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
                    validateForm.ErrorMessage = "El código del usuario es obligatorio.";
                    return validateForm;
                }

                if (string.IsNullOrEmpty(EditCopy.Name))
                {
                    validateForm.Success = false;
                    validateForm.ErrorMessage = "El nombre del usuario es obligatorio.";
                    return validateForm;
                }

                if (string.IsNullOrEmpty(EditCopy.PhoneNumber))
                {
                    validateForm.Success = false;
                    validateForm.ErrorMessage = "El número telefonico del usuario es obligatorio.";
                    return validateForm;
                }

                if (CreationMode)
                {
                    if (string.IsNullOrEmpty(EditCopy.PasswordHash))
                    {
                        validateForm.Success = false;
                        validateForm.ErrorMessage = "La contraseña del usuario es obligatorio.";
                        return validateForm;
                    }
                }

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
