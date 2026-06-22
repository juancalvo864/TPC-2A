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
    public partial class RecuperarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();

                UsuarioNegocio un = new UsuarioNegocio();
                Usuario usuario = un.ObtenerPorEmail(email);

                if (usuario != null)
                {
                    string nuevaPassword = GenerarPasswordRandom();

                    usuario.HashPassword = nuevaPassword;
                    un.Modificar(usuario);
                }

                pnlFormulario.Visible = false;
                pnlConfirmacion.Visible = true;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        private string GenerarPasswordRandom()
        {
            const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] password = new char[8];

            for (int i = 0; i < password.Length; i++)
                password[i] = caracteres[random.Next(caracteres.Length)];

            return new string(password);
        }
    }
}