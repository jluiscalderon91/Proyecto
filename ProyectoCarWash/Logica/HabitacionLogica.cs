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
    public class HabitacionLogica
    {
        private static HabitacionLogica instancia = null;

        public HabitacionLogica()
        {

        }

        public static HabitacionLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new HabitacionLogica();
                }

                return instancia;
            }
        }
        public bool RegistrarServicio(Servicio oServicio)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarServicio", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oServicio.Nombre);
                    cmd.Parameters.AddWithValue("Detalle", oServicio.Detalle);
                    cmd.Parameters.AddWithValue("Precio", Convert.ToDecimal(oServicio.PrecioTexto,new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("IdSucursal", oServicio.oSucursal.IdSucursal);
                    cmd.Parameters.AddWithValue("IdCategoria", oServicio.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Estado", oServicio.Estado);
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
        public bool ModificarServicio(Servicio oServicio)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarServicio", oConexion);
                    cmd.Parameters.AddWithValue("IdServicio", oServicio.IdServicio);
                    cmd.Parameters.AddWithValue("Nombre", oServicio.Nombre);
                    cmd.Parameters.AddWithValue("Detalle", oServicio.Detalle);
                    cmd.Parameters.AddWithValue("Precio", Convert.ToDecimal(oServicio.PrecioTexto, new CultureInfo("es-PE")));
                    cmd.Parameters.AddWithValue("IdSucursal", oServicio.oSucursal.IdSucursal);
                    cmd.Parameters.AddWithValue("IdCategoria", oServicio.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Estado", oServicio.Estado);
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
        public bool RegistrarPlataforma(Plataforma oPlataforma)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPlataforma", oConexion);
                    cmd.Parameters.AddWithValue("Numero", oPlataforma.Numero);
                    cmd.Parameters.AddWithValue("Detalle", oPlataforma.Detalle);
                    cmd.Parameters.AddWithValue("IdSucursal", oPlataforma.oSucursal.IdSucursal);
                    cmd.Parameters.AddWithValue("IdEstadoPlataforma", oPlataforma.oEstadoPlataforma.IdEstadoPlataforma);
                    cmd.Parameters.AddWithValue("Estado", oPlataforma.Estado);
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
        public bool ModificarPlataforma(Plataforma oPlataforma)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarPlataforma", oConexion);
                    cmd.Parameters.AddWithValue("IdPlataforma", oPlataforma.IdPlataforma);
                    cmd.Parameters.AddWithValue("Numero", oPlataforma.Numero);
                    cmd.Parameters.AddWithValue("Detalle", oPlataforma.Detalle);
                    cmd.Parameters.AddWithValue("IdSucursal", oPlataforma.oSucursal.IdSucursal);
                    cmd.Parameters.AddWithValue("IdEstadoPlataforma", oPlataforma.oEstadoPlataforma.IdEstadoPlataforma);
                    cmd.Parameters.AddWithValue("Estado", oPlataforma.Estado);
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
        public List<Plataforma> Listar()
        {
            List<Plataforma> Lista = new List<Plataforma>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.IdPlataforma,p.Numero,p.Detalle,s.IdSucursal,s.Descripcion[DescripcionSucursal],p.Estado,");
                    query.AppendLine("ep.IdEstadoPlataforma,ep.Descripcion[DescripcionEstadoPlataforma]");
                    query.AppendLine("from plataforma p");
                    query.AppendLine("inner join sucursal s on s.IdSucursal = p.IdSucursal");
                    //query.AppendLine("inner join CATEGORIA c on c.IdCategoria = h.IdCategoria");
                    query.AppendLine("inner join ESTADO_PLATAFORMA ep on ep.IdEstadoPlataforma = p.IdEstadoPlataforma");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Plataforma()
                            {
                                IdPlataforma = Convert.ToInt32(dr["IdPlataforma"]),
                                Numero = dr["Numero"].ToString(),
                                Detalle = dr["Detalle"].ToString(),
                                //Precio = Convert.ToDecimal(dr["Precio"].ToString()),
                                oSucursal = new Sucursal() { IdSucursal = Convert.ToInt32(dr["IdSucursal"]), Descripcion = dr["DescripcionSucursal"].ToString() },
                                //oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DescripcionCategoria"].ToString() },
                                oEstadoPlataforma = new EstadoPlataforma() { IdEstadoPlataforma = Convert.ToInt32(dr["IdEstadoPlataforma"]) , Descripcion = dr["DescripcionEstadoPlataforma"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Plataforma>();
                }
            }
            return Lista;
        }
        public List<Servicio> ListarServicio()
        {
            List<Servicio> Lista = new List<Servicio>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select se.IdServicio,se.Nombre,se.Detalle,se.Precio,s.IdSucursal,s.Descripcion[DescripcionSucursal],");
                    query.AppendLine("se.IdCategoria, c.Descripcion[DescripcionCategoria],se.Estado");
                    query.AppendLine("from SERVICIO se");
                    query.AppendLine("inner join SUCURSAL s on se.IdSucursal = s.IdSucursal");
                    query.AppendLine("inner join CATEGORIA c on c.IdCategoria = se.IdCategoria");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Servicio()
                            {
                                IdServicio = Convert.ToInt32(dr["IdServicio"]),
                                Nombre = dr["Nombre"].ToString(),
                                Detalle = dr["Detalle"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"].ToString()),
                                oSucursal = new Sucursal() { IdSucursal = Convert.ToInt32(dr["IdSucursal"]), Descripcion = dr["DescripcionSucursal"].ToString() },
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DescripcionCategoria"].ToString() },
                                //oEstadoPlataforma = new EstadoPlataforma() { IdEstadoPlataforma = Convert.ToInt32(dr["IdEstadoPlataforma"]), Descripcion = dr["DescripcionEstadoPlataforma"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Servicio>();
                }
            }
            return Lista;
        }
        public List<Plataforma> ListarPlataformas()
        {
            List<Plataforma> Lista = new List<Plataforma>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.IdPlataforma,p.Numero,p.Detalle,s.IdSucursal,s.Descripcion[DescripcionSucursal],");
                    query.AppendLine("p.IdEstadoPlataforma,ep.Descripcion[DescripcionEstadoPlataforma],p.Estado");
                    query.AppendLine("from PLATAFORMA p");
                    query.AppendLine("inner join SUCURSAL s on p.IdSucursal = s.IdSucursal");
                    query.AppendLine("inner join ESTADO_PLATAFORMA ep on ep.IdEstadoPlataforma = p.IdEstadoPlataforma where P.Estado = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Plataforma()
                            {
                                IdPlataforma = Convert.ToInt32(dr["IdPlataforma"]),
                                Numero = dr["Numero"].ToString(),
                                Detalle = dr["Detalle"].ToString(),
                                oSucursal = new Sucursal() { IdSucursal = Convert.ToInt32(dr["IdSucursal"]), Descripcion = dr["DescripcionSucursal"].ToString() },
                                oEstadoPlataforma = new EstadoPlataforma() { IdEstadoPlataforma = Convert.ToInt32(dr["IdEstadoPlataforma"]), Descripcion = dr["DescripcionEstadoPlataforma"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Plataforma>();
                }
            }
            return Lista;
        }
        public bool EliminarServicio(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update SERVICIO set Estado = 0 where IdServicio = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
        public bool EliminarPlataforma(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update PLATAFORMA set Estado = 0 where IdPlataforma = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool ActualizarEstado(int idhabitacion, int idestadohabitacion)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update HABITACION set idestadohabitacion = @idestadohabitacion where IdHabitacion = @idhabitacion ", oConexion);
                    cmd.Parameters.AddWithValue("@idhabitacion", idhabitacion);
                    cmd.Parameters.AddWithValue("@idestadohabitacion", idestadohabitacion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
}