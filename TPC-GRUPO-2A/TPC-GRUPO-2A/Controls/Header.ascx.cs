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
                string imagePath = Server.MapPath("~/Images/ProfileImages/" + u.ImgUrl);
                if (!string.IsNullOrEmpty(u.ImgUrl) && System.IO.File.Exists(imagePath))
                    imgPerfil.ImageUrl = "~/Images/ProfileImages/" + u.ImgUrl;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}