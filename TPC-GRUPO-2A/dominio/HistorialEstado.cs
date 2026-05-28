using System;

namespace dominio
{
    public class HistorialEstado
    {
        public int Id { get; set; }
        public Incidente Incidente { get; set; }
        public DateTime FechaCambio { get; set; }
        public EstadoIncidencia EstadoAnterior { get; set; }
        public EstadoIncidencia EstadoNuevo { get; set; }
        public Usuario ActorUsuario { get; set; }
        public string MotivoNota { get; set; }
    }
}
