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
