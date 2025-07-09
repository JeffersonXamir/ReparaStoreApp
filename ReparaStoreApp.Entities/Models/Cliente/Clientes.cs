using ReparaStoreApp.Entities.Models.Dispositivo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Cliente
{
    public class Clientes
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Correo { get; set; }
        [Required]
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

        public ICollection<Dispositivos> Dispositivos { get; set; }
    }
}
