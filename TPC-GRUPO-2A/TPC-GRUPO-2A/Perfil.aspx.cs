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

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            btnEditar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            btnEditar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            CargarDatos();
        }
    }
}