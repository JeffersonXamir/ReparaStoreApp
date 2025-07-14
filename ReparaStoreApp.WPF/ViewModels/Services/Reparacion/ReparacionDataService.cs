using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ReparacionesService;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Services.Reparacion
{
    public class ReparacionDataService : IDocumentDataService<ReparacionItem>
    {
        private readonly IReparacionesService _ReparacionService;
        private readonly IMapper _mapper;

        public ReparacionDataService(IReparacionesService ReparacionService, IMapper mapper)
        {
            _ReparacionService = ReparacionService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReparacionItem>> SearchAsync(string searchText, int page, int pageSize)
        {
            var Reparacions = await _ReparacionService.SearchAsync(searchText, page, pageSize);
            return _mapper.Map<IEnumerable<ReparacionItem>>(Reparacions);
        }

        public async Task<int> GetTotalCountAsync(string searchText)
        {
            return await _ReparacionService.GetCountAsync(searchText);
        }

        public async Task<ReparacionItem> GetByIdAsync(int id)
        {
            var Reparacion = await _ReparacionService.GetByIdAsync(id);
            return _mapper.Map<ReparacionItem>(Reparacion);
        }
    }
}
