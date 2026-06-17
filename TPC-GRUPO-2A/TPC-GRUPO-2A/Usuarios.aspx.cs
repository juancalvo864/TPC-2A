using negocio;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class Usuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.ObtenerTodos();
            dgvUsuarios.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuariosForm.aspx");
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("~/UsuariosForm.aspx?id=" + id);
            }
        }

        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkActivo = (CheckBox)sender;
            GridViewRow fila = (GridViewRow)chkActivo.NamingContainer;
            int id = (int)dgvUsuarios.DataKeys[fila.RowIndex].Value;

            UsuarioNegocio negocio = new UsuarioNegocio();
            dominio.Usuario usuario = negocio.ObtenerPorId(id);

            if (usuario == null)
                return;

            usuario.Activo = chkActivo.Checked;
            negocio.Modificar(usuario);
            CargarGrilla();
        }
    }
}
