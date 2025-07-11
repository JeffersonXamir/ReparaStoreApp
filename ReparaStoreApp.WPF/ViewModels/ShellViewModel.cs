using Caliburn.Micro;
using ReparaStoreApp.WPF.ViewModels.Login;
using ReparaStoreApp.WPF.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;

        private bool _isLoggedIn;

        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;

            // Mostrar la pantalla de login al inicio
            //ShowLogin();
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            // Mostrar la pantalla de login al inicio
            //ShowLogin();
            OnLoginSuccess();
            return base.OnActivateAsync(cancellationToken);
        }

        private void ShowLogin()
        {
            var loginVM = IoC.Get<LoginViewModel>();
            ActivateItemAsync(loginVM);
            _isLoggedIn = false;
        }

        public void OnLoginSuccess()
        {
            _isLoggedIn = true;
            var dashboardVM = IoC.Get<MainViewModel>();
            ActivateItemAsync(dashboardVM);
        }
    }
}
