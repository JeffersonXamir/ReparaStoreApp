using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ServiciosService;
using ReparaStoreApp.Data.Repositories.ServiciosRepository;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.ServiciosService
{
    public class ServiciosService : IServiciosService
    {
        private readonly IServiciosRepository _ServiciosRepository;
        private readonly IMapper _mapper;

        public ServiciosService(IServiciosRepository ServiciosRepository, IMapper mapper)
        {
            _ServiciosRepository = ServiciosRepository;
            _mapper = mapper;
        }

        public async Task<ItemEntity> GetServiciosByIdAsync(int id)
        {
            return await _ServiciosRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ItemEntity>> SearchServiciosAsync(string searchText, int page, int pageSize)
        {
            return await _ServiciosRepository.SearchAsync(searchText, page, pageSize);
        }

        public async Task<int> GetServiciosCountAsync(string searchText)
        {
            return await _ServiciosRepository.GetCountAsync(searchText);
        }

        public async Task SaveServiciosAsync(ServiciosItem Servicios)
        {
            if (Servicios == null) return;
            var ServiciosDb = _mapper.Map<ItemEntity>(Servicios);
            ServiciosDb.Tipo = TipoItem.Servicio;
            ServiciosDb.FechaCreacion = DateTime.UtcNow;
            ServiciosDb.Activo = true;
            await _ServiciosRepository.SaveAsync(ServiciosDb);
        }

        public async Task UpdateServiciosAsync(ServiciosItem Servicios)
        {
            if (Servicios == null) return;
            var ServiciosDb = await _ServiciosRepository.GetByIdAsync(Servicios.Id);

            ServiciosDb.Codigo = Servicios.Code;
            ServiciosDb.Nombre = Servicios.Name;
            ServiciosDb.Descripcion = Servicios.Descripcion;
            ServiciosDb.Precio = Servicios.Precio;
            ServiciosDb.TieneIVA = Servicios.TieneIVA;
            ServiciosDb.Nota = Servicios.Nota;
            ServiciosDb.UsuarioEdicionId = Servicios.UsuarioEdicionId;
            ServiciosDb.FechaEdicion = Servicios.FechaEdicion;

            await _ServiciosRepository.SaveAsync(ServiciosDb);
        }

        public async Task ActivateServiciosAsync(ServiciosItem Servicios)
        {
            if (Servicios == null) return;
            var ServiciosDb = await _ServiciosRepository.GetByIdAsync(Servicios.Id);
            await _ServiciosRepository.Activate(ServiciosDb);
        }

        public async Task DeleteServiciosAsync(ServiciosItem Servicios)
        {
            if (Servicios == null) return;
            var ServiciosDb = await _ServiciosRepository.GetByIdAsync(Servicios.Id);
            await _ServiciosRepository.Delete(ServiciosDb);
        }

    }
}