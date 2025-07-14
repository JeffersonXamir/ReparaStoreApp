using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common
{
    public abstract class DocumentItem : PropertyChangedBase, IDocumentItem
    {
        public int Id { get; set; }
        private string _numero = string.Empty;
        public string Numero
        {
            get => _numero;
            set
            {
                _numero = value;
                NotifyOfPropertyChange(() => Numero);
            }
        }

        private string _detalle = string.Empty;
        public string Detalle
        {
            get => _detalle;
            set
            {
                _detalle = value;
                NotifyOfPropertyChange(() => Detalle);
            }
        }

        private bool _activo;
        public bool Activo
        {
            get => _activo;
            set
            {
                _activo = value;
                NotifyOfPropertyChange(() => Activo);
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public interface IDocumentItem
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Detalle { get; set; }
        public bool Activo { get; set; }
    }
}
