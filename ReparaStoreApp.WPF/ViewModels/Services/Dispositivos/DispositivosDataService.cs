using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.DispositivosService;
using ReparaStoreApp.WPF.ViewModels.Services.Users;

namespace ReparaStoreApp.WPF.ViewModels.Services.Dispositivos
{
    public class DispositivosDataService : IDataService<DispositivosItem>
    {
        private readonly IDispositivosService _DispositivosService;
        private readonly IMapper _mapper;

        public DispositivosDataService(IDispositivosService DispositivosService, IMapper mapper)
        {
            _DispositivosService = DispositivosService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DispositivosItem>> SearchAsync(string searchText, int page, int pageSize)
        {
            var Dispositivoss = await _DispositivosService.SearchDispositivossAsync(searchText, page, pageSize);
            return _mapper.Map<IEnumerable<DispositivosItem>>(Dispositivoss);
        }

        public async Task<int> GetTotalCountAsync(string searchText)
        {
            return await _DispositivosService.GetDispositivosCountAsync(searchText);
        }

        public async Task<DispositivosItem> GetByIdAsync(int id)
        {
            var Dispositivos = await _DispositivosService.GetDispositivosByIdAsync(id);
            return _mapper.Map<DispositivosItem>(Dispositivos);
        }
    }
}

