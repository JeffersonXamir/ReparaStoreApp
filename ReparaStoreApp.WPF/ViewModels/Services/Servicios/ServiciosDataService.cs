using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ServiciosService;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Services.Servicios
{
    public class ServiciosDataService : IDataService<ProductServiceItem>
    {
        private readonly IServiciosService _ServiciosService;
        private readonly IMapper _mapper;

        public ServiciosDataService(IServiciosService ServiciosService, IMapper mapper)
        {
            _ServiciosService = ServiciosService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductServiceItem>> SearchAsync(string searchText, int page, int pageSize)
        {
            var Servicioss = await _ServiciosService.SearchServiciosAsync(searchText, page, pageSize);
            return _mapper.Map<IEnumerable<ProductServiceItem>>(Servicioss);
        }

        public async Task<int> GetTotalCountAsync(string searchText)
        {
            return await _ServiciosService.GetServiciosCountAsync(searchText);
        }

        public async Task<ProductServiceItem> GetByIdAsync(int id)
        {
            var Servicios = await _ServiciosService.GetServiciosByIdAsync(id);
            return _mapper.Map<ProductServiceItem>(Servicios);
        }
    }
}
