using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.KardexService
{
    public interface IKardexService
    {
        Task<IEnumerable<Kardex>> GetKardexReport(DateTime? fechaInicio, DateTime? fechaFin, int? tipoMovimiento, string filtroProducto);
    }
}
