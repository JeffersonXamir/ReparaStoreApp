using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Main
{
    public  class MainViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private string _welcomeMessage;

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            WelcomeMessage = "Bienvenido a la aplicación";

            // Suscribirse a eventos si es necesario
            _eventAggregator.Subscribe(this);
        }

        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set { Set(ref _welcomeMessage, value); }
        }

        // Ejemplo de comando para navegar a otra vista
        public void ShowOtherView()
        {
            // Implementar lógica de navegación
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        //protected override void OnDeactivate(bool close)
        //{
        //    // Limpieza si es necesario
        //    _eventAggregator.Unsubscribe(this);
        //    base.OnDeactivate(close);
        //}
    }
}
