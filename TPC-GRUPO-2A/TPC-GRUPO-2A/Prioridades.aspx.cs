using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class Prioridades : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            try
            {
                PrioridadNegocio pn = new PrioridadNegocio();
                dgvPrioridades.DataSource = pn.ObtenerTodos();
                dgvPrioridades.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }
    }
}