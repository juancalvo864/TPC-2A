using dominio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A.Controls
{
    public partial class Header : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Usuario u = (Usuario)Session["usuario"];
                lblNombre.Text = u.Nombre + " " + u.Apellido;
                lblRol.Text = u.Rol.Nombre;
                lblIniciales.Text = u.Nombre.Substring(0, 1).ToUpper() +
                                    (string.IsNullOrEmpty(u.Apellido) ? "" : u.Apellido.Substring(0, 1).ToUpper());
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}