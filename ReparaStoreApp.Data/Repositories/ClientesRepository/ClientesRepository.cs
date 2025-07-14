using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.ClientesRepository
{
    public  class ClientesRepository: IClientesRepository
    {
        private readonly AppDbContext _context;

        public ClientesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Clientes> GetByIdAsync(int id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Clientes>> SearchAsync(string searchText, int page, int pageSize)
        {
            // Garantiza que page sea al menos 1
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 100;

            return await _context.Clientes
                .Where(u => string.IsNullOrWhiteSpace(searchText) || u.Nombre.Contains(searchText))
                .OrderBy(u => u.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        
        public async Task<int> GetCountAsync(string searchText)
        {
            return await _context.Clientes
                .Where(u => string.IsNullOrEmpty(searchText) ||
                          //u.c.Contains(searchText) ||
                          u.Nombre.Contains(searchText))
                //u..Contains(searchText))
                .CountAsync();
        }

        public async Task SaveAsync(Clientes Clientes)
        {
            if (Clientes.Id == 0)
            {
                _context.Clientes.Add(Clientes);
            }
            else
            {
                _context.Clientes.Update(Clientes);
            }

            _context.SaveChanges();
        }

        public async Task Delete(Clientes Clientes)
        {
            Clientes.Activo = false;
            _context.SaveChanges();
        }

        public async Task Activate(Clientes Clientes)
        {
            Clientes.Activo = true;
            _context.SaveChanges();
        }
    }
}
