using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_GRUPO_2A
{
    public partial class Usuarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.ObtenerTodos(); 
            dgvUsuarios.DataBind(); 
        }
    }
}