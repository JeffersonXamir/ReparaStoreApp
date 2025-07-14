using ReparaStoreApp.Common;

namespace ReparaStoreApp.WPF.ViewModels.Services.Users
{
    public interface IDataService<T> where T : IItem, new()
    {
        Task<IEnumerable<T>> SearchAsync(string searchText, int page, int pageSize);
        Task<int> GetTotalCountAsync(string searchText);
        Task<T> GetByIdAsync(int id);
    }
}