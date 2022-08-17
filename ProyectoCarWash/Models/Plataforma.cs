using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Models
{
    public class Plataforma
    {
        public int IdPlataforma { get; set; }
        public string Numero { get; set; }
        public string Detalle { get; set; }
        //public decimal Precio { get; set; }
        //public string PrecioTexto { get; set; }
        public Sucursal oSucursal { get; set; }
        public Categoria oCategoria { get; set; }
        public EstadoPlataforma oEstadoPlataforma { get; set; }
        public bool Estado { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaMOdificacion { get; set; }

    }
}