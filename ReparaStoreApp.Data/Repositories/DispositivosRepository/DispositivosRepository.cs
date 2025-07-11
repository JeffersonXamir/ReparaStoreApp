using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.DispositivosRepository
{
    public class DispositivosRepository: IDispositivosRepository
    {
        private readonly AppDbContext _context;

        public DispositivosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dispositivos> GetByIdAsync(int id)
        {
            return await _context.Dispositivos
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Dispositivos>> SearchAsync(string searchText, int page, int pageSize)
        {
            return await _context.Dispositivos
                .Where(u => string.IsNullOrEmpty(searchText) ||
                           u.Nombre.Contains(searchText))
                //u.FullName.Contains(searchText))
                .OrderBy(u => u.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(string searchText)
        {
            return await _context.Dispositivos
                .Where(u => string.IsNullOrEmpty(searchText) ||
                          //u.c.Contains(searchText) ||
                          u.Nombre.Contains(searchText))
                //u..Contains(searchText))
                .CountAsync();
        }

        public async Task SaveAsync(Dispositivos Dispositivos)
        {
            if (Dispositivos.Id == 0)
            {
                _context.Dispositivos.Add(Dispositivos);
            }
            else
            {
                _context.Dispositivos.Update(Dispositivos);
            }

            _context.SaveChanges();
        }

        public async Task Delete(Dispositivos Dispositivos)
        {
            Dispositivos.Activo = false;
            _context.SaveChanges();
        }

        public async Task Activate(Dispositivos Dispositivos)
        {
            Dispositivos.Activo = true;
            _context.SaveChanges();
        }
    }
}
