using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;  

namespace negocio
{
    public class CategoriaNegocio
    {

        public List<Categoria> Listar()
        {
			List<Categoria> listaCategoria = new List<Categoria>();	
			AccesoDatos datos = new AccesoDatos();	

            try
			{
				datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
                    Categoria categoria = new Categoria();
					categoria.Id = (int)datos.Lector["Id"];	
					categoria.Descripcion = (string)datos.Lector["Descripcion"];	

					listaCategoria.Add(categoria);	
                }

                    return listaCategoria; 
			}
            catch (Exception ex)
            {
                throw new Exception("Error de lectura en la DB" + ex.Message);
            }
            finally
			{
				datos.cerrarConexion();	
            }
        }

        public void agregar(Categoria cat)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into CATEGORIAS(Descripcion) values(@Descripcion)");
                datos.setearParametro("@Descripcion", cat.Descripcion);
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

        public void editar(Categoria editado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE CATEGORIAS SET Descripcion = @Descripcion WHERE Id = @IdCategoria;");
                datos.setearParametro("@Descripcion", editado.Descripcion);
                datos.setearParametro("@IdCategoria", editado.Id);
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
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete FROM CATEGORIAS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ExisteCategoria(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM CATEGORIAS WHERE Descripcion = @Descripcion");
                datos.setearParametro("@Descripcion", descripcion);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector[0] > 0;
                }
                return false;
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

        public List<Categoria> Filtrar(string campo, string criterio, string filtro)
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, Descripcion FROM CATEGORIAS WHERE ";
                //if (campo == "Numerico")
                //{
                //    switch (criterio)
                //    {
                //        case "Mayor a":
                //            consulta += "A.Precio > " + filtro;
                //            break;
                //        case "Menor a":
                //            consulta += "A.Precio < " + filtro;
                //            break;
                //        default:
                //            consulta += "A.Precio = " + filtro;
                //            break;
                //    }
                //}
                if (campo == "Descripcion")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                //else
                //{
                //    switch (criterio)
                //    {
                //        case "Comienza con":
                //            consulta += "A.Nombre like '" + filtro + "%' ";
                //            break;
                //        case "Termina con":
                //            consulta += "A.Nombre like '%" + filtro + "'";
                //            break;
                //        default:
                //            consulta += "A.Nombre like '%" + filtro + "%'";
                //            break;
                //    }
                //}

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)datos.Lector["Id"];
                    categoria.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(categoria);
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
