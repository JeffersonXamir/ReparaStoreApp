using Caliburn.Micro;
using ReparaStoreApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace ReparaStoreApp.WPF.ViewModels.Controls.DialogControl
{
    public class OptionsDialogViewModel : Screen
    {
        private readonly IWindowManager _windowManager;

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

        private SymbolRegular _icon = SymbolRegular.List24;
        public SymbolRegular Icon
        {
            get => _icon;
            set => Set(ref _icon, value);
        }

        private bool _allowMultipleSelection = false;
        public bool AllowMultipleSelection
        {
            get => _allowMultipleSelection;
            set => Set(ref _allowMultipleSelection, value);
        }

        public BindableCollection<OptionsItem> Options { get; } = new BindableCollection<OptionsItem>();

        public IEnumerable<OptionsItem> SelectedOptions => Options.Where(o => o.IsChecked);

        public OptionsDialogViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public async Task<IEnumerable<OptionsItem>> ShowDialogAsync(
            string title,
            IEnumerable<OptionsItem> options,
            string message = null,
            SymbolRegular? icon = null,
            bool allowMultipleSelection = false)
        {
            Title = title;
            Message = message;
            AllowMultipleSelection = allowMultipleSelection;

            if (icon.HasValue)
                Icon = icon.Value;

            Options.Clear();
            Options.AddRange(options);

            await _windowManager.ShowDialogAsync(this);

            return SelectedOptions;
        }

        public void Accept()
        {
            if (!AllowMultipleSelection && SelectedOptions.Count() > 1)
            {
                // En caso de selección simple pero múltiples selecciones
                foreach (var option in Options)
                {
                    option.IsChecked = false;
                }
                return;
            }

            TryCloseAsync(true);
        }

        public void Cancel()
        {
            // Desmarcar todas las opciones al cancelar
            foreach (var option in Options)
            {
                option.IsChecked = false;
            }
            TryCloseAsync(false);
        }

        public void HandleSelectionChanged(OptionsItem item)
        {
            if (!AllowMultipleSelection && item.IsChecked)
            {
                // Para selección simple, desmarcar los demás
                foreach (var option in Options.Where(o => o.Id != item.Id))
                {
                    option.IsChecked = false;
                }
            }
        }
    }
}