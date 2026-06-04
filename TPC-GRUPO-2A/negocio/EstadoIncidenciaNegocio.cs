using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class EstadoIncidenciaNegocio
    {
        public List<EstadoIncidencia> ObtenerTodos()
        {
            List<EstadoIncidencia> lista = new List<EstadoIncidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre, activo FROM ESTADOS_INCIDENTE");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    EstadoIncidencia e = new EstadoIncidencia();
                    e.Id = (int)datos.Lector["id"];
                    e.Nombre = (string)datos.Lector["nombre"];
                    e.Activo = (bool)datos.Lector["activo"];
                    lista.Add(e);
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

        public EstadoIncidencia ObtenerPorNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre, activo FROM ESTADOS_INCIDENTE WHERE nombre = @nombre");
                datos.setearParametro("@nombre", nombre);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    EstadoIncidencia e = new EstadoIncidencia();
                    e.Id = (int)datos.Lector["id"];
                    e.Nombre = (string)datos.Lector["nombre"];
                    e.Activo = (bool)datos.Lector["activo"];
                    return e;
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
