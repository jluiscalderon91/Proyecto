using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Models
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public Producto oProducto { get; set; }
        public int IdVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaMOdificacion { get; set; }
    }
}