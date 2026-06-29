using dominio;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    "I.cliente_id, C.nombre as cliente_nombre, C.apellido as cliente_apellido, C.email as cliente_email, C.activo as cliente_activo, " +
                    "I.tipo_incidencia_id, T.nombre as tipo_incidencia_nombre, " +
                    "I.prioridad_id, P.nombre as prioridad_nombre, P.nivel as prioridad_nivel, " +
                    "I.estado_id, E.nombre as estado_nombre, " +
                    "I.usuario_creador_id, UC.nombre as creador_nombre, UC.apellido as creador_apellido, UC.email as creador_email, " +
                    "I.usuario_asignado_id, UA.nombre as asignado_nombre, UA.apellido as asignado_apellido, UA.email as asignado_email, " +
                    "I.dato_resolucion, I.fecha_resolucion, I.usuario_resolucion_id, " +
                    "UR.nombre as resolucion_nombre, UR.apellido as resolucion_apellido, " +
                    "I.comentario_cierre, I.fecha_cierre, I.usuario_cierre_id, " +
                    "UCI.nombre as cierre_nombre, UCI.apellido as cierre_apellido " +
                    "FROM INCIDENTES I " +
                    "INNER JOIN CLIENTES C ON I.cliente_id = C.id " +
                    "INNER JOIN TIPOS_INCIDENCIA T ON I.tipo_incidencia_id = T.id " +
                    "INNER JOIN PRIORIDADES P ON I.prioridad_id = P.id " +
                    "INNER JOIN ESTADOS_INCIDENTE E ON I.estado_id = E.id " +
                    "INNER JOIN USUARIOS UC ON I.usuario_creador_id = UC.id " +
                    "INNER JOIN USUARIOS UA ON I.usuario_asignado_id = UA.id " +
                    "LEFT JOIN USUARIOS UR ON I.usuario_resolucion_id = UR.id " +
                    "LEFT JOIN USUARIOS UCI ON I.usuario_cierre_id = UCI.id " +
                    "ORDER BY I.fecha_alta DESC");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                    incidentes.Add(MapearIncidente(datos));

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

        public List<Incidente> ObtenerVisiblesParaUsuario(Usuario usuario)
        {
            ValidarUsuario(usuario);

            List<Incidente> todos = ObtenerTodos();
            if (EsAdministradorOSupervisor(usuario))
                return todos;

            return todos.Where(x => x.UsuarioAsignado != null && x.UsuarioAsignado.Id == usuario.Id).ToList();
        }

        public Incidente ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ReglaNegocioException("La incidencia solicitada no es valida.");

            Incidente incidente = ObtenerTodos().FirstOrDefault(x => x.Id == id);
            if (incidente == null)
                throw new ReglaNegocioException("No se encontro la incidencia indicada.");

            incidente.Comentarios = ObtenerComentarios(incidente.Id);
            return incidente;
        }

        public int CrearIncidencia(Incidente incidente, Usuario actor)
        {
            ValidarUsuario(actor);
            ValidarDatosAlta(incidente);

            Cliente cliente = new ClienteNegocio().ObtenerPorId(incidente.Cliente.Id);
            if (cliente == null || !cliente.Activo)
                throw new ReglaNegocioException("El cliente debe existir y estar activo para registrar incidencias.");

            EstadoIncidencia estadoAbierto = ObtenerEstadoPorNombre(EstadoIncidencia.Nombres.Abierto);
            DateTime ahora = DateTime.Now;

            incidente.NroReclamo = GenerarNumeroReclamo();
            incidente.FechaAlta = ahora;
            incidente.FechaUltimaActualizacion = ahora;
            incidente.EstadoActual = estadoAbierto;
            incidente.UsuarioCreador = actor;
            incidente.UsuarioAsignado = actor;

            int idIncidente = InsertarIncidente(incidente);
            RegistrarCambioEstado(idIncidente, estadoAbierto, estadoAbierto, actor.Id, "Alta de incidencia");

            incidente.Id = idIncidente;
            incidente.Cliente = cliente;

            EnviarCorreoAlta(incidente);
            return idIncidente;
        }

        public void ActualizarIncidencia(Incidente incidenteActualizado, Usuario actor, string motivo)
        {
            ValidarUsuario(actor);
            ValidarDatosAlta(incidenteActualizado);

            Incidente actual = ObtenerPorId(incidenteActualizado.Id);
            ValidarAccesoIncidencia(actual, actor);
            ValidarNoCerrado(actual);

            actual.DescripcionProblematica = incidenteActualizado.DescripcionProblematica;
            actual.TipoIncidencia = incidenteActualizado.TipoIncidencia;
            actual.Prioridad = incidenteActualizado.Prioridad;
            actual.FechaUltimaActualizacion = DateTime.Now;

            EstadoIncidencia nuevoEstado = DeterminarEstadoPorModificacion(actual.EstadoActual);
            AplicarCambioEstado(actual, nuevoEstado, actor, TextoMotivo(motivo, "Modificacion de incidencia"));
            GuardarCambiosIncidente(actual);
        }

        public void ReasignarIncidencia(int incidenteId, int nuevoUsuarioAsignadoId, Usuario actor, string motivo)
        {
            ValidarUsuario(actor);
            if (!PuedeReasignar(actor))
                throw new ReglaNegocioException("Solo el supervisor o el administrador pueden reasignar incidencias.");
            if (nuevoUsuarioAsignadoId <= 0)
                throw new ReglaNegocioException("Debe indicar un usuario destino valido.");

            Incidente incidente = ObtenerPorId(incidenteId);
            ValidarNoCerrado(incidente);

            if (incidente.UsuarioAsignado != null && incidente.UsuarioAsignado.Id == nuevoUsuarioAsignadoId)
                return;

            Usuario nuevoAsignado = new UsuarioNegocio().ObtenerPorId(nuevoUsuarioAsignadoId);
            if (nuevoAsignado == null || !nuevoAsignado.Activo)
                throw new ReglaNegocioException("No se puede asignar la incidencia a un usuario inexistente o inactivo.");

            incidente.UsuarioAsignado = nuevoAsignado;
            incidente.FechaUltimaActualizacion = DateTime.Now;

            EstadoIncidencia estadoAsignado = ObtenerEstadoPorNombre(EstadoIncidencia.Nombres.Asignado);
            AplicarCambioEstado(incidente, estadoAsignado, actor, TextoMotivo(motivo, "Reasignacion de incidencia"));
            GuardarCambiosIncidente(incidente);
        }

        public List<ComentarioIncidencia> ObtenerComentarios(int incidenteId)
        {
            List<ComentarioIncidencia> comentarios = new List<ComentarioIncidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT C.id, C.incidente_id, C.comentario, C.fecha_comentario, " +
                    "U.id as usuario_id, U.nombre, U.apellido " +
                    "FROM COMENTARIOS_INCIDENTE C " +
                    "INNER JOIN USUARIOS U ON C.usuario_id = U.id " +
                    "WHERE C.incidente_id = @incidenteId " +
                    "ORDER BY C.fecha_comentario DESC");

                datos.setearParametro("@incidenteId", incidenteId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    comentarios.Add(new ComentarioIncidencia
                    {
                        Id = (int)datos.Lector["id"],
                        IncidenteId = (int)datos.Lector["incidente_id"],
                        Comentario = (string)datos.Lector["comentario"],
                        FechaComentario = (DateTime)datos.Lector["fecha_comentario"],
                        Usuario = new Usuario
                        {
                            Id = (int)datos.Lector["usuario_id"],
                            Nombre = (string)datos.Lector["nombre"],
                            Apellido = datos.Lector["apellido"] == DBNull.Value ? null : (string)datos.Lector["apellido"]
                        }
                    });
                }

                return comentarios;
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

        public void AgregarComentario(int incidenteId, string comentario, Usuario actor)
        {
            ValidarUsuario(actor);
            if (string.IsNullOrWhiteSpace(comentario))
                throw new ReglaNegocioException("Debes ingresar un comentario antes de guardarlo.");

            Incidente incidente = ObtenerPorId(incidenteId);
            ValidarAccesoIncidencia(incidente, actor);
            ValidarNoCerrado(incidente);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO COMENTARIOS_INCIDENTE (incidente_id, usuario_id, comentario, fecha_comentario) " +
                    "VALUES (@incidenteId, @usuarioId, @comentario, @fechaComentario)");

                datos.setearParametro("@incidenteId", incidenteId);
                datos.setearParametro("@usuarioId", actor.Id);
                datos.setearParametro("@comentario", comentario.Trim());
                datos.setearParametro("@fechaComentario", DateTime.Now);
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

        public void ResolverIncidencia(int incidenteId, string datoResolucion, Usuario actor)
        {
            ValidarUsuario(actor);
            if (string.IsNullOrWhiteSpace(datoResolucion))
                throw new ReglaNegocioException("Para resolver la incidencia debe indicar el dato final de resolucion.");

            Incidente incidente = ObtenerPorId(incidenteId);
            ValidarAccesoIncidencia(incidente, actor);
            ValidarNoCerrado(incidente);

            incidente.DatoResolucion = datoResolucion.Trim();
            incidente.FechaResolucion = DateTime.Now;
            incidente.UsuarioResolucion = actor;
            incidente.FechaUltimaActualizacion = incidente.FechaResolucion.Value;

            EstadoIncidencia estadoResuelto = ObtenerEstadoPorNombre(EstadoIncidencia.Nombres.Resuelto);
            AplicarCambioEstado(incidente, estadoResuelto, actor, "Resolucion de incidencia");
            GuardarCambiosIncidente(incidente);
            EnviarCorreoResolucion(incidente);
        }

        public void CerrarIncidencia(int incidenteId, string comentarioCierre, Usuario actor)
        {
            ValidarUsuario(actor);
            if (string.IsNullOrWhiteSpace(comentarioCierre))
                throw new ReglaNegocioException("Para cerrar la incidencia debe registrar un comentario final.");

            Incidente incidente = ObtenerPorId(incidenteId);
            ValidarAccesoIncidencia(incidente, actor);
            if (EsEstado(incidente.EstadoActual, EstadoIncidencia.Nombres.Cerrado))
                throw new ReglaNegocioException("La incidencia ya se encuentra cerrada.");

            incidente.ComentarioCierre = comentarioCierre.Trim();
            incidente.FechaCierre = DateTime.Now;
            incidente.UsuarioCierre = actor;
            incidente.FechaUltimaActualizacion = incidente.FechaCierre.Value;

            EstadoIncidencia estadoCerrado = ObtenerEstadoPorNombre(EstadoIncidencia.Nombres.Cerrado);
            AplicarCambioEstado(incidente, estadoCerrado, actor, "Cierre de incidencia");
            GuardarCambiosIncidente(incidente);
            EnviarCorreoCierre(incidente);
        }

        public bool PuedeReasignar(Usuario usuario)
        {
            return usuario != null && EsAdministradorOSupervisor(usuario);
        }

        public bool PuedeVer(Incidente incidente, Usuario usuario)
        {
            if (incidente == null)
                return false;

            ValidarUsuario(usuario);
            return EsAdministradorOSupervisor(usuario) ||
                   (incidente.UsuarioAsignado != null && incidente.UsuarioAsignado.Id == usuario.Id);
        }

        private int InsertarIncidente(Incidente incidente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO INCIDENTES (nro_reclamo, descripcion_problematica, fecha_alta, fecha_ultima_actualizacion, " +
                    "cliente_id, tipo_incidencia_id, prioridad_id, estado_id, usuario_creador_id, usuario_asignado_id) " +
                    "VALUES (@nroReclamo, @descripcion, @fechaAlta, @fechaActualizacion, " +
                    "@clienteId, @tipoId, @prioridadId, @estadoId, @creadorId, @asignadoId); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);");

                datos.setearParametro("@nroReclamo", incidente.NroReclamo);
                datos.setearParametro("@descripcion", incidente.DescripcionProblematica);
                datos.setearParametro("@fechaAlta", incidente.FechaAlta);
                datos.setearParametro("@fechaActualizacion", incidente.FechaUltimaActualizacion);
                datos.setearParametro("@clienteId", incidente.Cliente.Id);
                datos.setearParametro("@tipoId", incidente.TipoIncidencia.Id);
                datos.setearParametro("@prioridadId", incidente.Prioridad.Id);
                datos.setearParametro("@estadoId", incidente.EstadoActual.Id);
                datos.setearParametro("@creadorId", incidente.UsuarioCreador.Id);
                datos.setearParametro("@asignadoId", incidente.UsuarioAsignado.Id);

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

        private void GuardarCambiosIncidente(Incidente incidente)
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

                datos.setearParametro("@descripcion", incidente.DescripcionProblematica);
                datos.setearParametro("@tipoId", incidente.TipoIncidencia.Id);
                datos.setearParametro("@prioridadId", incidente.Prioridad.Id);
                datos.setearParametro("@estadoId", incidente.EstadoActual.Id);
                datos.setearParametro("@asignadoId", incidente.UsuarioAsignado.Id);
                datos.setearParametro("@fechaActualizacion", incidente.FechaUltimaActualizacion);
                datos.setearParametro("@datoResolucion", ValorDb(incidente.DatoResolucion));
                datos.setearParametro("@fechaResolucion", ValorDb(incidente.FechaResolucion));
                datos.setearParametro("@usuarioResolucionId", incidente.UsuarioResolucion != null ? (object)incidente.UsuarioResolucion.Id : DBNull.Value);
                datos.setearParametro("@comentarioCierre", ValorDb(incidente.ComentarioCierre));
                datos.setearParametro("@fechaCierre", ValorDb(incidente.FechaCierre));
                datos.setearParametro("@usuarioCierreId", incidente.UsuarioCierre != null ? (object)incidente.UsuarioCierre.Id : DBNull.Value);
                datos.setearParametro("@id", incidente.Id);

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

        private void RegistrarCambioEstado(int incidenteId, EstadoIncidencia estadoAnterior, EstadoIncidencia estadoNuevo, int? usuarioId, string motivo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO HISTORIAL_ESTADOS (incidente_id, estado_anterior_id, estado_nuevo_id, usuario_id, fecha_cambio, motivo_nota) " +
                    "VALUES (@incidenteId, @estadoAnteriorId, @estadoNuevoId, @usuarioId, @fechaCambio, @motivo)");

                datos.setearParametro("@incidenteId", incidenteId);
                datos.setearParametro("@estadoAnteriorId", estadoAnterior.Id);
                datos.setearParametro("@estadoNuevoId", estadoNuevo.Id);
                datos.setearParametro("@usuarioId", usuarioId.HasValue ? (object)usuarioId.Value : DBNull.Value);
                datos.setearParametro("@fechaCambio", DateTime.Now);
                datos.setearParametro("@motivo", ValorDb(motivo));

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

        private void AplicarCambioEstado(Incidente incidente, EstadoIncidencia nuevoEstado, Usuario actor, string motivo)
        {
            EstadoIncidencia estadoAnterior = incidente.EstadoActual;
            incidente.EstadoActual = nuevoEstado;
            RegistrarCambioEstado(incidente.Id, estadoAnterior, nuevoEstado, actor.Id, motivo);
        }

        private Incidente MapearIncidente(AccesoDatos datos)
        {
            Incidente incidente = new Incidente();

            incidente.Id = (int)datos.Lector["id"];
            incidente.NroReclamo = (string)datos.Lector["nro_reclamo"];
            incidente.DescripcionProblematica = (string)datos.Lector["descripcion_problematica"];
            incidente.FechaAlta = (DateTime)datos.Lector["fecha_alta"];
            incidente.FechaUltimaActualizacion = (DateTime)datos.Lector["fecha_ultima_actualizacion"];
            incidente.Cliente = new Cliente
            {
                Id = (int)datos.Lector["cliente_id"],
                Nombre = (string)datos.Lector["cliente_nombre"],
                Apellido = datos.Lector["cliente_apellido"] == DBNull.Value ? null : (string)datos.Lector["cliente_apellido"],
                Email = datos.Lector["cliente_email"] == DBNull.Value ? null : (string)datos.Lector["cliente_email"],
                Activo = (bool)datos.Lector["cliente_activo"]
            };
            incidente.TipoIncidencia = new TipoIncidencia
            {
                Id = (int)datos.Lector["tipo_incidencia_id"],
                Nombre = (string)datos.Lector["tipo_incidencia_nombre"]
            };
            incidente.Prioridad = new Prioridad
            {
                Id = (int)datos.Lector["prioridad_id"],
                Nombre = (string)datos.Lector["prioridad_nombre"],
                Nivel = (int)datos.Lector["prioridad_nivel"]
            };
            incidente.EstadoActual = new EstadoIncidencia
            {
                Id = (int)datos.Lector["estado_id"],
                Nombre = (string)datos.Lector["estado_nombre"]
            };
            incidente.UsuarioCreador = new Usuario
            {
                Id = (int)datos.Lector["usuario_creador_id"],
                Nombre = (string)datos.Lector["creador_nombre"],
                Apellido = datos.Lector["creador_apellido"] == DBNull.Value ? null : (string)datos.Lector["creador_apellido"],
                Email = datos.Lector["creador_email"] == DBNull.Value ? null : (string)datos.Lector["creador_email"]
            };
            incidente.UsuarioAsignado = new Usuario
            {
                Id = (int)datos.Lector["usuario_asignado_id"],
                Nombre = (string)datos.Lector["asignado_nombre"],
                Apellido = datos.Lector["asignado_apellido"] == DBNull.Value ? null : (string)datos.Lector["asignado_apellido"],
                Email = datos.Lector["asignado_email"] == DBNull.Value ? null : (string)datos.Lector["asignado_email"]
            };
            incidente.DatoResolucion = datos.Lector["dato_resolucion"] == DBNull.Value ? null : (string)datos.Lector["dato_resolucion"];
            incidente.FechaResolucion = datos.Lector["fecha_resolucion"] == DBNull.Value ? (DateTime?)null : (DateTime)datos.Lector["fecha_resolucion"];
            incidente.ComentarioCierre = datos.Lector["comentario_cierre"] == DBNull.Value ? null : (string)datos.Lector["comentario_cierre"];
            incidente.FechaCierre = datos.Lector["fecha_cierre"] == DBNull.Value ? (DateTime?)null : (DateTime)datos.Lector["fecha_cierre"];

            if (datos.Lector["usuario_resolucion_id"] != DBNull.Value)
            {
                incidente.UsuarioResolucion = new Usuario
                {
                    Id = (int)datos.Lector["usuario_resolucion_id"],
                    Nombre = datos.Lector["resolucion_nombre"] == DBNull.Value ? null : (string)datos.Lector["resolucion_nombre"],
                    Apellido = datos.Lector["resolucion_apellido"] == DBNull.Value ? null : (string)datos.Lector["resolucion_apellido"]
                };
            }

            if (datos.Lector["usuario_cierre_id"] != DBNull.Value)
            {
                incidente.UsuarioCierre = new Usuario
                {
                    Id = (int)datos.Lector["usuario_cierre_id"],
                    Nombre = datos.Lector["cierre_nombre"] == DBNull.Value ? null : (string)datos.Lector["cierre_nombre"],
                    Apellido = datos.Lector["cierre_apellido"] == DBNull.Value ? null : (string)datos.Lector["cierre_apellido"]
                };
            }

            return incidente;
        }

        private EstadoIncidencia ObtenerEstadoPorNombre(string nombre)
        {
            EstadoIncidencia estado = new EstadoIncidenciaNegocio().ObtenerPorNombre(nombre);
            if (estado == null)
                throw new ReglaNegocioException("No se encontro el estado requerido: " + nombre + ".");

            return estado;
        }

        private EstadoIncidencia DeterminarEstadoPorModificacion(EstadoIncidencia estadoActual)
        {
            if (EsEstado(estadoActual, EstadoIncidencia.Nombres.Resuelto) ||
                EsEstado(estadoActual, EstadoIncidencia.Nombres.Cerrado))
                return ObtenerEstadoPorNombre(EstadoIncidencia.Nombres.Reabierto);

            return ObtenerEstadoPorNombre(EstadoIncidencia.Nombres.EnAnalisis);
        }

        private void ValidarDatosAlta(Incidente incidente)
        {
            if (incidente == null)
                throw new ReglaNegocioException("Debe indicar los datos de la incidencia.");
            if (incidente.Cliente == null || incidente.Cliente.Id <= 0)
                throw new ReglaNegocioException("Debe seleccionar un cliente valido.");
            if (incidente.TipoIncidencia == null || incidente.TipoIncidencia.Id <= 0)
                throw new ReglaNegocioException("Debe seleccionar un tipo de incidencia valido.");
            if (incidente.Prioridad == null || incidente.Prioridad.Id <= 0)
                throw new ReglaNegocioException("Debe seleccionar una prioridad valida.");
            if (string.IsNullOrWhiteSpace(incidente.DescripcionProblematica))
                throw new ReglaNegocioException("Debe indicar la problematica del reclamo.");
        }

        private void ValidarUsuario(Usuario usuario)
        {
            if (usuario == null || usuario.Id <= 0)
                throw new ReglaNegocioException("Debe existir un usuario autenticado para operar con incidencias.");
        }

        private void ValidarAccesoIncidencia(Incidente incidente, Usuario usuario)
        {
            if (!PuedeVer(incidente, usuario))
                throw new ReglaNegocioException("El usuario no tiene permisos para operar sobre esta incidencia.");
        }

        private void ValidarNoCerrado(Incidente incidente)
        {
            if (EsEstado(incidente.EstadoActual, EstadoIncidencia.Nombres.Cerrado))
                throw new ReglaNegocioException("No se puede modificar una incidencia cerrada.");
        }

        private bool EsAdministradorOSupervisor(Usuario usuario)
        {
            if (usuario.Rol == null) return false;
            var rol = (RolesEnum)usuario.Rol.Id;
            return rol == RolesEnum.Administrador || rol == RolesEnum.Supervisor;
        }

        private bool EsEstado(EstadoIncidencia estado, string nombre)
        {
            return estado != null &&
                   string.Equals(estado.Nombre, nombre, StringComparison.OrdinalIgnoreCase);
        }

        private string GenerarNumeroReclamo()
        {
            return string.Format("REC-{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString("N").Substring(0, 6).ToUpperInvariant());
        }

        private string TextoMotivo(string motivo, string defecto)
        {
            return string.IsNullOrWhiteSpace(motivo) ? defecto : motivo.Trim();
        }

        private object ValorDb(object valor)
        {
            return valor ?? DBNull.Value;
        }

        private void EnviarCorreoAlta(Incidente incidente)
        {
            string asunto = "Alta de incidencia " + incidente.NroReclamo;
            string cuerpo =
                "Hola " + NombreCompletoCliente(incidente.Cliente) + "," + Environment.NewLine + Environment.NewLine +
                "Registramos tu incidencia con numero " + incidente.NroReclamo + "." + Environment.NewLine +
                "Tipo: " + incidente.TipoIncidencia.Nombre + Environment.NewLine +
                "Prioridad: " + incidente.Prioridad.Nombre + Environment.NewLine +
                "Detalle: " + incidente.DescripcionProblematica + Environment.NewLine + Environment.NewLine +
                "Gracias por comunicarte con el call center.";

            EnviarCorreoCliente(incidente.Cliente, asunto, cuerpo, "La incidencia fue creada pero no se pudo enviar el correo de alta.");
        }

        private void EnviarCorreoResolucion(Incidente incidente)
        {
            string asunto = "Incidencia resuelta " + incidente.NroReclamo;
            string cuerpo =
                "Hola " + NombreCompletoCliente(incidente.Cliente) + "," + Environment.NewLine + Environment.NewLine +
                "Tu incidencia " + incidente.NroReclamo + " fue resuelta." + Environment.NewLine +
                "Respuesta final: " + incidente.DatoResolucion + Environment.NewLine + Environment.NewLine +
                "Si necesitas mas ayuda, podes volver a contactarnos.";

            EnviarCorreoCliente(incidente.Cliente, asunto, cuerpo, "La incidencia fue resuelta pero no se pudo enviar el correo de resolucion.");
        }

        private void EnviarCorreoCierre(Incidente incidente)
        {
            string asunto = "Incidencia cerrada " + incidente.NroReclamo;
            string cuerpo =
                "Hola " + NombreCompletoCliente(incidente.Cliente) + "," + Environment.NewLine + Environment.NewLine +
                "La incidencia " + incidente.NroReclamo + " fue cerrada." + Environment.NewLine +
                "Comentario de cierre: " + incidente.ComentarioCierre + Environment.NewLine + Environment.NewLine +
                "Gracias por comunicarte con el call center.";

            EnviarCorreoCliente(incidente.Cliente, asunto, cuerpo, "La incidencia fue cerrada pero no se pudo enviar el correo de cierre.");
        }

        private void EnviarCorreoCliente(Cliente cliente, string asunto, string cuerpo, string mensajeError)
        {
            if (cliente == null || string.IsNullOrWhiteSpace(cliente.Email))
                throw new ReglaNegocioException("El cliente no tiene un email valido para notificaciones.");

            try
            {
                Correo correo = new Correo();
                correo.EnviarCorreo(cliente.Email, asunto, cuerpo);
            }
            catch (Exception ex)
            {
                throw new ReglaNegocioException(mensajeError + " " + ex.Message);
            }
        }

        private string NombreCompletoCliente(Cliente cliente)
        {
            if (cliente == null)
                return "cliente";

            string apellido = string.IsNullOrWhiteSpace(cliente.Apellido) ? string.Empty : " " + cliente.Apellido.Trim();
            return (cliente.Nombre ?? "cliente").Trim() + apellido;
        }
    }
}
