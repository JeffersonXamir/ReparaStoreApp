using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.ProductosService
{
    public interface IProductosService
    {
        Task<ItemEntity> GetProductosByIdAsync(int id);
        Task<IEnumerable<ItemEntity>> SearchProductosAsync(string searchText, int page, int pageSize);
        Task<int> GetProductosCountAsync(string searchText);
        Task SaveProductosAsync(ProductosItem Productos);
        Task UpdateProductosAsync(ProductosItem Productos);
        Task ActivateProductosAsync(ProductosItem Productos);
        Task DeleteProductosAsync(ProductosItem Productos);
    }
}
