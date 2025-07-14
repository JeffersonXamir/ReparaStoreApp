using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public class UserItem: Item
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; NotifyOfPropertyChange(()=> IsActive); }
        }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
