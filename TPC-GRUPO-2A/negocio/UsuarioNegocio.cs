using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.id, U.nombre, U.apellido, U.email, U.login, U.hash_password, " +
                                     "U.img_url, U.activo, U.fecha_creacion, R.id as rol_id, R.nombre as rol_nombre " +
                                     "FROM USUARIOS U INNER JOIN ROLES R ON U.rol_id = R.id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (int)datos.Lector["id"];
                    u.Nombre = (string)datos.Lector["nombre"];
                    u.Apellido = datos.Lector["apellido"] == DBNull.Value ? null : (string)datos.Lector["apellido"];
                    u.Email = (string)datos.Lector["email"];
                    u.Login = (string)datos.Lector["login"];
                    u.HashPassword = (string)datos.Lector["hash_password"];
                    u.Activo = (bool)datos.Lector["activo"];
                    u.FechaCreacion = (DateTime)datos.Lector["fecha_creacion"];
                    u.Rol = new Rol();
                    u.Rol.Id = (int)datos.Lector["rol_id"];
                    u.Rol.Nombre = (string)datos.Lector["rol_nombre"];
                    u.ImgUrl = datos.Lector["img_url"] == DBNull.Value ? null : (string)datos.Lector["img_url"];
                    lista.Add(u);
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

        public Usuario ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.id, U.nombre, U.apellido, U.email, U.login, U.hash_password, " +
                                     "U.img_url, U.activo, U.fecha_creacion, R.id as rol_id, R.nombre as rol_nombre " +
                                     "FROM USUARIOS U INNER JOIN ROLES R ON U.rol_id = R.id " +
                                     "WHERE U.id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (int)datos.Lector["id"];
                    u.Nombre = (string)datos.Lector["nombre"];
                    u.Apellido = datos.Lector["apellido"] == DBNull.Value ? null : (string)datos.Lector["apellido"];
                    u.Email = (string)datos.Lector["email"];
                    u.Login = (string)datos.Lector["login"];
                    u.HashPassword = (string)datos.Lector["hash_password"];
                    u.Activo = (bool)datos.Lector["activo"];
                    u.FechaCreacion = (DateTime)datos.Lector["fecha_creacion"];
                    u.Rol = new Rol();
                    u.Rol.Id = (int)datos.Lector["rol_id"];
                    u.Rol.Nombre = (string)datos.Lector["rol_nombre"];
                    u.ImgUrl = datos.Lector["img_url"] == DBNull.Value ? null : (string)datos.Lector["img_url"];
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

        public Usuario ObtenerPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.id, U.nombre, U.apellido, U.email, U.login, U.hash_password, " +
                                     "U.img_url, U.activo, U.fecha_creacion, R.id as rol_id, R.nombre as rol_nombre " +
                                     "FROM USUARIOS U INNER JOIN ROLES R ON U.rol_id = R.id " +
                                     "WHERE U.email = @email AND U.activo = 1");
                datos.setearParametro("@email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (int)datos.Lector["id"];
                    u.Nombre = (string)datos.Lector["nombre"];
                    u.Apellido = datos.Lector["apellido"] == DBNull.Value ? null : (string)datos.Lector["apellido"];
                    u.Email = (string)datos.Lector["email"];
                    u.Login = (string)datos.Lector["login"];
                    u.HashPassword = (string)datos.Lector["hash_password"];
                    u.Activo = (bool)datos.Lector["activo"];
                    u.FechaCreacion = (DateTime)datos.Lector["fecha_creacion"];
                    u.Rol = new Rol();
                    u.Rol.Id = (int)datos.Lector["rol_id"];
                    u.Rol.Nombre = (string)datos.Lector["rol_nombre"];
                    u.ImgUrl = datos.Lector["img_url"] == DBNull.Value ? null : (string)datos.Lector["img_url"];
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
        public void Agregar(Usuario u)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO USUARIOS (nombre, apellido, email, login, hash_password, rol_id, activo, fecha_creacion) " +
                                     "VALUES (@nombre, @apellido, @email, @login, @hashPassword, @rolId, @activo, @fechaCreacion)");

                datos.setearParametro("@nombre", u.Nombre);
                datos.setearParametro("@apellido", (object)u.Apellido ?? DBNull.Value);
                datos.setearParametro("@email", u.Email);
                datos.setearParametro("@login", u.Login);
                datos.setearParametro("@hashPassword", u.HashPassword);
                datos.setearParametro("@rolId", u.Rol.Id);
                datos.setearParametro("@activo", u.Activo);
                datos.setearParametro("@fechaCreacion", u.FechaCreacion);

                datos.ejecutarAccion();
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

        public void Modificar(Usuario u)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET nombre = @nombre, apellido = @apellido, email = @email, " +
                                     "hash_password = @hashPassword, img_url = @imgUrl, rol_id = @rolId, activo = @activo WHERE id = @id");

                datos.setearParametro("@nombre", u.Nombre);
                datos.setearParametro("@apellido", (object)u.Apellido ?? DBNull.Value);
                datos.setearParametro("@email", u.Email);
                datos.setearParametro("@hashPassword", u.HashPassword);
                datos.setearParametro("@rolId", u.Rol.Id);
                datos.setearParametro("@activo", u.Activo);
                datos.setearParametro("@id", u.Id);
                datos.setearParametro("@imgUrl", (object)u.ImgUrl ?? DBNull.Value);

                datos.ejecutarAccion();
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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM USUARIOS WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
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
