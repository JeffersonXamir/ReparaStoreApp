using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Security
{
    public class AuthResult
    {
        public bool Success { get; }
        public string Token { get; }
        public int UserId { get; }
        public string ErrorMessage { get; }

        private AuthResult(bool success, string token, int userId, string errorMessage)
        {
            this.Success = success;
            this.Token = token;
            this.UserId = userId;
            this.ErrorMessage = errorMessage;
        }

        public static AuthResult Ok(string token, int userId)
            => new AuthResult(true, token, userId, null);

        public static AuthResult Fail(string errorMessage)
            => new AuthResult(false, null, 0, errorMessage);
    }
}
