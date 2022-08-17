using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Clave { get; set; }
        public Persona oPersona { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }
    }
}