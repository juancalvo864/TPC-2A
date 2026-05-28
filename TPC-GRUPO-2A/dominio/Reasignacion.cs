using System;

namespace dominio
{
    public class Reasignacion
    {
        public int Id { get; set; }
        public Incidente Incidente { get; set; }
        public DateTime FechaReasignacion { get; set; }
        public Usuario UsuarioOrigen { get; set; }
        public Usuario UsuarioDestino { get; set; }
        public Usuario ActorSupervisor { get; set; }
        public string Motivo { get; set; }
    }
}
