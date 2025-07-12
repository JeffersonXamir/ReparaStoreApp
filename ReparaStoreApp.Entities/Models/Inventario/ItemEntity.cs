using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Inventario
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string Codigo { get; set; } // Código único
        public string Nombre { get; set; }
        public string Descripcion { get; set; } = "";
        public string Nota { get; set; } = "";
        public decimal Precio { get; set; }
        public TipoItem Tipo { get; set; } // Producto o Servicio
        public bool Activo { get; set; } = true;
        public bool TieneIVA { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }

    }

    public enum TipoItem
    {
        Producto,
        Servicio
    }
}
