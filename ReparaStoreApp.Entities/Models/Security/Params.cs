﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Entities.Models.Security
{
    public class Params
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Valor { get; set; }
        public string Nota { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreadorId { get; set; }
        public User UsuarioCreador { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public int? UsuarioEdicionId { get; set; }
        public User UsuarioEdicion { get; set; }

    }
}
