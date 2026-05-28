using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;  

namespace negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> Listar()
        {
			List<Articulo> listaArticulos = new List<Articulo>();	
			AccesoDatos datos = new AccesoDatos();	

            try
			{
				datos.setearConsulta("select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Descripcion as Marca, C.Descripcion as Categoria, A.IdMarca, A.IdCategoria from ARTICULOS A inner join MARCAS M on A.IdMarca = M.Id inner join CATEGORIAS C on A.IdCategoria = C.Id");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
                    Articulo articulo = new Articulo();
					articulo.Id = (int)datos.Lector["Id"];	
					articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];   
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];
                    articulo.Imagen = new Imagen();
                    listaArticulos.Add(articulo);	
                }

                    return listaArticulos; 
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

      
        public int Agregar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                                     "values(@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio); " +
                                     "select cast(SCOPE_IDENTITY() as int)");
                
                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@IdMarca", art.Marca.Id);
                datos.setearParametro("@IdCategoria", art.Categoria.Id);
                datos.setearParametro("@Precio", art.Precio);

                return (int)datos.ejecutarEscalar();
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


        public void Modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Articulos SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio WHERE Id = @Id");

                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@IdMarca", art.Marca.Id);
                datos.setearParametro("@IdCategoria", art.Categoria.Id);
                datos.setearParametro("@Precio", art.Precio);
                datos.setearParametro("@Id", art.Id);

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
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @Id; DELETE FROM ARTICULOS WHERE Id = @Id;");

                datos.setearParametro("@Id", id);

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
        public void Editar(Articulo editado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio FROM ARTICULOS A INNER JOIN IMAGENES I ON A.Id = I.IdArticulo WHERE A.Id = @IdArticulo; UPDATE IMAGENES SET ImagenUrl = @NuevaImagenUrl WHERE IdArticulo = @IdArticulo;");
                datos.setearParametro("@Codigo", editado.Codigo);
                datos.setearParametro("@Nombre", editado.Nombre);
                datos.setearParametro("@Descripcion", editado.Descripcion);
                datos.setearParametro("@IdMarca", editado.Marca.Id);
                datos.setearParametro("@IdCategoria", editado.Categoria.Id);
                datos.setearParametro("@Precio", editado.Precio);
                datos.setearParametro("@NuevaImagenUrl", editado.Imagen.ImagenUrl);
                datos.setearParametro("@IdArticulo", editado.Id);

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

        public bool ExisteCodigo(string codigo, int idArticulo = 0)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Articulos WHERE Codigo = @Codigo AND Id <> @Id");
                datos.setearParametro("@Codigo", codigo);
                datos.setearParametro("@Id", idArticulo);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]) > 0;
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

        public List<Articulo> Buscar(string texto, int? idMarca, int? idCategoria)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio,
                               M.Descripcion AS Marca, A.IdMarca,
                               C.Descripcion AS Categoria, A.IdCategoria
                               FROM ARTICULOS A
                               INNER JOIN MARCAS M ON A.IdMarca = M.Id
                               INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id
                               WHERE (@Texto IS NULL OR A.Codigo LIKE @Texto OR A.Nombre LIKE @Texto)
                               AND (@IdMarca IS NULL OR A.IdMarca = @IdMarca)
                               AND (@IdCategoria IS NULL OR A.IdCategoria = @IdCategoria)");

                datos.setearParametro("@Texto", string.IsNullOrWhiteSpace(texto) ? (object)DBNull.Value : "%" + texto.Trim() + "%");
                datos.setearParametro("@IdMarca", idMarca.HasValue ? (object)idMarca.Value : DBNull.Value);
                datos.setearParametro("@IdCategoria", idCategoria.HasValue ? (object)idCategoria.Value : DBNull.Value);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    articulo.Imagen = new Imagen();

                    lista.Add(articulo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Descripcion as Marca, C.Descripcion as Categoria, A.IdMarca, A.IdCategoria from ARTICULOS A inner join MARCAS M on A.IdMarca = M.Id inner join CATEGORIAS C on A.IdCategoria = C.Id and ";
                if(campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Precio > "+  filtro;
                            break;
                        case "Menor a":
                            consulta += "A.Precio < " + filtro;
                            break;
                        default :
                            consulta += "A.Precio = " + filtro;
                            break;  
                    }
                }
                else if(campo == "Código")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Codigo like '" + filtro + "%' " ;
                            break;
                        case "Termina con":
                            consulta += "Codigo like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Codigo like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch(criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Nombre like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "A.Nombre like '%" + filtro + "%'";
                            break;
                        default:
                            consulta += "A.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];
                    articulo.Imagen = new Imagen();
                    lista.Add(articulo);
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
