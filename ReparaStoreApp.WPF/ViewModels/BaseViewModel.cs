using Caliburn.Micro;
using System;

namespace ReparaStoreApp.WPF.ViewModels
{
    public abstract class BaseViewModel : Screen
    {
        protected readonly IEventAggregator _eventAggregator;
        private bool _hasFocus;
        private bool _editMode;
        private bool _creationMode;
        private string _statusMessage;
        private bool _isBusy;
        private CancellationTokenSource _statusMessageCts;

        protected BaseViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public bool HasFocus
        {
            get => _hasFocus;
            set
            {
                _hasFocus = value;
                NotifyOfPropertyChange(() => HasFocus);
            }
        }

        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                NotifyOfPropertyChange(() => EditMode);
            }
        }

        public bool CreationMode
        {
            get => _creationMode;
            set
            {
                _creationMode = value;
                NotifyOfPropertyChange(() => CreationMode);
            }
        }

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

        protected override async void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            await InitializeFocusState(); // Se ejecutará cuando la vista se adjunte
        }
       

        private async Task InitializeFocusState()
        {
            // Establece el estado inicial basado en si estamos editando
            if (EditMode || CreationMode)
            {
                await OnFocus();
            }
            else
            {
                await OnLostFocus();
            }
        }
        public virtual async Task OnFocus()
        {
            HasFocus = true;
            await _eventAggregator.PublishOnUIThreadAsync(new NavigationControlMessage { IsEnabled = false });

            if (!EditMode && !CreationMode)
            {
                // Modo normal - desactivar solo ciertos botones
                await _eventAggregator.PublishOnUIThreadAsync(new ControlMessage
                {
                    Enable = true,
                    Actions = new List<ToolbarButtonsAction>
                {
                    ToolbarButtonsAction.New,
                    ToolbarButtonsAction.Delete,
                    ToolbarButtonsAction.Refresh,
                    ToolbarButtonsAction.Print,
                    ToolbarButtonsAction.Search
                }
                });
            }
            else
            {
                // Modo edición/creación - mostrar solo guardar/cancelar
                await _eventAggregator.PublishOnUIThreadAsync(new ControlMessage
                {
                    Enable = true,
                    Actions = new List<ToolbarButtonsAction>
                {
                    ToolbarButtonsAction.Save,
                    ToolbarButtonsAction.Undo
                }
                });
            }
        }

        public virtual async Task OnLostFocus()
        {
            HasFocus = false;
            EditMode = false;
            CreationMode = false;
            await _eventAggregator.PublishOnUIThreadAsync(new NavigationControlMessage { IsEnabled = true });
            await ResetToolbar();
        }

        protected async Task ResetToolbar()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new ControlMessage
            {
                Enable = true,
                Actions = new List<ToolbarButtonsAction>
                {
                    ToolbarButtonsAction.New,
                    ToolbarButtonsAction.Delete,
                    ToolbarButtonsAction.Refresh,
                    ToolbarButtonsAction.Print,
                    ToolbarButtonsAction.Search
                }
            });
        }

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
        public virtual async Task New()
        {
            ShowNotification("Nuevo elemento creado");
            CreationMode = true;
            await OnFocus();
        }

        public virtual async Task Create()
        {
            ShowNotification("Creando elemento...");
            await OnLostFocus();
        }

        public virtual async Task Edit()
        {
            ShowNotification("Editando elemento...");
            EditMode = true;
            await OnFocus();
        }

        public virtual async Task Delete()
        {
            ShowNotification("Eliminando elemento...");
            await OnLostFocus();
        }

        public virtual async Task Update()
        {
            ShowNotification("Actualizando...");
            await OnLostFocus();
        }

        public virtual async Task Print()
        {
            ShowNotification("Preparando para imprimir...");
            await OnLostFocus();
        }

        public virtual async Task Undo()
        {
            ShowNotification("cancelando cambios...");
            await OnLostFocus();
        }

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
