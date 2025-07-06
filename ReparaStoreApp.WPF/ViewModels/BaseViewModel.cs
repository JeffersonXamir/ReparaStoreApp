using Caliburn.Micro;
using System;

namespace ReparaStoreApp.WPF.ViewModels
{
    public abstract class BaseViewModel : Screen
    {
        private string _statusMessage;
        private bool _isBusy;
        private CancellationTokenSource _statusMessageCts;
        

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                NotifyOfPropertyChange(() => StatusMessage);

                // Cancelar el timeout anterior si existe
                _statusMessageCts?.Cancel();

                // Iniciar nuevo timeout si hay mensaje
                if (!string.IsNullOrEmpty(value))
                {
                    _statusMessageCts = new CancellationTokenSource();
                    StartStatusMessageTimeout(_statusMessageCts.Token);
                }
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        // Métodos comunes que todos los ViewModels deben implementar
        //public virtual void New()
        //{
        //    StatusMessage = "Nuevo elemento creado";
        //    // Implementación específica en cada ViewModel
        //}

        //public virtual void Create()
        //{
        //    StatusMessage = "Creando elemento...";
        //    // Implementación específica en cada ViewModel
        //}

        //public virtual void Edit()
        //{
        //    StatusMessage = "Editando elemento...";
        //    // Implementación específica en cada ViewModel
        //}

        //public virtual void Delete()
        //{
        //    StatusMessage = "Eliminando elemento...";
        //    // Implementación específica en cada ViewModel
        //}

        //public virtual void Update()
        //{
        //    StatusMessage = "Actualizando...";
        //    // Implementación específica en cada ViewModel
        //}

        //public virtual void Print()
        //{
        //    StatusMessage = "Preparando para imprimir...";
        //    // Implementación específica en cada ViewModel
        //}

        //protected void ShowNotification(string message)
        //{
        //    StatusMessage = message;
        //}

        private async void StartStatusMessageTimeout(CancellationToken token)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(15), token);

                if (!token.IsCancellationRequested)
                {
                    StatusMessage = string.Empty;
                }
            }
            catch (TaskCanceledException)
            {
                // El timeout fue cancelado porque llegó un nuevo mensaje
            }
        }

        // Métodos virtuales con implementación base
        public virtual void New() => ShowNotification("Nuevo elemento creado");
        public virtual void Create() => ShowNotification("Creando elemento...");
        public virtual void Edit() => ShowNotification("Editando elemento...");
        public virtual void Delete() => ShowNotification("Eliminando elemento...");
        public virtual void Update() => ShowNotification("Actualizando...");
        public virtual void Print() => ShowNotification("Preparando para imprimir...");

        protected void ShowNotification(string message)
        {
            StatusMessage = message;
        }

        //protected override void OnDeactivate(bool close)
        //{
        //    _statusMessageCts?.Cancel();
        //    base.OnDeactivate(close);
        //}

        // Método para manejar errores
        protected void HandleError(Exception ex, string actionName)
        {
            StatusMessage = $"Error al {actionName}: {ex.Message}";
            // Aquí podrías también registrar el error en un log
        }
    }
}
