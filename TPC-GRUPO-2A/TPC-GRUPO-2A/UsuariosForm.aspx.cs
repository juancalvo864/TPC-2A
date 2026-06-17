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

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarFormularioEdicion(id);
                }
                else
                {
                    lblPasswordAyuda.Text = string.Empty;
                }
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

        private void CargarFormularioEdicion(int id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.ObtenerPorId(id);

            if (usuario == null)
            {
                Response.Redirect("~/Usuarios.aspx");
                return;
            }

            lblTitulo.Text = "Editar Usuario";
            btnEliminar.Visible = true;
            rfvPassword.Enabled = false;
            lblPasswordAyuda.Text = "Deja la contrasena vacia si no quieres cambiarla.";

            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            txtEmail.Text = usuario.Email;
            ddlRol.SelectedValue = usuario.Rol.Id.ToString();
            ddlActivo.SelectedValue = usuario.Activo ? "true" : "false";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("Usuario");
                if (!Page.IsValid)
                    return;

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario u;

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    u = negocio.ObtenerPorId(id);

                    if (u == null)
                    {
                        Response.Redirect("~/Usuarios.aspx");
                        return;
                    }
                }
                else
                {
                    u = new Usuario();

                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        rfvPassword.Enabled = true;
                        Page.Validate("Usuario");
                        return;
                    }
                }

                u.Nombre = txtNombre.Text.Trim();
                u.Apellido = txtApellido.Text.Trim();
                u.Email = txtEmail.Text.Trim();
                u.Login = u.Email;
                u.Activo = bool.Parse(ddlActivo.SelectedValue);
                u.Rol = new Rol();
                u.Rol.Id = int.Parse(ddlRol.SelectedValue);

                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    u.HashPassword = txtPassword.Text.Trim();

                if (Request.QueryString["id"] != null)
                    negocio.Modificar(u);
                else
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect("~/Usuarios.aspx");
                    return;
                }

                int id = int.Parse(Request.QueryString["id"]);
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Eliminar(id);

                Response.Redirect("~/Usuarios.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }
    }
}
