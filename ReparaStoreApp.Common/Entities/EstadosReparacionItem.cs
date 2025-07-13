using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common.Entities
{
    public class EstadosReparacionItem : Item
    {
        public EstadoReparacion Estado { get; set; }
    }
}
