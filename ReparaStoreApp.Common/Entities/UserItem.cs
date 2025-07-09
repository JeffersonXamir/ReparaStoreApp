using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public class UserItem: Item
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; NotifyOfPropertyChange(()=> IsActive); }
        }

    }
}
