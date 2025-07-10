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
            //ClientesDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Clientes.PasswordHash);
            //ClientesDb.CreatedAt = DateTime.UtcNow;
            //ClientesDb.IsActive = true;
            await _ClientesRepository.SaveAsync(ClientesDb);
        }

        public async Task UpdateClientesAsync(ClientesItem Clientes)
        {
            if (Clientes == null) return;
            var ClientesDb = await _ClientesRepository.GetByIdAsync(Clientes.Id);

            //ClientesDb.Code = Clientes.Code;
            //ClientesDb.Name = Clientes.Name;
            //ClientesDb.FirstName = Clientes.FirstName;
            //ClientesDb.LastName = Clientes.LastName;
            //ClientesDb.Email = Clientes.Email;
            //ClientesDb.PhoneNumber = Clientes.PhoneNumber;
            //ClientesDb.Note = Clientes.Note;


            // Verificar si el campo de contraseña tiene algo
            //if (!string.IsNullOrWhiteSpace(Clientes.PasswordHash))
            //{
            //    // Asumimos que se quiere cambiar la contraseña
            //    ClientesDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Clientes.PasswordHash);
            //}

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
