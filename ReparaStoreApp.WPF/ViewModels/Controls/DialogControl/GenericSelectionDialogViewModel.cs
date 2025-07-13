using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace ReparaStoreApp.WPF.ViewModels.Controls.DialogControl
{
    public class GenericSelectionDialogViewModel<TItem> : Screen where TItem : IItem, new()
    {
        private readonly IWindowManager _windowManager;
        private readonly GenericListViewModel<TItem> _listViewModel;
        public Action<GenericListViewModel<TItem>> ConfigureList { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public GenericListViewModel<TItem> ListViewModel => _listViewModel;

        public TItem SelectedItem { get; private set; }
        public bool WasConfirmed { get; private set; }

        public GenericSelectionDialogViewModel(
        IWindowManager windowManager,
        GenericListViewModel<TItem> listViewModel)
        {
            _windowManager = windowManager;
            _listViewModel = listViewModel;

            // Eliminar el manejador automático de selección
            // y reemplazarlo por uno que solo responda a doble clic
            _listViewModel.SearchText = "";
            _listViewModel.Refresh();
            _listViewModel.OnItemSelected = null; // Esto evita el cierre automático
            _listViewModel.Parent = this;

            // Configurar el manejador de selección para actualizar solo la propiedad
            _listViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(GenericListViewModel<TItem>.SelectedItem))
                {
                    // Solo actualizamos el SelectedItem del diálogo
                    SelectedItem = _listViewModel.SelectedItem;
                }
            };
        }

        public async Task<TItem> ShowDialogAsync(
            string title,
            string message = null,
            Action<GenericListViewModel<TItem>> configureList = null)
        {
            Title = title;
            Message = message;
            WasConfirmed = false;
            SelectedItem = default;

            
            configureList?.Invoke(_listViewModel);

            // Configurar las propiedades de la ventana
            var settings = new Dictionary<string, object>
            {
                { "Title", title },
                { "SizeToContent", SizeToContent.WidthAndHeight },
                { "ResizeMode", ResizeMode.NoResize },
                { "WindowStartupLocation", WindowStartupLocation.CenterOwner }
            };

            await _windowManager.ShowDialogAsync(this);

            return WasConfirmed ? SelectedItem : default;
        }

        public void Accept()
        {
            if (_listViewModel.SelectedItem != null)
            {
                SelectedItem = _listViewModel.SelectedItem;
                WasConfirmed = true;
                TryCloseAsync(true);
            }
        }

        public void Cancel()
        {
            WasConfirmed = false;
            TryCloseAsync(false);
        }

        public void HandleDoubleClick()
        {
            if (_listViewModel.SelectedItem != null)
            {
                SelectedItem = _listViewModel.SelectedItem;
                WasConfirmed = true;
                TryCloseAsync(true);
            }
        }

        public void OnLoaded()
        {
            // Forzar la actualización del título después de que se carga la vista
            NotifyOfPropertyChange(() => Title);
            NotifyOfPropertyChange(() => Message);
        }
    }
}
