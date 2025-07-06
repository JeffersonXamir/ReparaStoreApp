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

        public UserViewModel(IAuthService authService, IWindowManager windowManager)
        {
            _authService = authService;
            _windowManager = windowManager;

            Titulo = "Gestión de Usuarios";
        }

        public override void New()
        {
            try
            {
                base.New();
                // Lógica específica para nuevo usuario
                // Ejemplo: _windowManager.ShowDialogAsync(new NewUserViewModel());
            }
            catch (Exception ex)
            {
                HandleError(ex, "crear nuevo usuario");
            }
        }

        public override void Delete()
        {
            try
            {
                base.Delete();
                // Lógica específica para eliminar usuario
            }
            catch (Exception ex)
            {
                HandleError(ex, "eliminar usuario");
            }
        }
    }
}
