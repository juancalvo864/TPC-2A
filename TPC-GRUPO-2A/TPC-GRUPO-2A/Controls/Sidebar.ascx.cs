using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace TPC_GRUPO_2A.Controls
{
    public partial class Sidebar : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AplicarVisibilidadPorRol();
            MarcarLinkActivo();
        }

        private void AplicarVisibilidadPorRol()
        {
            lnkUsuarios.Visible = SesionHelper.EsAdministrador;
            divAdministracion.Visible = !SesionHelper.EsTelefonista;

            bool noEsTelefonista = !SesionHelper.EsTelefonista;
            lnkTipos.Visible       = noEsTelefonista;
            lnkPrioridades.Visible = noEsTelefonista;
        }

        private void MarcarLinkActivo()
        {
            string paginaActual = Request.AppRelativeCurrentExecutionFilePath.ToLower();

            QuitarActivo(lnkDashboard, lnkReclamos, lnkClientes, lnkUsuarios, lnkTipos, lnkPrioridades);

            if (paginaActual.Contains("incidencias"))
                Activar(lnkReclamos);
            else if (paginaActual.Contains("clientes"))
                Activar(lnkClientes);
            else if (paginaActual.Contains("usuarios"))
                Activar(lnkUsuarios);
            else if (paginaActual.Contains("tiposincidencias"))
                Activar(lnkTipos);
            else if (paginaActual.Contains("prioridades"))
                Activar(lnkPrioridades);
            else
                Activar(lnkDashboard);
        }

        private void QuitarActivo(params HtmlAnchor[] links)
        {
            foreach (HtmlAnchor link in links)
                link.Attributes["class"] = "sidebar-link";
        }

        private void Activar(HtmlAnchor link)
        {
            link.Attributes["class"] = "sidebar-link active";
        }
    }
}
