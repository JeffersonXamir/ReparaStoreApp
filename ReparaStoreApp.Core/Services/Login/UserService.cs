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
    public class UserService: IUserService
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

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchText, int page, int pageSize)
        {
            return await _userRepository.SearchAsync(searchText, page, pageSize);
        }

        public async Task<int> GetUserCountAsync(string searchText)
        {
            return await _userRepository.GetCountAsync(searchText);
        }

        public async Task SaveUserAsync(User user)
        {
            await _userRepository.SaveAsync(user);
        }

        public async Task UpdateUserAsync(UserItem user)
        {
            var userEntity = _mapper.Map<User>(user);
            await _userRepository.SaveAsync(userEntity);
        }
    }
}
