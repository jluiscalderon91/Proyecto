using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public decimal Precio { get; set; }
        public string PrecioTexto { get; set; }
        public int Cantidad { get; set; }
        public Sucursal oSucursal { get; set; }
        public bool Estado { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaMOdificacion { get; set; }
    }
}