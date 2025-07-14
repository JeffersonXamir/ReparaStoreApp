using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.DispositivosRepository
{
    public interface IDispositivosRepository
    {
        Task<Dispositivos> GetByIdAsync(int id);
        Task<IEnumerable<Dispositivos>> SearchAsync(string searchText, int page, int pageSize);
        Task<int> GetCountAsync(string searchText);
        Task SaveAsync(Dispositivos Dispositivos);
        Task Delete(Dispositivos Dispositivos);
        Task Activate(Dispositivos Dispositivos);
    }
}
