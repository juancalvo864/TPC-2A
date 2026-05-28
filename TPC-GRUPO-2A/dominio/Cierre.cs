using System;

namespace dominio
{
    public class Cierre
    {
        public int Id { get; set; }
        public Incidente Incidente { get; set; }
        public DateTime FechaCierre { get; set; }
        public Usuario Usuario { get; set; }
        public string ComentarioFinalObligatorio { get; set; }
        public string MotivoCierre { get; set; }
    }
}
