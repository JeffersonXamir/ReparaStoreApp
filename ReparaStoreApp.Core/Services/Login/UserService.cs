using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Data.Repositories.Login;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.Login
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchText, int page, int pageSize, string? filter = null)
        {
            return await _userRepository.SearchAsync(searchText, page, pageSize,filter);
        }

        public async Task<int> GetUserCountAsync(string searchText)
        {
            return await _userRepository.GetCountAsync(searchText);
        }

        public async Task SaveUserAsync(UserItem user)
        {
            if (user == null) return;
            var userDb = _mapper.Map<User>(user);
            userDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            userDb.CreatedAt = DateTime.UtcNow;
            userDb.IsActive = true;
            
            await _userRepository.SaveAsync(userDb);
        }

        public async Task UpdateUserAsync(UserItem user)
        {
            if (user == null) return;
            var userDb = await _userRepository.GetByIdAsync(user.Id);

            userDb.Code = user.Code;
            userDb.Name = user.Name;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.PhoneNumber = user.PhoneNumber;
            userDb.Note = user.Note;
            userDb.UserRoles = user.UserRoles;

            // Verificar si el campo de contraseña tiene algo
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                // Asumimos que se quiere cambiar la contraseña
                userDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            }

            await _userRepository.SaveAsync(userDb);
        }

        public async Task ActivateUserAsync(UserItem user)
        {
            if (user == null) return;
            var userDb = await _userRepository.GetByIdAsync(user.Id);
            await _userRepository.Activate(userDb);
        }

        public async Task DeleteUserAsync(UserItem user)
        {
            if (user == null) return;
            var userDb = await _userRepository.GetByIdAsync(user.Id);
            await _userRepository.Delete(userDb);
        }

        public async Task<ParamsItem> GetParamByCode(string code)
        {
            var param = await _userRepository.GetParamByCode(code);
            return _mapper.Map<ParamsItem>(param);
        }

        public async Task<IEnumerable<Role>> SearchRolesAsync(string searchText, int page, int pageSize)
        {
            return await _userRepository.SearchRolesAsync(searchText, page, pageSize);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(string? filter = null)
        {
            return await _userRepository.GetAllUsersAsync(filter);
        }
    }
}
