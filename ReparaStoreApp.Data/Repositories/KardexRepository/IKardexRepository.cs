using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.KardexRepository
{
    public interface IKardexRepository
    {
        Task<bool> RegistrarMovimientoAsync(int itemId, TipoMovimientoKardex tipo, int cantidad, decimal precioUnitario, string notas, int usuarioId);
        Task<IEnumerable<Kardex>> GetMovimientosPorItemAsync(int itemId, DateTime? fechaInicio, DateTime? fechaFin);
        Task<decimal> GetStockActualAsync(int itemId);
    }
}
