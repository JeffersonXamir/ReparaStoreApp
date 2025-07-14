using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Services.Users
{
    public class UserDataService : IDataService<UserItem>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserDataService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserItem>> SearchAsync(string searchText, int page, int pageSize)
        {
            var users = await _userService.SearchUsersAsync(searchText, page, pageSize);
            return _mapper.Map<IEnumerable<UserItem>>(users);
        }

        public async Task<int> GetTotalCountAsync(string searchText)
        {
            return await _userService.GetUserCountAsync(searchText);
        }

        public async Task<UserItem> GetByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return _mapper.Map<UserItem>(user);
        }
    }
}
