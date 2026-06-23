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
    public partial class PrioridadesForm : System.Web.UI.Page
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
                PrioridadNegocio pn = new PrioridadNegocio();
                Prioridad p = pn.ObtenerPorId(id);

                txtNombre.Text = p.Nombre;
                txtNivel.Text = p.Nivel.ToString();
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
                Prioridad p = new Prioridad();
                p.Nombre = txtNombre.Text.Trim();
                p.Nivel = int.Parse(txtNivel.Text.Trim());

                PrioridadNegocio pn = new PrioridadNegocio();

                if (Request.QueryString["id"] != null)
                {
                    p.Id = int.Parse(Request.QueryString["id"]);
                    p.Activo = pn.ObtenerPorId(p.Id).Activo;
                    pn.Modificar(p);
                }
                else
                {
                    p.Activo = true;
                    pn.Agregar(p);
                }

                Response.Redirect("~/Prioridades.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Prioridades.aspx");
        }
    }
}