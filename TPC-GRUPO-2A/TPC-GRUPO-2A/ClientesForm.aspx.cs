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
    public partial class ClientesForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtFechaAlta.Text = DateTime.Now.ToString("yyyy-MM-dd");


                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    pnlActivo.Visible=false;
                    txtFechaAlta.Enabled = false;
                    CargarFormularioEdicion(id);
                }
            }
        }

        private void CargarFormularioEdicion(int id)
        {
            try
            {
                ClienteNegocio cn = new ClienteNegocio();
                Cliente c = cn.ObtenerPorId(id);

                txtNombre.Text = c.Nombre;
                txtApellido.Text = c.Apellido;
                txtEmail.Text = c.Email;
                txtTelefono.Text = c.Telefono;
                txtIdentificacion.Text = c.Identificacion;
                txtFechaAlta.Text = c.FechaAlta.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("Cliente");

                if (!Page.IsValid) return;

                Cliente c = new Cliente();
                c.Nombre = txtNombre.Text.Trim();
                c.Apellido = txtApellido.Text.Trim();
                c.Email = txtEmail.Text.Trim();
                c.Telefono = txtTelefono.Text.Trim();
                c.Identificacion = txtIdentificacion.Text.Trim();
                c.FechaAlta = DateTime.Parse(txtFechaAlta.Text);
                c.Activo = chkActivo.Checked;

                ClienteNegocio cn = new ClienteNegocio();

                bool esEdicion = Request.QueryString["id"] != null;
                if (esEdicion)
                {
                    c.Id = int.Parse(Request.QueryString["id"]);
                    cn.Modificar(c);
                }
                else
                {
                    cn.Agregar(c);
                }

                string accion = esEdicion ? "modificado" : "registrado";
                ScriptManager.RegisterStartupScript(this, GetType(), "swOk",
                    $"swExito('Cliente {accion} correctamente.', 'Clientes.aspx');", true);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            string msg = mensaje.Replace("'", "\\'").Replace("\r\n", " ");
            ScriptManager.RegisterStartupScript(this, GetType(), "swErr",
                $"swError('{msg}');", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes.aspx");
        }
    }
}