using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.ServiciosRepository
{
    public interface IServiciosRepository
    {
        Task<ItemEntity> GetByIdAsync(int id);
        Task<IEnumerable<ItemEntity>> SearchAsync(string searchText, int page, int pageSize);
        Task<int> GetCountAsync(string searchText);
        Task SaveAsync(ItemEntity ItemEntity);
        Task Delete(ItemEntity ItemEntity);
        Task Activate(ItemEntity ItemEntity);
    }
}
