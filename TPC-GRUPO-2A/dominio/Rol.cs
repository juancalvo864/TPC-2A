namespace dominio
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static class Nombres
        {
            public const string Administrador = "Administrador";
            public const string Telefonista = "Telefonista";
            public const string Supervisor = "Supervisor";
        }
    }
}
