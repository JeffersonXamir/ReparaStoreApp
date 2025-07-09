using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Factura
{
    public class Factura
    {
        public int Id { get; set; }
        [Required]
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; }

        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }

        public int UsuarioId { get; set; }
        public User Usuario { get; set; }

        public ICollection<DetalleFactura> Detalles { get; set; }
    }
}
