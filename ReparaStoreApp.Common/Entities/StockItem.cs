using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public class StockItem
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public ItemEntity Producto { get; set; }
        public int Cantidad { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }
        public bool Activo { get; set; }
    }
}
