using Caliburn.Micro;
using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public class ReparacionDetalleItem : PropertyChangedBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        private int _reparacionId;
        public int ReparacionId
        {
            get => _reparacionId;
            set
            {
                _reparacionId = value;
                NotifyOfPropertyChange(() => ReparacionId);
            }
        }

        private Reparacion _reparacion;
        public Reparacion Reparacion
        {
            get => _reparacion;
            set
            {
                _reparacion = value;
                NotifyOfPropertyChange(() => Reparacion);
            }
        }

        private int _itemId;
        public int ItemId
        {
            get => _itemId;
            set
            {
                _itemId = value;
                NotifyOfPropertyChange(() => ItemId);
            }
        }

        private ItemEntity _item;
        public ItemEntity Item
        {
            get => _item;
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => Item);
            }
        }

        private int _cantidad;
        public int Cantidad
        {
            get => _cantidad;
            set
            {
                _cantidad = value;
                NotifyOfPropertyChange(() => Cantidad);
                CalculateDerivedValues();
            }
        }

        private decimal _precioUnitario;
        public decimal PrecioUnitario
        {
            get => _precioUnitario;
            set
            {
                _precioUnitario = value;
                NotifyOfPropertyChange(() => PrecioUnitario);
            }
        }

        private decimal _tasaIVA;
        public decimal TasaIVA
        {
            get => _tasaIVA;
            set
            {
                _tasaIVA = value;
                NotifyOfPropertyChange(() => TasaIVA);
            }
        }

        private decimal _totalIVA;
        public decimal TotalIVA
        {
            get => _totalIVA;
            set
            {
                _totalIVA = value;
                NotifyOfPropertyChange(() => TotalIVA);
            }
        }

        private decimal _subTotal;
        public decimal SubTotal
        {
            get => _subTotal;
            set
            {
                _subTotal = value;
                NotifyOfPropertyChange(() => SubTotal);
            }
        }

        private decimal _total;
        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                NotifyOfPropertyChange(() => Total);
            }
        }

        private bool _activo;
        public bool Activo
        {
            get => _activo;
            set
            {
                _activo = value;
                NotifyOfPropertyChange(() => Activo);
            }
        }

        private DateTime _fechaCreacion = DateTime.Now;
        public DateTime FechaCreacion
        {
            get => _fechaCreacion;
            set
            {
                _fechaCreacion = value;
                NotifyOfPropertyChange(() => FechaCreacion);
            }
        }

        private int _usuarioCreadorId;
        public int UsuarioCreadorId
        {
            get => _usuarioCreadorId;
            set
            {
                _usuarioCreadorId = value;
                NotifyOfPropertyChange(() => UsuarioCreadorId);
            }
        }

        private User _usuarioCreador;
        public User UsuarioCreador
        {
            get => _usuarioCreador;
            set
            {
                _usuarioCreador = value;
                NotifyOfPropertyChange(() => UsuarioCreador);
            }
        }

        private DateTime? _fechaEdicion;
        public DateTime? FechaEdicion
        {
            get => _fechaEdicion;
            set
            {
                _fechaEdicion = value;
                NotifyOfPropertyChange(() => FechaEdicion);
            }
        }

        private int? _usuarioEdicionId;
        public int? UsuarioEdicionId
        {
            get => _usuarioEdicionId;
            set
            {
                _usuarioEdicionId = value;
                NotifyOfPropertyChange(() => UsuarioEdicionId);
            }
        }

        private User _usuarioEdicion;
        public User UsuarioEdicion
        {
            get => _usuarioEdicion;
            set
            {
                _usuarioEdicion = value;
                NotifyOfPropertyChange(() => UsuarioEdicion);
            }
        }


        private void CalculateDerivedValues()
        {
            TotalIVA = (Cantidad * PrecioUnitario) * TasaIVA / 100;
            SubTotal = Cantidad * PrecioUnitario;
            Total = SubTotal + TotalIVA;

            // Notificar cambios en las propiedades calculadas
            NotifyOfPropertyChange(() => TotalIVA);
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Total);
        }
    }
}
