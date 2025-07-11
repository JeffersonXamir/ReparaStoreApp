using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.DispositivosService;
using ReparaStoreApp.Data.Repositories.DispositivosRepository;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.DispositivosService
{
    public class DispositivosService : IDispositivosService
    {
        private readonly IDispositivosRepository _DispositivosRepository;
        private readonly IMapper _mapper;

        public DispositivosService(IDispositivosRepository DispositivosRepository, IMapper mapper)
        {
            _DispositivosRepository = DispositivosRepository;
            _mapper = mapper;
        }

        public async Task<Dispositivos> GetDispositivosByIdAsync(int id)
        {
            return await _DispositivosRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Dispositivos>> SearchDispositivossAsync(string searchText, int page, int pageSize)
        {
            return await _DispositivosRepository.SearchAsync(searchText, page, pageSize);
        }

        public async Task<int> GetDispositivosCountAsync(string searchText)
        {
            return await _DispositivosRepository.GetCountAsync(searchText);
        }

        public async Task SaveDispositivosAsync(DispositivosItem Dispositivos)
        {
            if (Dispositivos == null) return;
            var DispositivosDb = _mapper.Map<Dispositivos>(Dispositivos);
            DispositivosDb.FechaCreacion = DateTime.UtcNow;
            DispositivosDb.Activo = true;
            await _DispositivosRepository.SaveAsync(DispositivosDb);
        }

        public async Task UpdateDispositivosAsync(DispositivosItem Dispositivos)
        {
            if (Dispositivos == null) return;
            var DispositivosDb = await _DispositivosRepository.GetByIdAsync(Dispositivos.Id);

            DispositivosDb.Codigo = Dispositivos.Code;
            DispositivosDb.Nombre = Dispositivos.Name;
            DispositivosDb.Marca = Dispositivos.Marca;
            DispositivosDb.Modelo = Dispositivos.Modelo;
            DispositivosDb.NumeroSerie = Dispositivos.NumeroSerie;
            DispositivosDb.Descripcion = Dispositivos.Descripcion;
            DispositivosDb.Estado = Dispositivos.Estado;
            DispositivosDb.ClienteId = Dispositivos.ClienteId;
            //DispositivosDb.Nota = Dispositivos.Nota;
            DispositivosDb.UsuarioEdicionId = Dispositivos.UsuarioEdicionId;
            DispositivosDb.FechaEdicion = Dispositivos.FechaEdicion;


            await _DispositivosRepository.SaveAsync(DispositivosDb);
        }

        public async Task ActivateDispositivosAsync(DispositivosItem Dispositivos)
        {
            if (Dispositivos == null) return;
            var DispositivosDb = await _DispositivosRepository.GetByIdAsync(Dispositivos.Id);
            await _DispositivosRepository.Activate(DispositivosDb);
        }

        public async Task DeleteDispositivosAsync(DispositivosItem Dispositivos)
        {
            if (Dispositivos == null) return;
            var DispositivosDb = await _DispositivosRepository.GetByIdAsync(Dispositivos.Id);
            await _DispositivosRepository.Delete(DispositivosDb);
        }

    }
}
