using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.Login
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> SearchAsync(string searchText, int page, int pageSize, string? filter = null);
        Task<int> GetCountAsync(string searchText);
        Task SaveAsync(User user);
        Task Delete(User user);
        Task Activate(User user);

        Task <Params> GetParamByCode(string code);

        Task<IEnumerable<Role>> SearchRolesAsync(string searchText, int page, int pageSize);
        Task<IEnumerable<User>> GetAllUsersAsync(string? filter = null);
    }
}
