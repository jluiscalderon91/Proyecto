using ProyectoCarWash.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoCarWash.Logica
{
    public class RecepcionLogica
    {
        private static RecepcionLogica instancia = null;

        public RecepcionLogica()
        {

        }

        public static RecepcionLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new RecepcionLogica();
                }

                return instancia;
            }
        }


        public List<RecepcionLavado> Listar()
        {
            List<RecepcionLavado> Lista = new List<RecepcionLavado>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select r.IdRecepcionLavado,v.IdVehiculo,v.Placa,p.Nombre,p.Apellido,p.Documento,p.Telefono,p.Correo,pl.IdPlataforma,pl.Numero,pl.Detalle,s.Descripcion[DesSucursal],");
                    query.AppendLine("convert(char(10), r.FechaEntrada, 103)[FechaEntrada], convert(char(10), r.FechaSalida, 103)[FechaSalida],r.PrecioInicial,r.Adelanto,r.PrecioRestante,r.Observacion,r.Estado");
                    query.AppendLine("from RECEPCION_LAVADO r");
                    query.AppendLine("inner join VEHICULO v on v.IdVehiculo = r.IdVehiculo");
                    query.AppendLine("inner join PERSONA p on p.IdPersona = v.IdPersona");
                    query.AppendLine("inner join PLATAFORMA pl on pl.IdPlataforma = r.IdPlataforma");
                    query.AppendLine("inner join SUCURSAL s on s.IdSucursal = pl.IdSucursal");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new RecepcionLavado()
                            {
                                IdRecepcionLavado = Convert.ToInt32(dr["IdRecepcionLavado"]),
                                oVehiculo = new Vehiculo()
                                {
                                    IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]),
                                    Placa = dr["Placa"].ToString(),
                                    oPersona = new Persona()
                                    {
                                        Nombre = dr["Nombre"].ToString(),
                                        Apellido = dr["Apellido"].ToString(),
                                        Documento = dr["Documento"].ToString(),
                                        Telefono = dr["Telefono"].ToString(),
                                        Correo = dr["Correo"].ToString(),
                                    }
                                },
                                oPlataforma = new Plataforma() {
                                    IdPlataforma = Convert.ToInt32(dr["IdPlataforma"]),
                                    Numero = dr["Numero"].ToString(),
                                    Detalle = dr["Detalle"].ToString(),
                                    oSucursal = new Sucursal() { Descripcion = dr["DesSucursal"].ToString() },
                                    //oCategoria = new Categoria() { Descripcion = dr["DesCategoria"].ToString() }
                                },
                                FechaEntradaTexto = dr["FechaEntrada"].ToString(),
                                FechaSalidaTexto = dr["FechaSalida"].ToString(),
                                PrecioInicial = Convert.ToDecimal(dr["PrecioInicial"].ToString(),new CultureInfo("es-PE")),
                                Adelanto = Convert.ToDecimal(dr["Adelanto"].ToString(), new CultureInfo("es-PE")),
                                PrecioRestante = Convert.ToDecimal(dr["PrecioRestante"].ToString(), new CultureInfo("es-PE")),
                                Observacion = dr["Observacion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<RecepcionLavado>();
                }
            }
            return Lista;
        }

        public bool Registrar(RecepcionLavado objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarRecepcion", oConexion);
                    cmd.Parameters.AddWithValue("IdCliente", objeto.oVehiculo.oPersona.IdPersona);
                    cmd.Parameters.AddWithValue("TipoDocumento", objeto.oVehiculo.oPersona.TipoDocumento);
                    cmd.Parameters.AddWithValue("Documento", objeto.oVehiculo.oPersona.Documento);
                    cmd.Parameters.AddWithValue("Nombre", objeto.oVehiculo.oPersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", objeto.oVehiculo.oPersona.Apellido);
                    cmd.Parameters.AddWithValue("Telefono", objeto.oVehiculo.oPersona.Telefono);
                    cmd.Parameters.AddWithValue("Correo", objeto.oVehiculo.oPersona.Correo);
                    cmd.Parameters.AddWithValue("IdVehiculo", objeto.oVehiculo.IdVehiculo);
                    cmd.Parameters.AddWithValue("Placa", objeto.oVehiculo.Placa);
                    cmd.Parameters.AddWithValue("Marca", objeto.oVehiculo.Marca);
                    cmd.Parameters.AddWithValue("Modelo", objeto.oVehiculo.Modelo);
                    cmd.Parameters.AddWithValue("Color", objeto.oVehiculo.Color);
                    cmd.Parameters.AddWithValue("IdTipoVehiculo", objeto.oVehiculo.oTipoVehiculo.IdTipoVehiculo);
                    cmd.Parameters.AddWithValue("IdServicio", objeto.oServicio.IdServicio);
                    cmd.Parameters.AddWithValue("IdPlataforma", objeto.oPlataforma.IdPlataforma);
                    cmd.Parameters.AddWithValue("FechaSalida", Convert.ToDateTime(objeto.FechaSalidaTexto,new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("PrecioInicial", Convert.ToDecimal(objeto.PrecioIncialTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("Adelanto", Convert.ToDecimal(objeto.AdelantoTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("PrecioRestante", Convert.ToDecimal(objeto.PrecioRestanteTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("Observacion", objeto.Observacion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Cerrar(RecepcionLavado objeto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {

                oConexion.Open();
                SqlTransaction objTransacion = oConexion.BeginTransaction();

                try
                {
                    
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("update recepcion set FechaSalidaConfirmacion = GETDATE() , TotalPagado = @totapagado , CostoPenalidad = @costopenalidad, Estado = 0");
                    query.AppendLine("where IdRecepcion = @idrecepecion");
                    query.AppendLine("");
                    query.AppendLine("update HABITACION set IdEstadoHabitacion = 3");
                    query.AppendLine("where IdHabitacion = @idhabitacion");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@totapagado", Convert.ToDecimal(objeto.TotalPagadoTexto,new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("@costopenalidad", Convert.ToDecimal(objeto.CostoPenalidadTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("@idrecepecion", objeto.IdRecepcionLavado);
                    cmd.Parameters.AddWithValue("@idhabitacion", objeto.oPlataforma.IdPlataforma);
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = objTransacion;

                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    objTransacion.Commit();

                }
                catch (Exception ex)
                {
                    objTransacion.Rollback();
                    respuesta = false;
                }

            }

            return respuesta;

        }


    }
}