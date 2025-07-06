using Caliburn.Micro;
using ReparaStoreApp.Security.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Users
{
    public class UserViewModel : Screen
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

    }
}
