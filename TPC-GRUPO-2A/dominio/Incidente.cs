using System;
using System.Collections.Generic;

namespace dominio
{
    public class Incidente
    {
        public int Id { get; set; }
        public string NroReclamo { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public string DescripcionProblematica { get; set; }
        public Cliente Cliente { get; set; }
        public TipoIncidencia TipoIncidencia { get; set; }
        public Prioridad Prioridad { get; set; }
        public EstadoIncidencia EstadoActual { get; set; }
        public Usuario UsuarioCreador { get; set; }
        public Usuario UsuarioAsignado { get; set; }
        public List<HistorialEstado> HistorialEstados { get; set; }

        public string DatoResolucion { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public Usuario UsuarioResolucion { get; set; }

        public string ComentarioCierre { get; set; }
        public DateTime? FechaCierre { get; set; }
        public Usuario UsuarioCierre { get; set; }

        public Incidente()
        {
            FechaAlta = DateTime.Now;
            FechaUltimaActualizacion = DateTime.Now;
            HistorialEstados = new List<HistorialEstado>();
        }
    }
}
