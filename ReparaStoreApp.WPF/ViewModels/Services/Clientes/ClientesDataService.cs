using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ClientesService;
using ReparaStoreApp.WPF.ViewModels.Services.Users;

namespace ReparaStoreApp.WPF.ViewModels.Services.Clientes
{
    internal class ClientesDataService : IDataService<ClientesItem>
    {
        private readonly IClientesService _ClientesService;
        private readonly IMapper _mapper;

        public ClientesDataService(IClientesService ClientesService, IMapper mapper)
        {
            _ClientesService = ClientesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientesItem>> SearchAsync(string searchText, int page, int pageSize)
        {
            var Clientess = await _ClientesService.SearchClientessAsync(searchText, page, pageSize);
            return _mapper.Map<IEnumerable<ClientesItem>>(Clientess);
        }

        public async Task<int> GetTotalCountAsync(string searchText)
        {
            return await _ClientesService.GetClientesCountAsync(searchText);
        }

        public async Task<ClientesItem> GetByIdAsync(int id)
        {
            var Clientes = await _ClientesService.GetClientesByIdAsync(id);
            return _mapper.Map<ClientesItem>(Clientes);
        }
    }
}
