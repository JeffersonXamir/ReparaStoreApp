using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ProductosService;
using ReparaStoreApp.Data.Repositories.ProductosRepository;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.ProductosService
{
    public class ProductosService : IProductosService
    {
        private readonly IProductosRepository _ProductosRepository;
        private readonly IMapper _mapper;

        public ProductosService(IProductosRepository ProductosRepository, IMapper mapper)
        {
            _ProductosRepository = ProductosRepository;
            _mapper = mapper;
        }

        public async Task<ItemEntity> GetProductosByIdAsync(int id)
        {
            return await _ProductosRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ItemEntity>> SearchProductosAsync(string searchText, int page, int pageSize)
        {
            return await _ProductosRepository.SearchAsync(searchText, page, pageSize);
        }

        public async Task<int> GetProductosCountAsync(string searchText)
        {
            return await _ProductosRepository.GetCountAsync(searchText);
        }

        public async Task SaveProductosAsync(ProductosItem Productos)
        {
            if (Productos == null) return;
            var ProductosDb = _mapper.Map<ItemEntity>(Productos);
            ProductosDb.Tipo = TipoItem.Producto;
            ProductosDb.FechaCreacion = DateTime.UtcNow;
            ProductosDb.Activo = true;
            await _ProductosRepository.SaveAsync(ProductosDb);
        }

        public async Task UpdateProductosAsync(ProductosItem Productos)
        {
            if (Productos == null) return;
            var ProductosDb = await _ProductosRepository.GetByIdAsync(Productos.Id);

            ProductosDb.Codigo = Productos.Code;
            ProductosDb.Nombre = Productos.Name;
            ProductosDb.Descripcion = Productos.Descripcion;
            ProductosDb.Precio = Productos.Precio;
            ProductosDb.TieneIVA = Productos.TieneIVA;
            ProductosDb.Nota = Productos.Nota;
            ProductosDb.UsuarioEdicionId = Productos.UsuarioEdicionId;
            ProductosDb.FechaEdicion = Productos.FechaEdicion;

            await _ProductosRepository.SaveAsync(ProductosDb);
        }

        public async Task ActivateProductosAsync(ProductosItem Productos)
        {
            if (Productos == null) return;
            var ProductosDb = await _ProductosRepository.GetByIdAsync(Productos.Id);
            await _ProductosRepository.Activate(ProductosDb);
        }

        public async Task DeleteProductosAsync(ProductosItem Productos)
        {
            if (Productos == null) return;
            var ProductosDb = await _ProductosRepository.GetByIdAsync(Productos.Id);
            await _ProductosRepository.Delete(ProductosDb);
        }

    }
}