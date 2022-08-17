using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Models
{
    public class RecepcionLavado
    {
        public int IdRecepcionLavado { get; set; }
        public Vehiculo oVehiculo { get; set; }
        public Plataforma oPlataforma { get; set; }
        public Servicio oServicio { get; set; }
        public DateTime FechaEntrada { get; set; }
        public string FechaEntradaTexto { get; set; }
        public DateTime FechaSalida { get; set; }
        public string FechaSalidaTexto { get; set; }

        public DateTime FechaSalidaConfirmacion { get; set; }
        public string FechaSalidaConfirmacionTexto { get; set; }

        public decimal PrecioInicial { get; set; }
        public string PrecioIncialTexto { get; set; }


        public decimal Adelanto { get; set; }
        public string AdelantoTexto { get; set; }
        public decimal PrecioRestante { get; set; }
        public string PrecioRestanteTexto { get; set; }

        public decimal TotalPagado { get; set; }
        public string TotalPagadoTexto { get; set; }

        public decimal CostoPenalidad { get; set; }
        public string CostoPenalidadTexto { get; set; }

        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public List<Venta> oVenta { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaMOdificacion { get; set; }
    }
}