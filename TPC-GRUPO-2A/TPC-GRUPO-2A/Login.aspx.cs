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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Response.Redirect("~/Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MostrarError("Completa usuario y contraseña.");
                return;
            }

            try
            {
                LoginNegocio loginNegocio = new LoginNegocio();
                Usuario usuario = loginNegocio.ValidarUsuario(login, password);

                if (usuario == null)
                {
                    MostrarError("Usuario o contraseña incorrectos.");
                    return;
                }

                Session["usuario"] = usuario;
                Response.Redirect("~/Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo iniciar sesión. " + ex.Message);
                Session["Error"] = ex;
            }
        }

        private void MostrarError(string mensaje)
        {
            litError.Text = mensaje;
            pnlError.Visible = true;
        }
    }
}
