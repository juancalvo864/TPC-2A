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
            if (!IsPostBack)
            {
                CargarGrilla();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarFormularioEdicion(id);
                }
            }
        }

        private void CargarGrilla()
        {
            try
            {
                PrioridadNegocio pn = new PrioridadNegocio();
                List<Prioridad> lista = pn.ObtenerTodos();
                Session["prioridades"] = lista;
                dgvPrioridades.DataSource = lista;
                dgvPrioridades.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        private void CargarFormularioEdicion(int id)
        {
            try
            {
                PrioridadNegocio pn = new PrioridadNegocio();
                Prioridad p = pn.ObtenerPorId(id);

                txtNombre.Text = p.Nombre;
                txtNivel.Text = p.Nivel.ToString();
                lblTituloPanel.Text = "Editar Prioridad";
                pnlFormulario.Visible = true;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblTituloPanel.Text = "Nueva Prioridad";
            pnlFormulario.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Prioridad p = new Prioridad();
                p.Nombre = txtNombre.Text.Trim();
                p.Nivel = int.Parse(txtNivel.Text.Trim());
                p.Activo = true;

                PrioridadNegocio pn = new PrioridadNegocio();

                if (Request.QueryString["id"] != null)
                {
                    p.Id = int.Parse(Request.QueryString["id"]);
                    pn.Modificar(p);
                }
                else
                {
                    pn.Agregar(p);
                }

                LimpiarFormulario();
                pnlFormulario.Visible = false;
                Response.Redirect("~/Prioridades.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            pnlFormulario.Visible = false;
            if (Request.QueryString["id"] != null)
            {
                Response.Redirect("~/Prioridades.aspx");
            }
        }
        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtNivel.Text = string.Empty;
        }

        protected void dgvPrioridades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("~/Prioridades.aspx?id=" + id);
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

            List<Prioridad> listaFiltrada = lista.FindAll(p =>
                p.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper())
            );

            dgvPrioridades.DataSource = listaFiltrada;
            dgvPrioridades.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            dgvPrioridades.DataSource = (List<Prioridad>)Session["prioridades"];
            dgvPrioridades.DataBind();
        }
    }
}