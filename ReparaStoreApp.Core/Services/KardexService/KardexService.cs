using AutoMapper;
using ReparaStoreApp.Data.Repositories.KardexRepository;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.KardexService
{
    public class KardexService : IKardexService
    {
        private readonly IKardexRepository _kardexRepository;
        private readonly IMapper _mapper;

        public KardexService(IKardexRepository kardexRepository, IMapper mapper)
        {
            _kardexRepository = kardexRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Kardex>> GetKardexReport(DateTime? fechaInicio, DateTime? fechaFin, int? tipoMovimiento, string filtroProducto)
        {
            var kardex = await _kardexRepository.GetKardexWithDetails(fechaInicio, fechaFin, tipoMovimiento, filtroProducto);
            return kardex;
        }
    }
}
