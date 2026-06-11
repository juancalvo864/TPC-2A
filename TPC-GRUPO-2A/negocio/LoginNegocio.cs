using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class LoginNegocio
    {
        public Usuario ValidarUsuario(string login, string password)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.id, U.nombre, U.apellido, U.email, U.login, U.hash_password, " +
                                     "U.activo, U.fecha_creacion, R.id as rol_id, R.nombre as rol_nombre " +
                                     "FROM USUARIOS U INNER JOIN ROLES R ON U.rol_id = R.id " +
                                     "WHERE U.login = @login AND U.hash_password = @password AND U.activo = 1");

                datos.setearParametro("@login", login);
                datos.setearParametro("@password", password);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (int)datos.Lector["id"];
                    u.Nombre = (string)datos.Lector["nombre"];
                    u.Apellido = (string)datos.Lector["apellido"];
                    u.Email = (string)datos.Lector["email"];
                    u.Login = (string)datos.Lector["login"];
                    u.HashPassword = (string)datos.Lector["hash_password"];
                    u.Activo = (bool)datos.Lector["activo"];
                    u.FechaCreacion = (DateTime)datos.Lector["fecha_creacion"];
                    u.Rol = new Rol();
                    u.Rol.Id = (int)datos.Lector["rol_id"];
                    u.Rol.Nombre = (string)datos.Lector["rol_nombre"];
                    return u;
                }

                return null;
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
    }
}
