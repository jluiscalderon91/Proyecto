using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public RecepcionLavado oRecepcion { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public List<DetalleVenta> oDetalleVenta { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaMOdificacion { get; set; }
    }
}