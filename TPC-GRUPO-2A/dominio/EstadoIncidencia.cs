namespace dominio
{
    public class EstadoIncidencia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static class Nombres
        {
            public const string Abierto = "Abierto";
            public const string Asignado = "Asignado";
            public const string EnAnalisis = "En Analisis";
            public const string Resuelto = "Resuelto";
            public const string Cerrado = "Cerrado";
            public const string Reabierto = "Reabierto";
        }
    }
}
