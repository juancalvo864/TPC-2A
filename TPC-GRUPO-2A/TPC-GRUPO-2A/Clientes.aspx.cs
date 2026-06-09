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
    public partial class Clientes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientesForm.aspx");
        }

        private void CargarGrilla()
        {
            try
            {
                ClienteNegocio cn = new ClienteNegocio();
                dgvClientes.DataSource = cn.ObtenerTodos();
                dgvClientes.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }
        protected void dgvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("~/ClientesForm.aspx?id=" + id);
            }

            if (e.CommandName == "Baja")
            {
                try
                {
                    int id = int.Parse(e.CommandArgument.ToString());

                    ClienteNegocio cn = new ClienteNegocio();
                    Cliente c = cn.ObtenerPorId(id);
                    c.Activo = !c.Activo;
                    cn.Modificar(c);

                    CargarGrilla();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                }
            }
        }
    }
}