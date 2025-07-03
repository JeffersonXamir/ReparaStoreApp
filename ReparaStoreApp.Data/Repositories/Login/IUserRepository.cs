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
        //Task AddAsync(User user);
        //Task UpdateAsync(User user);
        Task<User> GetByIdAsync(int id);
    }
}
