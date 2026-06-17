using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> clientes = new List<Cliente>();
            AccesoDatos db = new AccesoDatos();

            try
            {
                db.setearConsulta("SELECT id, nombre, apellido, email, telefono, identificacion, activo, fecha_alta FROM CLIENTES");
                db.ejecutarLectura();

                while (db.Lector.Read())
                {
                    Cliente c = new Cliente();
                    c.Id = db.Lector.GetInt32(0);
                    c.Nombre = db.Lector.GetString(1);
                    c.Apellido = db.Lector.IsDBNull(2) ? null : db.Lector.GetString(2); c.Email = db.Lector.GetString(3);
                    c.Telefono = db.Lector.IsDBNull(4) ? null : db.Lector.GetString(4);
                    c.Identificacion = db.Lector.IsDBNull(5) ? null : db.Lector.GetString(5);
                    c.Activo = db.Lector.GetBoolean(6);
                    c.FechaAlta = db.Lector.GetDateTime(7);
                    clientes.Add(c);
                }
            return clientes;
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.cerrarConexion();
            }

        }


        public List<Cliente> listarClientesConSP()  
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //string consulta = @"SELECT id, nombre, email, telefono, identificacion, activo, fecha_alta FROM Clientes";
                //datos.setearConsulta(consulta);

                datos.setearProcedimiento("SP_ListarClientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.Id = (int)datos.Lector["id"];
                    cliente.Nombre = (string)datos.Lector["nombre"];
                    cliente.Email = (string)datos.Lector["email"];
                    cliente.Telefono = datos.Lector["telefono"] == DBNull.Value ? null : (string)datos.Lector["telefono"];
                    cliente.Identificacion = datos.Lector["identificacion"] == DBNull.Value ? null : (string)datos.Lector["identificacion"];
                    cliente.Activo = (bool)datos.Lector["activo"];
                    cliente.FechaAlta = (DateTime)datos.Lector["fecha_alta"];

                    lista.Add(cliente);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Cliente ObtenerPorId(int id)
        {
            AccesoDatos db = new AccesoDatos();

            try
            {
                db.setearConsulta("SELECT id, nombre, apellido, email, telefono, identificacion, activo, fecha_alta FROM CLIENTES WHERE id = @id");
                db.setearParametro("@id", id);
                db.ejecutarLectura();

                if (db.Lector.Read())
                {
                    Cliente c = new Cliente();
                    c.Id = (int)db.Lector["id"];
                    c.Nombre = (string)db.Lector["nombre"];
                    c.Apellido = (string)db.Lector["apellido"];
                    c.Email = (string)db.Lector["email"];
                    c.Telefono = db.Lector["telefono"] == DBNull.Value ? null : (string)db.Lector["telefono"];
                    c.Identificacion = db.Lector["identificacion"] == DBNull.Value ? null : (string)db.Lector["identificacion"];
                    c.Activo = (bool)db.Lector["activo"];
                    c.FechaAlta = (DateTime)db.Lector["fecha_alta"];
                    return c;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.cerrarConexion();
            }
        }

        public void Agregar(Cliente c)
        {
            AccesoDatos db = new AccesoDatos();

            try
            {
                db.setearConsulta("INSERT INTO CLIENTES (nombre, apellido, email, telefono, identificacion, activo, fecha_alta) " +
                                  "VALUES (@nombre, @apellido, @email, @telefono, @identificacion, @activo, @fechaAlta)");

                db.setearParametro("@nombre", c.Nombre);
                db.setearParametro("@apellido", c.Apellido);
                db.setearParametro("@email", c.Email);
                db.setearParametro("@telefono", (object)c.Telefono ?? DBNull.Value);
                db.setearParametro("@identificacion", (object)c.Identificacion ?? DBNull.Value);
                db.setearParametro("@activo", c.Activo);
                db.setearParametro("@fechaAlta", c.FechaAlta);

                db.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.cerrarConexion();
            }
        }

        public void Modificar(Cliente c)
        {
            AccesoDatos db = new AccesoDatos();

            try
            {
                db.setearConsulta("UPDATE CLIENTES SET nombre = @nombre, apellido = @apellido, email = @email, " +
                                  "telefono = @telefono, identificacion = @identificacion, " +
                                  "activo = @activo WHERE id = @id");

                db.setearParametro("@nombre", c.Nombre);
                db.setearParametro("@apellido", c.Apellido);
                db.setearParametro("@email", c.Email);
                db.setearParametro("@telefono", (object)c.Telefono ?? DBNull.Value);
                db.setearParametro("@identificacion", (object)c.Identificacion ?? DBNull.Value);
                db.setearParametro("@activo", c.Activo);
                db.setearParametro("@id", c.Id);

                db.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.cerrarConexion();
            }
        }
    }
}
