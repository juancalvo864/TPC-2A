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

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblTituloPanel.Text = "Nueva Prioridad";
            pnlFormulario.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtNivel.Text = string.Empty;
        }
    }
}