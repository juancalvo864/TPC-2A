using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class Reclamos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                IncidenteNegocio inc = new IncidenteNegocio();
                List<Incidente> lista = inc.ObtenerTodos();
                Session["reclamos"] = lista;
                dgvReclamos.DataSource = lista;
                dgvReclamos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }
    }
}