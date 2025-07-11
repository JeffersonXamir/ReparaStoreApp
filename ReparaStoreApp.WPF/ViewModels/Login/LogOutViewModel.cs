using Caliburn.Micro;
using ReparaStoreApp.WPF.Helpers;
using ReparaStoreApp.WPF.Properties;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Login
{
    

    public class LogOutViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public LogOutViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public async void CerrarSesion()
        {
            await Task.Delay(1200); // Espera visual

            var settings = new Settings();
            settings.AuthToken = null;
            settings.UserId = 0;
            settings.Save();

            await _eventAggregator.PublishOnUIThreadAsync(new SessionClosedEvent());
        }
    }

}
