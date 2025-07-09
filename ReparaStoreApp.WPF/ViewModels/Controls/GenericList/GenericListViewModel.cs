using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common;
using ReparaStoreApp.WPF.Utils;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Controls.GenericList
{
    public class GenericListViewModel<TItem> : Screen where TItem : IItem, new()
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService<TItem> _dataService;
        private readonly IMapper _mapper;

        private BindableCollection<TItem> _items;
        private TItem _selectedItem;
        private string _searchText = "";
        private int _pageSize = 20;
        private int _currentPage = 1;
        private int _totalItems;
        private bool _isLoading;

        public string DisplayMemberPath { get; set; } = "Name";
        public string CodeMemberPath { get; set; } = "Code";

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
        public Func<string, Task<IEnumerable<TItem>>> SearchFunction { get; set; }

        public GenericListViewModel(
            IEventAggregator eventAggregator,
            IDataService<TItem> dataService,
            IMapper mapper)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _mapper = mapper;
            Items = new BindableCollection<TItem>();
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadData();
        }

        public async Task LoadData()
        {
            IsLoading = true;

            try
            {
                var data = await _dataService.SearchAsync(SearchText, CurrentPage, PageSize);
                Items = new BindableCollection<TItem>(data);
                TotalItems = await _dataService.GetTotalCountAsync(SearchText);

                // Fix for CS8601: Ensure SelectedItem is not null by checking if Items has any elements
                SelectedItem = Items.FirstOrDefault() ?? new TItem();
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

        public BindableCollection<TItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyOfPropertyChange();
            }
        }

        // Agregar esta propiedad para el manejo de selección
        public Func<TItem, Task> OnItemSelected { get; set; }

        public TItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange();

                if (value != null)
                {
                    // Llamar al manejador personalizado si existe
                    if (OnItemSelected != null)
                    {
                        Task.Run(() => OnItemSelected(value));
                    }
                    else
                    {
                        // Opción por defecto: publicar mensaje
                        _eventAggregator.PublishOnUIThreadAsync(new ItemSelectedMessage<TItem>(value));
                    }
                }
            }
        }

        private async Task PublishSelectionAsync(TItem item)
        {
            await _eventAggregator.PublishOnUIThreadAsync(new ItemSelectedMessage<TItem>(item));
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