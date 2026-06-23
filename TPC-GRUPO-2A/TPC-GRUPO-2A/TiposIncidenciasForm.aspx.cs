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
    public partial class TipoIncidenciasForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarFormularioEdicion(id);
                }
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
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TipoIncidencia t = new TipoIncidencia();
                t.Nombre = txtNombre.Text.Trim();
                t.Descripcion = txtDescripcion.Text.Trim();

                TipoIncidenciaNegocio tn = new TipoIncidenciaNegocio();

                if (Request.QueryString["id"] != null)
                {
                    t.Id = int.Parse(Request.QueryString["id"]);
                    t.Activo = tn.ObtenerPorId(t.Id).Activo;
                    tn.Modificar(t);
                }
                else
                {
                    t.Activo = true;
                    tn.Agregar(t);
                }

                Response.Redirect("~/TiposIncidencias.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TiposIncidencias.aspx");
        }
    }
}