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

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TiposIncidenciasForm.aspx");
        }
        protected void dgvTipos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("~/TiposIncidenciasForm.aspx?id=" + id);
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