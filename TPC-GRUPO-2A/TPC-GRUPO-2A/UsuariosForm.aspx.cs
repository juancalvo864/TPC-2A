using dominio;
using negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class UsuariosForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
        }

        private void CargarRoles()
        {
            RolNegocio negocio = new RolNegocio();
            ddlRol.DataSource = negocio.ObtenerTodos();
            ddlRol.DataTextField = "Nombre";
            ddlRol.DataValueField = "Id";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem("Selecciona un rol", ""));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("Usuario");
                if (!Page.IsValid)
                    return;

                Usuario u = new Usuario();
                u.Nombre = txtNombre.Text.Trim();
                u.Apellido = txtApellido.Text.Trim();
                u.Email = txtEmail.Text.Trim();
                u.Login = u.Email;
                u.HashPassword = txtPassword.Text.Trim();
                u.Activo = bool.Parse(ddlActivo.SelectedValue);
                u.Rol = new Rol();
                u.Rol.Id = int.Parse(ddlRol.SelectedValue);

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Agregar(u);

                Response.Redirect("~/Usuarios.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Usuarios.aspx");
        }
    }
}
