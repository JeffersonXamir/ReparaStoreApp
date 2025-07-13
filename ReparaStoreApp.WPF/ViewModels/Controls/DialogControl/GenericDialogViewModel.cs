using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Controls;

namespace ReparaStoreApp.WPF.ViewModels.Controls.DialogControl
{
    public class GenericDialogViewModel : Screen
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

        private string _primaryButtonText = "Aceptar";
        public string PrimaryButtonText
        {
            get => _primaryButtonText;
            set => Set(ref _primaryButtonText, value);
        }

        private string _secondaryButtonText = "Cancelar";
        public string SecondaryButtonText
        {
            get => _secondaryButtonText;
            set => Set(ref _secondaryButtonText, value);
        }

        private bool _showSecondaryButton = true;
        public bool ShowSecondaryButton
        {
            get => _showSecondaryButton;
            set => Set(ref _showSecondaryButton, value);
        }

        private ControlAppearance _primaryButtonAppearance = ControlAppearance.Primary;
        public ControlAppearance PrimaryButtonAppearance
        {
            get => _primaryButtonAppearance;
            set => Set(ref _primaryButtonAppearance, value);
        }

        private ControlAppearance _secondaryButtonAppearance = ControlAppearance.Secondary;
        public ControlAppearance SecondaryButtonAppearance
        {
            get => _secondaryButtonAppearance;
            set => Set(ref _secondaryButtonAppearance, value);
        }

        private object _content;
        public object Content
        {
            get => _content;
            set => Set(ref _content, value);
        }

        public bool Result { get; private set; }

        public GenericDialogViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public async Task<bool> ShowDialogAsync(
        string title,
        string message = null,
        object content = null,
        string primaryButtonText = "Aceptar",
        string secondaryButtonText = "Cancelar",
        bool showSecondaryButton = true)
        {
            Title = title;
            Message = message;
            Content = content;
            PrimaryButtonText = primaryButtonText;
            SecondaryButtonText = secondaryButtonText;
            ShowSecondaryButton = showSecondaryButton;

            var settings = new Dictionary<string, object>
        {
            { "Title", title },
            { "WindowStartupLocation", WindowStartupLocation.CenterOwner },
            { "ResizeMode", ResizeMode.NoResize }
        };

            await _windowManager.ShowDialogAsync(this, null, settings);

            return Result;
        }

        public void PrimaryButtonClick()
        {
            Result = true;
            TryCloseAsync(true);
        }

        public void SecondaryButtonClick()
        {
            Result = false;
            TryCloseAsync(false);
        }

        public void CloseButtonClick()
        {
            Result = false;
            TryCloseAsync(false);
        }
    }
}
