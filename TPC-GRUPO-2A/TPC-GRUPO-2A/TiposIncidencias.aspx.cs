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
                TipoIncidenciaNegocio tn = new TipoIncidenciaNegocio();
                List<TipoIncidencia> lista = tn.ObtenerTodos();
                Session["tiposIncidencia"] = lista;
                dgvTipos.DataSource = lista.FindAll(t => t.Activo);
                dgvTipos.DataBind();

            }
            catch(Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        private void CargarFormularioEdicion(int id)
        {
            try
            {
                TipoIncidenciaNegocio tn = new TipoIncidenciaNegocio();
                TipoIncidencia t = tn.ObtenerPorId(id);

                txtNombre.Text = t.Nombre;
                txtDescripcion.Text = t.Descripcion;
                lblTituloPanel.Text = "Editar Tipo de Incidencia";
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

                if (Request.QueryString["id"] != null)
                {
                    t.Id = int.Parse(Request.QueryString["id"]);
                    tn.Modificar(t);
                }
                else
                {
                    tn.Agregar(t);
                }

                LimpiarFormulario();
                pnlFormulario.Visible = false;
                Response.Redirect("~/TiposIncidencias.aspx");
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
                Response.Redirect("~/TiposIncidencias.aspx");
            }
        }
        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        protected void dgvTipos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("~/TiposIncidencias.aspx?id=" + id);
            }
            if (e.CommandName == "Baja")
            {
                try
                {
                    int id = int.Parse(e.CommandArgument.ToString());

                    TipoIncidenciaNegocio tn = new TipoIncidenciaNegocio();
                    TipoIncidencia t = tn.ObtenerPorId(id);
                    t.Activo = !t.Activo;
                    tn.Modificar(t);

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
            List<TipoIncidencia> lista = (List<TipoIncidencia>)Session["tiposIncidencia"];
            string busqueda = txtBuscar.Text.ToUpper();
            string estado = ddlEstado.SelectedValue;

            List<TipoIncidencia> listaFiltrada = lista.FindAll(t =>
            (t.Nombre.ToUpper().Contains(busqueda) ||
            (t.Descripcion != null && t.Descripcion.ToUpper().Contains(busqueda))) &&
            (estado == "todos" || (estado == "activo" ? t.Activo : !t.Activo)));

            dgvTipos.DataSource = listaFiltrada;
            dgvTipos.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            CargarGrilla();
        }
    }
}