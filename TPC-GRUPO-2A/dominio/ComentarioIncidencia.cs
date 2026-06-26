using System;

namespace dominio
{
    public class ComentarioIncidencia
    {
        public int Id { get; set; }
        public int IncidenteId { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaComentario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
