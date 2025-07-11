using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public class DispositivosItem : Item
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string NumeroSerie { get; set; }
        public string Descripcion { get; set; }
        public EstadoDispositivo Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }
        public DateTime FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }
        
        private bool _activo;
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; NotifyOfPropertyChange(() => Activo); }
        }

        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }

        //public ICollection<Reparacion> Reparaciones { get; set; }
    }
}
