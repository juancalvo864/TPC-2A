using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class Reclamos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarPanel();
            }
        }

        protected void btnNuevaIncidencia_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReclamosForm.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
            ddlPrioridad.SelectedIndex = 0;
            CargarPanel();
        }

        protected void dgvIncidencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Abrir")
                return;

            try
            {
                int id = int.Parse(e.CommandArgument.ToString());
                CargarModal(id);
                MostrarModal();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swErr", $"swError('{SanitizarJs(ex.Message)}');", true);
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hdnIncidenteId.Value);
                Usuario usuario = Session["usuario"] as Usuario;
                Incidente incidente = new Incidente
                {
                    Id = id,
                    TipoIncidencia = new TipoIncidencia { Id = int.Parse(ddlTipoDetalle.SelectedValue) },
                    Prioridad = new Prioridad { Id = int.Parse(ddlPrioridadDetalle.SelectedValue) },
                    Cliente = new Cliente { Id = 1 },
                    DescripcionProblematica = txtProblematicaDetalle.Text.Trim()
                };

                IncidenteNegocio negocio = new IncidenteNegocio();
                Incidente actual = negocio.ObtenerPorId(id);
                incidente.Cliente.Id = actual.Cliente.Id;
                negocio.ActualizarIncidencia(incidente, usuario, "Actualizacion desde panel");

                CargarPanel();
                CargarModal(id);
                MostrarModal();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swErr", $"swError('{SanitizarJs(ex.Message)}');", true);
                MostrarModal();
            }
        }

        protected void btnReasignar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hdnIncidenteId.Value);
                Usuario usuario = Session["usuario"] as Usuario;
                IncidenteNegocio negocio = new IncidenteNegocio();
                negocio.ReasignarIncidencia(id, int.Parse(ddlAsignadoDetalle.SelectedValue), usuario, "Reasignacion desde panel");

                CargarPanel();
                CargarModal(id);
                MostrarModal();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swErr", $"swError('{SanitizarJs(ex.Message)}');", true);
                MostrarModal();
            }
        }

        protected void btnAgregarComentario_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hdnIncidenteId.Value);
                Usuario usuario = Session["usuario"] as Usuario;
                IncidenteNegocio negocio = new IncidenteNegocio();
                negocio.AgregarComentario(id, txtNuevoComentario.Text, usuario);
                txtNuevoComentario.Text = string.Empty;

                CargarPanel();
                CargarModal(id);
                MostrarModal();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swErr", $"swError('{SanitizarJs(ex.Message)}');", true);
                MostrarModal();
            }
        }

        private void CargarCombos()
        {
            EstadoIncidenciaNegocio estadoNegocio = new EstadoIncidenciaNegocio();
            PrioridadNegocio prioridadNegocio = new PrioridadNegocio();

            ddlEstado.DataSource = estadoNegocio.ObtenerTodos().Where(x => x.Activo).ToList();
            ddlEstado.DataTextField = "Nombre";
            ddlEstado.DataValueField = "Nombre";
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("Todos los estados", string.Empty));

            ddlPrioridad.DataSource = prioridadNegocio.ObtenerTodos().Where(x => x.Activo).ToList();
            ddlPrioridad.DataTextField = "Nombre";
            ddlPrioridad.DataValueField = "Nombre";
            ddlPrioridad.DataBind();
            ddlPrioridad.Items.Insert(0, new ListItem("Todas las prioridades", string.Empty));
        }

        private void CargarPanel()
        {
            try
            {
                Usuario usuario = Session["usuario"] as Usuario;
                IncidenteNegocio negocio = new IncidenteNegocio();
                List<Incidente> lista = negocio.ObtenerVisiblesParaUsuario(usuario);
                Session["reclamos"] = lista;

                CargarIndicadores(lista);
                BindLista(lista);
            }
            catch (Exception ex)
            {
                Session["Error"] = ex;
            }
        }

        private void AplicarFiltros()
        {
            List<Incidente> lista = Session["reclamos"] as List<Incidente> ?? new List<Incidente>();
            string busqueda = (txtBuscar.Text ?? string.Empty).Trim().ToUpperInvariant();
            string estado = ddlEstado.SelectedValue;
            string prioridad = ddlPrioridad.SelectedValue;

            List<Incidente> filtrada = lista.Where(x =>
                (string.IsNullOrEmpty(busqueda) ||
                 x.NroReclamo.ToUpperInvariant().Contains(busqueda) ||
                 x.DescripcionProblematica.ToUpperInvariant().Contains(busqueda) ||
                 ObtenerNombreCliente(x.Cliente.Nombre, x.Cliente.Apellido).ToUpperInvariant().Contains(busqueda)) &&
                (string.IsNullOrEmpty(estado) || string.Equals(x.EstadoActual.Nombre, estado, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(prioridad) || string.Equals(x.Prioridad.Nombre, prioridad, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            CargarIndicadores(filtrada);
            BindLista(filtrada);
        }

        private void BindLista(List<Incidente> lista)
        {
            pnlSinResultados.Visible = lista.Count == 0;
            dgvIncidencias.Visible = lista.Count > 0;
            dgvIncidencias.DataSource = lista;
            dgvIncidencias.DataBind();
        }

        private void CargarIndicadores(List<Incidente> lista)
        {
            litAbiertas.Text = ContarPorEstado(lista, EstadoIncidencia.Nombres.Abierto).ToString();
            litAnalisis.Text = ContarPorEstado(lista, EstadoIncidencia.Nombres.EnAnalisis).ToString();
            litResueltas.Text = ContarPorEstado(lista, EstadoIncidencia.Nombres.Resuelto).ToString();
            litCerradas.Text = ContarPorEstado(lista, EstadoIncidencia.Nombres.Cerrado).ToString();
        }

        private int ContarPorEstado(List<Incidente> lista, string estado)
        {
            return lista.Count(x => x.EstadoActual != null &&
                                   string.Equals(x.EstadoActual.Nombre, estado, StringComparison.OrdinalIgnoreCase));
        }

        public string ObtenerClaseEstado(object estado)
        {
            string valor = estado == null ? string.Empty : estado.ToString();
            switch (valor)
            {
                case EstadoIncidencia.Nombres.Abierto:
                    return "text-bg-primary";
                case EstadoIncidencia.Nombres.Asignado:
                    return "text-bg-info";
                case EstadoIncidencia.Nombres.EnAnalisis:
                    return "text-bg-warning";
                case EstadoIncidencia.Nombres.Resuelto:
                    return "text-bg-success";
                case EstadoIncidencia.Nombres.Cerrado:
                    return "text-bg-secondary";
                case EstadoIncidencia.Nombres.Reabierto:
                    return "text-bg-danger";
                default:
                    return "text-bg-light";
            }
        }

        public string ObtenerClasePrioridad(object prioridad)
        {
            string valor = prioridad == null ? string.Empty : prioridad.ToString();
            switch (valor.ToUpperInvariant())
            {
                case "ALTA":
                    return "text-bg-danger";
                case "MEDIA":
                    return "text-bg-warning";
                case "BAJA":
                    return "text-bg-success";
                default:
                    return "text-bg-secondary";
            }
        }

        public string ObtenerNombreCliente(object nombre, object apellido)
        {
            string nombreTexto = nombre == null ? string.Empty : nombre.ToString();
            string apellidoTexto = apellido == null ? string.Empty : apellido.ToString();
            return (nombreTexto + " " + apellidoTexto).Trim();
        }

        public string ObtenerNombreUsuario(object nombre, object apellido)
        {
            return ObtenerNombreCliente(nombre, apellido);
        }

        public string ResumirTexto(object texto)
        {
            string valor = texto == null ? string.Empty : texto.ToString().Trim();
            if (valor.Length <= 72)
                return valor;

            return valor.Substring(0, 69) + "...";
        }

        private void CargarModal(int id)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            IncidenteNegocio negocio = new IncidenteNegocio();
            Incidente incidente = negocio.ObtenerPorId(id);

            if (!negocio.PuedeVer(incidente, usuario))
                throw new ReglaNegocioException("No tiene permisos para ver esta incidencia.");

            hdnIncidenteId.Value = incidente.Id.ToString();
            txtNroReclamo.Text = incidente.NroReclamo;
            txtClienteDetalle.Text = ObtenerNombreCliente(incidente.Cliente.Nombre, incidente.Cliente.Apellido);
            txtEstadoActual.Text = incidente.EstadoActual.Nombre;
            txtProblematicaDetalle.Text = incidente.DescripcionProblematica;

            CargarCombosDetalle(incidente, usuario);

            dgvComentarios.DataSource = incidente.Comentarios;
            dgvComentarios.DataBind();
        }

        private void CargarCombosDetalle(Incidente incidente, Usuario usuario)
        {
            ddlTipoDetalle.DataSource = new TipoIncidenciaNegocio().ObtenerTodos().Where(x => x.Activo).OrderBy(x => x.Nombre).ToList();
            ddlTipoDetalle.DataTextField = "Nombre";
            ddlTipoDetalle.DataValueField = "Id";
            ddlTipoDetalle.DataBind();
            ddlTipoDetalle.SelectedValue = incidente.TipoIncidencia.Id.ToString();

            ddlPrioridadDetalle.DataSource = new PrioridadNegocio().ObtenerTodos().Where(x => x.Activo).OrderBy(x => x.Nivel).ToList();
            ddlPrioridadDetalle.DataTextField = "Nombre";
            ddlPrioridadDetalle.DataValueField = "Id";
            ddlPrioridadDetalle.DataBind();
            ddlPrioridadDetalle.SelectedValue = incidente.Prioridad.Id.ToString();

            IncidenteNegocio incidenteNegocio = new IncidenteNegocio();
            bool puedeReasignar = incidenteNegocio.PuedeReasignar(usuario);
            pnlAsignacion.Visible = puedeReasignar;
            btnReasignar.Visible = puedeReasignar;

            if (!puedeReasignar)
                return;

            ddlAsignadoDetalle.DataSource = new UsuarioNegocio().ObtenerTodos().Where(x => x.Activo).OrderBy(x => x.Nombre).ThenBy(x => x.Apellido).ToList();
            ddlAsignadoDetalle.DataTextField = "Email";
            ddlAsignadoDetalle.DataValueField = "Id";
            ddlAsignadoDetalle.DataBind();
            AjustarTextoUsuarios();
            ddlAsignadoDetalle.SelectedValue = incidente.UsuarioAsignado.Id.ToString();
        }

        private void AjustarTextoUsuarios()
        {
            var usuarios = new UsuarioNegocio().ObtenerTodos().Where(x => x.Activo).OrderBy(x => x.Nombre).ThenBy(x => x.Apellido).ToList();
            ddlAsignadoDetalle.Items.Clear();
            foreach (Usuario item in usuarios)
            {
                string texto = string.Format("{0} {1} - {2}", item.Nombre, item.Apellido, item.Email).Trim();
                ddlAsignadoDetalle.Items.Add(new ListItem(texto, item.Id.ToString()));
            }
        }

        private void MostrarModal()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showIncidentModal",
                "var incidentModal = new bootstrap.Modal(document.getElementById('incidentModal')); incidentModal.show();", true);
        }

        private string SanitizarJs(string texto)
        {
            return (texto ?? string.Empty).Replace("'", "\\'").Replace("\r\n", " ");
        }
    }
}
