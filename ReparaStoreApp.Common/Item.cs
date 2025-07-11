using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common
{
    public abstract class Item: PropertyChangedBase, IItem
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        //public string Name { get; set; } = string.Empty;
        
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyOfPropertyChange(()=> Name); }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public interface IItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
