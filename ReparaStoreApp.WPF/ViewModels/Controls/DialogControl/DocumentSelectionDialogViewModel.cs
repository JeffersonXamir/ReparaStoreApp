using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReparaStoreApp.WPF.ViewModels.Controls.DialogControl
{

    public class DocumentSelectionDialogViewModel<TDocument> : Screen where TDocument : IDocumentItem, new()
    {
        private readonly IWindowManager _windowManager;
        private readonly DocumentListViewModel<TDocument> _listViewModel;
        public Action<DocumentListViewModel<TDocument>> ConfigureList { get; set; }

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

        public DocumentListViewModel<TDocument> ListViewModel => _listViewModel;

        public TDocument SelectedDocument { get; private set; }
        public bool WasConfirmed { get; private set; }

        public DocumentSelectionDialogViewModel(
            IWindowManager windowManager,
            DocumentListViewModel<TDocument> listViewModel)
        {
            _windowManager = windowManager;
            _listViewModel = listViewModel;

            _listViewModel.SearchText = "";
            _listViewModel.OnDocumentSelected = null;
            _listViewModel.Parent = this;

            _listViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(DocumentListViewModel<TDocument>.SelectedDocument))
                {
                    SelectedDocument = _listViewModel.SelectedDocument;
                }
            };
        }

        public async Task<TDocument> ShowDialogAsync(
            string title,
            string message = null,
            Action<DocumentListViewModel<TDocument>> configureList = null)
        {
            Title = title;
            Message = message;
            WasConfirmed = false;
            SelectedDocument = default;

            configureList?.Invoke(_listViewModel);

            var settings = new Dictionary<string, object>
            {
                { "Title", title },
                { "SizeToContent", SizeToContent.WidthAndHeight },
                { "ResizeMode", ResizeMode.NoResize },
                { "WindowStartupLocation", WindowStartupLocation.CenterOwner }
            };

            await _windowManager.ShowDialogAsync(this);

            return WasConfirmed ? SelectedDocument : default;
        }

        public void Accept()
        {
            if (_listViewModel.SelectedDocument != null)
            {
                SelectedDocument = _listViewModel.SelectedDocument;
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
            if (_listViewModel.SelectedDocument != null)
            {
                SelectedDocument = _listViewModel.SelectedDocument;
                WasConfirmed = true;
                TryCloseAsync(true);
            }
        }

        public void OnLoaded()
        {
            NotifyOfPropertyChange(() => Title);
            NotifyOfPropertyChange(() => Message);
        }
    }
}
