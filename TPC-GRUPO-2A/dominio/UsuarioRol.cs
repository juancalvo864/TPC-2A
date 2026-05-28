using System;

namespace dominio
{
    public class UsuarioRol
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Rol Rol { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
