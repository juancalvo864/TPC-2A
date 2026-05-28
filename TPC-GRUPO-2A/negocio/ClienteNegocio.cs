using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> clientes = new List<Cliente>();
            AccesoDatos db = new AccesoDatos();

            try
            {
                db.setearConsulta("SELECT id, nombre, email, telefono, identificacion, activo, fecha_alta FROM CLIENTES");
                db.ejecutarLectura();

                while (db.Lector.Read())
                {
                    Cliente c = new Cliente();
                    c.Id = db.Lector.GetInt32(0);
                    c.Nombre = db.Lector.GetString(1);
                    c.Email = db.Lector.GetString(2);
                    c.Telefono = db.Lector.IsDBNull(3) ? null : db.Lector.GetString(3);
                    c.Identificacion = db.Lector.IsDBNull(4) ? null : db.Lector.GetString(4);
                    c.Activo = db.Lector.GetBoolean(5);
                    c.FechaAlta = db.Lector.GetDateTime(6);
                    clientes.Add(c);
                }
            }
            finally
            {
                db.cerrarConexion();
            }

            return clientes;
        }

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
        }
    }
}
