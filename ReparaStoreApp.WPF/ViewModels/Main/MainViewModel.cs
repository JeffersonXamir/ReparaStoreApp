using Caliburn.Micro;
using ReparaStoreApp.Security.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Main
{
    public  class MainViewModel : Screen
    {
        private readonly IAuthService _authService;
        private readonly IWindowManager _windowManager;

        public MainViewModel(IAuthService authService, IWindowManager windowManager)
        {
            _authService = authService;
            _windowManager = windowManager;
        }

        public void ShowHome()
        {
            //ActivateItemAsync(new HomeViewModel());
        }

        public void ShowCustomers()
        {
            //ActivateItemAsync(new CustomersViewModel());
        }
    }
}
