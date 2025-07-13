using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.Login
{
    public interface IUserService
    {
        //Task<User> Authenticate(string username, string password);
        //Task<User> Create(User user, string password);
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> SearchUsersAsync(string searchText, int page, int pageSize, string? filter = null);
        Task<int> GetUserCountAsync(string searchText);
        Task SaveUserAsync(UserItem user);
        Task UpdateUserAsync(UserItem user);
        Task ActivateUserAsync(UserItem user);
        Task DeleteUserAsync(UserItem user);
        Task<ParamsItem> GetParamByCode(string code);
        Task<IEnumerable<Role>> SearchRolesAsync(string searchText, int page, int pageSize);
        Task<IEnumerable<User>> GetAllUsersAsync(string? filter = null);
    }
}
