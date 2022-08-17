using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Models
{
    public class EstadoPlataforma
    {
        public int IdEstadoPlataforma { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaMOdificacion { get; set; }
    }
}