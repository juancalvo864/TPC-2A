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
                List<Cliente> lista = cn.ObtenerTodos();
                Session["clientes"] = lista;
                dgvClientes.DataSource = lista.FindAll(c => c.Activo);
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

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            List<Cliente> lista = (List<Cliente>)Session["clientes"];
            string busqueda = txtBuscar.Text.ToUpper();
            string estado = ddlEstado.SelectedValue;

            List<Cliente> listaFiltrada = lista.FindAll(c =>
                (c.Nombre.ToUpper().Contains(busqueda) ||
                 c.Email.ToUpper().Contains(busqueda)) &&
                (estado == "todos" || (estado == "activo" ? c.Activo : !c.Activo)));

            dgvClientes.DataSource = listaFiltrada;
            dgvClientes.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            CargarGrilla();
        }
    }
}