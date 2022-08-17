using ProyectoCarWash.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoCarWash.Logica
{
    public class PisoLogica
    {
        private static PisoLogica instancia = null;

        public PisoLogica()
        {

        }

        public static PisoLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PisoLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Sucursal oPiso)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPiso", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", oPiso.Descripcion);
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

        public bool Modificar(Sucursal oPiso)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarPiso", oConexion);
                    cmd.Parameters.AddWithValue("IdPiso", oPiso.IdSucursal);
                    cmd.Parameters.AddWithValue("Descripcion", oPiso.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", oPiso.Estado);
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


        public List<Sucursal> Listar()
        {
            List<Sucursal> Lista = new List<Sucursal>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdSucursal,Descripcion,DireccionSucursal,Estado from Sucursal", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Sucursal()
                            {
                                IdSucursal = Convert.ToInt32(dr["IdSucursal"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                DireccionSucursal = dr["DireccionSucursal"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Sucursal>();
                }
            }
            return Lista;
        }
        public List<EstadoPlataforma> ListarEstadoPlataforma()
        {
            List<EstadoPlataforma> Lista = new List<EstadoPlataforma>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdEstadoPlataforma,Descripcion,Estado from ESTADO_PLATAFORMA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new EstadoPlataforma()
                            {
                                IdEstadoPlataforma = Convert.ToInt32(dr["IdEstadoPlataforma"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<EstadoPlataforma>();
                }
            }
            return Lista;
        }
        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE SUCURSAL set Estado = 0 where IdSucursal = @id", oConexion);
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
    }
}