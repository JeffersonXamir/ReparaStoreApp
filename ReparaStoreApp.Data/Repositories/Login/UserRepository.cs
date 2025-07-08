using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.Login
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserRoles)  // Carga UserRoles
                .ThenInclude(ur => ur.Role) // Carga los Roles relacionados
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> SearchAsync(string searchText, int page, int pageSize)
        {
            return await _context.Users
                .Where(u => string.IsNullOrEmpty(searchText) ||
                           u.Username.Contains(searchText) )
                           //u.FullName.Contains(searchText))
                .OrderBy(u => u.Username)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(string searchText)
        {
            return await _context.Users
                .Where(u => string.IsNullOrEmpty(searchText) ||
                          u.Username.Contains(searchText) )
                          //u..Contains(searchText))
                .CountAsync();
        }

        public async Task SaveAsync(User user)
        {
            if (user.Id == 0)
            {
                await _context.Users.AddAsync(user);
            }
            else
            {
                _context.Users.Update(user);
            }

            await _context.SaveChangesAsync();
        }
    }
}
