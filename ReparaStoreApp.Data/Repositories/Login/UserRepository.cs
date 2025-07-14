using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Security;
using System.Linq.Dynamic.Core;
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
                .FirstOrDefaultAsync(u => u.Name == username);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> SearchAsync(string searchText, int page, int pageSize, string? filter = null)
        {
            return _context.Users
                .Where(u => string.IsNullOrEmpty(searchText) ||
                           u.Name.Contains(searchText))
                //.Where(filter ?? "")
                .OrderBy(u => u.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<int> GetCountAsync(string searchText)
        {
            return await _context.Users
                .Where(u => string.IsNullOrEmpty(searchText) ||
                          u.Name.Contains(searchText))
                //u..Contains(searchText))
                .CountAsync();
        }

        public async Task SaveAsync(User user)
        {
            if (user.Id == 0)
            {
                _context.Users.Add(user);
            }
            else
            {
                var itemRemove = _context.UserRoles.Where(X => X.UserId == user.Id);
                _context.UserRoles.RemoveRange(itemRemove);

                _context.Users.Update(user);
            }

            _context.SaveChanges();
        }

        public async Task Delete(User user)
        {
            user.IsActive = false;
            _context.SaveChanges();
        }

        public async Task Activate(User user)
        {
            user.IsActive = true;
            _context.SaveChanges();
        }

        public async Task<Params> GetParamByCode(string code)
        {
            return await _context.ParamsDb.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<IEnumerable<Role>> SearchRolesAsync(string searchText, int page, int pageSize)
        {
            return await _context.Roles
                    .Where(u => string.IsNullOrEmpty(searchText) ||
                               u.Name.Contains(searchText))
                    //u.FullName.Contains(searchText))
                    .OrderBy(u => u.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(string? filter = null)
        {
            return _context.Users
                .Where(filter ?? "")
                .ToList();
        }
    }
}
