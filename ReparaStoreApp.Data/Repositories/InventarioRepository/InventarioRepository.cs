using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.InventarioRepository
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly AppDbContext _context;

        public InventarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Inventario> GetByItemIdAsync(int itemId)
        {
            return await _context.Inventario
                .FirstOrDefaultAsync(i => i.ProductoId == itemId);
        }

        public async Task<bool> ActualizarStockAsync(int itemId, int cantidad)
        {
            try
            {
                var inventario = await GetByItemIdAsync(itemId);

                if (inventario == null)
                {
                    inventario = new Inventario
                    {
                        ProductoId = itemId,
                        Cantidad = cantidad > 0 ? cantidad : 0,
                        StockMinimo = 5,
                        StockMaximo = 100,
                        Activo = true
                    };
                    await _context.Inventario.AddAsync(inventario);
                }
                else
                {
                    inventario.Cantidad += cantidad;
                    if (inventario.Cantidad < 0) inventario.Cantidad = 0;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SetStockAsync(int itemId, int cantidad, int stockMinimo = 5, int stockMaximo = 100)
        {
            try
            {
                var inventario = await GetByItemIdAsync(itemId);

                if (inventario == null)
                {
                    inventario = new Inventario
                    {
                        ProductoId = itemId,
                        Cantidad = cantidad,
                        StockMinimo = stockMinimo,
                        StockMaximo = stockMaximo,
                        Activo = true
                    };
                    await _context.Inventario.AddAsync(inventario);
                }
                else
                {
                    inventario.Cantidad = cantidad;
                    inventario.StockMinimo = stockMinimo;
                    inventario.StockMaximo = stockMaximo;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
