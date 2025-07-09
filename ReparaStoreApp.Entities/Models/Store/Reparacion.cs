using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Store
{
    public class Reparacion
    {
        public int Id { get; set; }
        [Required]
        public string Diagnostico { get; set; }
        public decimal CostoEstimado { get; set; }
        public decimal CostoFinal { get; set; }
        public EstadoReparacion Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? FechaCompletado { get; set; }

        public int DispositivoId { get; set; }
        public Dispositivos Dispositivo { get; set; }

        public int TecnicoId { get; set; }
        public User Tecnico { get; set; }

        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }
    }

    public enum EstadoReparacion
    {
        Pendiente,
        Aprobado,
        EnProceso,
        Completado,
        Rechazado
    }
}
