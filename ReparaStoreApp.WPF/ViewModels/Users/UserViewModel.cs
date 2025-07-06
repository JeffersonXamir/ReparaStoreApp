using Caliburn.Micro;
using ReparaStoreApp.Security.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Users
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IWindowManager _windowManager;
        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; NotifyOfPropertyChange(() => Titulo); }
        }

        public UserViewModel(IAuthService authService, IWindowManager windowManager, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _authService = authService;
            _windowManager = windowManager;

            Titulo = "Gestión de Usuarios";
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
        }

        public override Task New()
        {
            try
            {
                // Lógica específica para nuevo usuario
                // Ejemplo: _windowManager.ShowDialogAsync(new NewUserViewModel());
            }
            catch (Exception ex)
            {
                HandleError(ex, "crear nuevo usuario");
            }

            return base.New();
        }
        public override Task Create()
        {
            try
            {
                IsBusy = true;
                // Lógica para guardar el usuario
                ShowNotification("Usuario guardado exitosamente");
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
            return base.Undo();
        }
    }
}
