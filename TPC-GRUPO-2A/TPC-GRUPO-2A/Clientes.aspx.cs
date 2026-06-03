using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class Clientes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            dgvClientes.DataSource = negocio.listarClientesConSP();
            dgvClientes.DataBind(); 
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientesForm.aspx");
        }
    }
}