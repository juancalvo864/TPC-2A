using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class TipoIncidenciaNegocio
    {
        public List<TipoIncidencia> ObtenerTodos()
        {
            List<TipoIncidencia> lista = new List<TipoIncidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre, descripcion, activo FROM TIPOS_INCIDENCIA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoIncidencia t = new TipoIncidencia();
                    t.Id = (int)datos.Lector["id"];
                    t.Nombre = (string)datos.Lector["nombre"];
                    t.Descripcion = datos.Lector["descripcion"] == DBNull.Value ? null : (string)datos.Lector["descripcion"];
                    t.Activo = (bool)datos.Lector["activo"];
                    lista.Add(t);
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

        public void Agregar(TipoIncidencia t)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO TIPOS_INCIDENCIA (nombre, descripcion, activo) " +
                                     "VALUES (@nombre, @descripcion, @activo)");

                datos.setearParametro("@nombre", t.Nombre);
                datos.setearParametro("@descripcion", (object)t.Descripcion ?? DBNull.Value);
                datos.setearParametro("@activo", t.Activo);

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

        public void Modificar(TipoIncidencia t)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE TIPOS_INCIDENCIA SET nombre = @nombre, descripcion = @descripcion, " +
                                     "activo = @activo WHERE id = @id");

                datos.setearParametro("@nombre", t.Nombre);
                datos.setearParametro("@descripcion", (object)t.Descripcion ?? DBNull.Value);
                datos.setearParametro("@activo", t.Activo);
                datos.setearParametro("@id", t.Id);

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
