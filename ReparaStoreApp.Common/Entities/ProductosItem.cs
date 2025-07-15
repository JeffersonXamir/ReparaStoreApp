using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public  class ProductosItem : Item
    {
        private string _descripcion = "";
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; NotifyOfPropertyChange(() => Descripcion); }
        }

        private string _nota = "";
        public string Nota
        {
            get { return _nota; }
            set { _nota = value; NotifyOfPropertyChange(() => Nota); }
        }

        private decimal _precio;
        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; NotifyOfPropertyChange(() => Precio); }
        }

        private TipoItem _tipo;
        public TipoItem Tipo
        {
            get { return _tipo; }
            set { _tipo = value; NotifyOfPropertyChange(() => Tipo); }
        }


        private bool _activo;
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; NotifyOfPropertyChange(() => Activo); }
        }

        private bool _tieneIVA = true;
        public bool TieneIVA
        {
            get { return _tieneIVA; }
            set { _tieneIVA = value; NotifyOfPropertyChange(() => TieneIVA); }
        }

        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }
    }
}
