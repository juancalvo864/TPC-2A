using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class ReclamosForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarCombos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("Incidencia");
                if (!Page.IsValid)
                    return;

                Usuario usuario = Session["usuario"] as Usuario;
                Incidente incidente = new Incidente
                {
                    Cliente = new Cliente { Id = int.Parse(ddlCliente.SelectedValue) },
                    TipoIncidencia = new TipoIncidencia { Id = int.Parse(ddlTipo.SelectedValue) },
                    Prioridad = new Prioridad { Id = int.Parse(ddlPrioridad.SelectedValue) },
                    DescripcionProblematica = txtProblematica.Text.Trim()
                };

                IncidenteNegocio negocio = new IncidenteNegocio();
                negocio.CrearIncidencia(incidente, usuario);

                ScriptManager.RegisterStartupScript(this, GetType(), "swOk",
                    "swExito('Incidencia registrada correctamente.', 'Reclamos.aspx');", true);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reclamos.aspx");
        }

        private void CargarCombos()
        {
            List<Cliente> clientes = new ClienteNegocio()
                .ObtenerTodos()
                .Where(x => x.Activo)
                .OrderBy(x => x.Nombre)
                .ThenBy(x => x.Apellido)
                .ToList();

            ddlCliente.Items.Clear();
            foreach (Cliente cliente in clientes)
            {
                string texto = string.Format("{0} {1} - {2}", cliente.Nombre, cliente.Apellido, cliente.Email).Trim();
                ddlCliente.Items.Add(new ListItem(texto, cliente.Id.ToString()));
            }
            ddlCliente.Items.Insert(0, new ListItem("Seleccionar cliente", string.Empty));

            ddlTipo.DataSource = new TipoIncidenciaNegocio().ObtenerTodos().Where(x => x.Activo).OrderBy(x => x.Nombre).ToList();
            ddlTipo.DataTextField = "Nombre";
            ddlTipo.DataValueField = "Id";
            ddlTipo.DataBind();
            ddlTipo.Items.Insert(0, new ListItem("Seleccionar tipo", string.Empty));

            ddlPrioridad.DataSource = new PrioridadNegocio().ObtenerTodos().Where(x => x.Activo).OrderBy(x => x.Nivel).ToList();
            ddlPrioridad.DataTextField = "Nombre";
            ddlPrioridad.DataValueField = "Id";
            ddlPrioridad.DataBind();
            ddlPrioridad.Items.Insert(0, new ListItem("Seleccionar prioridad", string.Empty));
        }

        private void MostrarError(string mensaje)
        {
            string msg = mensaje.Replace("'", "\\'").Replace("\r\n", " ");
            ScriptManager.RegisterStartupScript(this, GetType(), "swErr",
                $"swError('{msg}');", true);
        }
    }
}
