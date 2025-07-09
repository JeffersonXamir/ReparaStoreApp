using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Factura
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        public int FacturaId { get; set; }
        public Factura Factura { get; set; }

        public int? ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int? ServicioId { get; set; }
        public Servicio Servicio { get; set; }
    }
}
