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
    public partial class TiposIncidencias : Page
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
                TipoIncidenciaNegocio tn = new TipoIncidenciaNegocio();
                dgvTipos.DataSource = tn.ObtenerTodos();
                dgvTipos.DataBind();

            }
            catch(Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            pnlFormulario.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoIncidencia t = new TipoIncidencia();
                t.Nombre = txtNombre.Text.Trim();
                t.Descripcion = txtDescripcion.Text.Trim();
                t.Activo = true;

                TipoIncidenciaNegocio tn = new TipoIncidenciaNegocio();
                tn.Agregar(t);

                LimpiarFormulario();
                pnlFormulario.Visible = false;
                CargarGrilla();
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
        }
        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }
    }
}