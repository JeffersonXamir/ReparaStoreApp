using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.InventarioRepository
{
    public interface IInventarioRepository
    {
        Task<Inventario> GetByItemIdAsync(int itemId);
        Task<bool> ActualizarStockAsync(int itemId, int cantidad);
        Task<bool> SetStockAsync(int itemId, int cantidad, int stockMinimo = 5, int stockMaximo = 100);
    }
}
