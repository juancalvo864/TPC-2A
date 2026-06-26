using System;

namespace negocio
{
    public class ReglaNegocioException : Exception
    {
        public ReglaNegocioException(string message) : base(message)
        {
        }
    }
}
