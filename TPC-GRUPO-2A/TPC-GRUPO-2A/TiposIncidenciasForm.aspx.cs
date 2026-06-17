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
    public partial class TiposIncidenciasForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chkActivo.Checked = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            TipoIncidencia tipoIncidencia = new TipoIncidencia();

            tipoIncidencia.Nombre = txtNombre.Text;
            tipoIncidencia.Descripcion = txtDescripcion.Text;
            tipoIncidencia.Activo = chkActivo.Checked;

            TipoIncidenciaNegocio negocio = new TipoIncidenciaNegocio();
            negocio.Agregar(tipoIncidencia);

            Response.Redirect("TiposIncidencias.aspx");
        }
    }
}