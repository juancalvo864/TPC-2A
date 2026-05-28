using System;

namespace dominio
{
    public class NotificacionEmail
    {
        public int Id { get; set; }
        public Incidente Incidente { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string TipoEvento { get; set; }
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string EstadoEnvio { get; set; }

        public static class TiposEvento
        {
            public const string Alta = "ALTA";
            public const string Resuelto = "RESUELTO";
            public const string Cerrado = "CERRADO";
        }

        public static class EstadosEnvio
        {
            public const string Enviado = "ENVIADO";
            public const string Fallido = "FALLIDO";
            public const string Pendiente = "PENDIENTE";
        }
    }
}
