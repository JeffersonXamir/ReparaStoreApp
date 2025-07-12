using ReparaStoreApp.Entities.Models.Inventario;
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
    public class FacturaDetalle
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public Factura Factura { get; set; }

        public int? ProductoId { get; set; }
        public ItemEntity Producto { get; set; }

        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TasaIVA { get; set; }
        public decimal TotalIVA { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }

        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }

    }
}
