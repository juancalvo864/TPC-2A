using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];
                txtNombre.Text = u.Nombre;
                txtApellido.Text = u.Apellido;
                txtEmail.Text = u.Email;
                txtRol.Text = u.Rol.Nombre;
                txtFechaCreacion.Text = u.FechaCreacion.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }
    }
}