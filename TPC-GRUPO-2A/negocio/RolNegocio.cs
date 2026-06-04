using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class RolNegocio
    {
        public List<Rol> ObtenerTodos()
        {
            List<Rol> lista = new List<Rol>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id, nombre, activo FROM ROLES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Rol r = new Rol();
                    r.Id = (int)datos.Lector["id"];
                    r.Nombre = (string)datos.Lector["nombre"];
                    r.Activo = (bool)datos.Lector["activo"];
                    lista.Add(r);
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
    }
}
