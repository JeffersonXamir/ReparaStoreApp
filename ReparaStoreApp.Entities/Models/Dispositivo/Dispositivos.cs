using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Dispositivo
{
    public class Dispositivos
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
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
        public bool Activo { get; set; }

        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }

        public ICollection<Reparacion> Reparaciones { get; set; }
    }

    public enum EstadoDispositivo
    {
        Ingresado,
        Chequeado,
        EnReparacion,
        Reparado,
        Entregado
    }
}
