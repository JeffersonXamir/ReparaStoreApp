using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Inventario
{
    public class Kardex
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TipoMovimientoKardex Tipo { get; set; }
        public int ProductoId { get; set; }
        public ItemEntity Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string Notas { get; set; }
        public int UsuarioId { get; set; }
        public User Usuario { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }

        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }
    }

    public enum TipoMovimientoKardex
    {
        Ingreso,
        Egreso
    }
}
