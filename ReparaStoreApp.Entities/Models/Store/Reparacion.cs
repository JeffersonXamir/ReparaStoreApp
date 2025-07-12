using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReparaStoreApp.Entities.Models.Ventas;

namespace ReparaStoreApp.Entities.Models.Store
{
    public class Reparacion
    {
        public int Id { get; set; }
        public string Numero { get; set; } 
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string NotasIngreso { get; set; } // Detalles del problema
        public string NotasReparado { get; set; } // Detalles de solución
        public decimal CostoEstimado { get; set; }
        public decimal CostoFinal { get; set; }
        public EstadoReparacion Estado { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }

        public DateTime? FechaAprobacion { get; set; }
        public int UsuarioAprobacionId { get; set; }
        public User UsuarioAprobacion { get; set; }
        
        public DateTime? FechaReparado { get; set; }
        public int UsuarioReparadoId { get; set; }
        public User UsuarioReparado { get; set; }

        // Relaciones
        public int DispositivoId { get; set; }
        public Dispositivos Dispositivo { get; set; }

        public int? TecnicoId { get; set; }
        public User? Tecnico { get; set; }

        public int? CajeroId { get; set; }
        public User? Cajero { get; set; }

        public List<ReparacionDetalle> Detalles { get; set; } = new();
        public int? FacturaId { get; set; }
        public Factura Factura { get; set; }

        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }
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
