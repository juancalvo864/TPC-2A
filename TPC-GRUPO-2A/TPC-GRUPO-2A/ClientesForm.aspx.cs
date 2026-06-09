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

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente c = new Cliente();
                c.Nombre = txtNombre.Text.Trim();
                c.Apellido = txtNombre.Text.Trim();
                c.Email = txtEmail.Text.Trim();
                c.Telefono = txtTelefono.Text.Trim();
                c.Identificacion = txtIdentificacion.Text.Trim();
                c.FechaAlta = DateTime.Parse(txtFechaAlta.Text);
                c.Activo = chkActivo.Checked;

                ClienteNegocio cn = new ClienteNegocio();
                cn.Agregar(c);
               
                Response.Redirect("~/Clientes.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clientes.aspx");
        }
    }
}