using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.KardexRepository
{
    public class KardexRepository : IKardexRepository
    {
        private readonly AppDbContext _context;

        public KardexRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Kardex>> GetKardexWithDetails(DateTime? fechaInicio, DateTime? fechaFin, int? tipoMovimiento, string filtroProducto)
        {
            var query = _context.Kardex
                .Include(k => k.Producto)
                .Include(k => k.Usuario)
                .Include(k => k.UsuarioCreador)
                .Include(k => k.UsuarioEdicion)
                .AsQueryable();

            if (fechaInicio.HasValue)
            {
                query = query.Where(k => k.Fecha.Date >= fechaInicio.Value.Date);
            }

            if (fechaFin.HasValue)
            {
                query = query.Where(k => k.Fecha.Date <= fechaFin.Value.Date);
            }

            if (tipoMovimiento.HasValue)
            {
                query = query.Where(k => (int)k.Tipo == tipoMovimiento.Value);
            }

            if (!string.IsNullOrWhiteSpace(filtroProducto))
            {
                query = query.Where(k =>
                    k.Producto.Nombre.Contains(filtroProducto) ||
                    k.Producto.Codigo.Contains(filtroProducto));
            }

            return await query.OrderByDescending(k => k.Fecha).ToListAsync();
        }

        public async Task<bool> RegistrarMovimientoAsync(int itemId, TipoMovimientoKardex tipo, int cantidad, decimal precioUnitario, string notas, int usuarioId)
        {
            try
            {
                var movimiento = new Kardex
                {
                    ProductoId = itemId,
                    Tipo = tipo,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnitario,
                    Total = cantidad * precioUnitario,
                    Notas = notas,
                    UsuarioId = usuarioId,
                    Fecha = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreadorId = usuarioId
                };

                await _context.Kardex.AddAsync(movimiento);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Kardex>> GetMovimientosPorItemAsync(int itemId, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var query = _context.Kardex
                .Where(k => k.ProductoId == itemId);

            if (fechaInicio.HasValue)
                query = query.Where(k => k.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(k => k.Fecha <= fechaFin.Value);

            return await query.OrderByDescending(k => k.Fecha).ToListAsync();
        }

        public async Task<decimal> GetStockActualAsync(int itemId)
        {
            var ingresos = await _context.Kardex
                .Where(k => k.ProductoId == itemId && k.Tipo == TipoMovimientoKardex.Ingreso)
                .SumAsync(k => k.Cantidad);

            var egresos = await _context.Kardex
                .Where(k => k.ProductoId == itemId && k.Tipo == TipoMovimientoKardex.Egreso)
                .SumAsync(k => k.Cantidad);

            return ingresos - egresos;
        }
    }
}
