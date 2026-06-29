using System.Web;
using dominio;

namespace TPC_GRUPO_2A
{
    public static class SesionHelper
    {
        public static Usuario UsuarioActual
            => HttpContext.Current.Session["usuario"] as Usuario;

        public static RolesEnum RolActual
            => (RolesEnum)UsuarioActual.Rol.Id;

        public static bool EsAdministrador => RolActual == RolesEnum.Administrador;
        public static bool EsSupervisor    => RolActual == RolesEnum.Supervisor;
        public static bool EsTelefonista   => RolActual == RolesEnum.Telefonista;
    }
}
