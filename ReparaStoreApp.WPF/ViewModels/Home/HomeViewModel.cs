using Caliburn.Micro;
using ReparaStoreApp.Security.Security;
using ReparaStoreApp.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IWindowManager _windowManager;
        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; NotifyOfPropertyChange(() => Titulo); }
        }

        public string WelcomeMessage { get; } = "Panel de control principal de la aplicación";

        public List<QuickStat> QuickStats { get; } = new()
        {
            new QuickStat("Clientes", "125", "+5 este mes"),
            new QuickStat("Reparaciones", "42", "3 pendientes"),
            new QuickStat("Ingresos", "$8,450", "+12% vs mes pasado")
        };

        public HomeViewModel(IAuthService authService, IWindowManager windowManager, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _authService = authService;
            _windowManager = windowManager;

            Titulo = "Inicio";
        }

        public async Task ShowStatistics()
        {
            // Lógica para mostrar estadísticas detalladas
            await Task.Delay(100); // Simula operación asíncrona
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

        public override Task<ValidateForm> ValidateForm()
        {
            throw new NotImplementedException();
        }
    }

    public record QuickStat(string Title, string Value, string Description);
}
