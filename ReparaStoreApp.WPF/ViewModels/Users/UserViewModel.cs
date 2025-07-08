using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Security.Security;
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
                _editCopy = value != null ? CloneUserItem(value) : new UserItem();
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
                // Deshabilitar la lista durante la edición
                _userListViewModel.IsListEnabled = false;

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
        public override Task Create()
        {
            try
            {
                IsBusy = true;

                if (CreationMode)
                {
                    // Lógica para nuevo usuario
                    //var newUser = await _userDataService.SaveAsync(EditCopy);
                    //CurrentUser = newUser;
                    ShowNotification("Usuario creado exitosamente");
                }
                else if (EditMode)
                {
                    // Lógica para edición
                    _userService.UpdateUserAsync(EditCopy);
                    CurrentUser = EditCopy; // Actualizar el original
                    ShowNotification("Usuario actualizado exitosamente");
                }

                // Finalizar modo
                CreationMode = false;
                EditMode = false;
                _userListViewModel.IsListEnabled = true;
            }
            catch (Exception ex)
            {
                ShowNotification($"Error al guardar: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
            return base.Create();

        }

        public override Task Delete()
        {
            try
            {
                // Lógica específica para eliminar usuario
            }
            catch (Exception ex)
            {
                HandleError(ex, "eliminar usuario");
            }
            return base.Delete();

        }

        public override Task Undo()
        {
            // Restaurar valores originales
            if (CurrentUser != null)
            {
                EditCopy = CloneUserItem(CurrentUser);
            }

            CreationMode = false;
            EditMode = false;
            _userListViewModel.IsListEnabled = true;

            return base.Undo();
        }
    }
}
