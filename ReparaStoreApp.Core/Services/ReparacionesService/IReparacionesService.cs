using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.ReparacionesService
{
    public interface IReparacionesService
    {
        Task<DocumentResponse> CreateAsync(ReparacionItem reparacionItem);
        Task<DocumentResponse> UpdateAsync(ReparacionItem reparacionItem);
        Task<DocumentResponse> DeleteAsync(int id, int usuarioId);

        Task<ReparacionItem> GetByIdAsync(int id);
        Task<IEnumerable<ReparacionItem>> SearchAsync(string searchText, int page, int pageSize);
        Task<int> GetCountAsync(string searchText);

        Task<DocumentResponse> AprobarReparacionAsync(int reparacionId, int usuarioId, bool aprobado, string nota);
        Task<DocumentResponse> FinalizarReparacionAsync(int reparacionId, int usuarioId);
    }
}
