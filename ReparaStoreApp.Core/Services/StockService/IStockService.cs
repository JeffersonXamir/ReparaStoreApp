using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.StockService
{
    public interface IStockService
    {
        Task<IEnumerable<StockItem>> GetStockReport(string filtroProducto);
    }
}
