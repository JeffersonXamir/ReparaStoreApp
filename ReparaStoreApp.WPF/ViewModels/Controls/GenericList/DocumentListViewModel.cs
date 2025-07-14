using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.WPF.Utils;
using ReparaStoreApp.WPF.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Controls.GenericList
{
    public class DocumentSelectedMessage<TDocument> where TDocument : IDocumentItem
    {
        public TDocument Document { get; }

        public DocumentSelectedMessage(TDocument document)
        {
            Document = document;
        }
    }

    public class DocumentListViewModel<TDocument> : Screen where TDocument : IDocumentItem, new()
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDocumentDataService<TDocument> _dataService;
        private readonly IMapper _mapper;

        private BindableCollection<TDocument> _documents;
        private TDocument _selectedDocument;
        private string _searchText = "";
        private int _pageSize = 20;
        private int _currentPage = 1;
        private int _totalItems;
        private bool _isLoading;

        public string DisplayMemberPath { get; set; } = "Numero";
        public string DetailMemberPath { get; set; } = "Detalle";

        private bool _isListEnabled = true;

        public bool IsListEnabled
        {
            get => _isListEnabled;
            set
            {
                _isListEnabled = value;
                NotifyOfPropertyChange();
            }
        }
        public Func<string, Task<IEnumerable<TDocument>>> SearchFunction { get; set; }

        public DocumentListViewModel(
            IEventAggregator eventAggregator,
            IDocumentDataService<TDocument> dataService,
            IMapper mapper)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _mapper = mapper;
            Documents = new BindableCollection<TDocument>();
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            SearchText = "";
            await LoadData();
        }

        public async Task LoadData()
        {
            IsLoading = true;

            try
            {
                var data = await _dataService.SearchAsync(SearchText, CurrentPage, PageSize);
                Documents = new BindableCollection<TDocument>(data);
                TotalItems = await _dataService.GetTotalCountAsync(SearchText);

                SelectedDocument = Documents.FirstOrDefault() ?? new TDocument();
            }
            catch (UnauthorizedAccessException)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new TokenExpiredMessage());
            }
            catch (Exception ex)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new ErrorMessage(ex.Message));
            }
            finally
            {
                IsLoading = false;
            }
        }

        private DebounceDispatcher _searchDebouncer = new DebounceDispatcher();
        public async Task Search()
        {
            await _searchDebouncer.Debounce(300, async () =>
            {
                CurrentPage = 1;
                await LoadData();
            });
        }

        public async Task NextPage()
        {
            CurrentPage++;
            await LoadData();
        }

        public bool CanNextPage => (CurrentPage * PageSize) < TotalItems;

        public async Task PreviousPage()
        {
            CurrentPage--;
            await LoadData();
        }

        public bool CanPreviousPage => CurrentPage > 1;

        public BindableCollection<TDocument> Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => CanNextPage);
                NotifyOfPropertyChange(() => CanPreviousPage);
            }
        }

        public Func<TDocument, Task> OnDocumentSelected { get; set; }

        public TDocument SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                _selectedDocument = value;
                NotifyOfPropertyChange();

                if (value != null)
                {
                    if (OnDocumentSelected != null)
                    {
                        Task.Run(() => OnDocumentSelected(value));
                    }
                    else
                    {
                        _eventAggregator.PublishOnUIThreadAsync(new DocumentSelectedMessage<TDocument>(value));
                    }
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                NotifyOfPropertyChange();
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                NotifyOfPropertyChange();
            }
        }

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                NotifyOfPropertyChange();
            }
        }

        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => CanNextPage);
                NotifyOfPropertyChange(() => CanPreviousPage);
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
