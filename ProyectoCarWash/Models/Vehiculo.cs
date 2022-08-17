using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Models
{
    public class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public TipoVehiculo oTipoVehiculo { get; set; }
        public Persona oPersona { get; set; }
        public bool Estado { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime FechaMOdificacion { get; set; }
    }
}