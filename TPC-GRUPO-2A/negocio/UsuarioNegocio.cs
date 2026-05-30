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
    public class UsuarioNegocio
    {

        public List<Usuario> listarUsuariosConSP()  
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //string consulta = @"SELECT id, nombre, email, telefono, identificacion, activo, fecha_alta FROM Usuarios";
                //datos.setearConsulta(consulta);

                datos.setearProcedimiento("SP_ListarUsuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.Id = (int)datos.Lector["id"];
                    usuario.Nombre = (string)datos.Lector["nombre"];
                    usuario.Email = datos.Lector["email"] == DBNull.Value ? "" : (string)datos.Lector["email"];
                    usuario.Login = (string)datos.Lector["login"];
                    usuario.HashPassword = (string)datos.Lector["hash_password"];
                    usuario.Activo = (bool)datos.Lector["activo"];
                    usuario.FechaCreacion = (DateTime)datos.Lector["fecha_creacion"];

                    usuario.Rol = new Rol();
                    usuario.Rol.Id = (int)datos.Lector["rol_id"];

                    lista.Add(usuario);
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


        /*
        public void Agregar(Cliente c)
        {
            AccesoDatos db = new AccesoDatos();

            try
            {
                db.setearConsulta("INSERT INTO CLIENTES (nombre, email, telefono, identificacion, activo, fecha_alta) " +
                                  "VALUES (@nombre, @email, @telefono, @identificacion, @activo, @fechaAlta)");

                db.setearParametro("@nombre", c.Nombre);
                db.setearParametro("@email", c.Email);
                db.setearParametro("@telefono", (object)c.Telefono ?? DBNull.Value);
                db.setearParametro("@identificacion", (object)c.Identificacion ?? DBNull.Value);
                db.setearParametro("@activo", c.Activo);
                db.setearParametro("@fechaAlta", c.FechaAlta);

                db.ejecutarAccion();
            }
            finally
            {
                db.cerrarConexion();
            }
        }*/

    }
}
