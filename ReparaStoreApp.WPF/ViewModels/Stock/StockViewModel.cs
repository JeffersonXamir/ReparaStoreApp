using Caliburn.Micro;
using ReparaStoreApp.Core.Services.StockService;
using ReparaStoreApp.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ReparaStoreApp.WPF.ViewModels.Stock
{
    public class StockViewModel : BaseViewModel
    {
        private readonly IStockService _stockService;
        private string _filtroProducto;
        private ICollectionView _stockItems;
        private int _totalProductos;
        private int _productosBajoStock;

        public StockViewModel(IStockService stockService, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _stockService = stockService;
            Task.Run(InitializeAsync).ConfigureAwait(false);
        }

        private async Task InitializeAsync()
        {
            await LoadStock();
        }

        public string FiltroProducto
        {
            get => _filtroProducto;
            set
            {
                _filtroProducto = value;
                NotifyOfPropertyChange(() => FiltroProducto);
            }
        }

        public ICollectionView StockItems
        {
            get => _stockItems;
            set
            {
                _stockItems = value;
                NotifyOfPropertyChange(() => StockItems);
            }
        }

        public int TotalProductos
        {
            get => _totalProductos;
            set
            {
                _totalProductos = value;
                NotifyOfPropertyChange(() => TotalProductos);
            }
        }

        public int ProductosBajoStock
        {
            get => _productosBajoStock;
            set
            {
                _productosBajoStock = value;
                NotifyOfPropertyChange(() => ProductosBajoStock);
            }
        }

        public async Task FiltrarStock()
        {
            await LoadStock();
        }

        public async Task ActualizarStock()
        {
            FiltroProducto = string.Empty;
            await LoadStock();
        }

        private async Task LoadStock()
        {
            var items = await _stockService.GetStockReport(FiltroProducto);
            StockItems = CollectionViewSource.GetDefaultView(items);

            TotalProductos = items.Count();
            ProductosBajoStock = items.Count(i => i.Cantidad < i.StockMinimo);
        }

        public override async Task Update()
        {
            await ActualizarStock();
            await base.Update();
        }

        public override async Task New()
        {
            
        }

        public override async Task Edit()
        {
            
        }

        public override async Task Create()
        {

        }
        public override async Task Delete()
        {

        }

        public override async Task<ValidateForm> ValidateForm()
        {
            ValidateForm validateForm = new ValidateForm();
            validateForm.Success = true;
            validateForm.ErrorMessage = string.Empty;

            return validateForm;
        }
    }
}
