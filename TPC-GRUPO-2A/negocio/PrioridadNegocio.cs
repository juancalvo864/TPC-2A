using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PrioridadNegocio
    {
        public List<Prioridad> ObtenerTodos()
        {
            List<Prioridad> lista = new List<Prioridad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre, nivel, activo FROM PRIORIDADES ORDER BY nivel ASC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Prioridad p = new Prioridad();
                    p.Id = (int)datos.Lector["id"];
                    p.Nombre = (string)datos.Lector["nombre"];
                    p.Nivel = (int)datos.Lector["nivel"];
                    p.Activo = (bool)datos.Lector["activo"];
                    lista.Add(p);
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

        public void Agregar(Prioridad p)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO PRIORIDADES (nombre, nivel, activo) " +
                                     "VALUES (@nombre, @nivel, @activo)");

                datos.setearParametro("@nombre", p.Nombre);
                datos.setearParametro("@nivel", p.Nivel);
                datos.setearParametro("@activo", p.Activo);

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

        public void Modificar(Prioridad p)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PRIORIDADES SET nombre = @nombre, nivel = @nivel, " +
                                     "activo = @activo WHERE id = @id");

                datos.setearParametro("@nombre", p.Nombre);
                datos.setearParametro("@nivel", p.Nivel);
                datos.setearParametro("@activo", p.Activo);
                datos.setearParametro("@id", p.Id);

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
