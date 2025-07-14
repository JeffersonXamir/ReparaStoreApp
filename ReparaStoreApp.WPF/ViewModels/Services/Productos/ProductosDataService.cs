using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ProductosService;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Services.Productos
{
    public  class ProductosDataService : IDataService<ProductosItem>
    {
        private readonly IProductosService _ProductosService;
        private readonly IMapper _mapper;

        public ProductosDataService(IProductosService ProductosService, IMapper mapper)
        {
            _ProductosService = ProductosService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductosItem>> SearchAsync(string searchText, int page, int pageSize)
        {
            var Productoss = await _ProductosService.SearchProductosAsync(searchText, page, pageSize);
            return _mapper.Map<IEnumerable<ProductosItem>>(Productoss);
        }

        public async Task<int> GetTotalCountAsync(string searchText)
        {
            return await _ProductosService.GetProductosCountAsync(searchText);
        }

        public async Task<ProductosItem> GetByIdAsync(int id)
        {
            var Productos = await _ProductosService.GetProductosByIdAsync(id);
            return _mapper.Map<ProductosItem>(Productos);
        }
    }
}
