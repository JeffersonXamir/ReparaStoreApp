using Caliburn.Micro;
using ReparaStoreApp.Core.Services.KardexService;
using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ReparaStoreApp.WPF.ViewModels.Kardex
{
    public class KardexViewModel : BaseViewModel
    {
        private readonly IKardexService _kardexService;
        private DateTime? _fechaInicio;
        private DateTime? _fechaFin;
        private string _filtroProducto;
        private int? _tipoMovimientoSeleccionado;
        private ICollectionView _kardexItems;
        private int _totalMovimientos;
        private decimal _totalIngresos;
        private decimal _totalEgresos;
        private decimal _saldoTotal;

        public KardexViewModel(IKardexService kardexService, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            _kardexService = kardexService;
            TiposMovimiento = new ObservableCollection<TipoMovimientoItem>
            {
                new TipoMovimientoItem { Id = null, Descripcion = "Todos" },
                new TipoMovimientoItem { Id = (int)TipoMovimientoKardex.Ingreso, Descripcion = "Ingreso" },
                new TipoMovimientoItem { Id = (int)TipoMovimientoKardex.Egreso, Descripcion = "Egreso" }
            };

            Task.Run(() => InitializeAsync()).ConfigureAwait(false);
        }

        public ObservableCollection<TipoMovimientoItem> TiposMovimiento { get; }

        public DateTime? FechaInicio
        {
            get => _fechaInicio;
            set
            {
                _fechaInicio = value;
                NotifyOfPropertyChange(() => FechaInicio);
            }
        }

        public DateTime? FechaFin
        {
            get => _fechaFin;
            set
            {
                _fechaFin = value;
                NotifyOfPropertyChange(() => FechaFin);
            }
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

        public int? TipoMovimientoSeleccionado
        {
            get => _tipoMovimientoSeleccionado;
            set
            {
                _tipoMovimientoSeleccionado = value;
                NotifyOfPropertyChange(() => TipoMovimientoSeleccionado);
            }
        }

        public ICollectionView KardexItems
        {
            get => _kardexItems;
            set
            {
                _kardexItems = value;
                NotifyOfPropertyChange(() => KardexItems);
            }
        }

        public int TotalMovimientos
        {
            get => _totalMovimientos;
            set
            {
                _totalMovimientos = value;
                NotifyOfPropertyChange(() => TotalMovimientos);
            }
        }

        public decimal TotalIngresos
        {
            get => _totalIngresos;
            set
            {
                _totalIngresos = value;
                NotifyOfPropertyChange(() => TotalIngresos);
            }
        }

        public decimal TotalEgresos
        {
            get => _totalEgresos;
            set
            {
                _totalEgresos = value;
                NotifyOfPropertyChange(() => TotalEgresos);
            }
        }

        public decimal SaldoTotal
        {
            get => _saldoTotal;
            set
            {
                _saldoTotal = value;
                NotifyOfPropertyChange(() => SaldoTotal);
            }
        }

        private async Task InitializeAsync()
        {
            try
            {
                await LoadKardex();
            }
            catch (Exception ex)
            {
                // Log the error or show a message to the user
                Console.WriteLine($"Error initializing Kardex: {ex.Message}");
            }
        }

        public async Task FiltrarKardex()
        {
            await LoadKardex();
        }

        public async Task ActualizarKardex()
        {
            FechaInicio = null;
            FechaFin = null;
            FiltroProducto = null;
            TipoMovimientoSeleccionado = null;
            await LoadKardex();
        }

        private async Task LoadKardex()
        {
            var items = await _kardexService.GetKardexReport(FechaInicio, FechaFin, TipoMovimientoSeleccionado, FiltroProducto);
            KardexItems = CollectionViewSource.GetDefaultView(items);

            TotalMovimientos = items.Count();
            TotalIngresos = items.Where(i => i.Tipo == TipoMovimientoKardex.Ingreso).Sum(i => i.Total);
            TotalEgresos = items.Where(i => i.Tipo == TipoMovimientoKardex.Egreso).Sum(i => i.Total);
            SaldoTotal = TotalIngresos - TotalEgresos;
        }

        public override async Task Update()
        {
            await ActualizarKardex();
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
