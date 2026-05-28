using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;  

namespace negocio
{
    public class MarcaNegocio
    {

        public List<Marca> Listar()
        {
			List<Marca> listaMarca = new List<Marca>();	
			AccesoDatos datos = new AccesoDatos();	

            try
			{
				datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Marca marca = new Marca();
					marca.Id = (int)datos.Lector["Id"];	
					marca.Descripcion = (string)datos.Lector["Descripcion"];	

					listaMarca.Add(marca);	
                }

                    return listaMarca; 
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


        public void Agregar(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into MARCAS(Descripcion) values(@Descripcion)");
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
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
        public void editar(Marca editado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @Descripcion WHERE Id = @IdMarca;");
                datos.setearParametro("@Descripcion", editado.Descripcion);
                datos.setearParametro("@IdMarca", editado.Id);

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
                datos.setearConsulta("delete FROM MARCAS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ExisteMarca(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM MARCAS WHERE Descripcion = @Descripcion");
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

        public List<Marca> Filtrar(string campo, string criterio, string filtro)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Id, Descripcion FROM MARCAS WHERE ";
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
                    Marca marca = new Marca();
                    marca.Id = (int)datos.Lector["Id"];
                    marca.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(marca);
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
