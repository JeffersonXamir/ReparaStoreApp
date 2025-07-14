using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Data.Repositories.StockRepository;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.StockService
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockItem>> GetStockReport(string filtroProducto)
        {
            var inventario = await _stockRepository.GetStockWithProducts(filtroProducto ?? "");
            var StockItems = _mapper.Map<IEnumerable<StockItem>>(inventario);
            return StockItems;
        }
    }
}
