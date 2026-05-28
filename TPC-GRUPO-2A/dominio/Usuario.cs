using System;
using System.Collections.Generic;

namespace dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<UsuarioRol> Roles { get; set; }

        public Usuario()
        {
            Roles = new List<UsuarioRol>();
        }
    }
}
