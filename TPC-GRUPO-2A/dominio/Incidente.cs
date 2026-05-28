using System;
using System.Collections.Generic;

namespace dominio
{
    public class Incidente
    {
        public int Id { get; set; }
        public string NroReclamo { get; set; }
        public DateTime FechaAlta { get; set; }
        public string DescripcionProblematica { get; set; }
        public Cliente Cliente { get; set; }
        public TipoIncidencia TipoIncidencia { get; set; }
        public Prioridad Prioridad { get; set; }
        public EstadoIncidencia EstadoActual { get; set; }
        public Usuario CreadorUsuario { get; set; }
        public Usuario AsignadoUsuario { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public bool EliminadoLogico { get; set; }
        public List<HistorialEstado> HistorialEstados { get; set; }
        public Resolucion Resolucion { get; set; }
        public Cierre Cierre { get; set; }

        public Incidente()
        {
            FechaAlta = DateTime.Now;
            FechaUltimaActualizacion = DateTime.Now;
            EliminadoLogico = false;
            HistorialEstados = new List<HistorialEstado>();
        }
    }
}
