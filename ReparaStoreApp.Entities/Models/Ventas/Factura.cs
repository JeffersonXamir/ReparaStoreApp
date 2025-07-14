using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Ventas
{
    public class Factura
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public EstadoFactura Estado { get; set; } = EstadoFactura.Borrador;

        public int ReparacionId { get; set; }
        public Reparacion Reparacion { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        
        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }

        public int UsuarioId { get; set; }
        public User Usuario { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }

        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }


        public ICollection<FacturaDetalle> Detalles { get; set; }
    }

    public enum EstadoFactura
    {
        Borrador,
        Procesada,
        Anulada
    }
}
