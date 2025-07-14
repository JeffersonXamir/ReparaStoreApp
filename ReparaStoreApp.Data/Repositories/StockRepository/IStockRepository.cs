using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.StockRepository
{
    public interface IStockRepository
    {
        Task<IEnumerable<Inventario>> GetStockWithProducts(string filtroProducto);
    }
}
