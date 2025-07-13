using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.ReparacionesRepository
{
    public interface IReparacionesRepository
    {
        Task<Reparacion> GetByIdAsync(int id);
        Task<IEnumerable<Reparacion>> SearchAsync(string searchText, int page, int pageSize);
        Task<int> GetCountAsync(string searchText);
        Task SaveAsync(Reparacion reparacion);
        Task DeleteAsync(Reparacion reparacion);

        Task AddDetalleAsync(ReparacionDetalle detalle);
        Task UpdateDetalleAsync(ReparacionDetalle detalle);
        Task<ReparacionDetalle> GetDetalleByIdAsync(int id);
        Task<IEnumerable<ReparacionDetalle>> GetDetallesByReparacionIdAsync(int reparacionId);
    }
}
