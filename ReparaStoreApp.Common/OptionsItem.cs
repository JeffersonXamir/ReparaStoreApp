using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common
{
    public class OptionsItem : Item
    {
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; NotifyOfPropertyChange(() => IsActive); }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                NotifyOfPropertyChange(() => IsChecked);
            }
        }
    }
}
