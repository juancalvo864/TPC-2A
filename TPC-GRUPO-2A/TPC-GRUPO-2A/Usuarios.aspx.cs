using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class Usuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SesionHelper.EsAdministrador)
                Response.Redirect("~/Default.aspx");

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            List<Usuario> listaUsuarios = negocio.ObtenerTodos();
            Session["usuarios"] = listaUsuarios;

            dgvUsuarios.DataSource = listaUsuarios.FindAll(t => t.Activo);
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

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            List<Usuario> listaUsuarios = (List<Usuario>)Session["usuarios"];
            string busqueda = txtBuscar.Text.ToUpper();
            string estado = ddlEstado.SelectedValue;

            List<Usuario> listaFiltrada = listaUsuarios.FindAll(c =>
                (c.Nombre.ToUpper().Contains(busqueda) ||
                 c.Email.ToUpper().Contains(busqueda)) &&
                (estado == "todos" || (estado == "activo" ? c.Activo : !c.Activo)));

            dgvUsuarios.DataSource = listaFiltrada;
            dgvUsuarios.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            CargarGrilla();
        }
    }
}
