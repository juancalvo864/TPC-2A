using System;

namespace dominio
{
    public class Resolucion
    {
        public int Id { get; set; }
        public Incidente Incidente { get; set; }
        public DateTime FechaResolucion { get; set; }
        public Usuario Usuario { get; set; }
        public string DatoFinalResolucion { get; set; }
    }
}
