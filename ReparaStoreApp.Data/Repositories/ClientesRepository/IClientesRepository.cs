using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.ClientesRepository
{
    public interface IClientesRepository
    {
        Task<Clientes> GetByIdAsync(int id);
        Task<IEnumerable<Clientes>> SearchAsync(string searchText, int page, int pageSize);
        Task<int> GetCountAsync(string searchText);
        Task SaveAsync(Clientes Clientes);
        Task Delete(Clientes Clientes);
        Task Activate(Clientes Clientes);
    }
}
