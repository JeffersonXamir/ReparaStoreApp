using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.DispositivosService
{
    public interface IDispositivosService
    {
        Task<Dispositivos> GetDispositivosByIdAsync(int id);
        Task<IEnumerable<Dispositivos>> SearchDispositivossAsync(string searchText, int page, int pageSize);
        Task<int> GetDispositivosCountAsync(string searchText);
        Task SaveDispositivosAsync(DispositivosItem Dispositivos);
        Task UpdateDispositivosAsync(DispositivosItem Dispositivos);
        Task ActivateDispositivosAsync(DispositivosItem Dispositivos);
        Task DeleteDispositivosAsync(DispositivosItem Dispositivos);
    }
}
