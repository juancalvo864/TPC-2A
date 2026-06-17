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
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx",false);
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
                pnlError.Visible = false;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            pnlPassword.Visible = true;
            btnEditar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario u = (Usuario)Session["usuario"];
                u.Nombre = txtNombre.Text.Trim();
                u.Apellido = txtApellido.Text.Trim();

                if (!string.IsNullOrEmpty(txtNuevaPassword.Text))
                {
                    if (txtNuevaPassword.Text != txtConfirmarPassword.Text)
                    {
                        MostrarError("Las contraseñas no coinciden.");
                        return;
                    }
                    u.HashPassword = txtNuevaPassword.Text;
                }

                UsuarioNegocio un = new UsuarioNegocio();
                un.Modificar(u);

                Session["usuario"] = u;

                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                pnlPassword.Visible = false;
                btnEditar.Visible = true;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            pnlPassword.Visible = false;
            btnEditar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            CargarDatos();
        }

        private void MostrarError(string mensaje)
        {
            litError.Text = mensaje;
            pnlError.Visible = true;
        }

    }
}