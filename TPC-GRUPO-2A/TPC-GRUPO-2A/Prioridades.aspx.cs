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
    public partial class Prioridades : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SesionHelper.EsTelefonista)
                Response.Redirect("~/Default.aspx");

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
                List<Prioridad> lista = pn.ObtenerTodos();
                Session["prioridades"] = lista;
                dgvPrioridades.DataSource = lista.FindAll(p => p.Activo);
                dgvPrioridades.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PrioridadesForm.aspx");
        }

        protected void dgvPrioridades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("~/PrioridadesForm.aspx?id=" + id);
            }

            if (e.CommandName == "Baja")
            {
                try
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    PrioridadNegocio pn = new PrioridadNegocio();
                    Prioridad p = pn.ObtenerPorId(id);
                    p.Activo = !p.Activo;
                    pn.Modificar(p);
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
            List<Prioridad> lista = (List<Prioridad>)Session["prioridades"];
            string busqueda = txtBuscar.Text.ToUpper();
            string estado = ddlEstado.SelectedValue;

            List<Prioridad> listaFiltrada = lista.FindAll(p =>
                p.Nombre.ToUpper().Contains(busqueda) &&
                (estado == "todos" || (estado == "activo" ? p.Activo : !p.Activo))
            );

            dgvPrioridades.DataSource = listaFiltrada;
            dgvPrioridades.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            CargarGrilla();
        }
    }
}