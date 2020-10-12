using Restaurant.Model.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Mantenedores
{
    public partial class Reporteria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
            }
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario.IdTipoUsuario != TipoUsuario.administrador)
            {
                Response.Redirect("../Mantenedores/Inicio.aspx");
            }
        }
    }
}