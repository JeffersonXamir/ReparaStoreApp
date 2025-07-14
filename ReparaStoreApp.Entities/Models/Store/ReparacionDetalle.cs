using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Store
{
    public class ReparacionDetalle
    {
        public int Id { get; set; }
        public int ReparacionId { get; set; }
        public Reparacion Reparacion { get; set; }

        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }

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
