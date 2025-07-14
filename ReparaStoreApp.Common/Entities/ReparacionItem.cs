using Caliburn.Micro;
using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;
using ReparaStoreApp.Entities.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public class ReparacionItem : DocumentItem
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string NotasIngreso { get; set; } = string.Empty;
        public string NotasReparado { get; set; } = string.Empty;
        public decimal CostoEstimado { get; set; }
        public decimal CostoFinal { get; set; }
        public EstadoReparacion Estado { get; set; }
        public bool Activo { get; set; } = false;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }

        public DateTime? FechaAprobacion { get; set; }
        public int UsuarioAprobacionId { get; set; }
        public User UsuarioAprobacion { get; set; }

        public DateTime? FechaReparado { get; set; }
        public int UsuarioReparadoId { get; set; }
        public User UsuarioReparado { get; set; }

        // Relaciones
        public int DispositivoId { get; set; }
        public Dispositivos Dispositivo { get; set; }

        public int? TecnicoId { get; set; }
        public User? Tecnico { get; set; }

        public int? CajeroId { get; set; }
        public User? Cajero { get; set; }

        //public BindableCollection<ReparacionDetalleItem> Detalles { get; set; } = new();
        private BindableCollection<ReparacionDetalleItem> _detalles = new BindableCollection<ReparacionDetalleItem>();
        public BindableCollection<ReparacionDetalleItem> Detalles
        {
            get => _detalles;
            set
            {
                _detalles = value;
                NotifyOfPropertyChange(() => Detalles);
            }
        }
        public int? FacturaId { get; set; }
        public Factura Factura { get; set; }

        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }
    }
}
