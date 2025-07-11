using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Data.Repositories.ClientesRepository;
using ReparaStoreApp.Data.Repositories.Login;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.ClientesService
{
    public class ClientesService : IClientesService
    {
        private readonly IClientesRepository _ClientesRepository;
        private readonly IMapper _mapper;

        public ClientesService(IClientesRepository ClientesRepository, IMapper mapper)
        {
            _ClientesRepository = ClientesRepository;
            _mapper = mapper;
        }

        public async Task<Clientes> GetClientesByIdAsync(int id)
        {
            return await _ClientesRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Clientes>> SearchClientessAsync(string searchText, int page, int pageSize)
        {
            return await _ClientesRepository.SearchAsync(searchText, page, pageSize);
        }

        public async Task<int> GetClientesCountAsync(string searchText)
        {
            return await _ClientesRepository.GetCountAsync(searchText);
        }

        public async Task SaveClientesAsync(ClientesItem Clientes)
        {
            if (Clientes == null) return;
            var ClientesDb = _mapper.Map<Clientes>(Clientes);
            ClientesDb.FechaCreacion = DateTime.UtcNow;
            ClientesDb.Activo = true;
            await _ClientesRepository.SaveAsync(ClientesDb);
        }

        public async Task UpdateClientesAsync(ClientesItem Clientes)
        {
            if (Clientes == null) return;
            var ClientesDb = await _ClientesRepository.GetByIdAsync(Clientes.Id);

            ClientesDb.Codigo = Clientes.Code;
            ClientesDb.Nombre = Clientes.Name;
            ClientesDb.Identificacion = Clientes.Identificacion;
            ClientesDb.PrimerNombre = Clientes.PrimerNombre;
            ClientesDb.SegundoNombre = Clientes.SegundoNombre;
            ClientesDb.PrimerApellido = Clientes.PrimerApellido;
            ClientesDb.SegundoApellido = Clientes.SegundoApellido;
            ClientesDb.FechaNacimiento = Clientes.FechaNacimiento;
            ClientesDb.Correo = Clientes.Correo;
            ClientesDb.Telefono = Clientes.Telefono;
            ClientesDb.Direccion = Clientes.Direccion;
            ClientesDb.Nota = Clientes.Nota;
            ClientesDb.UsuarioEdicionId = Clientes.UsuarioEdicionId;
            ClientesDb.FechaEdicion = Clientes.FechaEdicion;


            await _ClientesRepository.SaveAsync(ClientesDb);
        }

        public async Task ActivateClientesAsync(ClientesItem Clientes)
        {
            if (Clientes == null) return;
            var ClientesDb = await _ClientesRepository.GetByIdAsync(Clientes.Id);
            await _ClientesRepository.Activate(ClientesDb);
        }

        public async Task DeleteClientesAsync(ClientesItem Clientes)
        {
            if (Clientes == null) return;
            var ClientesDb = await _ClientesRepository.GetByIdAsync(Clientes.Id);
            await _ClientesRepository.Delete(ClientesDb);
        }

    }
}
