using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class IncidenteNegocio
    {

        public List<Incidente> ObtenerTodos()
        {
            List<Incidente> incidentes = new List<Incidente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT I.id, I.nro_reclamo, I.descripcion_problematica, I.fecha_alta, I.fecha_ultima_actualizacion, " +
                    "I.cliente_id, C.nombre as cliente_nombre, " +
                    "I.tipo_incidencia_id, T.nombre as tipo_incidencia_nombre, " +
                    "I.prioridad_id, P.nombre as prioridad_nombre, " +
                    "I.estado_id, E.nombre as estado_nombre, " +
                    "I.usuario_creador_id, UC.nombre as creador_nombre, " +
                    "I.usuario_asignado_id, UA.nombre as asignado_nombre, " +
                    "I.dato_resolucion, I.fecha_resolucion, I.comentario_cierre, I.fecha_cierre " +
                    "FROM INCIDENTES I " +
                    "INNER JOIN CLIENTES C ON I.cliente_id = C.id " +
                    "INNER JOIN TIPOS_INCIDENCIA T ON I.tipo_incidencia_id = T.id " +
                    "INNER JOIN PRIORIDADES P ON I.prioridad_id = P.id " +
                    "INNER JOIN ESTADOS_INCIDENTE E ON I.estado_id = E.id " +
                    "INNER JOIN USUARIOS UC ON I.usuario_creador_id = UC.id " +
                    "INNER JOIN USUARIOS UA ON I.usuario_asignado_id = UA.id " +
                    "ORDER BY I.fecha_alta DESC");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Incidente i = new Incidente();
                    i.Id = (int)datos.Lector["id"];
                    i.NroReclamo = (string)datos.Lector["nro_reclamo"];
                    i.DescripcionProblematica = (string)datos.Lector["descripcion_problematica"];
                    i.FechaAlta = (DateTime)datos.Lector["fecha_alta"];
                    i.FechaUltimaActualizacion = (DateTime)datos.Lector["fecha_ultima_actualizacion"];
                    i.Cliente = new Cliente();
                    i.Cliente.Id = (int)datos.Lector["cliente_id"];
                    i.Cliente.Nombre = (string)datos.Lector["cliente_nombre"];
                    i.TipoIncidencia = new TipoIncidencia();
                    i.TipoIncidencia.Id = (int)datos.Lector["tipo_incidencia_id"];
                    i.TipoIncidencia.Nombre = (string)datos.Lector["tipo_incidencia_nombre"];
                    i.Prioridad = new Prioridad();
                    i.Prioridad.Id = (int)datos.Lector["prioridad_id"];
                    i.Prioridad.Nombre = (string)datos.Lector["prioridad_nombre"];
                    i.EstadoActual = new EstadoIncidencia();
                    i.EstadoActual.Id = (int)datos.Lector["estado_id"];
                    i.EstadoActual.Nombre = (string)datos.Lector["estado_nombre"];
                    i.UsuarioCreador = new Usuario();
                    i.UsuarioCreador.Id = (int)datos.Lector["usuario_creador_id"];
                    i.UsuarioCreador.Nombre = (string)datos.Lector["creador_nombre"];
                    i.UsuarioAsignado = new Usuario();
                    i.UsuarioAsignado.Id = (int)datos.Lector["usuario_asignado_id"];
                    i.UsuarioAsignado.Nombre = (string)datos.Lector["asignado_nombre"];
                    i.DatoResolucion = datos.Lector["dato_resolucion"] == DBNull.Value ? null : (string)datos.Lector["dato_resolucion"];
                    i.FechaResolucion = datos.Lector["fecha_resolucion"] == DBNull.Value ? (DateTime?)null : (DateTime)datos.Lector["fecha_resolucion"];
                    i.ComentarioCierre = datos.Lector["comentario_cierre"] == DBNull.Value ? null : (string)datos.Lector["comentario_cierre"];
                    i.FechaCierre = datos.Lector["fecha_cierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos.Lector["fecha_cierre"];

                    incidentes.Add(i);
                }

                return incidentes;
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

        public void Agregar(Incidente i)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO INCIDENTES (nro_reclamo, descripcion_problematica, fecha_alta, fecha_ultima_actualizacion, " +
                    "cliente_id, tipo_incidencia_id, prioridad_id, estado_id, usuario_creador_id, usuario_asignado_id) " +
                    "VALUES (@nroReclamo, @descripcion, @fechaAlta, @fechaActualizacion, " +
                    "@clienteId, @tipoId, @prioridadId, @estadoId, @creadorId, @asignadoId)");

                datos.setearParametro("@nroReclamo", i.NroReclamo);
                datos.setearParametro("@descripcion", i.DescripcionProblematica);
                datos.setearParametro("@fechaAlta", i.FechaAlta);
                datos.setearParametro("@fechaActualizacion", i.FechaUltimaActualizacion);
                datos.setearParametro("@clienteId", i.Cliente.Id);
                datos.setearParametro("@tipoId", i.TipoIncidencia.Id);
                datos.setearParametro("@prioridadId", i.Prioridad.Id);
                datos.setearParametro("@estadoId", i.EstadoActual.Id);
                datos.setearParametro("@creadorId", i.UsuarioCreador.Id);
                datos.setearParametro("@asignadoId", i.UsuarioAsignado.Id);

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

        public void Modificar(Incidente i)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE INCIDENTES SET descripcion_problematica = @descripcion, " +
                    "tipo_incidencia_id = @tipoId, prioridad_id = @prioridadId, " +
                    "estado_id = @estadoId, usuario_asignado_id = @asignadoId, " +
                    "fecha_ultima_actualizacion = @fechaActualizacion, " +
                    "dato_resolucion = @datoResolucion, fecha_resolucion = @fechaResolucion, " +
                    "usuario_resolucion_id = @usuarioResolucionId, " +
                    "comentario_cierre = @comentarioCierre, fecha_cierre = @fechaCierre, " +
                    "usuario_cierre_id = @usuarioCierreId " +
                    "WHERE id = @id");

                datos.setearParametro("@descripcion", i.DescripcionProblematica);
                datos.setearParametro("@tipoId", i.TipoIncidencia.Id);
                datos.setearParametro("@prioridadId", i.Prioridad.Id);
                datos.setearParametro("@estadoId", i.EstadoActual.Id);
                datos.setearParametro("@asignadoId", i.UsuarioAsignado.Id);
                datos.setearParametro("@fechaActualizacion", DateTime.Now);
                datos.setearParametro("@datoResolucion", (object)i.DatoResolucion ?? DBNull.Value);
                datos.setearParametro("@fechaResolucion", (object)i.FechaResolucion ?? DBNull.Value);
                datos.setearParametro("@usuarioResolucionId", i.UsuarioResolucion != null ? (object)i.UsuarioResolucion.Id : DBNull.Value);
                datos.setearParametro("@comentarioCierre", (object)i.ComentarioCierre ?? DBNull.Value);
                datos.setearParametro("@fechaCierre", (object)i.FechaCierre ?? DBNull.Value);
                datos.setearParametro("@usuarioCierreId", i.UsuarioCierre != null ? (object)i.UsuarioCierre.Id : DBNull.Value);
                datos.setearParametro("@id", i.Id);

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

        public void CambiarEstado(int incidenteId, int estadoAnteriorId, int estadoNuevoId, int? usuarioId, string motivo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO HISTORIAL_ESTADOS (incidente_id, estado_anterior_id, estado_nuevo_id, usuario_id, fecha_cambio, motivo_nota) " +
                    "VALUES (@incidenteId, @estadoAnteriorId, @estadoNuevoId, @usuarioId, @fechaCambio, @motivo)");

                datos.setearParametro("@incidenteId", incidenteId);
                datos.setearParametro("@estadoAnteriorId", estadoAnteriorId);
                datos.setearParametro("@estadoNuevoId", estadoNuevoId);
                datos.setearParametro("@usuarioId", usuarioId.HasValue ? (object)usuarioId.Value : DBNull.Value);
                datos.setearParametro("@fechaCambio", DateTime.Now);
                datos.setearParametro("@motivo", (object)motivo ?? DBNull.Value);

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
