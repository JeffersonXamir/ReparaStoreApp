using ReparaStoreApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Services
{
    public interface IDocumentDataService<T> where T : IDocumentItem, new()
    {
        Task<IEnumerable<T>> SearchAsync(string searchText, int page, int pageSize);
        Task<int> GetTotalCountAsync(string searchText);
        Task<T> GetByIdAsync(int id);
    }
}
