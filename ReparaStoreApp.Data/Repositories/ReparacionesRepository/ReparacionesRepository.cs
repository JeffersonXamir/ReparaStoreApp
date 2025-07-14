using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Data.Repositories.ReparacionesRepository;
using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Data.Repositories.ReparacionesRepository
{
    public class ReparacionesRepository : IReparacionesRepository
    {
        private readonly AppDbContext _context;

        public ReparacionesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reparacion> GetByIdAsync(int id)
        {
            return await _context.Reparaciones
                .Include(r => r.Detalles)
                .Include(r => r.Dispositivo)
                .Include(r => r.Tecnico)
                .Include(r => r.Cajero)
                .Include(r => r.UsuarioReparado)
                .Include(r => r.UsuarioAprobacion)
                .Include(r => r.UsuarioCreador)
                .Include(r => r.UsuarioEdicion)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reparacion>> SearchAsync(string searchText, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 100;

            return await _context.Reparaciones
                .Include(r => r.Dispositivo)
                .Where(r => string.IsNullOrWhiteSpace(searchText) ||
                       r.Numero.Contains(searchText) ||
                       r.Descripcion.Contains(searchText) ||
                       r.Dispositivo.Marca.Contains(searchText) ||
                       r.Dispositivo.Modelo.Contains(searchText))
                .OrderByDescending(r => r.FechaCreacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(string searchText)
        {
            return await _context.Reparaciones
                .Where(r => string.IsNullOrEmpty(searchText) ||
                       r.Numero.Contains(searchText) ||
                       r.Descripcion.Contains(searchText) ||
                       r.Dispositivo.Marca.Contains(searchText) ||
                       r.Dispositivo.Modelo.Contains(searchText))
                .CountAsync();
        }

        public async Task SaveAsync(Reparacion reparacion)
        {
            if (reparacion.Id == 0)
            {
                await _context.Reparaciones.AddAsync(reparacion);
                reparacion.Numero = $"{reparacion.Numero}{reparacion.Id.ToString().PadLeft(6, '0')}";
            }
            else
            {
                _context.Reparaciones.Update(reparacion);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reparacion reparacion)
        {
            reparacion.Activo = false;
            await _context.SaveChangesAsync();
        }

        public async Task AddDetalleAsync(ReparacionDetalle detalle)
        {
            await _context.ReparacionDetalles.AddAsync(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDetalleAsync(ReparacionDetalle detalle)
        {
            _context.ReparacionDetalles.Update(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task<ReparacionDetalle> GetDetalleByIdAsync(int id)
        {
            return await _context.ReparacionDetalles
                .Include(d => d.Item)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<ReparacionDetalle>> GetDetallesByReparacionIdAsync(int reparacionId)
        {
            return await _context.ReparacionDetalles
                .Include(d => d.Item)
                .Where(d => d.ReparacionId == reparacionId && d.Activo)
                .ToListAsync();
        }
    }
}