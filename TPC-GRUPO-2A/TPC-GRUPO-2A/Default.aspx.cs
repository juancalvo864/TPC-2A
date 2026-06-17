using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_GRUPO_2A
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario u = (Usuario)Session["usuario"];

                if (u == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                
                lblNombreUsuario.Text = u.Nombre + " " + u.Apellido;
            }

        }

    }
}