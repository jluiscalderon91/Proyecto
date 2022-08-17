using ProyectoCarWash.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace ProyectoCarWash.Logica
{
    public class PersonaLogica
    {

        private static PersonaLogica instancia = null;

        public PersonaLogica()
        {

        }

        public static PersonaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PersonaLogica();
                }

                return instancia;
            }
        }

        public List<TipoPersona> ListarTipoPersona()
        {
            List<TipoPersona> Lista = new List<TipoPersona>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select IdTipoPersona,Descripcion from TIPO_PERSONA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new TipoPersona()
                            {
                                IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<TipoPersona>();
                }
            }
            return Lista;
        }

        public bool Registrar(Persona objeto)
        {
            DataTable dtVehiculos = ToDataTable(objeto.oVehicles);

            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarPersona", oConexion);
                    cmd.Parameters.AddWithValue("TipoDocumento", objeto.TipoDocumento);
                    cmd.Parameters.AddWithValue("Documento", objeto.Documento);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", objeto.Apellido);
                    cmd.Parameters.AddWithValue("Telefono", objeto.Telefono);
                    cmd.Parameters.AddWithValue("Correo", objeto.Correo);
                    cmd.Parameters.AddWithValue("IdTipoPersona", objeto.oTipoPersona.IdTipoPersona);
                    cmd.Parameters.AddWithValue("Estado", objeto.Estado);
                    cmd.Parameters.Add("Vehiculos", SqlDbType.Structured).Value = dtVehiculos;
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

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name == "oTipoVehiculo" ? "IdTipoVehiculo" : prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    if (Props[i].Name == "oTipoVehiculo")
                    {
                        values[i] = ((TipoVehiculo)Props[i].GetValue(item, null)).IdTipoVehiculo;
                    }
                    else {
                        values[i] = Props[i].GetValue(item, null);
                    }
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public bool Modificar(Persona objeto)
        {
            bool respuesta = true;
            DataTable dtVehiculos = ToDataTable(objeto.oVehicles);
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarPersona", oConexion);
                    cmd.Parameters.AddWithValue("IdPersona", objeto.IdPersona);
                    cmd.Parameters.AddWithValue("TipoDocumento", objeto.TipoDocumento);
                    cmd.Parameters.AddWithValue("Documento", objeto.Documento);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", objeto.Apellido);
                    cmd.Parameters.AddWithValue("Telefono", objeto.Telefono);
                    cmd.Parameters.AddWithValue("Correo", objeto.Correo);
                    cmd.Parameters.AddWithValue("IdTipoPersona", objeto.oTipoPersona.IdTipoPersona);
                    cmd.Parameters.AddWithValue("Estado", objeto.Estado);
                    cmd.Parameters.Add("Vehiculos", SqlDbType.Structured).Value = dtVehiculos;
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

        public List<Persona> Listar()
        {
            List<Persona> Lista = new List<Persona>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select p.IdPersona,p.TipoDocumento,p.Documento,p.Nombre,p.Apellido,p.Correo,u.IdUsuario,u.UserName,u.Clave, tp.IdTipoPersona,tp.Descripcion, p.Estado from persona p");
                    sb.AppendLine("inner join TIPO_PERSONA tp on tp.IdTipoPersona = p.IdTipoPersona");
                    sb.AppendLine("inner join USUARIO u on p.IdPersona = u.IdPersona");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Persona()
                            {
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                Documento = dr["Documento"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                //Clave = dr["Clave"].ToString(),
                                oUsuario = new Usuario() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]), UserName = dr["UserName"].ToString(), Clave = dr["Clave"].ToString() },
                                oTipoPersona = new TipoPersona() { IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]), Descripcion = dr["Descripcion"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Persona>();
                }
            }
            return Lista;
        }
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> Lista = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT u.IdUsuario, u.UserName, u.Clave, p.IdPersona, p.TipoDocumento, p.Documento, p.Nombre, p.Apellido, p.Telefono, p.Correo, tp.IdTipoPersona, tp.Descripcion[DescripcionTipoDocumento], u.Estado");
                    sb.AppendLine("FROM USUARIO u");
                    sb.AppendLine("INNER JOIN PERSONA p ON u.IdPersona = p.IdPersona");
                    sb.AppendLine("INNER JOIN TIPO_PERSONA tp ON tp.IdTipoPersona = p.IdTipoPersona");
                    //sb.AppendLine("inner join USUARIO u on p.IdPersona = u.IdPersona");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                UserName = dr["UserName"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                oPersona = new Persona() { IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                                           TipoDocumento = dr["TipoDocumento"].ToString(),
                                                           Documento = dr["Documento"].ToString(),
                                                           Nombre = dr["Nombre"].ToString(),
                                                           Apellido = dr["Apellido"].ToString(),
                                                           Telefono = dr["Telefono"].ToString(),
                                                           Correo = dr["Correo"].ToString(),
                                                           oTipoPersona = new TipoPersona() { IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]), Descripcion = dr["DescripcionTipoDocumento"].ToString() }
                                                         },                         
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Usuario>();
                }
            }
            return Lista;
        }
        public List<Persona> ListarPersonas()
        {
            List<Persona> Lista = new List<Persona>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select p.IdPersona,p.TipoDocumento,p.Documento,p.Nombre,p.Apellido,p.Telefono,p.Correo,tp.IdTipoPersona,tp.Descripcion,p.Estado from persona p");
                    sb.AppendLine("inner join TIPO_PERSONA tp on tp.IdTipoPersona = p.IdTipoPersona");
                    //sb.AppendLine("inner join USUARIO u on p.IdPersona = u.IdPersona");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Persona()
                            {
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                Documento = dr["Documento"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                oTipoPersona = new TipoPersona() { IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]), Descripcion = dr["Descripcion"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Persona>();
                }
            }
            return Lista;
        }

        public List<Vehiculo> ListarVehiculos()
        {
            List<Vehiculo> Lista = new List<Vehiculo>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select v.IdVehiculo,v.Placa,v.Marca,v.Modelo,v.Color,tv.IdTipoVehiculo,tv.Descripcion[DescripcionTipoVehiculo],p.IdPersona,");
                    sb.AppendLine("p.TipoDocumento,p.Documento,p.Nombre,p.Apellido,p.Telefono,p.Correo,tp.IdTipoPersona,tp.Descripcion[DescripcionTipoPersona] from persona p");
                    sb.AppendLine("inner join VEHICULO v on p.IdPersona = v.IdPersona");
                    sb.AppendLine("inner join TIPO_VEHICULO tv on tv.IdTipoVehiculo = v.IdTipoVehiculo");
                    sb.AppendLine("inner join TIPO_PERSONA tp on tp.IdTipoPersona = p.IdTipoPersona WHERE v.Estado = 1 AND P.Estado = 1");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Vehiculo()
                            {
                                IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]),
                                Placa = dr["Placa"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Color = dr["Color"].ToString(),
                                oTipoVehiculo = new TipoVehiculo() { 
                                    IdTipoVehiculo = Convert.ToInt32(dr["IdTipoVehiculo"]), 
                                    Descripcion = dr["DescripcionTipoVehiculo"].ToString()
                                },
                                oPersona = new Persona() { 
                                    IdPersona = Convert.ToInt32(dr["IdPersona"]), 
                                    TipoDocumento = dr["TipoDocumento"].ToString(), 
                                    Documento = dr["Documento"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    oTipoPersona = new TipoPersona() {
                                        IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]),
                                        Descripcion = dr["DescripcionTipoPersona"].ToString()
                                    }
                                }
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Vehiculo>();
                }
            }
            return Lista;
        }
        public List<Vehiculo> ListarVehiculos(int idpersona)
        {
            List<Vehiculo> Lista = new List<Vehiculo>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select v.IdVehiculo,v.Placa,v.Marca,v.Modelo,v.Color,tv.IdTipoVehiculo,tv.Descripcion[DescripcionTipoVehiculo],p.IdPersona,");
                    sb.AppendLine("p.TipoDocumento,p.Documento,p.Nombre,p.Apellido,p.Telefono,p.Correo,tp.IdTipoPersona,tp.Descripcion[DescripcionTipoPersona] from persona p");
                    sb.AppendLine("inner join VEHICULO v on p.IdPersona = v.IdPersona");
                    sb.AppendLine("inner join TIPO_VEHICULO tv on tv.IdTipoVehiculo = v.IdTipoVehiculo");
                    sb.AppendLine("inner join TIPO_PERSONA tp on tp.IdTipoPersona = p.IdTipoPersona WHERE P.IdPersona = " + idpersona + " AND v.Estado = 1 AND P.Estado = 1");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Vehiculo()
                            {
                                IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]),
                                Placa = dr["Placa"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Color = dr["Color"].ToString(),
                                oTipoVehiculo = new TipoVehiculo()
                                {
                                    IdTipoVehiculo = Convert.ToInt32(dr["IdTipoVehiculo"]),
                                    Descripcion = dr["DescripcionTipoVehiculo"].ToString()
                                },
                                oPersona = new Persona()
                                {
                                    IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                    TipoDocumento = dr["TipoDocumento"].ToString(),
                                    Documento = dr["Documento"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    oTipoPersona = new TipoPersona()
                                    {
                                        IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]),
                                        Descripcion = dr["DescripcionTipoPersona"].ToString()
                                    }
                                }
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Vehiculo>();
                }
            }
            return Lista;
        }
        public List<Servicio> ListarServicios()
        {
            List<Servicio> Lista = new List<Servicio>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select se.IdServicio,se.Nombre,se.Detalle,se.Precio,s.IdSucursal,");
                    sb.AppendLine("s.Descripcion[DescripcionSucursal],c.IdCategoria,c.Descripcion[DescripcionCategoria]");
                    sb.AppendLine("from SERVICIO se");
                    sb.AppendLine("inner join SUCURSAL s on s.IdSucursal = se.IdSucursal");
                    sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = se.IdCategoria");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
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
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                oSucursal = new Sucursal()
                                {
                                    IdSucursal = Convert.ToInt32(dr["IdSucursal"]),
                                    Descripcion = dr["DescripcionSucursal"].ToString()
                                },
                                oCategoria = new Categoria()
                                {
                                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                    Descripcion = dr["DescripcionCategoria"].ToString()
                                }
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
        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("update persona set Estado = 0, UsuarioModificacion = 1,FechaModificacion = GETDATE() where IdPersona = @id", oConexion);
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