using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public class ClientesItem : Item
    {
        private string _identificacion = string.Empty;
        public string Identificacion
        {
            get => _identificacion;
            set
            {
                _identificacion = value;
                NotifyOfPropertyChange(() => Identificacion);
            }
        }

        private string _primerNombre = string.Empty;
        public string PrimerNombre
        {
            get => _primerNombre;
            set
            {
                _primerNombre = value;
                NotifyOfPropertyChange(() => PrimerNombre);
            }
        }

        private string _segundoNombre = string.Empty;
        public string SegundoNombre
        {
            get => _segundoNombre;
            set
            {
                _segundoNombre = value;
                NotifyOfPropertyChange(() => SegundoNombre);
            }
        }

        private string _primerApellido = string.Empty;
        public string PrimerApellido
        {
            get => _primerApellido;
            set
            {
                _primerApellido = value;
                NotifyOfPropertyChange(() => PrimerApellido);
            }
        }

        private string _segundoApellido = string.Empty;
        public string SegundoApellido
        {
            get => _segundoApellido;
            set
            {
                _segundoApellido = value;
                NotifyOfPropertyChange(() => SegundoApellido);
            }
        }


        private DateTime _fechaNacimiento;
        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; NotifyOfPropertyChange(()=> FechaNacimiento); }
        }

        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Nota { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }
        public DateTime FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }
        //public bool Activo { get; set; }
        
        private bool _activo;
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; NotifyOfPropertyChange(()=> Activo); }
        }


        //public ICollection<Dispositivos> Dispositivos { get; set; }
    }
}
