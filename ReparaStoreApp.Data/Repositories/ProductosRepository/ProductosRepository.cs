using Microsoft.EntityFrameworkCore;
using ReparaStoreApp.Entities.Models.Inventario;

namespace ReparaStoreApp.Data.Repositories.ProductosRepository
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly AppDbContext _context;

        public ProductosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ItemEntity> GetByIdAsync(int id)
        {
            return await _context.ItemsEntity
                .FirstOrDefaultAsync(u => u.Id == id && u.Tipo == TipoItem.Producto);
        }

        public async Task<IEnumerable<ItemEntity>> SearchAsync(string searchText, int page, int pageSize)
        {
            // Garantiza que page sea al menos 1
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 100;

            return await _context.ItemsEntity
                .Where(u => string.IsNullOrWhiteSpace(searchText) || u.Nombre.Contains(searchText))
                .Where(u => u.Tipo == TipoItem.Producto) // Solo servicios 
                .OrderBy(u => u.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(string searchText)
        {
            return await _context.ItemsEntity
                .Where(u => string.IsNullOrEmpty(searchText) || u.Nombre.Contains(searchText))
                .Where(u => u.Tipo == TipoItem.Producto) // Solo servicios 
                .CountAsync();
        }

        public async Task SaveAsync(ItemEntity Servicios)
        {
            if (Servicios.Id == 0)
            {
                _context.ItemsEntity.Add(Servicios);
            }
            else
            {
                _context.ItemsEntity.Update(Servicios);
            }

            _context.SaveChanges();
        }

        public async Task Delete(ItemEntity Servicios)
        {
            Servicios.Activo = false;
            _context.SaveChanges();
        }

        public async Task Activate(ItemEntity Servicios)
        {
            Servicios.Activo = true;
            _context.SaveChanges();
        }
    }
}