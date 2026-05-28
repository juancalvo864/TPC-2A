using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_GRUPO_2A
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarClientes();
        }
        private void CargarClientes()
        {
            ClienteNegocio cn = new ClienteNegocio();
            dgvClientes.DataSource = cn.ObtenerTodos();
            dgvClientes.DataBind();
        }

    }
}