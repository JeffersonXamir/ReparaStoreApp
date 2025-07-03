using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Security.Security
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password);
        //string GenerateJwtToken(User user);
    }
}
