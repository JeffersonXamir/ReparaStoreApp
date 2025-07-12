using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.ServiciosService
{
    public interface IServiciosService
    {
        Task<ItemEntity> GetServiciosByIdAsync(int id);
        Task<IEnumerable<ItemEntity>> SearchServiciosAsync(string searchText, int page, int pageSize);
        Task<int> GetServiciosCountAsync(string searchText);
        Task SaveServiciosAsync(ServiciosItem Servicios);
        Task UpdateServiciosAsync(ServiciosItem Servicios);
        Task ActivateServiciosAsync(ServiciosItem Servicios);
        Task DeleteServiciosAsync(ServiciosItem Servicios);
    }
}
