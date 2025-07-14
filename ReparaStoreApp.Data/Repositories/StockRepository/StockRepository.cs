using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.StockRepository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventario>> GetStockWithProducts(string filtroProducto)
        {
            var query = _context.Inventario
                .Include(i => i.Producto)
                .Where(i => i.ProductoId != 0);

            if (!string.IsNullOrWhiteSpace(filtroProducto))
            {
                query = query.Where(i =>
                    i.Producto.Nombre.Contains(filtroProducto) ||
                    i.Producto.Codigo.Contains(filtroProducto));
            }

            return await query.ToListAsync();
        }
    }
}
