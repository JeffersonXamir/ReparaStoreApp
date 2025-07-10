using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.ClientesService
{
    public interface IClientesService
    {
        Task<Clientes> GetClientesByIdAsync(int id);
        Task<IEnumerable<Clientes>> SearchClientessAsync(string searchText, int page, int pageSize);
        Task<int> GetClientesCountAsync(string searchText);
        Task SaveClientesAsync(ClientesItem Clientes);
        Task UpdateClientesAsync(ClientesItem Clientes);
        Task ActivateClientesAsync(ClientesItem Clientes);
        Task DeleteClientesAsync(ClientesItem Clientes);
    }
}
